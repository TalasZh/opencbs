using Octopus.GUI.UserControl;

namespace Octopus.GUI.Accounting
{
    partial class AccountingJournals
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountingJournals));
            this.pnlRight = new System.Windows.Forms.Panel();
            this.btnView = new Octopus.GUI.UserControl.SweetButton();
            this.btnPost = new Octopus.GUI.UserControl.SweetButton();
            this.clbxFields = new System.Windows.Forms.CheckedListBox();
            this.cmbBranches = new System.Windows.Forms.ComboBox();
            this.lblBranch = new System.Windows.Forms.Label();
            this.dateTimePickerBeginDate = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblBeginDate = new System.Windows.Forms.Label();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.btnPreview = new Octopus.GUI.UserControl.SweetButton();
            this.bwRun = new System.ComponentModel.BackgroundWorker();
            this.tbcMain = new System.Windows.Forms.TabControl();
            this.tbpMovements = new System.Windows.Forms.TabPage();
            this.olvBookings = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn_EventId = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn_CreditAccount = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn_DebitAccount = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn_EventType = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn_Amount = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn_TransactionDate = new BrightIdeasSoftware.OLVColumn();
            this.cbxAllBookings = new System.Windows.Forms.CheckBox();
            this.tbpEvents = new System.Windows.Forms.TabPage();
            this.olvEvents = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn_Code = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn_Description = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn_Amnt = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn_Fee = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn_OLB = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn_OverdueDays = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn_OverduePrincipal = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn_Date = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn_Interest = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn_AccruedInterest = new BrightIdeasSoftware.OLVColumn();
            this.cbAllEvents = new System.Windows.Forms.CheckBox();
            this.timerClosure = new System.Windows.Forms.Timer(this.components);
            this.bwPostEvents = new System.ComponentModel.BackgroundWorker();
            this.bwPostBookings = new System.ComponentModel.BackgroundWorker();
            this.pnlRight.SuspendLayout();
            this.tbcMain.SuspendLayout();
            this.tbpMovements.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvBookings)).BeginInit();
            this.tbpEvents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvEvents)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlRight
            // 
            this.pnlRight.AccessibleDescription = null;
            this.pnlRight.AccessibleName = null;
            resources.ApplyResources(this.pnlRight, "pnlRight");
            this.pnlRight.BackgroundImage = null;
            this.pnlRight.Controls.Add(this.btnView);
            this.pnlRight.Controls.Add(this.btnPost);
            this.pnlRight.Controls.Add(this.clbxFields);
            this.pnlRight.Controls.Add(this.cmbBranches);
            this.pnlRight.Controls.Add(this.lblBranch);
            this.pnlRight.Controls.Add(this.dateTimePickerBeginDate);
            this.pnlRight.Controls.Add(this.dateTimePickerEndDate);
            this.pnlRight.Controls.Add(this.lblBeginDate);
            this.pnlRight.Controls.Add(this.lblEndDate);
            this.pnlRight.Controls.Add(this.btnPreview);
            this.pnlRight.Font = null;
            this.pnlRight.Name = "pnlRight";
            // 
            // btnView
            // 
            this.btnView.AccessibleDescription = null;
            this.btnView.AccessibleName = null;
            resources.ApplyResources(this.btnView, "btnView");
            this.btnView.BackgroundImage = null;
            this.btnView.Font = null;
            this.btnView.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.View;
            this.btnView.Menu = null;
            this.btnView.Name = "btnView";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.BtnViewClick);
            // 
            // btnPost
            // 
            this.btnPost.AccessibleDescription = null;
            this.btnPost.AccessibleName = null;
            resources.ApplyResources(this.btnPost, "btnPost");
            this.btnPost.BackgroundImage = null;
            this.btnPost.Font = null;
            this.btnPost.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Refresh;
            this.btnPost.Menu = null;
            this.btnPost.Name = "btnPost";
            this.btnPost.UseVisualStyleBackColor = true;
            this.btnPost.Click += new System.EventHandler(this.BtnPostClick);
            // 
            // clbxFields
            // 
            this.clbxFields.AccessibleDescription = null;
            this.clbxFields.AccessibleName = null;
            resources.ApplyResources(this.clbxFields, "clbxFields");
            this.clbxFields.BackgroundImage = null;
            this.clbxFields.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.clbxFields.CheckOnClick = true;
            this.clbxFields.Font = null;
            this.clbxFields.FormattingEnabled = true;
            this.clbxFields.Name = "clbxFields";
            // 
            // cmbBranches
            // 
            this.cmbBranches.AccessibleDescription = null;
            this.cmbBranches.AccessibleName = null;
            resources.ApplyResources(this.cmbBranches, "cmbBranches");
            this.cmbBranches.BackgroundImage = null;
            this.cmbBranches.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBranches.Font = null;
            this.cmbBranches.FormattingEnabled = true;
            this.cmbBranches.Name = "cmbBranches";
            // 
            // lblBranch
            // 
            this.lblBranch.AccessibleDescription = null;
            this.lblBranch.AccessibleName = null;
            resources.ApplyResources(this.lblBranch, "lblBranch");
            this.lblBranch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.lblBranch.Name = "lblBranch";
            // 
            // dateTimePickerBeginDate
            // 
            this.dateTimePickerBeginDate.AccessibleDescription = null;
            this.dateTimePickerBeginDate.AccessibleName = null;
            resources.ApplyResources(this.dateTimePickerBeginDate, "dateTimePickerBeginDate");
            this.dateTimePickerBeginDate.BackgroundImage = null;
            this.dateTimePickerBeginDate.CalendarFont = null;
            this.dateTimePickerBeginDate.CustomFormat = null;
            this.dateTimePickerBeginDate.Font = null;
            this.dateTimePickerBeginDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerBeginDate.Name = "dateTimePickerBeginDate";
            // 
            // dateTimePickerEndDate
            // 
            this.dateTimePickerEndDate.AccessibleDescription = null;
            this.dateTimePickerEndDate.AccessibleName = null;
            resources.ApplyResources(this.dateTimePickerEndDate, "dateTimePickerEndDate");
            this.dateTimePickerEndDate.BackgroundImage = null;
            this.dateTimePickerEndDate.CalendarFont = null;
            this.dateTimePickerEndDate.CustomFormat = null;
            this.dateTimePickerEndDate.Font = null;
            this.dateTimePickerEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerEndDate.Name = "dateTimePickerEndDate";
            // 
            // lblBeginDate
            // 
            this.lblBeginDate.AccessibleDescription = null;
            this.lblBeginDate.AccessibleName = null;
            resources.ApplyResources(this.lblBeginDate, "lblBeginDate");
            this.lblBeginDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.lblBeginDate.Name = "lblBeginDate";
            // 
            // lblEndDate
            // 
            this.lblEndDate.AccessibleDescription = null;
            this.lblEndDate.AccessibleName = null;
            resources.ApplyResources(this.lblEndDate, "lblEndDate");
            this.lblEndDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.lblEndDate.Name = "lblEndDate";
            // 
            // btnPreview
            // 
            this.btnPreview.AccessibleDescription = null;
            this.btnPreview.AccessibleName = null;
            resources.ApplyResources(this.btnPreview, "btnPreview");
            this.btnPreview.BackgroundImage = null;
            this.btnPreview.Font = null;
            this.btnPreview.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Refresh;
            this.btnPreview.Menu = null;
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.BtnRunClick);
            // 
            // bwRun
            // 
            this.bwRun.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BwRunDoWork);
            this.bwRun.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BwRunRunWorkerCompleted);
            // 
            // tbcMain
            // 
            this.tbcMain.AccessibleDescription = null;
            this.tbcMain.AccessibleName = null;
            resources.ApplyResources(this.tbcMain, "tbcMain");
            this.tbcMain.BackgroundImage = null;
            this.tbcMain.Controls.Add(this.tbpMovements);
            this.tbcMain.Controls.Add(this.tbpEvents);
            this.tbcMain.Font = null;
            this.tbcMain.Name = "tbcMain";
            this.tbcMain.SelectedIndex = 0;
            // 
            // tbpMovements
            // 
            this.tbpMovements.AccessibleDescription = null;
            this.tbpMovements.AccessibleName = null;
            resources.ApplyResources(this.tbpMovements, "tbpMovements");
            this.tbpMovements.BackgroundImage = null;
            this.tbpMovements.Controls.Add(this.olvBookings);
            this.tbpMovements.Controls.Add(this.cbxAllBookings);
            this.tbpMovements.Font = null;
            this.tbpMovements.Name = "tbpMovements";
            this.tbpMovements.UseVisualStyleBackColor = true;
            // 
            // olvBookings
            // 
            this.olvBookings.AccessibleDescription = null;
            this.olvBookings.AccessibleName = null;
            resources.ApplyResources(this.olvBookings, "olvBookings");
            this.olvBookings.AllColumns.Add(this.olvColumn_EventId);
            this.olvBookings.AllColumns.Add(this.olvColumn_CreditAccount);
            this.olvBookings.AllColumns.Add(this.olvColumn_DebitAccount);
            this.olvBookings.AllColumns.Add(this.olvColumn_EventType);
            this.olvBookings.AllColumns.Add(this.olvColumn_Amount);
            this.olvBookings.AllColumns.Add(this.olvColumn_TransactionDate);
            this.olvBookings.BackgroundImage = null;
            this.olvBookings.CheckBoxes = true;
            this.olvBookings.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn_EventId,
            this.olvColumn_CreditAccount,
            this.olvColumn_DebitAccount,
            this.olvColumn_EventType,
            this.olvColumn_Amount,
            this.olvColumn_TransactionDate});
            this.olvBookings.EmptyListMsg = null;
            this.olvBookings.Font = null;
            this.olvBookings.FullRowSelect = true;
            this.olvBookings.GridLines = true;
            this.olvBookings.GroupWithItemCountFormat = null;
            this.olvBookings.GroupWithItemCountSingularFormat = null;
            this.olvBookings.HasCollapsibleGroups = false;
            this.olvBookings.Name = "olvBookings";
            this.olvBookings.OverlayText.Text = null;
            this.olvBookings.UseCompatibleStateImageBehavior = false;
            this.olvBookings.View = System.Windows.Forms.View.Details;
            // 
            // olvColumn_EventId
            // 
            this.olvColumn_EventId.AspectName = "EventId";
            this.olvColumn_EventId.CheckBoxes = true;
            this.olvColumn_EventId.GroupWithItemCountFormat = null;
            this.olvColumn_EventId.GroupWithItemCountSingularFormat = null;
            resources.ApplyResources(this.olvColumn_EventId, "olvColumn_EventId");
            this.olvColumn_EventId.ToolTipText = null;
            // 
            // olvColumn_CreditAccount
            // 
            this.olvColumn_CreditAccount.AspectName = "CreditAccount";
            this.olvColumn_CreditAccount.Groupable = false;
            this.olvColumn_CreditAccount.GroupWithItemCountFormat = null;
            this.olvColumn_CreditAccount.GroupWithItemCountSingularFormat = null;
            resources.ApplyResources(this.olvColumn_CreditAccount, "olvColumn_CreditAccount");
            this.olvColumn_CreditAccount.ToolTipText = null;
            // 
            // olvColumn_DebitAccount
            // 
            this.olvColumn_DebitAccount.AspectName = "DebitAccount";
            this.olvColumn_DebitAccount.Groupable = false;
            this.olvColumn_DebitAccount.GroupWithItemCountFormat = null;
            this.olvColumn_DebitAccount.GroupWithItemCountSingularFormat = null;
            resources.ApplyResources(this.olvColumn_DebitAccount, "olvColumn_DebitAccount");
            this.olvColumn_DebitAccount.ToolTipText = null;
            // 
            // olvColumn_EventType
            // 
            this.olvColumn_EventType.AspectName = "EventType";
            this.olvColumn_EventType.GroupWithItemCountFormat = null;
            this.olvColumn_EventType.GroupWithItemCountSingularFormat = null;
            resources.ApplyResources(this.olvColumn_EventType, "olvColumn_EventType");
            this.olvColumn_EventType.ToolTipText = null;
            // 
            // olvColumn_Amount
            // 
            this.olvColumn_Amount.AspectName = "Amount";
            this.olvColumn_Amount.AutoCompleteEditor = false;
            this.olvColumn_Amount.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvColumn_Amount.Groupable = false;
            this.olvColumn_Amount.GroupWithItemCountFormat = null;
            this.olvColumn_Amount.GroupWithItemCountSingularFormat = null;
            this.olvColumn_Amount.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            resources.ApplyResources(this.olvColumn_Amount, "olvColumn_Amount");
            this.olvColumn_Amount.ToolTipText = null;
            // 
            // olvColumn_TransactionDate
            // 
            this.olvColumn_TransactionDate.AspectName = "Date";
            this.olvColumn_TransactionDate.Groupable = false;
            this.olvColumn_TransactionDate.GroupWithItemCountFormat = null;
            this.olvColumn_TransactionDate.GroupWithItemCountSingularFormat = null;
            resources.ApplyResources(this.olvColumn_TransactionDate, "olvColumn_TransactionDate");
            this.olvColumn_TransactionDate.ToolTipText = null;
            // 
            // cbxAllBookings
            // 
            this.cbxAllBookings.AccessibleDescription = null;
            this.cbxAllBookings.AccessibleName = null;
            resources.ApplyResources(this.cbxAllBookings, "cbxAllBookings");
            this.cbxAllBookings.BackgroundImage = null;
            this.cbxAllBookings.Font = null;
            this.cbxAllBookings.Name = "cbxAllBookings";
            this.cbxAllBookings.UseVisualStyleBackColor = true;
            this.cbxAllBookings.CheckedChanged += new System.EventHandler(this.CbxAllBookingsCheckedChanged);
            // 
            // tbpEvents
            // 
            this.tbpEvents.AccessibleDescription = null;
            this.tbpEvents.AccessibleName = null;
            resources.ApplyResources(this.tbpEvents, "tbpEvents");
            this.tbpEvents.BackgroundImage = null;
            this.tbpEvents.Controls.Add(this.olvEvents);
            this.tbpEvents.Controls.Add(this.cbAllEvents);
            this.tbpEvents.Font = null;
            this.tbpEvents.Name = "tbpEvents";
            this.tbpEvents.UseVisualStyleBackColor = true;
            // 
            // olvEvents
            // 
            this.olvEvents.AccessibleDescription = null;
            this.olvEvents.AccessibleName = null;
            resources.ApplyResources(this.olvEvents, "olvEvents");
            this.olvEvents.AllColumns.Add(this.olvColumn_Code);
            this.olvEvents.AllColumns.Add(this.olvColumn_Description);
            this.olvEvents.AllColumns.Add(this.olvColumn_Amnt);
            this.olvEvents.AllColumns.Add(this.olvColumn_Fee);
            this.olvEvents.AllColumns.Add(this.olvColumn_OLB);
            this.olvEvents.AllColumns.Add(this.olvColumn_OverdueDays);
            this.olvEvents.AllColumns.Add(this.olvColumn_OverduePrincipal);
            this.olvEvents.AllColumns.Add(this.olvColumn_Date);
            this.olvEvents.AllColumns.Add(this.olvColumn_Interest);
            this.olvEvents.AllColumns.Add(this.olvColumn_AccruedInterest);
            this.olvEvents.BackgroundImage = null;
            this.olvEvents.CheckBoxes = true;
            this.olvEvents.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn_Code,
            this.olvColumn_Description,
            this.olvColumn_Amnt,
            this.olvColumn_Fee,
            this.olvColumn_OLB,
            this.olvColumn_OverdueDays,
            this.olvColumn_OverduePrincipal,
            this.olvColumn_Date,
            this.olvColumn_Interest,
            this.olvColumn_AccruedInterest});
            this.olvEvents.EmptyListMsg = null;
            this.olvEvents.Font = null;
            this.olvEvents.FullRowSelect = true;
            this.olvEvents.GridLines = true;
            this.olvEvents.GroupWithItemCountFormat = null;
            this.olvEvents.GroupWithItemCountSingularFormat = null;
            this.olvEvents.HasCollapsibleGroups = false;
            this.olvEvents.Name = "olvEvents";
            this.olvEvents.OverlayText.Text = null;
            this.olvEvents.UseCompatibleStateImageBehavior = false;
            this.olvEvents.View = System.Windows.Forms.View.Details;
            // 
            // olvColumn_Code
            // 
            this.olvColumn_Code.AspectName = "Code";
            this.olvColumn_Code.GroupWithItemCountFormat = null;
            this.olvColumn_Code.GroupWithItemCountSingularFormat = null;
            resources.ApplyResources(this.olvColumn_Code, "olvColumn_Code");
            this.olvColumn_Code.ToolTipText = null;
            // 
            // olvColumn_Description
            // 
            this.olvColumn_Description.AspectName = "Description";
            this.olvColumn_Description.GroupWithItemCountFormat = null;
            this.olvColumn_Description.GroupWithItemCountSingularFormat = null;
            resources.ApplyResources(this.olvColumn_Description, "olvColumn_Description");
            this.olvColumn_Description.ToolTipText = null;
            // 
            // olvColumn_Amnt
            // 
            this.olvColumn_Amnt.AspectName = "Amount";
            this.olvColumn_Amnt.GroupWithItemCountFormat = null;
            this.olvColumn_Amnt.GroupWithItemCountSingularFormat = null;
            resources.ApplyResources(this.olvColumn_Amnt, "olvColumn_Amnt");
            this.olvColumn_Amnt.ToolTipText = null;
            // 
            // olvColumn_Fee
            // 
            this.olvColumn_Fee.AspectName = "Fee";
            this.olvColumn_Fee.GroupWithItemCountFormat = null;
            this.olvColumn_Fee.GroupWithItemCountSingularFormat = null;
            resources.ApplyResources(this.olvColumn_Fee, "olvColumn_Fee");
            this.olvColumn_Fee.ToolTipText = null;
            // 
            // olvColumn_OLB
            // 
            this.olvColumn_OLB.AspectName = "OLB";
            this.olvColumn_OLB.Groupable = false;
            this.olvColumn_OLB.GroupWithItemCountFormat = null;
            this.olvColumn_OLB.GroupWithItemCountSingularFormat = null;
            resources.ApplyResources(this.olvColumn_OLB, "olvColumn_OLB");
            this.olvColumn_OLB.ToolTipText = null;
            // 
            // olvColumn_OverdueDays
            // 
            this.olvColumn_OverdueDays.AspectName = "OverdueDays";
            this.olvColumn_OverdueDays.Groupable = false;
            this.olvColumn_OverdueDays.GroupWithItemCountFormat = null;
            this.olvColumn_OverdueDays.GroupWithItemCountSingularFormat = null;
            resources.ApplyResources(this.olvColumn_OverdueDays, "olvColumn_OverdueDays");
            this.olvColumn_OverdueDays.ToolTipText = null;
            // 
            // olvColumn_OverduePrincipal
            // 
            this.olvColumn_OverduePrincipal.AspectName = "OverduePrincipal";
            this.olvColumn_OverduePrincipal.AutoCompleteEditor = false;
            this.olvColumn_OverduePrincipal.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvColumn_OverduePrincipal.Groupable = false;
            this.olvColumn_OverduePrincipal.GroupWithItemCountFormat = null;
            this.olvColumn_OverduePrincipal.GroupWithItemCountSingularFormat = null;
            this.olvColumn_OverduePrincipal.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            resources.ApplyResources(this.olvColumn_OverduePrincipal, "olvColumn_OverduePrincipal");
            this.olvColumn_OverduePrincipal.ToolTipText = null;
            // 
            // olvColumn_Date
            // 
            this.olvColumn_Date.AspectName = "Date";
            this.olvColumn_Date.Groupable = false;
            this.olvColumn_Date.GroupWithItemCountFormat = null;
            this.olvColumn_Date.GroupWithItemCountSingularFormat = null;
            resources.ApplyResources(this.olvColumn_Date, "olvColumn_Date");
            this.olvColumn_Date.ToolTipText = null;
            // 
            // olvColumn_Interest
            // 
            this.olvColumn_Interest.AspectName = "Interest";
            this.olvColumn_Interest.GroupWithItemCountFormat = null;
            this.olvColumn_Interest.GroupWithItemCountSingularFormat = null;
            resources.ApplyResources(this.olvColumn_Interest, "olvColumn_Interest");
            this.olvColumn_Interest.ToolTipText = null;
            // 
            // olvColumn_AccruedInterest
            // 
            this.olvColumn_AccruedInterest.AspectName = "AccruedInterest";
            this.olvColumn_AccruedInterest.GroupWithItemCountFormat = null;
            this.olvColumn_AccruedInterest.GroupWithItemCountSingularFormat = null;
            resources.ApplyResources(this.olvColumn_AccruedInterest, "olvColumn_AccruedInterest");
            this.olvColumn_AccruedInterest.ToolTipText = null;
            // 
            // cbAllEvents
            // 
            this.cbAllEvents.AccessibleDescription = null;
            this.cbAllEvents.AccessibleName = null;
            resources.ApplyResources(this.cbAllEvents, "cbAllEvents");
            this.cbAllEvents.BackgroundImage = null;
            this.cbAllEvents.Font = null;
            this.cbAllEvents.Name = "cbAllEvents";
            this.cbAllEvents.UseVisualStyleBackColor = true;
            this.cbAllEvents.CheckedChanged += new System.EventHandler(this.CbAllEventsCheckedChanged);
            // 
            // timerClosure
            // 
            this.timerClosure.Interval = 5;
            this.timerClosure.Tick += new System.EventHandler(this.TimerClosureTick);
            // 
            // bwPostEvents
            // 
            this.bwPostEvents.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BwPostEventsDoWork);
            this.bwPostEvents.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BwPostEventsRunWorkerCompleted);
            // 
            // bwPostBookings
            // 
            this.bwPostBookings.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BwPostBookingsDoWork);
            this.bwPostBookings.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BwPostBookingsRunWorkerCompleted);
            // 
            // AccountingJournals
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.tbcMain);
            this.Controls.Add(this.pnlRight);
            this.Font = null;
            this.Name = "AccountingJournals";
            this.Load += new System.EventHandler(this.AccountingJournalsLoad);
            this.pnlRight.ResumeLayout(false);
            this.pnlRight.PerformLayout();
            this.tbcMain.ResumeLayout(false);
            this.tbpMovements.ResumeLayout(false);
            this.tbpMovements.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvBookings)).EndInit();
            this.tbpEvents.ResumeLayout(false);
            this.tbpEvents.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvEvents)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlRight;
        private SweetButton btnPreview;
        private System.Windows.Forms.ComboBox cmbBranches;
        private System.Windows.Forms.Label lblBranch;
        private System.Windows.Forms.DateTimePicker dateTimePickerBeginDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerEndDate;
        private System.Windows.Forms.Label lblBeginDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.ComponentModel.BackgroundWorker bwRun;
        private System.Windows.Forms.CheckedListBox clbxFields;
        private System.Windows.Forms.TabControl tbcMain;
        private System.Windows.Forms.TabPage tbpMovements;
        private BrightIdeasSoftware.ObjectListView olvBookings;
        private BrightIdeasSoftware.OLVColumn olvColumn_EventId;
        private BrightIdeasSoftware.OLVColumn olvColumn_CreditAccount;
        private BrightIdeasSoftware.OLVColumn olvColumn_DebitAccount;
        private BrightIdeasSoftware.OLVColumn olvColumn_EventType;
        private BrightIdeasSoftware.OLVColumn olvColumn_Amount;
        private BrightIdeasSoftware.OLVColumn olvColumn_TransactionDate;
        private System.Windows.Forms.TabPage tbpEvents;
        private BrightIdeasSoftware.ObjectListView olvEvents;
        private BrightIdeasSoftware.OLVColumn olvColumn_OLB;
        private BrightIdeasSoftware.OLVColumn olvColumn_OverdueDays;
        private BrightIdeasSoftware.OLVColumn olvColumn_Code;
        private BrightIdeasSoftware.OLVColumn olvColumn_OverduePrincipal;
        private BrightIdeasSoftware.OLVColumn olvColumn_Date;
        private BrightIdeasSoftware.OLVColumn olvColumn_Amnt;
        private BrightIdeasSoftware.OLVColumn olvColumn_Interest;
        private BrightIdeasSoftware.OLVColumn olvColumn_AccruedInterest;
        private BrightIdeasSoftware.OLVColumn olvColumn_Description;
        private System.Windows.Forms.CheckBox cbxAllBookings;
        private System.Windows.Forms.CheckBox cbAllEvents;
        private SweetButton btnPost;
        private BrightIdeasSoftware.OLVColumn olvColumn_Fee;
        private System.Windows.Forms.Timer timerClosure;
        private SweetButton btnView;
        private System.ComponentModel.BackgroundWorker bwPostEvents;
        private System.ComponentModel.BackgroundWorker bwPostBookings;

    }
}