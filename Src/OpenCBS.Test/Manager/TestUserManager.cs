// LICENSE PLACEHOLDER

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
