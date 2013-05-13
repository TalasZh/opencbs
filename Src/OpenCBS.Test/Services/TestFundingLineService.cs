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
        [ExpectedException(typeof (OpenCbsFundingLineException))]
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
        [ExpectedException(typeof (OpenCbsFundingLineException))]
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
