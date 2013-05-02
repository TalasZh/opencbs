// LICENSE PLACEHOLDER

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
