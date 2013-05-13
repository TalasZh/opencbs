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
using System.Drawing;
using System.Windows.Forms;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Clients;
using OpenCBS.CoreDomain.Contracts.Loans;
using OpenCBS.Services;
using OpenCBS.Shared;
using System.Linq;
using OpenCBS.MultiLanguageRessources;

namespace OpenCBS.GUI.Clients
{
    public partial class LoanSharesForm : Form
    {
        private readonly Loan _loan;
        private readonly OCurrency _total = 0;
        private readonly Group _group;

        public LoanSharesForm(Loan pLoan, Group pGroup)
        {
            InitializeComponent();
            _loan = pLoan;
            _group = pGroup;
            _total = _loan.LoanShares.Sum(x => x.Amount.Value);
            
            InitializeControls();
        }

        private void InitializeControls()
        {
            foreach (LoanShare ls in _loan.LoanShares)
            {
                ListViewItem item = new ListViewItem{Tag = ls, Text = ls.PersonName};
                ListViewItem.ListViewSubItem subItem = new ListViewItem.ListViewSubItem{Text = ls.Amount.GetFormatedValue(_loan.UseCents), Tag = ls.Amount};
                item.SubItems.Add(subItem);
                lvLoanShares.Items.Add(item);
            }
            string totalLabel=MultiLanguageStrings.GetString(Ressource.LoanSharesForm, "TotalLabel");

            ListViewItem sum = new ListViewItem(totalLabel);
            sum.Font = new Font(lvLoanShares.Font, FontStyle.Bold);
            sum.UseItemStyleForSubItems = false;
            sum.SubItems.Add("", Color.Black, Color.White, sum.Font);
            lvLoanShares.Items.Add(sum);
            _UpdateSum();
        }

        private void _UpdateSum()
        {
            OCurrency sum = 0;
            for (int i = 0; i < lvLoanShares.Items.Count - 1; i++)
            {
                OCurrency share = (OCurrency) lvLoanShares.Items[i].SubItems[1].Tag;
                sum += share;
            }
            int index = lvLoanShares.Items.Count - 1;
            Color fg = sum == _loan.Amount ? Color.Black : Color.Red;
            lvLoanShares.Items[index].SubItems[1].Text = sum.GetFormatedValue(_loan.UseCents);
            lvLoanShares.Items[index].SubItems[1].ForeColor = fg;
            btnOK.Enabled = !IsReadOnly && sum == _loan.Amount;
        }

        private void lvLoanShares_SubItemClicked(object sender, UserControl.SubItemEventArgs e)
        {
            if (1 == e.SubItem && !IsReadOnly && e.Item.Index < _loan.LoanShares.Count)
            {
                btnOK.Enabled = false;
                lvLoanShares.StartEditing(tbAmount, e.Item, e.SubItem);
            }
        }

        private void lvLoanShares_SubItemEndEditing(object sender, UserControl.SubItemEndEditingEventArgs e)
        {
            if (1 == e.SubItem)
            {
                OCurrency amount;
                OCurrency originalAmount = (OCurrency) e.Item.SubItems[e.SubItem].Tag;
                try
                {
                    amount = decimal.Parse(e.DisplayText);
                    amount = amount < 0 ?  originalAmount : amount;
                    amount = amount > _total ? originalAmount : amount;
                }
                catch
                {
                    amount = originalAmount;
                }
                e.DisplayText = amount.GetFormatedValue(_loan.UseCents);
                e.Item.SubItems[e.SubItem].Tag = amount;
                _UpdateSum();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lvLoanShares.Items.Count - 1; i++)
            {
                LoanShare ls = lvLoanShares.Items[i].Tag as LoanShare;
                ls.Amount = (OCurrency)lvLoanShares.Items[i].SubItems[1].Tag;
            }
        }

        private bool IsReadOnly
        {
            get
            {
                return _loan.Closed || _loan.Disbursed;
            }
        }
    }
}
