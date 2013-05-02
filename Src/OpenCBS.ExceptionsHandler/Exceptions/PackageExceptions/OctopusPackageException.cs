// LICENSE PLACEHOLDER

using System;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler
{
	/// <summary>
	/// Summary description for OctopusPackageExceptions.
	/// </summary>
    [Serializable]
    public class OctopusPackageException : OctopusException
	{
		private string message;
		public OctopusPackageException()
		{
			this.message = String.Empty;
		}

        protected OctopusPackageException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            message = info.GetString("Code");
        }

		public OctopusPackageException(string message)
		{
			this.message = message;
		}

		public override string ToString()
		{
			return this.message;
		}
	}
}
