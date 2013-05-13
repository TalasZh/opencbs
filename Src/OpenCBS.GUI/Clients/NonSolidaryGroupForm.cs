// Octopus MFS is an integrated suite for managing a Micro Finance Institution: 
// clients, contracts, accounting, reporting and risk
// Copyright © 2006,2007 OCTO Technology & OXUS Development Network
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License along
// with this program; if not, write to the Free Software Foundation, Inc.,
// 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
// Website: http://www.opencbs.com
// Contact: contact@opencbs.com

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Clients;
using OpenCBS.CoreDomain.Contracts.Loans;
using OpenCBS.CoreDomain.Contracts.Savings;
using OpenCBS.CoreDomain.Products;
using OpenCBS.Enums;
using OpenCBS.ExceptionsHandler;
using OpenCBS.Extensions;
using OpenCBS.GUI.Contracts;
using OpenCBS.GUI.Tools;
using OpenCBS.GUI.UserControl;
using OpenCBS.MultiLanguageRessources;
using OpenCBS.Services;
using OpenCBS.Services.Events;
using OpenCBS.Shared;
using BrightIdeasSoftware;
using OpenCBS.Shared.Settings;

namespace OpenCBS.GUI.Clients
{
    public partial class NonSolidaryGroupForm : SweetForm
    {
        private readonly Village _village;
        private AddressUserControl _ucAddress;
        private ContextMenuStrip _ctxProducts;
        private CustomizableFieldsControl _customizableFieldsControl;
        private String _title = null;
        private bool membersSaved = true;
        private readonly List<INonSolidarityGroup> _extensionGroups = new List<INonSolidarityGroup>();

        public NonSolidaryGroupForm()
        {
            InitializeComponent();
            _village = new Village {CreationDate = TimeProvider.Now};
            InitializeControls();
            InitializeCustomizableFields(null);
            InitializeTitle();
        }

        public NonSolidaryGroupForm(Village village)
        {
            _village = village;

            foreach (VillageMember member in _village.Members)
            {
                member.IsSaved = true;
                member.ActiveLoans = ServicesProvider.GetInstance().GetContractServices().FindActiveContracts(member.Tiers.Id);
            }

            InitializeComponent();
            InitializeControls();
            LoadMeetingDates();
            InitializeCustomizableFields(_village.Id);
            InitializeTitle();
        }

        private void LoadMeetingDates()
        {
            comboBoxMeetingDates.DataSource 
                = ServicesProvider.GetInstance().GetContractServices().FindInstallmentDatesForVillageActiveContracts(_village.Id);
        }

        private void InitializeCustomizableFields(int? linkId)
        {
            _customizableFieldsControl = new CustomizableFieldsControl(OCustomizableFieldEntities.NSG, linkId, false)
                                         {
                                             Dock = DockStyle.Fill,
                                             Enabled = true,
                                             Name = "customizableFieldsControl",
                                             Visible = true
                                         };

            tabPageCustomizableFields.Controls.Add(_customizableFieldsControl);
        }

