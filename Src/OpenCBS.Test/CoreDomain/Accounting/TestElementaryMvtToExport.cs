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

/*
 * Created by SharpDevelop.
 * User: Nicolas
 * Date: 12/02/2008
 * Time: 18:44
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using NUnit.Framework;
using OpenCBS.CoreDomain.Accounting;

namespace OpenCBS.Test.CoreDomain.Accounting
{
	/// <summary>
	/// Description of TestElementaryMvtToExport.
	/// </summary>
	[TestFixture]
	public class TestElementaryMvtToExport
	{
		BookingToExport _elementaryToExport;
		
		[TestFixtureSetUpAttribute]
		public void TestFixtureSetUp()
		{
			_elementaryToExport = new BookingToExport();
		}
		
		[Test]
		public void Get_Set_ContractCode()
		{	
			_elementaryToExport.ContractCode = "S/01/2008-1";
			Assert.AreEqual("S/01/2008-1",_elementaryToExport.ContractCode);
		}
		
		[Test]
		public void Get_Set_Purpose()
		{	
			_elementaryToExport.Purpose = "LODE-34";
			Assert.AreEqual("LODE-34",_elementaryToExport.Purpose);
		}
		
		[Test]
		public void Get_Set_CurrencyCode()
		{
            _elementaryToExport.BookingCurrency = new Currency{Id = 1, Name = "820"};
            Assert.AreEqual("820", _elementaryToExport.BookingCurrency.Name);
		}
		
		[Test]
		public void Get_Set_FundingLine()
		{	
			_elementaryToExport.FundingLine = "S/01/2008-13333";
			Assert.AreEqual("S/01/2008-13333",_elementaryToExport.FundingLine);
		}
		
		[Test]
		public void Get_Set_Number()
		{	
			_elementaryToExport.Number = 1;
			Assert.AreEqual(1,_elementaryToExport.Number);
		}
		
		[Test]
		public void Get_Set_DebitLocalAccountNumber()
		{	
			_elementaryToExport.DebitLocalAccountNumber = "1101011";
			Assert.AreEqual("1101011",_elementaryToExport.DebitLocalAccountNumber);
		}
		
		[Test]
		public void Get_Set_CreditLocalAccountNumber()
		{	
			_elementaryToExport.CreditLocalAccountNumber = "2202022";
			Assert.AreEqual("2202022",_elementaryToExport.CreditLocalAccountNumber);
		}
		
		[Test]
		public void Get_Set_InternalAmount()
		{	
			_elementaryToExport.InternalAmount = 1000;
			Assert.AreEqual(1000m,_elementaryToExport.InternalAmount.Value);
		}
		
		[Test]
		public void Get_Set_UserName()
		{	
			_elementaryToExport.UserName = "nicolas";
			Assert.AreEqual("nicolas",_elementaryToExport.UserName);
		}
		
		[Test]
		public void Get_Set_Date()
		{	
			_elementaryToExport.Date = new DateTime(2008,1,1);
			Assert.AreEqual(new DateTime(2008,1,1),_elementaryToExport.Date);
		}
		
		[Test]
		public void Get_Set_ExternalAmount_ExchangeRate_Null()
		{	
			_elementaryToExport.ExchangeRate = null;
			_elementaryToExport.InternalAmount = 1000;

            Assert.IsFalse(_elementaryToExport.ExternalAmount.HasValue);
		}
		
		[Test]
		public void Get_Set_ExternalAmount_ExchangeRate_NotNull()
		{	
			_elementaryToExport.ExchangeRate = 2;
			_elementaryToExport.InternalAmount = 1000;
            Assert.AreEqual(1000m/2m, _elementaryToExport.ExternalAmount.Value);
		}
		
		[Test]
		public void Get_Set_EventCode()
		{	
			_elementaryToExport.EventCode = "LODE-24";
			Assert.AreEqual("LODE-24",_elementaryToExport.EventCode);
		}
		
		[Test]
		public void Get_Set_ExchangeRate()
		{	
			_elementaryToExport.ExchangeRate = 10;
			Assert.AreEqual(10,_elementaryToExport.ExchangeRate);
		}
		
		[Test]
		public void Get_Set_MovmentSetId()
		{	
			_elementaryToExport.MovmentSetId = 10;
			Assert.AreEqual(10,_elementaryToExport.MovmentSetId);
		}
	}
}
