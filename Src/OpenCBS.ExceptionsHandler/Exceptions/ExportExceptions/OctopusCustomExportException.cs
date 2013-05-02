using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler.Exceptions.ExportExceptions
{
    [Serializable]
    public class OctopusCustomExportException : OctopusException
    {
        private readonly string _message;
        private OctopusCustomExportExceptionEnum _code;

        public OctopusCustomExportExceptionEnum Code
        { get { return _code; } }

        protected OctopusCustomExportException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _message = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", _code);
            base.GetObjectData(info, context);
        }

        public OctopusCustomExportException()
        {
            _message = string.Empty;
        }

        public OctopusCustomExportException(OctopusCustomExportExceptionEnum exceptionCode)
        {
            _code = exceptionCode;
            _message = _findException(exceptionCode);
        }

        public OctopusCustomExportException(string message)
        {
            _message = message;
        }

        public override string ToString()
        {
            return _message;
        }

        private string _findException(OctopusCustomExportExceptionEnum exceptionCode)
        {
            switch (exceptionCode)
            {
                case OctopusCustomExportExceptionEnum.FileNameIsEmpty: 
                    return "CustomExportFileNameIsEmpty.Text";
                case OctopusCustomExportExceptionEnum.FileExtensionIsIncorrect:
                    return "CustomExportFileExtensionIsIncorrect.Text";
                case OctopusCustomExportExceptionEnum.SomeRequiredFieldsAreMissing:
                    return "CustomExportSomeRequiredFieldsAreMissing.Text";
                default: return string.Empty;
            }
        }
    }

    [Serializable]
    public enum OctopusCustomExportExceptionEnum
    {
        FileNameIsEmpty,
        FileExtensionIsIncorrect,
        SomeRequiredFieldsAreMissing
    }
}
