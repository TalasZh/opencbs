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
using OpenCBS.CoreDomain.Contracts.Loans.Installments;

namespace OpenCBS.Test.CoreDomain
{
	/// <summary>
	/// Installment unit testing.
	/// </summary>
	
	[TestFixture]
	public class TestInstallment
	{
	    private Installment _installment;

        [SetUp]
        public void Test_Setup()
        {
            _installment = new Installment(new DateTime(2009, 7, 6), 100, 1000, 0, 0, 0, null, 1);
        }

		[Test]
		public void TestExpectedDate()
		{
			_installment.ExpectedDate = new DateTime(2006,6,28);
			Assert.AreEqual(new DateTime(2006,6,28),_installment.ExpectedDate);
		}

		[Test]
		public void TestInterestsRepayment()
		{
			_installment.InterestsRepayment = 20.5m;
			Assert.AreEqual(20.5m,_installment.InterestsRepayment.Value);
		}

		[Test]
		public void TestCapitalRepayment()
		{
			_installment.CapitalRepayment = 20.5m;
			Assert.AreEqual(20.5m,_installment.CapitalRepayment.Value);
		}

		[Test]
		public void TestPaidInterestsDefaultValue()
		{
			Installment i = new Installment();
			Assert.AreEqual(0m,i.PaidInterests.Value);
		}

		[Test]
		public void TestPaidInterests()
		{
			_installment.PaidInterests = 20.5m;
			Assert.AreEqual(20.5m,_installment.PaidInterests.Value);
		}

		[Test]
		public void TestPaidCapitalDefaultValue()
		{
			Installment i = new Installment();
			Assert.AreEqual(0m,i.PaidCapital.Value);
		}

		[Test]
		public void TestPaidCapital()
		{
			_installment.PaidCapital = 20.5m;
			Assert.AreEqual(20.5m,_installment.PaidCapital.Value);
		}

		[Test]
		public void TestPaidDate()
		{
			_installment.PaidDate = new DateTime(2006,6,28);
			Assert.AreEqual(new DateTime(2006,6,28),_installment.PaidDate.Value);
		}

		[Test]
		public void TestPaidDateNullValue()
		{
			_installment.PaidDate = null;
			Assert.IsTrue(!_installment.PaidDate.HasValue);
		}

        [Test]
        public void TestComment()
        {
            Assert.AreEqual(null, _installment.Comment);
            _installment.Comment = "Can't paid";
            Assert.AreEqual("Can't paid", _installment.Comment);
        }

        [Test]
        public void TestIsPartiallyRepaid1()
        {
            _installment.InterestsRepayment = 100;
            _installment.CapitalRepayment = 1000;

            _installment.PaidInterests = 0;
            _installment.PaidCapital = 0;
            Assert.IsFalse(_installment.IsPartiallyRepaid);
        }

        [Test]
        public void TestIsPartiallyRepaid7()
        {
            _installment.InterestsRepayment = 100;
            _installment.CapitalRepayment = 1000;

            _installment.PaidInterests = 100;
            _installment.PaidCapital = 1000;
            Assert.IsFalse(_installment.IsPartiallyRepaid);
        }

        [Test]
        public void TestIsPartiallyRepaid2()
        {
            _installment.InterestsRepayment = 100;
            _installment.CapitalRepayment = 1000;

            _installment.PaidInterests = 50;
            _installment.PaidCapital = 400;
            Assert.IsTrue(_installment.IsPartiallyRepaid);
        }

        [Test]
        public void TestIsPartiallyRepaid3()
        {
            _installment.InterestsRepayment = 100;
            _installment.CapitalRepayment = 1000;

            _installment.PaidInterests = 50;
            _installment.PaidCapital = 0;
            Assert.IsTrue(_installment.IsPartiallyRepaid);
        }

        [Test]
        public void TestIsPartiallyRepaid4()
        {
            _installment.InterestsRepayment = 100;
            _installment.CapitalRepayment = 1000;

            _installment.PaidInterests = 0;
            _installment.PaidCapital = 500;
            Assert.IsTrue(_installment.IsPartiallyRepaid);
        }

        [Test]
        public void TestIsPartiallyRepaid5()
        {
            _installment.InterestsRepayment = 100;
            _installment.CapitalRepayment = 1000;

            _installment.PaidInterests = 0;
            _installment.PaidCapital = 1000;
            Assert.IsTrue(_installment.IsPartiallyRepaid);
        }

