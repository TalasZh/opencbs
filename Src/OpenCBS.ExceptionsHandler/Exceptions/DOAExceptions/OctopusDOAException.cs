// LICENSE PLACEHOLDER

using System;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler
{
	/// <summary>
	/// Summary description for OctopusDOAExceptions.
	/// </summary>
    [Serializable]
    public class OctopusDOAException : OctopusException
	{
		private string _message;
		public OctopusDOAException()
		{
			_message = String.Empty;
		}

        protected OctopusDOAException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _message = info.GetString("Code");
        }

		public OctopusDOAException(string message)
		{
			_message = message;
		}

		public override string ToString()
		{
			return _message.ToString();
		}

	}
}
