using System.Drawing;
using System.Windows.Forms;

namespace Octopus.GUI.UserControl
{
    public class TextBoxLimit : TextBox
    {
        private Color _previousBackColor = Color.Empty;

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (Text.Length >= MaxLength)
            {
                if (!IsBackColorRemembered()) RememberBackColor();
                BackColor = Color.Red;
            }

            base.OnKeyPress(e);
        }

        protected override void OnTextChanged(System.EventArgs e)
        {
            base.OnTextChanged(e);
            if (Text.Length < MaxLength && IsBackColorRemembered())
            {
                SetPreviousBackColor();
            }
        }

        private void SetPreviousBackColor()
        {
            BackColor = _previousBackColor;
        }

        protected override void OnLostFocus(System.EventArgs e)
        {
            base.OnLostFocus(e);
            if (Text.Length <= MaxLength && IsBackColorRemembered())
            {
                SetPreviousBackColor();
            }
        }

        private void RememberBackColor()
        {
            _previousBackColor = BackColor;
        }

        private bool IsBackColorRemembered()
        {
            return _previousBackColor != Color.Empty;
        }
    }
}
