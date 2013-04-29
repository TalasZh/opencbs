
using System.Collections.Generic;
using Octopus.CoreDomain;
using Octopus.CoreDomain.SearchResult;
using NUnit.Framework;
using Octopus.Manager.Contracts;
using Octopus.Shared.Settings;

namespace Octopus.Test.Manager
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