        [Test]
        public void TestIsPartiallyRepaid6()
        {
            _installment.InterestsRepayment = 100;
            _installment.CapitalRepayment = 1000;

            _installment.PaidInterests = 100;
            _installment.PaidCapital = 0;
            Assert.IsTrue(_installment.IsPartiallyRepaid);
        }

	    [Test]
		public void TestIsRepaidWhenCapitalIsNotFullyRepaidAndInterestsAreFullyRepaid()
		{
			_installment.InterestsRepayment = 100;
			_installment.PaidInterests = 100;
			_installment.CapitalRepayment = 1000;
			_installment.PaidCapital = 200;
			Assert.IsFalse(_installment.IsRepaid);
		}

		[Test]
		public void TestIsRepaidWhenInterestsAreNotFullyRepaidAndCapitalIsRepaid()
		{
			_installment.InterestsRepayment = 100;
			_installment.PaidInterests = 50;
			_installment.CapitalRepayment = 1000;
			_installment.PaidCapital = 1000;
			Assert.IsFalse(_installment.IsRepaid);			
		}

		[Test]
		public void TestIsRepaidWhenInterestsAndCapitalAreNotFullyRepaid()
		{
			_installment.InterestsRepayment = 100;
			_installment.PaidInterests = 50;
			_installment.CapitalRepayment = 1000;
			_installment.PaidCapital = 500;
			Assert.IsFalse(_installment.IsRepaid);		
		}

		[Test]
		public void TestIsRepaidWhenInterestsAndCapitalAreFullyRepaid()
		{
			_installment.InterestsRepayment = 100;
			_installment.PaidInterests = 100;
			_installment.CapitalRepayment = 1000;
			_installment.PaidCapital = 1000;
			Assert.IsTrue(_installment.IsRepaid);					
		}

		[Test]
		public void TestAmountHasToPay()
		{
			_installment.InterestsRepayment = 20;
			_installment.CapitalRepayment = 30;
			_installment.PaidCapital = 0;
			_installment.PaidInterests = 0;
			Assert.AreEqual(50m,_installment.AmountHasToPayWithInterest.Value);

			_installment.PaidCapital = 5;
			Assert.AreEqual(45m,_installment.AmountHasToPayWithInterest.Value);

			_installment.PaidInterests = 7;
			Assert.AreEqual(38m,_installment.AmountHasToPayWithInterest.Value);

			_installment.PaidCapital = 30;
			_installment.PaidInterests = 20;
			Assert.AreEqual(0m,_installment.AmountHasToPayWithInterest.Value);
		}
        
		[Test]
		public void TestAmount()
		{
			_installment.InterestsRepayment = 20;
			_installment.CapitalRepayment = 30;
			
			Assert.AreEqual(50m,_installment.Amount.Value);
		}

		[Test]
		public void TestInstallmentCopy()
		{
            Installment installment = new Installment(new DateTime(2006, 2, 1), 100, 102, 234, 32, 0, null, 1);
			Installment installmentCopyFake = installment;
			Installment installmentCopy = installment.Copy();
			
			installment.PaidCapital = 2345678;
			Assert.AreEqual(2345678,installmentCopyFake.PaidCapital.Value);
			Assert.AreEqual(234,installmentCopy.PaidCapital.Value);
		}

        [Test]
        public void Test_Equals_IfNull_ReturnsFalse()
        {
            Assert.AreEqual(_installment.Equals(null), false);
        }

        [Test]
        public void Test_Equals_IfCopied_ReturnsTrue()
        {
            Installment i2 = _installment.Copy();
            Assert.AreEqual(_installment.Equals(i2), true);
        }

        [Test]
        public void Test_Equals_IfNumberNotEqual_ReturnsFalse()
        {
            Installment i2 = _installment.Copy();
            i2.Number = 2;
            Assert.AreEqual(_installment.Equals(i2), false);
        }

        [Test]
        public void Test_Equals_IfPrincipalNotEqual_ReturnsFalse()
        {
            Installment i2 = _installment.Copy();
            i2.CapitalRepayment = 2000m;
            Assert.AreEqual(_installment.Equals(i2), false);
        }

        [Test]
        public void Test_Equals_IfInterestNotEqual_ReturnsFalse()
        {
            Installment i2 = _installment.Copy();
            i2.InterestsRepayment = 200m;
            Assert.AreEqual(_installment.Equals(i2), false);
        }

        [Test]
        public void Test_Equals_IfDateNotEqual_ReturnsFalse()
        {
            Installment i2 = _installment.Copy();
            i2.ExpectedDate = new DateTime(2009, 7, 20);
            Assert.AreEqual(_installment.Equals(i2), false);
        }
	}

}
