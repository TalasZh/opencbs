// LICENSE PLACEHOLDER

using NUnit.Framework;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Clients;
using OpenCBS.Shared;

namespace OpenCBS.Test.CoreDomain
{
    [TestFixture]
    public class TestMember
    {
        private readonly Member _member = new Member();

        [Test]
        public void Tiers_GetAndRetrieved()
        {
            _member.Tiers.Id = 1;
            Assert.AreEqual(1,_member.Tiers.Id);
        }

        [Test]
        public void LoanShareAmount_GetAndRetrieved()
        {
            _member.LoanShareAmount = 10.3m;
            Assert.AreEqual(10.3m, _member.LoanShareAmount.Value);
        }

        [Test]
        public void Leader_GetAndRetrieved()
        {
            _member.IsLeader = true;
            Assert.AreEqual(true, _member.IsLeader);
        }

        [Test]
        public void CurrentlyIn_GetAndRetrieved()
        {
            _member.CurrentlyIn = true;
            Assert.AreEqual(true, _member.CurrentlyIn);
        }

        [Test]
        public void LeftDate_GetAndRetrieved_NotNull()
        {
            _member.LeftDate = TimeProvider.Today;
            Assert.AreEqual(TimeProvider.Today, _member.LeftDate);
        }

        [Test]
        public void JoinedDate_GetAndRetrieved()
        {
            _member.JoinedDate = TimeProvider.Today;
            Assert.AreEqual(TimeProvider.Today, _member.JoinedDate);
        }

        [Test]
        public void LeftDate_GetAndRetrieved_Null()
        {
            _member.LeftDate = null;
            Assert.AreEqual(null, _member.LeftDate);
        }

        [Test]
        public void TwoMembers_AreEgals()
        {
            Member a = new Member { Tiers = new Person { IdentificationData = "1234" } };
            Member b = new Member { Tiers = new Person { IdentificationData = "12346555" } };
            Member c = new Member { Tiers = new Person { IdentificationData = "1234" } };

            Assert.IsTrue(a == c);
            Assert.IsFalse(a == b);
        }

        [Test]
        public void TwoMembers_AreEgals_NullValues()
        {
            Member a = new Member { Tiers = new Person { IdentificationData = "1234" } };
            Member b = null;

            Assert.IsFalse(a == b);
        }

        [Test]
        public void TwoMembers_NotEgals_NullValues()
        {
            Member a = new Member { Tiers = new Person { IdentificationData = "1234" } };
            Member b = null;

            Assert.IsTrue(a != b);
        }
    }
}
