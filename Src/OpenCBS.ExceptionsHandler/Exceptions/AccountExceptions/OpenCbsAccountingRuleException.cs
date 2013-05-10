// LICENSE PLACEHOLDER

using System;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler.Exceptions.AccountExceptions
{
    [Serializable]
    public class OpenCbsAccountingRuleException : OpenCbsException
    {
        private readonly string _message;
        private OctopusAccountingRuleExceptionEnum _code;

        public OctopusAccountingRuleExceptionEnum Code
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

        public OpenCbsAccountingRuleException(OctopusAccountingRuleExceptionEnum exceptionCode)
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

        private string _findException(OctopusAccountingRuleExceptionEnum exceptionCode)
        {
            switch (exceptionCode)
            {
                case OctopusAccountingRuleExceptionEnum.GenericAccountIsInvalid:
                    return "AccountingRuleGenericAccountIsInvalid.Text";

                case OctopusAccountingRuleExceptionEnum.SpecificAccountIsInvalid:
                    return "AccountingRuleSpecificAccountIsInvalid.Text";

                case OctopusAccountingRuleExceptionEnum.GenericAndSpecificAccountsAreIdentical:
                    return "AccountingRuleGenericAndSpecificAccountsAreIdentical.Text";

                case OctopusAccountingRuleExceptionEnum.ClientTypeIsInvalid:
                    return "AccountingRuleClientTypeIsInvalid.Text";

                case OctopusAccountingRuleExceptionEnum.ProductTypeIsInvalid:
                    return "AccountingRuleProductTypeIsInvalid.Text";

                default:
                    return string.Empty;
            }
        }

    }

    [Serializable]
    public enum OctopusAccountingRuleExceptionEnum
    {
        GenericAccountIsInvalid,
        SpecificAccountIsInvalid,
        GenericAndSpecificAccountsAreIdentical,
        ProductTypeIsInvalid,
        ClientTypeIsInvalid
    }
}
