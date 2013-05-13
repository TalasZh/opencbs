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

using OpenCBS.Enums;
using OpenCBS.Manager;
using OpenCBS.CoreDomain;
using NUnit.Framework;

namespace OpenCBS.Test.Manager
{
    // Users are added in the Init.sql script

	[TestFixture]
	public class TestUserManager : BaseManagerTest
	{
	    private UserManager _userManager;

        [SetUp]
        protected override void SetUp()
        {
            base.SetUp();
            _userManager = (UserManager)container["UserManager"];
        }
        
        private static void AssertSelectedUser(User pUserToTest, string pExpectedUserName, User.Roles pExpectedRole, string pExpectedFirstName, string pExpectedLastName, char pExpectedSex)
        {
            Assert.AreEqual(pExpectedUserName, pUserToTest.UserName);
            Assert.IsNull(pUserToTest.Password);
            Assert.AreEqual(pExpectedRole, pUserToTest.Role);
            Assert.AreEqual(pExpectedFirstName, pUserToTest.FirstName);
            Assert.AreEqual(pExpectedLastName,pUserToTest.LastName);
            Assert.AreEqual(pExpectedSex, pUserToTest.Sex);
        }

        const int AzamUserId = 3;

		[Test]
		public void UpdateUser()
        {		    
		    User user = _userManager.SelectUser(AzamUserId, false);
            AssertSelectedUser(user, "azam", User.Roles.LOF, null, null, OGender.Male);

            user.FirstName = "maman";
            user.LastName = "maman";
            user.Password = "coucou";
		    user.Sex = OGender.Female;
            _userManager.UpdateUser(user);

            User updatedUser = _userManager.SelectUser(AzamUserId, false);
            AssertSelectedUser(updatedUser, "azam", User.Roles.LOF, "maman", "maman", OGender.Female);
        }

		[Test]
		public void DeleteRussianUser()
        {
            User user = _userManager.SelectUser(AzamUserId, false);
            Assert.AreEqual("azam", user.UserName);
			_userManager.DeleteUser(user);
            User deleteUser = _userManager.SelectUser(AzamUserId, false);
            Assert.IsNull(deleteUser);
		}
	}
}
