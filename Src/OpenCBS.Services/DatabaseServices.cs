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
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using Moletrator.SQLDocumentor;
using OpenCBS.CoreDomain.Database;
using OpenCBS.DatabaseConnection;
using OpenCBS.ExceptionsHandler;
using OpenCBS.Manager;
using OpenCBS.Manager.Database;
using OpenCBS.Shared;
using OpenCBS.Shared.Settings;
using OpenCBS.Shared.Settings.Remote;

namespace OpenCBS.Services
{
    public class DatabaseServices : BaseServices
    {
        private const string UPDATEDATABASE = "Database_Update_{0}_to_{1}.sql";
        private const string CREATEDATABASE = "CreateDatabase_{0}.sql";
        private const string INITIAL_DATAS = "InitialData.sql";
        private const string INITIAL_ACCOUNTING_RULES = "AccountingRules.sql";
        private const string UPGRADE_SCHEMA_FILE_NAME = "OCTOPUS_Upgrade_Schema.xml";


        public bool CheckSQLServerConnection()
        {
            return ConnectionManager.CheckSQLServerConnection();
        }

        public bool CheckSQLDatabaseConnection()
        {
            return ConnectionManager.CheckSQLDatabaseConnection();
        }

        public bool CheckSQLDatabaseVersion(string pExpectedVersion, string pDatabase)
        {
            SqlConnection connection = ConnectionManager.GeneralSqlConnection;
            try
            {
                connection.Open();
                string actualVersion = DatabaseManager.GetDatabaseVersion(pDatabase, connection);

                connection.Close();
                return pExpectedVersion == actualVersion ? true : false;
                           
            }
            catch(Exception)
            {
                connection.Close();
                throw;
            }
        }

        public string CheckSQLDatabaseSchema(string pDatabaseName)
        {
            SqlConnection connection = ConnectionManager.GeneralSqlConnection;
            try
            {
                connection.Open();

                bool same = false;
                string additionnalColumn = string.Empty, missingColumn = string.Empty;

                CompareDatabase.IsCurrentDatabaseIsRight(connection, pDatabaseName, ref same, ref additionnalColumn, ref missingColumn);
                connection.Close();

                return same ? string.Empty : "{0}: \n" + additionnalColumn + "{1}: \n" + missingColumn;
            }
            catch (Exception)
            {
                connection.Close();
                throw;
            }
        }

        public delegate void ExecuteUpgradeSqlDatabaseFile(string pCurrentDatabase, string pExpectedDatabase);
        public event ExecuteUpgradeSqlDatabaseFile UpdateDatabaseEvent;

        private static void _LoadUserDefinedFunctions(string database, SqlConnection conn)
        {
            string src = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            src = Path.Combine(src, "Update");
            src = Path.Combine(src, "DropAndCreateUDF.sql");
            string dest = Path.GetTempFileName();
            StreamReader sr = new StreamReader(src);
            string dllPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            dllPath = Path.Combine(dllPath, "OpenCBS.Stringifier.dll");
            string body = sr.ReadToEnd();
            sr.Close();
            body = string.Format(body, database, dllPath);
            StreamWriter sw = new StreamWriter(dest);
            sw.Write(body);
            sw.Close();
            DatabaseManager.ExecuteScript(dest, database, conn);
            File.Delete(dest);
        }

