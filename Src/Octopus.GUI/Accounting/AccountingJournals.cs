//Octopus MFS is an integrated suite for managing a Micro Finance Institution: clients, contracts, accounting, reporting and risk
//Copyright ?2006,2007 OCTO Technology & OXUS Development Network
//
//This program is free software; you can redistribute it and/or modify
//it under the terms of the GNU Lesser General Public License as published by
//the Free Software Foundation; either version 2 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU Lesser General Public License for more details.
//
//You should have received a copy of the GNU Lesser General Public License along
//with this program; if not, write to the Free Software Foundation, Inc.,
//51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
//
// Licence : http://www.octopusnetwork.org/OverviewLicence.aspx
//
// Website : http://www.octopusnetwork.org
// Business contact: business(at)octopusnetwork.org
// Technical contact email : tech(at)octopusnetwork.org 

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Octopus.CoreDomain;
using Octopus.CoreDomain.Accounting;
using Octopus.CoreDomain.Contracts.Loans;
using Octopus.CoreDomain.Contracts.Savings;
using Octopus.CoreDomain.Events;
using Octopus.CoreDomain.Events.Loan;
using Octopus.CoreDomain.Events.Saving;
using Octopus.Enums;
using Octopus.ExceptionsHandler;
using Octopus.GUI.UserControl;
using Octopus.MultiLanguageRessources;
using Octopus.Services;
using Octopus.Shared;
using Octopus.Shared.Settings;

namespace Octopus.GUI.Accounting
{
    public partial class AccountingJournals : SweetBaseForm
    {
        private List<Loan> _listLoan;
        private List<ISavingsContract> _savings;
        private readonly EventStock _generatedEvents =  new EventStock();
        private ClosureItems _closureItems;
        private readonly AccountingClosure _accountingClosure;
        private DateTime _beginDate;
        private DateTime _endDate;
        private Branch _branch;
        private ClosureOptions _closureOptions;
        private readonly int _mode;
        private readonly EventStock _eventStock = new EventStock();
        private List<Booking> _bookings = new List<Booking>();

        [DllImport("user32.dll")]
        static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);

        [DllImport("user32.dll")]
        static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        internal const UInt32 SC_CLOSE = 0xF060;
        internal const UInt32 MF_ENABLED = 0x00000000;
        internal const UInt32 MF_GRAYED = 0x00000001;
        internal const UInt32 MF_DISABLED = 0x00000002;
        internal const uint MF_BYCOMMAND = 0x00000000;

        public static void EnableCloseButton(IWin32Window window, bool bEnabled)
        {
            IntPtr hSystemMenu = GetSystemMenu(window.Handle, false);
            EnableMenuItem(hSystemMenu, SC_CLOSE, (MF_ENABLED | (bEnabled ? MF_ENABLED : MF_GRAYED)));
        }

        private delegate void UpdateStatusDelegate(string status, string info);

        public AccountingJournals()
        {
            InitializeComponent();
            _accountingClosure = new AccountingClosure();
        }

        public AccountingJournals(int mode)
        {
            InitializeComponent();
            _accountingClosure = new AccountingClosure();
            _mode = mode;
        }

        private static void SortBookingsByDate(List<Booking> list)
        {
            list.Sort((x, y) => x.Date.CompareTo(y.Date));
        }

        private void GenerateSavingsClosureEvents()
        {
            UpdateStatus("SelectSavingsContracts", "");
            _savings = ServicesProvider.GetInstance().GetSavingServices().SelectActiveContracts();
            List<SavingEvent> list = new List<SavingEvent>();
            foreach (ISavingsContract savingsContract in _savings)
            {
                UpdateStatus("SavingEventsGeneration", " ->" + savingsContract.Code);
                list.AddRange(savingsContract.Closure(TimeProvider.Now, User.CurrentUser));
            }
            _generatedEvents.AddRange(list);
        }

