using System;
using System.Diagnostics;
using System.Windows.Forms;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.CoreDomain.SearchResult;
using OpenCBS.CoreDomain.Contracts.Savings;
using OpenCBS.CoreDomain.Products;
using OpenCBS.Enums;
using OpenCBS.ExceptionsHandler;
using OpenCBS.ExceptionsHandler.Exceptions.SavingExceptions;
using OpenCBS.Services;
using OpenCBS.Shared;
using OpenCBS.GUI.UserControl;
using OpenCBS.GUI.Tools;
using OpenCBS.MultiLanguageRessources;
using OpenCBS.GUI.UserControl;

namespace OpenCBS.GUI.Contracts
{
    public partial class SavingsOperationForm : SweetBaseForm
    {
        #region Fields
        private OCurrency _amount = 0;
        private string _description;
        private readonly OSavingsOperation _bookingDirection;
        private readonly ISavingsContract _saving;
        private DateTime _date = TimeProvider.Now;
        private ISavingsContract _savingTarget;
        private OCurrency _amountMin;
        private OCurrency _amountMax;
        private OCurrency _chequeAmountMin;
        private OCurrency _chequeAmountMax;
        
        private OCurrency _flatFees = null;
        private double? _rateFees;

        private OCurrency _feesMin;
        private OCurrency _feesMax;

        private string _pendingSavingsMode;
        #endregion

        public SavingsOperationForm(ISavingsContract pSaving, OSavingsOperation pDirections)
        {
            InitializeComponent();
            _bookingDirection = pDirections;
            _saving = pSaving;
            SwitchBookingDirection();
            Initialize();
        }

        private void SwitchBookingDirection()
        {
            switch (_bookingDirection)
            {
                case OSavingsOperation.Credit:
                    {
                        _amountMin = _saving.Product.DepositMin;
                        _amountMax = _saving.Product.DepositMax;
                        _chequeAmountMin = _saving.Product.ChequeDepositMin;
                        _chequeAmountMax = _saving.Product.ChequeDepositMax;

                        if (_saving.Product is SavingsBookProduct)
                        {
                            if (((SavingsBookProduct) _saving.Product).DepositFees.HasValue)
                            {
                                _feesMin = ((SavingsBookProduct) _saving.Product).DepositFees;
                                _feesMax = ((SavingsBookProduct) _saving.Product).DepositFees;
                            }
                            else
                            {
                                _feesMin = ((SavingsBookProduct) _saving.Product).DepositFeesMin;
                                _feesMax = ((SavingsBookProduct) _saving.Product).DepositFeesMax;
                            }
                        }
                        break;
                    }
                case OSavingsOperation.Debit:
                    {
                        _amountMin = _saving.Product.WithdrawingMin;
                        _amountMax = _saving.Product.WithdrawingMax;
                        _chequeAmountMin = _saving.Product.ChequeDepositMin;
                        _chequeAmountMax = _saving.Product.ChequeDepositMax;

                        if (_saving.Product is SavingsBookProduct)
                        {
                            if (((SavingsBookProduct) _saving.Product).FlatWithdrawFees.HasValue ||
                                ((SavingsBookProduct) _saving.Product).FlatWithdrawFeesMin.HasValue)
                            {
                                if (((SavingsBookProduct) _saving.Product).FlatWithdrawFees.HasValue)
                                {
                                    _feesMin = ((SavingsBookProduct) _saving.Product).FlatWithdrawFees;
                                    _feesMax = ((SavingsBookProduct) _saving.Product).FlatWithdrawFees;
                                }
                                else
                                {
                                    _feesMin = ((SavingsBookProduct) _saving.Product).FlatWithdrawFeesMin;
                                    _feesMax = ((SavingsBookProduct) _saving.Product).FlatWithdrawFeesMax;
                                }
                            }
                            else
                            {
                                if (((SavingsBookProduct) _saving.Product).RateWithdrawFees.HasValue)
                                {
                                    _feesMin = (decimal) ((SavingsBookProduct) _saving.Product).RateWithdrawFees*100;
                                    _feesMax = (decimal) ((SavingsBookProduct) _saving.Product).RateWithdrawFees*100;
                                }
                                else
                                {
                                    _feesMin = (decimal) ((SavingsBookProduct) _saving.Product).RateWithdrawFeesMin*100;
                                    _feesMax = (decimal) ((SavingsBookProduct) _saving.Product).RateWithdrawFeesMax*100;
                                }
                            }
                        }
                        break;
                    }

                case OSavingsOperation.Transfer:
                    _amountMin = _saving.Product.TransferMin;
                    _amountMax = _saving.Product.TransferMax;

                    LoadTransferFee();
                    break;

                default:
                    _amountMin = 0;
                    _amountMax = decimal.MaxValue;

                    break;
            }
        }

