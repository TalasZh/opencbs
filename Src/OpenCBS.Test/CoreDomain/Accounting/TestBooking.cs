using System;
using NUnit.Framework;
using OpenCBS.CoreDomain.Accounting;

namespace OpenCBS.Test.CoreDomain
{
	/// <summary>
	/// Description of TestBooking.
	/// </summary>
	[TestFixture]
	public class TestBooking
	{
		Booking _booking = new Booking();
		
		[Test]
		public void Get_Set_Number()
		{
			_booking.Number = 1;
			Assert.AreEqual(1,_booking.Number);
		}
		
		[Test]
		public void Get_Set_CreditAccount()
		{
			_booking.CreditAccount = new Account();
			_booking.CreditAccount.Balance = 1000;
			
			Assert.AreEqual(1000m,_booking.CreditAccount.Balance.Value);
		}
		
		[Test]
		public void Get_Set_DebitAccount()
		{
			_booking.DebitAccount = new Account();
			_booking.DebitAccount.Balance = -1000;
			
			Assert.AreEqual(-1000m,_booking.DebitAccount.Balance.Value);
		}
		
		[Test]
		public void Get_Set_Amount()
		{
			_booking.Amount = 12;
			Assert.AreEqual(12m,_booking.Amount.Value);
		}
		
		[Test]
		public void Get_Set_AmountRounding()
		{
			_booking.Amount = 12.368777m;
			Assert.AreEqual(12.37m,_booking.AmountRounding.Value);
		}
		
		[Test]
		public void Get_Set_IsExported()
		{
			_booking.IsExported = true;
			Assert.IsTrue(_booking.IsExported);
		}
	}
}
