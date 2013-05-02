// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler
{
	/// <summary>
	/// Summary description for OctopusSavingException.
	/// </summary>
	[Serializable]
	public class OctopusSavingProductException : OctopusException
	{
        private readonly string _message;
        private OctopusSavingProductExceptionEnum _code;
        public OctopusSavingProductExceptionEnum Code { get { return _code; } }

        protected OctopusSavingProductException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _code = (OctopusSavingProductExceptionEnum)info.GetInt32("Code");
        }

        protected OctopusSavingProductException(SerializationInfo info, StreamingContext context, List<string> options)
            : base(info, context)
        {
            _code = (OctopusSavingProductExceptionEnum)info.GetInt32("Code");
            AdditionalOptions = options;
        }


		public OctopusSavingProductException()
		{
            _message = string.Empty;
		}

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", _code);
            base.GetObjectData(info, context);
        }

        public OctopusSavingProductException(OctopusSavingProductExceptionEnum exceptionCode)
        {
            _code = exceptionCode;
            _message = FindException(exceptionCode);
        }

        public OctopusSavingProductException(string message)
		{
			_message = message;
		}

		public override string ToString()
		{
			return _message;
		}

        private static string FindException(OctopusSavingProductExceptionEnum exceptionId)
        {
            string returned = String.Empty;
            switch (exceptionId)
            {
                case OctopusSavingProductExceptionEnum.NameIsEmpty:
                    returned = "SavingProductNameIsEmpty.Text";
                    break;

                case OctopusSavingProductExceptionEnum.DuplicateProductName:
                    returned = "SavingProductDuplicateProductName.Text";
                    break;

                case OctopusSavingProductExceptionEnum.DuplicateProductCode:
                    returned = "SavingProductDuplicateProductCode.Text";
                    break;

                case OctopusSavingProductExceptionEnum.BalanceIsInvalid:
                    returned = "SavingProductBalanceIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.InitialAmountIsInvalid:
                    returned = "SavingProductInitialAmountIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.WithdrawAmountIsInvalid:
                    returned = "SavingProductWithdrawAmountIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.DepositAmountIsInvalid:
                    returned = "SavingProductDepositAmountIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.InterestRateMinMaxIsInvalid:
                    returned = "SavingProductInterestRateMinMaxIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.DepositMinAmountIsInvalid:
                    returned = "SavingProductDepositMinAmountIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.InterestRateIsInvalid:
                    returned = "SavingProductInterestRateIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.InterestRateMinIsInvalid:
                    returned = "SavingProductInterestRateMinMaxIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.WithdrawMinAmountIsInvalid:
                    returned = "SavingProductWithdrawMinAmountIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.InitialAmountMinIsInvalid:
                    returned = "SavingProductInitialAmountMinIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.InitialAmountMinMaxIsInvalid:
                    returned = "SavingProductInitialAmountMinMaxIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.InitialAmountMaxNotInBalance:
                    returned = "SavingProductInitialAmountMaxNotInBalance.Text";
                    break;

                case OctopusSavingProductExceptionEnum.InitialAmountMinNotInBalance:
                    returned = "SavingProductInitialAmountMinNotInBalance.Text";
                    break;

                case OctopusSavingProductExceptionEnum.InterestsFrequencyIsInvalid:
                    returned = "SavingProductInterestsFrequencyIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.InterestsBaseIsInvalid:
                    returned = "SavingProductInterestsBaseIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.CurrencyIsEmpty:
                    returned = "CurrencyIsEmptyForSavingProduct.Text";
                    break;

                case OctopusSavingProductExceptionEnum.ClientTypeIsInvalid:
                    returned = "ClientTypeIsEmpty.Text";
                    break;

                case OctopusSavingProductExceptionEnum.CalculAmountBaseIsNull:
                    returned = "SavingProductCalculAmountBaseIsNull.Text";
                    break;

                case OctopusSavingProductExceptionEnum.InterestBaseIncompatibleFrequency:
                    returned = "SavingProductInterestBaseIncompatibleFrequency.Text";
                    break;

                case OctopusSavingProductExceptionEnum.EntryFeesMinMaxIsInvalid:
                    returned = "SavingProductEntryFeesMinMaxIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.EntryFeesIsInvalid:
                    returned = "SavingProductEntryFeesIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.NumberPeriodIsInvalid:
                    returned = "SavingProductNumberPeriodIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.NumberPeriodMinIsInvalid:
                    returned = "SavingProductNumberPeriodMinIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.NumberPeriodMinMaxIsInvalid:
                    returned = "SavingProductNumberPeriodMinMaxIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.WithdrawFeesTypeEmpty:
                    returned = "SavingProductWithdrawFeesTypeEmpty.Text";
                    break;

                case OctopusSavingProductExceptionEnum.FlatWithdrawFeesIsInvalid:
                    returned = "SavingProductFlatWithdrawFeesIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.FlatWithdrawFeesMinIsInvalid:
                    returned = "SavingProductFlatWithdrawFeesMinIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.FlatWithdrawFeesMinMaxIsInvalid:
                    returned = "SavingProductFlatWithdrawFeesMinMaxIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.RateWithdrawFeesIsInvalid:
                    returned = "SavingProductRateWithdrawFeesIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.RateWithdrawFeesMinIsInvalid:
                    returned = "SavingProductRateWithdrawFeesMinIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.RateWithdrawFeesMinMaxIsInvalid:
                    returned = "SavingProductRateWithdrawFeesMinMaxIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.TransferAmountIsInvalid:
                    returned = "SavingProductTransferAmountIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.TransferAmountMinIsInvalid:
                    returned = "SavingProductTransferAmountMinIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.WithdrawalFeesMinMaxIsInvalid:
                    returned = "SavingProductWithdrawalFeesMinMaxIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.WithdrawalFeesIsInvalid:
                    returned = "SavingProductWithdrawalFeesIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.WithdrawalFeesMinIsInvalid:
                    returned = "SavingProductWithdrawalFeesMinIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.CodeIsEmpty:
                    returned = "SavingProductCodeIsEmpty.Text";
                    break;

                case OctopusSavingProductExceptionEnum.TransferFeesTypeEmpty:
                    returned = "SavingProductTransferFeesTypeEmpty.Text";
                    break;

                case OctopusSavingProductExceptionEnum.FlatTransferFeesIsInvalid:
                    returned = "SavingProductFlatTransferFeesIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.FlatTransferFeesMinIsInvalid:
                    returned = "SavingProductFlatTransferFeesMinIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.FlatTransferFeesMinMaxIsInvalid:
                    returned = "SavingProductFlatTransferFeesMinMaxIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.RateTransferFeesIsInvalid:
                    returned = "SavingProductRateTransferFeesIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.RateTransferFeesMinIsInvalid:
                    returned = "SavingProductRateTransferFeesMinIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.RateTransferFeesMinMaxIsInvalid:
                    returned = "SavingProductRateTransferFeesMinMaxIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.InterBranchFlatTransferFeesIsInvalid:
                    returned = "SavingProductInterBranchFlatTransferFeesIsInvalid";
                    break;

                case OctopusSavingProductExceptionEnum.InterBranchFlatTransferFeesMinIsInvalid:
                    returned = "SavingProductInterBranchFlatTransferFeesMinIsInvalid";
                    break;

                case OctopusSavingProductExceptionEnum.InterBranchFlatTransferFeesMinMaxIsInvalid:
                    returned = "SavingProductInterBranchFlatTransferFeesMinMaxIsInvalid";
                    break;

                case OctopusSavingProductExceptionEnum.InterBranchRateTransferFeesIsInvalid:
                    returned = "SavingProductInterBranchRateTransferFeesIsInvalid";
                    break;

                case OctopusSavingProductExceptionEnum.InterBranchRateTransferFeesMinIsInvalid:
                    returned = "SavingProductInterBranchRateTransferFeesMinIsInvalid";
                    break;

                case OctopusSavingProductExceptionEnum.InterBranchRateTransferFeesMinMaxIsInvalid:
                    returned = "SavingProductInterBranchRateTransferFeesMinMaxIsInvalid";
                    break;

                case OctopusSavingProductExceptionEnum.LoanAmountIsInvalid:
                    returned = "SavingProductLoanAmountIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.LoanAmountMinIsInvalid:
                    returned = "SavingProductLoanAmountMinIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.LoanAmountMinMaxIsInvalid:
                    returned = "SavingProductLoanAmountMinMaxIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.DepositFeesIsInvalid:
                    returned = "SavingProductDepositFeesIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.DepositFeesMinIsInvalid:
                    returned = "SavingProductDepositFeesMinIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.CloseFeesIsInvalid:
                    returned = "SavingProductCloseFeesIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.CloseFeesMinIsInvalid:
                    returned = "SavingProductCloseFeesMinIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.ManagementFeesIsInvalid:
                    returned = "SavingProductManagementFeesIsInvalid.Text";
                    break;

                case OctopusSavingProductExceptionEnum.ManagementFeesMinIsInvalid:
                    returned = "SavingProductManagementFeesMinIsInvalid.Text";
                    break;
                case OctopusSavingProductExceptionEnum.PeriodicityIsEmpty:
                    returned = "SavingProductPeriodicityIsEmpty.Text";
                    break;
                case OctopusSavingProductExceptionEnum.OverdraftFeesIsInvalid:
                    returned = "OverdraftFeesIsInvalid.Text";
                    break;
                case OctopusSavingProductExceptionEnum.AgioFeesIsInvalid:
                    returned = "AgioFeesIsInvalid.Text";
                    break;
                case OctopusSavingProductExceptionEnum.ChequeDepositIsInvalid:
                    returned = "ChequeDepositIsInvalid";
                    break;
                case OctopusSavingProductExceptionEnum.ChequeDepositFeesIsInvalid:
                    returned = "ChequeDepositFeesIsInvalid";
                    break;
                case OctopusSavingProductExceptionEnum.ReopenFeesIsInvalid:
                    returned = "ReopenFeesIsInvalid";
                    break;
                case OctopusSavingProductExceptionEnum.ReopenFeesMinIsInvalid:
                    returned = "ReopenFeesMinIsInvalid";
                    break;
                case OctopusSavingProductExceptionEnum.PostingFrequencyIsInvalid:
                    returned = "PostingFrequencyIsInvalid.Text";
                    break;
                case OctopusSavingProductExceptionEnum.PeriodicityIsNotSet:
                    returned = "PeriodicityIsNotSet.Text";
                    break;
            }
            return returned;
        }
	}

	[Serializable]
	public enum OctopusSavingProductExceptionEnum
	{
		DuplicateProductName,
        DuplicateProductCode,
		InterestRateIsInvalid,
		BalanceIsInvalid,
		DepositMinAmountIsInvalid,
		DepositAmountIsInvalid,
        CodeIsEmpty,
		WithdrawAmountIsInvalid,
		WithdrawMinAmountIsInvalid,
		InterestRateMinIsInvalid,
		InterestRateMinMaxIsInvalid,
		InitialAmountIsInvalid,
        InitialAmountMinMaxIsInvalid,
		InitialAmountMinIsInvalid,
		InitialAmountMinNotInBalance,
		InitialAmountMaxNotInBalance,
        InterestsFrequencyIsInvalid,
        InterestsBaseIsInvalid,
        InterestBaseIncompatibleFrequency,
	    NameIsEmpty,
        CurrencyIsEmpty,
        ClientTypeIsInvalid,
        CalculAmountBaseIsNull,
        EntryFeesMinMaxIsInvalid,
        EntryFeesIsInvalid, 
        NumberPeriodIsInvalid,
        NumberPeriodMinIsInvalid,
        NumberPeriodMinMaxIsInvalid, 
        PeriodicityIsEmpty,
        WithdrawFeesTypeEmpty,
        FlatWithdrawFeesIsInvalid,
        FlatWithdrawFeesMinIsInvalid,
        FlatWithdrawFeesMinMaxIsInvalid,
        RateWithdrawFeesIsInvalid,
        RateWithdrawFeesMinIsInvalid,
        RateWithdrawFeesMinMaxIsInvalid,
        TransferAmountIsInvalid,
        TransferAmountMinIsInvalid,
        WithdrawalFeesMinMaxIsInvalid,
        WithdrawalFeesIsInvalid,
        WithdrawalFeesMinIsInvalid,
        TransferFeesTypeEmpty,

        FlatTransferFeesIsInvalid,
        FlatTransferFeesMinIsInvalid,
        FlatTransferFeesMinMaxIsInvalid,
        RateTransferFeesIsInvalid,
        RateTransferFeesMinIsInvalid,
        RateTransferFeesMinMaxIsInvalid,
        
        InterBranchFlatTransferFeesIsInvalid,
        InterBranchFlatTransferFeesMinIsInvalid,
        InterBranchFlatTransferFeesMinMaxIsInvalid,
        InterBranchRateTransferFeesIsInvalid,
        InterBranchRateTransferFeesMinIsInvalid,
        InterBranchRateTransferFeesMinMaxIsInvalid,

        LoanAmountIsInvalid,
        LoanAmountMinIsInvalid,
        LoanAmountMinMaxIsInvalid,
        DepositFeesIsInvalid,
        DepositFeesMinIsInvalid,
        CloseFeesIsInvalid,
        CloseFeesMinIsInvalid,
        ManagementFeesIsInvalid,
        ManagementFeesMinIsInvalid,
        OverdraftFeesIsInvalid,
        OverdraftFeesMinIsInvalid,
        AgioFeesIsInvalid,
        AgioFeesMinIsInvalid,
        ChequeDepositIsInvalid,
        ChequeDepositFeesIsInvalid,
        ReopenFeesIsInvalid,
        ReopenFeesMinIsInvalid,
        PostingFrequencyIsInvalid,
        PeriodicityIsNotSet

	}
}
