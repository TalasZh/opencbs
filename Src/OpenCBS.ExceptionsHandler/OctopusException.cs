// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler
{
	/// <summary>
	/// Summary description for OctopusException.
	/// </summary>
	[Serializable]
	public class OctopusException : ApplicationException
	{
		private readonly string _message;
        public List<string> AdditionalOptions { get; set; }

        protected OctopusException(SerializationInfo info, StreamingContext context ) : base(info, context)
        {
           _message = info.GetString("Code");
            Console.WriteLine("EXCEPTION USER EXCEPTION: {0}", _message);
        }

		public OctopusException()
		{
			_message = String.Empty;
		}

		public OctopusException(string message)
		{
			_message = message;
		}

		public override string ToString()
		{
			return _message;
		}
	}
}
