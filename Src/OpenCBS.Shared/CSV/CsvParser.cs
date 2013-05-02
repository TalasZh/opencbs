// LICENSE PLACEHOLDER

using System.Collections.Generic;

namespace OpenCBS.Shared.CSV
{
    public static class CsvParser
    {
        public static string[] Parse(string line)
        {
            if (string.IsNullOrEmpty(line))
                return EmptyRecords;

            int pos = 0;

            List<string> results = new List<string>();
            while (pos < line.Length)
            {
                string value;

                // Special handling for quoted field
                if (line[pos] == '"')
                {
                    // Skip initial quote
                    pos++;

                    // Parse quoted value
                    int start = pos;
                    while (pos < line.Length)
                    {
                        // Test for quote character
                        if (line[pos] == '"')
                        {
                            // Found one
                            pos++;

                            // If two quotes together, keep one
                            // Otherwise, indicates end of value
                            if (pos >= line.Length || line[pos] != '"')
                            {
                                pos--;
                                break;
                            }
                        }
                        pos++;
                    }
                    value = line.Substring(start, pos - start);
                    value = value.Replace("\"\"", "\"");
                }
                else
                {
                    // Parse unquoted value
                    int start = pos;
                    while (pos < line.Length && line[pos] != ',')
                        pos++;
                    value = line.Substring(start, pos - start);
                }

                results.Add(value);

                // Eat up to and including next comma
                while (pos < line.Length && line[pos] != ',')
                    pos++;
                if (pos < line.Length)
                    pos++;
            }

            // Return true if any columns read
            return  results.Count == 0 ? EmptyRecords : results.ToArray();
        }

        public static readonly string[] EmptyRecords = new string[0];
    }
}
