namespace OpenCBS.GUI.Contracts
{
    partial class AddTrancheForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddTrancheForm));
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
            this.groupBoxParameters = new System.Windows.Forms.GroupBox();
            this.cbApplynewInterestforOLB = new System.Windows.Forms.CheckBox();
            this.lbNewInterest = new System.Windows.Forms.Label();
            this.dateTimePickerStartDate = new System.Windows.Forms.DateTimePicker();
            this.labelStartDate = new System.Windows.Forms.Label();
            this.labelShiftDateDays = new System.Windows.Forms.Label();
            this.tbDateOffset = new System.Windows.Forms.TextBox();
            this.labelShiftDate = new System.Windows.Forms.Label();
            this.labelMaturityUnity = new System.Windows.Forms.Label();
            this.numericUpDownNewIR = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMaturity = new System.Windows.Forms.NumericUpDown();
            this.labelMaturity = new System.Windows.Forms.Label();
            this.labelContractCode = new System.Windows.Forms.Label();
            this.labelTitleRescheduleContract = new System.Windows.Forms.Label();
            this.groupBoxConfirm = new System.Windows.Forms.GroupBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonConfirm = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxParameters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNewIR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaturity)).BeginInit();
            this.groupBoxConfirm.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewRepayments
            // 
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
            resources.ApplyResources(this.listViewRepayments, "listViewRepayments");
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
            // groupBoxParameters
            //
            resources.ApplyResources(this.groupBoxParameters, "groupBoxParameters");
            this.groupBoxParameters.Controls.Add(this.cbApplynewInterestforOLB);
            this.groupBoxParameters.Controls.Add(this.lbNewInterest);
            this.groupBoxParameters.Controls.Add(this.dateTimePickerStartDate);
            this.groupBoxParameters.Controls.Add(this.labelStartDate);
            this.groupBoxParameters.Controls.Add(this.labelShiftDateDays);
            this.groupBoxParameters.Controls.Add(this.tbDateOffset);
            this.groupBoxParameters.Controls.Add(this.labelShiftDate);
            this.groupBoxParameters.Controls.Add(this.labelMaturityUnity);
            this.groupBoxParameters.Controls.Add(this.numericUpDownNewIR);
            this.groupBoxParameters.Controls.Add(this.numericUpDownMaturity);
            this.groupBoxParameters.Controls.Add(this.labelMaturity);
            this.groupBoxParameters.Controls.Add(this.labelContractCode);
            this.groupBoxParameters.Controls.Add(this.labelTitleRescheduleContract);
            this.groupBoxParameters.Name = "groupBoxParameters";
            this.groupBoxParameters.TabStop = false;
            // 
            // cbApplynewInterestforOLB
            // 
            resources.ApplyResources(this.cbApplynewInterestforOLB, "cbApplynewInterestforOLB");
            this.cbApplynewInterestforOLB.Name = "cbApplynewInterestforOLB";
            this.cbApplynewInterestforOLB.CheckedChanged += new System.EventHandler(this.cbApplynewInterestforOLB_CheckedChanged);
            // 
            // lbNewInterest
            // 
            resources.ApplyResources(this.lbNewInterest, "lbNewInterest");
            this.lbNewInterest.Name = "lbNewInterest";
            // 
            // dateTimePickerStartDate
            // 
            this.dateTimePickerStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            resources.ApplyResources(this.dateTimePickerStartDate, "dateTimePickerStartDate");
            this.dateTimePickerStartDate.Name = "dateTimePickerStartDate";
            this.dateTimePickerStartDate.ValueChanged += new System.EventHandler(this.dateTimePickerStartDate_ValueChanged);
            // 
            // labelStartDate
            // 
            resources.ApplyResources(this.labelStartDate, "labelStartDate");
            this.labelStartDate.Name = "labelStartDate";
            // 
            // labelShiftDateDays
            // 
            resources.ApplyResources(this.labelShiftDateDays, "labelShiftDateDays");
            this.labelShiftDateDays.Name = "labelShiftDateDays";
            // 
            // tbDateOffset
            // 
            resources.ApplyResources(this.tbDateOffset, "tbDateOffset");
            this.tbDateOffset.Name = "tbDateOffset";
            this.tbDateOffset.TextChanged += new System.EventHandler(this.tbDateOffset_TextChanged);
            this.tbDateOffset.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbDateOffset_KeyDown);
            this.tbDateOffset.Enter += new System.EventHandler(this.tbDateOffset_Enter);
            // 
            // labelShiftDate
            // 
            resources.ApplyResources(this.labelShiftDate, "labelShiftDate");
            this.labelShiftDate.Name = "labelShiftDate";
            // 
            // labelMaturityUnity
            // 
            resources.ApplyResources(this.labelMaturityUnity, "labelMaturityUnity");
            this.labelMaturityUnity.Name = "labelMaturityUnity";
            // 
            // numericUpDownNewIR
            // 
            this.numericUpDownNewIR.DecimalPlaces = 2;
            this.numericUpDownNewIR.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            resources.ApplyResources(this.numericUpDownNewIR, "numericUpDownNewIR");
            this.numericUpDownNewIR.Name = "numericUpDownNewIR";
            this.numericUpDownNewIR.ValueChanged += new System.EventHandler(this.numericUpDownNewIR_ValueChanged);
            // 
            // numericUpDownMaturity
            // 
            resources.ApplyResources(this.numericUpDownMaturity, "numericUpDownMaturity");
            this.numericUpDownMaturity.Name = "numericUpDownMaturity";
            this.numericUpDownMaturity.ValueChanged += new System.EventHandler(this.numericUpDownMaturity_ValueChanged);
            // 
            // labelMaturity
            // 
            resources.ApplyResources(this.labelMaturity, "labelMaturity");
            this.labelMaturity.Name = "labelMaturity";
            // 
            // labelContractCode
            // 
            resources.ApplyResources(this.labelContractCode, "labelContractCode");
            this.labelContractCode.Name = "labelContractCode";
            // 
            // labelTitleRescheduleContract
            // 
            resources.ApplyResources(this.labelTitleRescheduleContract, "labelTitleRescheduleContract");
            this.labelTitleRescheduleContract.Name = "labelTitleRescheduleContract";
            // 
            // groupBoxConfirm
            //
            resources.ApplyResources(this.groupBoxConfirm, "groupBoxConfirm");
            this.groupBoxConfirm.Controls.Add(this.buttonCancel);
            this.groupBoxConfirm.Controls.Add(this.buttonConfirm);
            this.groupBoxConfirm.Name = "groupBoxConfirm";
            this.groupBoxConfirm.TabStop = false;
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonCancel.Image = global::OpenCBS.GUI.Properties.Resources.theme1_1_bouton_close;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonConfirm
            // 
            resources.ApplyResources(this.buttonConfirm, "buttonConfirm");
            this.buttonConfirm.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonConfirm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonConfirm.Image = global::OpenCBS.GUI.Properties.Resources.theme1_1_bouton_validity;
            this.buttonConfirm.Name = "buttonConfirm";
            this.buttonConfirm.UseVisualStyleBackColor = false;
            this.buttonConfirm.Click += new System.EventHandler(this.buttonConfirm_Click);
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.groupBoxConfirm, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.listViewRepayments, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxParameters, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // AddTrancheForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "AddTrancheForm";
            this.Load += new System.EventHandler(this.AddTrancheForm_Load);
            this.groupBoxParameters.ResumeLayout(false);
            this.groupBoxParameters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNewIR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaturity)).EndInit();
            this.groupBoxConfirm.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewRepayments;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.GroupBox groupBoxParameters;
        private System.Windows.Forms.CheckBox cbApplynewInterestforOLB;
        private System.Windows.Forms.Label lbNewInterest;
        private System.Windows.Forms.DateTimePicker dateTimePickerStartDate;
        private System.Windows.Forms.Label labelStartDate;
        private System.Windows.Forms.Label labelShiftDateDays;
        private System.Windows.Forms.TextBox tbDateOffset;
        private System.Windows.Forms.Label labelShiftDate;
        private System.Windows.Forms.Label labelMaturityUnity;
        private System.Windows.Forms.NumericUpDown numericUpDownNewIR;
        private System.Windows.Forms.NumericUpDown numericUpDownMaturity;
        private System.Windows.Forms.Label labelMaturity;
        private System.Windows.Forms.Label labelContractCode;
        private System.Windows.Forms.Label labelTitleRescheduleContract;
        private System.Windows.Forms.GroupBox groupBoxConfirm;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonConfirm;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}