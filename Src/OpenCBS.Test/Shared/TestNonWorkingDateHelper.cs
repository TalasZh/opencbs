// LICENSE PLACEHOLDER

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
