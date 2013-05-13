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

using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using OpenCBS.Services;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.GUI.UserControl;

namespace OpenCBS.GUI.Configuration
{
    public partial class AddPaymentMethodForm : SweetOkCancelForm
    {
        private bool _isNew;
        private PaymentMethod _paymentMethod;
        private Branch branch;

        public PaymentMethod PaymentMethod
        {
            get
            {
                return _paymentMethod;
            }
        }

        public AddPaymentMethodForm()
        {
            InitializeComponent();
            Initialize(null);
        }

        public AddPaymentMethodForm(PaymentMethod paymentMethod)
        {
            InitializeComponent();
            Initialize(paymentMethod);
        }

        public AddPaymentMethodForm(Branch tBranch)
        {
            InitializeComponent();
            Initialize(null);
            branch = tBranch;
        }

        private void Initialize(PaymentMethod paymentMethod)
        {
            _isNew = paymentMethod == null;
            _paymentMethod = paymentMethod;

            List<PaymentMethod> methods = ServicesProvider.GetInstance().GetPaymentMethodServices().GetAllPaymentMethods();
            cmbPaymentMethod.Items.Clear();
            cmbPaymentMethod.ValueMember = "Id";
            cmbPaymentMethod.DisplayMember = "Name";
            cmbPaymentMethod.DataSource = methods;
            if (_paymentMethod != null && _paymentMethod.Name != null)
                cmbPaymentMethod.SelectedValue = _paymentMethod.Id;
            
            cmbAccount.Items.Clear();
            cmbAccount.ValueMember = "Number";
            cmbAccount.DisplayMember = "";
            cmbAccount.DataSource = ServicesProvider.GetInstance().GetChartOfAccountsServices().FindAllAccounts().OrderBy(item => item.Number).ToList();
            if (_paymentMethod != null && _paymentMethod.Account != null)
               cmbAccount.SelectedValue = _paymentMethod.Account.Number;
        }

        private void AddPaymentMethodForm_Load(object sender, System.EventArgs e)
        {
            Text = _isNew ? GetString("add") : GetString("edit");
            if (!_isNew)
            {
                cmbPaymentMethod.SelectedItem = _paymentMethod.Id;
                cmbAccount.SelectedItem = _paymentMethod.Account;
            }
        }

        private void AddPaymentMethodForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult.OK != DialogResult) return;
            PaymentMethod pm = cmbPaymentMethod.SelectedItem as PaymentMethod;
            _paymentMethod = new PaymentMethod
                                                   {
                                                       Id =
                                                           ServicesProvider.GetInstance().GetPaymentMethodServices().
                                                           GetPaymentMethodByName(pm.Name).Id,
                                                       Name = pm.Name,
                                                       Account = cmbAccount.SelectedItem as Account,
                                                       LinkId = _paymentMethod == null ? 0 : _paymentMethod.LinkId,
                                                       Branch = branch
                                                   };
            if (string.IsNullOrEmpty(_paymentMethod.Name))
            {
                Fail(GetString("NameIsEmpty.Text"));
                e.Cancel = true;
                return;
            }

            if (_paymentMethod.Account == null)
            {
                Fail(GetString("AccountIsEmpty.Text"));
                e.Cancel = true;
            }
        }
    }
}
