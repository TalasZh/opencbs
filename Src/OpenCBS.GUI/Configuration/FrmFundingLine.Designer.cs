using System.Windows.Forms;
using OpenCBS.GUI.UserControl;

namespace OpenCBS.GUI
{
    partial class FrmFundingLine
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
       private System.ComponentModel.IContainer components = null;
       private System.ComponentModel.ComponentResourceManager resources;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFundingLine));
            System.Windows.Forms.ColumnHeader columnHeaderName;
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBoxEvents = new System.Windows.Forms.GroupBox();
            this.listViewFundingLineEvent = new OpenCBS.GUI.UserControl.ListViewEx();
            this.columnHeaderCode = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderEventCreationDate = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderDirection = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderEventAmount = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderEventType = new System.Windows.Forms.ColumnHeader();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonDeleteFundingLineEvent = new System.Windows.Forms.Button();
            this.buttonAddFundingLineEvent = new System.Windows.Forms.Button();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.comboBoxCurrencies = new System.Windows.Forms.ComboBox();
            this.lblCurrency = new System.Windows.Forms.Label();
            this.labelCode = new System.Windows.Forms.Label();
            this.dateTimePickerFundingLineEndDate = new System.Windows.Forms.DateTimePicker();
            this.labelAnticipatedRemainingAmount = new System.Windows.Forms.Label();
            this.labelRealRemainingAmount = new System.Windows.Forms.Label();
            this.labelInitialAmount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePickerFundingLineBeginDate = new System.Windows.Forms.DateTimePicker();
            this.tbAnticipatedAmt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbRealAmt = new System.Windows.Forms.TextBox();
            this.textBoxFundingLineName = new System.Windows.Forms.TextBox();
            this.tbInitialAmt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxFundingLineCode = new System.Windows.Forms.TextBox();
            this.splitContainerGraph = new System.Windows.Forms.SplitContainer();
            this.zedGraphControlCashPrevision = new ZedGraph.ZedGraphControl();
            this.checkBoxIncludeLateLoans = new System.Windows.Forms.CheckBox();
            this.tabControlFundingLines = new System.Windows.Forms.TabControl();
            this.tabPageFundingLines = new System.Windows.Forms.TabPage();
            this.listViewFundingLine = new System.Windows.Forms.ListView();
            this.columnHeaderBeginDate = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderEndDate = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderAmount = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderResiduelAmount = new System.Windows.Forms.ColumnHeader();
            this.columnFinancial = new System.Windows.Forms.ColumnHeader();
            this.colCurrency = new System.Windows.Forms.ColumnHeader();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.tabPageFundingLineDetails = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxFundingLines = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.columnHeaderBodyCorporateSecondaryHomePhone = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderCorporateName = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderCorporateAmount = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderCorporateResidualAMount = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderFinancial = new System.Windows.Forms.ColumnHeader();
            columnHeaderName = new System.Windows.Forms.ColumnHeader();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBoxEvents.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.splitContainerGraph.Panel1.SuspendLayout();
            this.splitContainerGraph.Panel2.SuspendLayout();
            this.splitContainerGraph.SuspendLayout();
            this.tabControlFundingLines.SuspendLayout();
            this.tabPageFundingLines.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tabPageFundingLineDetails.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBoxFundingLines.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer2
            // 
            resources.ApplyResources(this.splitContainer2, "splitContainer2");
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBoxEvents);
            resources.ApplyResources(this.splitContainer2.Panel1, "splitContainer2.Panel1");
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox3);
            resources.ApplyResources(this.splitContainer2.Panel2, "splitContainer2.Panel2");
            // 
            // groupBoxEvents
            // 
            this.groupBoxEvents.Controls.Add(this.listViewFundingLineEvent);
            resources.ApplyResources(this.groupBoxEvents, "groupBoxEvents");
            this.groupBoxEvents.Name = "groupBoxEvents";
            this.groupBoxEvents.TabStop = false;
            // 
            // listViewFundingLineEvent
            // 
            this.listViewFundingLineEvent.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderCode,
            this.columnHeaderEventCreationDate,
            this.columnHeaderDirection,
            this.columnHeaderEventAmount,
            this.columnHeaderEventType});
            resources.ApplyResources(this.listViewFundingLineEvent, "listViewFundingLineEvent");
            this.listViewFundingLineEvent.DoubleClickActivation = false;
            this.listViewFundingLineEvent.FullRowSelect = true;
            this.listViewFundingLineEvent.GridLines = true;
            this.listViewFundingLineEvent.Name = "listViewFundingLineEvent";
            this.listViewFundingLineEvent.UseCompatibleStateImageBehavior = false;
            this.listViewFundingLineEvent.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderCode
            // 
            resources.ApplyResources(this.columnHeaderCode, "columnHeaderCode");
            // 
            // columnHeaderEventCreationDate
            // 
            resources.ApplyResources(this.columnHeaderEventCreationDate, "columnHeaderEventCreationDate");
            // 
            // columnHeaderDirection
            // 
            resources.ApplyResources(this.columnHeaderDirection, "columnHeaderDirection");
            // 
            // columnHeaderEventAmount
            // 
            resources.ApplyResources(this.columnHeaderEventAmount, "columnHeaderEventAmount");
            // 
            // columnHeaderEventType
            // 
            resources.ApplyResources(this.columnHeaderEventType, "columnHeaderEventType");
            // 
            // groupBox3
            //
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.buttonDeleteFundingLineEvent);
            this.groupBox3.Controls.Add(this.buttonAddFundingLineEvent);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // buttonDeleteFundingLineEvent
            // 
            resources.ApplyResources(this.buttonDeleteFundingLineEvent, "buttonDeleteFundingLineEvent");
            this.buttonDeleteFundingLineEvent.Name = "buttonDeleteFundingLineEvent";
            this.buttonDeleteFundingLineEvent.Click += new System.EventHandler(this.buttonDeleteFundingLineEvent_Click);
            // 
            // buttonAddFundingLineEvent
            // 
            resources.ApplyResources(this.buttonAddFundingLineEvent, "buttonAddFundingLineEvent");
            this.buttonAddFundingLineEvent.Name = "buttonAddFundingLineEvent";
            this.buttonAddFundingLineEvent.Click += new System.EventHandler(this.buttonAddFundingLineEvent_Click);
            // 
            // columnHeaderName
            // 
            resources.ApplyResources(columnHeaderName, "columnHeaderName");
            // 
            // splitContainer3
            // 
            resources.ApplyResources(this.splitContainer3, "splitContainer3");
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.comboBoxCurrencies);
            this.splitContainer3.Panel1.Controls.Add(this.lblCurrency);
            this.splitContainer3.Panel1.Controls.Add(this.labelCode);
            this.splitContainer3.Panel1.Controls.Add(this.dateTimePickerFundingLineEndDate);
            this.splitContainer3.Panel1.Controls.Add(this.labelAnticipatedRemainingAmount);
            this.splitContainer3.Panel1.Controls.Add(this.labelRealRemainingAmount);
            this.splitContainer3.Panel1.Controls.Add(this.labelInitialAmount);
            this.splitContainer3.Panel1.Controls.Add(this.label1);
            this.splitContainer3.Panel1.Controls.Add(this.dateTimePickerFundingLineBeginDate);
            this.splitContainer3.Panel1.Controls.Add(this.tbAnticipatedAmt);
            this.splitContainer3.Panel1.Controls.Add(this.label2);
            this.splitContainer3.Panel1.Controls.Add(this.tbRealAmt);
            this.splitContainer3.Panel1.Controls.Add(this.textBoxFundingLineName);
            this.splitContainer3.Panel1.Controls.Add(this.tbInitialAmt);
            this.splitContainer3.Panel1.Controls.Add(this.label3);
            this.splitContainer3.Panel1.Controls.Add(this.textBoxFundingLineCode);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainerGraph);
            // 
            // comboBoxCurrencies
            // 
            this.comboBoxCurrencies.DisplayMember = "Currency.Name";
            this.comboBoxCurrencies.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCurrencies.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxCurrencies, "comboBoxCurrencies");
            this.comboBoxCurrencies.Name = "comboBoxCurrencies";
            this.comboBoxCurrencies.SelectedIndexChanged += new System.EventHandler(this.comboBoxCurrencies_SelectedIndexChanged);
            // 
            // lblCurrency
            // 
            resources.ApplyResources(this.lblCurrency, "lblCurrency");
            this.lblCurrency.Name = "lblCurrency";
            // 
            // labelCode
            // 
            resources.ApplyResources(this.labelCode, "labelCode");
            this.labelCode.Name = "labelCode";
            // 
            // dateTimePickerFundingLineEndDate
            // 
            this.dateTimePickerFundingLineEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            resources.ApplyResources(this.dateTimePickerFundingLineEndDate, "dateTimePickerFundingLineEndDate");
            this.dateTimePickerFundingLineEndDate.Name = "dateTimePickerFundingLineEndDate";
            // 
            // labelAnticipatedRemainingAmount
            // 
            resources.ApplyResources(this.labelAnticipatedRemainingAmount, "labelAnticipatedRemainingAmount");
            this.labelAnticipatedRemainingAmount.Name = "labelAnticipatedRemainingAmount";
            // 
            // labelRealRemainingAmount
            // 
            resources.ApplyResources(this.labelRealRemainingAmount, "labelRealRemainingAmount");
            this.labelRealRemainingAmount.Name = "labelRealRemainingAmount";
            // 
            // labelInitialAmount
            // 
            resources.ApplyResources(this.labelInitialAmount, "labelInitialAmount");
            this.labelInitialAmount.Name = "labelInitialAmount";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // dateTimePickerFundingLineBeginDate
            // 
            this.dateTimePickerFundingLineBeginDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            resources.ApplyResources(this.dateTimePickerFundingLineBeginDate, "dateTimePickerFundingLineBeginDate");
            this.dateTimePickerFundingLineBeginDate.Name = "dateTimePickerFundingLineBeginDate";
            // 
            // tbAnticipatedAmt
            // 
            resources.ApplyResources(this.tbAnticipatedAmt, "tbAnticipatedAmt");
            this.tbAnticipatedAmt.Name = "tbAnticipatedAmt";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // tbRealAmt
            // 
            resources.ApplyResources(this.tbRealAmt, "tbRealAmt");
            this.tbRealAmt.Name = "tbRealAmt";
            // 
            // textBoxFundingLineName
            // 
            resources.ApplyResources(this.textBoxFundingLineName, "textBoxFundingLineName");
            this.textBoxFundingLineName.Name = "textBoxFundingLineName";
            // 
            // tbInitialAmt
            // 
            resources.ApplyResources(this.tbInitialAmt, "tbInitialAmt");
            this.tbInitialAmt.Name = "tbInitialAmt";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // textBoxFundingLineCode
            // 
            resources.ApplyResources(this.textBoxFundingLineCode, "textBoxFundingLineCode");
            this.textBoxFundingLineCode.Name = "textBoxFundingLineCode";
            // 
            // splitContainerGraph
            // 
            resources.ApplyResources(this.splitContainerGraph, "splitContainerGraph");
            this.splitContainerGraph.Name = "splitContainerGraph";
            // 
            // splitContainerGraph.Panel1
            // 
            this.splitContainerGraph.Panel1.Controls.Add(this.zedGraphControlCashPrevision);
            // 
            // splitContainerGraph.Panel2
            // 
            this.splitContainerGraph.Panel2.Controls.Add(this.checkBoxIncludeLateLoans);
            // 
            // zedGraphControlCashPrevision
            // 
            resources.ApplyResources(this.zedGraphControlCashPrevision, "zedGraphControlCashPrevision");
            this.zedGraphControlCashPrevision.BackColor = System.Drawing.Color.White;
            this.zedGraphControlCashPrevision.IsAutoScrollRange = false;
            this.zedGraphControlCashPrevision.IsEnableHPan = false;
            this.zedGraphControlCashPrevision.IsEnableHZoom = false;
            this.zedGraphControlCashPrevision.IsEnableVPan = false;
            this.zedGraphControlCashPrevision.IsEnableVZoom = false;
            this.zedGraphControlCashPrevision.IsScrollY2 = false;
            this.zedGraphControlCashPrevision.IsShowContextMenu = false;
            this.zedGraphControlCashPrevision.IsShowCursorValues = false;
            this.zedGraphControlCashPrevision.IsShowHScrollBar = false;
            this.zedGraphControlCashPrevision.IsShowPointValues = false;
            this.zedGraphControlCashPrevision.IsShowVScrollBar = false;
            this.zedGraphControlCashPrevision.IsZoomOnMouseCenter = false;
            this.zedGraphControlCashPrevision.Name = "zedGraphControlCashPrevision";
            this.zedGraphControlCashPrevision.PanButtons = System.Windows.Forms.MouseButtons.Left;
            this.zedGraphControlCashPrevision.PanButtons2 = System.Windows.Forms.MouseButtons.Middle;
            this.zedGraphControlCashPrevision.PanModifierKeys2 = System.Windows.Forms.Keys.None;
            this.zedGraphControlCashPrevision.PointDateFormat = "g";
            this.zedGraphControlCashPrevision.PointValueFormat = "G";
            this.zedGraphControlCashPrevision.ScrollMaxX = 0;
            this.zedGraphControlCashPrevision.ScrollMaxY = 0;
            this.zedGraphControlCashPrevision.ScrollMaxY2 = 0;
            this.zedGraphControlCashPrevision.ScrollMinX = 0;
            this.zedGraphControlCashPrevision.ScrollMinY = 0;
            this.zedGraphControlCashPrevision.ScrollMinY2 = 0;
            this.zedGraphControlCashPrevision.ZoomButtons = System.Windows.Forms.MouseButtons.Left;
            this.zedGraphControlCashPrevision.ZoomButtons2 = System.Windows.Forms.MouseButtons.None;
            this.zedGraphControlCashPrevision.ZoomModifierKeys = System.Windows.Forms.Keys.None;
            this.zedGraphControlCashPrevision.ZoomModifierKeys2 = System.Windows.Forms.Keys.None;
            this.zedGraphControlCashPrevision.ZoomStepFraction = 0.1;
            this.zedGraphControlCashPrevision.Click += new System.EventHandler(this.zedGraphControlCashPrevision_Click);
            // 
            // checkBoxIncludeLateLoans
            // 
            resources.ApplyResources(this.checkBoxIncludeLateLoans, "checkBoxIncludeLateLoans");
            this.checkBoxIncludeLateLoans.Checked = true;
            this.checkBoxIncludeLateLoans.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxIncludeLateLoans.Name = "checkBoxIncludeLateLoans";
            this.checkBoxIncludeLateLoans.CheckedChanged += new System.EventHandler(this.checkBoxIncludeLateLoans_CheckedChanged);
            // 
            // tabControlFundingLines
            // 
            this.tabControlFundingLines.Controls.Add(this.tabPageFundingLines);
            this.tabControlFundingLines.Controls.Add(this.tabPageFundingLineDetails);
            resources.ApplyResources(this.tabControlFundingLines, "tabControlFundingLines");
            this.tabControlFundingLines.Name = "tabControlFundingLines";
            this.tabControlFundingLines.SelectedIndex = 0;
            // 
            // tabPageFundingLines
            // 
            this.tabPageFundingLines.Controls.Add(this.listViewFundingLine);
            this.tabPageFundingLines.Controls.Add(this.flowLayoutPanel1);
            resources.ApplyResources(this.tabPageFundingLines, "tabPageFundingLines");
            this.tabPageFundingLines.Name = "tabPageFundingLines";
            // 
            // listViewFundingLine
            // 
            this.listViewFundingLine.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeaderName,
            this.columnHeaderBeginDate,
            this.columnHeaderEndDate,
            this.columnHeaderAmount,
            this.columnHeaderResiduelAmount,
            this.columnFinancial,
            this.colCurrency});
            resources.ApplyResources(this.listViewFundingLine, "listViewFundingLine");
            this.listViewFundingLine.FullRowSelect = true;
            this.listViewFundingLine.GridLines = true;
            this.listViewFundingLine.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewFundingLine.MultiSelect = false;
            this.listViewFundingLine.Name = "listViewFundingLine";
            this.listViewFundingLine.UseCompatibleStateImageBehavior = false;
            this.listViewFundingLine.View = System.Windows.Forms.View.Details;
            this.listViewFundingLine.DoubleClick += new System.EventHandler(this.listViewFundingLine_DoubleClick);
            // 
            // columnHeaderBeginDate
            // 
            resources.ApplyResources(this.columnHeaderBeginDate, "columnHeaderBeginDate");
            // 
            // columnHeaderEndDate
            // 
            resources.ApplyResources(this.columnHeaderEndDate, "columnHeaderEndDate");
            // 
            // columnHeaderAmount
            // 
            resources.ApplyResources(this.columnHeaderAmount, "columnHeaderAmount");
            // 
            // columnHeaderResiduelAmount
            // 
            resources.ApplyResources(this.columnHeaderResiduelAmount, "columnHeaderResiduelAmount");
            // 
            // columnFinancial
            // 
            resources.ApplyResources(this.columnFinancial, "columnFinancial");
            // 
            // colCurrency
            // 
            resources.ApplyResources(this.colCurrency, "colCurrency");
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Controls.Add(this.btnAdd);
            this.flowLayoutPanel1.Controls.Add(this.btnDelete);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // btnAdd
            // 
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // btnDelete
            // 
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // tabPageFundingLineDetails
            // 
            this.tabPageFundingLineDetails.Controls.Add(this.tableLayoutPanel1);
            resources.ApplyResources(this.tabPageFundingLineDetails, "tabPageFundingLineDetails");
            this.tabPageFundingLineDetails.Name = "tabPageFundingLineDetails";
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.groupBoxFundingLines, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.groupBox4, 0, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // groupBoxFundingLines
            //
            resources.ApplyResources(this.groupBoxFundingLines, "groupBoxFundingLines");
            this.groupBoxFundingLines.Controls.Add(this.splitContainer3);
            this.groupBoxFundingLines.Name = "groupBoxFundingLines";
            this.groupBoxFundingLines.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnSave);
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // columnHeaderCorporateName
            // 
            resources.ApplyResources(this.columnHeaderCorporateName, "columnHeaderCorporateName");
            // 
            // columnHeaderCorporateAmount
            // 
            resources.ApplyResources(this.columnHeaderCorporateAmount, "columnHeaderCorporateAmount");
            // 
            // columnHeaderCorporateResidualAMount
            // 
            resources.ApplyResources(this.columnHeaderCorporateResidualAMount, "columnHeaderCorporateResidualAMount");
            // 
            // columnHeaderFinancial
            // 
            resources.ApplyResources(this.columnHeaderFinancial, "columnHeaderFinancial");
            // 
            // FrmFundingLine
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.tabControlFundingLines);
            this.Name = "FrmFundingLine";
            this.Controls.SetChildIndex(this.tabControlFundingLines, 0);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.groupBoxEvents.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.splitContainerGraph.Panel1.ResumeLayout(false);
            this.splitContainerGraph.Panel2.ResumeLayout(false);
            this.splitContainerGraph.Panel2.PerformLayout();
            this.splitContainerGraph.ResumeLayout(false);
            this.tabControlFundingLines.ResumeLayout(false);
            this.tabPageFundingLines.ResumeLayout(false);
            this.tabPageFundingLines.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tabPageFundingLineDetails.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBoxFundingLines.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

    

        #endregion

        private System.Windows.Forms.ColumnHeader columnHeaderBodyCorporateSecondaryHomePhone;
        private System.Windows.Forms.TabControl tabControlFundingLines;
        private System.Windows.Forms.TabPage tabPageFundingLines;
        private System.Windows.Forms.ListView listViewFundingLine;
        private System.Windows.Forms.ColumnHeader columnHeaderBeginDate;
        private System.Windows.Forms.ColumnHeader columnHeaderEndDate;
        private System.Windows.Forms.ColumnHeader columnHeaderAmount;
        private System.Windows.Forms.ColumnHeader columnHeaderResiduelAmount;
        private System.Windows.Forms.ColumnHeader columnFinancial;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TabPage tabPageFundingLineDetails;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBoxFundingLines;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Label labelCode;
        private System.Windows.Forms.DateTimePicker dateTimePickerFundingLineEndDate;
        private System.Windows.Forms.Label labelAnticipatedRemainingAmount;
        private System.Windows.Forms.Label labelRealRemainingAmount;
        private System.Windows.Forms.Label labelInitialAmount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePickerFundingLineBeginDate;
        private System.Windows.Forms.TextBox tbAnticipatedAmt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbRealAmt;
        private System.Windows.Forms.TextBox textBoxFundingLineName;
        private System.Windows.Forms.TextBox tbInitialAmt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxFundingLineCode;
        private ZedGraph.ZedGraphControl zedGraphControlCashPrevision;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBoxEvents;
        private ListViewEx listViewFundingLineEvent;
        private System.Windows.Forms.ColumnHeader columnHeaderCode;
        private System.Windows.Forms.ColumnHeader columnHeaderEventCreationDate;
        private System.Windows.Forms.ColumnHeader columnHeaderDirection;
        private System.Windows.Forms.ColumnHeader columnHeaderEventAmount;
        private System.Windows.Forms.ColumnHeader columnHeaderEventType;
        private System.Windows.Forms.ColumnHeader columnHeaderCorporateName;
        private System.Windows.Forms.ColumnHeader columnHeaderCorporateAmount;
        private System.Windows.Forms.ColumnHeader columnHeaderCorporateResidualAMount;
        private System.Windows.Forms.ColumnHeader columnHeaderFinancial;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button buttonDeleteFundingLineEvent;
        private System.Windows.Forms.Button buttonAddFundingLineEvent;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnSave;
        private CheckBox checkBoxIncludeLateLoans;
        private SplitContainer splitContainerGraph;
        private Label lblCurrency;
        private ComboBox comboBoxCurrencies;
        private ColumnHeader colCurrency;
        private FlowLayoutPanel flowLayoutPanel1;
    }
}