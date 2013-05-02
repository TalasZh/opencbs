namespace OpenCBS.GUI.Contracts
{
    using OpenCBS.GUI.UserControl;

    partial class CloseSavingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CloseSavingsForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bCancel = new System.Windows.Forms.Button();
            this.bSave = new System.Windows.Forms.Button();
            this.gbActionWithAmount = new System.Windows.Forms.GroupBox();
            this.plTransfer = new System.Windows.Forms.Panel();
            this.tbTargetAccount = new System.Windows.Forms.TextBox();
            this.btSearchContract = new System.Windows.Forms.Button();
            this.lbTargetSavings = new System.Windows.Forms.Label();
            this.lbClientName = new System.Windows.Forms.Label();
            this.rbTransfer = new System.Windows.Forms.RadioButton();
            this.rbWithdraw = new System.Windows.Forms.RadioButton();
            this.tableLayoutAmount = new System.Windows.Forms.TableLayoutPanel();
            this.labelCloseFees = new System.Windows.Forms.Label();
            this.labelCloseFeesValue = new System.Windows.Forms.Label();
            this.lbTotalAmountValue = new System.Windows.Forms.Label();
            this.labelAmountOnAccount = new System.Windows.Forms.Label();
            this.gbCloseFees = new System.Windows.Forms.GroupBox();
            this.lbCloseFeesMinMax = new System.Windows.Forms.Label();
            this.udCloseFees = new System.Windows.Forms.NumericUpDown();
            this.checkBoxDesactivateFees = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbActionWithAmount.SuspendLayout();
            this.plTransfer.SuspendLayout();
            this.tableLayoutAmount.SuspendLayout();
            this.gbCloseFees.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udCloseFees)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.gbActionWithAmount, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutAmount, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.gbCloseFees, 0, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bCancel);
            this.groupBox1.Controls.Add(this.bSave);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // bCancel
            // 
            resources.ApplyResources(this.bCancel, "bCancel");
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.Name = "bCancel";
            // 
            // bSave
            // 
            resources.ApplyResources(this.bSave, "bSave");
            this.bSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bSave.Name = "bSave";
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // gbActionWithAmount
            // 
            this.gbActionWithAmount.Controls.Add(this.plTransfer);
            this.gbActionWithAmount.Controls.Add(this.rbTransfer);
            this.gbActionWithAmount.Controls.Add(this.rbWithdraw);
            resources.ApplyResources(this.gbActionWithAmount, "gbActionWithAmount");
            this.gbActionWithAmount.Name = "gbActionWithAmount";
            this.gbActionWithAmount.TabStop = false;
            // 
            // plTransfer
            // 
            this.plTransfer.Controls.Add(this.tbTargetAccount);
            this.plTransfer.Controls.Add(this.btSearchContract);
            this.plTransfer.Controls.Add(this.lbTargetSavings);
            this.plTransfer.Controls.Add(this.lbClientName);
            resources.ApplyResources(this.plTransfer, "plTransfer");
            this.plTransfer.Name = "plTransfer";
            // 
            // tbTargetAccount
            // 
            resources.ApplyResources(this.tbTargetAccount, "tbTargetAccount");
            this.tbTargetAccount.Name = "tbTargetAccount";
            // 
            // btSearchContract
            //
            resources.ApplyResources(this.btSearchContract, "btSearchContract");
            this.btSearchContract.Name = "btSearchContract";
            this.btSearchContract.Click += new System.EventHandler(this.btSearchContract_Click);
            // 
            // lbTargetSavings
            //
            resources.ApplyResources(this.lbTargetSavings, "lbTargetSavings");
            this.lbTargetSavings.Name = "lbTargetSavings";
            // 
            // lbClientName
            //
            resources.ApplyResources(this.lbClientName, "lbClientName");
            this.lbClientName.Name = "lbClientName";
            // 
            // rbTransfer
            // 
            resources.ApplyResources(this.rbTransfer, "rbTransfer");
            this.rbTransfer.Name = "rbTransfer";
            this.rbTransfer.CheckedChanged += new System.EventHandler(this.rbTransfer_CheckedChanged);
            // 
            // rbWithdraw
            // 
            resources.ApplyResources(this.rbWithdraw, "rbWithdraw");
            this.rbWithdraw.Checked = true;
            this.rbWithdraw.Name = "rbWithdraw";
            this.rbWithdraw.TabStop = true;
            // 
            // tableLayoutAmount
            // 
            this.tableLayoutAmount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            resources.ApplyResources(this.tableLayoutAmount, "tableLayoutAmount");
            this.tableLayoutAmount.Controls.Add(this.labelCloseFees, 0, 1);
            this.tableLayoutAmount.Controls.Add(this.labelCloseFeesValue, 1, 1);
            this.tableLayoutAmount.Controls.Add(this.lbTotalAmountValue, 1, 0);
            this.tableLayoutAmount.Controls.Add(this.labelAmountOnAccount, 0, 0);
            this.tableLayoutAmount.Name = "tableLayoutAmount";
            // 
            // labelCloseFees
            // 
            resources.ApplyResources(this.labelCloseFees, "labelCloseFees");
            this.labelCloseFees.Name = "labelCloseFees";
            // 
            // labelCloseFeesValue
            // 
            resources.ApplyResources(this.labelCloseFeesValue, "labelCloseFeesValue");
            this.labelCloseFeesValue.Name = "labelCloseFeesValue";
            // 
            // lbTotalAmountValue
            // 
            resources.ApplyResources(this.lbTotalAmountValue, "lbTotalAmountValue");
            this.lbTotalAmountValue.Name = "lbTotalAmountValue";
            // 
            // labelAmountOnAccount
            // 
            resources.ApplyResources(this.labelAmountOnAccount, "labelAmountOnAccount");
            this.labelAmountOnAccount.Name = "labelAmountOnAccount";
            // 
            // gbCloseFees
            // 
            this.gbCloseFees.Controls.Add(this.lbCloseFeesMinMax);
            this.gbCloseFees.Controls.Add(this.udCloseFees);
            this.gbCloseFees.Controls.Add(this.checkBoxDesactivateFees);
            resources.ApplyResources(this.gbCloseFees, "gbCloseFees");
            this.gbCloseFees.Name = "gbCloseFees";
            this.gbCloseFees.TabStop = false;
            // 
            // lbCloseFeesMinMax
            //
            resources.ApplyResources(this.lbCloseFeesMinMax, "lbCloseFeesMinMax");
            this.lbCloseFeesMinMax.Name = "lbCloseFeesMinMax";
            // 
            // udCloseFees
            // 
            resources.ApplyResources(this.udCloseFees, "udCloseFees");
            this.udCloseFees.Name = "udCloseFees";
            this.udCloseFees.ValueChanged += new System.EventHandler(this.udCloseFees_ValueChanged);
            // 
            // checkBoxDesactivateFees
            // 
            resources.ApplyResources(this.checkBoxDesactivateFees, "checkBoxDesactivateFees");
            this.checkBoxDesactivateFees.Name = "checkBoxDesactivateFees";
            this.checkBoxDesactivateFees.CheckedChanged += new System.EventHandler(this.checkBoxDesactivateFees_CheckedChanged);
            // 
            // CloseSavingsForm
            // 
            this.AcceptButton = this.bSave;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bCancel;
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CloseSavingsForm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.gbActionWithAmount.ResumeLayout(false);
            this.gbActionWithAmount.PerformLayout();
            this.plTransfer.ResumeLayout(false);
            this.plTransfer.PerformLayout();
            this.tableLayoutAmount.ResumeLayout(false);
            this.tableLayoutAmount.PerformLayout();
            this.gbCloseFees.ResumeLayout(false);
            this.gbCloseFees.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udCloseFees)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Button bSave;
        private System.Windows.Forms.GroupBox gbActionWithAmount;
        private System.Windows.Forms.TableLayoutPanel tableLayoutAmount;
        private System.Windows.Forms.Label labelAmountOnAccount;
        private System.Windows.Forms.Label lbTotalAmountValue;
        private System.Windows.Forms.RadioButton rbWithdraw;
        private System.Windows.Forms.RadioButton rbTransfer;
        private System.Windows.Forms.Panel plTransfer;
        private System.Windows.Forms.TextBox tbTargetAccount;
        private System.Windows.Forms.Label lbTargetSavings;
        private System.Windows.Forms.Label lbClientName;
        private System.Windows.Forms.Label labelCloseFees;
        private System.Windows.Forms.Label labelCloseFeesValue;
        private System.Windows.Forms.CheckBox checkBoxDesactivateFees;
        private System.Windows.Forms.Button btSearchContract;
        private System.Windows.Forms.GroupBox gbCloseFees;
        private System.Windows.Forms.NumericUpDown udCloseFees;
        private System.Windows.Forms.Label lbCloseFeesMinMax;
    }
}