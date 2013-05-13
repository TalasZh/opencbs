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
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Events.Teller;
using OpenCBS.ExceptionsHandler;
using OpenCBS.Services;
using OpenCBS.GUI.UserControl;
using OpenCBS.Shared;

namespace OpenCBS.GUI.TellerManagement
{
    public partial class FrmOpenCloseTeller : SweetBaseForm
    {
        private readonly bool _isOpen;
        private Teller _teller;
        private bool _canClose;

        public OpenOfDayAmountEvent OpenOfDayAmountEvent { get; set; }
        public CloseOfDayAmountEvent CloseOfDayAmountEvent { get; set; }
        public OpenAmountPositiveDifferenceEvent OpenAmountPositiveDifferenceEvent { get; set; }
        public OpenAmountNegativeDifferenceEvent OpenAmountNegativeDifferenceEvent { get; set; }
        public CloseAmountPositiveDifferenceEvent CloseAmountPositiveDifferenceEvent { get; set; }
        public CloseAmountNegativeDifferenceEvent CloseAmountNegativeDifferenceEvent { get; set; }
        
        public Teller Teller
        {
            get
            {
                return _teller;
            }
        }
        
        public FrmOpenCloseTeller(bool isOpen)
        {
            InitializeComponent();
            _isOpen = isOpen;
            Text = isOpen ? GetString("openTellerText") : GetString("closeTellerText");
            InitializeTellersComboBox();
            labelCurrency.Text = ServicesProvider.GetInstance().GetCurrencyServices().GetPivot().Code;
            if (isOpen)
                OpenOfDayAmountEvent = new OpenOfDayAmountEvent();
            else
                CloseOfDayAmountEvent = new CloseOfDayAmountEvent();
        }

        private void InitializeTellersComboBox()
        {
            cmbTellers.ValueMember = "Id";
            cmbTellers.DisplayMember = "";
            
            cmbTellers.DataSource = 
                ServicesProvider.GetInstance().GetTellerServices().
                FindAllNonDeletedTellersOfUser(User.CurrentUser).
                OrderBy(item => item.Name).ToList();
            if (!_isOpen)
            {
                cmbTellers.Enabled = false;
                cmbTellers.SelectedItem = Teller.CurrentTeller;
            }
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            Close();    
        }

        private bool Check(Teller teller)
        {
            return teller.MinAmountTeller < teller.MaxAmountTeller && teller.MaxAmountTeller > 0;
        }

        private bool CheckIfTellerAmountIsValid(OCurrency amount, Teller teller)
        {
            return teller.MinAmountTeller <= amount && amount <= teller.MaxAmountTeller;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                OCurrency amount = decimal.Parse(tbAmount.Text);
                if (_isOpen)
                {
                    foreach (Teller teller in ServicesProvider.GetInstance().GetTellerServices().FindAllNonDeletedTellersOfUser(User.CurrentUser))
                    {
                        if ((int)cmbTellers.SelectedValue == (int)teller.Id)
                        {
                            _teller = teller;
                        }
                    }
                    if (Check(_teller) && !CheckIfTellerAmountIsValid(amount, _teller))
                        throw new Exception(GetString("AmountShouldBeBetweenValues.Text"));
                    GenerateTellerEvents(amount, _isOpen);
                }
                else
                {
                    if (Check(Teller.CurrentTeller) && !CheckIfTellerAmountIsValid(amount, _teller))
                        throw new Exception(GetString("AmountShouldBeBetweenValues.Text"));
                    GenerateTellerEvents(amount, _isOpen);
                    Notify(Teller.CurrentTeller.Name + " " + GetString("tellerIsClosedText"));
                }
                _canClose = true;
                Close();
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
        }

