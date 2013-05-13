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
