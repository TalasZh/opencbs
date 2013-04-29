namespace Octopus.GUI.UserControl
{
    partial class PrintButton
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
            this.components = new System.ComponentModel.Container();
            this._menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SuspendLayout();
            // 
            // _menu
            // 
            this._menu.Name = "_menu";
            this._menu.Size = new System.Drawing.Size(61, 4);
            // 
            // PrintButton
            // 
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_bouton;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ContextMenuStrip = this._menu;
            this.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Print;
            this.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Menu = this._menu;
            this.UseVisualStyleBackColor = false;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip _menu;
    }
}
