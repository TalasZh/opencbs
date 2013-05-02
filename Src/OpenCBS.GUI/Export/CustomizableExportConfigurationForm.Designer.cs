namespace OpenCBS.GUI.Export
{
    partial class CustomizableExportConfigurationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomizableExportConfigurationForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageDescription = new System.Windows.Forms.TabPage();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelFileName = new System.Windows.Forms.Label();
            this.fieldListUserControl1 = new OpenCBS.GUI.Export.FieldListUserControl();
            this.tabPageFormat = new System.Windows.Forms.TabPage();
            this.textBoxExtension = new System.Windows.Forms.TextBox();
            this.labelExtension = new System.Windows.Forms.Label();
            this.comboBoxEncloseChar = new System.Windows.Forms.ComboBox();
            this.comboBoxFieldDelimiter = new System.Windows.Forms.ComboBox();
            this.checkBoxEncloseChar = new System.Windows.Forms.CheckBox();
            this.checkBoxDisplayHeader = new System.Windows.Forms.CheckBox();
            this.checkBoxSpecificLength = new System.Windows.Forms.CheckBox();
            this.checkBoxHasDelimiter = new System.Windows.Forms.CheckBox();
            this.tabPageSpecific = new System.Windows.Forms.TabPage();
            this.panelInstallmentExportFile = new System.Windows.Forms.Panel();
            this.checkBoxTagAsPending = new System.Windows.Forms.CheckBox();
            this.comboBoxPaymentMethods = new System.Windows.Forms.ComboBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.saveExportFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.tabControl1.SuspendLayout();
            this.tabPageDescription.SuspendLayout();
            this.tabPageFormat.SuspendLayout();
            this.tabPageSpecific.SuspendLayout();
            this.panelInstallmentExportFile.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPageDescription);
            this.tabControl1.Controls.Add(this.tabPageFormat);
            this.tabControl1.Controls.Add(this.tabPageSpecific);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPageDescription
            // 
            this.tabPageDescription.Controls.Add(this.textBoxName);
            this.tabPageDescription.Controls.Add(this.labelFileName);
            this.tabPageDescription.Controls.Add(this.fieldListUserControl1);
            resources.ApplyResources(this.tabPageDescription, "tabPageDescription");
            this.tabPageDescription.Name = "tabPageDescription";
            // 
            // textBoxName
            // 
            resources.ApplyResources(this.textBoxName, "textBoxName");
            this.textBoxName.Name = "textBoxName";
            // 
            // labelFileName
            // 
            resources.ApplyResources(this.labelFileName, "labelFileName");
            this.labelFileName.Name = "labelFileName";
            // 
            // fieldListUserControl1
            // 
            resources.ApplyResources(this.fieldListUserControl1, "fieldListUserControl1");
            this.fieldListUserControl1.BackColor = System.Drawing.Color.Transparent;
            this.fieldListUserControl1.Name = "fieldListUserControl1";
            this.fieldListUserControl1.SelectedFields = null;
            // 
            // tabPageFormat
            // 
            this.tabPageFormat.Controls.Add(this.textBoxExtension);
            this.tabPageFormat.Controls.Add(this.labelExtension);
            this.tabPageFormat.Controls.Add(this.comboBoxEncloseChar);
            this.tabPageFormat.Controls.Add(this.comboBoxFieldDelimiter);
            this.tabPageFormat.Controls.Add(this.checkBoxEncloseChar);
            this.tabPageFormat.Controls.Add(this.checkBoxDisplayHeader);
            this.tabPageFormat.Controls.Add(this.checkBoxSpecificLength);
            this.tabPageFormat.Controls.Add(this.checkBoxHasDelimiter);
            resources.ApplyResources(this.tabPageFormat, "tabPageFormat");
            this.tabPageFormat.Name = "tabPageFormat";
            // 
            // textBoxExtension
            // 
            resources.ApplyResources(this.textBoxExtension, "textBoxExtension");
            this.textBoxExtension.Name = "textBoxExtension";
            // 
            // labelExtension
            // 
            resources.ApplyResources(this.labelExtension, "labelExtension");
            this.labelExtension.Name = "labelExtension";
            // 
            // comboBoxEncloseChar
            // 
            resources.ApplyResources(this.comboBoxEncloseChar, "comboBoxEncloseChar");
            this.comboBoxEncloseChar.FormattingEnabled = true;
            this.comboBoxEncloseChar.Items.AddRange(new object[] {
            resources.GetString("comboBoxEncloseChar.Items"),
            resources.GetString("comboBoxEncloseChar.Items1")});
            this.comboBoxEncloseChar.Name = "comboBoxEncloseChar";
            // 
            // comboBoxFieldDelimiter
            // 
            resources.ApplyResources(this.comboBoxFieldDelimiter, "comboBoxFieldDelimiter");
            this.comboBoxFieldDelimiter.FormattingEnabled = true;
            this.comboBoxFieldDelimiter.Items.AddRange(new object[] {
            resources.GetString("comboBoxFieldDelimiter.Items"),
            resources.GetString("comboBoxFieldDelimiter.Items1"),
            resources.GetString("comboBoxFieldDelimiter.Items2"),
            resources.GetString("comboBoxFieldDelimiter.Items3")});
            this.comboBoxFieldDelimiter.Name = "comboBoxFieldDelimiter";
            // 
            // checkBoxEncloseChar
            // 
            resources.ApplyResources(this.checkBoxEncloseChar, "checkBoxEncloseChar");
            this.checkBoxEncloseChar.Name = "checkBoxEncloseChar";
            this.checkBoxEncloseChar.CheckedChanged += new System.EventHandler(this.checkBoxEncloseChar_CheckedChanged);
            // 
            // checkBoxDisplayHeader
            // 
            resources.ApplyResources(this.checkBoxDisplayHeader, "checkBoxDisplayHeader");
            this.checkBoxDisplayHeader.Name = "checkBoxDisplayHeader";
            // 
            // checkBoxSpecificLength
            // 
            resources.ApplyResources(this.checkBoxSpecificLength, "checkBoxSpecificLength");
            this.checkBoxSpecificLength.Checked = true;
            this.checkBoxSpecificLength.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSpecificLength.Name = "checkBoxSpecificLength";
            this.checkBoxSpecificLength.CheckedChanged += new System.EventHandler(this.checkBoxSpecificLength_CheckedChanged);
            // 
            // checkBoxHasDelimiter
            // 
            resources.ApplyResources(this.checkBoxHasDelimiter, "checkBoxHasDelimiter");
            this.checkBoxHasDelimiter.Name = "checkBoxHasDelimiter";
            this.checkBoxHasDelimiter.CheckedChanged += new System.EventHandler(this.checkBoxHasDelimiter_CheckedChanged);
            // 
            // tabPageSpecific
            // 
            this.tabPageSpecific.Controls.Add(this.panelInstallmentExportFile);
            resources.ApplyResources(this.tabPageSpecific, "tabPageSpecific");
            this.tabPageSpecific.Name = "tabPageSpecific";
            // 
            // panelInstallmentExportFile
            // 
            this.panelInstallmentExportFile.Controls.Add(this.checkBoxTagAsPending);
            this.panelInstallmentExportFile.Controls.Add(this.comboBoxPaymentMethods);
            resources.ApplyResources(this.panelInstallmentExportFile, "panelInstallmentExportFile");
            this.panelInstallmentExportFile.Name = "panelInstallmentExportFile";
            // 
            // checkBoxTagAsPending
            // 
            resources.ApplyResources(this.checkBoxTagAsPending, "checkBoxTagAsPending");
            this.checkBoxTagAsPending.Name = "checkBoxTagAsPending";
            this.checkBoxTagAsPending.CheckedChanged += new System.EventHandler(this.checkBoxTagAsPending_CheckedChanged);
            // 
            // comboBoxPaymentMethods
            // 
            this.comboBoxPaymentMethods.DisplayMember = "Name";
            this.comboBoxPaymentMethods.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.comboBoxPaymentMethods, "comboBoxPaymentMethods");
            this.comboBoxPaymentMethods.FormattingEnabled = true;
            this.comboBoxPaymentMethods.Name = "comboBoxPaymentMethods";
            this.comboBoxPaymentMethods.ValueMember = "Id";
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonSave
            // 
            resources.ApplyResources(this.buttonSave, "buttonSave");
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // saveExportFileDialog
            // 
            resources.ApplyResources(this.saveExportFileDialog, "saveExportFileDialog");
            // 
            // CustomizableExportConfigurationForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.tabControl1);
            this.Name = "CustomizableExportConfigurationForm";
            this.tabControl1.ResumeLayout(false);
            this.tabPageDescription.ResumeLayout(false);
            this.tabPageDescription.PerformLayout();
            this.tabPageFormat.ResumeLayout(false);
            this.tabPageFormat.PerformLayout();
            this.tabPageSpecific.ResumeLayout(false);
            this.panelInstallmentExportFile.ResumeLayout(false);
            this.panelInstallmentExportFile.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageDescription;
        private System.Windows.Forms.TabPage tabPageFormat;
        private FieldListUserControl fieldListUserControl1;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelFileName;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.ComboBox comboBoxFieldDelimiter;
        private System.Windows.Forms.CheckBox checkBoxHasDelimiter;
        private System.Windows.Forms.CheckBox checkBoxSpecificLength;
        private System.Windows.Forms.ComboBox comboBoxEncloseChar;
        private System.Windows.Forms.CheckBox checkBoxEncloseChar;
        private System.Windows.Forms.Label labelExtension;
        private System.Windows.Forms.TextBox textBoxExtension;
        private System.Windows.Forms.SaveFileDialog saveExportFileDialog;
        private System.Windows.Forms.CheckBox checkBoxDisplayHeader;
        private System.Windows.Forms.TabPage tabPageSpecific;
        private System.Windows.Forms.CheckBox checkBoxTagAsPending;
        private System.Windows.Forms.ComboBox comboBoxPaymentMethods;
        private System.Windows.Forms.Panel panelInstallmentExportFile;
    }
}