        private void GetBookings(ClosureOptions options)
        {
            List<Booking> bookings = new List<Booking>();
            EventStock eventStock = new EventStock();

            AccountingRuleCollection rules = ServicesProvider.GetInstance().GetAccountingRuleServices().SelectAll();
            rules.SortByOrder();

            if (options.DoLoanClosure)
            {
                UpdateStatus("LoanClosureProcessing", "");
                eventStock =
                    ServicesProvider.GetInstance().GetEventProcessorServices().SelectEventsForClosure(_beginDate, _endDate, _branch);
                UpdateStatus("LoanClosureProcessing", eventStock.GetEvents().Count.ToString());
               //add generated events for processing
            }

            if (options.DoSavingClosure)
            {
                UpdateStatus("SavingsClosureProcessing", "");
                
                eventStock.AddRange(
                    ServicesProvider.GetInstance().GetSavingServices().SelectEventsForClosure(
                    _beginDate, _endDate, _branch));

                UpdateStatus("SavingsClosureProcessing", eventStock.GetEvents().Count.ToString());
            }

            if (options.DoTellerManagementClosure)
            {
                UpdateStatus("TellerManagementProcessing", "");
                eventStock.AddRange(ServicesProvider.GetInstance().GetEventProcessorServices().GetTellerEventsForClosure(
                    _beginDate, _endDate));
                UpdateStatus("EventClosureProcessing", eventStock.GetEvents().Count.ToString());
            }
            
            //important to have sorted list
            eventStock.SortEventsById();
            //closure procesing
            timerClosure.Start();
            timerClosure.Enabled = true;

            //set ex rate    
            List<ExchangeRate> rates =
                    ServicesProvider.GetInstance().GetExchangeRateServices().SelectRatesByDate(_beginDate, _endDate);

            List<CoreDomain.Accounting.FiscalYear> fiscalYears =
                ServicesProvider.GetInstance().GetChartOfAccountsServices().SelectFiscalYears();

            bookings.AddRange(
                _accountingClosure.GetBookings(
                rules, 
                eventStock,
                ServicesProvider.GetInstance().GetTellerServices().FindAllTellers(),
                ServicesProvider.GetInstance().GetPaymentMethodServices().GetAllPaymentMethodsForClosure(), 
                rates,
                fiscalYears));

            timerClosure.Stop();
            timerClosure.Enabled = false;

            //manual transactions
            if (options.DoManualEntries)
            {
                bookings.AddRange(ServicesProvider.GetInstance().GetAccountingServices().SelectMovements(false, rates,
                                                                                                         fiscalYears));
            }

            #region Reversal
            if (options.DoReversalTransactions)
            {
                UpdateStatus("ReversalTransactionProcessing", "");
                bookings.AddRange(ServicesProvider.GetInstance().GetAccountingServices().SelectMovementsForReverse(
                    rates,
                    fiscalYears));
            }
            #endregion

            //add reversal provision booking 
            if (options.DoLoanClosure)
            {
                bookings.AddRange(
                    _generatedEvents.GetProvisionEvents().Select(
                        provisionEvent =>
                        ServicesProvider.GetInstance().GetAccountingServices().SelectProvisionMovments(
                            provisionEvent.ContracId, rates,
                            fiscalYears)).Where(b => b != null));
            }

            SortBookingsByDate(bookings);

            FillListView(bookings);
        }

        private void FillListView(IEnumerable<Booking> bookings)
        {
            olvColumn_EventId.GroupKeyGetter = delegate(object rowObject)
                                                   {
                                                       return ((Booking) rowObject).Name;
                                                   };

            olvColumn_EventId.GroupKeyToTitleConverter = delegate(object groupKey)
                                                             {
                                                                 return groupKey.ToString();
                                                             };

            olvColumn_Amount.AspectToStringConverter = delegate(object value)
                                                           {
                                                               if (value.ToString().Length > 0)
                                                               {
                                                                   return ((OCurrency)value).GetFormatedValue(true);
                                                               }
                                                               return null;
                                                           };

            olvColumn_TransactionDate.AspectToStringConverter = delegate(object value)
                                                                    {
                                                                        if (value.ToString().Length > 0)
                                                                        {
                                                                            return ((DateTime)value).ToString();
                                                                        }
                                                                        return null;
                                                                    };
            UpdateStatus("", "loading list ...");
            olvBookings.SetObjects(bookings);
            UpdateStatus("", "Done");
        }

