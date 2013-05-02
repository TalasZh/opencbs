using System;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler.Exceptions.FundingLineExceptions
{

    [Serializable]
    public class OctopusFundingLineException : OctopusException
    {
         private string _code;
         public OctopusFundingLineException(OctopusFundingLineExceptionEnum exception)
        {
            _code = _FindException(exception);
        }

        public override string ToString()
        {
            return _code;
        }

        protected OctopusFundingLineException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _code = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", _code);
            base.GetObjectData(info, context);
        }

        private string _FindException(OctopusFundingLineExceptionEnum exception)
        {
            string returned = String.Empty;
            switch (exception)
            {
                case OctopusFundingLineExceptionEnum.CodeIsEmpty:
                    returned = "OctopusFundingLineExceptionCodeIsEmpty.Text";
                    break;
                case OctopusFundingLineExceptionEnum.NameIsEmpty:
                    returned = "OctopusFundingLineExceptionNameIsEmpty.Text";
                    break;
                case OctopusFundingLineExceptionEnum.BeginDateGreaterEndDate:
                    returned = "OctopusFundingLineExceptionBeginDateGreaterEndDate.Text";
                    break;
                case OctopusFundingLineExceptionEnum.BadFundingLineID:
                    returned = "OctopusFundingLineExceptionBadFundingLineId.Text";
                    break;
                case OctopusFundingLineExceptionEnum.FundingLineNameExists:
                    returned = "OctopusFundingLineExceptionFundingLineNameExists.Text";
                    break;
                case OctopusFundingLineExceptionEnum.CurrencyIsEmpty:
                    returned = "CurrencyIsEmpty.Text";
                    break;
            }
            return returned;
        }
    }


    [Serializable]
    public enum OctopusFundingLineExceptionEnum
    {
        CodeIsEmpty,
        NameIsEmpty,
        BeginDateGreaterEndDate,
        BadFundingLineID,
        FundingLineNameExists,
        CurrencyIsEmpty
    }


}
