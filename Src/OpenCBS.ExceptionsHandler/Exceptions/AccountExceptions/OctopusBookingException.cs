// LICENSE PLACEHOLDER

using System;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler
{
	/// <summary>
	/// Summary description for OctopusAccountException.
	/// </summary>
    [Serializable]
    public class OctopusBookingException : OctopusException
	{
		private string _code;
        public OctopusBookingException(OctopusBookingExceptionsEnum exceptionCode)
		{
			_code = _FindException(exceptionCode);
		}

		public override string ToString()
		{
			return _code;
		}

        protected OctopusBookingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _code = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", _code);
            base.GetObjectData(info, context);
        }

        private string _FindException(OctopusBookingExceptionsEnum exceptionId)
		{
			string returned = String.Empty;
			switch(exceptionId)
			{
				case OctopusBookingExceptionsEnum.NameIsEmpty:
					returned = "OctopusBookingExceptionsNameIsEmpty.Text";
                    break;
                case OctopusBookingExceptionsEnum.CreditAccountIsEmpty:
                    returned = "OctopusBookingExceptionsCreditAccountIsEmpty.Text";
                    break;
                case OctopusBookingExceptionsEnum.DebitAccountIsEmpty:
                    returned = "OctopusBookingExceptionsDebitAccountIsEmpty.Text";
                    break;   
                case OctopusBookingExceptionsEnum.DebitAndCreditAccountAreIdentical:
                    returned = "OctopusBookingExceptionDebitAndCreditAccountAreIdentical.Text";
                    break;
                case OctopusBookingExceptionsEnum.BookingIsEmpty:
                    returned = "BookingIsEmpty.Text";
                    break;
                case OctopusBookingExceptionsEnum.NotDeletableClosure:
                    returned = "OctopusNotDeletableClosure.Text";
                    break;
			}
			return returned;
		}
	}

    [Serializable]
    public enum OctopusBookingExceptionsEnum
	{
	    NameIsEmpty,
        DebitAccountIsEmpty,
        CreditAccountIsEmpty,
        DebitAndCreditAccountAreIdentical,
        BookingIsEmpty,
        NotDeletableClosure
	}
}
