using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Octopus.CoreDomain;
using Octopus.CoreDomain.Clients;
using Octopus.CoreDomain.FundingLines;
using Octopus.Enums;
using Octopus.ExceptionsHandler;
using Octopus.Extensions;
using Octopus.GUI.Clients;
using Octopus.GUI.Tools;
using Octopus.MultiLanguageRessources;
using Octopus.Services;
using Octopus.Services.Events;
using Octopus.Shared;
using Octopus.CoreDomain.Contracts.Savings;

namespace Octopus.GUI.UserControl
{
    public partial class CorporateUserControl : ClientControl
    {
        private Corporate _corporate;
        private readonly Form _mdifrom;
        private bool _create = true;
        private bool _saved;
        private AddressUserControl addressUserControlFirst;
        public event EventHandler SaveCorporateFundingLine;
        public event EventHandler SaveContact;
        public event EventHandler ViewProject;
        public event EventHandler ButtonCancel;
        public event EventHandler ButtonSaveClick;
        public event EventHandler CloseCorporate;
        public event EventHandler AddSelectedSaving;
        public event EventHandler ViewSelectedSaving;

        private readonly List<ICorporate> _extensionCorporates = new List<ICorporate>();

        private CustomizableFieldsControl _customziableFieldsControl;

        public System.Windows.Forms.UserControl PanelSavings
        {
            get { return savingsListUserControl1; }
        }

        public bool ButtonAddSavingsEnabled
        {
            get { return savingsListUserControl1.ButtonAddSavingsEnabled; }
            set { savingsListUserControl1.ButtonAddSavingsEnabled = value; }
        }

        public void RemoveSavings()
        {
            tabControlCorporate.TabPages.Remove(tabPageSavings);
        }

        public Corporate Corporate
        {
            get { return _corporate; }
            set { _corporate = value; }
        }

        private void InitializeCorporate()
        {
            if (_corporate.Id != 0)
            {
                buttonSave.Text = MultiLanguageStrings.GetString(Ressource.CorporateUserControl, "buttonUpdate.Text");
                    //"Update corporate";

                textBoxLastNameCorporate.Text = _corporate.Name;

                textBoxSigle.Text = _corporate.Sigle;
                dateTimePickerDateOfCreate.Value = _corporate.RegistrationDate == DateTime.MinValue
                                                       ? TimeProvider.Today
                                                       : _corporate.RegistrationDate;
                textBoxSmallNameCorporate.Text = _corporate.SmallName;
                textBoxCorpLoanCycle.Text = _corporate.LoanCycle.ToString(CultureInfo.InvariantCulture);
 
                addressUserControlFirst.City = _corporate.City;
                addressUserControlFirst.District = _corporate.District;
                addressUserControlFirst.HomePhone = _corporate.HomePhone;
                addressUserControlFirst.PersonalPhone = _corporate.PersonalPhone;
                addressUserControlFirst.Email = _corporate.Email;
                addressUserControlFirst.Comments = _corporate.Address;
                addressUserControlFirst.ZipCode = _corporate.ZipCode;

                eacCorporate.Activity = _corporate.Activity;

                DisplayListContactCorporate(_corporate.Contacts);
                DisplaySavings(_corporate.Savings);
                InitPrintButton();
            }
            else
            {
                _corporate.LoanCycle = 0;
            }

            foreach (Branch branch in User.CurrentUser.Branches)
                cbBranch.Items.Add(branch);

            if (_corporate.Id != 0) 
                cbBranch.SelectedItem = _corporate.Branch;
            else if (cbBranch.Items.Count > 0) 
                cbBranch.SelectedIndex = 0;
        }

        public CorporateUserControl()
        {
            InitializeComponent();
            InitializeUserControlsAddress();
            _corporate = new Corporate();
        }

        private readonly FundingLine _fundingLine;
        public CorporateUserControl(FundingLine pFundingLine)
        {
            _fundingLine = pFundingLine;
            InitializeComponent();
            InitializeUserControlsAddress();
            _corporate = new Corporate();
        }

        public CorporateUserControl(Project pProject)
        {
            InitializeComponent();
            InitializeUserControlsAddress();
            _corporate = new Corporate();
        }

        public CorporateUserControl(Corporate pCorporate, FundingLine pFundingLine)
        {
            InitializeComponent();
            _corporate = pCorporate;
            _fundingLine = pFundingLine;
            InitializeUserControlsAddress();
            InitializeCorporate();
            InitializeCustomizableFields(_corporate.Id);
        }

        public CorporateUserControl(Form pMdiParent)
        {
            _mdifrom = pMdiParent;
            InitializeComponent();
            _corporate = new Corporate();
            _fundingLine = null;
            InitializeUserControlsAddress();
        }

