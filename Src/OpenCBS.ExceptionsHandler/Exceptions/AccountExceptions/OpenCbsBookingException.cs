// LICENSE PLACEHOLDER

using System;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler
{
	/// <summary>
	/// Summary description for OpenCbsAccountException.
	/// </summary>
    [Serializable]
    public class OpenCbsBookingException : OpenCbsException
	{
		private string _code;
        public OpenCbsBookingException(OpenCbsBookingExceptionsEnum exceptionCode)
		{
			_code = _FindException(exceptionCode);
		}

		public override string ToString()
		{
			return _code;
		}

        protected OpenCbsBookingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _code = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", _code);
            base.GetObjectData(info, context);
        }

        private string _FindException(OpenCbsBookingExceptionsEnum exceptionId)
		{
			string returned = String.Empty;
			switch(exceptionId)
			{
				case OpenCbsBookingExceptionsEnum.NameIsEmpty:
					returned = "OctopusBookingExceptionsNameIsEmpty.Text";
                    break;
                case OpenCbsBookingExceptionsEnum.CreditAccountIsEmpty:
                    returned = "OctopusBookingExceptionsCreditAccountIsEmpty.Text";
                    break;
                case OpenCbsBookingExceptionsEnum.DebitAccountIsEmpty:
                    returned = "OctopusBookingExceptionsDebitAccountIsEmpty.Text";
                    break;   
                case OpenCbsBookingExceptionsEnum.DebitAndCreditAccountAreIdentical:
                    returned = "OctopusBookingExceptionDebitAndCreditAccountAreIdentical.Text";
                    break;
                case OpenCbsBookingExceptionsEnum.BookingIsEmpty:
                    returned = "BookingIsEmpty.Text";
                    break;
                case OpenCbsBookingExceptionsEnum.NotDeletableClosure:
                    returned = "OctopusNotDeletableClosure.Text";
                    break;
			}
			return returned;
		}
	}

    [Serializable]
    public enum OpenCbsBookingExceptionsEnum
	{
	    NameIsEmpty,
        DebitAccountIsEmpty,
        CreditAccountIsEmpty,
        DebitAndCreditAccountAreIdentical,
        BookingIsEmpty,
        NotDeletableClosure
	}
}
