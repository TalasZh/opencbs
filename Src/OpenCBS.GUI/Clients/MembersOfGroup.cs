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
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using OpenCBS.CoreDomain.Clients;
using OpenCBS.CoreDomain.Contracts.Loans;
using OpenCBS.Enums;
using OpenCBS.Shared;

namespace OpenCBS.GUI.Clients
{
    public partial class MembersOfGroup : Form
    {
        private OCurrency _memberAmount = 0;
        private readonly Loan _loan;
        private readonly DateTime _paymentDate;

        public OCurrency MemberRemainingAmount 
        { 
            get
            {
                return _memberAmount;
            }
            set
            {
                _memberAmount = value;
            }
        }

        public Member Member { get; set; }

        public MembersOfGroup()
        {
            InitializeComponent();
        }

        public MembersOfGroup(List<Member> pMembers, Loan pLoan, DateTime pDate)
        {
            InitializeComponent();
            _loan = pLoan;
            _paymentDate = pDate;
            OCurrency olb = _loan.CalculateActualOlb();
            Member leader = null;
            int roundTo = _loan.UseCents ? 2 : 0;
            OCurrency loanAmount =
                _loan.Events.GetLoanRepaymentEvents().Where(
                    rpe => rpe.RepaymentType == OPaymentType.PersonTotalPayment && !rpe.Deleted).Aggregate(
                        _loan.Amount, (current, rpe) => current - rpe.Principal);

            foreach (Member person in pMembers)
            {
                OCurrency olbByPerson = 0;
                OCurrency actualOlb = _loan.CalculateActualOlb();

                foreach (LoanShare loanShare in _loan.LoanShares)
                {
                    if (loanShare.PersonId == person.Tiers.Id && person.CurrentlyIn)
                    {
                        olbByPerson = actualOlb*loanShare.Amount/loanAmount;
                    }
                }
                olb -= Math.Round(olbByPerson.Value, roundTo);
                // Define the list items
                if (!person.IsLeader)
                {
                    Color color = person.CurrentlyIn ? Color.Black : Color.Silver;
                    ListViewItem lvi = new ListViewItem {Tag = person, Text = ((Person) person.Tiers).Name};
                    lvi.UseItemStyleForSubItems = false;
                    lvi.ForeColor = color;
                    lvi.SubItems.Add(olbByPerson.GetFormatedValue(_loan.UseCents));
                    listViewMembers.Items.Add(lvi);
                }
                else
                {
                    leader = person;
                    leader.LoanShareAmount = olbByPerson;
                }
            }

            if (leader != null)
            {
                leader.LoanShareAmount += olb;
                Color color = leader.CurrentlyIn ? Color.Red : Color.Silver;
                ListViewItem lvi = new ListViewItem { Tag = leader, Text = ((Person)leader.Tiers).Name };
                lvi.UseItemStyleForSubItems = false;
                lvi.ForeColor = color;
                lvi.SubItems.Add(leader.LoanShareAmount.GetFormatedValue(_loan.UseCents));
                listViewMembers.Items.Add(lvi);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (listViewMembers.SelectedItems.Count > 0)
            {
                Member member = (Member) listViewMembers.SelectedItems[0].Tag;

                if (!member.IsLeader && member.CurrentlyIn)
                {
                    OCurrency loanShareAmount = 0;

                    foreach (LoanShare loanShare in _loan.LoanShares)
                    {
                        if (loanShare.PersonId == member.Tiers.Id)
                        {
                            loanShareAmount = loanShare.Amount;
                        }
                    }

                    member.LoanShareAmount = loanShareAmount;
                    member.CurrentlyIn = false;
                    _loan.EscapedMember = member;
                    _memberAmount =
                        _loan.CalculateMaximumAmountForEscapedMember(
                            _loan.NbOfInstallments - _loan.NbOfInstallmentsNotRepaid + 1,
                            _paymentDate, false, 0, 0, false, 0, false, loanShareAmount);
                    
                    Member = member;
                    Close();
                }
            }
        }

        private void listViewMembers_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnOK_Click(sender, null);
        }
    }
}
