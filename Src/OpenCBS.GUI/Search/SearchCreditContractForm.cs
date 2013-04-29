//Octopus MFS is an integrated suite for managing a Micro Finance Institution: clients, contracts, accounting, reporting and risk
//Copyright � 2006,2007 OCTO Technology & OXUS Development Network
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
using System.ComponentModel;
using System.Windows.Forms;
using Octopus.CoreDomain;
using Octopus.CoreDomain.Clients;
using Octopus.CoreDomain.SearchResult;
using Octopus.ExceptionsHandler;
using Octopus.GUI.UserControl;
using Octopus.MultiLanguageRessources;
using Octopus.Services;
using Octopus.Enums;

namespace Octopus.GUI
{
    using Octopus.CoreDomain.Contracts.Loans;

    /// <summary>
    /// Description r�sum�e de SearchClientForm.
    /// </summary>
    public class SearchCreditContractForm : SweetBaseForm
    {
        private GroupBox groupBoxButtonBottom;
        private TextBox textBoxCurrentlyPage;
        private System.Windows.Forms.Button buttonPreview;
        private System.Windows.Forms.Button buttonNext;
        private Label lblTitle;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSearch;
        private GroupBox groupBoxSearchParameters;
        private TextBox textBoxQuery;
        private IContainer components;
        private int _currentPageNumber;
        private int _numberOfRecords;
        private int _numbersTotalPage;
        private Control _mdiForm;
        private TableLayoutPanel tableLayoutPanel1;
        private ListView listViewContracts;
        private ColumnHeader columnHeaderImage;
        private ColumnHeader columnHeaderContractCode;
        private ColumnHeader columnHeaderClientType;
        private ColumnHeader columnHeaderClientName;
        private ColumnHeader columnHeaderLoanOfficer;
        private ColumnHeader columnHeaderStartDate;
        private ColumnHeader columnHeaderCloseDate;
        
        private string _query;
        private ImageList imageListClient;
        private ColumnHeader columnHeaderStatus;
        private GroupBox groupBoxContractType;
        private ColumnHeader columnHeaderContractType;
        private RadioButton radioButtonSavingContract;
        private RadioButton radioButtonCreditContract;
        private static SearchCreditContractForm _theUniqueInstance;
        private bool _isSearchSavingContractForTransfer;
        private bool _isSearchLoanContractForCompulsory;
        private ImageList imageListSort;
        private ListViewSorter Sorter;

        public SavingSearchResult SelectedSavingContract { get; private set; }
        public CreditSearchResult SelectedLoanContract { get; private set; }

        private SearchCreditContractForm(Control pMDIForm)
        {
            Initialization(pMDIForm);
        }

        public static SearchCreditContractForm GetInstance(Control pMDIForm)
        {
            if (_theUniqueInstance == null)
                return _theUniqueInstance = new SearchCreditContractForm(pMDIForm);
             if (_theUniqueInstance._mdiForm == null)
                    _theUniqueInstance._mdiForm = pMDIForm;
                return _theUniqueInstance;
        }

        private void Initialization(Control pMDIForm)
        {
            InitializeComponent();
            _mdiForm = pMDIForm;
            InitializeSearchParameters();

            Sorter = new ListViewSorter();
            listViewContracts.ListViewItemSorter = Sorter;
        }

        public DialogResult ShowForSearchSavingsContract()
        {
            Visible = false;

            _isSearchSavingContractForTransfer = true;
            
            radioButtonSavingContract.Checked = true;
            groupBoxContractType.Enabled = false;

            ReInitializeValues();
            ReInitializeSearchParameters();

            return this.ShowDialog();
        }

        public DialogResult ShowForSearchSavingsContractForTransfer(string pClient)
        {
            this.Visible = false;

            _isSearchSavingContractForTransfer = true;

            radioButtonSavingContract.Checked = true;
            groupBoxContractType.Enabled = false;

            ReInitializeValues();
            ReInitializeSearchParameters();

            textBoxQuery.Text = pClient;

            return ShowDialog();
        }

        public DialogResult ShowForSearchLoanContractForCompulsory(string pClient)
        {
            Visible = false;

            _isSearchLoanContractForCompulsory = true;

            radioButtonCreditContract.Checked = true;
            groupBoxContractType.Enabled = false;

            ReInitializeValues();
            ReInitializeSearchParameters();

            textBoxQuery.Text = pClient;

            return ShowDialog();
        }

