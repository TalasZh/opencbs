// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.CoreDomain.FundingLines;
using OpenCBS.Manager;
using OpenCBS.Manager.Clients;
using OpenCBS.CoreDomain.SearchResult;


namespace OpenCBS.Test.Manager
{
    [TestFixture]
    public class TestFundingLineManager : BaseManagerTest
    {
        [Test]
        public void InitFundingLineManager()
        {
            var fund = (FundingLineManager)container["FundingLineManager"];
            Assert.IsNotNull(fund);
            Assert.IsTrue(container.Count > 0);
        }

        [Test]
        public  void SelectFundingLine()
        {
            var fund = (FundingLineManager)container["FundingLineManager"];

            var liste = fund.SelectFundingLines();
            Assert.IsNotNull(liste);

            Assert.IsTrue(liste.Count>0);
        }

        [Test]
        public void SelectFundingLines()
        {
            var fund = (FundingLineManager)container["FundingLineManager"];

            var listeFund = fund.SelectFundingLines();
            Assert.IsNotNull(listeFund);
            Assert.IsTrue(listeFund.Count > 2);
        }



        [Test]
        public void SelectFundinLineById()
        {
            var fund = (FundingLineManager)container["FundingLineManager"];
            var f = fund.SelectFundingLineById(1, false);
            Assert.IsNotNull(f);
            Assert.IsTrue(f.Purpose == "JHT");
        }

        [Test]
        public void AddFundinLine()
        {
            var fundingLineManager = (FundingLineManager)container["FundingLineManager"];
            var fund = new FundingLine
                           {
                               Id = 0,
                               Purpose = "Microsoft financement",
                               Name = "Clyon",
                               Deleted = false,
                               StartDate = DateTime.Now,
                               EndDate = DateTime.Now,
                               Currency = new Currency{Id = 1}
                           };
            fundingLineManager.AddFundingLine(fund);
            Assert.IsTrue(fund.Id > 0);
        }

        [Test]
        public void UpdateFundinLine()
        {
            var fundingLineManager = (FundingLineManager)container["FundingLineManager"];
            var fund = new FundingLine
                           {
                               Purpose = "Microsoft financement",
                               Name = "XXX",
                               Deleted = false,
                               StartDate = DateTime.Now,
                               EndDate = DateTime.Now,
                               Currency = new Currency{Id=1}
                           };
            fundingLineManager.AddFundingLine(fund);

            var fundingLineInitial = new FundingLine
                                         {
                                             Id = fund.Id,
                                             Purpose = "Microsoft",
                                             Name = "XXX",
                                             Deleted = false,
                                             StartDate = DateTime.Now,
                                             EndDate = DateTime.Now,
                                             Currency = new Currency{Id =1}
                                         };
            fundingLineManager.UpdateFundingLine(fundingLineInitial);
            var fundingLineFinal = fundingLineManager.SelectFundingLineById(fundingLineInitial.Id, false);
            Assert.AreEqual(fundingLineFinal.Purpose, "Microsoft");
        }

        [Test]
        public void DeleteFundinLine()
        {
            FundingLineManager fundingLineManager = (FundingLineManager)container["FundingLineManager"];
            FundingLine fund = new FundingLine
                           {
                               Purpose = "Microsoft financement",
                               Name = "XXX",
                               Deleted = false,
                               StartDate = DateTime.Now,
                               EndDate = DateTime.Now,
                               Currency = new Currency{Id = 1}
                           };
            fundingLineManager.AddFundingLine(fund);

            FundingLine fundingLineInitial = fundingLineManager.SelectFundingLineById(fund.Id, false);

            Assert.AreEqual("XXX", fundingLineInitial.Name);
            fundingLineManager.DeleteFundingLine(fundingLineInitial);
            FundingLine fundingLineFinal = fundingLineManager.SelectFundingLineById(fundingLineInitial.Id, false);
            Assert.IsNull(fundingLineFinal);
        }
        [Test]
        public void SelectFundingLinesNotIncludesDeleted()
        {
           var fund = (FundingLineManager)container["FundingLineManager"];
           DeleteFundinLine();
           var listeFund = fund.SelectFundingLines();
           Boolean found = false;
           foreach (FundingLine line in listeFund)
           {
             if( line.Name == "XXX")
                found = true;
           }
           Assert.IsFalse(found);
        }

        [Test]
        public void SelectFundingLineEvents()
        {
           var fundingLineServices = (FundingLineManager)container["FundingLineManager"];
           var fundingLine = new FundingLine { Id = 3 };
           var liste = fundingLineServices.SelectFundingLineEvents(fundingLine);

           Assert.IsTrue(liste.Count > 0);
           Assert.IsNotNull(liste);
        }


        [Test]
        public void TestGetNumberCorporate()
        {
            ClientManager clientManagement = (ClientManager)container["ClientManager"];
            Assert.IsNotNull(clientManagement);
            int number = clientManagement.GetNumberOfRecordsFoundForSearchCorporates(string.Empty);
            Assert.IsTrue(number>0);

        }

        [Test]
        public void TestCorporateSearch()
        {
            ClientManager clientManagement = (ClientManager)container["ClientManager"];
            Assert.IsNotNull(clientManagement);
            List<ClientSearchResult> clientResult = clientManagement.SearchCorporate(1, null);
            Assert.IsNotNull(clientResult);
        }
    }
}