        private void InitializeComboBoxBranches()
        {
            List<Branch> branches = new List<Branch>
                                        {
                                            new Branch
                                                {
                                                    Id = 0,
                                                    Code =
                                                        MultiLanguageStrings.GetString(Ressource.AccountingRule,
                                                                                       "All.Text"),
                                                    Description =
                                                        MultiLanguageStrings.GetString(Ressource.AccountingRule,
                                                                                       "All.Text"),
                                                    Name =
                                                        MultiLanguageStrings.GetString(Ressource.AccountingRule,
                                                                                       "All.Text")
                                                }
                                        };

            branches.AddRange(ServicesProvider.GetInstance().GetBranchService().FindAllNonDeleted());

            cmbBranches.Items.Clear();
            cmbBranches.ValueMember = "Id";
            cmbBranches.DisplayMember = "";
            cmbBranches.DataSource = branches;
        }

        private void AccountingJournalsLoad(object sender, EventArgs e)
        {
            InitializeComboBoxBranches();
            InitializeClosureOptions();
            dateTimePickerBeginDate.Value = TimeProvider.Now.Date;
            dateTimePickerEndDate.Value = TimeProvider.Now.Date;

            if(_mode == 0)
            {
                tbcMain.TabPages.Remove(tbpEvents);
            }

            if(_mode == 1)
            {
                tbcMain.TabPages.Remove(tbpMovements);
            }

            decimal v;
            int overdueDays;

            olvColumn_Amnt.AspectToStringConverter = delegate(object value)
            {
                if (value != null && decimal.TryParse(value.ToString(), out v))
                {
                    return ((OCurrency)value).GetFormatedValue(true);
                }
                return null;
            };

            olvColumn_OLB.AspectToStringConverter = delegate(object value)
            {
                if (value != null && decimal.TryParse(value.ToString(), out v))
                {
                    return ((OCurrency)value).GetFormatedValue(true);
                }
                return null;
            };

            olvColumn_Interest.AspectToStringConverter = delegate(object value)
            {
                if (value != null && decimal.TryParse(value.ToString(), out v))
                {
                    return ((OCurrency)value).GetFormatedValue(true);
                }
                return null;
            };

            olvColumn_AccruedInterest.AspectToStringConverter = delegate(object value)
            {
                if (value != null && decimal.TryParse(value.ToString(), out v))
                {
                    return ((OCurrency)value).GetFormatedValue(true);
                }
                return null;
            };

            olvColumn_OverduePrincipal.AspectToStringConverter = delegate(object value)
            {
                if (value != null && decimal.TryParse(value.ToString(), out v))
                {
                    return ((OCurrency)value).GetFormatedValue(true);
                }
                return null;
            };

            olvColumn_Fee.AspectToStringConverter = delegate(object value)
            {
                if (value != null && decimal.TryParse(value.ToString(), out v))
                {
                    return ((OCurrency)value).GetFormatedValue(true);
                }
                return null;
            };

            olvColumn_OverdueDays.AspectToStringConverter = delegate(object value)
            {
                if (value != null && int.TryParse(value.ToString(), out overdueDays))
                {
                    return value.ToString();
                }
                return null;
            };

            olvColumn_Date.AspectToStringConverter = delegate(object value)
            {
                if (value != null && value.ToString().Length > 0)
                {
                    return ((DateTime)value).ToString();
                }
                return null;
            };
        }

        public void LateLoanProcessing()
        {
            foreach (Loan loan in _listLoan)
            {
                UpdateStatus("OverdueEventsProcessingForContract", " ->" + loan.Code);
                OverdueEvent e = loan.GetOverdueEvent(TimeProvider.Now);

                if (e != null)
                {
                    _generatedEvents.Add(e);
                }
            }
        }

        public void ProvisionLoanProcessing()
        {
            ProvisionTable provisionTable = ProvisionTable.GetInstance(User.CurrentUser);
            foreach (Loan loan in _listLoan)
            {
                _accountingClosure.ClosureStatus = "ProvisionIsBeingProcessedForContract";
                _accountingClosure.ClosureStatusInfo = " ->" + loan.Code;
                ProvisionEvent e = loan.GetProvisionEvent(TimeProvider.Now, provisionTable);

                if (e != null)
                {
                   _generatedEvents.Add(e);
                }
            }
        }

        public void AccruedInterestLoanProcessing()
        {
            if (ServicesProvider.GetInstance().GetGeneralSettings().AccountingProcesses == OAccountingProcesses.Accrual)
            {
                foreach (Loan loan in _listLoan)
                {
                    UpdateStatus("AccruedInterestIsBeingProcessedForContract", " ->" + loan.Code);
                    AccruedInterestEvent e = loan.GetAccruedInterestEvent(TimeProvider.Now);

                    if (e != null)
                    {
                        _generatedEvents.Add(e);
                    }
                }
            }
        }

