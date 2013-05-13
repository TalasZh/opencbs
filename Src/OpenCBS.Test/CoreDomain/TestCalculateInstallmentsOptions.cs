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
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.CoreDomain.Contracts.Loans;
using OpenCBS.CoreDomain.Contracts.Loans.CalculateInstallments;
using OpenCBS.Enums;
using OpenCBS.Shared;
using OpenCBS.Shared.Settings;

namespace OpenCBS.Test.CoreDomain
{
	/// <summary>
	/// Description of TestCalculateInstallmentsOptions.
	/// </summary>
	[TestFixtureAttribute]
	public class TestCalculateInstallmentsOptions
	{
	    private CalculateInstallmentsOptions _installmentsOptions;
		[TestFixtureSetUpAttribute]
		public void testFixtureSetUp()
		{
			Loan credit = new Loan(new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), 
                ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));
		    credit.Id = 4;

            _installmentsOptions = new CalculateInstallmentsOptions(credit.StartDate, OLoanTypes.Flat, true, credit, false);
		}
		
		[Test]
		public void Get_Set_Credit()
		{
			Assert.AreEqual(4,_installmentsOptions.Contract.Id);
		}

        [Test]
        public void Get_Set_ChangeDate()
        {
            Assert.IsFalse(_installmentsOptions.ChangeDate);
        }

        [Test]
        public void Get_Set_LoanType()
        {
            Assert.AreEqual(OLoanTypes.Flat, _installmentsOptions.LoanType);
        }

        [Test]
        public void Get_Set_IsExotic()
        {
            Assert.IsTrue(_installmentsOptions.IsExotic);
        }
	}
}
