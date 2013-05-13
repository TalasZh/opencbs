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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenCBS.CoreDomain.Export;
using OpenCBS.CoreDomain.Export.FieldType;

namespace OpenCBS.GUI.Export
{
    public partial class DecimalFieldPropertiesForm : Form
    {
        public DecimalFieldPropertiesForm()
        {
            InitializeComponent();
        }

        public DecimalFieldType DecimalFieldType
        {
            set 
            {
                tnDecimalNumber.Text = value.DecimalNumber.ToString();
                textBoxDecimalSeparator.Text = value.DecimalSeparator;
                textBoxGroupSeparator.Text = value.GroupSeparator;
                checkBoxAlignRight.Checked = value.AlignRight;
            }
            get
            {
                var decimalFieldType = new DecimalFieldType
                {
                    DecimalSeparator = textBoxDecimalSeparator.Text,
                    GroupSeparator = textBoxGroupSeparator.Text,
                    AlignRight = checkBoxAlignRight.Checked
                };
                try
                {
                    decimalFieldType.DecimalNumber = Convert.ToInt32(tnDecimalNumber.Text);
                }
                catch { }

                return decimalFieldType;
            }
        }

        private void _refreshSample()
        {
            decimal d = 1250.45M;
            try
            {
                labelSampleValue.Text =  DecimalFieldType.Format(d, null);
            }
            catch {}
        }

        private void tnDecimalNumber_NumberChanged(object sender, EventArgs e)
        {
            _refreshSample();
        }

        private void textBoxDecimalSeparator_TextChanged(object sender, EventArgs e)
        {
            _refreshSample();
        }

        private void textBoxGroupSeparator_TextChanged(object sender, EventArgs e)
        {
            _refreshSample();
        }
    }
}
