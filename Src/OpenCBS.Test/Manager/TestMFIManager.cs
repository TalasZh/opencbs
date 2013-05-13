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
using OpenCBS.Manager;
using OpenCBS.CoreDomain;

namespace OpenCBS.Test.Manager
{
    [TestFixture]
    public class TestMFIManager
    {
        private MFIManager _mfiManager;
        private MFI _mfi;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _mfiManager = new MFIManager(DataUtil.TESTDB);
            _mfi = new MFI();
        }

        [SetUp]
        public void SetUp()
        {
            _mfi.Name = "MFI";
            _mfi.Login = "Login";
            _mfi.Password = "Password";
        }

        [TearDown]
        public void TearDown()
        {
            _mfiManager.DeleteMFI();
        }

        [Test]
        public void TestCreateMFISuccess()
        {
            _mfiManager.DeleteMFI();

            bool result = _mfiManager.CreateMFI(_mfi);

            Assert.IsTrue(result);
        }

        [Test]
        public void TestCreateMFIFailure()
        {
            _mfiManager.CreateMFI(_mfi);

            bool result = _mfiManager.CreateMFI(_mfi);

            Assert.IsFalse(result);
        }

        [Test]
        public void TestAddAndSelectMFI()
        {
            _mfiManager.CreateMFI(_mfi);
            Assert.IsNotNull(_mfiManager.SelectMFI());
        }

        [Test]
        public void TestSelectMFIWhenNoMFI()
        {
            MFI returnedMFI = new MFI();

            returnedMFI = _mfiManager.SelectMFI();

            Assert.AreEqual(null, returnedMFI.Login);
            Assert.AreEqual(null, returnedMFI.Password);
            Assert.AreEqual(null, returnedMFI.Name);
        }

        [Test]
        public void TestSelectMFI()
        {
            MFI returnedMFI = new MFI();

            _mfiManager.CreateMFI(_mfi);
            returnedMFI = _mfiManager.SelectMFI();

            Assert.AreEqual(_mfi.Login, returnedMFI.Login);
            Assert.AreEqual(_mfi.Name, returnedMFI.Name);
            Assert.AreEqual(_mfi.Password, returnedMFI.Password);
        }


        [Test]
        public void TestCreateAndUpdateMFISuccess()
        {
            MFI returnedMFI = new MFI();
            MFI newMFI = new MFI { Name = "UpdatedName", Password = "UpdatedPassword", Login = "UpdatedLogin" };

            _mfiManager.CreateMFI(_mfi);

            bool result = _mfiManager.UpdateMFI(newMFI);

            Assert.IsTrue(result);

            returnedMFI = _mfiManager.SelectMFI();

            Assert.AreEqual(newMFI.Login, returnedMFI.Login);
            Assert.AreEqual(newMFI.Name, returnedMFI.Name);
            Assert.AreEqual(newMFI.Password, returnedMFI.Password);
        }

        [Test]
        public void TestUpdateMFIFailure()
        {
            bool result = _mfiManager.UpdateMFI(_mfi);

            Assert.IsFalse(result);
        }

        [Test]
        public void TestCreateAndDeleteMFI()
        {
            MFI returnedMFI = new MFI();

            _mfiManager.CreateMFI(_mfi);
            _mfiManager.DeleteMFI();
            returnedMFI = _mfiManager.SelectMFI();

            Assert.AreEqual(null, returnedMFI.Login);
            Assert.AreEqual(null, returnedMFI.Password);
            Assert.AreEqual(null, returnedMFI.Name);
        }
    }
}
