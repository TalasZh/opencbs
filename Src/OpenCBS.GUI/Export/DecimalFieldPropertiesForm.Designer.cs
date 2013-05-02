namespace OpenCBS.GUI.Export
{
    partial class DecimalFieldPropertiesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DecimalFieldPropertiesForm));
            this.labelDecimalNumber = new System.Windows.Forms.Label();
            this.tnDecimalNumber = new OpenCBS.GUI.UserControl.TextNumericUserControl();
            this.labelDecimalSeparator = new System.Windows.Forms.Label();
            this.textBoxDecimalSeparator = new System.Windows.Forms.TextBox();
            this.labelGroupSeparator = new System.Windows.Forms.Label();
            this.textBoxGroupSeparator = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.checkBoxAlignRight = new System.Windows.Forms.CheckBox();
            this.labelSampleValue = new System.Windows.Forms.Label();
            this.labelSampleLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelDecimalNumber
            // 
            this.labelDecimalNumber.AccessibleDescription = null;
            this.labelDecimalNumber.AccessibleName = null;
            resources.ApplyResources(this.labelDecimalNumber, "labelDecimalNumber");
            this.labelDecimalNumber.Font = null;
            this.labelDecimalNumber.Name = "labelDecimalNumber";
            // 
            // tnDecimalNumber
            // 
            this.tnDecimalNumber.AccessibleDescription = null;
            this.tnDecimalNumber.AccessibleName = null;
            resources.ApplyResources(this.tnDecimalNumber, "tnDecimalNumber");
            this.tnDecimalNumber.BackgroundImage = null;
            this.tnDecimalNumber.Font = null;
            this.tnDecimalNumber.Name = "tnDecimalNumber";
            this.tnDecimalNumber.NumberChanged += new System.EventHandler(this.tnDecimalNumber_NumberChanged);
            // 
            // labelDecimalSeparator
            // 
            this.labelDecimalSeparator.AccessibleDescription = null;
            this.labelDecimalSeparator.AccessibleName = null;
            resources.ApplyResources(this.labelDecimalSeparator, "labelDecimalSeparator");
            this.labelDecimalSeparator.Font = null;
            this.labelDecimalSeparator.Name = "labelDecimalSeparator";
            // 
            // textBoxDecimalSeparator
            // 
            this.textBoxDecimalSeparator.AccessibleDescription = null;
            this.textBoxDecimalSeparator.AccessibleName = null;
            resources.ApplyResources(this.textBoxDecimalSeparator, "textBoxDecimalSeparator");
            this.textBoxDecimalSeparator.BackgroundImage = null;
            this.textBoxDecimalSeparator.Font = null;
            this.textBoxDecimalSeparator.Name = "textBoxDecimalSeparator";
            this.textBoxDecimalSeparator.TextChanged += new System.EventHandler(this.textBoxDecimalSeparator_TextChanged);
            // 
            // labelGroupSeparator
            // 
            this.labelGroupSeparator.AccessibleDescription = null;
            this.labelGroupSeparator.AccessibleName = null;
            resources.ApplyResources(this.labelGroupSeparator, "labelGroupSeparator");
            this.labelGroupSeparator.Font = null;
            this.labelGroupSeparator.Name = "labelGroupSeparator";
            // 
            // textBoxGroupSeparator
            // 
            this.textBoxGroupSeparator.AccessibleDescription = null;
            this.textBoxGroupSeparator.AccessibleName = null;
            resources.ApplyResources(this.textBoxGroupSeparator, "textBoxGroupSeparator");
            this.textBoxGroupSeparator.BackgroundImage = null;
            this.textBoxGroupSeparator.Font = null;
            this.textBoxGroupSeparator.Name = "textBoxGroupSeparator";
            this.textBoxGroupSeparator.TextChanged += new System.EventHandler(this.textBoxGroupSeparator_TextChanged);
            // 
            // buttonSave
            // 
            this.buttonSave.AccessibleDescription = null;
            this.buttonSave.AccessibleName = null;
            resources.ApplyResources(this.buttonSave, "buttonSave");
            this.buttonSave.BackgroundImage = null;
            this.buttonSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonSave.Font = null;
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.AccessibleDescription = null;
            this.buttonCancel.AccessibleName = null;
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.BackgroundImage = null;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Font = null;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // checkBoxAlignRight
            // 
            this.checkBoxAlignRight.AccessibleDescription = null;
            this.checkBoxAlignRight.AccessibleName = null;
            resources.ApplyResources(this.checkBoxAlignRight, "checkBoxAlignRight");
            this.checkBoxAlignRight.Font = null;
            this.checkBoxAlignRight.Name = "checkBoxAlignRight";
            // 
            // labelSampleValue
            // 
            this.labelSampleValue.AccessibleDescription = null;
            this.labelSampleValue.AccessibleName = null;
            resources.ApplyResources(this.labelSampleValue, "labelSampleValue");
            this.labelSampleValue.Name = "labelSampleValue";
            // 
            // labelSampleLabel
            // 
            this.labelSampleLabel.AccessibleDescription = null;
            this.labelSampleLabel.AccessibleName = null;
            resources.ApplyResources(this.labelSampleLabel, "labelSampleLabel");
            this.labelSampleLabel.Name = "labelSampleLabel";
            // 
            // DecimalFieldPropertiesForm
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelSampleValue);
            this.Controls.Add(this.labelSampleLabel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.checkBoxAlignRight);
            this.Controls.Add(this.textBoxGroupSeparator);
            this.Controls.Add(this.textBoxDecimalSeparator);
            this.Controls.Add(this.labelGroupSeparator);
            this.Controls.Add(this.labelDecimalSeparator);
            this.Controls.Add(this.tnDecimalNumber);
            this.Controls.Add(this.labelDecimalNumber);
            this.Font = null;
            this.Name = "DecimalFieldPropertiesForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelDecimalNumber;
        private OpenCBS.GUI.UserControl.TextNumericUserControl tnDecimalNumber;
        private System.Windows.Forms.Label labelDecimalSeparator;
        private System.Windows.Forms.TextBox textBoxDecimalSeparator;
        private System.Windows.Forms.Label labelGroupSeparator;
        private System.Windows.Forms.TextBox textBoxGroupSeparator;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.CheckBox checkBoxAlignRight;
        private System.Windows.Forms.Label labelSampleValue;
        private System.Windows.Forms.Label labelSampleLabel;
    }
}