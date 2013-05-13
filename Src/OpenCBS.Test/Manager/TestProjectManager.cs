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
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.CoreDomain.FundingLines;
using OpenCBS.Enums;
using OpenCBS.Manager;
using OpenCBS.Shared;

namespace OpenCBS.Test.Manager
{
    [TestFixture]
    public class TestProjectManager
    {
        

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {

            ProvisioningTable.GetInstance(new User()).AddProvisioningRate(new ProvisioningRate(1, 0, 1000, 0));
           
            new DataHelper().DeleteInstallments();
            new DataHelper().DeleteCreditContract();
            new DataHelper().DeletedProject();
            new DataHelper().DeleteTiers();
            new DataHelper().DeleteInstallmentTypes();
            new DataHelper().DeletePackage();
            new DataHelper().DeleteAllUser();
        }

        [TearDown]
        public void TearDown()
        {
            new DataHelper().DeleteInstallments();
            new DataHelper().DeleteCreditContract();
            new DataHelper().DeletedProject();
            new DataHelper().DeleteTiers();
            new DataHelper().DeleteInstallmentTypes();
            new DataHelper().DeletePackage();
            new DataHelper().DeleteAllUser();
        }

        [Test]
        public void AddProjectAndSelectProjectById()
        {
            Project project = new Project();
            project.ProjectStatus = OProjectStatus.Refused;
            project.Code = "TEST";
            project.Name = "PROJECT";
            project.Aim = "Not Set";
            project.BeginDate = TimeProvider.Today;
            Person person = new Person();
            person.Id = new DataHelper().AddGenericTiersIntoDatabase(OClientTypes.Person);

            project.Credits.Add(_AddCredit());
            

            ProjectManager projectManager = new ProjectManager(DataUtil.TESTDB);
          
            int projectId = projectManager.AddProject(project,person.Id,null);

            Project selectedProject = projectManager.SelectProject(projectId);

            Assert.AreEqual(projectId, selectedProject.Id);
            Assert.AreEqual("TEST", selectedProject.Code);
            Assert.AreEqual("PROJECT", selectedProject.Name);
            Assert.AreEqual("Not Set", selectedProject.Aim);
            Assert.AreEqual(OProjectStatus.Refused, selectedProject.ProjectStatus);
            Assert.AreEqual(1,selectedProject.Credits.Count);
            Assert.AreEqual(TimeProvider.Today,selectedProject.BeginDate);
        }
        
        private static Credit _AddCredit()
        {
            GeneralSettings.GetInstance("").DeleteAllParameters();
            GeneralSettings.GetInstance("").AddParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Cash);
            GeneralSettings.GetInstance("").AddParameter(OGeneralSettings.USECENTS, true);
            FundingLineManager _fundingLineManager = new FundingLineManager(DataUtil.TESTDB);
            FundingLine fund = new FundingLine();
            fund.Purpose = "Microsoft financement";
            fund.Name = "AFD130";
            fund.Deleted = false;
            fund.StartDate = DateTime.Now;
            fund.EndDate = DateTime.Now;
            _fundingLineManager.AddFundingLine(fund, null);
            Package _package = new Package();
            _package.Name = "Package";
            _package.Delete = false;
            _package.LoanType = OLoanTypes.Flat;
            _package.ClientType = 'G';
            _package.ChargeInterestWithinGracePeriod = true;
            _package.InstallmentType = new DataHelper().AddBiWeeklyInstallmentType();
            _package.FundingLine = fund;
            _package.Id = new ProductManager(DataUtil.TESTDB).AddPackage(_package);
            User user = new User();
            user.Id = new DataHelper().AddUserWithIntermediaryAttributs();

            Credit credit =
                new Credit(_package, 1000, 3, 6, 0, new DateTime(2006, 1, 1), user, GeneralSettings.GetInstance(""),
                           NonWorkingDateSingleton.GetInstance(""),ProvisioningTable.GetInstance(user));
            credit.CreationDate = DateTime.Today.AddDays(-1);
            credit.StartDate = DateTime.Today;
            credit.LoanOfficer = user;
            credit.CloseDate = DateTime.Today.AddDays(1);
            credit.BranchCode = "CA";

           
            credit.FundingLine = fund;
            //new DataHelper().AddGenericFundingLine();
            credit.Code = "TEST";

