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
using NUnit.Framework;
using OpenCBS.CoreDomain.SearchResult;
using OpenCBS.Manager.Clients;

namespace OpenCBS.Test.Manager
{
    [TestFixture]
    public class TestClientManagementProxy:BaseManagerTest
    {
        [Test]
        public void SearchPersonByCriteres()
        {
            ClientManager clientManagement = (ClientManager)container["ClientManager"];
            List<ClientSearchResult> result = clientManagement.SearchPersonByCriteres(1, "");
            Assert.IsTrue(result.Count > 0);
            Assert.IsNotNull(result);
        }

        [Test]
        public void SearchPersonByCriteres1()
        {
            ClientManager clientManagement = (ClientManager)container["ClientManager"];
            List<ClientSearchResult> result = clientManagement.SearchPersonByCriteres(1, "gtgtgt");
            Assert.IsFalse(result.Count>0);
        }

        [Test]
        public void SearchCorporateByCriteres()
        {
            ClientManager clientManagement = (ClientManager)container["ClientManager"];
            List<ClientSearchResult> result = clientManagement.SearchCorporateByCriteres(2, 1, "");
            Assert.IsTrue(result.Count > 0); 
            Assert.IsNotNull(result);
        }

        [Test]
        public void SearchCorporateByCriteres1()
        {
            ClientManager clientManagement = (ClientManager)container["ClientManager"];
            List<ClientSearchResult> result = clientManagement.SearchCorporateByCriteres(2, 1, "frfr");
            Assert.IsFalse(result.Count > 0);
        }

        [Test]
        public void SearchGroupByCriteres()
        {
            ClientManager clientManagement = (ClientManager)container["ClientManager"];
            List<ClientSearchResult> result = clientManagement.SearchGroupByCriteres(1, "");
            Assert.IsTrue(result.Count > 0); 
            Assert.IsNotNull(result);
        }

        [Test]
        public void SearchGroupByCriteres1()
        {
            ClientManager clientManagement = (ClientManager)container["ClientManager"];
            List<ClientSearchResult> result = clientManagement.SearchGroupByCriteres(1, "frgrgr");
            Assert.IsFalse(result.Count > 0);
        }

        [Test]
        public void GetNumberCorporate()
        {
            ClientManager clientManagement = (ClientManager)container["ClientManager"];
            int result = clientManagement.GetNumberCorporate("");
            Assert.IsTrue(result > 0); 
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetNumberCorporate1()
        {
            ClientManager clientManagement = (ClientManager)container["ClientManager"];
            int result = clientManagement.GetNumberCorporate("hju");
            Assert.IsTrue(result==0);
        }

        [Test]
        public void GetNumberPerson()
        {

            ClientManager clientManagement = (ClientManager)container["ClientManager"];
           int result = clientManagement.GetNumberPerson("", 1);
            Assert.IsTrue(result > 0); 
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetNumberPerson1()
        {
            ClientManager clientManagement = (ClientManager)container["ClientManager"];
            int result = clientManagement.GetNumberPerson("jloiu", 1);
            Assert.IsTrue(result == 0);
        }

        [Test]
        public void GetNumberGroup()
        {
            ClientManager clientManagement = (ClientManager)container["ClientManager"];
            int  result = clientManagement.GetNumberGroup("");
            Assert.IsTrue(result > 0); 
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetNumberGroup1()
        {

            ClientManager clientManagement = (ClientManager)container["ClientManager"];
            int result = clientManagement.GetNumberGroup("jklo");
            Assert.IsTrue(result == 0);
        }
    }
}
