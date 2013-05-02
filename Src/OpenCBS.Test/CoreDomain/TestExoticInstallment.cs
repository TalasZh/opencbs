// LICENSE PLACEHOLDER

using System;
using NUnit.Framework;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;

namespace OpenCBS.Test.CoreDomain
{
	/// <summary>
	/// Summary description for TestExoticInstallment.
	/// </summary>
	[TestFixture]
	public class TestExoticInstallment
	{
		private ExoticInstallment exoticInstallment;

		[SetUp]
		public void SetUp()
		{
			exoticInstallment = new ExoticInstallment();
			exoticInstallment.Number = 1;
			exoticInstallment.PrincipalCoeff = 0.2;
			exoticInstallment.InterestCoeff = 0.4;
		}
		
		[Test]
		public void TestNumberCorrectlySetAndRetrieved()
		{
			Assert.AreEqual(1,exoticInstallment.Number);
		}

		[Test]
		public void TestPrincipalCoeffCorrectlySetAndRetrieved()
		{
			Assert.AreEqual(0.2,exoticInstallment.PrincipalCoeff);
		}

		[Test]
		public void TestInterestCoeffCorrectlySetAndRetrieved()
		{
			Assert.AreEqual(0.4,exoticInstallment.InterestCoeff.Value);
		}

		[Test]
		public void TestExoticProductCorrectlySetAndRetrieved()
		{
			ExoticInstallmentsTable agro = new ExoticInstallmentsTable();
			agro.Name = "agro";
			Assert.AreEqual("agro",agro.Name);
		}
	}
}
