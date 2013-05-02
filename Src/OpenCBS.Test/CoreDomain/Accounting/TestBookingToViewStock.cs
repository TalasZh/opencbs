using System;
using NUnit.Framework;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.Enums;

namespace OpenCBS.Test.CoreDomain.Accounting
{
	/// <summary>
	/// Description of TestBookingStock.
	/// </summary>
	[TestFixture]
	public class TestBookingToViewStock
	{
		BookingToViewStock _bookingToViewStock;
		
		[SetUp]
		public void SetUp()
		{
			Account _account = new Account();
			_account.Balance = 300;
			
			BookingToView _booking1 = new BookingToView(OBookingDirections.Debit,1000,new DateTime(2008,1,1),
			                                            3.45,"S/01/2008-1","LDRE34");

            BookingToView _booking2 = new BookingToView(OBookingDirections.Credit, 1333, new DateTime(2008, 1, 2),
			                                            4,"S/01/2008-1","LDRE35");

            BookingToView _booking3 = new BookingToView(OBookingDirections.Debit, 10, new DateTime(2008, 1, 5),
			                                            5.45,"S/01/2008-3","LDRE36");
			
			_bookingToViewStock = new BookingToViewStock(_account);
			_bookingToViewStock.Add(_booking1);
			_bookingToViewStock.Add(_booking2);
			_bookingToViewStock.Add(_booking3);
		}
		
		[Test]
		public void GetSelectedBookingBalanceInInternalCurrency_NoBooking()
		{
			_bookingToViewStock = new BookingToViewStock(new Account());
			Assert.AreEqual(0m,_bookingToViewStock.GetSelectedBookingBalanceInInternalCurrency.Value);
		}
		
		[Test]
		public void GetSelectedBookingBalanceInExternalCurrency_NoBooking()
		{
			_bookingToViewStock = new BookingToViewStock(new Account());
			Assert.AreEqual(0m,_bookingToViewStock.GetSelectedBookingBalanceInExternalCurrency.Value);
		}
		
		[Test]
		public void GetSelectedBookingBalanceInInternalCurrency_Booking()
		{
            BookingToView booking4 = new BookingToView(OBookingDirections.Credit, 133, new DateTime(2008, 1, 6),
			                                            5.45,"S/01/2008-3","LDRE37");
			
			_bookingToViewStock.Add(booking4);
			
			Assert.AreEqual(-456m,_bookingToViewStock.GetSelectedBookingBalanceInInternalCurrency.Value);
		}
		
		[Test]
		public void GetSelectedBookingBalanceInExternalCurrency_Booking_ExchangeRateNotNull()
		{
            BookingToView booking4 = new BookingToView(OBookingDirections.Credit, 133, new DateTime(2008, 1, 6),
			                                            5.45,"S/01/2008-3","LDRE37");
			
			_bookingToViewStock.Add(booking4);
			
			Assert.AreEqual(-2552.35m,_bookingToViewStock.GetSelectedBookingBalanceInExternalCurrency.Value);
		}
		
		[Test]
		public void GetSelectedBookingBalanceInExternalCurrency_Booking_ExchangeRateNull()
		{
            BookingToView booking4 = new BookingToView(OBookingDirections.Credit, 133, new DateTime(2008, 1, 6),
			                                            null,"S/01/2008-3","LDRE37");
			
			_bookingToViewStock.Add(booking4);
            Assert.IsFalse(_bookingToViewStock.GetSelectedBookingBalanceInExternalCurrency.HasValue);
		}
		
		[Test]
		public void GetAccountBalance()
		{
			Assert.AreEqual(300m,_bookingToViewStock.GetAccountBalance.Value);
		}	
	}
}
