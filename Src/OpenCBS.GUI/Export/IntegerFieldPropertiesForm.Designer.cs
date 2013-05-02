namespace OpenCBS.GUI.Export
{
    partial class IntegerFieldPropertiesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IntegerFieldPropertiesForm));
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.checkBoxAlignRight = new System.Windows.Forms.CheckBox();
            this.checkBoxDisplayZero = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
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
            // checkBoxDisplayZero
            // 
            this.checkBoxDisplayZero.AccessibleDescription = null;
            this.checkBoxDisplayZero.AccessibleName = null;
            resources.ApplyResources(this.checkBoxDisplayZero, "checkBoxDisplayZero");
            this.checkBoxDisplayZero.Font = null;
            this.checkBoxDisplayZero.Name = "checkBoxDisplayZero";
            // 
            // IntegerFieldPropertiesForm
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.checkBoxDisplayZero);
            this.Controls.Add(this.checkBoxAlignRight);
            this.Font = null;
            this.Name = "IntegerFieldPropertiesForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.CheckBox checkBoxAlignRight;
        private System.Windows.Forms.CheckBox checkBoxDisplayZero;
    }
}