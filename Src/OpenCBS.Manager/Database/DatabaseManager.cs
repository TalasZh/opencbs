// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Xml.XPath;
using OpenCBS.CoreDomain;
using System.IO;
using System.Data;
using System.Xml;
using OpenCBS.Shared.Settings;
using OpenCBS.CoreDomain.Database;

namespace OpenCBS.Manager.Database
{
    public class DatabaseManager
    {
        internal  string _database;

        public DatabaseManager(User pUser)
        {
            Init();
        }

        public DatabaseManager(string pTestDb)
        {
            Init();
        }
        private static DatabaseManager _theUniqueInstance;

        private void Init()
        {
            _database = TechnicalSettings.DatabaseName;
        }

        public static DatabaseManager GetInstance(string pTestDb)
        {
            return _theUniqueInstance ?? (_theUniqueInstance = new DatabaseManager(pTestDb));
        }

        public static void CreateDatabase(string pDatabaseName, SqlConnection pSqlConnection)
        {
            string sqlText = "CREATE DATABASE " + pDatabaseName;
            OpenCbsCommand cmd = new OpenCbsCommand(sqlText, pSqlConnection);

            cmd.ExecuteNonQuery();
        }

        public static void DeleteDatabase(string pDatabaseName, SqlConnection pSqlConnection)
        {
            string sqlText = string.Format("ALTER DATABASE {0} SET SINGLE_USER WITH ROLLBACK IMMEDIATE", pDatabaseName);
            OpenCbsCommand cmd = new OpenCbsCommand(sqlText, pSqlConnection);
            cmd.ExecuteNonQuery();

            sqlText = "DROP DATABASE " + pDatabaseName;
            cmd = new OpenCbsCommand(sqlText, pSqlConnection);
            cmd.ExecuteNonQuery();
        }
        
        /// <summary>
        /// Executes a SQL script file.<br/>
        /// Each peace of script delimited by "GO" is executed individually.<br/>
        /// If any error occurs, the script execution stops and returns an error.<br/>
        /// Exept after the /** IGNORE ERRORS **/ section where all errors are ignored.<br/>
        /// Warning : Only /*xxx*/ comments on a single line are supported!<br/>
        /// </summary>
        /// <param name="pScriptFile">Scripy file path</param>
        /// <param name="pDatabaseName"></param>
        /// <param name="pSqlConnection"></param>
        /// <returns>Script exec result status</returns>

        public static void ExecuteScript(string pScriptFile, string pDatabaseName, SqlConnection pSqlConnection)
        {
            List<string> queries = new List<string> {string.Format("USE [{0}]", pDatabaseName)};
            queries.AddRange(_ParseSqlFile(pScriptFile));

            foreach (string query in queries)
            {
                OpenCbsCommand command = new OpenCbsCommand(query, pSqlConnection) { CommandTimeout = 480 };
                command.ExecuteNonQuery();
            }
        }

        private static List<string> _ParseSqlFile(string pScriptFile)
        {
            List<string> queries = new List<string>();
            Encoding encoding = Encoding.GetEncoding("utf-8");
            FileStream fs = new FileStream(pScriptFile, FileMode.Open);
            using (StreamReader reader = new StreamReader(fs, encoding))
            {
                // Parse file and get individual queries (separated by GO)
                while (!reader.EndOfStream)
                {
                    StringBuilder sb = new StringBuilder();
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        if (line.StartsWith("GO")) break;
                        if ((!line.StartsWith("/*")) && (line.Length > 0))
                        {
                            sb.Append(line);
                            sb.Append("\n");
                        }
                    }
                    string q = sb.ToString();
                    if ((!q.StartsWith("SET QUOTED_IDENTIFIER"))
                        && (!q.StartsWith("SET ANSI_"))
                        && (q.Length > 0))
                    {
                        queries.Add(q);
                    }
                }
            }
            return queries;
        }

        /// <summary>
        /// Generate script for recreating objects and return
        /// it to the caller.
        /// </summary>
        /// <returns>Script</returns>
        public static string GetObjectLoadScript()
        {
            string path = Path.Combine(UserSettings.GetUpdatePath, "Objects.xml");
            XPathDocument xmldoc = new XPathDocument(path);
            XPathNavigator nav = xmldoc.CreateNavigator();
            XPathExpression expr = nav.Compile("/database/object");
            expr.AddSort("@priority", XmlSortOrder.Ascending, XmlCaseOrder.None, null, XmlDataType.Number);
            XPathNodeIterator iterator = nav.Select(expr);

            string retval = string.Empty;
            while (iterator.MoveNext())
            {
                XPathNavigator create = iterator.Current.SelectSingleNode("create");
                XPathNavigator drop = iterator.Current.SelectSingleNode("drop");
                retval += string.Format("{0}\r\nGO\r\n\r\n{1}\r\nGO\r\n\r\n", drop.Value, create.Value);
            }
            return retval;
        }

