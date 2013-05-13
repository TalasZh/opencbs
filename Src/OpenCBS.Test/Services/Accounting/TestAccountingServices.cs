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
using OpenCBS.CoreDomain;
using OpenCBS.Enums;
using OpenCBS.Shared;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.Shared.Settings;

namespace OpenCBS.Test.Services.Accounting
{
    /// <summary>
    /// Description r�sum�e de TestAccountingServices.
    /// </summary>
    [TestFixture]
    public class TestAccountingServices
    {
        private OpenCBS.Services.Accounting.AccountingServices _accountingServices;

        //[Test]
        //public void TestFindExchangeRateWhenConversionNotNeed()
        //{
        //    ApplicationSettings paramInDatabaseServices = ApplicationSettings.GetInstance("");
        //    paramInDatabaseServices.DeleteAllParameters();
        //    paramInDatabaseServices.AddParameter(OGeneralSettings.EXTERNALCURRENCY, null);

        //    _accountingServices = new OpenCBS.Services.Accounting.AccountingServices(paramInDatabaseServices);
        //    Assert.IsNull(_accountingServices.FindExchangeRate(DateTime.Today, new Currency{Id = 1}));
        //}

        [Test]
        public void TestConvertAmountToExternalCurrency()
        {
            _accountingServices = new OpenCBS.Services.Accounting.AccountingServices(DataUtil.TESTDB);
            OCurrency amount = 100;
            ExchangeRate rate = new ExchangeRate {Date = DateTime.Today, Rate = 3, Currency = new Currency {Id = 1}};
            Assert.AreEqual(33.3333m, _accountingServices.ConvertAmountToExternalCurrency(amount, rate).Value);
        }
        [Test]
        public void TestFindAllBookings()
        {
            //_accountingServices = new OpenCBS.Services.Accounting.AccountingServices(new User{Id = 5});
            //OCurrency amount = 100;

            //Booking booking = new Booking();
            //booking.Amount = 100;
            //booking.Description = "Manual booking";
            //booking.CreditAccount = new Account{Id = 1};
            //booking.DebitAccount = new Account{Id = 2};
            //booking.CreditAccount.Currency_Id = 1;
            //booking.DebitAccount.Currency_Id = 1;
            //AccountingTransaction mvtSet = new AccountingTransaction();
            //mvtSet.AddBooking(booking);
            //_accountingServices.BookManualEntry(null, mvtSet, TimeProvider.Today, new User{Id = 5});


            //BookingToViewStock _bookings = _accountingServices.FindAllBookings(new Account { Id = 1 }, TimeProvider.Today.AddDays(-1),
            //                                                              TimeProvider.Today);
            //Assert.AreEqual(_bookings.Count, 1);

        }

    }
}