        public DialogResult SearchContracts(string pClient)
        {
            Visible = false;

            _isSearchLoanContractForCompulsory = true;
            _isSearchSavingContractForTransfer = true;
            SelectedLoanContract = null;
            SelectedSavingContract = null;
            radioButtonCreditContract.Checked = true;
            ReInitializeValues();
            ReInitializeSearchParameters();

            textBoxQuery.Text = pClient;

            return ShowDialog();
        }

        public DialogResult ShowForSearchCompulsorySavings(string pClient)
        {
            Visible = false;

            _isSearchSavingContractForTransfer = true;
            radioButtonSavingContract.Checked = true;
            groupBoxContractType.Enabled = false;

            ReInitializeValues();
            ReInitializeSearchParameters();

            textBoxQuery.Text = pClient;
            return ShowDialog();
        }

        private void ReInitializeValues()
        {
            lblTitle.Text = MultiLanguageStrings.GetString(Ressource.SearchCreditContractForm, "result.Text");
            textBoxCurrentlyPage.Text = String.Empty;
            listViewContracts.Items.Clear();
        }

        private void InitializeSearchParameters()
        {
            _currentPageNumber = 1;
            _numberOfRecords = 0;
            _numbersTotalPage = 1;
            _query = String.Empty;
        }

        private void CheckContractAndDisplayIt(CreditSearchResult pCredit)
        {
            if (!_isSearchLoanContractForCompulsory)
            {
                try
                {
                    ClientServices clientServices = ServicesProvider.GetInstance().GetClientServices();

                    // if client has no contract (i.e. in Village)
                    if (pCredit.Id == 0)
                    {
                        MessageBox.Show(@"This client in a village has no contract yet!");
                    }
                    else
                    {
                        IClient client = clientServices.FindTiersByContractId(pCredit.Id);

                        if (clientServices.CheckIfTiersIsValid(client))
                        {
                            ((LotrasmicMainWindowForm)_mdiForm).InitializeCreditContractForm(client, pCredit.Id);
                        }
                    }
                }
                catch (Exception ex)
                {
                    new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
                }
            }
            else
            {
                groupBoxContractType.Enabled = true;
                _isSearchSavingContractForTransfer = false;
                SelectedLoanContract = pCredit;
                DialogResult = DialogResult.OK;
            }
        }

        private void CheckContractAndDisplayIt(SavingSearchResult saving)
        {
            if (!_isSearchSavingContractForTransfer)
            {
                try
                {
                    ClientServices clientServices = ServicesProvider.GetInstance().GetClientServices();

                    IClient client = ServicesProvider.GetInstance().GetClientServices().FindTiers(saving.ClientId, saving.ClientType);

                    if (clientServices.CheckIfTiersIsValid(client))
                    {
                        ((LotrasmicMainWindowForm)_mdiForm).InitializeSavingContractForm(client, saving.Id);
                    }
                }
                catch (Exception ex)
                {
                    new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
                }
            }
            else
            {
                groupBoxContractType.Enabled = true;
                _isSearchSavingContractForTransfer = false;
                SelectedSavingContract = saving;
                DialogResult = DialogResult.OK;
            }
        }

        private void DisplayContracts()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (radioButtonCreditContract.Checked)
                {
                    List<CreditSearchResult> creditResult = ServicesProvider.GetInstance().GetContractServices().
                        FindContracts(_currentPageNumber, out _numbersTotalPage, out _numberOfRecords, _query);
                    InitializeListViewContract(creditResult);
                }
                if (radioButtonSavingContract.Checked)
                {
                    bool activeContractsOnly = _isSearchSavingContractForTransfer;
                    List<SavingSearchResult> savingResult = ServicesProvider.GetInstance().GetSavingServices().FindContracts(_currentPageNumber,
                        out _numbersTotalPage, out _numberOfRecords, _query, _isSearchSavingContractForTransfer, activeContractsOnly);
                    _numberOfRecords = savingResult.Count;
                    
                    InitializeListViewContract(savingResult);
                }

