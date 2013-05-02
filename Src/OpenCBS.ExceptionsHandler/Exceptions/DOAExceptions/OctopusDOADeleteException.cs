// LICENSE PLACEHOLDER

using System;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler
{
	/// <summary>
	/// Summary description for OctopusDOADeleteException.
	/// </summary>
    [Serializable]
    public class OctopusDOADeleteException : OctopusDOAException
	{
		private string _code;
		public OctopusDOADeleteException(OctopusDOADeleteExceptionEnum exceptionCode)
		{
			_code = _FindException(exceptionCode);
		}

        protected OctopusDOADeleteException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _code = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", _code);
            base.GetObjectData(info, context);
        }

		private static string _FindException(OctopusDOADeleteExceptionEnum exceptionId)
		{
			string returned = String.Empty;
			switch(exceptionId)
			{
				case OctopusDOADeleteExceptionEnum.HasChildrens:
					returned = "DOAExceptionHasChildrens.Text";
					break;

			}
			return returned;
		}

		public override string ToString()
		{
			return _code;
		}
	}

    [Serializable]
	public enum OctopusDOADeleteExceptionEnum
	{
		HasChildrens,
	}
}
