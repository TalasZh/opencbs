//Octopus MFS is an integrated suite for managing a Micro Finance Institution: clients, contracts, accounting, reporting and risk
//Copyright © 2006,2007 OCTO Technology & OXUS Development Network
//
//This program is free software; you can redistribute it and/or modify
//it under the terms of the GNU Lesser General Public License as published by
//the Free Software Foundation; either version 2 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU Lesser General Public License for more details.
//
//You should have received a copy of the GNU Lesser General Public License along
//with this program; if not, write to the Free Software Foundation, Inc.,
//51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
//
// Licence : http://www.octopusnetwork.org/OverviewLicence.aspx
//
// Website : http://www.octopusnetwork.org
// Business contact: business(at)octopusnetwork.org
// Technical contact email : tech(at)octopusnetwork.org 

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Octopus.CoreDomain;
using Octopus.GUI.Tools;
using Octopus.GUI.UserControl;
using Octopus.Services;
using Octopus.ExceptionsHandler;

namespace Octopus.GUI.Configuration
{
    public partial class UserForm : SweetForm
    {
        private User _user;

        public UserForm()
        {
            _user = new User();
            InitializeComponent();
            InitUsers();
        }

        private void InitializeComboBoxRoles()
        {
            cmbRoles.Items.Clear();
            List<Role> roles = ServicesProvider.GetInstance().GetRoleServices().FindAllRoles(false);

            foreach (Role _role in roles)
            {
                cmbRoles.Items.Add(_role);
            }
        }

        private void InitUsers()
        {
            lvUsers.Items.Clear();
            List<User> users = ServicesProvider.GetInstance().GetUserServices().FindAll(false);
            foreach (User u in users)
            {
                ListViewItem item = new ListViewItem
                {
                    Text = u.UserName
                    , Tag = u
                }; 
                item.SubItems.Add(u.UserRole.ToString());
                item.SubItems.Add(u.FirstName);
                item.SubItems.Add(u.LastName);
                item.SubItems.Add(u.Mail);
                item.SubItems.Add(u.Sex.GenderString());
                item.SubItems.Add(u.SubordinateCount.ToString());
                item.SubItems.Add(u.Phone);
                lvUsers.Items.Add(item);
            }
        }

        private void textBoxUsername_TextChanged(object sender, EventArgs e)
        {
            _user.UserName = ServicesHelper.CheckTextBoxText(txbUsername.Text);
        }

        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {
            _user.Password = ServicesHelper.CheckTextBoxText(txbPassword.Text);
        }

        private void textBoxFirstname_TextChanged(object sender, EventArgs e)
        {
            _user.FirstName = ServicesHelper.CheckTextBoxText(txbFirstname.Text);
        }

        private void textBoxLastname_TextChanged(object sender, EventArgs e)
        {
            _user.LastName = ServicesHelper.CheckTextBoxText(txbLastname.Text);
        }

        private void _ChangeAllLabelForeColor(Color pColor)
        {
            lblFirstName.ForeColor = pColor;
            lblLastName.ForeColor = pColor;
            lblPassword.ForeColor = pColor;
            lblRole.ForeColor = pColor;
            lblUsername.ForeColor = pColor;
            lblMail.ForeColor = pColor;
            lblPhone.ForeColor = pColor;
        }

        private void _InitializeControls(UserServices.UserErrors pUserErrors)
        {
            _ChangeAllLabelForeColor(Color.FromArgb(0, 88, 56));

            if (pUserErrors.FirstNameError)
                lblFirstName.ForeColor = Color.Red;
            if (pUserErrors.LastNameError)
                lblLastName.ForeColor = Color.Red;
            if (pUserErrors.LoginError)
                lblUsername.ForeColor = Color.Red;
            if (pUserErrors.PasswordError)
                lblPassword.ForeColor = Color.Red;
            if (pUserErrors.RoleError)
                lblRole.ForeColor = Color.Red;
            if (pUserErrors.MailError)
                lblRole.ForeColor = Color.Red;
        }

