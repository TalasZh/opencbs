// LICENSE PLACEHOLDER

using System;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler
{
	/// <summary>
	/// Summary description for OctopusDOASaveException.
	/// </summary>
    [Serializable]
    public class OctopusDOASaveException : OctopusDOAException
	{
		private string code;
		public OctopusDOASaveException(OctopusDOASaveExceptionEnum exceptionCode)
		{
			code = _FindException(exceptionCode);
		}

        protected OctopusDOASaveException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            code = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", code);
            base.GetObjectData(info, context);
        }

		private string _FindException(OctopusDOASaveExceptionEnum exceptionId)
		{
			string returned = String.Empty;
			switch(exceptionId)
			{
				case OctopusDOASaveExceptionEnum.NameIsNull:
					returned = "DOAExceptionNameIsNull.Text";
					break;

				case OctopusDOASaveExceptionEnum.CastInvalid:
					returned = "DOAExceptionCastInvalid.Text";
					break;

				case OctopusDOASaveExceptionEnum.AlreadyExist:
					returned = "DOAExceptionAlreadyExist.Text";
					break;
			}
			return returned;
		}

		public override string ToString()
		{
			return code;
		}
	}

    [Serializable]
	public enum OctopusDOASaveExceptionEnum
	{
		NameIsNull,
		CastInvalid,
		AlreadyExist,
	}
}
