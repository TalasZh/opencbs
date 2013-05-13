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
	/// Summary description for OpenCbsTiersSaveException.
	/// </summary>
	[Serializable]
	public class OpenCbsTiersSaveException : OpenCbsTiersException
	{
		private string code;

        protected OpenCbsTiersSaveException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            code = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", code);
            base.GetObjectData(info, context);
        }

      	public OpenCbsTiersSaveException(OpenCbsTiersSaveExceptionEnum exceptionCode)
		{
			code = FindException(exceptionCode);
		}

        public OpenCbsTiersSaveException(OpenCbsTiersSaveExceptionEnum exceptionCode, List<string> options)
        {
            AdditionalOptions = options;
            code = FindException(exceptionCode);
        }

		public override string ToString()
		{
			return code;
		}

		private string FindException(OpenCbsTiersSaveExceptionEnum exceptionId)
		{
			string returned = String.Empty;
			switch(exceptionId)
			{
				case OpenCbsTiersSaveExceptionEnum.TiersIsNull:
					returned = "OTE1.Text";
					break;

				case OpenCbsTiersSaveExceptionEnum.EconomicActivityIsNull:
					returned = "OTE2.Text";
					break;

				case OpenCbsTiersSaveExceptionEnum.DistrictIsNull:
					returned = "OTE3.Text";
					break;

				case OpenCbsTiersSaveExceptionEnum.FirstNameIsNull:
					returned = "OTE4.Text";
					break;

				case OpenCbsTiersSaveExceptionEnum.SexIsNull:
					returned = "OTE5.Text";
					break;

				case OpenCbsTiersSaveExceptionEnum.IdentificationDataIsNull:
					returned = "OTE6.Text";
					break;

				case OpenCbsTiersSaveExceptionEnum.LastNameIsEmpty:
					returned = "OTE7.Text";
					break;

				case OpenCbsTiersSaveExceptionEnum.CityIsNull:
					returned = "OTE8.Text";
					break;

				case OpenCbsTiersSaveExceptionEnum.SecondaryDistrictIsNull:
					returned = "OTE10.Text";
					break;

				case OpenCbsTiersSaveExceptionEnum.SecondaryCityIsNull:
					returned = "OTE11.Text";
					break;

				case OpenCbsTiersSaveExceptionEnum.SecondaryCommentsIsNull:
					returned = "OTE12.Text";
					break;

				case OpenCbsTiersSaveExceptionEnum.DistrictIsBad:
					returned = "OTE13.Text";
					break;

				case OpenCbsTiersSaveExceptionEnum.SecondaryDistrictIsBad:
					returned = "OTE14.Text";
					break;

				case OpenCbsTiersSaveExceptionEnum.TiersIsGroup:
					returned = "OTE15.Text";
					break;

				case OpenCbsTiersSaveExceptionEnum.PersonAlreadyInThisGroup:
					returned = "OTE16.Text";
					break;

				case OpenCbsTiersSaveExceptionEnum.PersonIsActive:
					returned = "OTE17.Text";
					break;

				case OpenCbsTiersSaveExceptionEnum.PersonIsALeader:
					returned = "OTE18.Text";
					break;

				case OpenCbsTiersSaveExceptionEnum.NameIsEmpty:
					returned = "OTE19.Text";
					break;

				case OpenCbsTiersSaveExceptionEnum.LeaderIsEmpty:
					returned = "OTE20.Text";
					break;

				case OpenCbsTiersSaveExceptionEnum.NbOfDependantsIsBadlyInformed:
					returned = "OTE21.Text";
					break;

				case OpenCbsTiersSaveExceptionEnum.NbOfChildrensIsBadlyInformed:
					returned = "OTE22.Text";
					break;

				case OpenCbsTiersSaveExceptionEnum.NbOfChidrensWithBasicEducationisBadlyInformed:
					returned = "OTE23.Text";
					break;

				case OpenCbsTiersSaveExceptionEnum.ExperienceIsBadlyInformed:
					returned = "OTE24.Text";
					break;

				case OpenCbsTiersSaveExceptionEnum.NbOfPeopleWorkingWithinIsBadlyInformed:
					returned = "OTE25.Text";
					break;

				case OpenCbsTiersSaveExceptionEnum.OtherOrganizationAmountIsBadlyInformed:
					returned = "OTE26.Text";
					break;

				case OpenCbsTiersSaveExceptionEnum.OtherOrganizationDebtsIsBadlyInformed:
					returned = "OTE27.Text";
					break;

				case OpenCbsTiersSaveExceptionEnum.HouseSizeIsBadlyInformed:
					returned = "OTE28.Text";
					break;

				case OpenCbsTiersSaveExceptionEnum.HouseTimeLivingInIsBadlyInformed:
					returned = "OTE29.Text";
					break;

				case OpenCbsTiersSaveExceptionEnum.LandPlotSizeIsBadlyInformed:
					returned = "OTE30.Text";
					break;

				case OpenCbsTiersSaveExceptionEnum.LivestockNumberIsBadlyInformed:
					returned = "OTE31.Text";
					break;

				case OpenCbsTiersSaveExceptionEnum.MonthlyIncomeIsBadlyInformed:
					returned = "OTE32.Text";
					break;

				case OpenCbsTiersSaveExceptionEnum.MonthlyExpenditureIsBadlyInformed:
					returned = "OTE33.Text";
					break;

				case OpenCbsTiersSaveExceptionEnum.NoEnoughPersonsInThisGroup:
					returned = "OTE34.Text";
					break;

                case OpenCbsTiersSaveExceptionEnum.IdentificationDataAlreadyUsed:
                    returned = "OTE35.Text";
                    break;

                case OpenCbsTiersSaveExceptionEnum.NameAlreadyUsedInDistrict:
                    returned = "OTE36.Text";
                    break;

                case OpenCbsTiersSaveExceptionEnum.CommentsNeedFullIfBadClient:
                    returned = "OTE37.Text";
                    break;

                case OpenCbsTiersSaveExceptionEnum.LoanCycleIsEmpty:
                    returned = "OTE38.Text";
                    break;

                case OpenCbsTiersSaveExceptionEnum.IdentificationDataDoesntMatch:
                    returned = "OTE40.Text";
                    break;

                case OpenCbsTiersSaveExceptionEnum.LoanOfficerIsEmpty:
			        returned = "OTE41.Text";
			        break;

                case OpenCbsTiersSaveExceptionEnum.WrongIdPattern:
                    returned = "WrongIDPattern.Text";
                    break;
                case OpenCbsTiersSaveExceptionEnum.BirthDateIsWrong:
			        returned = "BirthDateIsWrong.Text";
			        break;

                case OpenCbsTiersSaveExceptionEnum.PersonalBankBicCodeIsWrong:
                    returned =  "PersonalBankBicCodeIsWrong.Text";
                    break;

                case OpenCbsTiersSaveExceptionEnum.BusinessBankBicCodeIsWrong:
                    returned = "BusinessBankBicCodeIsWrong.Text";
                    break;

                case OpenCbsTiersSaveExceptionEnum.PersonalBankIbanIsWrong:
                    returned = "PersonalBankIbanIsWrong.Text";
                    break;

                case OpenCbsTiersSaveExceptionEnum.BusinessBankIbanIsWrong:
                    returned = "BusinessBankIbanIsWrong.Text";
                    break;
                case OpenCbsTiersSaveExceptionEnum.PersonIsInTheGroup:
                    returned = "PersonIsInTheGroup.Text";
                    break;

                case OpenCbsTiersSaveExceptionEnum.BranchIsEmpty:
			        returned = "BranchIsEmpty";
			        break;

                case OpenCbsTiersSaveExceptionEnum.TooMuchPersonsInThisGroup:
                    returned = "TooMuchPersonsInThisGroup.Text";
                    break;

                case OpenCbsTiersSaveExceptionEnum.AgeIsNotInRange:
			        returned = "AgeIsNotInRange.Text";
			        break;

                case OpenCbsTiersSaveExceptionEnum.GuarantorMaxLoansCoveredExceed:
			        returned = "GuarantorMaxLoansCoveredExceed.Text";
                    break;
			}
			return returned;
		}
	}

    [Serializable]
    public enum OpenCbsTiersSaveExceptionEnum
    {
        TiersIsNull,
        CommentsNeedFullIfBadClient,
        TiersIsGroup,
        TiersIsPerson,
        TiersIsCorporate,
        EconomicActivityIsNull,
        CityIsNull,
        DistrictIsNull,
        DistrictIsBad,
        FirstNameIsNull,
        SexIsNull,
        NameIsEmpty,
        IdentificationDataIsNull,
        LastNameIsEmpty,
        SecondaryDistrictIsNull,
        SecondaryDistrictIsBad,
        SecondaryCityIsNull,
        SecondaryCommentsIsNull,
        PersonAlreadyInThisGroup,
        PersonIsActive,
        PersonIsALeader,
        LeaderIsEmpty,
        NbOfDependantsIsBadlyInformed,
        NbOfChildrensIsBadlyInformed,
        NbOfChidrensWithBasicEducationisBadlyInformed,
        ExperienceIsBadlyInformed,
        NbOfPeopleWorkingWithinIsBadlyInformed,
        OtherOrganizationAmountIsBadlyInformed,
        OtherOrganizationDebtsIsBadlyInformed,
        HouseSizeIsBadlyInformed,
        HouseTimeLivingInIsBadlyInformed,
        LandPlotSizeIsBadlyInformed,
        LivestockNumberIsBadlyInformed,
        MonthlyIncomeIsBadlyInformed,
        MonthlyExpenditureIsBadlyInformed,
        NoEnoughPersonsInThisGroup,
        TooMuchPersonsInThisGroup,
        IdentificationDataAlreadyUsed,
        NameAlreadyUsedInDistrict,
        LoanCycleIsEmpty,
        IdentificationDataDoesntMatch,
        LoanOfficerIsEmpty,
        WrongIdPattern,
        BirthDateIsWrong,
        PersonalBankBicCodeIsWrong,
        BusinessBankBicCodeIsWrong,
        PersonalBankIbanIsWrong,
        BusinessBankIbanIsWrong,
        PersonIsInTheGroup,
        BranchIsEmpty,
        AgeIsNotInRange,
        GuarantorMaxLoansCoveredExceed
    }
    
}