        public CorporateUserControl(Corporate corporate, Form pMdiParent)
        {
            _mdifrom = pMdiParent;
           _corporate = corporate;
            _fundingLine = null;
            InitializeComponent();
            InitializeUserControlsAddress();
            InitializeCorporate();
            PicturesServices ps = ServicesProvider.GetInstance().GetPicturesServices();
            if (ps.IsEnabled())
            {
                pictureBox1.Image = ps.GetPicture("CORPORATE", corporate.Id, true, 0);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox2.Image = ps.GetPicture("CORPORATE", corporate.Id, true, 1);
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else
            {
                pictureBox1.Visible = false;
                pictureBox2.Visible = false;
                linkLabelChangePhoto.Visible = false;
                linkLabelChangePhoto2.Visible = false;
            }
            InitializeCustomizableFields(_corporate.Id);
        }

        private void InitializeUserControlsAddress()
        {
            addressUserControlFirst = new AddressUserControl
                                          {
                                              TextBoxHomePhoneText =
                                                  MultiLanguageStrings.GetString(Ressource.PersonUserControl,
                                                                                 "Businesscellphone.Text"),
                                              TextBoxPersonalPhoneText =
                                                  MultiLanguageStrings.GetString(Ressource.PersonUserControl,
                                                                                 "Businessphone.Text"),
                                              Dock = DockStyle.Fill
                                          };
            groupBoxAddress.Controls.Add(addressUserControlFirst);
        }

        private void InitializeCustomizableFields(int linkId)
        {
            _customziableFieldsControl = new CustomizableFieldsControl(OCustomizableFieldEntities.Corporate, linkId, false)
            {
                Dock = DockStyle.Fill,
                Enabled = true,
                Name = "customizableFieldsControl",
                Visible = true
            };

            tabPageCustomizableFields.Controls.Add(_customziableFieldsControl);
        }

        private void RecoverDatasFromUserControlsAddress()
        {
            _corporate.ZipCode = addressUserControlFirst.ZipCode;
            _corporate.HomeType = addressUserControlFirst.HomeType;
            _corporate.Email = addressUserControlFirst.Email;
            _corporate.District = addressUserControlFirst.District;
            _corporate.City = addressUserControlFirst.City;
            _corporate.Address = addressUserControlFirst.Comments;
            _corporate.HomePhone = addressUserControlFirst.HomePhone;
            _corporate.PersonalPhone = addressUserControlFirst.PersonalPhone;
        }

        public bool CorporateSaved
        {
            get{ return _saved; }
        }

        private void SaveCorporate(object sender, EventArgs e)
        {
            try
            {
                _corporate.CreationDate = TimeProvider.Now;
                _corporate.RegistrationDate = dateTimePickerDateOfCreate.Value;
                _corporate.AgrementSolidarity = false;
                
                
                RecoverDatasFromUserControlsAddress();
                _corporate.Name = textBoxLastNameCorporate.Text;
                _corporate.Sigle = textBoxSigle.Text;
                _corporate.SmallName = textBoxSmallNameCorporate.Text;

                _corporate.Branch = (Branch) cbBranch.SelectedItem;
                if (_corporate.Name != null)
                    _corporate.Name = _corporate.Name.Trim();

                _corporate.Sigle = _corporate.Sigle.Trim();
                _corporate.SmallName = _corporate.SmallName.Trim();
                _customziableFieldsControl.Check();
                EventProcessorServices es = ServicesProvider.GetInstance().GetEventProcessorServices();
                if (_corporate.Id == 0)
                {
                    _corporate.Id = ServicesProvider
                        .GetInstance()
                        .GetClientServices()
                        .SaveCorporate(_corporate, _fundingLine, tx => _extensionCorporates.ForEach(c => c.Save(_corporate, tx)));
                    buttonSave.Text = MultiLanguageStrings.GetString(Ressource.CorporateUserControl, "buttonUpdate.Text");
                    es.LogClientSaveUpdateEvent(_corporate, true);
                }
                else
                {
                    ServicesProvider.
                        GetInstance()
                        .GetClientServices()
                        .SaveCorporate(Corporate, null, tx => _extensionCorporates.ForEach(c => c.Save(Corporate, tx)));
                    es.LogClientSaveUpdateEvent(_corporate, false);
                }

                if (_corporate.Id > 0)
                    _customziableFieldsControl.Save(_corporate.Id);


                if (SaveCorporateFundingLine != null)
                    SaveCorporateFundingLine(this, e);

                InitializeCustomizableFields(_corporate.Id);

                _saved = true;
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
            finally
            {
                _create = false;
                if(CloseCorporate != null)
                    CloseCorporate(this, null);
            }
            if (ButtonSaveClick != null)
                ButtonSaveClick(this,e);
        }

        private void DisplayListContactCorporate(IEnumerable<Contact> contacts)
        {
            lvContacts.Items.Clear();
            foreach (Contact contact in contacts)
            {
                var listViewItem = new ListViewItem(((Person)contact.Tiers).Name) {Tag = contact};
                listViewItem.SubItems.Add(((Person)contact.Tiers).PersonalPhone);
                lvContacts.Items.Add(listViewItem);
            }
        }

        private void ButtonCancelClick(object sender, EventArgs e)
        {
            if (ButtonCancel != null)
                ButtonCancel(this, e);
        }

        public void DisplaySavings(IEnumerable<ISavingsContract> pSavings)
        {
            savingsListUserControl1.DisplaySavings(pSavings);
        }

        public event EventHandler ButtonAddProjectClick;

        private void ButtonDeleteClick1(object sender, EventArgs e)
        {
            if (lvContacts.SelectedItems.Count != 0)
            {
                _corporate.Contacts.Remove((Contact)lvContacts.SelectedItems[0].Tag);
                DisplayListContactCorporate(_corporate.Contacts);
            }
        }

        private void CorporateUserControlLoad(object sender, EventArgs e)
        {
            Tabs = tabControlCorporate;
            Client = _corporate;
            InitDocuments();
            LoadExtensions();
            //if (!ServicesProvider.GetInstance().GetGeneralSettings().UseProjects)
            //    ButtonAddProjectClick(buttonViewProject, null);

        }

        private void LoadExtensions()
        {
            foreach (ICorporate c in Extension.Instance.Extensions.Select(e => e.QueryInterface(typeof(ICorporate))).OfType<ICorporate>())
            {
                _extensionCorporates.Add(c);
                TabPage[] pages = c.GetTabPages(_corporate);
                if (null == pages) continue;
                tabControlCorporate.TabPages.AddRange(pages);
            }
        }

        private void SavingsListUserControl1AddSelectedSaving(object sender, EventArgs e)
        {
            if (AddSelectedSaving != null)
                AddSelectedSaving(sender, e);
        }

        private void SavingsListUserControl1ViewSelectedSaving(object sender, EventArgs e)
        {
            if (ViewSelectedSaving != null)
                ViewSelectedSaving(sender, e);
        }

        private void PictureBox1Click(object sender, EventArgs e)
        {
            ShowPictureForm showPictureForm;
            if (sender is PictureBox)
            {
                if (((PictureBox)sender).Name=="pictureBox1")
                {

                    showPictureForm = new ShowPictureForm(_corporate, this, 0);
                    showPictureForm.ShowDialog();
                   
                }
                else if (((PictureBox)sender).Name=="pictureBox2")
                {
                    showPictureForm = new ShowPictureForm(_corporate, this, 1);
                    showPictureForm.ShowDialog();
                }
            }
            else if (sender is LinkLabel)
            {
                switch (((LinkLabel)sender).Name)
                {
                    case "linkLabelChangePhoto":
                        showPictureForm = new ShowPictureForm(_corporate, this, 0);
                        showPictureForm.ShowDialog();
                        break;
                    case "linkLabelChangePhoto2":
                        showPictureForm = new ShowPictureForm(_corporate, this, 1);
                        showPictureForm.ShowDialog();
                        break;
                }
            }
        }

        private void InitPrintButton()
        {
            btnPrint.ReportInitializer = report => report.SetParamValue("corporate_id", _corporate.Id);
            btnPrint.LoadReports();
        }

        public void SyncLoanCycle()
        {
            textBoxCorpLoanCycle.Text = _corporate.LoanCycle.ToString(CultureInfo.InvariantCulture);
        }

        private void EacCorporateEconomicActivityChange(object sender, EconomicActivtyEventArgs e)
        {
            if (_corporate != null) _corporate.Activity = eacCorporate.Activity;
        }

        private void BtnSelectContactClick(object sender, EventArgs e)
        {

            using (SearchClientForm searchClientForm = SearchClientForm.GetInstance(OClientTypes.Person, true))
            {
                searchClientForm.ShowDialog();
                var contact = new Contact();
                try
                {
                    if (searchClientForm.Client != null)
                        contact.Tiers = searchClientForm.Client;

                    if (!ServicesProvider.GetInstance().GetClientServices().ClientCanBeAddToCorporate(
                        searchClientForm.Client, Corporate)) return;

                    if (contact.Tiers != null)
                        Corporate.Contacts.Add(contact);

                    DisplayListContactCorporate(Corporate.Contacts);
                }
                catch (Exception ex)
                {
                    new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
                }
            }
        }

        private void BtnAddContactClick(object sender, EventArgs e)
        {
            var personForm = new ClientForm(OClientTypes.Person, _mdifrom, true);
            personForm.ShowDialog();
            Contact contact = new Contact {Tiers = personForm.Person};
            if (contact.Tiers != null)
                Corporate.Contacts.Add(contact);
            DisplayListContactCorporate(Corporate.Contacts);
        }
    }
}