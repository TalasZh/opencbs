using OpenCBS.GUI.UserControl;

namespace OpenCBS.GUI.Accounting
{
    partial class AccountTrialBalance
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountTrialBalance));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tlvBalances = new BrightIdeasSoftware.TreeListView();
            this.olvColumnLACNumber = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnLACLabel = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnLACBalance = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnLACCurrency = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbContractCode = new System.Windows.Forms.ComboBox();
            this.labelLoanContractCode = new System.Windows.Forms.Label();
            this.lblBranch = new System.Windows.Forms.Label();
            this.lblCurrency = new System.Windows.Forms.Label();
            this.cbBranches = new System.Windows.Forms.ComboBox();
            this.cbCurrencies = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tlvBalances)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.tlvBalances);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            // 
            // tlvBalances
            // 
            this.tlvBalances.AllColumns.Add(this.olvColumnLACNumber);
            this.tlvBalances.AllColumns.Add(this.olvColumnLACLabel);
            this.tlvBalances.AllColumns.Add(this.olvColumnLACBalance);
            this.tlvBalances.AllColumns.Add(this.olvColumnLACCurrency);
            this.tlvBalances.CheckBoxes = false;
            this.tlvBalances.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumnLACNumber,
            this.olvColumnLACLabel,
            this.olvColumnLACBalance,
            this.olvColumnLACCurrency});
            resources.ApplyResources(this.tlvBalances, "tlvBalances");
            this.tlvBalances.Name = "tlvBalances";
            this.tlvBalances.OwnerDraw = true;
            this.tlvBalances.ShowGroups = false;
            this.tlvBalances.UseCompatibleStateImageBehavior = false;
            this.tlvBalances.View = System.Windows.Forms.View.Details;
            this.tlvBalances.VirtualMode = true;
            // 
            // olvColumnLACNumber
            // 
            this.olvColumnLACNumber.AspectName = "Number";
            resources.ApplyResources(this.olvColumnLACNumber, "olvColumnLACNumber");
            // 
            // olvColumnLACLabel
            // 
            this.olvColumnLACLabel.AspectName = "Label";
            resources.ApplyResources(this.olvColumnLACLabel, "olvColumnLACLabel");
            // 
            // olvColumnLACBalance
            // 
            this.olvColumnLACBalance.AspectName = "Balance";
            this.olvColumnLACBalance.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            resources.ApplyResources(this.olvColumnLACBalance, "olvColumnLACBalance");
            // 
            // olvColumnLACCurrency
            // 
            this.olvColumnLACCurrency.AspectName = "CurrencyCode";
            resources.ApplyResources(this.olvColumnLACCurrency, "olvColumnLACCurrency");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbContractCode);
            this.groupBox1.Controls.Add(this.labelLoanContractCode);
            this.groupBox1.Controls.Add(this.lblBranch);
            this.groupBox1.Controls.Add(this.lblCurrency);
            this.groupBox1.Controls.Add(this.cbBranches);
            this.groupBox1.Controls.Add(this.cbCurrencies);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // cbContractCode
            // 
            resources.ApplyResources(this.cbContractCode, "cbContractCode");
            this.cbContractCode.DisplayMember = "Currency.Name";
            this.cbContractCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbContractCode.FormattingEnabled = true;
            this.cbContractCode.Items.AddRange(new object[] {
            resources.GetString("cbContractCode.Items"),
            resources.GetString("cbContractCode.Items1")});
            this.cbContractCode.Name = "cbContractCode";
            this.cbContractCode.DropDownClosed += new System.EventHandler(this.cbContractCode_DropDownClosed);
            // 
            // labelLoanContractCode
            // 
            resources.ApplyResources(this.labelLoanContractCode, "labelLoanContractCode");
            this.labelLoanContractCode.BackColor = System.Drawing.Color.Transparent;
            this.labelLoanContractCode.Name = "labelLoanContractCode";
            // 
            // lblBranch
            // 
            resources.ApplyResources(this.lblBranch, "lblBranch");
            this.lblBranch.BackColor = System.Drawing.Color.Transparent;
            this.lblBranch.Name = "lblBranch";
            // 
            // lblCurrency
            // 
            resources.ApplyResources(this.lblCurrency, "lblCurrency");
            this.lblCurrency.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrency.Name = "lblCurrency";
            // 
            // cbBranches
            // 
            resources.ApplyResources(this.cbBranches, "cbBranches");
            this.cbBranches.DisplayMember = "Currency.Name";
            this.cbBranches.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBranches.FormattingEnabled = true;
            this.cbBranches.Name = "cbBranches";
            this.cbBranches.SelectedIndexChanged += new System.EventHandler(this.cbBranches_SelectedIndexChanged);
            // 
            // cbCurrencies
            // 
            resources.ApplyResources(this.cbCurrencies, "cbCurrencies");
            this.cbCurrencies.DisplayMember = "Currency.Name";
            this.cbCurrencies.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCurrencies.FormattingEnabled = true;
            this.cbCurrencies.Name = "cbCurrencies";
            this.cbCurrencies.SelectedIndexChanged += new System.EventHandler(this.cbCurrencies_SelectedIndexChanged);
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
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
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
            // AccountTrialBalance
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "AccountTrialBalance";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tlvBalances)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private BrightIdeasSoftware.TreeListView tlvBalances;
        private BrightIdeasSoftware.OLVColumn olvColumnLACNumber;
        private BrightIdeasSoftware.OLVColumn olvColumnLACLabel;
        private BrightIdeasSoftware.OLVColumn olvColumnLACBalance;
        private BrightIdeasSoftware.OLVColumn olvColumnLACCurrency;
        private System.Windows.Forms.Label lblCurrency;
        private System.Windows.Forms.ComboBox cbCurrencies;
        private System.Windows.Forms.Label labelLoanContractCode;
        private System.Windows.Forms.ComboBox cbContractCode;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblBranch;
        private System.Windows.Forms.ComboBox cbBranches;
    }
}