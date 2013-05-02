// LICENSE PLACEHOLDER

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using OpenCBS.CoreDomain.Online;
using OpenCBS.Shared.Settings;
using OpenCBS.CoreDomain;
using System.IO;
using OpenCBS.Shared.Settings.Remote;
using OpenCBS.Shared;

namespace OpenCBS.DatabaseConnection
{
    [Serializable]
    public class Remoting : IConnectionManager
    {
        private bool _connectionInitSuceeded;
        private SqlConnection _secondarySqlConnection;
        private SqlConnection _connection;
        private SqlConnection sqlConnectionForRestore;
        private SqlTransaction sqlTransaction;
        private static Remoting _theUniqueInstance;


        static private IDictionary account_table = new Hashtable();
        static public IDictionary GetAccountTable()
        { return account_table; }

        private SqlConnection _getConnectionByMd5(string pMd5)
        {
            if (account_table.Contains(pMd5))
            {
                UserRemotingContext userContext = (UserRemotingContext)account_table[pMd5];
                userContext.Token.dcr_timeout();

                if (userContext.Connection.State != ConnectionState.Open)
                    userContext.Connection.Open();

                return userContext.Connection;
            }
            return null;
        }

        private SqlConnection _getSecondaryConnectionByMd5(string pMd5)
        {
            if (account_table.Contains(pMd5))
            {
                UserRemotingContext userContext = (UserRemotingContext)account_table[pMd5];
                userContext.Token.dcr_timeout();

                if (userContext.SecondaryConnection.State != ConnectionState.Open)
                    userContext.SecondaryConnection.Open();

                return userContext.SecondaryConnection;
            }
            return null; 
        }

        public void SuppressRemotingInfos(string pMd5, string pComputerName, string pLoginName)
        {
            if (account_table.Contains(pMd5))
            {
                Log.RemotingServiceUsersLogger.InfoFormat(@"[DECONNECTION {2}\{3}]: Users, branch name:{0} login:{1}, disconnected", ((UserRemotingContext)account_table[pMd5]).Token.Account, ((UserRemotingContext)account_table[pMd5]).Token.Login, pComputerName, pLoginName);
                Log.RemotingServiceLogger.InfoFormat(@"[DECONNECTION {2}\{3}]: Users, branch name:{0} login:{1}, disconnected", ((UserRemotingContext)account_table[pMd5]).Token.Account, ((UserRemotingContext)account_table[pMd5]).Token.Login, pComputerName, pLoginName);
                account_table.Remove(pMd5);
            }
        }


        public void RunTimeout()
        {
            Log.RemotingServiceLogger.Warn("Call Of RUN_TIMEOUT");
            Timer timer = new Timer();
            timer.create_timer();
        }

        public string GetAuthentification(string pOctoLogin, string pOctoPass, string pOctoAccount, string pComputerName, string pLoginName)
        {
            Log.RemotingServiceLogger.DebugFormat("Call of get_authentification with login : {0}, pass : {1}, account : {2}", pOctoLogin, pOctoPass, pOctoAccount);
            Log.RemotingServiceLogger.DebugFormat(" login : {0}, pass : {1}, server : {2}", RemoteServerSettings.GetSettings().LoginName, RemoteServerSettings.GetSettings().Password, RemoteServerSettings.GetSettings().ServerName);

            string md5String = "";
            Token token = _getTokenByAccountName(pOctoAccount);
            UserRemotingContext connectionManager = new UserRemotingContext();

            if (_checkAccount(token, pOctoLogin, pOctoPass) == false)
            {
                // Throw exeption
                // FIXME
                Log.RemotingServiceLogger.Error("Les User/Pass octo donn�e ne sont pas valide");
                throw new Exception("messageBoxUserPasswordIncorrect.Text");
            }

            // Generate the md5
            DateTime date = DateTime.Now;

            string ToHash = token.get_unique_string();
            ToHash += "|" + date.Minute;

            byte[] data = new byte[ToHash.Length];
            Encoder enc = Encoding.ASCII.GetEncoder();
            enc.GetBytes(ToHash.ToCharArray(), 0, ToHash.Length, data, 0, true);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(data);
            md5String = BitConverter.ToString(result).Replace("-", "").ToLower();


            string connectionString = "user id=" + token.Login + ";password=" + token.Pass + ";server=" +
                                      RemoteServerSettings.GetSettings().ServerName + ";initial catalog=" + token.Account;
            Log.RemotingServiceLogger.DebugFormat("Connection String {0}", connectionString);
            SqlConnection connection = new SqlConnection(connectionString);
            SqlConnection secondaryConnection = new SqlConnection(connectionString);
            
            connectionManager.Token = token;
            connectionManager.Connection = connection;
            connectionManager.SecondaryConnection = secondaryConnection;

            if (account_table.Contains(md5String) == false)
            {
                Log.RemotingServiceLogger.DebugFormat("The account Dictionary doesnt contain this md5 : {0}", md5String);
                account_table[md5String] = connectionManager;
            }

            // Set the connection in the connectionManager
            Log.RemotingServiceLogger.DebugFormat("ConnectionString: {0}", connection.ConnectionString);
            this.SetConnection(connection);
            //OpenCBS.DatabaseConnection.ConnectionManager.GetInstance().SetConnection(connection);

            Log.RemotingServiceUsersLogger.InfoFormat(@"[CONNECTION {3}\{4}] Users, branch name:{0} login:{1}, connected \n \t Connection string :{2}", pOctoAccount, pOctoLogin, connectionString, pComputerName, pLoginName);
            Log.RemotingServiceLogger.InfoFormat(@"[CONNECTION {3}\{4}] Users, branch name:{0} login:{1}, connected \n \t Connection string :{2}", pOctoAccount, pOctoLogin, connectionString, pComputerName, pLoginName);

            return md5String;
        }


