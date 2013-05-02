// LICENSE PLACEHOLDER

using System;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler
{
	/// <summary>
	/// Summary description for OctopusUserExceptions.
	/// </summary>
	[Serializable]
	public class OctopusUserException : OctopusException
	{
		private string message;

        protected OctopusUserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            message = info.GetString("Code");
            Console.WriteLine("EXCEPTION USER EXCEPTION: {0}", message);
        }

		public OctopusUserException()
		{
			this.message = String.Empty;
		}
		
		public OctopusUserException(string message)
		{
			this.message = message;
		}
		public override string ToString()
		{
			return this.message.ToString();
		}
	}
}
