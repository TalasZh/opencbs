// LICENSE PLACEHOLDER

using System;
using System.Data.SqlClient;
using OpenCBS.CoreDomain;
using OpenCBS.DatabaseConnection;
using OpenCBS.Shared.Settings;


namespace OpenCBS.Manager
{
    /// <summary>
    /// Parent class for dabase managers.
    /// </summary>
    public class Manager : MarshalByRefObject
    {
        private readonly ConnectionManager _connectionManager;
        protected readonly string _md5;

        public SqlConnection GetConnection()
        {
            return CoreDomain.DatabaseConnection.GetConnection();
        }

        protected Manager(string pDatabaseConnectionString)
        {
            _connectionManager = ConnectionManager.GetInstance(pDatabaseConnectionString);
        }

        protected Manager(User pUser)
        {
            _md5 = pUser.Md5;
            _connectionManager = ConnectionManager.GetInstance();
        }

        protected SqlConnection AttachmentsConnection
        {
            get
            {
                return null == _connectionManager ? null : _connectionManager.AttachmentsSqlConnection;
            }
        }

        protected bool RecordExists(string tableName, SqlConnection connection)
        {
            const string checkSql = "IF EXISTS(SELECT 1 FROM {0}) SELECT 1 ELSE SELECT 0";
            string query = string.Format(checkSql, tableName);
            using (SqlConnection conn = GetConnection())
            using (OctopusCommand command = new OctopusCommand(query, conn))
                    return Convert.ToInt32(command.ExecuteScalar()) == 1;
        }

        protected void DeleteDatasFromTable(string tableName, SqlTransaction transaction)
        {
            const string deleteSql = "DELETE FROM {0}";
            string query = string.Format(deleteSql, tableName);
            using (OctopusCommand command = new OctopusCommand(query, transaction.Connection, transaction))
                command.ExecuteNonQuery();
        }
    }
}
