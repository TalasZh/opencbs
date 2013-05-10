// LICENSE PLACEHOLDER

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
