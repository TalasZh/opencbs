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
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.Enums;

namespace OpenCBS.Test.CoreDomain
{
	/// <summary>
	/// Description r�sum�e de Class1.
	/// </summary>
	[TestFixture]
	public class TestAccount
	{
		Account testAccount = new Account();

		[Test]
		public void AccountIdCorrectlySetAndRetrieved()
		{
			testAccount.Id = 1;
			Assert.AreEqual(1,testAccount.Id);
		}
		
		[Test]
		public void AccountNumberCorrectlySetAndRetrieved()
		{
			Account account = new Account();
			account.Number = "1011";
			Assert.AreEqual("1011",account.Number);
		}

		[Test]
		public void BalanceRoundedCorrectlySetAndRetrieved()
		{
			Account account = new Account();
			account.Balance = 100.347m;
			Assert.AreEqual(100.35m,account.BalanceRounded.Value);
		}

		[Test]
		public void AccountLabelCorrectlySetAndRetrieved()
		{
			testAccount.Label = "Assets";
			Assert.AreEqual("Assets",testAccount.Label);
		}


		[Test]
		public void AccountBalanceCorrectlySetAndRetrieved()
		{
			testAccount.Balance = -27.50m;
			Assert.AreEqual(-27.50m,testAccount.Balance.Value);
		}
	
		[Test]
		public void AccountTypeCodeCorrectlySetAndRetrieved()
		{
			testAccount.TypeCode = "CASH_CREDIT";
			Assert.AreEqual("CASH_CREDIT",testAccount.TypeCode);
		}
		
		[Test]
		public void DebitPlusCorrectlySetAndRetrieved()
		{
			Account account = new Account();
			account.DebitPlus = true;
			Assert.IsTrue(account.DebitPlus);			
		}

		[Test]
		public void AccountDescriptionCorrectlySetAndRetrieved()
		{
            testAccount.AccountCategory = OAccountCategories.BalanceSheetAsset;
            Assert.AreEqual(OAccountCategories.BalanceSheetAsset, testAccount.AccountCategory);
		}

        [Test]
        public void AccountParentAccountIdCorrectlySetAndRetrieved()
        {
            Assert.AreEqual(false, testAccount.ParentAccountId.HasValue);
            testAccount.ParentAccountId = 10;
            Assert.AreEqual(10, testAccount.ParentAccountId.Value);
        }

		[Test]
		public void TestIfToStringReturnAccountNumberPlusAccountLabel()
		{
			testAccount.Number = "1011";
			testAccount.Label = "Cash";
			Assert.AreEqual("1011 : Cash",testAccount.ToString());
		}

		[Test]
		public void TestIfAccountIsCorrectlyDebitedWhenDebitPlus()
		{
			testAccount.DebitPlus = true;
			testAccount.Balance = 1000;
			testAccount.Debit(174.25m);
			Assert.AreEqual(1174.25m,testAccount.Balance.Value);
		}

		[Test]
		public void TestIfAccountIsCorrectlyCreditedWhenDebitPlus()
		{
			testAccount.DebitPlus = true;
			testAccount.Balance = 1000;
			testAccount.Credit(200);
			Assert.AreEqual(800m,testAccount.Balance.Value);
		}

		[Test]
		public void TestIfAccountIsCorrectlyDebitedWhenNotDebitPlus()
		{
			testAccount.DebitPlus = false;
			testAccount.Balance = 1000;
			testAccount.Debit(174.25m);
			Assert.AreEqual(825.75m,testAccount.Balance.Value);
		}

		[Test]
		public void TestIfAccountIsCorrectlyCreditedWhenNotDebitPlus()
		{
			testAccount.DebitPlus = false;
			testAccount.Balance = 1000;
			testAccount.Credit(200);
			Assert.AreEqual(1200m,testAccount.Balance.Value);
		}
	}
}
