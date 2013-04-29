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
