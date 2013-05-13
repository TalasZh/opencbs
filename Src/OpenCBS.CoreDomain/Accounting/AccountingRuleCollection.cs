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
using OpenCBS.CoreDomain.Contracts.Savings;
using OpenCBS.Enums;
using OpenCBS.CoreDomain.Clients;
using OpenCBS.CoreDomain.Contracts;
using OpenCBS.CoreDomain.Contracts.Loans;

namespace OpenCBS.CoreDomain.Accounting
{
    [Serializable]
    public class AccountingRuleCollection : IList<IAccountingRule>
    {
        private readonly List<IAccountingRule> _rules;

        public AccountingRuleCollection()
        {
            _rules = new List<IAccountingRule>();
        }

        public Account GetSpecificAccount(string pNumber, ISavingsContract pSavings, OBookingDirections pBookingDirection)
        {
            var query = GetContractAccountingRules();

            query = query.Where(item => item.DebitAccount.Number == pNumber && item.Deleted == false).ToList();
            if (query.Count() == 0)
                return null;

            query = query.Where(item => item.ProductType == OProductTypes.All
                    || (item.ProductType == OProductTypes.Saving && (item.SavingProduct == null || item.SavingProduct.Id == pSavings.Product.Id))).ToList();

            if (query.Count == 0)
                return null;

            query = query.Where(item => item.BookingDirection == OBookingDirections.Both
                    || item.BookingDirection == pBookingDirection).ToList();

            if (query.Count == 0)
                return null;

            OClientTypes clientType;

            if (pSavings.Client is Person)
                clientType = OClientTypes.Person;
            else if (pSavings.Client is Group)
                clientType = OClientTypes.Group;
            else if (pSavings.Client is Corporate)
                clientType = OClientTypes.Corporate;
            else
                clientType = OClientTypes.Village;

            query = query.Where(item => item.ClientType == OClientTypes.All
                     || item.ClientType == clientType).ToList();

            if (query.Count == 0)
                return null;

            if (pSavings.Client is Person)
                query = query.Where(item => item.EconomicActivity == null
                    || item.EconomicActivity.Id == ((Person)pSavings.Client).Activity.Id).ToList();
            else if (pSavings.Client is Corporate)
                query = query.Where(item => item.EconomicActivity == null
                    || item.EconomicActivity.Id == ((Corporate)pSavings.Client).Activity.Id).ToList();
            else
                query = query.Where(item => item.EconomicActivity == null).ToList();

            if (query.Count == 0)
                return null;

            return query[0].CreditAccount;
        }

        public Account GetSpecificAccount(string pNumber, IContract pContract, OBookingDirections pBookingDirection)
        {
            List<ContractAccountingRule> query = GetContractAccountingRules();

            query = query.Where(item => item.DebitAccount.Number == pNumber && item.Deleted == false).ToList();

            if (query.Count == 0)
                return null;
            
            if (pContract is Loan)
                query = query.Where(item => item.ProductType == OProductTypes.All
                    || (item.ProductType == OProductTypes.Loan && (item.LoanProduct == null || item.LoanProduct.Id == pContract.Product.Id))).ToList();
            else
                query = query.Where(item => item.ProductType == OProductTypes.All).ToList();

            if (query.Count == 0)
                return null;

            query = query.Where(item => item.BookingDirection == OBookingDirections.Both
                    || item.BookingDirection == pBookingDirection).ToList();

            if (query.Count == 0)
                return null;

            OClientTypes clientType;

            if (pContract.Project.Client is Person)
                clientType = OClientTypes.Person;
            else if (pContract.Project.Client is Group)
                clientType = OClientTypes.Group;
            else if (pContract.Project.Client is Corporate)
                clientType = OClientTypes.Corporate;
            else
                clientType = OClientTypes.Village;

            query = query.Where(item => item.ClientType == OClientTypes.All
                     || item.ClientType == clientType).ToList();

            if (query.Count == 0)
                return null;

            if (pContract.Project.Client is Person)
                query = query.Where(item => item.EconomicActivity == null
                    || item.EconomicActivity.Id == ((Person)pContract.Project.Client).Activity.Id).ToList();
            else if (pContract.Project.Client is Corporate)
                query = query.Where(item => item.EconomicActivity == null
                    || item.EconomicActivity.Id == ((Corporate)pContract.Project.Client).Activity.Id).ToList();
            else
                query = query.Where(item => item.EconomicActivity == null).ToList();

            if (query.Count == 0)
                return null;

            return query[0].CreditAccount;
        }

        public List<ContractAccountingRule> GetContractAccountingRules()
        {
            return _rules.OfType<ContractAccountingRule>().ToList();
        }

        public List<FundingLineAccountingRule> GetFundingLineAccountingRules()
        {
            return _rules.OfType<FundingLineAccountingRule>().ToList();
        }

        #region IList<IAccountingRule> Members

        public void SortByOrder()
        {
            _rules.Sort((x, y) => x.Order.CompareTo(y.Order));
        }

        public int IndexOf(IAccountingRule item)
        {
            return _rules.IndexOf(item);
        }

        public void Insert(int index, IAccountingRule item)
        {
            _rules.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _rules.RemoveAt(index);
        }

        public IAccountingRule this[int index]
        {
            get
            {
                return _rules[index];
            }
            set
            {
                _rules[index] = value;
            }
        }

        #endregion

        #region ICollection<IAccountingRule> Members

        public void Add(IAccountingRule item)
        {
            _rules.Add(item);
        }

        public void Clear()
        {
            _rules.Clear();
        }

        public bool Contains(IAccountingRule item)
        {
            return _rules.Contains(item);
        }

        public void CopyTo(IAccountingRule[] array, int arrayIndex)
        {
            _rules.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _rules.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(IAccountingRule item)
        {
            return _rules.Remove(item);
        }

        #endregion

        #region IEnumerable<IAccountingRule> Members

        public IEnumerator<IAccountingRule> GetEnumerator()
        {
            return _rules.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _rules.GetEnumerator();
        }

        #endregion
    }
}
 
