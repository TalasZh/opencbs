
using System;
using NUnit.Framework;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.CoreDomain.Contracts.Loans;
using OpenCBS.Enums;
using OpenCBS.Shared;
using OpenCBS.Shared.Settings;

namespace OpenCBS.Test.CoreDomain.Contracts
{
    [TestFixture]
    public class TestProject
    {
        private readonly Project _project = new Project();

        [Test]
        public void Get_Set_Id()
        {
            _project.Id = 3;
            Assert.AreEqual(3,_project.Id);
        }

        [Test]
        public void Get_Set_ProjectStatus()
        {
            _project.ProjectStatus = OProjectStatus.Abandoned;
            Assert.AreEqual(OProjectStatus.Abandoned,_project.ProjectStatus);
        }

        [Test]
        public void Get_Set_Credits()
        {
            _project.AddCredit(new Loan(new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User())),OClientTypes.Group);
            Assert.AreEqual(1, _project.Credits.Count);

            Assert.AreEqual(OClientTypes.Group, _project.Credits[0].ClientType);
        }

        [Test]
        public void Get_Set_Name()
        {
            _project.Name = "TEST";
            Assert.AreEqual("TEST", _project.Name);
        }

        [Test]
        public void Get_Set_Code()
        {
            _project.Code = "TEST2";
            Assert.AreEqual("TEST2", _project.Code);
        }

        [Test]
        public void Get_Set_Aim()
        {
            _project.Aim = "TEST3";
            Assert.AreEqual("TEST3", _project.Aim);
        }

        [Test]
        public void Get_Set_Market()
        {
            _project.Market = "TEST3";
            Assert.AreEqual("TEST3", _project.Market);
        }

        [Test]
        public void Get_Set_Concurrence()
        {
            _project.Concurrence = "TEST3";
            Assert.AreEqual("TEST3", _project.Concurrence);
        }

        [Test]
        public void Get_Set_Experience()
        {
            _project.Experience = "TEST3";
            Assert.AreEqual("TEST3", _project.Experience);
        }

        [Test]
        public void Get_Set_Abilities()
        {
            _project.Abilities = "TEST3";
            Assert.AreEqual("TEST3", _project.Abilities);
        }

        [Test]
        public void Get_Set_Purpose()
        {
            _project.Purpose = "TEST3";
            Assert.AreEqual("TEST3", _project.Purpose);
        }

        [Test]
        public void Get_Set_BeginDate()
        {
            _project.BeginDate = new DateTime(2006,1,1);
            Assert.AreEqual(new DateTime(2006, 1, 1), _project.BeginDate);
        }

        //[Test]
        //public void TestGenerateProjectCodeWithPackageNameLenghtLowerThan4()
        //{
        //    Credit pContract = new Credit(new User(), GeneralSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisioningTable.GetInstance(new User())); new Credit(new User(), GeneralSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisioningTable.GetInstance(new User()));
        //    Project aProject = new Project();
        //    aProject.ClientCode = "1/4";
        //    aProject.Id = 42;
        //    aProject.BeginDate = new DateTime(2008, 7, 18);
        //    GeneralSettings param;
        //    User user = new User();
        //    param = GeneralSettings.GetInstance(user.Md5);
        //    //Person person = new Person();
        //    //person.LoanCycle = 1;
            

        //    Assert.AreEqual(param.BranchCode+"/08/1/4",aProject.GenerateProjectCode());
        //}
    }
}
