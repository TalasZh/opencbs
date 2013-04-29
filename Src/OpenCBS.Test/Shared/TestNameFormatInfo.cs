//Octopus MFS is an integrated suite for managing a Micro Finance Institution: clients, contracts, accounting, reporting and risk
//Copyright © 2006,2007 OCTO Technology & OXUS Development Network
//
//This program is free software; you can redistribute it and/or modify
//it under the terms of the GNU Lesser General Public License as published by
//the Free Software Foundation; either version 2 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU Lesser General Public License for more details.
//
//You should have received a copy of the GNU Lesser General Public License along
//with this program; if not, write to the Free Software Foundation, Inc.,
//51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
//
// Licence : http://www.octopusnetwork.org/OverviewLicence.aspx
//
// Website : http://www.octopusnetwork.org
// Business contact: business(at)octopusnetwork.org
// Technical contact email : tech(at)octopusnetwork.org 

using Octopus.Shared;
using NUnit.Framework;

namespace Octopus.Test.Shared
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
