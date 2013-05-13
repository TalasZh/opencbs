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
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using OpenCBS.ExceptionsHandler;
using OpenCBS.MultiLanguageRessources;
using OpenCBS.Shared;
using OpenCBS.Shared.Settings;
using ZedGraph;
using OpenCBS.Services;
using OpenCBS.CoreDomain.Accounting;

namespace OpenCBS.GUI.Accounting
{
	/// <summary>
	/// Summary description for ExchangeRate.
	/// </summary>
	public partial class ExchangeRateForm : Form
	{
	    private DateTime _date;
	    private int _currentMonth;
	    private int _currentYear;
	    private DateTime? _initializeDate;
	    private ExchangeRate _exchangeRate;
	    private List<Currency> _currencies;
	    private Currency pivotCurrency;
	    private Currency _currency;
	    private Point comboBoxCurrenciesLocation;
	    private Point textBoxRateValueLocation;
	    private Point labelSwappedLocation;
	    private Point labelExternalCurrencyLocation;
	    private Point labelInternalCurrencyLocation;
	    private Point buttonOKLocation;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		
        public ExchangeRate ExchangeRate
        {
            get { return _exchangeRate; }
        }

		public ExchangeRateForm()
		{
            InitializeComponent();
		   
		    _exchangeRate = null;
		    _initializeDate = null;
		    _currency = null;
		    _date = TimeProvider.Today;
            mCRate.SetDate(_date); 
		    _InitializeSecurity();
         }

		public ExchangeRateForm(DateTime pDate, Currency pCurrency)
		{
            InitializeComponent();
            _exchangeRate = null;
		    _initializeDate = pDate;
		    _currency = pCurrency;
            _date = pDate;
            if(!mCRate.SelectionStart.Date.Equals(_date.Date))
                mCRate.SetDate(_date);
            _InitializeSecurity();
           
		}
        private void _InitializeComboBoxCurrencies()
        {
            comboBoxCurrencies.Items.Clear();

            //if (_currency == null)
            //{
                List<Currency> currencies = ServicesProvider.GetInstance().GetCurrencyServices().FindAllCurrencies();

                foreach (Currency cur in currencies)
                {
                    if (cur.IsPivot)
                    {
                        pivotCurrency = cur;
                    }
                    else
                    {
                        comboBoxCurrencies.Items.Add(cur);
                        comboBoxCurrencies.SelectedItem = cur;
                    }
                }
            //}
            //else
            //{
            //    pivotCurrency = ServicesProvider.GetInstance().GetCurrencyServices().GetPivot();
            //    comboBoxCurrencies.Items.Add(_currency);
            //}
            if(comboBoxCurrencies.Items.Count>0) comboBoxCurrencies.SelectedIndex =0;
            _InitializeTextBoxLabels();
            
        }
        private void _InitializeSecurity()
        {
            //if (!User.CurrentUser.isVisitor && !User.CurrentUser.isCashier)
            //{
                _InitializeComboBoxCurrencies();
            //    return;
            //}
//            textBoxRateValue.Visible = false;
            //buttonOK.Visible = false; 
        }

		private void buttonOK_Click(object sender, EventArgs e)
		{
			SaveExchangeRate();
		}

