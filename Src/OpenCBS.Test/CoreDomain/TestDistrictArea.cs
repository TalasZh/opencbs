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
	public class TestDistrict
	{
		private District district;

		public TestDistrict()
		{
			district = new District();
		}

		[Test]
		public void TestConstructor()
		{
			District district = new District(1,"tress",new Province(1,"Sugh"));
			Assert.AreEqual(1,district.Id);
			Assert.AreEqual("tress",district.Name);
			Assert.AreEqual(1,district.Province.Id);
			Assert.AreEqual("Sugh",district.Province.Name);
		}
		
		[Test]
		public void IdCorrectlySetAndRetrieved()
		{
			district.Id = 1;
			Assert.AreEqual(1,district.Id);
		}

		[Test]
		public void NameCorrectlySetAndRetrieved()
		{
			district.Name = "France";
			Assert.AreEqual("France",district.Name);
		}

		[Test]
		public void ProvinceCorrectlySetAndRetrieved()
		{
			district.Province = new Province(1,"Sugh");
			Assert.AreEqual(1,district.Province.Id);
			Assert.AreEqual("Sugh",district.Province.Name);
		}
	}
}
