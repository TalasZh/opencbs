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
using System.Drawing;
using System.Windows.Forms;
using Octopus.CoreDomain.Clients;
using Octopus.CoreDomain.SearchResult;
using Octopus.GUI.UserControl;
using Octopus.Services;
using Octopus.MultiLanguageRessources;
using Octopus.ExceptionsHandler;
using Octopus.Enums;

namespace Octopus.GUI
{
    using Octopus.CoreDomain;
    using Octopus.CoreDomain.Contracts.Loans;

    /// <summary>
    /// Description résumée de SearchClientForm.
    /// </summary>
    public class SearchClientForm : SweetBaseForm
    {
        private GroupBox groupBoxSearchParameters;
        private Button buttonCancel;
        private SweetButton buttonSearch;
        private Label labelTitleResult;
        private System.ComponentModel.IContainer components;
        private GroupBox groupBoxButtonBottom;
        private SweetButton buttonPreview;
        private SweetButton buttonNext;
        private TextBox textBoxCurrentlyPage;
        private TextBox textBoxQuery;
        private int _currentPageNumber;
        private int _numbersTotalPage;
        private int _numberOfRecords;
        private SweetButton buttonPrintReport;
        private Control _mdiForm;
        private bool _closeAfterSelect;
        private string _query;
        private static SearchClientForm _theUniqueInstance1;
        private static SearchClientForm _theUniqueInstance3;
        private TableLayoutPanel tableLayoutPanel1;
        private ListView listViewClient;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeaderName;
        private ColumnHeader columnHeaderPassportNumber;
        private ColumnHeader columnHeaderLoanCycle;
        private ColumnHeader columnHeaderDistrict;
        private ColumnHeader columnHeaderCity;
        private ColumnHeader columnHeaderMemberOf;
        private ColumnHeader columnHeaderActive;
        private GroupBox groupBoxCorporates;
        private RadioButton radioButtonCorporate;
        private RadioButton radioButtonPerson;
        private IClient _client;
        private bool _viewSearchCorporate;
        private GroupBox groupBoxActive;
        private CheckBox checkBoxNotactive;
        private CheckBox checkBoxActive;
        private CheckBox checkBoxVillages;
        private CheckBox checkBoxGroups;
        private CheckBox checkBoxPersons;
        private OClientTypes _clientType;
        private ImageList imageListSort;
        private ListViewSorter Sorter;
        public bool ViewSearchCorporate
        {
            get { return _viewSearchCorporate; }
            set { _viewSearchCorporate = value; }
        }

        public IClient Client
        {
            get { return _client; }
        }

        private void Initialization(Control pMDIForm, bool pCloseAfterSelect)
        {
            _mdiForm = pMDIForm;
            _closeAfterSelect = pCloseAfterSelect;
            InitializeSearchParameters();
            textBoxCurrentlyPage.Text = string.Format("{0} {1} / {2}", 
                MultiLanguageStrings.GetString(Ressource.SearchClientForm, "nbOfPages.Text"), 
                _currentPageNumber, _numbersTotalPage);
            textBoxQuery.Focus();

            Sorter = new ListViewSorter();
            listViewClient.ListViewItemSorter = Sorter;
        }

        public void WatchCorporate(OClientTypes pClientTypes, bool includeOnlyActive)
        {
            groupBoxCorporates.Enabled = false;
            if (pClientTypes == OClientTypes.Person || pClientTypes == OClientTypes.Group || OClientTypes.Village == pClientTypes)
            {
                radioButtonPerson.Checked = true;
                checkBoxPersons.Checked = checkBoxPersons.Checked = pClientTypes == OClientTypes.Person;
                checkBoxGroups.Checked = pClientTypes == OClientTypes.Group;
                checkBoxVillages.Checked = pClientTypes == OClientTypes.Village;
                checkBoxPersons.Enabled = true;
                checkBoxGroups.Enabled = true;
                checkBoxVillages.Enabled = true;
                if (includeOnlyActive)
                {
                    checkBoxActive.Enabled = false;
                    checkBoxActive.Checked = false;
                    checkBoxNotactive.Enabled = false;
                    checkBoxNotactive.Checked = true;
                }
            }
            else
            {
                radioButtonCorporate.Checked = true;
            }
        }

