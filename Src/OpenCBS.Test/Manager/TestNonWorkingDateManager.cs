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
