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

using OpenCBS.CoreDomain.FundingLines;
using OpenCBS.ExceptionsHandler.Exceptions.FundingLineExceptions;

namespace OpenCBS.Services.Rules
{
   class ValidateAmountofEventFundingLine:IStrategyFundingLineEvent
   {
      #region IStrategyFundingLineEvent Members

      public void ApplyRules(FundingLineEvent e)
        {

            if (e.Amount <= decimal.Zero)
                throw new OpenCbsFundingLineEventException(OpenCbsFundingLineEventExceptionEnum.AmountIsLessZero);
            if (e.Code == string.Empty)
               throw new OpenCbsFundingLineEventException(OpenCbsFundingLineEventExceptionEnum.AmountIsEmpty);
            if (e.Amount > e.FundingLine.RealRemainingAmount && e.Type == Enums.OFundingLineEventTypes.Disbursment)
               throw new OpenCbsFundingLineEventException(OpenCbsFundingLineEventExceptionEnum.CommitmentFinancialIsNotEnough);
            if (e.Amount > e.FundingLine.AnticipatedRemainingAmount && 
                e.Movement == Enums.OBookingDirections.Debit && e.Type != Enums.OFundingLineEventTypes.Disbursment)
                throw new OpenCbsFundingLineEventException(OpenCbsFundingLineEventExceptionEnum.CommitmentFinancialIsNotEnough);
        }
            

        #endregion
    }
}


