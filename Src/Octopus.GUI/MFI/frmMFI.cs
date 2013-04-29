using System;
using System.Windows.Forms;
using Octopus.Services;
using Octopus.ExceptionsHandler;

namespace Octopus.GUI.MFI
{
    public partial class frmMFI : Form
    {
        Octopus.CoreDomain.MFI _mfi;
        bool _update;
        bool _synchro = false;

        public frmMFI(bool pSynchro)
        {
            InitializeComponent();
            _synchro = pSynchro;          
            _update = false;

            _mfi = ServicesProvider.GetInstance().GetMFIServices().FindMFI();

            if (_mfi.Login != null)
            {
                textBoxLogin.Text = _mfi.Login;
                textBoxName.Text = _mfi.Name;
                textBoxPassword.Text = _mfi.Password;
                textBoxConfirmPassword.Text = _mfi.Password;

                _update = true;
            }
        }

        public frmMFI()
        {
            InitializeComponent();
            _update = false;
            _mfi = ServicesProvider.GetInstance().GetMFIServices().FindMFI();

            if (_mfi.Login != null)
            {
                textBoxLogin.Text = _mfi.Login;
                textBoxName.Text = _mfi.Name;
                textBoxPassword.Text = _mfi.Password;
                textBoxConfirmPassword.Text = _mfi.Password;

                _update = true;
            }
        }

        private void buttonValidate_Click(object sender, EventArgs e)
        {
            try
            {
                ServicesProvider.GetInstance().GetMFIServices().CheckIfSamePassword(textBoxPassword.Text, textBoxConfirmPassword.Text);

                _mfi.Name = textBoxName.Text;
                _mfi.Login = textBoxLogin.Text;
                _mfi.Password = textBoxPassword.Text;

                if (!_update)
                    ServicesProvider.GetInstance().GetMFIServices().CreateMFI(_mfi);

                else
                    ServicesProvider.GetInstance().GetMFIServices().UpdateMFI(_mfi);

                if (_synchro)
                {
                    // A rétablir

                    //MFISynchronize.MFISynchronize synchro = new MFISynchronize.MFISynchronize();
                    //synchro.SendSynchronizedMfi(User.CurrentUser);
                }

                this.Hide();

            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
