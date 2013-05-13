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
