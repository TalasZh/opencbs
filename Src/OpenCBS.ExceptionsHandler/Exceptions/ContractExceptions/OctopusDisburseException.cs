// LICENSE PLACEHOLDER

using System;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler
{
	/// <summary>
	/// Summary description for OctopusRepayExceptions.
	/// </summary>
    [Serializable]
    public class OctopusDisburseException : OctopusException
	{
		private string _code;
		public OctopusDisburseException(OctopusDisburseExceptionsEnum exceptionCode)
		{
			_code = _FindException(exceptionCode);
		}

		public override string ToString()
		{
			return _code;
		}

        protected OctopusDisburseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _code = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", _code);
            base.GetObjectData(info, context);
        }

		private string _FindException(OctopusDisburseExceptionsEnum exceptionId)
		{
			string returned = String.Empty;
			switch(exceptionId)
			{
				case OctopusDisburseExceptionsEnum.DateIsWrong:
					returned = "DisburseExceptionDateWrong.Text";
					break;
			}
			return returned;
		}
	}

    [Serializable]
	public enum OctopusDisburseExceptionsEnum
	{
		DateIsWrong,
	}
}
