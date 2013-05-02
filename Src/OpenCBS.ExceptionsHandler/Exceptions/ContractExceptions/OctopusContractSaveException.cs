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

namespace OpenCBS.ExceptionsHandler
{
	/// <summary>
	/// Summary description for OctopusContractSaveException.
	/// </summary>
    [Serializable]
    public class OctopusContractSaveException : OctopusContractException
	{
		private string _code;
		public OctopusContractSaveException(OctopusContractSaveExceptionEnum exceptionCode)
		{
			_code = FindException(exceptionCode);
		}

		public override string ToString()
		{
			return _code;
		}

        protected OctopusContractSaveException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _code = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", _code);
            base.GetObjectData(info, context);
        }

		private static string FindException(OctopusContractSaveExceptionEnum exceptionId)
		{
			string returned = String.Empty;
			switch(exceptionId)
			{
				case OctopusContractSaveExceptionEnum.ContractIsNull:
					returned = "ContractExceptionContractIsNull.Text";
					break;

				case OctopusContractSaveExceptionEnum.AmountIsNull:
					returned = "ContractExceptionAmountIsNull.Text";
					break;

				case OctopusContractSaveExceptionEnum.EntryFeesIsNull:
					returned = "ContractExceptionEntryFeesIsNull.Text";
					break;

				case OctopusContractSaveExceptionEnum.AnticipatedRepaymentPenaltiesIsNull:
					returned = "ContractExceptionAnticipatedRepaymentPenaltiesIsNull.Text";
					break;

				case OctopusContractSaveExceptionEnum.NonRepaymentPenaltiesIsNull:
					returned = "ContractExceptionNonRepaymentPenaltiesIsNull.Text";
					break;

				case OctopusContractSaveExceptionEnum.NumberOfInstallmentIsNull:
					returned = "ContractExceptionNumberOfInstallmentsIsNull.Text";
					break;

				case OctopusContractSaveExceptionEnum.GracePeriodIsNull:
					returned = "ContractExceptionGracePeriodIsNull.Text";
					break;

				case OctopusContractSaveExceptionEnum.InterestRateIsNull:
					returned = "ContractExceptionInterestRateIsNull.Text";
					break;

				case OctopusContractSaveExceptionEnum.InstallmentTypeIsNull:
					returned = "ContractExceptionInstallmentTypeIsNull.Text";
					break;

				case OctopusContractSaveExceptionEnum.FundingLineIsNull:
					returned = "ContractExceptionFundingLineIsNull.Text";
					break;

				case OctopusContractSaveExceptionEnum.LoanOfficerIsNull:
					returned = "ContractExceptionLoanOfficerIsNull.Text";
					break;

				case OctopusContractSaveExceptionEnum.BeneficiaryIsNull:
					returned = "ContractExceptionBeneficiaryIsNull.Text";
					break;

				case OctopusContractSaveExceptionEnum.BeneficiaryIsActive:
					returned = "ContractExceptionBeneficiaryIsActive.Text";
					break;

				case OctopusContractSaveExceptionEnum.DisburseIsNull:
					returned = "ContractExceptionDisburseDate.Text";
                    break;

                case OctopusContractSaveExceptionEnum.BeneficiaryIsBad:
                    returned = "ContractExceptionBeneficiaryIsBad.Text";
                    break;

                case OctopusContractSaveExceptionEnum.EventIsNull:                                                                                                                                                                                                                                                                                                                                                                                  
                    returned = "ContractExceptionEventIsNull.Text";
                    break;

                case OctopusContractSaveExceptionEnum.EventNotCancelable:
                    returned = "ContractExceptionEventNotCancelable.Text";
                    break;

                case OctopusContractSaveExceptionEnum.EventCommentIsEmpty:
                    returned = "ContractExceptionEventCommentIsEmpty.Text";
                    break;

                case OctopusContractSaveExceptionEnum.BeneficiaryIsAllowOneLoans:
                    returned = "ContractExceptionBeneficiaryIsAllowOneLoans.Text";
                    break;

                case OctopusContractSaveExceptionEnum.ProjectIsNull:
                    returned = "ContractExceptionProjectIsNull.Text";
                    break;

                case OctopusContractSaveExceptionEnum.CorporateIsNull:
                    returned = "ContractExceptionCorporateIsNull.Text";
                    break;

                case OctopusContractSaveExceptionEnum.CreditCommiteeCommentNotModified:
                    returned = "ContractExceptionCreditCommiteeCommentNotModified.Text";
                    break;

                case OctopusContractSaveExceptionEnum.StatusNotModified:
                    returned = "ContractExceptionStatusNotModified.Text";
                    break;
                case OctopusContractSaveExceptionEnum.CurrencyMisMatch:
                    returned = "CurrencyMisMatch.Text";
                    break;
                case OctopusContractSaveExceptionEnum.LoanShareAmountIsEmpty:
                    returned = "LoanShareAmountIsEmpty.Text";
                    break;
                case OctopusContractSaveExceptionEnum.LoanWasValidatedLaterThanDisbursed:
                    returned = "LoanWasValidatedLaterThanDisbursed.Text";
                    break;
                case OctopusContractSaveExceptionEnum.TrancheDate:
                    returned = "TrancheDateError.Text";
                    break;
                case OctopusContractSaveExceptionEnum.TrancheAmount:
                    returned = "TrancheAmountError.Text";
                    break;
                case OctopusContractSaveExceptionEnum.FieldIsNotUnique:
                    returned = "FieldIsNotUnique.Text";
                    break;
                case OctopusContractSaveExceptionEnum.FieldIsMandatory:
                    returned = "FieldIsMandatory.Text";
                    break;
                case OctopusContractSaveExceptionEnum.NumberFieldIsNotANumber:
                    returned = "NumberFieldIsNotANumber.Text";
                    break;
                case OctopusContractSaveExceptionEnum.FieldEmpty:
                    returned = "FieldEmpty.Text";
                    break;
                case OctopusContractSaveExceptionEnum.ZeroFee:
                    returned = "ZeroFee.Text";
                    break;
                case OctopusContractSaveExceptionEnum.WrongEvent:
                    returned = "WrongEvent.Text";
                    break;
                case OctopusContractSaveExceptionEnum.LoanHasNoCompulsorySavings:
                    returned = "LoanHasNoCompulsorySavingsError.Text";
                    break;
                case OctopusContractSaveExceptionEnum.OperationOutsideCurrentFiscalYear:
                    returned = "OperationOutsideCurrentFiscalYear.Text";
                    break;
                case OctopusContractSaveExceptionEnum.EconomicActivityNotSet:
                    returned = "EconomicActivityNotSet.Text";
                    break;
                case OctopusContractSaveExceptionEnum.TrancheMaturityError:
                    returned = "TrancheMaturityError.Text";
                    break;
                case OctopusContractSaveExceptionEnum.LoanIsFlatForTranche:
                    returned = "LoanIsFlatForTranche.Text";
                    break;
                case OctopusContractSaveExceptionEnum.CurrentInstallmentIsNotFullyRepaid:
                    returned = "CurrentInstallmentIsNotFullyRepaid.Text";
                    break;
                case OctopusContractSaveExceptionEnum.LoanAlreadyDisbursed:
                    returned = "LoanAlreadyDisbursed.Text";
                    break;
			}
			return returned;
		}
	}

    [Serializable]
    public enum OctopusContractSaveExceptionEnum
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
