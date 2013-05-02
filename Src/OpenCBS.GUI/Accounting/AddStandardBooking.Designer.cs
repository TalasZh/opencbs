using OpenCBS.GUI.UserControl;

namespace OpenCBS.GUI.Accounting
{
    partial class AddStandardBooking
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddStandardBooking));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxData = new System.Windows.Forms.GroupBox();
            this.comboBoxCredit = new System.Windows.Forms.ComboBox();
            this.comboBoxDebit = new System.Windows.Forms.ComboBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelCredit = new System.Windows.Forms.Label();
            this.labelDebit = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.groupBoxAction = new System.Windows.Forms.GroupBox();
            this.btSaving = new System.Windows.Forms.Button();
            this.bClose = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBoxData.SuspendLayout();
            this.groupBoxAction.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.groupBoxData, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxAction, 0, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // groupBoxData
            // 
            this.groupBoxData.Controls.Add(this.comboBoxCredit);
            this.groupBoxData.Controls.Add(this.comboBoxDebit);
            this.groupBoxData.Controls.Add(this.textBoxName);
            this.groupBoxData.Controls.Add(this.labelCredit);
            this.groupBoxData.Controls.Add(this.labelDebit);
            this.groupBoxData.Controls.Add(this.labelName);
            resources.ApplyResources(this.groupBoxData, "groupBoxData");
            this.groupBoxData.Name = "groupBoxData";
            this.groupBoxData.TabStop = false;
            // 
            // comboBoxCredit
            // 
            resources.ApplyResources(this.comboBoxCredit, "comboBoxCredit");
            this.comboBoxCredit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCredit.FormattingEnabled = true;
            this.comboBoxCredit.Name = "comboBoxCredit";
            // 
            // comboBoxDebit
            // 
            resources.ApplyResources(this.comboBoxDebit, "comboBoxDebit");
            this.comboBoxDebit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDebit.FormattingEnabled = true;
            this.comboBoxDebit.Name = "comboBoxDebit";
            // 
            // textBoxName
            // 
            resources.ApplyResources(this.textBoxName, "textBoxName");
            this.textBoxName.Name = "textBoxName";
            // 
            // labelCredit
            // 
            resources.ApplyResources(this.labelCredit, "labelCredit");
            this.labelCredit.Name = "labelCredit";
            // 
            // labelDebit
            // 
            resources.ApplyResources(this.labelDebit, "labelDebit");
            this.labelDebit.Name = "labelDebit";
            // 
            // labelName
            // 
            resources.ApplyResources(this.labelName, "labelName");
            this.labelName.Name = "labelName";
            // 
            // groupBoxAction
            // 
            this.groupBoxAction.Controls.Add(this.btSaving);
            this.groupBoxAction.Controls.Add(this.bClose);
            resources.ApplyResources(this.groupBoxAction, "groupBoxAction");
            this.groupBoxAction.Name = "groupBoxAction";
            this.groupBoxAction.TabStop = false;
            // 
            // btSaving
            // 
            resources.ApplyResources(this.btSaving, "btSaving");
            this.btSaving.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btSaving.Name = "btSaving";
            // 
            // bClose
            // 
            resources.ApplyResources(this.bClose, "bClose");
            this.bClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bClose.Name = "bClose";
            // 
            // AddStandardBooking
            // 
            this.AcceptButton = this.btSaving;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bClose;
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AddStandardBooking";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBoxData.ResumeLayout(false);
            this.groupBoxData.PerformLayout();
            this.groupBoxAction.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBoxData;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.GroupBox groupBoxAction;
        private System.Windows.Forms.Button btSaving;
        private System.Windows.Forms.Button bClose;
        private System.Windows.Forms.ComboBox comboBoxCredit;
        private System.Windows.Forms.ComboBox comboBoxDebit;
        private System.Windows.Forms.Label labelCredit;
        private System.Windows.Forms.Label labelDebit;
    }
}