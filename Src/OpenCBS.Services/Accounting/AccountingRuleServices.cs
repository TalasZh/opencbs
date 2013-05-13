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
using System.Data;
using OpenCBS.Manager.Accounting;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.ExceptionsHandler.Exceptions.AccountExceptions;
using OpenCBS.Enums;

namespace OpenCBS.Services.Accounting
{
    public class AccountingRuleServices : BaseServices
    {
        private AccountingRuleManager _accountingRuleManager;
        private User _user;
        public string ClosureStatus { get; set; }
        public string ClosureStatusInfo { get; set; }

        public AccountingRuleServices(User pUser)
        {
            _user = pUser;
            _accountingRuleManager = new AccountingRuleManager(_user);
        }

        public AccountingRuleServices(string pTestDb)
        {
            _accountingRuleManager = new AccountingRuleManager(pTestDb);
        }

        public AccountingRuleServices(AccountingRuleManager pAccountingRuleManager)
        {
            _accountingRuleManager = pAccountingRuleManager;
        }

        public int SaveAccountingRule(IAccountingRule pRule)
        {
            ValidateAccountingRule(pRule);
            return _accountingRuleManager.AddAccountingRule(pRule);
        }

        public void UpdateAccountingRule(IAccountingRule pRule)
        {
            ValidateAccountingRule(pRule);
            _accountingRuleManager.UpdateAccountingRule(pRule);
        }

        public void DeleteAccountingRule(IAccountingRule pRule)
        {
            _accountingRuleManager.DeleteAccountingRule(pRule);
        }

        public void DeleteAllAccountingRules()
        {
            RunAction(transaction => _accountingRuleManager.DeleteAllAccountingRules(), _user);
        }
        
        public IAccountingRule Select(int pId)
        {
            return _accountingRuleManager.Select(pId);
        }

        public AccountingRuleCollection SelectAll()
        {
            return _accountingRuleManager.SelectAll();
        }

        public DataSet GetRuleCollectionDataset()
        {
            return _accountingRuleManager.GetRuleCollectionDataset();
        }

        public AccountingRuleCollection SelectAllByEventType(string eventType)
        {
            return _accountingRuleManager.SelectAllByEventType(eventType);
        }

        private void ValidateAccountingRule(IAccountingRule pRule)
        {
            if (!_validateAccount(pRule.DebitAccount))
                throw new OpenCbsAccountingRuleException(OpenCbsAccountingRuleExceptionEnum.GenericAccountIsInvalid);

            if (!_validateAccount(pRule.CreditAccount))
                throw new OpenCbsAccountingRuleException(OpenCbsAccountingRuleExceptionEnum.SpecificAccountIsInvalid);

            if (pRule.DebitAccount.Id == pRule.CreditAccount.Id)
                throw new OpenCbsAccountingRuleException(OpenCbsAccountingRuleExceptionEnum.GenericAndSpecificAccountsAreIdentical);

            if (pRule is ContractAccountingRule)
                ValidateContractAccountingRule(pRule as ContractAccountingRule);
        }

        private void ValidateContractAccountingRule(ContractAccountingRule pRule)
        {
            if (!Enum.IsDefined(typeof(OProductTypes), pRule.ProductType.ToString()))
                throw new OpenCbsAccountingRuleException(OpenCbsAccountingRuleExceptionEnum.ProductTypeIsInvalid);

            if (!Enum.IsDefined(typeof(OClientTypes), pRule.ClientType.ToString()))
                throw new OpenCbsAccountingRuleException(OpenCbsAccountingRuleExceptionEnum.ClientTypeIsInvalid);
        }

        private bool _validateAccount(Account pAccount)
        {
            if (pAccount != null)
                return (pAccount.Id != 0 && pAccount.Number != null);
            return false;
        }
      }
}
