// LICENSE PLACEHOLDER

using System;
using System.Data.SqlClient;
using System.Diagnostics;
using OpenCBS.CoreDomain;
using OpenCBS.DatabaseConnection;

namespace OpenCBS.Services
{
    public class BaseServices : MarshalByRefObject
    {
        protected T Run<T>(Func<SqlConnection, T> func)
        {
            SqlConnection connection = ConnectionManager.GeneralSqlConnection;
            try
            {
                connection.Open();
                return func(connection);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        protected T Run<T>(Func<SqlCommand, T> func)
        {
            SqlConnection connection = ConnectionManager.GeneralSqlConnection;
            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                return func(command);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        protected void RunAction(Action<SqlCommand> action)
        {
            SqlConnection connection = ConnectionManager.GeneralSqlConnection;
            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                action(command);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        protected void RunAction(Action<SqlTransaction> action, User user)
        {
            SqlTransaction sqlTransac = ConnectionManager.GetInstance().GetSqlTransaction(user.Md5);

            try
            {
                action(sqlTransac);
                sqlTransac.Commit();
            }
            catch (Exception)
            {
                sqlTransac.Rollback();
                throw;
            }
        }
    }

    [Security]
    public class Services : ContextBoundObject
    {
        private readonly ConnectionManager _connectionManager;
        private readonly User _user;
        
        protected Services(User pUser)
        {
            _user = pUser;
            _connectionManager = ConnectionManager.GetInstance();
        }

        protected Services()
        {
            _user = null;
            _connectionManager = null;
        }

        protected SqlTransaction DefaultSqlTransaction
        {
            get
            {
                return _connectionManager == null
                           ? null
                           : _connectionManager.GetSqlTransaction(_user.Md5);
            }
        }
    }
}
