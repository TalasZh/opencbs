// LICENSE PLACEHOLDER

using System;
using OpenCBS.CoreDomain;
using NUnit.Framework;
using OpenCBS.CoreDomain.Clients;

namespace OpenCBS.Test.CoreDomain
{
	[TestFixture]
	public class TestGuarantor
	{
		Guarantor guarantor = new Guarantor();

		[Test]
		public void TestTiersCorrectlySetAndRetrieved()
		{
			Person person = new Person();
			person.LastName = "TOTO";
			guarantor.Tiers = person;
			Assert.AreEqual("TOTO",((Person)guarantor.Tiers).LastName);
		}

		[Test]
		public void TestAmountCorrectlySetAndRetrieved()
		{
			guarantor.Amount = 200.50m;
			Assert.AreEqual(200.50m,guarantor.Amount.Value);
		}
	}
}
