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

using NUnit.Framework;
using NUnit.Mocks;
using OpenCBS.Manager;
using OpenCBS.Services;
using OpenCBS.CoreDomain;
using OpenCBS.ExceptionsHandler;

namespace OpenCBS.Test.Services
{
    [TestFixture]
    public class TestMFIServices
    {
        private DynamicMock _mockMFIManagement;

        [SetUp]
        public void SetUp()
        {
            _mockMFIManagement = new DynamicMock(typeof(MFIManager));
        }

        private static MFIServices _SetMockManager(DynamicMock pDynamicMock)
        {
            MFIManager mfiManager = (MFIManager)pDynamicMock.MockInstance;
            return new MFIServices(mfiManager);
        }       

        [Test]
        public void TestSelectMFI()
        {
            MFI mfi = new MFI { Name = "MFI", Login = "Login", Password = "Password" };
            MFI returnedMFI = new MFI();

            _mockMFIManagement.SetReturnValue("SelectMFI", mfi);

            MFIServices mfiServices = _SetMockManager(_mockMFIManagement);
            returnedMFI = mfiServices.FindMFI();

            Assert.AreEqual("MFI", returnedMFI.Name);
            Assert.AreEqual("Login", returnedMFI.Login);
            Assert.AreEqual("Password", returnedMFI.Password);
        }

        [Test]
        public void TestSelectMFIWithNoMFI()
        {
            MFI mfi = new MFI();
            MFI returnedMFI = new MFI();

            _mockMFIManagement.SetReturnValue("SelectMFI", mfi);

            MFIServices mfiServices = _SetMockManager(_mockMFIManagement);
            returnedMFI = mfiServices.FindMFI();

            Assert.AreEqual(null, returnedMFI.Name);
            Assert.AreEqual(null, returnedMFI.Login);
            Assert.AreEqual(null, returnedMFI.Password);
        }

        [Test]
        public void TestCreateMFISuccess()
        {
            MFI mfi = new MFI { Name = "MFI", Login = "Login", Password = "Password" };

            _mockMFIManagement.ExpectAndReturn("CreateMFI",true, mfi);
            MFIServices mfiServices = _SetMockManager(_mockMFIManagement);
            bool result = mfiServices.CreateMFI(mfi);
            Assert.IsTrue(result);
        }

        [Test]
        public void TestCreateMFIFailure()
        {
            MFI mfi = new MFI { Name = "MFI", Login = "Login", Password = "Password" };

            _mockMFIManagement.ExpectAndReturn("CreateMFI", false, mfi);
            MFIServices mfiServices = _SetMockManager(_mockMFIManagement);
            bool result = mfiServices.CreateMFI(mfi);
            Assert.IsFalse(result);
        }

        [Test]
        [ExpectedException(typeof(OpenCbsMfiExceptions))]
        public void TestCreateMFINameIsEmpty()
        {
            MFI mfi = new MFI { Name = "", Login = "Login", Password = "Password" };

            _mockMFIManagement.Expect("CreateMFI", mfi);
            MFIServices mfiServices = _SetMockManager(_mockMFIManagement);
            mfiServices.CreateMFI(mfi);
        }

        [Test]
        [ExpectedException(typeof(OpenCbsMfiExceptions))]
        public void TestCreateMFILoginIsEmptyAndPasswordIsFilled()
        {
            MFI mfi = new MFI { Name = "MFI", Login = "", Password = "Password" };

            _mockMFIManagement.Expect("CreateMFI", mfi);
            MFIServices mfiServices = _SetMockManager(_mockMFIManagement);
            mfiServices.CreateMFI(mfi);
        }

        [Test]
        [ExpectedException(typeof(OpenCbsMfiExceptions))]
        public void TestCreateMFILoginIsFilledAndPasswordIsEmpty()
        {
            MFI mfi = new MFI { Name = "MFI", Login = "Login", Password = "" };

            _mockMFIManagement.Expect("CreateMFI", mfi);
            MFIServices mfiServices = _SetMockManager(_mockMFIManagement);
            mfiServices.CreateMFI(mfi);
        }

        [Test]
        public void TestUpdateMFISuccess()
        {
            MFI mfi = new MFI { Name = "MFI", Login = "Login", Password = "Password" };

            _mockMFIManagement.ExpectAndReturn("UpdateMFI", true, mfi);
            MFIServices mfiServices = _SetMockManager(_mockMFIManagement);
            bool result = mfiServices.UpdateMFI(mfi);
            Assert.IsTrue(result);
        }

        [Test]
        public void TestUpdateMFIFailure()
        {
            MFI mfi = new MFI { Name = "MFI", Login = "Login", Password = "Password" };

            _mockMFIManagement.ExpectAndReturn("UpdateMFI", false, mfi);
            MFIServices mfiServices = _SetMockManager(_mockMFIManagement);
            bool result = mfiServices.UpdateMFI(mfi);
            Assert.IsFalse(result);
        }

        [Test]
        [ExpectedException(typeof(OpenCbsMfiExceptions))]
        public void TestUpdateMFINameIsEmpty()
        {
            MFI mfi = new MFI { Name = "", Login = "Login", Password = "Password" };

            _mockMFIManagement.Expect("UpdateMFI", mfi);
            MFIServices mfiServices = _SetMockManager(_mockMFIManagement);
            mfiServices.UpdateMFI(mfi);
        }

        [Test]
        [ExpectedException(typeof(OpenCbsMfiExceptions))]
        public void TestUpdateMFILoginIsEmptyAndPasswordIsFilled()
        {
            MFI mfi = new MFI { Name = "MFI", Login = "", Password = "Password" };

            _mockMFIManagement.Expect("UpdateMFI", mfi);
            MFIServices mfiServices = _SetMockManager(_mockMFIManagement);
            mfiServices.UpdateMFI(mfi);
        }

        [Test]
        [ExpectedException(typeof(OpenCbsMfiExceptions))]
        public void TestUpdateMFILoginIsFilledAndPasswordIsEmpty()
        {
            MFI mfi = new MFI { Name = "MFI", Login = "Login", Password = "" };

            _mockMFIManagement.Expect("UpdateMFI", mfi);
            MFIServices mfiServices = _SetMockManager(_mockMFIManagement);
            mfiServices.UpdateMFI(mfi);
        }

        [Test]
        public void TestCheckIfSamePasswordSuccess()
        {
            MFIServices mfiServices = new MFIServices(User.CurrentUser);

            bool result = mfiServices.CheckIfSamePassword("Password","Password");

            Assert.IsTrue(result);
        }

        [Test]
        [ExpectedException(typeof(OpenCbsMfiExceptions))]
        public void TestCheckIfSamePasswordFailure()
        {
            MFIServices mfiServices = new MFIServices(User.CurrentUser);

           mfiServices.CheckIfSamePassword("Password", "DifferentPassword");
        }
    }
}
