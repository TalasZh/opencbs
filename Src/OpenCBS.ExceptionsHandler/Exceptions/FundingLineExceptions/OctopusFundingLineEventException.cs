using System;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler.Exceptions.FundingLineExceptions
{
   [Serializable]
   public class OctopusFundingLineEventException : OctopusException
 
   {
      private string _code;
        public OctopusFundingLineEventException(OctopusFundingLineEventExceptionEnum exception)
        {
            _code = _FindException(exception);
        }

        public override string ToString()
        {
            return _code;
        }

        protected OctopusFundingLineEventException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _code = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", _code);
            base.GetObjectData(info, context);
        }

        private string _FindException(OctopusFundingLineEventExceptionEnum exception)
        {
            string returned = String.Empty;
            switch (exception)
            {
                case OctopusFundingLineEventExceptionEnum.CodeIsEmpty:
                    returned = "OctopusFundingLineEventExceptionCodeIsEmpty.Text";
                    break;

                case OctopusFundingLineEventExceptionEnum.NameIsEmpty:
                    returned = "OctopusFundingLineEventExceptionNameIsEmpty.Text";
                    break;


                case OctopusFundingLineEventExceptionEnum.AmountIsEmpty:
                    returned = "OctopusFundingLineEventExceptionAmountIsEmpty.Text";
                    break;

                case OctopusFundingLineEventExceptionEnum.AmountIsLessZero:
                    returned = "OctopusFundingLineEventExceptionAmountIsLessZero.Text";
                    break;
                case OctopusFundingLineEventExceptionEnum.DirectionIsEmpty:
                    returned = "OctopusFundingLineEventExceptionDirectionIsEmpty.Text";
                    break;
                case OctopusFundingLineEventExceptionEnum.AmountIsTaller:
                    returned = "OctopusFundingLineEventExceptionAmountIsTaller.Text";
                    break;

                case OctopusFundingLineEventExceptionEnum.AmountIsBigger:
                    returned = "OctopusFundingLineEventExceptionAmountIsBigger.Text";
                    break;
                case OctopusFundingLineEventExceptionEnum.CommitmentFinancialIsNotEnough:
                    returned = "OctopusFundingLineEventExceptionCommitmentFinancialIsNotEnough.Text";
                    break;

                case OctopusFundingLineEventExceptionEnum.AmountIsNonCompliant:
                    returned = "OctopusFundingLineEventExceptionAmountIsNonCompliant.Text";
                    break;
            }
            return returned;
        }


       
    }

    [Serializable]
     public enum OctopusFundingLineEventExceptionEnum
        {
            CodeIsEmpty,
            NameIsEmpty,
            AmountIsEmpty,
            AmountIsLessZero,
            DirectionIsEmpty,
            AmountIsTaller,
            CommitmentFinancialIsNotEnough,
            AmountIsNonCompliant,
            AmountIsBigger
        }
   }
