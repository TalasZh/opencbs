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
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using OpenCBS.GUI.Report_Browser;
using OpenCBS.GUI.UserControl;
using OpenCBS.MultiLanguageRessources;
using OpenCBS.Reports;
using OpenCBS.Reports.Forms;
using OpenCBS.Services;
using OpenCBS.Shared;
using System.Collections;

namespace OpenCBS.GUI.Accounting
{
    /// <summary>
	/// Summary description for ExportTransactions.
	/// </summary>
    public class ExportBookingsForm : SweetForm
    {
        private System.Windows.Forms.Button btnExport;
        private ListView listViewTransactionsList;
	    private BackgroundWorker _bwExportWorker;
	    private BackgroundWorker _bwExportToFile;
        private BackgroundWorker _bwSelect;
        private ProgressBar progressBarExport;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnDeselectAll;
        private Label lbSlash;
        private Label labelSelected;
        private Label labelTotal;
	    private int _selected;
	    private int _total;
        private Encoding _encoding;
        private DataTable _dataTable;
        private DataTable _idTable;
        private TextBox tbSeparator;
        private Label lbSeparator;
        private CheckBox cbQuoteNonnumeric;
        private ComboBox cmbEncoding;
        private ComboBox cbProcNames;
        private System.Windows.Forms.Button btnPrepareExport;
        private Panel panel1;
        private Panel panel2;
        private FlowLayoutPanel flowLayoutPanel1;
        private Label lbProcedure;
        private Label lbOptions;
        private Panel panel3;
        private Label label3;
        private CheckedListBox clbxFields;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components;
        private int _index = -2;

		public ExportBookingsForm()
		{
		    _selected = _total = 0;
            InitializeComponent();
            cmbEncoding.SelectedIndex = 0;
		}

        private void Initialization(object sender, EventArgs e)
		{ 
		    List<string> names = ServicesProvider.GetInstance().GetAccountingServices().SelectExportAccountingProcNames();

            if (names != null && names.Count > 0)
                foreach (string name in names)
                    cbProcNames.Items.Add(name.Substring(17, name.Length - 17));
            
		    if (cbProcNames.Items.Count > 0)
                cbProcNames.SelectedIndex = 0;
		}

        private void InitializeListView()
        {

            listViewTransactionsList.Invoke(new MethodInvoker(
                delegate
                    {
                        listViewTransactionsList.Visible = false;
                        if (_dataTable != null)
                        {
                            listViewTransactionsList.Columns.Add("Sr #", 60);

                            foreach (DataColumn column in _dataTable.Columns)
                            {
                                listViewTransactionsList.Columns.Add(column.ColumnName, 120);
                                clbxFields.Items.Add(column.ColumnName, CheckState.Checked);
                            }

                            for (int i = 0; i <= _dataTable.Rows.Count-1; i++)
                            {
                                ListViewItem item = new ListViewItem((i + 1).ToString());
                                item.SubItems.AddRange(Array.ConvertAll(_dataTable.Rows[i].ItemArray, p => p.ToString()));
                                item.Tag = _dataTable.Rows[i];
                                listViewTransactionsList.Items.Add(item);
                            }
                        }
                    }
            ));
        }

        public void BwExportWorker_WorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            listViewTransactionsList.Invoke(new MethodInvoker(delegate { listViewTransactionsList.Visible = true; } ));
            progressBarExport.Invoke(new MethodInvoker(delegate
                                                           {
                                                               progressBarExport.Value = 0;
                                                           }
                                         ));

            Activate();
            if (e.Result != null)
            {
                if (e.Result.Equals("success"))
                {
                    btnExport.Invoke(new MethodInvoker(delegate { btnExport.Enabled = true; }));
                    bool hasAdditionalTable = _idTable != null;
                    btnSelectAll.Invoke(new MethodInvoker(delegate { btnSelectAll.Enabled = !hasAdditionalTable; }));
                    btnDeselectAll.Invoke(new MethodInvoker(delegate { btnDeselectAll.Enabled = !hasAdditionalTable; }));
                    if(hasAdditionalTable) SelectAll();
                }
            }
            else
            {
                MessageBox.Show(MultiLanguageStrings.GetString(Ressource.ExportBookingsForm, "NotFound.Text"));
            }
        }

