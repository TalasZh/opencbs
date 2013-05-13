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
	/// Summary description for OpenCbsPackageSaveException.
	/// </summary>
    [Serializable]
    public class OpenCbsPackageSaveException : OpenCbsPackageException
	{
		private string code;
		public OpenCbsPackageSaveException(OpenCbsPackageSaveExceptionEnum exceptionCode)
		{
			code = FindException(exceptionCode);
		}

        protected OpenCbsPackageSaveException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            code = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", code);
            base.GetObjectData(info, context);
        }

		private string FindException(OpenCbsPackageSaveExceptionEnum exceptionId)
		{
			string returned = String.Empty;
			switch(exceptionId)
			{
				case OpenCbsPackageSaveExceptionEnum.NameIsNull:
					returned = "PackageExceptionNameIsNull.Text";
					break;

				case OpenCbsPackageSaveExceptionEnum.InstallmentTypeIsNull:
					returned = "PackageExceptionInstallmentTypeIsNull.Text";
					break;

				case OpenCbsPackageSaveExceptionEnum.InstallmentTypeIsBad:
					returned = "PackageExceptionInstallmentTypeIsBad.Text";
					break;

				case OpenCbsPackageSaveExceptionEnum.InterestRateGroupBadlyInformed:
					returned = "PackageExceptionInterestRateGroupBadlyInformed.Text";
					break;

				case OpenCbsPackageSaveExceptionEnum.GracePeriodGroupBadlyInformed:
					returned = "PackageExceptionGracePeriodGroupBadlyInformed.Text";
					break;

				case OpenCbsPackageSaveExceptionEnum.NonRepaymentPenaltiesBadlyInformed:
					returned = "PackageExceptionNonRepaymentPenaltiesBadlyInformed.Text";
					break;

				case OpenCbsPackageSaveExceptionEnum.AnticipatedRepaymentPenaltiesBadlyInformed:
					returned = "PackageExceptionAnticipatedTotalRepaymentPenaltiesBadlyInformed.Text";
					break;

				case OpenCbsPackageSaveExceptionEnum.EntryFeesBadlyInformed:
					returned = "PackageExceptionEntryFeesBadlyInformed.Text";
					break;

				case OpenCbsPackageSaveExceptionEnum.NumberOfInstallmentBadlyInformed:
					returned = "PackageExceptionNumberOfInstallmentBadlyInformed.Text";
					break;

				case OpenCbsPackageSaveExceptionEnum.AmountBadlyInformed:
					returned = "PackageExceptionAmountBadlyInformed.Text";
					break;

				case OpenCbsPackageSaveExceptionEnum.ExoticProductIsBad:
					returned = "PackageExceptionExoticProductIsBad.Text";
					break;

				case OpenCbsPackageSaveExceptionEnum.ExoticProductIsNull:
					returned = "PackageExceptionExoticProductIsNull.Text";
					break;

				case OpenCbsPackageSaveExceptionEnum.ExoticInstallmentIsNull:
					returned = "PackageExceptionExoticInstallmentIsNull.Text";
					break;

				case OpenCbsPackageSaveExceptionEnum.ExoticInstallmentIsBad:
					returned = "PackageExceptionExoticInstallmentIsBad.Text";
					break;

				case OpenCbsPackageSaveExceptionEnum.ExoticProductContainsNoInstallment:
					returned = "PackageExceptionExoticProductContainsNoInstallment.Text";
					break;

                case OpenCbsPackageSaveExceptionEnum.ExoticProductNameAlreadyExist:
					returned = "PackageExceptionExoticProductNameAlreadyExist.Text";
                    break;

                case OpenCbsPackageSaveExceptionEnum.LoanCycleNameAlreadyExists:
                    returned = "PackageExceptionAmountCycleStockNameAlreadyExist.Text";
                    break;

				case OpenCbsPackageSaveExceptionEnum.NameAlreadyExist:
					returned = "PackageExceptionNameAlreadyExist.Text";
                    break;

                case OpenCbsPackageSaveExceptionEnum.AmountCycleStockIsNull:
                    returned = "PackageExceptionAmountCycleStockIsNull.Text";
                    break;

                case OpenCbsPackageSaveExceptionEnum.AmountCycleStockIsBad:
                    returned = "PackageExceptionAmountCycleStockIsBad.Text";
                    break;

                case OpenCbsPackageSaveExceptionEnum.InstallmentTypeNameAlreadyExist:
                    returned = "PackageExceptionInstallmentTypeNameAlreadyExist.Text";
                    break;

                case OpenCbsPackageSaveExceptionEnum.InstallmentTypeValuesAlreadyExist:
                    returned = "PackageExceptionInstallmentTypeValuesAlreadyExist.Text";
                    break;

                case OpenCbsPackageSaveExceptionEnum.InstallmentTypeValuesAreUsed:
                    returned = "PackageExceptionInstallmentTypeValuesAreUsed.Text";
                    break;

                case OpenCbsPackageSaveExceptionEnum.ExoticProductSumInIncorrect:
                    returned = "PackageExceptionExoticProductInIncorrect.Text";
                    break;

                case OpenCbsPackageSaveExceptionEnum.GuarantedAmountBadlyInformed:
                    returned = "PackageExceptionGuarantedAmountBadlyInformed.Text";
                    break;

                case OpenCbsPackageSaveExceptionEnum.GuarantedFeesBadlyInformed:
                    returned = "PackageExceptionGuarantedFeesBadlyInformed.Text";
                    break;

                case OpenCbsPackageSaveExceptionEnum.CodeIsEmpty:
                    returned = "PackageExceptionProductCodeIsEmpty.Text";
                    break;

                case OpenCbsPackageSaveExceptionEnum.CurrencyIsEmpty:
                    returned = "CurrencyIsEmpty.Text";
                    break;

                case OpenCbsPackageSaveExceptionEnum.CurrencyMisMatch:
                    returned = "CurrencyMisMatch.Text";
                    break;
                case OpenCbsPackageSaveExceptionEnum.ClientTypeIsEmpty:
			        returned = "ClientTypeIsEmpty.Text";
                    break;
                case OpenCbsPackageSaveExceptionEnum.CompulsorySavingSettingsEmpty:
                    returned = "CompulsorySavingSettingsEmpty.Text";
                    break;
                case OpenCbsPackageSaveExceptionEnum.CycleParametersAreNotFilled:
			        returned = "CycleParametersAreNotFilled.Text";
			        break;
                case  OpenCbsPackageSaveExceptionEnum.CycleParametersHaveBeenFilledIncorrectly:
			        returned = "CycleParametersAreWrong.Text";
                    break;
                case OpenCbsPackageSaveExceptionEnum.AmountCanNotBeZero:
			        returned = "AmountCanNotBeZero.Text";
                    break;
                case OpenCbsPackageSaveExceptionEnum.MaturityCanNotBeZero:
			        returned = "MaturityCanNotBeZero.Text";
                    break;
                case OpenCbsPackageSaveExceptionEnum.InsuranceBadlyFilled:
			        returned = "InsuranceBadlyFilled.Text";
                    break;
                case OpenCbsPackageSaveExceptionEnum.LOCAmountHaveBeenFilledIncorrectly:
			        returned = "LOCAmountHaveBeenFilledIncorrectly.Text";
                    break;
                case OpenCbsPackageSaveExceptionEnum.LOCMaturityHaveBeenFilledIncorrectly:
			        returned = "LOCMaturityHaveBeenFilledIncorrectly.Text";
                    break;
                case OpenCbsPackageSaveExceptionEnum.LOCFieldsAreNotFilled:
			        returned = "LOCFieldsAreNotFilled.Text";
                    break;
                case OpenCbsPackageSaveExceptionEnum.CycleAlreadyExists:
                    returned = "AddEntryFeeCycleError.Text";
			        break;
                case OpenCbsPackageSaveExceptionEnum.GracePeriodOfLateFeesIsNotFilled:
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
	public enum OpenCbsPackageSaveExceptionEnum
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
