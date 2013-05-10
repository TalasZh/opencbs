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
                    returned = "OpenCbsFundingLineEventExceptionCodeIsEmpty.Text";
                    break;

                case OpenCbsFundingLineEventExceptionEnum.NameIsEmpty:
                    returned = "OpenCbsFundingLineEventExceptionNameIsEmpty.Text";
                    break;


                case OpenCbsFundingLineEventExceptionEnum.AmountIsEmpty:
                    returned = "OpenCbsFundingLineEventExceptionAmountIsEmpty.Text";
                    break;

                case OpenCbsFundingLineEventExceptionEnum.AmountIsLessZero:
                    returned = "OpenCbsFundingLineEventExceptionAmountIsLessZero.Text";
                    break;
                case OpenCbsFundingLineEventExceptionEnum.DirectionIsEmpty:
                    returned = "OpenCbsFundingLineEventExceptionDirectionIsEmpty.Text";
                    break;
                case OpenCbsFundingLineEventExceptionEnum.AmountIsTaller:
                    returned = "OpenCbsFundingLineEventExceptionAmountIsTaller.Text";
                    break;

                case OpenCbsFundingLineEventExceptionEnum.AmountIsBigger:
                    returned = "OpenCbsFundingLineEventExceptionAmountIsBigger.Text";
                    break;
                case OpenCbsFundingLineEventExceptionEnum.CommitmentFinancialIsNotEnough:
                    returned = "OpenCbsFundingLineEventExceptionCommitmentFinancialIsNotEnough.Text";
                    break;

                case OpenCbsFundingLineEventExceptionEnum.AmountIsNonCompliant:
                    returned = "OpenCbsFundingLineEventExceptionAmountIsNonCompliant.Text";
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
