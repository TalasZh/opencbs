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
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using OpenCBS.DatabaseConnection;
using OpenCBS.ExceptionsHandler.Exceptions.AccountExceptions;
using OpenCBS.Manager;
using OpenCBS.Manager.Accounting;
using OpenCBS.CoreDomain;
using OpenCBS.ExceptionsHandler;
using OpenCBS.Shared;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.Manager.Currencies;
using System.Linq;

namespace OpenCBS.Services.Accounting
{
	/// <summary>
	/// Summary description for ChartOfAccountsServices.
	/// </summary>
    public class ChartOfAccountsServices : BaseServices
	{
        private User _user = new User();
        private readonly AccountManager _accountManagement;
	    private readonly TellerManager _tellerManager;
        private readonly ProvisioningRuleManager _provisionningRuleManager;
        private readonly LoanScaleManager _loanScaleManager;
        private readonly CurrencyManager _currencyManager;

	    public ChartOfAccountsServices(User pUser)
        {
            _user = pUser;
            _accountManagement = new AccountManager(pUser);
            _tellerManager = new TellerManager(pUser);
            _provisionningRuleManager = new ProvisioningRuleManager(pUser);
            _loanScaleManager = new LoanScaleManager(pUser);
            _currencyManager = new CurrencyManager(pUser);
        }

		public ChartOfAccountsServices(string testDB)
		{
			_accountManagement = new AccountManager(testDB);
            _provisionningRuleManager = new ProvisioningRuleManager(testDB);
            _tellerManager = new TellerManager(testDB);
            _loanScaleManager = new LoanScaleManager(testDB);
            _currencyManager = new CurrencyManager(testDB);
            ConnectionManager.GetInstance(testDB);

		}

        public ChartOfAccountsServices(AccountManager pAccountManager)
        {
            _accountManagement = pAccountManager;
        }

		public List<Account> FindAllAccounts()
        {
            return _accountManagement.SelectAllAccounts();
        }

        public List<Account> FindAllAccountsWithoutTeller(int accountId)
        {
            return _accountManagement.SelectAllAccountsWithoutTeller(accountId);
        }

        public DataSet GetAccountsDataset()
        {
            return _accountManagement.GetAccountsDataset();
        }

        public List<AccountCategory> SelectAccountCategories()
        {
            return _accountManagement.SelectAccountCategories();
        }

        public Account SelectAccountById(int id)
        {
            return _accountManagement.Select(id);
        }

        public void CheckDatasAreCorrecltyFilled(Account account)
        {
            CheckAccountData(account);

            if (!account.ParentAccountId.HasValue || account.ParentAccountId == -1) return;

            var parentAccount = _accountManagement.SelectChartAccount(account.ParentAccountId.Value);
            CheckAccountParent(account, parentAccount);
        }

	    private static void CheckAccountParent(Account account, Account parentAccount)
	    {
	        if (parentAccount == null)
	            throw new OpenCbsAccountException(OpenCbsAccountExceptionsEnum.ParentAccountDoesntExists);
	        if (parentAccount.AccountCategory != account.AccountCategory)
	            throw new OpenCbsAccountException(OpenCbsAccountExceptionsEnum.ParentAccountNotSameDescription);
	        if (parentAccount.Id == account.Id)
	            throw new OpenCbsAccountException(OpenCbsAccountExceptionsEnum.ParentAccountIsInvalid);
	    }

	    private void CheckAccountData(Account account)
	    {
	        if (account == null)
	            throw new OpenCbsAccountException(OpenCbsAccountExceptionsEnum.AccountIsNull);
	        if (string.IsNullOrEmpty(account.Number))
	            throw new OpenCbsAccountException(OpenCbsAccountExceptionsEnum.NumberIsNull);
	        if (string.IsNullOrEmpty(account.Label))
	            throw new OpenCbsAccountException(OpenCbsAccountExceptionsEnum.LabelIsNull);
	        if (_accountManagement.SelectAccountCategoriesById((int) account.AccountCategory) == null)
	            throw new OpenCbsAccountException(OpenCbsAccountExceptionsEnum.ParentAccountDoesntExists);
	    }

	    public void CheckAccountCategory(AccountCategory accountCategory)
        {
                List<Account> accounts = _accountManagement.SelectAccountByCategory(accountCategory);
                if (accounts != null)
                    throw new OpenCbsAccountException(OpenCbsAccountExceptionsEnum.AccountCannotBeDeleted);
               
        }

        public void UpdateAccount(Account account)
        {
            CheckDatasAreCorrecltyFilled(account);
            _accountManagement.Update(account);
        }

        public void Insert(Account account)
        {
            CheckDatasAreCorrecltyFilled(account);

            using (SqlConnection conn = _accountManagement.GetConnection())
            {
                SqlTransaction tx = conn.BeginTransaction();
                try
                {
                    _accountManagement.Insert(account, tx);
                    tx.Commit();
                }
                catch (Exception exception)
                {
                    tx.Rollback();
                    if (exception is SqlException)
                        if (((SqlException)exception).Number==2627)
                            //2627 is a standard number of error related to unique keys duplication
                            throw new OpenCbsAccountException(OpenCbsAccountExceptionsEnum.DuplicatedAccount);
                    throw;
                }
            }
        }

