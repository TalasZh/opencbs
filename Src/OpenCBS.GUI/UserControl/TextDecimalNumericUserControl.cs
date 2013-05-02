using System;
using System.Globalization;
using System.Windows.Forms;

namespace OpenCBS.GUI.UserControl
{
    public partial class TextDecimalNumericUserControl : System.Windows.Forms.UserControl
    {
        public event EventHandler NumberChanged;

        public TextDecimalNumericUserControl()
        {
            InitializeComponent();
        }

        public override string Text
        {
            get { return textBoxNumeric.Text; }
            set { textBoxNumeric.Text = value; }
        }

        private void textBoxNumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            int keyCode = e.KeyChar;

            if ((keyCode >= 48 && keyCode <= 57) || (keyCode == 8) || (Char.IsControl(e.KeyChar) && e.KeyChar
                != ((char)Keys.V | (char)Keys.ControlKey)) || (Char.IsControl(e.KeyChar) && e.KeyChar !=
                ((char)Keys.C | (char)Keys.ControlKey)) || (e.KeyChar.ToString() == NumberFormatInfo.CurrentInfo.NumberDecimalSeparator))
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

        private void textBoxNumeric_TextChanged(object sender, EventArgs e)
        {
            if (NumberChanged != null)
                NumberChanged(this, e);
        }
    }
}
