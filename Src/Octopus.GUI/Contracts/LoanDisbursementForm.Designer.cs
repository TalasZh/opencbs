using System.ComponentModel;
using System.Windows.Forms;
using Octopus.GUI.UserControl;

namespace Octopus.GUI.Contracts
{
    public partial class LoanDisbursementForm
    {
        private GroupBox groupBoxButton;
        private SweetButton buttonCancel;
        private SweetButton buttonSave;
        private Label _lbLoanCode;
        private Label _lbAmount;
        private Label _lbFees;
        private Label lblPivotCurrency;
        private CheckBox checkBoxFees;
        private IContainer components;
        private SweetButton buttonAddExchangeRate;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoanDisbursementForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._lbAmountValue = new System.Windows.Forms.Label();
            this.lblPivotCurrency = new System.Windows.Forms.Label();
            this._lbFundingLine = new System.Windows.Forms.Label();
            this._lbFundingLineValue = new System.Windows.Forms.Label();
            this._lbLoanCode = new System.Windows.Forms.Label();
            this._lbLoanCodeValue = new System.Windows.Forms.Label();
            this.cmbPaymentMethod = new System.Windows.Forms.ComboBox();
            this._lbAmount = new System.Windows.Forms.Label();
            this._lbFees = new System.Windows.Forms.Label();
            this.lblFeesCurrencyPivot = new System.Windows.Forms.Label();
            this.checkBoxFees = new System.Windows.Forms.CheckBox();
            this._lbPaymentMethod = new System.Windows.Forms.Label();
            this.lblAmountCurrency = new System.Windows.Forms.Label();
            this.lblEntryFeeCurrency = new System.Windows.Forms.Label();
            this.tbEntryFee = new System.Windows.Forms.TextBox();
            this.groupBoxButton = new System.Windows.Forms.GroupBox();
            this.btnPrint = new Octopus.GUI.UserControl.PrintButton();
            this.buttonAddExchangeRate = new Octopus.GUI.UserControl.SweetButton();
            this.buttonCancel = new Octopus.GUI.UserControl.SweetButton();
            this.buttonSave = new Octopus.GUI.UserControl.SweetButton();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBoxButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.splitContainer1.Panel1, "splitContainer1.Panel1");
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBoxButton);
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.Controls.Add(this._lbAmountValue, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblPivotCurrency, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this._lbFundingLine, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this._lbFundingLineValue, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this._lbLoanCode, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this._lbLoanCodeValue, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmbPaymentMethod, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this._lbAmount, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this._lbFees, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblFeesCurrencyPivot, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.checkBoxFees, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this._lbPaymentMethod, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblAmountCurrency, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblEntryFeeCurrency, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.tbEntryFee, 1, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // _lbAmountValue
            // 
            resources.ApplyResources(this._lbAmountValue, "_lbAmountValue");
            this._lbAmountValue.BackColor = System.Drawing.Color.Transparent;
            this._lbAmountValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this._lbAmountValue.Name = "_lbAmountValue";
            // 
            // lblPivotCurrency
            // 
            resources.ApplyResources(this.lblPivotCurrency, "lblPivotCurrency");
            this.lblPivotCurrency.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.lblPivotCurrency.Name = "lblPivotCurrency";
            // 
            // _lbFundingLine
            // 
            resources.ApplyResources(this._lbFundingLine, "_lbFundingLine");
            this._lbFundingLine.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this._lbFundingLine.Name = "_lbFundingLine";
            // 
            // _lbFundingLineValue
            // 
            resources.ApplyResources(this._lbFundingLineValue, "_lbFundingLineValue");
            this.tableLayoutPanel1.SetColumnSpan(this._lbFundingLineValue, 3);
            this._lbFundingLineValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this._lbFundingLineValue.Name = "_lbFundingLineValue";
            // 
            // _lbLoanCode
            // 
            resources.ApplyResources(this._lbLoanCode, "_lbLoanCode");
            this._lbLoanCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this._lbLoanCode.Name = "_lbLoanCode";
            // 
            // _lbLoanCodeValue
            // 
            resources.ApplyResources(this._lbLoanCodeValue, "_lbLoanCodeValue");
            this.tableLayoutPanel1.SetColumnSpan(this._lbLoanCodeValue, 3);
            this._lbLoanCodeValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this._lbLoanCodeValue.Name = "_lbLoanCodeValue";
            // 
            // cmbPaymentMethod
            // 
            this.cmbPaymentMethod.DisplayMember = "Name";
            this.cmbPaymentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmbPaymentMethod, "cmbPaymentMethod");
            this.cmbPaymentMethod.FormattingEnabled = true;
            this.cmbPaymentMethod.Name = "cmbPaymentMethod";
            this.cmbPaymentMethod.ValueMember = "Id";
            // 
            // _lbAmount
            // 
            resources.ApplyResources(this._lbAmount, "_lbAmount");
            this._lbAmount.BackColor = System.Drawing.Color.Transparent;
            this._lbAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this._lbAmount.Name = "_lbAmount";
            // 
            // _lbFees
            // 
            resources.ApplyResources(this._lbFees, "_lbFees");
            this._lbFees.BackColor = System.Drawing.Color.Transparent;
            this._lbFees.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this._lbFees.Name = "_lbFees";
            // 
            // lblFeesCurrencyPivot
            // 
            resources.ApplyResources(this.lblFeesCurrencyPivot, "lblFeesCurrencyPivot");
            this.lblFeesCurrencyPivot.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.lblFeesCurrencyPivot.Name = "lblFeesCurrencyPivot";
            // 
            // checkBoxFees
            // 
            this.checkBoxFees.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.checkBoxFees, "checkBoxFees");
            this.checkBoxFees.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.checkBoxFees.Name = "checkBoxFees";
            this.checkBoxFees.UseVisualStyleBackColor = false;
            this.checkBoxFees.CheckedChanged += new System.EventHandler(this.CheckBoxFeesCheckedChanged);
            // 
            // _lbPaymentMethod
            // 
            resources.ApplyResources(this._lbPaymentMethod, "_lbPaymentMethod");
            this._lbPaymentMethod.BackColor = System.Drawing.Color.Transparent;
            this._lbPaymentMethod.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this._lbPaymentMethod.Name = "_lbPaymentMethod";
            // 
            // lblAmountCurrency
            // 
            resources.ApplyResources(this.lblAmountCurrency, "lblAmountCurrency");
            this.lblAmountCurrency.BackColor = System.Drawing.Color.Transparent;
            this.lblAmountCurrency.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.lblAmountCurrency.Name = "lblAmountCurrency";
            // 
            // lblEntryFeeCurrency
            // 
            resources.ApplyResources(this.lblEntryFeeCurrency, "lblEntryFeeCurrency");
            this.lblEntryFeeCurrency.BackColor = System.Drawing.Color.Transparent;
            this.lblEntryFeeCurrency.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.lblEntryFeeCurrency.Name = "lblEntryFeeCurrency";
            // 
            // tbEntryFee
            // 
            resources.ApplyResources(this.tbEntryFee, "tbEntryFee");
            this.tbEntryFee.Name = "tbEntryFee";
            this.tbEntryFee.TextChanged += new System.EventHandler(this.TbEntryFeeTextChanged);
            this.tbEntryFee.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TbEntryFeeValueKeyPress);
            // 
            // groupBoxButton
            // 
            this.groupBoxButton.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.groupBoxButton, "groupBoxButton");
            this.groupBoxButton.Controls.Add(this.btnPrint);
            this.groupBoxButton.Controls.Add(this.buttonAddExchangeRate);
            this.groupBoxButton.Controls.Add(this.buttonCancel);
            this.groupBoxButton.Controls.Add(this.buttonSave);
            this.groupBoxButton.Name = "groupBoxButton";
            this.groupBoxButton.TabStop = false;
            // 
            // btnPrint
            // 
            resources.ApplyResources(this.btnPrint, "btnPrint");
            this.btnPrint.AttachmentPoint = Octopus.Reports.AttachmentPoint.LoanDetails;
            this.btnPrint.BackColor = System.Drawing.Color.Gainsboro;
            this.btnPrint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnPrint.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Print;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.ReportInitializer = null;
            this.btnPrint.UseVisualStyleBackColor = true;
            // 
            // buttonAddExchangeRate
            // 
            this.buttonAddExchangeRate.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.buttonAddExchangeRate, "buttonAddExchangeRate");
            this.buttonAddExchangeRate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonAddExchangeRate.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.New;
            this.buttonAddExchangeRate.Menu = null;
            this.buttonAddExchangeRate.Name = "buttonAddExchangeRate";
            this.buttonAddExchangeRate.UseVisualStyleBackColor = false;
            this.buttonAddExchangeRate.Click += new System.EventHandler(this.ButtonAddExchangeRateClick);
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonCancel.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Close;
            this.buttonCancel.Menu = null;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.buttonSave, "buttonSave");
            this.buttonSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonSave.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Save;
            this.buttonSave.Menu = null;
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSaveClick);
            // 
            // LoanDisbursementForm
            // 
            this.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "LoanDisbursementForm";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBoxButton.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private SplitContainer splitContainer1;
        private Label _lbAmountValue;
        private Label _lbFundingLineValue;
        private Label _lbLoanCodeValue;
        private Label _lbFundingLine;
        private Label lblFeesCurrencyPivot;
        private TableLayoutPanel tableLayoutPanel1;
        private ComboBox cmbPaymentMethod;
        private Label _lbPaymentMethod;
        private PrintButton btnPrint;
        private Label lblAmountCurrency;
        private Label lblEntryFeeCurrency;
        private TextBox tbEntryFee;
    }
}
