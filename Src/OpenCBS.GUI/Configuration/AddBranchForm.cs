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
using System.Diagnostics;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.ExceptionsHandler;
using OpenCBS.GUI.UserControl;
using System.Windows.Forms;
using OpenCBS.Services;

namespace OpenCBS.GUI.Configuration
{
    public partial class AddBranchForm : SweetOkCancelForm
    {
        public bool IsNew { get; set; }
        public Branch Branch { get; set; }
        private string tempName;
        private string tempCode;
        private string tempAddress;
        private string tempDescription;

        public AddBranchForm()
        {
            InitializeComponent();
        }

        private void CheckName()
        {
            SetOkButtonEnabled(tbName.Text.Length > 0);
        }

        private void tbName_TextChanged(object sender, System.EventArgs e)
        {
            CheckName();
        }

        private void AddBranchForm_Load(object sender, System.EventArgs e)
        {
            Debug.Assert(Branch != null, "Branch is null");
            Text = IsNew ? GetString("add") : GetString("edit");
            if (IsNew)
            {
                tabControl.TabPages.Remove(tabPageAddPaymentMethod);
            }
            tempName = Branch.Name;
            tempCode = Branch.Code;
            tempAddress = Branch.Address;
            tempDescription = Branch.Description;
            tbName.Text = Branch.Name;
            tbCode.Text = Branch.Code;
            tbAddress.Text = Branch.Address;
            tbDescription.Text = Branch.Description;
            
            LoadPaymentMethods();
            
        }

        private void AddBranchForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult.OK != DialogResult) return;

            Branch.Name = tbName.Text;
            Branch.Code = tbCode.Text;
            Branch.Address = tbAddress.Text;
            Branch.Description = tbDescription.Text;
            if (Branch.Name != null)
                Branch.Name = Branch.Name.Trim();
            if (Branch.Code != null)
                Branch.Code = Branch.Code.Trim();
            if (Branch.Address != null)
                Branch.Address = Branch.Address.Trim();
            try
            {
                if (IsNew)
                {
                    ServicesProvider.GetInstance().GetBranchService().Add(Branch);
                }
                else
                {
                    ServicesProvider.GetInstance().GetBranchService().Update(Branch, tempName, tempCode, tempAddress, tempDescription);
                }
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
                e.Cancel = true;
            }
        }

        public void LoadPaymentMethods()
        {
            lvPaymentMethods.Items.Clear();
            List<PaymentMethod> methods =
                ServicesProvider.GetInstance().GetPaymentMethodServices().GetAllPaymentMethodsOfBranch(Branch.Id);
            foreach (PaymentMethod method in methods)
            {
                ListViewItem lvi = new ListViewItem { Tag = method };
                lvi.UseItemStyleForSubItems = false;
                lvi.SubItems.Add(method.Name);
                lvi.SubItems.Add(method.Account.ToString());
                lvi.SubItems.Add(method.Date.ToShortDateString());
                lvPaymentMethods.Items.Add(lvi);
            }
        }

        private void btnAddPaymentMethod_Click(object sender, EventArgs e)
        {
            AddPaymentMethodForm frm = new AddPaymentMethodForm(Branch);
            if (DialogResult.OK != frm.ShowDialog()) return;
            try
            {
                ServicesProvider.GetInstance().GetPaymentMethodServices().AddPaymentMethodToBranch(frm.PaymentMethod);
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
            LoadPaymentMethods();
        }

        private void btnDeletePaymentMethod_Click(object sender, EventArgs e)
        {
            if (lvPaymentMethods.SelectedItems.Count == 0)
                return;
            PaymentMethod paymentMethod = lvPaymentMethods.SelectedItems[0].Tag as PaymentMethod;
            Debug.Assert(paymentMethod != null, "Payment method not selected!");
            if (!Confirm("confirmDelete")) return;
                ServicesProvider.GetInstance().GetPaymentMethodServices().Delete(paymentMethod);
            
            LoadPaymentMethods();
        }

        private void btnEditPaymentMethod_Click(object sender, EventArgs e)
        {
            if (lvPaymentMethods.SelectedItems.Count == 0)
                return;
            PaymentMethod paymentMethod = (PaymentMethod)lvPaymentMethods.SelectedItems[0].Tag;
            Debug.Assert(paymentMethod != null, "Payment method not selected!");
            AddPaymentMethodForm frm = new AddPaymentMethodForm(paymentMethod);
            if (DialogResult.OK != frm.ShowDialog()) return;
            try
            {
                ServicesProvider.GetInstance().GetPaymentMethodServices().Update(frm.PaymentMethod);
            }
            catch(Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
            LoadPaymentMethods();
        }
    }
}
