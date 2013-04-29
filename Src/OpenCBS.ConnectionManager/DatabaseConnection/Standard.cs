using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Octopus.Shared.Settings;

namespace Octopus.DatabaseConnection
{
    [Serializable]
    public class Standard : IConnectionManager
    {
        private SqlConnection _connection;
        private SqlConnection _secondaryConnection;
        private SqlConnection _connectionOnMasterDatabase;
        private SqlConnection _attachmentsConnection;
        private string _connectionStringForRestore;

        private static IConnectionManager _theUniqueInstance;
        private SqlTransaction sqlTransaction;

        public SqlConnection GetSqlConnection(string pM5)
        {
            return _connection;
        }

        public SqlConnection GetSecondarySqlConnection(string pM5)
        {
            return _secondaryConnection;
        }


        private Standard()
        {
            InitConnections(TechnicalSettings.DatabaseLoginName, TechnicalSettings.DatabasePassword, TechnicalSettings.DatabaseServerName, TechnicalSettings.DatabaseName, "240");
        }

        private Standard(string pTestDb)
        {
            InitConnections(TechnicalSettings.DatabaseLoginName, TechnicalSettings.DatabasePassword, TechnicalSettings.DatabaseServerName, pTestDb, "60");
        }

        private Standard(string pLogin, string pPassword, string pServer, string pDatabase, string pTimeout)
        {
            InitConnections(pLogin, pPassword, pServer, pDatabase, pTimeout);
        }

        private bool _connectionInitSuceeded = false;

        public bool ConnectionInitSuceeded { get { return _connectionInitSuceeded; } }

        private void InitConnections(string pLogin, string pPassword, string pServer, string pDatabase, string pTimeout)
        {
            try
            {
                _connectionOnMasterDatabase = new SqlConnection(
                   String.Format("user id={0};password={1};data source={2};persist security info=False;initial catalog=MASTER;connection timeout={3}",
                                 pLogin, pPassword, pServer, pTimeout));

                _connectionStringForRestore =
                    String.Format("user id={0};password={1};data source={2};persist security info=False;initial catalog=MASTER;connection timeout={3}",
                                  pLogin, pPassword, pServer, pTimeout);

                _connection = new SqlConnection(
                    String.Format("user id={0};password={1};data source={2};persist security info=False;initial catalog={3};connection timeout={4};Asynchronous Processing=true",
                                  pLogin, pPassword, pServer, pDatabase, pTimeout));
                _connection.Open();

                _secondaryConnection = new SqlConnection(
                    String.Format("user id={0};password={1};data source={2};persist security info=False;initial catalog={3};connection timeout={4}",
                                  pLogin, pPassword, pServer, pDatabase, pTimeout));
                _secondaryConnection.Open();

                _attachmentsConnection = new SqlConnection(
                    String.Format("user id={0};password={1};data source={2};persist security info=False;initial catalog={3};connection timeout={4}",
                                  pLogin, pPassword, pServer, pDatabase + "_attachments", pTimeout));
                
                _connectionInitSuceeded = true;
            }
            catch
            {
                _connectionInitSuceeded = false;
            }
        }

        public static IConnectionManager GetInstance()
        {
            //creer une nouvelle instance s il n en existe pas deja une autre
            if (_theUniqueInstance == null)
                return _theUniqueInstance = new Standard();
            else
                return _theUniqueInstance;
        }

        public static void KillSingleton()
        {
            _theUniqueInstance = null;	// TODO: Beurk !
        }

        public static IConnectionManager GetInstance(string pLogin, string pPassword, string pServer, string pDatabase, string pTimeout)
        {
            //creer une nouvelle instance s il n en existe pas deja une autre
            if (_theUniqueInstance == null)
                return _theUniqueInstance = new Standard(pLogin, pPassword, pServer, pDatabase, pTimeout);
            else
                return _theUniqueInstance;
        }

        public static IConnectionManager GetInstance(string testDB)
        {
            //creer une nouvelle instance s il n en existe pas deja une autre
            if (_theUniqueInstance == null)
                return _theUniqueInstance = new Standard(testDB);
            else
                return _theUniqueInstance;
        }

        public SqlConnection SqlConnection
        {
            get
            {
                if (_connection.State == ConnectionState.Closed)
                {
                    try
                    {
                        _connection.Open();
                    }
                    catch (SqlException sqlEx)
                    {
                        throw new ApplicationException("Unable to connect to database (" + _connection.DataSource + "/" + _connection.Database + "). Please contact your local IT administrator.", sqlEx);
                    }
                }
                return _connection;
            }
        }

        public SqlConnection SecondarySqlConnection
        {
            get
            {
                if (_secondaryConnection.State == ConnectionState.Closed)
                {
                    try
                    {
                        _secondaryConnection.Open();
                    }
                    catch (SqlException sqlEx)
                    {
                        throw new ApplicationException("Unable to connect to database (" + _secondaryConnection.DataSource + "/" + _secondaryConnection.Database + "). Please contact your local IT administrator.", sqlEx);
                    }
                }
                return _secondaryConnection;
            }
        }

