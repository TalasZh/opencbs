using System;
using System.Drawing;
using System.Windows.Forms;

namespace Octopus.GUI.Database
{
    public partial class FrmDatabaseName : Form
    {
        private bool _badDatabaseName;
        private string _name;

        public string Result
        {
            get { return _name; }
        }

        public FrmDatabaseName()
        {
            InitializeComponent();
        }

        private void textBoxDatabaseName_TextChanged(object sender, EventArgs e)
        {
            _badDatabaseName = false;
            textBoxDatabaseName.BackColor = Color.White;
            if(string.IsNullOrEmpty(textBoxDatabaseName.Text))
            {
                _badDatabaseName = true;
                textBoxDatabaseName.BackColor = Color.Red;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (_badDatabaseName)
                MessageBox.Show("Error in database name", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                _name = textBoxDatabaseName.Text;
                Close();
            }
        }

        private void textBoxDatabaseName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonSave.PerformClick();
            }
        }
    }
}
