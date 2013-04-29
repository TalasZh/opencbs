using System;
using System.Drawing;
using System.Windows.Forms;

namespace Octopus.GUI
{
    public partial class InputBox : Form
    {
        private string _name;

        public InputBox()
        {
            InitializeComponent();
            CenterToParent();
        }

        public string Result
        {
            get { return _name; }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            _name = textBoxName.Text;
            Close();
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                textBoxName.BackColor = Color.Red;
                buttonOK.Enabled = false;
            }
            else
            {
                textBoxName.BackColor = Color.White;
                buttonOK.Enabled = true;
            }
        }
    }
}