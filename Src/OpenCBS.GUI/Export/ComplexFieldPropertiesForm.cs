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
using OpenCBS.CoreDomain.Export.Files;
using OpenCBS.CoreDomain.Export.Fields;
using OpenCBS.CoreDomain.Export.FieldType;

namespace OpenCBS.GUI.Export
{
    public partial class ComplexFieldPropertiesForm : Form
    {
        public ComplexFieldPropertiesForm()
        {
            InitializeComponent();
        }

        public ComplexField ComplexField
        {
            get
            {
                return new ComplexField
                {
                    DisplayName = textBoxName.Text,
                    Name = textBoxName.Text,
                    Fields = fieldListUserControl.SelectedFields,
                    Length = _getLenght()
                };
            }
            set
            {
                textBoxName.Text = value.DisplayName;
                fieldListUserControl.SelectedFields = value.Fields;
            }
        }

        public List<IField> DefaultFields
        {
            set
            {
                fieldListUserControl.DefaultList = value;
            }
        }

        public bool UseSpecificLength
        {
            set { fieldListUserControl.UseSpecificLenght = value; }
        }

        public bool ExportMode
        {
            set { fieldListUserControl.ExportMode = value; }
        }

        private int? _getLenght()
        {
            int? length = 0;

            foreach (var field in fieldListUserControl.SelectedFields)
            {
                length += field.Length;
            }

            return length != 0 ? length + fieldListUserControl.SelectedFields.Count - 1 : null;
        }
    }
}
