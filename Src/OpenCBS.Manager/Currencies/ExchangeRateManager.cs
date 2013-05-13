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
using System.Data.SqlClient;
using OpenCBS.CoreDomain;
using OpenCBS.Shared;
using OpenCBS.CoreDomain.Accounting;

namespace OpenCBS.Manager.Currencies
{
    public class ExchangeRateManager : Manager
    {
        public ExchangeRateManager(User pUser) : base(pUser){}
        public ExchangeRateManager(string pTestDb) : base(pTestDb){}


        public void Add(DateTime pDate, double pRate, Currency pCurrency)
        {
            const string q = "INSERT INTO [ExchangeRates] ([exchange_date], [exchange_rate],[currency_id]) " +
                             "VALUES(@date,@rate,@currency)";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                SetExchangeRate(c, pDate.Date, pRate,pCurrency);
                c.ExecuteNonQuery();
            }
        }

        private static void SetExchangeRate(OpenCbsCommand c, DateTime pDate, double pRate, Currency pCurrency)
        {
            c.AddParam("@date", pDate);
            c.AddParam("@rate", pRate);
            c.AddParam("@currency",pCurrency.Id);
        }

        public void Update(DateTime pDate, double pRate, Currency pCurrency)
        {
            const string q = @"UPDATE [ExchangeRates] 
                               SET [exchange_rate] = @rate 
                               WHERE exchange_date = @date 
                                 AND currency_id = @currency";

            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                SetExchangeRate(c, pDate.Date, pRate, pCurrency);
                c.ExecuteNonQuery();
            }
        }
        public List<ExchangeRate> SelectExchangeRateForAllCurrencies(DateTime pDate)
        {
            const string q =
                @"SELECT * 
                  FROM ExchangeRates 
                  INNER JOIN Currencies ON ExchangeRates.currency_id = Currencies.id 
                  WHERE exchange_date = @date";
            
            List<ExchangeRate> rates;
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                c.AddParam("@date", pDate);
                using (OpenCbsReader r = c.ExecuteReader())
                {
                    if (r == null || r.Empty) return null;

                    rates = new List<ExchangeRate>();
                    while (r.Read())
                    {
                        ExchangeRate newRate = new ExchangeRate
                                                   {
                                                       Date = pDate,
                                                       Rate = r.GetDouble("exchange_rate"),
                                                       Currency = new Currency
                                                                      {
                                                                          Id = r.GetInt("currency_id"),
                                                                          IsPivot = r.GetBool("is_pivot"),
                                                                          IsSwapped = r.GetBool("is_swapped"),
                                                                          Name = r.GetString("name"),
                                                                          Code = r.GetString("code")
                                                                      }
                                                   };
                        rates.Add(newRate);
                    }
                }
            }
            return rates;
        }

        public List<ExchangeRate> SelectRatesByDate(DateTime beginDate, DateTime endDate)
        {
            const string q =
                   @"SELECT 
                     exchange_date, 
                     exchange_rate, 
                     currency_id,
                    is_pivot,
                    is_swapped,
                    name,
                    code
                  FROM ExchangeRates 
                  INNER JOIN Currencies ON ExchangeRates.currency_id = Currencies.id 
                  WHERE exchange_date BETWEEN @beginDate AND @endDate";

            List<ExchangeRate> rates;
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                c.AddParam("@beginDate", beginDate.Date);
                c.AddParam("@endDate", endDate.Date);
                using (OpenCbsReader r = c.ExecuteReader())
                {
                    if (r == null || r.Empty) return null;

                    rates = new List<ExchangeRate>();
                    while (r.Read())
                    {
                        ExchangeRate newRate = new ExchangeRate
                        {
                            Date = r.GetDateTime("exchange_date"),
                            Rate = r.GetDouble("exchange_rate"),
                            Currency = new Currency
                            {
                                Id = r.GetInt("currency_id"),
                                IsPivot = r.GetBool("is_pivot"),
                                IsSwapped = r.GetBool("is_swapped"),
                                Name = r.GetString("name"),
                                Code = r.GetString("code")
                            }
                        };
                        rates.Add(newRate);
                    }
                }
            }
            return rates;
        }

        public ExchangeRate Select(DateTime pDate, Currency pCurrency)
        {
            const string q =  @"SELECT exchange_date, exchange_rate, currency_id
                                FROM ExchangeRates 
                                WHERE DATEADD(dd, 0, DATEDIFF(dd, 0, exchange_date)) = @date 
                                  AND currency_id = @currency";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                c.AddParam("@date", pDate.Date);
                c.AddParam("@currency", pCurrency.Id);

                using (OpenCbsReader r = c.ExecuteReader())
                {
                    if (r.Empty)
                        return null;

                    r.Read();

                    return new ExchangeRate
                               {
                                   Date = pDate,
                                   Rate = r.GetDouble("exchange_rate"),
                                   Currency = pCurrency
                               };
                }
            }
        }

        public double GetMostRecentlyRate(DateTime pDate, Currency pCurrency)
        {
            const string q = @"SELECT TOP 1 exchange_rate 
                                    FROM ExchangeRates 
                                    WHERE exchange_date <= @date AND currency_id = @currency 
                                    ORDER BY exchange_date DESC";

            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                c.AddParam("@currency",pCurrency.Id);
                c.AddParam("@date", pDate);
                return Convert.ToDouble(c.ExecuteScalar());
            }
        }

        public double GetNearestRate(DateTime pDate, Currency pCurrency)
        {
            const string q = @"SELECT TOP 1 exchange_rate 
                                    FROM ExchangeRates 
                                    WHERE exchange_date <= @date AND currency_id = @currency 
                                    ORDER BY exchange_date DESC";

            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                c.AddParam("@currency", pCurrency.Id);
                c.AddParam("@date", pDate);
                return Convert.ToDouble(c.ExecuteScalar());
            }
        }
    }
}
