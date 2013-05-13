// Octopus MFS is an integrated suite for managing a Micro Finance Institution: 
// clients, contracts, accounting, reporting and risk
// Copyright © 2006,2007 OCTO Technology & OXUS Development Network
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License along
// with this program; if not, write to the Free Software Foundation, Inc.,
// 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
// Website: http://www.opencbs.com
// Contact: contact@opencbs.com

using System;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler
{
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
