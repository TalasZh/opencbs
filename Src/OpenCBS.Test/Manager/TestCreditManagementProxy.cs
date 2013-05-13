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

using System.Collections.Generic;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.SearchResult;
using NUnit.Framework;
using OpenCBS.Manager.Contracts;
using OpenCBS.Shared.Settings;

namespace OpenCBS.Test.Manager
{
    [TestFixture]
    public class TestCreditManagementProxy:BaseManagerTest
    {
        [SetUp]
        protected override void SetUp()
        {
            base.SetUp();
            User.CurrentUser.Id = 1;
        }

        [Test]
        public void SearchCreditContractByCriteres()
        {
            LoanManager c = (LoanManager)container["LoanManager"];
            int count;
            List<CreditSearchResult> list = c.SearchCreditContractByCriteres(1, "", out count);
            Assert.IsTrue(list.Count > 0);
        }

        [Test]
        public void GetNumberCreditContract0()
        {
            LoanManager c = (LoanManager)container["LoanManager"];
            int count;
            c.SearchCreditContractByCriteres(1, "", out count);
            Assert.IsTrue(count > 0);
        }

        [Test]
        public void GetNumberCreditContract1()
        {
            LoanManager c = (LoanManager)container["LoanManager"];
            int count;
            c.SearchCreditContractByCriteres(1, "DFR", out count);
            Assert.IsTrue(count == 0);
        }

        [Test]
        public void SearchCreditContractByCriteresProxy()
        {
            LoanManager c = (LoanManager)container["LoanManager"];
            int count;
            List<CreditSearchResult> list = c.SearchCreditContractByCriteres(1, "", out count);
            Assert.IsTrue(list.Count > 0);
        }

        [Test]
        public void SearchCreditContractByCriteresProxy1()
        {
            LoanManager c = (LoanManager)container["LoanManager"];
            int count;
            List<CreditSearchResult> list = c.SearchCreditContractByCriteres(1, "44", out count);
            Assert.IsTrue(list.Count > 0);
        }
    }
}
