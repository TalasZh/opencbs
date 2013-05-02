// LICENSE PLACEHOLDER

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
