// LICENSE PLACEHOLDER

using System;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler
{
	/// <summary>
	/// Summary description for OctopusDOAExceptions.
	/// </summary>
    [Serializable]
    public class OpenCbsDoaException : OpenCbsException
	{
		private string _message;
		public OpenCbsDoaException()
		{
			_message = String.Empty;
		}

        protected OpenCbsDoaException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _message = info.GetString("Code");
        }

		public OpenCbsDoaException(string message)
		{
			_message = message;
		}

		public override string ToString()
		{
			return _message.ToString();
		}

	}
}
