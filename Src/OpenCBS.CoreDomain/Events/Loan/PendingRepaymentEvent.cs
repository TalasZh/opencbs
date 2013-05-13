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

namespace OpenCBS.CoreDomain.Events
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
