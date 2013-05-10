// LICENSE PLACEHOLDER

using System;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler
{
	/// <summary>
	/// Summary description for OpenCbsUserDeleteException.
	/// </summary>
	[Serializable]
	public class OpenCbsUserDeleteException : OpenCbsUserException
	{
		private string code;

        protected OpenCbsUserDeleteException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            code = info.GetString("Code");
        }

		public OpenCbsUserDeleteException(OpenCbsUserDeleteExceptionEnum exceptionCode)
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

		private string FindException(OpenCbsUserDeleteExceptionEnum exceptionId)
		{
			string returned = String.Empty;
			switch(exceptionId)
			{
				case OpenCbsUserDeleteExceptionEnum.UserIsNull:
					returned = "OUE1.Text";
					break;

				case OpenCbsUserDeleteExceptionEnum.AdministratorUser:
					returned = "OUE2.Text";
					break;

                case OpenCbsUserDeleteExceptionEnum.UserHasContract:
                    returned = "OUE9.Text";
                    break;

                case OpenCbsUserDeleteExceptionEnum.UserHasTeller:
			        returned = "OUE10.Text";
                    break;
			}
			return returned;
		}
	}

    [Serializable]
	public enum OpenCbsUserDeleteExceptionEnum
	{
		UserIsNull,
		AdministratorUser,
        UserHasContract,
        UserHasTeller
	}
}
