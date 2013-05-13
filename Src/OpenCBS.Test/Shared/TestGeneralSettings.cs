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
using OpenCBS.Enums;
using OpenCBS.Shared;
using OpenCBS.Shared.Settings;

namespace OpenCBS.Test.Shared
{
	/// <summary>
	/// Summary description for TestGeneralSettings.
	/// </summary>
	[TestFixture]
	public class TestGeneralSettings
	{
		ApplicationSettings dbParam;

		[TestFixtureSetUp]
		public void TestFixtureSetUp()
		{
			dbParam = ApplicationSettings.GetInstance("");
			dbParam.DeleteAllParameters();
		}

        [Test]
        public void GetAccountingProcess_Cash()
        {
            dbParam.DeleteAllParameters();
            dbParam.AddParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Cash);
            Assert.AreEqual(OAccountingProcesses.Cash,dbParam.AccountingProcesses);
        }

        [Test]
        public void GetAccountingProcess_Accrual()
        {
            dbParam.DeleteAllParameters();
            dbParam.AddParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);
            Assert.AreEqual(OAccountingProcesses.Accrual, dbParam.AccountingProcesses);
        }

        [Test]
        public void GetLateDaysAfterAccrualCeases_NotNull()
        {
            dbParam.DeleteAllParameters();
            dbParam.AddParameter(OGeneralSettings.LATEDAYSAFTERACCRUALCEASES, 90);
            Assert.AreEqual(90,dbParam.LateDaysAfterAccrualCeases);
        }

        [Test]
        public void GetLateDaysAfterAccrualCeases_0()
        {
            dbParam.DeleteAllParameters();
            dbParam.AddParameter(OGeneralSettings.LATEDAYSAFTERACCRUALCEASES, null);
            Assert.AreEqual(0, dbParam.LateDaysAfterAccrualCeases);
        }
        [Test]
        public void GetAllowMultipleLoans_true()
        {
            dbParam.DeleteAllParameters();
            dbParam.AddParameter(OGeneralSettings.ALLOWSMULTIPLELOANS, true);
            Assert.IsTrue(dbParam.IsAllowMultipleLoans);
            

        }
	}
}
