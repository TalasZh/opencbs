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
