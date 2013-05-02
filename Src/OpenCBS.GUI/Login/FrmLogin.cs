// LICENSE PLACEHOLDER

using System;
using System.Windows.Forms;
using OpenCBS.CoreDomain;
using OpenCBS.MultiLanguageRessources;
using OpenCBS.Services;

namespace OpenCBS.GUI.Login
{
    public partial class FrmLogin : Form
    {
        private readonly string _userName;
        private readonly string _password;

        public FrmLogin(string pUserName, string pPassword)
        {
            InitializeComponent();

            _userName = pUserName;
            _password = pPassword;
        }

        private void OnLoad(object sender, EventArgs e)
        {
            if (_userName != null)
                textBoxUserName.Text = _userName;

            if (_password != null)
                textBoxPassword.Text = _password;

            textBoxUserName.Focus();
        }

        private void OnExitButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        private void OnOkButtonClick(object sender, EventArgs e)
        {
            if (!(UserIsValid()))
                SetUser();
            else
                MessageBox.Show(MultiLanguageStrings.GetString(
                    Ressource.PasswordForm,
                    "messageBoxUserPasswordBlank.Text"), "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SetUser()
        {
            User user = ServicesProvider.GetInstance().GetUserServices().Find(textBoxUserName.Text, textBoxPassword.Text);

            if (user == null)
            {
                MessageBox.Show(MultiLanguageStrings.GetString(Ressource.PasswordForm, "messageBoxUserPasswordIncorrect.Text"), "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                textBoxPassword.Focus();
                textBoxPassword.SelectAll();
            }
            else
            {
                User.CurrentUser = user;
                Close();
            }
        }

        private bool UserIsValid()
        {
            return string.IsNullOrEmpty(textBoxUserName.Text) || string.IsNullOrEmpty(textBoxPassword.Text);
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {

        }
    }
}
