// LICENSE PLACEHOLDER

using System;
using System.Data;
using System.IO;
using System.Linq;

namespace OpenCBS.Shared.CSV
{
    public class CsvImportExport
    {
        public void Import(string filePath, Action<string[]> readAction)
        {
            if(readAction == null) throw new ArgumentNullException("readAction");
            using(FileStream stream = File.OpenRead(filePath))
            {
                CsvFileReader reader = new CsvFileReader(stream);
                string[] records;
                while (reader.ReadRow(out records))
                    readAction(records);
            }
        }

        public void Export(string filePath, DataSet dataSet)
        {
            if(string.IsNullOrEmpty(filePath)) throw new ArgumentException("Empty filename for export");

            using(FileStream stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                CsvFileWriter writer = new CsvFileWriter(stream);
                DataTable table = dataSet.Tables[0];
                foreach (DataRow row in table.Rows)
                {
                    object[] rowItems = row.ItemArray;
                    string[] items = rowItems.Select(i => i.ToString())
                        .ToArray();
                    writer.WriteRow(items);
                }
            }
        }
    }
}
