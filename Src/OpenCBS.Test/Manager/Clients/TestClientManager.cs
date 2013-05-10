// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Clients;
using OpenCBS.CoreDomain.EconomicActivities;
using OpenCBS.CoreDomain.SearchResult;
using OpenCBS.DatabaseConnection;
using OpenCBS.Enums;
using OpenCBS.Manager;
using OpenCBS.Manager.Clients;
using OpenCBS.Shared;
using OpenCBS.Shared.Settings;

namespace OpenCBS.Test.Manager
{
	/// <summary>
	/// Class containing tests ensuring the validity of ClientManagement methods.
	/// </summary>
	[TestFixture]
	public class TestClientManager
	{
		private ClientManager _cltManagement;
	    private LocationsManager _locations;
	    private ProjectManager _projectManager;
        private readonly DataHelper _addDataForTesting = new DataHelper();

		private Person _person;
        private Group _group;
		private EconomicActivity _agriculture;
		private District _district;
	    private Branch _branch;

		[TestFixtureSetUp]
		public void TestFixtureSetUp()
		{
            TechnicalSettings.UseOnlineMode = false;

			_DeleteDatas();
            _cltManagement = new ClientManager(DataUtil.TESTDB);
            _locations = new LocationsManager(DataUtil.TESTDB);
			_agriculture = _addDataForTesting.AddDomainOfApplicationAgriculture();
			_district = _addDataForTesting.AddDistrictIntoDatabase();
            _projectManager = new ProjectManager(DataUtil.TESTDB);
            ApplicationSettings settings = ApplicationSettings.GetInstance("");
            settings.UpdateParameter(OGeneralSettings.NAME_FORMAT, "L U");
        }

		[TestFixtureTearDown]
		public void TestFixtureTearDown()
		{
			_DeleteDatas();
		}

		private void _DeleteDatas()
		{
            _addDataForTesting.DeleteInstallments();
            _addDataForTesting.DeleteCreditContract();
            _addDataForTesting.DeletedProject();
            _addDataForTesting.DeleteTiers();
            _addDataForTesting.DeleteInstallmentTypes();
            _addDataForTesting.DeletePackage();
            _addDataForTesting.DeleteAllUser();
		}
		
		[SetUp]
		public void SetUp()
		{
			_DeleteDatas();
            _cltManagement = new ClientManager(DataUtil.TESTDB);
			_agriculture = _addDataForTesting.AddDomainOfApplicationAgriculture();
			_district = _addDataForTesting.AddDistrictIntoDatabase();
            _addDataForTesting.AddBranchIntoDatabase();

		    _branch = new Branch {Id = 1, Name = "Default"};
			_person = new Person();
			_person.Active = true;
		    _person.BadClient = false;
		    _person.LoanCycle = 1;
			_person.City = "Dushambe";
			_person.District = _district;
            
			_person.FirstName = "Nicolas";
			_person.LastName = "BARON";
		    _person.Sex = 'M';
			_person.IdentificationData = "123ARK3VC";
            _person.HouseHoldHead = true;
            _person.StudyLevel = "1";
            _person.SSNumber = "3333";
            _person.CAFNumber = "2424";
            _person.HousingSituation = "test";
		    _person.UnemploymentMonths = 1;
            _person.Nationality = "France";
            _person.BirthPlace = "Nancy";
            _person.Email = "man@yahoo.fr";
            _person.SecondaryEmail = "man@yahoo2.fr";
            _person.HomeType = "Maison";
            _person.ZipCode = "12345";
            _person.SecondaryZipCode = "123458";
            _person.SecondaryHomeType = "Maison";
            _person.Handicapped = true;
		    _person.Status = OClientStatus.Inactive;
            _person.FollowUpComment = "Comment follow Up";
            _person.FirstContact = new DateTime(2009, 12, 25);
            _person.FirstAppointment = new DateTime(2010, 04, 01);
		    _person.Sponsor1 = "coucou";
            _person.Sponsor1Comment = "coucou2";
		    _person.Branch = _branch;

            _group = new Group();
			_group.Active = true;
		    _group.BadClient = false;
			_group.City = "Dushambe";
		    _group.LoanCycle = 2;
			_group.District = _district;
			_group.Name = "SCG";
            _group.ZipCode = "12345";
            _group.SecondaryZipCode = "123458";
            _group.Email = "man@yahoo.fr";
            _group.SecondaryEmail = "man@yahoo2.fr";
            _group.HomeType = "Maison";
            _group.SecondaryHomeType = "Maison";
            _group.Status = OClientStatus.Active;
            _group.FollowUpComment = "Follow Up Comment Group";
            _group.Sponsor2 = "coucou";
            _group.Sponsor2Comment = "coucou2";
		    _group.Branch = _branch;
		}
		
		private void PrepareNullAuthorizedValues()
		{
			_person.Scoring = 0.6;
			_person.OtherOrgName = "Planet Finance";
			_person.OtherOrgAmount = 500m;
			_person.OtherOrgDebts = 200.43m;
			_person.Address = "Kaboul Street";
            _person.Email = "man@yahoo.fr";
            _person.SecondaryEmail = "man@yahoo2.fr";
		    _person.HomeType = "Maison";
            _person.SecondaryHomeType = "Maison";
			_person.SecondaryDistrict = _district;
			_person.SecondaryCity = "Kathlon";
			_person.SecondaryAddress = "32 rue des acacias";
			_person.DateOfBirth = new DateTime(1983,4,6);
			_person.NbOfDependents = 1;
			_person.NbOfChildren = 1;
			_person.ChildrenBasicEducation = 2;
			_person.LivestockNumber = 4;
			_person.LivestockType = "Cows";
		    _person.StudyLevel = "1";
		    _person.SSNumber = "3333";
		    _person.CAFNumber = "2424";
		    _person.HousingSituation = "test";
		    _person.Nationality = "France";
		    _person.BirthPlace = "Nancy";

			_person.LandplotSize = 100;
			_person.HomeSize = 100;
			_person.HomeTimeLivingIn = 2;
			_person.CapitalOthersEquipments = "Tractor";
			_person.Activity = _agriculture;
			_person.Experience = 3;
			_person.NbOfPeople = 5;

			_group.Scoring = 0.6;
			_group.OtherOrgName = "Planet Finance";
			_group.OtherOrgAmount = 500m;
			_group.OtherOrgDebts = 200.43m;
			_group.Address = "Kaboul Street";
			_group.SecondaryDistrict = _district;
            _group.SecondaryCity = "Kathlon";
            _group.Email = "man@yahoo.fr";
            _group.SecondaryEmail = "man@yahoo2.fr";
            _group.HomeType = "Maison";
            _group.SecondaryHomeType = "Maison";
			_group.SecondaryAddress = "32 rue des acacias";
			_group.EstablishmentDate = new DateTime(2006,7,14);
			_group.FollowUpComment = "oh my God";
		}
		
		[Test]
		public void TestAddPersonAndSelectPersonByIdNonNullAuthorizedValues()
		{
            Project project = new Project("TEST");
            project.Name = "TEST";
            project.Code = "NotSet";
            project.Aim = "NotSet";
            project.BeginDate = TimeProvider.Today;
            _person.AddProject(project);
            _person.Id = _cltManagement.AddPerson(_person);
            project.Id = _projectManager.Add(project, _person.Id);

			Person selectedPerson = _cltManagement.SelectPersonById(_person.Id);
		    _AssertPerson(_person, selectedPerson);
		}

