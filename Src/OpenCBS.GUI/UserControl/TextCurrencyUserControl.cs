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

namespace OpenCBS.GUI.UserControl
{
	/// <summary>
	/// Summary description for TextCurrencyUserControl.
	/// </summary>
	public class TextCurrencyUserControl : System.Windows.Forms.UserControl
	{
        private Label label1;
        private TextBox textBoxExternalCurrency;
        private TextBox textBoxInternalCurrency;
        private Label label2;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public TextCurrencyUserControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxExternalCurrency = new System.Windows.Forms.TextBox();
            this.textBoxInternalCurrency = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(109, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "EC";
            // 
            // textBoxExternalCurrency
            // 
            this.textBoxExternalCurrency.Location = new System.Drawing.Point(3, 3);
            this.textBoxExternalCurrency.Name = "textBoxExternalCurrency";
            this.textBoxExternalCurrency.Size = new System.Drawing.Size(100, 20);
            this.textBoxExternalCurrency.TabIndex = 1;
            // 
            // textBoxInternalCurrency
            // 
            this.textBoxInternalCurrency.Location = new System.Drawing.Point(177, 3);
            this.textBoxInternalCurrency.Name = "textBoxInternalCurrency";
            this.textBoxInternalCurrency.Size = new System.Drawing.Size(100, 20);
            this.textBoxInternalCurrency.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(283, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "IC";
            // 
            // TextCurrencyUserControl
            // 
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxInternalCurrency);
            this.Controls.Add(this.textBoxExternalCurrency);
            this.Controls.Add(this.label1);
            this.Name = "TextCurrencyUserControl";
            this.Size = new System.Drawing.Size(327, 27);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
	}
}
