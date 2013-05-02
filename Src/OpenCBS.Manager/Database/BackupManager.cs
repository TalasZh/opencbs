//Octopus MFS is an integrated suite for managing a Micro Finance Institution: clients, contracts, accounting, reporting and risk
//Copyright � 2006,2007 OCTO Technology & OXUS Development Network
//
//This program is free software; you can redistribute it and/or modify
//it under the terms of the GNU Lesser General Public License as published by
//the Free Software Foundation; either version 2 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU Lesser General Public License for more details.
//
//You should have received a copy of the GNU Lesser General Public License along
//with this program; if not, write to the Free Software Foundation, Inc.,
//51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
//
// Licence : http://www.octopusnetwork.org/OverviewLicence.aspx
//
// Website : http://www.octopusnetwork.org
// Business contact: business(at)octopusnetwork.org
// Technical contact email : tech(at)octopusnetwork.org 

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using Ionic.Zip;

namespace OpenCBS.Manager.Database
{
    public static class BackupManager
    {
        private static class ExtendedRegistry
        {
            [DllImport("advapi32.dll", EntryPoint = "RegOpenKeyEx")]
            private static extern int RegOpenKeyEx_DllImport(UIntPtr hKey, string subkey, uint options, int sam, out IntPtr phkResult);

            [DllImport("advapi32.dll", EntryPoint = "RegQueryValueEx")]
            private static extern int RegQueryValueEx_DllImport(IntPtr hKey, string lpValueName, int lpReserved, out uint lpType, StringBuilder lpData, ref uint lpcbData);

            [DllImport("kernel32.dll")]
            private static extern uint FormatMessage(uint dwFlags, IntPtr lpSource, uint dwMessageId, uint dwLanguageId, ref IntPtr lpBuffer, uint nSize, IntPtr Arguments);

            public static string GetKeyValue(string subkey, string key)
            {
                UIntPtr HKEY_LOCAL_MACHINE = (UIntPtr) 0x80000002;
                const int KEY_WOW64_64KEY = 0x0100;
                const int KEY_QUERY_VALUE = 0x1;

                const uint FORMAT_MESSAGE_ALLOCATE_BUFFER = 0x00000100;
                const uint FORMAT_MESSAGE_IGNORE_INSERTS = 0x00000200;
                const uint FORMAT_MESSAGE_FROM_SYSTEM = 0x00001000;
                const uint FLAGS =
                    FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM | FORMAT_MESSAGE_IGNORE_INSERTS;

                IntPtr hKeyVal;
                uint lpType;
                uint lpcbData = 0;
                string retval = string.Empty;

                unchecked
                {
                    try
                    {
                        int ret;
                        ret = RegOpenKeyEx_DllImport(HKEY_LOCAL_MACHINE, subkey, 0, KEY_QUERY_VALUE | KEY_WOW64_64KEY,
                                               out hKeyVal);
                        if (ret > 0)
                        {
                            IntPtr lpMsgBuf = IntPtr.Zero;
                            FormatMessage(FLAGS, IntPtr.Zero, (uint) ret, 0, ref lpMsgBuf, 0, IntPtr.Zero);
                            string msg = Marshal.PtrToStringAnsi(lpMsgBuf);
                            System.Diagnostics.Debug.WriteLine(msg);
                        }
                        ret = RegQueryValueEx_DllImport(hKeyVal, key, 0, out lpType, null, ref lpcbData);
                        StringBuilder data = new StringBuilder((int) lpcbData);
                        ret = RegQueryValueEx_DllImport(hKeyVal, key, 0, out lpType, data, ref lpcbData);
                        retval = data.ToString();
                    }
                    catch (Exception error)
                    {
                        System.Diagnostics.Debug.WriteLine(error.Message);
                        throw;
                    }
                }
                return retval;
            }
        }

        private static bool DatabaseExists(string name, SqlConnection conn)
        {
            string q = @"SELECT CASE
            WHEN DB_ID('{0}') IS NULL THEN 0 ELSE 1 END";
            q = string.Format(q, name);

            OctopusCommand cmd = new OctopusCommand();
            cmd.CommandText = q;
            cmd.Connection = conn;
            return Convert.ToBoolean(cmd.ExecuteScalar());
        }

