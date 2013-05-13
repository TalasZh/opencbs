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
