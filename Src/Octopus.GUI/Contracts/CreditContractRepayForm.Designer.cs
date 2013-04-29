using System.ComponentModel;
using System.Windows.Forms;
using Octopus.GUI.UserControl;

namespace Octopus.GUI.Contracts
{
    public partial class CreditContractRepayForm
    {
        private GroupBox groupBoxButton;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private GroupBox groupBoxDetails;
        private CheckBox checkBoxFees;
        private Label _lbDate;
        private Label _lbLoanCode;
        private Label _lbLoanCodeValue;
        private Label _lbInstalmentNbValue;
        private GroupBox groupBoxRepayDetails;
        private Label labelRepayPrincipal;
        private Label labelRepayInterest;
        private Label labelRepayFees;
        private Label _lbInstalmentNb;
        private DateTimePicker dtpRepaymentDate;
        private Label _lbICPrincipal;
        private Label lbInterest;
        private Label _lbICFees;
        private Label _lbICAmountMinMax;
        private ListView listViewRepayments;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private ColumnHeader columnHeader7;
        private ColumnHeader columnHeader8;
        private ColumnHeader columnHeader9;
        private Label lblAmountToGoBackNormal;
        private TextBox textBoxPenalties;
        private GroupBox gbTypeOfRepayment;
        private RadioButton _rbKeepInitialSchedule;
        private RadioButton _rbKeepNotInitialSchedule;
        private Label _lbICCommisions;
        private Label labelRepayCommissions;
        private TextBox textBoxInterest;
        private CheckBox checkBoxInterests;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label _lbClientName;
        private Label _lbClientNameValue;
        private System.Windows.Forms.Button _btAddExchangeRate;
        private GroupBox groupBoxAmount;
        private Label _lbECName;
        private Label _lbICName;
        private Label _lbECAmountMinMax;
        private Panel panelEC;
        private Label _lbECAmountToGoBackNormal;
        private Label _lbECPrincipal;
        private Label lbCInterest;
        private Label _lbECCommissions;
        private Label _lbECFees;
        private ToolTip toolTip;
        private TableLayoutPanel tableLayoutPanel1;
        private IContainer components;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreditContractRepayForm));
            this.listViewRepayments = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxButton = new System.Windows.Forms.GroupBox();
            this._btAddExchangeRate = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.groupBoxDetails = new System.Windows.Forms.GroupBox();
            this.buttonSelectAGroupPerson = new System.Windows.Forms.Button();
            this.textBoxComment = new System.Windows.Forms.TextBox();
            this.cmbPaymentMethod = new System.Windows.Forms.ComboBox();
            this.checkBoxPending = new System.Windows.Forms.CheckBox();
            this.checkBoxTotalAmount = new System.Windows.Forms.CheckBox();
            this.groupBoxAmount = new System.Windows.Forms.GroupBox();
            this.nudICAmount = new System.Windows.Forms.NumericUpDown();
            this.panelEC = new System.Windows.Forms.Panel();
            this.nudECAmount = new System.Windows.Forms.NumericUpDown();
            this._lbECAmountToGoBackNormal = new System.Windows.Forms.Label();
            this._lbECAmountMinMax = new System.Windows.Forms.Label();
            this._lbECName = new System.Windows.Forms.Label();
            this._lbICName = new System.Windows.Forms.Label();
            this._lbICAmountMinMax = new System.Windows.Forms.Label();
            this.lblAmountToGoBackNormal = new System.Windows.Forms.Label();
            this._lbClientNameValue = new System.Windows.Forms.Label();
            this._lbClientName = new System.Windows.Forms.Label();
            this.gbTypeOfRepayment = new System.Windows.Forms.GroupBox();
            this.rbProportionPayment = new System.Windows.Forms.RadioButton();
            this._rbKeepNotInitialSchedule = new System.Windows.Forms.RadioButton();
            this._rbKeepInitialSchedule = new System.Windows.Forms.RadioButton();
            this.dtpRepaymentDate = new System.Windows.Forms.DateTimePicker();
            this.groupBoxRepayDetails = new System.Windows.Forms.GroupBox();
            this._lbECPrincipal = new System.Windows.Forms.Label();
            this._lbICPrincipal = new System.Windows.Forms.Label();
            this.labelRepayPrincipal = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbCInterest = new System.Windows.Forms.Label();
            this.textBoxInterest = new System.Windows.Forms.TextBox();
            this.lbInterest = new System.Windows.Forms.Label();
            this.labelRepayInterest = new System.Windows.Forms.Label();
            this.checkBoxInterests = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this._lbECCommissions = new System.Windows.Forms.Label();
            this._lbICCommisions = new System.Windows.Forms.Label();
            this.textBoxCommission = new System.Windows.Forms.TextBox();
            this._lbECFees = new System.Windows.Forms.Label();
            this._lbICFees = new System.Windows.Forms.Label();
            this.textBoxPenalties = new System.Windows.Forms.TextBox();
            this.labelRepayCommissions = new System.Windows.Forms.Label();
            this.checkBoxFees = new System.Windows.Forms.CheckBox();
            this.labelRepayFees = new System.Windows.Forms.Label();
            this._lbInstalmentNbValue = new System.Windows.Forms.Label();
            this._lbLoanCodeValue = new System.Windows.Forms.Label();
            this._lbInstalmentNb = new System.Windows.Forms.Label();
            this.labelPaymentMethod = new System.Windows.Forms.Label();
            this._lbDate = new System.Windows.Forms.Label();
            this.labelCommentOptional = new System.Windows.Forms.Label();
            this.labelComment = new System.Windows.Forms.Label();
            this._lbLoanCode = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBoxButton.SuspendLayout();
            this.groupBoxDetails.SuspendLayout();
            this.groupBoxAmount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudICAmount)).BeginInit();
            this.panelEC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudECAmount)).BeginInit();
            this.gbTypeOfRepayment.SuspendLayout();
            this.groupBoxRepayDetails.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewRepayments
            // 
            this.listViewRepayments.AccessibleDescription = null;
            this.listViewRepayments.AccessibleName = null;
            resources.ApplyResources(this.listViewRepayments, "listViewRepayments");
            this.listViewRepayments.BackgroundImage = null;
            this.listViewRepayments.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9});
            this.listViewRepayments.FullRowSelect = true;
            this.listViewRepayments.GridLines = true;
            this.listViewRepayments.MultiSelect = false;
            this.listViewRepayments.Name = "listViewRepayments";
            this.listViewRepayments.UseCompatibleStateImageBehavior = false;
            this.listViewRepayments.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // columnHeader2
            // 
            resources.ApplyResources(this.columnHeader2, "columnHeader2");
            // 
            // columnHeader3
            // 
            resources.ApplyResources(this.columnHeader3, "columnHeader3");
            // 
            // columnHeader4
            // 
            resources.ApplyResources(this.columnHeader4, "columnHeader4");
            // 
            // columnHeader5
            // 
            resources.ApplyResources(this.columnHeader5, "columnHeader5");
            // 
            // columnHeader6
            // 
            resources.ApplyResources(this.columnHeader6, "columnHeader6");
            // 
            // columnHeader7
            // 
            resources.ApplyResources(this.columnHeader7, "columnHeader7");
            // 
            // columnHeader8
            // 
            resources.ApplyResources(this.columnHeader8, "columnHeader8");
            // 
            // columnHeader9
            // 
            resources.ApplyResources(this.columnHeader9, "columnHeader9");
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AccessibleDescription = null;
            this.tableLayoutPanel1.AccessibleName = null;
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.BackgroundImage = null;
            this.tableLayoutPanel1.Controls.Add(this.groupBoxButton, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.listViewRepayments, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxDetails, 0, 0);
            this.tableLayoutPanel1.Font = null;
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // groupBoxButton
            // 
            this.groupBoxButton.AccessibleDescription = null;
            this.groupBoxButton.AccessibleName = null;
            resources.ApplyResources(this.groupBoxButton, "groupBoxButton");
            this.groupBoxButton.Controls.Add(this._btAddExchangeRate);
            this.groupBoxButton.Controls.Add(this.buttonCancel);
            this.groupBoxButton.Controls.Add(this.buttonSave);
            this.groupBoxButton.Font = null;
            this.groupBoxButton.Name = "groupBoxButton";
            this.groupBoxButton.TabStop = false;
            // 
            // _btAddExchangeRate
            // 
            this._btAddExchangeRate.AccessibleDescription = null;
            this._btAddExchangeRate.AccessibleName = null;
            resources.ApplyResources(this._btAddExchangeRate, "_btAddExchangeRate");
            this._btAddExchangeRate.Name = "_btAddExchangeRate";
            this._btAddExchangeRate.Click += new System.EventHandler(this._btAddExchangeRate_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.AccessibleDescription = null;
            this.buttonCancel.AccessibleName = null;
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.AccessibleDescription = null;
            this.buttonSave.AccessibleName = null;
            resources.ApplyResources(this.buttonSave, "buttonSave");
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // groupBoxDetails
            // 
            this.groupBoxDetails.AccessibleDescription = null;
            this.groupBoxDetails.AccessibleName = null;
            resources.ApplyResources(this.groupBoxDetails, "groupBoxDetails");
            this.groupBoxDetails.Controls.Add(this.buttonSelectAGroupPerson);
            this.groupBoxDetails.Controls.Add(this.textBoxComment);
            this.groupBoxDetails.Controls.Add(this.cmbPaymentMethod);
            this.groupBoxDetails.Controls.Add(this.checkBoxPending);
            this.groupBoxDetails.Controls.Add(this.checkBoxTotalAmount);
            this.groupBoxDetails.Controls.Add(this.groupBoxAmount);
            this.groupBoxDetails.Controls.Add(this._lbClientNameValue);
            this.groupBoxDetails.Controls.Add(this._lbClientName);
            this.groupBoxDetails.Controls.Add(this.gbTypeOfRepayment);
            this.groupBoxDetails.Controls.Add(this.dtpRepaymentDate);
            this.groupBoxDetails.Controls.Add(this.groupBoxRepayDetails);
            this.groupBoxDetails.Controls.Add(this._lbInstalmentNbValue);
            this.groupBoxDetails.Controls.Add(this._lbLoanCodeValue);
            this.groupBoxDetails.Controls.Add(this._lbInstalmentNb);
            this.groupBoxDetails.Controls.Add(this.labelPaymentMethod);
            this.groupBoxDetails.Controls.Add(this._lbDate);
            this.groupBoxDetails.Controls.Add(this.labelCommentOptional);
            this.groupBoxDetails.Controls.Add(this.labelComment);
            this.groupBoxDetails.Controls.Add(this._lbLoanCode);
            this.groupBoxDetails.Font = null;
            this.groupBoxDetails.Name = "groupBoxDetails";
            this.groupBoxDetails.TabStop = false;
            // 
            // buttonSelectAGroupPerson
            // 
            this.buttonSelectAGroupPerson.AccessibleDescription = null;
            this.buttonSelectAGroupPerson.AccessibleName = null;
            resources.ApplyResources(this.buttonSelectAGroupPerson, "buttonSelectAGroupPerson");
            this.buttonSelectAGroupPerson.BackColor = System.Drawing.Color.Transparent;
            this.buttonSelectAGroupPerson.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_pastille_personne;
            this.buttonSelectAGroupPerson.Font = null;
            this.buttonSelectAGroupPerson.Name = "buttonSelectAGroupPerson";
            this.buttonSelectAGroupPerson.UseVisualStyleBackColor = false;
            this.buttonSelectAGroupPerson.Click += new System.EventHandler(this.buttonSelectAGroupPerson_Click);
            // 
            // textBoxComment
            // 
            this.textBoxComment.AccessibleDescription = null;
            this.textBoxComment.AccessibleName = null;
            resources.ApplyResources(this.textBoxComment, "textBoxComment");
            this.textBoxComment.BackgroundImage = null;
            this.textBoxComment.Name = "textBoxComment";
            // 
            // cmbPaymentMethod
            // 
            this.cmbPaymentMethod.AccessibleDescription = null;
            this.cmbPaymentMethod.AccessibleName = null;
            resources.ApplyResources(this.cmbPaymentMethod, "cmbPaymentMethod");
            this.cmbPaymentMethod.BackgroundImage = null;
            this.cmbPaymentMethod.DisplayMember = "Name";
            this.cmbPaymentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaymentMethod.Font = null;
            this.cmbPaymentMethod.FormattingEnabled = true;
            this.cmbPaymentMethod.Name = "cmbPaymentMethod";
            this.cmbPaymentMethod.ValueMember = "Id";
            this.cmbPaymentMethod.SelectedIndexChanged += new System.EventHandler(this.comboBoxPaymentMethod_SelectedIndexChanged);
            // 
            // checkBoxPending
            // 
            this.checkBoxPending.AccessibleDescription = null;
            this.checkBoxPending.AccessibleName = null;
            resources.ApplyResources(this.checkBoxPending, "checkBoxPending");
            this.checkBoxPending.Checked = true;
            this.checkBoxPending.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxPending.Font = null;
            this.checkBoxPending.Name = "checkBoxPending";
            this.checkBoxPending.CheckedChanged += new System.EventHandler(this.checkBoxPending_CheckedChanged);
            // 
            // checkBoxTotalAmount
            // 
            this.checkBoxTotalAmount.AccessibleDescription = null;
            this.checkBoxTotalAmount.AccessibleName = null;
            resources.ApplyResources(this.checkBoxTotalAmount, "checkBoxTotalAmount");
            this.checkBoxTotalAmount.Font = null;
            this.checkBoxTotalAmount.Name = "checkBoxTotalAmount";
            this.checkBoxTotalAmount.CheckedChanged += new System.EventHandler(this.checkBoxTotalAmount_CheckedChanged);
            // 
            // groupBoxAmount
            // 
            this.groupBoxAmount.AccessibleDescription = null;
            this.groupBoxAmount.AccessibleName = null;
            resources.ApplyResources(this.groupBoxAmount, "groupBoxAmount");
            this.groupBoxAmount.Controls.Add(this.nudICAmount);
            this.groupBoxAmount.Controls.Add(this.panelEC);
            this.groupBoxAmount.Controls.Add(this._lbICName);
            this.groupBoxAmount.Controls.Add(this._lbICAmountMinMax);
            this.groupBoxAmount.Controls.Add(this.lblAmountToGoBackNormal);
            this.groupBoxAmount.Font = null;
            this.groupBoxAmount.Name = "groupBoxAmount";
            this.groupBoxAmount.TabStop = false;
            // 
            // nudICAmount
            // 
            this.nudICAmount.AccessibleDescription = null;
            this.nudICAmount.AccessibleName = null;
            resources.ApplyResources(this.nudICAmount, "nudICAmount");
            this.nudICAmount.Font = null;
            this.nudICAmount.Name = "nudICAmount";
            this.nudICAmount.ValueChanged += new System.EventHandler(this.nudICAmount_ValueChanged);
            // 
            // panelEC
            // 
            this.panelEC.AccessibleDescription = null;
            this.panelEC.AccessibleName = null;
            resources.ApplyResources(this.panelEC, "panelEC");
            this.panelEC.BackgroundImage = null;
            this.panelEC.Controls.Add(this.nudECAmount);
            this.panelEC.Controls.Add(this._lbECAmountToGoBackNormal);
            this.panelEC.Controls.Add(this._lbECAmountMinMax);
            this.panelEC.Controls.Add(this._lbECName);
            this.panelEC.Font = null;
            this.panelEC.Name = "panelEC";
            // 
            // nudECAmount
            // 
            this.nudECAmount.AccessibleDescription = null;
            this.nudECAmount.AccessibleName = null;
            resources.ApplyResources(this.nudECAmount, "nudECAmount");
            this.nudECAmount.Font = null;
            this.nudECAmount.Name = "nudECAmount";
            this.nudECAmount.ValueChanged += new System.EventHandler(this.nudECAmount_ValueChanged);
            // 
            // _lbECAmountToGoBackNormal
            // 
            this._lbECAmountToGoBackNormal.AccessibleDescription = null;
            this._lbECAmountToGoBackNormal.AccessibleName = null;
            resources.ApplyResources(this._lbECAmountToGoBackNormal, "_lbECAmountToGoBackNormal");
            this._lbECAmountToGoBackNormal.Name = "_lbECAmountToGoBackNormal";
            // 
            // _lbECAmountMinMax
            // 
            this._lbECAmountMinMax.AccessibleDescription = null;
            this._lbECAmountMinMax.AccessibleName = null;
            resources.ApplyResources(this._lbECAmountMinMax, "_lbECAmountMinMax");
            this._lbECAmountMinMax.Name = "_lbECAmountMinMax";
            // 
            // _lbECName
            // 
            this._lbECName.AccessibleDescription = null;
            this._lbECName.AccessibleName = null;
            resources.ApplyResources(this._lbECName, "_lbECName");
            this._lbECName.Name = "_lbECName";
            // 
            // _lbICName
            // 
            this._lbICName.AccessibleDescription = null;
            this._lbICName.AccessibleName = null;
            resources.ApplyResources(this._lbICName, "_lbICName");
            this._lbICName.Name = "_lbICName";
            // 
            // _lbICAmountMinMax
            // 
            this._lbICAmountMinMax.AccessibleDescription = null;
            this._lbICAmountMinMax.AccessibleName = null;
            resources.ApplyResources(this._lbICAmountMinMax, "_lbICAmountMinMax");
            this._lbICAmountMinMax.Name = "_lbICAmountMinMax";
            // 
            // lblAmountToGoBackNormal
            // 
            this.lblAmountToGoBackNormal.AccessibleDescription = null;
            this.lblAmountToGoBackNormal.AccessibleName = null;
            resources.ApplyResources(this.lblAmountToGoBackNormal, "lblAmountToGoBackNormal");
            this.lblAmountToGoBackNormal.Name = "lblAmountToGoBackNormal";
            this.lblAmountToGoBackNormal.MouseLeave += new System.EventHandler(this._lbICAmountToGoBackNormal_MouseLeave);
            this.lblAmountToGoBackNormal.DoubleClick += new System.EventHandler(this._lbICAmountToGoBackNormal_DoubleClick);
            this.lblAmountToGoBackNormal.MouseEnter += new System.EventHandler(this._lbICAmountToGoBackNormal_MouseEnter);
            // 
            // _lbClientNameValue
            // 
            this._lbClientNameValue.AccessibleDescription = null;
            this._lbClientNameValue.AccessibleName = null;
            resources.ApplyResources(this._lbClientNameValue, "_lbClientNameValue");
            this._lbClientNameValue.Font = null;
            this._lbClientNameValue.Name = "_lbClientNameValue";
            // 
            // _lbClientName
            // 
            this._lbClientName.AccessibleDescription = null;
            this._lbClientName.AccessibleName = null;
            resources.ApplyResources(this._lbClientName, "_lbClientName");
            this._lbClientName.Name = "_lbClientName";
            // 
            // gbTypeOfRepayment
            // 
            this.gbTypeOfRepayment.AccessibleDescription = null;
            this.gbTypeOfRepayment.AccessibleName = null;
            resources.ApplyResources(this.gbTypeOfRepayment, "gbTypeOfRepayment");
            this.gbTypeOfRepayment.Controls.Add(this.rbProportionPayment);
            this.gbTypeOfRepayment.Controls.Add(this._rbKeepNotInitialSchedule);
            this.gbTypeOfRepayment.Controls.Add(this._rbKeepInitialSchedule);
            this.gbTypeOfRepayment.Name = "gbTypeOfRepayment";
            this.gbTypeOfRepayment.TabStop = false;
            // 
            // rbProportionPayment
            // 
            this.rbProportionPayment.AccessibleDescription = null;
            this.rbProportionPayment.AccessibleName = null;
            resources.ApplyResources(this.rbProportionPayment, "rbProportionPayment");
            this.rbProportionPayment.Name = "rbProportionPayment";
            this.rbProportionPayment.Click += new System.EventHandler(this.rbProportionPayment_Click);
            // 
            // _rbKeepNotInitialSchedule
            // 
            this._rbKeepNotInitialSchedule.AccessibleDescription = null;
            this._rbKeepNotInitialSchedule.AccessibleName = null;
            resources.ApplyResources(this._rbKeepNotInitialSchedule, "_rbKeepNotInitialSchedule");
            this._rbKeepNotInitialSchedule.Name = "_rbKeepNotInitialSchedule";
            this._rbKeepNotInitialSchedule.Click += new System.EventHandler(this._rbKeepNotInitialSchedule_Click);
            // 
            // _rbKeepInitialSchedule
            // 
            this._rbKeepInitialSchedule.AccessibleDescription = null;
            this._rbKeepInitialSchedule.AccessibleName = null;
            resources.ApplyResources(this._rbKeepInitialSchedule, "_rbKeepInitialSchedule");
            this._rbKeepInitialSchedule.Checked = true;
            this._rbKeepInitialSchedule.Name = "_rbKeepInitialSchedule";
            this._rbKeepInitialSchedule.TabStop = true;
            this._rbKeepInitialSchedule.Click += new System.EventHandler(this._rbKeepInitialSchedule_Click);
            // 
            // dtpRepaymentDate
            // 
            this.dtpRepaymentDate.AccessibleDescription = null;
            this.dtpRepaymentDate.AccessibleName = null;
            resources.ApplyResources(this.dtpRepaymentDate, "dtpRepaymentDate");
            this.dtpRepaymentDate.BackgroundImage = null;
            this.dtpRepaymentDate.CalendarFont = null;
            this.dtpRepaymentDate.CustomFormat = null;
            this.dtpRepaymentDate.Font = null;
            this.dtpRepaymentDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpRepaymentDate.Name = "dtpRepaymentDate";
            this.dtpRepaymentDate.KeyUp += new System.Windows.Forms.KeyEventHandler(this._dateTimePicker_KeyUp);
            this.dtpRepaymentDate.CloseUp += new System.EventHandler(this._dateTimePicker_CloseUp);
            // 
            // groupBoxRepayDetails
            // 
            this.groupBoxRepayDetails.AccessibleDescription = null;
            this.groupBoxRepayDetails.AccessibleName = null;
            resources.ApplyResources(this.groupBoxRepayDetails, "groupBoxRepayDetails");
            this.groupBoxRepayDetails.Controls.Add(this._lbECPrincipal);
            this.groupBoxRepayDetails.Controls.Add(this._lbICPrincipal);
            this.groupBoxRepayDetails.Controls.Add(this.labelRepayPrincipal);
            this.groupBoxRepayDetails.Controls.Add(this.groupBox1);
            this.groupBoxRepayDetails.Controls.Add(this.groupBox2);
            this.groupBoxRepayDetails.Font = null;
            this.groupBoxRepayDetails.Name = "groupBoxRepayDetails";
            this.groupBoxRepayDetails.TabStop = false;
            // 
            // _lbECPrincipal
            // 
            this._lbECPrincipal.AccessibleDescription = null;
            this._lbECPrincipal.AccessibleName = null;
            resources.ApplyResources(this._lbECPrincipal, "_lbECPrincipal");
            this._lbECPrincipal.Font = null;
            this._lbECPrincipal.Name = "_lbECPrincipal";
            // 
            // _lbICPrincipal
            // 
            this._lbICPrincipal.AccessibleDescription = null;
            this._lbICPrincipal.AccessibleName = null;
            resources.ApplyResources(this._lbICPrincipal, "_lbICPrincipal");
            this._lbICPrincipal.Font = null;
            this._lbICPrincipal.Name = "_lbICPrincipal";
            // 
            // labelRepayPrincipal
            // 
            this.labelRepayPrincipal.AccessibleDescription = null;
            this.labelRepayPrincipal.AccessibleName = null;
            resources.ApplyResources(this.labelRepayPrincipal, "labelRepayPrincipal");
            this.labelRepayPrincipal.Font = null;
            this.labelRepayPrincipal.Name = "labelRepayPrincipal";
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = null;
            this.groupBox1.AccessibleName = null;
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.lbCInterest);
            this.groupBox1.Controls.Add(this.textBoxInterest);
            this.groupBox1.Controls.Add(this.lbInterest);
            this.groupBox1.Controls.Add(this.labelRepayInterest);
            this.groupBox1.Controls.Add(this.checkBoxInterests);
            this.groupBox1.Font = null;
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // lbCInterest
            // 
            this.lbCInterest.AccessibleDescription = null;
            this.lbCInterest.AccessibleName = null;
            resources.ApplyResources(this.lbCInterest, "lbCInterest");
            this.lbCInterest.Font = null;
            this.lbCInterest.Name = "lbCInterest";
            // 
            // textBoxInterest
            // 
            this.textBoxInterest.AccessibleDescription = null;
            this.textBoxInterest.AccessibleName = null;
            resources.ApplyResources(this.textBoxInterest, "textBoxInterest");
            this.textBoxInterest.BackgroundImage = null;
            this.textBoxInterest.Font = null;
            this.textBoxInterest.Name = "textBoxInterest";
            this.textBoxInterest.TextChanged += new System.EventHandler(this.textBoxInterests_TextChanged);
            this.textBoxInterest.Leave += new System.EventHandler(this.textBoxInterest_Leave);
            // 
            // lbInterest
            // 
            this.lbInterest.AccessibleDescription = null;
            this.lbInterest.AccessibleName = null;
            resources.ApplyResources(this.lbInterest, "lbInterest");
            this.lbInterest.Font = null;
            this.lbInterest.Name = "lbInterest";
            // 
            // labelRepayInterest
            // 
            this.labelRepayInterest.AccessibleDescription = null;
            this.labelRepayInterest.AccessibleName = null;
            resources.ApplyResources(this.labelRepayInterest, "labelRepayInterest");
            this.labelRepayInterest.Font = null;
            this.labelRepayInterest.Name = "labelRepayInterest";
            // 
            // checkBoxInterests
            // 
            this.checkBoxInterests.AccessibleDescription = null;
            this.checkBoxInterests.AccessibleName = null;
            resources.ApplyResources(this.checkBoxInterests, "checkBoxInterests");
            this.checkBoxInterests.Font = null;
            this.checkBoxInterests.Name = "checkBoxInterests";
            this.checkBoxInterests.CheckedChanged += new System.EventHandler(this.checkBoxInterests_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.AccessibleDescription = null;
            this.groupBox2.AccessibleName = null;
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this._lbECCommissions);
            this.groupBox2.Controls.Add(this._lbICCommisions);
            this.groupBox2.Controls.Add(this.textBoxCommission);
            this.groupBox2.Controls.Add(this._lbECFees);
            this.groupBox2.Controls.Add(this._lbICFees);
            this.groupBox2.Controls.Add(this.textBoxPenalties);
            this.groupBox2.Controls.Add(this.labelRepayCommissions);
            this.groupBox2.Controls.Add(this.checkBoxFees);
            this.groupBox2.Controls.Add(this.labelRepayFees);
            this.groupBox2.Font = null;
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // _lbECCommissions
            // 
            this._lbECCommissions.AccessibleDescription = null;
            this._lbECCommissions.AccessibleName = null;
            resources.ApplyResources(this._lbECCommissions, "_lbECCommissions");
            this._lbECCommissions.Font = null;
            this._lbECCommissions.Name = "_lbECCommissions";
            // 
            // _lbICCommisions
            // 
            this._lbICCommisions.AccessibleDescription = null;
            this._lbICCommisions.AccessibleName = null;
            resources.ApplyResources(this._lbICCommisions, "_lbICCommisions");
            this._lbICCommisions.Font = null;
            this._lbICCommisions.Name = "_lbICCommisions";
            // 
            // textBoxCommission
            // 
            this.textBoxCommission.AccessibleDescription = null;
            this.textBoxCommission.AccessibleName = null;
            resources.ApplyResources(this.textBoxCommission, "textBoxCommission");
            this.textBoxCommission.BackgroundImage = null;
            this.textBoxCommission.Font = null;
            this.textBoxCommission.Name = "textBoxCommission";
            this.textBoxCommission.TextChanged += new System.EventHandler(this.textBoxCommission_TextChanged);
            this.textBoxCommission.Leave += new System.EventHandler(this.textBoxInterest_Leave);
            // 
            // _lbECFees
            // 
            this._lbECFees.AccessibleDescription = null;
            this._lbECFees.AccessibleName = null;
            resources.ApplyResources(this._lbECFees, "_lbECFees");
            this._lbECFees.Font = null;
            this._lbECFees.Name = "_lbECFees";
            // 
            // _lbICFees
            // 
            this._lbICFees.AccessibleDescription = null;
            this._lbICFees.AccessibleName = null;
            resources.ApplyResources(this._lbICFees, "_lbICFees");
            this._lbICFees.Font = null;
            this._lbICFees.Name = "_lbICFees";
            // 
            // textBoxPenalties
            // 
            this.textBoxPenalties.AccessibleDescription = null;
            this.textBoxPenalties.AccessibleName = null;
            resources.ApplyResources(this.textBoxPenalties, "textBoxPenalties");
            this.textBoxPenalties.BackgroundImage = null;
            this.textBoxPenalties.Font = null;
            this.textBoxPenalties.Name = "textBoxPenalties";
            this.textBoxPenalties.TextChanged += new System.EventHandler(this.textBoxPenalties_TextChanged);
            this.textBoxPenalties.Leave += new System.EventHandler(this.textBoxInterest_Leave);
            // 
            // labelRepayCommissions
            // 
            this.labelRepayCommissions.AccessibleDescription = null;
            this.labelRepayCommissions.AccessibleName = null;
            resources.ApplyResources(this.labelRepayCommissions, "labelRepayCommissions");
            this.labelRepayCommissions.Font = null;
            this.labelRepayCommissions.Name = "labelRepayCommissions";
            // 
            // checkBoxFees
            // 
            this.checkBoxFees.AccessibleDescription = null;
            this.checkBoxFees.AccessibleName = null;
            resources.ApplyResources(this.checkBoxFees, "checkBoxFees");
            this.checkBoxFees.Font = null;
            this.checkBoxFees.Name = "checkBoxFees";
            this.checkBoxFees.CheckedChanged += new System.EventHandler(this.checkBoxFees_CheckedChanged);
            // 
            // labelRepayFees
            // 
            this.labelRepayFees.AccessibleDescription = null;
            this.labelRepayFees.AccessibleName = null;
            resources.ApplyResources(this.labelRepayFees, "labelRepayFees");
            this.labelRepayFees.Name = "labelRepayFees";
            this.labelRepayFees.MouseHover += new System.EventHandler(this.labelRepayFees_MouseHover);
            // 
            // _lbInstalmentNbValue
            // 
            this._lbInstalmentNbValue.AccessibleDescription = null;
            this._lbInstalmentNbValue.AccessibleName = null;
            resources.ApplyResources(this._lbInstalmentNbValue, "_lbInstalmentNbValue");
            this._lbInstalmentNbValue.Font = null;
            this._lbInstalmentNbValue.Name = "_lbInstalmentNbValue";
            // 
            // _lbLoanCodeValue
            // 
            this._lbLoanCodeValue.AccessibleDescription = null;
            this._lbLoanCodeValue.AccessibleName = null;
            resources.ApplyResources(this._lbLoanCodeValue, "_lbLoanCodeValue");
            this._lbLoanCodeValue.Font = null;
            this._lbLoanCodeValue.Name = "_lbLoanCodeValue";
            // 
            // _lbInstalmentNb
            // 
            this._lbInstalmentNb.AccessibleDescription = null;
            this._lbInstalmentNb.AccessibleName = null;
            resources.ApplyResources(this._lbInstalmentNb, "_lbInstalmentNb");
            this._lbInstalmentNb.Name = "_lbInstalmentNb";
            // 
            // labelPaymentMethod
            // 
            this.labelPaymentMethod.AccessibleDescription = null;
            this.labelPaymentMethod.AccessibleName = null;
            resources.ApplyResources(this.labelPaymentMethod, "labelPaymentMethod");
            this.labelPaymentMethod.BackColor = System.Drawing.Color.Transparent;
            this.labelPaymentMethod.Name = "labelPaymentMethod";
            // 
            // _lbDate
            // 
            this._lbDate.AccessibleDescription = null;
            this._lbDate.AccessibleName = null;
            resources.ApplyResources(this._lbDate, "_lbDate");
            this._lbDate.Name = "_lbDate";
            // 
            // labelCommentOptional
            // 
            this.labelCommentOptional.AccessibleDescription = null;
            this.labelCommentOptional.AccessibleName = null;
            resources.ApplyResources(this.labelCommentOptional, "labelCommentOptional");
            this.labelCommentOptional.BackColor = System.Drawing.Color.Transparent;
            this.labelCommentOptional.Name = "labelCommentOptional";
            // 
            // labelComment
            // 
            this.labelComment.AccessibleDescription = null;
            this.labelComment.AccessibleName = null;
            resources.ApplyResources(this.labelComment, "labelComment");
            this.labelComment.BackColor = System.Drawing.Color.Transparent;
            this.labelComment.Name = "labelComment";
            // 
            // _lbLoanCode
            // 
            this._lbLoanCode.AccessibleDescription = null;
            this._lbLoanCode.AccessibleName = null;
            resources.ApplyResources(this._lbLoanCode, "_lbLoanCode");
            this._lbLoanCode.Name = "_lbLoanCode";
            // 
            // CreditContractRepayForm
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreditContractRepayForm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBoxButton.ResumeLayout(false);
            this.groupBoxDetails.ResumeLayout(false);
            this.groupBoxDetails.PerformLayout();
            this.groupBoxAmount.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudICAmount)).EndInit();
            this.panelEC.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudECAmount)).EndInit();
            this.gbTypeOfRepayment.ResumeLayout(false);
            this.groupBoxRepayDetails.ResumeLayout(false);
            this.groupBoxRepayDetails.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private CheckBox checkBoxTotalAmount;
        private TextBox textBoxCommission;
        private ComboBox cmbPaymentMethod;
        private Label labelPaymentMethod;
        private TextBox textBoxComment;
        private Label labelCommentOptional;
        private Label labelComment;
        private CheckBox checkBoxPending;
        private Button buttonSelectAGroupPerson;
        private NumericUpDown nudICAmount;
        private NumericUpDown nudECAmount;
        private RadioButton rbProportionPayment;

    }
}