        private static void _AssertPerson(Person pExpectedPerson, Person pActualPerson)
        {
            Assert.AreEqual(pExpectedPerson.Id, pActualPerson.Id);
            Assert.AreEqual(pExpectedPerson.Type, pActualPerson.Type);
            
            if(!string.IsNullOrEmpty(pExpectedPerson.City))
                Assert.AreEqual(pExpectedPerson.City.ToUpper(), pActualPerson.City);
            
            Assert.AreEqual(pExpectedPerson.District.Name, pActualPerson.District.Name);
            Assert.AreEqual(pExpectedPerson.FirstName.ToLower(), pActualPerson.FirstName.ToLower());
            Assert.AreEqual(pExpectedPerson.LastName.ToUpper(), pActualPerson.LastName.ToUpper());
            Assert.AreEqual(pExpectedPerson.Sex, pActualPerson.Sex);
            Assert.AreEqual(pExpectedPerson.LoanCycle, pActualPerson.LoanCycle);
            Assert.AreEqual(pExpectedPerson.IdentificationData, pActualPerson.IdentificationData);
            Assert.AreEqual(pExpectedPerson.CashReceiptIn, pActualPerson.CashReceiptIn);
            Assert.AreEqual(pExpectedPerson.CashReceiptOut, pActualPerson.CashReceiptOut);
            Assert.AreEqual(pExpectedPerson.HouseHoldHead, pActualPerson.HouseHoldHead);
            Assert.AreEqual(pExpectedPerson.BadClient, pActualPerson.BadClient);
            Assert.AreEqual(pExpectedPerson.Projects.Count, pActualPerson.Projects.Count);
            Assert.AreEqual(pExpectedPerson.Scoring, pActualPerson.Scoring);
            Assert.AreEqual(pExpectedPerson.MotherName, pActualPerson.MotherName);
            Assert.AreEqual(pExpectedPerson.OtherOrgName,pActualPerson.OtherOrgName);
            if (pExpectedPerson.OtherOrgAmount.HasValue)
            Assert.AreEqual(pExpectedPerson.OtherOrgAmount.Value,pActualPerson.OtherOrgAmount.Value);
            if (pExpectedPerson.OtherOrgDebts.HasValue)
            Assert.AreEqual(pExpectedPerson.OtherOrgDebts.Value,pActualPerson.OtherOrgDebts.Value);
            Assert.AreEqual(pExpectedPerson.Address,pActualPerson.Address);
            if (pExpectedPerson.SecondaryDistrict != null)
                Assert.AreEqual(pExpectedPerson.SecondaryDistrict.Name, pActualPerson.SecondaryDistrict.Name);
            else
                Assert.AreEqual(pExpectedPerson.SecondaryDistrict, pActualPerson.SecondaryDistrict);
            
            if (!string.IsNullOrEmpty(pExpectedPerson.SecondaryCity))
                Assert.AreEqual(pExpectedPerson.SecondaryCity.ToUpper(), pActualPerson.SecondaryCity);
            
            Assert.AreEqual(pExpectedPerson.SecondaryAddress,pActualPerson.SecondaryAddress);
            Assert.AreEqual(pExpectedPerson.DateOfBirth ,pActualPerson.DateOfBirth);
            Assert.AreEqual(pExpectedPerson.NbOfDependents ,pActualPerson.NbOfDependents);
            Assert.AreEqual(pExpectedPerson.NbOfChildren ,pActualPerson.NbOfChildren);
            Assert.AreEqual(pExpectedPerson.ChildrenBasicEducation ,pActualPerson.ChildrenBasicEducation);
            Assert.AreEqual(pExpectedPerson.LivestockNumber ,pActualPerson.LivestockNumber);
            Assert.AreEqual(pExpectedPerson.LivestockType ,pActualPerson.LivestockType);
            Assert.AreEqual(pExpectedPerson.LandplotSize,pActualPerson.LandplotSize);
            Assert.AreEqual(pExpectedPerson.HomeSize ,pActualPerson.HomeSize);
            Assert.AreEqual(pExpectedPerson.HomeTimeLivingIn ,pActualPerson.HomeTimeLivingIn);
            Assert.AreEqual(pExpectedPerson.CapitalOthersEquipments ,pActualPerson.CapitalOthersEquipments);

            if (pExpectedPerson.Activity != null)
                Assert.AreEqual(pExpectedPerson.Activity.Name ,pActualPerson.Activity.Name);
            else
                Assert.AreEqual(pExpectedPerson.Activity, pActualPerson.Activity);

            Assert.AreEqual(pExpectedPerson.Experience ,pActualPerson.Experience);
            Assert.AreEqual(pExpectedPerson.NbOfPeople ,pActualPerson.NbOfPeople);
            Assert.AreEqual(pExpectedPerson.BadClient, pActualPerson.BadClient);

            Assert.AreEqual(pExpectedPerson.StudyLevel, pActualPerson.StudyLevel);
            Assert.AreEqual(pExpectedPerson.SSNumber, pActualPerson.SSNumber);
            Assert.AreEqual(pExpectedPerson.CAFNumber, pActualPerson.CAFNumber);
            Assert.AreEqual(pExpectedPerson.HousingSituation, pActualPerson.HousingSituation);
            Assert.AreEqual(pExpectedPerson.Nationality, pActualPerson.Nationality);
            Assert.AreEqual(pExpectedPerson.BirthPlace, pActualPerson.BirthPlace);
            Assert.AreEqual(pExpectedPerson.Email, pActualPerson.Email);
            Assert.AreEqual(pExpectedPerson.SecondaryEmail, pActualPerson.SecondaryEmail);
            Assert.AreEqual(pExpectedPerson.HomeType, pActualPerson.HomeType);
            Assert.AreEqual(pExpectedPerson.SecondaryHomeType, pActualPerson.SecondaryHomeType);
            Assert.AreEqual(pExpectedPerson.ZipCode, pActualPerson.ZipCode);
            Assert.AreEqual(pExpectedPerson.SecondaryZipCode, pActualPerson.SecondaryZipCode);
            Assert.AreEqual(pExpectedPerson.Handicapped, pActualPerson.Handicapped);
            Assert.AreEqual(pExpectedPerson.FollowUpComment, pActualPerson.FollowUpComment);
            if (pExpectedPerson.FirstContact.HasValue)
                Assert.AreEqual(pExpectedPerson.FirstContact.Value, pActualPerson.FirstContact.Value);
            if (pExpectedPerson.FirstAppointment.HasValue)
                Assert.AreEqual(pExpectedPerson.FirstAppointment.Value, pActualPerson.FirstAppointment.Value);
        }

	    [Ignore]
        public void TestAddPersonAndSelectPersonByIdWhenPersonHasProjects()
        {
            _person.AddProject();
            _person.AddProject();
            _person.AddProject();

            _person.Id = _cltManagement.AddPerson(_person);
            Person selectedPerson = _cltManagement.SelectPersonById(_person.Id);
            
            _AssertPerson(_person, selectedPerson);
        }

        [Ignore]
        public void TestAddPersonAndSelectPersonByIdWhenPersonHasProject()
        {
            _person.AddProject();
            _person.Id = _cltManagement.AddPerson(_person);
            Person selectedPerson = _cltManagement.SelectPersonById(_person.Id);

            _AssertPerson(_person, selectedPerson);
        }

