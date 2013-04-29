using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Octopus.Services;
using Octopus.MultiLanguageRessources;
using Octopus.CoreDomain;
using Octopus.Enums;

namespace Octopus.GUI.Contracts
{
    public partial class ReassignContractsForm : Form
    {
        private List<User> users;
        private List<Alert> alertStock;
        public ReassignContractsForm()
        {
            InitializeComponent();
            _InitializeLoanOfficer();
            alertStock = InitializerContracts(0);
            cbLoanOfficerFrom.SelectedIndex = 0;
            cbLoanOfficerTo.SelectedIndex = 0;
        }


        private void _InitializeLoanOfficer()
        {
            cbLoanOfficerFrom.Items.Clear();

            users = ServicesProvider.GetInstance().GetUserServices().FindAll(false).OrderBy(item => item.FirstName).ThenBy(item => item.LastName).ToList();
            
            foreach (User user in users)
            {
                cbLoanOfficerFrom.Items.Add(user);

                if (!user.IsDeleted && (user.UserRole.IsRoleForLoan || user.UserRole.IsRoleForSaving))
                {
                    cbLoanOfficerTo.Items.Add(user);
                }
            }
        }

        private List<Alert> InitializerContracts(int officerId)
        {
            bool onlyActive = chkBox_only_active.Checked;
            listViewAlert.Items.Clear();

            alertStock = ServicesProvider.GetInstance().GetContractServices().FindContractsByOfficerWAct(officerId, onlyActive);

            foreach (Alert alert in alertStock)
            {
                listViewAlert.Items.Add(CreateListViewItem(alert));
            }

            toolStripStatusLabelTotal.Text = String.Format("Total contracts: {0}", listViewAlert.Items.Count);
            return alertStock;
        }

        private ListViewItem CreateListViewItem(Alert alert)
        {
            ListViewItem listViewItem = new ListViewItem(alert.LoanCode){
                                                    Tag = alert.LoanId,
                                                    ImageIndex = (alert.Type == 'D' ? 1 : 0)
                                                };
            listViewAlert.CheckBoxes = true;

            listViewItem.SubItems.Add(alert.EffectDate.ToShortDateString());
            listViewItem.SubItems.Add(alert.Amount.GetFormatedValue(alert.UseCents));
            listViewItem.SubItems.Add(alert.ClientName);
            listViewItem.SubItems.Add(alert.DistrictName);
            listViewItem.SubItems.Add(alert.StartDate.ToShortDateString());
            listViewItem.SubItems.Add(alert.CloseDate.ToShortDateString());
            listViewItem.SubItems.Add(alert.CreationDate.ToShortDateString());
            listViewItem.SubItems.Add(alert.InstallmentTypes);
            listViewItem.SubItems.Add(alert.InterestRate.ToString());
            listViewItem.SubItems.Add(alert.OLB.GetFormatedValue(alert.UseCents));
            listViewItem.SubItems.Add(alert.LoanId.ToString());
            
            if (alert.Color == OAlertColors.Color1)
                listViewItem.BackColor = Color.FromArgb(226, 0, 26);
            else if (alert.Color == OAlertColors.Color2)
                listViewItem.BackColor = Color.FromArgb(255, 92, 92);
            else if (alert.Color == OAlertColors.Color3)
                listViewItem.BackColor = Color.FromArgb(255, 187, 120);
            else if (alert.Color == OAlertColors.Color4)
                listViewItem.BackColor = Color.FromArgb(147, 181, 167);
            else if (alert.Color == OAlertColors.Color5)
                listViewItem.BackColor = Color.FromArgb(188, 209, 199);
            else if (alert.Color == OAlertColors.Color6)
                listViewItem.BackColor = Color.FromArgb(217, 229, 223);
            else listViewItem.BackColor = Color.White;

            return listViewItem;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
            Dispose();
        }

        private int GetLoanOfficerID(string loanOfficer)
        {
            List<User> myUser = users.FindAll(delegate(User officerId)
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

        private void cbLoanOfficerFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializerContracts(GetLoanOfficerID(cbLoanOfficerFrom.Text));
            textBoxContractFilter.Text="";
            checkBoxAll.Checked = false;
        }

        private void cbLoanOfficerTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLoanOfficerFrom.Text == cbLoanOfficerTo.Text)
            {
                cbLoanOfficerTo.SelectedIndex = 0;
            }
            checkBoxAll.Checked = false;
        }

        private void buttonAssing_Click(object sender, EventArgs e)
        {
            bool isCheked = false;
            if ((cbLoanOfficerTo.Text.Length > 0) && (cbLoanOfficerFrom.Text.Length>0 ))
            {

                foreach (ListViewItem item in listViewAlert.Items)
                {
                    if (item.Checked)
                    {
                        isCheked = true;
                        ServicesProvider.GetInstance().GetContractServices().ReassignContract(Convert.ToInt32(item.Tag), GetLoanOfficerID(cbLoanOfficerTo.Text), GetLoanOfficerID(cbLoanOfficerFrom.Text));

                    }
                }
                if (!isCheked)
                {
                    MessageBox.Show(MultiLanguageStrings.GetString(Ressource.ReassingContract, "selectContract.Text"),
                            MultiLanguageStrings.GetString(Ressource.ReassingContract, "title.Text"), MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                }
                InitializerContracts(GetLoanOfficerID(cbLoanOfficerFrom.Text));
            }
            else
            {
                MessageBox.Show(MultiLanguageStrings.GetString(Ressource.ReassingContract, "dataerror.Text"),
                        MultiLanguageStrings.GetString(Ressource.ReassingContract, "title.Text"), MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
            }
            
        }

        private void listViewAlert_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewAlert.Items)
            {
                item.Checked = item.Selected;
            }
        }

        private void checkBoxAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewAlert.Items)
            {
                item.Checked = checkBoxAll.Checked;
            }
        }

        private void textBoxContractFilter_TextChanged(object sender, EventArgs e)
        {
            _FilterContracts(textBoxContractFilter.Text);
        }

        private void _FilterContracts(String filter)
       {
          if (filter != null || !filter.Equals(""))
          {
             filter = filter.ToUpper();
             
             listViewAlert.Items.Clear();

             foreach (Alert alert in alertStock)
             {
                if (alert.DistrictName.ToUpper().Contains(filter) ||
                    alert.ClientName.ToUpper().Contains(filter) ||
                    alert.LoanCode.ToUpper().Contains(filter))
                {
                   listViewAlert.Items.Add(CreateListViewItem(alert));
                }
             }
          }
          toolStripStatusLabelTotal.Text = String.Format("Total contracts: {0}", listViewAlert.Items.Count);

       }

        private void chkBox_only_active_CheckedChanged(object sender, EventArgs e)
        {
            InitializerContracts(GetLoanOfficerID(cbLoanOfficerFrom.Text));
        }
    }
}