                lblTitle.Text = string.Format("{0} ({1})", MultiLanguageStrings.GetString(Ressource.SearchCreditContractForm, "result.Text"), _numberOfRecords);
                textBoxCurrentlyPage.Text = string.Format("{0}{1} / {2}", MultiLanguageStrings.GetString(Ressource.SearchCreditContractForm, "page.Text"), _currentPageNumber, _numbersTotalPage);
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

        private void InitializeListViewContract(IEnumerable<CreditSearchResult> pResult)
        {
            listViewContracts.Items.Clear();
            foreach (CreditSearchResult result in pResult)
            {
                ListViewItem item = new ListViewItem();
                if (result.ClientType == "G")
                    item.ImageIndex = 1;
                else
                    item.ImageIndex = 0;
                item.Tag = result;
                item.SubItems.Add(result.ContractCode);
                item.SubItems.Add(result.ClientType);
                item.SubItems.Add(result.ClientName);
                item.SubItems.Add(result.LoanOfficer.ToString());
                item.SubItems.Add(result.ContractStartDate);
                item.SubItems.Add(result.ContractEndDate);
                item.SubItems.Add(GetString(result.ContractStatus));
                item.SubItems.Add("Credit");
                listViewContracts.Items.Add(item);
            }
        }

        private void InitializeListViewContract(IEnumerable<SavingSearchResult> pResult)
        {
            listViewContracts.Items.Clear();
            foreach (SavingSearchResult result in pResult)
            {
                ListViewItem item = new ListViewItem();
                if (result.ClientType == OClientTypes.Group)
                    item.ImageIndex = 1;
                else
                    item.ImageIndex = 0;
                item.Tag = result;
                item.SubItems.Add(result.ContractCode);
                item.SubItems.Add(result.ClientTypeCode);
                item.SubItems.Add(result.ClientName);
                item.SubItems.Add(result.LoanOfficer.ToString());
                item.SubItems.Add(result.ContractStartDate.ToShortDateString());
                item.SubItems.Add(result.ContractEndDate.HasValue ? result.ContractEndDate.Value.ToShortDateString() : "");
                item.SubItems.Add(MultiLanguageStrings.GetString(Ressource.SearchCreditContractForm, result.Status + ".Text"));
                item.SubItems.Add(MultiLanguageStrings.GetString(Ressource.SearchCreditContractForm, 
                    result.ContractType == "B" ? "SavingsBook.Text" : result.ContractType == "T" ? "SavingsDeposit.Text" : "CompulsorySavings.Text"));
                listViewContracts.Items.Add(item);
            }
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchCreditContractForm));
            this.groupBoxSearchParameters = new System.Windows.Forms.GroupBox();
            this.groupBoxContractType = new System.Windows.Forms.GroupBox();
            this.radioButtonSavingContract = new System.Windows.Forms.RadioButton();
            this.radioButtonCreditContract = new System.Windows.Forms.RadioButton();
            this.textBoxQuery = new System.Windows.Forms.TextBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.groupBoxButtonBottom = new System.Windows.Forms.GroupBox();
            this.textBoxCurrentlyPage = new System.Windows.Forms.TextBox();
            this.buttonPreview = new System.Windows.Forms.Button();
            this.buttonNext = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.listViewContracts = new System.Windows.Forms.ListView();
            this.columnHeaderImage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderContractCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderClientType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderClientName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderLoanOfficer = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderStartDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderCloseDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderContractType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageListSort = new System.Windows.Forms.ImageList(this.components);
            this.imageListClient = new System.Windows.Forms.ImageList(this.components);
            this.groupBoxSearchParameters.SuspendLayout();
            this.groupBoxContractType.SuspendLayout();
            this.groupBoxButtonBottom.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxSearchParameters
            // 
            this.groupBoxSearchParameters.Controls.Add(this.groupBoxContractType);
            this.groupBoxSearchParameters.Controls.Add(this.textBoxQuery);
            this.groupBoxSearchParameters.Controls.Add(this.buttonSearch);
            resources.ApplyResources(this.groupBoxSearchParameters, "groupBoxSearchParameters");
            this.groupBoxSearchParameters.Name = "groupBoxSearchParameters";
            this.groupBoxSearchParameters.TabStop = false;
            // 
            // groupBoxContractType
            // 
            this.groupBoxContractType.Controls.Add(this.radioButtonSavingContract);
            this.groupBoxContractType.Controls.Add(this.radioButtonCreditContract);
            resources.ApplyResources(this.groupBoxContractType, "groupBoxContractType");
            this.groupBoxContractType.Name = "groupBoxContractType";
            this.groupBoxContractType.TabStop = false;
            // 
            // radioButtonSavingContract
            // 
            resources.ApplyResources(this.radioButtonSavingContract, "radioButtonSavingContract");
            this.radioButtonSavingContract.Name = "radioButtonSavingContract";
            // 
            // radioButtonCreditContract
            // 
            resources.ApplyResources(this.radioButtonCreditContract, "radioButtonCreditContract");
            this.radioButtonCreditContract.Checked = true;
            this.radioButtonCreditContract.Name = "radioButtonCreditContract";
            this.radioButtonCreditContract.TabStop = true;
            // 
            // textBoxQuery
            // 
            resources.ApplyResources(this.textBoxQuery, "textBoxQuery");
            this.textBoxQuery.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.textBoxQuery.Name = "textBoxQuery";
            this.textBoxQuery.TextChanged += new System.EventHandler(this.textBoxQuery_TextChanged);
            // 
            // buttonSearch
            // 
            resources.ApplyResources(this.buttonSearch, "buttonSearch");
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(152)))));
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Name = "lblTitle";
            // 
            // groupBoxButtonBottom
            // 
            this.groupBoxButtonBottom.Controls.Add(this.textBoxCurrentlyPage);
            this.groupBoxButtonBottom.Controls.Add(this.buttonPreview);
            this.groupBoxButtonBottom.Controls.Add(this.buttonCancel);
            this.groupBoxButtonBottom.Controls.Add(this.buttonNext);
            resources.ApplyResources(this.groupBoxButtonBottom, "groupBoxButtonBottom");
            this.groupBoxButtonBottom.Name = "groupBoxButtonBottom";
            this.groupBoxButtonBottom.TabStop = false;
            // 
            // textBoxCurrentlyPage
            // 
            resources.ApplyResources(this.textBoxCurrentlyPage, "textBoxCurrentlyPage");
            this.textBoxCurrentlyPage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.textBoxCurrentlyPage.Name = "textBoxCurrentlyPage";
            // 
            // buttonPreview
            // 
            resources.ApplyResources(this.buttonPreview, "buttonPreview");
            this.buttonPreview.Name = "buttonPreview";
            this.buttonPreview.Click += new System.EventHandler(this.buttonPreview_Click);
            // 
            // buttonNext
            // 
            resources.ApplyResources(this.buttonNext, "buttonNext");
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.groupBoxSearchParameters, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxButtonBottom, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblTitle, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.listViewContracts, 0, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // listViewContracts
            // 
            this.listViewContracts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderImage,
            this.columnHeaderContractCode,
            this.columnHeaderClientType,
            this.columnHeaderClientName,
            this.columnHeaderLoanOfficer,
            this.columnHeaderStartDate,
            this.columnHeaderCloseDate,
            this.columnHeaderStatus,
            this.columnHeaderContractType});
            resources.ApplyResources(this.listViewContracts, "listViewContracts");
            this.listViewContracts.FullRowSelect = true;
            this.listViewContracts.GridLines = true;
            this.listViewContracts.MultiSelect = false;
            this.listViewContracts.Name = "listViewContracts";
            this.listViewContracts.SmallImageList = this.imageListSort;
            this.listViewContracts.UseCompatibleStateImageBehavior = false;
            this.listViewContracts.View = System.Windows.Forms.View.Details;
            this.listViewContracts.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewContracts_ColumnClick);
            this.listViewContracts.DoubleClick += new System.EventHandler(this.listViewContracts_DoubleClick);
            // 
            // columnHeaderImage
            // 
            resources.ApplyResources(this.columnHeaderImage, "columnHeaderImage");
            // 
            // columnHeaderContractCode
            // 
            resources.ApplyResources(this.columnHeaderContractCode, "columnHeaderContractCode");
            // 
            // columnHeaderClientType
            // 
            resources.ApplyResources(this.columnHeaderClientType, "columnHeaderClientType");
            // 
            // columnHeaderClientName
            // 
            resources.ApplyResources(this.columnHeaderClientName, "columnHeaderClientName");
            // 
            // columnHeaderLoanOfficer
            // 
            resources.ApplyResources(this.columnHeaderLoanOfficer, "columnHeaderLoanOfficer");
            // 
            // columnHeaderStartDate
            // 
            resources.ApplyResources(this.columnHeaderStartDate, "columnHeaderStartDate");
            // 
            // columnHeaderCloseDate
            // 
            resources.ApplyResources(this.columnHeaderCloseDate, "columnHeaderCloseDate");
            // 
            // columnHeaderStatus
            // 
            resources.ApplyResources(this.columnHeaderStatus, "columnHeaderStatus");
            // 
            // columnHeaderContractType
            // 
            resources.ApplyResources(this.columnHeaderContractType, "columnHeaderContractType");
            // 
            // imageListSort
            // 
            this.imageListSort.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListSort.ImageStream")));
            this.imageListSort.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListSort.Images.SetKeyName(0, "flecheGD.bmp");
            this.imageListSort.Images.SetKeyName(1, "flecheDG.bmp");
            this.imageListSort.Images.SetKeyName(2, "theme1.1_bouton_down_small.png");
            this.imageListSort.Images.SetKeyName(3, "theme1.1_bouton_up_small.png");
            // 
            // imageListClient
            // 
            this.imageListClient.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListClient.ImageStream")));
            this.imageListClient.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListClient.Images.SetKeyName(0, "new_client.png");
            this.imageListClient.Images.SetKeyName(1, "new_group.png");
            // 
            // SearchCreditContractForm
            // 
            this.AcceptButton = this.buttonSearch;
            this.CancelButton = this.buttonCancel;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "SearchCreditContractForm";
            this.groupBoxSearchParameters.ResumeLayout(false);
            this.groupBoxSearchParameters.PerformLayout();
            this.groupBoxContractType.ResumeLayout(false);
            this.groupBoxContractType.PerformLayout();
            this.groupBoxButtonBottom.ResumeLayout(false);
            this.groupBoxButtonBottom.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

        /// <summary>
        /// Nettoyage des ressources utilis�es.
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
            _theUniqueInstance = null;
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            ReInitializeSearchParameters();
            DisplayContracts();
        }
        private void ReInitializeSearchParameters()
        {
            _currentPageNumber = 1;
            _numberOfRecords = 0;
            _numbersTotalPage = 1;

        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonPreview_Click(object sender, EventArgs e)
        {
            if (_currentPageNumber != 1) _currentPageNumber--;
            DisplayContracts();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (_currentPageNumber != _numbersTotalPage) _currentPageNumber++;
            DisplayContracts();
        }

        private void textBoxQuery_TextChanged(object sender, EventArgs e)
        {
            _query = textBoxQuery.Text;
            ReInitializeValues();
        }

        private void listViewContracts_DoubleClick(object sender, EventArgs e)
        {
            object result = listViewContracts.SelectedItems[0].Tag;
            if (result is CreditSearchResult)
            {
                CreditSearchResult csr = (CreditSearchResult) result;
                if (!csr.IsViewableBy(User.CurrentUser))
                {
                    Fail("cannotView");
                    return;
                }
                CheckContractAndDisplayIt((CreditSearchResult) result);
            }
            else
            {
                SavingSearchResult ssr = (SavingSearchResult) result;
                if (!ssr.IsViewableBy(User.CurrentUser))
                {
                    Fail("cannotView");
                    return;
                }
                CheckContractAndDisplayIt((SavingSearchResult) result);
            }
        }

        private void listViewContracts_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            listViewContracts.Columns[Sorter.LastSort].ImageIndex = -1;
            if (Sorter.LastSort == e.Column)
            {
                if (listViewContracts.Sorting == SortOrder.Ascending)
                    listViewContracts.Sorting = SortOrder.Descending;
                else
                    listViewContracts.Sorting = SortOrder.Ascending;
            }
            else
            {
                listViewContracts.Sorting = SortOrder.Descending;
            }
            Sorter.ByColumn = e.Column;
            Sorter.reset = true;

            if (listViewContracts.Items.Count > 0)
                listViewContracts.Columns[Sorter.ByColumn].ImageIndex = listViewContracts.Sorting == SortOrder.Ascending ? 2 : 3;
            
            listViewContracts.Sort();
        }
    }
}