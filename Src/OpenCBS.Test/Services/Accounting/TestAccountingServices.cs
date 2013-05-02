// LICENSE PLACEHOLDER

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
