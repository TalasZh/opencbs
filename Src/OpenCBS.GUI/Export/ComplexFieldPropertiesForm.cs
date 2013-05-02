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
