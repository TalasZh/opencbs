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
using OpenCBS.CoreDomain;

namespace OpenCBS.Test.CoreDomain
{
    [TestFixture]
    public class TestMFI
    {
        private MFI mfi;

        [SetUp]
        public void SetUp()
        {
            mfi = new MFI();
        }

        [Test]
        public void Get_MFIName()
        {
            mfi.Name = "MFI";
            Assert.AreEqual("MFI", mfi.Name);
        }

        [Test]
        public void Get_MFIPassword()
        {
            mfi.Password = "Password";
            Assert.AreEqual("Password", mfi.Password);
        }

        [Test]
        public void Get_MFILogin()
        {
            mfi.Login = "TestMFI";
            Assert.AreEqual("TestMFI", mfi.Login);
        }

    }
}
