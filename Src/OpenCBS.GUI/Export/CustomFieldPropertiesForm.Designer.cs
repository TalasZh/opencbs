namespace OpenCBS.GUI.Export
{
    partial class CustomFieldPropertiesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomFieldPropertiesForm));
            this.labelName = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelDefaultValue = new System.Windows.Forms.Label();
            this.textBoxDefaultValue = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelName
            // 
            this.labelName.AccessibleDescription = null;
            this.labelName.AccessibleName = null;
            resources.ApplyResources(this.labelName, "labelName");
            this.labelName.Font = null;
            this.labelName.Name = "labelName";
            // 
            // textBoxName
            // 
            this.textBoxName.AccessibleDescription = null;
            this.textBoxName.AccessibleName = null;
            resources.ApplyResources(this.textBoxName, "textBoxName");
            this.textBoxName.BackgroundImage = null;
            this.textBoxName.Font = null;
            this.textBoxName.Name = "textBoxName";
            // 
            // labelDefaultValue
            // 
            this.labelDefaultValue.AccessibleDescription = null;
            this.labelDefaultValue.AccessibleName = null;
            resources.ApplyResources(this.labelDefaultValue, "labelDefaultValue");
            this.labelDefaultValue.Font = null;
            this.labelDefaultValue.Name = "labelDefaultValue";
            // 
            // textBoxDefaultValue
            // 
            this.textBoxDefaultValue.AccessibleDescription = null;
            this.textBoxDefaultValue.AccessibleName = null;
            resources.ApplyResources(this.textBoxDefaultValue, "textBoxDefaultValue");
            this.textBoxDefaultValue.BackgroundImage = null;
            this.textBoxDefaultValue.Font = null;
            this.textBoxDefaultValue.Name = "textBoxDefaultValue";
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
            // CustomFieldPropertiesForm
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.textBoxDefaultValue);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.labelDefaultValue);
            this.Controls.Add(this.labelName);
            this.Font = null;
            this.Name = "CustomFieldPropertiesForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelDefaultValue;
        private System.Windows.Forms.TextBox textBoxDefaultValue;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
    }
}