		private void SaveExchangeRate()
		{
            try
            {
                Currency selectedCur = comboBoxCurrencies.SelectedItem as Currency;
                double amount;
                if(selectedCur.IsSwapped)
                    amount = 1/Convert.ToDouble(textBoxRateValue.Text);
                else
                    amount = Convert.ToDouble(textBoxRateValue.Text);
                ServicesProvider.GetInstance().GetExchangeRateServices().SaveRate(_date, amount, selectedCur);
                _Initialization(true);
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
            if (_initializeDate.HasValue)
            {
                //check whether exchange rate exists for all currencies
                foreach (Currency _cur in comboBoxCurrencies.Items)
                {
                    if (ServicesProvider.GetInstance().GetExchangeRateServices().SelectExchangeRate(
                            _initializeDate.Value, _cur) != null)
                        continue;
                    MessageBox.Show(MultiLanguageStrings.GetString(Ressource.ExchangeRateForm,
                                                                   "RateIsNullForCurrency.Text") + _cur.Name);
                    return;

                }
                _exchangeRate = _currency == null
                                    ? ServicesProvider.GetInstance().GetExchangeRateServices().SelectExchangeRate(
                                          _initializeDate.Value, comboBoxCurrencies.SelectedItem as Currency)
                                    : (_currency.IsPivot
                                           ? new ExchangeRate
                                                 {
                                                     Currency = _currency,
                                                     Date = _initializeDate.Value,
                                                     Rate = 1
                                                 }
                                           : ServicesProvider.GetInstance().GetExchangeRateServices().SelectExchangeRate
                                                 (
                                                 _initializeDate.Value, _currency)
                                      );
            
            }
		    Close();
		}

	    private void _InitializeCurrentMonth()
        {
            _currentMonth = _date.Month;
            _currentYear = _date.Year;
        }

	    private void _Initialization(bool pforceUpdate)
	    {
	        if (_currentMonth != _date.Month || _currentYear != _date.Year || pforceUpdate)
            {
                _InitializeCurrentMonth();
                InitializeListViewExchangeRate(_date);
                string[] abscisse = ServicesProvider.GetInstance().GetExchangeRateServices().CalculateDate(_date.Month, _date.Year);
                List<double[]> ordonnees = ServicesProvider.GetInstance().GetExchangeRateServices().CalculateCurve(_date.Month, _date.Year);
                InitializeGraphRateEvolution(abscisse, ordonnees);
                InitializeValuesForRatesList();
            }
            _InitializeCurrentRate();
        }

        private void _InitializeCurrentRate()
        {
            Currency selected = comboBoxCurrencies.SelectedItem as Currency;
            if(selected !=null)
            {
                ExchangeRate rate = ServicesProvider.GetInstance().GetExchangeRateServices().SelectExchangeRate(_date, selected);
                if (rate == null)
                {
                    textBoxRateValue.Text = "0";
                    buttonOK.Text = MultiLanguageStrings.GetString(Ressource.ExchangeRateForm, "save.Text");
                }
                else
                {
                    textBoxRateValue.Text = selected.IsSwapped? (1/rate.Rate).ToString() : rate.Rate.ToString();
                    buttonOK.Text = MultiLanguageStrings.GetString(Ressource.ExchangeRateForm, "update.Text");
                }
            }
        }

	    private void InitializeValuesForRatesList()
        {
            string month = CultureInfo.CreateSpecificCulture(UserSettings.Language).DateTimeFormat.GetMonthName(_date.Month);
            lbRateDetails.Text = string.Format("{0} ( {1} {2} )", MultiLanguageStrings.GetString(Ressource.ExchangeRateForm, "existingRate.text"), month, _date.Year);
            lbRateEvolution.Text = string.Format("{0} ( {1} {2} )", MultiLanguageStrings.GetString(Ressource.ExchangeRateForm, "rateEvolution.Text"), month, _date.Year);
	        _InitializeTextBoxLabels();
            _InitializeCurrentRate();
        }

        private void _InitializeTextBoxLabels()
        {
            Currency selected = comboBoxCurrencies.SelectedItem as Currency;
            labelSwapped.Visible = false;
            buttonOK.Enabled = true;
            if (pivotCurrency == null || comboBoxCurrencies.Items.Count < 1)
            {
                labelInternalCurrency.Visible = false;
                labelExternalCurrency.Visible = false;
//                textBoxRateValue.Visible = false;
                buttonOK.Enabled = false;
            }
            else if (selected != null)
            {
                labelInternalCurrency.Text = "1 " + (selected.IsSwapped ? selected.Name : pivotCurrency.Name) + " = ";
                labelExternalCurrency.Text = (selected.IsSwapped ? pivotCurrency.Name : selected.Name);
                labelSwapped.Visible = selected.IsSwapped;
                labelInternalCurrency.Visible = labelExternalCurrency.Visible = textBoxRateValue.Visible = true;
            }
        }

	    private void InitializeGraphRateEvolution(string[] pAbscisse, IEnumerable<double[]> pOrdonnees)
        {
            GraphPane myPane = zedGraphControlExchangeRateEvolution.GraphPane;

            // Set the title and axis labels
            myPane.Title = "";
            myPane.XAxis.Title = "";
            myPane.YAxis.Title = "";
            myPane.CurveList = new CurveList();
            myPane.FontSpec.FontColor = Color.FromArgb(0, 88, 56);
            // Make up some data points
            string[] labels = pAbscisse;


	        Color[] _colors =
	            {
	                Color.Chocolate,
	                Color.YellowGreen,
	                Color.Red,
	                Color.Black,
	                Color.Sienna,
	                Color.DarkGoldenrod,
	                Color.Maroon,
	                Color.OliveDrab
	            };
	        int i = 0;
            foreach (double[] ordonnee in pOrdonnees)
            {
                LineItem myCurve = myPane.AddCurve(_currencies.ToArray()[i].ToString(), null, ordonnee, _colors[i%_colors.Length], SymbolType.Circle);
                myCurve.Line.IsSmooth = false;
                i++;
            }
            
            myPane.AxisFill = new Fill(Color.White, Color.FromArgb(255, 255, 166), 90F);

            // Set the XAxis to Text type
            myPane.XAxis.Type = AxisType.Text;
            // Set the XAxis labels
            myPane.XAxis.TextLabels = labels;
            // Set the labels at an angle so they don't overlap
            myPane.XAxis.ScaleFontSpec.Angle = 40;
            myPane.XAxis.IsShowGrid = true;
            myPane.YAxis.IsShowGrid = true;
            myPane.PaneFill = new Fill(Color.White);
            // Calculate the Axis Scale Ranges
            zedGraphControlExchangeRateEvolution.AxisChange();
            zedGraphControlExchangeRateEvolution.Refresh();
        }

	    private void InitializeListViewExchangeRate(DateTime date)
        {
            lvExchangeRate.Items.Clear();
            lvExchangeRate.Columns.Clear();
            
            _currencies = ServicesProvider.GetInstance().GetCurrencyServices().FindAllCurrencies();
            
            lvExchangeRate.Columns.Add("date", "Date");

            DateTime startDate = new DateTime(date.Year, date.Month, 1);
            DateTime endDate = startDate.AddMonths(1);

            foreach (Currency currency in _currencies)
            {
                if (currency.IsPivot) continue;
                lvExchangeRate.Columns.Add(currency.Id.ToString(), currency.Name);

            }

            while (startDate < endDate)
            {
                List<ExchangeRate> _rates =
                    ServicesProvider.GetInstance().GetExchangeRateServices().SelectExchangeRateForAllCurrencies(startDate);
                ListViewItem item = new ListViewItem(startDate.ToShortDateString());
                for(int i = 0; i< _currencies.Count-1; i++)
                {
                    item.SubItems.Add("-");
                }
              
                if(_rates!=null)
                    foreach (ExchangeRate rate in _rates)
                    {
                        if (rate.Rate == 0) continue;
                        if (rate.Currency.IsPivot) continue;
                        int colInd = lvExchangeRate.Columns.IndexOfKey(rate.Currency.Id.ToString());

                        item.SubItems[colInd].Text = rate.Currency.IsSwapped
                                                         ? (1/rate.Rate).ToString()
                                                         : rate.Rate.ToString();
                    }
                startDate = startDate.AddDays(1);
                lvExchangeRate.Items.Add(item);
            }
        }

	    private void ExchangeRateForm_Load(object sender, EventArgs e)
        {
            _Initialization(false);
            comboBoxCurrenciesLocation = new Point(282, 21);
            textBoxRateValueLocation = new Point(343, 59);
            labelInternalCurrencyLocation = new Point(279,63);
            labelExternalCurrencyLocation = new Point(410, 63);
            labelSwappedLocation = new Point(282, 127);
            buttonOKLocation = new Point(282,160);
            
            comboBoxCurrencies.Location = comboBoxCurrenciesLocation;
            textBoxRateValue.Location = textBoxRateValueLocation;
	        labelExternalCurrency.Location = labelExternalCurrencyLocation;
	        labelInternalCurrency.Location = labelInternalCurrencyLocation;
            labelSwapped.Location = labelSwappedLocation;
	        buttonOK.Location = buttonOKLocation;
        }

        private void mCRate_DateChanged(object sender, DateRangeEventArgs e)
        {
            _date = mCRate.SelectionStart;
            _Initialization(false);
        }

        private void comboBoxCurrencies_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeValuesForRatesList();
        }
	}
}
