// LICENSE PLACEHOLDER

using System;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler
{
    [Serializable]
    public class OpenCbsDoaDeleteException : OpenCbsDoaException
	{
		private string _code;
		public OpenCbsDoaDeleteException(OpenCbsDOADeleteExceptionEnum exceptionCode)
		{
			_code = _FindException(exceptionCode);
		}

        protected OpenCbsDoaDeleteException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _code = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", _code);
            base.GetObjectData(info, context);
        }

		private static string _FindException(OpenCbsDOADeleteExceptionEnum exceptionId)
		{
			string returned = String.Empty;
			switch(exceptionId)
			{
				case OpenCbsDOADeleteExceptionEnum.HasChildrens:
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
	public enum OpenCbsDOADeleteExceptionEnum
	{
		HasChildrens,
	}
}
