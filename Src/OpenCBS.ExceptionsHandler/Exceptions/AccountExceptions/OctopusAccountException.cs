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

namespace OpenCBS.ExceptionsHandler.Exceptions.AccountExceptions
{
	/// <summary>
	/// Summary description for OctopusAccountException.
	/// </summary>
    [Serializable]
    public class OctopusAccountException : OctopusException
	{
		private readonly string _code;
		public OctopusAccountException(OctopusAccountExceptionsEnum exceptionCode)
		{
			_code = _FindException(exceptionCode);
		}

		public override string ToString()
		{
			return _code;
		}

        protected OctopusAccountException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _code = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", _code);
            base.GetObjectData(info, context);
        }

		private string _FindException(OctopusAccountExceptionsEnum exceptionId)
		{
			string returned = String.Empty;
			switch(exceptionId)
			{
				case OctopusAccountExceptionsEnum.AccountIsNull:
					returned = "AccountExceptionAccountIsNull.Text";
					break;

				case OctopusAccountExceptionsEnum.LabelIsNull:
					returned = "AccountExceptionLabelIsNull.Text";
					break;
					
				case OctopusAccountExceptionsEnum.LocalNumberIsNull:
					returned = "AccountExceptionLocalNumberIsNull.Text";
					break;
					
				case OctopusAccountExceptionsEnum.NumberIsNull:
					returned = "AccountExceptionNumberIsNull.Text";
					break;

				case OctopusAccountExceptionsEnum.IncorrectAmountFormat:
					returned = "AccountExceptionIncorrectAmountFormat.Text";
					break;

				case OctopusAccountExceptionsEnum.MovementSetUnbalanced:
					returned = "AccountExceptionMovementSetUnbalanced.Text";
					break;

				case OctopusAccountExceptionsEnum.ProvisioningRateNbOfDaysMaxEmpty:
					returned = "ProvisioningRateNbOfDaysMaxEmpty.Text";
					break;

				case OctopusAccountExceptionsEnum.ProvisioningRateNbOfDaysMinEmpty:
					returned = "ProvisioningRateNbOfDaysMinEmpty.Text";
					break;

				case OctopusAccountExceptionsEnum.ProvisioningRateRateEmpty:
					returned = "ProvisioningRateRateEmpty.Text";
					break;
                case OctopusAccountExceptionsEnum.LoanScaleTableMin:
                    returned = "LoanScaleTableMin.Text";
                    break;
                case OctopusAccountExceptionsEnum.LoanScaleTableMax:
                    returned = "LoanScaleTableMax.Text";
                    break;
                case OctopusAccountExceptionsEnum.CurrencyAlreadyExists:
                    returned = "CurrencyAlreadyExists.Text";
                    break;
                case OctopusAccountExceptionsEnum.MaximumCurrencyLimitReached:
                    returned = "MaximumCurrencyLimitReached.Text";
                    break;
                case OctopusAccountExceptionsEnum.CurrencyIsEmpty:
                    returned = "AccountExceptionCurrencyIsNull.Text.Text";
                    break;
                case OctopusAccountExceptionsEnum.DescriptionIsEmpty:
                    returned = "AccountExceptionDescriptionIsEmpty.Text";
                    break;
                case OctopusAccountExceptionsEnum.AccountCannotBeDeleted:
                    returned = "AccountExceptionCannotBeDeleted.Text";
                    break;
                case OctopusAccountExceptionsEnum.ParentAccountDoesntExists:
                    returned = "AccountExceptionParentAccountDoesntExists.Text";
                    break;
                case OctopusAccountExceptionsEnum.ParentAccountNotSameDescription:
                    returned = "AccountExceptionParentAccountNotSameDescription.Text";
                    break;
                case OctopusAccountExceptionsEnum.ParentAccountIsInvalid:
                    returned = "AccountExceptionParentAccountIsInvalid.Text";
                    break;
                case OctopusAccountExceptionsEnum.EqualAccounts :
			        returned = "ManualEntrySameAccount.Text";
			        break;
                case OctopusAccountExceptionsEnum.AccountIsUsed :
			        returned = "AccountExceptionIsUsed.Text";
			        break;
                case OctopusAccountExceptionsEnum.ImportIdExist:
			        returned = "AccountImportIdExist.Text";
                    break;
                case OctopusAccountExceptionsEnum.ImportNumbersExist:
                    returned = "AccountImportNumbersExist.Text";
                    break;
                case OctopusAccountExceptionsEnum.DuplicatedAccount:
			        returned = "DuplicatedAccount.Text";
                    break;
			}
			return returned;
		}
	}

    [Serializable]
	public enum OctopusAccountExceptionsEnum
	{
		AccountIsNull,
		LabelIsNull,
		LocalNumberIsNull,
		NumberIsNull,
		IncorrectAmountFormat,
		MovementSetUnbalanced,
		ProvisioningRateNbOfDaysMinEmpty,
		ProvisioningRateNbOfDaysMaxEmpty,
		ProvisioningRateRateEmpty,
        LoanScaleTableMin,
        LoanScaleTableMax,
        CurrencyAlreadyExists,
        MaximumCurrencyLimitReached,
        CurrencyIsEmpty,
        DescriptionIsEmpty,
        AccountCannotBeDeleted,
        ParentAccountDoesntExists,
        ParentAccountNotSameDescription,
        ParentAccountIsInvalid,
        EqualAccounts,
        AccountIsUsed,
        ImportIdExist,
        ImportNumbersExist,
        DuplicatedAccount
	}
}
