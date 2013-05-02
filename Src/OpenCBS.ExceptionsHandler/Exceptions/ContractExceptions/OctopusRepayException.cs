// LICENSE PLACEHOLDER

using System;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler
{
	/// <summary>
	/// Summary description for OctopusRepayException.
	/// </summary>
    [Serializable]
    public class OctopusRepayException : OctopusException
	{
		private string _code;
		public OctopusRepayException(OctopusRepayExceptionsEnum exceptionCode)
		{
			_code = _FindException(exceptionCode);
		}

		public override string ToString()
		{
			return _code;
		}

        protected OctopusRepayException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            _code = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", _code);
            base.GetObjectData(info, context);
        }

		private string _FindException(OctopusRepayExceptionsEnum exceptionId)
		{
			string returned = String.Empty;
			switch(exceptionId)
			{
				case OctopusRepayExceptionsEnum.NegativeAmount:
					returned = "RepayExceptionNegativeAmount.Text";
					break;

				case OctopusRepayExceptionsEnum.AmountGreaterThanTotalRemainingAmount:
					returned = "RepayExceptionAmountGreaterThanTotalRemainingAmount.Text";
					break;
					
				case OctopusRepayExceptionsEnum.AmountIsNull:
					returned = "RepayExceptionAmountIsNull.Text";
					break;
					
				case OctopusRepayExceptionsEnum.MaxPastDueDaysReached:
					returned = "RepayExceptionMaxPastDueDaysReached.Text";
                    break;

                case OctopusRepayExceptionsEnum.CantRepayInTheFutur:
                    returned = "CantRepayInTheFutur.Text";
                    break;

                case OctopusRepayExceptionsEnum.DecimalAmount:
                    returned = "DecimalAmount.Text";
                    break;

                case OctopusRepayExceptionsEnum.AllInstallmentRepaid:
                    returned = "AllInstallmentRepaid.Text";
                    break;

                case OctopusRepayExceptionsEnum.RepaymentBeforeDisburse:
                    returned = "RepaymentBeforeDisburse.Text";
                    break;

                case OctopusRepayExceptionsEnum.RepaymentBeforeLastEventDate:
			        returned = "RepaymentBeforeLastEventDate.Text";
                    break;
			}
			return returned;
		}
	}

    [Serializable]
	public enum OctopusRepayExceptionsEnum
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
