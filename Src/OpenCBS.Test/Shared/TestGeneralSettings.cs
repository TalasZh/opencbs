// LICENSE PLACEHOLDER

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
