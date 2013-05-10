// LICENSE PLACEHOLDER

using System;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler.Exceptions.FundingLineExceptions
{
   [Serializable]
   public class OpenCbsFundingLineEventException : OpenCbsException
 
   {
      private string _code;
        public OpenCbsFundingLineEventException(OpenCbsFundingLineEventExceptionEnum exception)
        {
            _code = _FindException(exception);
        }

        public override string ToString()
        {
            return _code;
        }

        protected OpenCbsFundingLineEventException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _code = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", _code);
            base.GetObjectData(info, context);
        }

        private string _FindException(OpenCbsFundingLineEventExceptionEnum exception)
        {
            string returned = String.Empty;
            switch (exception)
            {
                case OpenCbsFundingLineEventExceptionEnum.CodeIsEmpty:
                    returned = "OctopusFundingLineEventExceptionCodeIsEmpty.Text";
                    break;

                case OpenCbsFundingLineEventExceptionEnum.NameIsEmpty:
                    returned = "OctopusFundingLineEventExceptionNameIsEmpty.Text";
                    break;


                case OpenCbsFundingLineEventExceptionEnum.AmountIsEmpty:
                    returned = "OctopusFundingLineEventExceptionAmountIsEmpty.Text";
                    break;

                case OpenCbsFundingLineEventExceptionEnum.AmountIsLessZero:
                    returned = "OctopusFundingLineEventExceptionAmountIsLessZero.Text";
                    break;
                case OpenCbsFundingLineEventExceptionEnum.DirectionIsEmpty:
                    returned = "OctopusFundingLineEventExceptionDirectionIsEmpty.Text";
                    break;
                case OpenCbsFundingLineEventExceptionEnum.AmountIsTaller:
                    returned = "OctopusFundingLineEventExceptionAmountIsTaller.Text";
                    break;

                case OpenCbsFundingLineEventExceptionEnum.AmountIsBigger:
                    returned = "OctopusFundingLineEventExceptionAmountIsBigger.Text";
                    break;
                case OpenCbsFundingLineEventExceptionEnum.CommitmentFinancialIsNotEnough:
                    returned = "OctopusFundingLineEventExceptionCommitmentFinancialIsNotEnough.Text";
                    break;

                case OpenCbsFundingLineEventExceptionEnum.AmountIsNonCompliant:
                    returned = "OctopusFundingLineEventExceptionAmountIsNonCompliant.Text";
                    break;
            }
            return returned;
        }


       
    }

    [Serializable]
     public enum OpenCbsFundingLineEventExceptionEnum
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
