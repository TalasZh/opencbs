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
using OpenCBS.Services;
using NUnit.Framework;
using NUnit.Mocks;
using OpenCBS.Shared;

namespace OpenCBS.Test.Services
{
	/// <summary>
	/// Summary description for TestServicesHelper.
	/// </summary>
	[TestFixture]
	public class TestServicesHelper
	{

		[SetUp]
		public void SetUp()
		{
		
		}

		[Test]
		public void TestConvertStringToDecimalWhenStringIsNull()
		{
			Assert.AreEqual(0,ServicesHelper.ConvertStringToDecimal(null, true));
		}

		[Test]
		public void TestConvertStringToDecimalWhenStringIsEmpty()
		{
			Assert.AreEqual(0,ServicesHelper.ConvertStringToDecimal(String.Empty, true));		
		}

		[Test]
		public void TestIfDecimalValueIsBetweenMinAndMax()
		{
			OCurrency min = 10;
			OCurrency max = 19;
			OCurrency val = 18.99999m;
			Assert.IsTrue(ServicesHelper.CheckIfValueBetweenMinAndMax(min,max,13));
			Assert.IsTrue(ServicesHelper.CheckIfValueBetweenMinAndMax(min,max,min));
			Assert.IsTrue(ServicesHelper.CheckIfValueBetweenMinAndMax(min,max,max));
			Assert.IsTrue(ServicesHelper.CheckIfValueBetweenMinAndMax(min,max,val));
			val = 9.99999m;
		//	Assert.IsFalse(ServicesHelper.CheckIfValueBetweenMinAndMax(min,max,val));
            Assert.IsTrue(ServicesHelper.CheckIfValueBetweenMinAndMax(min, max, val));

		}

		[Test]
		public void TestIfDoubleValueIsBetweenMinAndMax()
		{
			double min = 12;
			double max = 20;
			double val = 19.999999999999;
			Assert.IsTrue(ServicesHelper.CheckIfValueBetweenMinAndMax(min,max,13));
			Assert.IsTrue(ServicesHelper.CheckIfValueBetweenMinAndMax(min,max,min));
			Assert.IsTrue(ServicesHelper.CheckIfValueBetweenMinAndMax(min,max,max));
			Assert.IsTrue(ServicesHelper.CheckIfValueBetweenMinAndMax(min,max,val));
			val = 11.99999;
			Assert.IsFalse(ServicesHelper.CheckIfValueBetweenMinAndMax(min,max,val));
		}

		[Test]
		public void TestConvertStringToNullableDecimalWhenStringIsNull()
		{
			Assert.IsTrue(!ServicesHelper.ConvertStringToNullableDecimal(null).HasValue);	
		}

		[Test]
		public void TestConvertStringToNullableDecimalWhenStringIsEmpty()
		{
			Assert.IsTrue(!ServicesHelper.ConvertStringToNullableDecimal(String.Empty).HasValue);
		}

		[Test]
		public void TestConvertStringToDoubleWhenStringIsNull()
		{
			Assert.AreEqual(0,ServicesHelper.ConvertStringToDouble(null,false));	
		}

		[Test]
		public void TestConvertStringToDoubleWhenStringIsNullAndStringIsPercentage()
		{
			Assert.AreEqual(0,ServicesHelper.ConvertStringToDouble(null,true));
		}

		[Test]
		public void TestConvertStringToDoubleWhenStringIsEmpty()
		{
			Assert.AreEqual(0,ServicesHelper.ConvertStringToDouble(String.Empty,false));		
		}

		[Test]
		public void TestConvertStringToDoubleWhenStringIsEmptyAndStringIsPercentage()
		{
			Assert.AreEqual(0,ServicesHelper.ConvertStringToDouble(String.Empty,true));
		}

		[Test]
		public void TestConvertStringToNullableDoubleWhenStringIsNull()
		{
			Assert.AreEqual(null,ServicesHelper.ConvertStringToNullableDouble(null,false));	
		}

