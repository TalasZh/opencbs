using System;

namespace OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment
{
    [Serializable]
    public class NonRepaymentPenalties
    {
        private double _initialAmount; 
        private double _olb;        
        private double _overDuePrincipal;   
        private double _overDueInterest;

        public NonRepaymentPenalties()
        {
        }

        public NonRepaymentPenalties(double pInitialAmount, double pOlb, double pOverduePrincipal, double pOverdueInterest)
        {
            _initialAmount = pInitialAmount;
            _olb = pOlb;
            _overDueInterest = pOverdueInterest;
            _overDuePrincipal = pOverduePrincipal;
        }

        public double InitialAmount
        {
            get { return _initialAmount; }
            set { _initialAmount = value; }
        }

        public double OLB
        {
            get { return _olb; }
            set { _olb = value; }
        }

        public double OverDuePrincipal
        {
            get { return _overDuePrincipal; }
            set { _overDuePrincipal = value; }
        }

        public double OverDueInterest
        {
            get { return _overDueInterest; }
            set { _overDueInterest = value; }
        }
    }

    [Serializable]
    public class NonRepaymentPenaltiesNullableValues
    {
        private double? _initialAmount; 
        private double? _olb;        
        private double? _overDuePrincipal;   
        private double? _overDueInterest;

        public NonRepaymentPenaltiesNullableValues()
        {
        }

        public NonRepaymentPenaltiesNullableValues(double? pInitialAmount, double? pOlb, double? pOverduePrincipal, double? pOverdueInterest)
        {
            _initialAmount = pInitialAmount;
            _olb = pOlb;
            _overDueInterest = pOverdueInterest;
            _overDuePrincipal = pOverduePrincipal;
        }

        public double? InitialAmount
        {
            get { return _initialAmount; }
            set { _initialAmount = value; }
        }

        public double? OLB 
        {
            get { return _olb; }
            set { _olb = value; }
        }

        public double? OverDuePrincipal
        {
            get { return _overDuePrincipal; }
            set { _overDuePrincipal = value; }
        }

        public double? OverDueInterest
        {
            get { return _overDueInterest; }
            set { _overDueInterest = value; }
        }
    }
}