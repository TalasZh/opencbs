// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using OpenCBS.CoreDomain.Clients;
using OpenCBS.GUI.UserControl;
using OpenCBS.Services;
using OpenCBS.CoreDomain.SearchResult;
using OpenCBS.MultiLanguageRessources;
using OpenCBS.ExceptionsHandler;
using OpenCBS.CoreDomain;
using OpenCBS.Enums;

namespace OpenCBS.GUI.Projets
{
    public partial class SearchProjectForm : Form
    {
        private Control _mdiForm;
        private bool _closeAfterSelect;
        private IClient _client;
        private string _query;
        private int  _currentPageNumber ;
        private int   _numberOfRecords;
        private int   _numbersTotalPage;
        private Project _projet;
        private static SearchProjectForm _theInstance;
        private ListViewSorter Sorter;

        public SearchProjectForm()
        {
            InitializeComponent();
            _AddColumnSorter();
        }
        public SearchProjectForm(Control pMDIForm, bool pCloseAfterSelect)
        {
            InitializeComponent();
            _Initialization(pMDIForm, pCloseAfterSelect);
            _InitializeSearchParameters();
            _AddColumnSorter();
        }
        public SearchProjectForm(Control pMDIForm)
        {
            InitializeComponent();
            _Initialization(pMDIForm);
            _InitializeSearchParameters();
            _AddColumnSorter();
        }

        private void _AddColumnSorter()
        {
            Sorter = new ListViewSorter();
            listViewClient.ListViewItemSorter = Sorter;
        }

        private void _Initialization(Control pMDIForm)
        {
            _mdiForm = pMDIForm;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            _ReInitializeSearchParameters();
            _DisplayTiers();
        }
        private void _Initialization(Control pMDIForm, bool pCloseAfterSelect)
        {
            _mdiForm = pMDIForm;
            _closeAfterSelect = pCloseAfterSelect;
        }
        private void _ReInitializeSearchParameters()
        {
            _currentPageNumber = 1;
            _numberOfRecords = 0;
            _numbersTotalPage = 1;
        }
        private void _InitializeListViewProject(IEnumerable<ProjetSearchResult> pResult)
        {
            listViewClient.Items.Clear();
            foreach (ProjetSearchResult result in pResult)
            {
                ListViewItem item = new ListViewItem();
                item.Tag = result;
                item.SubItems.Add(result.Code);
                item.SubItems.Add(result.ProjectName);
                item.SubItems.Add(result.Entite);
                item.SubItems.Add(result.Aim);
                item.SubItems.Add(result.Status.ToString());
                listViewClient.Items.Add(item);
            }

        }

        private void _InitializeSearchParameters()
        {
            _currentPageNumber = 1;
            _numberOfRecords = 0;
            _numbersTotalPage = 1;
            _query = String.Empty;
        }
        private void _ReinitializeValues()
        {
            labelTitleResult.Text = MultiLanguageStrings.GetString(Ressource.SearchClientForm, "result.Text");
            listViewClient.Items.Clear();
            textBoxCurrentlyPage.Text = String.Empty;
            buttonPrintReport.Enabled = false;
        }
   
        private void _DisplayTiers()
        {
          try
            {
                Cursor = Cursors.WaitCursor;
                List<ProjetSearchResult> result = ServicesProvider.GetInstance().GetProjectServices().FindProjectByCriteres(_currentPageNumber, out _numbersTotalPage, out _numberOfRecords, _query);
                _InitializeListViewProject(result);
                
                labelTitleResult.Text = string.Format("{0} ({1})", MultiLanguageStrings.GetString(Ressource.SearchClientForm, "result.Text"), _numberOfRecords);
                textBoxCurrentlyPage.Text = MultiLanguageStrings.GetString(Ressource.SearchClientForm, "nbOfPages.Text") + _currentPageNumber + " / " + _numbersTotalPage;
                if (_numberOfRecords != 0)
                    buttonPrintReport.Enabled = true;
                else
                    buttonPrintReport.Enabled = false;

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

        private void textBoxQuery_TextChanged(object sender, EventArgs e)
        {
            _query = textBoxQuery.Text;
            _ReinitializeValues();
        }
      
        public static SearchProjectForm GetInstance(Control pMDIForm)
        {
            return _theInstance ?? (_theInstance = new SearchProjectForm(pMDIForm));
        }

        private void listViewClient_DoubleClick(object sender, EventArgs e)
        {
            ProjetSearchResult result = (ProjetSearchResult)listViewClient.SelectedItems[0].Tag;
            _CheckTiersAndDisplayIt(result);
        }
        private void _CheckTiersAndDisplayIt(ProjetSearchResult result)
        {
            try
            {

                _client = ServicesProvider.GetInstance().GetClientServices().FindTiers(result.TiersId, result.Type);
                _projet = ServicesProvider.GetInstance().GetProjectServices().FindProjectById(result.Id);
                _projet.Client = _client;
               

                if (result.Type == OClientTypes.Person)
                        ((LotrasmicMainWindowForm)_mdiForm).InitializePersonForm((Person)_client,_projet);
                if (result.Type == OClientTypes.Group)
                        ((LotrasmicMainWindowForm)_mdiForm).InitializeGroupForm((Group)_client,_projet);
                if (result.Type == OClientTypes.Corporate)
                        ((LotrasmicMainWindowForm)_mdiForm).InitializeCorporateForm((Corporate)_client,_projet);
               
                    Close();
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
        }

        private void buttonPreview_Click(object sender, EventArgs e)
        {
            if (_currentPageNumber != 1) _currentPageNumber--;
            _DisplayTiers();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (_currentPageNumber != _numbersTotalPage) _currentPageNumber++;
            _DisplayTiers();
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
