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
using System.Collections;
using NUnit.Framework;
using OpenCBS.Enums;
using OpenCBS.Manager;
using OpenCBS.Shared;

namespace OpenCBS.Test.Manager
{
	/// <summary>
	/// Summary description for TestDatabaseParametersManagement.
	/// </summary>
	[TestFixture]
    public class TestGeneralSettingsManager : BaseManagerTest
	{
        private GeneralSettingsManager generalSettingsManager;

		[TestFixtureSetUp]
		public void TextFixtureSetUp()
		{
            ApplicationSettings.GetInstance().SetSetting(ApplicationSettings.Settings.ONLINE, false);
            generalSettingsManager = new GeneralSettingsManager(DataUtil.TESTDB);
		}

		[Test]
		public void TestUpdateGroupCashReceipts()
		{
			GeneralSettings dataParam = GeneralSettings.GetInstance("");
            generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.GROUPEDCASHRECEIPTS, true);
			Assert.AreEqual(true, dataParam.IsCashReceiptsGrouped);

			generalSettingsManager.FillGeneralSettings();
			Assert.AreEqual(true, dataParam.IsCashReceiptsGrouped);

            generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.GROUPEDCASHRECEIPTS, false);
			Assert.AreEqual(false, dataParam.IsCashReceiptsGrouped);

