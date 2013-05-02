using OpenCBS.GUI.UserControl;

namespace OpenCBS.GUI.Accounting
{
    partial class FrmAddContractAccountingRule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAddContractAccountingRule));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.gbxButtons = new System.Windows.Forms.GroupBox();
            this.btnSaving = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.gbxDetails = new System.Windows.Forms.GroupBox();
            this.tbDescription = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.tbOrder = new OpenCBS.GUI.UserControl.TextNumericUserControl();
            this.lblOrder = new System.Windows.Forms.Label();
            this.cmbEventAttribute = new System.Windows.Forms.ComboBox();
            this.lbEventAttribute = new System.Windows.Forms.Label();
            this.cmbEventCode = new System.Windows.Forms.ComboBox();
            this.lbEventType = new System.Windows.Forms.Label();
            this.gbxCriterias = new System.Windows.Forms.GroupBox();
            this.lbCurrency = new System.Windows.Forms.Label();
            this.cmbCurrency = new System.Windows.Forms.ComboBox();
            this.lbProductDescription = new System.Windows.Forms.Label();
            this.lbEconomicActivity = new System.Windows.Forms.Label();
            this.lbClientType = new System.Windows.Forms.Label();
            this.cmbEconomicActivity = new System.Windows.Forms.ComboBox();
            this.lbProduct = new System.Windows.Forms.Label();
            this.cmbClientType = new System.Windows.Forms.ComboBox();
            this.cmbProduct = new System.Windows.Forms.ComboBox();
            this.lbProductType = new System.Windows.Forms.Label();
            this.cmbProductType = new System.Windows.Forms.ComboBox();
            this.cbmEntryDirection = new System.Windows.Forms.ComboBox();
            this.cmbCreditAccount = new System.Windows.Forms.ComboBox();
            this.cmbDebitAccount = new System.Windows.Forms.ComboBox();
            this.lbEntryDirection = new System.Windows.Forms.Label();
            this.lbCreditAccount = new System.Windows.Forms.Label();
            this.lbDebitAccount = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.gbxButtons.SuspendLayout();
            this.gbxDetails.SuspendLayout();
            this.gbxCriterias.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.gbxButtons, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.gbxDetails, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // gbxButtons
            // 
            this.gbxButtons.Controls.Add(this.btnSaving);
            this.gbxButtons.Controls.Add(this.btnClose);
            resources.ApplyResources(this.gbxButtons, "gbxButtons");
            this.gbxButtons.Name = "gbxButtons";
            this.gbxButtons.TabStop = false;
            // 
            // btnSaving
            // 
            resources.ApplyResources(this.btnSaving, "btnSaving");
            this.btnSaving.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSaving.Name = "btnSaving";
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Name = "btnClose";
            // 
            // gbxDetails
            // 
            this.gbxDetails.Controls.Add(this.tbDescription);
            this.gbxDetails.Controls.Add(this.lblDescription);
            this.gbxDetails.Controls.Add(this.tbOrder);
            this.gbxDetails.Controls.Add(this.lblOrder);
            this.gbxDetails.Controls.Add(this.cmbEventAttribute);
            this.gbxDetails.Controls.Add(this.lbEventAttribute);
            this.gbxDetails.Controls.Add(this.cmbEventCode);
            this.gbxDetails.Controls.Add(this.lbEventType);
            this.gbxDetails.Controls.Add(this.gbxCriterias);
            this.gbxDetails.Controls.Add(this.cbmEntryDirection);
            this.gbxDetails.Controls.Add(this.cmbCreditAccount);
            this.gbxDetails.Controls.Add(this.cmbDebitAccount);
            this.gbxDetails.Controls.Add(this.lbEntryDirection);
            this.gbxDetails.Controls.Add(this.lbCreditAccount);
            this.gbxDetails.Controls.Add(this.lbDebitAccount);
            resources.ApplyResources(this.gbxDetails, "gbxDetails");
            this.gbxDetails.Name = "gbxDetails";
            this.gbxDetails.TabStop = false;
            // 
            // tbDescription
            // 
            resources.ApplyResources(this.tbDescription, "tbDescription");
            this.tbDescription.Name = "tbDescription";
            // 
            // lblDescription
            // 
            resources.ApplyResources(this.lblDescription, "lblDescription");
            this.lblDescription.Name = "lblDescription";
            // 
            // tbOrder
            // 
            resources.ApplyResources(this.tbOrder, "tbOrder");
            this.tbOrder.Name = "tbOrder";
            // 
            // lblOrder
            // 
            resources.ApplyResources(this.lblOrder, "lblOrder");
            this.lblOrder.Name = "lblOrder";
            // 
            // cmbEventAttribute
            // 
            this.cmbEventAttribute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmbEventAttribute, "cmbEventAttribute");
            this.cmbEventAttribute.FormattingEnabled = true;
            this.cmbEventAttribute.Name = "cmbEventAttribute";
            // 
            // lbEventAttribute
            // 
            resources.ApplyResources(this.lbEventAttribute, "lbEventAttribute");
            this.lbEventAttribute.Name = "lbEventAttribute";
            // 
            // cmbEventCode
            // 
            this.cmbEventCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmbEventCode, "cmbEventCode");
            this.cmbEventCode.FormattingEnabled = true;
            this.cmbEventCode.Name = "cmbEventCode";
            this.cmbEventCode.SelectedIndexChanged += new System.EventHandler(this.cbEventType_SelectedIndexChanged);
            // 
            // lbEventType
            // 
            resources.ApplyResources(this.lbEventType, "lbEventType");
            this.lbEventType.Name = "lbEventType";
            // 
            // gbxCriterias
            // 
            resources.ApplyResources(this.gbxCriterias, "gbxCriterias");
            this.gbxCriterias.Controls.Add(this.lbCurrency);
            this.gbxCriterias.Controls.Add(this.cmbCurrency);
            this.gbxCriterias.Controls.Add(this.lbProductDescription);
            this.gbxCriterias.Controls.Add(this.lbEconomicActivity);
            this.gbxCriterias.Controls.Add(this.lbClientType);
            this.gbxCriterias.Controls.Add(this.cmbEconomicActivity);
            this.gbxCriterias.Controls.Add(this.lbProduct);
            this.gbxCriterias.Controls.Add(this.cmbClientType);
            this.gbxCriterias.Controls.Add(this.cmbProduct);
            this.gbxCriterias.Controls.Add(this.lbProductType);
            this.gbxCriterias.Controls.Add(this.cmbProductType);
            this.gbxCriterias.Name = "gbxCriterias";
            this.gbxCriterias.TabStop = false;
            // 
            // lbCurrency
            // 
            resources.ApplyResources(this.lbCurrency, "lbCurrency");
            this.lbCurrency.Name = "lbCurrency";
            // 
            // cmbCurrency
            // 
            this.cmbCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmbCurrency, "cmbCurrency");
            this.cmbCurrency.FormattingEnabled = true;
            this.cmbCurrency.Name = "cmbCurrency";
            // 
            // lbProductDescription
            // 
            resources.ApplyResources(this.lbProductDescription, "lbProductDescription");
            this.lbProductDescription.Name = "lbProductDescription";
            // 
            // lbEconomicActivity
            // 
            resources.ApplyResources(this.lbEconomicActivity, "lbEconomicActivity");
            this.lbEconomicActivity.Name = "lbEconomicActivity";
            // 
            // lbClientType
            // 
            resources.ApplyResources(this.lbClientType, "lbClientType");
            this.lbClientType.Name = "lbClientType";
            // 
            // cmbEconomicActivity
            // 
            this.cmbEconomicActivity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmbEconomicActivity, "cmbEconomicActivity");
            this.cmbEconomicActivity.FormattingEnabled = true;
            this.cmbEconomicActivity.Name = "cmbEconomicActivity";
            // 
            // lbProduct
            // 
            resources.ApplyResources(this.lbProduct, "lbProduct");
            this.lbProduct.Name = "lbProduct";
            // 
            // cmbClientType
            // 
            this.cmbClientType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmbClientType, "cmbClientType");
            this.cmbClientType.FormattingEnabled = true;
            this.cmbClientType.Name = "cmbClientType";
            this.cmbClientType.SelectedIndexChanged += new System.EventHandler(this.comboBoxClientType_SelectedIndexChanged);
            // 
            // cmbProduct
            // 
            this.cmbProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmbProduct, "cmbProduct");
            this.cmbProduct.FormattingEnabled = true;
            this.cmbProduct.Name = "cmbProduct";
            // 
            // lbProductType
            // 
            resources.ApplyResources(this.lbProductType, "lbProductType");
            this.lbProductType.Name = "lbProductType";
            // 
            // cmbProductType
            // 
            this.cmbProductType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmbProductType, "cmbProductType");
            this.cmbProductType.FormattingEnabled = true;
            this.cmbProductType.Name = "cmbProductType";
            this.cmbProductType.SelectedIndexChanged += new System.EventHandler(this.comboBoxProductType_SelectedIndexChanged);
            // 
            // cbmEntryDirection
            // 
            this.cbmEntryDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cbmEntryDirection, "cbmEntryDirection");
            this.cbmEntryDirection.FormattingEnabled = true;
            this.cbmEntryDirection.Name = "cbmEntryDirection";
            // 
            // cmbCreditAccount
            // 
            this.cmbCreditAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmbCreditAccount, "cmbCreditAccount");
            this.cmbCreditAccount.FormattingEnabled = true;
            this.cmbCreditAccount.Name = "cmbCreditAccount";
            // 
            // cmbDebitAccount
            // 
            this.cmbDebitAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmbDebitAccount, "cmbDebitAccount");
            this.cmbDebitAccount.FormattingEnabled = true;
            this.cmbDebitAccount.Name = "cmbDebitAccount";
            // 
            // lbEntryDirection
            // 
            resources.ApplyResources(this.lbEntryDirection, "lbEntryDirection");
            this.lbEntryDirection.Name = "lbEntryDirection";
            // 
            // lbCreditAccount
            // 
            resources.ApplyResources(this.lbCreditAccount, "lbCreditAccount");
            this.lbCreditAccount.Name = "lbCreditAccount";
            // 
            // lbDebitAccount
            // 
            resources.ApplyResources(this.lbDebitAccount, "lbDebitAccount");
            this.lbDebitAccount.Name = "lbDebitAccount";
            // 
            // FrmAddContractAccountingRule
            // 
            this.AcceptButton = this.btnSaving;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmAddContractAccountingRule";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.gbxButtons.ResumeLayout(false);
            this.gbxDetails.ResumeLayout(false);
            this.gbxDetails.PerformLayout();
            this.gbxCriterias.ResumeLayout(false);
            this.gbxCriterias.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox gbxButtons;
        private System.Windows.Forms.GroupBox gbxDetails;
        private System.Windows.Forms.Label lbDebitAccount;
        private System.Windows.Forms.ComboBox cmbDebitAccount;
        private System.Windows.Forms.GroupBox gbxCriterias;
        private System.Windows.Forms.ComboBox cmbCreditAccount;
        private System.Windows.Forms.Label lbCreditAccount;
        private System.Windows.Forms.Label lbProductType;
        private System.Windows.Forms.ComboBox cmbProductType;
        private System.Windows.Forms.Label lbClientType;
        private System.Windows.Forms.Label lbProduct;
        private System.Windows.Forms.ComboBox cmbClientType;
        private System.Windows.Forms.ComboBox cmbProduct;
        private System.Windows.Forms.Label lbEconomicActivity;
        private System.Windows.Forms.ComboBox cmbEconomicActivity;
        private System.Windows.Forms.Button btnSaving;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lbProductDescription;
        private System.Windows.Forms.ComboBox cbmEntryDirection;
        private System.Windows.Forms.Label lbEntryDirection;
        private System.Windows.Forms.Label lbEventType;
        private System.Windows.Forms.ComboBox cmbEventAttribute;
        private System.Windows.Forms.Label lbEventAttribute;
        private System.Windows.Forms.ComboBox cmbEventCode;
        private OpenCBS.GUI.UserControl.TextNumericUserControl tbOrder;
        private System.Windows.Forms.Label lblOrder;
        private System.Windows.Forms.TextBox tbDescription;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lbCurrency;
        private System.Windows.Forms.ComboBox cmbCurrency;
    }
}