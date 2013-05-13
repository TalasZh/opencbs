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
using System.Windows.Forms;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.ExceptionsHandler;
using OpenCBS.GUI.UserControl;
using OpenCBS.MultiLanguageRessources;
using OpenCBS.Services;
using OpenCBS.Shared;
using OpenCBS.Enums;

namespace OpenCBS.GUI.Accounting
{
    public partial class AddBooking : Form
    {
        private ElemMvtUserControl _elem;
        private ExchangeRate _rate;

        public AddBooking()
        {
            InitializeComponent();
            _elem = new ElemMvtUserControl();
            _elem.Dock = DockStyle.Fill;
            tabPageEntry.Controls.Add(_elem);

            cbBookings.ValueMember = "Id";
            cbBookings.DisplayMember = "";
            cbBookings.DataSource = ServicesProvider.GetInstance().GetStandardBookingServices().SelectAllStandardBookings();
            _InitializeComboBoxCurrencies();
            InitializeComboBoxBranches();
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

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (tabControl.SelectedTab == tabPageEntry)
                {
                    try
                    {
                        if (_elem.Save())
                        {

                            if (MessageBox.Show(
                                MultiLanguageStrings.GetString(Ressource.ElemMvtUserControl, "AccountSaved.Text"), "",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
                                _elem.InitializeControl();
                            else
                                Close();
                        }
                    }
                    catch(Exception ex)
                    {
                        new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
                    }
                }
                else
                {
                    _CheckExchangeRate();

                    var booking = (Booking)(cbBookings.SelectedItem);
                    booking.Amount = Convert.ToInt32(textBoxAmount.Text);
                    booking.Description = textBoxDescription.Text;
                    booking.ExchangeRate = _rate.Rate;
                    booking.Date = TimeProvider.Now;
                    booking.Currency = (Currency) cbCurrencies.SelectedItem;
                    booking.User = User.CurrentUser;
                    booking.Branch = cbBranches.SelectedItem as Branch;
                        
                    btnSave.Enabled = false;

                    ServicesProvider.GetInstance().GetAccountingServices().BookManualEntry(booking, User.CurrentUser);
                    ServicesProvider.GetInstance().GetEventProcessorServices().LogUser(OUserEvents.UserStandardBookingEvent, textBoxDescription.Text, User.CurrentUser.Id);
                    cbBookings.Text = "";
                    textBoxAmount.Text = "";
                    textBoxDescription.Text = "";

                    if (MessageBox.Show(MultiLanguageStrings.GetString(Ressource.ElemMvtUserControl, "AccountSaved.Text"), "", MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question) == DialogResult.Yes)
                        btnSave.Enabled = true;
                    else
                        Close();
                }
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
        }

        private void textBoxAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            int keyCode = e.KeyChar;

            if ((keyCode >= 48 && keyCode <= 57) 
                || (keyCode == 8) 
                || (Char.IsControl(e.KeyChar) 
                && e.KeyChar
                != ((char)Keys.V | (char)Keys.ControlKey)) 
                || (Char.IsControl(e.KeyChar) && e.KeyChar 
                != ((char)Keys.C | (char)Keys.ControlKey)) 
                || (e.KeyChar.ToString() == System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator))
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

        private void _CheckExchangeRate()
        {
            var selectedCur = cbCurrencies.SelectedItem as Currency;

            if (!ServicesProvider.GetInstance().GetCurrencyServices().GetPivot().Equals(selectedCur))
            {
                _rate = ServicesProvider.GetInstance().GetExchangeRateServices().SelectExchangeRate(DateTime.Now, selectedCur);
                
                while (_rate == null)
                {
                    var xrForm = new ExchangeRateForm(DateTime.Now, selectedCur);
                    xrForm.ShowDialog();
                    if (xrForm.ExchangeRate != null)
                    {
                        if (xrForm.ExchangeRate.Currency.Equals(selectedCur) &&
                            xrForm.ExchangeRate.Date.Day.Equals(DateTime.Now.Day))
                            _rate = xrForm.ExchangeRate;
                    }
                }
            }
            else
            {
                _rate = new ExchangeRate(){Rate = 1};
            }
        }

    }
}
