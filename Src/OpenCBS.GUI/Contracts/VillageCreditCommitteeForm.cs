//Octopus MFS is an integrated suite for managing a Micro Finance Institution: clients, contracts, accounting, reporting and risk
//Copyright ?2006,2007 OCTO Technology & OXUS Development Network
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
using System.Drawing;
using System.Windows.Forms;
using Octopus.CoreDomain;
using Octopus.CoreDomain.Clients;
using Octopus.CoreDomain.Contracts.Loans;
using Octopus.Enums;
using Octopus.ExceptionsHandler;
using Octopus.GUI.UserControl;
using Octopus.MultiLanguageRessources;
using Octopus.Services;
using Octopus.Shared;

namespace Octopus.GUI.Contracts
{
    public partial class VillageCreditCommitteeForm : SweetBaseForm
    {
        private readonly Village _village;
        private bool _blockItemCheck;
        private DateTime _CreditCommitteeDate = TimeProvider.Today;
        
        private const int IdxAmount = 2;
        private const int IdxCurrency = 3;
        private const int IdxLoanOfficer = 4;
        private const int IdxStatus = 5;
        private const int IdxCreditCommitteeDate = 6;
        private const int IdxValidationCode = 7;
        private const int IdxComment = 8;
        
        public VillageCreditCommitteeForm(Village village)
        {
            _village = village;
            InitializeComponent();
            InitializeControls();
        }

