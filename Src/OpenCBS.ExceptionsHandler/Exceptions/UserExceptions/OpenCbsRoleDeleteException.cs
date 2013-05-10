// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler
{
    /// <summary>
    /// Summary description for OpenCbsAccountException.
    /// </summary>
    [Serializable]
    public class OpenCbsRoleDeleteException : OpenCbsException
    {
        private readonly string _code;

        public OpenCbsRoleDeleteException(OpenCbsRoleDeleteExceptionsEnum exceptionCode)
        {
            _code = _FindException(exceptionCode);
        }

        public OpenCbsRoleDeleteException(OpenCbsRoleDeleteExceptionsEnum exceptionCode, string method)
        {
            _code = _FindException(exceptionCode);
            AdditionalOptions = new List<string> {method};
        }

        public override string ToString()
        {
            return _code;
        }

        protected OpenCbsRoleDeleteException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _code = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", _code);
            base.GetObjectData(info, context);
        }

        private string _FindException(OpenCbsRoleDeleteExceptionsEnum exceptionId)
        {
            string returned = String.Empty;
            switch (exceptionId)
            {
                case OpenCbsRoleDeleteExceptionsEnum.RoleIsNull:
                    returned = "RoleIsNull.Text";
                    break;

                case OpenCbsRoleDeleteExceptionsEnum.RoleHasUsers:
                    returned = "RoleHasUsers.Text";
                    break;

                case OpenCbsRoleDeleteExceptionsEnum.ActionProhibited:
                    returned = "ActionProhibitedForUser.Text";
                    break;
            }
            return returned;
        }
    }

    [Serializable]
    public enum OpenCbsRoleDeleteExceptionsEnum
    {
        RoleIsNull,
        RoleHasUsers, 
        ActionProhibited
    }
}
