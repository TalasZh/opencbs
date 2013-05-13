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
using NUnit.Framework;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Clients;
using OpenCBS.CoreDomain.EconomicActivities;
using OpenCBS.Enums;

namespace OpenCBS.Test.CoreDomain
{
	[TestFixture]
	public class TestPerson
	{
		private Person person;

		[SetUp]
		public void SetUp()
		{
			person = new Person();
		}

		[Test]
		public void IdCorrectlySetAndRetrieved()
		{
			person.Id = 1;
			Assert.AreEqual(1,person.Id);
		}

		[Test]
		public void TypeCorrectlySetAndRetrieved()
		{
            person.Type = OClientTypes.Group;
            Assert.AreEqual(OClientTypes.Group, person.Type);
		}

		[Test]
		public void TestIfScoringCorrectlySetAndRetrieved()
		{
			person.Scoring = 13.5;
			Assert.AreEqual(13.5,person.Scoring.Value);
		}

		[Test]
		public void TestIfLoanCycleCorrectlySetAndRetrieved()
		{
			person.LoanCycle = 1;
			Assert.AreEqual(1,person.LoanCycle);
		}

		[Test]
		public void TestIfActiveIsCorrectlySetAndRetrieved()
		{
			person.Active = true;
			Assert.IsTrue(person.Active);
		}

        [Test]
        public void FatherCorrectlySetAndRetrieved()
        {
            person.FatherName = "John";
            Assert.AreEqual("John",person.FatherName);
        }

        [Test]
        public void MotherCorrectlySetAndRetrieved()
        {
            person.MotherName = "Marry";
            Assert.AreEqual("Marry", person.MotherName);
        }

		[Test]
		public void TestIfOtherOrgNameIsCorrectlySetAndRetrieved()
		{
			person.OtherOrgName = "Planet Finance";
			Assert.AreEqual("Planet Finance" , person.OtherOrgName);
		}

		[Test]
		public void TestIfOtherOrgAmountIsCorrectlySetAndRetrieved()
		{
			person.OtherOrgAmount = 200.5m;
			Assert.AreEqual(200.5m,person.OtherOrgAmount.Value);
		}

		[Test]
		public void TestIfOtherOrgDebtsIsCorrectlySetAndRetrieved()
		{
			person.OtherOrgDebts = 200.5m;
			Assert.AreEqual(200.5m,person.OtherOrgDebts.Value);
		}

		[Test]
		public void TestIfAddressIsCorrectlySetAndRetrieved()
		{
			person.Address = "50 avenue des Champs Elys�es";
			Assert.AreEqual("50 avenue des Champs Elys�es",person.Address);
		}

		[Test]
		public void TestIfCityIsCorrectlySetAndRetrieved()
		{
			person.City = "Paris";
			Assert.AreEqual("Paris",person.City);
		}

		[Test]
		public void TestIfDistrictIsCorrectlySetAndRetrieved()
		{
			District district = new District(1,"Qath",new Province(1,"Khatlon"));
			person.District = district;

			Assert.AreEqual(1,person.District.Id);
			Assert.AreEqual("Qath",person.District.Name);
			Assert.AreEqual(1,person.District.Province.Id);
			Assert.AreEqual("Khatlon",person.District.Province.Name);
		}
		
		[Test]
		public void TestIfSecondaryAddressIsCorrectlySetAndRetrieved()
		{
			person.SecondaryAddress = "50 avenue des Champs Elys�es";
			Assert.AreEqual("50 avenue des Champs Elys�es",person.SecondaryAddress);
		}

		[Test]
		public void TestIfSecondaryCityIsCorrectlySetAndRetrieved()
		{
			person.SecondaryCity = "Paris";
			Assert.AreEqual("Paris",person.SecondaryCity);
		}

		[Test]
		public void TestIfSecondaryDistrictIsCorrectlySetAndRetrieved()
		{
			District district = new District(1,"tress",new Province(1,"Sugh"));
			person.SecondaryDistrict = district;
			Assert.AreEqual(district.Id,person.SecondaryDistrict.Id);
			Assert.AreEqual(district.Name,person.SecondaryDistrict.Name);
			Assert.AreEqual(1,person.SecondaryDistrict.Province.Id);
			Assert.AreEqual("Sugh",person.SecondaryDistrict.Province.Name);
		}

