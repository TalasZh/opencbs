// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler
{
	/// <summary>
	/// Summary description for OpenCbsException.
	/// </summary>
	[Serializable]
	public class OpenCbsException : ApplicationException
	{
		private readonly string _message;
        public List<string> AdditionalOptions { get; set; }

        protected OpenCbsException(SerializationInfo info, StreamingContext context ) : base(info, context)
        {
           _message = info.GetString("Code");
            Console.WriteLine("EXCEPTION USER EXCEPTION: {0}", _message);
        }

		public OpenCbsException()
		{
			_message = String.Empty;
		}

		public OpenCbsException(string message)
		{
			_message = message;
		}

		public override string ToString()
		{
			return _message;
		}
	}
}