        public static SearchClientForm GetInstance(Control pMDIForm)
        {
            if (_theUniqueInstance1 == null)
                return _theUniqueInstance1 = new SearchClientForm(pMDIForm);
            
            return _theUniqueInstance1;
        }

        public static SearchClientForm GetInstance(OClientTypes pTiersEnum, bool includeNotactiveOnly)
        {
            if (_theUniqueInstance3 == null)
                return _theUniqueInstance3 = new SearchClientForm(pTiersEnum, includeNotactiveOnly);
            
            return _theUniqueInstance3;
        }

        public static SearchClientForm GetInstanceForVillage()
        {
            return new SearchClientForm(OClientTypes.Person, true);
        }

        private int test;

        private SearchClientForm(Control pMDIForm)
        {
            InitializeComponent();
            Initialization(pMDIForm, false);
            test = 1;
        }
        protected SearchClientForm() : this(null)
        {
        }

        protected SearchClientForm(OClientTypes pTiersEnum, bool inludeOnlyNotactive)
        {
            _clientType = pTiersEnum;
            InitializeComponent();
            Initialization(null, true);
            WatchCorporate(pTiersEnum, inludeOnlyNotactive);
            test = 2;
        }
        private void ReinitializeValues()
        {
            labelTitleResult.Text = MultiLanguageStrings.GetString(Ressource.SearchClientForm, "result.Text");
            listViewClient.Items.Clear();
            textBoxCurrentlyPage.Text = String.Empty;
            buttonPrintReport.Enabled = false;
        }
        private void InitializeSearchParameters()
        {
            _currentPageNumber = 1;
            _numberOfRecords = 0;
            _numbersTotalPage = 1;
            this.radioButtonPerson.Checked = true;
        }

        private void ReInitializeSearchParameters()
        {
            _currentPageNumber = 1;
            _numberOfRecords = 0;
            _numbersTotalPage = 1;
        }

        private void DisplayTiers()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                //not active = 0, active = 1, both - 2, none - 3
                int onlyActive = 2;
                if (checkBoxActive.Checked && !checkBoxNotactive.Checked) onlyActive = 1;
                else if (checkBoxNotactive.Checked && !checkBoxActive.Checked) onlyActive = 0;
                else if (!checkBoxNotactive.Checked && !checkBoxActive.Checked) onlyActive = 3;

                if (radioButtonPerson.Checked)
                {
                    List<ClientSearchResult> results;
                    if (OClientTypes.Village == _clientType)
                    {
                        results = ServicesProvider.GetInstance().GetClientServices().FindInactivePersons(_currentPageNumber,
                            out _numbersTotalPage, out _numberOfRecords, _query);
                    }
                    else
                    {
                        results = ServicesProvider.GetInstance().GetClientServices().FindTiers(out _numbersTotalPage, out _numberOfRecords,
                            _query, onlyActive, _currentPageNumber, Convert.ToInt32(checkBoxPersons.Checked), 
                            Convert.ToInt32(checkBoxGroups.Checked), Convert.ToInt32(checkBoxVillages.Checked));
                    }
                    InitializeListViewClientPerson(results);
                }
                else
                {
                    List<ClientSearchResult> result = ServicesProvider.GetInstance().GetClientServices().
                        FindTiersCorporates(onlyActive, _currentPageNumber, out _numbersTotalPage, out _numberOfRecords, _query);
                    InitializeListViewClientCorporate(result);
                }

                labelTitleResult.Text = 
                    string.Format("{0} ({1})", MultiLanguageStrings.GetString(Ressource.SearchClientForm, "result.Text"), _numberOfRecords);
                textBoxCurrentlyPage.Text = 
                    MultiLanguageStrings.GetString(Ressource.SearchClientForm, "nbOfPages.Text") + _currentPageNumber + " / " + _numbersTotalPage;
                
