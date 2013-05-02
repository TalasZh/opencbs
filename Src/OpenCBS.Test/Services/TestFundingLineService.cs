// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Mocks;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.CoreDomain.Clients;
using OpenCBS.CoreDomain.FundingLines;
using OpenCBS.ExceptionsHandler.Exceptions.FundingLineExceptions;
using OpenCBS.Manager;
using OpenCBS.Manager.Accounting;
using OpenCBS.Manager.Products;
using OpenCBS.Services;
using OpenCBS.Shared;
using OpenCBS.Shared.Settings;
using FundingLine=OpenCBS.CoreDomain.FundingLines.FundingLine;
using OpenCBS.Services.Events;
using OpenCBS.Enums;

namespace OpenCBS.Test.Services
{
    [TestFixture]
    public class TestFundingLineService : BaseServicesTest
    {
        [Test]
        public void InitFundingLineServiceTest()
        {
            ClientServices service = (ClientServices) container["ClientServices"];
            Assert.IsTrue(container.Count > 0);
            Assert.IsNotNull(service);
        }

        [Test]
        public void SelectFundingLines()
        {
            FundingLineServices service = (FundingLineServices) container["FundingLineServices"];
            List<FundingLine> liste = service.SelectFundingLines();
            Assert.IsTrue(liste.Count > 0);
        }

        [Test]
        [ExpectedException(typeof (OctopusFundingLineException))]
        public void AddFundinLineWithNameThatAlreadyExists()
        {
            var fundingLineServices = (FundingLineServices) container["FundingLineServices"];

            var fund = new FundingLine
                           {
                               Purpose = "Microsoft financement",
                               Name = "XXX",
                               Deleted = false,
                               StartDate = DateTime.Now,
                               EndDate = DateTime.Now
                           };
            fundingLineServices.Create(fund);

            var fundingLineToAdd = new FundingLine
                                       {
                                           Purpose = "Microsoft financement",
                                           Name = "XXX",
                                           Deleted = false,
                                           StartDate = DateTime.Now,
                                           EndDate = DateTime.Now
                                       };
            fundingLineServices.Create(fund);
        }

        //Funding Line Events Test
        [Test]
        [ExpectedException(typeof (OctopusFundingLineException))]
        public void AddEventFundingLineWithoutFundingLine()
        {
            //Add Funding Line Event

            var fundingLineServices = (FundingLineServices) container["FundingLineServices"];
            var ev = new FundingLineEvent
                         {
                             Code = "KAO",
                             Type = OpenCBS.Enums.OFundingLineEventTypes.Entry,
                             CreationDate = DateTime.Now,
                             EndDate = DateTime.Now.AddDays(1),
                             Amount = 1000
                         };
            fundingLineServices.AddFundingLineEvent(ev, null);
        }

        public void DeleteEventFundingLine()
        {
            var fundingLineServices = (FundingLineManager) container["FundingLineServices"];
            var fundingLine = fundingLineServices.SelectFundingLineById(1, true);

            var ev = new FundingLineEvent
                         {
                             Id = 1,
                             Code = "NewKAO",
                             Type = OpenCBS.Enums.OFundingLineEventTypes.Entry,
                             FundingLine = fundingLine
                         };
            int id = fundingLineServices.SelectFundingLineEventId(ev);
            Assert.IsTrue(id > 1);
            fundingLineServices.DeleteFundingLineEvent(ev);

            id = fundingLineServices.SelectFundingLineEventId(ev);
            Assert.IsTrue(id < 1);
        }
    }
}
