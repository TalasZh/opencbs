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
using System.Text;
using NUnit.Framework;
using OpenCBS.CoreDomain.Events.Saving;
using OpenCBS.CoreDomain.Products;
using OpenCBS.Enums;
using OpenCBS.Shared;

namespace OpenCBS.Test.CoreDomain.Contracts.Savings
{
	[TestFixture]
	public class TestSavingEvent
	{

		[Test]
		public void Get_Set_Id()
		{
			SavingDepositEvent saving = new SavingDepositEvent { Id = 1 };
			Assert.AreEqual(1, saving.Id);
		}

		public void Get_Set_Description()
		{
			SavingDepositEvent saving = new SavingDepositEvent { Description = "Saving Description" };
			Assert.AreEqual("Saving Description", saving.Description);
		}

		[Test]
		public void Get_Set_Date()
		{
			SavingDepositEvent saving = new SavingDepositEvent { Date = TimeProvider.Today };
			Assert.AreEqual(TimeProvider.Today, saving.Date);
		}

		[Test]
		public void Get_Set_Amount()
		{
			SavingDepositEvent saving = new SavingDepositEvent { Amount = 125.5m };
			Assert.AreEqual(125.5m, saving.Amount.Value);
		}

		[Test]
		public void CompareEventGreaterThanDate()
		{
			SavingDepositEvent event1 = new SavingDepositEvent { Date = new DateTime(2008, 10, 15) };
			SavingDepositEvent event2 = new SavingDepositEvent { Date = new DateTime(2008, 10, 13) };
			Assert.IsTrue(event1 > event2);
		}

		[Test]
		public void CompareEventLessThanDate()
		{
			SavingDepositEvent event1 = new SavingDepositEvent { Date = new DateTime(2008, 10, 13) };
			SavingDepositEvent event2 = new SavingDepositEvent { Date = new DateTime(2008, 10, 15) };
			Assert.IsTrue(event1 < event2);
		}

        [Test]
        public void Get_Set_RelatedAccountCode()
        {
            SavingDebitTransferEvent event1 = new SavingDebitTransferEvent { RelatedContractCode = "CODE/RELATED/ACCOUNT/1" };
            Assert.AreEqual("CODE/RELATED/ACCOUNT/1", event1.RelatedContractCode);
            SavingCreditTransferEvent event2 = new SavingCreditTransferEvent { RelatedContractCode = "CODE/RELATED/ACCOUNT/2" };
            Assert.AreEqual("CODE/RELATED/ACCOUNT/2", event2.RelatedContractCode);
        }

		[Test]
		public void Get_Code()
		{
			SavingInitialDepositEvent savingInitialDeposit = new SavingInitialDepositEvent();
			Assert.AreEqual("SVIE", savingInitialDeposit.Code);
			SavingDepositEvent savingDeposit = new SavingDepositEvent();
			Assert.AreEqual("SVDE", savingDeposit.Code);
			SavingWithdrawEvent savingWithDraw = new SavingWithdrawEvent();
			Assert.AreEqual("SVWE", savingWithDraw.Code);
            SavingInterestsAccrualEvent savingAccrualInterest = new SavingInterestsAccrualEvent();
            Assert.AreEqual("SIAE", savingAccrualInterest.Code);
            SavingInterestsPostingEvent savingPostingInterest = new SavingInterestsPostingEvent();
            Assert.AreEqual("SIPE", savingPostingInterest.Code);
            SavingCreditTransferEvent savingCreditTransfer = new SavingCreditTransferEvent();
            Assert.AreEqual("SCTE", savingCreditTransfer.Code);
            SavingDebitTransferEvent savingDebitTransfer = new SavingDebitTransferEvent();
            Assert.AreEqual("SDTE", savingDebitTransfer.Code);
		}

	}
}
