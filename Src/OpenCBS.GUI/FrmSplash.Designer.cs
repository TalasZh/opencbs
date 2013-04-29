//Octopus MFS is an integrated suite for managing a Micro Finance Institution: clients, contracts, accounting, reporting and risk
//Copyright � 2006,2007 OCTO Technology & OXUS Development Network
//
//This program is free software; you can redistribute it and/or modify
//it under the terms of the GNU Lesser General Public License as published by
//the Free Software Foundation; either version 2 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU Lesser General Public License for more details.
//
//You should have received a copy of the GNU Lesser General Public License along
//with this program; if not, write to the Free Software Foundation, Inc.,
//51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
//
// Licence : http://www.octopusnetwork.org/OverviewLicence.aspx
//
// Website : http://www.octopusnetwork.org
// Business contact: business(at)octopusnetwork.org
// Technical contact email : tech(at)octopusnetwork.org 

namespace Octopus.GUI
{
    partial class FrmSplash
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSplash));
            this.label1 = new System.Windows.Forms.Label();
            this.labelConfigurationValue = new System.Windows.Forms.Label();
            this.bWOneToSeven = new System.ComponentModel.BackgroundWorker();
            this.bWSeventToEight = new System.ComponentModel.BackgroundWorker();
            this.pictureBoxAboutOctopus = new System.Windows.Forms.PictureBox();
            this.oPBMacroProgression = new Octopus.GUI.OctopusProgressBar();
            this.oPBarMicroProgression = new Octopus.GUI.OctopusProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAboutOctopus)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // labelConfigurationValue
            // 
            this.labelConfigurationValue.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.labelConfigurationValue, "labelConfigurationValue");
            this.labelConfigurationValue.Name = "labelConfigurationValue";
            // 
            // bWOneToSeven
            // 
            this.bWOneToSeven.WorkerReportsProgress = true;
            this.bWOneToSeven.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.bWOneToSeven.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            this.bWOneToSeven.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            // 
            // bWSeventToEight
            // 
            this.bWSeventToEight.WorkerReportsProgress = true;
            this.bWSeventToEight.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bWSeventToEight_DoWork);
            this.bWSeventToEight.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bWSeventToEight_RunWorkerCompleted);
            this.bWSeventToEight.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bWSeventToEight_ProgressChanged);
            // 
            // pictureBoxAboutOctopus
            // 
            resources.ApplyResources(this.pictureBoxAboutOctopus, "pictureBoxAboutOctopus");
            this.pictureBoxAboutOctopus.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxAboutOctopus.InitialImage = null;
            this.pictureBoxAboutOctopus.Name = "pictureBoxAboutOctopus";
            this.pictureBoxAboutOctopus.TabStop = false;
            // 
            // oPBMacroProgression
            // 
            this.oPBMacroProgression.BackColor = System.Drawing.Color.Transparent;
            this.oPBMacroProgression.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.oPBMacroProgression, "oPBMacroProgression");
            this.oPBMacroProgression.Name = "oPBMacroProgression";
            this.oPBMacroProgression.Step = 13;
            this.oPBMacroProgression.Value = 0;
            // 
            // oPBarMicroProgression
            // 
            this.oPBarMicroProgression.BackColor = System.Drawing.Color.Transparent;
            this.oPBarMicroProgression.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.oPBarMicroProgression, "oPBarMicroProgression");
            this.oPBarMicroProgression.Name = "oPBarMicroProgression";
            this.oPBarMicroProgression.Step = 10;
            this.oPBarMicroProgression.Value = 0;
            // 
            // FrmSplash
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.pictureBoxAboutOctopus);
            this.Controls.Add(this.oPBMacroProgression);
            this.Controls.Add(this.oPBarMicroProgression);
            this.Controls.Add(this.labelConfigurationValue);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSplash";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Shown += new System.EventHandler(this.FrmSplash_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAboutOctopus)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelConfigurationValue;
        private OctopusProgressBar oPBarMicroProgression;
        private OctopusProgressBar oPBMacroProgression;
        private System.ComponentModel.BackgroundWorker bWOneToSeven;
        private System.ComponentModel.BackgroundWorker bWSeventToEight;
        private System.Windows.Forms.PictureBox pictureBoxAboutOctopus;
    }
}

