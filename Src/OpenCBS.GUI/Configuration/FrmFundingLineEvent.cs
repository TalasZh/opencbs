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
using System.Collections;
using System.Windows.Forms;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.Enums;
using OpenCBS.ExceptionsHandler;
using OpenCBS.ExceptionsHandler.Exceptions.FundingLineExceptions;
using OpenCBS.GUI.Accounting;
using OpenCBS.MultiLanguageRessources;
using OpenCBS.Services;
using OpenCBS.Shared;
using OpenCBS.CoreDomain.FundingLines;

namespace OpenCBS.GUI.Configuration
{
   public partial class FrmFundingLineEvent : Form
   {
        private FundingLineEvent _fundingLineEvent=null;
        private FundingLine _FundingLine;
        private ExchangeRate _exchangeRate;

        public FrmFundingLineEvent(FundingLine pFundingLine)
        {
            InitializeComponent();
            _FundingLine = pFundingLine;
            _exchangeRate=null;
            _fundingLineEvent = new FundingLineEvent();
            InitializeComboBoxDirections();
            dateTimePickerEvent.Value = TimeProvider.Now;
        }

        public FrmFundingLineEvent(FundingLine pFundingLine, FundingLineEvent pFundingLineEvent)
        {
            InitializeComponent();
            _FundingLine = pFundingLine;
            _fundingLineEvent = pFundingLineEvent;
            InitializeComboBoxDirections();
            dateTimePickerEvent.Value = TimeProvider.Now;
        }

        public FundingLineEvent FundingLineEvent
        {
            get { return _fundingLineEvent; }
        }

        private void InitializeComboBoxDirections()
        {
            comboBoxDirection.Items.Clear();
            comboBoxDirection.Items.Add(new DictionaryEntry(
              MultiLanguageStrings.GetString(Ressource.FrmFundingLineEvent, "Debit.Text"), OBookingDirections.Debit));
            comboBoxDirection.Items.Add(new DictionaryEntry(
               MultiLanguageStrings.GetString(Ressource.FrmFundingLineEvent, "Credit.Text"), OBookingDirections.Credit));
            comboBoxDirection.SelectedIndex = 1;
        }

        private bool _saved;

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                _saved = true;

                if (comboBoxDirection.SelectedIndex == -1)
                    throw new OpenCbsFundingLineEventException(OpenCbsFundingLineEventExceptionEnum.DirectionIsEmpty);

                _fundingLineEvent.Movement = (OBookingDirections)((DictionaryEntry)comboBoxDirection.SelectedItem).Value;


                if (textBoxCode.Text == string.Empty)
                    throw new OpenCbsFundingLineEventException(OpenCbsFundingLineEventExceptionEnum.CodeIsEmpty);

                _fundingLineEvent.Code = textBoxCode.Text;

                if (textBoxAmount.Text == string.Empty)
                    throw new OpenCbsFundingLineEventException(OpenCbsFundingLineEventExceptionEnum.AmountIsEmpty);

                decimal amount;

                if (!decimal.TryParse(textBoxAmount.Text, out amount))
                    throw new OpenCbsFundingLineEventException(OpenCbsFundingLineEventExceptionEnum.AmountIsNonCompliant);

                if (amount >= 1000000000000000)
                    throw new OpenCbsFundingLineEventException(OpenCbsFundingLineEventExceptionEnum.AmountIsBigger);

                _fundingLineEvent.Amount = amount;

                if (_exchangeRate == null)
                {
                    throw new OpenCbsExchangeRateException(OpenCbsExchangeRateExceptionEnum.ExchangeRateIsNull);
                }
                _fundingLineEvent.CreationDate = dateTimePickerEvent.Value;
                _fundingLineEvent.FundingLine = _FundingLine;
                _fundingLineEvent.Type = OFundingLineEventTypes.Entry;


                Close();
            }
            catch (Exception ex)
            {
                _saved = false;
                SetExchangeRate();
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
                //if(ex is OpenCbsExchangeRateException)
                //{
                //    ExchangeRateForm _xrForm = new ExchangeRateForm(new DateTime(_fundingLineEvent.CreationDate.Year, _fundingLineEvent.CreationDate.Month, _fundingLineEvent.CreationDate.Day), _fundingLineEvent.FundingLine.Currency);
                //    _xrForm.ShowDialog();
                //    if (_xrForm.ExchangeRate != null)
                //    {
                //        buttonSave.Enabled = true;
                //    }
                //    else buttonSave.Enabled = false;
                //}
            }
        }

       private void SetExchangeRate()
       {
           _exchangeRate = null;
           if (ServicesProvider.GetInstance().GetCurrencyServices().FindAllCurrencies().Count == 1)
           {
               _exchangeRate = new ExchangeRate
               {
                   Currency = _FundingLine.Currency,
                   Date = dateTimePickerEvent.Value,
                   Rate = 1
               };
           }
           buttonSave.Enabled = false;

           DateTime _date = dateTimePickerEvent.Value;
           try
           {
               if (!ServicesProvider.GetInstance().GetExchangeRateServices().RateExistsForEachCurrency
                   (ServicesProvider.GetInstance().GetCurrencyServices().FindAllCurrencies(), _date.Date)
                   /*&& (User.CurrentUser.HasAdminRole || User.CurrentUser.HasSuperAdminRole)*/)
               {
                   buttonAddRate.Enabled = true;
                   var _xrForm = 
                       new ExchangeRateForm(new DateTime(dateTimePickerEvent.Value.Year, _date.Month, _date.Day), 
                           _FundingLine.Currency);
                   _xrForm.ShowDialog();
               }
               _exchangeRate = ServicesProvider.GetInstance().GetAccountingServices().FindExchangeRate(_date.Date, _FundingLine.Currency);
           }
           catch (Exception ex)
           {
               new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
           }
           finally
           {
               if (_exchangeRate != null)
               {
                   buttonSave.Enabled = true;
                   buttonAddRate.Visible = false;
               }
               else
               {
                   buttonSave.Enabled = false;
                   //buttonAddRate.Enabled = User.CurrentUser.isAdmin || User.CurrentUser.isSuperAdmin;
                   buttonAddRate.Enabled = true;
               }

           }
       }
      private void buttonCancel_Click(object sender, EventArgs e)
      {
         _fundingLineEvent = null;
         _saved = false;
         Close();
      }

      private void frmFundingLineEvent_FormClosing(object sender, FormClosingEventArgs e)
      {
         if (!_saved)
            _fundingLineEvent = null;
      }

      private void textBoxAmount_KeyPress(object sender, KeyPressEventArgs e)
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

      private void buttonAddRate_Click(object sender, EventArgs e)
      {
          SetExchangeRate();
      }

      private void dateTimePickerEvent_ValueChanged(object sender, EventArgs e)
      {
          SetExchangeRate();
      }
   }
}
