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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using OpenCBS.CoreDomain.Contracts.Loans;
using OpenCBS.Enums;
using OpenCBS.ExceptionsHandler;
using OpenCBS.GUI.Accounting;
using OpenCBS.MultiLanguageRessources;
using OpenCBS.Services;
using OpenCBS.Shared;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Accounting;

namespace OpenCBS.GUI.UserControl
{
	/// <summary>
	/// Summary description for ElemMvtUserControl.
	/// </summary>
	public class ElemMvtUserControl : System.Windows.Forms.UserControl
	{
		private Label lblAmount;
		private Label lblAccountDebit;
        private ComboBox cbAccountDebit;
		private GroupBox groupBoxMvts;
		private Container components;
        private Label lblCurrency;
        private ComboBox cbAccountCredit;
        private Label lblAccountCredit;
		public new event EventHandler OnClick;
        private Label lblDescription;
        private TextBox txbDescription;
        private ComboBox cbCurrencies;

        private Account _debitAccount;
        private Account _creditAccount;
	    private OCurrency _amount;
	    private ExchangeRate _exchangeRate;
        private bool _deleteIsPossible;
        private TextBox _txbAmount;
        private Label blbBranch;
        private ComboBox cbBranches;
        private bool _selected;
	    private Branch _branch;

		public ElemMvtUserControl(Loan pContract, bool pDeleteIsPossible)
		{
			InitializeComponent();
		    _Initialization(pContract, pDeleteIsPossible);
		}

        public ElemMvtUserControl()
        {
            InitializeComponent();
            _Initialization(null, false);
        }
        private void _Initialization(Loan pContract, bool pDeleteIsPossible)
        {
            _deleteIsPossible = pDeleteIsPossible;
            InitializeComboBoxAccounts();
            groupBoxMvts.Click += groupBoxMvts_Click;
            _InitializeComboBoxCurrencies();
            InitializeComboBoxBranches();
        }
        public void InitializeControl()
        {
            InitializeComboBoxAccounts();
            _txbAmount.Text = "";
            cbAccountCredit.Text = "";
            cbAccountDebit.Text = "";
            _amount = null;
            _debitAccount = null;
            _creditAccount = null;
            _branch = null;
            txbDescription.Clear();
        }

        public bool DeleteIsPossible
        {
            get { return _deleteIsPossible;}
        }
        
        protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ElemMvtUserControl));
            this.lblAmount = new System.Windows.Forms.Label();
            this.lblAccountDebit = new System.Windows.Forms.Label();
            this.cbAccountDebit = new System.Windows.Forms.ComboBox();
            this.groupBoxMvts = new System.Windows.Forms.GroupBox();
            this._txbAmount = new System.Windows.Forms.TextBox();
            this.cbCurrencies = new System.Windows.Forms.ComboBox();
            this.blbBranch = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txbDescription = new System.Windows.Forms.TextBox();
            this.cbBranches = new System.Windows.Forms.ComboBox();
            this.cbAccountCredit = new System.Windows.Forms.ComboBox();
            this.lblAccountCredit = new System.Windows.Forms.Label();
            this.lblCurrency = new System.Windows.Forms.Label();
            this.groupBoxMvts.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblAmount
            // 
            resources.ApplyResources(this.lblAmount, "lblAmount");
            this.lblAmount.Name = "lblAmount";
            // 
            // lblAccountDebit
            // 
            resources.ApplyResources(this.lblAccountDebit, "lblAccountDebit");
            this.lblAccountDebit.Name = "lblAccountDebit";
            // 
            // cbAccountDebit
            // 
            this.cbAccountDebit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cbAccountDebit, "cbAccountDebit");
            this.cbAccountDebit.Name = "cbAccountDebit";
            // 
            // groupBoxMvts
            //
            this.groupBoxMvts.Controls.Add(this._txbAmount);
            this.groupBoxMvts.Controls.Add(this.cbCurrencies);
            this.groupBoxMvts.Controls.Add(this.blbBranch);
            this.groupBoxMvts.Controls.Add(this.lblDescription);
            this.groupBoxMvts.Controls.Add(this.txbDescription);
            this.groupBoxMvts.Controls.Add(this.cbBranches);
            this.groupBoxMvts.Controls.Add(this.cbAccountCredit);
            this.groupBoxMvts.Controls.Add(this.lblAccountCredit);
            this.groupBoxMvts.Controls.Add(this.lblCurrency);
            this.groupBoxMvts.Controls.Add(this.lblAmount);
            this.groupBoxMvts.Controls.Add(this.cbAccountDebit);
            this.groupBoxMvts.Controls.Add(this.lblAccountDebit);
            resources.ApplyResources(this.groupBoxMvts, "groupBoxMvts");
            this.groupBoxMvts.Name = "groupBoxMvts";
            this.groupBoxMvts.TabStop = false;
            // 
            // _txbAmount
            // 
            resources.ApplyResources(this._txbAmount, "_txbAmount");
            this._txbAmount.Name = "_txbAmount";
            this._txbAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbAmount_KeyPress);
            // 
            // cbCurrencies
            // 
            this.cbCurrencies.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cbCurrencies, "cbCurrencies");
            this.cbCurrencies.FormattingEnabled = true;
            this.cbCurrencies.Name = "cbCurrencies";
            this.cbCurrencies.SelectionChangeCommitted += new System.EventHandler(this.comboBoxCurrencies_SelectionChangeCommitted);
            // 
            // blbBranch
            // 
            resources.ApplyResources(this.blbBranch, "blbBranch");
            this.blbBranch.Name = "blbBranch";
            // 
            // lblDescription
            // 
            resources.ApplyResources(this.lblDescription, "lblDescription");
            this.lblDescription.BackColor = System.Drawing.Color.Transparent;
            this.lblDescription.Name = "lblDescription";
            // 
            // txbDescription
            // 
            resources.ApplyResources(this.txbDescription, "txbDescription");
            this.txbDescription.Name = "txbDescription";
            // 
            // cbBranches
            // 
            this.cbBranches.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cbBranches, "cbBranches");
            this.cbBranches.Name = "cbBranches";
            // 
            // cbAccountCredit
            // 
            this.cbAccountCredit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cbAccountCredit, "cbAccountCredit");
            this.cbAccountCredit.Name = "cbAccountCredit";
            // 
            // lblAccountCredit
            // 
            resources.ApplyResources(this.lblAccountCredit, "lblAccountCredit");
            this.lblAccountCredit.Name = "lblAccountCredit";
            // 
            // lblCurrency
            // 
            resources.ApplyResources(this.lblCurrency, "lblCurrency");
            this.lblCurrency.Name = "lblCurrency";
            // 
            // ElemMvtUserControl
            // 
            this.Controls.Add(this.groupBoxMvts);
            this.Name = "ElemMvtUserControl";
            resources.ApplyResources(this, "$this");
            this.groupBoxMvts.ResumeLayout(false);
            this.groupBoxMvts.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private void InitializeComboBoxAccounts()
        {
            cbAccountDebit.ValueMember = "Number";
            cbAccountDebit.DisplayMember = "";
		    cbAccountDebit.DataSource = ServicesProvider.GetInstance().GetChartOfAccountsServices().FindAllAccounts();

            cbAccountCredit.ValueMember = "Number";
            cbAccountCredit.DisplayMember = "";
            cbAccountCredit.DataSource = ServicesProvider.GetInstance().GetChartOfAccountsServices().FindAllAccounts();
		}

		public void SetFocus(bool val)
		{
			if (val)
			{
				_selected = true;
				groupBoxMvts.BackColor = Color.FromArgb(153, 204, 153);
			}
			else
			{
                _selected = false;
				groupBoxMvts.BackColor = Color.White;
			}
		}

		private void groupBoxMvts_Click(object sender, EventArgs e)
		{
            //SetFocus(true);
            //if (OnClick != null)
            //    OnClick(this, e);
		}

		public bool Selected
		{
            get { return _selected; }
		}

        public bool Save()
        {
            if (string.IsNullOrEmpty(_txbAmount.Text))
            {
                string caption = MultiLanguageStrings.GetString("SweetBaseForm", "error");
                string message = MultiLanguageStrings.GetString(Ressource.ElemMvtUserControl, "manualEntryInvalidAmount.Text");
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            _amount = ServicesHelper.ConvertStringToNullableDecimal(_txbAmount.Text);
            
            _GetAccounts();
            _CheckExchangeRate();
            _GetBranches();
            
                Booking booking = new Booking
                                      {
                                          Description = txbDescription.Text,
                                          Amount = _amount.Value,
                                          DebitAccount = _debitAccount,
                                          CreditAccount = _creditAccount,
                                          Date = TimeProvider.Now,
                                          Currency = (Currency)cbCurrencies.SelectedItem,
                                          ExchangeRate = _exchangeRate.Rate,
                                          Branch = _branch,
                                          User = User.CurrentUser
                                      };

                ServicesProvider.GetInstance().GetAccountingServices().BookManualEntry(booking, User.CurrentUser);
                ServicesProvider.GetInstance().GetEventProcessorServices().LogUser(OUserEvents.UserManualEntryEvent, txbDescription.Text, User.CurrentUser.Id);
                
                _deleteIsPossible = false;

            return true;
        }
        private void _InitializeComboBoxCurrencies()
        {
            cbCurrencies.Items.Clear();
            cbCurrencies.Text = MultiLanguageStrings.GetString(Ressource.FrmAddLoanProduct, "Currency.Text");
            List<Currency> currencies = ServicesProvider.GetInstance().GetCurrencyServices().FindAllCurrencies();
            cbCurrencies.ValueMember = "Id";
            cbCurrencies.DisplayMember = "";
            cbCurrencies.DataSource = currencies;
        } 

        private void InitializeComboBoxBranches()
        {
            cbBranches.Items.Clear();
            List<Branch> branches = ServicesProvider.GetInstance().GetBranchService().FindAllNonDeleted();
            cbBranches.ValueMember = "Id";
            cbBranches.DisplayMember = "";
            cbBranches.DataSource = branches;
        }

        private void comboBoxCurrencies_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbCurrencies.SelectedItem != null)
            {
                _CheckExchangeRate();
            }
        }

        private void _GetBranches()
        {
            Branch branch = cbBranches.SelectedItem as Branch;
            if (branch != null)
            {
                _branch = branch;
            }
            else
            {
                _branch = null;
            }
        }

        private void _GetAccounts()
        {
            if (cbAccountCredit.SelectedItem != null)
                _creditAccount = ((Account)cbAccountCredit.SelectedItem);
            else
                _creditAccount = null;

            if (cbAccountDebit.SelectedItem != null)
                _debitAccount = ((Account)cbAccountDebit.SelectedItem);
            else
                _debitAccount = null;
        }

        private void _CheckExchangeRate()
        {
            Currency selectedCur = cbCurrencies.SelectedItem as Currency;

            if (!selectedCur.IsPivot)
            {
                _exchangeRate = ServicesProvider.GetInstance().GetExchangeRateServices().SelectExchangeRate(
                    DateTime.Now, selectedCur);
                while (_exchangeRate == null)
                {
                    ExchangeRateForm xrForm = new ExchangeRateForm(DateTime.Now, selectedCur);
                    xrForm.ShowDialog();
                    if (xrForm.ExchangeRate != null)
                    {
                        if (xrForm.ExchangeRate.Currency.Equals(selectedCur) &&
                            xrForm.ExchangeRate.Date.Day.Equals(DateTime.Now.Day))
                            _exchangeRate = xrForm.ExchangeRate;
                    }
                }
            }
            else
            {
                _exchangeRate = new ExchangeRate {Rate = 1};
            }
        }

        private void txbAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            int keyCode = e.KeyChar;

            if ((keyCode >= 48 && keyCode <= 57) || (keyCode == 8) || (Char.IsControl(e.KeyChar) && e.KeyChar
                != ((char)Keys.V | (char)Keys.ControlKey)) || (Char.IsControl(e.KeyChar) && e.KeyChar !=
                ((char)Keys.C | (char)Keys.ControlKey)) || (e.KeyChar.ToString() ==
                System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator))
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }
	}
}