        [Test]
        public void AddPerson_Get_Set_Sponsor1()
        {
            _person.Sponsor1 = "nicolas";
            int id = _cltManagement.AddPerson(_person);

            Person selectedPerson = _cltManagement.SelectPersonById(id);
            Assert.AreEqual("nicolas", selectedPerson.Sponsor1);
        }

        [Test]
        public void AddPerson_Get_Set_Sponsor1Comment()
        {
            _person.Sponsor1Comment = "nico";
            int id = _cltManagement.AddPerson(_person);

            Person selectedPerson = _cltManagement.SelectPersonById(id);
            Assert.AreEqual("nico", selectedPerson.Sponsor1Comment);
        }
        [Test]
        public void AddPerson_Get_Set_Sponsor2()
        {
            _person.Sponsor2 = "florinae";
            int id = _cltManagement.AddPerson(_person);

            Person selectedPerson = _cltManagement.SelectPersonById(id);
            Assert.AreEqual("florinae", selectedPerson.Sponsor2);
        }
        [Test]
        public void AddPerson_Get_Set_Sponsor2Comment()
        {
            _person.Sponsor2Comment = "111111";
            int id = _cltManagement.AddPerson(_person);

            Person selectedPerson = _cltManagement.SelectPersonById(id);
            Assert.AreEqual("111111", selectedPerson.Sponsor2Comment);
        }

        [Test]
        public void AddPerson_Get_Set_UnemploymentMonths()
        {
            _person.UnemploymentMonths = 1;
            int id = _cltManagement.AddPerson(_person);

            Person selectedPerson = _cltManagement.SelectPersonById(id);
            Assert.AreEqual(1, selectedPerson.UnemploymentMonths);
        }

        [Test]
        public void AddPerson_FirstNameInLowerCase()
        {
            _person.FirstName = "nicolas";
            int id = _cltManagement.AddPerson(_person);

            Person selectedPerson = _cltManagement.SelectPersonById(id);
            Assert.AreEqual("nicolas",selectedPerson.FirstName);
        }

        [Test]
        public void AddPerson_CityInUpperCase()
        {
            _person.City = "PARIS";
            int id = _cltManagement.AddPerson(_person);

            Person selectedPerson = _cltManagement.SelectPersonById(id);
            Assert.AreEqual("PARIS", selectedPerson.City);
        }

        [Test]
        public void AddPerson_CityInUpperCase_ButSavedInLowerCase()
        {
            _person.City = "paris";
            int id = _cltManagement.AddPerson(_person);

            Person selectedPerson = _cltManagement.SelectPersonById(id);
            Assert.AreEqual("PARIS", selectedPerson.City);
        }

        [Test]
        public void AddPerson_LastNameInUpperCase()
        {
            _person.LastName = "MANGIN";
            int id = _cltManagement.AddPerson(_person);

            Person selectedPerson = _cltManagement.SelectPersonById(id);
            Assert.AreEqual("MANGIN", selectedPerson.LastName);
        }
        [Test]
        public void AddPerson_FirstNameInLowerCase_ButSaveInUpperCase()
        {
            _person.FirstName = "Nicolas";
            int id = _cltManagement.AddPerson(_person);

            Person selectedPerson = _cltManagement.SelectPersonById(id);
            Assert.AreEqual("nicolas", selectedPerson.FirstName.ToLower());
        }

        [Test]
        public void AddPerson_LastNameInUpperCase_ButSaveInlowerCase()
        {
            _person.LastName = "mangin";
            int id = _cltManagement.AddPerson(_person);

            Person selectedPerson = _cltManagement.SelectPersonById(id);
            Assert.AreEqual("MANGIN", selectedPerson.LastName.ToUpper());
        }
	    [Test]
		public void TestAddPersonAndSelectPersonById()
		{
            _person.MotherName = "Radha";
			_person.CashReceiptIn = 12;
			_person.CashReceiptOut = 2;
			_person.Image = @"c:\image.jpg";
			_person.Id = _cltManagement.AddPerson(_person);
			Person selectedPerson = _cltManagement.SelectPersonById(_person.Id);

            _AssertPerson(_person, selectedPerson);
            Assert.AreEqual("Nicolas BARON", selectedPerson.Name);
		}

		[Test]
		public void TestAddPersonAndSelectPersonByIdNullAuthorizedValuesWhenAllAreNull()
		{
            _person.MotherName = "Radha";
            _person.Id = _cltManagement.AddPerson(_person);
			Person selectedPerson = _cltManagement.SelectPersonById(_person.Id);
            
            _AssertPerson(_person, selectedPerson);
		}

		[Test]
		public void TestAddPersonAndSelectPersonByIdNullAuthorizedValuesWhenNotNull()
		{
			PrepareNullAuthorizedValues();
            _person.MotherName = "Radha";
            _person.HomePhone = "12345678";
            _person.PersonalPhone = "87654321";
            _person.SecondaryHomePhone = "987987654";
            _person.SecondaryPersonalPhone = "12344321";
            _person.ZipCode = "1234";
            _person.SecondaryZipCode = "12344";
            _person.HomeType = "12331";
            _person.SecondaryHomeType = "12461";
			_person.Id = _cltManagement.AddPerson(_person);

			Person selectedPerson = _cltManagement.SelectPersonById(_person.Id);

            _AssertPerson(_person, selectedPerson);

            Assert.AreEqual("1234", selectedPerson.ZipCode);
            Assert.AreEqual("12344", selectedPerson.SecondaryZipCode);
            Assert.AreEqual("12331", selectedPerson.HomeType);
            Assert.AreEqual("12461", selectedPerson.SecondaryHomeType);

            Assert.AreEqual("12345678", selectedPerson.HomePhone);
            Assert.AreEqual("87654321", selectedPerson.PersonalPhone);
            Assert.AreEqual("987987654", selectedPerson.SecondaryHomePhone);
            Assert.AreEqual("12344321", selectedPerson.SecondaryPersonalPhone);
            Assert.AreEqual("Radha", selectedPerson.MotherName);
            Assert.AreEqual(_person.MotherName, selectedPerson.MotherName);
            Assert.AreEqual(_person.Scoring,selectedPerson.Scoring);
			Assert.AreEqual(_person.OtherOrgName,selectedPerson.OtherOrgName);
			Assert.AreEqual(_person.OtherOrgAmount.Value,selectedPerson.OtherOrgAmount.Value);
			Assert.AreEqual(_person.OtherOrgDebts.Value,selectedPerson.OtherOrgDebts.Value);
			Assert.AreEqual(_person.Address,selectedPerson.Address);
			Assert.AreEqual(_person.SecondaryDistrict.Name,selectedPerson.SecondaryDistrict.Name);
			if(!string.IsNullOrEmpty(_person.SecondaryCity))
                Assert.AreEqual(_person.SecondaryCity.ToUpper(),selectedPerson.SecondaryCity);
			Assert.AreEqual(_person.SecondaryAddress,selectedPerson.SecondaryAddress);
			Assert.AreEqual(_person.DateOfBirth,selectedPerson.DateOfBirth);
			Assert.AreEqual(_person.NbOfDependents,selectedPerson.NbOfDependents);
			Assert.AreEqual(_person.NbOfChildren,selectedPerson.NbOfChildren);
			Assert.AreEqual(_person.ChildrenBasicEducation,selectedPerson.ChildrenBasicEducation);
			Assert.AreEqual(_person.LivestockNumber,selectedPerson.LivestockNumber);
			Assert.AreEqual(_person.LivestockType,selectedPerson.LivestockType);
			Assert.AreEqual(_person.LandplotSize,selectedPerson.LandplotSize);
			Assert.AreEqual(_person.HomeSize,selectedPerson.HomeSize);
			Assert.AreEqual(_person.HomeTimeLivingIn,selectedPerson.HomeTimeLivingIn);
			Assert.AreEqual(_person.CapitalOthersEquipments,selectedPerson.CapitalOthersEquipments);
			Assert.AreEqual(_person.Activity.Name,selectedPerson.Activity.Name);
			Assert.AreEqual(_person.Experience,selectedPerson.Experience);
			Assert.AreEqual(_person.NbOfPeople,selectedPerson.NbOfPeople);
		}

