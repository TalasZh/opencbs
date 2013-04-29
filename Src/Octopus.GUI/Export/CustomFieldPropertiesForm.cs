using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Octopus.CoreDomain.Export;
using Octopus.CoreDomain.Export.Fields;

namespace Octopus.GUI.Export
{
    public partial class CustomFieldPropertiesForm : Form
    {
        private CustomField _customField;

        public CustomFieldPropertiesForm()
        {
            InitializeComponent();
        }

        public CustomField CustomField
        {
            get
            {
                _customField.Name = textBoxName.Text;
                _customField.DisplayName = textBoxName.Text;
                _customField.DefaultValue = textBoxDefaultValue.Text;
                return _customField;
            }
            set
            {
                textBoxDefaultValue.Text = value.DefaultValue;
                textBoxName.Text = value.DisplayName;
                _customField = value;
            }
        }
    }
}
