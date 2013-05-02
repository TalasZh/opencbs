// LICENSE PLACEHOLDER

using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using NUnit.Framework;
using OpenCBS.Enums;
using OpenCBS.Shared;
using OpenCBS.Manager;
using System.Collections;
using OpenCBS.Shared.Settings;

namespace OpenCBS.Test.Manager
{
    [TestFixture]
    public class TestNonWorkingDateManagement
    {
        private NonWorkingDateSingleton nonWorkingDateHelper;
        private NonWorkingDateManagement nonWorkingDateManager;

      

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            TechnicalSettings.UseOnlineMode = false;
            nonWorkingDateManager = new NonWorkingDateManagement(DataUtil.TESTDB);

            nonWorkingDateManager.DeleteAllPublicHolidays();
            
            nonWorkingDateManager.AddPublicHoliday(new DictionaryEntry(new DateTime(2006, 1, 1), "New Year Eve"));
            nonWorkingDateManager.AddPublicHoliday(new DictionaryEntry(new DateTime(2006, 3, 21), "Idi Navruz"));
            nonWorkingDateManager.AddPublicHoliday(new DictionaryEntry(new DateTime(2006, 11, 6), "Idi Ramazan"));
            
            ApplicationSettings data = ApplicationSettings.GetInstance("");
            data.DeleteAllParameters();
            data.AddParameter(OGeneralSettings.WEEKENDDAY1, 6);
            data.AddParameter(OGeneralSettings.WEEKENDDAY2, 0);
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            nonWorkingDateManager.DeleteAllPublicHolidays();
        }

        [Test]
        public void TestIfWeekEndDaysAreCorrectlyFilled()
        {
            nonWorkingDateHelper = nonWorkingDateManager.FillNonWorkingDateHelper();
            Assert.AreEqual(6, nonWorkingDateHelper.WeekEndDay1);
            Assert.AreEqual(0, nonWorkingDateHelper.WeekEndDay2);
        }

        [Test]
        public void TestIfPublicHolidaysAreCorrectlyFilled()
        {
            nonWorkingDateHelper = nonWorkingDateManager.FillNonWorkingDateHelper();
            Assert.AreEqual(3, nonWorkingDateHelper.PublicHolidays.Count);
            Assert.AreEqual("Idi Navruz", nonWorkingDateHelper.PublicHolidays[new DateTime(2006, 3, 21)]);
            nonWorkingDateHelper = nonWorkingDateManager.FillNonWorkingDateHelper();
            Assert.AreEqual(3, nonWorkingDateHelper.PublicHolidays.Count);
        }
    }
}