			generalSettingsManager.FillGeneralSettings();
			Assert.AreEqual(false, dataParam.IsCashReceiptsGrouped);
		}

		[Test]
		public void TestUpdateCashReceiptBeforeConfirmation()
		{
			GeneralSettings dataParam = GeneralSettings.GetInstance("");
            generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.CASHRECEIPTBEFORECONFIRMATION, true);
			Assert.AreEqual(true, dataParam.IsCashReceiptBeforeConfirmation);

            generalSettingsManager.FillGeneralSettings();
			Assert.AreEqual(true, dataParam.IsCashReceiptBeforeConfirmation);

            generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.CASHRECEIPTBEFORECONFIRMATION, false);
			Assert.AreEqual(false, dataParam.IsCashReceiptBeforeConfirmation);

            generalSettingsManager.FillGeneralSettings();
			Assert.AreEqual(false, dataParam.IsCashReceiptBeforeConfirmation);
		}

		[Test]
		public void TestUpdateInternalCurrency()
		{
			GeneralSettings dataParam = GeneralSettings.GetInstance("");
            generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.INTERNALCURRENCY, "SOM");
			Assert.AreEqual("SOM", dataParam.InternalCurrency);

            generalSettingsManager.FillGeneralSettings();
			Assert.AreEqual("SOM", dataParam.InternalCurrency);

            generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.INTERNALCURRENCY, "USD");
			Assert.AreEqual("USD", dataParam.InternalCurrency);

            generalSettingsManager.FillGeneralSettings();
			Assert.AreEqual("USD", dataParam.InternalCurrency);
		}

		[Test]
		public void TestUpdateExternalCurrency()
		{
			GeneralSettings dataParam = GeneralSettings.GetInstance("");
            generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.EXTERNALCURRENCY, "SOM");
			Assert.AreEqual("SOM", dataParam.ExternalCurrency);

			generalSettingsManager.FillGeneralSettings();
			Assert.AreEqual("SOM", dataParam.ExternalCurrency);

            generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.EXTERNALCURRENCY, "USD");
			Assert.AreEqual("USD", dataParam.ExternalCurrency);

			generalSettingsManager.FillGeneralSettings();
			Assert.AreEqual("USD", dataParam.ExternalCurrency);
		}

		[Test]
			public void TestUpdateExternalCurrencyWhenNull()
		{
			GeneralSettings dataParam = GeneralSettings.GetInstance("");
            generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.EXTERNALCURRENCY, null);
			Assert.AreEqual(null, dataParam.ExternalCurrency);

			generalSettingsManager.FillGeneralSettings();
			Assert.AreEqual(null, dataParam.ExternalCurrency);
		}

		[Test]
		public void TestUpdateBranchCode()
		{
			GeneralSettings dataParam = GeneralSettings.GetInstance("");
            generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.BRANCHCODE, "KT");
			Assert.AreEqual("KT", dataParam.BranchCode);

			generalSettingsManager.FillGeneralSettings();
			Assert.AreEqual("KT", dataParam.BranchCode);

            generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.BRANCHCODE, "FR");
			Assert.AreEqual("FR", dataParam.BranchCode);

			generalSettingsManager.FillGeneralSettings();
			Assert.AreEqual("FR", dataParam.BranchCode);
		}

		[Test]
		public void TestUpdateCountry()
		{
			GeneralSettings dataParam = GeneralSettings.GetInstance("");
            generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.COUNTRY, "FRANCE");
			Assert.AreEqual("FRANCE", dataParam.Country);

			generalSettingsManager.FillGeneralSettings();
			Assert.AreEqual("FRANCE", dataParam.Country);

            generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.COUNTRY, "AGHANISTAN");
			Assert.AreEqual("AGHANISTAN", dataParam.Country);

			generalSettingsManager.FillGeneralSettings();
			Assert.AreEqual("AGHANISTAN", dataParam.Country);
		}

		[Test]
		public void TestUpdateWeekEndDay1()
		{
			GeneralSettings dataParam = GeneralSettings.GetInstance("");
            generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.WEEKENDDAY1, 1);
			Assert.AreEqual(1, dataParam.WeekEndDay1);

			generalSettingsManager.FillGeneralSettings();
			Assert.AreEqual(1, dataParam.WeekEndDay1);

            generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.WEEKENDDAY1, 4);
			Assert.AreEqual(4, dataParam.WeekEndDay1);

			generalSettingsManager.FillGeneralSettings();
			Assert.AreEqual(4, dataParam.WeekEndDay1);
		}

		[Test]
		public void TestUpdateWeekEndDay2()
		{
			GeneralSettings dataParam = GeneralSettings.GetInstance("");
            generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.WEEKENDDAY2, 2);
			Assert.AreEqual(2, dataParam.WeekEndDay2);

			generalSettingsManager.FillGeneralSettings();
			Assert.AreEqual(2, dataParam.WeekEndDay2);

            generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.WEEKENDDAY2, 5);
			Assert.AreEqual(5, dataParam.WeekEndDay2);

			generalSettingsManager.FillGeneralSettings();
			Assert.AreEqual(5, dataParam.WeekEndDay2);
		}

		[Test]
		public void TestUpdatePayFirstInstallmentRealValue()
		{
			GeneralSettings dataParam = GeneralSettings.GetInstance("");
            generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.PAYFIRSTINSTALLMENTREALVALUE, true);
			Assert.AreEqual(true, dataParam.PayFirstInterestRealValue);

			generalSettingsManager.FillGeneralSettings();
			Assert.AreEqual(true, dataParam.PayFirstInterestRealValue);

            generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.PAYFIRSTINSTALLMENTREALVALUE, false);
			Assert.AreEqual(false, dataParam.PayFirstInterestRealValue);

			generalSettingsManager.FillGeneralSettings();
			Assert.AreEqual(false, dataParam.PayFirstInterestRealValue);
		}

		[Test]
		public void TestUpdateCityMandatory()
		{
			GeneralSettings dataParam = GeneralSettings.GetInstance("");
            generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.CITYMANDATORY, true);
			Assert.AreEqual(true, dataParam.IsCityMandatory);

			generalSettingsManager.FillGeneralSettings();
			Assert.AreEqual(true, dataParam.IsCityMandatory);

            generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.CITYMANDATORY, false);
			Assert.AreEqual(false, dataParam.IsCityMandatory);

			generalSettingsManager.FillGeneralSettings();
			Assert.AreEqual(false, dataParam.IsCityMandatory);
		}

		[Test]
		public void TestUpdateGroupMinMembers()
		{
			GeneralSettings dataParam = GeneralSettings.GetInstance("");
            generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.GROUPMINMEMBERS, 2);
			Assert.AreEqual(2, dataParam.GroupMinMembers);

			generalSettingsManager.FillGeneralSettings();
			Assert.AreEqual(2, dataParam.GroupMinMembers);

            generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.GROUPMINMEMBERS, 5);
			Assert.AreEqual(5, dataParam.GroupMinMembers);

			generalSettingsManager.FillGeneralSettings();
			Assert.AreEqual(5, dataParam.GroupMinMembers);
		}

		[Test]
		public void TestUpdateCityOpenValue()
		{
			GeneralSettings dataParam = GeneralSettings.GetInstance("");
            generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.CITYOPENVALUE, true);
			Assert.AreEqual(true, dataParam.IsCityAnOpenValue);

			generalSettingsManager.FillGeneralSettings();
			Assert.AreEqual(true, dataParam.IsCityAnOpenValue);

            generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.CITYOPENVALUE, false);
			Assert.AreEqual(false, dataParam.IsCityAnOpenValue);

			generalSettingsManager.FillGeneralSettings();
			Assert.AreEqual(false, dataParam.IsCityAnOpenValue);
		}

		[Test]
		public void TestUpdateLoanOfficerPortfolioFilter()
		{
			GeneralSettings dataParam = GeneralSettings.GetInstance("");
            generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.LOANOFFICERPORTFOLIOFILTER, true);
			Assert.AreEqual(true, dataParam.IsLoanOfficerPortfolioActive);

			generalSettingsManager.FillGeneralSettings();
			Assert.AreEqual(true, dataParam.IsLoanOfficerPortfolioActive);

            generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.LOANOFFICERPORTFOLIOFILTER, false);
			Assert.AreEqual(false, dataParam.IsLoanOfficerPortfolioActive);

			generalSettingsManager.FillGeneralSettings();
			Assert.AreEqual(false, dataParam.IsLoanOfficerPortfolioActive);
		}

		[Test]
		public void TestSelectAllParameters()
		{
			generalSettingsManager.FillGeneralSettings();
			GeneralSettings dataParam = GeneralSettings.GetInstance("");

			Assert.AreEqual(29, dataParam.DbParamList.Count);
		}
		[Test]
		public void TestAddIntParameter()
        {
            generalSettingsManager.DeleteSelectedParameter("INTPARAM");
			DictionaryEntry entry = new DictionaryEntry("INTPARAM", 2);
			generalSettingsManager.AddParameter(entry);
            Assert.AreEqual(2, Convert.ToInt32(generalSettingsManager.SelectParameterValue("INTPARAM")));
			generalSettingsManager.DeleteSelectedParameter("INTPARAM");
		}

		[Test]
		public void TestAddStringParameter()
        {
            generalSettingsManager.DeleteSelectedParameter("STRINGPARAM");
			DictionaryEntry entry = new DictionaryEntry("STRINGPARAM", "hello");
			generalSettingsManager.AddParameter(entry);
            Assert.AreEqual("hello", generalSettingsManager.SelectParameterValue("STRINGPARAM").ToString());
			generalSettingsManager.DeleteSelectedParameter("STRINGPARAM");
		}

		[Test]
		public void TestAddDoubleParameter()
		{
            double val = 2;
            generalSettingsManager.DeleteSelectedParameter("DOUBLEPARAM");
			DictionaryEntry entry = new DictionaryEntry("DOUBLEPARAM", val);
			generalSettingsManager.AddParameter(entry);
            Assert.AreEqual(2, Convert.ToDouble(generalSettingsManager.SelectParameterValue("DOUBLEPARAM")));
			generalSettingsManager.DeleteSelectedParameter("DOUBLEPARAM");
		}

		[Test]
		public void TestAddBooleanParameter()
        {
            generalSettingsManager.DeleteSelectedParameter("BOOLPARAM");
			DictionaryEntry entry = new DictionaryEntry("BOOLPARAM", true);
			generalSettingsManager.AddParameter(entry);
            Assert.AreEqual(true, Convert.ToBoolean(generalSettingsManager.SelectParameterValue("BOOLPARAM")));
			generalSettingsManager.DeleteSelectedParameter("BOOLPARAM");
		}

		[Test]
		public void TestAddDateParameter()
        {
            generalSettingsManager.DeleteSelectedParameter("DATEPARAM");
			DictionaryEntry entry = new DictionaryEntry("DATEPARAM", new DateTime(2006, 1, 1));
			generalSettingsManager.AddParameter(entry);
            Assert.AreEqual(new DateTime(2006, 1, 1), Convert.ToDateTime(generalSettingsManager.SelectParameterValue("DATEPARAM")));
			generalSettingsManager.DeleteSelectedParameter("DATEPARAM");
		}

		[Test]
		public void TestAddNullParameterForExternalCurrency()
        {
            generalSettingsManager.DeleteSelectedParameter("NULLPARAM");
			DictionaryEntry entry = new DictionaryEntry("NULLPARAM", null);
			generalSettingsManager.AddParameter(entry);
            Assert.IsNull(generalSettingsManager.SelectParameterValue("NULLPARAM"));
			generalSettingsManager.DeleteSelectedParameter("NULLPARAM");
		}
	}
}