        private void InitializeTitle()
        {
            if (_village != null && !string.IsNullOrEmpty(_village.Name))
            {
                if (string.IsNullOrEmpty(_title))
                {
                    // Store the genuine title (first opening).
                    _title = Text;
                }
                else
                {
                    // Restore the genuine title (e.g. after update).
                     Text = _title;
                }
                Text += "  " + " [" + _village.Name + "]";
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();  
        }

        internal bool Save()
        {
            try
            {
                UpdateVillageDataFromGui();
                InitializeTitle();
                bool save = 0 == _village.Id;
                _village.Name = _village.Name.Trim();
                _customizableFieldsControl.Check();
                _village.Id = ServicesProvider
                    .GetInstance()
                    .GetClientServices()
                    .SaveNonSolidarityGroup(_village, (tx, id) => _extensionGroups.ForEach(g => g.Save(_village, tx)));

                if (_village.Id > 0)
                    _customizableFieldsControl.Save(_village.Id);

                ServicesProvider.GetInstance().GetClientServices().SetFavouriteLoanOfficerForVillage(_village);
                EventProcessorServices es = ServicesProvider.GetInstance().GetEventProcessorServices();
                es.LogClientSaveUpdateEvent(_village, save);
                btnSave.Text = MultiLanguageStrings.GetString(Ressource.VillageForm, "Update.Text");
                tpMembers.Enabled = true;
                tabPageLoan.Enabled = true;
                tabPageSavings.Enabled = true;
                membersSaved = true;
                
                foreach (VillageMember member in _village.Members)
                    member.IsSaved = true;


            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
                return false;
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();   
        }

        private DayOfWeek GetDayOfWeek()
        {
            DayOfWeek dayOfWeek = DayOfWeek.Monday;
            switch (cmbWeekDay.SelectedIndex)
            {
                case 0: dayOfWeek = DayOfWeek.Sunday;
                    break;
                case 1: dayOfWeek = DayOfWeek.Monday;
                    break;
                case 2: dayOfWeek = DayOfWeek.Tuesday;
                    break;
                case 3: dayOfWeek = DayOfWeek.Wednesday;
                    break;
                case 4: dayOfWeek = DayOfWeek.Thursday;
                    break;
                case 5: dayOfWeek = DayOfWeek.Friday;
                    break;
                case 6: dayOfWeek = DayOfWeek.Saturday;
                    break;
            }

            return dayOfWeek;
        }

        private void UpdateVillageDataFromGui()
        {
            _village.Name = tbName.Text;
            _village.EstablishmentDate = dtDate.Value;
            _village.District = _ucAddress.District;
            _village.City = _ucAddress.City;
            _village.Address = _ucAddress.Comments;
            _village.ZipCode = _ucAddress.ZipCode;
            _village.LoanOfficer = (User) cbLoanOfficers.SelectedItem;
            _village.MeetingDay = cbMeetingDay.Checked ? (DayOfWeek?) GetDayOfWeek() : null;
            _village.Branch = (Branch) cbBranch.SelectedItem;
        }

        private void InitializeControls()
        {
            _ucAddress = new AddressUserControl();
            _ctxProducts = new ContextMenuStrip();
            _ucAddress.ExtraVisible = false;
            _ucAddress.Dock = DockStyle.Fill;
            gbAddress.Controls.Add(_ucAddress);
            InitializeLoanOfficers();            
            if (_village != null && _village.Id > 0)
            {
                tbName.Text = _village.Name;
                dtDate.Value = _village.EstablishmentDate.HasValue ? _village.EstablishmentDate.Value : TimeProvider.Now;
                _ucAddress.District = _village.District;
                _ucAddress.City = _village.City;
                _ucAddress.Comments = _village.Address;
                _ucAddress.ZipCode = _village.ZipCode;
                btnSave.Text = MultiLanguageStrings.GetString(Ressource.VillageForm, "Update.Text");
                DisplayMembers();
                DisplaySavings();
                DisplayLoans();

                if (_village.MeetingDay.HasValue)
                {
                    cmbWeekDay.SelectedIndex = (int) _village.MeetingDay;
                    cbMeetingDay.Checked = true;
                }

                if (_village.LoanOfficer != null)
                {
                    foreach (object item in cbLoanOfficers.Items)
                    {
                        User user = item as User;
                        if (user != null && user.Id == _village.LoanOfficer.Id)
                        {
                            cbLoanOfficers.SelectedItem = item;
                            break;
                        }
                    }
                }
            }
            else
            {
                tabPageLoan.Enabled = false;
                tabPageSavings.Enabled = false;
            }

            PicturesServices ps = ServicesProvider.GetInstance().GetPicturesServices();
            if (ps.IsEnabled())
            {
                pictureBox1.Image = ps.GetPicture("VILLAGE", _village.Id, true, 0);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox2.Image = ps.GetPicture("VILLAGE", _village.Id, true, 1);
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else
            {
                pictureBox1.Visible = false;
                pictureBox2.Visible = false;
                linkLabelChangePhoto.Visible = false;
                linkLabelChangePhoto2.Visible = false;
            }

            foreach (Branch branch in User.CurrentUser.Branches)
            {
                cbBranch.Items.Add(branch);
            }
            if (_village.Id > 0)
            {
                cbBranch.SelectedItem = _village.Branch;
            }
            else if (cbBranch.Items.Count > 0)
            {
                cbBranch.SelectedIndex = 0;
            }

            btnPrint.ReportInitializer = report => report.SetParamValue("village_id", _village.Id);
            btnPrint.LoadReports();
        }

        private void InitializeLoanOfficers()
        {
            cbLoanOfficers.Items.Clear();
            cbLoanOfficers.Items.Add(User.CurrentUser);
            foreach (User subordinate in User.CurrentUser.Subordinates)
            {
                if (subordinate.IsDeleted) continue;
                cbLoanOfficers.Items.Add(subordinate);
            }
            cbLoanOfficers.SelectedIndex = 0;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchClientForm frm = SearchClientForm.GetInstanceForVillage();
            frm.ShowDialog();
            if (DialogResult.OK != frm.DialogResult) return;

            try
            {
                var clientServices = ServicesProvider.GetInstance().GetClientServices();
                var client = frm.Client;
                if (clientServices.ClientIsAPerson(client))
                {
                    var personId = client.Id;
                    clientServices.CheckPersonGroupCount(personId);
                    var member = new VillageMember { Tiers = client, JoinedDate = TimeProvider.Now, CurrentlyIn = true, IsLeader = false, IsSaved = false};
                    member.ActiveLoans = ServicesProvider.GetInstance().GetContractServices().
                        FindActiveContracts(personId);
                    
                    List<ISavingsContract> savingsContracts =
                        ServicesProvider.GetInstance().GetSavingServices().GetSavingsByClientId(member.Tiers.Id);
           
                    foreach (ISavingsContract contract in savingsContracts)
                    {
                        member.Tiers.Savings.Add(contract);
                    }

                    clientServices.AddExistingMember(_village, member);
                    membersSaved = false;
                    DisplayMembers();
                    DisplayLoans();
                    DisplaySavings();
                }
            } catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
        }

        private void DisplayMembers()
        {
            lvMembers.Items.Clear();
            if (cbxShowRemovedMembers.Checked)
                ShowAllMembers();
            else
                ShowCurrentMembers();
        }

        private void ShowAllMembers()
        {
            string activeClient = MultiLanguageStrings.GetString(Ressource.VillageForm, "ActiveClient");
            string inActiveClient = MultiLanguageStrings.GetString(Ressource.VillageForm, "InactiveClient");

            foreach (VillageMember member in _village.MemberHistory.OrderBy(i => i.Tiers.Name))
            {
                member.ActiveLoans = ServicesProvider.GetInstance().GetContractServices().FindActiveContracts(member.Tiers.Id);
                Person person = (Person)member.Tiers;
                ListViewItem item = new ListViewItem(person.Name) { Tag = member };
                item.SubItems.Add(person.IdentificationData);

                item.SubItems.Add(person.Active ? activeClient : inActiveClient);
                item.SubItems.Add(member.JoinedDate.ToShortDateString());
                if (_village.Leader != null)
                {
                    if (member.LeftDate != null && member.CurrentlyIn == false)
                    {
                        item.SubItems.Add(member.LeftDate.Value.ToShortDateString());
                        item.BackColor = Color.Red;
                    }
                    else if (person.Id == _village.Leader.Tiers.Id)
                    {
                        item.BackColor = Color.FromArgb(0, 88, 56);
                        item.ForeColor = Color.White;
                    }
                    else
                        item.BackColor = Color.White;
                }
                lvMembers.Items.Add(item);
            }
        }

        private void ShowCurrentMembers()
        {
            string activeClient = MultiLanguageStrings.GetString(Ressource.VillageForm, "ActiveClient");
            string inActiveClient = MultiLanguageStrings.GetString(Ressource.VillageForm, "InactiveClient");

            foreach (VillageMember member in _village.Members.OrderBy(i => i.Tiers.Name))
            {
                member.ActiveLoans =
                    ServicesProvider.GetInstance().GetContractServices().FindActiveContracts(member.Tiers.Id);
                Person person = (Person) member.Tiers;
                ListViewItem item = new ListViewItem(person.Name) {Tag = member};
                item.SubItems.Add(person.IdentificationData);

                item.SubItems.Add(person.Active ? activeClient : inActiveClient);
                item.SubItems.Add(member.JoinedDate.ToShortDateString());

                if (_village.Leader != null)
                {
                    if (person.Id == _village.Leader.Tiers.Id)
                    {
                        item.BackColor = Color.FromArgb(0, 88, 56);
                        item.ForeColor = Color.White;
                    }
                    else
                        item.BackColor = Color.White;
                }
                
                lvMembers.Items.Add(item);
            }
        }

        private void DisplayLoans()
        {
            listViewLoans.Items.Clear();
            if (!cbxDisplayAllLoans.Checked)
            {
                foreach (VillageMember member in _village.Members)
                {
                    ShowActiveLoans(member);
                }
            }
            else
            {
                if (_village.MemberHistory==null || _village.MemberHistory.Count==0)
                    _village.MemberHistory =
                   ServicesProvider.GetInstance().GetClientServices().FindVillageHistoryPersons(_village.Id);
                foreach (VillageMember member in _village.MemberHistory)
                {
                    ShowAllLoans(member);
                }
            }
        }

        private void ShowAllLoans(VillageMember member)
        {
            List<Loan> allLoansOfMember =
                ServicesProvider.GetInstance().GetContractServices().FindAllLoansOfClient(member.Tiers.Id);
            foreach (Loan item in allLoansOfMember)
            {
                if (item.CreationDate.Date>=member.JoinedDate.Date)
                    if (member.LeftDate!=null)
                    {
                        if (member.LeftDate.Value.Date>=item.CreationDate.Date 
                            && member.JoinedDate.Date<=item.CreationDate.Date)
                            ShowLoanInListView(member, item);
                    }
                    else
                    {
                        ShowLoanInListView(member,item);
                    }
            }
        }

        private void ShowActiveLoans(VillageMember member)
        {
            if (member.ActiveLoans != null)
            {
                foreach (Loan loan in member.ActiveLoans)
                {
                    ShowLoanInListView(member, loan);
                }
            }
        }

        private void ShowLoanInListView(VillageMember member, Loan loan)
        {
            Person person = (Person)member.Tiers;
            ApplicationSettings dataParam = ApplicationSettings.GetInstance(string.Empty);
            int decimalPlaces = dataParam.InterestRateDecimalPlaces;
            ListViewItem item = new ListViewItem(person.Name) { Tag = member };
            if (loan == null || _village.EstablishmentDate==null) return;
            if (loan.CreationDate.Date >= _village.EstablishmentDate.Value.Date && _village.Id == loan.NsgID)
            {
                item.SubItems.Add(loan.ProductName);
                item.SubItems.Add(loan.Code);
                item.SubItems.Add(MultiLanguageStrings.GetString(Ressource.ClientForm, loan.ContractStatus + ".Text"));
                item.SubItems.Add(loan.Amount.GetFormatedValue(loan.UseCents));
                item.SubItems.Add(
                    loan.CalculateActualOlbBasedOnRepayments().GetFormatedValue(loan.UseCents));
                item.SubItems.Add(loan.Product.Currency.Name);
                item.SubItems.Add(Math.Round(loan.InterestRate*100m, decimalPlaces).ToString());
                item.SubItems.Add(loan.InstallmentType.Name);
                item.SubItems.Add(loan.NbOfInstallments.ToString());
                item.SubItems.Add(loan.AlignDisbursementDate.ToShortDateString());

                if (loan.GetLastNonDeletedEvent() != null) item.SubItems.Add(loan.GetLastNonDeletedEvent().Date.ToShortDateString());
                else item.SubItems.Add("-");

                if (loan.NextInstallment != null)
                {
                    item.SubItems.Add(loan.NextInstallment.ExpectedDate.ToShortDateString());
                    item.SubItems.Add(
                        ServicesHelper.ConvertDecimalToString(loan.NextInstallment.Amount.Value));
                }
                else
                {
                    item.SubItems.Add("-");
                    item.SubItems.Add("-");
                }
                item.SubItems.Add(loan.CloseDate.ToShortDateString());
                if (member.LeftDate != null)
                    item.BackColor = Color.Red;
                listViewLoans.Items.Add(item);
            }
        }

        private void DisplaySavings()
        {
            listViewSavings.Items.Clear();

            foreach (var member in _village.Members)
            {
                if (member.Tiers.Savings.Count > 0)
                {
                    ListViewGroup group = new ListViewGroup(member.Tiers.Name) {Tag = member.Tiers};

                    List<ISavingsContract> savings = ServicesProvider.GetInstance().GetSavingServices().GetSavingsByClientId(member.Tiers.Id);

                    foreach (var saving in savings)
                    {
                        ListViewItem item = new ListViewItem(member.Tiers.Name) { Tag = saving };
                        item.SubItems.Add(saving.Code);
                        item.SubItems.Add(MultiLanguageStrings.GetString(Ressource.ClientForm, 
                            saving is SavingBookContract ? "SavingsBook.Text" : "CompulsorySavings.Text"));
                        item.SubItems.Add(saving.Product.Name);
                        item.SubItems.Add(saving.GetBalance().GetFormatedValue(saving.Product.Currency.UseCents));
                        item.SubItems.Add(saving.Product.Currency.Code);
                        item.SubItems.Add(saving.CreationDate.ToShortDateString());
                        item.SubItems.Add((saving.Events.Count != 0) ? saving.Events[saving.Events.Count - 1].Date.ToShortDateString() : "");
                        item.SubItems.Add(saving.Status.ToString());
                        item.SubItems.Add(saving.ClosedDate.HasValue ? saving.ClosedDate.Value.ToShortDateString() : "");
                        item.Group = group;                        
                        listViewSavings.Items.Add(item);
                    }
                }
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (1 == lvMembers.SelectedItems.Count && lvMembers.SelectedItems[0].BackColor!=Color.Red)
                {
                    VillageMember member = (VillageMember)lvMembers.SelectedItems[0].Tag;
                    if (member.ActiveLoans != null && member.ActiveLoans.Count > 0)
                    {
                        MessageBox.Show(MultiLanguageStrings.GetString(Ressource.VillageForm, "RemoveProhibited.Text"),
                            MultiLanguageStrings.GetString(Ressource.VillageForm, "RemoveProhibited.Caption"),
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        if (DialogResult.Yes == MessageBox.Show(MultiLanguageStrings.GetString(Ressource.VillageForm, "RemoveConfirm.Text"),
                            MultiLanguageStrings.GetString(Ressource.VillageForm, "RemoveConfirm.Caption"),
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                        {
                            ServicesProvider.GetInstance().GetClientServices().RemoveMember(_village, member);
                            membersSaved = false;
                            DisplayMembers();
                            DisplayLoans();
                        }
                    }
                }    
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
            
        }

        private void lvMembers_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (lvMembers.SelectedItems.Count != 0)
            {
                VillageMember member = (VillageMember) lvMembers.SelectedItems[0].Tag;
                if (member != null)
                {
                    btnRemove.Enabled = (1 == lvMembers.SelectedItems.Count && member.CurrentlyIn);
                    btnSetAsLeader.Enabled = (1 == lvMembers.SelectedItems.Count && member.CurrentlyIn);
                }
            }
            else
            {
                btnRemove.Enabled = false;
                btnSetAsLeader.Enabled = false;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ClientForm frm = new ClientForm(OClientTypes.Person, MdiParent, true);
            if (frm.ShowDialog() != DialogResult.OK) return;
            try
            {
                Person person = frm.Person;
                if (ServicesProvider.GetInstance().GetClientServices().ClientIsAPerson(person))
                {
                    var member = new VillageMember { Tiers = person, JoinedDate = TimeProvider.Now, CurrentlyIn = true, IsLeader = false, IsSaved = false};
                    member.ActiveLoans = ServicesProvider.GetInstance().GetContractServices().
                         FindActiveContracts(member.Tiers.Id);
                    
                    _village.AddMember(member);
                    membersSaved = false;
                    DisplayMembers();
                    DisplayLoans();
                }
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
        }

        private void lvMembers_DoubleClick(object sender, EventArgs e)
        {
            VillageMember member = (VillageMember) lvMembers.SelectedItems[0].Tag;
            if (member != null)
            {
                ClientForm frm;
                if (member.ActiveLoans != null && member.ActiveLoans.Count != 0)
                {
                    IClient client = ServicesProvider.GetInstance().GetClientServices().FindTiersByContractId(member.ActiveLoans[0].Id);
                    if (client.Projects != null)
                        foreach (Project project in client.Projects)
                            if (project.Credits != null)
                                foreach (Loan loan in project.Credits)
                                    loan.CompulsorySavings = ServicesProvider.GetInstance().GetSavingServices().GetSavingForLoan(loan.Id, true);
                    frm = new ClientForm(client, member.ActiveLoans[0].Id, MdiParent, "tabPageDetails");
                }
                else
                {
                    frm = new ClientForm((Person) member.Tiers, MdiParent);
                }
                frm.ShowDialog();
            }
        }

        private void btnAddLoan_Click(object sender, EventArgs e)
        {
            _ctxProducts.Items.Clear();
            ProductServices svc = ServicesProvider.GetInstance().GetProductServices();
            List<LoanProduct> products = svc.FindAllPackages(false, OClientTypes.Village);
            foreach (LoanProduct product in products)
            {
                ToolStripMenuItem item = new ToolStripMenuItem {Text = product.Name, Tag = product};
                item.Click += LoanProduct_Click;
                _ctxProducts.Items.Add(item);
            }
            _ctxProducts.Show(btnAddLoan, 0 - _ctxProducts.Size.Width,0);
        }

        private void LoanProduct_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem) sender;
            if (null == item) return;
            LoanProduct product = (LoanProduct) item.Tag;
            if (null == product) return;
            VillageAddLoanForm frm = new VillageAddLoanForm(_village, product, this);
            
            if (DialogResult.OK == frm.ShowDialog())
            {
                ((LotrasmicMainWindowForm) MdiParent).ReloadAlerts();
            }
            DisplayMembers();
            DisplayLoans();
        }

        private void buttonLoanDisbursment_Click(object sender, EventArgs e)
        {
            VillageDisburseLoanForm frm = new VillageDisburseLoanForm(_village);
            if (DialogResult.OK == frm.ShowDialog())
            {
                DisplayMembers();
                DisplayLoans();
                LoadMeetingDates();
                ((LotrasmicMainWindowForm) MdiParent).ReloadAlerts();
            }
        }

        private void btnRepay_Click(object sender, EventArgs e)
        {
            FastRepaymentForm frm = new FastRepaymentForm(_village);
            if (DialogResult.OK == frm.ShowDialog())
            {
                DisplayMembers();
                DisplayLoans();
                ((LotrasmicMainWindowForm) MdiParent).ReloadAlerts();
            }
        }
        private bool CheckDataInOpenFiscalYear()
        {
            try
            {
                var coaServices = ServicesProvider.GetInstance().GetChartOfAccountsServices();
                var fiscalYear =
                    coaServices.SelectFiscalYears().Find(y => y.OpenDate <= TimeProvider.Now && y.CloseDate == null);
                if (fiscalYear == null)
                {
                    throw new OpenCbsContractSaveException(
                        OpenCbsContractSaveExceptionEnum.OperationOutsideCurrentFiscalYear);
                }
                return true;
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
                return false;
            }
        }

        private void btnAddSavings_Click(object sender, EventArgs e)
        {
            if(!CheckDataInOpenFiscalYear())return;
            _ctxProducts.Items.Clear();
            SavingProductServices svc = ServicesProvider.GetInstance().GetSavingProductServices();
            List<ISavingProduct> products = svc.FindAllSavingsProducts(false, OClientTypes.Village);
            foreach (ISavingProduct product in products)
            {
                ToolStripMenuItem item = new ToolStripMenuItem { Text = product.Name, Tag = product };
                item.Click += SavingsProduct_Click;
                _ctxProducts.Items.Add(item);
            }
            _ctxProducts.Show(btnAddSavings, 0 - _ctxProducts.Size.Width, 0);
        }

        private void SavingsProduct_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            if (null == item) return;
            ISavingProduct product = (ISavingProduct)item.Tag;
            if (null == product) return;
            VillageAddSavingsForm frm = new VillageAddSavingsForm(_village, product, this);
            if (frm.ShowDialog() == DialogResult.OK)
                DisplaySavings();
        }

        private void buttonViewSaving_Click(object sender, EventArgs e)
        {
            DisplaySelectedSaving();
        }

        private void DisplaySelectedSaving()
        {
            if (listViewSavings.SelectedItems.Count > 0)
            {
                IClient member = (IClient)listViewSavings.SelectedItems[0].Group.Tag;
                if (member != null)
                {
                    ClientForm personForm = new ClientForm((Person)member, MdiParent);
                    personForm.DisplaySaving(((ISavingsContract)listViewSavings.SelectedItems[0].Tag).Id, member);
                    personForm.ShowDialog();
                    DisplaySavings();
                }
            }
        }

        private void listViewSavings_DoubleClick(object sender, EventArgs e)
        {
            DisplaySelectedSaving();
        }

        private void changePhotoLinkLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int photoSubId = 0;
            if (sender is LinkLabel)
            {
                if (((LinkLabel)sender).Name == "linkLabelChangePhoto2")
                    photoSubId = 1;
            }

            ShowPictureForm showPictureForm = new ShowPictureForm(_village, this, photoSubId);
            showPictureForm.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            int photoSubId = 0;
            if (sender is PictureBox)
            {
                if (((PictureBox)sender).Name == "pictureBox2")
                    photoSubId = 1;
            }

            ShowPictureForm showPictureForm = new ShowPictureForm(_village, this, photoSubId);
            showPictureForm.ShowDialog();
        }

        private void listViewLoans_DoubleClick(object sender, EventArgs e)
        {
            VillageMember member = (VillageMember)listViewLoans.SelectedItems[0].Tag;
            if (member != null)
            {
                ClientForm frm;
                if (member.ActiveLoans != null)
                {
                    if (member.ActiveLoans.Count>0)
                    {
                        IClient client = ServicesProvider.GetInstance().GetClientServices().FindTiersByContractId(member.ActiveLoans[0].Id);
                        if (client.Projects != null)
                            foreach (Project project in client.Projects)
                                if (project.Credits != null)
                                    foreach (Loan loan in project.Credits)
                                        loan.CompulsorySavings = ServicesProvider.GetInstance().GetSavingServices().GetSavingForLoan(loan.Id, true);
                        frm = new ClientForm(client, member.ActiveLoans[0].Id, MdiParent, "tabPageLoansDetails");
                    }
                    else
                    {
                        frm = new ClientForm((Person)member.Tiers, MdiParent);
                    }
                }
                else
                {
                    frm = new ClientForm((Person)member.Tiers, MdiParent);
                }
                frm.ShowDialog();
                
                
                if (_village.Members!=null) 
                    if(_village.Members.Count!=0)
                { 
                    for (int i = 0; i < _village.Members.Count; i++)
                    {
                        if (_village.Members[i] == member)
                        {
                            _village.Members[i].ActiveLoans = 
                                ServicesProvider.GetInstance().GetContractServices().FindActiveContracts(member.Tiers.Id);
                        }
                    }
                }

                DisplayLoans();
                ((LotrasmicMainWindowForm)MdiParent).ReloadAlerts();
            }
        }

        private void cbMeetingDay_CheckedChanged(object sender, EventArgs e)
        {
            cmbWeekDay.Enabled = cbMeetingDay.Checked;
        }

        private void NonSolidaryGroupForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            GC.Collect();
        }

        private void cbxShowRemovedMembers_CheckedChanged(object sender, EventArgs e)
        {
            if (!membersSaved)
                if(Confirm("ConfirmSave.Text"))
                    Save();
            if (cbxShowRemovedMembers.Checked)
            {
                _village.MemberHistory =
                    ServicesProvider.GetInstance().GetClientServices().FindVillageHistoryPersons(_village.Id);
            }
            else
            {
                _village.Members.Clear();
                ServicesProvider.GetInstance().GetClientServices().GetNotRemovedVillageMembers(_village);
                foreach (VillageMember member in _village.Members)
                    member.IsSaved = true;
            }
            membersSaved = true;
            DisplayMembers();
        }

        private void btnSetAsLeader_Click(object sender, EventArgs e)
        {
            if (lvMembers.SelectedItems.Count != 0)
                _ChangeLeader(((VillageMember)lvMembers.SelectedItems[0].Tag));
        }

        private void _ChangeLeader(VillageMember newLeader)
        {
            _village.Leader = newLeader;
            DisplayMembers();
        }

        private void cbxDisplayAllLoans_CheckedChanged(object sender, EventArgs e)
        {
            DisplayLoans();
        }

        private void buttonFastDeposit_Click(object sender, EventArgs e)
        {
            if(!CheckDataInOpenFiscalYear())return;
            FastDepositForm frm = new FastDepositForm(_village);
            if (frm.ShowDialog() == DialogResult.OK)
                DisplaySavings();
        }

        private void comboBoxMeetingDates_SelectedValueChanged(object sender, EventArgs e)
        {
            List<VillageAttendee> attendees = 
                ServicesProvider.GetInstance().GetContractServices().FindMeetingAttendees(_village.Id,((DateTime)comboBoxMeetingDates.SelectedValue).Date);

            olvAttendees.SetObjects(attendees);
        }

        private void buttonUpdateAttendence_Click(object sender, EventArgs e)
        {
            foreach (OLVListItem item in olvAttendees.Items)
            {
                VillageAttendee attendee = new VillageAttendee();

                attendee.Id = Convert.ToInt32(item.SubItems[0].Text);
                attendee.VillageId = _village.Id;
                attendee.TiersId = Convert.ToInt32(item.SubItems[1].Text);
                attendee.AttendedDate = ((DateTime)this.comboBoxMeetingDates.SelectedValue).Date;
                attendee.Attended = Convert.ToBoolean(item.SubItems[3].Text);
                attendee.Comment = item.SubItems[4].Text;
                attendee.LoanId = Convert.ToInt32(item.SubItems[5].Text);  
                ServicesProvider.GetInstance().GetContractServices().UpdateMeetingAttendees(attendee);
                item.SubItems[0].Text = attendee.Id.ToString();
            }
        }

        private void btnValidateLoans_Click(object sender, EventArgs e)
        {
            VillageCreditCommitteeForm frm = new VillageCreditCommitteeForm(_village);
            if (DialogResult.OK == frm.ShowDialog())
            {
                DisplayLoans();
                LoadMeetingDates();
                ((LotrasmicMainWindowForm)MdiParent).ReloadAlerts();
            }
        }

        private void NonSolidaryGroupForm_Load(object sender, EventArgs e)
        {
            LoadExtensions();
        }

        private void LoadExtensions()
        {
            foreach (INonSolidarityGroup g in Extension.Instance.Extensions.Select(e => e.QueryInterface(typeof(INonSolidarityGroup))).OfType<INonSolidarityGroup>())
            {
                _extensionGroups.Add(g);
                TabPage[] pages = g.GetTabPages(_village);
                if (null == pages) continue;
                tabVillage.TabPages.AddRange(pages);
            }
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