            return credit;
        }

        [Test]
        public void UpdateProject()
        {
            Project project = new Project();
            project.ProjectStatus = OProjectStatus.Refused;
            project.Code = "TEST";
            project.Name = "PROJECT";
            project.Aim = "Not Set";
            project.BeginDate = TimeProvider.Today;

            Person person = new Person();
            person.Id = new DataHelper().AddGenericTiersIntoDatabase(OClientTypes.Person);

            ProjectManager projectManager = new ProjectManager(DataUtil.TESTDB);
            project.Id = projectManager.AddProject(project, person.Id,null);

            project.ProjectStatus = OProjectStatus.Refused;
            project.Code = "TEST2";
            project.Name = "PROJECT2";
            project.Aim = "Not Set2";
            project.BeginDate = TimeProvider.Today.AddDays(1);

            projectManager.UpdateProject(project,null);

            Project selectedProject = projectManager.SelectProject(project.Id);

            Assert.AreEqual(OProjectStatus.Refused, selectedProject.ProjectStatus);
            Assert.AreEqual("TEST2", selectedProject.Code);
            Assert.AreEqual("PROJECT2", selectedProject.Name);
            Assert.AreEqual("Not Set2", selectedProject.Aim);
            Assert.AreEqual(TimeProvider.Today.AddDays(1), selectedProject.BeginDate);
        }

        [Test]
        public void SelectFiewProjectsByClientId()
        {
            Project project1 = new Project();
            project1.ProjectStatus = OProjectStatus.Refused;
            project1.Code = "TEST";
            project1.Name = "PROJECT";
            project1.Aim = "Not Set";
            project1.BeginDate = TimeProvider.Today;
            Project project2 = new Project();
            project2.ProjectStatus = OProjectStatus.Refused;
            project2.Code = "TEST";
            project2.Name = "PROJECT";
            project2.Aim = "Not Set";
            project2.BeginDate = TimeProvider.Today;
            Project project3 = new Project();
            project3.ProjectStatus = OProjectStatus.Refused;
            project3.Code = "TEST";
            project3.Name = "PROJECT";
            project3.Aim = "Not Set";
            project3.BeginDate = TimeProvider.Today;


            Person person = new Person();
            person.Id = new DataHelper().AddGenericTiersIntoDatabase(OClientTypes.Person);
            
            ProjectManager projectManager = new ProjectManager(DataUtil.TESTDB);

            project1.Id = projectManager.AddProject(project1, person.Id, null);
            project2.Id = projectManager.AddProject(project2, person.Id, null);
            project3.Id = projectManager.AddProject(project3, person.Id, null);
            
            List<Project> list = projectManager.SelectProjectsByClientId(person.Id);

            Assert.AreEqual(3,list.Count);
        }

        [Test]
        public void SelectUniqueProjectsByClientId()
        {
            Project project = new Project();
            project.ProjectStatus = OProjectStatus.Refused;
            project.Code = "TEST";
            project.Name = "PROJECT";
            project.Aim = "Not Set";
            project.BeginDate = TimeProvider.Today;
            Person person = new Person();
            person.Id = new DataHelper().AddGenericTiersIntoDatabase(OClientTypes.Person);

            project.Credits.Add(_AddCredit());

            ProjectManager projectManager = new ProjectManager(DataUtil.TESTDB);

            project.Id = projectManager.AddProject(project, person.Id, null);
            List<Project> list = projectManager.SelectProjectsByClientId(person.Id);

            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(OProjectStatus.Refused, list[0].ProjectStatus);
            Assert.AreEqual(1, list[0].Credits.Count);
        }

        [Test]
        public void SelectNullProjectsByClientId()
        {
            Person person = new Person();
            person.Id = new DataHelper().AddGenericTiersIntoDatabase(OClientTypes.Person);

            ProjectManager projectManager = new ProjectManager(DataUtil.TESTDB);
            List<Project> list = projectManager.SelectProjectsByClientId(person.Id);

            Assert.AreEqual(0, list.Count);
        }
    }
}
