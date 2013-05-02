// LICENSE PLACEHOLDER

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
