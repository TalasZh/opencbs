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
using System.Windows.Forms;
using OpenCBS.Shared;
using OpenCBS.CoreDomain.SearchResult;
using OpenCBS.CoreDomain.Products;

namespace OpenCBS.GUI.Contracts
{
    public partial class CloseSavingsForm : Form
    {
        private OCurrency _amount;
        private OCurrency _closeFees;
        private readonly SavingsBookProduct _savingsBookProduct;

        public bool IsWithdraw
        {
            get { return rbWithdraw.Checked; }
        }

        private bool UseCents
        {
            get 
            {
                return _savingsBookProduct == null || _savingsBookProduct.Currency.UseCents;
            }
        }

        public bool IsTransfer
        {
            get { return rbTransfer.Checked; }
        }

        public bool IsDesactivateCloseFees
        {
            get { return checkBoxDesactivateFees.Checked; }
        }

        public OCurrency Amount
        {
            get
            {
                if (checkBoxDesactivateFees.Checked)
                    return _amount;
                else
                    return _amount - _closeFees;
            }
        }

        public OCurrency CloseFees
        {
            get { return _closeFees; }
        }

        public SavingSearchResult ToSaving { get; private set; }

        public CloseSavingsForm(OCurrency pBalance)
        {
            InitializeComponent();

            lbTotalAmountValue.Text = pBalance.GetFormatedValue(UseCents);
            gbCloseFees.Enabled = false;
            _amount = pBalance;
            _closeFees = 0;

            Initialize();
        }

        public CloseSavingsForm(ISavingProduct savingBookProduct, OCurrency pBalance, OCurrency pCloseFees)
        {
            InitializeComponent();
            
            _amount = pBalance;
            _closeFees = pCloseFees;
            
            if (savingBookProduct is SavingsBookProduct)
                _savingsBookProduct = (SavingsBookProduct)savingBookProduct;
            lbTotalAmountValue.Text = pBalance.GetFormatedValue(UseCents);
            gbCloseFees.Enabled = (_closeFees.Value > 0);

            Initialize();
        }

        

        private void Initialize()
        {
            udCloseFees.DecimalPlaces = UseCents ? 2 : 0;

            if (_savingsBookProduct.CloseFees.HasValue)
            {
                udCloseFees.Minimum = _closeFees.Value;
                udCloseFees.Maximum = _closeFees.Value;
                udCloseFees.Value = _closeFees.Value;

                lbCloseFeesMinMax.Text = string.Format("{0} {1}", 
                    _savingsBookProduct.CloseFees.GetFormatedValue(UseCents),
                    _savingsBookProduct.Currency.Code);
            }
            else
            {
                udCloseFees.Minimum = _savingsBookProduct.CloseFeesMin.Value;
                udCloseFees.Maximum = _savingsBookProduct.CloseFeesMax.Value;
                if (   _closeFees.Value >= udCloseFees.Minimum 
                    && _closeFees.Value <= udCloseFees.Maximum)
                    udCloseFees.Value = _closeFees.Value;
                else
                    udCloseFees.Value = udCloseFees.Minimum;

                lbCloseFeesMinMax.Text = string.Format("{0}{1} {4}\r\n{2}{3} {4}",
                       "Min: ", _savingsBookProduct.CloseFeesMin.GetFormatedValue(UseCents),
                       "Max: ", _savingsBookProduct.CloseFeesMax.GetFormatedValue(UseCents),
                       _savingsBookProduct.Currency.Code);
            }
        }

        private void btSearchContract_Click(object sender, EventArgs e)
        {
            SearchCreditContractForm searchCreditContractForm = SearchCreditContractForm.GetInstance(null);
            searchCreditContractForm.BringToFront();
            searchCreditContractForm.WindowState = FormWindowState.Normal;
            if (searchCreditContractForm.ShowForSearchSavingsContractForTransfer("") == DialogResult.OK)
            {
                ToSaving = searchCreditContractForm.SelectedSavingContract;
                lbClientName.Text = ToSaving.ClientName;
                tbTargetAccount.Text = ToSaving.ContractCode;
            }
        }

        private void rbTransfer_CheckedChanged(object sender, EventArgs e)
        {
            plTransfer.Visible = rbTransfer.Checked;
        }

        private void checkBoxDesactivateFees_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDesactivateFees.Checked)
            {
                lbTotalAmountValue.Text = _amount.GetFormatedValue(UseCents);
                udCloseFees.Value = udCloseFees.Minimum;
                udCloseFees.Enabled = false;
                labelCloseFeesValue.Text = "0";
            }
            else
            {
                lbTotalAmountValue.Text = _amount.GetFormatedValue(UseCents);
                labelCloseFeesValue.Text = _closeFees.GetFormatedValue(UseCents);
                udCloseFees.Value = udCloseFees.Minimum;
                udCloseFees.Enabled = true;
            }
            udCloseFees_ValueChanged(sender, e);
        }

        private void udCloseFees_ValueChanged(object sender, EventArgs e)
        {
            if (checkBoxDesactivateFees.Checked) return;
            OCurrency fees = udCloseFees.Value;
            labelCloseFeesValue.Text = fees.GetFormatedValue(UseCents);
            lbTotalAmountValue.Text = (_amount - fees).GetFormatedValue(UseCents);
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            _closeFees = udCloseFees.Value;
        }
    }
}
