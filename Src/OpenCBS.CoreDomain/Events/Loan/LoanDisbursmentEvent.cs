// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using OpenCBS.CoreDomain.Events.Loan;
using OpenCBS.Shared;
using OpenCBS.CoreDomain.Accounting;

namespace OpenCBS.CoreDomain.Events
{
    [Serializable]
	public class LoanDisbursmentEvent : Event
	{
        public OCurrency Amount { get; set; }
        public List<LoanEntryFeeEvent> Commissions { get; set; }
//        public CreditInsuranceEvent CreditInsuranceEvent { get; set; }
        public OCurrency Interest { get; set; }
        public OCurrency Fee { get; set; }
//        public override PaymentMethod PaymentMethod { get; set; }

        public int? PaymentMethodId { get; set; }
		public override string Code
		{
			get { return "LODE"; }
			set { _code = value; }
		}

        public override string Description { get; set; }

        public override Event Copy()
		{
			return (LoanDisbursmentEvent) MemberwiseClone();
		}
	}
}
