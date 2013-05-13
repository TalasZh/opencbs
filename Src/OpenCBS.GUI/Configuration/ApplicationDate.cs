// Octopus MFS is an integrated suite for managing a Micro Finance Institution: 
// clients, contracts, accounting, reporting and risk
// Copyright © 2006,2007 OCTO Technology & OXUS Development Network
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License along
// with this program; if not, write to the Free Software Foundation, Inc.,
// 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
// Website: http://www.opencbs.com
// Contact: contact@opencbs.com

using System;
using OpenCBS.GUI.UserControl;

namespace OpenCBS.GUI
{
	/// <summary>
	/// Summary description for ApplicationDate.
	/// </summary>
	public class ApplicationDate : SweetOkCancelForm
	{
		private System.Windows.Forms.Label lblCurrentDateLabel;
		private System.Windows.Forms.Label lblCurentDate;
		private System.Windows.Forms.MonthCalendar mcalCurrentDate;
        private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.GroupBox groupBox2;

	    public DateTime Today { get; set; }

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ApplicationDate()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApplicationDate));
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.mcalCurrentDate = new System.Windows.Forms.MonthCalendar();
            this.lblCurrentDateLabel = new System.Windows.Forms.Label();
            this.lblCurentDate = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitter1
            // 
            resources.ApplyResources(this.splitter1, "splitter1");
            this.splitter1.Name = "splitter1";
            this.splitter1.TabStop = false;
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.mcalCurrentDate);
            this.groupBox2.Controls.Add(this.lblCurrentDateLabel);
            this.groupBox2.Controls.Add(this.lblCurentDate);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // mcalCurrentDate
            // 
            this.mcalCurrentDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            resources.ApplyResources(this.mcalCurrentDate, "mcalCurrentDate");
            this.mcalCurrentDate.MaxSelectionCount = 1;
            this.mcalCurrentDate.Name = "mcalCurrentDate";
            this.mcalCurrentDate.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.mcalCurrentDate_DateSelected);
            this.mcalCurrentDate.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mcalCurrentDate_MouseUp);
            this.mcalCurrentDate.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.mcalCurrentDate_DateChanged);
            // 
            // lblCurrentDateLabel
            // 
            this.lblCurrentDateLabel.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lblCurrentDateLabel, "lblCurrentDateLabel");
            this.lblCurrentDateLabel.Name = "lblCurrentDateLabel";
            // 
            // lblCurentDate
            // 
            this.lblCurentDate.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.lblCurentDate, "lblCurentDate");
            this.lblCurentDate.Name = "lblCurentDate";
            // 
            // ApplicationDate
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.splitter1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ApplicationDate";
            this.Load += new System.EventHandler(this.ApplicationDate_Load);
            this.Controls.SetChildIndex(this.splitter1, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void ApplicationDate_Load(object sender, System.EventArgs e)
		{
			mcalCurrentDate.SetDate(Today);
			lblCurentDate.Text = Today.ToShortDateString();
		}

		private void mcalCurrentDate_DateChanged(object sender, System.Windows.Forms.DateRangeEventArgs e)
		{
		    Today = mcalCurrentDate.SelectionStart;
			lblCurentDate.Text = Today.ToShortDateString();
            mcalCurrentDate.Invalidate();
		}

        private void mcalCurrentDate_DateSelected(object sender, System.Windows.Forms.DateRangeEventArgs e)
        {
            mcalCurrentDate.Invalidate();
        }

        private void mcalCurrentDate_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            mcalCurrentDate.Invalidate();
        }

	}
}
