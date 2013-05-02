// LICENSE PLACEHOLDER

using System;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler
{
    [Serializable]
    public class OctopusCollateralSaveException : OctopusCollateralException   
    {
        private string _code;
        public OctopusCollateralSaveException(OctopusCollateralSaveExceptionEnum exceptionCode)
		{
			_code = _FindException(exceptionCode);
		}

        protected OctopusCollateralSaveException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _code = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", _code);
            base.GetObjectData(info, context);
        }

        private static string _FindException(OctopusCollateralSaveExceptionEnum exceptionId)
		{
			string returned = String.Empty;
			switch(exceptionId)
			{
                case OctopusCollateralSaveExceptionEnum.NameIsNull:
					returned = "CollateralExceptionNameIsNull.Text";
					break;

                case OctopusCollateralSaveExceptionEnum.CastInvalid:
                    returned = "CollateralExceptionCastInvalid.Text";
					break;

                case OctopusCollateralSaveExceptionEnum.AlreadyExist:
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
    public enum OctopusCollateralSaveExceptionEnum
    {
        NameIsNull,
        CastInvalid,
        AlreadyExist,
    }
}
