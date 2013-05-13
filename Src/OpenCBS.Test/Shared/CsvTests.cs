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

using System.IO;
using NUnit.Framework;
using OpenCBS.Shared.CSV;

namespace OpenCBS.Test.Shared
{
    [TestFixture]
    public class CsvTests
    {
        [Test]
        public void SimpleCommaWrite()
        {
            MemoryStream stream = new MemoryStream();
            CsvFileWriter writer = new CsvFileWriter(stream);
            writer.WriteRow(new [] {"one", "two", "three"});
            StreamReader reader = new StreamReader(stream);
            stream.Position = 0;
            Assert.AreEqual("one,two,three", reader.ReadLine());
        }

        [Test]
        public void SpecialCharInValue()
        {
            MemoryStream stream = new MemoryStream();
            CsvFileWriter writer = new CsvFileWriter(stream);
            writer.WriteRow(new[] { "one,two", "t\"h\"\"ree" });
            StreamReader reader = new StreamReader(stream);
            stream.Position = 0;
            Assert.AreEqual("\"one,two\",\"t\"\"h\"\"\"\"ree\"", reader.ReadLine());
        }

        [Test]
        public void ReadRow()
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            const string row = "one,\"t,wo\",\"th\"\"ree\"";
            writer.WriteLine(row);
            writer.Flush();
            stream.Position = 0;

            CsvFileReader reader = new CsvFileReader(stream);
            string[] records;
            reader.ReadRow(out records);
            CollectionAssert.AreEqual(new [] {"one", "t,wo", "th\"ree"}, records);
        }
    }
}