		[Test]
		public void TestSelectPersonByIdWhenNoDataMatchesWithId()
		{
			Assert.IsNull(_cltManagement.SelectPersonById(-200));
		}
		
		[Test]
		public void TestSearchPersonsInDatabaseWhenAllParametersSet()
		{
			Person nico = _person;
			_cltManagement.AddPerson(nico);
			Person olivier = _person;
			olivier.FirstName = "Olivier";
			olivier.LastName = "BARON";
			_cltManagement.AddPerson(olivier);
            List<ClientSearchResult> result = _cltManagement.SearchPersonsInDatabase(1, "BA");
            Assert.AreEqual(2, result.Count);
		}

        [Ignore]
		public void TestSearchPersonsInDatabaseWithNameParameterSetMatchesWithFirstName()
		{
			Person nico = _person;
			_cltManagement.AddPerson(nico);
			Person olivier = _person;
			olivier.FirstName = "Olivier";
			olivier.LastName = "BARON";
			_cltManagement.AddPerson(olivier);
            List<ClientSearchResult> result = _cltManagement.SearchPersonsInDatabase(1, "Oli");
            Assert.AreEqual(1, result.Count);
		}

		[Test]
		public void TestSearchPersonsInDatabaseWithDistrictParameterSet()
		{
			Person nico = _person;
			_cltManagement.AddPerson(nico);
			Person olivier = _person;
			olivier.FirstName = "Olivier";
			olivier.LastName = "BARON";
			_cltManagement.AddPerson(olivier);
            List<ClientSearchResult> result = _cltManagement.SearchPersonsInDatabase(1, "Distr");
            Assert.AreEqual(2, result.Count);
		}

        [Ignore]
		public void TestSearchPersonsInDatabaseWithCityParameterSet()
		{
			Person nico = _person;
			_cltManagement.AddPerson(nico);
			Person olivier = _person;
			olivier.FirstName = "Olivier";
			olivier.LastName = "BARON";
			_cltManagement.AddPerson(olivier);
            List<ClientSearchResult> result = _cltManagement.SearchPersonsInDatabase(1, "Dush");
            Assert.AreEqual(2, result.Count);
		}

		[Test]
		public void TestGetNumberOfRecordsFoundForSearchPersonsWhenRecordsExist()
		{
			_person.Id = _cltManagement.AddPerson(_person);
			
			Person firstPersonToAdd = new Person();
			firstPersonToAdd.Active = true;
			firstPersonToAdd.City = "Dushambe";
			firstPersonToAdd.District = _district;
			firstPersonToAdd.FirstName = "Olivier";
			firstPersonToAdd.LastName = "BARON";
			firstPersonToAdd.Sex = 'M';
			firstPersonToAdd.IdentificationData = "145VK133";
			firstPersonToAdd.HouseHoldHead = false;
		    firstPersonToAdd.Branch = _branch;
			firstPersonToAdd.Id = _cltManagement.AddPerson(firstPersonToAdd);
			
			Person secondPersonToAdd = new Person();
			secondPersonToAdd.Active = true;
			secondPersonToAdd.City = "Dushambe";
			secondPersonToAdd.District = _district;
			secondPersonToAdd.FirstName = "Philippe";
			secondPersonToAdd.LastName = "BARON";
			secondPersonToAdd.Sex = 'M';
			secondPersonToAdd.IdentificationData = "2334KGHT";
			secondPersonToAdd.HouseHoldHead = false;
		    secondPersonToAdd.Branch = _branch;
			secondPersonToAdd.Id = _cltManagement.AddPerson(secondPersonToAdd);

            List<ClientSearchResult> result = _cltManagement.SearchPersonsInDatabase(1, "BARON");
            Assert.AreEqual(3, result.Count);
		}

		[Test]
		public void TestGetNumberOfRecordsFoundForSearchPersonsWhenRecordsDontExist()
		{
            Assert.AreEqual(0,_cltManagement.GetNumberOfRecordsFoundForSearchPersons("BARON"));
		}

