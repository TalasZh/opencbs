// LICENSE PLACEHOLDER

using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OpenCBS.Shared.CSV
{
    public class CsvFileReader : StreamReader
    {
        public CsvFileReader(Stream stream) : base(stream, Encoding.UTF8)
        {
        }

        public CsvFileReader(string filename) : base(filename, Encoding.UTF8)
        {
        }        

        public bool ReadRow(out string[] records)
        {
            string line = ReadLine();
            records = CsvParser.Parse(line);
            return records.Length > 0;
        }
    }
}
