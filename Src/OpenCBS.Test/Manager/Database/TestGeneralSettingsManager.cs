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
using OpenCBS.Manager.Database;
using OpenCBS.Shared.Settings;

namespace OpenCBS.Test.Manager.Database
{
    /// <summary>
    /// Summary description for TestDatabaseParametersManagement.
    /// </summary>
    [TestFixture]
    public class TestGeneralSettingsManager : BaseManagerTest
    {
        private ApplicationSettingsManager _generalSettingsManager;

        [TestFixtureSetUp]
        public void TextFixtureSetUp()
        {
            TechnicalSettings.UseOnlineMode = false;
            _generalSettingsManager = new ApplicationSettingsManager(DataUtil.TESTDB);
        }

        [Test]
        public void TestUpdateCountry()
        {
            ApplicationSettings dataParam = ApplicationSettings.GetInstance("");
            _generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.COUNTRY, "FRANCE");
            Assert.AreEqual("FRANCE", dataParam.Country);

            _generalSettingsManager.FillGeneralSettings();
            Assert.AreEqual("FRANCE", dataParam.Country);

            _generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.COUNTRY, "AGHANISTAN");
            Assert.AreEqual("AGHANISTAN", dataParam.Country);

            _generalSettingsManager.FillGeneralSettings();
            Assert.AreEqual("AGHANISTAN", dataParam.Country);
        }

        [Test]
        public void TestUpdateWeekEndDay1()
        {
            ApplicationSettings dataParam = ApplicationSettings.GetInstance("");
            _generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.WEEKENDDAY1, 1);
            Assert.AreEqual(1, dataParam.WeekEndDay1);

            _generalSettingsManager.FillGeneralSettings();
            Assert.AreEqual(1, dataParam.WeekEndDay1);

            _generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.WEEKENDDAY1, 4);
            Assert.AreEqual(4, dataParam.WeekEndDay1);

            _generalSettingsManager.FillGeneralSettings();
            Assert.AreEqual(4, dataParam.WeekEndDay1);
        }

        [Test]
        public void TestUpdateWeekEndDay2()
        {
            ApplicationSettings dataParam = ApplicationSettings.GetInstance("");
            _generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.WEEKENDDAY2, 2);
            Assert.AreEqual(2, dataParam.WeekEndDay2);

            _generalSettingsManager.FillGeneralSettings();
            Assert.AreEqual(2, dataParam.WeekEndDay2);

            _generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.WEEKENDDAY2, 5);
            Assert.AreEqual(5, dataParam.WeekEndDay2);

            _generalSettingsManager.FillGeneralSettings();
            Assert.AreEqual(5, dataParam.WeekEndDay2);
        }

        [Test]
        public void TestInterestRateDecimalPlaces()
        {
            ApplicationSettings dataParam = ApplicationSettings.GetInstance("");
            _generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.INTEREST_RATE_DECIMAL_PLACES, 5);
            _generalSettingsManager.FillGeneralSettings();
            Assert.AreEqual(5, dataParam.InterestRateDecimalPlaces);
        }

        [Test]
        public void TestUpdatePayFirstInstallmentRealValue()
        {
            ApplicationSettings dataParam = ApplicationSettings.GetInstance("");
            _generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.PAYFIRSTINSTALLMENTREALVALUE, true);
            Assert.AreEqual(true, dataParam.PayFirstInterestRealValue);

            _generalSettingsManager.FillGeneralSettings();
            Assert.AreEqual(true, dataParam.PayFirstInterestRealValue);

            _generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.PAYFIRSTINSTALLMENTREALVALUE, false);
            Assert.AreEqual(false, dataParam.PayFirstInterestRealValue);

            _generalSettingsManager.FillGeneralSettings();
            Assert.AreEqual(false, dataParam.PayFirstInterestRealValue);
        }

        [Test]
        public void TestUpdateCityMandatory()
        {
            ApplicationSettings dataParam = ApplicationSettings.GetInstance("");
            _generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.CITYMANDATORY, true);
            Assert.AreEqual(true, dataParam.IsCityMandatory);

            _generalSettingsManager.FillGeneralSettings();
            Assert.AreEqual(true, dataParam.IsCityMandatory);

            _generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.CITYMANDATORY, false);
            Assert.AreEqual(false, dataParam.IsCityMandatory);

            _generalSettingsManager.FillGeneralSettings();
            Assert.AreEqual(false, dataParam.IsCityMandatory);
        }

        [Test]
        public void TestUpdateAutomaticID()
        {
            ApplicationSettings dataParam = ApplicationSettings.GetInstance("");
            _generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.AUTOMATIC_ID, false);
            Assert.AreEqual(false, dataParam.IsAutomaticID);

            _generalSettingsManager.FillGeneralSettings();
            Assert.AreEqual(false, dataParam.IsAutomaticID);

            _generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.AUTOMATIC_ID, true);
            Assert.AreEqual(true, dataParam.IsAutomaticID);

            _generalSettingsManager.FillGeneralSettings();
            Assert.AreEqual(true, dataParam.IsAutomaticID);
        }

        [Test]
        public void TestUpdateGroupMinMembers()
        {
            ApplicationSettings dataParam = ApplicationSettings.GetInstance("");
            _generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.GROUPMINMEMBERS, 2);
            Assert.AreEqual(2, dataParam.GroupMinMembers);

            _generalSettingsManager.FillGeneralSettings();
            Assert.AreEqual(2, dataParam.GroupMinMembers);

            _generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.GROUPMINMEMBERS, 5);
            Assert.AreEqual(5, dataParam.GroupMinMembers);

            _generalSettingsManager.FillGeneralSettings();
            Assert.AreEqual(5, dataParam.GroupMinMembers);
        }

        [Test]
        public void TestUpdateGroupMaxMembers()
        {
            ApplicationSettings dataParam = ApplicationSettings.GetInstance("");
            _generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.GROUPMAXMEMBERS, 6);
            Assert.AreEqual(6, dataParam.GroupMaxMembers);

            _generalSettingsManager.FillGeneralSettings();
            Assert.AreEqual(6, dataParam.GroupMaxMembers);

            _generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.GROUPMAXMEMBERS, 10);
            Assert.AreEqual(10, dataParam.GroupMaxMembers);

            _generalSettingsManager.FillGeneralSettings();
            Assert.AreEqual(10, dataParam.GroupMaxMembers);
        }

        [Test]
        public void TestUpdateVillageMinMembers()
        {
            ApplicationSettings dataParam = ApplicationSettings.GetInstance("");
            _generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.VILLAGEMINMEMBERS, 2);
            Assert.AreEqual(2, dataParam.VillageMinMembers);

            _generalSettingsManager.FillGeneralSettings();
            Assert.AreEqual(2, dataParam.VillageMinMembers);

            _generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.VILLAGEMINMEMBERS, 5);
            Assert.AreEqual(5, dataParam.VillageMinMembers);

            _generalSettingsManager.FillGeneralSettings();
            Assert.AreEqual(5, dataParam.VillageMinMembers);
        }

        [Test]
        public void TestUpdateVillageMaxMembers()
        {
            ApplicationSettings dataParam = ApplicationSettings.GetInstance("");
            _generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.VILLAGEMAXMEMBERS, 6);
            Assert.AreEqual(6, dataParam.VillageMaxMembers);

            _generalSettingsManager.FillGeneralSettings();
            Assert.AreEqual(6, dataParam.VillageMaxMembers);

            _generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.VILLAGEMAXMEMBERS, 10);
            Assert.AreEqual(10, dataParam.VillageMaxMembers);

            _generalSettingsManager.FillGeneralSettings();
            Assert.AreEqual(10, dataParam.VillageMaxMembers);
        }

        [Test]
        public void TestUpdateClientAgeMin()
        {
            ApplicationSettings dataParam = ApplicationSettings.GetInstance("");
            _generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.CLIENT_AGE_MIN, 19);
            Assert.AreEqual(19, dataParam.ClientAgeMin);

            _generalSettingsManager.FillGeneralSettings();
            Assert.AreEqual(19, dataParam.ClientAgeMin);

            _generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.CLIENT_AGE_MIN, 20);
            Assert.AreEqual(20, dataParam.ClientAgeMin);
            
            _generalSettingsManager.FillGeneralSettings();
            Assert.AreEqual(20, dataParam.ClientAgeMin);
        }

        [Test]
        public void TestUpdateClientAgeMax()
        {
            ApplicationSettings dataParam = ApplicationSettings.GetInstance("");
            _generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.CLIENT_AGE_MAX, 99);
            Assert.AreEqual(99, dataParam.ClientAgeMax);

            _generalSettingsManager.FillGeneralSettings();
            Assert.AreEqual(99, dataParam.ClientAgeMax);

            _generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.CLIENT_AGE_MAX, 100);
            Assert.AreEqual(100, dataParam.ClientAgeMax);

            _generalSettingsManager.FillGeneralSettings();
            Assert.AreEqual(100, dataParam.ClientAgeMax);
        }

        public void TestUpdateMaxLoansCovered()
        {
            ApplicationSettings dataParam = ApplicationSettings.GetInstance("");
            _generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.MAX_LOANS_COVERED, 5);
            Assert.AreEqual(5, dataParam.MaxLoansCovered);

            _generalSettingsManager.FillGeneralSettings();
            Assert.AreEqual(5, dataParam.MaxLoansCovered);

            _generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.MAX_LOANS_COVERED, 10);
            Assert.AreEqual(10, dataParam.MaxLoansCovered);

            _generalSettingsManager.FillGeneralSettings();
            Assert.AreEqual(10, dataParam.MaxLoansCovered);
        }

        [Test]
        public void TestUpdateMaxGuarantorAmount()
        {
            ApplicationSettings dataParam = ApplicationSettings.GetInstance("");
            _generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.MAX_GUARANTOR_AMOUNT, 10000);
            Assert.AreEqual(10000, dataParam.MaxGuarantorAmount);

            _generalSettingsManager.FillGeneralSettings();
            Assert.AreEqual(10000, dataParam.MaxGuarantorAmount);

            _generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.MAX_GUARANTOR_AMOUNT, 20000);
            Assert.AreEqual(20000, dataParam.MaxGuarantorAmount);

            _generalSettingsManager.FillGeneralSettings();
            Assert.AreEqual(20000, dataParam.MaxGuarantorAmount);
        }

        [Test]
        public void TestUpdateCityOpenValue()
        {
            ApplicationSettings dataParam = ApplicationSettings.GetInstance("");
            _generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.CITYOPENVALUE, true);
            Assert.AreEqual(true, dataParam.IsCityAnOpenValue);

            _generalSettingsManager.FillGeneralSettings();
            Assert.AreEqual(true, dataParam.IsCityAnOpenValue);

            _generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.CITYOPENVALUE, false);
            Assert.AreEqual(false, dataParam.IsCityAnOpenValue);

            _generalSettingsManager.FillGeneralSettings();
            Assert.AreEqual(false, dataParam.IsCityAnOpenValue);
        }

        [Test]
        public void TestUpdateSavingsCodeTemplate()
        {
            ApplicationSettings dataParam = ApplicationSettings.GetInstance("");
            _generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.SAVINGS_CODE_TEMPLATE, "BC/YY/PC-PS/CN-ID");
            Assert.AreEqual("BC/YY/PC-PS/CN-ID", dataParam.SavingsCodeTemplate);

            _generalSettingsManager.FillGeneralSettings();
            Assert.AreEqual("BC/YY/PC-PS/CN-ID", dataParam.SavingsCodeTemplate);

             _generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.SAVINGS_CODE_TEMPLATE, "IC/BC/CS/ID");
            Assert.AreEqual("IC/BC/CS/ID", dataParam.SavingsCodeTemplate);

            _generalSettingsManager.FillGeneralSettings();
            Assert.AreEqual("IC/BC/CS/ID", dataParam.SavingsCodeTemplate);
        }

        [Test]
        public void TestUpdateIMFCode()
        {
            ApplicationSettings dataParam = ApplicationSettings.GetInstance("");
            _generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.IMF_CODE, "AIRDIE");
            Assert.AreEqual("AIRDIE", dataParam.ImfCode);

            _generalSettingsManager.FillGeneralSettings();
            Assert.AreEqual("AIRDIE", dataParam.ImfCode);

            _generalSettingsManager.UpdateSelectedParameter(OGeneralSettings.IMF_CODE, "I01");
            Assert.AreEqual("I01", dataParam.ImfCode);

            _generalSettingsManager.FillGeneralSettings();
            Assert.AreEqual("I01", dataParam.ImfCode);
        }

        [Test]
        public void TestSelectAllParameters()
        {
            _generalSettingsManager.FillGeneralSettings();
            ApplicationSettings dataParam = ApplicationSettings.GetInstance("");

            Assert.AreEqual(43, dataParam.DbParamList.Count);
        }
        [Test]
        public void TestAddIntParameter()
        {
            _generalSettingsManager.DeleteSelectedParameter("INTPARAM");
            DictionaryEntry entry = new DictionaryEntry("INTPARAM", 2);
            _generalSettingsManager.AddParameter(entry);
            Assert.AreEqual(2, Convert.ToInt32(_generalSettingsManager.SelectParameterValue("INTPARAM")));
            _generalSettingsManager.DeleteSelectedParameter("INTPARAM");
        }
        [Test]
        public void TestAddStringParameter()
        {
            _generalSettingsManager.DeleteSelectedParameter("STRINGPARAM");
            DictionaryEntry entry = new DictionaryEntry("STRINGPARAM", "hello");
            _generalSettingsManager.AddParameter(entry);
            Assert.AreEqual("hello", _generalSettingsManager.SelectParameterValue("STRINGPARAM").ToString());
            _generalSettingsManager.DeleteSelectedParameter("STRINGPARAM");
        }
        [Test]
        public void TestAddDoubleParameter()
        {
            double val = 2;
            _generalSettingsManager.DeleteSelectedParameter("DOUBLEPARAM");
            DictionaryEntry entry = new DictionaryEntry("DOUBLEPARAM", val);
            _generalSettingsManager.AddParameter(entry);
            Assert.AreEqual(2, Convert.ToDouble(_generalSettingsManager.SelectParameterValue("DOUBLEPARAM")));
            _generalSettingsManager.DeleteSelectedParameter("DOUBLEPARAM");
        }
        [Test]
        public void TestAddBooleanParameter()
        {
            _generalSettingsManager.DeleteSelectedParameter("BOOLPARAM");
            DictionaryEntry entry = new DictionaryEntry("BOOLPARAM", true);
            _generalSettingsManager.AddParameter(entry);
            Assert.AreEqual(true, Convert.ToBoolean(_generalSettingsManager.SelectParameterValue("BOOLPARAM")));
            _generalSettingsManager.DeleteSelectedParameter("BOOLPARAM");
        }

        [Test]
        public void TestAddDateParameter()
        {
            _generalSettingsManager.DeleteSelectedParameter("DATEPARAM");
            DictionaryEntry entry = new DictionaryEntry("DATEPARAM", new DateTime(2006, 1, 1));
            _generalSettingsManager.AddParameter(entry);
            Assert.AreEqual(new DateTime(2006, 1, 1), Convert.ToDateTime(_generalSettingsManager.SelectParameterValue("DATEPARAM")));
            _generalSettingsManager.DeleteSelectedParameter("DATEPARAM");
        }

        [Test]
        public void TestAddNullParameterForExternalCurrency()
        {
            _generalSettingsManager.DeleteSelectedParameter("NULLPARAM");
            DictionaryEntry entry = new DictionaryEntry("NULLPARAM", null);
            _generalSettingsManager.AddParameter(entry);
            Assert.IsNull(_generalSettingsManager.SelectParameterValue("NULLPARAM"));
            _generalSettingsManager.DeleteSelectedParameter("NULLPARAM");
        }
    }
}
