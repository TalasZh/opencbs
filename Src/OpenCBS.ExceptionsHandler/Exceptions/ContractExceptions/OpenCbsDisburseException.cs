// LICENSE PLACEHOLDER

using System;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler
{
    [Serializable]
    public class OpenCbsDisburseException : OpenCbsException
	{
		private string _code;
		public OpenCbsDisburseException(OpenCbsDisburseExceptionsEnum exceptionCode)
		{
			_code = _FindException(exceptionCode);
		}

		public override string ToString()
		{
			return _code;
		}

        protected OpenCbsDisburseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _code = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", _code);
            base.GetObjectData(info, context);
        }

		private string _FindException(OpenCbsDisburseExceptionsEnum exceptionId)
		{
			string returned = String.Empty;
			switch(exceptionId)
			{
				case OpenCbsDisburseExceptionsEnum.DateIsWrong:
					returned = "DisburseExceptionDateWrong.Text";
					break;
			}
			return returned;
		}
	}

    [Serializable]
	public enum OpenCbsDisburseExceptionsEnum
	{
		DateIsWrong,
	}
}
