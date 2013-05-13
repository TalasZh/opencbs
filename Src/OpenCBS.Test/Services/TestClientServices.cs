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
using NUnit.Framework;
using NUnit.Mocks;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Clients;
using OpenCBS.CoreDomain.EconomicActivities;
using OpenCBS.CoreDomain.SearchResult;
using OpenCBS.Manager;
using OpenCBS.Manager.Clients;
using OpenCBS.Services;
using OpenCBS.Enums;
using OpenCBS.Shared;
using OpenCBS.Shared.Settings;

namespace OpenCBS.Test.Services
{
    /// <summary>
    /// This is the test class of the class ClientServices.
    /// </summary>
    [TestFixture]
    public class TestClientServices
    {
        private ClientServices clientServices;
        private ClientManager clientManagement;
        private BranchManager branchManager;
        private DynamicMock mockClientManagement;
        private readonly int ERRORVALUE = -1;
        private Branch _branch;

        [TestFixtureSetUp]
        public void TextFixtureSetUp()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ENFORCE_ID_PATTERN, 1);
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ID_PATTERN, @"[0-9]{2}[a-z]{2}");
            mockClientManagement = new DynamicMock(typeof(ClientManager));
            _branch = new Branch {Id = 1, Name = "Default"};
        }

        [Test]
        public void TestFindTiersWhenNoResult()
        {
            int numberTotalPage = 1;
            int numberOfRecords = 1;
            string query = "nicolas";
            List <ClientSearchResult> list = new List<ClientSearchResult>();
            //mockClientManagement.ExpectAndReturn("GetNumberGroup", 0, query);
            //mockClientManagement.ExpectAndReturn("GetNumberPerson", 0, query);
            //mockClientManagement.ExpectAndReturn("GetNumberVillages", 0, query);

            //mockClientManagement.ExpectAndReturn("SearchGroupByCriteres", list, numberTotalPage, query);
            //mockClientManagement.ExpectAndReturn("SearchPersonByCriteres", list, numberTotalPage, query);
            //mockClientManagement.ExpectAndReturn("SearchVillagesByCriteres", list, numberTotalPage, query);
            mockClientManagement.ExpectAndReturn("GetFoundRecordsNumber",0, query, 2, 1, 1, 1);

            mockClientManagement.ExpectAndReturn("SearchAllByCriteres", list, query,2, numberTotalPage,1, 1, 1);
           
            clientManagement = (ClientManager)mockClientManagement.MockInstance;
            clientServices = new ClientServices(clientManagement);

            List<ClientSearchResult> result = clientServices.FindTiers(out numberTotalPage, out numberOfRecords,query, 2,  numberTotalPage, 1, 1, 1);
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void TestFindTiersWhenTiersIsPersonAndRecordsAreFound()
        {
            int numberTotalPage = 1;
            int numberOfRecords = 1;
            string query = "nicolas";

            List<ClientSearchResult> listGroup = new List<ClientSearchResult>();

            for (int i = 1; i <= 15; i++)
            {
                listGroup.Add(new ClientSearchResult(i, "nicolas", OClientTypes.Person, true, 1, "district", "city", false, "OOOOO","test"));
            }

            //mockClientManagement.ExpectAndReturn("GetNumberGroup", 0, query);
            //mockClientManagement.ExpectAndReturn("GetNumberPerson", 0, query);
            //mockClientManagement.ExpectAndReturn("GetNumberVillages", 0, query);
            mockClientManagement.ExpectAndReturn("GetFoundRecordsNumber", 15, query, 1, 1, 1, 1);
            //mockClientManagement.ExpectAndReturn("SearchGroupByCriteres", listGroup, numberTotalPage, query);
            //mockClientManagement.ExpectAndReturn("SearchPersonByCriteres", listPerson, numberTotalPage, query);
            //mockClientManagement.ExpectAndReturn("SearchVillagesByCriteres", listVillages, numberTotalPage, query);
            mockClientManagement.ExpectAndReturn("SearchAllByCriteres", listGroup, query,1, numberTotalPage,1, 1, 1);
            clientManagement = (ClientManager)mockClientManagement.MockInstance;
            clientServices = new ClientServices(clientManagement);

            List<ClientSearchResult> result = clientServices.FindTiers(out numberTotalPage, out numberOfRecords, query, 1, numberTotalPage, 1, 1, 1);
            Assert.AreEqual(15, result.Count);
        }

        [Test]
        [ExpectedException(typeof(OpenCBS.ExceptionsHandler.OpenCbsTiersSaveException))]
        public void TestIfTiersIsValidWhenTiersIsNull()
        {
            clientServices.CheckIfTiersIsValid(null);
        }

        [Test]
        public void TestIfTiersIsValidWhenTiersIsNotNull()
        {
            Person person = new Person();
            Assert.IsTrue(clientServices.CheckIfTiersIsValid(person));
        }

        [Test]
        [ExpectedException(typeof(OpenCBS.ExceptionsHandler.OpenCbsTiersSaveException))]
        public void TestIfSecondaryAddressPartiallyFilledButDistrictIsNull()
        {
            Person person = new Person();
            person.SecondaryAddress = "address";
            person.SecondaryCity = "city";
            person.SecondaryDistrict = null;

            clientServices.SecondaryAddressCorrectlyFilled(person);
        }

        [Test]
        [ExpectedException(typeof(OpenCBS.ExceptionsHandler.OpenCbsTiersSaveException))]
        public void TestIfSecondaryAddressPartiallyFilledButCityIsNull()
        {
            Person person = new Person();
            person.SecondaryAddress = "address";
            person.SecondaryCity = null;
            person.SecondaryDistrict = new District();

            clientServices.SecondaryAddressCorrectlyFilled(person);
        }

        [Test]
        public void TestIfSecondaryAddressPartiallyFilledButCommentsIsNull()
        {
            Person person = new Person();
            person.SecondaryAddress = null;
            person.SecondaryCity = "city";
            person.SecondaryDistrict = new District();

            clientServices.SecondaryAddressCorrectlyFilled(person);
        }

        [Test]
        [ExpectedException(typeof(OpenCBS.ExceptionsHandler.OpenCbsTiersSaveException))]
        public void TestSavePersonButActivityIsNull()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ID_PATTERN, @"[0-9]{2}[a-z]{2}");

            Person person = AddPerson(true, null, 1, "city", new District(1, "district", new Province(1, "province")),
                "nicolas", "MANGIN", 'M', "12345", true);

            clientServices.SavePerson(ref person);
        }


        [Test]
        [ExpectedException(typeof(OpenCBS.ExceptionsHandler.OpenCbsTiersSaveException))]
        public void TestSavePersonButNbOfDependentsIsBad()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ID_PATTERN, @"[0-9]{2}[a-z]{2}");
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ENFORCE_ID_PATTERN, 1);

            Person person = AddPerson(true, new EconomicActivity(1, "tiki", null, false), 1, "city",
                new District(1, "district", new Province(1, "province")), "nicolas", "mangin", 'M', "12aa", true);

            person.NbOfDependents = ERRORVALUE;
            //clientManagement = (ClientManager)mockClientManagement.MockInstance;
            //clientServices = new ClientServices(clientManagement);
            clientServices.SavePerson(ref person);
        }

        [Test]
        [ExpectedException(typeof(OpenCBS.ExceptionsHandler.OpenCbsTiersSaveException))]
        public void TestSavePersonButNbOfChildrensIsBad()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ENFORCE_ID_PATTERN, 1);
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ID_PATTERN, @"[0-9]{2}[a-z]{2}");

            Person person = AddPerson(true, new EconomicActivity(1, "tiki", null, false), 1, "city",
                new District(1, "district", new Province(1, "province")), "nicolas", "mangin", 'M', "12aa", true);

            person.NbOfDependents = 1;
            person.NbOfChildren = ERRORVALUE;

            clientServices.SavePerson(ref person);
        }

        [Test]
        [ExpectedException(typeof(OpenCBS.ExceptionsHandler.OpenCbsTiersSaveException))]
        public void TestSavePersonButNbOfChildrensWithBasicEducationIsBad()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ID_PATTERN, @"[0-9]{2}[a-z]{2}");

            Person person = AddPerson(true, new EconomicActivity(1, "tiki", null, false), 1, "city",
                new District(1, "district", new Province(1, "province")), "nicolas", "mangin", 'M', "12aa", true);

            person.NbOfDependents = 1;
            person.NbOfChildren = 1;
            person.ChildrenBasicEducation = ERRORVALUE;

            clientServices.SavePerson(ref person);
        }

        [Test]
        [ExpectedException(typeof(OpenCBS.ExceptionsHandler.OpenCbsTiersSaveException))]
        public void TestSavePersonButExperienceIsBad()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ID_PATTERN, @"[0-9]{2}[a-z]{2}");

            Person person = AddPerson(true, new EconomicActivity(1, "tiki", null, false), 1, "city",
                new District(1, "district", new Province(1, "province")), "nicolas", "mangin", 'M', "12aa", true);

            person.NbOfDependents = 1;
            person.NbOfChildren = 1;
            person.ChildrenBasicEducation = 1;
            person.Experience = ERRORVALUE;

            clientServices.SavePerson(ref person);
        }

        [Test]
        [ExpectedException(typeof(OpenCBS.ExceptionsHandler.OpenCbsTiersSaveException))]
        public void TestSavePersonButNbOfPeopleIsBad()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ID_PATTERN, @"[0-9]{2}[a-z]{2}");

            Person person = AddPerson(true, new EconomicActivity(1, "tiki", null, false), 1, "city",
                new District(1, "district", new Province(1, "province")), "nicolas", "mangin", 'M', "12aa", true);

            person.NbOfDependents = 1;
            person.NbOfChildren = 1;
            person.ChildrenBasicEducation = 1;
            person.Experience = 1;
            person.NbOfPeople = ERRORVALUE;

            clientServices.SavePerson(ref person);
        }

        [Test]
        [ExpectedException(typeof(OpenCBS.ExceptionsHandler.OpenCbsTiersSaveException))]
        public void TestSavePersonButAmountWithOtherOrganizationIsBad()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ID_PATTERN, @"[0-9]{2}[a-z]{2}");

            Person person = AddPerson(true, new EconomicActivity(1, "tiki", null, false), 1, "city",
                new District(1, "district", new Province(1, "province")), "nicolas", "mangin", 'M', "12aa", true);

            person.NbOfDependents = 1;
            person.NbOfChildren = 1;
            person.ChildrenBasicEducation = 1;
            person.Experience = 1;
            person.NbOfPeople = 1;
            person.OtherOrgAmount = ERRORVALUE;

            clientServices.SavePerson(ref person);
        }

        [Test]
        [ExpectedException(typeof(OpenCBS.ExceptionsHandler.OpenCbsTiersSaveException))]
        public void TestSavePersonButAmountWithOtherDebtsIsBad()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ID_PATTERN, @"[0-9]{2}[a-z]{2}");

            Person person = AddPerson(true, new EconomicActivity(1, "tiki", null, false), 1, "city",
                new District(1, "district", new Province(1, "province")), "nicolas", "mangin", 'M', "12aa", true);

            person.NbOfDependents = 1;
            person.NbOfChildren = 1;
            person.ChildrenBasicEducation = 1;
            person.Experience = 1;
            person.NbOfPeople = 1;
            person.OtherOrgAmount = 1;
            person.OtherOrgDebts = ERRORVALUE;

            clientServices.SavePerson(ref person);
        }

        [Test]
        [ExpectedException(typeof(OpenCBS.ExceptionsHandler.OpenCbsTiersSaveException))]
        public void TestSavePersonButHomeSizeIsBad()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ID_PATTERN, @"[0-9]{2}[a-z]{2}");

            Person person = AddPerson(true, new EconomicActivity(1, "tiki", null, false), 1, "city",
                new District(1, "district", new Province(1, "province")), "nicolas", "mangin", 'M', "12aa", true);

            person.NbOfDependents = 1;
            person.NbOfChildren = 1;
            person.ChildrenBasicEducation = 1;
            person.Experience = 1;
            person.NbOfPeople = 1;
            person.OtherOrgAmount = 1;
            person.OtherOrgDebts = 1;
            person.HomeSize = ERRORVALUE;

            clientServices.SavePerson(ref person);
        }

        [Test]
        [ExpectedException(typeof(OpenCBS.ExceptionsHandler.OpenCbsTiersSaveException))]
        public void TestSavePersonButTimeLivingInIsBad()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ID_PATTERN, @"[0-9]{2}[a-z]{2}");

            Person person = AddPerson(true, new EconomicActivity(1, "tiki", null, false), 1, "city", new District(1, "district", new Province(1, "province")), "nicolas", "mangin", 'M', "12aa", true);

            person.NbOfDependents = 1;
            person.NbOfChildren = 1;
            person.ChildrenBasicEducation = 1;
            person.Experience = 1;
            person.NbOfPeople = 1;
            person.OtherOrgAmount = 1;
            person.OtherOrgDebts = 1;
            person.HomeSize = 1;
            person.HomeTimeLivingIn = ERRORVALUE;

            clientServices.SavePerson(ref person);
        }

        [Test]
        [ExpectedException(typeof(OpenCBS.ExceptionsHandler.OpenCbsTiersSaveException))]
        public void TestSavePersonButLandPlotSizeIsBad()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ID_PATTERN, @"[0-9]{2}[a-z]{2}");

            Person person = AddPerson(true, new EconomicActivity(1, "tiki", null, false), 1, "city", new District(1, "district", new Province(1, "province")), "nicolas", "mangin", 'M', "12aa", true);

            person.NbOfDependents = 1;
            person.NbOfChildren = 1;
            person.ChildrenBasicEducation = 1;
            person.Experience = 1;
            person.NbOfPeople = 1;
            person.OtherOrgAmount = 1;
            person.OtherOrgDebts = 1;
            person.HomeSize = 1;
            person.HomeTimeLivingIn = 1;
            person.LandplotSize = ERRORVALUE;

            clientServices.SavePerson(ref person);
        }

        [Test]
        [ExpectedException(typeof(OpenCBS.ExceptionsHandler.OpenCbsTiersSaveException))]
        public void TestSavePersonButLivestockNumberIsBad()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ID_PATTERN, @"[0-9]{2}[a-z]{2}");
            Person person = AddPerson(true, new EconomicActivity(1, "tiki", null, false), 1, "city", new District(1, "district", new Province(1, "province")), "nicolas", "mangin", 'M', "12aa", true);

            person.NbOfDependents = 1;
            person.NbOfChildren = 1;
            person.ChildrenBasicEducation = 1;
            person.Experience = 1;
            person.NbOfPeople = 1;
            person.OtherOrgAmount = 1;
            person.OtherOrgDebts = 1;
            person.HomeSize = 1;
            person.HomeTimeLivingIn = 1;
            person.LandplotSize = 1;
            person.LivestockNumber = ERRORVALUE;

            clientServices.SavePerson(ref person);
        }

        [Test]
        [ExpectedException(typeof(OpenCBS.ExceptionsHandler.OpenCbsTiersSaveException))]
        public void TestSavePersonButDistrictIsNull()
        {
            Person person = AddPerson(true, new EconomicActivity(1, "tiki", null, false), 1, "city", null, "nicolas", "mangin", 'M', "12345", true);

            clientServices.SavePerson(ref person);
        }

        [Test]
        [ExpectedException(typeof(OpenCBS.ExceptionsHandler.OpenCbsTiersSaveException))]
        public void TestSavePersonButCityIsNull()
        {
            Person person = AddPerson(true, new EconomicActivity(1, "tiki", null, false), 1, null, new District(1, "district", new Province(1, "province")), "nicolas", "mangin", 'M', "12ED", true);

            clientServices.SavePerson(ref person);
        }

        [Test]
        [ExpectedException(typeof(OpenCBS.ExceptionsHandler.OpenCbsTiersSaveException))]
        public void TestSavePersonButFirstNameIsNull()
        {
            Person person = AddPerson(true, new EconomicActivity(1, "tiki", null, false), 1, "city", new District(1, "district", new Province(1, "province")), null, "mangin", 'M', "12345", true);

            clientServices.SavePerson(ref person);
        }

        [Test]
        [ExpectedException(typeof(OpenCBS.ExceptionsHandler.OpenCbsTiersSaveException))]
        public void TestSavePersonButSexIsNull()
        {
            Person person = AddPerson(true, new EconomicActivity(1, "tiki", null, false), 1, "city", new District(1, "district", new Province(1, "province")), "nicolas", "mangin", Convert.ToChar(0), "12345", true);

            clientServices.SavePerson(ref person);
        }

        [Test]
        [ExpectedException(typeof(OpenCBS.ExceptionsHandler.OpenCbsTiersSaveException))]
        public void TestSavePersonButIdentificationDataIsNull()
        {
            // Ru55
            Assert.Ignore();
            Person person = AddPerson(true, new EconomicActivity(1, "tiki", null, false), 1, "city", new District(1, "district", new Province(1, "province")), "nicolas", "mangin", 'M', null, true);

            clientServices.SavePerson(ref person);
        }
        [Test]
        [ExpectedException(typeof(OpenCBS.ExceptionsHandler.OpenCbsTiersSaveException))]
        public void TestSavePersonButIdentificationDataIsBad()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ENFORCE_ID_PATTERN, 1);
            Person person = AddPerson(true, new EconomicActivity(1, "tiki", null, false), 1, "city", new District(1, "district", new Province(1, "province")), "nicolas", "mangin", 'M', "1111111", true);

            clientServices.SavePerson(ref person);
        }
        [Test]
        [ExpectedException(typeof(OpenCBS.ExceptionsHandler.OpenCbsTiersSaveException))]
        public void TestSavePersonButLastNameIsNull()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ID_PATTERN, @"[0-9]{2}[A-Z]{2}");
            Person person = AddPerson(true, new EconomicActivity(1, "tiki", null, false), 1, "city", new District(1, "district", new Province(1, "province")), "nicolas", null, 'M', "12ED", true);

            clientServices.SavePerson(ref person);
        }

        [Test]
        [ExpectedException(typeof(OpenCBS.ExceptionsHandler.OpenCbsTiersSaveException))]
        public void TestSavePersonWhenSecondaryAddressNotNullButSecondaryCityIsNull()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ID_PATTERN, @"[0-9]{2}[A-Z]{2}");
            Person person = AddPerson(true, new EconomicActivity(1, "tiki", null, false), 1, "city", new District(1, "district", new Province(1, "province")), "nicolas", "MANGIN", 'M', "12ED", true);

            person.SecondaryDistrict = new District(1, "district", new Province(1, "province"));
            person.SecondaryAddress = "address";
            person.SecondaryCity = null;

            clientServices.SavePerson(ref person);
        }

        [Test]
        [ExpectedException(typeof(OpenCBS.ExceptionsHandler.OpenCbsTiersSaveException))]
        public void TestSavePersonWhenSecondaryAddressNotNullButSecondaryDistrictIsNull()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ID_PATTERN, @"[0-9]{2}[a-z]{2}");
            Person person = AddPerson(true, new EconomicActivity(1, "tiki", null, false), 1, "city", new District(1, "district", new Province(1, "province")), "nicolas", "MANGIN", 'M', "12aa", true);

            person.SecondaryDistrict = null;
            person.SecondaryCity = "city";
            person.SecondaryAddress = "address";

            clientServices.SavePerson(ref person);
        }
        [Test]
        [ExpectedException(typeof(OpenCBS.ExceptionsHandler.OpenCbsTiersSaveException))]
        public void TestSavePersonWhenDateOfBirthIsBad()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ID_PATTERN, @"[0-9]{2}[a-z]{2}");
            Person person = AddPerson(true, new EconomicActivity(1, "tiki", null, false), 1, "city", new District(1, "district", new Province(1, "province")), "nicolas", "MANGIN", 'M', "12aa", true);
            person.DateOfBirth = DateTime.Now.Date;
            clientServices.SavePerson(ref person);
        }
        private void DeleteAllData()
        {
            AddDataForTestingTransaction addDataForTesting = new AddDataForTestingTransaction();
         	addDataForTesting.DeleteSaving();
			addDataForTesting.DeleteCreditContract();
            addDataForTesting.DeleteTiers();
            addDataForTesting.DeleteCity();
            addDataForTesting.DeleteDistrict();
            addDataForTesting.DeleteProvince();
            addDataForTesting.DeleteCorporate();
            addDataForTesting.DeleteEconomicActivities();
        }

        [Test]
        public void TestSavePerson()
        {
            AddDataForTestingTransaction addDataForTesting = new AddDataForTestingTransaction();
            clientManagement = new ClientManager(DataUtil.TESTDB);
            clientServices = new ClientServices(clientManagement);

            DeleteAllData();

            EconomicActivity agriculture = addDataForTesting.AddDomainOfApplicationAgriculture();
            District district = addDataForTesting.AddDistrictIntoDatabase();
            ApplicationSettingsServices _appSettings= ServicesProvider.GetInstance().GetApplicationSettingsServices();
            _appSettings.FillGeneralDatabaseParameter();
            _appSettings.UpdateSelectedParameter(OGeneralSettings.ID_PATTERN, "[0-9]{2}[A-Z]{2}");
            _appSettings.UpdateSelectedParameter(OGeneralSettings.CITYMANDATORY, false);

            Person person = AddPerson(true, agriculture, 1, "Dushambe", district, "Nicolas", "MANGIN", 'M', "12ED", true);
            person.Branch = _branch;
            Assert.AreEqual(String.Empty, clientServices.SavePerson(ref person));
        }

        private Person AddPerson(bool active, EconomicActivity dOA, int loanCycle, string city, District district, string firstName, string lastname,
            char sex, string identificationData, bool houseHoldHead)
        {
            Person person = new Person();
            person.Active = active;
            person.Activity = dOA;
            person.City = city;
            person.District = district;
            person.FirstName = firstName;
            person.LastName = lastname;
            person.Sex = sex;
            person.LoanCycle = loanCycle;
            person.IdentificationData = identificationData;
            person.HouseHoldHead = houseHoldHead;
            person.DateOfBirth = new DateTime(1980, 12, 12);
            return person;
        }

        [Test]
        public void TestUpdatePerson()
        {
            AddDataForTestingTransaction addDataForTesting = new AddDataForTestingTransaction();
            clientManagement = new ClientManager(DataUtil.TESTDB);
            clientServices = new ClientServices(clientManagement);

            DeleteAllData();
            ApplicationSettingsServices _appSettings = ServicesProvider.GetInstance().GetApplicationSettingsServices();
            _appSettings.FillGeneralDatabaseParameter();
            _appSettings.UpdateSelectedParameter(OGeneralSettings.ID_PATTERN, "[0-9]{2}[A-Z]{2}");

            EconomicActivity agriculture = addDataForTesting.AddDomainOfApplicationAgriculture();
            District district = addDataForTesting.AddDistrictIntoDatabase();

            Person person = AddPerson(true, agriculture, 1, "Dushambe", district, "Nicolas", "MANGIN", 'M', "12ED", true);
            person.Branch = _branch;
            Assert.AreEqual(String.Empty, clientServices.SavePerson(ref person));

            person.FirstName = "floriane";
            person.IdentificationData = "34PP";
            Assert.AreEqual(String.Empty, clientServices.SavePerson(ref person));
        }

        [Test]
        [ExpectedException(typeof(OpenCBS.ExceptionsHandler.OpenCbsTiersSaveException))]
        public void TestIfClientIsAPerson()
        {
            Person person = new Person();
            Assert.IsTrue(clientServices.ClientIsAPerson(person));

            Group group = new Group();
            clientServices.ClientIsAPerson(group);
        }

        [Test]
        [ExpectedException(typeof(OpenCBS.ExceptionsHandler.OpenCbsTiersSaveException))]
        public void TestIfClientCanBeAddToAGroupWhenClientAlreadyInThisGroup()
        {
            Person person = new Person {Id = 1};
            Group group = new Group();
            group.AddMember(new Member { Tiers = person, LoanShareAmount = 1000, CurrentlyIn = true, IsLeader = true, JoinedDate = TimeProvider.Today });

            clientServices.ClientCanBeAddToAGroup(person, group);
        }

        [Test]
        [ExpectedException(typeof(OpenCBS.ExceptionsHandler.OpenCbsTiersSaveException))]
        public void TestIfClientCanBeAddToAGroupWhenClientIsActive()
        {
            Person person = new Person {Id = 1, Active = true};
            Group group = new Group();

            clientManagement = new ClientManager(DataUtil.TESTDB);
            clientServices = new ClientServices(clientManagement);

            clientServices.ClientCanBeAddToAGroup(person, group);
        }

        [Test]
        //[ExpectedException(typeof(OpenCBS.ExceptionsHandler.OpenCbsTiersSaveException))]
        public void TestIfClientCanBeAddToAGroupWhenClientIsALeader()
        {
            Person person = new Person {Id = 1, Active = false};
            Group group = new Group();

            mockClientManagement.ExpectAndReturn("IsLeader", true, person.Id);
            clientManagement = (ClientManager)mockClientManagement.MockInstance;
            clientServices = new ClientServices(clientManagement);

            clientServices.ClientCanBeAddToAGroup(person, group);
        }

        [Test]
        [ExpectedException(typeof(OpenCBS.ExceptionsHandler.OpenCbsTiersSaveException))]
        public void TestSaveGroupWhenGroupIsNull()
        {
            ApplicationSettings dataParam = ApplicationSettings.GetInstance("");
            dataParam = GetDataBaseParam(4, true);

            ClientServices services = new ClientServices(dataParam);
            Group group = null;

            services.SaveSolidarityGroup(ref group);
        }

        private static Group AddGroup(bool active, string city, District district, string name, Member leader, int NbOfMembers)
        {

            Group group = new Group {Active = active, City = city, District = district, Name = name, Leader = leader};

            for (int i = 0; i < NbOfMembers; i++)
            {
                group.AddMember(new Member { Tiers = new Person(), LoanShareAmount = 1000 + i, CurrentlyIn = true, IsLeader = true, JoinedDate = TimeProvider.Today });
            }
            return group;
        }


        private static ApplicationSettings GetDataBaseParam(int groupMinMembers, bool cityMandatory)
        {
            ApplicationSettings dataParam = ApplicationSettings.GetInstance("");
            dataParam.DeleteAllParameters();
            dataParam.AddParameter(OGeneralSettings.GROUPMINMEMBERS, 4);
            dataParam.AddParameter(OGeneralSettings.GROUPMAXMEMBERS, 10);
            dataParam.AddParameter(OGeneralSettings.CITYMANDATORY, true);
            return dataParam;
        }

        [Test]
        [ExpectedException(typeof(OpenCBS.ExceptionsHandler.OpenCbsTiersSaveException))]
        public void TestSaveGroupWhenGroupNameIsNull()
        {
            ApplicationSettings dataParam = GetDataBaseParam(4, true);

            ClientServices services = new ClientServices(dataParam);
            Member leader = new Member {Tiers = {Id = 1}};
            Group group = AddGroup(true, "Dushambe", new District(1, "district", new Province(1, "province")), null, leader, 4);

            services.SaveSolidarityGroup(ref group);
        }

        [Test]
        [ExpectedException(typeof(OpenCBS.ExceptionsHandler.OpenCbsTiersSaveException))]
        public void TestSaveGroupWhenOtherOrganizationAmountsIsBadlyInformed()
        {
            ApplicationSettings dataParam = GetDataBaseParam(4, true);

            ClientServices services = new ClientServices(dataParam);

            Member leader = new Member {Tiers = {Id = 1}};
            Group group = AddGroup(true, "Dushambe", new District(1, "district", new Province(1, "province")), "Nicolas", leader, 4);
            group.OtherOrgAmount = ERRORVALUE;

            services.SaveSolidarityGroup(ref group);
        }

        [Test]
        [ExpectedException(typeof(OpenCBS.ExceptionsHandler.OpenCbsTiersSaveException))]
        public void TestSaveGroupWhenOtherOrganizationDebtsIsBadlyInformed()
        {
            ApplicationSettings dataParam = GetDataBaseParam(4, true);

            ClientServices services = new ClientServices(dataParam);

            Member leader = new Member {Tiers = {Id = 1}};
            Group group = AddGroup(true, "Dushambe", new District(1, "district", new Province(1, "province")), "Nicolas", leader, 4);
            group.OtherOrgAmount = 100;
            group.OtherOrgDebts = ERRORVALUE;

            services.SaveSolidarityGroup(ref group);
        }

        [Test]
        [ExpectedException(typeof(OpenCBS.ExceptionsHandler.OpenCbsTiersSaveException))]
        public void TestSaveGroupWhenDistrictIsNull()
        {
            ApplicationSettings dataParam = GetDataBaseParam(4, true);

            ClientServices services = new ClientServices(dataParam);

            Member leader = new Member {Tiers = {Id = 1}};
            Group group = AddGroup(true, "Dushambe", null, "Nicolas", leader, 4);

            services.SaveSolidarityGroup(ref group);
        }

        [Test]
        [ExpectedException(typeof(OpenCBS.ExceptionsHandler.OpenCbsTiersSaveException))]
        public void TestSaveGroupWhenNoEnoughMembers()
        {
            ApplicationSettings dataParam = GetDataBaseParam(4, true);

            ClientServices services = new ClientServices(dataParam);
            Member leader = new Member {Tiers = {Id = 1}};
            Group group = AddGroup(true, "Dushambe", new District(1, "district", new Province(1, "province")), "Nicolas", leader, 0);

            services.SaveSolidarityGroup(ref group);
        }


        [Test]
        [ExpectedException(typeof(OpenCBS.ExceptionsHandler.OpenCbsTiersSaveException))]
        public void TestSaveGroupWhenCityIsNullButMandatory()
        {
            ApplicationSettings dataParam = GetDataBaseParam(4, true);

            ClientServices services = new ClientServices(dataParam);
            Member leader = new Member {Tiers = {Id = 1}};
            Group group = AddGroup(true, null, new District(1, "district", new Province(1, "province")), "Nicolas", leader, 4);

            services.SaveSolidarityGroup(ref group);
        }

        [Test]
        [ExpectedException(typeof(OpenCBS.ExceptionsHandler.OpenCbsTiersSaveException))]
        public void TestSaveGroupWhenLeaderIsNull()
        {
            ApplicationSettings dataParam = GetDataBaseParam(4, true);

            ClientServices services = new ClientServices(dataParam);
            Group group = AddGroup(true, "Dushambe", new District(1, "district", new Province(1, "province")), "Nicolas", null, 4);

            services.SaveSolidarityGroup(ref group);
        }

        [Test]
        public void TestSaveGroupWhenCityIsNotMandatory()
        {
            AddDataForTestingTransaction addDataForTesting = new AddDataForTestingTransaction();
            clientManagement = new ClientManager(DataUtil.TESTDB);
            clientServices = new ClientServices(clientManagement);
            branchManager = new BranchManager(DataUtil.TESTDB);
            //List<Branch> branchs = branchManager.SelectAll();

            ApplicationSettings dataParam = ApplicationSettings.GetInstance("");
            dataParam.DeleteAllParameters();
            dataParam.AddParameter(OGeneralSettings.GROUPMINMEMBERS, 4);
            dataParam.AddParameter(OGeneralSettings.GROUPMAXMEMBERS, 10);
            dataParam.AddParameter(OGeneralSettings.CITYMANDATORY, false);
            DeleteAllData();

            Person leader = addDataForTesting.AddPerson();
            Person members = addDataForTesting.AddPersonBis();
            Person membersTer = addDataForTesting.AddPersonTer();
            Person membersQuater = addDataForTesting.AddPersonQuater();
            Group group = new Group
                              {
                                  Active = true,
                                  City = null,
                                  District = leader.District,
                                  Name = "SCG",
                                  Leader = new Member { Tiers = leader, LoanShareAmount = 1000, CurrentlyIn = true, IsLeader = true, JoinedDate = TimeProvider.Today },
                                  LoanCycle = 3,
                                  Branch = _branch
                              };

            group.AddMember(new Member { Tiers = members, LoanShareAmount = 200, CurrentlyIn = true, IsLeader = false, JoinedDate = TimeProvider.Today });
            group.AddMember(new Member { Tiers = membersTer, LoanShareAmount = 400, CurrentlyIn = true, IsLeader = false, JoinedDate = TimeProvider.Today });
            group.AddMember(new Member { Tiers = membersQuater, LoanShareAmount = 700, CurrentlyIn = true, IsLeader = false, JoinedDate = TimeProvider.Today });

            Assert.AreEqual(String.Empty, clientServices.SaveSolidarityGroup(ref group));
        }


        [Test]
        public void TestSaveGroup()
        {
            AddDataForTestingTransaction addDataForTesting = new AddDataForTestingTransaction();
            clientManagement = new ClientManager(DataUtil.TESTDB);
            clientServices = new ClientServices(clientManagement);
            branchManager = new BranchManager(DataUtil.TESTDB);
            //List<Branch> branchs = branchManager.SelectAll();

            DeleteAllData();
            Person leader = addDataForTesting.AddPerson();
            Person members = addDataForTesting.AddPersonBis();
            Person membersTer = addDataForTesting.AddPersonTer();
            Person membersQuater = addDataForTesting.AddPersonQuater();
            Group group = new Group
                              {
                                  Active = true,
                                  City = "Dushambe",
                                  District = leader.District,
                                  Name = "SCG",
                                  LoanCycle = 4,
                                  Leader = new Member { Tiers = leader, LoanShareAmount = 1000, CurrentlyIn = true, IsLeader = true, JoinedDate = TimeProvider.Today },
                                  Branch = _branch
                              };
            group.AddMember(new Member { Tiers = members, LoanShareAmount = 200, CurrentlyIn = true, IsLeader = false, JoinedDate = TimeProvider.Today });
            group.AddMember(new Member { Tiers = membersTer, LoanShareAmount = 400, CurrentlyIn = true, IsLeader = false, JoinedDate = TimeProvider.Today });
            group.AddMember(new Member { Tiers = membersQuater, LoanShareAmount = 700, CurrentlyIn = true, IsLeader = false, JoinedDate = TimeProvider.Today });

            Assert.AreEqual(String.Empty, clientServices.SaveSolidarityGroup(ref group));
        }

        [Test]
        public void TestUpdateGroup()
        {
            AddDataForTestingTransaction addDataForTesting = new AddDataForTestingTransaction();
            clientManagement = new ClientManager(DataUtil.TESTDB);
            clientServices = new ClientServices(clientManagement);

            DeleteAllData();

            Person leader = addDataForTesting.AddPerson();
            Person members = addDataForTesting.AddPersonBis();
            Person membersTer = addDataForTesting.AddPersonTer();
            Person membersQuater = addDataForTesting.AddPersonQuater();
            Group group = new Group
                              {
                                  Active = true,
                                  City = "Dushambe",
                                  District = leader.District,
                                  LoanCycle = 2,
                                  Name = "SCG",
                                  Leader =new Member{Tiers = leader,LoanShareAmount = 1000,CurrentlyIn = true,IsLeader = true,JoinedDate = TimeProvider.Today},
                                  Branch = _branch
                              };

            group.AddMember(new Member { Tiers = members, LoanShareAmount = 200, CurrentlyIn = true, IsLeader = false, JoinedDate = TimeProvider.Today });
            group.AddMember(new Member { Tiers = membersTer, LoanShareAmount = 400, CurrentlyIn = true, IsLeader = false, JoinedDate = TimeProvider.Today });
            group.AddMember(new Member { Tiers = membersQuater, LoanShareAmount = 700, CurrentlyIn = true, IsLeader = false, JoinedDate = TimeProvider.Today });

            Assert.AreEqual(String.Empty, clientServices.SaveSolidarityGroup(ref group));

            group.Name = "SCG2";
            Assert.AreEqual(String.Empty, clientServices.SaveSolidarityGroup(ref group));
        }
   
        [TestFixtureTearDown]
        public void TextFixtureTearDown()
        {
            DeleteAllData();
        }
    }
}
