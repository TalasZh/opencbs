using Octopus.GUI.UserControl;

namespace Octopus.GUI
{
    partial class FrmInstallmentTypes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInstallmentTypes));
            this.listViewInstallmentTypes = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelName = new System.Windows.Forms.Label();
            this.labelNbDays = new System.Windows.Forms.Label();
            this.labelNbMonths = new System.Windows.Forms.Label();
            this.numericUpDownMonths = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownDays = new System.Windows.Forms.NumericUpDown();
            this.buttonAdd = new Octopus.GUI.UserControl.SweetButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonDelete = new Octopus.GUI.UserControl.SweetButton();
            this.buttonEdit = new Octopus.GUI.UserControl.SweetButton();
            this.buttonExit = new Octopus.GUI.UserControl.SweetButton();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMonths)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDays)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewInstallmentTypes
            // 
            this.listViewInstallmentTypes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            resources.ApplyResources(this.listViewInstallmentTypes, "listViewInstallmentTypes");
            this.listViewInstallmentTypes.FullRowSelect = true;
            this.listViewInstallmentTypes.GridLines = true;
            this.listViewInstallmentTypes.MultiSelect = false;
            this.listViewInstallmentTypes.Name = "listViewInstallmentTypes";
            this.listViewInstallmentTypes.UseCompatibleStateImageBehavior = false;
            this.listViewInstallmentTypes.View = System.Windows.Forms.View.Details;
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
            // textBoxName
            // 
            resources.ApplyResources(this.textBoxName, "textBoxName");
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // labelName
            // 
            resources.ApplyResources(this.labelName, "labelName");
            this.labelName.BackColor = System.Drawing.Color.Transparent;
            this.labelName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.labelName.Name = "labelName";
            // 
            // labelNbDays
            // 
            resources.ApplyResources(this.labelNbDays, "labelNbDays");
            this.labelNbDays.BackColor = System.Drawing.Color.Transparent;
            this.labelNbDays.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.labelNbDays.Name = "labelNbDays";
            // 
            // labelNbMonths
            // 
            resources.ApplyResources(this.labelNbMonths, "labelNbMonths");
            this.labelNbMonths.BackColor = System.Drawing.Color.Transparent;
            this.labelNbMonths.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.labelNbMonths.Name = "labelNbMonths";
            // 
            // numericUpDownMonths
            // 
            resources.ApplyResources(this.numericUpDownMonths, "numericUpDownMonths");
            this.numericUpDownMonths.Name = "numericUpDownMonths";
            this.numericUpDownMonths.ValueChanged += new System.EventHandler(this.numericUpDownMonths_ValueChanged);
            // 
            // numericUpDownDays
            // 
            resources.ApplyResources(this.numericUpDownDays, "numericUpDownDays");
            this.numericUpDownDays.Name = "numericUpDownDays";
            this.numericUpDownDays.ValueChanged += new System.EventHandler(this.numericUpDownDays_ValueChanged);
            // 
            // buttonAdd
            // 
            this.buttonAdd.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.buttonAdd, "buttonAdd");
            this.buttonAdd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonAdd.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.New;
            this.buttonAdd.Menu = null;
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.UseVisualStyleBackColor = false;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonDelete);
            this.groupBox1.Controls.Add(this.buttonEdit);
            this.groupBox1.Controls.Add(this.labelName);
            this.groupBox1.Controls.Add(this.buttonAdd);
            this.groupBox1.Controls.Add(this.numericUpDownDays);
            this.groupBox1.Controls.Add(this.labelNbDays);
            this.groupBox1.Controls.Add(this.textBoxName);
            this.groupBox1.Controls.Add(this.numericUpDownMonths);
            this.groupBox1.Controls.Add(this.labelNbMonths);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(56)))));
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // buttonDelete
            // 
            this.buttonDelete.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.buttonDelete, "buttonDelete");
            this.buttonDelete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonDelete.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Delete;
            this.buttonDelete.Menu = null;
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.UseVisualStyleBackColor = false;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonEdit
            // 
            this.buttonEdit.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.buttonEdit, "buttonEdit");
            this.buttonEdit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonEdit.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Edit;
            this.buttonEdit.Menu = null;
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.UseVisualStyleBackColor = false;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.buttonExit, "buttonExit");
            this.buttonExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonExit.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Close;
            this.buttonExit.Menu = null;
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // FrmInstallmentTypes
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.listViewInstallmentTypes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmInstallmentTypes";
            this.Load += new System.EventHandler(this.FrmProvinces_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMonths)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDays)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private SweetButton buttonAdd;
        private System.Windows.Forms.ListView listViewInstallmentTypes;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Label labelNbDays;
        private System.Windows.Forms.Label labelNbMonths;
        private System.Windows.Forms.NumericUpDown numericUpDownMonths;
        private System.Windows.Forms.NumericUpDown numericUpDownDays;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.GroupBox groupBox1;
        private SweetButton buttonDelete;
        private SweetButton buttonEdit;
        private SweetButton buttonExit;
    }
}