// LICENSE PLACEHOLDER

using System;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler
{
    [Serializable]
    public class OpenCbsCollateralUpdateException : OpenCbsCollateralException
    {
		private string _code;
        public OpenCbsCollateralUpdateException(OpenCbsCollateralUpdateExceptionEnum exceptionCode)
		{
			_code = _FindException(exceptionCode);
		}

        protected OpenCbsCollateralUpdateException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _code = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", _code);
            base.GetObjectData(info, context);
        }

        private string _FindException(OpenCbsCollateralUpdateExceptionEnum exceptionId)
		{
			string returned = String.Empty;
			switch(exceptionId)
			{
                case OpenCbsCollateralUpdateExceptionEnum.NewNameIsNull:
					returned = "CollateralExceptionNewNameIsNull.Text";
					break;

                case OpenCbsCollateralUpdateExceptionEnum.NoSelect:
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
    public enum OpenCbsCollateralUpdateExceptionEnum
	{
		NoSelect,
		NewNameIsNull,
	}
}