        public static void Backup(string file, string backupPath, string dbName, SqlConnection conn)
        {
            string rawFile = BackupToFile(file, dbName, conn);
            string zippedFile = Path.Combine(backupPath, file) + ".zip";
            using (ZipFile zip = new ZipFile())
            {
                zip.AlternateEncoding = Encoding.UTF8;
                zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
                zip.AddFile(rawFile, string.Empty);
                zip.Save(zippedFile);
            }
            try
            {
                File.Delete(rawFile);
            }
            catch
            {
            }
        }

        public static void RawBackup(string dbName, string backupFolder, SqlConnection conn)
        {
            string fileName = string.Format("{0}.bak", dbName);
            string rawFile = BackupToFile(fileName, dbName, conn);
            string destFile = Path.Combine(backupFolder, fileName);
            File.Copy(rawFile, destFile, true);
            try
            {
                File.Delete(rawFile);
            }
            catch
            {
            }
        }

        private static string BackupToFile(string fileName, string database, SqlConnection connection)
        {
            // To perform a backup we first need to get the appropriate backup folder, which is a bit tricky.
            // First, we need to get the service name.
            const string query = "SELECT @@SERVICENAME AS name";
            OctopusCommand cmd = new OctopusCommand(query, connection);
            string serviceName = string.Empty;
            using (OctopusReader reader = cmd.ExecuteReader())
            {
                if (reader.Empty) return null;
                reader.Read();
                serviceName = reader.GetString("name");
            }
            
            // Then get the instance name from the registry
            const string sqlServerKey = @"SOFTWARE\Microsoft\Microsoft SQL Server";
            string key = string.Format(@"{0}\Instance Names\SQL", sqlServerKey);
            string instanceName = ExtendedRegistry.GetKeyValue(key, serviceName);
            if (string.IsNullOrEmpty(instanceName)) return null;
            
            // Finally, get the backup directory
            key = string.Format(@"{0}\{1}\MSSQLServer", sqlServerKey, instanceName);
            string backupDirectory = ExtendedRegistry.GetKeyValue(key, "BackupDirectory");
            if (string.IsNullOrEmpty(backupDirectory)) return null;

            string path = Path.Combine(backupDirectory, fileName);
            BackupToFileImpl(path, database, connection);
            return path;
        }


        private static Dictionary<string, char> GetFilelist(string file,SqlConnection connection)
        {
            //list of files of a backup.
            Dictionary<string, char> databaseFiles = new Dictionary<string, char>();

            string q = @"RESTORE FILELISTONLY FROM DISK = '{0}'";
            q = string.Format(q, file);
            OctopusCommand cmd = new OctopusCommand(q, connection);
            using (OctopusReader reader = cmd.ExecuteReader())
            {
                if (null == reader || reader.Empty) return null;
                while (reader.Read())
                {
                    string logicalName = reader.GetString("LogicalName");
                    char type = reader.GetString("Type").ToCharArray()[0];
                    databaseFiles.Add(logicalName, type);
                }
            }
            return databaseFiles;
        }

        public static void Restore(string file, string dbName, string dataDir, SqlConnection conn)
        {
            string tempFile = Unzip(file, GetTempBackupPath());

            RawRestore(tempFile, dbName, dataDir, conn);

            File.Delete(tempFile);
        }

        public static void RawRestore(string filePath, string dbName, string dataDir, SqlConnection conn)
        {
            SwitchOnSigleMode(dbName, conn);

            string query =
                @"DECLARE 
            @DestDataBaseName varchar(255),
            @BackupFile varchar(255)

            SET @DestDataBaseName = '{0}'
            SET @BackupFile = '{1}'

            RESTORE DATABASE @DestDataBaseName FROM DISK = @BackupFile
            WITH REPLACE";

            query = string.Format(query, dbName, filePath);

            Dictionary<string, char> logicalFiles = GetFilelist(filePath, conn);

            foreach (KeyValuePair<string, char> logicalFile in logicalFiles)
            {
                const string moveString = ", MOVE '{0}' TO '{1}'";
                switch (logicalFile.Value)
                {
                    case 'D':
                        query += string.Format(
                            moveString
                            , logicalFile.Key
                            , Path.Combine(dataDir, dbName + ".mdf")
                            );
                        break;
                    case 'L':
                        query += string.Format(
                            moveString
                            , logicalFile.Key
                            , Path.Combine(dataDir, dbName + ".ldf")
                            );
                        break;
                }
            }

            using (OctopusCommand command = new OctopusCommand(query, conn))
            {
                command.CommandTimeout = 300;
                command.ExecuteNonQuery();
            }
            
            SwitchOffSigleMode(dbName, conn);
        }

