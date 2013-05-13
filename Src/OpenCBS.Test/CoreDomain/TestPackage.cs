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
using OpenCBS.CoreDomain;
using NUnit.Framework;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.CoreDomain.Products;

namespace OpenCBS.Test.CoreDomain
{
	/// <summary>
	/// Description r�sum�e de TestPackage.
	/// </summary>
	[TestFixture]
	public class TestPackage
	{
		private LoanProduct package = new LoanProduct();

		[Test]
		public void PackageIdCorrectlySetAndRetrieved()
		{
			package.Id = 1;
			Assert.AreEqual(1,package.Id);
		}

		[Test]
		public void PackageNameCorrectlySetAndRetrieved()
		{
			package.Name = "pack1";
			Assert.AreEqual("pack1",package.Name);
		}

		[Test]
		public void PackageDeleteCorrectlySetAndRetrieved()
		{
			package.Delete = true;
			Assert.AreEqual(true,package.Delete);
		}

		[Test]
		public void PackageAmountCorrectlySetAndRetrieved()
		{
			package.Amount = 20.5m;
			Assert.AreEqual(20.5m,package.Amount.Value);
		}

		[Test]
		public void PackageAmountMinCorrectlySetAndRetrieved()
		{
			package.AmountMin = 20.5m;
			Assert.AreEqual(20.5,package.AmountMin.Value);
		}

		[Test]
		public void PackageAmountMaxCorrectlySetAndRetrieved()
		{
			package.AmountMax = 20.5m;
			Assert.AreEqual(20.5m, package.AmountMax.Value);
		}

		[Test]
		public void PackageInterestRateCorrectlySetAndRetrieved()
		{
			package.InterestRate = 10;
			Assert.AreEqual(10,package.InterestRate.Value);
		}
		[Test]
		public void PackageInterestRateMinCorrectlySetAndRetrieved()
		{
			package.InterestRateMin = 10;
			Assert.AreEqual(10,package.InterestRateMin.Value);
		}
		[Test]
		public void PackageInterestRateMaxCorrectlySetAndRetrieved()
		{
			package.InterestRateMax = 10;
			Assert.AreEqual(10,package.InterestRateMax.Value);
		}
		[Test]
		public void PackageGracePeriodCorrectlySetAndRetrieved()
		{
			package.GracePeriod = 0;
			Assert.AreEqual(0,package.GracePeriod.Value);
		}

		[Test]
		public void PackageGracePeriodMinCorrectlySetAndRetrieved()
		{
			package.GracePeriodMin = 0;
			Assert.AreEqual(0,package.GracePeriodMin.Value);
		}

		[Test]
		public void PackageGracePeriodMaxCorrectlySetAndRetrieved()
		{
			package.GracePeriodMax = 0;
			Assert.AreEqual(0,package.GracePeriodMax.Value);
		}

		[Test]
		public void PackageinstallmentTypeCorrectlySetAndRetrieved()
		{
			InstallmentType installmentType = new InstallmentType();
			installmentType.Name = "Monthly";
			package.InstallmentType = installmentType;
			Assert.IsTrue(package.InstallmentType.Equals(installmentType));
		}

		[Test]
		public void PackageNbreOfInstallmentCorrectlySetAndRetrieved()
		{
			package.NbOfInstallments = 10;
			Assert.AreEqual(10,package.NbOfInstallments.Value);
		}

		[Test]
		public void PackageNbreOfInstallmentMinCorrectlySetAndRetrieved()
		{
			package.NbOfInstallmentsMin = 10;
			Assert.AreEqual(10,package.NbOfInstallmentsMin.Value);
		}

		[Test]
		public void PackageNbreOfInstallmentMaxCorrectlySetAndRetrieved()
		{
			package.NbOfInstallmentsMax = 10;
			Assert.AreEqual(10,package.NbOfInstallmentsMax.Value);
		}

		[Test]
		public void PackageExoticProductCorrectlySetAndRetrieved()
		{
			ExoticInstallmentsTable exoticProduct = new ExoticInstallmentsTable();
			ExoticInstallment e1 = new ExoticInstallment();
			e1.Number = 1;
			e1.PrincipalCoeff = 0.23;
			e1.InterestCoeff = 0.15;
			exoticProduct.Add(e1);
			package.ExoticProduct = exoticProduct;
			Assert.IsTrue(package.ExoticProduct.Equals(exoticProduct));
		}

        //[Test]
        //public void PackageNonRepaymentPenaltiesCorrectlySetAndRetrieved()
        //{
        //    package.NonRepaymentPenalties = 2.5;
        //    Assert.AreEqual(2.5m,package.NonRepaymentPenalties.Value);
        //}

        //[Test]
        //public void PackageNonRepaymentPenaltiesMinCorrectlySetAndRetrieved()
        //{
        //    package.NonRepaymentPenaltiesMin = 2.5;
        //    Assert.AreEqual(2.5m,package.NonRepaymentPenaltiesMin.Value);
        //}

        //[Test]
        //public void PackageNonRepaymentPenaltiesMaxCorrectlySetAndRetrieved()
        //{
        //    package.NonRepaymentPenaltiesMax = 2.5;
        //    Assert.AreEqual(2.5m,package.NonRepaymentPenaltiesMax.Value);
        //}

		[Test]
		public void PackageAnticipatedTotalRepaymentPenalties()
		{
			package.AnticipatedTotalRepaymentPenalties = 2.5;
			Assert.AreEqual(2.5m,package.AnticipatedTotalRepaymentPenalties.Value);
		}

		[Test]
		public void PackageAnticipatedTotalRepaymentPenaltiesMin()
		{
			package.AnticipatedTotalRepaymentPenaltiesMin = 2.5;
			Assert.AreEqual(2.5m,package.AnticipatedTotalRepaymentPenaltiesMin.Value);
		}

		[Test]
		public void PackageAnticipatedTotalRepaymentPenaltiesMax()
		{
			package.AnticipatedTotalRepaymentPenaltiesMax = 2.5;
			Assert.AreEqual(2.5m,package.AnticipatedTotalRepaymentPenaltiesMax.Value);
		}

		[Test]
		public void PackageEntryFees()
		{
            EntryFee entryFee = new EntryFee();
		    entryFee.Value = (decimal)2.5;
            package.EntryFees = new List<EntryFee>();
            package.EntryFees.Add(entryFee);
            Assert.AreEqual(2.5m, package.EntryFees[0].Value);
		}

		[Test]
		public void PackageEntryFeesMin()
		{
			EntryFee fee = new EntryFee();
		    fee.Min = (decimal) 2.5;
            package.EntryFees = new List<EntryFee>();
            package.EntryFees.Add(fee);
			Assert.AreEqual(2.5m,package.EntryFees[0].Min);
            
		}

		[Test]
		public void PackageEntryFeesMax()
		{
            EntryFee fee = new EntryFee();
		    fee.Max = (decimal)2.5;
            package.EntryFees=new List<EntryFee>();
		    package.EntryFees.Add(fee);
			Assert.AreEqual(2.5m,package.EntryFees[0].Max);
		}

		[Test]
		public void TestDoesPackageUseLoanCycle()
		{
            LoanProduct pack = new LoanProduct();
		    pack.CycleId = 3;
			Assert.IsTrue(pack.UseLoanCycle);

		    pack.CycleId = null;
			Assert.IsFalse(pack.UseLoanCycle);
		}
	}
}
