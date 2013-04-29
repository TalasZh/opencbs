using System;

namespace Octopus.GUI.UserControl
{
    public partial class SweetForm : SweetBaseForm
    {
        public SweetForm()
        {
            InitializeComponent();
        }

        public override string Text
        {
            get
            {
                return base.Text;
            }

            set
            {
                base.Text = value;
                if(lblTitle != null) lblTitle.Text = value;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
   }
}