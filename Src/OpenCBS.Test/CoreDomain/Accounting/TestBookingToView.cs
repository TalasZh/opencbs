// LICENSE PLACEHOLDER

using System;
using NUnit.Framework;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.Enums;

namespace OpenCBS.Test.CoreDomain.Accounting
{
	/// <summary>
	/// Description of TestBookingToView.
	/// </summary>
	[TestFixture]
	public class TestBookingToView
	{
		BookingToView _bookingToView = new BookingToView();
		
		[Test]
		public void Get_Set_ExchangeRate()
		{
			_bookingToView.ExchangeRate = 12.35;
			Assert.AreEqual(12.35,_bookingToView.ExchangeRate);
		}
		
		[Test]
		public void Get_Set_ContractCode()
		{
			_bookingToView.ContractCode = "S/01/2008";
			Assert.AreEqual("S/01/2008",_bookingToView.ContractCode);
		}
		
		[Test]
		public void Get_Set_Direction()
		{
			_bookingToView.Direction = OBookingDirections.Credit;
            Assert.AreEqual(OBookingDirections.Credit, _bookingToView.Direction);
		}
		
		[Test]
		public void Get_Set_AmountInternal()
		{
			_bookingToView.AmountInternal = 1344.45m;
			Assert.AreEqual(1344.45m,_bookingToView.AmountInternal.Value);
		}
		
		[Test]
		public void Get_Set_ExternalAmount_Null()
		{
			_bookingToView.ExchangeRate = null;
			_bookingToView.AmountInternal = 1000;

            Assert.IsFalse(_bookingToView.ExternalAmount.HasValue);
		}
			
		[Test]
		public void Get_Set_Date()
		{
			_bookingToView.Date = new DateTime(2008,1,1);
			Assert.AreEqual(new DateTime(2008,1,1),_bookingToView.Date);
		}
		
		[Test]
		public void Get_Set_EventCode()
		{
			_bookingToView.EventCode = "LDGE_1923";
			Assert.AreEqual("LDGE_1923",_bookingToView.EventCode);
		}
	}
}
