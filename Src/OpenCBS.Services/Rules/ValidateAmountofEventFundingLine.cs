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
                throw new OctopusFundingLineEventException(OctopusFundingLineEventExceptionEnum.AmountIsLessZero);
            if (e.Code == string.Empty)
               throw new OctopusFundingLineEventException(OctopusFundingLineEventExceptionEnum.AmountIsEmpty);
            if (e.Amount > e.FundingLine.RealRemainingAmount && e.Type == Enums.OFundingLineEventTypes.Disbursment)
               throw new OctopusFundingLineEventException(OctopusFundingLineEventExceptionEnum.CommitmentFinancialIsNotEnough);
            if (e.Amount > e.FundingLine.AnticipatedRemainingAmount && 
                e.Movement == Enums.OBookingDirections.Debit && e.Type != Enums.OFundingLineEventTypes.Disbursment)
                throw new OctopusFundingLineEventException(OctopusFundingLineEventExceptionEnum.CommitmentFinancialIsNotEnough);
        }
            

        #endregion
    }
}


