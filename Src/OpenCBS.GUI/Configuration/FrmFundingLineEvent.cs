// LICENSE PLACEHOLDER

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
                    throw new OctopusFundingLineEventException(OctopusFundingLineEventExceptionEnum.DirectionIsEmpty);

                _fundingLineEvent.Movement = (OBookingDirections)((DictionaryEntry)comboBoxDirection.SelectedItem).Value;


                if (textBoxCode.Text == string.Empty)
                    throw new OctopusFundingLineEventException(OctopusFundingLineEventExceptionEnum.CodeIsEmpty);

                _fundingLineEvent.Code = textBoxCode.Text;

                if (textBoxAmount.Text == string.Empty)
                    throw new OctopusFundingLineEventException(OctopusFundingLineEventExceptionEnum.AmountIsEmpty);

                decimal amount;

                if (!decimal.TryParse(textBoxAmount.Text, out amount))
                    throw new OctopusFundingLineEventException(OctopusFundingLineEventExceptionEnum.AmountIsNonCompliant);

                if (amount >= 1000000000000000)
                    throw new OctopusFundingLineEventException(OctopusFundingLineEventExceptionEnum.AmountIsBigger);

                _fundingLineEvent.Amount = amount;

                if (_exchangeRate == null)
                {
                    throw new OctopusExchangeRateException(OctopusExchangeRateExceptionEnum.ExchangeRateIsNull);
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
                //if(ex is OctopusExchangeRateException)
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
