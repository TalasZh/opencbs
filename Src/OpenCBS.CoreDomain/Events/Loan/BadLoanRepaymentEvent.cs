// LICENSE PLACEHOLDER

using System;
using OpenCBS.CoreDomain.Events.Loan;
using OpenCBS.Enums;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Events
{
    [Serializable]
	public class BadLoanRepaymentEvent : RepaymentEvent
	{
        public BadLoanRepaymentEvent() { AccruedProvision = 0; OLB = 0; }

		public override string Code
		{
			get
			{
                string code = "RBLE";
                switch (RepaymentType)
                {
                    case OPaymentType.TotalPayment:
                    {
                        if (PastDueDays == 0)
                            code = "ATR";
                        break;
                    }
                    case OPaymentType.PartialPayment:
                    {
                        if(PastDueDays == 0)
                            code = "APR";
                        break;
                    }
                    case OPaymentType.PersonTotalPayment:
                    {
                        if (PastDueDays == 0)
                            code = "APTR";
                        break;
                    }
                }

                return code;
			}
		}

        public OCurrency AccruedProvision { get; set; }
        public OCurrency OLB { get; set; }

        public override Event Copy()
		{
			return (BadLoanRepaymentEvent) MemberwiseClone();
		}
	}
}
