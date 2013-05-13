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
