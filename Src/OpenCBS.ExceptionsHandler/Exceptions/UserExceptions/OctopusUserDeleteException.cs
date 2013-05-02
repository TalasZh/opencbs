// LICENSE PLACEHOLDER

using System;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler
{
	/// <summary>
	/// Summary description for OctopusUserDeleteException.
	/// </summary>
	[Serializable]
	public class OctopusUserDeleteException : OctopusUserException
	{
		private string code;

        protected OctopusUserDeleteException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            code = info.GetString("Code");
        }

		public OctopusUserDeleteException(OctopusUserDeleteExceptionEnum exceptionCode)
		{
			this.code = this.FindException(exceptionCode);
            Console.WriteLine("EXCEPTION USER DELETE EXCEPTION: {0}",code);
		}

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code",code);
            base.GetObjectData(info, context);
        }

		public override string ToString()
		{
			return this.code;
		}

		private string FindException(OctopusUserDeleteExceptionEnum exceptionId)
		{
			string returned = String.Empty;
			switch(exceptionId)
			{
				case OctopusUserDeleteExceptionEnum.UserIsNull:
					returned = "OUE1.Text";
					break;

				case OctopusUserDeleteExceptionEnum.AdministratorUser:
					returned = "OUE2.Text";
					break;

                case OctopusUserDeleteExceptionEnum.UserHasContract:
                    returned = "OUE9.Text";
                    break;

                case OctopusUserDeleteExceptionEnum.UserHasTeller:
			        returned = "OUE10.Text";
                    break;
			}
			return returned;
		}
	}

    [Serializable]
	public enum OctopusUserDeleteExceptionEnum
	{
		UserIsNull,
		AdministratorUser,
        UserHasContract,
        UserHasTeller
	}
}
