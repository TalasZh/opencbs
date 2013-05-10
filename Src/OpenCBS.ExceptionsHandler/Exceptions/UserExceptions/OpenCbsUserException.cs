// LICENSE PLACEHOLDER

using System;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler
{
	/// <summary>
	/// Summary description for OctopusUserExceptions.
	/// </summary>
	[Serializable]
	public class OpenCbsUserException : OpenCbsException
	{
		private string message;

        protected OpenCbsUserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            message = info.GetString("Code");
            Console.WriteLine("EXCEPTION USER EXCEPTION: {0}", message);
        }

		public OpenCbsUserException()
		{
			this.message = String.Empty;
		}
		
		public OpenCbsUserException(string message)
		{
			this.message = message;
		}
		public override string ToString()
		{
			return this.message.ToString();
		}
	}
}
