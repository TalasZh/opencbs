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
    public class OpenCbsRepayException : OpenCbsException
	{
		private string _code;
		public OpenCbsRepayException(OpenCbsRepayExceptionsEnum exceptionCode)
		{
			_code = _FindException(exceptionCode);
		}

		public override string ToString()
		{
			return _code;
		}

        protected OpenCbsRepayException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            _code = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", _code);
            base.GetObjectData(info, context);
        }

		private string _FindException(OpenCbsRepayExceptionsEnum exceptionId)
		{
			string returned = String.Empty;
			switch(exceptionId)
			{
				case OpenCbsRepayExceptionsEnum.NegativeAmount:
					returned = "RepayExceptionNegativeAmount.Text";
					break;

				case OpenCbsRepayExceptionsEnum.AmountGreaterThanTotalRemainingAmount:
					returned = "RepayExceptionAmountGreaterThanTotalRemainingAmount.Text";
					break;
					
				case OpenCbsRepayExceptionsEnum.AmountIsNull:
					returned = "RepayExceptionAmountIsNull.Text";
					break;
					
				case OpenCbsRepayExceptionsEnum.MaxPastDueDaysReached:
					returned = "RepayExceptionMaxPastDueDaysReached.Text";
                    break;

                case OpenCbsRepayExceptionsEnum.CantRepayInTheFutur:
                    returned = "CantRepayInTheFutur.Text";
                    break;

                case OpenCbsRepayExceptionsEnum.DecimalAmount:
                    returned = "DecimalAmount.Text";
                    break;

                case OpenCbsRepayExceptionsEnum.AllInstallmentRepaid:
                    returned = "AllInstallmentRepaid.Text";
                    break;

                case OpenCbsRepayExceptionsEnum.RepaymentBeforeDisburse:
                    returned = "RepaymentBeforeDisburse.Text";
                    break;

                case OpenCbsRepayExceptionsEnum.RepaymentBeforeLastEventDate:
			        returned = "RepaymentBeforeLastEventDate.Text";
                    break;
			}
			return returned;
		}
	}

    [Serializable]
	public enum OpenCbsRepayExceptionsEnum
	{
		NegativeAmount,
		AmountIsNull,
        CantRepayInTheFutur,
		AmountGreaterThanTotalRemainingAmount,
		MaxPastDueDaysReached,
        DecimalAmount,
        AllInstallmentRepaid,
        RepaymentBeforeDisburse,
        RepaymentBeforeLastEventDate
	}
}
