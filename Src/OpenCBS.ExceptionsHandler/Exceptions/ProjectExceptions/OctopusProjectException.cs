// LICENSE PLACEHOLDER

using System;
using System.Runtime.Serialization;


namespace OpenCBS.ExceptionsHandler
{
    [Serializable]
    public class OctopusProjectException : OctopusException
    {
        private readonly string _message;

        protected OctopusProjectException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _message = info.GetString("Code");
        }

		public OctopusProjectException()
		{
            _message = string.Empty;
		}

        public OctopusProjectException(string message)
		{
			_message = message;
		}

		public override string ToString()
		{
			return _message;
		}
    }
}
