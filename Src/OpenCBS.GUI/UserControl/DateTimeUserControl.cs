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
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using OpenCBS.Shared;

namespace OpenCBS.GUI
{
	/// <summary>
	/// Summary description for DateTimeUserControl.
	/// </summary>
	public class DateTimeUserControl : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.CheckBox checkBoxDate;
		private System.Windows.Forms.DateTimePicker dateTimePickerDate;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DateTimeUserControl()
		{
			InitializeComponent();
			this.dateTimePickerDate.Value = TimeProvider.Today;
		}

		public DateTime? Date
		{
			get
			{
				if(this.checkBoxDate.Checked)
					return this.dateTimePickerDate.Value;
				else
					return null;
			}
		}

		[Category("Configurations"), Browsable(true), Description("The text contained in the control.")]
		public string DateText
		{
			get
			{
				return this.checkBoxDate.Text;
			}
			set
			{
				this.checkBoxDate.Text = value;
			}
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DateTimeUserControl));
            this.dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
            this.checkBoxDate = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // dateTimePickerDate
            // 
            resources.ApplyResources(this.dateTimePickerDate, "dateTimePickerDate");
            this.dateTimePickerDate.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerDate.Name = "dateTimePickerDate";
            // 
            // checkBoxDate
            // 
            resources.ApplyResources(this.checkBoxDate, "checkBoxDate");
            this.checkBoxDate.Name = "checkBoxDate";
            this.checkBoxDate.CheckedChanged += new System.EventHandler(this.checkBoxDate_CheckedChanged);
            // 
            // DateTimeUserControl
            //
            this.Controls.Add(this.dateTimePickerDate);
            this.Controls.Add(this.checkBoxDate);
            this.Name = "DateTimeUserControl";
            resources.ApplyResources(this, "$this");
            this.ResumeLayout(false);

		}
		#endregion

		private void checkBoxDate_CheckedChanged(object sender, System.EventArgs e)
		{
			this.dateTimePickerDate.Enabled = this.checkBoxDate.Checked;
		}
	}
}
