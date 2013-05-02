// LICENSE PLACEHOLDER

using System;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler
{
	/// <summary>
	/// Summary description for OctopusInstallmentsExceptions.
	/// </summary>
    [Serializable]
    public class OctopusInstallmentsException : OctopusException
	{
		private string _code;
		public OctopusInstallmentsException(OctopusInstallmentsExceptionsEnum exceptionCode)
		{
			_code = _FindException(exceptionCode);
		}

        protected OctopusInstallmentsException(SerializationInfo info, StreamingContext context)
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

		private string _FindException(OctopusInstallmentsExceptionsEnum exceptionId)
		{
			string returned = String.Empty;
			switch(exceptionId)
			{
				case OctopusInstallmentsExceptionsEnum.InstallmentIsNull:
					returned = "InstallmentExceptionInstallmentIsNull.Text";
					break;

				case OctopusInstallmentsExceptionsEnum.InstallmentAlreadyRepaid:
					returned = "InstallmentExceptionInstallmentAlreadyRepaid.Text";
					break;

				case OctopusInstallmentsExceptionsEnum.NotFirstInstallmentToRepay:
					returned = "InstallmentExceptionNotFirstInstallmentToRepay.Text";
					break;

				case OctopusInstallmentsExceptionsEnum.NotFirstInstallmentToCancelRepay:
					returned = "InstallmentExceptionNotFirstInstallmentToCancelRepay.Text";
					break;
			}
			return returned;
		}
	}

    [Serializable]
	public enum OctopusInstallmentsExceptionsEnum
	{
		InstallmentIsNull,
		InstallmentAlreadyRepaid,
		NotFirstInstallmentToRepay,
		NotFirstInstallmentToCancelRepay,
	}
}
