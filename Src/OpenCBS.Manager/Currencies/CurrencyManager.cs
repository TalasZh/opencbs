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
using OpenCBS.CoreDomain.Accounting;

namespace OpenCBS.Manager.Currencies
{
    public class CurrencyManager : Manager
    {
        public CurrencyManager(string pDatabaseConnectionString) : base(pDatabaseConnectionString){}
        public CurrencyManager(User pUser) : base(pUser){}

        public int Add(Currency pCurrency, SqlTransaction t)
        {
            const string q = @"INSERT INTO [Currencies] ([name], [code], [is_pivot], [is_swapped],use_cents)
                                    VALUES(@name, @code, @is_pivot, @is_swapped,@use_cents) SELECT SCOPE_IDENTITY()";
            using (OpenCbsCommand c = new OpenCbsCommand(q, t.Connection, t))
            {
                c.AddParam("@name", pCurrency.Name);
                c.AddParam("@code", pCurrency.Code);
                c.AddParam("@is_pivot", pCurrency.IsPivot);
                c.AddParam("@is_swapped", pCurrency.IsSwapped);
                c.AddParam("@use_cents", pCurrency.UseCents);
                return int.Parse(c.ExecuteScalar().ToString());
            }
        }

        public void Update(Currency pCurrency, SqlTransaction t)
        {
            const string q = @"UPDATE [Currencies] set [name] = @name, [code] = @code, 
                                            [is_pivot] = @is_pivot, [is_swapped] = @is_swapped, use_cents = @use_cents
                                    WHERE [id] = @currencyID";
            
            using (OpenCbsCommand c = new OpenCbsCommand(q, t.Connection, t))
            {
                c.AddParam("@currencyID", pCurrency.Id);
                c.AddParam("@name", pCurrency.Name);
                c.AddParam("@code", pCurrency.Code);
                c.AddParam("@is_pivot", pCurrency.IsPivot);
                c.AddParam("@is_swapped", pCurrency.IsSwapped);
                c.AddParam("@use_cents", pCurrency.UseCents);
                c.ExecuteNonQuery();
            }
        }
        public List<Currency> SelectAllCurrencies(SqlTransaction t)
        {
            List<Currency> currencies = new List<Currency>();
            const string q = @"SELECT * FROM Currencies ORDER BY is_pivot DESC";

            using (OpenCbsCommand c = new OpenCbsCommand(q, t.Connection, t))
            using (OpenCbsReader r = c.ExecuteReader())
            {
                if (r == null || r.Empty) return currencies;
                while (r.Read())
                {
                    currencies.Add(new Currency
                    {
                        Id = r.GetInt("id"),
                        Name = r.GetString("name"),
                        Code = r.GetString("code"),
                        IsPivot = r.GetBool("is_pivot"),
                        IsSwapped = r.GetBool("is_swapped"),
                        UseCents = r.GetBool("use_cents")
                    });
                }
            }
            
            return currencies;
        }
        public Currency GetPivot()
        {
            Currency pivot=null;
            const string q = "SELECT * FROM Currencies where is_pivot = 1";

            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            using (OpenCbsReader r = c.ExecuteReader())
            {
                if (r == null || r.Empty) return pivot;
                while (r.Read())
                {
                   pivot = new Currency
                    {
                        Id = r.GetInt("id"),
                        Name = r.GetString("name"),
                        Code = r.GetString("code"),
                        IsPivot = r.GetBool("is_pivot"),
                        IsSwapped = r.GetBool("is_swapped"),
                        UseCents = r.GetBool("use_cents")
                    };
                }
                return pivot;
            }
        }


        public Currency SelectCurrencyById(int id)
        {
            const string q = @"SELECT name, code, is_pivot, is_swapped, use_cents FROM [Currencies] where id = @id";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                c.AddParam("@id", id);
                using (OpenCbsReader r = c.ExecuteReader())
                {
                    if (r == null || r.Empty) return null;

                    r.Read();
                    return new Currency
                    {
                        Id = id,
                        Code = r.GetString("code"),
                        Name = r.GetString("name"),
                        IsPivot = r.GetBool("is_pivot"),
                        IsSwapped = r.GetBool("is_swapped"),
                        UseCents = r.GetBool("use_cents")
                    };
                }
            }
        }

        public Currency SelectCurrencyByName(string name)
        {
            const string q = @"SELECT id, code, is_pivot, is_swapped, use_cents FROM [Currencies] WHERE name = @name";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                c.AddParam("@name", name);
                using (OpenCbsReader r = c.ExecuteReader())
                {
                    if (r == null || r.Empty) return null;

                    r.Read();
                    return new Currency
                    {
                        Id = r.GetInt("id"),
                        Code = r.GetString("code"),
                        Name = name,
                        IsPivot = r.GetBool("is_pivot"),
                        IsSwapped = r.GetBool("is_swapped"),
                        UseCents = r.GetBool("use_cents")
                    };
                }
            }
        }

        //public Currency SelectCurrencyByName(string pName)
        //{
        //    const string q = @"SELECT id, name, code, is_pivot, is_swapped FROM [Currencies] where name = @name";
        //    using (SqlConnection conn = GetConnection())
        //    using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
        //    {
        //        c.AddParam("@name", pName);
        //        using (OpenCbsReader r = c.ExecuteReader())
        //        {
        //            if (r == null || r.Empty) return null;

        //            r.Read();
        //            return new Currency
        //            {
        //                Id = r.GetInt("id", reader),
        //                Code = r.GetString("code", reader),
        //                Name = r.GetString("name", reader),
        //                IsPivot = r.GetBool("is_pivot", reader),
        //                IsSwapped = r.GetBool("is_swapped", reader)
        //            };


        //        }
        //    }
        //}

        public bool IsThisCurrencyAlreadyExist(string pCode,string pName)
        {
            const string q = @"SELECT * FROM [Currencies] where name = @name AND code = @code";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                c.AddParam("@code",pCode);
                c.AddParam("@name", pName);

                using (OpenCbsReader r = c.ExecuteReader())
                {
                    if (r == null || r.Empty) return false;

                    return true;
                }
            }
        }

        public int GetContractQuantityByCurrencyId(int currencyId)
        {
            const string q = @"
                            SELECT COUNT(Credit.id) AS contract_quantity
                            FROM [dbo].[Currencies] AS Cur
                            INNER JOIN Packages AS Pack ON Pack.currency_id=Cur.id
                            INNER JOIN Credit ON Credit.package_id=Pack.id
                            WHERE Cur.id=@currency_id";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                c.AddParam("@currency_id", currencyId);
                return (int)c.ExecuteScalar();
            }
        }
    }
}
