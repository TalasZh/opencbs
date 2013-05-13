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
using System.Collections.Generic;
using NUnit.Framework;
using OpenCBS.CoreDomain;
using OpenCBS.Shared;
using System.Collections;
using OpenCBS.Services;

namespace OpenCBS.Test.Services
{
    [TestFixture]
    public class TestConsolidationServices
    {
        [Test]
        public void GetCurrentConsolidation_Monthly_ZeroPeriod_EmptyList()
        {
            //Acteurs
            int nbOfPeriod = 0;
            DateTime referencyDate = new DateTime(2009, 03, 27);

            //Actions
            List<DictionaryEntry> result = new ConsolidationServices(new User()).GetCurrentConsolidation('M', nbOfPeriod,
                referencyDate);
            //Assertions
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void GetCurrentConsolidation_Monthly_OnePeriod_OneRowList()
        {
            //Acteurs
            int nbOfPeriod = 1;
            DateTime referencyDate = new DateTime(2009, 03, 27);

            //Actions
            List<DictionaryEntry> result = new ConsolidationServices(new User()).GetCurrentConsolidation('M', nbOfPeriod,
                referencyDate);

            //Assertions
            Assert.AreEqual(1,result.Count);
        }

        [Test]
        public void GetCurrentConsolidation_Monthly_TwelvePeriods_TwelveRowsList()
        {
            //Acteurs
            int nbOfPeriod = 12;
            DateTime referencyDate = new DateTime(2009, 03, 27);

            //Actions
            List<DictionaryEntry> result = new ConsolidationServices(new User()).GetCurrentConsolidation('M', nbOfPeriod,
                referencyDate);

            //Assertions
            Assert.AreEqual(12, result.Count);
        }

        [Test]
        public void GetCurrentConsolidation_Monthly_OnePeriod_DisplayOneInPeriodColumn()
        {
            //Acteurs
            int nbOfPeriod = 1;
            DateTime referencyDate = new DateTime(2009, 03, 27);

            //Actions
            List<DictionaryEntry> result = new ConsolidationServices(new User()).GetCurrentConsolidation('M', nbOfPeriod,
                referencyDate);

            //Assertions
            Assert.AreEqual(1,result[0].Key);
        }

        [Test]
        public void GetCurrentConsolidation_Monthly_FivePeriod_SortPeriodNumberInPeriodColumn()
        {
            //Acteurs
            int nbOfPeriod = 5;
            DateTime referencyDate = new DateTime(2009, 03, 27);

            //Actions
            List<DictionaryEntry> result = new ConsolidationServices(new User()).GetCurrentConsolidation('M', nbOfPeriod,
                referencyDate);

            //Assertions
            Assert.AreEqual(5, result[0].Key);
            Assert.AreEqual(4, result[1].Key);
            Assert.AreEqual(3, result[2].Key);
            Assert.AreEqual(2, result[3].Key);
            Assert.AreEqual(1, result[4].Key);
        }

        [Test]
        public void GetCurrentConsolidation_Monthly_OnePeriod_DisplayDateInRangeDateColumn()
        {
            //Acteurs
            int nbOfPeriod = 1;
            DateTime referencyDate = new DateTime(2009, 03, 27);

            //Actions
            List<DictionaryEntry> result = new ConsolidationServices(new User()).GetCurrentConsolidation('M', nbOfPeriod,
                referencyDate);

            //Assertions
            Assert.AreEqual(string.Format("{0} - {1}", new DateTime(2009, 2, 27).ToShortDateString(),
                new DateTime(2009, 3, 27).ToShortDateString()), result[0].Value);
        }
        
        [Test]
        public void GetCurrentConsolidation_Weekly_OnePeriod_DisplayDateInRangeDateColumn()
        {
            //Acteurs
            int nbOfPeriod = 1;
            DateTime referencyDate = new DateTime(2009, 03, 27);

            //Actions
            List<DictionaryEntry> result = new ConsolidationServices(new User()).GetCurrentConsolidation('W', nbOfPeriod,
                referencyDate);

            //Assertions
            Assert.AreEqual(string.Format("{0} - {1}", new DateTime(2009, 3, 20).ToShortDateString(),
                new DateTime(2009, 3, 27).ToShortDateString()), result[0].Value);
        }
        
        [Test]
        public void GetCurrentConsolidation_Daily_OnePeriod_DisplayDateInRangeDateColumn()
        {
            //Acteurs
            int nbOfPeriod = 1;
            DateTime referencyDate = new DateTime(2009, 03, 27);

            //Actions
            List<DictionaryEntry> result = new ConsolidationServices(new User()).GetCurrentConsolidation('D', nbOfPeriod,
                referencyDate);

            //Assertions
            Assert.AreEqual(string.Format("{0} - {1}", new DateTime(2009, 3, 26).ToShortDateString(),
                new DateTime(2009, 3, 27).ToShortDateString()), result[0].Value);
        }

        [Test]
        public void GetCurrentConsolidation_Monthly_TwoPeriod_DisplayDatesInRangeDateColumnSortedByPeriodNumber()
        {
            //Acteurs
            int nbOfPeriod = 2;
            DateTime referencyDate = new DateTime(2009, 03, 27);

            //Actions
            List<DictionaryEntry> result = new ConsolidationServices(new User()).GetCurrentConsolidation('M', nbOfPeriod,
                referencyDate);

            //Assertions
            Assert.AreEqual(2, result[0].Key);
            Assert.AreEqual(1, result[1].Key);
            Assert.AreEqual(string.Format("{0} - {1}", new DateTime(2009, 2, 27).ToShortDateString(),
                new DateTime(2009, 3, 27).ToShortDateString()), result[0].Value);
            Assert.AreEqual(string.Format("{0} - {1}", new DateTime(2009, 1, 27).ToShortDateString(),
                new DateTime(2009, 2, 27).ToShortDateString()), result[1].Value);
        }

        [Test]
        public void GetCurrentConsolidation_Weekly_FivePeriod_DisplayDatesInRangeDateColumnSortedByPeriodNumber()
        {
            //Acteurs
            int nbOfPeriod = 5;
            DateTime referencyDate = new DateTime(2009,03,27);

            //Actions
            List<DictionaryEntry> result = new ConsolidationServices(new User()).GetCurrentConsolidation('W', nbOfPeriod,
                referencyDate);

            //Assertions
            Assert.AreEqual(5, result[0].Key);
            Assert.AreEqual(string.Format("{0} - {1}", new DateTime(2009, 3, 20).ToShortDateString(),
                new DateTime(2009, 3, 27).ToShortDateString()), result[0].Value);
            Assert.AreEqual(4, result[1].Key);
            Assert.AreEqual(string.Format("{0} - {1}", new DateTime(2009, 3, 13).ToShortDateString(),
                new DateTime(2009, 3, 20).ToShortDateString()), result[1].Value);
            Assert.AreEqual(3, result[2].Key);
            Assert.AreEqual(string.Format("{0} - {1}", new DateTime(2009, 3, 6).ToShortDateString(),
                new DateTime(2009, 3, 13).ToShortDateString()), result[2].Value);
            Assert.AreEqual(2, result[3].Key);
            Assert.AreEqual(string.Format("{0} - {1}", new DateTime(2009, 2, 27).ToShortDateString(),
                new DateTime(2009, 3, 6).ToShortDateString()), result[3].Value);
            Assert.AreEqual(1, result[4].Key);
            Assert.AreEqual(string.Format("{0} - {1}", new DateTime(2009, 2, 20).ToShortDateString(),
                new DateTime(2009, 2, 27).ToShortDateString()), result[4].Value);
        }

        [Test]
        public void GetCurrentConsolidation_Daily_TwoPeriod_DisplayDatesInRangeDateColumnSortedByPeriodNumber()
        {
            //Acteurs
            int nbOfPeriod = 2;
            DateTime referencyDate = new DateTime(2009, 03, 27);

            //Actions
            List<DictionaryEntry> result = new ConsolidationServices(new User()).GetCurrentConsolidation('D', nbOfPeriod,
                referencyDate);

            //Assertions
            Assert.AreEqual(2, result[0].Key);
            Assert.AreEqual(1, result[1].Key);
            Assert.AreEqual(string.Format("{0} - {1}", new DateTime(2009, 3, 26).ToShortDateString(),
                new DateTime(2009, 3, 27).ToShortDateString()), result[0].Value);
            Assert.AreEqual(string.Format("{0} - {1}", new DateTime(2009, 3, 25).ToShortDateString(),
                new DateTime(2009, 3, 26).ToShortDateString()), result[1].Value);
        }
    }
}
