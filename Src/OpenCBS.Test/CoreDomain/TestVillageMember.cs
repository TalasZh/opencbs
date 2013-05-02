// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenCBS.CoreDomain.Clients;
using OpenCBS.Shared;

namespace OpenCBS.Test.CoreDomain
{
    [TestFixture]
    public class TestVillageMember
    {
        [Test]
        public void TestLeftDateIsNull()
        {
            VillageMember member = new VillageMember();
            Assert.IsNull(member.LeftDate);
        }

        [Test]
        public void TestCurrentlyIn()
        {
            VillageMember member = new VillageMember();
            member.CurrentlyIn = true;
            Assert.IsTrue(member.CurrentlyIn);
        }

    }
}
