// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler
{
    /// <summary>
    /// Summary description for OpenCbsSavingException.
    /// </summary>
    [Serializable]
    public class OpenCbsTellerException : OpenCbsException
    {
        private readonly string _message;
        private OpenCbsTellerExceptionEnum _code;
        public OpenCbsTellerExceptionEnum Code { get { return _code; } }

        protected OpenCbsTellerException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _code = (OpenCbsTellerExceptionEnum)info.GetInt32("Code");
        }

        protected OpenCbsTellerException(SerializationInfo info, StreamingContext context, List<string> options)
            : base(info, context)
        {
            _code = (OpenCbsTellerExceptionEnum)info.GetInt32("Code");
            AdditionalOptions = options;
        }


        public OpenCbsTellerException()
        {
            _message = string.Empty;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", _code);
            base.GetObjectData(info, context);
        }

        public OpenCbsTellerException(OpenCbsTellerExceptionEnum exceptionCode)
        {
            _code = exceptionCode;
            _message = FindException(exceptionCode);
        }

        public OpenCbsTellerException(string message)
        {
            _message = message;
        }

        public override string ToString()
        {
            return _message;
        }

        private static string FindException(OpenCbsTellerExceptionEnum exceptionId)
        {
            string returned = String.Empty;
            switch (exceptionId)
            {
                case OpenCbsTellerExceptionEnum.NameIsEmpty:
                    returned = "TellerNameIsEmpty.Text";
                    break;
                
                case OpenCbsTellerExceptionEnum.AccountIsEmpty:
                    returned = "TellerAccountIsEmpty.Text";
                    break;

                case OpenCbsTellerExceptionEnum.BranchIsEmpty:
                    returned = "TellerBranchIsEmpty.Text";
                    break;

                case OpenCbsTellerExceptionEnum.CurrencyIsEmpty:
                    returned = "TellerCurrencyIsEmpty.Text";
                    break;

                case OpenCbsTellerExceptionEnum.UserIsEmpty:
                    returned = "TellerUserIsEmpty.Text";
                    break;

                case OpenCbsTellerExceptionEnum.NameIsExists:
                    returned = "TellerNameIsExists.Text";
                    break;

                case OpenCbsTellerExceptionEnum.MinMaxAmountIsInvalid:
                    returned = "TellerAmountMinMaxIsInvalid.Text";
                    break;

                case OpenCbsTellerExceptionEnum.VaultExists:
                    returned = "TellerVaultExists.Text";
                    break;

            }
            return returned;
        }
    }

    [Serializable]
    public enum OpenCbsTellerExceptionEnum
    {
        NameIsEmpty
        , AccountIsEmpty
        , UserIsEmpty
        , BranchIsEmpty
        , CurrencyIsEmpty
        , NameIsExists
        , MinMaxAmountIsInvalid
        , VaultExists
    }
}
