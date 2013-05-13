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
using OpenCBS.CoreDomain.Products;
using OpenCBS.Enums;

namespace OpenCBS.Test.CoreDomain.Contracts.Savings
{
	[TestFixture]
	public class TestSavingProduct
	{

		[Test]
		public void Get_Set_Delete()
		{
			SavingProduct saving = new SavingProduct { Delete = true };
			Assert.IsTrue(saving.Delete);
		}

		[Test]
		public void Get_Set_Id()
		{
			SavingProduct saving = new SavingProduct { Id = 1 };
			Assert.AreEqual(1, saving.Id);
		}

		[Test]
		public void Get_Set_Name()
		{
			SavingProduct saving = new SavingProduct { Name = "Saving Product Name" };
			Assert.AreEqual("Saving Product Name", saving.Name);
		}

		[Test]
		public void Get_Set_ClientType()
		{
			SavingProduct saving = new SavingProduct { ClientType = OClientTypes.Person };
			Assert.AreEqual(OClientTypes.Person, saving.ClientType);
		}

		[Test]
		public void Get_Set_InitialAmountMin()
		{
			SavingProduct saving = new SavingProduct { InitialAmountMin = 150m };
			Assert.AreEqual(150m, saving.InitialAmountMin.Value);
		}

		[Test]
		public void Get_Set_InitialAmountMax()
		{
			SavingProduct saving = new SavingProduct { InitialAmountMax = 300m };
			Assert.AreEqual(300m, saving.InitialAmountMax.Value);
		}

		[Test]
		public void Get_Set_BalanceMin()
		{
			SavingProduct saving = new SavingProduct { BalanceMin = 150m };
			Assert.AreEqual(150m, saving.BalanceMin.Value);
		}

		[Test]
		public void Get_Set_BalanceMax()
		{
			SavingProduct saving = new SavingProduct { BalanceMax = 300m };
			Assert.AreEqual(300m, saving.BalanceMax.Value);
		}

		[Test]
		public void Get_Set_WithdrawingMin()
		{
			SavingProduct saving = new SavingProduct { WithdrawingMin = 150m };
			Assert.AreEqual(150m, saving.WithdrawingMin.Value);
		}

		[Test]
		public void Get_Set_WithdrawingMax()
		{
			SavingProduct saving = new SavingProduct { WithdrawingMax = 300m };
			Assert.AreEqual(300m, saving.WithdrawingMax.Value);
		}

		[Test]
		public void Get_Set_DepositMin()
		{
			SavingProduct saving = new SavingProduct { DepositMin = 100m };
			Assert.AreEqual(100m, saving.DepositMin.Value);
		}

		[Test]
		public void Get_Set_DepositMax()
		{
			SavingProduct saving = new SavingProduct { DepositMax = 500m };
			Assert.AreEqual(500m, saving.DepositMax.Value);
		}

		[Test]
		public void Get_Set_InterestRate()
		{
			SavingProduct saving = new SavingProduct { InterestRate = .15d };
			Assert.AreEqual(.15d, saving.InterestRate);
		}

		[Test]
		public void Get_Set_InterestRateMin()
		{
			SavingProduct saving = new SavingProduct { InterestRateMin = .15d };
			Assert.AreEqual(.15d, saving.InterestRateMin);
		}

		[Test]
		public void Get_Set_InterestRateMax()
		{
			SavingProduct saving = new SavingProduct { InterestRateMax = .55d };
			Assert.AreEqual(.55d, saving.InterestRateMax);
		}

        [Test]
        public void Get_Set_InterestBase()
        {
            SavingProduct product = new SavingProduct { InterestBase = OSavingInterestBase.Monthly };
            Assert.AreEqual(OSavingInterestBase.Monthly, product.InterestBase);
        }

        [Test]
        public void Get_Set_InterestFrequency()
        {
            SavingProduct product = new SavingProduct { InterestFrequency = OSavingInterestFrequency.EndOfWeek };
            Assert.AreEqual(OSavingInterestFrequency.EndOfWeek, product.InterestFrequency);
        }


	}
}
