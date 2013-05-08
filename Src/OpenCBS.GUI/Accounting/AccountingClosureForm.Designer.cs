namespace OpenCBS.GUI.Accounting
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
            this.olvColumn_Id = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn_Date = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn_CountOfTransactions = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn_User = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnView = new System.Windows.Forms.Button();
            this.btnGenerateEvents = new System.Windows.Forms.Button();
            this.btnDeleteRule = new System.Windows.Forms.Button();
            this.btnPostBookings = new System.Windows.Forms.Button();
            this.imageListSort = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.sweetButton1 = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
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
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.olvClosures);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            // 
            // olvClosures
            // 
            this.olvClosures.AllColumns.Add(this.olvColumn_Id);
            this.olvClosures.AllColumns.Add(this.olvColumn_Date);
            this.olvClosures.AllColumns.Add(this.olvColumn_CountOfTransactions);
            this.olvClosures.AllColumns.Add(this.olvColumn_User);
            this.olvClosures.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn_Id,
            this.olvColumn_Date,
            this.olvColumn_CountOfTransactions,
            this.olvColumn_User});
            resources.ApplyResources(this.olvClosures, "olvClosures");
            this.olvClosures.FullRowSelect = true;
            this.olvClosures.GridLines = true;
            this.olvClosures.HasCollapsibleGroups = false;
            this.olvClosures.Name = "olvClosures";
            this.olvClosures.ShowGroups = false;
            this.olvClosures.UseCompatibleStateImageBehavior = false;
            this.olvClosures.View = System.Windows.Forms.View.Details;
            this.olvClosures.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.olvClosures_FormatRow);
            this.olvClosures.DoubleClick += new System.EventHandler(this.olvClosures_DoubleClick);
            // 
            // olvColumn_Id
            // 
            this.olvColumn_Id.AspectName = "Id";
            resources.ApplyResources(this.olvColumn_Id, "olvColumn_Id");
            // 
            // olvColumn_Date
            // 
            this.olvColumn_Date.AspectName = "Date";
            resources.ApplyResources(this.olvColumn_Date, "olvColumn_Date");
            // 
            // olvColumn_CountOfTransactions
            // 
            this.olvColumn_CountOfTransactions.AspectName = "CountOfTransactions";
            resources.ApplyResources(this.olvColumn_CountOfTransactions, "olvColumn_CountOfTransactions");
            // 
            // olvColumn_User
            // 
            this.olvColumn_User.AspectName = "User";
            resources.ApplyResources(this.olvColumn_User, "olvColumn_User");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnView);
            this.groupBox1.Controls.Add(this.btnGenerateEvents);
            this.groupBox1.Controls.Add(this.btnDeleteRule);
            this.groupBox1.Controls.Add(this.btnPostBookings);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // btnView
            // 
            resources.ApplyResources(this.btnView, "btnView");
            this.btnView.Name = "btnView";
            this.btnView.Click += new System.EventHandler(this.BtnViewClick);
            // 
            // btnGenerateEvents
            // 
            resources.ApplyResources(this.btnGenerateEvents, "btnGenerateEvents");
            this.btnGenerateEvents.Name = "btnGenerateEvents";
            this.btnGenerateEvents.Click += new System.EventHandler(this.BtnGenerateEventsClick);
            // 
            // btnDeleteRule
            // 
            resources.ApplyResources(this.btnDeleteRule, "btnDeleteRule");
            this.btnDeleteRule.Name = "btnDeleteRule";
            this.btnDeleteRule.Click += new System.EventHandler(this.btnDeleteRule_Click);
            // 
            // btnPostBookings
            // 
            resources.ApplyResources(this.btnPostBookings, "btnPostBookings");
            this.btnPostBookings.Name = "btnPostBookings";
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
            this.panel1.Controls.Add(this.sweetButton1);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.lblTitle);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // sweetButton1
            // 
            resources.ApplyResources(this.sweetButton1, "sweetButton1");
            this.sweetButton1.Name = "sweetButton1";
            this.sweetButton1.Click += new System.EventHandler(this.SweetButton1Click);
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(152)))));
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Name = "lblTitle";
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // AccountingClosureForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
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
        private System.Windows.Forms.Button btnDeleteRule;
        private System.Windows.Forms.Button btnPostBookings;
        private System.Windows.Forms.ImageList imageListSort;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnGenerateEvents;
        private BrightIdeasSoftware.ObjectListView olvClosures;
        private BrightIdeasSoftware.OLVColumn olvColumn_Id;
        private BrightIdeasSoftware.OLVColumn olvColumn_Date;
        private BrightIdeasSoftware.OLVColumn olvColumn_CountOfTransactions;
        private BrightIdeasSoftware.OLVColumn olvColumn_User;
        private System.Windows.Forms.Button sweetButton1;
        private System.Windows.Forms.Button btnView;
    }
}