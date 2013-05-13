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

namespace OpenCBS.ExceptionsHandler.Exceptions.AccountExceptions
{
	/// <summary>
	/// Summary description for OpenCbsAccountException.
	/// </summary>
    [Serializable]
    public class OpenCbsAccountException : OpenCbsException
	{
		private readonly string _code;
		public OpenCbsAccountException(OpenCbsAccountExceptionsEnum exceptionCode)
		{
			_code = _FindException(exceptionCode);
		}

		public override string ToString()
		{
			return _code;
		}

        protected OpenCbsAccountException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _code = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", _code);
            base.GetObjectData(info, context);
        }

		private string _FindException(OpenCbsAccountExceptionsEnum exceptionId)
		{
			string returned = String.Empty;
			switch(exceptionId)
			{
				case OpenCbsAccountExceptionsEnum.AccountIsNull:
					returned = "AccountExceptionAccountIsNull.Text";
					break;

				case OpenCbsAccountExceptionsEnum.LabelIsNull:
					returned = "AccountExceptionLabelIsNull.Text";
					break;
					
				case OpenCbsAccountExceptionsEnum.LocalNumberIsNull:
					returned = "AccountExceptionLocalNumberIsNull.Text";
					break;
					
				case OpenCbsAccountExceptionsEnum.NumberIsNull:
					returned = "AccountExceptionNumberIsNull.Text";
					break;

				case OpenCbsAccountExceptionsEnum.IncorrectAmountFormat:
					returned = "AccountExceptionIncorrectAmountFormat.Text";
					break;

				case OpenCbsAccountExceptionsEnum.MovementSetUnbalanced:
					returned = "AccountExceptionMovementSetUnbalanced.Text";
					break;

				case OpenCbsAccountExceptionsEnum.ProvisioningRateNbOfDaysMaxEmpty:
					returned = "ProvisioningRateNbOfDaysMaxEmpty.Text";
					break;

				case OpenCbsAccountExceptionsEnum.ProvisioningRateNbOfDaysMinEmpty:
					returned = "ProvisioningRateNbOfDaysMinEmpty.Text";
					break;

				case OpenCbsAccountExceptionsEnum.ProvisioningRateRateEmpty:
					returned = "ProvisioningRateRateEmpty.Text";
					break;
                case OpenCbsAccountExceptionsEnum.LoanScaleTableMin:
                    returned = "LoanScaleTableMin.Text";
                    break;
                case OpenCbsAccountExceptionsEnum.LoanScaleTableMax:
                    returned = "LoanScaleTableMax.Text";
                    break;
                case OpenCbsAccountExceptionsEnum.CurrencyAlreadyExists:
                    returned = "CurrencyAlreadyExists.Text";
                    break;
                case OpenCbsAccountExceptionsEnum.MaximumCurrencyLimitReached:
                    returned = "MaximumCurrencyLimitReached.Text";
                    break;
                case OpenCbsAccountExceptionsEnum.CurrencyIsEmpty:
                    returned = "AccountExceptionCurrencyIsNull.Text.Text";
                    break;
                case OpenCbsAccountExceptionsEnum.DescriptionIsEmpty:
                    returned = "AccountExceptionDescriptionIsEmpty.Text";
                    break;
                case OpenCbsAccountExceptionsEnum.AccountCannotBeDeleted:
                    returned = "AccountExceptionCannotBeDeleted.Text";
                    break;
                case OpenCbsAccountExceptionsEnum.ParentAccountDoesntExists:
                    returned = "AccountExceptionParentAccountDoesntExists.Text";
                    break;
                case OpenCbsAccountExceptionsEnum.ParentAccountNotSameDescription:
                    returned = "AccountExceptionParentAccountNotSameDescription.Text";
                    break;
                case OpenCbsAccountExceptionsEnum.ParentAccountIsInvalid:
                    returned = "AccountExceptionParentAccountIsInvalid.Text";
                    break;
                case OpenCbsAccountExceptionsEnum.EqualAccounts :
			        returned = "ManualEntrySameAccount.Text";
			        break;
                case OpenCbsAccountExceptionsEnum.AccountIsUsed :
			        returned = "AccountExceptionIsUsed.Text";
			        break;
                case OpenCbsAccountExceptionsEnum.ImportIdExist:
			        returned = "AccountImportIdExist.Text";
                    break;
                case OpenCbsAccountExceptionsEnum.ImportNumbersExist:
                    returned = "AccountImportNumbersExist.Text";
                    break;
                case OpenCbsAccountExceptionsEnum.DuplicatedAccount:
			        returned = "DuplicatedAccount.Text";
                    break;
			}
			return returned;
		}
	}

    [Serializable]
	public enum OpenCbsAccountExceptionsEnum
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