        public bool UpdateDatabase(string pExpectedVersion, string pDatabaseName, string pScriptPath)
        {
            SqlConnection connection = ConnectionManager.GeneralSqlConnection;
            try
            {
                connection.Open();
                string currentVersion = DatabaseManager.GetDatabaseVersion(pDatabaseName, connection);
                
                List<SqlUpdateDatabaseScript> scripts = _GetScriptsToUpgradeDatabase(pExpectedVersion, currentVersion);
                foreach (SqlUpdateDatabaseScript script in scripts)
                {
                    if (UpdateDatabaseEvent != null)
                        UpdateDatabaseEvent(script.Current, script.Expected);

                    string createSqlfile = Path.Combine(pScriptPath, string.Format(UPDATEDATABASE, script.Current, script.Expected));
                    DatabaseManager.ExecuteScript(createSqlfile, pDatabaseName, connection);
                }

                if (UpdateDatabaseEvent != null)
                    UpdateDatabaseEvent("", "");
                DatabaseManager.ExecuteScript(_GetSqlObjects(), pDatabaseName, connection);

                try
                {
                    _LoadUserDefinedFunctions(pDatabaseName, connection);
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Cannot load OpenCBS.Stringifier.dll: " + e.Message);
                }

                connection.Close();
                return true;
            }
            catch (Exception e)
            {
                connection.Close();
                MessageBox.Show("A database migration problem occured. This can be due to a bug or to a change made in your data model. Please try again using the option 'Create new database' and contact your support.",
                    "Upgrading error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Application.Exit();
                return false;//throw;
            }
        }

        public void DropDatabase(string dbName)
        {
            SqlConnection connection = ConnectionManager.GeneralSqlConnection;

            try
            {
                connection.Open();
                if (DatabaseManager.DatabaseExists(dbName, connection))
                    DatabaseManager.DeleteDatabase(dbName, connection);
            } finally
            {
                connection.Close();
            }            
        }

        private static bool CreateDatabaseImpl(string name, string version, string scriptPath, SqlConnection conn)
        {
            try
            {
                conn.Open();
                DatabaseManager.CreateDatabase(name, conn);

                string createSqlfile = Path.Combine(scriptPath, string.Format(CREATEDATABASE, version));
                DatabaseManager.ExecuteScript(createSqlfile, name, conn);
                DatabaseManager.ExecuteScript(Path.Combine(scriptPath, INITIAL_DATAS), name, conn);
                DatabaseManager.ExecuteScript(Path.Combine(scriptPath, INITIAL_ACCOUNTING_RULES), name, conn);
                DatabaseManager.ExecuteScript(_GetSqlObjects(), name, conn);

                try
                {
                    _LoadUserDefinedFunctions(name, conn);
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Cannot load OpenCBS.Stringifier.dll: " + e.Message);
                }

                return true;
            }
            finally
            {
                conn.Close();
            }
        }

        public bool CreateDatabase(string pDatabaseName, string pDatabaseVersion, string pScriptPath)
        {
            SqlConnection connection = ConnectionManager.GeneralSqlConnection;
            return CreateDatabaseImpl(pDatabaseName, pDatabaseVersion, pScriptPath, connection);
        }

        public string GetDatabaseDefaultPath()
        {
            return Run(
                connection =>
                    {
                        string path = string.Empty;
                        try
                        {
                            path = DatabaseManager.GetDatabasePath(connection);
                        }
                        catch
                        {
                            return path;
                        }         
                        return path;
                    }
                );
        }

        public bool CreateDatabase(string pAccountName, string pDatabaseName, string pLogin, string pPassword, string pVersion, string pScriptPath)
        {
            SqlConnection connection = DatabaseConnection.Remoting.GetSqlConnectionOnMaster();
            CreateDatabaseImpl(pDatabaseName, pVersion, pScriptPath, connection);
            _addDatabaseToAccounts(pAccountName, pDatabaseName, pLogin, pPassword, connection);

            return true;
        }

        public bool CreateAccountDatabase(string pScriptPath)
        {
            string sqlConnection = String.Format(@"user id={0};password={1};data source={2};persist security info=False;initial catalog={3};connection timeout=10",
                RemoteServerSettings.GetSettings().LoginName, RemoteServerSettings.GetSettings().Password, RemoteServerSettings.GetSettings().ServerName, "MASTER");

            SqlConnection connection = new SqlConnection(sqlConnection);
            try
            {
                connection.Open();
                DatabaseManager.CreateDatabase("Accounts", connection);
                DatabaseManager.ExecuteScript(pScriptPath, "Accounts", connection);
                connection.Close();
                return true;
            }
            catch
            {
                connection.Close();
                throw;
            }
        }

        public bool UpdateAccountActive(string pAccountName, bool pActive)
        {
            string sqlConnection = String.Format(@"user id={0};password={1};data source={2};persist security info=False;initial catalog={3};connection timeout=10",
                RemoteServerSettings.GetSettings().LoginName, RemoteServerSettings.GetSettings().Password, RemoteServerSettings.GetSettings().ServerName, "Accounts");


            SqlConnection connection = new SqlConnection(sqlConnection);
            try
            {
                connection.Open();
                int affectedRows = DatabaseManager.UpdateAccountActive(pAccountName, pActive, connection);
                connection.Close();
                return affectedRows == 1;
            }
            catch
            {
                connection.Close();
                throw;
            }
        }

        public bool ChangeAdminPasswordForAccount(string pAccountName, string pPassword)
        {
            SqlConnection connection = DatabaseConnection.Remoting.GetSqlConnectionOnMaster();
            try
            {
                connection.Open();
                int affectedRows = DatabaseManager.ChangeAdminPasswordForAccount(pAccountName, pPassword, connection);
                connection.Close();
                return affectedRows == 1;
            }
            catch
            {
                connection.Close();
                throw;
            }

        }

        private bool _addDatabaseToAccounts(string pAccountName, string pDatabaseName, string pLogin, string pPassword, SqlConnection pSqlConnection)
        {
            try
            {
                pSqlConnection.Open();
                DatabaseManager.AddDatabaseToAccounts(pAccountName, pDatabaseName, pLogin, pPassword, pSqlConnection);
                pSqlConnection.Close();
                return true;
            }
            catch (Exception ex)
            {
                pSqlConnection.Close();
                throw;
            }
        }

        /// <summary>
        /// Read all database objects from the xml file, generate
        /// update script, and save it into a temp file.
        /// </summary>
        /// <returns>Path to the temp file</returns>
        private static string _GetSqlObjects()
        {
            string retval = Path.GetTempFileName();
            StreamWriter writer = new StreamWriter(retval);
            writer.Write(DatabaseManager.GetObjectLoadScript());
            writer.Close();
            return retval;
        }

        public SqlDatabaseSettings GetSQLDatabaseSettings(string pDatabaseName)
        {
            SqlConnection connection = ConnectionManager.GeneralSqlConnection;
            try
            {
                connection.Open();
                SqlDatabaseSettings sqlDatabase = _GetDatabaseInfos(pDatabaseName, connection);

                connection.Close();
                return sqlDatabase;
            }
            catch (Exception)
            {
                connection.Close();
                throw;
            }
        }

        public List<SqlDatabaseSettings> GetSQLDatabasesSettings(string pDatabaseServerName, string pDatabaseLoginName, string pDatabasePassword)
        {
            SqlConnection connection = ConnectionManager.GeneralSqlConnection;
            try
            {
                connection.Open();
                SQLInfoEnumerator sie = new SQLInfoEnumerator
                {
                    SQLServer = pDatabaseServerName,
                    Username = pDatabaseLoginName,
                    Password = pDatabasePassword
                };

                List<SqlDatabaseSettings> list = new List<SqlDatabaseSettings>();
                foreach (string database in sie.EnumerateSQLServersDatabases())
                {
                    SqlDatabaseSettings sqlDatabase = _GetDatabaseInfos(database, connection);
                    if (sqlDatabase == null) continue;

                    list.Add(sqlDatabase);
                }

                connection.Close();
                List<string> filter = TechnicalSettings.AvailableDatabases;
                return list.FindAll(db => 0 == filter.Count || filter.Contains(db.Name));
            }
            catch (Exception)
            {
                connection.Close();
                throw;
            }
        }

        public List<SqlAccountsDatabase> GetSQLAccountsDatabases()
        {
            return DatabaseManager.GetListDatabasesIntoAccounts(DatabaseConnection.Remoting.GetSqlConnectionOnMaster());
        }

        public bool ExecuteScript(string pScriptPath, string pDatabase, string pServerName, string pLoginName, string pPassword)
        {
            string sqlConnection = String.Format(@"user id={0};password={1};data source={2};persist security info=False;initial catalog={3};connection timeout=10",
                pLoginName, pPassword, pServerName, pDatabase);

            SqlConnection connection = new SqlConnection(sqlConnection);
            try
            {
                connection.Open();
                DatabaseManager.ExecuteScript(pScriptPath, pDatabase, connection);
                connection.Close();
                return true;
            }
            catch
            {
                connection.Close();
                throw;
            }
        }

        public string GetDatabaseNameForAccount(string pAccount, string pServerName, string pLoginName, string pPassword)
        {
            string sqlConnection = String.Format(@"user id={0};password={1};data source={2};persist security info=False;initial catalog={3};connection timeout=10",
                pLoginName, pPassword, pServerName, "Master");

            SqlConnection connection = new SqlConnection(sqlConnection);
            try
            {
                connection.Open();
                var db = DatabaseManager.GetDatabaseNameForAccount(pAccount, connection);
                connection.Close();
                return db;
            }
            catch
            {
                connection.Close();
                throw;
            }
        }

        private static SqlDatabaseSettings _GetDatabaseInfos(string pDatabaseName, SqlConnection pSqlConnection)
        {
            SqlDatabaseSettings sqlDatabase = new SqlDatabaseSettings { Name = pDatabaseName };
            string version = DatabaseManager.GetDatabaseVersion(pDatabaseName, pSqlConnection);

            if (string.IsNullOrEmpty(version)) return null;

            sqlDatabase.Version = version;
            sqlDatabase.Size = DatabaseManager.GetDatabaseSize(pDatabaseName, pSqlConnection);
            string code = DatabaseManager.GetDatabaseBranchCode(pDatabaseName, pSqlConnection);
            if (string.IsNullOrEmpty(code))
                code = DatabaseManager.GetDatabaseBranchCodeFromBranches(pDatabaseName, pSqlConnection);
            sqlDatabase.BranchCode = code;
            return sqlDatabase;
        }

        public string RawBackup(string dbName, string version, string branchCode, string backupPath)
        {
            SqlConnection conn = ConnectionManager.GeneralSqlConnection;
            try
            {
                conn.Open();
                string timestamp = TimeProvider.Today.ToString("ddMMyyyy");
                string fileName = 
                    _FindAvailableName(String.Format("Octopus-{0}-{1}-{2}-@-{3}.bak", version, dbName, 
                    branchCode, timestamp));
                BackupManager.Backup(fileName, backupPath, dbName, conn);

                if (DatabaseManager.DatabaseExists(dbName + "_attachments", conn))
                {
                    string attachments = string.Format("Octopus-{0}-{1}-{2}-@-{3}-attachments.bak"
                        , version
                        , dbName
                        , branchCode
                        , timestamp
                    );
                    BackupManager.Backup(attachments, backupPath, dbName + "_attachments", conn);
                }

                return fileName;
            }
            finally
            {
                conn.Close();
            }
        }

        public void RawBackup(string dbName, string backupFolder)
        {
            SqlConnection conn = ConnectionManager.GeneralSqlConnection;
            try
            {
                conn.Open();
                BackupManager.RawBackup(dbName, backupFolder, conn);
            }
            finally
            {
                conn.Close();
            }
        }

        public bool Restore(string backupPath, string dbName)
        {
            SqlConnection connection = ConnectionManager.GeneralSqlConnection;

            try
            {
                connection.Open();

                string dataDirectory = GetDatabaseDefaultPath();
                BackupManager.Restore(backupPath, dbName, dataDirectory, connection);

                string attFile = Path.GetFileNameWithoutExtension(backupPath);
                attFile = Path.GetFileNameWithoutExtension(attFile);
                attFile += "-attachments.bak";
                attFile = Path.Combine(Path.GetDirectoryName(backupPath), attFile);
                if (!File.Exists(attFile))
                    attFile += ".zip";

                if (File.Exists(attFile))
                {
                    dbName += "_attachments";
                    if (!DatabaseManager.DatabaseExists(dbName, connection))
                        DatabaseManager.CreateDatabase(dbName, connection);

                    BackupManager.Restore(attFile, dbName, dataDirectory, connection);
                }

                connection.Close();
                return true;
            }
            catch (Exception)
            {
                connection.Close();
                throw;
            }
        }

        public void RawRestore(string filePath, string dbName)
        {
            SqlConnection connection = ConnectionManager.GeneralSqlConnection;

            try
            {
                connection.Open();

                string dataDirectory = GetDatabaseDefaultPath();
                BackupManager.RawRestore(filePath, dbName, dataDirectory, connection);
            } finally
            {
                connection.Close();   
            }
        }

        public void GenerateEmtyDatabase(string dbName)
        {
            SqlConnection connection = ConnectionManager.GeneralSqlConnection;

            try
            {
                connection.Open();
                DatabaseManager.CreateDatabase(dbName, connection);
            } finally { connection.Close(); }
        }

        private static string _FindAvailableName(string pFileName)
        {
            bool available = false;
            int counter = -1;
            string name = pFileName;
            while (!available)
            {
                counter++;
                if (counter > 0) name += string.Format(" ({0})", counter);
                available = (!_IsThisNameAlreadyUsed(name));
            }
            return name;
        }

        private static bool _IsThisNameAlreadyUsed(string pName)
        {
            string fileName = Path.Combine(UserSettings.BackupPath, pName);
            return (File.Exists(fileName + ".bak")) || (File.Exists(fileName + ".bak.zip"));
        }

        private static List<SqlUpdateDatabaseScript> _GetScriptsToUpgradeDatabase(string pExpectedVersion, string pCurrentVersion)
        {
            List<SqlUpdateDatabaseScript> scripts = new List<SqlUpdateDatabaseScript>();
            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.Load(Path.Combine(UserSettings.GetUpdatePath, UPGRADE_SCHEMA_FILE_NAME));
            XmlNodeList listColumn = xmlDoc.GetElementsByTagName("upgrade");
            bool start = false;
            foreach (XmlNode elem in listColumn)
            {
                SqlUpdateDatabaseScript script = new SqlUpdateDatabaseScript { Current = elem.Attributes["current"].Value };
                if (script.Current == pCurrentVersion)
                    start = true;

                if (!start) continue;
                
                script.Expected = elem.Attributes["expected"].Value;
                script.Name = string.Format(UPDATEDATABASE, script.Current, script.Expected);
                scripts.Add(script);

                if (script.Expected == pExpectedVersion)
                    return scripts;
            }
            return scripts;
        }

        /// <summary>
        /// Dump all the necessary database objects (views, functions, and stored procedures)
        /// into mdb file.
        /// </summary>
        public void DumpObjects(string pDatabaseName)
        {
            SqlConnection connection = ConnectionManager.GeneralSqlConnection;
            try
            {
                connection.Open();
                DatabaseManager.DumpObjects(pDatabaseName, connection);
                connection.Close();
            }
            catch (Exception)
            {
                connection.Close();
                throw;
            }
        }

        public void ShrinkDatabase()
        {
            SqlConnection connection = ConnectionManager.GeneralSqlConnection;
            try
            {
                connection.Open();
                DatabaseManager.ShrinkDatabase(TechnicalSettings.DatabaseName, connection);
                connection.Close();
            }
            catch (Exception)
            {
                connection.Close();
                throw;
            }
        }

        public void SaveDatabaseDiagramsInXml(bool pBool,string pDatabaseName)
        {
            SqlConnection connection = ConnectionManager.GeneralSqlConnection; 
            try
            {
                connection.Open();
                CompareDatabase.SaveDatabaseDiagramsInXml(pBool,pDatabaseName,connection);
                connection.Close();
            }
            catch (Exception)
            {
                connection.Close();
                throw;
            }
        }
    }
}
