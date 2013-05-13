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

using OpenCBS.CoreDomain;
using NUnit.Framework;
using OpenCBS.Enums;

namespace OpenCBS.Test.CoreDomain
{
	[TestFixture]
	public class TestUser
	{
		private User _user;

		[SetUp]
		public void SetUp()
		{
			_user = new User();
		}

		[Test]
		public void Id()
		{
			_user.Id = 1;
			Assert.AreEqual(1,_user.Id);
		}

		[Test]
		public void UserName()
		{
			_user.UserName = "Nicolas";
			Assert.AreEqual("Nicolas",_user.UserName);
		}

		[Test]
		public void Password()
		{
			_user.Password = "toto";
			Assert.AreEqual("toto",_user.Password);
		}
		
		[Test]
		public void FirstName()
		{
			_user.FirstName = "Nicolas";
			Assert.AreEqual("Nicolas",_user.FirstName);
		}

		[Test]
		public void LastName()
		{
			_user.LastName = "BARON";
			Assert.AreEqual("BARON",_user.LastName);
		}

        [Test]
        public void InitialSexIsMale()
        {
            Assert.AreEqual(OGender.Male, _user.Sex);
        }

        [Test]
        public void User_ToString()
        {
            _user.FirstName = "Jojo";
            _user.LastName = "LAPIN";
            Assert.AreEqual(_user.ToString(), "Jojo LAPIN");
        }
	}
}
