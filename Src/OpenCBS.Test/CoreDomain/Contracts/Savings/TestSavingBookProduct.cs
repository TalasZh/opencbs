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
			SavingsBookProduct savings = new SavingsBookProduct { Delete = true };
			Assert.IsTrue(savings.Delete);
		}

		[Test]
		public void Get_Set_Id()
		{
			SavingsBookProduct savings = new SavingsBookProduct { Id = 1 };
			Assert.AreEqual(1, savings.Id);
		}

		[Test]
		public void Get_Set_Name()
		{
			SavingsBookProduct savings = new SavingsBookProduct { Name = "Saving Product Name" };
			Assert.AreEqual("Saving Product Name", savings.Name);
		}

        [Test]
        public void Get_Set_Code()
        {
            SavingsBookProduct savings = new SavingsBookProduct { Code = "Saving Product Code" };
            Assert.AreEqual("Saving Product Code", savings.Code);
        }

		[Test]
		public void Get_Set_ClientType()
		{
			SavingsBookProduct savings = new SavingsBookProduct { ClientType = OClientTypes.Person };
			Assert.AreEqual(OClientTypes.Person, savings.ClientType);
		}

		[Test]
		public void Get_Set_InitialAmountMin()
		{
			SavingsBookProduct savings = new SavingsBookProduct { InitialAmountMin = 150m };
			Assert.AreEqual(150m, savings.InitialAmountMin.Value);
		}

		[Test]
		public void Get_Set_InitialAmountMax()
		{
			SavingsBookProduct savings = new SavingsBookProduct { InitialAmountMax = 300m };
			Assert.AreEqual(300m, savings.InitialAmountMax.Value);
		}

		[Test]
		public void Get_Set_BalanceMin()
		{
			SavingsBookProduct savings = new SavingsBookProduct { BalanceMin = 150m };
			Assert.AreEqual(150m, savings.BalanceMin.Value);
		}

		[Test]
		public void Get_Set_BalanceMax()
		{
			SavingsBookProduct savings = new SavingsBookProduct { BalanceMax = 300m };
			Assert.AreEqual(300m, savings.BalanceMax.Value);
		}

		[Test]
		public void Get_Set_WithdrawingMin()
		{
			SavingsBookProduct savings = new SavingsBookProduct { WithdrawingMin = 150m };
			Assert.AreEqual(150m, savings.WithdrawingMin.Value);
		}

		[Test]
		public void Get_Set_WithdrawingMax()
		{
			SavingsBookProduct savings = new SavingsBookProduct { WithdrawingMax = 300m };
			Assert.AreEqual(300m, savings.WithdrawingMax.Value);
		}

		[Test]
		public void Get_Set_DepositMin()
		{
			SavingsBookProduct savings = new SavingsBookProduct { DepositMin = 100m };
			Assert.AreEqual(100m, savings.DepositMin.Value);
		}

		[Test]
		public void Get_Set_DepositMax()
		{
			SavingsBookProduct savings = new SavingsBookProduct { DepositMax = 500m };
			Assert.AreEqual(500m, savings.DepositMax.Value);
		}

        [Test]
        public void Get_Set_TransferMin()
        {
            SavingsBookProduct product = new SavingsBookProduct { TransferMin = 10m };
            Assert.AreEqual(10m, product.TransferMin.Value);
        }
        
        [Test]
        public void Get_Set_TransferMax()
        {
            SavingsBookProduct product = new SavingsBookProduct { TransferMax = 50m };
            Assert.AreEqual(50m, product.TransferMax.Value);
        }

		[Test]
		public void Get_Set_InterestRate()
		{
			SavingsBookProduct savings = new SavingsBookProduct { InterestRate = .15d };
			Assert.AreEqual(.15d, savings.InterestRate);
		}

		[Test]
		public void Get_Set_InterestRateMin()
		{
			SavingsBookProduct savings = new SavingsBookProduct { InterestRateMin = .15d };
			Assert.AreEqual(.15d, savings.InterestRateMin);
		}

		[Test]
		public void Get_Set_InterestRateMax()
		{
			SavingsBookProduct savings = new SavingsBookProduct { InterestRateMax = .55d };
			Assert.AreEqual(.55d, savings.InterestRateMax);
		}

        [Test]
        public void Get_Set_InterestBase()
        {
            SavingsBookProduct product = new SavingsBookProduct { InterestBase = OSavingInterestBase.Monthly };
            Assert.AreEqual(OSavingInterestBase.Monthly, product.InterestBase);
        }

        [Test]
        public void Get_Set_InterestFrequency()
        {
            SavingsBookProduct product = new SavingsBookProduct { InterestFrequency = OSavingInterestFrequency.EndOfWeek };
            Assert.AreEqual(OSavingInterestFrequency.EndOfWeek, product.InterestFrequency);
        }

        [Test]
        public void Get_Set_CalculAmountBase()
        {
            SavingsBookProduct product = new SavingsBookProduct { CalculAmountBase = OSavingCalculAmountBase.MinimalAmount };
            Assert.AreEqual(OSavingCalculAmountBase.MinimalAmount, product.CalculAmountBase);
        }

        [Test]
        public void Get_Set_WithdrawFeesType()
        {
            SavingsBookProduct product = new SavingsBookProduct { WithdrawFeesType = OSavingsFeesType.Flat };
            Assert.AreEqual(OSavingsFeesType.Flat, product.WithdrawFeesType);
        }

        [Test]
        public void Get_Set_FlatWithdrawFeesMin()
        {
            SavingsBookProduct product = new SavingsBookProduct { FlatWithdrawFeesMin = 10m };
            Assert.AreEqual(10m, product.FlatWithdrawFeesMin.Value);
        }

        [Test]
        public void Get_Set_FlatWithdrawFeesMax()
        {
            SavingsBookProduct product = new SavingsBookProduct { FlatWithdrawFeesMax = 50m };
            Assert.AreEqual(50m, product.FlatWithdrawFeesMax.Value);
        }

        [Test]
        public void Get_Set_FlatWithdrawFees()
        {
            SavingsBookProduct product = new SavingsBookProduct { FlatWithdrawFees = 25m };
            Assert.AreEqual(25m, product.FlatWithdrawFees.Value);
        }

        [Test]
        public void Get_Set_RateWithdrawFeesMin()
        {
            SavingsBookProduct product = new SavingsBookProduct { RateWithdrawFeesMin = 0.01d };
            Assert.AreEqual(0.01d, product.RateWithdrawFeesMin.Value);
        }

        [Test]
        public void Get_Set_RateWithdrawFeesMax()
        {
            SavingsBookProduct product = new SavingsBookProduct { RateWithdrawFeesMax = 0.10d };
            Assert.AreEqual(0.10d, product.RateWithdrawFeesMax.Value);
        }

        [Test]
        public void Get_Set_RateWithdrawFees()
        {
            SavingsBookProduct product = new SavingsBookProduct { RateWithdrawFees = 0.05d };
            Assert.AreEqual(0.05d, product.RateWithdrawFees.Value);
        }

        [Test]
        public void Get_Set_TransferFeesType()
        {
            SavingsBookProduct product = new SavingsBookProduct { TransferFeesType = OSavingsFeesType.Flat };
            Assert.AreEqual(OSavingsFeesType.Flat, product.TransferFeesType);
        }

        [Test]
        public void Get_Set_FlatTransferFeesMin()
        {
            SavingsBookProduct product = new SavingsBookProduct { FlatTransferFeesMin = 10m };
            Assert.AreEqual(10m, product.FlatTransferFeesMin.Value);
        }

        [Test]
        public void Get_Set_FlatTransferFeesMax()
        {
            SavingsBookProduct product = new SavingsBookProduct { FlatTransferFeesMax = 50m };
            Assert.AreEqual(50m, product.FlatTransferFeesMax.Value);
        }

        [Test]
        public void Get_Set_FlatTransferFees()
        {
            SavingsBookProduct product = new SavingsBookProduct { FlatTransferFees = 25m };
            Assert.AreEqual(25m, product.FlatTransferFees.Value);
        }

        [Test]
        public void Get_Set_RateTransferFeesMin()
        {
            SavingsBookProduct product = new SavingsBookProduct { RateTransferFeesMin = 0.01d };
            Assert.AreEqual(0.01d, product.RateTransferFeesMin.Value);
        }

        [Test]
        public void Get_Set_RateTransferFeesMax()
        {
            SavingsBookProduct product = new SavingsBookProduct { RateTransferFeesMax = 0.10d };
            Assert.AreEqual(0.10d, product.RateTransferFeesMax.Value);
        }

        [Test]
        public void Get_Set_RateTransferFees()
        {
            SavingsBookProduct product = new SavingsBookProduct { RateTransferFees = 0.05d };
            Assert.AreEqual(0.05d, product.RateTransferFees.Value);
        }
	}
}
