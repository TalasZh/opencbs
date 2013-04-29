namespace Octopus.GUI
{
    partial class ProjectFollowUp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectFollowUp));
            this.tBComment = new System.Windows.Forms.TextBox();
            this.tBCA = new System.Windows.Forms.TextBox();
            this.lProjectYear = new System.Windows.Forms.Label();
            this.lProjectJobs1 = new System.Windows.Forms.Label();
            this.lProjectJobs2 = new System.Windows.Forms.Label();
            this.lProjectFollowUpCA = new System.Windows.Forms.Label();
            this.lProjectPersonalSituation = new System.Windows.Forms.Label();
            this.lProjectActivity = new System.Windows.Forms.Label();
            this.lProjectComment = new System.Windows.Forms.Label();
            this.buttonCancel = new Octopus.GUI.UserControl.SweetButton();
            this.buttonSave = new Octopus.GUI.UserControl.SweetButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBoxActivity = new System.Windows.Forms.ComboBox();
            this.comboBoxPersonalSituation = new System.Windows.Forms.ComboBox();
            this.numericUpDownJobs1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownJobs2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownYear = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownJobs1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownJobs2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownYear)).BeginInit();
            this.SuspendLayout();
            // 
            // tBComment
            // 
            this.tBComment.AcceptsReturn = true;
            this.tBComment.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.tBComment, "tBComment");
            this.tBComment.Name = "tBComment";
            // 
            // tBCA
            // 
            this.tBCA.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.tBCA, "tBCA");
            this.tBCA.Name = "tBCA";
            // 
            // lProjectYear
            // 
            resources.ApplyResources(this.lProjectYear, "lProjectYear");
            this.lProjectYear.BackColor = System.Drawing.Color.Transparent;
            this.lProjectYear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.lProjectYear.Name = "lProjectYear";
            // 
            // lProjectJobs1
            // 
            resources.ApplyResources(this.lProjectJobs1, "lProjectJobs1");
            this.lProjectJobs1.BackColor = System.Drawing.Color.Transparent;
            this.lProjectJobs1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.lProjectJobs1.Name = "lProjectJobs1";
            // 
            // lProjectJobs2
            // 
            resources.ApplyResources(this.lProjectJobs2, "lProjectJobs2");
            this.lProjectJobs2.BackColor = System.Drawing.Color.Transparent;
            this.lProjectJobs2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.lProjectJobs2.Name = "lProjectJobs2";
            // 
            // lProjectFollowUpCA
            // 
            resources.ApplyResources(this.lProjectFollowUpCA, "lProjectFollowUpCA");
            this.lProjectFollowUpCA.BackColor = System.Drawing.Color.Transparent;
            this.lProjectFollowUpCA.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.lProjectFollowUpCA.Name = "lProjectFollowUpCA";
            // 
            // lProjectPersonalSituation
            // 
            resources.ApplyResources(this.lProjectPersonalSituation, "lProjectPersonalSituation");
            this.lProjectPersonalSituation.BackColor = System.Drawing.Color.Transparent;
            this.lProjectPersonalSituation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.lProjectPersonalSituation.Name = "lProjectPersonalSituation";
            // 
            // lProjectActivity
            // 
            resources.ApplyResources(this.lProjectActivity, "lProjectActivity");
            this.lProjectActivity.BackColor = System.Drawing.Color.Transparent;
            this.lProjectActivity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.lProjectActivity.Name = "lProjectActivity";
            // 
            // lProjectComment
            // 
            resources.ApplyResources(this.lProjectComment, "lProjectComment");
            this.lProjectComment.BackColor = System.Drawing.Color.Transparent;
            this.lProjectComment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.lProjectComment.Name = "lProjectComment";
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonCancel.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Close;
            this.buttonCancel.Image = global::Octopus.GUI.Properties.Resources.theme1_1_bouton_close;
            this.buttonCancel.Menu = null;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            // 
            resources.ApplyResources(this.buttonSave, "buttonSave");
            this.buttonSave.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonSave.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Save;
            this.buttonSave.Image = global::Octopus.GUI.Properties.Resources.theme1_1_bouton_save;
            this.buttonSave.Menu = null;
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.comboBoxActivity);
            this.groupBox1.Controls.Add(this.comboBoxPersonalSituation);
            this.groupBox1.Controls.Add(this.numericUpDownJobs1);
            this.groupBox1.Controls.Add(this.numericUpDownJobs2);
            this.groupBox1.Controls.Add(this.numericUpDownYear);
            this.groupBox1.Controls.Add(this.lProjectYear);
            this.groupBox1.Controls.Add(this.buttonSave);
            this.groupBox1.Controls.Add(this.lProjectComment);
            this.groupBox1.Controls.Add(this.buttonCancel);
            this.groupBox1.Controls.Add(this.lProjectActivity);
            this.groupBox1.Controls.Add(this.tBComment);
            this.groupBox1.Controls.Add(this.lProjectPersonalSituation);
            this.groupBox1.Controls.Add(this.lProjectFollowUpCA);
            this.groupBox1.Controls.Add(this.lProjectJobs2);
            this.groupBox1.Controls.Add(this.tBCA);
            this.groupBox1.Controls.Add(this.lProjectJobs1);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // comboBoxActivity
            // 
            this.comboBoxActivity.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxActivity, "comboBoxActivity");
            this.comboBoxActivity.Name = "comboBoxActivity";
            this.comboBoxActivity.DropDown += new System.EventHandler(this.comboBoxActivity_DropDown);
            // 
            // comboBoxPersonalSituation
            // 
            this.comboBoxPersonalSituation.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxPersonalSituation, "comboBoxPersonalSituation");
            this.comboBoxPersonalSituation.Name = "comboBoxPersonalSituation";
            this.comboBoxPersonalSituation.DropDown += new System.EventHandler(this.comboBoxPersonalSituation_DropDown);
            // 
            // numericUpDownJobs1
            // 
            resources.ApplyResources(this.numericUpDownJobs1, "numericUpDownJobs1");
            this.numericUpDownJobs1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownJobs1.Name = "numericUpDownJobs1";
            this.numericUpDownJobs1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDownJobs2
            // 
            resources.ApplyResources(this.numericUpDownJobs2, "numericUpDownJobs2");
            this.numericUpDownJobs2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownJobs2.Name = "numericUpDownJobs2";
            this.numericUpDownJobs2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDownYear
            // 
            resources.ApplyResources(this.numericUpDownYear, "numericUpDownYear");
            this.numericUpDownYear.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownYear.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownYear.Name = "numericUpDownYear";
            this.numericUpDownYear.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // ProjectFollowUp
            // 
            this.AcceptButton = this.buttonSave;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_gris_180;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ProjectFollowUp";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownJobs1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownJobs2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownYear)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tBComment;
        private System.Windows.Forms.TextBox tBCA;
        private System.Windows.Forms.Label lProjectYear;
        private System.Windows.Forms.Label lProjectJobs1;
        private System.Windows.Forms.Label lProjectJobs2;
        private System.Windows.Forms.Label lProjectFollowUpCA;
        private System.Windows.Forms.Label lProjectPersonalSituation;
        private System.Windows.Forms.Label lProjectActivity;
        private System.Windows.Forms.Label lProjectComment;
        private Octopus.GUI.UserControl.SweetButton buttonCancel;
        private Octopus.GUI.UserControl.SweetButton buttonSave;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numericUpDownYear;
        private System.Windows.Forms.NumericUpDown numericUpDownJobs1;
        private System.Windows.Forms.NumericUpDown numericUpDownJobs2;
        private System.Windows.Forms.ComboBox comboBoxActivity;
        private System.Windows.Forms.ComboBox comboBoxPersonalSituation;
    }
}