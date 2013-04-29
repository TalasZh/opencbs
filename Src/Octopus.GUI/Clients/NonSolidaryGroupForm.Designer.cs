using Octopus.GUI.UserControl;

namespace Octopus.GUI.Clients
{
    partial class NonSolidaryGroupForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NonSolidaryGroupForm));
            this.gbDetails = new System.Windows.Forms.GroupBox();
            this.panelNSGDetails = new System.Windows.Forms.TableLayoutPanel();
            this.cbBranch = new System.Windows.Forms.ComboBox();
            this.lblBranch = new System.Windows.Forms.Label();
            this.lblWeekDay = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.lblDate = new System.Windows.Forms.Label();
            this.dtDate = new System.Windows.Forms.DateTimePicker();
            this.lblLoanOfficer = new System.Windows.Forms.Label();
            this.cbLoanOfficers = new System.Windows.Forms.ComboBox();
            this.cmbWeekDay = new System.Windows.Forms.ComboBox();
            this.cbMeetingDay = new System.Windows.Forms.CheckBox();
            this.linkLabelChangePhoto2 = new System.Windows.Forms.LinkLabel();
            this.linkLabelChangePhoto = new System.Windows.Forms.LinkLabel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelNSGControls = new System.Windows.Forms.Panel();
            this.btnPrint = new Octopus.GUI.UserControl.PrintButton();
            this.btnSave = new Octopus.GUI.UserControl.SweetButton();
            this.btnCancel2 = new Octopus.GUI.UserControl.SweetButton();
            this.tabVillage = new System.Windows.Forms.TabControl();
            this.tpAddress = new System.Windows.Forms.TabPage();
            this.gbAddress = new System.Windows.Forms.GroupBox();
            this.tpMembers = new System.Windows.Forms.TabPage();
            this.lvMembers = new System.Windows.Forms.ListView();
            this.colName = new System.Windows.Forms.ColumnHeader();
            this.colPassport = new System.Windows.Forms.ColumnHeader();
            this.colStatus = new System.Windows.Forms.ColumnHeader();
            this.colJoinDate = new System.Windows.Forms.ColumnHeader();
            this.colLeftDate = new System.Windows.Forms.ColumnHeader();
            this.panelMembersControls = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSearch = new Octopus.GUI.UserControl.SweetButton();
            this.btnAdd = new Octopus.GUI.UserControl.SweetButton();
            this.btnRemove = new Octopus.GUI.UserControl.SweetButton();
            this.btnSetAsLeader = new Octopus.GUI.UserControl.SweetButton();
            this.cbxShowRemovedMembers = new System.Windows.Forms.CheckBox();
            this.tabPageLoan = new System.Windows.Forms.TabPage();
            this.listViewLoans = new System.Windows.Forms.ListView();
            this.clientName = new System.Windows.Forms.ColumnHeader();
            this.loanProduct = new System.Windows.Forms.ColumnHeader();
            this.contractCode = new System.Windows.Forms.ColumnHeader();
            this.status = new System.Windows.Forms.ColumnHeader();
            this.amount = new System.Windows.Forms.ColumnHeader();
            this.olb = new System.Windows.Forms.ColumnHeader();
            this.currency = new System.Windows.Forms.ColumnHeader();
            this.interestRate = new System.Windows.Forms.ColumnHeader();
            this.installmentType = new System.Windows.Forms.ColumnHeader();
            this.numbOfInstallments = new System.Windows.Forms.ColumnHeader();
            this.disbursedDate = new System.Windows.Forms.ColumnHeader();
            this.lastEventDate = new System.Windows.Forms.ColumnHeader();
            this.nextEventDate = new System.Windows.Forms.ColumnHeader();
            this.nextAmountToRepay = new System.Windows.Forms.ColumnHeader();
            this.closeDate = new System.Windows.Forms.ColumnHeader();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAddLoan = new Octopus.GUI.UserControl.SweetButton();
            this.btnValidateLoans = new Octopus.GUI.UserControl.SweetButton();
            this.btnDisburse = new Octopus.GUI.UserControl.SweetButton();
            this.btnRepay = new Octopus.GUI.UserControl.SweetButton();
            this.cbxDisplayAllLoans = new System.Windows.Forms.CheckBox();
            this.tabPageSavings = new System.Windows.Forms.TabPage();
            this.listViewSavings = new System.Windows.Forms.ListView();
            this.columnHeaderClientName = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderCode = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderType = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderProduct = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderBalance = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderCurrency = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderCreationDate = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderLastEventDate = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderStatus = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderCloseDate = new System.Windows.Forms.ColumnHeader();
            this.panelSavingsControls = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAddSavings = new Octopus.GUI.UserControl.SweetButton();
            this.buttonViewSaving = new Octopus.GUI.UserControl.SweetButton();
            this.buttonFastDeposit = new Octopus.GUI.UserControl.SweetButton();
            this.tabPageMeetings = new System.Windows.Forms.TabPage();
            this.olvAttendees = new BrightIdeasSoftware.ObjectListView();
            this.olvAttendeeIdColumn = new BrightIdeasSoftware.OLVColumn();
            this.olvAttendeeTiersIdColumn = new BrightIdeasSoftware.OLVColumn();
            this.olvAttendeeNameColumn = new BrightIdeasSoftware.OLVColumn();
            this.olvAttendeeAttendedColumn = new BrightIdeasSoftware.OLVColumn();
            this.olvAttendeeCommentColumn = new BrightIdeasSoftware.OLVColumn();
            this.olvAttendeeLoanIdColumn = new BrightIdeasSoftware.OLVColumn();
            this.panelAttendeesControls = new System.Windows.Forms.FlowLayoutPanel();
            this.labelMeetingDate = new System.Windows.Forms.Label();
            this.comboBoxMeetingDates = new System.Windows.Forms.ComboBox();
            this.buttonUpdateAttendence = new Octopus.GUI.UserControl.SweetButton();
            this.tabPageCustomizableFields = new System.Windows.Forms.TabPage();
            this.gbDetails.SuspendLayout();
            this.panelNSGDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelNSGControls.SuspendLayout();
            this.tabVillage.SuspendLayout();
            this.tpAddress.SuspendLayout();
            this.tpMembers.SuspendLayout();
            this.panelMembersControls.SuspendLayout();
            this.tabPageLoan.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.tabPageSavings.SuspendLayout();
            this.panelSavingsControls.SuspendLayout();
            this.tabPageMeetings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvAttendees)).BeginInit();
            this.panelAttendeesControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbDetails
            // 
            this.gbDetails.Controls.Add(this.panelNSGDetails);
            this.gbDetails.Controls.Add(this.linkLabelChangePhoto2);
            this.gbDetails.Controls.Add(this.linkLabelChangePhoto);
            this.gbDetails.Controls.Add(this.pictureBox2);
            this.gbDetails.Controls.Add(this.pictureBox1);
            resources.ApplyResources(this.gbDetails, "gbDetails");
            this.gbDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.gbDetails.Name = "gbDetails";
            this.gbDetails.TabStop = false;
            // 
            // panelNSGDetails
            // 
            resources.ApplyResources(this.panelNSGDetails, "panelNSGDetails");
            this.panelNSGDetails.Controls.Add(this.cbBranch, 1, 4);
            this.panelNSGDetails.Controls.Add(this.lblBranch, 0, 4);
            this.panelNSGDetails.Controls.Add(this.lblWeekDay, 0, 3);
            this.panelNSGDetails.Controls.Add(this.lblName, 0, 0);
            this.panelNSGDetails.Controls.Add(this.tbName, 1, 0);
            this.panelNSGDetails.Controls.Add(this.lblDate, 0, 1);
            this.panelNSGDetails.Controls.Add(this.dtDate, 1, 1);
            this.panelNSGDetails.Controls.Add(this.lblLoanOfficer, 0, 2);
            this.panelNSGDetails.Controls.Add(this.cbLoanOfficers, 1, 2);
            this.panelNSGDetails.Controls.Add(this.cmbWeekDay, 1, 3);
            this.panelNSGDetails.Controls.Add(this.cbMeetingDay, 2, 3);
            this.panelNSGDetails.Name = "panelNSGDetails";
            // 
            // cbBranch
            // 
            resources.ApplyResources(this.cbBranch, "cbBranch");
            this.cbBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBranch.FormattingEnabled = true;
            this.cbBranch.Name = "cbBranch";
            // 
            // lblBranch
            // 
            resources.ApplyResources(this.lblBranch, "lblBranch");
            this.lblBranch.Name = "lblBranch";
            // 
            // lblWeekDay
            // 
            resources.ApplyResources(this.lblWeekDay, "lblWeekDay");
            this.lblWeekDay.Name = "lblWeekDay";
            // 
            // lblName
            // 
            resources.ApplyResources(this.lblName, "lblName");
            this.lblName.Name = "lblName";
            // 
            // tbName
            // 
            this.panelNSGDetails.SetColumnSpan(this.tbName, 2);
            resources.ApplyResources(this.tbName, "tbName");
            this.tbName.Name = "tbName";
            this.tbName.TextChanged += new System.EventHandler(this.tbName_TextChanged);
            // 
            // lblDate
            // 
            resources.ApplyResources(this.lblDate, "lblDate");
            this.lblDate.Name = "lblDate";
            // 
            // dtDate
            // 
            this.panelNSGDetails.SetColumnSpan(this.dtDate, 2);
            resources.ApplyResources(this.dtDate, "dtDate");
            this.dtDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtDate.Name = "dtDate";
            // 
            // lblLoanOfficer
            // 
            resources.ApplyResources(this.lblLoanOfficer, "lblLoanOfficer");
            this.lblLoanOfficer.Name = "lblLoanOfficer";
            // 
            // cbLoanOfficers
            // 
            this.panelNSGDetails.SetColumnSpan(this.cbLoanOfficers, 2);
            this.cbLoanOfficers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cbLoanOfficers, "cbLoanOfficers");
            this.cbLoanOfficers.FormattingEnabled = true;
            this.cbLoanOfficers.Name = "cbLoanOfficers";
            // 
            // cmbWeekDay
            // 
            resources.ApplyResources(this.cmbWeekDay, "cmbWeekDay");
            this.cmbWeekDay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWeekDay.FormattingEnabled = true;
            this.cmbWeekDay.Items.AddRange(new object[] {
            resources.GetString("cmbWeekDay.Items"),
            resources.GetString("cmbWeekDay.Items1"),
            resources.GetString("cmbWeekDay.Items2"),
            resources.GetString("cmbWeekDay.Items3"),
            resources.GetString("cmbWeekDay.Items4"),
            resources.GetString("cmbWeekDay.Items5"),
            resources.GetString("cmbWeekDay.Items6")});
            this.cmbWeekDay.Name = "cmbWeekDay";
            // 
            // cbMeetingDay
            // 
            resources.ApplyResources(this.cbMeetingDay, "cbMeetingDay");
            this.cbMeetingDay.Name = "cbMeetingDay";
            this.cbMeetingDay.UseVisualStyleBackColor = true;
            this.cbMeetingDay.CheckedChanged += new System.EventHandler(this.cbMeetingDay_CheckedChanged);
            // 
            // linkLabelChangePhoto2
            // 
            resources.ApplyResources(this.linkLabelChangePhoto2, "linkLabelChangePhoto2");
            this.linkLabelChangePhoto2.Name = "linkLabelChangePhoto2";
            this.linkLabelChangePhoto2.TabStop = true;
            this.linkLabelChangePhoto2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.changePhotoLinkLbl_LinkClicked);
            // 
            // linkLabelChangePhoto
            // 
            resources.ApplyResources(this.linkLabelChangePhoto, "linkLabelChangePhoto");
            this.linkLabelChangePhoto.Name = "linkLabelChangePhoto";
            this.linkLabelChangePhoto.TabStop = true;
            this.linkLabelChangePhoto.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.changePhotoLinkLbl_LinkClicked);
            // 
            // pictureBox2
            // 
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // panelNSGControls
            // 
            this.panelNSGControls.Controls.Add(this.btnPrint);
            this.panelNSGControls.Controls.Add(this.btnSave);
            this.panelNSGControls.Controls.Add(this.btnCancel2);
            resources.ApplyResources(this.panelNSGControls, "panelNSGControls");
            this.panelNSGControls.Name = "panelNSGControls";
            // 
            // btnPrint
            // 
            resources.ApplyResources(this.btnPrint, "btnPrint");
            this.btnPrint.AttachmentPoint = Octopus.Reports.AttachmentPoint.VillageDetails;
            this.btnPrint.BackColor = System.Drawing.Color.Gainsboro;
            this.btnPrint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnPrint.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Print;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.ReportInitializer = null;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Visibility = Octopus.Reports.Visibility.NonSolidaryGroup;
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.BackColor = System.Drawing.Color.Gainsboro;
            this.btnSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnSave.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Save;
            this.btnSave.Menu = null;
            this.btnSave.Name = "btnSave";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel2
            // 
            resources.ApplyResources(this.btnCancel2, "btnCancel2");
            this.btnCancel2.BackColor = System.Drawing.Color.Gainsboro;
            this.btnCancel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnCancel2.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Close;
            this.btnCancel2.Menu = null;
            this.btnCancel2.Name = "btnCancel2";
            this.btnCancel2.UseVisualStyleBackColor = false;
            this.btnCancel2.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tabVillage
            // 
            this.tabVillage.Controls.Add(this.tpAddress);
            this.tabVillage.Controls.Add(this.tpMembers);
            this.tabVillage.Controls.Add(this.tabPageLoan);
            this.tabVillage.Controls.Add(this.tabPageSavings);
            this.tabVillage.Controls.Add(this.tabPageMeetings);
            this.tabVillage.Controls.Add(this.tabPageCustomizableFields);
            resources.ApplyResources(this.tabVillage, "tabVillage");
            this.tabVillage.Name = "tabVillage";
            this.tabVillage.SelectedIndex = 0;
            // 
            // tpAddress
            // 
            resources.ApplyResources(this.tpAddress, "tpAddress");
            this.tpAddress.Controls.Add(this.gbAddress);
            this.tpAddress.Name = "tpAddress";
            this.tpAddress.UseVisualStyleBackColor = true;
            // 
            // gbAddress
            // 
            resources.ApplyResources(this.gbAddress, "gbAddress");
            this.gbAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.gbAddress.Name = "gbAddress";
            this.gbAddress.TabStop = false;
            // 
            // tpMembers
            // 
            this.tpMembers.Controls.Add(this.lvMembers);
            this.tpMembers.Controls.Add(this.panelMembersControls);
            resources.ApplyResources(this.tpMembers, "tpMembers");
            this.tpMembers.Name = "tpMembers";
            this.tpMembers.UseVisualStyleBackColor = true;
            // 
            // lvMembers
            // 
            this.lvMembers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colPassport,
            this.colStatus,
            this.colJoinDate,
            this.colLeftDate});
            resources.ApplyResources(this.lvMembers, "lvMembers");
            this.lvMembers.FullRowSelect = true;
            this.lvMembers.GridLines = true;
            this.lvMembers.HideSelection = false;
            this.lvMembers.MultiSelect = false;
            this.lvMembers.Name = "lvMembers";
            this.lvMembers.UseCompatibleStateImageBehavior = false;
            this.lvMembers.View = System.Windows.Forms.View.Details;
            this.lvMembers.DoubleClick += new System.EventHandler(this.lvMembers_DoubleClick);
            this.lvMembers.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvMembers_ItemSelectionChanged);
            // 
            // colName
            // 
            resources.ApplyResources(this.colName, "colName");
            // 
            // colPassport
            // 
            resources.ApplyResources(this.colPassport, "colPassport");
            // 
            // colStatus
            // 
            resources.ApplyResources(this.colStatus, "colStatus");
            // 
            // colJoinDate
            // 
            resources.ApplyResources(this.colJoinDate, "colJoinDate");
            // 
            // colLeftDate
            // 
            resources.ApplyResources(this.colLeftDate, "colLeftDate");
            // 
            // panelMembersControls
            // 
            resources.ApplyResources(this.panelMembersControls, "panelMembersControls");
            this.panelMembersControls.Controls.Add(this.btnSearch);
            this.panelMembersControls.Controls.Add(this.btnAdd);
            this.panelMembersControls.Controls.Add(this.btnRemove);
            this.panelMembersControls.Controls.Add(this.btnSetAsLeader);
            this.panelMembersControls.Controls.Add(this.cbxShowRemovedMembers);
            this.panelMembersControls.Name = "panelMembersControls";
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.btnSearch, "btnSearch");
            this.btnSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnSearch.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Search;
            this.btnSearch.Menu = null;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnAdd.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.New;
            this.btnAdd.Menu = null;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.btnRemove, "btnRemove");
            this.btnRemove.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnRemove.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Delete;
            this.btnRemove.Menu = null;
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.UseVisualStyleBackColor = false;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnSetAsLeader
            // 
            this.btnSetAsLeader.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.btnSetAsLeader, "btnSetAsLeader");
            this.btnSetAsLeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnSetAsLeader.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.None;
            this.btnSetAsLeader.Menu = null;
            this.btnSetAsLeader.Name = "btnSetAsLeader";
            this.btnSetAsLeader.UseVisualStyleBackColor = false;
            this.btnSetAsLeader.Click += new System.EventHandler(this.btnSetAsLeader_Click);
            // 
            // cbxShowRemovedMembers
            // 
            resources.ApplyResources(this.cbxShowRemovedMembers, "cbxShowRemovedMembers");
            this.cbxShowRemovedMembers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.cbxShowRemovedMembers.Name = "cbxShowRemovedMembers";
            this.cbxShowRemovedMembers.UseVisualStyleBackColor = true;
            this.cbxShowRemovedMembers.CheckedChanged += new System.EventHandler(this.cbxShowRemovedMembers_CheckedChanged);
            // 
            // tabPageLoan
            // 
            this.tabPageLoan.Controls.Add(this.listViewLoans);
            this.tabPageLoan.Controls.Add(this.flowLayoutPanel3);
            resources.ApplyResources(this.tabPageLoan, "tabPageLoan");
            this.tabPageLoan.Name = "tabPageLoan";
            this.tabPageLoan.UseVisualStyleBackColor = true;
            // 
            // listViewLoans
            // 
            this.listViewLoans.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clientName,
            this.loanProduct,
            this.contractCode,
            this.status,
            this.amount,
            this.olb,
            this.currency,
            this.interestRate,
            this.installmentType,
            this.numbOfInstallments,
            this.disbursedDate,
            this.lastEventDate,
            this.nextEventDate,
            this.nextAmountToRepay,
            this.closeDate});
            resources.ApplyResources(this.listViewLoans, "listViewLoans");
            this.listViewLoans.FullRowSelect = true;
            this.listViewLoans.GridLines = true;
            this.listViewLoans.Name = "listViewLoans";
            this.listViewLoans.UseCompatibleStateImageBehavior = false;
            this.listViewLoans.View = System.Windows.Forms.View.Details;
            this.listViewLoans.DoubleClick += new System.EventHandler(this.listViewLoans_DoubleClick);
            // 
            // clientName
            // 
            resources.ApplyResources(this.clientName, "clientName");
            // 
            // loanProduct
            // 
            resources.ApplyResources(this.loanProduct, "loanProduct");
            // 
            // contractCode
            // 
            resources.ApplyResources(this.contractCode, "contractCode");
            // 
            // status
            // 
            resources.ApplyResources(this.status, "status");
            // 
            // amount
            // 
            resources.ApplyResources(this.amount, "amount");
            // 
            // olb
            // 
            resources.ApplyResources(this.olb, "olb");
            // 
            // currency
            // 
            resources.ApplyResources(this.currency, "currency");
            // 
            // interestRate
            // 
            resources.ApplyResources(this.interestRate, "interestRate");
            // 
            // installmentType
            // 
            resources.ApplyResources(this.installmentType, "installmentType");
            // 
            // numbOfInstallments
            // 
            resources.ApplyResources(this.numbOfInstallments, "numbOfInstallments");
            // 
            // disbursedDate
            // 
            resources.ApplyResources(this.disbursedDate, "disbursedDate");
            // 
            // lastEventDate
            // 
            resources.ApplyResources(this.lastEventDate, "lastEventDate");
            // 
            // nextEventDate
            // 
            resources.ApplyResources(this.nextEventDate, "nextEventDate");
            // 
            // nextAmountToRepay
            // 
            resources.ApplyResources(this.nextAmountToRepay, "nextAmountToRepay");
            // 
            // closeDate
            // 
            resources.ApplyResources(this.closeDate, "closeDate");
            // 
            // flowLayoutPanel3
            // 
            resources.ApplyResources(this.flowLayoutPanel3, "flowLayoutPanel3");
            this.flowLayoutPanel3.Controls.Add(this.btnAddLoan);
            this.flowLayoutPanel3.Controls.Add(this.btnValidateLoans);
            this.flowLayoutPanel3.Controls.Add(this.btnDisburse);
            this.flowLayoutPanel3.Controls.Add(this.btnRepay);
            this.flowLayoutPanel3.Controls.Add(this.cbxDisplayAllLoans);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            // 
            // btnAddLoan
            // 
            this.btnAddLoan.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.btnAddLoan, "btnAddLoan");
            this.btnAddLoan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnAddLoan.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.New;
            this.btnAddLoan.Menu = null;
            this.btnAddLoan.Name = "btnAddLoan";
            this.btnAddLoan.UseVisualStyleBackColor = false;
            this.btnAddLoan.Click += new System.EventHandler(this.btnAddLoan_Click);
            // 
            // btnValidateLoans
            // 
            this.btnValidateLoans.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.btnValidateLoans, "btnValidateLoans");
            this.btnValidateLoans.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnValidateLoans.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.None;
            this.btnValidateLoans.Menu = null;
            this.btnValidateLoans.Name = "btnValidateLoans";
            this.btnValidateLoans.UseVisualStyleBackColor = false;
            this.btnValidateLoans.Click += new System.EventHandler(this.btnValidateLoans_Click);
            // 
            // btnDisburse
            // 
            this.btnDisburse.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.btnDisburse, "btnDisburse");
            this.btnDisburse.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnDisburse.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.None;
            this.btnDisburse.Menu = null;
            this.btnDisburse.Name = "btnDisburse";
            this.btnDisburse.Tag = true;
            this.btnDisburse.UseVisualStyleBackColor = false;
            this.btnDisburse.Click += new System.EventHandler(this.buttonLoanDisbursment_Click);
            // 
            // btnRepay
            // 
            this.btnRepay.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.btnRepay, "btnRepay");
            this.btnRepay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnRepay.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.None;
            this.btnRepay.Menu = null;
            this.btnRepay.Name = "btnRepay";
            this.btnRepay.Tag = true;
            this.btnRepay.UseVisualStyleBackColor = false;
            this.btnRepay.Click += new System.EventHandler(this.btnRepay_Click);
            // 
            // cbxDisplayAllLoans
            // 
            resources.ApplyResources(this.cbxDisplayAllLoans, "cbxDisplayAllLoans");
            this.cbxDisplayAllLoans.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.cbxDisplayAllLoans.Name = "cbxDisplayAllLoans";
            this.cbxDisplayAllLoans.UseVisualStyleBackColor = true;
            this.cbxDisplayAllLoans.CheckedChanged += new System.EventHandler(this.cbxDisplayAllLoans_CheckedChanged);
            // 
            // tabPageSavings
            // 
            this.tabPageSavings.Controls.Add(this.listViewSavings);
            this.tabPageSavings.Controls.Add(this.panelSavingsControls);
            resources.ApplyResources(this.tabPageSavings, "tabPageSavings");
            this.tabPageSavings.Name = "tabPageSavings";
            this.tabPageSavings.UseVisualStyleBackColor = true;
            // 
            // listViewSavings
            // 
            this.listViewSavings.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderClientName,
            this.columnHeaderCode,
            this.columnHeaderType,
            this.columnHeaderProduct,
            this.columnHeaderBalance,
            this.columnHeaderCurrency,
            this.columnHeaderCreationDate,
            this.columnHeaderLastEventDate,
            this.columnHeaderStatus,
            this.columnHeaderCloseDate});
            resources.ApplyResources(this.listViewSavings, "listViewSavings");
            this.listViewSavings.FullRowSelect = true;
            this.listViewSavings.GridLines = true;
            this.listViewSavings.MultiSelect = false;
            this.listViewSavings.Name = "listViewSavings";
            this.listViewSavings.UseCompatibleStateImageBehavior = false;
            this.listViewSavings.View = System.Windows.Forms.View.Details;
            this.listViewSavings.DoubleClick += new System.EventHandler(this.listViewSavings_DoubleClick);
            // 
            // columnHeaderClientName
            // 
            resources.ApplyResources(this.columnHeaderClientName, "columnHeaderClientName");
            // 
            // columnHeaderCode
            // 
            resources.ApplyResources(this.columnHeaderCode, "columnHeaderCode");
            // 
            // columnHeaderType
            // 
            resources.ApplyResources(this.columnHeaderType, "columnHeaderType");
            // 
            // columnHeaderProduct
            // 
            resources.ApplyResources(this.columnHeaderProduct, "columnHeaderProduct");
            // 
            // columnHeaderBalance
            // 
            resources.ApplyResources(this.columnHeaderBalance, "columnHeaderBalance");
            // 
            // columnHeaderCurrency
            // 
            resources.ApplyResources(this.columnHeaderCurrency, "columnHeaderCurrency");
            // 
            // columnHeaderCreationDate
            // 
            resources.ApplyResources(this.columnHeaderCreationDate, "columnHeaderCreationDate");
            // 
            // columnHeaderLastEventDate
            // 
            resources.ApplyResources(this.columnHeaderLastEventDate, "columnHeaderLastEventDate");
            // 
            // columnHeaderStatus
            // 
            resources.ApplyResources(this.columnHeaderStatus, "columnHeaderStatus");
            // 
            // columnHeaderCloseDate
            // 
            resources.ApplyResources(this.columnHeaderCloseDate, "columnHeaderCloseDate");
            // 
            // panelSavingsControls
            // 
            resources.ApplyResources(this.panelSavingsControls, "panelSavingsControls");
            this.panelSavingsControls.Controls.Add(this.btnAddSavings);
            this.panelSavingsControls.Controls.Add(this.buttonViewSaving);
            this.panelSavingsControls.Controls.Add(this.buttonFastDeposit);
            this.panelSavingsControls.Name = "panelSavingsControls";
            // 
            // btnAddSavings
            // 
            this.btnAddSavings.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.btnAddSavings, "btnAddSavings");
            this.btnAddSavings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnAddSavings.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.New;
            this.btnAddSavings.Menu = null;
            this.btnAddSavings.Name = "btnAddSavings";
            this.btnAddSavings.UseVisualStyleBackColor = false;
            this.btnAddSavings.Click += new System.EventHandler(this.btnAddSavings_Click);
            // 
            // buttonViewSaving
            // 
            this.buttonViewSaving.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.buttonViewSaving, "buttonViewSaving");
            this.buttonViewSaving.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonViewSaving.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.View;
            this.buttonViewSaving.Menu = null;
            this.buttonViewSaving.Name = "buttonViewSaving";
            this.buttonViewSaving.UseVisualStyleBackColor = false;
            this.buttonViewSaving.Click += new System.EventHandler(this.buttonViewSaving_Click);
            // 
            // buttonFastDeposit
            // 
            this.buttonFastDeposit.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.buttonFastDeposit, "buttonFastDeposit");
            this.buttonFastDeposit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonFastDeposit.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.None;
            this.buttonFastDeposit.Menu = null;
            this.buttonFastDeposit.Name = "buttonFastDeposit";
            this.buttonFastDeposit.UseVisualStyleBackColor = false;
            this.buttonFastDeposit.Click += new System.EventHandler(this.buttonFastDeposit_Click);
            // 
            // tabPageMeetings
            // 
            this.tabPageMeetings.Controls.Add(this.olvAttendees);
            this.tabPageMeetings.Controls.Add(this.panelAttendeesControls);
            resources.ApplyResources(this.tabPageMeetings, "tabPageMeetings");
            this.tabPageMeetings.Name = "tabPageMeetings";
            this.tabPageMeetings.UseVisualStyleBackColor = true;
            // 
            // olvAttendees
            // 
            this.olvAttendees.AllColumns.Add(this.olvAttendeeIdColumn);
            this.olvAttendees.AllColumns.Add(this.olvAttendeeTiersIdColumn);
            this.olvAttendees.AllColumns.Add(this.olvAttendeeNameColumn);
            this.olvAttendees.AllColumns.Add(this.olvAttendeeAttendedColumn);
            this.olvAttendees.AllColumns.Add(this.olvAttendeeCommentColumn);
            this.olvAttendees.AllColumns.Add(this.olvAttendeeLoanIdColumn);
            this.olvAttendees.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.DoubleClick;
            this.olvAttendees.CheckedAspectName = "";
            this.olvAttendees.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvAttendeeIdColumn,
            this.olvAttendeeTiersIdColumn,
            this.olvAttendeeNameColumn,
            this.olvAttendeeAttendedColumn,
            this.olvAttendeeCommentColumn,
            this.olvAttendeeLoanIdColumn});
            resources.ApplyResources(this.olvAttendees, "olvAttendees");
            this.olvAttendees.FullRowSelect = true;
            this.olvAttendees.GridLines = true;
            this.olvAttendees.HideSelection = false;
            this.olvAttendees.Name = "olvAttendees";
            this.olvAttendees.OwnerDraw = true;
            this.olvAttendees.SelectedColumnTint = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.olvAttendees.ShowGroups = false;
            this.olvAttendees.ShowImagesOnSubItems = true;
            this.olvAttendees.UseCompatibleStateImageBehavior = false;
            this.olvAttendees.View = System.Windows.Forms.View.Details;
            // 
            // olvAttendeeIdColumn
            // 
            this.olvAttendeeIdColumn.AspectName = "Id";
            this.olvAttendeeIdColumn.IsEditable = false;
            resources.ApplyResources(this.olvAttendeeIdColumn, "olvAttendeeIdColumn");
            // 
            // olvAttendeeTiersIdColumn
            // 
            this.olvAttendeeTiersIdColumn.AspectName = "TiersId";
            this.olvAttendeeTiersIdColumn.IsEditable = false;
            resources.ApplyResources(this.olvAttendeeTiersIdColumn, "olvAttendeeTiersIdColumn");
            // 
            // olvAttendeeNameColumn
            // 
            this.olvAttendeeNameColumn.AspectName = "PersonName";
            this.olvAttendeeNameColumn.IsEditable = false;
            resources.ApplyResources(this.olvAttendeeNameColumn, "olvAttendeeNameColumn");
            // 
            // olvAttendeeAttendedColumn
            // 
            this.olvAttendeeAttendedColumn.AspectName = "Attended";
            this.olvAttendeeAttendedColumn.CheckBoxes = true;
            this.olvAttendeeAttendedColumn.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            resources.ApplyResources(this.olvAttendeeAttendedColumn, "olvAttendeeAttendedColumn");
            // 
            // olvAttendeeCommentColumn
            // 
            this.olvAttendeeCommentColumn.AspectName = "Comment";
            resources.ApplyResources(this.olvAttendeeCommentColumn, "olvAttendeeCommentColumn");
            // 
            // olvAttendeeLoanIdColumn
            // 
            this.olvAttendeeLoanIdColumn.AspectName = "LoanId";
            this.olvAttendeeLoanIdColumn.IsEditable = false;
            resources.ApplyResources(this.olvAttendeeLoanIdColumn, "olvAttendeeLoanIdColumn");
            // 
            // panelAttendeesControls
            // 
            resources.ApplyResources(this.panelAttendeesControls, "panelAttendeesControls");
            this.panelAttendeesControls.Controls.Add(this.labelMeetingDate);
            this.panelAttendeesControls.Controls.Add(this.comboBoxMeetingDates);
            this.panelAttendeesControls.Controls.Add(this.buttonUpdateAttendence);
            this.panelAttendeesControls.Name = "panelAttendeesControls";
            // 
            // labelMeetingDate
            // 
            resources.ApplyResources(this.labelMeetingDate, "labelMeetingDate");
            this.labelMeetingDate.Name = "labelMeetingDate";
            // 
            // comboBoxMeetingDates
            // 
            resources.ApplyResources(this.comboBoxMeetingDates, "comboBoxMeetingDates");
            this.comboBoxMeetingDates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMeetingDates.FormattingEnabled = true;
            this.comboBoxMeetingDates.Name = "comboBoxMeetingDates";
            this.comboBoxMeetingDates.SelectedValueChanged += new System.EventHandler(this.comboBoxMeetingDates_SelectedValueChanged);
            // 
            // buttonUpdateAttendence
            // 
            this.buttonUpdateAttendence.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.buttonUpdateAttendence, "buttonUpdateAttendence");
            this.buttonUpdateAttendence.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonUpdateAttendence.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Save;
            this.buttonUpdateAttendence.Menu = null;
            this.buttonUpdateAttendence.Name = "buttonUpdateAttendence";
            this.buttonUpdateAttendence.UseVisualStyleBackColor = false;
            this.buttonUpdateAttendence.Click += new System.EventHandler(this.buttonUpdateAttendence_Click);
            // 
            // tabPageCustomizableFields
            // 
            resources.ApplyResources(this.tabPageCustomizableFields, "tabPageCustomizableFields");
            this.tabPageCustomizableFields.Name = "tabPageCustomizableFields";
            this.tabPageCustomizableFields.UseVisualStyleBackColor = true;
            // 
            // NonSolidaryGroupForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.tabVillage);
            this.Controls.Add(this.panelNSGControls);
            this.Controls.Add(this.gbDetails);
            this.Name = "NonSolidaryGroupForm";
            this.Load += new System.EventHandler(this.NonSolidaryGroupForm_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.NonSolidaryGroupForm_FormClosed);
            this.Controls.SetChildIndex(this.gbDetails, 0);
            this.Controls.SetChildIndex(this.panelNSGControls, 0);
            this.Controls.SetChildIndex(this.tabVillage, 0);
            this.gbDetails.ResumeLayout(false);
            this.gbDetails.PerformLayout();
            this.panelNSGDetails.ResumeLayout(false);
            this.panelNSGDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelNSGControls.ResumeLayout(false);
            this.tabVillage.ResumeLayout(false);
            this.tpAddress.ResumeLayout(false);
            this.tpMembers.ResumeLayout(false);
            this.tpMembers.PerformLayout();
            this.panelMembersControls.ResumeLayout(false);
            this.panelMembersControls.PerformLayout();
            this.tabPageLoan.ResumeLayout(false);
            this.tabPageLoan.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.tabPageSavings.ResumeLayout(false);
            this.tabPageSavings.PerformLayout();
            this.panelSavingsControls.ResumeLayout(false);
            this.tabPageMeetings.ResumeLayout(false);
            this.tabPageMeetings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvAttendees)).EndInit();
            this.panelAttendeesControls.ResumeLayout(false);
            this.panelAttendeesControls.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbDetails;
        private System.Windows.Forms.DateTimePicker dtDate;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Panel panelNSGControls;
        private SweetButton btnCancel2;
        private SweetButton btnSave;
        private System.Windows.Forms.TabControl tabVillage;
        private System.Windows.Forms.TabPage tpAddress;
        private System.Windows.Forms.TabPage tpMembers;
        private System.Windows.Forms.GroupBox gbAddress;
        private System.Windows.Forms.ListView lvMembers;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colPassport;
        private SweetButton btnSearch;
        private SweetButton btnRemove;
        private SweetButton btnAdd;
        private System.Windows.Forms.ComboBox cbLoanOfficers;
        private System.Windows.Forms.ColumnHeader colStatus;
        private System.Windows.Forms.TabPage tabPageSavings;
        private System.Windows.Forms.ListView listViewSavings;
        private SweetButton btnAddSavings;
        private System.Windows.Forms.ColumnHeader columnHeaderCode;
        private System.Windows.Forms.ColumnHeader columnHeaderType;
        private System.Windows.Forms.ColumnHeader columnHeaderBalance;
        private System.Windows.Forms.ColumnHeader columnHeaderCreationDate;
        private System.Windows.Forms.ColumnHeader columnHeaderLastEventDate;
        private System.Windows.Forms.ColumnHeader columnHeaderStatus;
        private System.Windows.Forms.ColumnHeader columnHeaderCloseDate;
        private System.Windows.Forms.ColumnHeader columnHeaderProduct;
        private SweetButton buttonViewSaving;
        private SweetButton buttonFastDeposit;
        private System.Windows.Forms.LinkLabel linkLabelChangePhoto;
        public System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel linkLabelChangePhoto2;
        public System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TabPage tabPageLoan;
        private SweetButton btnAddLoan;
        private SweetButton btnRepay;
        private SweetButton btnDisburse;
        private System.Windows.Forms.ListView listViewLoans;
        private System.Windows.Forms.ColumnHeader clientName;
        private System.Windows.Forms.ColumnHeader loanProduct;
        private System.Windows.Forms.ColumnHeader amount;
        private System.Windows.Forms.ColumnHeader olb;
        private System.Windows.Forms.ColumnHeader contractCode;
        private System.Windows.Forms.ColumnHeader currency;
        private System.Windows.Forms.ColumnHeader interestRate;
        private System.Windows.Forms.ColumnHeader installmentType;
        private System.Windows.Forms.ColumnHeader numbOfInstallments;
        private System.Windows.Forms.ColumnHeader disbursedDate;
        private System.Windows.Forms.ColumnHeader status;
        private System.Windows.Forms.TableLayoutPanel panelNSGDetails;
        private System.Windows.Forms.FlowLayoutPanel panelSavingsControls;
        private System.Windows.Forms.Label lblLoanOfficer;
        private System.Windows.Forms.Label lblWeekDay;
        private System.Windows.Forms.ComboBox cmbWeekDay;
        private System.Windows.Forms.CheckBox cbMeetingDay;
        private System.Windows.Forms.ColumnHeader lastEventDate;
        private System.Windows.Forms.ColumnHeader nextEventDate;
        private System.Windows.Forms.ColumnHeader nextAmountToRepay;
        private System.Windows.Forms.ComboBox cbBranch;
        private System.Windows.Forms.Label lblBranch;
        private System.Windows.Forms.ColumnHeader columnHeaderClientName;
        private System.Windows.Forms.ColumnHeader columnHeaderCurrency;
        private System.Windows.Forms.ColumnHeader colJoinDate;
        private System.Windows.Forms.ColumnHeader colLeftDate;
        private System.Windows.Forms.CheckBox cbxShowRemovedMembers;
        private SweetButton btnSetAsLeader;
        private System.Windows.Forms.CheckBox cbxDisplayAllLoans;
        private System.Windows.Forms.ColumnHeader closeDate;
        private System.Windows.Forms.TabPage tabPageMeetings;
        private System.Windows.Forms.FlowLayoutPanel panelAttendeesControls;
        private SweetButton buttonUpdateAttendence;
        private BrightIdeasSoftware.ObjectListView olvAttendees;
        private BrightIdeasSoftware.OLVColumn olvAttendeeNameColumn;
        private BrightIdeasSoftware.OLVColumn olvAttendeeAttendedColumn;
        private BrightIdeasSoftware.OLVColumn olvAttendeeCommentColumn;
        private System.Windows.Forms.Label labelMeetingDate;
        private System.Windows.Forms.ComboBox comboBoxMeetingDates;
        private BrightIdeasSoftware.OLVColumn olvAttendeeIdColumn;
        private BrightIdeasSoftware.OLVColumn olvAttendeeTiersIdColumn;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.FlowLayoutPanel panelMembersControls;
        private SweetButton btnValidateLoans;
        private System.Windows.Forms.TabPage tabPageCustomizableFields;
        private BrightIdeasSoftware.OLVColumn olvAttendeeLoanIdColumn;
        private PrintButton btnPrint;
    }
}