                buttonPrintReport.Enabled = _numberOfRecords != 0;
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void InitializeListViewClientPerson(IEnumerable<ClientSearchResult> pResult)
        {
            listViewClient.Items.Clear();
            foreach (ClientSearchResult result in pResult)
            {
                var item = new ListViewItem();
                item.ImageIndex = result.Type == OClientTypes.Person ? 4 : 5;
                item.Tag = result;
                item.SubItems.Add(result.Name);
                item.SubItems.Add(GetString(result.Active.ToString()));
                item.SubItems.Add(result.PassportNumber);
                item.SubItems.Add(result.Type == OClientTypes.Village ? "-" : result.LoanCycle.ToString());
                item.SubItems.Add(result.District);
                item.SubItems.Add(result.City);
                item.SubItems.Add(result.MemberOf);
                if (result.BadClient) item.BackColor = Color.Red;
                listViewClient.Items.Add(item);
            }
        }
        private void InitializeListViewClientCorporate(IEnumerable<ClientSearchResult> pResult)
        {
            listViewClient.Items.Clear();

            foreach (ClientSearchResult result in pResult)
            {
                var item = new ListViewItem();
                item.ImageIndex = result.Type == OClientTypes.Person ? 4 : 5;
                item.Tag = result;
                item.SubItems.Add(result.Name);
                item.SubItems.Add(result.Active.ToString());
                item.SubItems.Add(result.Siret);
                item.SubItems.Add(Convert.ToString(result.LoanCycle));
                item.SubItems.Add(result.District);
                item.SubItems.Add(result.City);
                if (result.BadClient) item.BackColor = Color.Red;
                listViewClient.Items.Add(item);
            }
        }

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
            if (test == 2) _theUniqueInstance3 = null;
            if (test == 1) _theUniqueInstance1 = null;
        }

