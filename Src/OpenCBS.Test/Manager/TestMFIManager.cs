// LICENSE PLACEHOLDER

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
