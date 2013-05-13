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
using OpenCBS.Enums;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Accounting
{
    /// <summary>
    /// Description r�sum�e de Account
    /// </summary>
    [Serializable]
    public class Account
	{
        private string _number;
        private string _label;
		private OCurrency _balance;
        private bool _debitPlus;
        private OCurrency _stockBalance=decimal.Zero;
        public int Id { get; set; }
        public int CurrencyId { get; set; }
        public string TypeCode { get; set; }
        public OAccountCategories AccountCategory { get; set; }
        public int? ParentAccountId { get; set; }
        public int Left { get; set; }
        public int Right { get; set; }

        public string CurrencyCode { get; set; }

        public Account()
		{

		}

        public Account(string accountNumber, string label, OCurrency   balance, string typeCode, bool debitPlus, OAccountCategories category, int pCurrencyId)
		{
			_number = accountNumber;
			_label = label;
			_balance = balance;
			TypeCode = typeCode;
			_debitPlus = debitPlus;
			AccountCategory = category;
            CurrencyId = pCurrencyId;
		}
        public Account(string accountNumber, string label, OCurrency balance, string typeCode, bool debitPlus, int id, OAccountCategories category, int pCurrencyId)
        {
            _number = accountNumber;
            _label = label;
            _balance = balance;
            TypeCode = typeCode;
            _debitPlus = debitPlus;
            Id = id;
            AccountCategory = category;
            CurrencyId = pCurrencyId;
        }
		public Account(string accountNumber, string accountLabel)
		{
			_number = accountNumber;
			_label = accountLabel;
		}

        public bool Type
        {
            get; set;
        }

        public override string ToString()
        {
            return Number + " : " + _label;
        }

        public string Number
		{
			get { return _number; }
			set { _number = value; }
		}
        public OCurrency StockBalance
        {
            get { return _stockBalance; }
            set { _stockBalance = value; }
        }
        public string Label
		{
			get { return _label; }
			set { _label = value; }
		}

		public OCurrency Balance
		{
			get { return _balance; }
			set { _balance = value; }
		}

        public OCurrency OpenBalance
        {
            get; set;
        }

        public OCurrency CloseBalance
        {
            get; set;
        }

        public OCurrency   BalanceRounded
		{
            //get { return _balance; }
            get { return Math.Round(_balance.Value, 2); }
		}

		public bool DebitPlus
		{
			get { return _debitPlus; }
			set { _debitPlus = value; }
		}

		/// <summary>
		/// this method makes a debit operation on an account based on the debitPlus property of the account.
		/// </summary>
		/// <param name="amount">the amount to debit</param>
		public void Debit(OCurrency   amount)
		{
			if (_debitPlus)
				_balance += amount;
			else
				_balance -= amount;
		}

		/// <summary>
		/// this method makes a credit operation on an account based on the debitPlus property of the account.
		/// </summary>
		/// <param name="amount">the amount to credit</param>
		public void Credit(OCurrency amount)
		{
			if (_debitPlus)
				_balance -= amount;
			else
				_balance += amount;
		}

        public Account Copy()
        {
            return (Account) MemberwiseClone();
        }
	}

    [Serializable]
    public class AccountCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public AccountCategory()
        {

        }
    }
}
