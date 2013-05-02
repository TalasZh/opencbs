// LICENSE PLACEHOLDER

using System;
using System.Data.SqlClient;
using OpenCBS.Shared.Settings;

namespace OpenCBS.DatabaseConnection
{
    [Serializable]
    public class ConnectionManager
    {
        private readonly IConnectionManager _connectionManager;
        private static ConnectionManager _theUniqueInstance;

        private ConnectionManager()
        {
            _connectionManager = TechnicalSettings.UseOnlineMode ? Remoting.GetInstance() : Standard.GetInstance();
        }

        private ConnectionManager(string pTestDb)
        {
            _connectionManager = Standard.GetInstance(pTestDb);
        }

        private ConnectionManager(string pLogin, string pPassword, string pServer, string pDatabase, string pTimeout)
        {
            _connectionManager = TechnicalSettings.UseOnlineMode 
                ? Remoting.GetInstance() 
                : Standard.GetInstance(pLogin, pPassword, pServer, pDatabase, pTimeout);
        }

        public static ConnectionManager GetInstance()
        {
            return _theUniqueInstance ?? (_theUniqueInstance = new ConnectionManager());
        }

        public static ConnectionManager GetInstance(string pLogin, string pPassword, string pServer, string pDatabase, string pTimeout)
        {
            return _theUniqueInstance ?? (_theUniqueInstance = new ConnectionManager(pLogin, pPassword, pServer, pDatabase, pTimeout));
        }

        public static ConnectionManager GetInstance(string pTestDb)
        {
            return _theUniqueInstance ?? (_theUniqueInstance = new ConnectionManager(pTestDb));
        }

        public static void KillSingleton()
        {
            Standard.KillSingleton();
            _theUniqueInstance = null;
        }

        public SqlConnection GetSqlConnection(string pMd5)
        {
            return _connectionManager.GetSqlConnection(pMd5);
        }

        public SqlConnection GetSecondarySqlConnection(string pMd5)
        {
            return _connectionManager.GetSecondarySqlConnection(pMd5);
        }

        public SqlConnection SqlConnection
        {
            get { return _connectionManager.SqlConnection; }
        }

        public SqlConnection SecondarySqlConnection
        {
            get { return _connectionManager.SecondarySqlConnection; }
        }

        public SqlConnection AttachmentsSqlConnection
        {
            get { return _connectionManager.AttachmentsSqlConnection; }
        }

        public SqlTransaction GetSqlTransaction(string pMd5)
        {
            return _connectionManager.GetSqlTransaction(pMd5);
        }

        public void CloseConnection()
        {
            _connectionManager.CloseConnection();
        }

        public void CloseSecondaryConnection()
        {
            _connectionManager.CloseSecondaryConnection();
        }

        public bool ConnectionInitSuceeded
        {
            get { return _connectionManager.ConnectionInitSuceeded; }
        }

        public SqlConnection SqlConnectionOnMaster
        {
            get { return _connectionManager.SqlConnectionOnMaster; }
        }

        public SqlConnection SqlConnectionForRestore
        {
            get { return _connectionManager.SqlConnectionForRestore; }
        }

        public void KillAllConnections()
        {
            _connectionManager.KillAllConnections();
        }

        public void SetConnection(SqlConnection pConnection)
        {
            _connectionManager.SetConnection(pConnection);
        }


        public static bool CheckSQLServerConnection()
        {
            return TechnicalSettings.UseOnlineMode
                ? Remoting.CheckSQLServerConnection()
                : Standard.CheckSQLServerConnection();
        }

        public static bool CheckSQLDatabaseConnection()
        {
            return Standard.CheckSQLDatabaseConnection();
        }

        public static SqlConnection GeneralSqlConnection
        {
            get
            {
                return TechnicalSettings.UseOnlineMode
                    ? Remoting.GetSqlConnectionOnMaster()
                    : Standard.MasterConnection();
                //? Remoting.CheckSQLServerConnection()
                //: Standard.CheckSQLServerConnection();
            }
        }
    }
}