	    /// <summary>
		/// Clean up any resources being used.
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
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportBookingsForm));
            this.listViewTransactionsList = new System.Windows.Forms.ListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbSlash = new System.Windows.Forms.Label();
            this.labelTotal = new System.Windows.Forms.Label();
            this.labelSelected = new System.Windows.Forms.Label();
            this.btnPrepareExport = new System.Windows.Forms.Button();
            this.cbProcNames = new System.Windows.Forms.ComboBox();
            this.lbSeparator = new System.Windows.Forms.Label();
            this.tbSeparator = new System.Windows.Forms.TextBox();
            this.cmbEncoding = new System.Windows.Forms.ComboBox();
            this.cbQuoteNonnumeric = new System.Windows.Forms.CheckBox();
            this.btnDeselectAll = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.progressBarExport = new System.Windows.Forms.ProgressBar();
            this.btnExport = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lbProcedure = new System.Windows.Forms.Label();
            this.lbOptions = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.clbxFields = new System.Windows.Forms.CheckedListBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewTransactionsList
            // 
            this.listViewTransactionsList.CheckBoxes = true;
            resources.ApplyResources(this.listViewTransactionsList, "listViewTransactionsList");
            this.listViewTransactionsList.FullRowSelect = true;
            this.listViewTransactionsList.GridLines = true;
            this.listViewTransactionsList.Name = "listViewTransactionsList";
            this.listViewTransactionsList.UseCompatibleStateImageBehavior = false;
            this.listViewTransactionsList.View = System.Windows.Forms.View.Details;
            this.listViewTransactionsList.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listViewTransactionsList_ItemChecked);
            this.listViewTransactionsList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listViewTransactionsList_ItemCheck);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.lbSlash);
            this.panel1.Controls.Add(this.labelTotal);
            this.panel1.Controls.Add(this.labelSelected);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // lbSlash
            // 
            resources.ApplyResources(this.lbSlash, "lbSlash");
            this.lbSlash.Name = "lbSlash";
            // 
            // labelTotal
            // 
            resources.ApplyResources(this.labelTotal, "labelTotal");
            this.labelTotal.BackColor = System.Drawing.Color.Transparent;
            this.labelTotal.Name = "labelTotal";
            // 
            // labelSelected
            // 
            resources.ApplyResources(this.labelSelected, "labelSelected");
            this.labelSelected.BackColor = System.Drawing.Color.Transparent;
            this.labelSelected.Name = "labelSelected";
            // 
            // btnPrepareExport
            // 
            resources.ApplyResources(this.btnPrepareExport, "btnPrepareExport");
            this.btnPrepareExport.Name = "btnPrepareExport";
            this.btnPrepareExport.Click += new System.EventHandler(this.buttonPrepareExport_Click);
            // 
            // cbProcNames
            // 
            this.cbProcNames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProcNames.FormattingEnabled = true;
            resources.ApplyResources(this.cbProcNames, "cbProcNames");
            this.cbProcNames.Name = "cbProcNames";
            this.cbProcNames.SelectedIndexChanged += new System.EventHandler(this.cbProcNames_SelectedIndexChanged);
            // 
            // lbSeparator
            // 
            resources.ApplyResources(this.lbSeparator, "lbSeparator");
            this.lbSeparator.Name = "lbSeparator";
            // 
            // tbSeparator
            // 
            resources.ApplyResources(this.tbSeparator, "tbSeparator");
            this.tbSeparator.Name = "tbSeparator";
            // 
            // cmbEncoding
            // 
            this.cmbEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEncoding.FormattingEnabled = true;
            this.cmbEncoding.Items.AddRange(new object[] {
            resources.GetString("cmbEncoding.Items"),
            resources.GetString("cmbEncoding.Items1"),
            resources.GetString("cmbEncoding.Items2")});
            resources.ApplyResources(this.cmbEncoding, "cmbEncoding");
            this.cmbEncoding.Name = "cmbEncoding";
            this.cmbEncoding.SelectedIndexChanged += new System.EventHandler(this.cmbEncoding_SelectedIndexChanged);
            // 
            // cbQuoteNonnumeric
            // 
            resources.ApplyResources(this.cbQuoteNonnumeric, "cbQuoteNonnumeric");
            this.cbQuoteNonnumeric.Name = "cbQuoteNonnumeric";
            // 
            // btnDeselectAll
            // 
            resources.ApplyResources(this.btnDeselectAll, "btnDeselectAll");
            this.btnDeselectAll.Name = "btnDeselectAll";
            this.btnDeselectAll.Click += new System.EventHandler(this.btnDeselectAll_Click);
            // 
            // btnSelectAll
            // 
            resources.ApplyResources(this.btnSelectAll, "btnSelectAll");
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // progressBarExport
            // 
            resources.ApplyResources(this.progressBarExport, "progressBarExport");
            this.progressBarExport.Name = "progressBarExport";
            // 
            // btnExport
            // 
            resources.ApplyResources(this.btnExport, "btnExport");
            this.btnExport.Name = "btnExport";
            this.btnExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.listViewTransactionsList);
            this.panel2.Controls.Add(this.flowLayoutPanel1);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.flowLayoutPanel1.Controls.Add(this.lbProcedure);
            this.flowLayoutPanel1.Controls.Add(this.cbProcNames);
            this.flowLayoutPanel1.Controls.Add(this.btnPrepareExport);
            this.flowLayoutPanel1.Controls.Add(this.lbOptions);
            this.flowLayoutPanel1.Controls.Add(this.cmbEncoding);
            this.flowLayoutPanel1.Controls.Add(this.panel3);
            this.flowLayoutPanel1.Controls.Add(this.cbQuoteNonnumeric);
            this.flowLayoutPanel1.Controls.Add(this.label3);
            this.flowLayoutPanel1.Controls.Add(this.panel1);
            this.flowLayoutPanel1.Controls.Add(this.btnSelectAll);
            this.flowLayoutPanel1.Controls.Add(this.btnDeselectAll);
            this.flowLayoutPanel1.Controls.Add(this.btnExport);
            this.flowLayoutPanel1.Controls.Add(this.progressBarExport);
            this.flowLayoutPanel1.Controls.Add(this.clbxFields);
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // lbProcedure
            // 
            resources.ApplyResources(this.lbProcedure, "lbProcedure");
            this.lbProcedure.Name = "lbProcedure";
            // 
            // lbOptions
            // 
            resources.ApplyResources(this.lbOptions, "lbOptions");
            this.lbOptions.Name = "lbOptions";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Controls.Add(this.tbSeparator);
            this.panel3.Controls.Add(this.lbSeparator);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // clbxFields
            // 
            this.clbxFields.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.clbxFields.CheckOnClick = true;
            resources.ApplyResources(this.clbxFields, "clbxFields");
            this.clbxFields.FormattingEnabled = true;
            this.clbxFields.Name = "clbxFields";
            this.clbxFields.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbxFields_ItemCheck);
            // 
            // ExportBookingsForm
            //
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.panel2);
            this.Name = "ExportBookingsForm";
            this.Load += new System.EventHandler(this.Initialization);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private void buttonExport_Click(object sender, EventArgs e)
		{
            if (listViewTransactionsList.Items.Count == 0)
            {
                Notify("PrepareTransactionBeforeExport.Text");
                return;
            }
            
            if (listViewTransactionsList.Items.Count > 0)
            {
                progressBarExport.Maximum = listViewTransactionsList.Items.Count;
                progressBarExport.Step = 1;
                progressBarExport.Minimum = 0;
                progressBarExport.Value = 0;
                string fileName = ExportFile.SaveTextToNewPath();
                
                if (string.IsNullOrEmpty(fileName))
                {
                    btnSelectAll.Enabled = true;
                    btnDeselectAll.Enabled = true;
                    return;
                }

                _bwExportToFile = new BackgroundWorker
                                      {
                                          WorkerReportsProgress = true,
                                          WorkerSupportsCancellation = true
                                      };
                _bwExportToFile.DoWork += BwExportToFile_DoWork;
                _bwExportToFile.RunWorkerCompleted += BwExportToFile_WorkCompleted;
                _bwExportToFile.RunWorkerAsync(fileName);
            }
		}

        private void BwExportToFile_WorkCompleted(object sender, RunWorkerCompletedEventArgs args)
        {
            progressBarExport.Invoke(new MethodInvoker(delegate
                                                           {
                                                               progressBarExport.Minimum = 0;
                                                               progressBarExport.Maximum = 100;
                                                               progressBarExport.Value = 5;
                                                           }
                                         ));

            if(args.Error!=null || args.Cancelled)
            {
                MessageBox.Show(MultiLanguageStrings.GetString(Ressource.ExportBookingsForm, "ExportCancelled.Text") + @"   " + args.Error);
                _dataTable.Clear();
                listViewTransactionsList.Clear();
                clbxFields.Items.Clear();
                return;
            } 

            MessageBox.Show(MultiLanguageStrings.GetString(Ressource.ExportBookingsForm, "ExportFinished.Text"));
            _dataTable.Clear();
            listViewTransactionsList.Clear();
            clbxFields.Items.Clear();
        }

        private void BwExportToFile_DoWork(object sender, DoWorkEventArgs args)
        {
            ListView.ListViewItemCollection items = null;
            listViewTransactionsList.Invoke(new MethodInvoker(delegate { items = listViewTransactionsList.Items; } ));

            string separator = tbSeparator.Text.Trim() == "" ? ";" : tbSeparator.Text.Trim();
            progressBarExport.Invoke(new MethodInvoker(delegate
                                                           {
                                                               progressBarExport.Minimum = 0;
                                                               progressBarExport.Value = 0;
                                                               progressBarExport.Step = 1;
                                                               progressBarExport.Maximum = 2;
                                                           }
                                         ));

            StreamWriter writer = new StreamWriter(args.Argument.ToString(), false, _encoding);

            int count = 0;
            string idSetLoan = "0";
            string idSetSaving = "0";
            string idSetManual = "0";
            const int parts = 50;
            ArrayList arrList = new ArrayList();
            
            for (int i = 0; i < listViewTransactionsList.Columns.Count; i++)
            {
                foreach (var checkedItem in clbxFields.CheckedItems)
                {
                    if (listViewTransactionsList.Columns[i].Text == checkedItem.ToString())
                    {
                        arrList.Add(i);
                    }
                }
            }

            for (int i = 0; i < items.Count; i++)
            {
                bool isChecked = false;
                progressBarExport.Invoke(new MethodInvoker(delegate { progressBarExport.PerformStep(); } ));
                listViewTransactionsList.Invoke(new MethodInvoker(delegate { isChecked = items[i].Checked; }));

                if (isChecked)
                {
                    DataRow curRow = null;
                    listViewTransactionsList.Invoke(new MethodInvoker(delegate { curRow = (DataRow)items[i].Tag; }));

                    string[] arr = new String[clbxFields.CheckedItems.Count];
                    int arrIdex = 0;
                    for (int j = 0; j < curRow.Table.Columns.Count; j++)
                    {
                        foreach (int index in arrList)
                        {
                            if (index - 1 == j)
                            {
                                arr[arrIdex] = cbQuoteNonnumeric.Checked
                                             ? curRow.ItemArray[j].GetType() == typeof (string)
                                                   ? "\"" + curRow.ItemArray[j] + "\""
                                                   : curRow.ItemArray[j].ToString()
                                             : curRow.ItemArray[j].ToString();
                                arrIdex++;
                            }
                        }
                    }

                    writer.WriteLine(string.Join(separator, arr));
                    
                    count++;
                    if (_idTable == null)
                    {
                        switch (curRow.ItemArray[1].ToString())
                        {
                            case "L": { idSetLoan += "," + curRow.ItemArray[0]; break; }
                            case "S": { idSetSaving += "," + curRow.ItemArray[0]; break; }
                            case "M": { idSetManual += "," + curRow.ItemArray[0]; break; }
                        }

                        if (count == parts)
                        {
                            ServicesProvider.GetInstance().GetAccountingServices().UpdateElementaryMvtExportedValue(idSetLoan, 0);
                            ServicesProvider.GetInstance().GetAccountingServices().UpdateElementaryMvtExportedValue(idSetSaving, 1);
                            ServicesProvider.GetInstance().GetAccountingServices().UpdateElementaryMvtExportedValue(idSetManual, 2);
                            count = 0;
                            idSetLoan = "0";
                            idSetSaving = "0";
                            idSetManual = "0";
                        }
                    }
                }
            }
            if (_idTable != null)
            {
                count = 0;
                foreach (DataRow idRow in _idTable.Rows)
                {
                    switch (idRow.ItemArray[1].ToString())
                    {
                        case "L": { idSetLoan += "," + idRow.ItemArray[0]; break; }
                        case "S": { idSetSaving += "," + idRow.ItemArray[0]; break; }
                        case "M": { idSetManual += "," + idRow.ItemArray[0]; break; }
                    }
                    count++;
                    if (count != parts) continue;

                    ServicesProvider.GetInstance().GetAccountingServices().UpdateElementaryMvtExportedValue(idSetLoan, 0);
                    ServicesProvider.GetInstance().GetAccountingServices().UpdateElementaryMvtExportedValue(idSetSaving, 1);
                    ServicesProvider.GetInstance().GetAccountingServices().UpdateElementaryMvtExportedValue(idSetManual, 2);
                    count = 0;
                    idSetLoan = "0";
                    idSetSaving = "0";
                    idSetManual = "0";
                }
            }

            ServicesProvider.GetInstance().GetAccountingServices().UpdateElementaryMvtExportedValue(idSetLoan, 0);
            ServicesProvider.GetInstance().GetAccountingServices().UpdateElementaryMvtExportedValue(idSetSaving, 1);
            ServicesProvider.GetInstance().GetAccountingServices().UpdateElementaryMvtExportedValue(idSetManual, 2);

            writer.Close();

            progressBarExport.Invoke(new MethodInvoker(delegate { progressBarExport.PerformStep(); } ));
            progressBarExport.Invoke(new MethodInvoker(delegate { progressBarExport.PerformStep(); } ));
        }

	    private void ExportBookings_Load(object sender, EventArgs e)
        {
            progressBarExport.Maximum = 100;
            progressBarExport.Value = 5;
            _bwExportWorker = new BackgroundWorker {WorkerReportsProgress = true, WorkerSupportsCancellation = true};
	        _bwExportWorker.DoWork += BwExportWorker_DoWork;
            _bwExportWorker.RunWorkerCompleted += BwExportWorker_WorkCompleted;
            _bwExportWorker.RunWorkerAsync();
		}

        private void BwExportWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Invoke(new MethodInvoker(delegate
                                         {
                                             labelTotal.Text = _total.ToString();
                                             labelSelected.Text = @"0";
                                             progressBarExport.Value = 15;
                                         }));
            InitializeListView();
            string isSuccess = "failure";
            listViewTransactionsList.Invoke(new MethodInvoker(delegate
                                                                  {
                                                                      isSuccess = listViewTransactionsList.Items.Count >
                                                                                  0
                                                                                      ? "success"
                                                                                      : "failure";
                                                                  }));
            e.Result = isSuccess;
            _bwExportWorker.ReportProgress(100, e.Result);
        }

	    private void btnSelectAll_Click(object sender, EventArgs e) {
	        SelectAll();
	    }

        private void SelectAll()
        {
            if (listViewTransactionsList.Items.Count == 0)
                return;
            progressBarExport.Minimum = 0;
            progressBarExport.Value = 0;
            progressBarExport.Step = 1;
            progressBarExport.Maximum = listViewTransactionsList.Items.Count;
            _bwSelect.RunWorkerAsync("1");
        }

        private void btnDeselectAll_Click(object sender, EventArgs e) {
            if (listViewTransactionsList.Items.Count == 0)
                return;
            progressBarExport.Minimum = 0;
            progressBarExport.Value = 0;
            progressBarExport.Step = 1;
            progressBarExport.Maximum = listViewTransactionsList.Items.Count;
            _bwSelect.RunWorkerAsync("0");
        }

        private void BwSelect_DoWork(object sender, DoWorkEventArgs e)
        {
            bool toSelect = e.Argument.Equals("1") ? true : false;
            int counter = 0;
            _selected = 0;

            listViewTransactionsList.Invoke(
                new MethodInvoker(delegate { counter = listViewTransactionsList.Items.Count; }));
            
            for (int i = 0; i < counter; i++)
            {
                listViewTransactionsList.Invoke(new MethodInvoker(delegate
                                                                      {
                                                                          listViewTransactionsList.Items[i].Checked =
                                                                              toSelect;
                                                                          _selected++;
                                                                      }));

                progressBarExport.Invoke(new MethodInvoker(delegate {progressBarExport.PerformStep();}));
            }

            listViewTransactionsList.Invalidate();
            progressBarExport.Invoke(new MethodInvoker(delegate
                                                           {
                                                               labelSelected.Text = toSelect
                                                                                        ? (_selected / 2).ToString()
                                                                                        : "0";
                                                           }
                                         ));
        }

	    private void listViewTransactionsList_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if(e.Item.Checked)
                _selected++;
            else
                _selected--;

            _selected = _selected < 0 ? 0 : _selected;
            labelSelected.Text = _selected.ToString();
        }

        private void buttonPrepareExport_Click(object sender, EventArgs e)
        {
            if (cbProcNames.Items.Count > 0)
            {
                List<ReportParamV2> paramList = new List<ReportParamV2>();

                Dictionary<string, string> parameters =
                    ServicesProvider.GetInstance().GetAccountingServices().SelectExportAccountingProcParams("ExportAccounting_" + cbProcNames.Text);

                if (parameters != null && parameters.Count > 0)
                {
                    foreach (var parameter in parameters)
                    {                        
                        ReportParamV2 reportParam;
                        string paramName = parameter.Key.TrimStart('@');
                        if (paramName.Equals("branch_id", StringComparison.CurrentCultureIgnoreCase))
                            reportParam = new BranchParam(string.Empty);
                        else
                        {
                            string paramType = parameter.Value;
                            switch (paramType)
                            {
                                case "bit":
                                    reportParam = new BoolParam(false);
                                    break;
                                case "datetime":
                                    reportParam = new DateParam(DateTime.Today);
                                    break;
                                case "char":
                                    reportParam = new CharParam(' ');
                                    break;
                                case "nvarchar":
                                case "varchar":
                                case "text":
                                    reportParam = new StringParam(string.Empty);
                                    break;
                                case "int":
                                    reportParam = new IntParam(1);
                                    break;
                                case "float":
                                    reportParam = new DoubleParam(1d);
                                    break;
                                case "money":
                                    reportParam = new DecimalParam(1m);
                                    break;
                                default:
                                    throw new NotImplementedException(string.Format("Sql type:{0} is not handled.", paramName));
                            }
                        }

                        reportParam.Label = paramName;
                        reportParam.Name = paramName;
                        paramList.Add(reportParam);
                    }
                    ReportParamsForm frm = new ReportParamsForm(paramList, cbProcNames.Text);
                    frm.ShowDialog();
                }

                _dataTable =
                    ServicesProvider.GetInstance().GetAccountingServices().FindElementaryMvtsToExport(
                        "ExportAccounting_"
                        + cbProcNames.Text, paramList, out _idTable);

                _total = _dataTable.Rows.Count;

                _bwSelect = new BackgroundWorker {WorkerSupportsCancellation = true};
                _bwSelect.DoWork += BwSelect_DoWork;
                
                ExportBookings_Load(this, null);
            }
        }

        private void cmbEncoding_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbEncoding.SelectedIndex)
            {
                case 0: { _encoding = System.Text.Encoding.Unicode; break;}     
                case 1: { _encoding = System.Text.Encoding.GetEncoding(1250); break;}     
                case 2: { _encoding = System.Text.Encoding.GetEncoding(1251); break;}     
            }
        }

        private void cbProcNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnPrepareExport.Enabled = true;
        }

        private void clbxFields_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            foreach (ColumnHeader column in listViewTransactionsList.Columns)
            {
                if (column.Index == e.Index + 1)
                {
                    switch (e.NewValue)
                    {
                        case CheckState.Unchecked:
                            column.Width = 0;
                            break;
                        case CheckState.Checked:
                            column.Width = 120;
                            break;
                    }
                }
            }
        }

        private void listViewTransactionsList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (_idTable != null) e.NewValue = CheckState.Checked;
        }
    }
}
