// LICENSE PLACEHOLDER

using System;
using OpenCBS.CoreDomain;
using NUnit.Framework;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;

namespace OpenCBS.Test.CoreDomain
{
	/// <summary>
	/// Summary description for TestInstallmentType.
	/// </summary>
	[TestFixture]
	public class TestInstallmentType
	{
		private InstallmentType installmentType;

		[Test]
		public void TestIfInstallmentTypeIdIsCorrectlySetAndRetrieved()
		{
			installmentType = new InstallmentType();
			installmentType.Id = 1;
			Assert.AreEqual(1,installmentType.Id);
		}

		[Test]
		public void TestIfInstallmentTypeNameIsCorrectlySetAndRetrieved()
		{
			installmentType = new InstallmentType();
			installmentType.Name = "Monthly";
			Assert.AreEqual("Monthly",installmentType.Name);
		}

		[Test]
		public void TestNbOfDaysEqualsToZeroWhenNoSpecifiedValue()
		{
			InstallmentType noDays = new InstallmentType();
			Assert.AreEqual(0,noDays.NbOfDays);
		}

		[Test]
		public void TestNbOfDaysCorrectlySetAndRetrieved()
		{
			installmentType.NbOfDays = 4;
			Assert.AreEqual(4,installmentType.NbOfDays);
		}

		[Test]
		public void TestNbOfMonthsEqualsToZeroWhenNoSpecifiedValue()
		{
			InstallmentType noMonths = new InstallmentType();
			Assert.AreEqual(0,noMonths.NbOfMonths);
		}

		[Test]
		public void TestNbOfMonthsIsCorrectlySetAndRetrieved()
		{
			installmentType.NbOfMonths = 5;
			Assert.AreEqual(5,installmentType.NbOfMonths);
		}

		[Test]
		public void TestIfToStringReturnName()
		{
			installmentType.Name = "Monthly";
			Assert.AreEqual("Monthly",installmentType.ToString());
		}
	}
}
