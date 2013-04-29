using System;
using Octopus.CoreDomain.Events.Loan;

namespace Octopus.CoreDomain.Events
{
    [Serializable]
    public class PendingRepaymentEvent : RepaymentEvent
    {
        public PendingRepaymentEvent(RepaymentEvent pEvent)
        {
            if (pEvent is RescheduledLoanRepaymentEvent)
                Code = "PRLR";
            else if (pEvent is BadLoanRepaymentEvent)
                Code = "PBLR";
            else if (pEvent is RepaymentOverWriteOffEvent)
                Code = "PRWO";
            else
                Code = "PERE";
        }

        public PendingRepaymentEvent(string pCode)
        {
            _code = pCode;
        }

        public override string Code
        {
            get { return _code; }
        }

        public override Event Copy()
        {
            return (PendingRepaymentEvent)MemberwiseClone();
        }

        public RepaymentEvent CopyAsRepaymentEvent()
        {
            RepaymentEvent rPe;

            if (Code == "PRLR")
                rPe = new RescheduledLoanRepaymentEvent();
            else if (Code == "PBLR")
                rPe = new BadLoanRepaymentEvent();
            else if (Code == "PRWO")
                rPe = new RepaymentOverWriteOffEvent();
            else
                rPe = new RepaymentEvent();

            rPe.AccruedInterest = this.AccruedInterest;
            rPe.Cancelable = this.Cancelable;
            rPe.ClientType = this.ClientType;
            rPe.Commissions = this.Commissions;
            rPe.Date = this.Date;
            rPe.InstallmentNumber = this.InstallmentNumber;
            rPe.InterestPrepayment = this.InterestPrepayment;
            rPe.Interests = this.Interests;
            rPe.PastDueDays = this.PastDueDays;
            rPe.Penalties = this.Penalties;
            rPe.Principal = this.Principal;
            rPe.RepaymentType = this.RepaymentType;

            return rPe;
        }
    }
}
