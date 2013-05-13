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

using System.Drawing;
using System.Windows.Forms;

namespace OpenCBS.GUI.UserControl
{
    public class TextBoxLimit : TextBox
    {
        private Color _previousBackColor = Color.Empty;

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (Text.Length >= MaxLength)
            {
                if (!IsBackColorRemembered()) RememberBackColor();
                BackColor = Color.Red;
            }

            base.OnKeyPress(e);
        }

        protected override void OnTextChanged(System.EventArgs e)
        {
            base.OnTextChanged(e);
            if (Text.Length < MaxLength && IsBackColorRemembered())
            {
                SetPreviousBackColor();
            }
        }

        private void SetPreviousBackColor()
        {
            BackColor = _previousBackColor;
        }

        protected override void OnLostFocus(System.EventArgs e)
        {
            base.OnLostFocus(e);
            if (Text.Length <= MaxLength && IsBackColorRemembered())
            {
                SetPreviousBackColor();
            }
        }

        private void RememberBackColor()
        {
            _previousBackColor = BackColor;
        }

        private bool IsBackColorRemembered()
        {
            return _previousBackColor != Color.Empty;
        }
    }
}
