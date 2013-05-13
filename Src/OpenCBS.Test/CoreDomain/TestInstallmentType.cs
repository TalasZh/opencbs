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
