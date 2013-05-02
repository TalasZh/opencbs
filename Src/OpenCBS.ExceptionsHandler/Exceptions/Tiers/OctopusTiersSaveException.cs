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

namespace OpenCBS.ExceptionsHandler
{
	/// <summary>
	/// Summary description for OctopusTiersSaveException.
	/// </summary>
	[Serializable]
	public class OctopusTiersSaveException : OctopusTiersException
	{
		private string code;

        protected OctopusTiersSaveException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            code = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", code);
            base.GetObjectData(info, context);
        }

      	public OctopusTiersSaveException(OctopusTiersSaveExceptionEnum exceptionCode)
		{
			code = FindException(exceptionCode);
		}

        public OctopusTiersSaveException(OctopusTiersSaveExceptionEnum exceptionCode, List<string> options)
        {
            AdditionalOptions = options;
            code = FindException(exceptionCode);
        }

		public override string ToString()
		{
			return code;
		}

		private string FindException(OctopusTiersSaveExceptionEnum exceptionId)
		{
			string returned = String.Empty;
			switch(exceptionId)
			{
				case OctopusTiersSaveExceptionEnum.TiersIsNull:
					returned = "OTE1.Text";
					break;

				case OctopusTiersSaveExceptionEnum.EconomicActivityIsNull:
					returned = "OTE2.Text";
					break;

				case OctopusTiersSaveExceptionEnum.DistrictIsNull:
					returned = "OTE3.Text";
					break;

				case OctopusTiersSaveExceptionEnum.FirstNameIsNull:
					returned = "OTE4.Text";
					break;

				case OctopusTiersSaveExceptionEnum.SexIsNull:
					returned = "OTE5.Text";
					break;

				case OctopusTiersSaveExceptionEnum.IdentificationDataIsNull:
					returned = "OTE6.Text";
					break;

				case OctopusTiersSaveExceptionEnum.LastNameIsEmpty:
					returned = "OTE7.Text";
					break;

				case OctopusTiersSaveExceptionEnum.CityIsNull:
					returned = "OTE8.Text";
					break;

				case OctopusTiersSaveExceptionEnum.SecondaryDistrictIsNull:
					returned = "OTE10.Text";
					break;

				case OctopusTiersSaveExceptionEnum.SecondaryCityIsNull:
					returned = "OTE11.Text";
					break;

				case OctopusTiersSaveExceptionEnum.SecondaryCommentsIsNull:
					returned = "OTE12.Text";
					break;

				case OctopusTiersSaveExceptionEnum.DistrictIsBad:
					returned = "OTE13.Text";
					break;

				case OctopusTiersSaveExceptionEnum.SecondaryDistrictIsBad:
					returned = "OTE14.Text";
					break;

				case OctopusTiersSaveExceptionEnum.TiersIsGroup:
					returned = "OTE15.Text";
					break;

				case OctopusTiersSaveExceptionEnum.PersonAlreadyInThisGroup:
					returned = "OTE16.Text";
					break;

				case OctopusTiersSaveExceptionEnum.PersonIsActive:
					returned = "OTE17.Text";
					break;

				case OctopusTiersSaveExceptionEnum.PersonIsALeader:
					returned = "OTE18.Text";
					break;

				case OctopusTiersSaveExceptionEnum.NameIsEmpty:
					returned = "OTE19.Text";
					break;

				case OctopusTiersSaveExceptionEnum.LeaderIsEmpty:
					returned = "OTE20.Text";
					break;

				case OctopusTiersSaveExceptionEnum.NbOfDependantsIsBadlyInformed:
					returned = "OTE21.Text";
					break;

				case OctopusTiersSaveExceptionEnum.NbOfChildrensIsBadlyInformed:
					returned = "OTE22.Text";
					break;

				case OctopusTiersSaveExceptionEnum.NbOfChidrensWithBasicEducationisBadlyInformed:
					returned = "OTE23.Text";
					break;

				case OctopusTiersSaveExceptionEnum.ExperienceIsBadlyInformed:
					returned = "OTE24.Text";
					break;

				case OctopusTiersSaveExceptionEnum.NbOfPeopleWorkingWithinIsBadlyInformed:
					returned = "OTE25.Text";
					break;

				case OctopusTiersSaveExceptionEnum.OtherOrganizationAmountIsBadlyInformed:
					returned = "OTE26.Text";
					break;

				case OctopusTiersSaveExceptionEnum.OtherOrganizationDebtsIsBadlyInformed:
					returned = "OTE27.Text";
					break;

				case OctopusTiersSaveExceptionEnum.HouseSizeIsBadlyInformed:
					returned = "OTE28.Text";
					break;

				case OctopusTiersSaveExceptionEnum.HouseTimeLivingInIsBadlyInformed:
					returned = "OTE29.Text";
					break;

				case OctopusTiersSaveExceptionEnum.LandPlotSizeIsBadlyInformed:
					returned = "OTE30.Text";
					break;

				case OctopusTiersSaveExceptionEnum.LivestockNumberIsBadlyInformed:
					returned = "OTE31.Text";
					break;

				case OctopusTiersSaveExceptionEnum.MonthlyIncomeIsBadlyInformed:
					returned = "OTE32.Text";
					break;

				case OctopusTiersSaveExceptionEnum.MonthlyExpenditureIsBadlyInformed:
					returned = "OTE33.Text";
					break;

				case OctopusTiersSaveExceptionEnum.NoEnoughPersonsInThisGroup:
					returned = "OTE34.Text";
					break;

                case OctopusTiersSaveExceptionEnum.IdentificationDataAlreadyUsed:
                    returned = "OTE35.Text";
                    break;

                case OctopusTiersSaveExceptionEnum.NameAlreadyUsedInDistrict:
                    returned = "OTE36.Text";
                    break;

                case OctopusTiersSaveExceptionEnum.CommentsNeedFullIfBadClient:
                    returned = "OTE37.Text";
                    break;

                case OctopusTiersSaveExceptionEnum.LoanCycleIsEmpty:
                    returned = "OTE38.Text";
                    break;

                case OctopusTiersSaveExceptionEnum.IdentificationDataDoesntMatch:
                    returned = "OTE40.Text";
                    break;

                case OctopusTiersSaveExceptionEnum.LoanOfficerIsEmpty:
			        returned = "OTE41.Text";
			        break;

                case OctopusTiersSaveExceptionEnum.WrongIdPattern:
                    returned = "WrongIDPattern.Text";
                    break;
                case OctopusTiersSaveExceptionEnum.BirthDateIsWrong:
			        returned = "BirthDateIsWrong.Text";
			        break;

                case OctopusTiersSaveExceptionEnum.PersonalBankBicCodeIsWrong:
                    returned =  "PersonalBankBicCodeIsWrong.Text";
                    break;

                case OctopusTiersSaveExceptionEnum.BusinessBankBicCodeIsWrong:
                    returned = "BusinessBankBicCodeIsWrong.Text";
                    break;

                case OctopusTiersSaveExceptionEnum.PersonalBankIbanIsWrong:
                    returned = "PersonalBankIbanIsWrong.Text";
                    break;

                case OctopusTiersSaveExceptionEnum.BusinessBankIbanIsWrong:
                    returned = "BusinessBankIbanIsWrong.Text";
                    break;
                case OctopusTiersSaveExceptionEnum.PersonIsInTheGroup:
                    returned = "PersonIsInTheGroup.Text";
                    break;

                case OctopusTiersSaveExceptionEnum.BranchIsEmpty:
			        returned = "BranchIsEmpty";
			        break;

                case OctopusTiersSaveExceptionEnum.TooMuchPersonsInThisGroup:
                    returned = "TooMuchPersonsInThisGroup.Text";
                    break;

                case OctopusTiersSaveExceptionEnum.AgeIsNotInRange:
			        returned = "AgeIsNotInRange.Text";
			        break;

                case OctopusTiersSaveExceptionEnum.GuarantorMaxLoansCoveredExceed:
			        returned = "GuarantorMaxLoansCoveredExceed.Text";
                    break;
			}
			return returned;
		}
	}

    [Serializable]
    public enum OctopusTiersSaveExceptionEnum
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
