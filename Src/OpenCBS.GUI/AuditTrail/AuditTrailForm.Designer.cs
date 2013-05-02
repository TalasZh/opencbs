using OpenCBS.GUI.UserControl;

namespace OpenCBS.GUI.AuditTrail
{
    partial class AuditTrailForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuditTrailForm));
            this.split2 = new System.Windows.Forms.SplitContainer();
            this.lvEventType = new System.Windows.Forms.ListView();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tabPeriod = new System.Windows.Forms.TableLayoutPanel();
            this.dtFrom = new System.Windows.Forms.DateTimePicker();
            this.dtTo = new System.Windows.Forms.DateTimePicker();
            this.lblMdash = new System.Windows.Forms.Label();
            this.lblPeriod = new System.Windows.Forms.Label();
            this.cbBranch = new System.Windows.Forms.ComboBox();
            this.lblBranch = new System.Windows.Forms.Label();
            this.cbUser = new System.Windows.Forms.ComboBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.chkIncludeDeleted = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnUncheckAll = new System.Windows.Forms.Button();
            this.btnCheckAll = new System.Windows.Forms.Button();
            this.panOptions = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.bwReport = new System.ComponentModel.BackgroundWorker();
            this.bwRefresh = new System.ComponentModel.BackgroundWorker();
            this.olvEvents = new BrightIdeasSoftware.ObjectListView();
            this.colEvents_Date = new BrightIdeasSoftware.OLVColumn();
            this.colEvents_EntryDate = new BrightIdeasSoftware.OLVColumn();
            this.colEvents_Code = new BrightIdeasSoftware.OLVColumn();
            this.colEvents_User = new BrightIdeasSoftware.OLVColumn();
            this.colEvents_Role = new BrightIdeasSoftware.OLVColumn();
            this.colEvents_Description = new BrightIdeasSoftware.OLVColumn();
            this.colEvents_BranchName = new BrightIdeasSoftware.OLVColumn();
            this.split2.Panel1.SuspendLayout();
            this.split2.Panel2.SuspendLayout();
            this.split2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabPeriod.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvEvents)).BeginInit();
            this.SuspendLayout();
            // 
            // split2
            // 
            resources.ApplyResources(this.split2, "split2");
            this.split2.Name = "split2";
            // 
            // split2.Panel1
            // 
            this.split2.Panel1.Controls.Add(this.lvEventType);
            resources.ApplyResources(this.split2.Panel1, "split2.Panel1");
            // 
            // split2.Panel2
            // 
            this.split2.Panel2.Controls.Add(this.panel1);
            this.split2.Panel2.Controls.Add(this.panel3);
            this.split2.Panel2.Controls.Add(this.panel2);
            resources.ApplyResources(this.split2.Panel2, "split2.Panel2");
            // 
            // lvEventType
            // 
            this.lvEventType.CheckBoxes = true;
            this.lvEventType.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader1});
            resources.ApplyResources(this.lvEventType, "lvEventType");
            this.lvEventType.Name = "lvEventType";
            this.lvEventType.UseCompatibleStateImageBehavior = false;
            this.lvEventType.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            resources.ApplyResources(this.columnHeader2, "columnHeader2");
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.btnPrint);
            this.panel1.Name = "panel1";
            // 
            // btnRefresh
            // 
            resources.ApplyResources(this.btnRefresh, "btnRefresh");
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Click += new System.EventHandler(this.OnRefreshClick);
            // 
            // btnPrint
            // 
            resources.ApplyResources(this.btnPrint, "btnPrint");
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Click += new System.EventHandler(this.OnPrintClick);
            // 
            // panel3
            // 
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Controls.Add(this.tabPeriod);
            this.panel3.Controls.Add(this.lblPeriod);
            this.panel3.Controls.Add(this.cbBranch);
            this.panel3.Controls.Add(this.lblBranch);
            this.panel3.Controls.Add(this.cbUser);
            this.panel3.Controls.Add(this.lblUser);
            this.panel3.Controls.Add(this.chkIncludeDeleted);
            this.panel3.Name = "panel3";
            // 
            // tabPeriod
            // 
            resources.ApplyResources(this.tabPeriod, "tabPeriod");
            this.tabPeriod.Controls.Add(this.dtFrom, 0, 0);
            this.tabPeriod.Controls.Add(this.dtTo, 2, 0);
            this.tabPeriod.Controls.Add(this.lblMdash, 1, 0);
            this.tabPeriod.Name = "tabPeriod";
            // 
            // dtFrom
            // 
            this.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            resources.ApplyResources(this.dtFrom, "dtFrom");
            this.dtFrom.Name = "dtFrom";
            // 
            // dtTo
            // 
            this.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            resources.ApplyResources(this.dtTo, "dtTo");
            this.dtTo.Name = "dtTo";
            // 
            // lblMdash
            // 
            resources.ApplyResources(this.lblMdash, "lblMdash");
            this.lblMdash.Name = "lblMdash";
            // 
            // lblPeriod
            // 
            resources.ApplyResources(this.lblPeriod, "lblPeriod");
            this.lblPeriod.Name = "lblPeriod";
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
            // cbUser
            // 
            this.cbUser.DisplayMember = "User.Name";
            resources.ApplyResources(this.cbUser, "cbUser");
            this.cbUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUser.FormattingEnabled = true;
            this.cbUser.Name = "cbUser";
            // 
            // lblUser
            // 
            resources.ApplyResources(this.lblUser, "lblUser");
            this.lblUser.Name = "lblUser";
            // 
            // chkIncludeDeleted
            // 
            resources.ApplyResources(this.chkIncludeDeleted, "chkIncludeDeleted");
            this.chkIncludeDeleted.Name = "chkIncludeDeleted";
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Controls.Add(this.btnUncheckAll);
            this.panel2.Controls.Add(this.btnCheckAll);
            this.panel2.Name = "panel2";
            // 
            // btnUncheckAll
            // 
            resources.ApplyResources(this.btnUncheckAll, "btnUncheckAll");
            this.btnUncheckAll.Name = "btnUncheckAll";
            this.btnUncheckAll.Click += new System.EventHandler(this.OnUncheckAllClick);
            // 
            // btnCheckAll
            // 
            resources.ApplyResources(this.btnCheckAll, "btnCheckAll");
            this.btnCheckAll.Name = "btnCheckAll";
            this.btnCheckAll.Click += new System.EventHandler(this.OnCheckAllClick);
            // 
            // panOptions
            // 
            this.panOptions.Controls.Add(this.split2);
            resources.ApplyResources(this.panOptions, "panOptions");
            this.panOptions.MinimumSize = new System.Drawing.Size(300, 0);
            this.panOptions.Name = "panOptions";
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            // 
            // lblTitle
            // 
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblTitle.Name = "lblTitle";
            // 
            // bwReport
            // 
            this.bwReport.DoWork += new System.ComponentModel.DoWorkEventHandler(this.OnPrintDoWork);
            this.bwReport.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.OnPrintCompleted);
            // 
            // bwRefresh
            // 
            this.bwRefresh.DoWork += new System.ComponentModel.DoWorkEventHandler(this.OnRefreshDoWork);
            this.bwRefresh.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.OnRefreshCompleted);
            // 
            // olvEvents
            // 
            this.olvEvents.AllColumns.Add(this.colEvents_Date);
            this.olvEvents.AllColumns.Add(this.colEvents_EntryDate);
            this.olvEvents.AllColumns.Add(this.colEvents_Code);
            this.olvEvents.AllColumns.Add(this.colEvents_User);
            this.olvEvents.AllColumns.Add(this.colEvents_Role);
            this.olvEvents.AllColumns.Add(this.colEvents_Description);
            this.olvEvents.AllColumns.Add(this.colEvents_BranchName);
            this.olvEvents.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colEvents_Date,
            this.colEvents_EntryDate,
            this.colEvents_Code,
            this.colEvents_User,
            this.colEvents_Role,
            this.colEvents_Description,
            this.colEvents_BranchName});
            resources.ApplyResources(this.olvEvents, "olvEvents");
            this.olvEvents.FullRowSelect = true;
            this.olvEvents.GridLines = true;
            this.olvEvents.Name = "olvEvents";
            this.olvEvents.ShowGroups = false;
            this.olvEvents.UseCompatibleStateImageBehavior = false;
            this.olvEvents.View = System.Windows.Forms.View.Details;
            // 
            // colEvents_Date
            // 
            this.colEvents_Date.AspectName = "Date";
            resources.ApplyResources(this.colEvents_Date, "colEvents_Date");
            // 
            // colEvents_EntryDate
            // 
            this.colEvents_EntryDate.AspectName = "EntryDate";
            resources.ApplyResources(this.colEvents_EntryDate, "colEvents_EntryDate");
            // 
            // colEvents_Code
            // 
            this.colEvents_Code.AspectName = "Code";
            resources.ApplyResources(this.colEvents_Code, "colEvents_Code");
            // 
            // colEvents_User
            // 
            this.colEvents_User.AspectName = "UserName";
            resources.ApplyResources(this.colEvents_User, "colEvents_User");
            // 
            // colEvents_Role
            // 
            this.colEvents_Role.AspectName = "UserRole";
            resources.ApplyResources(this.colEvents_Role, "colEvents_Role");
            // 
            // colEvents_Description
            // 
            this.colEvents_Description.AspectName = "Description";
            resources.ApplyResources(this.colEvents_Description, "colEvents_Description");
            // 
            // colEvents_BranchName
            // 
            this.colEvents_BranchName.AspectName = "BranchName";
            resources.ApplyResources(this.colEvents_BranchName, "colEvents_BranchName");
            // 
            // AuditTrailForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.olvEvents);
            this.Controls.Add(this.panOptions);
            this.Name = "AuditTrailForm";
            this.Load += new System.EventHandler(this.OnLoad);
            this.Controls.SetChildIndex(this.panOptions, 0);
            this.Controls.SetChildIndex(this.olvEvents, 0);
            this.split2.Panel1.ResumeLayout(false);
            this.split2.Panel2.ResumeLayout(false);
            this.split2.Panel2.PerformLayout();
            this.split2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tabPeriod.ResumeLayout(false);
            this.tabPeriod.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panOptions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.olvEvents)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.DateTimePicker dtTo;
        private System.Windows.Forms.DateTimePicker dtFrom;
        private System.Windows.Forms.Label lblPeriod;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.ComboBox cbUser;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TableLayoutPanel tabPeriod;
        private System.Windows.Forms.Label lblMdash;
        private System.Windows.Forms.Panel panOptions;
        private System.Windows.Forms.ListView lvEventType;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.SplitContainer split2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnCheckAll;
        private System.Windows.Forms.Button btnUncheckAll;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.CheckBox chkIncludeDeleted;
        private System.Windows.Forms.Panel panel3;
        private System.ComponentModel.BackgroundWorker bwReport;
        private System.ComponentModel.BackgroundWorker bwRefresh;
        private BrightIdeasSoftware.ObjectListView olvEvents;
        private BrightIdeasSoftware.OLVColumn colEvents_Date;
        private BrightIdeasSoftware.OLVColumn colEvents_EntryDate;
        private BrightIdeasSoftware.OLVColumn colEvents_Code;
        private BrightIdeasSoftware.OLVColumn colEvents_User;
        private BrightIdeasSoftware.OLVColumn colEvents_Role;
        private BrightIdeasSoftware.OLVColumn colEvents_Description;
        private System.Windows.Forms.ComboBox cbBranch;
        private System.Windows.Forms.Label lblBranch;
        private BrightIdeasSoftware.OLVColumn colEvents_BranchName;
    }
}