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
	/// Summary description for OctopusPackageSaveException.
	/// </summary>
    [Serializable]
    public class OctopusPackageSaveException : OctopusPackageException
	{
		private string code;
		public OctopusPackageSaveException(OctopusPackageSaveExceptionEnum exceptionCode)
		{
			code = FindException(exceptionCode);
		}

        protected OctopusPackageSaveException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            code = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", code);
            base.GetObjectData(info, context);
        }

		private string FindException(OctopusPackageSaveExceptionEnum exceptionId)
		{
			string returned = String.Empty;
			switch(exceptionId)
			{
				case OctopusPackageSaveExceptionEnum.NameIsNull:
					returned = "PackageExceptionNameIsNull.Text";
					break;

				case OctopusPackageSaveExceptionEnum.InstallmentTypeIsNull:
					returned = "PackageExceptionInstallmentTypeIsNull.Text";
					break;

				case OctopusPackageSaveExceptionEnum.InstallmentTypeIsBad:
					returned = "PackageExceptionInstallmentTypeIsBad.Text";
					break;

				case OctopusPackageSaveExceptionEnum.InterestRateGroupBadlyInformed:
					returned = "PackageExceptionInterestRateGroupBadlyInformed.Text";
					break;

				case OctopusPackageSaveExceptionEnum.GracePeriodGroupBadlyInformed:
					returned = "PackageExceptionGracePeriodGroupBadlyInformed.Text";
					break;

				case OctopusPackageSaveExceptionEnum.NonRepaymentPenaltiesBadlyInformed:
					returned = "PackageExceptionNonRepaymentPenaltiesBadlyInformed.Text";
					break;

				case OctopusPackageSaveExceptionEnum.AnticipatedRepaymentPenaltiesBadlyInformed:
					returned = "PackageExceptionAnticipatedTotalRepaymentPenaltiesBadlyInformed.Text";
					break;

				case OctopusPackageSaveExceptionEnum.EntryFeesBadlyInformed:
					returned = "PackageExceptionEntryFeesBadlyInformed.Text";
					break;

				case OctopusPackageSaveExceptionEnum.NumberOfInstallmentBadlyInformed:
					returned = "PackageExceptionNumberOfInstallmentBadlyInformed.Text";
					break;

				case OctopusPackageSaveExceptionEnum.AmountBadlyInformed:
					returned = "PackageExceptionAmountBadlyInformed.Text";
					break;

				case OctopusPackageSaveExceptionEnum.ExoticProductIsBad:
					returned = "PackageExceptionExoticProductIsBad.Text";
					break;

				case OctopusPackageSaveExceptionEnum.ExoticProductIsNull:
					returned = "PackageExceptionExoticProductIsNull.Text";
					break;

				case OctopusPackageSaveExceptionEnum.ExoticInstallmentIsNull:
					returned = "PackageExceptionExoticInstallmentIsNull.Text";
					break;

				case OctopusPackageSaveExceptionEnum.ExoticInstallmentIsBad:
					returned = "PackageExceptionExoticInstallmentIsBad.Text";
					break;

				case OctopusPackageSaveExceptionEnum.ExoticProductContainsNoInstallment:
					returned = "PackageExceptionExoticProductContainsNoInstallment.Text";
					break;

                case OctopusPackageSaveExceptionEnum.ExoticProductNameAlreadyExist:
					returned = "PackageExceptionExoticProductNameAlreadyExist.Text";
                    break;

                case OctopusPackageSaveExceptionEnum.LoanCycleNameAlreadyExists:
                    returned = "PackageExceptionAmountCycleStockNameAlreadyExist.Text";
                    break;

				case OctopusPackageSaveExceptionEnum.NameAlreadyExist:
					returned = "PackageExceptionNameAlreadyExist.Text";
                    break;

                case OctopusPackageSaveExceptionEnum.AmountCycleStockIsNull:
                    returned = "PackageExceptionAmountCycleStockIsNull.Text";
                    break;

                case OctopusPackageSaveExceptionEnum.AmountCycleStockIsBad:
                    returned = "PackageExceptionAmountCycleStockIsBad.Text";
                    break;

                case OctopusPackageSaveExceptionEnum.InstallmentTypeNameAlreadyExist:
                    returned = "PackageExceptionInstallmentTypeNameAlreadyExist.Text";
                    break;

                case OctopusPackageSaveExceptionEnum.InstallmentTypeValuesAlreadyExist:
                    returned = "PackageExceptionInstallmentTypeValuesAlreadyExist.Text";
                    break;

                case OctopusPackageSaveExceptionEnum.InstallmentTypeValuesAreUsed:
                    returned = "PackageExceptionInstallmentTypeValuesAreUsed.Text";
                    break;

                case OctopusPackageSaveExceptionEnum.ExoticProductSumInIncorrect:
                    returned = "PackageExceptionExoticProductInIncorrect.Text";
                    break;

                case OctopusPackageSaveExceptionEnum.GuarantedAmountBadlyInformed:
                    returned = "PackageExceptionGuarantedAmountBadlyInformed.Text";
                    break;

                case OctopusPackageSaveExceptionEnum.GuarantedFeesBadlyInformed:
                    returned = "PackageExceptionGuarantedFeesBadlyInformed.Text";
                    break;

                case OctopusPackageSaveExceptionEnum.CodeIsEmpty:
                    returned = "PackageExceptionProductCodeIsEmpty.Text";
                    break;

                case OctopusPackageSaveExceptionEnum.CurrencyIsEmpty:
                    returned = "CurrencyIsEmpty.Text";
                    break;

                case OctopusPackageSaveExceptionEnum.CurrencyMisMatch:
                    returned = "CurrencyMisMatch.Text";
                    break;
                case OctopusPackageSaveExceptionEnum.ClientTypeIsEmpty:
			        returned = "ClientTypeIsEmpty.Text";
                    break;
                case OctopusPackageSaveExceptionEnum.CompulsorySavingSettingsEmpty:
                    returned = "CompulsorySavingSettingsEmpty.Text";
                    break;
                case OctopusPackageSaveExceptionEnum.CycleParametersAreNotFilled:
			        returned = "CycleParametersAreNotFilled.Text";
			        break;
                case  OctopusPackageSaveExceptionEnum.CycleParametersHaveBeenFilledIncorrectly:
			        returned = "CycleParametersAreWrong.Text";
                    break;
                case OctopusPackageSaveExceptionEnum.AmountCanNotBeZero:
			        returned = "AmountCanNotBeZero.Text";
                    break;
                case OctopusPackageSaveExceptionEnum.MaturityCanNotBeZero:
			        returned = "MaturityCanNotBeZero.Text";
                    break;
                case OctopusPackageSaveExceptionEnum.InsuranceBadlyFilled:
			        returned = "InsuranceBadlyFilled.Text";
                    break;
                case OctopusPackageSaveExceptionEnum.LOCAmountHaveBeenFilledIncorrectly:
			        returned = "LOCAmountHaveBeenFilledIncorrectly.Text";
                    break;
                case OctopusPackageSaveExceptionEnum.LOCMaturityHaveBeenFilledIncorrectly:
			        returned = "LOCMaturityHaveBeenFilledIncorrectly.Text";
                    break;
                case OctopusPackageSaveExceptionEnum.LOCFieldsAreNotFilled:
			        returned = "LOCFieldsAreNotFilled.Text";
                    break;
                case OctopusPackageSaveExceptionEnum.CycleAlreadyExists:
                    returned = "AddEntryFeeCycleError.Text";
			        break;
                case OctopusPackageSaveExceptionEnum.GracePeriodOfLateFeesIsNotFilled:
			        returned = "GracePeriodOfLateFeesIsNotFilled.Text";
                    break;
			}
			return returned;
		}

		public override string ToString()
		{
			return code;
		}
	}

    [Serializable]
	public enum OctopusPackageSaveExceptionEnum
	{
		NameIsNull,
		NameAlreadyExist,
		InstallmentTypeIsNull,
		InstallmentTypeIsBad,
        InstallmentTypeNameAlreadyExist,
        InstallmentTypeValuesAlreadyExist,
        InstallmentTypeValuesAreUsed,
		InterestRateGroupBadlyInformed,
		GracePeriodGroupBadlyInformed,
		NonRepaymentPenaltiesBadlyInformed,
		AnticipatedRepaymentPenaltiesBadlyInformed,
		EntryFeesBadlyInformed,
		NumberOfInstallmentBadlyInformed,
		AmountBadlyInformed,
		ExoticProductIsNull,
		ExoticProductIsBad,
        ExoticProductSumInIncorrect,
		ExoticInstallmentIsNull,
		ExoticInstallmentIsBad,
		ExoticProductContainsNoInstallment,
		ExoticProductNameAlreadyExist,
        LoanCycleNameAlreadyExists,
	    AmountCycleStockIsNull,
        AmountCycleStockIsBad,
        GuarantedFeesBadlyInformed,
        GuarantedAmountBadlyInformed, 
        CodeIsEmpty,
        CurrencyIsEmpty,
        CurrencyMisMatch,
        ClientTypeIsEmpty,
        CompulsorySavingSettingsEmpty,
        CycleParametersAreNotFilled,
        CycleParametersHaveBeenFilledIncorrectly,
        AmountCanNotBeZero,
        MaturityCanNotBeZero,
        InsuranceBadlyFilled,
        LOCFieldsAreNotFilled,
        LOCMaturityHaveBeenFilledIncorrectly,
        LOCAmountHaveBeenFilledIncorrectly,
        CycleAlreadyExists,
        GracePeriodOfLateFeesIsNotFilled
	}
}
