// LICENSE PLACEHOLDER

using System;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler
{
    [Serializable]
    public class OctopusCollateralException : OctopusException
    {
        private string _message;
		public OctopusCollateralException()
		{
			_message = String.Empty;
		}

        protected OctopusCollateralException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _message = info.GetString("Code");
        }

        public OctopusCollateralException(string message)
		{
			_message = message;
		}

		public override string ToString()
		{
			return _message.ToString();
		}
    }
}
