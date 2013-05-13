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
