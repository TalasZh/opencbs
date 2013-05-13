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
using System.Collections;
using OpenCBS.CoreDomain;
using NUnit.Framework;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.Enums;

namespace OpenCBS.Test.CoreDomain
{
	/// <summary>
	/// Summary description for TestExoticProduct.
	/// </summary>
	
	[TestFixture]
	public class TestExoticProduct
	{
		private ExoticInstallmentsTable exoticProduct;
		private ExoticInstallment e1;
		private ExoticInstallment e2;

		[SetUp]
		public void SetUp()
		{
			exoticProduct = new ExoticInstallmentsTable();
			exoticProduct.Id = 1;
			exoticProduct.Name = "proportional interest rate with 6 installments";

			e1 = new ExoticInstallment();
			e1.Number = 1;
			e1.PrincipalCoeff = 0.4;
			e1.InterestCoeff = 0.2;

			e2 = new ExoticInstallment();
			e2.Number = 2;
			e2.PrincipalCoeff = 0.3;
			e2.InterestCoeff = 0.15;
			exoticProduct.Add(e1);
			exoticProduct.Add(e2);
		}

		[Test]
		public void TestIfIdIsCorrectlySetAndRetrieved()
		{
			Assert.AreEqual(1,exoticProduct.Id);
		}

		[Test]
		public void TestIfNameIsCorrectlySetAndRetrieved()
		{
			Assert.AreEqual("proportional interest rate with 6 installments",exoticProduct.Name);
		}

		[Test]
		public void TestIfInstallmentsAreCorrectlyAddedToInstallmentList()
		{
			Assert.AreEqual(2,exoticProduct.GetNumberOfInstallments);
		}

		[Test]
		public void IfInstallmentCorrectlyDeleted()
		{
			exoticProduct.Remove(e1);
			Assert.AreEqual(1,exoticProduct.GetNumberOfInstallments);
		}

		[Test]
		public void GetNumberOfInstallments()
		{
            ExoticInstallmentsTable product = new ExoticInstallmentsTable();
            product.Add(e1);
            product.Add(e2);
            Assert.AreEqual(2, product.GetNumberOfInstallments);
		}

		[Test]
		public void TestIsExoticProductForDecliningRatePackage()
		{
			exoticProduct = new ExoticInstallmentsTable();

			e1 = new ExoticInstallment();
			e1.Number = 1;
			e1.PrincipalCoeff = 0.4;
			e1.InterestCoeff = null;

			e2 = new ExoticInstallment();
			e2.Number = 2;
			e2.PrincipalCoeff = 0.3;
			e2.InterestCoeff = null;
			exoticProduct.Add(e1);
			exoticProduct.Add(e2);

			Assert.IsTrue(exoticProduct.IsExoticProductForDecliningRatePackage);
		}

		[Test]
		public void TestIsExoticProductForDecliningRatePackageWhenItIsNot()
		{
			exoticProduct = new ExoticInstallmentsTable();

			e1 = new ExoticInstallment();
			e1.Number = 1;
			e1.PrincipalCoeff = 0.4;
			e1.InterestCoeff = 0.2;

			e2 = new ExoticInstallment();
			e2.Number = 2;
			e2.PrincipalCoeff = 0.3;
			e2.InterestCoeff = null;
			exoticProduct.Add(e1);
			exoticProduct.Add(e2);

			Assert.IsFalse(exoticProduct.IsExoticProductForDecliningRatePackage);
		}

		[Test]
		public void TestIsExoticProductForFlatRatePackage()
		{
			exoticProduct = new ExoticInstallmentsTable();

			e1 = new ExoticInstallment();
			e1.Number = 1;
			e1.PrincipalCoeff = 0.4;
			e1.InterestCoeff = 0.2;

			e2 = new ExoticInstallment();
			e2.Number = 2;
			e2.PrincipalCoeff = 0.3;
			e2.InterestCoeff = 0;
			exoticProduct.Add(e1);
			exoticProduct.Add(e2);

			Assert.IsTrue(exoticProduct.IsExoticProductForFlatRatePackage);
		}

		[Test]
		public void TestIsExoticProductForFlatRatePackageWhenItIsNot()
		{
			exoticProduct = new ExoticInstallmentsTable();

			e1 = new ExoticInstallment();
			e1.Number = 1;
			e1.PrincipalCoeff = 0.4;
			e1.InterestCoeff = null;

			e2 = new ExoticInstallment();
			e2.Number = 2;
			e2.PrincipalCoeff = 0.3;
			e2.InterestCoeff = null;
			exoticProduct.Add(e1);
			exoticProduct.Add(e2);

			Assert.IsFalse(exoticProduct.IsExoticProductForFlatRatePackage);
		}

		[Test]
		public void TestGetSumOfPrincipalCoeffWhenNoInstallment()
		{
			ExoticInstallmentsTable product = new ExoticInstallmentsTable();
			Assert.AreEqual(0,product.SumOfPrincipalCoeff);
		}

		[Test]
		public void TestGetSumOfPrincipalCoeffWhenSumIsNull()
		{
			ExoticInstallment exoticInstallment1 = new ExoticInstallment();
			ExoticInstallment exoticInstallment2 = new ExoticInstallment();

			ExoticInstallmentsTable product = new ExoticInstallmentsTable();
			product.Add(exoticInstallment1);
			product.Add(exoticInstallment2);

			Assert.AreEqual(0,product.SumOfPrincipalCoeff);
		}

