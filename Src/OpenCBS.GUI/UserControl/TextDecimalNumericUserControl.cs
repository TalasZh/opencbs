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
using System.Globalization;
using System.Windows.Forms;

namespace OpenCBS.GUI.UserControl
{
    public partial class TextDecimalNumericUserControl : System.Windows.Forms.UserControl
    {
        public event EventHandler NumberChanged;

        public TextDecimalNumericUserControl()
        {
            InitializeComponent();
        }

        public override string Text
        {
            get { return textBoxNumeric.Text; }
            set { textBoxNumeric.Text = value; }
        }

        private void textBoxNumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            int keyCode = e.KeyChar;

            if ((keyCode >= 48 && keyCode <= 57) || (keyCode == 8) || (Char.IsControl(e.KeyChar) && e.KeyChar
                != ((char)Keys.V | (char)Keys.ControlKey)) || (Char.IsControl(e.KeyChar) && e.KeyChar !=
                ((char)Keys.C | (char)Keys.ControlKey)) || (e.KeyChar.ToString() == NumberFormatInfo.CurrentInfo.NumberDecimalSeparator))
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

        private void textBoxNumeric_TextChanged(object sender, EventArgs e)
        {
            if (NumberChanged != null)
                NumberChanged(this, e);
        }
    }
}