        public void InsertCoa(Account[] accounts, bool deleteRelated)
        {
            Array.ForEach(accounts, CheckAccountData);            
            if(!deleteRelated && accounts.Any())
            {
                int[] ids = accounts.Select(a => a.Id).ToArray();
                if (_accountManagement.IdsExist(ids)) throw new OpenCbsAccountException(OpenCbsAccountExceptionsEnum.ImportIdExist);
                
                string[] accountNumbers = accounts.Select(a => a.Number).ToArray();
                if (_accountManagement.NumbersExist(accountNumbers)) throw new OpenCbsAccountException(OpenCbsAccountExceptionsEnum.ImportNumbersExist);
            }
            RunAction(transaction=>
                    {
                        _tellerManager.DeleteAll(transaction);
                        if(deleteRelated)
                        {
                            _accountManagement.DeleteRelatedRecords(transaction);
                            _accountManagement.Delete(transaction);
                        }

                        List<Account> accountsToInsert = new List<Account>(accounts);
                        List<Account> insertedAccounts = new List<Account>(accounts.Length);
                        while (accountsToInsert.Count > 0)
                        {
                            int oldCount = insertedAccounts.Count;
                            for (int index = 0; index < accountsToInsert.Count; index++)
                            {
                                Account account = accountsToInsert[index];
                                int? parentAccountId = account.ParentAccountId;

                                Account parentAccount = null;
                                if (parentAccountId.HasValue && parentAccountId != 0)
                                {
                                    if(deleteRelated)
                                        parentAccount = insertedAccounts.FirstOrDefault(a => a.Id == parentAccountId);
                                    else
                                        parentAccount =
                                            _accountManagement.SelectChartAccount(parentAccountId.Value) ??
                                            insertedAccounts.FirstOrDefault(a => a.Id == parentAccountId);
                                }

                                if (
                                    (parentAccountId == null || parentAccountId == 0) ||
                                    parentAccount != null
                                )
                                {
                                    if(parentAccount != null) CheckAccountParent(account, parentAccount);
                                    try
                                    {
                                        _accountManagement.Insert(account, transaction, true);
                                    }
                                    catch(SqlException exception)
                                    {
                                        if (exception.Number == 2627)
                                            //2627 is a standard number of error related to unique keys duplication
                                            throw new OpenCbsAccountException(OpenCbsAccountExceptionsEnum.DuplicatedAccount);
                                    }
                                    
                                    insertedAccounts.Add(account);
                                    accountsToInsert.Remove(account);
                                    index--;
                                }
                            }

                            if (oldCount == insertedAccounts.Count)
                                throw new OpenCbsException(string.Format(
                                    "Chart of accounts contain non valid tree. Please check these accounts {0}.",
                                    string.Join(",", accountsToInsert.Select(a => a.Number).ToArray())));
                        }
                    }, _user);
        }

        public void InsertAccountCategory(AccountCategory accountCategory)
        {
            using (SqlConnection conn = _accountManagement.GetConnection())
            using (SqlTransaction sqlTransac = conn.BeginTransaction())
            {

                try
                {
                    _accountManagement.InsertAccountCategory(accountCategory, sqlTransac);

                    sqlTransac.Commit();
                }
                catch (Exception)
                {
                    sqlTransac.Rollback();
                    throw;
                }
            }
        }

        public void DeleteAccount(Account account)
        {
            if (_accountManagement.SelectAccountingRuleByChartOfAccountsId(account))
                throw new OpenCbsAccountException(OpenCbsAccountExceptionsEnum.AccountIsUsed);

            CheckDatasAreCorrecltyFilled(account);
            _accountManagement.DeleteAccount(account);
        }

	    public void Delete()
        {
            _accountManagement.Delete();
        }

        public void DeleteAccountCategory(AccountCategory accountCategory)
        {
            CheckAccountCategory(accountCategory);
            _accountManagement.DeleteAccountCategory(accountCategory);
        }

		public void AddProvisioningRate(ProvisioningRate pR)
		{
            ProvisionTable pT = ProvisionTable.GetInstance(_user);
		
            pR.Number = pT.ProvisioningRates.Count + 1;
			pT.Add(pR);
            using (SqlConnection conn = _provisionningRuleManager.GetConnection())
            using (SqlTransaction sqlTransac = conn.BeginTransaction())
            {
                try
                {
                    _provisionningRuleManager.AddProvisioningRate(pR, sqlTransac);
                    sqlTransac.Commit();
                }
                catch (Exception ex)
                {
                    sqlTransac.Rollback();
                    throw ex;
                }
            }
		}
        public void AddLoanScale(LoanScaleRate lR)
        {
            LoanScaleTable lT = LoanScaleTable.GetInstance(_user);
            if (lR.ScaleMin == 0)
                throw new OpenCbsAccountException(OpenCbsAccountExceptionsEnum.LoanScaleTableMin);
            if (lR.ScaleMax == 0)
                throw new OpenCbsAccountException(OpenCbsAccountExceptionsEnum.LoanScaleTableMax);

            lR.Number = lT.LoanScaleRates.Count + 1;
        
            lT.AddLoanScaleRate(lR);
             using (SqlConnection conn = _loanScaleManager.GetConnection())
             using (SqlTransaction sqlTransac = conn.BeginTransaction())
             {
                 try
                 {
                     _loanScaleManager.InsertLoanScale(lR, sqlTransac);
                     sqlTransac.Commit();
                 }
                 catch (Exception ex)
                 {
                     sqlTransac.Rollback();
                     throw ex;
                 }
             }
        }