        private void UpdateStatus(string status, string info)
        {
            if (InvokeRequired)
            {
                Invoke(new UpdateStatusDelegate(UpdateStatus), new object[] { status, info });
                return;                
            }
            
            Text = GetString(status + ".Text") + @" " + info;
        }

        private void GenerateEvents(ClosureOptions options)
        {
            if (options.DoOverdue || options.DoProvision || options.DoAccrued || options.DoLoanClosure)
            {
                UpdateStatus("PerformingClosure", "");
                _listLoan = ServicesProvider.GetInstance().GetContractServices().SelectContractsForClosure();
                UpdateStatus("", "Contracts -> " + _listLoan.Count.ToString());
            }
            //generate overdue events
            if (options.DoOverdue)
            {
                UpdateStatus("OverdueEventsProcessing", "");
                LateLoanProcessing();
            }
            //generate provision events
            if (options.DoProvision)
            {
                UpdateStatus("ProvisionEventsProcessing", "");
                ProvisionLoanProcessing();
            }
            //generate accrued interest events
            if (options.DoAccrued)
            {
                UpdateStatus("AccruedInterestEventsProcessing", "");
                AccruedInterestLoanProcessing();
            }

            if (options.DoSavingEvents)
            {
                UpdateStatus("SavingEventsGeneration", "");
                GenerateSavingsClosureEvents();
            }

            UpdateStatus("", "Loding list ...");
            olvEvents.SetObjects(_generatedEvents);
            UpdateStatus("", "Done");
        }
        
        private void InitializeClosureOptions()
        {
            _closureItems = new ClosureItems();

            if (_mode == 1)
            {
                _closureItems.Items.Add(new ClosureOption {Name = "Credit: accrued interests", Id = 0});
                _closureItems.Items.Add(new ClosureOption {Name = "Credit: overdue processing", Id = 1});
                _closureItems.Items.Add(new ClosureOption {Name = "Credit: loan loss provision", Id = 2});
                _closureItems.Items.Add(new ClosureOption {Name = "Savings: events generation", Id = 3});
                btnView.Visible = false;
            }

            if (_mode == 0)
            {
                _closureItems.Items.Add(new ClosureOption {Name = "Credit + Savings: cancelled transactions", Id = 0});
                _closureItems.Items.Add(new ClosureOption {Name = "Credit: accounting closure", Id = 1});
                _closureItems.Items.Add(new ClosureOption {Name = "Savings: accounting closure", Id = 2});
                _closureItems.Items.Add(new ClosureOption {Name = "Manual entries", Id = 3 });
                _closureItems.Items.Add(new ClosureOption {Name = "Teller management", Id = 4});
            }

            clbxFields.Items.AddRange(_closureItems.Items.ToArray());
        }

        private void BtnRunClick(object sender, EventArgs e)
        {
            _beginDate = dateTimePickerBeginDate.Value.Date;
            _endDate = dateTimePickerEndDate.Value.Date;
            _branch = ((Branch)cmbBranches.SelectedItem);
            
            btnPreview.Enabled = false;
            dateTimePickerBeginDate.Enabled = false;
            dateTimePickerEndDate.Enabled = false;
            cmbBranches.Enabled = false;
            clbxFields.Enabled = false;
            btnPost.Enabled = false;
            btnView.Enabled = false;
            //DisableX();

            EnableCloseButton(this, false);

            if (_mode == 0)
            {
                _closureOptions = new ClosureOptions
                                      {
                                          DoAccrued = false,
                                          DoOverdue = false,
                                          DoProvision = false,
                                          DoSavingEvents = false,
                                          DoReversalTransactions =
                                              clbxFields.CheckedItems.Contains(_closureItems.Items[0]),
                                          DoLoanClosure = clbxFields.CheckedItems.Contains(_closureItems.Items[1]),
                                          DoSavingClosure = clbxFields.CheckedItems.Contains(_closureItems.Items[2]),
                                          DoManualEntries = clbxFields.CheckedItems.Contains(_closureItems.Items[3]),
                                          DoTellerManagementClosure = clbxFields.CheckedItems.Contains(_closureItems.Items[4])
                                      };
            }

            if (_mode == 1)
            {
                //generaion of events
                _closureOptions = new ClosureOptions
                                      {
                                          DoAccrued = clbxFields.CheckedItems.Contains(_closureItems.Items[0]),
                                          DoOverdue = clbxFields.CheckedItems.Contains(_closureItems.Items[1]),
                                          DoProvision = clbxFields.CheckedItems.Contains(_closureItems.Items[2]),
                                          DoSavingEvents = clbxFields.CheckedItems.Contains(_closureItems.Items[3]),
                                          DoReversalTransactions = false,
                                          DoLoanClosure = false,
                                          DoSavingClosure = false,
                                          DoManualEntries = false
                                      };
            }

            bwRun.RunWorkerAsync();
        }

