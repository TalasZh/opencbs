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
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler
{
	/// <summary>
	/// Summary description for OpenCbsSavingException.
	/// </summary>
	[Serializable]
	public class OpenCbsSavingProductException : OpenCbsException
	{
        private readonly string _message;
        private OpenCbsSavingProductExceptionEnum _code;
        public OpenCbsSavingProductExceptionEnum Code { get { return _code; } }

        protected OpenCbsSavingProductException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _code = (OpenCbsSavingProductExceptionEnum)info.GetInt32("Code");
        }

        protected OpenCbsSavingProductException(SerializationInfo info, StreamingContext context, List<string> options)
            : base(info, context)
        {
            _code = (OpenCbsSavingProductExceptionEnum)info.GetInt32("Code");
            AdditionalOptions = options;
        }


		public OpenCbsSavingProductException()
		{
            _message = string.Empty;
		}

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", _code);
            base.GetObjectData(info, context);
        }

        public OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum exceptionCode)
        {
            _code = exceptionCode;
            _message = FindException(exceptionCode);
        }

        public OpenCbsSavingProductException(string message)
		{
			_message = message;
		}

		public override string ToString()
		{
			return _message;
		}

        private static string FindException(OpenCbsSavingProductExceptionEnum exceptionId)
        {
            string returned = String.Empty;
            switch (exceptionId)
            {
                case OpenCbsSavingProductExceptionEnum.NameIsEmpty:
                    returned = "SavingProductNameIsEmpty.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.DuplicateProductName:
                    returned = "SavingProductDuplicateProductName.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.DuplicateProductCode:
                    returned = "SavingProductDuplicateProductCode.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.BalanceIsInvalid:
                    returned = "SavingProductBalanceIsInvalid.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.InitialAmountIsInvalid:
                    returned = "SavingProductInitialAmountIsInvalid.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.WithdrawAmountIsInvalid:
                    returned = "SavingProductWithdrawAmountIsInvalid.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.DepositAmountIsInvalid:
                    returned = "SavingProductDepositAmountIsInvalid.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.InterestRateMinMaxIsInvalid:
                    returned = "SavingProductInterestRateMinMaxIsInvalid.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.DepositMinAmountIsInvalid:
                    returned = "SavingProductDepositMinAmountIsInvalid.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.InterestRateIsInvalid:
                    returned = "SavingProductInterestRateIsInvalid.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.InterestRateMinIsInvalid:
                    returned = "SavingProductInterestRateMinMaxIsInvalid.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.WithdrawMinAmountIsInvalid:
                    returned = "SavingProductWithdrawMinAmountIsInvalid.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.InitialAmountMinIsInvalid:
                    returned = "SavingProductInitialAmountMinIsInvalid.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.InitialAmountMinMaxIsInvalid:
                    returned = "SavingProductInitialAmountMinMaxIsInvalid.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.InitialAmountMaxNotInBalance:
                    returned = "SavingProductInitialAmountMaxNotInBalance.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.InitialAmountMinNotInBalance:
                    returned = "SavingProductInitialAmountMinNotInBalance.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.InterestsFrequencyIsInvalid:
                    returned = "SavingProductInterestsFrequencyIsInvalid.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.InterestsBaseIsInvalid:
                    returned = "SavingProductInterestsBaseIsInvalid.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.CurrencyIsEmpty:
                    returned = "CurrencyIsEmptyForSavingProduct.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.ClientTypeIsInvalid:
                    returned = "ClientTypeIsEmpty.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.CalculAmountBaseIsNull:
                    returned = "SavingProductCalculAmountBaseIsNull.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.InterestBaseIncompatibleFrequency:
                    returned = "SavingProductInterestBaseIncompatibleFrequency.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.EntryFeesMinMaxIsInvalid:
                    returned = "SavingProductEntryFeesMinMaxIsInvalid.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.EntryFeesIsInvalid:
                    returned = "SavingProductEntryFeesIsInvalid.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.NumberPeriodIsInvalid:
                    returned = "SavingProductNumberPeriodIsInvalid.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.NumberPeriodMinIsInvalid:
                    returned = "SavingProductNumberPeriodMinIsInvalid.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.NumberPeriodMinMaxIsInvalid:
                    returned = "SavingProductNumberPeriodMinMaxIsInvalid.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.WithdrawFeesTypeEmpty:
                    returned = "SavingProductWithdrawFeesTypeEmpty.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.FlatWithdrawFeesIsInvalid:
                    returned = "SavingProductFlatWithdrawFeesIsInvalid.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.FlatWithdrawFeesMinIsInvalid:
                    returned = "SavingProductFlatWithdrawFeesMinIsInvalid.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.FlatWithdrawFeesMinMaxIsInvalid:
                    returned = "SavingProductFlatWithdrawFeesMinMaxIsInvalid.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.RateWithdrawFeesIsInvalid:
                    returned = "SavingProductRateWithdrawFeesIsInvalid.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.RateWithdrawFeesMinIsInvalid:
                    returned = "SavingProductRateWithdrawFeesMinIsInvalid.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.RateWithdrawFeesMinMaxIsInvalid:
                    returned = "SavingProductRateWithdrawFeesMinMaxIsInvalid.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.TransferAmountIsInvalid:
                    returned = "SavingProductTransferAmountIsInvalid.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.TransferAmountMinIsInvalid:
                    returned = "SavingProductTransferAmountMinIsInvalid.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.WithdrawalFeesMinMaxIsInvalid:
                    returned = "SavingProductWithdrawalFeesMinMaxIsInvalid.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.WithdrawalFeesIsInvalid:
                    returned = "SavingProductWithdrawalFeesIsInvalid.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.WithdrawalFeesMinIsInvalid:
                    returned = "SavingProductWithdrawalFeesMinIsInvalid.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.CodeIsEmpty:
                    returned = "SavingProductCodeIsEmpty.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.TransferFeesTypeEmpty:
                    returned = "SavingProductTransferFeesTypeEmpty.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.FlatTransferFeesIsInvalid:
                    returned = "SavingProductFlatTransferFeesIsInvalid.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.FlatTransferFeesMinIsInvalid:
                    returned = "SavingProductFlatTransferFeesMinIsInvalid.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.FlatTransferFeesMinMaxIsInvalid:
                    returned = "SavingProductFlatTransferFeesMinMaxIsInvalid.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.RateTransferFeesIsInvalid:
                    returned = "SavingProductRateTransferFeesIsInvalid.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.RateTransferFeesMinIsInvalid:
                    returned = "SavingProductRateTransferFeesMinIsInvalid.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.RateTransferFeesMinMaxIsInvalid:
                    returned = "SavingProductRateTransferFeesMinMaxIsInvalid.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.InterBranchFlatTransferFeesIsInvalid:
                    returned = "SavingProductInterBranchFlatTransferFeesIsInvalid";
                    break;

                case OpenCbsSavingProductExceptionEnum.InterBranchFlatTransferFeesMinIsInvalid:
                    returned = "SavingProductInterBranchFlatTransferFeesMinIsInvalid";
                    break;

                case OpenCbsSavingProductExceptionEnum.InterBranchFlatTransferFeesMinMaxIsInvalid:
                    returned = "SavingProductInterBranchFlatTransferFeesMinMaxIsInvalid";
                    break;

                case OpenCbsSavingProductExceptionEnum.InterBranchRateTransferFeesIsInvalid:
                    returned = "SavingProductInterBranchRateTransferFeesIsInvalid";
                    break;

                case OpenCbsSavingProductExceptionEnum.InterBranchRateTransferFeesMinIsInvalid:
                    returned = "SavingProductInterBranchRateTransferFeesMinIsInvalid";
                    break;

                case OpenCbsSavingProductExceptionEnum.InterBranchRateTransferFeesMinMaxIsInvalid:
                    returned = "SavingProductInterBranchRateTransferFeesMinMaxIsInvalid";
                    break;

                case OpenCbsSavingProductExceptionEnum.LoanAmountIsInvalid:
                    returned = "SavingProductLoanAmountIsInvalid.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.LoanAmountMinIsInvalid:
                    returned = "SavingProductLoanAmountMinIsInvalid.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.LoanAmountMinMaxIsInvalid:
                    returned = "SavingProductLoanAmountMinMaxIsInvalid.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.DepositFeesIsInvalid:
                    returned = "SavingProductDepositFeesIsInvalid.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.DepositFeesMinIsInvalid:
                    returned = "SavingProductDepositFeesMinIsInvalid.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.CloseFeesIsInvalid:
                    returned = "SavingProductCloseFeesIsInvalid.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.CloseFeesMinIsInvalid:
                    returned = "SavingProductCloseFeesMinIsInvalid.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.ManagementFeesIsInvalid:
                    returned = "SavingProductManagementFeesIsInvalid.Text";
                    break;

                case OpenCbsSavingProductExceptionEnum.ManagementFeesMinIsInvalid:
                    returned = "SavingProductManagementFeesMinIsInvalid.Text";
                    break;
                case OpenCbsSavingProductExceptionEnum.PeriodicityIsEmpty:
                    returned = "SavingProductPeriodicityIsEmpty.Text";
                    break;
                case OpenCbsSavingProductExceptionEnum.OverdraftFeesIsInvalid:
                    returned = "OverdraftFeesIsInvalid.Text";
                    break;
                case OpenCbsSavingProductExceptionEnum.AgioFeesIsInvalid:
                    returned = "AgioFeesIsInvalid.Text";
                    break;
                case OpenCbsSavingProductExceptionEnum.ChequeDepositIsInvalid:
                    returned = "ChequeDepositIsInvalid";
                    break;
                case OpenCbsSavingProductExceptionEnum.ChequeDepositFeesIsInvalid:
                    returned = "ChequeDepositFeesIsInvalid";
                    break;
                case OpenCbsSavingProductExceptionEnum.ReopenFeesIsInvalid:
                    returned = "ReopenFeesIsInvalid";
                    break;
                case OpenCbsSavingProductExceptionEnum.ReopenFeesMinIsInvalid:
                    returned = "ReopenFeesMinIsInvalid";
                    break;
                case OpenCbsSavingProductExceptionEnum.PostingFrequencyIsInvalid:
                    returned = "PostingFrequencyIsInvalid.Text";
                    break;
                case OpenCbsSavingProductExceptionEnum.PeriodicityIsNotSet:
                    returned = "PeriodicityIsNotSet.Text";
                    break;
            }
            return returned;
        }
	}

	[Serializable]
	public enum OpenCbsSavingProductExceptionEnum
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
