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
