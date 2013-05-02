// LICENSE PLACEHOLDER

using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using OpenCBS.Shared.Settings;

namespace OpenCBS.GUI
{
	/// <summary>
	/// Summary description for AboutOctopus.
	/// </summary>
	public class AboutOctopus : Form
    {
		private Label label1;
        private LinkLabel linkLabel1;
        private Label lblAssembliesInfo;
        private Label label2;
        private Timer timerThanks;
        private Label lblThanks;
        private PictureBox pictureBoxAboutOctopus;
        private IContainer components;

        private int _thanksPos = 0;
        private int _thanksCounter = 0;

		public AboutOctopus()
		{
			InitializeComponent();
			InitializeLabels();
		}

        private void InitializeLabels()
        {
            if (TechnicalSettings.SoftwareBuild != string.Empty) label1.Text = TechnicalSettings.SoftwareVersionWithBuild;
            else label1.Text = TechnicalSettings.SoftwareVersion;

            lblAssembliesInfo.Text = "";
            try
            {
                System.Reflection.Assembly assCrystal =
                    System.Reflection.Assembly.Load("CrystalDecisions.CrystalReports.Engine");
                lblAssembliesInfo.Text += "Crystal Reports v" + assCrystal.GetName().Version.ToString();
            } catch (Exception) {}
            try
            {
                System.Reflection.Assembly assZed = System.Reflection.Assembly.Load("ZedGraph");
                lblAssembliesInfo.Text += "\nZedGraph v" + assZed.GetName().Version.ToString();
            } catch (Exception) { }
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutOctopus));
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.lblAssembliesInfo = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.timerThanks = new System.Windows.Forms.Timer(this.components);
            this.lblThanks = new System.Windows.Forms.Label();
            this.pictureBoxAboutOctopus = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAboutOctopus)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AllowDrop = true;
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Name = "label1";
            // 
            // linkLabel1
            // 
            resources.ApplyResources(this.linkLabel1, "linkLabel1");
            this.linkLabel1.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel1.ForeColor = System.Drawing.Color.White;
            this.linkLabel1.LinkColor = System.Drawing.Color.White;
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.TabStop = true;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // lblAssembliesInfo
            // 
            resources.ApplyResources(this.lblAssembliesInfo, "lblAssembliesInfo");
            this.lblAssembliesInfo.BackColor = System.Drawing.Color.Transparent;
            this.lblAssembliesInfo.Name = "lblAssembliesInfo";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Name = "label2";
            // 
            // timerThanks
            // 
            this.timerThanks.Enabled = true;
            this.timerThanks.Interval = 80;
            this.timerThanks.Tick += new System.EventHandler(this.timerThanks_Tick);
            // 
            // lblThanks
            // 
            this.lblThanks.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lblThanks, "lblThanks");
            this.lblThanks.Name = "lblThanks";
            this.lblThanks.UseMnemonic = false;
            // 
            // pictureBoxAboutOctopus
            // 
            resources.ApplyResources(this.pictureBoxAboutOctopus, "pictureBoxAboutOctopus");
            this.pictureBoxAboutOctopus.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxAboutOctopus.Name = "pictureBoxAboutOctopus";
            this.pictureBoxAboutOctopus.TabStop = false;
            // 
            // AboutOctopus
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBoxAboutOctopus);
            this.Controls.Add(this.lblThanks);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblAssembliesInfo);
            this.Controls.Add(this.linkLabel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutOctopus";
            this.Load += new System.EventHandler(this.AboutOctopus_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAboutOctopus)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.octopusnetwork.org");
        }

        private void AboutOctopus_Load(object sender, EventArgs e)
        {
            lblThanks.Text = _thanks[0];
        }

        private string[] _thanks =  { 
            "Octopus is developed by OCTO Technology and OXUS Development Network.",
            //"Octopus technical leader:\n\nNicolas Mangin",
            "Development core team:\n\nAndrey Galkin, Pavel Bastov, Farida Abdulkhafizova, Evgeniy Abdukarimov, Ruslan Kazakov",
            //"Have also participated to development :\n\nEric Groise & Nicolas Baron",
            "Release manager:\n\nJulien Mahuzier",
            "And thanks to all our users for using\nOctopus Micro Finance Suite",
        };

        private void timerThanks_Tick(object sender, EventArgs e)
        {
            _thanksCounter++;

            if ((_thanksCounter >= 40) && (_thanksCounter < 50))
            {
                int delta = _thanksCounter - 40;
                lblThanks.ForeColor = Color.FromArgb(delta * 24, 86 + delta * 17, delta*24);
            }
            if  (_thanksCounter == 50)
            {
                lblThanks.Text = "";
            }
            if  (_thanksCounter == 54)
            {
                _thanksPos++;
                if (_thanksPos == _thanks.Length)
                {
                    timerThanks.Enabled = false;
                    lblThanks.Visible = false;
                    return;
                }
                lblThanks.Text = _thanks[_thanksPos];
            }
            if ((_thanksCounter >= 55) && (_thanksCounter < 65))
            {
                int delta = 10 - (_thanksCounter - 55);
                lblThanks.ForeColor = Color.FromArgb(delta * 24, 86 + delta * 16, delta * 24);
            }
            if (_thanksCounter == 65)
            {
                lblThanks.ForeColor = Color.FromArgb(0, 86, 0);
                _thanksCounter = 0;
            }
        }

	}
}
