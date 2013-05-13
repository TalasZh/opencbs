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