        public static string GetDatabaseVersion(string pDatabase, SqlConnection pSqlConnection)
        {
            if (!IsOctopusDatabase(pDatabase, pSqlConnection)) return string.Empty;

            string sqlText = string.Format(
                "USE [{0}] SELECT [value] FROM [TechnicalParameters] WHERE [name]='version'", pDatabase);
            using (OpenCbsCommand select = new OpenCbsCommand(sqlText, pSqlConnection))
            using (OpenCbsReader reader = select.ExecuteReader())
            {
                if (reader.Empty) return string.Empty;
                reader.Read();
                return reader.GetString("value");
            }
        }

        public static bool IsOctopusDatabase(string dbName, SqlConnection connection)
        {
            string query =
                string.Format("USE [{0}] SELECT 1 FROM sys.tables WHERE NAME = 'TechnicalParameters'", dbName);

            using (OpenCbsCommand command = new OpenCbsCommand(query, connection))
                return command.ExecuteScalar() != null;
        }

        public static string GetDatabaseBranchCode(string pDatabaseName, SqlConnection pSqlConnection)
        {
            string sqlText = string.Format("USE [{0}] SELECT [value] FROM [GeneralParameters] WHERE [key]='BRANCH_CODE'", pDatabaseName);
            using (OpenCbsCommand select = new OpenCbsCommand(sqlText, pSqlConnection))
            {
                using (OpenCbsReader reader = select.ExecuteReader())
                {
                    if (reader.Empty) return string.Empty;
                    reader.Read();
                    return reader.GetString("value");
                }
            }
        }

        public static string GetDatabaseBranchCodeFromBranches(string pDatabaseName, SqlConnection pSqlConnection)
        {
            string sqlText = string.Format("USE [{0}] SELECT TOP 1 [code] FROM [Branches] WHERE [deleted] = 0", pDatabaseName);
            using (OpenCbsCommand select = new OpenCbsCommand(sqlText, pSqlConnection))
            {
                using (OpenCbsReader reader = select.ExecuteReader())
                {
                    if (reader.Empty) return string.Empty;
                    reader.Read();
                    string code = reader.GetString("code"); 
                    if (string.IsNullOrEmpty(code))
                        throw new ApplicationException(
                            string.Format("Emty or no branch code found for database: {0}.",pDatabaseName));
                    return code;
                }
            }
        }

        public static string GetDatabaseSize(string pDatabase, SqlConnection pSqlConnection)
        {
            string sql = string.Format("USE [{0}] EXEC sp_spaceused", pDatabase);

            using (OpenCbsCommand cmd = new OpenCbsCommand(sql, pSqlConnection))
            {
                using (OpenCbsReader reader = cmd.ExecuteReader())
                {
                    if (reader.Empty) return string.Empty;
                    if (reader.Read())
                    {
                        return reader.GetString("database_size");
                    }
                    return string.Empty;
                }
            }
        }

