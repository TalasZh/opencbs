// LICENSE PLACEHOLDER

using System;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.Shared;
using OpenCBS.Enums;

namespace OpenCBS.CoreDomain.Contracts.Loans.Installments
{
    /// <summary>
    /// Description r�sum�e de Installment.
    /// </summary>
    [Serializable]
    public class Installment : IInstallment
    {
        private OCurrency _interestsRepayment;
        private OCurrency _capitalRepayment;
        private OCurrency _paidInterests = 0;
        private OCurrency _paidCapital = 0;
        private OCurrency _paidFees = 0;
        private OCurrency _paidCommision = 0;
        private OCurrency _unpaidCommision = 0;
        private OCurrency _olbAfterRepayment = 0;
        private bool _pending;
        
        public Installment()
        {
            CalculatedPenalty = 0m;
            IsLastToRepay = false;
        }
        public Installment(DateTime expectedDate,OCurrency interestRepayment,OCurrency capitalRepayment,
                           OCurrency paidCapital, OCurrency paidInterests, OCurrency paidFees, DateTime? paidDate, int number)
        {
            ExpectedDate = expectedDate;
            _interestsRepayment = interestRepayment;
            _capitalRepayment = capitalRepayment;
            _paidCapital = paidCapital;
            _paidInterests = paidInterests;
            _paidFees = paidFees;
            PaidDate = paidDate;
            Number = number;
            CalculatedPenalty = 0m;
            IsLastToRepay = false;
        }

        public DateTime StartDate { get; set; }
        public DateTime ExpectedDate { get; set; }

        public OCurrency FeesUnpaid { get; set; }

        public bool NotPaidYet { get; set; }

        public string Comment { get; set; }
        public bool IsPending
        {
            get { return _pending; }
            set { _pending = value; }
        }

        public OCurrency InterestsRepayment
        {
            get{ return _interestsRepayment;}
            set{_interestsRepayment = value;}
        }

        public OCurrency PaidFees
        {
            get { return _paidFees; }
            set { _paidFees = value; }
        }

        public OCurrency PaidCommissions
        {
            get { return _paidCommision; }
            set { _paidCommision = value; }
        }

        public OCurrency CommissionsUnpaid
        {
            get { return _unpaidCommision; }
            set { _unpaidCommision = value; }
        }

        public OCurrency CapitalRepayment
        {
            get{return _capitalRepayment;}
            set{_capitalRepayment = value;}
        }

        public OCurrency   PaidCapital
        {
            get{return _paidCapital;}
            set{_paidCapital = value;}
        }

        public OCurrency   PaidInterests
        {
            get{return _paidInterests;}
            set{_paidInterests = value;}
        }

        public DateTime? PaidDate { get; set; }

        public int Number { get; set; }

        public OCurrency Amount
        {
            get
            {
                return  _interestsRepayment + _capitalRepayment;
            }
        }

        public OCurrency   AmountHasToPayWithInterest
        {
            get 
            { 
                return AmountComparer.Compare(_interestsRepayment + _capitalRepayment - _paidCapital - _paidInterests, 0) == 0
                           ? 0
                           : _interestsRepayment + _capitalRepayment - _paidCapital - _paidInterests;
            }
        }

        public OCurrency   PrincipalHasToPay
        {
            get {
                return AmountComparer.Compare(_capitalRepayment - _paidCapital, 0) == 0
                           ? 0
                           : _capitalRepayment - _paidCapital;
            }
        }

        public OCurrency   InterestHasToPay
        {
            get {
                return AmountComparer.Compare(_interestsRepayment - _paidInterests, 0) == 0
                           ? 0
                           : _interestsRepayment - _paidInterests;
            }
        }


        /// <summary>
        /// OLB is the OLB before installment repayment
        /// </summary>
        public OCurrency OLB { get; set; }
        public OCurrency Proportion { get; set; }
        public OCurrency OLBAfterRepayment
        {
            get{return OLB - CapitalRepayment;}
            set{_olbAfterRepayment = value;}
        }

        /// <summary>
        /// This method determines if an installment is fully repaid
        /// Business rule : to be considered as repaid, an installment must have interestsRepayment and capitalRepayment equal to
        /// paidCapital and paidInterests (effective amounts paid by the client)
        /// </summary>
        public bool IsRepaid
        {
            get
            {
                return AmountComparer.Equals(_interestsRepayment, _paidInterests) && AmountComparer.Equals(Math.Round(_capitalRepayment.Value, 2, MidpointRounding.AwayFromZero), _paidCapital) ? true : false;
            }
        }

        public bool IsPartiallyRepaid
        {
            get
            {
                OCurrency amount = _capitalRepayment + _interestsRepayment - _paidInterests - _paidCapital;
                return amount != 0 && !AmountComparer.Equals(amount, _capitalRepayment + _interestsRepayment);
            }
        }

        public Installment Copy()
        {
            return (Installment)MemberwiseClone();
        }

        public override bool Equals(object obj)
        {
            Installment compareTo = obj as Installment;
            if (null == compareTo) return false;
            if (Number != compareTo.Number) return false;
            if (CapitalRepayment != compareTo.CapitalRepayment) return false;
            if (InterestsRepayment != compareTo.InterestsRepayment) return false;
            if (!ExpectedDate.Equals(compareTo.ExpectedDate)) return false;
            return true;
        }

        public override int GetHashCode()
        {
            return Number;
        }

        public OCurrency CalculatedPenalty { get; set; }
        public bool IsLastToRepay { get; set; }
    }
}
