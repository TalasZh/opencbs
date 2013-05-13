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
using OpenCBS.Services;
using OpenCBS.CoreDomain.Accounting;

namespace OpenCBS.Test.Services.Accounting
{
    /// <summary>
    /// Description résumée de TestAccountingServices.
    /// </summary>
    [TestFixture]
    public class TestStandardBookingServices
    {
        private OpenCBS.Services.Accounting.StandardBookingServices _standardBookingServicesServices;

        [Test]
        public void TestCreateStandardBooking()
        {
            Assert.Ignore();
            Booking booking = new Booking();
            booking.Name = "Test";
            booking.CreditAccount = new Account("111", "Test");
            booking.DebitAccount = new Account("222", "Test");
            _standardBookingServicesServices = ServicesProvider.GetInstance().GetStandardBookingServices();
            _standardBookingServicesServices.CreateStandardBooking(booking);
            List<Booking> bookings = _standardBookingServicesServices.SelectAllStandardBookings();
            Assert.AreEqual(bookings.Count, 1);
            
        }

        [Test]
        public void TestSelectAllStandardBookings()
        {
            Assert.Ignore();
            Booking booking = new Booking();
            booking.Name = "Tefdfst";
            booking.CreditAccount = new Account("1ert", "Testrt");
            booking.DebitAccount = new Account("22ret", "Tesrbgt");
            _standardBookingServicesServices = ServicesProvider.GetInstance().GetStandardBookingServices();
            _standardBookingServicesServices.CreateStandardBooking(booking);

            booking.Name = "Tesfdft1";
            booking.CreditAccount = new Account("111f1", "Test1");
            booking.DebitAccount = new Account("2221", "Test1");
            _standardBookingServicesServices = ServicesProvider.GetInstance().GetStandardBookingServices();
            _standardBookingServicesServices.CreateStandardBooking(booking);

            List<Booking> bookings = _standardBookingServicesServices.SelectAllStandardBookings();
            Assert.IsNotEmpty(bookings);
        }

        [Test]
        public void TestDeleteStandardBooking()
        {
            Assert.Ignore();
            Booking booking = new Booking();
            booking.Name = "Tdfdfest";
            booking.CreditAccount = new Account("1eyyturt", "Tesyutrt");
            booking.DebitAccount = new Account("22ytutyuret", "Tetyuyusrt");
            _standardBookingServicesServices = ServicesProvider.GetInstance().GetStandardBookingServices();
            _standardBookingServicesServices.CreateStandardBooking(booking);

            List<Booking> bookings = _standardBookingServicesServices.SelectAllStandardBookings();
            _standardBookingServicesServices.DeleteStandardBooking(bookings[0].Id);
            booking = _standardBookingServicesServices.SelectStandardBookingById(bookings[0].Id);
            Assert.IsNull(booking.Name);
        }

        public void TestUpdateStandardBooking()
        {
            Booking booking = new Booking();
            booking.Name = "Tdfdfest";
            booking.CreditAccount = new Account("1eyyturt", "Tesyutrt");
            booking.DebitAccount = new Account("22ytutyuret", "Tetyuyusrt");
            _standardBookingServicesServices = ServicesProvider.GetInstance().GetStandardBookingServices();
            _standardBookingServicesServices.CreateStandardBooking(booking);

            List<Booking> bookings = _standardBookingServicesServices.SelectAllStandardBookings();
            int id = bookings[0].Id;
            booking = _standardBookingServicesServices.SelectStandardBookingById(id);
            booking.Name = "Shoulbe";
            _standardBookingServicesServices.UpdateStandardBookings(booking);
            booking = _standardBookingServicesServices.SelectStandardBookingById(id);
            Assert.AreNotEqual(booking.Name, "Shoulbe");
        }

    }
}
