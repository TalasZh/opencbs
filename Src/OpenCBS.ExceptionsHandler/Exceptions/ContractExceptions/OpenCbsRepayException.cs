// LICENSE PLACEHOLDER

using System;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler
{
	/// <summary>
	/// Summary description for OpenCbsRepayException.
	/// </summary>
    [Serializable]
    public class OpenCbsRepayException : OpenCbsException
	{
		private string _code;
		public OpenCbsRepayException(OpenCbsRepayExceptionsEnum exceptionCode)
		{
			_code = _FindException(exceptionCode);
		}

		public override string ToString()
		{
			return _code;
		}

        protected OpenCbsRepayException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            _code = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", _code);
            base.GetObjectData(info, context);
        }

		private string _FindException(OpenCbsRepayExceptionsEnum exceptionId)
		{
			string returned = String.Empty;
			switch(exceptionId)
			{
				case OpenCbsRepayExceptionsEnum.NegativeAmount:
					returned = "RepayExceptionNegativeAmount.Text";
					break;

				case OpenCbsRepayExceptionsEnum.AmountGreaterThanTotalRemainingAmount:
					returned = "RepayExceptionAmountGreaterThanTotalRemainingAmount.Text";
					break;
					
				case OpenCbsRepayExceptionsEnum.AmountIsNull:
					returned = "RepayExceptionAmountIsNull.Text";
					break;
					
				case OpenCbsRepayExceptionsEnum.MaxPastDueDaysReached:
					returned = "RepayExceptionMaxPastDueDaysReached.Text";
                    break;

                case OpenCbsRepayExceptionsEnum.CantRepayInTheFutur:
                    returned = "CantRepayInTheFutur.Text";
                    break;

                case OpenCbsRepayExceptionsEnum.DecimalAmount:
                    returned = "DecimalAmount.Text";
                    break;

                case OpenCbsRepayExceptionsEnum.AllInstallmentRepaid:
                    returned = "AllInstallmentRepaid.Text";
                    break;

                case OpenCbsRepayExceptionsEnum.RepaymentBeforeDisburse:
                    returned = "RepaymentBeforeDisburse.Text";
                    break;

                case OpenCbsRepayExceptionsEnum.RepaymentBeforeLastEventDate:
			        returned = "RepaymentBeforeLastEventDate.Text";
                    break;
			}
			return returned;
		}
	}

    [Serializable]
	public enum OpenCbsRepayExceptionsEnum
	{
		NegativeAmount,
		AmountIsNull,
        CantRepayInTheFutur,
		AmountGreaterThanTotalRemainingAmount,
		MaxPastDueDaysReached,
        DecimalAmount,
        AllInstallmentRepaid,
        RepaymentBeforeDisburse,
        RepaymentBeforeLastEventDate
	}
}
