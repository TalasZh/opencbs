// LICENSE PLACEHOLDER

using System;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler
{
    [Serializable]
    public class OpenCbsCollateralSaveException : OpenCbsCollateralException   
    {
        private string _code;
        public OpenCbsCollateralSaveException(OpenCbsCollateralSaveExceptionEnum exceptionCode)
		{
			_code = _FindException(exceptionCode);
		}

        protected OpenCbsCollateralSaveException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _code = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", _code);
            base.GetObjectData(info, context);
        }

        private static string _FindException(OpenCbsCollateralSaveExceptionEnum exceptionId)
		{
			string returned = String.Empty;
			switch(exceptionId)
			{
                case OpenCbsCollateralSaveExceptionEnum.NameIsNull:
					returned = "CollateralExceptionNameIsNull.Text";
					break;

                case OpenCbsCollateralSaveExceptionEnum.CastInvalid:
                    returned = "CollateralExceptionCastInvalid.Text";
					break;

                case OpenCbsCollateralSaveExceptionEnum.AlreadyExist:
                    returned = "CollateralExceptionAlreadyExist.Text";
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
    public enum OpenCbsCollateralSaveExceptionEnum
    {
        NameIsNull,
        CastInvalid,
        AlreadyExist,
    }
}