        SqlConnection _currentConnection = null;
        private SqlConnection _getAccountSqlconnection()
        {
            Log.RemotingServiceLogger.Debug("Call get_account_sqlconnection");

            if (_currentConnection == null)
            {
                _currentConnection = new SqlConnection();

                string chaine = "user id=" + RemoteServerSettings.GetSettings().LoginName + ";password=" +
                                RemoteServerSettings.GetSettings().Password + ";server=" +
                                RemoteServerSettings.GetSettings().ServerName + ";initial catalog=Accounts";
                _currentConnection.ConnectionString = chaine;
                _currentConnection.Open();
            }
            return _currentConnection;
        }

        // Get the token by passing him the accountName
        private Token _getTokenByAccountName(string pAccount)
        {
            Log.RemotingServiceLogger.DebugFormat("Call getTokenByAccountName with: {0}", pAccount);

            Token token = null;

            string sqlText = @"SELECT user_name, password, database_name, active
                               FROM SqlAccounts 
                               WHERE account_name=@account";

            try
            {
                SqlConnection connection = _getAccountSqlconnection();
                SqlCommand command = new SqlCommand(sqlText, connection);
                command.Parameters.Add("account", SqlDbType.NVarChar);
                command.Parameters["account"].Value = pAccount;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();

                        string oUser = reader.GetString(reader.GetOrdinal("user_name"));
                        string oPass = reader.GetString(reader.GetOrdinal("password"));
                        string oDbName = reader.GetString(reader.GetOrdinal("database_name"));
                        bool active = reader.GetBoolean(reader.GetOrdinal("active"));

                        if (!active)
                        {
                            Log.RemotingServiceLogger.ErrorFormat("Account {0} inactive", pAccount);
                            throw new Exception("AccountInactive.Text");
                        }

                        token = new Token(oUser, oPass, oDbName);
                    }
                    else
                    {
                        Log.RemotingServiceLogger.ErrorFormat("Account {0} incorrect", pAccount);
                        throw new Exception("AccountNameIncorrect.Text");
                    }
                }
            }
            catch (Exception e)
            {
                Log.RemotingServiceLogger.Error("Error during connection to the server", e);
                throw;
            }
            return token;
        }

        // Check the validity of the login/pass/account
        private bool _checkAccount(Token pSqlToken, string pOctoLogin, string pOctoPass)
        {
            Log.RemotingServiceLogger.Debug("Call CheckAccount");

            if (pSqlToken == null)
            {
                Log.RemotingServiceLogger.Debug("Call CheckAccount with pSqlToken null");
                return false;
            }

            Log.RemotingServiceLogger.DebugFormat("Call check_account user = {0} pass = {1} token = login {2}, pass {3}, account {4}", pOctoLogin, pOctoPass, pSqlToken.Login, pSqlToken.Pass, pSqlToken.Account);
            int valid = 0;
            string connection_string = "user id=" + pSqlToken.Login + ";password=" + pSqlToken.Pass + ";server=" + RemoteServerSettings.GetSettings().ServerName + ";initial catalog=" + pSqlToken.Account;
            SqlConnection connection = new SqlConnection(connection_string);

            if (connection.State != ConnectionState.Open)
                connection.Open();

            // CHECK if the pOctoLogin/pOctoPass is a valid octo Account
            string sqlText = "select * from dbo.Users where user_name =@username and user_pass =@password ";
            SqlCommand command = new SqlCommand(sqlText, connection);

            command.Parameters.Add("username", SqlDbType.NVarChar);
            command.Parameters["username"].Value = pOctoLogin;
            command.Parameters.Add("password", SqlDbType.NVarChar);
            command.Parameters["password"].Value = pOctoPass;
            try
            {
                valid = int.Parse(command.ExecuteScalar().ToString());
            }
            catch
            {
                return false;
            }
            return true;
        }


        public SqlConnection GetSqlConnection(string pMd5)
        {
            return _getConnectionByMd5(pMd5);
        }

        public SqlConnection GetSecondarySqlConnection(string pMd5)
        {
            return _getSecondaryConnectionByMd5(pMd5);
        }

        public void SetConnection(SqlConnection pConnection)
        {
            _connection = pConnection;
            _connection.Open();
        }

        public static Remoting GetInstance()
        {
            if (_theUniqueInstance == null)
                return _theUniqueInstance = new Remoting();
            return _theUniqueInstance;
        }

        private Remoting()
        {
            Console.WriteLine("Bon j'ai la connection");
        }

        public Remoting(string testDB)
        {
            //ApplicationSettings regParameters = ApplicationSettings.GetInstance();
            //InitConnections(regParameters.Login, regParameters.Password, regParameters.Server, testDB, regParameters.TimeOut);
        }

        public Remoting(string pLogin, string pPassword, string pServer, string pDatabase, string pTimeout)
        {
            //InitConnections(pLogin, pPassword, pServer, pDatabase, pTimeout);
        }

        private void InitConnections(string pLogin, string pPassword, string pServer, string pDatabase, string pTimeout)
        {
            try
            {
                _connection = new SqlConnection(
                    String.Format("user id={0};password={1};data source={2};persist security info=False;initial catalog={3};connection timeout={4}",
                                  pLogin, pPassword, pServer, pDatabase, pTimeout));
                _connection.Open();

                _secondarySqlConnection =  new SqlConnection(
                    String.Format("user id={0};password={1};data source={2};persist security info=False;initial catalog={3};connection timeout={4}",
                                  pLogin, pPassword, pServer, pDatabase, pTimeout));
                
                _secondarySqlConnection.Open();

                _connectionInitSuceeded = true;
            
                
            }
            catch (Exception)
            {
                _connectionInitSuceeded = false;
            }
        }

        
        public bool CheckConnection()
        {
            throw new NotImplementedException();
        }

        public void CloseConnection()
        {
            throw new NotImplementedException();
        }

        public void CloseSecondaryConnection()
        {
            throw new NotImplementedException();
        }

        public bool ConnectionInitSuceeded
        {
            get { return _connectionInitSuceeded; }
        }

        public void KillAllConnections()
        {
            throw new NotImplementedException();
        }

        public SqlConnection SecondarySqlConnection
        {
            get { return _secondarySqlConnection; }
        }

        public SqlConnection SqlConnection
        {
            get { return _connection; }
        }

        public SqlConnection SqlConnectionForRestore
        {
            get { return sqlConnectionForRestore; }
        }

        public SqlConnection SqlConnectionOnMaster
        {
            get { return null; }
        }

        public SqlConnection AttachmentsSqlConnection
        {
            get { return null; }
        }

        public static SqlConnection GetSqlConnectionOnMaster()
        {
                string sqlConnection = String.Format(@"user id={0};password={1};data source={2};persist security info=False;initial catalog={3};connection timeout=10",
                                        RemoteServerSettings.GetSettings().LoginName, RemoteServerSettings.GetSettings().Password, RemoteServerSettings.GetSettings().ServerName, "MASTER");
                return new SqlConnection(sqlConnection);
        }


        public SqlTransaction GetSqlTransaction(string pMd5)
        {
            _connection = _getConnectionByMd5(pMd5);
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
                    Log.RemotingServiceLogger.Error("Unable to connect to database (" + _connection.DataSource + "/" + _connection.Database +
                        "). Please contact your local IT administrator.", ex);
                }
            }
            else
            {
                try
                {
                    throw new ApplicationException("COUCOU");
                }
                catch (ApplicationException ex)
                {
                    System.Diagnostics.Trace.WriteLine(ex.StackTrace);
                }
                sqlTransaction = _connection.BeginTransaction();
            }
            return sqlTransaction;
        }

        public static bool CheckSQLServerConnection()
        {
            string sqlConnection = String.Format(@"user id={0};password={1};data source={2};persist security info=False;initial catalog={3};connection timeout=10",
                RemoteServerSettings.GetSettings().LoginName, RemoteServerSettings.GetSettings().Password, RemoteServerSettings.GetSettings().ServerName, "MASTER");

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

        public static bool CheckSQLDatabaseConnection()
        {

            string sqlConnection = String.Format(@"user id={0};password={1};data source={2};persist security info=False;initial catalog={3};connection timeout=10",
                RemoteServerSettings.GetSettings().LoginName, RemoteServerSettings.GetSettings().Password, RemoteServerSettings.GetSettings().ServerName, "Accounts");
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
    }
}
