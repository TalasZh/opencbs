// LICENSE PLACEHOLDER

using System;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler.Exceptions.FundingLineExceptions
{

    [Serializable]
    public class OpenCbsFundingLineException : OpenCbsException
    {
         private string _code;
         public OpenCbsFundingLineException(OpenCbsFundingLineExceptionEnum exception)
        {
            _code = _FindException(exception);
        }

        public override string ToString()
        {
            return _code;
        }

        protected OpenCbsFundingLineException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _code = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", _code);
            base.GetObjectData(info, context);
        }

        private string _FindException(OpenCbsFundingLineExceptionEnum exception)
        {
            string returned = String.Empty;
            switch (exception)
            {
                case OpenCbsFundingLineExceptionEnum.CodeIsEmpty:
                    returned = "OpenCbsFundingLineExceptionCodeIsEmpty.Text";
                    break;
                case OpenCbsFundingLineExceptionEnum.NameIsEmpty:
                    returned = "OpenCbsFundingLineExceptionNameIsEmpty.Text";
                    break;
                case OpenCbsFundingLineExceptionEnum.BeginDateGreaterEndDate:
                    returned = "OpenCbsFundingLineExceptionBeginDateGreaterEndDate.Text";
                    break;
                case OpenCbsFundingLineExceptionEnum.BadFundingLineID:
                    returned = "OpenCbsFundingLineExceptionBadFundingLineId.Text";
                    break;
                case OpenCbsFundingLineExceptionEnum.FundingLineNameExists:
                    returned = "OpenCbsFundingLineExceptionFundingLineNameExists.Text";
                    break;
                case OpenCbsFundingLineExceptionEnum.CurrencyIsEmpty:
                    returned = "CurrencyIsEmpty.Text";
                    break;
            }
            return returned;
        }
    }


    [Serializable]
    public enum OpenCbsFundingLineExceptionEnum
    {
        CodeIsEmpty,
        NameIsEmpty,
        BeginDateGreaterEndDate,
        BadFundingLineID,
        FundingLineNameExists,
        CurrencyIsEmpty
    }


}