        public static string GetDatabasePath(SqlConnection pSqlConnection)
        {
            const string sqlText =
                @"
                    SELECT physical_name
                    FROM   [master].sys.master_files
                    WHERE  database_id = 1 AND FILE_ID = 1
                ";

            using (OpenCbsCommand select = new OpenCbsCommand(sqlText, pSqlConnection))
            {
                string dbFilePath = (string)select.ExecuteScalar();
                return Path.GetDirectoryName(dbFilePath);
            }
        }

      
        public static void ShrinkDatabase(string pDatabaseName, SqlConnection pSqlConnection)
        {
            string database = TechnicalSettings.DatabaseName;
            string sql1 = String.Format("ALTER DATABASE {0} SET RECOVERY SIMPLE", database);
            string sql2 = String.Format("ALTER DATABASE {0} SET AUTO_SHRINK ON", database);

            try
            {
                OpenCbsCommand cmd = new OpenCbsCommand(sql1, pSqlConnection);
                cmd.ExecuteNonQuery();
                cmd = new OpenCbsCommand(sql2, pSqlConnection);
                cmd.ExecuteNonQuery();

                string sql = String.Format("DBCC SHRINKDATABASE ({0})", database);
                cmd = new OpenCbsCommand(sql, pSqlConnection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception) { }
        }

        private static List<string> GetObjectNamesToDump()
        {
            List<string> retval = new List<string>
            {
                "FilteredCreditContracts",
                "check_installment_paid",
                "PenaltiesCalculation",
                "GetOLB",
                "getLateDays",
                "GetNbMembers",
                "GetDueInterest",
                "GetClientID",
                "next_contractEvents",
                "InstallmentSnapshot",
                "ActiveLoans",
                "ActiveClients",
                "ExchangeRatesEx",
                "GetListMembersGroupLoan",
                "GetTrueLoanCycle",
                "GetXR",
                "CalculateLatePenalty",
                "GetDisbursementDate",
                "LoanEntryFees",
                "Disbursements",
                "Disbursements_MC",
                "RepaymentsAll",
                "RepaymentsAll_MC",
                "Repayments",
                "Repayments_MC",
                "Balances",
                "Balances_MC",
                "ClosedContracts",
                "ClosedLoans",
                "ClosedLoans_MC",
                "ClosedContracts_MC", 
                "StringListToTable", 
                "AuditTrailEvents",
                "ExportAccounting_Transactions",
                "ExportAccounting_Transaction_with_dates",
                "GetAccountBalance",
                "GetAccountChilds",
                "GetChartOfAccountsBalances",
                "IntListToTable",
                "Clients",
                "getSavingBalance",
                "ActiveSavingContracts",
                "ActiveSavingContracts_MC",
                "ActiveSavingAccounts",
                "ActiveSavingAccounts_MC",
                "RepaymentSchedule",
                "SavingDeposits",
                "SavingDeposits_MC",
                "SavingWithdrawals",
                "SavingWithdrawals_MC",
                "SavingTransfers",
                "SavingTransfers_MC",
                "SavingCommissions",
                "SavingCommissions_MC",
                "SavingPenalties",
                "SavingPenalties_MC",
                "GetAccountBookings",
                "GetBalanceByAccountCategory",
                "ExpectedInstallments",
                "ExpectedInstallments_MC",
                "ActiveLoans_MC",
                "LateAmounts",
                "LateAmounts_MC",
                "ActiveClients_MC",
                "getEntryFees",
                "Alerts",
                "GetAdvancedFieldValue",
                "RescheduledLoans",
                "RescheduledLoans_MC",
                "WrittenOffLoans",
                "Movements",
                "FilteredManualAccountingMovements",
                "FilteredSavingsAccountingMovements",
                "FilteredLoanAccountingMovements",
                "TrialBalance",
                "AccountMovements",
                "Guarantors",
                "Collaterals",
                "GetTellerBalance",
                "GetDashboard",
            };
            return retval;
        }

        public static string GetObjectCreateScript(string db, string name, SqlConnection conn)
        {
            string q = string.Format("{0}..sp_helptext", db);
            OpenCbsCommand cmd = new OpenCbsCommand(q, conn).AsStoredProcedure();
            cmd.AddParam("@objname", name);
            var buffer = new StringBuilder(2048);
            using (OpenCbsReader reader = cmd.ExecuteReader())
            {
                if (null == reader) return string.Empty;
                while (reader.Read())
                {
                    buffer.Append(reader.GetString("Text"));
                }
            }
            return buffer.ToString();
        }

        public static string GetObjectDropScript(string db, string name, SqlConnection conn)
        {
            string q = @"SELECT xtype 
            FROM {0}..sysobjects
            WHERE name = @name";
            q = string.Format(q, db);

            OpenCbsCommand c = new OpenCbsCommand(q, conn);
            c.AddParam("@name", name);
            string xtype = c.ExecuteScalar().ToString().Trim();

            string xtypeObject = string.Empty;
            switch (xtype)
            {
                case "FN":
                case "IF":
                case "TF":
                    xtypeObject = "FUNCTION";
                    break;

                case "P":
                    xtypeObject = "PROCEDURE";
                    break;

                case "V":
                    xtypeObject = "VIEW";
                    break;

                default:
                    Debug.Fail("Cannot be here.");
                    break;
            }

            const string retval = @"IF  EXISTS (
                SELECT * 
                FROM sys.objects 
                WHERE object_id = OBJECT_ID(N'dbo.{0}') AND type = N'{1}'
            )
            DROP {2} [dbo].[{0}]";

            return string.Format(retval, name, xtype, xtypeObject);
        }

        public static void DumpObjects(string db, SqlConnection conn)
        {
            List<string> names = GetObjectNamesToDump();
            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            doc.AppendChild(dec);
            XmlElement root = doc.CreateElement("database");
            doc.AppendChild(root);

            int priority = 1;
            foreach (string name in names)
            {
                XmlElement node = doc.CreateElement("object");
                node.SetAttribute("name", name);
                node.SetAttribute("priority", Convert.ToString(priority));
                root.AppendChild(node);

                XmlElement createNode = doc.CreateElement("create");
                string createScript = GetObjectCreateScript(db, name, conn);
                XmlCDataSection createText = doc.CreateCDataSection(createScript);
                createNode.AppendChild(createText);
                node.AppendChild(createNode);

                XmlElement dropNode = doc.CreateElement("drop");
                string dropScript = GetObjectDropScript(db, name, conn);
                XmlCDataSection dropText = doc.CreateCDataSection(dropScript);
                dropNode.AppendChild(dropText);
                node.AppendChild(dropNode);

                priority++;
            }

            string file = Path.Combine(UserSettings.GetUpdatePath, "Objects.xml");
            doc.Save(file);
        }

