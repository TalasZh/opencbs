// LICENSE PLACEHOLDER

using System;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler
{
    [Serializable]
    public class OpenCbsCollateralException : OpenCbsException
    {
        private string _message;
		public OpenCbsCollateralException()
		{
			_message = String.Empty;
		}

        protected OpenCbsCollateralException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _message = info.GetString("Code");
        }

        public OpenCbsCollateralException(string message)
		{
			_message = message;
		}

		public override string ToString()
		{
			return _message.ToString();
		}
    }
}