	    [Test]
		public void TestUpdatePerson()
		{
			_person.Id = _cltManagement.AddPerson(_person);

			_person.Active = false;
			_person.City = "Kathlon";
			_person.District = _district;
			_person.FirstName = "Sabrina";
			_person.LastName = "Mecd";
			_person.Sex = 'F';
			_person.IdentificationData = "ERP345";
			_person.HouseHoldHead = false;
			_person.CashReceiptIn = 10;
			_person.CashReceiptOut = 12;
			_person.Image = @"c:\test.jpg";
		    _person.LoanCycle = 34;
            _person.HomePhone = "0";
            _person.PersonalPhone = "1";
            _person.SecondaryHomePhone = "2";
            _person.SecondaryPersonalPhone = "3";
            _person.StudyLevel = "4";
            _person.SSNumber = "24242";
		    _person.Handicapped = true;
            _person.CAFNumber = "24636236324";
            _person.HousingSituation = "TGSDGSDg";

            _person.Nationality = "France2";
            _person.BirthPlace = "Nancy2";
            _person.Email = "man@yahoo.fr2";
            _person.SecondaryEmail = "man@yahoo342.fr";
            _person.HomeType = "Maison45";
            _person.SecondaryHomeType = "Maison3535";

            _person.ZipCode = "54321";
            _person.SecondaryZipCode = "45654";
            _person.MotherName = "Radha";
		    _person.Status = OClientStatus.Pending;
			PrepareNullAuthorizedValues();

			_cltManagement.UpdatePerson(_person);

			Person selectedPerson = _cltManagement.SelectPersonById(_person.Id);

            _AssertPerson(_person, selectedPerson);
            if(string.IsNullOrEmpty(_person.City))
    			Assert.AreEqual(_person.City.ToUpper(),selectedPerson.City);
            Assert.AreEqual(@"c:\test.jpg", _person.Image);
            Assert.AreEqual(_person.District.Name, selectedPerson.District.Name);
            Assert.AreEqual("0", selectedPerson.HomePhone);
            Assert.AreEqual("54321", selectedPerson.ZipCode);
            Assert.AreEqual("45654", selectedPerson.SecondaryZipCode);
            Assert.AreEqual("1", selectedPerson.PersonalPhone);
            Assert.AreEqual("2", selectedPerson.SecondaryHomePhone);
            Assert.AreEqual("3", selectedPerson.SecondaryPersonalPhone);
            Assert.AreEqual("Radha", selectedPerson.MotherName);

            Assert.AreEqual(_person.MotherName, selectedPerson.MotherName);
			Assert.AreEqual(_person.FirstName.ToLower(),selectedPerson.FirstName);
			Assert.AreEqual(_person.LastName.ToUpper(),selectedPerson.LastName);
			Assert.AreEqual(_person.Sex,selectedPerson.Sex);
			Assert.AreEqual(_person.IdentificationData,selectedPerson.IdentificationData);
			Assert.IsFalse(selectedPerson.HouseHoldHead);
			Assert.AreEqual(10,_person.CashReceiptIn.Value);
			Assert.AreEqual(12,_person.CashReceiptOut.Value);
			Assert.AreEqual(_person.Scoring,selectedPerson.Scoring);
			Assert.AreEqual(_person.OtherOrgName,selectedPerson.OtherOrgName);
			Assert.AreEqual(_person.OtherOrgAmount.Value,selectedPerson.OtherOrgAmount.Value);
			Assert.AreEqual(_person.OtherOrgDebts.Value,selectedPerson.OtherOrgDebts.Value);
			Assert.AreEqual(_person.Address,selectedPerson.Address);
			Assert.AreEqual(_person.SecondaryDistrict.Name,selectedPerson.SecondaryDistrict.Name);
			
            if(!string.IsNullOrEmpty(_person.SecondaryCity))
                Assert.AreEqual(_person.SecondaryCity.ToUpper(),selectedPerson.SecondaryCity);
			
            Assert.AreEqual(_person.SecondaryAddress,selectedPerson.SecondaryAddress);
			Assert.AreEqual(_person.DateOfBirth,selectedPerson.DateOfBirth);
			Assert.AreEqual(_person.NbOfDependents,selectedPerson.NbOfDependents);
			Assert.AreEqual(_person.NbOfChildren,selectedPerson.NbOfChildren);
			Assert.AreEqual(_person.ChildrenBasicEducation,selectedPerson.ChildrenBasicEducation);
			Assert.AreEqual(_person.LivestockNumber,selectedPerson.LivestockNumber);
			Assert.AreEqual(_person.LivestockType,selectedPerson.LivestockType);
			Assert.AreEqual(_person.LandplotSize,selectedPerson.LandplotSize);
			Assert.AreEqual(_person.HomeSize,selectedPerson.HomeSize);
			Assert.AreEqual(_person.HomeTimeLivingIn,selectedPerson.HomeTimeLivingIn);
			Assert.AreEqual(_person.CapitalOthersEquipments,selectedPerson.CapitalOthersEquipments);
			Assert.AreEqual(_person.Activity.Name,selectedPerson.Activity.Name);
			Assert.AreEqual(_person.Experience,selectedPerson.Experience);
			Assert.AreEqual(_person.NbOfPeople,selectedPerson.NbOfPeople);
		}

		[Test]
		public void TestAddGroupAndSelectGroupByIdNonNullAuthorizedValues()
		{
			_person.Id = _cltManagement.AddPerson(_person);


            _group.AddMember(new Member { Tiers = _person, LoanShareAmount = 1000, CurrentlyIn = true, IsLeader = false, JoinedDate = TimeProvider.Today });

			_group.Id = _cltManagement.AddNewGroup(_group);
            Group selectedGroup = _cltManagement.SelectGroupById(_group.Id);
			
			Assert.AreEqual(_group.Id,selectedGroup.Id);
			Assert.AreEqual(OClientTypes.Group,selectedGroup.Type);
            if(!string.IsNullOrEmpty(_group.City))
                Assert.AreEqual(_group.City.ToUpper(),selectedGroup.City);
			Assert.AreEqual(_group.District.Name,selectedGroup.District.Name);
			Assert.AreEqual(_group.Name,selectedGroup.Name);
            Assert.AreEqual(1, selectedGroup.GetNumberOfMembers);
            Assert.AreEqual(OClientStatus.Active, selectedGroup.Status);
			Assert.AreEqual(null,selectedGroup.CashReceiptIn);
			Assert.AreEqual(null,selectedGroup.CashReceiptOut);
            Assert.IsFalse(selectedGroup.BadClient);
		}

		[Test]
		public void TestAddGroupAndSelectGroupById()
		{
			_person.CashReceiptIn = 12;
			_person.CashReceiptOut = 3;
			_person.Id = _cltManagement.AddPerson(_person);

            _group.AddMember(new Member { Tiers = _person, LoanShareAmount = 1000, CurrentlyIn = true, IsLeader = false, JoinedDate = TimeProvider.Today });

			_group.CashReceiptIn = 3;
			_group.CashReceiptOut = 5;
		    _group.BadClient = true;

			_group.Id = _cltManagement.AddNewGroup(_group);
            Group selectedGroup = _cltManagement.SelectGroupById(_group.Id);
			
			Assert.AreEqual(_group.Id,selectedGroup.Id);
			Assert.AreEqual(3,selectedGroup.CashReceiptIn.Value);
			Assert.AreEqual(5,selectedGroup.CashReceiptOut.Value);
            Assert.IsTrue(selectedGroup.BadClient);

			Assert.AreEqual(OClientTypes.Group,selectedGroup.Type);
            if(!string.IsNullOrEmpty(_group.City))
    			Assert.AreEqual(_group.City.ToUpper(),selectedGroup.City);
			Assert.AreEqual(_group.District.Name,selectedGroup.District.Name);
			Assert.AreEqual(_group.Name,selectedGroup.Name);
			Assert.AreEqual(1,_group.GetNumberOfMembers);
			Assert.AreEqual(1000m,_group.Members[0].LoanShareAmount.Value);
		}

		[Test]
		public void TestAddGroupAndSelectGroupByIdNullAuthorizedValuesWhenAllAreNull()
		{
			_person.Id = _cltManagement.AddPerson(_person);

            _group.AddMember(new Member { Tiers = _person, LoanShareAmount = 1000, CurrentlyIn = true, IsLeader = false, JoinedDate = TimeProvider.Today });

			_group.Id = _cltManagement.AddNewGroup(_group);
            Group selectedGroup = _cltManagement.SelectGroupById(_group.Id);
			
			Assert.IsTrue(!selectedGroup.Scoring.HasValue);
			Assert.IsNull(selectedGroup.OtherOrgName);
			Assert.IsTrue(!selectedGroup.OtherOrgAmount.HasValue);
			Assert.IsTrue(!selectedGroup.OtherOrgDebts.HasValue);
			Assert.IsNull(selectedGroup.Address);
			Assert.IsNull(selectedGroup.SecondaryDistrict);
			Assert.IsNull(selectedGroup.SecondaryCity);
			Assert.IsNull(selectedGroup.SecondaryAddress);
			Assert.IsTrue(!selectedGroup.EstablishmentDate.HasValue);
			Assert.AreEqual(1,selectedGroup.GetNumberOfMembers);
			Assert.IsNull(_group.Comments);
		}

