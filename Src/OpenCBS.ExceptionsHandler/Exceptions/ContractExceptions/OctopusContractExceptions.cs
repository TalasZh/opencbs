// LICENSE PLACEHOLDER

using System;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler
{
	/// <summary>
	/// Summary description for OctopusContractExceptions.
	/// </summary>
    [Serializable]
    public class OctopusContractException : OctopusException
	{
		private string _message;
		public OctopusContractException()
		{
			_message = String.Empty;
		}

        protected OctopusContractException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _message = info.GetString("Code");
        }

		public OctopusContractException(string message)
		{
			_message = message;
		}
		public override string ToString()
		{
			return _message;
		}
	}
}
