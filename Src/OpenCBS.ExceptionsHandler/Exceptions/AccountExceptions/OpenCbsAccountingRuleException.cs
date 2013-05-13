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

namespace OpenCBS.ExceptionsHandler.Exceptions.AccountExceptions
{
    [Serializable]
    public class OpenCbsAccountingRuleException : OpenCbsException
    {
        private readonly string _message;
        private OpenCbsAccountingRuleExceptionEnum _code;

        public OpenCbsAccountingRuleExceptionEnum Code
        { get { return _code; } }

        protected OpenCbsAccountingRuleException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _message = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", _code);
            base.GetObjectData(info, context);
        }

		public OpenCbsAccountingRuleException()
		{
            _message = string.Empty;
		}

        public OpenCbsAccountingRuleException(OpenCbsAccountingRuleExceptionEnum exceptionCode)
        {
            _code = exceptionCode;
            _message = _findException(exceptionCode);
        }

        public OpenCbsAccountingRuleException(string message)
		{
			_message = message;
		}

		public override string ToString()
		{
			return _message;
		}

        private string _findException(OpenCbsAccountingRuleExceptionEnum exceptionCode)
        {
            switch (exceptionCode)
            {
                case OpenCbsAccountingRuleExceptionEnum.GenericAccountIsInvalid:
                    return "AccountingRuleGenericAccountIsInvalid.Text";

                case OpenCbsAccountingRuleExceptionEnum.SpecificAccountIsInvalid:
                    return "AccountingRuleSpecificAccountIsInvalid.Text";

                case OpenCbsAccountingRuleExceptionEnum.GenericAndSpecificAccountsAreIdentical:
                    return "AccountingRuleGenericAndSpecificAccountsAreIdentical.Text";

                case OpenCbsAccountingRuleExceptionEnum.ClientTypeIsInvalid:
                    return "AccountingRuleClientTypeIsInvalid.Text";

                case OpenCbsAccountingRuleExceptionEnum.ProductTypeIsInvalid:
                    return "AccountingRuleProductTypeIsInvalid.Text";

                default:
                    return string.Empty;
            }
        }

    }

    [Serializable]
    public enum OpenCbsAccountingRuleExceptionEnum
    {
        GenericAccountIsInvalid,
        SpecificAccountIsInvalid,
        GenericAndSpecificAccountsAreIdentical,
        ProductTypeIsInvalid,
        ClientTypeIsInvalid
    }
}