		[Test]
		public void TestAddGroupAndSelectGroupByIdNullAuthorizedValuesWhenNotNull()
		{
			_person.Id = _cltManagement.AddPerson(_person);

            _group.AddMember(new Member { Tiers = _person, LoanShareAmount = 1000, CurrentlyIn = true, IsLeader = false, JoinedDate = TimeProvider.Today });

			PrepareNullAuthorizedValues();
			_group.Id = _cltManagement.AddNewGroup(_group);

            Group selectedGroup = _cltManagement.SelectGroupById(_group.Id);
			
			Assert.AreEqual(_group.Scoring,selectedGroup.Scoring);
			Assert.AreEqual(_group.OtherOrgName,selectedGroup.OtherOrgName);
			Assert.AreEqual(_group.OtherOrgAmount.Value,selectedGroup.OtherOrgAmount.Value);
			Assert.AreEqual(_group.OtherOrgDebts.Value,selectedGroup.OtherOrgDebts.Value);
			Assert.AreEqual(_group.Address,selectedGroup.Address);
			Assert.AreEqual(_group.SecondaryDistrict.Name,selectedGroup.SecondaryDistrict.Name);
			if(!string.IsNullOrEmpty(_group.SecondaryCity))
                Assert.AreEqual(_group.SecondaryCity.ToUpper(),selectedGroup.SecondaryCity);
			Assert.AreEqual(_group.SecondaryAddress,selectedGroup.SecondaryAddress);
			Assert.AreEqual(_group.EstablishmentDate,selectedGroup.EstablishmentDate);
			Assert.AreEqual(_group.FollowUpComment,selectedGroup.FollowUpComment);
			Assert.AreEqual(1,selectedGroup.GetNumberOfMembers);
		}

		[Test]
		public void TestSelectGroupWhenNoDataMatchesWithId()
		{
			Assert.IsNull(_cltManagement.SelectGroupById(-15));
		}

		[Ignore]
		public void TestUpdateGroupExceptPersonsList()
		{
			_group.Id = _cltManagement.AddNewGroup(_group,null);

			_group.Active = true;
			_group.City = "Dushambe";
			_group.District = _district;
			_group.Name = "SCG";
			_group.CashReceiptIn = 10;
			_group.CashReceiptOut = 14;
		    _group.Status = OClientStatus.Deleted;


		    _group.HomeType = "Maison";
            _group.SecondaryZipCode = "75000";
            _group.SecondaryHomeType = "Maison2";
            _group.ZipCode = "75002";
            _group.HomePhone = "0";
            _group.PersonalPhone = "1";
            _group.SecondaryHomePhone = "2";
            _group.SecondaryPersonalPhone = "3";
			PrepareNullAuthorizedValues();

			_cltManagement.UpdateGroup(_group,null);

            Group selectedGroup = _cltManagement.SelectGroupById(_group.Id);
			
			Assert.AreEqual(_group.Id,selectedGroup.Id);
			Assert.AreEqual(_group.City,selectedGroup.City);

            Assert.AreEqual("75002", _group.ZipCode);
            Assert.AreEqual("75000", _group.SecondaryZipCode);
            Assert.AreEqual("Maison", _group.HomeType);
            Assert.AreEqual("Maison2", _group.SecondaryHomeType);

            Assert.AreEqual("0", _group.HomePhone);
            Assert.AreEqual("1", _group.PersonalPhone);
            Assert.AreEqual("2", _group.SecondaryHomePhone);
            Assert.AreEqual("3", _group.SecondaryPersonalPhone);
			Assert.AreEqual(_group.District.Name,selectedGroup.District.Name);
            Assert.AreEqual(_group.Name, selectedGroup.Name);
            Assert.AreEqual(10, _group.CashReceiptIn.Value);
            Assert.AreEqual(OClientStatus.Deleted, _group.Status);
			Assert.AreEqual(14,_group.CashReceiptOut.Value);
			Assert.AreEqual(_group.Scoring,selectedGroup.Scoring);
			Assert.AreEqual(_group.OtherOrgName,selectedGroup.OtherOrgName);
			Assert.AreEqual(_group.OtherOrgAmount,selectedGroup.OtherOrgAmount);
			Assert.AreEqual(_group.OtherOrgDebts,selectedGroup.OtherOrgDebts);
			Assert.AreEqual(_group.Address,selectedGroup.Address);
			Assert.AreEqual(_group.SecondaryDistrict.Name,selectedGroup.SecondaryDistrict.Name);
			Assert.AreEqual(_group.SecondaryCity,selectedGroup.SecondaryCity);
			Assert.AreEqual(_group.SecondaryAddress,selectedGroup.SecondaryAddress);
			Assert.AreEqual(_group.EstablishmentDate,selectedGroup.EstablishmentDate);
			Assert.AreEqual(_group.Comments,selectedGroup.Comments);
		}

		[Test]
		public void TestUpdateGroupWhenNeedToAddPersonsToGroup()
		{
			_person.Id = _cltManagement.AddPerson(_person);

            _group.AddMember(new Member { Tiers = _person, LoanShareAmount = 1000, CurrentlyIn = true, IsLeader = false, JoinedDate = TimeProvider.Today });

			_group.Id = _cltManagement.AddNewGroup(_group);

            Group selectedGroup = _cltManagement.SelectGroupById(_group.Id);
			Assert.AreEqual(1,selectedGroup.GetNumberOfMembers);

			Person firstPersonToAdd = new Person
			                              {
			                                  Active = true,
			                                  City = "Dus   hambe",
			                                  District = _district,
			                                  FirstName = "Olivier",
			                                  LastName = "BARON",
			                                  Sex = 'M',
			                                  IdentificationData = "145VK133",
			                                  HouseHoldHead = false,
                                              Branch = _branch
			                              };
		    firstPersonToAdd.Id = _cltManagement.AddPerson(firstPersonToAdd);
			
			Person secondPersonToAdd = new Person
			                               {
			                                   Active = true,
			                                   City = "Dushambe",
			                                   District = _district,
			                                   FirstName = "Philippe",
			                                   LastName = "BARON",
			                                   Sex = 'M',
			                                   IdentificationData = "2334KGHT",
			                                   HouseHoldHead = false,
                                               Branch = _branch
			                               };
		    secondPersonToAdd.Id = _cltManagement.AddPerson(secondPersonToAdd);

			_group.Members[0].LoanShareAmount = 100;
            _group.AddMember(new Member { Tiers = firstPersonToAdd, LoanShareAmount = 1000, CurrentlyIn = true, IsLeader = false, JoinedDate = TimeProvider.Today });
            _group.AddMember(new Member { Tiers = secondPersonToAdd, LoanShareAmount = 1000, CurrentlyIn = true, IsLeader = false, JoinedDate = TimeProvider.Today });

			_cltManagement.UpdateGroup(_group);
			selectedGroup = _cltManagement.SelectGroupById(_group.Id);

			Assert.AreEqual(3,selectedGroup.GetNumberOfMembers);
		}

		private static bool SelectCurrentlyIn(int personId)
		{
			ConnectionManager cm = ConnectionManager.GetInstance("opencbs_test");

            OpenCbsCommand select = new OpenCbsCommand("SELECT currently_in FROM PersonGroupBelonging WHERE person_id = " + personId,
				cm.SqlConnection);
			
			using (OpenCbsReader reader = select.ExecuteReader())
			{
				reader.Read();
                return reader.GetBool("currently_in");
			}
		}

