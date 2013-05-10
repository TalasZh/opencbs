// LICENSE PLACEHOLDER

using System;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler
{
    [Serializable]
    public class OpenCbsContractException : OpenCbsException
	{
		private string _message;
		public OpenCbsContractException()
		{
			_message = String.Empty;
		}

        protected OpenCbsContractException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _message = info.GetString("Code");
        }

		public OpenCbsContractException(string message)
		{
			_message = message;
		}
		public override string ToString()
		{
			return _message;
		}
	}
}