        public static void AddDatabaseToAccounts(string pAccountName, string pDatabaseName, string pLogin, string pPassword, SqlConnection pSqlConnection)
        {
            const string sqlText = @"INSERT INTO [Accounts].[dbo].[SqlAccounts]
                                   ([account_name],[database_name],[user_name],[password],[active])
                                    VALUES (@accountName, @databaseName, @login, @password,1)";

            using (OpenCbsCommand insert = new OpenCbsCommand(sqlText, pSqlConnection))
            {
                insert.AddParam("@accountName",  pAccountName);
                insert.AddParam("@databaseName",  pDatabaseName);
                insert.AddParam("@login",  pLogin);
                insert.AddParam("@password", pPassword);

                insert.ExecuteNonQuery();
            }
        }

        public static int UpdateAccountActive(string pAccountName, bool pActive, SqlConnection pSqlConnection)
        {
            const string sqlText = @"UPDATE [Accounts].[dbo].[SqlAccounts]
                                     SET active = @active
                                     WHERE [account_name] = @accountName";
            using (OpenCbsCommand update = new OpenCbsCommand(sqlText, pSqlConnection))
            {
                update.AddParam("@active",  pActive);
                update.AddParam("@accountName",  pAccountName);

                return update.ExecuteNonQuery();
            }
        }

        public static List<SqlAccountsDatabase> GetListDatabasesIntoAccounts(SqlConnection pSqlConnection)
        {
            List<SqlAccountsDatabase> databases = new List<SqlAccountsDatabase>();

            const string sqlText = @"SELECT *
                                     FROM [Accounts].[dbo].[SqlAccounts]";

            if (pSqlConnection.State == ConnectionState.Closed)
                pSqlConnection.Open();

            using (OpenCbsCommand select = new OpenCbsCommand(sqlText, pSqlConnection))
            {
                using (OpenCbsReader reader = select.ExecuteReader())
                {

                    if (reader == null || reader.Empty) return databases;

                    while (reader.Read())
                    {
                        databases.Add(new SqlAccountsDatabase
                        {
                            Account = reader.GetString("account_name"),
                            Database = reader.GetString("database_name"),
                            Login = reader.GetString("user_name"),
                            Password = reader.GetString("password"),
                            Active = reader.GetBool("active")
                        });
                    }
                }
            }

            foreach (SqlAccountsDatabase db in databases)
            {
                db.Version = GetDatabaseVersion(db.Database, pSqlConnection);
            }

            return databases;
        }

        public static string GetDatabaseNameForAccount(string pAccountName, SqlConnection pSqlConnection)
        {
            const string sqlText = @"SELECT database_name
                                     FROM Accounts.dbo.SqlAccounts
                                     WHERE account_name = @account";

            if (pSqlConnection.State == ConnectionState.Closed)
                pSqlConnection.Open();

            using (OpenCbsCommand select = new OpenCbsCommand(sqlText, pSqlConnection))
            {
                select.AddParam("@account", pAccountName);
                return (string)select.ExecuteScalar();
            }
        }

        public static int ChangeAdminPasswordForAccount(string pAccountName, string pPassword, SqlConnection pSqlConnection)
        {
            const string sqlText = @"DECLARE @dbName NVARCHAR(50),
                                             @query NVARCHAR(200)
                                     
                                     SELECT @dbName = database_name
	                                 FROM Accounts.dbo.SqlAccounts
	                                 WHERE account_name = @account
	
	                                 SET @query = 'UPDATE ' + @dbName + '.dbo.Users	SET user_pass = ''' + @password + ''' WHERE [user_name] = ''admin'''
		
	                                 EXEC (@query)";

            if (pSqlConnection.State == ConnectionState.Closed)
                pSqlConnection.Open();

            using (OpenCbsCommand update = new OpenCbsCommand(sqlText, pSqlConnection))
            {
                update.AddParam("@account",  pAccountName);
                update.AddParam("@password",  pPassword);

                return update.ExecuteNonQuery();
            }
        }

        public static bool DatabaseExists(string name, SqlConnection conn)
        {
            string q = @"SELECT CASE
            WHEN DB_ID('{0}') IS NULL THEN 0 ELSE 1 END";
            q = string.Format(q, name);

            OpenCbsCommand cmd = new OpenCbsCommand {CommandText = q, Connection = conn};
            return Convert.ToBoolean(cmd.ExecuteScalar());
        }
    }
}
