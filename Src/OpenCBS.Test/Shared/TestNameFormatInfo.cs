// LICENSE PLACEHOLDER

using OpenCBS.Shared;
using NUnit.Framework;

namespace OpenCBS.Test.Shared
{
    [TestFixture]
    public class TestNameFormatInfo
    {
        private NameFormatInfo formatInfo;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            formatInfo = new NameFormatInfo();
        }

        [Test]
        public void TestAsIs()
        {
            Assert.AreEqual("pAvEL", string.Format(formatInfo, "{0:#}", "pAvEL"));
        }

        [Test]
        public void TestLowercase()
        {
            Assert.AreEqual("pavel", string.Format(formatInfo, "{0:L}", "pAvEL"));
        }

        [Test]
        public void TestUppercase()
        {
            Assert.AreEqual("PAVEL", string.Format(formatInfo, "{0:U}", "pAvEL"));
        }

        [Test]
        public void TestFirstLetterUppercaseRestAsIs()
        {
            Assert.AreEqual("PavEL", string.Format(formatInfo, "{0:U#}", "pavEL"));
        }

        [Test]
        public void TestFirstLetterUppercaseRestLowercase()
        {
            Assert.AreEqual("Pavel", string.Format(formatInfo, "{0:UL}", "pavEL"));
        }
    }
}
