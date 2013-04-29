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
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Octopus.ExceptionsHandler.Exceptions.SavingExceptions
{
	/// <summary>
	/// Summary description for OctopusSavingException.
	/// </summary>
	[Serializable]
	public class OctopusSavingException : OctopusException
	{
		private OctopusSavingExceptionEnum _code;
        private readonly string _message;
		public OctopusSavingExceptionEnum Code{get{ return _code;}}
        
		public OctopusSavingException(OctopusSavingExceptionEnum exceptionCode)
		{
            _code = exceptionCode;
            _message = _FindException(exceptionCode);
		}

        public OctopusSavingException(OctopusSavingExceptionEnum exceptionCode, List<string> additionalOptions)
        {
            _code = exceptionCode;
            _message = _FindException(exceptionCode);
            AdditionalOptions = additionalOptions;
        }

		public override string ToString()
		{
			return _message;
		}

		protected OctopusSavingException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			_code = (OctopusSavingExceptionEnum)info.GetInt32("Code");
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Code", (int)_code);
			base.GetObjectData(info, context);
		}

        private static string _FindException(OctopusSavingExceptionEnum exceptionId)
        {
            string returned = String.Empty;
            switch (exceptionId)
            {
                case OctopusSavingExceptionEnum.BalanceIsInvalid:
                    returned = "SavingBalanceIsInvalid.Text";
                    break;

                case OctopusSavingExceptionEnum.DepositAmountIsInvalid:
                    returned = "SavingDepositAmountIsInvalid.Text";
                    break;

                case OctopusSavingExceptionEnum.DepositDateIsInvalid:
                    returned = "SavingDepositDateIsInvalid.Text";
                    break;

                case OctopusSavingExceptionEnum.InitialAmountIsInvalid:
                    returned = "SavingInitialAmountIsInvalid.Text";
                    break;

                case OctopusSavingExceptionEnum.InterestRateIsInvalid:
                    returned = "SavingInterestRateIsInvalid.Text";
                    break;

                case OctopusSavingExceptionEnum.ProductIsInvalid:
                    returned = "SavingProductIsInvalid.Text";
                    break;

                case OctopusSavingExceptionEnum.SavingContractIsNull:
                    returned = "SavingContractIsNull.Text";
                    break;

                case OctopusSavingExceptionEnum.WithdrawalDateIsInvalid:
                    returned = "SavingWithdrawalDateIsInvalid.Text";
                    break;

                case OctopusSavingExceptionEnum.WithdrawAmountIsInvalid:
                    returned = "SavingWithdrawAmountIsInvalid.Text";
                    break;

                case OctopusSavingExceptionEnum.EntryFeesIsInvalid:
                    returned = "SavingEntryFeesIsInvalid.Text";
                    break;

                case OctopusSavingExceptionEnum.ContractNotAtMaturity:
                    returned = "SavingContractNotAtMaturity.Text";
                    break;

                case OctopusSavingExceptionEnum.TransferAmountIsInvalid:
                    returned = "SavingTransferAmountIsInvalid.Text";
                    break;

                case OctopusSavingExceptionEnum.SavingsContractForTransferIdenticals:
                    returned = "SavingContractForTransferIdenticals.Text";
                    break;
            
                case OctopusSavingExceptionEnum.CreditTransferAccountInvalid:
                    returned = "SavingCreditTransferAccountInvalid.Text";
                    break;

                case OctopusSavingExceptionEnum.WithdrawFeesIsInvalid:
                    returned = "SavingWithdrawFeesIsInvalid.Text";
                    break;

                case OctopusSavingExceptionEnum.SavingsContractForTransferNotSameCurrncy:
                    returned = "SavingContractForTransferNotSameCurrncy.Text";
                    break;

                case OctopusSavingExceptionEnum.RolloverIsInvalid:
                    returned = "SavingContractRolloverIsInvalid.Text";
                    break;

                case OctopusSavingExceptionEnum.TransferAccountIsInvalid:
                    returned = "SavingContractRolloverTransferAccountIsInvalid.Text";
                    break;

                case OctopusSavingExceptionEnum.TransferAccountIsClosed:
                    returned = "SavingContractRolloverTransferAccountIsClosed.Text";
                    break;

                case OctopusSavingExceptionEnum.TransferAccountIsSavingDepositOrCompulsorySavings:
                    returned = "SavingContractRolloverTransferAccountIsSavingDepositOrCompulsorySavings.Text";
                    break;

                case OctopusSavingExceptionEnum.TransferAccountHasNotSameClient:
                    returned = "SavingContractRolloverTransferAccountHasNotSameClient.Text";
                    break;

                case OctopusSavingExceptionEnum.LoanIsInvalid:
                    returned = "SavingContractLoanIsInvalid.Text";
                    break;

                case OctopusSavingExceptionEnum.LoanIsClosed:
                    returned = "SavingContractLoanIsClosed.Text";
                    break;

                case OctopusSavingExceptionEnum.LoanHasNotSameClient:
                    returned = "SavingContractLoanHasNotSameClient.Text";
                    break;

                case OctopusSavingExceptionEnum.LoanHasNotSameCurrency:
                    returned = "SavingContractLoanHasNotSameCurrency.Text";
                    break;

                case OctopusSavingExceptionEnum.TransferFeesIsInvalid:
                    returned = "SavingContractTransferFeesIsInvalid.Text";
                    break;

                case OctopusSavingExceptionEnum.CompulsorySavingsLinkedToLoan:
                    returned = "SavingContractCompulsorySavingsLinkedToLoan.Text";
                    break;

                case OctopusSavingExceptionEnum.LoanDoesNotAcceptSavings:
                    returned = "SavingContractLoanDoesNotAcceptSavings.Text";
                    break;

                case OctopusSavingExceptionEnum.NoCompulsorySavings:
                    returned = "SavingContractNoCompulsorySavings.Text";
                    break;

                case OctopusSavingExceptionEnum.AmountOnCompulsorySavingsIsInvalid:
                    returned = "SavingContractAmountOnCompulsorySavingsIsInvalid.Text";
                    break;

                case OctopusSavingExceptionEnum.DepositFeesIsInvalid:
                    returned = "SavingContractDepositFeesIsInvalid.Text";
                    break;

                case OctopusSavingExceptionEnum.CloseFeesIsInvalid:
                    returned = "SavingContractCloseFeesIsInvalid.Text";
                    break;

                case OctopusSavingExceptionEnum.ManagementFeesIsInvalid:
                    returned = "SavingContractManagementFeesIsInvalid.Text";
                    break;
                case OctopusSavingExceptionEnum.AgioFeesIsInvalid:
                    returned = "SavingContractAgioFeesIsInvalid.Text";
                    break;
                case OctopusSavingExceptionEnum.SavingsEventCannotBeCanceled:
                    returned = "SavingsEventCannotBeCanceled.Text";
                    break;
                case OctopusSavingExceptionEnum.SavingsEventCommentIsEmpty:
                    returned = "SavingsEventCommentIsEmpty.Text";
                    break;
                case OctopusSavingExceptionEnum.BalanceOnCurrentSavingAccount:
                    returned = "BalanceOnCurrentSavingAccount";
                    break;
                case OctopusSavingExceptionEnum.BalanceOnCurrentSavingAccountForTransfer:
                    returned = "BalanceOnCurrentSavingAccountForTransfer";
                    break;

                case OctopusSavingExceptionEnum.CompulsorySavingsContractIsNotActive:
                    returned = "CompulsorySavingsContractIsNotActive.Text";
                    break;
                case  OctopusSavingExceptionEnum.TransferAccountHasWrongCurrency:
                    returned = "TransferAccountHasWrongCurrency.Text";
                    break;
                case OctopusSavingExceptionEnum.TransactionInvalid:
                    returned = "SelectBeforeConf.Text";
                    break;
            }

            return returned;
        }
	}

    [Serializable]
    public enum OctopusSavingExceptionEnum
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
