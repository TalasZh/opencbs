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
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using OpenCBS.Services;
using OpenCBS.CoreDomain;


namespace OpenCBS.GUI.Configuration
{
    public partial class frmUserSelection : Form
    {
        private List<User> _users;
        private readonly int _userId;


        public frmUserSelection(int userId)
        { 
            _userId = userId;
            InitializeComponent();
            _InitializeLoanOfficer();
           
        }

        private void _InitializeLoanOfficer()
        {
            cbUsers.Items.Clear();

            _users = ServicesProvider.GetInstance().GetUserServices().FindAll(false).OrderBy(item => item.FirstName).ThenBy(item => item.LastName).ToList();

            foreach (User user in _users)
            {
                if (!user.IsDeleted && (user.UserRole.IsRoleForLoan || user.UserRole.IsRoleForSaving))
                {
                    cbUsers.Items.Add(user);
                }
            }
            cbUsers.SelectedIndex = 0;
        }

        private int GetLoanOfficerId(string loanOfficer)
        {
            List<User> myUser = _users.FindAll(delegate(User officerId)
            {
                return officerId.Name == loanOfficer;
            });

            int Id = 0;
            myUser.ForEach(delegate(User officerId)
            {
                Id = officerId.Id;
            });
            return Id;
        }

        private void btnAssing_Click(object sender, EventArgs e)
        {
            var contracts = ServicesProvider.GetInstance().GetContractServices().FindContractsByOfficer(_userId);

            foreach (Alert contract in contracts)
            {
                ServicesProvider.GetInstance().GetContractServices().ReassignContract(contract.LoanId, GetLoanOfficerId(cbUsers.Text), _userId);
            }
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