        private void Initialize()
        {
            lblSavingCurrency.Text = _saving.Product.Currency.Code;
            lblTotalSavingCurrency.Text = _saving.Product.Currency.Code;
            lblSavingCurrencyFees.Text = _saving.Product.Currency.Code;

            nudTotalAmount.Minimum = 0;
            nudTotalAmount.Maximum = decimal.MaxValue;

            nudAmount.Minimum = _amountMin.Value;
            nudAmount.Maximum = _amountMax.Value;

            lbAmountMinMax.Text = string.Format("{0} {1} {4}\n\r{2} {3} {4}",
                   "min", _amountMin.GetFormatedValue(_saving.Product.Currency.UseCents),
                   "max", _amountMax.GetFormatedValue(_saving.Product.Currency.UseCents),
                   _saving.Product.Currency.Code);

            lblAmountFeesMinMax.Text = string.Format("{0} {1} {4}\n\r{2} {3} {4}",
                   "min", _feesMin.GetFormatedValue(_saving.Product.Currency.UseCents),
                   "max", _feesMax.GetFormatedValue(_saving.Product.Currency.UseCents),
                   _saving.Product.Currency.Code);

            switch (_bookingDirection)
            {
                case OSavingsOperation.Credit:
                    {
                        Text = MultiLanguageStrings.GetString(Ressource.FrmAddSavingEvent, "Deposit.Text");
                        Name = MultiLanguageStrings.GetString(Ressource.FrmAddSavingEvent, "Deposit.Text");
                        btnSave.Text = MultiLanguageStrings.GetString(Ressource.FrmAddSavingEvent, "Deposit.Text");
                        tbxSavingCode.Text = MultiLanguageStrings.GetString(Ressource.FrmAddSavingEvent, "Deposit.Text");
                        plTransfer.Visible = false;
                        pnlSavingPending.Visible = true;
                        rbxDebit.Visible = false;
                        rbxCredit.Visible = false;
                        cbBookings.Visible = false;
                        break;
                    }
                case OSavingsOperation.Debit:
                    {
                        Text = MultiLanguageStrings.GetString(Ressource.FrmAddSavingEvent, "Withdrawal.Text");
                        Name = MultiLanguageStrings.GetString(Ressource.FrmAddSavingEvent, "Withdrawal.Text");
                        btnSave.Text = MultiLanguageStrings.GetString(Ressource.FrmAddSavingEvent, "Withdrawal.Text");
                        tbxSavingCode.Text = MultiLanguageStrings.GetString(Ressource.FrmAddSavingEvent, "Withdrawal.Text");
                        plTransfer.Visible = false;
                        pnlSavingPending.Visible = false;
                        cbSavingsMethod.Visible = true;
                        rbxDebit.Visible = false;
                        rbxCredit.Visible = false;
                        cbBookings.Visible = false;
                        break;
                    }
                case OSavingsOperation.Transfer:
                    {
                        Text = MultiLanguageStrings.GetString(Ressource.FrmAddSavingEvent, "Transfer.Text");
                        Name = MultiLanguageStrings.GetString(Ressource.FrmAddSavingEvent, "Transfer.Text");
                        btnSave.Text = MultiLanguageStrings.GetString(Ressource.FrmAddSavingEvent, "Transfer.Text");
                        tbxSavingCode.Text = MultiLanguageStrings.GetString(Ressource.FrmAddSavingEvent, "Transfer.Text");
                        plTransfer.Visible = true;
                        tbTargetAccount.Visible = true;
                        btnSearchContract.Visible = true;
                        pnlSavingPending.Visible = false;
                        lbTargetSavings.Visible = true;
                        lblClientName.Visible = true;
                        rbxDebit.Visible = false;
                        rbxCredit.Visible = false;
                        cbBookings.Visible = false;
                        cbSavingsMethod.Visible = false;
                        break;
                    }
                case OSavingsOperation.SpecialOperation:
                    {
                        Text = MultiLanguageStrings.GetString(Ressource.FrmAddSavingEvent, "SpecialOperation.Text");
                        Name = MultiLanguageStrings.GetString(Ressource.FrmAddSavingEvent, "SpecialOperation.Text");
                        btnSave.Text = MultiLanguageStrings.GetString(Ressource.FrmAddSavingEvent, "ComfirmOperation.Text");
                        tbxSavingCode.Text = MultiLanguageStrings.GetString(Ressource.FrmAddSavingEvent, "SpecialOperation.Text");
                        lblPaymentMethod.Text = MultiLanguageStrings.GetString(Ressource.FrmAddSavingEvent, "StandardBooking.Text");
                        plTransfer.Visible = true;
                        pnlSavingPending.Visible = true;
                        cbxPending.Visible = false;
                        cbSavingsMethod.Visible = false;
                        lblAmountFeesMinMax.Visible = false;
                        lbAmountMinMax.Visible = false;
                        rbxDebit.Visible = true;
                        rbxCredit.Visible = true;
                        btnSearchContract.Visible = false;
                        tbTargetAccount.Visible = false;
                        lblClientName.Visible = false;
                        updAmountFees.Visible = false;
                        lblSavingCurrencyFees.Visible = false;
                        lblFees.Visible = false;
                        lblTotalAmount.Visible = false;
                        nudTotalAmount.Visible = false;
                        lblTotalSavingCurrency.Visible = false;
                        lbAmountMinMaxCurrencyPivot.Visible = false;

                        cbBookings.Items.Clear();
                        foreach (Booking booking in ServicesProvider.GetInstance().GetStandardBookingServices().SelectAllStandardBookings())
                        {
                            cbBookings.Items.Add(booking);
                        }
                        cbBookings.Visible = true;

                        _feesMin = 0;
                        _feesMax = 0;
                        _rateFees = 0;

                        break;
                    }
                default: break;
            }

            /*dtpDate.MinDate = _saving.GetLastPostingDate();
            if (_saving.GetLastPostingDate() <= TimeProvider.Today)
                dtpDate.MaxDate = TimeProvider.Today;*/

            dtpDate.Value = new DateTime(dtpDate.Value.Year, dtpDate.Value.Month, dtpDate.Value.Day, TimeProvider.Now.Hour, TimeProvider.Now.Minute, TimeProvider.Now.Second);

            _pendingSavingsMode = ServicesProvider.GetInstance().GetGeneralSettings().PendingSavingsMode.ToUpper();
//             cbSavingsMethod.DataBind(typeof(OPaymentMethods), Ressource.SavingsOperationForm, false);
             cbSavingsMethod.DataBind(typeof(OSavingsMethods), Ressource.FrmAddSavingEvent, false);

             switch (_bookingDirection)
             {
                 case OSavingsOperation.Credit:
                     {
                         switch (cbSavingsMethod.SelectedValue.ToString())
                         {
                             case "Cheque":
                                 _flatFees = ((SavingBookContract)_saving).ChequeDepositFees;
                                 break;
                             default:
                                 _flatFees = ((SavingBookContract)_saving).DepositFees;
                                 break;
                         }
                         break;
                     }
                 case OSavingsOperation.Debit:
                     {
                         if (((SavingBookContract)_saving).FlatWithdrawFees.HasValue)
                             _flatFees = ((SavingBookContract)_saving).FlatWithdrawFees;
                         else
                             _rateFees = ((SavingBookContract)_saving).RateWithdrawFees;
                         break;
                     }
                 case OSavingsOperation.Transfer:
                     {
                         if (((SavingBookContract)_saving).FlatTransferFees.HasValue)
                             _flatFees = ((SavingBookContract)_saving).FlatTransferFees;
                         else
                             _rateFees = ((SavingBookContract)_saving).RateTransferFees;
                         break;
                     }
             }

            updAmountFees.Minimum = _feesMin.Value;
            updAmountFees.Maximum = _feesMax.Value;
            
            decimal value = _flatFees.HasValue ? _flatFees.Value : ((decimal)(_rateFees)) * 100;
            updAmountFees.Value = value < updAmountFees.Minimum || value > updAmountFees.Maximum
                                      ? updAmountFees.Minimum
                                      : value;
        }

