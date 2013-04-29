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
