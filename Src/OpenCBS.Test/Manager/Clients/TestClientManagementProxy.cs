
using System.Collections.Generic;
using NUnit.Framework;
using Octopus.CoreDomain.SearchResult;
using Octopus.Manager.Clients;

namespace Octopus.Test.Manager
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
