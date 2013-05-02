// LICENSE PLACEHOLDER

using System;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler
{
    [Serializable]
    public class OctopusCollateralDeleteException : OctopusCollateralException
    {
        private string _code;
        public OctopusCollateralDeleteException(OctopusCollateralDeleteExceptionEnum exceptionCode)
		{
			_code = _FindException(exceptionCode);
		}

        protected OctopusCollateralDeleteException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _code = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", _code);
            base.GetObjectData(info, context);
        }

        private static string _FindException(OctopusCollateralDeleteExceptionEnum exceptionId)
		{
			string returned = String.Empty;
			switch(exceptionId)
			{
                case OctopusCollateralDeleteExceptionEnum.HasChildrens:
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
    public enum OctopusCollateralDeleteExceptionEnum
	{
		HasChildrens,
	}
}
