// LICENSE PLACEHOLDER

using System;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler
{
	/// <summary>
	/// Summary description for OctopusTiersExceptions.
	/// </summary>
    [Serializable]
	public class OctopusTiersException : OctopusException
	{
		private string message;

        protected OctopusTiersException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            message = info.GetString("Code");
        }

		public OctopusTiersException()
		{
            this.message = string.Empty;
		}
		
		public OctopusTiersException(string message)
		{
			this.message = message;
		}
		public override string ToString()
		{
			return this.message;
		}
	}
}