        private void TbSavingCodeTextChanged(object sender, EventArgs e)
        {
            _description = tbxSavingCode.Text;
        }

        private void BSaveClick(object sender, EventArgs e)
        {
            nudAmount_ValueChanged(sender, e);
            bool pending = cbxPending.Visible && cbxPending.Checked;
            OSavingsMethods savingsMethod =
                (OSavingsMethods) Enum.Parse(typeof (OSavingsMethods), cbSavingsMethod.SelectedValue.ToString());
            try
            {
                _date = new DateTime(dtpDate.Value.Year, dtpDate.Value.Month, dtpDate.Value.Day, DateTime.Now.Hour,
                                     DateTime.Now.Minute, DateTime.Now.Second);

                SavingServices savingServices = ServicesProvider.GetInstance().GetSavingServices();

                if (_date.Date < DateTime.Today.Date)
                    savingServices.PerformBackDateOperations(_date);
                else if (_date.Date > DateTime.Today.Date)
                    savingServices.PerformFutureDateOperations(_date);

                if (_saving.HasPendingEvents())
                    if (!savingServices.AllowOperationsDuringPendingDeposit())
                        return;

                if ((_flatFees.HasValue && updAmountFees.Value != _flatFees) || (_rateFees.HasValue && updAmountFees.Value != (decimal)(_rateFees * 100)))
                    if (!savingServices.AllowSettingSavingsOperationsFeesManually())
                        return;

                switch (_bookingDirection)
                {
                    case OSavingsOperation.Credit:
                        {
                            if (_saving is SavingBookContract)
                            {
                                if (savingsMethod == OSavingsMethods.Cheque)
                                    ((SavingBookContract)_saving).ChequeDepositFees = updAmountFees.Value;
                                else ((SavingBookContract) _saving).DepositFees = updAmountFees.Value;
                            }

                            savingServices.Deposit(_saving, _date, _amount, _description, User.CurrentUser, pending,
                                                   savingsMethod, null, Teller.CurrentTeller);
                            break;
                        }
                    case OSavingsOperation.Debit:
                        {
                            if (_saving is SavingBookContract)
                            {
                                if (_flatFees.HasValue) ((SavingBookContract)_saving).FlatWithdrawFees = updAmountFees.Value;
                                else ((SavingBookContract)_saving).RateWithdrawFees = (double) (updAmountFees.Value / 100);
                            }

                            savingServices.Withdraw(_saving, _date, _amount, _description, User.CurrentUser, Teller.CurrentTeller);
                            break;
                        }
                    case OSavingsOperation.Transfer:
                        {
                            if (_saving is SavingBookContract)
                            {
                                if (_flatFees.HasValue) ((SavingBookContract)_saving).FlatTransferFees = updAmountFees.Value;
                                else ((SavingBookContract)_saving).RateTransferFees = (double)(updAmountFees.Value / 100);
                            }
                            decimal fee = nudTotalAmount.Value - nudAmount.Value;
                            savingServices.Transfer(_saving, _savingTarget, _date, _amount, fee, _description, User.CurrentUser, false);
                            break;
                        }

                    case OSavingsOperation.SpecialOperation:
                        {
                            if (cbBookings.SelectedItem != null)
                            {
                                Booking booking = (Booking) cbBookings.SelectedItem;
                                booking.Branch = _saving.Branch;
                                savingServices.SpecialOperation(_saving, _date, _amount, _description, User.CurrentUser,
                                                                savingsMethod, rbxCredit.Checked, booking);
                                break;
                            }
                            throw new OctopusSavingException(OctopusSavingExceptionEnum.TransactionInvalid);
                        }
                }
               
                Close();
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btSearchContract_Click(object sender, EventArgs e)
        {
            SearchCreditContractForm searchCreditContractForm = SearchCreditContractForm.GetInstance(null);
            searchCreditContractForm.BringToFront();
            searchCreditContractForm.WindowState = FormWindowState.Normal;
            if (searchCreditContractForm.ShowForSearchSavingsContractForTransfer("") == DialogResult.OK)
            {
                SavingSearchResult saving = searchCreditContractForm.SelectedSavingContract;
                lblClientName.Text = saving.ClientName;
                tbTargetAccount.Text = saving.ContractCode;

                _savingTarget = ServicesProvider.GetInstance().GetSavingServices().GetSaving(saving.Id);
                lblInterBranch.Visible = !IsNormalTransfer();
                LoadTransferFee();
            }
        }

        private void comboBoxSavingsMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbxPending.Visible = (_pendingSavingsMode.Contains(cbSavingsMethod.SelectedValue.ToString().ToUpper()));

            switch (_bookingDirection)
            {
                case OSavingsOperation.Credit:
                    switch (cbSavingsMethod.SelectedValue.ToString())
                    {
                        case "Cheque":
                            if (((SavingsBookProduct)_saving.Product).ChequeDepositFeesMin.HasValue)
                            {
                                _feesMin = ((SavingsBookProduct)_saving.Product).ChequeDepositFeesMin;
                                _feesMax = ((SavingsBookProduct)_saving.Product).ChequeDepositFeesMax;
                                _flatFees = _feesMin;   
                            }
                            else
                            {
                                _flatFees = ((SavingBookContract)_saving).ChequeDepositFees.Value;
                                _feesMin = _feesMax = _flatFees;
                            }

                            updAmountFees.Minimum = _feesMin.Value;
                            updAmountFees.Maximum = _feesMax.Value;
                            updAmountFees.Text = ((SavingBookContract)_saving).ChequeDepositFees.GetFormatedValue(_saving.Product.Currency.UseCents);

                            nudAmount_ValueChanged(nudAmount, e);

                            lbAmountMinMax.Text = string.Format("{0} {1} {4}\n\r{2} {3} {4}",
                                                        "min", _chequeAmountMin.GetFormatedValue(_saving.Product.Currency.UseCents),
                                                        "max", _chequeAmountMax.GetFormatedValue(_saving.Product.Currency.UseCents),
                                                        _saving.Product.Currency.Code);

                            lblAmountFeesMinMax.Text = string.Format("{0} {1} {4}\n\r{2} {3} {4}",
                                                        "min", _feesMin.GetFormatedValue(_saving.Product.Currency.UseCents),
                                                        "max", _feesMax.GetFormatedValue(_saving.Product.Currency.UseCents),
                                                        _saving.Product.Currency.Code);

                            break;
                        default:
                            if (_saving is SavingBookContract)
                            {
                                if (((SavingsBookProduct)_saving.Product).DepositFeesMin.HasValue)
                                {
                                    _feesMin = ((SavingsBookProduct)_saving.Product).DepositFeesMin;
                                    _feesMax = ((SavingsBookProduct)_saving.Product).DepositFeesMax;
                                    _flatFees = _feesMin;
                                }
                                else
                                {
                                    _flatFees = ((SavingBookContract)_saving).DepositFees.Value;
                                    _feesMin = _feesMax = _flatFees;
                                }

                                updAmountFees.Minimum = _feesMin.Value;
                                updAmountFees.Maximum = _feesMax.Value;
                                updAmountFees.Text = ((SavingBookContract)_saving).DepositFees.GetFormatedValue(_saving.Product.Currency.UseCents);
                            }

                            nudAmount_ValueChanged(nudAmount, e);

                            lbAmountMinMax.Text = string.Format("{0} {1} {4}\n\r{2} {3} {4}",
                                                        "min", _amountMin.GetFormatedValue(_saving.Product.Currency.UseCents),
                                                        "max", _amountMax.GetFormatedValue(_saving.Product.Currency.UseCents),
                                                        _saving.Product.Currency.Code);

                            lblAmountFeesMinMax.Text = string.Format("{0} {1} {4}\n\r{2} {3} {4}",
                                                        "min", _feesMin.GetFormatedValue(_saving.Product.Currency.UseCents),
                                                        "max", _feesMax.GetFormatedValue(_saving.Product.Currency.UseCents),
                                                        _saving.Product.Currency.Code);

                            break;
                    }
                    break;
                default: break;
            }
        }

        private void nudAmount_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                _amount = nudAmount.Value;
                bool isFlat = false;

                if (_saving is SavingBookContract)
                {
                    SavingsBookProduct p = (SavingsBookProduct) _saving.Product;
                    if (IsNormalTransfer())
                    {
                        isFlat = p.FlatTransferFees.HasValue || p.FlatTransferFeesMin.HasValue;
                    }
                    else
                    {
                        isFlat = p.InterBranchTransferFee.IsFlat;
                    }
                }

                OCurrency fee = isFlat ? updAmountFees.Value : _amount*updAmountFees.Value/100;
                nudTotalAmount.Value = (_amount + fee).Value;
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
        }

