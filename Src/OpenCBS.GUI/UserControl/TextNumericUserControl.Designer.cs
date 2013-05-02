namespace OpenCBS.GUI.UserControl
{
    partial class TextNumericUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxNumeric = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxNumeric
            // 
            this.textBoxNumeric.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxNumeric.Location = new System.Drawing.Point(0, 0);
            this.textBoxNumeric.Name = "textBoxNumeric";
            this.textBoxNumeric.Size = new System.Drawing.Size(100, 20);
            this.textBoxNumeric.TabIndex = 0;
            this.textBoxNumeric.TextChanged += new System.EventHandler(this.textBoxNumeric_TextChanged);
            this.textBoxNumeric.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxNumeric_KeyPress);
            // 
            // TextNumericUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBoxNumeric);
            this.Name = "TextNumericUserControl";
            this.Size = new System.Drawing.Size(100, 20);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxNumeric;
    }
}
