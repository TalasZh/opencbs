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
using System.Text;
using Octopus.Manager;
using Octopus.CoreDomain;

namespace Octopus.Services
{
    public class SQLToolServices : MarshalByRefObject
    {
        private readonly SQLToolManager sqlToolManager;
        private readonly User _user;

        public SQLToolServices(User pUser)
        {
            _user = pUser;
            sqlToolManager = new SQLToolManager(pUser);
        }

        //For Fitness test only
        public SQLToolServices(string pLogin, string pPassword, string pServer, string pDatabase, string pSetupPath, string pTimeout)
        {
           
        }

        public string RunSQL(string sqlText)
        {
            SqlTransaction transac = DatabaseConnection.ConnectionManager.GetInstance().GetSqlTransaction(_user.Md5);
            
            try
            {
                sqlToolManager.RunSQL(sqlText, transac);
                transac.Commit();
            }
            catch (Exception ex)
            {
                transac.Rollback();
                return ex.Message.ToString();
            }

            return "ok";
        }
    }
}
