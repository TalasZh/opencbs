// LICENSE PLACEHOLDER

using System;
using System.Runtime.Serialization;


namespace OpenCBS.ExceptionsHandler
{
    [Serializable]
    public class OpenCbsProjectException : OpenCbsException
    {
        private readonly string _message;

        protected OpenCbsProjectException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _message = info.GetString("Code");
        }

		public OpenCbsProjectException()
		{
            _message = string.Empty;
		}

        public OpenCbsProjectException(string message)
		{
			_message = message;
		}

		public override string ToString()
		{
			return _message;
		}
    }
}
