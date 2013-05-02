// LICENSE PLACEHOLDER

using System;
using OpenCBS.CoreDomain;
using NUnit.Framework;

namespace OpenCBS.Test.CoreDomain
{
	/// <summary>
	/// Description r�sum�e de TestCountryArea.
	/// </summary>
	/// 
	[TestFixture]
	public class TestProvince
	{
		private Province province;

		public TestProvince()
		{
			province = new Province();
		}
		[Test]
		public void IdCorrectlySetAndRetrieved()
		{
			province.Id = 1;
			Assert.AreEqual(1,province.Id);
		}

		[Test]
		public void NameCorrectlySetAndRetrieved()
		{
			province.Name = "France";
			Assert.AreEqual("France",province.Name);
		}
	}
}
