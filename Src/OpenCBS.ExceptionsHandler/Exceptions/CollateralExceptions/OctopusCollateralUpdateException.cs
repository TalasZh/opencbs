// LICENSE PLACEHOLDER

using System;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler
{
    [Serializable]
    public class OctopusCollateralUpdateException : OctopusCollateralException
    {
		private string _code;
        public OctopusCollateralUpdateException(OctopusCollateralUpdateExceptionEnum exceptionCode)
		{
			_code = _FindException(exceptionCode);
		}

        protected OctopusCollateralUpdateException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _code = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", _code);
            base.GetObjectData(info, context);
        }

        private string _FindException(OctopusCollateralUpdateExceptionEnum exceptionId)
		{
			string returned = String.Empty;
			switch(exceptionId)
			{
                case OctopusCollateralUpdateExceptionEnum.NewNameIsNull:
					returned = "CollateralExceptionNewNameIsNull.Text";
					break;

                case OctopusCollateralUpdateExceptionEnum.NoSelect:
                    returned = "CollateralExceptionNoSelect.Text";
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
    public enum OctopusCollateralUpdateExceptionEnum
	{
		NoSelect,
		NewNameIsNull,
	}
}
