// LICENSE PLACEHOLDER

using System.ComponentModel;
using System.Windows.Forms;

namespace OpenCBS.GUI
{
	/// <summary>
	/// Summary description for AboutForm.
	/// </summary>
	public class AboutForm : Form
    {
        private PictureBox aboutPictureBox;
        private IContainer components;

		public AboutForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.aboutPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.aboutPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // aboutPictureBox
            // 
            resources.ApplyResources(this.aboutPictureBox, "aboutPictureBox");
            this.aboutPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.aboutPictureBox.BackgroundImage = global::OpenCBS.GUI.Properties.Resources.LOGO;
            this.aboutPictureBox.Name = "aboutPictureBox";
            this.aboutPictureBox.TabStop = false;
            // 
            // AboutForm
            // 
            resources.ApplyResources(this, "$this");
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.aboutPictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            ((System.ComponentModel.ISupportInitialize)(this.aboutPictureBox)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion
	}
}
