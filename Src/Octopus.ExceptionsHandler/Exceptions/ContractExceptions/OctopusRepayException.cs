//Octopus MFS is an integrated suite for managing a Micro Finance Institution: clients, contracts, accounting, reporting and risk
//Copyright © 2006,2007 OCTO Technology & OXUS Development Network
//
//This program is free software; you can redistribute it and/or modify
//it under the terms of the GNU Lesser General Public License as published by
//the Free Software Foundation; either version 2 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU Lesser General Public License for more details.
//
//You should have received a copy of the GNU Lesser General Public License along
//with this program; if not, write to the Free Software Foundation, Inc.,
//51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
//
// Licence : http://www.octopusnetwork.org/OverviewLicence.aspx
//
// Website : http://www.octopusnetwork.org
// Business contact: business(at)octopusnetwork.org
// Technical contact email : tech(at)octopusnetwork.org 

using System;
using System.Runtime.Serialization;

namespace Octopus.ExceptionsHandler
{
	/// <summary>
	/// Summary description for OctopusRepayException.
	/// </summary>
    [Serializable]
    public class OctopusRepayException : OctopusException
	{
		private string _code;
		public OctopusRepayException(OctopusRepayExceptionsEnum exceptionCode)
		{
			_code = _FindException(exceptionCode);
		}

		public override string ToString()
		{
			return _code;
		}

        protected OctopusRepayException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            _code = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", _code);
            base.GetObjectData(info, context);
        }

		private string _FindException(OctopusRepayExceptionsEnum exceptionId)
		{
			string returned = String.Empty;
			switch(exceptionId)
			{
				case OctopusRepayExceptionsEnum.NegativeAmount:
					returned = "RepayExceptionNegativeAmount.Text";
					break;

				case OctopusRepayExceptionsEnum.AmountGreaterThanTotalRemainingAmount:
					returned = "RepayExceptionAmountGreaterThanTotalRemainingAmount.Text";
					break;
					
				case OctopusRepayExceptionsEnum.AmountIsNull:
					returned = "RepayExceptionAmountIsNull.Text";
					break;
					
				case OctopusRepayExceptionsEnum.MaxPastDueDaysReached:
					returned = "RepayExceptionMaxPastDueDaysReached.Text";
                    break;

                case OctopusRepayExceptionsEnum.CantRepayInTheFutur:
                    returned = "CantRepayInTheFutur.Text";
                    break;

                case OctopusRepayExceptionsEnum.DecimalAmount:
                    returned = "DecimalAmount.Text";
                    break;

                case OctopusRepayExceptionsEnum.AllInstallmentRepaid:
                    returned = "AllInstallmentRepaid.Text";
                    break;

                case OctopusRepayExceptionsEnum.RepaymentBeforeDisburse:
                    returned = "RepaymentBeforeDisburse.Text";
                    break;

                case OctopusRepayExceptionsEnum.RepaymentBeforeLastEventDate:
			        returned = "RepaymentBeforeLastEventDate.Text";
                    break;
			}
			return returned;
		}
	}

    [Serializable]
	public enum OctopusRepayExceptionsEnum
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
