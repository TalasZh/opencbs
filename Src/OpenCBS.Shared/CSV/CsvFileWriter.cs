using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Octopus.Shared.CSV
{
    public class CsvFileWriter : StreamWriter
    {
        public CsvFileWriter(Stream stream) : base(stream, Encoding.UTF8)
        {            
        }

        public CsvFileWriter(string fileName, bool append) : base(fileName, append, Encoding.UTF8)
        {            
        }

        public void WriteRow(IEnumerable<string> row)
        {
            if(row == null) throw new ArgumentNullException("row");

            StringBuilder builder = new StringBuilder();
            bool firstColumn = true;
            foreach (string value in row)
            {
                if (!firstColumn)
                    builder.Append(',');

                if (value.IndexOfAny(EscapedCharacters) != -1)
                    builder.AppendFormat("\"{0}\"", value.Replace(Quote, EscapedQuote));
                else
                    builder.Append(value);
                firstColumn = false;
            }
            WriteLine(builder.ToString());
            Flush();
        }

        private const string Quote = "\"";
        private const string EscapedQuote = "\"\"";
        private static readonly char[] EscapedCharacters = { ',', '"', '\n' };
    }
}