		[Test]
		public void TestIfSecondaryAddressIsEmpty()
		{
			Person person = new Person();
			District district = new District(1,"tress",new Province(1,"Sugh"));

			person.SecondaryDistrict = null;
			person.SecondaryCity = null;
			Assert.IsTrue(person.SecondaryAddressIsEmpty);

			person.SecondaryDistrict = district;
			Assert.IsFalse(person.SecondaryAddressIsEmpty);

			person.SecondaryCity = "Paris";
			Assert.IsFalse(person.SecondaryAddressIsEmpty);

			person.SecondaryDistrict = null;
			person.SecondaryCity = null;
			Assert.IsTrue(person.SecondaryAddressIsEmpty);
		}

		[Test]
		public void TestIdSecondaryAddressPartiallyFilled()
		{
			Person person = new Person();
			District district = new District(1,"tress",new Province(1,"Sugh"));

			person.SecondaryDistrict = null;
			person.SecondaryCity = null;
			Assert.IsFalse(person.SecondaryAddressPartiallyFilled);

			person.SecondaryDistrict = district;
			Assert.IsTrue(person.SecondaryAddressPartiallyFilled);

			person.SecondaryCity = "city";
			Assert.IsFalse(person.SecondaryAddressPartiallyFilled);

			person.SecondaryDistrict = null;
			person.SecondaryCity = null;
			Assert.IsFalse(person.SecondaryAddressPartiallyFilled);

			person.SecondaryCity = "paris";
			Assert.IsTrue(person.SecondaryAddressPartiallyFilled);
		}

		[Test]
		public void TestIfSexIsCorrectlySetAndRetrieved()
		{
			person.Sex = 'M';
			Assert.AreEqual('M',person.Sex);
		}
		
		[Test]
		public void TestIfIdentificationDataIsCorrectlySetAndRetrieved()
		{
			person.IdentificationData = "1234AFDK5";
			Assert.AreEqual("1234AFDK5",person.IdentificationData);
		}

		[Test]
		public void TestIfFirstNameIsCorrectlySetAndRetrieved()
		{
			person.FirstName = "Nicolas";
			Assert.AreEqual("Nicolas",person.FirstName);
		}

		[Test]
		public void TestIfLastNameIsCorrectlySetAndRetrieved()
		{
			person.LastName = "BARON";
			Assert.AreEqual("BARON",person.LastName);
		}

		[Test]
		public void TestIfDateOfBirthIsCorrectlySetAndRetrieved()
		{
			person.DateOfBirth = new DateTime(1983,6,4);
			Assert.AreEqual(new DateTime(1983,6,4),person.DateOfBirth.Value);
		}

        [Test]
        public void TestIfDateOfFirstContactIsCorrectlySetAndRetrieved()
        {
            person.FirstContact = new DateTime(2009, 12, 25);
            Assert.AreEqual(new DateTime(2009, 12, 25), person.FirstContact.Value);
        }

        [Test]
        public void TestIfDateOfFirstAppointmentIsCorrectlySetAndRetrieved()
        {
            person.FirstAppointment = new DateTime(2010, 04, 1);
            Assert.AreEqual(new DateTime(2010, 04, 1), person.FirstAppointment.Value);
        }

		[Test]
		public void TestIfHouseholdHeadIsCorrectlySetAndRetrieved()
		{
			person.HouseHoldHead = true;
			Assert.IsTrue(person.HouseHoldHead);
		}

		[Test]
		public void TestIfNbOfDependentsIsCorrectlySetAndRetrieved()
		{
			person.NbOfDependents = 12;
			Assert.AreEqual(12,person.NbOfDependents.Value);
		}

		[Test]
		public void TestIfNbOfChildrenIsCorrectlySetAndRetrieved()
		{
			person.NbOfChildren = 12;
			Assert.AreEqual(12,person.NbOfChildren.Value);
		}

		[Test]
		public void TestIfNbOfChildrenBasicEducationIsCorrectlySetAndRetrieved()
		{
			person.ChildrenBasicEducation= 10;
			Assert.AreEqual(10,person.ChildrenBasicEducation.Value);
		}

		[Test]
		public void TestIfNbOfiveStockNumberLIsCorrectlySetAndRetrieved()
		{
			person.LivestockNumber = 50;
			Assert.AreEqual(50,person.LivestockNumber.Value);
		}

		[Test]
		public void TestIfLivestockTypeIsCorrectlySetAndRetrieved()
		{
			person.LivestockType = "Cows";
			Assert.AreEqual("Cows",person.LivestockType);
		}

		[Test]
		public void TestIfLandplotSizeCorrectlySetAndRetrieved()
		{
			person.LandplotSize = 100.5;
			Assert.AreEqual(100.5,person.LandplotSize.Value);
		}

		[Test]
		public void TestIfHomeSizeCorrectlySetAndRetrieved()
		{
			person.HomeSize = 100.5;
			Assert.AreEqual(100.5,person.HomeSize.Value);
		}

