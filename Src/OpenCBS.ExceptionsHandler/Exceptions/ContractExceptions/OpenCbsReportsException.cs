// LICENSE PLACEHOLDER

using System;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler
{
	/// <summary>
	/// Summary description for OctopusReportsException.
	/// </summary>
    [Serializable]
    public class OpenCbsReportsException : OpenCbsException
	{
		private string _code;
		public OpenCbsReportsException(OpenCbsReportsExceptionsEnum exceptionCode)
		{
            _code = _FindException(exceptionCode);
		}

        protected OpenCbsReportsException(SerializationInfo info, StreamingContext context)
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

        private string _FindException(OpenCbsReportsExceptionsEnum exceptionId)
		{
			string returned = String.Empty;
			switch(exceptionId)
			{
				case OpenCbsReportsExceptionsEnum.NoResult:
					returned = "ReportsExceptionsNoResult.Text";
					break;

				case OpenCbsReportsExceptionsEnum.NeedExchangeRate:
					returned = "ReportsExceptionsNeedExchangeRate.Text";
					break;
                case OpenCbsReportsExceptionsEnum.CannotLoadReport:
                    returned = "CannotLoadReport.Text";
                    break;
                case OpenCbsReportsExceptionsEnum.ReportProcedureSourceEmpty:
                    returned = "ReportProcedureSourceEmpty.Text";
                    break;
                case OpenCbsReportsExceptionsEnum.CannotGetDataSource:
                    returned = "CannotGetDataSource.Text";
                    break;
                case OpenCbsReportsExceptionsEnum.CannotLoadParameters:
                    returned = "CannotLoadParameters.Text";
                    break;

			}
			return returned;
		}
	}

    [Serializable]
	public enum OpenCbsReportsExceptionsEnum
	{
		NoResult,
        NeedExchangeRate, 
        CannotLoadReport,
        ReportProcedureSourceEmpty,
        CannotGetDataSource,
        CannotLoadParameters

	}
}
