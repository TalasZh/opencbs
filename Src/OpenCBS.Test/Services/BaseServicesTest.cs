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

using System.Collections;
using System.Data.SqlClient;
using System.Reflection;
using NUnit.Framework;
using OpenCBS.DatabaseConnection;
using OpenCBS.Enums;
using OpenCBS.Manager;
using OpenCBS.Manager.Clients;
using OpenCBS.Services;
using System.IO;
using OpenCBS.Shared.Settings;

namespace OpenCBS.Test.Services
{
    public abstract class BaseServicesTest
    {
        protected Hashtable container = null;

        [TestFixtureSetUp]
        public void InitManager()
        {
            ApplicationSettings.GetInstance("").DeleteAllParameters();
            ApplicationSettings.GetInstance("").AddParameter(OGeneralSettings.CALCULATIONLATEFEESDURINGPUBLICHOLIDAYS, true);
            ApplicationSettings.GetInstance("").AddParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Cash);
            TechnicalSettings.UseOnlineMode = false;
            //Create Manager
            FundingLineManager fundingLineManager = new FundingLineManager(DataUtil.TESTDB);
            ClientManager clientManagement = new ClientManager(DataUtil.TESTDB);
            ConnectionManager connectionManager = ConnectionManager.GetInstance(DataUtil.TESTDB);
            //Create Service
            EconomicActivityServices economicActivityServices = new EconomicActivityServices(DataUtil.TESTDB);
            ClientServices clientService = new ClientServices(clientManagement);
            FundingLineServices fundingLineService = new FundingLineServices(fundingLineManager, clientManagement);
            ProjectServices projectService = new ProjectServices(DataUtil.TESTDB);

            container = new Hashtable
                            {
                                {"EconomicActivityServices", economicActivityServices},
                                {"ClientServices", clientService},
                                {"FundingLineServices", fundingLineService},
                                {"ProjectServices", projectService},
                                {"ConnectionManager", connectionManager}
                            };
        }

        [SetUp]
        protected virtual void SetUp()
        {
            _InitScript();
        }

        [TearDown]
        protected virtual void Dispose()
        {

        }

        private static void _InitScript()
        {
            Assembly a = Assembly.GetExecutingAssembly();
            Stream stream = a.GetManifestResourceStream("OpenCBS.Test._Sql.Init.sql");

            if (stream == null)    return;
            StreamReader streamReader = new StreamReader(stream);
            string stringSQL = streamReader.ReadToEnd();
            ConnectionManager connectionManager = ConnectionManager.GetInstance();
            SqlCommand insert = new SqlCommand(stringSQL, connectionManager.SqlConnection);
            insert.ExecuteNonQuery();
        }
    }
}

