// Octopus MFS is an integrated suite for managing a Micro Finance Institution: 
// clients, contracts, accounting, reporting and risk
// Copyright © 2006,2007 OCTO Technology & OXUS Development Network
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License along
// with this program; if not, write to the Free Software Foundation, Inc.,
// 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
// Website: http://www.opencbs.com
// Contact: contact@opencbs.com

using System;
using System.Windows.Forms;
using OpenCBS.Services;
using OpenCBS.ExceptionsHandler;

namespace OpenCBS.GUI.MFI
{
    public partial class frmMFI : Form
    {
        OpenCBS.CoreDomain.MFI _mfi;
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
