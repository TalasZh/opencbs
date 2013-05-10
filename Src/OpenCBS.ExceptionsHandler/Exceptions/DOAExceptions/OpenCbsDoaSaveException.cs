// LICENSE PLACEHOLDER

using System;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler
{
    [Serializable]
    public class OpenCbsDoaSaveException : OpenCbsDoaException
	{
		private string code;
		public OpenCbsDoaSaveException(OpenCbsDOASaveExceptionEnum exceptionCode)
		{
			code = _FindException(exceptionCode);
		}

        protected OpenCbsDoaSaveException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            code = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", code);
            base.GetObjectData(info, context);
        }

		private string _FindException(OpenCbsDOASaveExceptionEnum exceptionId)
		{
			string returned = String.Empty;
			switch(exceptionId)
			{
				case OpenCbsDOASaveExceptionEnum.NameIsNull:
					returned = "DOAExceptionNameIsNull.Text";
					break;

				case OpenCbsDOASaveExceptionEnum.CastInvalid:
					returned = "DOAExceptionCastInvalid.Text";
					break;

				case OpenCbsDOASaveExceptionEnum.AlreadyExist:
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
	public enum OpenCbsDOASaveExceptionEnum
	{
		NameIsNull,
		CastInvalid,
		AlreadyExist,
	}
}
