using OpenCBS.GUI.UserControl;

namespace OpenCBS.GUI.Contracts
{
    partial class OpenSavingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OpenSavingsForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bCancel = new System.Windows.Forms.Button();
            this.bSave = new System.Windows.Forms.Button();
            this.gbModifyInitialAmountEntryFees = new System.Windows.Forms.GroupBox();
            this.nudInitialAmount = new System.Windows.Forms.NumericUpDown();
            this.udEntryFees = new System.Windows.Forms.NumericUpDown();
            this.lbInitialAmountMinMax = new System.Windows.Forms.Label();
            this.lbEntryFeesMinMax = new System.Windows.Forms.Label();
            this.lbEntryFees = new System.Windows.Forms.Label();
            this.lbInitialAmount = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lbConfirmationTotalAmount = new System.Windows.Forms.Label();
            this.lbInitialAmountConfirmation = new System.Windows.Forms.Label();
            this.lbEntryFeesConfirmation = new System.Windows.Forms.Label();
            this.lbTotalAmountValue = new System.Windows.Forms.Label();
            this.lbInitialAmountValue = new System.Windows.Forms.Label();
            this.lbEntryFeesValue = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbModifyInitialAmountEntryFees.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudInitialAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udEntryFees)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.gbModifyInitialAmountEntryFees, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
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
            // gbModifyInitialAmountEntryFees
            // 
            this.gbModifyInitialAmountEntryFees.Controls.Add(this.nudInitialAmount);
            this.gbModifyInitialAmountEntryFees.Controls.Add(this.udEntryFees);
            this.gbModifyInitialAmountEntryFees.Controls.Add(this.lbInitialAmountMinMax);
            this.gbModifyInitialAmountEntryFees.Controls.Add(this.lbEntryFeesMinMax);
            this.gbModifyInitialAmountEntryFees.Controls.Add(this.lbEntryFees);
            this.gbModifyInitialAmountEntryFees.Controls.Add(this.lbInitialAmount);
            resources.ApplyResources(this.gbModifyInitialAmountEntryFees, "gbModifyInitialAmountEntryFees");
            this.gbModifyInitialAmountEntryFees.Name = "gbModifyInitialAmountEntryFees";
            this.gbModifyInitialAmountEntryFees.TabStop = false;
            // 
            // nudInitialAmount
            // 
            resources.ApplyResources(this.nudInitialAmount, "nudInitialAmount");
            this.nudInitialAmount.Name = "nudInitialAmount";
            this.nudInitialAmount.ValueChanged += new System.EventHandler(this.nudInitialAmount_ValueChanged);
            // 
            // udEntryFees
            // 
            resources.ApplyResources(this.udEntryFees, "udEntryFees");
            this.udEntryFees.Name = "udEntryFees";
            this.udEntryFees.ValueChanged += new System.EventHandler(this.udEntryFees_ValueChanged);
            // 
            // lbInitialAmountMinMax
            //
            resources.ApplyResources(this.lbInitialAmountMinMax, "lbInitialAmountMinMax");
            this.lbInitialAmountMinMax.Name = "lbInitialAmountMinMax";
            // 
            // lbEntryFeesMinMax
            //
            resources.ApplyResources(this.lbEntryFeesMinMax, "lbEntryFeesMinMax");
            this.lbEntryFeesMinMax.Name = "lbEntryFeesMinMax";
            // 
            // lbEntryFees
            // 
            resources.ApplyResources(this.lbEntryFees, "lbEntryFees");
            this.lbEntryFees.Name = "lbEntryFees";
            // 
            // lbInitialAmount
            // 
            resources.ApplyResources(this.lbInitialAmount, "lbInitialAmount");
            this.lbInitialAmount.Name = "lbInitialAmount";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AllowDrop = true;
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.lbConfirmationTotalAmount, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lbInitialAmountConfirmation, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.lbEntryFeesConfirmation, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.lbTotalAmountValue, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.lbInitialAmountValue, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.lbEntryFeesValue, 2, 2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // lbConfirmationTotalAmount
            // 
            resources.ApplyResources(this.lbConfirmationTotalAmount, "lbConfirmationTotalAmount");
            this.tableLayoutPanel2.SetColumnSpan(this.lbConfirmationTotalAmount, 2);
            this.lbConfirmationTotalAmount.Name = "lbConfirmationTotalAmount";
            // 
            // lbInitialAmountConfirmation
            // 
            resources.ApplyResources(this.lbInitialAmountConfirmation, "lbInitialAmountConfirmation");
            this.lbInitialAmountConfirmation.Name = "lbInitialAmountConfirmation";
            // 
            // lbEntryFeesConfirmation
            // 
            resources.ApplyResources(this.lbEntryFeesConfirmation, "lbEntryFeesConfirmation");
            this.lbEntryFeesConfirmation.Name = "lbEntryFeesConfirmation";
            // 
            // lbTotalAmountValue
            // 
            resources.ApplyResources(this.lbTotalAmountValue, "lbTotalAmountValue");
            this.lbTotalAmountValue.Name = "lbTotalAmountValue";
            // 
            // lbInitialAmountValue
            // 
            resources.ApplyResources(this.lbInitialAmountValue, "lbInitialAmountValue");
            this.lbInitialAmountValue.Name = "lbInitialAmountValue";
            // 
            // lbEntryFeesValue
            // 
            resources.ApplyResources(this.lbEntryFeesValue, "lbEntryFeesValue");
            this.lbEntryFeesValue.Name = "lbEntryFeesValue";
            // 
            // OpenSavingsForm
            // 
            this.AcceptButton = this.bSave;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bCancel;
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OpenSavingsForm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.gbModifyInitialAmountEntryFees.ResumeLayout(false);
            this.gbModifyInitialAmountEntryFees.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudInitialAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udEntryFees)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Button bSave;
        private System.Windows.Forms.GroupBox gbModifyInitialAmountEntryFees;
        private System.Windows.Forms.Label lbEntryFeesMinMax;
        private System.Windows.Forms.Label lbEntryFees;
        private System.Windows.Forms.Label lbInitialAmount;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lbTotalAmountValue;
        private System.Windows.Forms.Label lbEntryFeesValue;
        private System.Windows.Forms.Label lbInitialAmountValue;
        private System.Windows.Forms.Label lbInitialAmountConfirmation;
        private System.Windows.Forms.Label lbEntryFeesConfirmation;
        private System.Windows.Forms.Label lbConfirmationTotalAmount;
        private System.Windows.Forms.Label lbInitialAmountMinMax;
        private System.Windows.Forms.NumericUpDown udEntryFees;
        private System.Windows.Forms.NumericUpDown nudInitialAmount;
    }
}