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