        public void CreateFiscalYear(FiscalYear fiscalYear)
        {
            using (SqlConnection conn = _accountManagement.GetConnection())
            {
                SqlTransaction sqlTransac = conn.BeginTransaction();
                try
                {
                    _accountManagement.InsertFiscalYear(sqlTransac, fiscalYear);
                    sqlTransac.Commit();
                }
                catch (Exception ex)
                {
                    sqlTransac.Rollback();
                    throw ex;
                }
            }
        }

        public List<FiscalYear> SelectFiscalYears()
        {
            return _accountManagement.SelectFiscalYears();
        }

        public void DeleteFiscalYear(FiscalYear fiscalYear)
        {
            using (SqlConnection conn = _accountManagement.GetConnection())
            {
                SqlTransaction sqlTransac = conn.BeginTransaction();
                try
                {
                    _accountManagement.DeleteFiscalYear(sqlTransac, fiscalYear);
                    sqlTransac.Commit();
                }
                catch (Exception ex)
                {
                    sqlTransac.Rollback();
                    throw ex;
                }
            }
        }

        public void UpdateFiscalYear(FiscalYear fiscalYear)
        {
            using (SqlConnection conn = _accountManagement.GetConnection())
            {
                SqlTransaction sqlTransac = conn.BeginTransaction();
                try
                {
                    _accountManagement.UpdateFiscalYear(sqlTransac, fiscalYear);
                    sqlTransac.Commit();
                }
                catch (Exception ex)
                {
                    sqlTransac.Rollback();
                    throw ex;
                }
            }
        }

        public void DeleteLoanScale(LoanScaleRate ls)
        {
            LoanScaleTable lt = LoanScaleTable.GetInstance(_user);
            lt.DeleteLoanScaleRate(ls);
        }

        public void UpdateLoanScaleTableInstance()
        {
            using (SqlConnection conn = _loanScaleManager.GetConnection())
            using (SqlTransaction sqlTransac = conn.BeginTransaction())
            {
                LoanScaleTable lT = LoanScaleTable.GetInstance(_user);
                try
                {
                    _loanScaleManager.Delete(sqlTransac);
                    int i = 1;
                    foreach (LoanScaleRate lR in lT.LoanScaleRates)
                    {
                        lR.Number = i;
                        _loanScaleManager.InsertLoanScale(lR, sqlTransac);
                        i++;
                    }
                    sqlTransac.Commit();
                }
                catch (Exception ex)
                {
                    sqlTransac.Rollback();
                    throw ex;
                }
            }
        }

        public void CheckGeneralSettings()
        {
            ProvisionTable.GetInstance(_user).ProvisioningRates = _provisionningRuleManager.SelectAllProvisioningRates();
            LoanScaleTable.GetInstance(_user).LoanScaleRates = new ArrayList();
            _loanScaleManager.SelectLoanScales();
        }

		public void UpdateProvisioningTableInstance()
		{
		    using (SqlConnection conn  =  _provisionningRuleManager.GetConnection())
            using (SqlTransaction sqlTransac = conn.BeginTransaction())
            {
                ProvisionTable pT = ProvisionTable.GetInstance(_user);
                try
                {
                    _provisionningRuleManager.DeleteAllProvisioningRules(sqlTransac);
                    int i = 1;
                    foreach (ProvisioningRate pR in pT.ProvisioningRates)
                    {
                        pR.Number = i;
                        _provisionningRuleManager.AddProvisioningRate(pR, sqlTransac);
                        i++;
                    }
                    sqlTransac.Commit();
                }
                catch (Exception ex)
                {
                    sqlTransac.Rollback();
                    throw ex;
                }
            }
		}

        public List<Account> RecalculateBalances(List<Account> pAccounts)
        {
            foreach (Account account in pAccounts.Where(item => item.ParentAccountId == null))
                account.Balance += _getChildsBalance(pAccounts, account);

            return pAccounts;
        }

        private OCurrency _getChildsBalance(List<Account> pAccounts, Account pAccount)
        {
            OCurrency balance = 0;
            foreach (Account account in pAccounts.Where(item => item.ParentAccountId == pAccount.Id))
            {
                account.Balance += _getChildsBalance(pAccounts, account);
                balance += account.Balance;
            }

            return balance;
        }

	    public IEnumerable<string> HasRelatedDatas()
	    {
	        return _accountManagement.NotEmptyRelatedObjects();
	    }
	}
}
