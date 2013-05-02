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
    public partial class DateFieldPropertiesForm : Form
    {
        public DateFieldPropertiesForm()
        {
            InitializeComponent();
        }

        public DateFieldType DateFieldType
        {
            set
            {
                textBoxDateFormat.Text = value.StringFormat;
                checkBoxAlignRight.Checked = value.AlignRight;
            }
            get
            {
                return new DateFieldType
                {
                    StringFormat = textBoxDateFormat.Text,
                    AlignRight = checkBoxAlignRight.Checked
                };
            }
        }

        private void linkLabelDocumentation_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://msdn.microsoft.com/en-gb/library/8kb3ddd4(v=VS.95).aspx");
        }

        private void textBoxDateFormat_TextChanged(object sender, EventArgs e)
        {
            labelSampleValue.Text = DateTime.Today.ToString(textBoxDateFormat.Text);
        }
    }
}
