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
using System.Linq;
using OpenCBS.CoreDomain.Accounting.Comparer;
using OpenCBS.CoreDomain.Accounting.Interfaces;
using OpenCBS.Shared;
using OpenCBS.CoreDomain.Contracts.Savings;
using OpenCBS.CoreDomain.Contracts;
using OpenCBS.Enums;

namespace OpenCBS.CoreDomain.Accounting
{
    public class ChartOfAccounts : MarshalByRefObject, IChartOfAccounts
	{
		private List<Account> _accounts;
        private AccountingRuleCollection _accountingRules;

        private static readonly IDictionary<string, ChartOfAccounts> ChartsOfAccounts = new Dictionary<string, ChartOfAccounts>();

        public static void SuppressRemotingInfos(string pMd5)
        {
            if (ChartsOfAccounts.ContainsKey(pMd5))
            {
                ChartsOfAccounts.Remove(pMd5);
            }
        }

        public static void SuppressAll()
        {
            ChartsOfAccounts.Clear();
        }

		public ChartOfAccounts()
		{
            _accounts = new List<Account>();
            DefaultAccounts = new List<Account>();
			_FillDefaultAccountsList(1);
		}

        public AccountingRuleCollection AccountingRuleCollection
        {
            get { return _accountingRules; }
            set { _accountingRules = value; }
        }

        public void FillAccountsList()
        {
            _accounts = DefaultAccounts;
        }

        public void SetActualAccountsList(List<Account> actualAccounts)
        {
            _accounts = actualAccounts;
        }

	    private void _FillDefaultAccountsList(int pCurrencyID)
		{
            DefaultAccounts = Accounting.DefaultAccounts.DefaultAccount(pCurrencyID);
		}

        public static ChartOfAccounts GetInstance(User pUser)
        {
            if (!ChartsOfAccounts.ContainsKey(pUser.Md5))
                ChartsOfAccounts.Add(pUser.Md5, new ChartOfAccounts());
            return ChartsOfAccounts[pUser.Md5];
        }

		public void AddAccount(Account account)
		{
			_accounts.Add(account);
		}

		public Account GetAccountByTypeCode(string typeCode, int pCurrencyId)
		{
		    foreach(Account account in _accounts)
			{
				if (account.TypeCode.Equals(typeCode) && account.CurrencyId==pCurrencyId)
				{
					return account;
				}
			}

			return null;
		}

        public Account GetAccountByNumber(string pNumber, int pCurrencyId)
        {
            return _getAccountByNumber(pNumber, pCurrencyId);
        }

        public Account GetAccountByNumber(string pNumber, int pCurrencyId, ISavingsContract pSavingsContract, OBookingDirections pBookingDirection)
		{
            //if (_accountingRules != null)
            //{
            //    var specificAccount = _accountingRules.GetSpecificAccount(pNumber, pSavingsContract, pBookingDirection);
            //    if (specificAccount != null)
            //        pNumber = specificAccount.Number;
            //}

		    return _getAccountByNumber(pNumber, pCurrencyId);
		}

        public Account GetAccountByNumber(string pNumber, int pCurrencyId, IContract pContract, OBookingDirections pBookingDirection)
        {
            //if (_accountingRules != null)
            //{
            //    var specificAccount = _accountingRules.GetSpecificAccount(pNumber, pContract, pBookingDirection);
            //    if (specificAccount != null)
            //        pNumber = specificAccount.Number;
            //}

            return _getAccountByNumber(pNumber, pCurrencyId);
        }

        private Account _getAccountByNumber(string pNumber, int pCurrencyId)
        {
            return _accounts.FirstOrDefault(item => item.Number.Equals(pNumber) && item.CurrencyId == pCurrencyId);
        }

        public OCurrency GetPivotBalance(string pNumber)
        {
            OCurrency balancedAmount = new OCurrency(0);
            foreach (Account account in _accounts)
            {
                if (account.Number.Equals(pNumber))
                {
                    balancedAmount += account.Balance;
                }
            }
            return balancedAmount;
        }
        public Account GetAccountById(int pId)
        {
            foreach (Account account in _accounts)
            {
                if (account.Id.Equals(pId))
                {
                    return account;
                }
            }
            return null;
        }

		/// <summary>
		/// This method is required to book a movement set
		/// Each elementary mvt is differently booked if it is a debit or a credit movement
		/// </summary>
		/// <param name="movementSet">the movement set to book</param>
        public void Book(AccountingTransaction movementSet)
		{
			foreach(Booking Booking in movementSet.Bookings)
			{
                GetAccountByTypeCode(Booking.DebitAccount.TypeCode, Booking.DebitAccount.CurrencyId).Debit(Booking.Amount);
                GetAccountByTypeCode(Booking.CreditAccount.TypeCode, Booking.CreditAccount.CurrencyId).Credit(Booking.Amount);
			}
		}

		public void UnBook(AccountingTransaction movementSet)
		{
			foreach(Booking booking in movementSet.Bookings)
			{
				GetAccountByTypeCode(booking.CreditAccount.TypeCode, booking.CreditAccount.CurrencyId).Debit(booking.Amount);
                GetAccountByTypeCode(booking.DebitAccount.TypeCode, booking.CreditAccount.CurrencyId).Credit(booking.Amount);
			}
		}

	    public List<Account> Accounts
		{
			get
			{
                _accounts.Sort(new AccountComparer());
				return _accounts;
			}
			set {_accounts = value;}
		}

        public List<Account> UniqueAccounts
        {
            get
            {
                List<Account> uniqueAccounts = new List<Account>();
                foreach (Account account in _accounts)
                {
                    if (uniqueAccounts.Find(e => ((e.Number == account.Number) ? true : false)) == null)
                    {
                        uniqueAccounts.Add(account);
                    }
                }
                return uniqueAccounts;
            }
        }

        public List<Account> DefaultAccounts { get; set; }
	}
}
