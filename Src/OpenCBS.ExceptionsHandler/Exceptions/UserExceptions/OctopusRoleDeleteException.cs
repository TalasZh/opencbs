// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler
{
    /// <summary>
    /// Summary description for OctopusAccountException.
    /// </summary>
    [Serializable]
    public class OctopusRoleDeleteException : OctopusException
    {
        private readonly string _code;

        public OctopusRoleDeleteException(OctopusRoleDeleteExceptionsEnum exceptionCode)
        {
            _code = _FindException(exceptionCode);
        }

        public OctopusRoleDeleteException(OctopusRoleDeleteExceptionsEnum exceptionCode, string method)
        {
            _code = _FindException(exceptionCode);
            AdditionalOptions = new List<string> {method};
        }

        public override string ToString()
        {
            return _code;
        }

        protected OctopusRoleDeleteException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _code = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", _code);
            base.GetObjectData(info, context);
        }

        private string _FindException(OctopusRoleDeleteExceptionsEnum exceptionId)
        {
            string returned = String.Empty;
            switch (exceptionId)
            {
                case OctopusRoleDeleteExceptionsEnum.RoleIsNull:
                    returned = "RoleIsNull.Text";
                    break;

                case OctopusRoleDeleteExceptionsEnum.RoleHasUsers:
                    returned = "RoleHasUsers.Text";
                    break;

                case OctopusRoleDeleteExceptionsEnum.ActionProhibited:
                    returned = "ActionProhibitedForUser.Text";
                    break;
            }
            return returned;
        }
    }

    [Serializable]
    public enum OctopusRoleDeleteExceptionsEnum
    {
        RoleIsNull,
        RoleHasUsers, 
        ActionProhibited
    }
}