        private void SaveUser()
        {
            try
            {
                UserServices.UserErrors userErrors = ServicesProvider.GetInstance().GetUserServices().SaveUser(_user);
                if (userErrors.FindError)
                {
                    MessageBox.Show(userErrors.ResultMessage, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _InitializeControls(userErrors);
                }
                else
                {
                    InitUsers();
                    
                    foreach (ListViewItem item in lvUsers.Items)
                    {
                        if (((User)item.Tag).Id == _user.Id)
                        {
                            item.Selected = true;
                            listViewUsers_Click(this, new EventArgs());
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (txbConfirmPassword.Text == txbPassword.Text)
            {
                SaveUser();
            }
            else
            {
                txbPassword.BackColor = Color.Red;
                txbPassword.Focus();
                txbPassword.Text = "";
            }
            txbConfirmPassword.Text = "";
        }

        private void EraseUser()
        {
            _user = new User();
            InitializeUser();
        }

        private void comboBoxRoles_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _user.UserRole = (Role) cmbRoles.SelectedItem;
        }

        private void InitializeUser()
        {
            txbFirstname.Text = _user.FirstName;
            txbLastname.Text = _user.LastName;
            txbPassword.Text = _user.Password;
            txbUsername.Text = _user.UserName;
            txbPhone.Text = _user.Phone;
            txbMail.Text = _user.Mail;
            cmbRoles.Text = _user.UserRole.Id > 0 ? _user.UserRole.RoleName : "";
            cmbRoles.Enabled = true;
            cmbSex.SelectGender(_user.Sex);
        }

        private void OnDeleteClick(object sender, EventArgs e)
        {
            try
            {
                bool canErase = false;
                if (_user.HasContract)
                {
                    frmUserSelection selectionForm = new frmUserSelection(_user.Id);

                    if (DialogResult.OK == selectionForm.ShowDialog()) canErase = true;
                }
                else if (Confirm("confirmDelete"))
                {
                    canErase = true;
                }

                if (canErase)
                {
                    ((LotrasmicMainWindowForm)MdiParent).ReloadAlerts();
                    ServicesProvider.GetInstance().GetUserServices().Delete(_user);
                    InitUsers();
                    EraseUser();
                    Notify("deleted");
                }
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
            OnSelectedUserChanged(lvUsers, null);
        }

        private void listViewUsers_Click(object sender, EventArgs e)
        {
            _user = (User) lvUsers.SelectedItems[0].Tag;
            _ChangeAllLabelForeColor(Color.FromArgb(0, 88, 56));
            InitializeUser();
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            _ChangeAllLabelForeColor(Color.FromArgb(0, 88, 56));
            btnSubordinates.Enabled = false;
            EraseUser();
        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            InitializeComboBoxRoles();
            InitializeSex();
        }

        private void InitializeSex()
        {
            cmbSex.LoadGender();
        }

        private void textBoxMail_TextChanged(object sender, EventArgs e)
        {
            _user.Mail = ServicesHelper.CheckTextBoxText(txbMail.Text);
        }

        private void textBoxPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (txbPassword.BackColor == Color.White) return;
            txbPassword.BackColor = Color.White;
            txbPassword.ForeColor = Color.Black;
        }

        private void OnSelectedUserChanged(object sender, EventArgs e)
        {
            ListView lv = sender as ListView;
            Debug.Assert(lv != null, "Sender is null");
            bool sel = lv.SelectedItems.Count > 0;
            btnDelete.Enabled = sel;
            btnSubordinates.Enabled = sel;
            EraseUser();
        }

        private void OnSubordinatesClicked(object sender, EventArgs e)
        {
            UserServices us = ServicesProvider.GetInstance().GetUserServices();
            BranchService bs = ServicesProvider.GetInstance().GetBranchService();
            
            User boss = _user;
            Debug.Assert(boss != null, "User is null");
            EditUserForm frm = new EditUserForm
            {
                Boss = boss
                , AllUsers = us.FindAllExcept(boss, true)
                , AllBranches = bs.FindAllNonDeleted()
            };

            if (DialogResult.OK != frm.ShowDialog()) return;

            boss.ClearSubordinates();
            boss.AddSubordinates(frm.NewSubordinates());
            boss.ClearBranches();
            boss.AddBranches(frm.NewBranches());
            us.Save(boss);
            RefreshUser(boss);

            if (boss.Id != User.CurrentUser.Id) return;

            ((LotrasmicMainWindowForm)MdiParent).ReloadAlerts();
        }

        private void RefreshUser(User user)
        {
            foreach (ListViewItem item in lvUsers.Items)
            {
                if (!user.Equals(item.Tag as User)) continue;

                item.SubItems[6].Text = user.SubordinateCount.ToString();
            }
        }

        private void cmbSex_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _user.Sex = cmbSex.GetGender();
        }

        private void txbPhone_TextChanged(object sender, EventArgs e)
        {
            _user.Phone = ServicesHelper.CheckTextBoxText(txbPhone.Text);
        }
    }
}