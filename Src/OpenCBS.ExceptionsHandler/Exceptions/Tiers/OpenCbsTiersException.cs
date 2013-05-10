// LICENSE PLACEHOLDER

using System;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler
{
	/// <summary>
	/// Summary description for OctopusTiersExceptions.
	/// </summary>
    [Serializable]
	public class OpenCbsTiersException : OpenCbsException
	{
		private string message;

        protected OpenCbsTiersException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            message = info.GetString("Code");
        }

		public OpenCbsTiersException()
		{
            this.message = string.Empty;
		}
		
		public OpenCbsTiersException(string message)
		{
			this.message = message;
		}
		public override string ToString()
		{
			return this.message;
		}
	}
}
