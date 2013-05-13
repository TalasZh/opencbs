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
using OpenCBS.CoreDomain;
using OpenCBS.ExceptionsHandler;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.Manager.Currencies;

namespace OpenCBS.Services.Currencies
{
    [Security()]
    public class ExchangeRateServices : ContextBoundObject
    {
        private readonly ExchangeRateManager _rateManager;
        private readonly CurrencyManager _currencyManager;
        private User _user;

        public ExchangeRateServices(User pUser)
        {
            _rateManager = new ExchangeRateManager(pUser);
            _currencyManager = new CurrencyManager(pUser);
            _user = pUser;
        }

        public ExchangeRateServices(string pTestDb)
        {
            _rateManager = new ExchangeRateManager(pTestDb);
            _currencyManager = new CurrencyManager(pTestDb);
        }

        public Dictionary<DateTime, List<double>> SelectExchangeRatesForASpecifiedMonth(int pMonth, int pYear, List<Currency> pCurrencies)
        {
            Dictionary<DateTime, List<double>> rates = new Dictionary<DateTime, List<double>>();

            DateTime date = new DateTime(pYear, pMonth, 1);
            DateTime endDate = date.AddMonths(1);
            while (date < endDate)
            {
                List<double> list = new List<double>();
                foreach (Currency currency in pCurrencies)
                {
                    ExchangeRate rate = _rateManager.Select(date, currency) ?? new ExchangeRate { Date = date, Rate = 0 };
                    list.Add(rate.Rate);
                }
                rates.Add(date, list);
                date = date.AddDays(1);
            }
            return rates;
        }

        public ExchangeRate SelectExchangeRate(DateTime pDate,Currency pCurrency)
        {
            return _rateManager.Select(pDate, pCurrency);
        }

        public bool RateExistsForEachCurrency(List<Currency> pCurrencies, DateTime pDate)
        {
            foreach(Currency _cur in pCurrencies)
            {
                if (_cur.IsPivot) continue;
                if(_rateManager.Select(pDate, _cur)==null)
                    return false;
            }
            return true;
        }

        public List<ExchangeRate> SelectExchangeRateForAllCurrencies(DateTime pDate)
        {
            return _rateManager.SelectExchangeRateForAllCurrencies(pDate);
        }

        public List<ExchangeRate> SelectRatesByDate(DateTime beginDate, DateTime endDate)
        {
            //set the date to the end of day to select all events for the day
            endDate.AddHours(23);
            endDate.AddMinutes(59);
            endDate.AddSeconds(59);
            return _rateManager.SelectRatesByDate(beginDate, endDate);
        }

        public string[] CalculateDate(int pMonth, int pYear)
        {
            DateTime date = new DateTime(pYear, pMonth, 1);
            DateTime endDate = date.AddMonths(1);

            string[] list = new string[(endDate - date).Days];
            int i = 0;
            while (date < endDate)
            {
                list[i] = (date.ToShortDateString());
                date = date.AddDays(1);
                i++;
            }
            return list;
        }

        public List<double[]> CalculateCurve(int pMonth, int pYear)
        {
            List<double[]> curve = new List<double[]>();
            List<Currency> currencies = new CurrencyServices(_user).FindAllCurrencies();
            DateTime date = new DateTime(pYear, pMonth, 1);
            DateTime endDate = date.AddMonths(1);

            foreach (Currency currency in currencies)
            {
                double[] realAmount = new double[(endDate - date).Days];
                int i = 0;
                while (date < endDate)
                {
                    ExchangeRate rate = _rateManager.Select(date, currency);
                    realAmount[i] = (rate == null) ? 0 : rate.Rate;
                    date = date.AddDays(1);
                    i++;
                }
                date = new DateTime(pYear, pMonth, 1);
                curve.Add(realAmount);
            }
            return curve;
        }

        public void SaveRate(DateTime pDate, double pRate, Currency pCurrency)
        {
            if (pDate == DateTime.MinValue)
                throw new OpenCbsExchangeRateException(OpenCbsExchangeRateExceptionEnum.DateIsNull);

            if (pRate == 0)
                throw new OpenCbsExchangeRateException(OpenCbsExchangeRateExceptionEnum.RateIsEmpty);

            if (pCurrency ==null)
                throw new OpenCbsExchangeRateException(OpenCbsExchangeRateExceptionEnum.RateIsEmpty);

            if (SelectExchangeRate(pDate,pCurrency) != null)
                _rateManager.Update(pDate, pRate, pCurrency);
            else
                _rateManager.Add(pDate, pRate, pCurrency);
        }

        public double GetMostRecentlyRate(DateTime pDate, Currency pCurrency)
        {
            return _rateManager.GetMostRecentlyRate(pDate,pCurrency);
        }
    }
}
