
using NUnit.Framework;
using Octopus.CoreDomain;

namespace Octopus.Test.CoreDomain
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
