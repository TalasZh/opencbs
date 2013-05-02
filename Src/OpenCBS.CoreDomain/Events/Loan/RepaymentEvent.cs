// LICENSE PLACEHOLDER

using System;
using OpenCBS.CoreDomain.Events.Loan;
using OpenCBS.Enums;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Events
{
    [Serializable]
	public class RepaymentEvent : Event
	{
        public RepaymentEvent() 
        { 
            Penalties = 0; 
            Commissions = 0;
            CalculatedPenalties = 0;
            WrittenOffPenalties = 0;
            UnpaidPenalties = 0;
        }

		public override string Code
		{
            get
            {
                string code = "RGLE";
                switch (RepaymentType)
                {
                    case OPaymentType.TotalPayment:
                        {
                            code = "ATR";
                            break;
                        }
                    case OPaymentType.PartialPayment:
                        {
                            code = "APR";
                            break;
                        }
                    case OPaymentType.ProportionalPayment:
                        {
                            code = "APR";
                            break;
                        }
                    case OPaymentType.PersonTotalPayment:
                        {
                            code = "APTR";
                            break;
                        }
                }

                return code;
            }
			set
            {
                _code = value;
            }
		}

        public int PastDueDays { get; set; }
        public OCurrency Principal { get; set; }
        public OCurrency Commissions { get; set; }
        public OCurrency Penalties { get; set; }
        public OCurrency Interests { get; set; }
        public OCurrency AccruedInterest { get; set; }
        public OCurrency InterestPrepayment { get; set; }
        public OCurrency CalculatedPenalties { get; set; }
        public OCurrency WrittenOffPenalties { get; set; }
        public OCurrency UnpaidPenalties { get; set; }

//        public override PaymentMethod PaymentMethod { get; set; }
        public int? PaymentMethodId { get; set; }
        /// <summary>
        /// Sum of penalties and commissions
        /// </summary>
		public OCurrency Fees
		{
			get
			{
				return Penalties + Commissions;
			}
		}
        
        public override Event Copy()
		{
			return (RepaymentEvent)MemberwiseClone();
		}
	}
}
