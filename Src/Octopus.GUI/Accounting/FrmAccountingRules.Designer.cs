using Octopus.GUI.UserControl;

namespace Octopus.GUI.Accounting
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
            this.clhEventType = new System.Windows.Forms.ColumnHeader();
            this.clhEventAttribute = new System.Windows.Forms.ColumnHeader();
            this.clhDebitAccount = new System.Windows.Forms.ColumnHeader();
            this.clhCreditAccount = new System.Windows.Forms.ColumnHeader();
            this.clhOrder = new System.Windows.Forms.ColumnHeader();
            this.clhDescription = new System.Windows.Forms.ColumnHeader();
            this.clhContractProductType = new System.Windows.Forms.ColumnHeader();
            this.clhContractProduct = new System.Windows.Forms.ColumnHeader();
            this.clhContractClientType = new System.Windows.Forms.ColumnHeader();
            this.clhContractEconomicActivity = new System.Windows.Forms.ColumnHeader();
            this.clhCurrency = new System.Windows.Forms.ColumnHeader();
            this.clhPaymentMethod = new System.Windows.Forms.ColumnHeader();
            this.cntmnuRules = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.pasetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListSort = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.sweetButton1 = new Octopus.GUI.UserControl.SweetButton();
            this.sbtnExport = new Octopus.GUI.UserControl.SweetButton();
            this.cbEventTypes = new System.Windows.Forms.ComboBox();
            this.btnDeleteRule = new Octopus.GUI.UserControl.SweetButton();
            this.btnEditRule = new Octopus.GUI.UserControl.SweetButton();
            this.btnAddRule = new Octopus.GUI.UserControl.SweetButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClose = new Octopus.GUI.UserControl.SweetButton();
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
            this.listViewContractsRules.DoubleClick += new System.EventHandler(this.ListViewAccountingRulesDoubleClick);
            this.listViewContractsRules.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListViewContractsRulesColumnClick);
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
            this.groupBox1.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_gris;
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.sweetButton1);
            this.groupBox1.Controls.Add(this.sbtnExport);
            this.groupBox1.Controls.Add(this.cbEventTypes);
            this.groupBox1.Controls.Add(this.btnDeleteRule);
            this.groupBox1.Controls.Add(this.btnEditRule);
            this.groupBox1.Controls.Add(this.btnAddRule);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // sweetButton1
            // 
            resources.ApplyResources(this.sweetButton1, "sweetButton1");
            this.sweetButton1.BackColor = System.Drawing.Color.Gainsboro;
            this.sweetButton1.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_bouton;
            this.sweetButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.sweetButton1.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Import;
            this.sweetButton1.Menu = null;
            this.sweetButton1.Name = "sweetButton1";
            this.sweetButton1.UseVisualStyleBackColor = false;
            this.sweetButton1.Click += new System.EventHandler(this.SweetButton1Click);
            // 
            // sbtnExport
            // 
            resources.ApplyResources(this.sbtnExport, "sbtnExport");
            this.sbtnExport.BackColor = System.Drawing.Color.Gainsboro;
            this.sbtnExport.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_bouton;
            this.sbtnExport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.sbtnExport.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Export;
            this.sbtnExport.Menu = null;
            this.sbtnExport.Name = "sbtnExport";
            this.sbtnExport.UseVisualStyleBackColor = false;
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
            this.btnDeleteRule.BackColor = System.Drawing.Color.Gainsboro;
            this.btnDeleteRule.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_bouton;
            this.btnDeleteRule.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnDeleteRule.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Delete;
            this.btnDeleteRule.Menu = null;
            this.btnDeleteRule.Name = "btnDeleteRule";
            this.btnDeleteRule.UseVisualStyleBackColor = false;
            this.btnDeleteRule.Click += new System.EventHandler(this.ButtonDeleteRuleClick);
            // 
            // btnEditRule
            // 
            resources.ApplyResources(this.btnEditRule, "btnEditRule");
            this.btnEditRule.BackColor = System.Drawing.Color.Gainsboro;
            this.btnEditRule.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_bouton;
            this.btnEditRule.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnEditRule.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Edit;
            this.btnEditRule.Image = global::Octopus.GUI.Properties.Resources.theme1_1_view;
            this.btnEditRule.Menu = null;
            this.btnEditRule.Name = "btnEditRule";
            this.btnEditRule.UseVisualStyleBackColor = false;
            this.btnEditRule.Click += new System.EventHandler(this.ButtonEditRuleClick);
            // 
            // btnAddRule
            // 
            resources.ApplyResources(this.btnAddRule, "btnAddRule");
            this.btnAddRule.BackColor = System.Drawing.Color.Gainsboro;
            this.btnAddRule.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_bouton;
            this.btnAddRule.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnAddRule.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.New;
            this.btnAddRule.Image = global::Octopus.GUI.Properties.Resources.theme1_1_bouton_new;
            this.btnAddRule.Menu = null;
            this.btnAddRule.Name = "btnAddRule";
            this.btnAddRule.UseVisualStyleBackColor = false;
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
            this.btnClose.BackColor = System.Drawing.Color.Gainsboro;
            this.btnClose.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_bouton;
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnClose.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Close;
            this.btnClose.Image = global::Octopus.GUI.Properties.Resources.theme1_1_bouton_close;
            this.btnClose.Menu = null;
            this.btnClose.Name = "btnClose";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.ButtonExitClick);
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Image = global::Octopus.GUI.Properties.Resources.theme1_1_pastille_contrat;
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
        private SweetButton btnClose;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private SweetButton btnAddRule;
        private SweetButton btnDeleteRule;
        private SweetButton btnEditRule;
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
        private SweetButton sbtnExport;
        private SweetButton sweetButton1;
        private System.Windows.Forms.OpenFileDialog fileDialog;
        private System.Windows.Forms.ColumnHeader clhPaymentMethod;

    }
}