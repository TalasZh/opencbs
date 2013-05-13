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

namespace OpenCBS.Test.CoreDomain.Contracts
{
    [TestFixture]
    public class TestCity
    {
        [Test]
        public void Get_Set_District()
        {
            City city = new City {DistrictId = 1};
            Assert.AreEqual(1,city.DistrictId);
        }

        [Test]
        public void Get_Set_Id()
        {
            City city = new City { Id = 1 };
            Assert.AreEqual(1, city.Id);
        }

        [Test]
        public void Get_Set_Name()
        {
            City city = new City { Name = "Paris" };
            Assert.AreEqual("Paris", city.Name);
        }

        [Test]
        public void Get_Set_Deleted()
        {
            City city = new City { Deleted = true };
            Assert.AreEqual(true, city.Deleted);
        }

        [Test]
        public void Get_Set_ToString()
        {
            City city = new City { Name = "paris" };
            Assert.AreEqual("paris", city.ToString());
        }
    }
}
