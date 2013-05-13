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

namespace OpenCBS.GUI
{
    public partial class OpenCBSProgressBar : Label
    {
        private int _value,_step, _maximum = 100, _minimum = 0;
        
        public OpenCBSProgressBar()
        {
            InitializeComponent();
        }

        public void PerformStep()
        {
            int tempValue = _value + _step;

            if (tempValue > _maximum)
                tempValue = _maximum;
            else if (tempValue < _minimum)
                tempValue = _minimum;
            
            _value = tempValue;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            DrawPrgressBarBorder(e.Graphics);
            DrawProgressBar(e.Graphics);
            base.OnPaint(e);
        }

        private void DrawProgressBar(Graphics g)
        {
            SolidBrush brush = new SolidBrush(Color.FromArgb(0,81,152));
            float percent = (float)(_value - _minimum) / (_maximum - _minimum);
            g.FillRectangle(brush, ClientRectangle.Left + 2, ClientRectangle.Top + 2, (int)((ClientRectangle.Width - 4) * percent), ClientRectangle.Height - 4);
        }

        private void DrawPrgressBarBorder(Graphics g)
        {
            ControlPaint.DrawBorder(g, ClientRectangle, Color.FromArgb(0, 81,152), ButtonBorderStyle.Solid);
        }

        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public int Step
        {
            get { return _step; }
            set { _step = value; }
        }
    }
}
