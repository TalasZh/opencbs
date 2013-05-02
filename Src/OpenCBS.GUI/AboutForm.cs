// LICENSE PLACEHOLDER

using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using OpenCBS.Shared.Settings;

namespace OpenCBS.GUI
{
	/// <summary>
	/// Summary description for AboutForm.
	/// </summary>
	public class AboutForm : Form
    {
        private PictureBox pictureBoxAboutOctopus;
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
            this.pictureBoxAboutOctopus = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAboutOctopus)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxAboutOctopus
            // 
            resources.ApplyResources(this.pictureBoxAboutOctopus, "pictureBoxAboutOctopus");
            this.pictureBoxAboutOctopus.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxAboutOctopus.BackgroundImage = global::OpenCBS.GUI.Properties.Resources.LOGO;
            this.pictureBoxAboutOctopus.Name = "pictureBoxAboutOctopus";
            this.pictureBoxAboutOctopus.TabStop = false;
            // 
            // AboutForm
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.pictureBoxAboutOctopus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAboutOctopus)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.octopusnetwork.org");
        }
	}
}
