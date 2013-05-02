using System.Drawing;

namespace OpenCBS.GUI.UserControl
{
    public partial class SweetOkCancelForm : SweetBaseForm
    {
        public SweetOkCancelForm()
        {
            Font = SystemFonts.MessageBoxFont;
            InitializeComponent();
        }

        public void SetOkButtonEnabled(bool enabled)
        {
            btnOk.Enabled = enabled;
        }
    }
}