        public SqlConnection AttachmentsSqlConnection
        {
            get
            {
                if (ConnectionState.Closed == _attachmentsConnection.State)
                {
                    try
                    {
                        _attachmentsConnection.Open();
                    }
                    catch
                    {
                    }
                }
                return _attachmentsConnection;
            }
        }


        public SqlTransaction GetSqlTransaction(string pMd5)
        {
            if (_connection.State == ConnectionState.Closed)
            {
                try
                {
                    _connection.Open();
                }
                catch (SqlException ex)
                {
                    throw new ApplicationException(
                        "Unable to connect to database (" + _connection.DataSource + "/" + _connection.Database +
                        "). Please contact your local IT administrator.", ex);
                }
            }
            else
            {
                Trace.WriteLine(Environment.StackTrace);
                sqlTransaction = _connection.BeginTransaction();
            }
            return sqlTransaction;
        }

        public void CloseConnection()
        {
            if (_connection.State != ConnectionState.Closed)
            {
                _connection.Close();
            }
        }

        public void CloseSecondaryConnection()
        {
            if (_secondaryConnection.State != ConnectionState.Closed)
                _secondaryConnection.Close();
        }
        /// <summary>
        /// Get a connection on the master database.<br/>
        /// (Used for maintenance database operations)
        /// </summary>
        public SqlConnection SqlConnectionOnMaster
        {
            get
            {
                return _connectionOnMasterDatabase;
            }
        }

        /// <summary>
        /// Get a connection on the master database with a long timeout.<br/>
        /// (Used for  database restore)
        /// </summary>
        public SqlConnection SqlConnectionForRestore
        {
            get
            {
                return new SqlConnection(_connectionStringForRestore);
            }
        }

        public void KillAllConnections()
        {
            String sql = @"DECLARE loop_name INSENSITIVE CURSOR FOR
                      SELECT spid
                       FROM master..sysprocesses
                       WHERE dbid = DB_ID('{0}')

                    OPEN loop_name
                    DECLARE @conn_id SMALLINT
                    DECLARE @exec_str VARCHAR(255)
                    FETCH NEXT FROM loop_name INTO @conn_id
                    WHILE (@@fetch_status = 0)
                      BEGIN
                        SELECT @exec_str = 'KILL ' + CONVERT(VARCHAR(7), @conn_id)
                        EXEC( @exec_str )
                        FETCH NEXT FROM loop_name INTO @conn_id
                      END
                    DEALLOCATE loop_name
                    ";
            _connectionOnMasterDatabase.Open();
            sql = String.Format(sql, TechnicalSettings.DatabaseName);
            SqlCommand cmd = new SqlCommand(sql, _connectionOnMasterDatabase);
            cmd.ExecuteNonQuery();
            _connectionOnMasterDatabase.Close();
        }

        /// <summary>
        /// Checks if database connection works.
        /// </summary>
        /// <returns></returns>
        //public bool CheckConnection()
        //{
        //    string sqlConnection = String.Format(@"user id={0};password={1};data source={2};persist security info=False;initial catalog=MASTER;connection timeout=10",
        //        TechnicalSettings.DatabaseLoginName, TechnicalSettings.DatabasePassword, TechnicalSettings.DatabaseServerName);
            
        //    SqlConnection connection = new SqlConnection(sqlConnection);
        //    try
        //    {    
        //        connection.Open();
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //    connection.Close();
        //    return true;
        //}

        #region IConnectionManager Members


        public void SetConnection(SqlConnection pConnection)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        public static bool CheckSQLServerConnection()
        {
            string sqlConnection = String.Format(@"user id={0};password={1};data source={2};persist security info=False;initial catalog=MASTER;connection timeout=10",
                TechnicalSettings.DatabaseLoginName, TechnicalSettings.DatabasePassword, TechnicalSettings.DatabaseServerName);
            SqlConnection connection = new SqlConnection(sqlConnection);
            try
            {
                connection.Open();
                connection.Close();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool CheckSQLDatabaseConnection()
        {
            string sqlConnection = String.Format(@"user id={0};password={1};data source={2};persist security info=False;initial catalog={3};connection timeout=10",
                TechnicalSettings.DatabaseLoginName, TechnicalSettings.DatabasePassword, TechnicalSettings.DatabaseServerName,TechnicalSettings.DatabaseName);
            SqlConnection connection = new SqlConnection(sqlConnection);
            try
            {
                connection.Open();
                connection.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static SqlConnection MasterConnection()
        {
            string sqlConnection = String.Format(@"user id={0};password={1};data source={2};persist security info=False;initial catalog=MASTER;connection timeout=10",
                TechnicalSettings.DatabaseLoginName, TechnicalSettings.DatabasePassword, TechnicalSettings.DatabaseServerName);
            return new SqlConnection(sqlConnection);
        }
    }
}
