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

using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using System.Xml.XPath;
using NUnit.Framework;
using System.Collections;
using OpenCBS.DatabaseConnection;
using OpenCBS.Manager;
using OpenCBS.Manager.Accounting;
using OpenCBS.Manager.Clients;
using OpenCBS.Manager.Contracts;
using OpenCBS.Manager.Currencies;
using OpenCBS.Manager.Events;
using System.IO;
using OpenCBS.Manager.Products;
using OpenCBS.Shared.Settings;
using OpenCBS.Services;

namespace OpenCBS.Test.Manager
{
    [TestFixture]
    public abstract class BaseManagerTest
    {
        protected Hashtable container;

        [TestFixtureSetUp]
        public void InitManager()
        {
            TechnicalSettings.CheckSettings();

            LoanProductManager loanProductManager = new LoanProductManager(DataUtil.TESTDB);
            AccountingTransactionManager accountingTransactionManager = new AccountingTransactionManager(DataUtil.TESTDB);
            EventManager eventManager = new EventManager(DataUtil.TESTDB); 
            ExchangeRateManager exchangeRateManager = new ExchangeRateManager(DataUtil.TESTDB);
            ProvisioningRuleManager provisioningRuleManager = new ProvisioningRuleManager(DataUtil.TESTDB);
            AccountManager accountManager = new AccountManager(DataUtil.TESTDB);
            InstallmentTypeManager installmentTypeManager = new InstallmentTypeManager(DataUtil.TESTDB);
            UserManager userManager = new UserManager(DataUtil.TESTDB);
            EconomicActivityManager economicActivityManager = new EconomicActivityManager(DataUtil.TESTDB);
            InstallmentManager installmentManager = new InstallmentManager(DataUtil.TESTDB);
            FundingLineManager fundingLineManager = new FundingLineManager(DataUtil.TESTDB);
            ClientManager clientManager = new ClientManager(DataUtil.TESTDB);
            LocationsManager locationsManager = new LocationsManager(DataUtil.TESTDB);
            LoanManager loanManager = new LoanManager(DataUtil.TESTDB);
            ProjectManager projectManager = new ProjectManager(DataUtil.TESTDB);
			MFIManager mfiManager = new MFIManager(DataUtil.TESTDB);
			SavingManager savingManager = new SavingManager(DataUtil.TESTDB);
			SavingProductManager savingProductManager = new SavingProductManager(DataUtil.TESTDB);
			SavingEventManager savingEventManager = new SavingEventManager(DataUtil.TESTDB);
            CurrencyManager currencyManager = new CurrencyManager(DataUtil.TESTDB);
            AccountingRuleManager accountingRuleManager = new AccountingRuleManager(DataUtil.TESTDB);
            FundingLineServices fundingLineServices = new FundingLineServices(DataUtil.TESTDB);
            
            container = new Hashtable
                            {
                                {"LoanProductManager", loanProductManager},
                                {"AccountingTransactionManager", accountingTransactionManager},
                                {"EventManager", eventManager},
                                {"ExchangeRateManager", exchangeRateManager},
                                {"ProvisioningRuleManager", provisioningRuleManager},
                                {"AccountManager", accountManager},
                                {"InstallmentTypeManager", installmentTypeManager},
                                {"UserManager", userManager},
                                {"FundingLineManager", fundingLineManager},
                                {"LoanManager", loanManager},
                                {"ClientManager", clientManager},
                                {"LocationsManager", locationsManager},
                                {"ProjectManager", projectManager},
                                {"EconomicActivityManager", economicActivityManager},
                                {"InstallmentManager", installmentManager},
                                {"MFIManager", mfiManager},
                                {"SavingManager", savingManager},
                                {"SavingProductManager", savingProductManager},
                                {"SavingEventManager", savingEventManager},
                                {"CurrencyManager", currencyManager},
                                {"FundingLineServices", fundingLineServices},
                                {"AccountingRuleManager", accountingRuleManager}
                            };
        }

        [TestFixtureTearDown]
        public void DisposeManager()
        {
            container = null;
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
            
            if (stream == null) return;

            StreamReader streamReader = new StreamReader(stream);
            string stringSql = streamReader.ReadToEnd();
            ConnectionManager connectionManager = ConnectionManager.GetInstance(DataUtil.TESTDB);
            using (SqlCommand insert = new SqlCommand(stringSql, connectionManager.SqlConnection))
            {
                insert.ExecuteNonQuery();
            }

            //List<string> queries = new List<string> { string.Format("USE [{0}]", DataUtil.TESTDB) };
            //queries.AddRange(_ParseSqlFile(_GetSqlObjects()));

            //foreach (string query in queries)
            //{
            //    SqlCommand command = new SqlCommand(query, connectionManager.SqlConnection) { CommandTimeout = 480 };
            //    command.ExecuteNonQuery();
            //}
        }

        private static string GetObjectLoadScript()
        {
            Assembly a = Assembly.GetExecutingAssembly();
            string path = Path.Combine(a.Location, "Objects.xml");
            Stream stream = a.GetManifestResourceStream("OpenCBS.Test._Sql.Objects.xml");
            XPathDocument xmldoc = new XPathDocument(stream);
            XPathNavigator nav = xmldoc.CreateNavigator();
            XPathExpression expr = nav.Compile("/database/object");
            expr.AddSort("@priority", XmlSortOrder.Ascending, XmlCaseOrder.None, null, XmlDataType.Number);
            XPathNodeIterator iterator = nav.Select(expr);

            string retval = string.Empty;
            while (iterator.MoveNext())
            {
                XPathNavigator create = iterator.Current.SelectSingleNode("create");
                XPathNavigator drop = iterator.Current.SelectSingleNode("drop");
                retval += string.Format("{0}\r\nGO\r\n\r\n{1}\r\nGO\r\n\r\n", drop.Value, create.Value);
            }
            return retval;
        }

        private static string _GetSqlObjects()
        {
            string retval = Path.GetTempFileName();
            StreamWriter writer = new StreamWriter(retval);
            writer.Write(GetObjectLoadScript());
            writer.Close();
            return retval;
        }

        private static List<string> _ParseSqlFile(string pScriptFile)
        {
            List<string> queries = new List<string>();
            Encoding encoding = Encoding.GetEncoding("utf-8");
            FileStream fs = new FileStream(pScriptFile, FileMode.Open);
            using (StreamReader reader = new StreamReader(fs, encoding))
            {
                // Parse file and get individual queries (separated by GO)
                while (!reader.EndOfStream)
                {
                    StringBuilder sb = new StringBuilder();
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        if (line.StartsWith("GO")) break;
                        if ((!line.StartsWith("/*")) && (line.Length > 0))
                        {
                            sb.Append(line);
                            sb.Append("\n");
                        }
                    }
                    string q = sb.ToString();
                    if ((!q.StartsWith("SET QUOTED_IDENTIFIER"))
                        && (!q.StartsWith("SET ANSI_"))
                        && (q.Length > 0))
                    {
                        queries.Add(q);
                    }
                }
            }
            return queries;
        }
    }
}
