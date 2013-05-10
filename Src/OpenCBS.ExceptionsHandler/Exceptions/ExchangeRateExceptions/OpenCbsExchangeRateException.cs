// LICENSE PLACEHOLDER

using System;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler
{
	/// <summary>
	/// Summary description for OctopusExchangeRateExceptions.
	/// </summary>
    [Serializable]
    public class OpenCbsExchangeRateException : OpenCbsException
	{
		private string _code;
		public OpenCbsExchangeRateException(OpenCbsExchangeRateExceptionEnum exception)
		{
			_code = _FindException(exception);
		}

		public override string ToString()
		{
			return _code;
		}

        protected OpenCbsExchangeRateException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _code = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", _code);
            base.GetObjectData(info, context);
        }

		private string _FindException(OpenCbsExchangeRateExceptionEnum exception)
		{
			string returned = String.Empty;
			switch(exception)
			{
				case OpenCbsExchangeRateExceptionEnum.ExchangeRateIsNull:
					returned = "ExchangeRateExceptionExchangeRateIsNull.Text";
					break;
				
				case OpenCbsExchangeRateExceptionEnum.DateIsNull:
					returned = "ExchangeRateExceptionDateIsNull.Text";
					break;

				case OpenCbsExchangeRateExceptionEnum.RateIsEmpty:
					returned = "ExchangeRateExceptionRateIsEmpty.Text";
					break;
                case OpenCbsExchangeRateExceptionEnum.CurrencyIsEmpty:
                    returned = "ExchangeRateExceptionCurrencyIsEmpty.Text";
                    break;
                case OpenCbsExchangeRateExceptionEnum.ThisCurrencyIsPivot:
                    returned = "ExchangeRateExceptionThisCurrencyIsPivot.Text";
                    break;
			}
			return returned;
		}
	}

    [Serializable]
	public enum OpenCbsExchangeRateExceptionEnum
	{
		ExchangeRateIsNull,
		DateIsNull,
		RateIsEmpty,
        CurrencyIsEmpty,
        ThisCurrencyIsPivot
	}
}
