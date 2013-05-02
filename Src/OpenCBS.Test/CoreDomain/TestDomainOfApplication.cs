// LICENSE PLACEHOLDER

using NUnit.Framework;
using OpenCBS.CoreDomain.EconomicActivities;

namespace OpenCBS.Test.CoreDomain
{
	/// <summary>
	/// Summary description for TestDomainOfApplication.
	/// </summary>
	
	[TestFixture]
	public class TestDomainOfApplication
	{
		private EconomicActivity doa;

		[SetUp]
		public void SetUp()
		{
			doa = new EconomicActivity();
		}

		[Test]
		public void TestIfIdIsCorrectlySetAndRetrieved()
		{
			doa.Id = 1;
			Assert.AreEqual(1,doa.Id);
		}

		[Test]
		public void TestIfNameIsCorrectlySetAndRetrieved()
		{
			doa.Name = "Agriculture";
			Assert.AreEqual("Agriculture",doa.Name);
		}

		[Test]
		public void TestIfParentIsCorrectlySetAndRetrieved()
		{
			EconomicActivity parent = new EconomicActivity();
			parent.Id = 1;
			doa.Parent = parent;
			Assert.AreEqual(1,doa.Parent.Id);
		}

		[Test]
		public void TestIfWasDeletedValueIsCorrectlySetAndRetrieved()
		{
			doa.Deleted = true;
			Assert.IsTrue(doa.Deleted);
		}
	}
}
