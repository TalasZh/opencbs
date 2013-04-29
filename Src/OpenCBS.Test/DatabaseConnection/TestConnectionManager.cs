
using NUnit.Framework;
using Octopus.DatabaseConnection;
using Octopus.Shared.Settings;

namespace Octopus.Test.DatabaseConnection
{
    [TestFixture]
    public class TestConnectionManager
    {
        private string _serverName;
        private string _databaseName;
        private string _databaseLogin;
        private string _databasePassword;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            TechnicalSettings.CheckSettings();
            TechnicalSettings.UseOnlineMode = false;
            _serverName = TechnicalSettings.DatabaseServerName;
            _databaseName = TechnicalSettings.DatabaseName;
            _databaseLogin = TechnicalSettings.DatabaseLoginName;
            _databasePassword = TechnicalSettings.DatabasePassword;
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            TechnicalSettings.DatabaseServerName = _serverName;
            TechnicalSettings.DatabaseName = _databaseName;
            TechnicalSettings.DatabaseLoginName = _databaseLogin;
            TechnicalSettings.DatabasePassword = _databasePassword;
        }

        [SetUp]
        public void SetUp()
        {
            TechnicalSettings.DatabaseServerName = _serverName;
            TechnicalSettings.DatabaseName = _databaseName;
            TechnicalSettings.DatabaseLoginName = _databaseLogin;
            TechnicalSettings.DatabasePassword = _databasePassword;
        }

        [Test]
        public void CheckSQLServerConnection_ServerNameEmpty_ServerLoginEmpty_ServerPasswordEmpty_ServerDatabaseNameEmpty()
        {
            TechnicalSettings.DatabaseServerName = string.Empty;
            TechnicalSettings.DatabaseName = string.Empty;
            TechnicalSettings.DatabaseLoginName = string.Empty;
            TechnicalSettings.DatabasePassword = string.Empty;
            Assert.AreEqual(false, ConnectionManager.CheckSQLServerConnection());
        }

        [Test]
        public void CheckSQLServerConnection_ServerNameOK_ServerLoginEmpty_ServerPasswordEmpty_ServerDatabaseNameEmpty()
        {
            TechnicalSettings.DatabaseServerName = _serverName;
            TechnicalSettings.DatabaseName = string.Empty;
            TechnicalSettings.DatabaseLoginName = string.Empty;
            TechnicalSettings.DatabasePassword = string.Empty;
            Assert.AreEqual(false, ConnectionManager.CheckSQLServerConnection());
        }

        [Test]
        public void CheckSQLServerConnection_ServerNameOK_ServerLoginOK_ServerPasswordEmpty_ServerDatabaseNameEmpty()
        {
            TechnicalSettings.DatabaseServerName = _serverName;
            TechnicalSettings.DatabaseName = string.Empty;
            TechnicalSettings.DatabaseLoginName = _databaseLogin;
            TechnicalSettings.DatabasePassword = string.Empty;
            Assert.AreEqual(false, ConnectionManager.CheckSQLServerConnection());
        }

        [Test]
        public void CheckSQLServerConnection_ServerNameOK_ServerLoginOK_ServerPasswordOK_ServerDatabaseNameEmpty()
        {
            TechnicalSettings.DatabaseServerName = _serverName;
            TechnicalSettings.DatabaseName = string.Empty;
            TechnicalSettings.DatabaseLoginName = _databaseLogin;
            TechnicalSettings.DatabasePassword = _databasePassword;
            Assert.AreEqual(true, ConnectionManager.CheckSQLServerConnection());
        }
    }
}