		[Test]
		public void TestIfHomeTimeLivingInCorrectlySetAndRetrieved()
		{
			person.HomeTimeLivingIn = 5;
			Assert.AreEqual(5,person.HomeTimeLivingIn.Value);
		}

		[Test]
		public void TestIfCapitalOthersEquipmentsIsCorrectlySetAndRetrieved()
		{
			person.CapitalOthersEquipments = "1 tractor + 1 TV";
			Assert.AreEqual("1 tractor + 1 TV",person.CapitalOthersEquipments);
		}

		[Test]
		public void TestIfDomainOfApplicationIsCorrectlySetAndRetrieved()
		{
			EconomicActivity agriculture = new EconomicActivity(1,"Agriculture",new EconomicActivity(),false);
			person.Activity = agriculture;
			Assert.AreEqual(1,person.Activity.Id);
			Assert.AreEqual("Agriculture",person.Activity.Name);
			Assert.IsNotNull(person.Activity.Parent);
			Assert.IsFalse(person.Activity.Deleted);
		}
		
		[Test]
		public void TestIfExperienceIsCorrectlySetAndRetrieved()
		{
			person.Experience = 4;
			Assert.AreEqual(4,person.Experience.Value);
		}

		[Test]
		public void TestIfNbOfPeopleIsCorrectlySetAndRetrieved()
		{
			person.NbOfPeople = 4;
			Assert.AreEqual(4,person.NbOfPeople.Value);
		}

		[Test]
		public void TestIfPersonHasAHouse()
		{
			Person newPerson = new Person();
			newPerson.HomeSize = null;
			newPerson.HomeTimeLivingIn = null;
			Assert.IsFalse(person.HasHome());
			
			newPerson.HomeSize = 123;
			Assert.IsTrue(newPerson.HasHome());

			newPerson.HomeTimeLivingIn = 2;
			Assert.IsTrue(newPerson.HasHome());
			
			newPerson.HomeSize = null;
			Assert.IsTrue(newPerson.HasHome());
		}

		[Test]
		public void TestIfPersonHasALandPlot()
		{
			Person newPerson = new Person();
			newPerson.LandplotSize = null;
			Assert.IsFalse(newPerson.HasLandPlot());

			newPerson.LandplotSize = 123;
			Assert.IsTrue(newPerson.HasLandPlot());
		}

		[Test]
		public void TestIfPersonHasLivestock()
		{
			Person newPerson = new Person();
			newPerson.LivestockNumber = null;
			newPerson.LivestockType = null;
			Assert.IsFalse(newPerson.HasLivestock());

			newPerson.LivestockNumber = 12;
			Assert.IsTrue(newPerson.HasLivestock());

			newPerson.LivestockType = "cows";
			Assert.IsTrue(newPerson.HasLivestock());

			newPerson.LivestockNumber = null;;
			Assert.IsTrue(newPerson.HasLivestock());
		}

		[Test]
		public void TestIfPersonIsMemberOfAnOtherOrganization()
		{
			Person newPerson = new Person();
			newPerson.OtherOrgAmount = null;
			newPerson.OtherOrgDebts = null;
			newPerson.OtherOrgName = null;
			Assert.IsFalse(newPerson.HasOtherOrganization());

			newPerson.OtherOrgAmount = 123;
			Assert.IsTrue(newPerson.HasOtherOrganization());

			newPerson.OtherOrgDebts = 1233;
			Assert.IsTrue(newPerson.HasOtherOrganization());

			newPerson.OtherOrgName = "planet finance";
			Assert.IsTrue(newPerson.HasOtherOrganization());

			newPerson.OtherOrgAmount = null;
			Assert.IsTrue(newPerson.HasOtherOrganization());

			newPerson.OtherOrgDebts = null;
			Assert.IsTrue(newPerson.HasOtherOrganization());

			newPerson.OtherOrgName = null;
			Assert.IsFalse(newPerson.HasOtherOrganization());
		}

        [Test]
        public void Projects_Add_Get()
        {
            Person pers = new Person {Id = 1, LoanCycle = 3};

            pers.AddProject();

            Assert.AreEqual(1, pers.Projects.Count);
            Assert.AreEqual("3/1", pers.Projects[0].ClientCode);
        }

	    [Test]
	    public void TestNumberOfProjectsEqualsZeroWhenFirstCreated()
	    {
            Person test = new Person {Id = 1};
            Assert.AreEqual(0, test.NbOfProjects);
	    }

        [Test]
        public void TestNumberOfProjectsIsNonzeroWhenProjectAdded()
        {
            Person test = new Person {Id = 1};
            test.AddProject();
            Assert.AreEqual(1, test.NbOfProjects);
        }
	}
}