	    [Test]
		public void TestUpdateGroupWhenNeedToChangeLeader()
		{
			_person.CashReceiptIn = 19;
			_person.CashReceiptOut = 2;
			_person.Id = _cltManagement.AddPerson(_person);

			Person firstPersonToAdd = new Person
			                              {
			                                  Active = true,
			                                  City = "Dushambe",
			                                  District = _district,
			                                  FirstName = "Nicolas",
			                                  LastName = "MANGIN",
			                                  Sex = 'M',
			                                  IdentificationData = "145VK133",
			                                  HouseHoldHead = false,
			                                  CashReceiptIn = 15,
			                                  CashReceiptOut = 23,
                                              Branch = _branch
			                              };
	        firstPersonToAdd.Id = _cltManagement.AddPerson(firstPersonToAdd);

			Person secondPersonToAdd = new Person
			                               {
			                                   Active = true,
			                                   City = "Dushambe",
			                                   District = _district,
			                                   FirstName = "Philippe",
			                                   LastName = "BARON",
			                                   Sex = 'M',
			                                   IdentificationData = "2334KGHT",
			                                   HouseHoldHead = false,
			                                   CashReceiptIn = 1,
			                                   CashReceiptOut = 4,
                                               Branch = _branch
			                               };
	        secondPersonToAdd.Id = _cltManagement.AddPerson(secondPersonToAdd);

            _group.Leader = new Member { Tiers = _person, LoanShareAmount = 0, CurrentlyIn = true, IsLeader = true, JoinedDate = TimeProvider.Today };
            _group.AddMember(new Member { Tiers = firstPersonToAdd, LoanShareAmount = 1000, CurrentlyIn = true, IsLeader = false, JoinedDate = TimeProvider.Today });
            _group.AddMember(new Member { Tiers = secondPersonToAdd, LoanShareAmount = 1000, CurrentlyIn = true, IsLeader = false, JoinedDate = TimeProvider.Today });
			_group.CashReceiptIn = 10;
			_group.CashReceiptOut = 14;

			_group.Id = _cltManagement.AddNewGroup(_group);

            Group selectedGroup = _cltManagement.SelectGroupById(_group.Id);

			Assert.AreEqual(3,selectedGroup.GetNumberOfMembers);
			Assert.AreEqual(_person.LastName,((Person)selectedGroup.Leader.Tiers).LastName);

			Assert.AreEqual(10,_group.CashReceiptIn.Value);
			Assert.AreEqual(14,_group.CashReceiptOut.Value);

            Assert.AreEqual(19, ((Person)selectedGroup.Leader.Tiers).CashReceiptIn.Value);
            Assert.AreEqual(2, ((Person)selectedGroup.Leader.Tiers).CashReceiptOut.Value);

			_group.CashReceiptIn = 101;
			_group.CashReceiptOut = 141;

            _group.Leader = new Member { Tiers = firstPersonToAdd, LoanShareAmount = 0, CurrentlyIn = true, IsLeader = true, JoinedDate = TimeProvider.Today };
            ((Person)selectedGroup.Leader.Tiers).CashReceiptIn = 151;
            ((Person)selectedGroup.Leader.Tiers).CashReceiptOut = 231;

			_cltManagement.UpdateGroup(_group);
			selectedGroup = _cltManagement.SelectGroupById(_group.Id);

			Assert.AreEqual(3,selectedGroup.GetNumberOfMembers);
			Assert.AreEqual(101,_group.CashReceiptIn.Value);
			Assert.AreEqual(141,_group.CashReceiptOut.Value);

            Assert.AreEqual(firstPersonToAdd.LastName.ToUpper(), ((Person)selectedGroup.Leader.Tiers).LastName);
            Assert.AreEqual(firstPersonToAdd.FirstName.ToLower(), ((Person)selectedGroup.Leader.Tiers).FirstName);
            Assert.AreEqual(firstPersonToAdd.Id, ((Person)selectedGroup.Leader.Tiers).Id);
		}
		[Test]
		public void TestUpdateGroupWhenNeedToRemovePersonsFromGroup()
		{
			_person.Id = _cltManagement.AddPerson(_person);
            Member member = new Member { Tiers = _person, LoanShareAmount = 1000, CurrentlyIn = true, IsLeader = false, JoinedDate = TimeProvider.Today };

			Person firstPersonToAdd = new Person
			                              {
			                                  Active = true,
			                                  City = "Dushambe",
			                                  District = _district,
			                                  FirstName = "Nicolas",
			                                  LastName = "MANGIN",
			                                  Sex = 'M',
			                                  IdentificationData = "145VK133",
			                                  HouseHoldHead = false,
                                              Branch = _branch
			                              };
		    firstPersonToAdd.Id = _cltManagement.AddPerson(firstPersonToAdd);
            Member firstMemberToAdd = new Member { Tiers = firstPersonToAdd, LoanShareAmount = 1000, CurrentlyIn = true, IsLeader = false, JoinedDate = TimeProvider.Today };

			Person secondPersonToAdd = new Person
			                               {
			                                   Active = true,
			                                   City = "Dushambe",
			                                   District = _district,
			                                   FirstName = "Philippe",
			                                   LastName = "BARON",
			                                   Sex = 'M',
			                                   IdentificationData = "2334KGHT",
			                                   HouseHoldHead = false,
                                               Branch = _branch
			                               };
		    secondPersonToAdd.Id = _cltManagement.AddPerson(secondPersonToAdd);
            Member secondMemberToAdd = new Member { Tiers = secondPersonToAdd, LoanShareAmount = 1000, CurrentlyIn = true, IsLeader = false, JoinedDate = TimeProvider.Today };

            _group.AddMember(member);
			_group.AddMember(firstMemberToAdd);
            _group.AddMember(secondMemberToAdd);

			_group.Id = _cltManagement.AddNewGroup(_group);

            Group selectedGroup = _cltManagement.SelectGroupById(_group.Id);
			Assert.AreEqual(3,selectedGroup.GetNumberOfMembers);

            _group.DeleteMember(member);
            _group.DeleteMember(secondMemberToAdd);

			_cltManagement.UpdateGroup(_group);

			selectedGroup = _cltManagement.SelectGroupById(_group.Id);
			Assert.AreEqual(1,selectedGroup.GetNumberOfMembers);
			
			Assert.IsFalse(SelectCurrentlyIn(_person.Id));
			Assert.IsFalse(SelectCurrentlyIn(secondPersonToAdd.Id));
		}

		[Test]
		public void TestSearchGroupsInDatabaseWhenAllParametersSet()
		{
            Group group1 = new Group{Active = true,City = "Dushambe",District = _district,LoanCycle = 1,Name = "SCG1",Branch = _branch};
		    _cltManagement.AddNewGroup(group1);

            Group group2 = new Group{Active = true,City = "Dushambe",LoanCycle = 1,District = _district,Name = "SCG2",Branch = _branch};
		    _cltManagement.AddNewGroup(group2);

		    List<ClientSearchResult> result = _cltManagement.SearchGroups(1, "SCG");
            Assert.AreEqual(2,result.Count);
		}

		[Test]
		public void TestSearchGroupsInDatabaseWithAmountCycleParameterSet()
		{
            Group group1 = new Group{Active = true,City = "Dushambe",District = _district,LoanCycle = 1,Name = "SCG1",Branch = _branch};
		    _cltManagement.AddNewGroup(group1);

            Group group2 = new Group{Active = true,City = "Dushambe",LoanCycle = 1,District = _district,Name = "SCG2",Branch = _branch};
		    _cltManagement.AddNewGroup(group2);

            List<ClientSearchResult> result = _cltManagement.SearchGroups(1, "SCG");
            Assert.AreEqual(2, result.Count);
		}