        private bool IsNormalTransfer()
        {
            if (null == _savingTarget) return true;
            Debug.Assert(_saving.Branch != null, "Source branch not defined.");
            Debug.Assert(_savingTarget.Branch != null, "Target branch not defined.");
            return _saving.Branch.Id == _savingTarget.Branch.Id;
        }

        private void LoadTransferFee()
        {
            if (IsNormalTransfer())
            {
                LoadNormalTransferFee();
            }
            else
            {
                LoadInterBranchTransferFee();
            }

            updAmountFees.Minimum = _feesMin.Value;
            updAmountFees.Maximum = _feesMax.Value;

            lblAmountFeesMinMax.Text = string.Format("{0} {1} {4}\n\r{2} {3} {4}",
                   "min", _feesMin.GetFormatedValue(_saving.Product.Currency.UseCents),
                   "max", _feesMax.GetFormatedValue(_saving.Product.Currency.UseCents),
                   _saving.Product.Currency.Code);
        }

        private void LoadInterBranchTransferFee()
        {
            if (!(_saving.Product is SavingsBookProduct)) return;

            Fee fee = (_saving.Product as SavingsBookProduct).InterBranchTransferFee;
            if (fee.IsRange)
            {
                _feesMin = fee.Min.Value;
                _feesMax = fee.Max.Value;
            }
            else
            {
                _feesMin = _feesMax = fee.Value.Value;
            }
        }

        private void LoadNormalTransferFee()
        {
            if (!(_saving.Product is SavingsBookProduct)) return;

            SavingsBookProduct p = (SavingsBookProduct)_saving.Product;
            if (p.FlatTransferFees.HasValue || p.FlatTransferFeesMin.HasValue)
            {
                if (p.FlatTransferFees.HasValue)
                {
                    _feesMin = _feesMax = p.FlatTransferFees;
                }
                else
                {
                    _feesMin = p.FlatTransferFeesMin;
                    _feesMax = p.FlatTransferFeesMax;
                }
            }
            else
            {
                if (p.RateTransferFees.HasValue)
                {
                    _feesMin = _feesMax = (decimal)p.RateTransferFees * 100;
                }
                else
                {
                    _feesMin = (decimal)p.RateTransferFeesMin * 100;
                    _feesMax = (decimal)p.RateTransferFeesMax * 100;
                }
            }
        }
    }
}
