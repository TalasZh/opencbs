// LICENSE PLACEHOLDER

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
        [ExpectedException(typeof(OctopusMFIExceptions))]
        public void TestCreateMFINameIsEmpty()
        {
            MFI mfi = new MFI { Name = "", Login = "Login", Password = "Password" };

            _mockMFIManagement.Expect("CreateMFI", mfi);
            MFIServices mfiServices = _SetMockManager(_mockMFIManagement);
            mfiServices.CreateMFI(mfi);
        }

        [Test]
        [ExpectedException(typeof(OctopusMFIExceptions))]
        public void TestCreateMFILoginIsEmptyAndPasswordIsFilled()
        {
            MFI mfi = new MFI { Name = "MFI", Login = "", Password = "Password" };

            _mockMFIManagement.Expect("CreateMFI", mfi);
            MFIServices mfiServices = _SetMockManager(_mockMFIManagement);
            mfiServices.CreateMFI(mfi);
        }

        [Test]
        [ExpectedException(typeof(OctopusMFIExceptions))]
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
        [ExpectedException(typeof(OctopusMFIExceptions))]
        public void TestUpdateMFINameIsEmpty()
        {
            MFI mfi = new MFI { Name = "", Login = "Login", Password = "Password" };

            _mockMFIManagement.Expect("UpdateMFI", mfi);
            MFIServices mfiServices = _SetMockManager(_mockMFIManagement);
            mfiServices.UpdateMFI(mfi);
        }

        [Test]
        [ExpectedException(typeof(OctopusMFIExceptions))]
        public void TestUpdateMFILoginIsEmptyAndPasswordIsFilled()
        {
            MFI mfi = new MFI { Name = "MFI", Login = "", Password = "Password" };

            _mockMFIManagement.Expect("UpdateMFI", mfi);
            MFIServices mfiServices = _SetMockManager(_mockMFIManagement);
            mfiServices.UpdateMFI(mfi);
        }

        [Test]
        [ExpectedException(typeof(OctopusMFIExceptions))]
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
        [ExpectedException(typeof(OctopusMFIExceptions))]
        public void TestCheckIfSamePasswordFailure()
        {
            MFIServices mfiServices = new MFIServices(User.CurrentUser);

           mfiServices.CheckIfSamePassword("Password", "DifferentPassword");
        }
    }
}
