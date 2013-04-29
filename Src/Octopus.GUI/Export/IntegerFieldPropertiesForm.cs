using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Octopus.CoreDomain.Export;
using Octopus.CoreDomain.Export.FieldType;

namespace Octopus.GUI.Export
{
    public partial class IntegerFieldPropertiesForm : Form
    {
        public IntegerFieldPropertiesForm()
        {
            InitializeComponent();
        }

        public IntegerFieldType IntegerFieldType
        {
            set
            {
                checkBoxAlignRight.Checked = value.AlignRight;
                checkBoxDisplayZero.Checked = value.DisplayZeroBefore;
            }
            get
            {
                return new IntegerFieldType
                {
                    AlignRight = checkBoxAlignRight.Checked,
                    DisplayZeroBefore = checkBoxDisplayZero.Checked
                };
            }
        }
    }
}