        private static void SwitchOnSigleMode(string pDatabaseName, SqlConnection pSqlConnection)
        {
            string sqlText = string.Format(@"ALTER DATABASE {0} SET SINGLE_USER WITH ROLLBACK IMMEDIATE", pDatabaseName);

            using (OctopusCommand cmd = new OctopusCommand(sqlText, pSqlConnection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        private static void SwitchOffSigleMode(string pDatabaseName, SqlConnection pSqlConnection)
        {
            string sqlText = string.Format(@"ALTER DATABASE {0} SET MULTI_USER", pDatabaseName);

            using (OctopusCommand cmd = new OctopusCommand(sqlText, pSqlConnection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        private static string Unzip(string file, string to)
        {
            FileInfo fi = new FileInfo(file);

            if (".zip" == fi.Extension.ToLower())
            {
                using (ZipFile zip = new ZipFile(file))
                {
                    zip[0].Extract(to, ExtractExistingFileAction.OverwriteSilently);
                    return Path.Combine(to, zip[0].FileName);
                }
            }

            to = Path.Combine(to, fi.Name);
            File.Copy(file, to, true);
            return to;
        }

        private static void BackupToFileImpl(string pTempSavingPath, string pDatabaseName, SqlConnection pSqlConnection)
        {
            
            string sqlRecoveryMode1 = String.Format("ALTER DATABASE {0} SET RECOVERY FULL", pDatabaseName);

            string sqlAutoShrink2 = String.Format("ALTER DATABASE {0} SET AUTO_SHRINK ON", pDatabaseName);

            string sqlBackupFile3 = String.Format(@"
					DECLARE
					@DataBaseName varchar(255),
					@BackupFile varchar(255)
	
					SET @DataBaseName = '{0}'
					SET @BackupFile = N'{1}'

					BACKUP DATABASE @DataBaseName TO DISK= @BackupFile
					WITH FORMAT", pDatabaseName, pTempSavingPath);

            string sqlTruncLogFile4 = String.Format(@"
                    DECLARE 
                    @DestDataBaseName varchar(255)
                    SET @DestDataBaseName = '{0}'
                    BACKUP LOG @DestDataBaseName WITH TRUNCATE_ONLY", pDatabaseName);

            string sqlTruncLogFile5 = String.Format(@"
                    DECLARE 
                    @DestDataBaseName varchar(255),
                    @LogName varchar(255)
                    SET @DestDataBaseName = '{0}'
                    SET @LogName = (SELECT name from sysfiles where groupid = 0)
                    DBCC SHRINKFILE(@Logname)", pDatabaseName);

            const string sqlSqlServerVersion 
                = @"SELECT CONVERT(INTEGER, CONVERT(FLOAT, CONVERT(VARCHAR(3), SERVERPROPERTY('productversion'))))";
            
            // Ensure recovery mode is FULL
            var cmd = new OctopusCommand(sqlRecoveryMode1, pSqlConnection);
            cmd.CommandTimeout = 300;
            cmd.ExecuteNonQuery();

             //Ensure auto shrink is on
            cmd = new OctopusCommand(sqlAutoShrink2, pSqlConnection);
            cmd.CommandTimeout = 300;
            cmd.ExecuteNonQuery();

            // Backup data int file
            cmd = new OctopusCommand(sqlBackupFile3, pSqlConnection);
            cmd.CommandTimeout = 300;
            cmd.ExecuteNonQuery();

            // Trunc transaction log
            cmd = new OctopusCommand(sqlSqlServerVersion, pSqlConnection);
            cmd.CommandTimeout = 300;
            var sqlVersion = (int)cmd.ExecuteScalar();

            if (sqlVersion < 10) // If SQL Server is 2000 or 2005
            {
                cmd = new OctopusCommand(sqlTruncLogFile4, pSqlConnection);
                cmd.CommandTimeout = 300;
                cmd.ExecuteNonQuery();
            }
            else // If SQL Server is 2008 or higher
            {
                cmd = new OctopusCommand(sqlTruncLogFile5, pSqlConnection);
                cmd.CommandTimeout = 300;
                cmd.ExecuteNonQuery();
            }
            
        }

        private static string GetTempBackupPath()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Temp");
        }
    }
}