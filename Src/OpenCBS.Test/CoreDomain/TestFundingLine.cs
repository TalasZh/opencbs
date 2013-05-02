// LICENSE PLACEHOLDER

using System;
using OpenCBS.CoreDomain;
using NUnit.Framework;
using OpenCBS.CoreDomain.FundingLines;

namespace OpenCBS.Test.CoreDomain
{
	
	[TestFixture]
	public class TestFundingLine
	{
		private FundingLine fundingLine = new FundingLine("AFD130",false);

		[Test]
		public void CodeCorrectlySetAndRetrieved()
		{
            FundingLine fundingLine = new FundingLine("AFD130",false);
            Assert.AreEqual("AFD130", fundingLine.Name);
		}

		[Test]
		public void DeletedCorrectlySetAndRetrieved()
		{
			Assert.IsFalse(fundingLine.Deleted);
		}

		[Test]
		public void TestIfToStringReturnCode()
		{
			Assert.AreEqual("AFD130",fundingLine.ToString());
		}
	}
}
