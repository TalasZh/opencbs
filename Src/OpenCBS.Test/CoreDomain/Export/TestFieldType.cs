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
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenCBS.CoreDomain.Export;
using OpenCBS.CoreDomain.Export.FieldType;
using OpenCBS.CoreDomain.Export.Files;

namespace OpenCBS.Test.CoreDomain.Export
{
    [TestFixture]
    public class TestFieldType
    {
        [Test]
        public void TestStringFieldType()
        {
            StringFieldType stringFieldType = new StringFieldType();        
            var s = "Hello World!";

            Assert.AreEqual("Hello World!   ", stringFieldType.Format(s, 15));

            stringFieldType.AlignRight = true;
            Assert.AreEqual("   Hello World!", stringFieldType.Format(s, 15));

            Assert.AreEqual("Hello", stringFieldType.Format(s, 5));

            stringFieldType.ReplacementList.Add("Hello", "Hi");
            stringFieldType.ReplacementList.Add("World", "Everybody");

            Assert.AreEqual("Hi Everybody!", stringFieldType.Format(s, null));

            var iban = "FR1420041010050500013M02606";
            stringFieldType.AlignRight = false;
            stringFieldType.StartPosition = 4;

            Assert.AreEqual("20041010050500013M02606", stringFieldType.Format(iban, null));

            stringFieldType.EndPosition = 9;
            Assert.AreEqual("20041", stringFieldType.Format(iban, null));
            Assert.AreEqual("20041  ", stringFieldType.Format(iban, 7));
            
            stringFieldType.StartPosition = 9;
            stringFieldType.EndPosition = 14;
            Assert.AreEqual("01005", stringFieldType.Format(iban, null));

            stringFieldType.StartPosition = 14;
            stringFieldType.EndPosition = 25;
            Assert.AreEqual("0500013M026", stringFieldType.Format(iban, null));

            stringFieldType.StartPosition = 25;
            stringFieldType.EndPosition = 27;
            Assert.AreEqual("06", stringFieldType.Format(iban, 2));
        }

        [Test]
        public void TestDecimalFieldType()
        {
            DecimalFieldType decimalFieldType = new DecimalFieldType()
            {
                DecimalNumber = 3,
                DecimalSeparator = ",",
                GroupSeparator = "."
            };

            decimal d = 1250.45M;

            Assert.AreEqual("1.250,450 ", decimalFieldType.Format(d, 10));
            
            decimalFieldType.AlignRight = true;
            Assert.AreEqual(" 1.250,450", decimalFieldType.Format(d, 10));
            Assert.AreEqual(1250.45M, decimalFieldType.Parse("1.250,450"));

            decimalFieldType.DecimalNumber = 2;
            decimalFieldType.DecimalSeparator = ".";
            decimalFieldType.GroupSeparator = "";
            Assert.AreEqual("   1250.45", decimalFieldType.Format(d, 10));
            Assert.AreEqual(1250.45M, decimalFieldType.Parse("1250.45"));

        }

        [Test]
        public void TestIntegerFieldType()
        {
            IntegerFieldType integerFieldType = new IntegerFieldType();

            int i = 4560;

            Assert.AreEqual("4560", integerFieldType.Format(i, 4));
            Assert.AreEqual(4560, integerFieldType.Parse("4560"));

            integerFieldType.DisplayZeroBefore = true;
            Assert.AreEqual("4560", integerFieldType.Format(i, 4));
            Assert.AreEqual("0000004560", integerFieldType.Format(i, 10));
            Assert.AreEqual(4560, integerFieldType.Parse("0000004560"));

            integerFieldType.DisplayZeroBefore = false;
            integerFieldType.AlignRight = true;
            Assert.AreEqual("      4560", integerFieldType.Format(i, 10));
            Assert.AreEqual(4560, integerFieldType.Parse("      4560"));
        }

        [Test]
        public void TestDateFieldType()
        {
            DateFieldType dateFieldType = new DateFieldType();
            DateTime date = new DateTime(2010, 06, 24);

            Assert.AreEqual("240610", dateFieldType.Format(date, null));
            Assert.AreEqual(new DateTime(2010, 06, 24), dateFieldType.Parse("240610"));

            dateFieldType.StringFormat = "MMddyyyy";
            Assert.AreEqual("06242010", dateFieldType.Format(date, null));
            Assert.AreEqual(new DateTime(2010, 06, 24), dateFieldType.Parse("06242010"));
        }

        [Test]
        public void TestSplitLine()
        {
            string line = "\"C/CR/ÉR/09/MA/CIS/2/1/203  \" 13 85,33 150610";

            List<string> splitedLine = AImportFile<Installment>.SplitLine(' ', '"', line);
            Assert.AreEqual("\"C/CR/ÉR/09/MA/CIS/2/1/203  \"", splitedLine[0]);
            Assert.AreEqual("13", splitedLine[1]);
            Assert.AreEqual("85,33", splitedLine[2]);
            Assert.AreEqual("150610", splitedLine[3]);

            line = "\"C/CR/ÉR/10/MA/CIA/2/1/947\" 5 127,04 270610";
            splitedLine = AImportFile<Installment>.SplitLine(' ', '"', line);
            Assert.AreEqual("\"C/CR/ÉR/10/MA/CIA/2/1/947\"", splitedLine[0]);
            Assert.AreEqual("5", splitedLine[1]);
            Assert.AreEqual("127,04", splitedLine[2]);
            Assert.AreEqual("270610", splitedLine[3]);

            line = "\"C/CR/ÉR/10/MA/CIA/2/1/947\",5,\"127,04\",270610";
            splitedLine = AImportFile<Installment>.SplitLine(',', '"', line);
            Assert.AreEqual("\"C/CR/ÉR/10/MA/CIA/2/1/947\"", splitedLine[0]);
            Assert.AreEqual("5", splitedLine[1]);
            Assert.AreEqual("\"127,04\"", splitedLine[2]);
            Assert.AreEqual("270610", splitedLine[3]);
        }
    }
}