		[Test]
		public void TestGetSumOfPrincipalCoeff()
		{
			ExoticInstallment exoticInstallment1 = new ExoticInstallment();
			exoticInstallment1.PrincipalCoeff = 123;
			ExoticInstallment exoticInstallment2 = new ExoticInstallment();
			exoticInstallment2.PrincipalCoeff = 100;

			ExoticInstallmentsTable product = new ExoticInstallmentsTable();
			product.Add(exoticInstallment1);
			product.Add(exoticInstallment2);

			Assert.AreEqual(223,product.SumOfPrincipalCoeff);
		}

		
		[Test]
		public void TestGetSumOfInterestCoeffWhenNoInstallment()
		{
			ExoticInstallmentsTable product = new ExoticInstallmentsTable();
			Assert.AreEqual(0,product.SumOfInterestCoeff);
		}

		[Test]
		public void TestGetSumOfInterestCoeffWhenSumIsNull()
		{
			ExoticInstallment exoticInstallment1 = new ExoticInstallment();
			ExoticInstallment exoticInstallment2 = new ExoticInstallment();

			ExoticInstallmentsTable product = new ExoticInstallmentsTable();
			product.Add(exoticInstallment1);
			product.Add(exoticInstallment2);

			Assert.AreEqual(0,product.SumOfInterestCoeff);
		}

		[Test]
		public void TestGetSumOfInterestCoeff()
		{
			ExoticInstallment exoticInstallment1 = new ExoticInstallment();
			exoticInstallment1.InterestCoeff = 123;
			ExoticInstallment exoticInstallment2 = new ExoticInstallment();
			exoticInstallment2.InterestCoeff = 100;

			ExoticInstallmentsTable product = new ExoticInstallmentsTable();
			product.Add(exoticInstallment1);
			product.Add(exoticInstallment2);

			Assert.AreEqual(223,product.SumOfInterestCoeff);
		}

        [Test]
        public void CheckSumOfInterestAndPrincipalWhenInterestAndPrincipalCoeffEgal100()
        {
            ExoticInstallment exoticInstallment1 = new ExoticInstallment();
            exoticInstallment1.InterestCoeff = 1;
            exoticInstallment1.PrincipalCoeff = 0;
            ExoticInstallment exoticInstallment2 = new ExoticInstallment();
            exoticInstallment2.InterestCoeff = 0;
            exoticInstallment2.PrincipalCoeff = 1;

            ExoticInstallmentsTable product = new ExoticInstallmentsTable();
            product.Add(exoticInstallment1);
            product.Add(exoticInstallment2);

            Assert.IsTrue(product.CheckIfSumIsOk(OLoanTypes.Flat));
        }

        [Test]
        public void CheckSumOfInterestAndPrincipal_InterestCoefEgal90_PrincipalCoeffEgal100()
        {
            ExoticInstallment exoticInstallment1 = new ExoticInstallment();
            exoticInstallment1.InterestCoeff = 70;
            exoticInstallment1.PrincipalCoeff = 0;
            ExoticInstallment exoticInstallment2 = new ExoticInstallment();
            exoticInstallment2.InterestCoeff = 20;
            exoticInstallment2.PrincipalCoeff = 100;

            ExoticInstallmentsTable product = new ExoticInstallmentsTable();
            product.Add(exoticInstallment1);
            product.Add(exoticInstallment2);

            Assert.IsFalse(product.CheckIfSumIsOk(OLoanTypes.Flat));
        }

        [Test]
        public void CheckSumOfInterestAndPrincipal_InterestCoefEgal100_PrincipalCoeffEgal10()
        {
            ExoticInstallment exoticInstallment1 = new ExoticInstallment();
            exoticInstallment1.InterestCoeff = 70;
            exoticInstallment1.PrincipalCoeff = 0;
            ExoticInstallment exoticInstallment2 = new ExoticInstallment();
            exoticInstallment2.InterestCoeff = 30;
            exoticInstallment2.PrincipalCoeff = 10;

            ExoticInstallmentsTable product = new ExoticInstallmentsTable();
            product.Add(exoticInstallment1);
            product.Add(exoticInstallment2);

            Assert.IsFalse(product.CheckIfSumIsOk(OLoanTypes.Flat));
        }

        [Test]
        public void CheckSumOfInterestAndPrincipal_InterestCoefEgal90_PrincipalCoeffEgal10()
        {
            ExoticInstallment exoticInstallment1 = new ExoticInstallment();
            exoticInstallment1.InterestCoeff = 70;
            exoticInstallment1.PrincipalCoeff = 0;
            ExoticInstallment exoticInstallment2 = new ExoticInstallment();
            exoticInstallment2.InterestCoeff = 20;
            exoticInstallment2.PrincipalCoeff = 10;

            ExoticInstallmentsTable product = new ExoticInstallmentsTable();
            product.Add(exoticInstallment1);
            product.Add(exoticInstallment2);

            Assert.IsFalse(product.CheckIfSumIsOk(OLoanTypes.Flat));
        }

        [Test]
        public void TestSumForDecliningInterestRateType()
        {
            var i1 = new ExoticInstallment() {PrincipalCoeff = 0.7, InterestCoeff = 0};
            var i2 = new ExoticInstallment() {PrincipalCoeff = 0.3, InterestCoeff = 0};
            var exotic = new ExoticInstallmentsTable();
            exotic.Add(i1);
            exotic.Add(i2);
            Assert.IsTrue(exotic.CheckIfSumIsOk(OLoanTypes.DecliningFixedPrincipal));
        }
	}
}