		[Test]
		public void TestSearchGroupsInDatabaseWithDistrictParameterSet()
		{
            Group group1 = new Group {Active = true, City = "Dushambe", District = _district, Name = "SCG1",Branch = _branch};
		    _cltManagement.AddNewGroup(group1);

            Group group2 = new Group {Active = true, City = "Dushambe", District = _district, Name = "SCG2",Branch = _branch};
		    _cltManagement.AddNewGroup(group2);

            List<ClientSearchResult> result = _cltManagement.SearchGroups(1, "Distr");
            Assert.AreEqual(2, result.Count);
		}



		[Ignore]
		public void TestSearchGroupsInDatabaseWithCityParameterSet()
		{
            Group group1 = new Group{Active = true,City = "Dushambe",District = _district,Name = "SCG1",LoanCycle = 1};
		    _cltManagement.AddNewGroup(group1,null);

            Group group2 = new Group{Active = true,City = "Dushambe",District = _district,Name = "SCG2",LoanCycle = 2};
		    _cltManagement.AddNewGroup(group2,null);

            List<ClientSearchResult> result = _cltManagement.SearchGroups(1, "Dushambe");
            Assert.AreEqual(2, result.Count);
		}

		[Test]
		public void TestGetNumberOfRecordsFoundForSearchGroupsWhenRecordsExist()
		{
			_cltManagement.AddNewGroup(_group);

            Group group1 = new Group {Active = true, City = "Dushambe", District = _district, Name = "SCG1",Branch = _branch};
		    _cltManagement.AddNewGroup(group1);

            Group group2 = new Group {Active = true, City = "Dushambe", District = _district, Name = "SCG2",Branch = _branch};
		    _cltManagement.AddNewGroup(group2);
            List<ClientSearchResult> result = _cltManagement.SearchGroups(1, "SCG");
            Assert.AreEqual(3, result.Count);
		}

		[Test]
		public void TestGetNumberOfRecordsFoundForSearchGroupsWhenRecordsDontExist()
		{
            Assert.AreEqual(0,_cltManagement.GetNumberOfRecordsFoundForSearchGroups("BARON"));
		}

		[Test]
		public void TestIsLeaderWhenTrue()
		{
			_person.Id = _cltManagement.AddPerson(_person);
            Member member = new Member { Tiers = _person, LoanShareAmount = 1000, CurrentlyIn = true, IsLeader = true, JoinedDate = TimeProvider.Today };
            _group.AddMember(member);
            _group.Leader = member;

			_group.Id = _cltManagement.AddNewGroup(_group);

			Assert.IsTrue(_cltManagement.IsLeader(_person.Id, _group.Id));
		}
		
		[Test]
		public void TestIsLeaderWhenFalse()
		{
			_person.Id = _cltManagement.AddPerson(_person);

            _group.AddMember(new Member { Tiers = _person, LoanShareAmount = 1000, CurrentlyIn = true, IsLeader = false, JoinedDate = TimeProvider.Today });

			_group.Id = _cltManagement.AddNewGroup(_group);
			Assert.IsFalse(_cltManagement.IsLeader(_person.Id, _group.Id));
		}

		[Test]
		public void TestIsLeaderWhenPersonIsNotInAGroup()
		{
			_person.Id = _cltManagement.AddPerson(_person);

			Assert.IsFalse(_cltManagement.IsLeader(_person.Id, _group.Id));
		}

		[Test]
		public void TestSelectGroupIdsByPersonId()
		{
			_person.Id = _cltManagement.AddPerson(_person);

            Group group1 = new Group {Active = true, City = "Dushambe", District = _district, Name = "SCG1", Branch = _branch};

            group1.AddMember(new Member { Tiers = _person, LoanShareAmount = 1000, CurrentlyIn = true, IsLeader = false, JoinedDate = TimeProvider.Today });
			_cltManagement.AddNewGroup(group1);

            Group group2 = new Group {Active = true, City = "Dushambe", District = _district, Name = "SCG2", Branch = _branch};
            group2.AddMember(new Member { Tiers = _person, LoanShareAmount = 2000, CurrentlyIn = true, IsLeader = true, JoinedDate = TimeProvider.Today });
			_cltManagement.AddNewGroup(group2);

			Assert.AreEqual(2,_cltManagement.SelectGroupIdsByPersonId(_person.Id).Count);
		}

        [Test]
        public void TestSelectGroup()
        {
            Person leader = new Person
            {
                FirstName = "Nicolas",
                LastName = "MANGIN",
                Sex = 'M',
                IdentificationData = "12345",
                LoanCycle = 1,
                BadClient = false,
                Address = "NotSet",
                City = "NotSet",
                DateOfBirth = TimeProvider.Today,
                District = _district,
                Activity = _agriculture,
                Branch = _branch
            };
            leader.Id = _cltManagement.AddPerson(leader);

            Person member1 = new Person
            {
                FirstName = "Mariam",
                LastName = "MANGIN",
                Sex = 'M',
                IdentificationData = "123456",
                LoanCycle = 1,
                BadClient = false,
                Address = "NotSet",
                City = "NotSet",
                DateOfBirth = TimeProvider.Today,
                District = _district,
                Activity = _agriculture,
                Branch = _branch
            };
            member1.Id = _cltManagement.AddPerson(member1);

            Person member2 = new Person
            {
                FirstName = "Vincent",
                LastName = "MANGIN",
                Sex = 'M',
                IdentificationData = "1234567",
                LoanCycle = 1,
                BadClient = false,
                Address = "NotSet",
                City = "NotSet",
                DateOfBirth = TimeProvider.Today,
                District = _district,
                Activity = _agriculture,
                Branch = _branch
            };
            member2.Id = _cltManagement.AddPerson(member2);

            Person member3 = new Person
            {
                FirstName = "Rudy",
                LastName = "MANGIN",
                Sex = 'M',
                IdentificationData = "12345678",
                LoanCycle = 1,
                BadClient = false,
                Address = "NotSet",
                City = "NotSet",
                DateOfBirth = TimeProvider.Today,
                District = _district,
                Activity = _agriculture,
                Branch = _branch
            };
            member3.Id = _cltManagement.AddPerson(member3);

            Group grp = new Group
            {
                Leader = new Member { Tiers = leader, LoanShareAmount = 0, CurrentlyIn = true, IsLeader = true, JoinedDate = TimeProvider.Today },
                EstablishmentDate = TimeProvider.Today,
                Name = "GroupTest",
                Branch = _branch
            };

            grp.AddMember(new Member { Tiers = member1, LoanShareAmount = 100, CurrentlyIn = true, IsLeader = false, JoinedDate = TimeProvider.Today });
            grp.AddMember(new Member { Tiers = member2, LoanShareAmount = 100, CurrentlyIn = true, IsLeader = false, JoinedDate = TimeProvider.Today });
            grp.AddMember(new Member { Tiers = member3, LoanShareAmount = 100, CurrentlyIn = true, IsLeader = false, JoinedDate = TimeProvider.Today });
            grp.District = _district;
            grp.Address = "NotSet";
            grp.City = "NotSet";
            grp.Id = _cltManagement.AddNewGroup(grp);

            Group newGroup = _cltManagement.SelectGroupById(grp.Id);

            Assert.AreEqual("nicolas MANGIN", newGroup.Leader.Tiers.Name);
        }

	    [Test]
        public void TestGetNumberCorporate()
        {
            int number = _cltManagement.GetNumberOfRecordsFoundForSearchCorporates(string.Empty);
            Assert.AreEqual(0,number);
        }
	}
}
