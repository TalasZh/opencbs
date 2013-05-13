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
