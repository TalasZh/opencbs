namespace OpenCBS.GUI.Accounting
{
    partial class ManualEntries
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManualEntries));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lvlMovements = new System.Windows.Forms.ListView();
            this.clhId = new System.Windows.Forms.ColumnHeader();
            this.clhDebitAccount = new System.Windows.Forms.ColumnHeader();
            this.clhCreditAccount = new System.Windows.Forms.ColumnHeader();
            this.clhAmount = new System.Windows.Forms.ColumnHeader();
            this.clhCurrency = new System.Windows.Forms.ColumnHeader();
            this.clhDescription = new System.Windows.Forms.ColumnHeader();
            this.clhDate = new System.Windows.Forms.ColumnHeader();
            this.clhExchangeRate = new System.Windows.Forms.ColumnHeader();
            this.clhUser = new System.Windows.Forms.ColumnHeader();
            this.chBranch = new System.Windows.Forms.ColumnHeader();
            this.imageListSort = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDeleteRule = new System.Windows.Forms.Button();
            this.btnAddRule = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.lvlMovements);
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
            // lvlMovements
            // 
            this.lvlMovements.AccessibleDescription = null;
            this.lvlMovements.AccessibleName = null;
            resources.ApplyResources(this.lvlMovements, "lvlMovements");
            this.lvlMovements.BackgroundImage = null;
            this.lvlMovements.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clhId,
            this.clhDebitAccount,
            this.clhCreditAccount,
            this.clhAmount,
            this.clhCurrency,
            this.clhDescription,
            this.clhDate,
            this.clhExchangeRate,
            this.clhUser,
            this.chBranch});
            this.lvlMovements.Font = null;
            this.lvlMovements.FullRowSelect = true;
            this.lvlMovements.GridLines = true;
            this.lvlMovements.MultiSelect = false;
            this.lvlMovements.Name = "lvlMovements";
            this.lvlMovements.SmallImageList = this.imageListSort;
            this.lvlMovements.UseCompatibleStateImageBehavior = false;
            this.lvlMovements.View = System.Windows.Forms.View.Details;
            // 
            // clhId
            // 
            resources.ApplyResources(this.clhId, "clhId");
            // 
            // clhDebitAccount
            // 
            resources.ApplyResources(this.clhDebitAccount, "clhDebitAccount");
            // 
            // clhCreditAccount
            // 
            resources.ApplyResources(this.clhCreditAccount, "clhCreditAccount");
            // 
            // clhAmount
            // 
            resources.ApplyResources(this.clhAmount, "clhAmount");
            // 
            // clhCurrency
            // 
            resources.ApplyResources(this.clhCurrency, "clhCurrency");
            // 
            // clhDescription
            // 
            resources.ApplyResources(this.clhDescription, "clhDescription");
            // 
            // clhDate
            // 
            resources.ApplyResources(this.clhDate, "clhDate");
            // 
            // clhExchangeRate
            // 
            resources.ApplyResources(this.clhExchangeRate, "clhExchangeRate");
            // 
            // clhUser
            // 
            resources.ApplyResources(this.clhUser, "clhUser");
            // 
            // chBranch
            // 
            resources.ApplyResources(this.chBranch, "chBranch");
            // 
            // imageListSort
            // 
            this.imageListSort.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListSort.ImageStream")));
            this.imageListSort.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListSort.Images.SetKeyName(0, "theme1.1_bouton_down_small.png");
            this.imageListSort.Images.SetKeyName(1, "theme1.1_bouton_up_small.png");
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = null;
            this.groupBox1.AccessibleName = null;
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.btnDeleteRule);
            this.groupBox1.Controls.Add(this.btnAddRule);
            this.groupBox1.Font = null;
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // btnDeleteRule
            // 
            this.btnDeleteRule.AccessibleDescription = null;
            this.btnDeleteRule.AccessibleName = null;
            resources.ApplyResources(this.btnDeleteRule, "btnDeleteRule");
            this.btnDeleteRule.Name = "btnDeleteRule";
            // 
            // btnAddRule
            // 
            this.btnAddRule.AccessibleDescription = null;
            this.btnAddRule.AccessibleName = null;
            resources.ApplyResources(this.btnAddRule, "btnAddRule");
            this.btnAddRule.Name = "btnAddRule";
            this.btnAddRule.Click += new System.EventHandler(this.btnAddRule_Click);
            // 
            // panel1
            // 
            this.panel1.AccessibleDescription = null;
            this.panel1.AccessibleName = null;
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackgroundImage = null;
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Font = null;
            this.panel1.Name = "panel1";
            // 
            // btnClose
            // 
            this.btnClose.AccessibleDescription = null;
            this.btnClose.AccessibleName = null;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AccessibleDescription = null;
            this.lblTitle.AccessibleName = null;
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
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
            // ManualEntries
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = null;
            this.Name = "ManualEntries";
            this.Load += new System.EventHandler(this.ManualEntries_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDeleteRule;
        private System.Windows.Forms.Button btnAddRule;
        private System.Windows.Forms.ImageList imageListSort;
        private System.Windows.Forms.ColumnHeader clhAmount;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ColumnHeader clhExchangeRate;
        private System.Windows.Forms.ColumnHeader clhId;
        private System.Windows.Forms.ListView lvlMovements;
        private System.Windows.Forms.ColumnHeader clhDebitAccount;
        private System.Windows.Forms.ColumnHeader clhCreditAccount;
        private System.Windows.Forms.ColumnHeader clhDescription;
        private System.Windows.Forms.ColumnHeader clhDate;
        private System.Windows.Forms.ColumnHeader clhCurrency;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ColumnHeader clhUser;
        private System.Windows.Forms.ColumnHeader chBranch;
    }
}