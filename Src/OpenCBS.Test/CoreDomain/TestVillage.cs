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
