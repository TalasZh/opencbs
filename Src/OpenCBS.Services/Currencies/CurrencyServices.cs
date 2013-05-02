// LICENSE PLACEHOLDER

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
                    throw new OctopusAccountException(OctopusAccountExceptionsEnum.CurrencyAlreadyExists);

                SqlTransaction sqlTransac = conn.BeginTransaction();

                if (_currencyManager.SelectAllCurrencies(sqlTransac).Count > 1)
                    throw new OctopusAccountException(OctopusAccountExceptionsEnum.MaximumCurrencyLimitReached);

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
