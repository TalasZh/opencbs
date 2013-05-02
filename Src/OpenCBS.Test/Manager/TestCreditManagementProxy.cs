
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
