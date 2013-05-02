using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenCBS.CoreDomain;
using OpenCBS.GUI.UserControl;
using OpenCBS.MultiLanguageRessources;

namespace OpenCBS.GUI.Tools
{
    public partial class PasswordForm : SweetOkCancelForm
    {
        public User User { get; set; }
        public string NewPassword;
        public PasswordForm()
        {
            InitializeComponent();
        }

        public PasswordForm(User user)
        {
            InitializeComponent();
            User = user;
            textBoxUserName.Text = User.UserName;
        }

        private void PasswordForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != DialogResult.OK) return;
            if (User.Password != textBoxOldPswd.Text)
            {
                Fail("oldPasswordIsWrong");
                e.Cancel = true;
            }
            else if (textBoxNewPswd.Text.Length<4 || textBoxNewPswd.Text.Length>30)
            {
                Fail("littleOrBigPassword");
                e.Cancel = true;
            }
            else if (!string.Equals(textBoxNewPswd.Text, textBoxConfirmNewPswd.Text))
            {
                Fail("newPasswordIsWrong");
                e.Cancel = true;
            }
            NewPassword = textBoxNewPswd.Text;
        }
    }
}
