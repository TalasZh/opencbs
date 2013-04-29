namespace Octopus.GUI.Accounting
{
    partial class AccountingClosureForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountingClosureForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.olvClosures = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn_Id = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn_Date = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn_CountOfTransactions = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn_User = new BrightIdeasSoftware.OLVColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnView = new Octopus.GUI.UserControl.SweetButton();
            this.btnGenerateEvents = new Octopus.GUI.UserControl.SweetButton();
            this.btnDeleteRule = new Octopus.GUI.UserControl.SweetButton();
            this.btnPostBookings = new Octopus.GUI.UserControl.SweetButton();
            this.imageListSort = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.sweetButton1 = new Octopus.GUI.UserControl.SweetButton();
            this.btnClose = new Octopus.GUI.UserControl.SweetButton();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvClosures)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.AccessibleDescription = null;
            this.splitContainer1.AccessibleName = null;
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.BackgroundImage = null;
            this.splitContainer1.Font = null;
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AccessibleDescription = null;
            this.splitContainer1.Panel1.AccessibleName = null;
            resources.ApplyResources(this.splitContainer1.Panel1, "splitContainer1.Panel1");
            this.splitContainer1.Panel1.BackgroundImage = null;
            this.splitContainer1.Panel1.Controls.Add(this.olvClosures);
            this.splitContainer1.Panel1.Font = null;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AccessibleDescription = null;
            this.splitContainer1.Panel2.AccessibleName = null;
            resources.ApplyResources(this.splitContainer1.Panel2, "splitContainer1.Panel2");
            this.splitContainer1.Panel2.BackgroundImage = null;
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Font = null;
            // 
            // olvClosures
            // 
            this.olvClosures.AccessibleDescription = null;
            this.olvClosures.AccessibleName = null;
            resources.ApplyResources(this.olvClosures, "olvClosures");
            this.olvClosures.AllColumns.Add(this.olvColumn_Id);
            this.olvClosures.AllColumns.Add(this.olvColumn_Date);
            this.olvClosures.AllColumns.Add(this.olvColumn_CountOfTransactions);
            this.olvClosures.AllColumns.Add(this.olvColumn_User);
            this.olvClosures.BackgroundImage = null;
            this.olvClosures.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn_Id,
            this.olvColumn_Date,
            this.olvColumn_CountOfTransactions,
            this.olvColumn_User});
            this.olvClosures.EmptyListMsg = null;
            this.olvClosures.Font = null;
            this.olvClosures.FullRowSelect = true;
            this.olvClosures.GridLines = true;
            this.olvClosures.GroupWithItemCountFormat = null;
            this.olvClosures.GroupWithItemCountSingularFormat = null;
            this.olvClosures.HasCollapsibleGroups = false;
            this.olvClosures.Name = "olvClosures";
            this.olvClosures.OverlayText.Text = null;
            this.olvClosures.ShowGroups = false;
            this.olvClosures.UseCompatibleStateImageBehavior = false;
            this.olvClosures.View = System.Windows.Forms.View.Details;
            this.olvClosures.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.olvClosures_FormatRow);
            this.olvClosures.DoubleClick += new System.EventHandler(this.olvClosures_DoubleClick);
            // 
            // olvColumn_Id
            // 
            this.olvColumn_Id.AspectName = "Id";
            this.olvColumn_Id.GroupWithItemCountFormat = null;
            this.olvColumn_Id.GroupWithItemCountSingularFormat = null;
            resources.ApplyResources(this.olvColumn_Id, "olvColumn_Id");
            this.olvColumn_Id.ToolTipText = null;
            // 
            // olvColumn_Date
            // 
            this.olvColumn_Date.AspectName = "Date";
            this.olvColumn_Date.GroupWithItemCountFormat = null;
            this.olvColumn_Date.GroupWithItemCountSingularFormat = null;
            resources.ApplyResources(this.olvColumn_Date, "olvColumn_Date");
            this.olvColumn_Date.ToolTipText = null;
            // 
            // olvColumn_CountOfTransactions
            // 
            this.olvColumn_CountOfTransactions.AspectName = "CountOfTransactions";
            this.olvColumn_CountOfTransactions.GroupWithItemCountFormat = null;
            this.olvColumn_CountOfTransactions.GroupWithItemCountSingularFormat = null;
            resources.ApplyResources(this.olvColumn_CountOfTransactions, "olvColumn_CountOfTransactions");
            this.olvColumn_CountOfTransactions.ToolTipText = null;
            // 
            // olvColumn_User
            // 
            this.olvColumn_User.AspectName = "User";
            this.olvColumn_User.GroupWithItemCountFormat = null;
            this.olvColumn_User.GroupWithItemCountSingularFormat = null;
            resources.ApplyResources(this.olvColumn_User, "olvColumn_User");
            this.olvColumn_User.ToolTipText = null;
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = null;
            this.groupBox1.AccessibleName = null;
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_gris;
            this.groupBox1.Controls.Add(this.btnView);
            this.groupBox1.Controls.Add(this.btnGenerateEvents);
            this.groupBox1.Controls.Add(this.btnDeleteRule);
            this.groupBox1.Controls.Add(this.btnPostBookings);
            this.groupBox1.Font = null;
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // btnView
            // 
            this.btnView.AccessibleDescription = null;
            this.btnView.AccessibleName = null;
            resources.ApplyResources(this.btnView, "btnView");
            this.btnView.BackColor = System.Drawing.Color.Gainsboro;
            this.btnView.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_bouton;
            this.btnView.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnView.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.View;
            this.btnView.Image = global::Octopus.GUI.Properties.Resources.theme1_1_bouton_delete;
            this.btnView.Menu = null;
            this.btnView.Name = "btnView";
            this.btnView.UseVisualStyleBackColor = false;
            this.btnView.Click += new System.EventHandler(this.BtnViewClick);
            // 
            // btnGenerateEvents
            // 
            this.btnGenerateEvents.AccessibleDescription = null;
            this.btnGenerateEvents.AccessibleName = null;
            resources.ApplyResources(this.btnGenerateEvents, "btnGenerateEvents");
            this.btnGenerateEvents.BackColor = System.Drawing.Color.Gainsboro;
            this.btnGenerateEvents.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_bouton;
            this.btnGenerateEvents.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnGenerateEvents.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.New;
            this.btnGenerateEvents.Image = global::Octopus.GUI.Properties.Resources.theme1_1_bouton_new;
            this.btnGenerateEvents.Menu = null;
            this.btnGenerateEvents.Name = "btnGenerateEvents";
            this.btnGenerateEvents.UseVisualStyleBackColor = false;
            this.btnGenerateEvents.Click += new System.EventHandler(this.BtnGenerateEventsClick);
            // 
            // btnDeleteRule
            // 
            this.btnDeleteRule.AccessibleDescription = null;
            this.btnDeleteRule.AccessibleName = null;
            resources.ApplyResources(this.btnDeleteRule, "btnDeleteRule");
            this.btnDeleteRule.BackColor = System.Drawing.Color.Gainsboro;
            this.btnDeleteRule.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_bouton;
            this.btnDeleteRule.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnDeleteRule.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Delete;
            this.btnDeleteRule.Image = global::Octopus.GUI.Properties.Resources.theme1_1_bouton_delete;
            this.btnDeleteRule.Menu = null;
            this.btnDeleteRule.Name = "btnDeleteRule";
            this.btnDeleteRule.UseVisualStyleBackColor = false;
            this.btnDeleteRule.Click += new System.EventHandler(this.btnDeleteRule_Click);
            // 
            // btnPostBookings
            // 
            this.btnPostBookings.AccessibleDescription = null;
            this.btnPostBookings.AccessibleName = null;
            resources.ApplyResources(this.btnPostBookings, "btnPostBookings");
            this.btnPostBookings.BackColor = System.Drawing.Color.Gainsboro;
            this.btnPostBookings.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_bouton;
            this.btnPostBookings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnPostBookings.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.New;
            this.btnPostBookings.Image = global::Octopus.GUI.Properties.Resources.theme1_1_bouton_new;
            this.btnPostBookings.Menu = null;
            this.btnPostBookings.Name = "btnPostBookings";
            this.btnPostBookings.UseVisualStyleBackColor = false;
            this.btnPostBookings.Click += new System.EventHandler(this.BtnAddRuleClick);
            // 
            // imageListSort
            // 
            this.imageListSort.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListSort.ImageStream")));
            this.imageListSort.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListSort.Images.SetKeyName(0, "theme1.1_bouton_down_small.png");
            this.imageListSort.Images.SetKeyName(1, "theme1.1_bouton_up_small.png");
            // 
            // panel1
            // 
            this.panel1.AccessibleDescription = null;
            this.panel1.AccessibleName = null;
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackgroundImage = null;
            this.panel1.Controls.Add(this.sweetButton1);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Font = null;
            this.panel1.Name = "panel1";
            // 
            // sweetButton1
            // 
            this.sweetButton1.AccessibleDescription = null;
            this.sweetButton1.AccessibleName = null;
            resources.ApplyResources(this.sweetButton1, "sweetButton1");
            this.sweetButton1.BackColor = System.Drawing.Color.Gainsboro;
            this.sweetButton1.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_bouton;
            this.sweetButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.sweetButton1.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Close;
            this.sweetButton1.Image = global::Octopus.GUI.Properties.Resources.theme1_1_bouton_close;
            this.sweetButton1.Menu = null;
            this.sweetButton1.Name = "sweetButton1";
            this.sweetButton1.UseVisualStyleBackColor = false;
            this.sweetButton1.Click += new System.EventHandler(this.SweetButton1Click);
            // 
            // btnClose
            // 
            this.btnClose.AccessibleDescription = null;
            this.btnClose.AccessibleName = null;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.BackColor = System.Drawing.Color.Gainsboro;
            this.btnClose.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_bouton;
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnClose.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Close;
            this.btnClose.Image = global::Octopus.GUI.Properties.Resources.theme1_1_bouton_close;
            this.btnClose.Menu = null;
            this.btnClose.Name = "btnClose";
            this.btnClose.UseVisualStyleBackColor = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AccessibleDescription = null;
            this.lblTitle.AccessibleName = null;
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Image = global::Octopus.GUI.Properties.Resources.theme1_1_pastille_contrat;
            this.lblTitle.Name = "lblTitle";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AccessibleDescription = null;
            this.tableLayoutPanel1.AccessibleName = null;
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.BackgroundImage = null;
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanel1.Font = null;
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // AccountingClosureForm
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = null;
            this.Name = "AccountingClosureForm";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.olvClosures)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private Octopus.GUI.UserControl.SweetButton btnDeleteRule;
        private Octopus.GUI.UserControl.SweetButton btnPostBookings;
        private System.Windows.Forms.ImageList imageListSort;
        private Octopus.GUI.UserControl.SweetButton btnClose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Octopus.GUI.UserControl.SweetButton btnGenerateEvents;
        private BrightIdeasSoftware.ObjectListView olvClosures;
        private BrightIdeasSoftware.OLVColumn olvColumn_Id;
        private BrightIdeasSoftware.OLVColumn olvColumn_Date;
        private BrightIdeasSoftware.OLVColumn olvColumn_CountOfTransactions;
        private BrightIdeasSoftware.OLVColumn olvColumn_User;
        private Octopus.GUI.UserControl.SweetButton sweetButton1;
        private Octopus.GUI.UserControl.SweetButton btnView;
    }
}