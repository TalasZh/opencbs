// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler.Exceptions.ExportExceptions
{
    [Serializable]
    public class OpenCbsCustomExportException : OpenCbsException
    {
        private readonly string _message;
        private OpenCbsCustomExportExceptionEnum _code;

        public OpenCbsCustomExportExceptionEnum Code
        { get { return _code; } }

        protected OpenCbsCustomExportException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _message = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", _code);
            base.GetObjectData(info, context);
        }

        public OpenCbsCustomExportException()
        {
            _message = string.Empty;
        }

        public OpenCbsCustomExportException(OpenCbsCustomExportExceptionEnum exceptionCode)
        {
            _code = exceptionCode;
            _message = _findException(exceptionCode);
        }

        public OpenCbsCustomExportException(string message)
        {
            _message = message;
        }

        public override string ToString()
        {
            return _message;
        }

        private string _findException(OpenCbsCustomExportExceptionEnum exceptionCode)
        {
            switch (exceptionCode)
            {
                case OpenCbsCustomExportExceptionEnum.FileNameIsEmpty: 
                    return "CustomExportFileNameIsEmpty.Text";
                case OpenCbsCustomExportExceptionEnum.FileExtensionIsIncorrect:
                    return "CustomExportFileExtensionIsIncorrect.Text";
                case OpenCbsCustomExportExceptionEnum.SomeRequiredFieldsAreMissing:
                    return "CustomExportSomeRequiredFieldsAreMissing.Text";
                default: return string.Empty;
            }
        }
    }

    [Serializable]
    public enum OpenCbsCustomExportExceptionEnum
    {
        FileNameIsEmpty,
        FileExtensionIsIncorrect,
        SomeRequiredFieldsAreMissing
    }
}