        #region Code génér?par le Concepteur Windows Form
        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchClientForm));
            this.labelTitleResult = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxSearchParameters = new System.Windows.Forms.GroupBox();
            this.groupBoxActive = new System.Windows.Forms.GroupBox();
            this.checkBoxNotactive = new System.Windows.Forms.CheckBox();
            this.checkBoxActive = new System.Windows.Forms.CheckBox();
            this.groupBoxCorporates = new System.Windows.Forms.GroupBox();
            this.checkBoxVillages = new System.Windows.Forms.CheckBox();
            this.checkBoxGroups = new System.Windows.Forms.CheckBox();
            this.checkBoxPersons = new System.Windows.Forms.CheckBox();
            this.radioButtonCorporate = new System.Windows.Forms.RadioButton();
            this.radioButtonPerson = new System.Windows.Forms.RadioButton();
            this.buttonPrintReport = new Octopus.GUI.UserControl.SweetButton();
            this.textBoxQuery = new System.Windows.Forms.TextBox();
            this.buttonSearch = new Octopus.GUI.UserControl.SweetButton();
            this.groupBoxButtonBottom = new System.Windows.Forms.GroupBox();
            this.textBoxCurrentlyPage = new System.Windows.Forms.TextBox();
            this.buttonPreview = new Octopus.GUI.UserControl.SweetButton();
            this.buttonNext = new Octopus.GUI.UserControl.SweetButton();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.listViewClient = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderName = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderActive = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderPassportNumber = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderLoanCycle = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderDistrict = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderCity = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderMemberOf = new System.Windows.Forms.ColumnHeader();
            this.imageListSort = new System.Windows.Forms.ImageList(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBoxSearchParameters.SuspendLayout();
            this.groupBoxActive.SuspendLayout();
            this.groupBoxCorporates.SuspendLayout();
            this.groupBoxButtonBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelTitleResult
            // 
            this.labelTitleResult.AccessibleDescription = null;
            this.labelTitleResult.AccessibleName = null;
            resources.ApplyResources(this.labelTitleResult, "labelTitleResult");
            this.labelTitleResult.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.labelTitleResult.ForeColor = System.Drawing.Color.White;
            this.labelTitleResult.Name = "labelTitleResult";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AccessibleDescription = null;
            this.tableLayoutPanel1.AccessibleName = null;
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.BackgroundImage = null;
            this.tableLayoutPanel1.Controls.Add(this.groupBoxSearchParameters, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxButtonBottom, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelTitleResult, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.listViewClient, 0, 2);
            this.tableLayoutPanel1.Font = null;
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // groupBoxSearchParameters
            // 
            this.groupBoxSearchParameters.AccessibleDescription = null;
            this.groupBoxSearchParameters.AccessibleName = null;
            resources.ApplyResources(this.groupBoxSearchParameters, "groupBoxSearchParameters");
            this.groupBoxSearchParameters.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxSearchParameters.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_gris;
            this.groupBoxSearchParameters.Controls.Add(this.groupBoxActive);
            this.groupBoxSearchParameters.Controls.Add(this.groupBoxCorporates);
            this.groupBoxSearchParameters.Controls.Add(this.buttonPrintReport);
            this.groupBoxSearchParameters.Controls.Add(this.textBoxQuery);
            this.groupBoxSearchParameters.Controls.Add(this.buttonSearch);
            this.groupBoxSearchParameters.Font = null;
            this.groupBoxSearchParameters.Name = "groupBoxSearchParameters";
            this.groupBoxSearchParameters.TabStop = false;
            // 
            // groupBoxActive
            // 
            this.groupBoxActive.AccessibleDescription = null;
            this.groupBoxActive.AccessibleName = null;
            resources.ApplyResources(this.groupBoxActive, "groupBoxActive");
            this.groupBoxActive.BackgroundImage = null;
            this.groupBoxActive.Controls.Add(this.checkBoxNotactive);
            this.groupBoxActive.Controls.Add(this.checkBoxActive);
            this.groupBoxActive.Font = null;
            this.groupBoxActive.Name = "groupBoxActive";
            this.groupBoxActive.TabStop = false;
            // 
            // checkBoxNotactive
            // 
            this.checkBoxNotactive.AccessibleDescription = null;
            this.checkBoxNotactive.AccessibleName = null;
            resources.ApplyResources(this.checkBoxNotactive, "checkBoxNotactive");
            this.checkBoxNotactive.BackgroundImage = null;
            this.checkBoxNotactive.Checked = true;
            this.checkBoxNotactive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxNotactive.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.checkBoxNotactive.Name = "checkBoxNotactive";
            this.checkBoxNotactive.UseVisualStyleBackColor = true;
            // 
            // checkBoxActive
            // 
            this.checkBoxActive.AccessibleDescription = null;
            this.checkBoxActive.AccessibleName = null;
            resources.ApplyResources(this.checkBoxActive, "checkBoxActive");
            this.checkBoxActive.BackgroundImage = null;
            this.checkBoxActive.Checked = true;
            this.checkBoxActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxActive.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.checkBoxActive.Name = "checkBoxActive";
            this.checkBoxActive.UseVisualStyleBackColor = true;
            // 
            // groupBoxCorporates
            // 
            this.groupBoxCorporates.AccessibleDescription = null;
            this.groupBoxCorporates.AccessibleName = null;
            resources.ApplyResources(this.groupBoxCorporates, "groupBoxCorporates");
            this.groupBoxCorporates.BackgroundImage = null;
            this.groupBoxCorporates.Controls.Add(this.checkBoxVillages);
            this.groupBoxCorporates.Controls.Add(this.checkBoxGroups);
            this.groupBoxCorporates.Controls.Add(this.checkBoxPersons);
            this.groupBoxCorporates.Controls.Add(this.radioButtonCorporate);
            this.groupBoxCorporates.Controls.Add(this.radioButtonPerson);
            this.groupBoxCorporates.Font = null;
            this.groupBoxCorporates.Name = "groupBoxCorporates";
            this.groupBoxCorporates.TabStop = false;
            // 
            // checkBoxVillages
            // 
            this.checkBoxVillages.AccessibleDescription = null;
            this.checkBoxVillages.AccessibleName = null;
            resources.ApplyResources(this.checkBoxVillages, "checkBoxVillages");
            this.checkBoxVillages.BackgroundImage = null;
            this.checkBoxVillages.Checked = true;
            this.checkBoxVillages.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxVillages.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.checkBoxVillages.Name = "checkBoxVillages";
            this.checkBoxVillages.UseVisualStyleBackColor = true;
            // 
            // checkBoxGroups
            // 
            this.checkBoxGroups.AccessibleDescription = null;
            this.checkBoxGroups.AccessibleName = null;
            resources.ApplyResources(this.checkBoxGroups, "checkBoxGroups");
            this.checkBoxGroups.BackgroundImage = null;
            this.checkBoxGroups.Checked = true;
            this.checkBoxGroups.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxGroups.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.checkBoxGroups.Name = "checkBoxGroups";
            this.checkBoxGroups.UseVisualStyleBackColor = true;
            // 
            // checkBoxPersons
            // 
            this.checkBoxPersons.AccessibleDescription = null;
            this.checkBoxPersons.AccessibleName = null;
            resources.ApplyResources(this.checkBoxPersons, "checkBoxPersons");
            this.checkBoxPersons.BackgroundImage = null;
            this.checkBoxPersons.Checked = true;
            this.checkBoxPersons.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxPersons.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.checkBoxPersons.Name = "checkBoxPersons";
            this.checkBoxPersons.UseVisualStyleBackColor = true;
            // 
            // radioButtonCorporate
            // 
            this.radioButtonCorporate.AccessibleDescription = null;
            this.radioButtonCorporate.AccessibleName = null;
            resources.ApplyResources(this.radioButtonCorporate, "radioButtonCorporate");
            this.radioButtonCorporate.BackgroundImage = null;
            this.radioButtonCorporate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.radioButtonCorporate.Name = "radioButtonCorporate";
            this.radioButtonCorporate.UseVisualStyleBackColor = true;
            this.radioButtonCorporate.CheckedChanged += new System.EventHandler(this.radioButtonCorporate_CheckedChanged);
            // 
            // radioButtonPerson
            // 
            this.radioButtonPerson.AccessibleDescription = null;
            this.radioButtonPerson.AccessibleName = null;
            resources.ApplyResources(this.radioButtonPerson, "radioButtonPerson");
            this.radioButtonPerson.BackColor = System.Drawing.Color.Transparent;
            this.radioButtonPerson.BackgroundImage = null;
            this.radioButtonPerson.Checked = true;
            this.radioButtonPerson.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.radioButtonPerson.Name = "radioButtonPerson";
            this.radioButtonPerson.TabStop = true;
            this.radioButtonPerson.UseVisualStyleBackColor = false;
            this.radioButtonPerson.CheckedChanged += new System.EventHandler(this.radioButtonPerson_CheckedChanged);
            // 
            // buttonPrintReport
            // 
            this.buttonPrintReport.AccessibleDescription = null;
            this.buttonPrintReport.AccessibleName = null;
            resources.ApplyResources(this.buttonPrintReport, "buttonPrintReport");
            this.buttonPrintReport.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonPrintReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonPrintReport.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Print;
            this.buttonPrintReport.Image = global::Octopus.GUI.Properties.Resources.theme1_1_bouton_print;
            this.buttonPrintReport.Menu = null;
            this.buttonPrintReport.Name = "buttonPrintReport";
            this.buttonPrintReport.UseVisualStyleBackColor = false;
            this.buttonPrintReport.Click += new System.EventHandler(this.buttonPrintReport_Click);
            // 
            // textBoxQuery
            // 
            this.textBoxQuery.AccessibleDescription = null;
            this.textBoxQuery.AccessibleName = null;
            resources.ApplyResources(this.textBoxQuery, "textBoxQuery");
            this.textBoxQuery.BackgroundImage = null;
            this.textBoxQuery.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.textBoxQuery.Name = "textBoxQuery";
            this.textBoxQuery.TextChanged += new System.EventHandler(this.textBoxQuery_TextChanged);
            // 
            // buttonSearch
            // 
            this.buttonSearch.AccessibleDescription = null;
            this.buttonSearch.AccessibleName = null;
            resources.ApplyResources(this.buttonSearch, "buttonSearch");
            this.buttonSearch.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonSearch.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Search;
            this.buttonSearch.Image = global::Octopus.GUI.Properties.Resources.theme1_1_search;
            this.buttonSearch.Menu = null;
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.UseVisualStyleBackColor = false;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // groupBoxButtonBottom
            // 
            this.groupBoxButtonBottom.AccessibleDescription = null;
            this.groupBoxButtonBottom.AccessibleName = null;
            resources.ApplyResources(this.groupBoxButtonBottom, "groupBoxButtonBottom");
            this.groupBoxButtonBottom.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_gris_180;
            this.groupBoxButtonBottom.Controls.Add(this.textBoxCurrentlyPage);
            this.groupBoxButtonBottom.Controls.Add(this.buttonPreview);
            this.groupBoxButtonBottom.Controls.Add(this.buttonNext);
            this.groupBoxButtonBottom.Controls.Add(this.buttonCancel);
            this.groupBoxButtonBottom.Font = null;
            this.groupBoxButtonBottom.Name = "groupBoxButtonBottom";
            this.groupBoxButtonBottom.TabStop = false;
            // 
            // textBoxCurrentlyPage
            // 
            this.textBoxCurrentlyPage.AccessibleDescription = null;
            this.textBoxCurrentlyPage.AccessibleName = null;
            resources.ApplyResources(this.textBoxCurrentlyPage, "textBoxCurrentlyPage");
            this.textBoxCurrentlyPage.BackgroundImage = null;
            this.textBoxCurrentlyPage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.textBoxCurrentlyPage.Name = "textBoxCurrentlyPage";
            // 
            // buttonPreview
            // 
            this.buttonPreview.AccessibleDescription = null;
            this.buttonPreview.AccessibleName = null;
            resources.ApplyResources(this.buttonPreview, "buttonPreview");
            this.buttonPreview.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonPreview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonPreview.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Previous;
            this.buttonPreview.Image = global::Octopus.GUI.Properties.Resources.theme1_1_bouton_previous;
            this.buttonPreview.Menu = null;
            this.buttonPreview.Name = "buttonPreview";
            this.buttonPreview.UseVisualStyleBackColor = false;
            this.buttonPreview.Click += new System.EventHandler(this.buttonPreview_Click);
            // 
            // buttonNext
            // 
            this.buttonNext.AccessibleDescription = null;
            this.buttonNext.AccessibleName = null;
            resources.ApplyResources(this.buttonNext, "buttonNext");
            this.buttonNext.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonNext.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonNext.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Next;
            this.buttonNext.Image = global::Octopus.GUI.Properties.Resources.theme1_1_bouton_next;
            this.buttonNext.Menu = null;
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.UseVisualStyleBackColor = false;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.AccessibleDescription = null;
            this.buttonCancel.AccessibleName = null;
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonCancel.Image = global::Octopus.GUI.Properties.Resources.theme1_1_bouton_close;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // listViewClient
            // 
            this.listViewClient.AccessibleDescription = null;
            this.listViewClient.AccessibleName = null;
            resources.ApplyResources(this.listViewClient, "listViewClient");
            this.listViewClient.BackgroundImage = null;
            this.listViewClient.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeaderName,
            this.columnHeaderActive,
            this.columnHeaderPassportNumber,
            this.columnHeaderLoanCycle,
            this.columnHeaderDistrict,
            this.columnHeaderCity,
            this.columnHeaderMemberOf});
            this.listViewClient.Font = null;
            this.listViewClient.FullRowSelect = true;
            this.listViewClient.GridLines = true;
            this.listViewClient.Name = "listViewClient";
            this.listViewClient.SmallImageList = this.imageListSort;
            this.listViewClient.UseCompatibleStateImageBehavior = false;
            this.listViewClient.View = System.Windows.Forms.View.Details;
            this.listViewClient.DoubleClick += new System.EventHandler(this.listViewClient_DoubleClick);
            this.listViewClient.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewClient_ColumnClick);
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // columnHeaderName
            // 
            resources.ApplyResources(this.columnHeaderName, "columnHeaderName");
            // 
            // columnHeaderActive
            // 
            resources.ApplyResources(this.columnHeaderActive, "columnHeaderActive");
            // 
            // columnHeaderPassportNumber
            // 
            resources.ApplyResources(this.columnHeaderPassportNumber, "columnHeaderPassportNumber");
            // 
            // columnHeaderLoanCycle
            // 
            resources.ApplyResources(this.columnHeaderLoanCycle, "columnHeaderLoanCycle");
            // 
            // columnHeaderDistrict
            // 
            resources.ApplyResources(this.columnHeaderDistrict, "columnHeaderDistrict");
            // 
            // columnHeaderCity
            // 
            resources.ApplyResources(this.columnHeaderCity, "columnHeaderCity");
            // 
            // columnHeaderMemberOf
            // 
            resources.ApplyResources(this.columnHeaderMemberOf, "columnHeaderMemberOf");
            // 
            // imageListSort
            // 
            this.imageListSort.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListSort.ImageStream")));
            this.imageListSort.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListSort.Images.SetKeyName(0, "");
            this.imageListSort.Images.SetKeyName(1, "");
            this.imageListSort.Images.SetKeyName(2, "theme1.1_bouton_down_small.png");
            this.imageListSort.Images.SetKeyName(3, "theme1.1_bouton_up_small.png");
            this.imageListSort.Images.SetKeyName(4, "new_client.png");
            this.imageListSort.Images.SetKeyName(5, "new_group.png");
            // 
            // SearchClientForm
            // 
            this.AcceptButton = this.buttonSearch;
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = null;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = null;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SearchClientForm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBoxSearchParameters.ResumeLayout(false);
            this.groupBoxSearchParameters.PerformLayout();
            this.groupBoxActive.ResumeLayout(false);
            this.groupBoxActive.PerformLayout();
            this.groupBoxCorporates.ResumeLayout(false);
            this.groupBoxCorporates.PerformLayout();
            this.groupBoxButtonBottom.ResumeLayout(false);
            this.groupBoxButtonBottom.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private void buttonSearch_Click(object sender, System.EventArgs e)
        {
            ReInitializeSearchParameters();
            DisplayTiers();
        }

        private void buttonCancel_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void buttonPreview_Click(object sender, System.EventArgs e)
        {
            if (_currentPageNumber != 1) _currentPageNumber--;
            DisplayTiers();
        }

        private void buttonNext_Click(object sender, System.EventArgs e)
        {
            if (_currentPageNumber != _numbersTotalPage) _currentPageNumber++;
            DisplayTiers();
        }

        private void buttonPrintReport_Click(object sender, EventArgs e)
        {
            InitializeClientListReport();
        }

        public void InitializeClientListReport()
        {
            //List<ClientSearchResult> allClient =  ServicesProvider.GetInstance().GetClientServices().FindAllTiers(out _numberOfRecords, _query);
            //DataSet dsTiers = new DataSet();
            //DataTable dsTable = dsTiers.Tables.Add();
            //dsTable.Columns.Add("Name");
            //dsTable.Columns.Add("Active");
            //dsTable.Columns.Add("passport_number");
            //dsTable.Columns.Add("loan_cycle");
            //dsTable.Columns.Add("District");
            //dsTable.Columns.Add("City");
            //dsTable.Columns.Add("MemberOf");
            //foreach (ClientSearchResult client in allClient)
            //{
            //    dsTable.Rows.Add(client.Name,client.Active, client.PassportNumber, client.LoanCycle, client.District, client.City, client.MemberOf);
            //}
            //FrmReportViewer viewer = new FrmReportViewer("ClientList.rpt", null, dsTiers, OInternalReports.ClientList);
            //viewer.Show();
        }

        private void textBoxQuery_TextChanged(object sender, EventArgs e)
        {
            _query = textBoxQuery.Text;
            ReinitializeValues();
        }

        private void listViewClient_DoubleClick(object sender, EventArgs e)
        {
            var result = (ClientSearchResult) listViewClient.SelectedItems[0].Tag;
            HandleTierSelect(result);

            if (_closeAfterSelect)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        protected virtual void HandleTierSelect(ClientSearchResult pClient)
        {
            try
            {
                _client = ServicesProvider.GetInstance().GetClientServices().FindTiers(pClient.Id, pClient.Type);

                /*
                 * This code is for loading compulsory savings. Compulsory savings are being 
                 * loaded here because in LoanManager class SavingsManager trigers problems.
                 * Ruslan Kazakov
                 */
                if (_client.Projects != null)
                    foreach (Project project in _client.Projects)
                        if (project.Credits != null)
                            foreach (Loan loan in project.Credits)
                                loan.CompulsorySavings = ServicesProvider.GetInstance().GetSavingServices().GetSavingForLoan(loan.Id, true);

                if (test != 2)
                {
                    if (pClient.Type == OClientTypes.Person)
                        ((LotrasmicMainWindowForm)_mdiForm).InitializePersonForm((Person)_client, null);
                    if (pClient.Type == OClientTypes.Group)
                        ((LotrasmicMainWindowForm)_mdiForm).InitializeGroupForm((Group)_client, null);
                    if (pClient.Type == OClientTypes.Corporate)
                        ((LotrasmicMainWindowForm)_mdiForm).InitializeCorporateForm((Corporate)_client, null);
                    if (OClientTypes.Village == pClient.Type)
                        ((LotrasmicMainWindowForm)_mdiForm).InitializeVillageForm((Village)_client);
                }
                else DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
        }
        private void radioButtonPerson_CheckedChanged(object sender, EventArgs e)
        {
            listViewClient.Items.Clear();
            listViewClient.Columns[3].Name = "PassportNumber";
            listViewClient.Columns[3].Text = MultiLanguageStrings.GetString(Ressource.SearchClientForm, "PassportNumber.Text");// "Passeport's Number";
            labelTitleResult.Text = string.Empty;
            if (radioButtonPerson.Checked)
            {
                checkBoxPersons.Enabled = true;
                checkBoxGroups.Enabled = true;
                checkBoxVillages.Enabled = true;
            }
        }
        private void radioButtonCorporate_CheckedChanged(object sender, EventArgs e)
        {
            listViewClient.Items.Clear();
            listViewClient.Columns[3].Name = "Siret";
            listViewClient.Columns[3].Text = "Siret";
            labelTitleResult.Text = string.Empty;
            if (radioButtonCorporate.Checked)
            {
                checkBoxPersons.Enabled = false;
                checkBoxGroups.Enabled = false;
                checkBoxVillages.Enabled = false;
            }
        }

        private void listViewClient_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            listViewClient.Columns[Sorter.LastSort].ImageIndex = -1;
            if (Sorter.LastSort == e.Column)
            {
                if (listViewClient.Sorting == SortOrder.Ascending)
                    listViewClient.Sorting = SortOrder.Descending;
                else
                    listViewClient.Sorting = SortOrder.Ascending;
            }
            else
            {
                listViewClient.Sorting = SortOrder.Descending;
            }
            Sorter.ByColumn = e.Column;
            Sorter.reset = true;

            if (listViewClient.Items.Count > 0)
                listViewClient.Columns[Sorter.ByColumn].ImageIndex = listViewClient.Sorting == SortOrder.Ascending ? 2 : 3;

            listViewClient.Sort();
        }
    }
}