        private void InitializeControls()
        {
            lvMembers.Items.Clear();
            Color dfc = Color.Gray;
            Color fc = Color.Black;
            Color bc = Color.White;
            
            foreach (VillageMember member in _village.Members)
            {
                if (member.ActiveLoans.Count == 0) continue;
                foreach (Loan loan in member.ActiveLoans)
                {
                    if (loan.ContractStatus == OContractStatus.Active)
                        continue;
                    Person person = (Person) member.Tiers;

                    ListViewItem item = new ListViewItem(person.Name) {Tag = loan};
                    item.SubItems.Add(person.IdentificationData);
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, "", loan.Amount.HasValue ? dfc : fc,
                                                                       bc, item.Font));
                    item.SubItems.Add(loan.Product.Currency.Code);
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, "", dfc, bc, item.Font));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, "", dfc, bc, item.Font));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, "", dfc, bc, item.Font));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, "", dfc, bc, item.Font));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, "", true ? dfc : fc, bc, item.Font));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, "", dfc, bc, item.Font));
                    lvMembers.Items.Add(item);
                }
                
               
            }
            cbStatus.Items.Add(GetContractStatusItem(OContractStatus.Pending).Value);
            cbStatus.Items.Add(GetContractStatusItem(OContractStatus.Postponed).Value);
            cbStatus.Items.Add(GetContractStatusItem(OContractStatus.Validated).Value);
            cbStatus.Items.Add(GetContractStatusItem(OContractStatus.Refused).Value);
            cbStatus.Items.Add(GetContractStatusItem(OContractStatus.Abandoned).Value);

            lvMembers.SubItemClicked += lvMembers_SubItemClicked;
            lvMembers.SubItemEndEditing += lvMembers_SubItemEndEditing;
            lvMembers.DoubleClickActivation = true;
        }

        private KeyValuePair<OContractStatus, string> GetContractStatusItem(OContractStatus oContractStatus)
        {
            string statusName = oContractStatus.GetName();
            string statusText = MultiLanguageStrings.GetString(Ressource.ClientForm,string.Format("{0}.Text", statusName));
            return new KeyValuePair<OContractStatus, string>(oContractStatus, statusText);
        }

        private void lvMembers_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            ListViewItem item = e.Item;
            item.Font = new Font("Arial", 9F, item.Checked ? FontStyle.Bold : FontStyle.Regular);
            foreach (ListViewItem.ListViewSubItem subitem in item.SubItems)
            {
                subitem.Font = item.Font;
            }
            if (item.Checked)
            {
                if (string.IsNullOrEmpty(item.SubItems[IdxAmount].Text)) // Amount
                {
                    item.SubItems[IdxAmount].Text = ((Loan)item.Tag).Amount.GetFormatedValue(false);
                }
                if (string.IsNullOrEmpty(item.SubItems[IdxCurrency].Text)) // Currency
                {
                    item.SubItems[IdxCurrency].Text = ((Loan)item.Tag).Product.Currency.Code;
                }
                
                if (string.IsNullOrEmpty(item.SubItems[IdxLoanOfficer].Text)) // Loan officer
                {
                    item.SubItems[IdxLoanOfficer].Text = _village.LoanOfficer.ToString();
                }

                var statusSubItem = item.SubItems[IdxStatus];
                if (string.IsNullOrEmpty(statusSubItem.Text)) // Status
                {
                    var loan = (Loan) item.Tag;
                    var items = cbStatus.Items;
                    if (loan.ContractStatus == OContractStatus.Pending)
                        statusSubItem.Text = items[0].ToString();
                    if (loan.ContractStatus == OContractStatus.Postponed)
                        statusSubItem.Text = items[1].ToString();
                    if (loan.ContractStatus == OContractStatus.Validated)
                        statusSubItem.Text = items[2].ToString();
                    if (loan.ContractStatus == OContractStatus.Refused)
                        statusSubItem.Text = items[3].ToString();
                    if (loan.ContractStatus == OContractStatus.Abandoned)
                        statusSubItem.Text = items[4].ToString();

                    statusSubItem.Tag = loan.ContractStatus;
                }

                if (string.IsNullOrEmpty(item.SubItems[IdxCreditCommitteeDate].Text)) // Credit Committee date
                {
                    item.SubItems[IdxCreditCommitteeDate].Text = TimeProvider.Today.ToShortDateString();
                    item.SubItems[IdxCreditCommitteeDate].Tag = TimeProvider.Today.ToShortDateString();
                }
            }
            else
            {
                for (int i = 2; i < item.SubItems.Count; i++)
                {
                    item.SubItems[i].Text = "";
                }
            }
        }

        private void lvMembers_SubItemClicked(object sender, SubItemEventArgs e)
        {
            if (e.Item.Checked)
            {
                switch (e.SubItem)
                {
                    case IdxStatus:
                        lvMembers.StartEditing(cbStatus, e.Item, e.SubItem);
                        break;

                    case IdxCreditCommitteeDate:
                        lvMembers.StartEditing(dtCreditCommittee, e.Item, e.SubItem);
                        break;

                    case IdxValidationCode:
                        lvMembers.StartEditing(tbValidationCode, e.Item, e.SubItem);
                        break;

                    case IdxComment:
                        lvMembers.StartEditing(tbComment, e.Item, e.SubItem);
                        break;

                    default:
                        break;
                }
            }
        }

        private void lvMembers_SubItemEndEditing(object sender, SubItemEndEditingEventArgs e)
        {
            var subItems = e.Item.SubItems;
            switch (e.SubItem)
            {
                case IdxStatus:
                    var items = cbStatus.Items;
                    if (cbStatus.SelectedItem == items[0])
                        subItems[IdxStatus].Tag = OContractStatus.Pending;
                    if (cbStatus.SelectedItem == items[1])
                        subItems[IdxStatus].Tag = OContractStatus.Postponed;
                    if (cbStatus.SelectedItem == items[2])
                        subItems[IdxStatus].Tag = OContractStatus.Validated;
                    if (cbStatus.SelectedItem == items[3])
                        subItems[IdxStatus].Tag = OContractStatus.Refused;
                    if (cbStatus.SelectedItem == items[4])
                        subItems[IdxStatus].Tag = OContractStatus.Abandoned;
                    break;

                case IdxCreditCommitteeDate:
                    subItems[e.SubItem].Tag = _CreditCommitteeDate;
                    break;

                case IdxValidationCode:
                    subItems[e.SubItem].Text = tbValidationCode.Text;
                    break;

                case IdxComment:
                    subItems[e.SubItem].Text = tbComment.SelectedText;
                    break;

                default:
                    break;
            }
        }

        // The two events below are necessary to prevent
        // the list view from ticking / unticking the checkbox
        // when the user double-clicks the row. The trick was found
        // here:
        // http://www.ncsystems.ru/en/programming/ListView_ItemCheck
        private void lvMembers_MouseDown(object sender, MouseEventArgs e)
        {
            if (MouseButtons.Left == e.Button)
            {
                if (e.Clicks >= 2)
                {
                    _blockItemCheck = true;
                }
            }
        }

        private void lvMembers_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (_blockItemCheck)
            {
                e.NewValue = e.CurrentValue;
                _blockItemCheck = !_blockItemCheck;
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewItem item in lvMembers.Items)
                {
                    if (!item.Checked) continue;
                    var loan = item.Tag as Loan;
                    string comment = item.SubItems[IdxComment].Text;
                    OContractStatus currentStatus = loan.ContractStatus;
                    OContractStatus status = (OContractStatus) item.SubItems[IdxStatus].Tag;
                    if (currentStatus==status) continue;
                    string code = item.SubItems[IdxValidationCode].Text;
                    DateTime date = Convert.ToDateTime(item.SubItems[IdxCreditCommitteeDate].Tag);

                    VillageMember activeMember = null;

                    foreach (VillageMember member in _village.Members)
                    {
                        int tIndex = member.ActiveLoans.IndexOf(loan);
                        if (tIndex > -1)
                        {
                            activeMember = member;
                        }
                    }

                    IClient client = activeMember.Tiers;

                    loan.CreditCommiteeDate = date;
                    loan.ContractStatus = status;
                    loan.CreditCommitteeCode = code;
                    loan.CreditCommiteeComment = comment;
                    Project project = client.Projects[0];
                    
                    loan = ServicesProvider.GetInstance().GetContractServices().UpdateContractStatus(loan, project, client, currentStatus==OContractStatus.Validated);
                    if (OContractStatus.Refused == status || 
                        OContractStatus.Abandoned == status ||
                        OContractStatus.Closed == status)
                    {
                        loan.Closed = true;
                        activeMember.ActiveLoans.Remove(loan);
                        if (activeMember.ActiveLoans.Count == 0)
                        {
                            client.Active = false;
                            client.Status = OClientStatus.Inactive;
                        }
                    }
                    else
                    {
                        loan.Closed = false;
                        client.Active = true;
                        client.Status = OClientStatus.Active;
                    }
                }
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dtCreditCommittee_ValueChanged(object sender, EventArgs e)
        {
            _CreditCommitteeDate = dtCreditCommittee.Value;
        }
    }
}
