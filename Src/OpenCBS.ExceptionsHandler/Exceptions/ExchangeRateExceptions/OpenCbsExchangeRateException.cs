// Octopus MFS is an integrated suite for managing a Micro Finance Institution: 
// clients, contracts, accounting, reporting and risk
// Copyright © 2006,2007 OCTO Technology & OXUS Development Network
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License along
// with this program; if not, write to the Free Software Foundation, Inc.,
// 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
// Website: http://www.opencbs.com
// Contact: contact@opencbs.com

using System;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler
{
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
