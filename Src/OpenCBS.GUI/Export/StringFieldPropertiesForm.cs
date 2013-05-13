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
    public partial class StringFieldPropertiesForm : Form
    {
        private StringFieldType _stringFieldType;

        public StringFieldPropertiesForm()
        {
            InitializeComponent();
        }

        public StringFieldType StringFieldType
        {
            get
            {
                _stringFieldType.AlignRight = checkBoxAlignRight.Checked;
                _stringFieldType.ReplacementList.Clear();
                try
                {
                    _stringFieldType.StartPosition = Convert.ToInt32(tnStartPosition.Text);
                    _stringFieldType.EndPosition = Convert.ToInt32(textBoxEndPosition.Text);
                }
                catch { }
                foreach (DataGridViewRow row in dgvReplacementList.Rows)
                {
                    if (!row.IsNewRow)
                        _stringFieldType.ReplacementList.Add(row.Cells[0].Value as string, row.Cells[1].Value as string);
                }
                return _stringFieldType;
            }
            set
            {
                _stringFieldType = value;
                tnStartPosition.Text = _stringFieldType.StartPosition.ToString();
                checkBoxAlignRight.Checked = _stringFieldType.AlignRight;
                textBoxEndPosition.Text = _stringFieldType.EndPosition.HasValue ? _stringFieldType.EndPosition.Value.ToString() : "*";
                foreach (var key in _stringFieldType.ReplacementList.Keys)
                {
                    dgvReplacementList.Rows.Add(key, _stringFieldType.ReplacementList[key]);
                }
            }
        }

        public int? EndPosition
        {
            get
            {
                try
                {
                    return Convert.ToInt32(textBoxEndPosition.Text);
                }
                catch
                {
                    return null;
                }
            }
            set
            {
                if (value != null)
                    textBoxEndPosition.Text = value.ToString();
                else
                    textBoxEndPosition.Text = "*";
            }
        }
    }
}
