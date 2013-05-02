// LICENSE PLACEHOLDER

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

