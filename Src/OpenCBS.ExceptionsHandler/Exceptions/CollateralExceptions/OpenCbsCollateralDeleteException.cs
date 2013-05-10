// LICENSE PLACEHOLDER

using System;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler
{
    [Serializable]
    public class OpenCbsCollateralDeleteException : OpenCbsCollateralException
    {
        private string _code;
        public OpenCbsCollateralDeleteException(OpenCbsCollateralDeleteExceptionEnum exceptionCode)
		{
			_code = _FindException(exceptionCode);
		}

        protected OpenCbsCollateralDeleteException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _code = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", _code);
            base.GetObjectData(info, context);
        }

        private static string _FindException(OpenCbsCollateralDeleteExceptionEnum exceptionId)
		{
			string returned = String.Empty;
			switch(exceptionId)
			{
                case OpenCbsCollateralDeleteExceptionEnum.HasChildrens:
					returned = "CollateralExceptionHasChildrens.Text";
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
    public enum OpenCbsCollateralDeleteExceptionEnum
	{
		HasChildrens,
	}
}
