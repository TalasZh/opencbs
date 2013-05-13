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
using System.IO;
using System.Text;

namespace OpenCBS.Shared.CSV
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
