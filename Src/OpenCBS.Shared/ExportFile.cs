// LICENSE PLACEHOLDER

using System;
using System.Data;
using System.Windows.Forms;
using OpenCBS.Shared.CSV;

namespace OpenCBS.Shared
{
	/// <summary>
	/// Description r�sum�e de ExportFile.
    /// </summary>
    [Serializable]
	public static class ExportFile
	{
		public static void SaveToFile(DataSet dataset, string fileName, string separator)
		{
            if (string.IsNullOrEmpty(fileName))
                fileName = SaveTextToNewPath();

            if (fileName != null)
                SaveTextToPath(fileName, dataset);
		}

		public static string SaveTextToNewPath()
		{
			string path = null;
			SaveFileDialog dlg = new SaveFileDialog
			                         {
			                             DefaultExt = "csv",
			                             Filter =
			                                 "Plain Text (*.txt)|*.txt|Word Document (*.doc)|*.doc|CSV Document (*.csv)|*.csv|Excel Document (*.xls)|*.xls|All files (*.*)|*.*",
			                             FilterIndex = 1,
			                             InitialDirectory = Application.CommonAppDataPath
			                         };
		    if (dlg.ShowDialog() == DialogResult.OK)
			{
				path = dlg.FileName;
			}

			return path;
		}

        private static void SaveTextToPath(string path, DataSet dataSet)
		{
            CsvImportExport exporter = new CsvImportExport();
            exporter.Export(path, dataSet);
		}
	}
}
