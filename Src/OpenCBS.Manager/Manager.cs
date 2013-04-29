//Octopus MFS is an integrated suite for managing a Micro Finance Institution: clients, contracts, accounting, reporting and risk
//Copyright © 2006,2007 OCTO Technology & OXUS Development Network
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
using System.Data.SqlClient;
using Octopus.CoreDomain;
using Octopus.DatabaseConnection;
using Octopus.Shared.Settings;


namespace Octopus.Manager
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
