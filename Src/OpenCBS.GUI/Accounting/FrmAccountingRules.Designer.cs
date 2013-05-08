using OpenCBS.GUI.UserControl;

namespace OpenCBS.GUI.Accounting
{
    partial class FrmAccountingRules
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAccountingRules));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listViewContractsRules = new System.Windows.Forms.ListView();
            this.clhEventType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clhEventAttribute = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clhDebitAccount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clhCreditAccount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clhOrder = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clhDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clhContractProductType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clhContractProduct = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clhContractClientType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clhContractEconomicActivity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clhCurrency = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clhPaymentMethod = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cntmnuRules = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.pasetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListSort = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.sweetButton1 = new System.Windows.Forms.Button();
            this.sbtnExport = new System.Windows.Forms.Button();
            this.cbEventTypes = new System.Windows.Forms.ComboBox();
            this.btnDeleteRule = new System.Windows.Forms.Button();
            this.btnEditRule = new System.Windows.Forms.Button();
            this.btnAddRule = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.fileDialog = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.cntmnuRules.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.listViewContractsRules);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            // 
            // listViewContractsRules
            // 
            this.listViewContractsRules.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clhEventType,
            this.clhEventAttribute,
            this.clhDebitAccount,
            this.clhCreditAccount,
            this.clhOrder,
            this.clhDescription,
            this.clhContractProductType,
            this.clhContractProduct,
            this.clhContractClientType,
            this.clhContractEconomicActivity,
            this.clhCurrency,
            this.clhPaymentMethod});
            this.listViewContractsRules.ContextMenuStrip = this.cntmnuRules;
            resources.ApplyResources(this.listViewContractsRules, "listViewContractsRules");
            this.listViewContractsRules.FullRowSelect = true;
            this.listViewContractsRules.GridLines = true;
            this.listViewContractsRules.MultiSelect = false;
            this.listViewContractsRules.Name = "listViewContractsRules";
            this.listViewContractsRules.SmallImageList = this.imageListSort;
            this.listViewContractsRules.UseCompatibleStateImageBehavior = false;
            this.listViewContractsRules.View = System.Windows.Forms.View.Details;
            this.listViewContractsRules.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListViewContractsRulesColumnClick);
            this.listViewContractsRules.DoubleClick += new System.EventHandler(this.ListViewAccountingRulesDoubleClick);
            // 
            // clhEventType
            // 
            resources.ApplyResources(this.clhEventType, "clhEventType");
            // 
            // clhEventAttribute
            // 
            resources.ApplyResources(this.clhEventAttribute, "clhEventAttribute");
            // 
            // clhDebitAccount
            // 
            resources.ApplyResources(this.clhDebitAccount, "clhDebitAccount");
            // 
            // clhCreditAccount
            // 
            resources.ApplyResources(this.clhCreditAccount, "clhCreditAccount");
            // 
            // clhOrder
            // 
            resources.ApplyResources(this.clhOrder, "clhOrder");
            // 
            // clhDescription
            // 
            resources.ApplyResources(this.clhDescription, "clhDescription");
            // 
            // clhContractProductType
            // 
            resources.ApplyResources(this.clhContractProductType, "clhContractProductType");
            // 
            // clhContractProduct
            // 
            resources.ApplyResources(this.clhContractProduct, "clhContractProduct");
            // 
            // clhContractClientType
            // 
            resources.ApplyResources(this.clhContractClientType, "clhContractClientType");
            // 
            // clhContractEconomicActivity
            // 
            resources.ApplyResources(this.clhContractEconomicActivity, "clhContractEconomicActivity");
            // 
            // clhCurrency
            // 
            resources.ApplyResources(this.clhCurrency, "clhCurrency");
            // 
            // clhPaymentMethod
            // 
            resources.ApplyResources(this.clhPaymentMethod, "clhPaymentMethod");
            // 
            // cntmnuRules
            // 
            resources.ApplyResources(this.cntmnuRules, "cntmnuRules");
            this.cntmnuRules.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyStripMenuItem1,
            this.pasetToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.cntmnuRules.Name = "cntmnuRules";
            // 
            // copyStripMenuItem1
            // 
            this.copyStripMenuItem1.Name = "copyStripMenuItem1";
            resources.ApplyResources(this.copyStripMenuItem1, "copyStripMenuItem1");
            this.copyStripMenuItem1.Click += new System.EventHandler(this.CopyStripMenuItem1Click);
            // 
            // pasetToolStripMenuItem
            // 
            this.pasetToolStripMenuItem.Name = "pasetToolStripMenuItem";
            resources.ApplyResources(this.pasetToolStripMenuItem, "pasetToolStripMenuItem");
            this.pasetToolStripMenuItem.Click += new System.EventHandler(this.PasetToolStripMenuItemClick);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            resources.ApplyResources(this.deleteToolStripMenuItem, "deleteToolStripMenuItem");
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItemClick);
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
            this.groupBox1.Controls.Add(this.sweetButton1);
            this.groupBox1.Controls.Add(this.sbtnExport);
            this.groupBox1.Controls.Add(this.cbEventTypes);
            this.groupBox1.Controls.Add(this.btnDeleteRule);
            this.groupBox1.Controls.Add(this.btnEditRule);
            this.groupBox1.Controls.Add(this.btnAddRule);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // sweetButton1
            // 
            resources.ApplyResources(this.sweetButton1, "sweetButton1");
            this.sweetButton1.Name = "sweetButton1";
            this.sweetButton1.Click += new System.EventHandler(this.SweetButton1Click);
            // 
            // sbtnExport
            // 
            resources.ApplyResources(this.sbtnExport, "sbtnExport");
            this.sbtnExport.Name = "sbtnExport";
            this.sbtnExport.Click += new System.EventHandler(this.SbtnExportClick);
            // 
            // cbEventTypes
            // 
            resources.ApplyResources(this.cbEventTypes, "cbEventTypes");
            this.cbEventTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEventTypes.FormattingEnabled = true;
            this.cbEventTypes.Name = "cbEventTypes";
            this.cbEventTypes.SelectedValueChanged += new System.EventHandler(this.CbEventTypesSelectedValueChanged);
            // 
            // btnDeleteRule
            // 
            resources.ApplyResources(this.btnDeleteRule, "btnDeleteRule");
            this.btnDeleteRule.Name = "btnDeleteRule";
            this.btnDeleteRule.Click += new System.EventHandler(this.ButtonDeleteRuleClick);
            // 
            // btnEditRule
            // 
            resources.ApplyResources(this.btnEditRule, "btnEditRule");
            this.btnEditRule.Name = "btnEditRule";
            this.btnEditRule.Click += new System.EventHandler(this.ButtonEditRuleClick);
            // 
            // btnAddRule
            // 
            resources.ApplyResources(this.btnAddRule, "btnAddRule");
            this.btnAddRule.Name = "btnAddRule";
            this.btnAddRule.Click += new System.EventHandler(this.ButtonAddRuleClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.lblTitle);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.Click += new System.EventHandler(this.ButtonExitClick);
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
            // fileDialog
            // 
            this.fileDialog.DefaultExt = "*.CSV";
            // 
            // FrmAccountingRules
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FrmAccountingRules";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.cntmnuRules.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAddRule;
        private System.Windows.Forms.Button btnDeleteRule;
        private System.Windows.Forms.Button btnEditRule;
        private System.Windows.Forms.ListView listViewContractsRules;
        private System.Windows.Forms.ColumnHeader clhEventType;
        private System.Windows.Forms.ColumnHeader clhEventAttribute;
        private System.Windows.Forms.ColumnHeader clhDebitAccount;
        private System.Windows.Forms.ColumnHeader clhCreditAccount;
        private System.Windows.Forms.ColumnHeader clhContractProductType;
        private System.Windows.Forms.ColumnHeader clhContractProduct;
        private System.Windows.Forms.ColumnHeader clhContractClientType;
        private System.Windows.Forms.ColumnHeader clhContractEconomicActivity;
        private System.Windows.Forms.ColumnHeader clhOrder;
        private System.Windows.Forms.ColumnHeader clhDescription;
        private System.Windows.Forms.ComboBox cbEventTypes;
        private System.Windows.Forms.ImageList imageListSort;
        private System.Windows.Forms.ColumnHeader clhCurrency;
        private System.Windows.Forms.ContextMenuStrip cntmnuRules;
        private System.Windows.Forms.ToolStripMenuItem copyStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem pasetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.Button sbtnExport;
        private System.Windows.Forms.Button sweetButton1;
        private System.Windows.Forms.OpenFileDialog fileDialog;
        private System.Windows.Forms.ColumnHeader clhPaymentMethod;

    }
}