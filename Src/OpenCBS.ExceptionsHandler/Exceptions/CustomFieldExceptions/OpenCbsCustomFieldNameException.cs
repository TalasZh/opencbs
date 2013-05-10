// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using OpenCBS.Enums;


namespace OpenCBS.ExceptionsHandler.Exceptions.CustomFieldExceptions
{
    [Serializable]
    public class OpenCbsCustomFieldNameException:OpenCbsException
    {
        private readonly string _message;

        public OpenCbsCustomFieldNameException(OCustomFieldExceptionEnum customFieldNameException)
        {
            _message = FindExceptionMessage(customFieldNameException);
        }

        private string FindExceptionMessage(OCustomFieldExceptionEnum customFieldExceptionEnum)
        {
            string message = string.Empty;
            switch (customFieldExceptionEnum)
            {
                case OCustomFieldExceptionEnum.FieldNameCanNotContainComma:
                    message = "FieldNameCanNotContainComma.Text";
                    break;
                case OCustomFieldExceptionEnum.FieldLimited:
                    message = "FieldLimited.Text";
                    break;
                default: break;
            }
            return message;
        }

        public override string ToString()
        {
            return _message;
        }
    }
}
