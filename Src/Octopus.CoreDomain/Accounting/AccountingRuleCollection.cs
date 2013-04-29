using System;
using System.Collections.Generic;
using System.Linq;
using Octopus.CoreDomain.Contracts.Savings;
using Octopus.Enums;
using Octopus.CoreDomain.Clients;
using Octopus.CoreDomain.Contracts;
using Octopus.CoreDomain.Contracts.Loans;

namespace Octopus.CoreDomain.Accounting
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
 