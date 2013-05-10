// LICENSE PLACEHOLDER

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
