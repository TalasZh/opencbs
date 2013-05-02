// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenCBS.CoreDomain.Clients;
using OpenCBS.Enums;
using OpenCBS.Shared;

namespace OpenCBS.Test.CoreDomain
{
    [TestFixture]
    public class TestVillage
    {
        private Village _village = null;

        [SetUp]
        public void SetUp()
        {
            _village = new Village();
        }

        [Test]
        public void TestType()
        {
            Assert.AreEqual(OClientTypes.Village, _village.Type);
        }

        [Test]
        public void TestAddDelete()
        {
            Person person = new Person {Id = 1};
            VillageMember member = new VillageMember {Tiers = person, JoinedDate = TimeProvider.Now, CurrentlyIn = true, IsLeader = false};
            _village.AddMember(member);
            Assert.AreEqual(1, _village.Members.Count);
            _village.DeleteMember(member);
            Assert.AreEqual(0, _village.Members.Count);
        }

        [Test]
        public void TestAddDeleteLeftDate()
        {
            Person person = new Person {Id = 1};
            VillageMember member = new VillageMember {Tiers = person, JoinedDate = TimeProvider.Now, CurrentlyIn = true, IsLeader = false};
            _village.AddMember(member);
            _village.DeleteMember(member);
            Assert.IsTrue(TimeProvider.Now.Equals(member.LeftDate));
        }

        [Test]
        public void TestAddDeleteDifferent()
        {
            Person person1 = new Person {Id = 1};
            Person person2 = new Person {Id = 2};
            VillageMember member1 = new VillageMember {Tiers = person1, JoinedDate = TimeProvider.Now, CurrentlyIn = true, IsLeader = false};
            VillageMember member2 = new VillageMember {Tiers = person2, JoinedDate = TimeProvider.Now, CurrentlyIn = true, IsLeader = false};
            _village.AddMember(member1);
            _village.DeleteMember(member2);
            Assert.AreEqual(1, _village.Members.Count);
        }

        [Test]
        public void TestAddSameTwice()
        {
            Person person = new Person {Id = 1};
            VillageMember member = new VillageMember {Tiers = person, JoinedDate = TimeProvider.Now, CurrentlyIn = true, IsLeader = false};
            _village.AddMember(member);
            _village.AddMember(member);
            Assert.AreEqual(1, _village.Members.Count);
        }
    }
}
