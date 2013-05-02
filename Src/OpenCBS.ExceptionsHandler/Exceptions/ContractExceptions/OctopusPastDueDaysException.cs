// LICENSE PLACEHOLDER

using System;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler
{
	/// <summary>
	/// Summary description for OctopusPastDueDaysExceptions.
	/// </summary>
    [Serializable]
    public class OctopusPastDueDaysException : OctopusException
	{
		private string _code;
		public OctopusPastDueDaysException()
		{
			_code = "RepayExceptionMaxPastDueDaysReached.Text";
		}

        protected OctopusPastDueDaysException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _code = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", _code);
            base.GetObjectData(info, context);
        }

		public override string ToString()
		{
			return _code;
		}
	}
}
