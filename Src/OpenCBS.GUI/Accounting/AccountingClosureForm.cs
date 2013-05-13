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
using BrightIdeasSoftware;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.Services;

namespace OpenCBS.GUI.Accounting
{
    public partial class AccountingClosureForm : Form
    {
        private void LoadClosureData()
        {
            olvClosures.SetObjects(ServicesProvider.GetInstance().GetAccountingServices().SelectAccountingClosures());
        }

        public AccountingClosureForm()
        {
            InitializeComponent();
            LoadClosureData();
        }

        private void BtnGenerateEventsClick(object sender, EventArgs e)
        {
            AccountingJournals accountingJournals = new AccountingJournals(1);
            accountingJournals.ShowDialog();
        }

        private void BtnAddRuleClick(object sender, EventArgs e)
        {
            AccountingJournals accountingJournals = new AccountingJournals(0);
            accountingJournals.ShowDialog();
            olvClosures.SetObjects(ServicesProvider.GetInstance().GetAccountingServices().SelectAccountingClosures());
        }

        private void SweetButton1Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnViewClick(object sender, EventArgs e)
        {
            if (olvClosures.SelectedObject != null)
            {
                ClosureBookings closureBookings =
                    new ClosureBookings(((AccountingClosure) olvClosures.SelectedObject).Id);
                closureBookings.ShowDialog();
            }
        }

        private void olvClosures_DoubleClick(object sender, EventArgs e)
        {
            if (olvClosures.SelectedObject != null)
            {
                ClosureBookings closureBookings =
                    new ClosureBookings(((AccountingClosure)olvClosures.SelectedObject).Id);
                closureBookings.ShowDialog();
            }
        }

        private void btnDeleteRule_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to delete closure?", "Closure", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                if (olvClosures.SelectedObject != null)
                {
                    ServicesProvider.GetInstance().GetAccountingServices().DeleteClosure(
                        ((AccountingClosure) olvClosures.SelectedObject).Id);
                    LoadClosureData();
                }
            }
        }

        private void olvClosures_FormatRow(object sender, FormatRowEventArgs e)
        {
            AccountingClosure closure = (AccountingClosure)e.Model;

            if(closure.Deleted)
                e.Item.ForeColor = Color.Gray;
        }
    }
}
