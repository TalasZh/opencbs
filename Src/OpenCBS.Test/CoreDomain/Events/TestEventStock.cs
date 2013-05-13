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

using NUnit.Framework;
using OpenCBS.CoreDomain.Events;
using OpenCBS.CoreDomain.Events.Loan;

namespace OpenCBS.Test.CoreDomain.Events
{
    [TestFixture]
    public class TestEventStock
    {
        private EventStock _eventStock;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _eventStock = new EventStock();
            _eventStock.Add(new RepaymentEvent());
            _eventStock.Add(new RescheduledLoanRepaymentEvent());
            _eventStock.Add(new BadLoanRepaymentEvent());
            _eventStock.Add(new StatisticalProvisionningEvent());
            _eventStock.Add(new LoanDisbursmentEvent());
            _eventStock.Add(new RescheduleLoanEvent());
            _eventStock.Add(new WriteOffEvent());
        }

        [Test]
        public void GetEventByType_Event()
        {
            Assert.AreEqual(0, _eventStock.GetEventsByType(typeof(Event)).Count);
        }

        [Test]
        public void GetEventByType_RepaymentEvent()
        {
            Assert.AreEqual(1, _eventStock.GetEventsByType(typeof(RepaymentEvent)).Count);
        }

        [Test]
        public void GetEvents()
        {
            Assert.AreEqual(7, _eventStock.GetEvents().Count);
        }

        [Test]
        public void GetRepaymentEvents()
        {
            Assert.AreEqual(3, _eventStock.GetRepaymentEvents().Count);
        }
    }
}
