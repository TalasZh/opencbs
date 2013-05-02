// LICENSE PLACEHOLDER

using System;
using System.Collections;
using System.Collections.Generic;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.Enums;

namespace OpenCBS.CoreDomain
{
    /// <summary>
    /// Summary description for ExoticProduct.
    /// </summary>
    [Serializable]
    public class ExoticInstallmentsTable : IEnumerable
    {
        private int _id;
        private string _name;
        private ArrayList _installmentList;

        public ExoticInstallmentsTable()
        {
            _installmentList = new ArrayList();
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        // Required for serialization
        public void Add(Object pExoticInstallment)
        {
            Add((ExoticInstallment) pExoticInstallment);
        }

        public void Add(ExoticInstallment pExoticInstallment)
        {
            pExoticInstallment.Number = _installmentList.Count + 1;
            _installmentList.Add(pExoticInstallment);
        }

        public void Add(List<ExoticInstallment> pExoticInstallments)
        {
            foreach (ExoticInstallment installment in pExoticInstallments)
            {
                installment.Number = _installmentList.Count + 1;
                _installmentList.Add(installment);
            }
        }

        public void Remove(ExoticInstallment pExoticInstallment)
        {
            _installmentList.Remove(pExoticInstallment);
            for(int i = 1; i <= _installmentList.Count; i++)
            {
                ExoticInstallment installment = (ExoticInstallment) _installmentList[i - 1];
                installment.Number = i;
            }
        }

        public void Move(ExoticInstallment pExoticInstallment, int pNewPosition)
        {
            _installmentList.Remove(pExoticInstallment);
            _installmentList.Insert(pNewPosition - 1, pExoticInstallment);

            for (int i = 1; i <= _installmentList.Count; i++)
            {
                ExoticInstallment installment = (ExoticInstallment)_installmentList[i - 1];
                installment.Number = i;
            }
        }
        /// <summary>
        /// Get exotic installment or null
        /// </summary>
        /// <param name="rank"></param>
        /// <returns>return ExoticInstallment or null</returns>
        public ExoticInstallment GetExoticInstallment(int rank)
        {
            return rank < _installmentList.Count ? _installmentList[rank] as ExoticInstallment : null;
        }

        public int GetNumberOfInstallments
        {
            get { return _installmentList.Count; } 
        }

        public bool IsExoticProductForDecliningRatePackage
        {
            get
            {
                foreach (ExoticInstallment exoInstallment in _installmentList)
                {
                    if (exoInstallment.InterestCoeff.HasValue)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public bool IsExoticProductForFlatRatePackage
        {
            get
            {
                foreach (ExoticInstallment exoInstallment in _installmentList)
                {
                    if (!exoInstallment.InterestCoeff.HasValue)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public double SumOfPrincipalCoeff
        {
            get
            {
                return _SumOfPrincipal();
            }
        }

        private double _SumOfPrincipal()
        {
            decimal sum = 0;
            foreach (ExoticInstallment e in _installmentList)
            {
                sum += (decimal) e.PrincipalCoeff;
            }
            return (double) sum;
        }

        public bool CheckIfSumIsOk(OLoanTypes loanType)
        {
            if (OLoanTypes.DecliningFixedPrincipal == loanType)
            {
                return 100 == _SumOfPrincipal() * 100;
            }
            return _SumOfPrincipal() * 100 == 100 && _SumOfInterest() * 100 == 100;
        }

        public double SumOfInterestCoeff
        {
            get
            {
                return _SumOfInterest();
            }
        }

        private double _SumOfInterest()
        {
            decimal sum = 0;
            foreach (ExoticInstallment e in _installmentList)
            {
                if (e.InterestCoeff.HasValue)
                    sum += (decimal) e.InterestCoeff;
            }
            return (double) sum;
        }

        public IEnumerator GetEnumerator()
        {
            return _installmentList.GetEnumerator();
        }
    }
}
