// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler.Exceptions.SavingExceptions
{
	/// <summary>
	/// Summary description for OpenCbsSavingException.
	/// </summary>
	[Serializable]
	public class OpenCbsSavingException : OpenCbsException
	{
		private OpenCbsSavingExceptionEnum _code;
        private readonly string _message;
		public OpenCbsSavingExceptionEnum Code{get{ return _code;}}
        
		public OpenCbsSavingException(OpenCbsSavingExceptionEnum exceptionCode)
		{
            _code = exceptionCode;
            _message = _FindException(exceptionCode);
		}

        public OpenCbsSavingException(OpenCbsSavingExceptionEnum exceptionCode, List<string> additionalOptions)
        {
            _code = exceptionCode;
            _message = _FindException(exceptionCode);
            AdditionalOptions = additionalOptions;
        }

		public override string ToString()
		{
			return _message;
		}

		protected OpenCbsSavingException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			_code = (OpenCbsSavingExceptionEnum)info.GetInt32("Code");
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Code", (int)_code);
			base.GetObjectData(info, context);
		}

        private static string _FindException(OpenCbsSavingExceptionEnum exceptionId)
        {
            string returned = String.Empty;
            switch (exceptionId)
            {
                case OpenCbsSavingExceptionEnum.BalanceIsInvalid:
                    returned = "SavingBalanceIsInvalid.Text";
                    break;

                case OpenCbsSavingExceptionEnum.DepositAmountIsInvalid:
                    returned = "SavingDepositAmountIsInvalid.Text";
                    break;

                case OpenCbsSavingExceptionEnum.DepositDateIsInvalid:
                    returned = "SavingDepositDateIsInvalid.Text";
                    break;

                case OpenCbsSavingExceptionEnum.InitialAmountIsInvalid:
                    returned = "SavingInitialAmountIsInvalid.Text";
                    break;

                case OpenCbsSavingExceptionEnum.InterestRateIsInvalid:
                    returned = "SavingInterestRateIsInvalid.Text";
                    break;

                case OpenCbsSavingExceptionEnum.ProductIsInvalid:
                    returned = "SavingProductIsInvalid.Text";
                    break;

                case OpenCbsSavingExceptionEnum.SavingContractIsNull:
                    returned = "SavingContractIsNull.Text";
                    break;

                case OpenCbsSavingExceptionEnum.WithdrawalDateIsInvalid:
                    returned = "SavingWithdrawalDateIsInvalid.Text";
                    break;

                case OpenCbsSavingExceptionEnum.WithdrawAmountIsInvalid:
                    returned = "SavingWithdrawAmountIsInvalid.Text";
                    break;

                case OpenCbsSavingExceptionEnum.EntryFeesIsInvalid:
                    returned = "SavingEntryFeesIsInvalid.Text";
                    break;

                case OpenCbsSavingExceptionEnum.ContractNotAtMaturity:
                    returned = "SavingContractNotAtMaturity.Text";
                    break;

                case OpenCbsSavingExceptionEnum.TransferAmountIsInvalid:
                    returned = "SavingTransferAmountIsInvalid.Text";
                    break;

                case OpenCbsSavingExceptionEnum.SavingsContractForTransferIdenticals:
                    returned = "SavingContractForTransferIdenticals.Text";
                    break;
            
                case OpenCbsSavingExceptionEnum.CreditTransferAccountInvalid:
                    returned = "SavingCreditTransferAccountInvalid.Text";
                    break;

                case OpenCbsSavingExceptionEnum.WithdrawFeesIsInvalid:
                    returned = "SavingWithdrawFeesIsInvalid.Text";
                    break;

                case OpenCbsSavingExceptionEnum.SavingsContractForTransferNotSameCurrncy:
                    returned = "SavingContractForTransferNotSameCurrncy.Text";
                    break;

                case OpenCbsSavingExceptionEnum.RolloverIsInvalid:
                    returned = "SavingContractRolloverIsInvalid.Text";
                    break;

                case OpenCbsSavingExceptionEnum.TransferAccountIsInvalid:
                    returned = "SavingContractRolloverTransferAccountIsInvalid.Text";
                    break;

                case OpenCbsSavingExceptionEnum.TransferAccountIsClosed:
                    returned = "SavingContractRolloverTransferAccountIsClosed.Text";
                    break;

                case OpenCbsSavingExceptionEnum.TransferAccountIsSavingDepositOrCompulsorySavings:
                    returned = "SavingContractRolloverTransferAccountIsSavingDepositOrCompulsorySavings.Text";
                    break;

                case OpenCbsSavingExceptionEnum.TransferAccountHasNotSameClient:
                    returned = "SavingContractRolloverTransferAccountHasNotSameClient.Text";
                    break;

                case OpenCbsSavingExceptionEnum.LoanIsInvalid:
                    returned = "SavingContractLoanIsInvalid.Text";
                    break;

                case OpenCbsSavingExceptionEnum.LoanIsClosed:
                    returned = "SavingContractLoanIsClosed.Text";
                    break;

                case OpenCbsSavingExceptionEnum.LoanHasNotSameClient:
                    returned = "SavingContractLoanHasNotSameClient.Text";
                    break;

                case OpenCbsSavingExceptionEnum.LoanHasNotSameCurrency:
                    returned = "SavingContractLoanHasNotSameCurrency.Text";
                    break;

                case OpenCbsSavingExceptionEnum.TransferFeesIsInvalid:
                    returned = "SavingContractTransferFeesIsInvalid.Text";
                    break;

                case OpenCbsSavingExceptionEnum.CompulsorySavingsLinkedToLoan:
                    returned = "SavingContractCompulsorySavingsLinkedToLoan.Text";
                    break;

                case OpenCbsSavingExceptionEnum.LoanDoesNotAcceptSavings:
                    returned = "SavingContractLoanDoesNotAcceptSavings.Text";
                    break;

                case OpenCbsSavingExceptionEnum.NoCompulsorySavings:
                    returned = "SavingContractNoCompulsorySavings.Text";
                    break;

                case OpenCbsSavingExceptionEnum.AmountOnCompulsorySavingsIsInvalid:
                    returned = "SavingContractAmountOnCompulsorySavingsIsInvalid.Text";
                    break;

                case OpenCbsSavingExceptionEnum.DepositFeesIsInvalid:
                    returned = "SavingContractDepositFeesIsInvalid.Text";
                    break;

                case OpenCbsSavingExceptionEnum.CloseFeesIsInvalid:
                    returned = "SavingContractCloseFeesIsInvalid.Text";
                    break;

                case OpenCbsSavingExceptionEnum.ManagementFeesIsInvalid:
                    returned = "SavingContractManagementFeesIsInvalid.Text";
                    break;
                case OpenCbsSavingExceptionEnum.AgioFeesIsInvalid:
                    returned = "SavingContractAgioFeesIsInvalid.Text";
                    break;
                case OpenCbsSavingExceptionEnum.SavingsEventCannotBeCanceled:
                    returned = "SavingsEventCannotBeCanceled.Text";
                    break;
                case OpenCbsSavingExceptionEnum.SavingsEventCommentIsEmpty:
                    returned = "SavingsEventCommentIsEmpty.Text";
                    break;
                case OpenCbsSavingExceptionEnum.BalanceOnCurrentSavingAccount:
                    returned = "BalanceOnCurrentSavingAccount";
                    break;
                case OpenCbsSavingExceptionEnum.BalanceOnCurrentSavingAccountForTransfer:
                    returned = "BalanceOnCurrentSavingAccountForTransfer";
                    break;

                case OpenCbsSavingExceptionEnum.CompulsorySavingsContractIsNotActive:
                    returned = "CompulsorySavingsContractIsNotActive.Text";
                    break;
                case  OpenCbsSavingExceptionEnum.TransferAccountHasWrongCurrency:
                    returned = "TransferAccountHasWrongCurrency.Text";
                    break;
                case OpenCbsSavingExceptionEnum.TransactionInvalid:
                    returned = "SelectBeforeConf.Text";
                    break;
            }

            return returned;
        }
	}

    [Serializable]
    public enum OpenCbsSavingExceptionEnum
    {
		SavingContractIsNull,
		InterestRateIsInvalid,
		InitialAmountIsInvalid,
		BalanceIsInvalid,
		DepositAmountIsInvalid,
		WithdrawAmountIsInvalid,
        ProductIsInvalid,
        DepositDateIsInvalid,
        WithdrawalDateIsInvalid,
        EntryFeesIsInvalid,
        ContractNotAtMaturity,
        TransferAmountIsInvalid, 
        SavingsContractForTransferIdenticals,
        CreditTransferAccountInvalid,
        WithdrawFeesIsInvalid,
        SavingsContractForTransferNotSameCurrncy,
        RolloverIsInvalid,
        TransferAccountIsInvalid,
        TransferAccountIsClosed,
        TransferAccountIsSavingDepositOrCompulsorySavings,
        TransferAccountHasNotSameClient,
        LoanIsInvalid,
        LoanIsClosed,
        LoanHasNotSameClient,
        LoanHasNotSameCurrency,
        TransferFeesIsInvalid,
        CompulsorySavingsLinkedToLoan,
        LoanDoesNotAcceptSavings,
        NoCompulsorySavings,
        AmountOnCompulsorySavingsIsInvalid,
        DepositFeesIsInvalid,
        CloseFeesIsInvalid,
        ManagementFeesIsInvalid,
        AgioFeesIsInvalid,
        SavingsEventCannotBeCanceled,
        SavingsEventCommentIsEmpty,
        BalanceOnCurrentSavingAccount,
        BalanceOnCurrentSavingAccountForTransfer,
        CompulsorySavingsContractIsNotActive,
        TransferAccountHasWrongCurrency,
        TransactionInvalid
    }
}
