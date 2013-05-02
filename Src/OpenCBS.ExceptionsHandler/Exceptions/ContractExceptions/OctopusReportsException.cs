// LICENSE PLACEHOLDER

using System;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler
{
	/// <summary>
	/// Summary description for OctopusReportsException.
	/// </summary>
    [Serializable]
    public class OctopusReportsException : OctopusException
	{
		private string _code;
		public OctopusReportsException(OctopusReportsExceptionsEnum exceptionCode)
		{
            _code = _FindException(exceptionCode);
		}

        protected OctopusReportsException(SerializationInfo info, StreamingContext context)
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

        private string _FindException(OctopusReportsExceptionsEnum exceptionId)
		{
			string returned = String.Empty;
			switch(exceptionId)
			{
				case OctopusReportsExceptionsEnum.NoResult:
					returned = "ReportsExceptionsNoResult.Text";
					break;

				case OctopusReportsExceptionsEnum.NeedExchangeRate:
					returned = "ReportsExceptionsNeedExchangeRate.Text";
					break;
                case OctopusReportsExceptionsEnum.CannotLoadReport:
                    returned = "CannotLoadReport.Text";
                    break;
                case OctopusReportsExceptionsEnum.ReportProcedureSourceEmpty:
                    returned = "ReportProcedureSourceEmpty.Text";
                    break;
                case OctopusReportsExceptionsEnum.CannotGetDataSource:
                    returned = "CannotGetDataSource.Text";
                    break;
                case OctopusReportsExceptionsEnum.CannotLoadParameters:
                    returned = "CannotLoadParameters.Text";
                    break;

			}
			return returned;
		}
	}

    [Serializable]
	public enum OctopusReportsExceptionsEnum
	{
		NoResult,
        NeedExchangeRate, 
        CannotLoadReport,
        ReportProcedureSourceEmpty,
        CannotGetDataSource,
        CannotLoadParameters

	}
}