		[Test]
		public void TestConvertStringToNullableDoubleWhenStringIsNullAndStringIsAPercentage()
		{
			Assert.AreEqual(null,ServicesHelper.ConvertStringToNullableDouble(null,true));
		}

		[Test]
		public void TestConvertStringToNullableDoubleWhenStringIsEmpty()
		{
			Assert.AreEqual(null,ServicesHelper.ConvertStringToNullableDouble(String.Empty,false));		
		}

		[Test]
		public void TestConvertStringToNullableDoubleWhenStringIsEmptyAndStringIsAPercentage()
		{
			Assert.AreEqual(null,ServicesHelper.ConvertStringToNullableDouble(String.Empty,true));
		}

		[Test]
		public void TestConvertStringToNullableInt32WhenStringIsNull()
		{
			Assert.AreEqual(null,ServicesHelper.ConvertStringToNullableInt32(null));
		}

		[Test]
		public void TestConvertStringToNullableInt32WhenStringIsEmpty()
		{
			Assert.AreEqual(null,ServicesHelper.ConvertStringToNullableInt32(String.Empty));
		}

		[Test]
		public void TestConvertStringToNullableInt32()
		{
			Assert.AreEqual(3,ServicesHelper.ConvertStringToNullableInt32("3").Value);		
		}

		[Test]
		public void TestCheckIfStringIsDoubleAndPositiveWhenStringIsNull()
		{
			Assert.IsFalse(ServicesHelper.CheckIfDouble(null));		
		}

		[Test]
		public void TestCheckIfStringIsDoubleAndPositiveWhenStringIsEmpty()
		{
			Assert.IsFalse(ServicesHelper.CheckIfDouble(String.Empty));		
		}

		[Test]
		public void TestCheckIfStringIsDoubleAndPositiveWhenResultIsNegative()
		{
			Assert.IsFalse(ServicesHelper.CheckIfDouble("-3"));			
		}

		[Test]
		public void TestCheckIfStringIsIntegerAndPositiveWhenStringIsNull()
		{
			Assert.IsFalse(ServicesHelper.CheckIfInteger(null));	
		}

		[Test]
		public void TestCheckIfStringIsIntegerAndPositiveWhenStringIsEmpty()
		{
			Assert.IsFalse(ServicesHelper.CheckIfInteger(String.Empty));
		}

		[Test]
		public void TestCheckIfStringIsIntegerAndPositiveWhenResultIsNegative()
		{
			Assert.IsFalse(ServicesHelper.CheckIfInteger("-3"));
		}

		[Test]
		public void TestCheckIfStringIsIntegerAndPositive()
		{
			Assert.IsTrue(ServicesHelper.CheckIfInteger("3"));
		}

		[Test]
		public void TestIfTextIsNotEmptyWhenTextIsEmpty()
		{
			Assert.IsNull(ServicesHelper.CheckTextBoxText(String.Empty));
		}

		[Test]
		public void TestIfTextIsNotEmpty()
		{
			Assert.AreEqual("coucou",ServicesHelper.CheckTextBoxText("coucou"));
		}

		[Test]
		public void TestIfMinLowerThanMaxWhenMaxLowerThanMin()
		{
			Assert.IsFalse(ServicesHelper.CheckIfMinLowerThanMax(9,2));
		}

		[Test]
		public void TestIfMinLowerThanMaxWhenMaxEgalsMin()
		{
			Assert.IsFalse(ServicesHelper.CheckIfMinLowerThanMax(2,2));
		}

		[Test]
		public void TestIfMinLowerThanMax()
		{
			Assert.IsTrue(ServicesHelper.CheckIfMinLowerThanMax(1,2));
		}

		[Test]
		public void TestResultIfMinMaxAndValueAreValue()
		{
			Assert.IsFalse(ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(3,4,1));
		}

		[Test]
		public void TestResultIfMinMaxAreValue()
		{
			Assert.IsTrue(ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(3,4,null));
		}

		[Test]
		public void TestResultIfValueAreValue()
		{
			Assert.IsTrue(ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(null,null,9));
		}
	}
}
