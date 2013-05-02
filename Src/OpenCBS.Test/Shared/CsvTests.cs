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
