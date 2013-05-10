// LICENSE PLACEHOLDER

using System;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler
{
    [Serializable]
    public class OpenCbsInstallmentsException : OpenCbsException
	{
		private string _code;
		public OpenCbsInstallmentsException(OpenCbsInstallmentsExceptionsEnum exceptionCode)
		{
			_code = _FindException(exceptionCode);
		}

        protected OpenCbsInstallmentsException(SerializationInfo info, StreamingContext context)
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

		private string _FindException(OpenCbsInstallmentsExceptionsEnum exceptionId)
		{
			string returned = String.Empty;
			switch(exceptionId)
			{
				case OpenCbsInstallmentsExceptionsEnum.InstallmentIsNull:
					returned = "InstallmentExceptionInstallmentIsNull.Text";
					break;

				case OpenCbsInstallmentsExceptionsEnum.InstallmentAlreadyRepaid:
					returned = "InstallmentExceptionInstallmentAlreadyRepaid.Text";
					break;

				case OpenCbsInstallmentsExceptionsEnum.NotFirstInstallmentToRepay:
					returned = "InstallmentExceptionNotFirstInstallmentToRepay.Text";
					break;

				case OpenCbsInstallmentsExceptionsEnum.NotFirstInstallmentToCancelRepay:
					returned = "InstallmentExceptionNotFirstInstallmentToCancelRepay.Text";
					break;
			}
			return returned;
		}
	}

    [Serializable]
	public enum OpenCbsInstallmentsExceptionsEnum
	{
		InstallmentIsNull,
		InstallmentAlreadyRepaid,
		NotFirstInstallmentToRepay,
		NotFirstInstallmentToCancelRepay,
	}
}
