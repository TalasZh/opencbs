using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Octopus.Services;
using Octopus.CoreDomain;


namespace Octopus.GUI.Configuration
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