        private void GenerateTellerEvents(OCurrency amount, bool isOpen)
        {
            if (isOpen)
            {
                OpenOfDayAmountEvent.Amount = amount;
                OpenOfDayAmountEvent.TellerId = _teller.Id;
                OpenOfDayAmountEvent.Date = TimeProvider.Now;

                OCurrency expectedOpeningBalance =
                    ServicesProvider.GetInstance().GetTellerServices().GetTellerBalance(_teller);
                
                OCurrency cashAmount = decimal.Parse(tbAmount.Text);

                if (expectedOpeningBalance < cashAmount)
                {
                    OpenAmountPositiveDifferenceEvent = new OpenAmountPositiveDifferenceEvent();
                    OpenAmountPositiveDifferenceEvent.TellerId = _teller.Id;
                    OpenAmountPositiveDifferenceEvent.Date = TimeProvider.Now;
                    OpenAmountPositiveDifferenceEvent.Amount = cashAmount - expectedOpeningBalance;
                }
                else if (expectedOpeningBalance > cashAmount)
                {
                    OpenAmountNegativeDifferenceEvent = new OpenAmountNegativeDifferenceEvent();
                    OpenAmountNegativeDifferenceEvent.TellerId = _teller.Id;
                    OpenAmountNegativeDifferenceEvent.Date = TimeProvider.Now;
                    OpenAmountNegativeDifferenceEvent.Amount = expectedOpeningBalance - cashAmount;
                }
            }
            else
            {
                CloseOfDayAmountEvent.Amount = amount;
                CloseOfDayAmountEvent.TellerId = Teller.CurrentTeller.Id;
                CloseOfDayAmountEvent.Date = TimeProvider.Now;

                OCurrency excpectedClosingBalance =
                   ServicesProvider.GetInstance().GetTellerServices().GetTellerBalance(_teller);
                excpectedClosingBalance +=
                    ServicesProvider.GetInstance().GetTellerServices().GetLatestTellerOpenBalance(_teller);
                

                OCurrency cashAmount = decimal.Parse(tbAmount.Text);
                if (excpectedClosingBalance < cashAmount)
                {
                    CloseAmountPositiveDifferenceEvent = new CloseAmountPositiveDifferenceEvent();
                    CloseAmountPositiveDifferenceEvent.Amount = cashAmount - excpectedClosingBalance;
                    CloseAmountPositiveDifferenceEvent.TellerId = Teller.CurrentTeller.Id;
                    CloseAmountPositiveDifferenceEvent.Date = TimeProvider.Now;

                }
                else if (excpectedClosingBalance > cashAmount)
                {
                    CloseAmountNegativeDifferenceEvent = new CloseAmountNegativeDifferenceEvent();
                    CloseAmountNegativeDifferenceEvent.Amount = excpectedClosingBalance - cashAmount;
                    CloseAmountNegativeDifferenceEvent.TellerId = Teller.CurrentTeller.Id;
                    CloseAmountNegativeDifferenceEvent.Date = TimeProvider.Now;
                }
            }
        }

        private void cmbTellers_SelectedValueChanged(object sender, System.EventArgs e)
        {
            if (cmbTellers.SelectedValue != null)
            {
                foreach (Teller teller in ServicesProvider.GetInstance().GetTellerServices().FindAllNonDeletedTellersOfUser(User.CurrentUser))
                {
                    if ((int) cmbTellers.SelectedValue == (int) teller.Id)
                    {
                        _teller = teller;
                    }
                }
            }
        }

        private void FrmOpenCloseTeller_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_canClose) e.Cancel = true;
        }

        private static OCurrency CheckAmount(TextBox textBox, bool useOccurrency, bool allowNegativeNumber)
        {
            textBox.BackColor = Color.White;
            try
            {
                if (String.IsNullOrEmpty(textBox.Text.Trim()))
                    return null;
                if (!allowNegativeNumber && Convert.ToDecimal(textBox.Text) < 0)
                    throw new Exception();

                return Convert.ToDecimal(textBox.Text);
            }
            catch (Exception)
            {
                textBox.BackColor = Color.Red;
                return null;
            }
        }

        private void tbAmount_TextChanged(object sender, EventArgs e)
        {
            CheckAmount(tbAmount, true, false);
        }

        private void tbAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            int keyCode = e.KeyChar;

            if (
                (keyCode >= 48 && keyCode <= 57) ||
                (keyCode == 8) ||
                (Char.IsControl(e.KeyChar) && e.KeyChar != ((char)Keys.V | (char)Keys.ControlKey))
                ||
                (Char.IsControl(e.KeyChar) && e.KeyChar != ((char)Keys.C | (char)Keys.ControlKey))
                ||
                (e.KeyChar.ToString() == NumberFormatInfo.CurrentInfo.NumberDecimalSeparator)
                )
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

    }
}
