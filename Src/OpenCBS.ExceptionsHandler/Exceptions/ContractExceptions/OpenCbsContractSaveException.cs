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
	/// <summary>
	/// Summary description for OpenCbsContractSaveException.
	/// </summary>
    [Serializable]
    public class OpenCbsContractSaveException : OpenCbsContractException
	{
		private string _code;
		public OpenCbsContractSaveException(OpenCbsContractSaveExceptionEnum exceptionCode)
		{
			_code = FindException(exceptionCode);
		}

		public override string ToString()
		{
			return _code;
		}

        protected OpenCbsContractSaveException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _code = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", _code);
            base.GetObjectData(info, context);
        }

		private static string FindException(OpenCbsContractSaveExceptionEnum exceptionId)
		{
			string returned = String.Empty;
			switch(exceptionId)
			{
				case OpenCbsContractSaveExceptionEnum.ContractIsNull:
					returned = "ContractExceptionContractIsNull.Text";
					break;

				case OpenCbsContractSaveExceptionEnum.AmountIsNull:
					returned = "ContractExceptionAmountIsNull.Text";
					break;

				case OpenCbsContractSaveExceptionEnum.EntryFeesIsNull:
					returned = "ContractExceptionEntryFeesIsNull.Text";
					break;

				case OpenCbsContractSaveExceptionEnum.AnticipatedRepaymentPenaltiesIsNull:
					returned = "ContractExceptionAnticipatedRepaymentPenaltiesIsNull.Text";
					break;

				case OpenCbsContractSaveExceptionEnum.NonRepaymentPenaltiesIsNull:
					returned = "ContractExceptionNonRepaymentPenaltiesIsNull.Text";
					break;

				case OpenCbsContractSaveExceptionEnum.NumberOfInstallmentIsNull:
					returned = "ContractExceptionNumberOfInstallmentsIsNull.Text";
					break;

				case OpenCbsContractSaveExceptionEnum.GracePeriodIsNull:
					returned = "ContractExceptionGracePeriodIsNull.Text";
					break;

				case OpenCbsContractSaveExceptionEnum.InterestRateIsNull:
					returned = "ContractExceptionInterestRateIsNull.Text";
					break;

				case OpenCbsContractSaveExceptionEnum.InstallmentTypeIsNull:
					returned = "ContractExceptionInstallmentTypeIsNull.Text";
					break;

				case OpenCbsContractSaveExceptionEnum.FundingLineIsNull:
					returned = "ContractExceptionFundingLineIsNull.Text";
					break;

				case OpenCbsContractSaveExceptionEnum.LoanOfficerIsNull:
					returned = "ContractExceptionLoanOfficerIsNull.Text";
					break;

				case OpenCbsContractSaveExceptionEnum.BeneficiaryIsNull:
					returned = "ContractExceptionBeneficiaryIsNull.Text";
					break;

				case OpenCbsContractSaveExceptionEnum.BeneficiaryIsActive:
					returned = "ContractExceptionBeneficiaryIsActive.Text";
					break;

				case OpenCbsContractSaveExceptionEnum.DisburseIsNull:
					returned = "ContractExceptionDisburseDate.Text";
                    break;

                case OpenCbsContractSaveExceptionEnum.BeneficiaryIsBad:
                    returned = "ContractExceptionBeneficiaryIsBad.Text";
                    break;

                case OpenCbsContractSaveExceptionEnum.EventIsNull:                                                                                                                                                                                                                                                                                                                                                                                  
                    returned = "ContractExceptionEventIsNull.Text";
                    break;

                case OpenCbsContractSaveExceptionEnum.EventNotCancelable:
                    returned = "ContractExceptionEventNotCancelable.Text";
                    break;

                case OpenCbsContractSaveExceptionEnum.EventCommentIsEmpty:
                    returned = "ContractExceptionEventCommentIsEmpty.Text";
                    break;

                case OpenCbsContractSaveExceptionEnum.BeneficiaryIsAllowOneLoans:
                    returned = "ContractExceptionBeneficiaryIsAllowOneLoans.Text";
                    break;

                case OpenCbsContractSaveExceptionEnum.ProjectIsNull:
                    returned = "ContractExceptionProjectIsNull.Text";
                    break;

                case OpenCbsContractSaveExceptionEnum.CorporateIsNull:
                    returned = "ContractExceptionCorporateIsNull.Text";
                    break;

                case OpenCbsContractSaveExceptionEnum.CreditCommiteeCommentNotModified:
                    returned = "ContractExceptionCreditCommiteeCommentNotModified.Text";
                    break;

                case OpenCbsContractSaveExceptionEnum.StatusNotModified:
                    returned = "ContractExceptionStatusNotModified.Text";
                    break;
                case OpenCbsContractSaveExceptionEnum.CurrencyMisMatch:
                    returned = "CurrencyMisMatch.Text";
                    break;
                case OpenCbsContractSaveExceptionEnum.LoanShareAmountIsEmpty:
                    returned = "LoanShareAmountIsEmpty.Text";
                    break;
                case OpenCbsContractSaveExceptionEnum.LoanWasValidatedLaterThanDisbursed:
                    returned = "LoanWasValidatedLaterThanDisbursed.Text";
                    break;
                case OpenCbsContractSaveExceptionEnum.TrancheDate:
                    returned = "TrancheDateError.Text";
                    break;
                case OpenCbsContractSaveExceptionEnum.TrancheAmount:
                    returned = "TrancheAmountError.Text";
                    break;
                case OpenCbsContractSaveExceptionEnum.FieldIsNotUnique:
                    returned = "FieldIsNotUnique.Text";
                    break;
                case OpenCbsContractSaveExceptionEnum.FieldIsMandatory:
                    returned = "FieldIsMandatory.Text";
                    break;
                case OpenCbsContractSaveExceptionEnum.NumberFieldIsNotANumber:
                    returned = "NumberFieldIsNotANumber.Text";
                    break;
                case OpenCbsContractSaveExceptionEnum.FieldEmpty:
                    returned = "FieldEmpty.Text";
                    break;
                case OpenCbsContractSaveExceptionEnum.ZeroFee:
                    returned = "ZeroFee.Text";
                    break;
                case OpenCbsContractSaveExceptionEnum.WrongEvent:
                    returned = "WrongEvent.Text";
                    break;
                case OpenCbsContractSaveExceptionEnum.LoanHasNoCompulsorySavings:
                    returned = "LoanHasNoCompulsorySavingsError.Text";
                    break;
                case OpenCbsContractSaveExceptionEnum.OperationOutsideCurrentFiscalYear:
                    returned = "OperationOutsideCurrentFiscalYear.Text";
                    break;
                case OpenCbsContractSaveExceptionEnum.EconomicActivityNotSet:
                    returned = "EconomicActivityNotSet.Text";
                    break;
                case OpenCbsContractSaveExceptionEnum.TrancheMaturityError:
                    returned = "TrancheMaturityError.Text";
                    break;
                case OpenCbsContractSaveExceptionEnum.LoanIsFlatForTranche:
                    returned = "LoanIsFlatForTranche.Text";
                    break;
                case OpenCbsContractSaveExceptionEnum.CurrentInstallmentIsNotFullyRepaid:
                    returned = "CurrentInstallmentIsNotFullyRepaid.Text";
                    break;
                case OpenCbsContractSaveExceptionEnum.LoanAlreadyDisbursed:
                    returned = "LoanAlreadyDisbursed.Text";
                    break;
			}
			return returned;
		}
	}

    [Serializable]
    public enum OpenCbsContractSaveExceptionEnum
    {
        ContractIsNull,
        InterestRateIsNull,
        GracePeriodIsNull,
        NumberOfInstallmentIsNull,
        NonRepaymentPenaltiesIsNull,
        AnticipatedRepaymentPenaltiesIsNull,
        EntryFeesIsNull,
        AmountIsNull,
        InstallmentTypeIsNull,
        FundingLineIsNull,
        LoanOfficerIsNull,
        BeneficiaryIsNull,
        BeneficiaryIsActive,
        BeneficiaryIsAllowOneLoans,
        DisburseIsNull,
        BeneficiaryIsBad,
        EventIsNull,
        EventNotCancelable,
        EventCommentIsEmpty,
        ProjectIsNull,
        CorporateIsNull,
        CreditCommiteeCommentNotModified,
        StatusNotModified,
        CurrencyMisMatch,
        LoanShareAmountIsEmpty,
        LoanWasValidatedLaterThanDisbursed,
        TrancheDate,
        TrancheMaturityError,
        CurrentInstallmentIsNotFullyRepaid,
        LoanIsFlatForTranche,
        TrancheAmount,
        LoanHasNoCompulsorySavings,
        FieldIsNotUnique,
        FieldIsMandatory,
        FieldEmpty,
        NumberFieldIsNotANumber,
        ZeroFee,
        WrongEvent,
        OperationOutsideCurrentFiscalYear,
        EconomicActivityNotSet,
        LoanAlreadyDisbursed
    }
}
