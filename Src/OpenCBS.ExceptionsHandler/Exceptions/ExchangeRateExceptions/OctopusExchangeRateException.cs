// LICENSE PLACEHOLDER

using System;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler
{
	/// <summary>
	/// Summary description for OctopusExchangeRateExceptions.
	/// </summary>
    [Serializable]
    public class OctopusExchangeRateException : OctopusException
	{
		private string _code;
		public OctopusExchangeRateException(OctopusExchangeRateExceptionEnum exception)
		{
			_code = _FindException(exception);
		}

		public override string ToString()
		{
			return _code;
		}

        protected OctopusExchangeRateException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _code = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", _code);
            base.GetObjectData(info, context);
        }

		private string _FindException(OctopusExchangeRateExceptionEnum exception)
		{
			string returned = String.Empty;
			switch(exception)
			{
				case OctopusExchangeRateExceptionEnum.ExchangeRateIsNull:
					returned = "ExchangeRateExceptionExchangeRateIsNull.Text";
					break;
				
				case OctopusExchangeRateExceptionEnum.DateIsNull:
					returned = "ExchangeRateExceptionDateIsNull.Text";
					break;

				case OctopusExchangeRateExceptionEnum.RateIsEmpty:
					returned = "ExchangeRateExceptionRateIsEmpty.Text";
					break;
                case OctopusExchangeRateExceptionEnum.CurrencyIsEmpty:
                    returned = "ExchangeRateExceptionCurrencyIsEmpty.Text";
                    break;
                case OctopusExchangeRateExceptionEnum.ThisCurrencyIsPivot:
                    returned = "ExchangeRateExceptionThisCurrencyIsPivot.Text";
                    break;
			}
			return returned;
		}
	}

    [Serializable]
	public enum OctopusExchangeRateExceptionEnum
	{
		ExchangeRateIsNull,
		DateIsNull,
		RateIsEmpty,
        CurrencyIsEmpty,
        ThisCurrencyIsPivot
	}
}
