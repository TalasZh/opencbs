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
using OpenCBS.Shared;
using System.Collections;

namespace OpenCBS.Test.Shared
{
	[TestFixture]
	public class TestNonWorkingDateHelper
	{
		
		private NonWorkingDateSingleton nonWorkingDateHelper = NonWorkingDateSingleton.GetInstance(""); 

		[Test]
		public void TestGetWeekEndDay1()
		{			
			nonWorkingDateHelper.WeekEndDay1 = 6;
			Assert.AreEqual(6,nonWorkingDateHelper.WeekEndDay1);
		}

		[Test]
		public void TestGetWeekEndDay2()
		{
			nonWorkingDateHelper.WeekEndDay2 = 7;
			Assert.AreEqual(7,nonWorkingDateHelper.WeekEndDay2);	
		}

		[Test]
		public void TestPublicHolidays()
		{
            nonWorkingDateHelper.PublicHolidays = new Dictionary<DateTime, string>();
			nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006,5,1),"Labouring Solidarity Day");
			nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006,5,9),"Victory Day");
			nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006,6,27),"National Unitary Day");
			Assert.AreEqual(3,nonWorkingDateHelper.PublicHolidays.Count);
			Assert.AreEqual("Victory Day",nonWorkingDateHelper.PublicHolidays[new DateTime(2006,5,9)]);
		}
	
	}
}
