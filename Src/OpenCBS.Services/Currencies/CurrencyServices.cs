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
using OpenCBS.DatabaseConnection;
using OpenCBS.ExceptionsHandler;
using OpenCBS.ExceptionsHandler.Exceptions.AccountExceptions;
using OpenCBS.Manager.Currencies;
using OpenCBS.Services.Accounting;

namespace OpenCBS.Services.Currencies
{
    public class CurrencyServices : Services //MarshalByRefObject
    {
        private readonly CurrencyManager _currencyManager;
        private readonly User _user;
        public CurrencyServices(User pUser)
        {
            _currencyManager = new CurrencyManager(pUser);
            _user = pUser;
        }

        public CurrencyServices(string pTestDb)
        {
            _currencyManager = new CurrencyManager(pTestDb);
        }

        public List<Currency> FindAllCurrencies(SqlTransaction pSqlTransac)
        {
            return _currencyManager.SelectAllCurrencies(pSqlTransac);
        }

        public List<Currency> FindAllCurrencies()
        {
            using (SqlConnection conn = _currencyManager.GetConnection())
            {
                SqlTransaction t = conn.BeginTransaction();
                try
                {
                    List<Currency> _currencies = FindAllCurrencies(t);
                    t.Commit();
                    return _currencies;
                }
                catch
                {
                    t.Rollback();
                    return null;
                }
            }
        }

        public Currency FindCurrencyByName(string name)
        {
            return _currencyManager.SelectCurrencyByName(name);
        }

        public int AddNewCurrency(Currency pNewCurrency)
        {
            using (SqlConnection conn = _currencyManager.GetConnection())
            {
                int cId = 0;
                if (_currencyManager.IsThisCurrencyAlreadyExist(pNewCurrency.Code, pNewCurrency.Name))
                    throw new OpenCbsAccountException(OpenCbsAccountExceptionsEnum.CurrencyAlreadyExists);

                SqlTransaction sqlTransac = conn.BeginTransaction();

                if (_currencyManager.SelectAllCurrencies(sqlTransac).Count > 1)
                    throw new OpenCbsAccountException(OpenCbsAccountExceptionsEnum.MaximumCurrencyLimitReached);

                try
                {

                    cId = _currencyManager.Add(pNewCurrency, sqlTransac);
                    sqlTransac.Commit();

                }
                catch (Exception)
                {
                    sqlTransac.Rollback();
                    throw;
                }
                return cId;
            }

        }
        public void UpdateNewCurrency(Currency pNewCurrency)
        {
            using (SqlConnection conn = _currencyManager.GetConnection())
            {
                SqlTransaction t = conn.BeginTransaction();

                try
                {
                    _currencyManager.Update(pNewCurrency, t);
                    t.Commit();
                }
                catch (Exception)
                {
                    t.Rollback();
                    throw;
                }
            }
        }
        public Currency GetPivot()
        {
            return _currencyManager.GetPivot();
        }

        public Currency GetCurrency(int currencyId)
        {
            return _currencyManager.SelectCurrencyById(currencyId);
        }

        public bool IsCurrencyUsed(int currencyId)
        {
            return _currencyManager.GetContractQuantityByCurrencyId(currencyId) > 0;
        }
    }
}
