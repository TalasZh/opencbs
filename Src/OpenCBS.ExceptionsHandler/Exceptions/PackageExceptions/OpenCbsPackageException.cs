// LICENSE PLACEHOLDER

using System;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler
{
    [Serializable]
    public class OpenCbsPackageException : OpenCbsException
	{
		private string message;
		public OpenCbsPackageException()
		{
			this.message = String.Empty;
		}

        protected OpenCbsPackageException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            message = info.GetString("Code");
        }

		public OpenCbsPackageException(string message)
		{
			this.message = message;
		}

		public override string ToString()
		{
			return this.message;
		}
	}
}