        private void CbAllEventsCheckedChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in olvEvents.Items)
            {
                item.Checked = cbAllEvents.Checked;
            }
        }

        private void CbxAllBookingsCheckedChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in olvBookings.Items)
            {
                item.Checked = cbxAllBookings.Checked;
            }
        }

        private void TimerClosureTick(object sender, EventArgs e)
        {
            UpdateStatus(_accountingClosure.ClosureStatus, _accountingClosure.ClosureStatusInfo);
        }

        private void BwRunDoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                btnPreview.StartProgress();
                if (_mode == 0)
                    GetBookings(_closureOptions);
                
                if(_mode == 1)
                    GenerateEvents(_closureOptions);

                btnPreview.StopProgress();
                
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
        }

        private void BtnPostClick(object sender, EventArgs e)
        {
            btnPost.Enabled = false;
            EnableCloseButton(this, false);

            try
            {
                if (_mode == 0)
                {
                    _bookings = olvBookings.CheckedObjects.Cast<Booking>().ToList();
                    if (_bookings.Count > 0)
                    {
                        bwPostBookings.RunWorkerAsync();
                    }
                    else
                    {
                        MessageBox.Show(@"Please select items !");
                        btnPost.Enabled = true;
                        EnableCloseButton(this, true);
                    }
                }

                if (_mode == 1)
                {
                    foreach (Event evt in olvEvents.CheckedObjects)
                    {
                        _eventStock.Add(evt);
                    }

                    if (_eventStock.GetEvents().Count > 0)
                    {
                        bwPostEvents.RunWorkerAsync();
                    }
                    else
                    {
                        MessageBox.Show(@"Please select items !");
                        EnableCloseButton(this, true);
                        btnPost.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
        }

        private void BtnViewClick(object sender, EventArgs e)
        {
            TrialBalancePreview trialBalance = new TrialBalancePreview(olvBookings.CheckedObjects.Cast<Booking>().ToList());
            trialBalance.ShowDialog();
        }

        private void BwPostEventsDoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                btnPost.StartProgress();
                UpdateStatus("", "Posting ..." + _eventStock.GetEvents().Count.ToString());
                ServicesProvider.GetInstance().GetContractServices().PostEvents(_listLoan, _eventStock);
                //savings

                UpdateStatus("", "Posting ..." + _eventStock.GetSavingEvents().Count.ToString());
                ServicesProvider.GetInstance().GetSavingServices().PostEvents(_savings, _eventStock.GetSavingEvents());

                btnPost.StopProgress();
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
        }

        private void BwPostBookingsDoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                btnPost.StartProgress();
                UpdateStatus("", "Posting ..." + _bookings.Count);
                ServicesProvider.GetInstance().GetAccountingServices().DoMovements(_bookings);
                btnPost.StopProgress();
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
        }

        private void BwRunRunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            btnPost.Enabled = true;
            cbAllEvents.Checked = true;
            btnView.Enabled = true;
            EnableCloseButton(this, true);

            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(UserSettings.Language);
            MessageBox.Show(GetString("DataGenerated.Text") + @"!");
        }

        private void BwPostEventsRunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(UserSettings.Language);
            MessageBox.Show(@"Events have been added !");
            EnableCloseButton(this, true);
        }

        private void BwPostBookingsRunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            btnView.Enabled = true;
            EnableCloseButton(this, true);

            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(UserSettings.Language);
            MessageBox.Show(@"Bookings have been posted !");
        }
    }
}
