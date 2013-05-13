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
