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

using NUnit.Framework;
using OpenCBS.Services;
using OpenCBS.Shared.Settings;

namespace OpenCBS.Test.Services
{
    [TestFixture]
    public class TestDatabaseServices
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
            Assert.AreEqual(false, new DatabaseServices().CheckSQLServerConnection());
        }

        [Test]
        public void CheckSQLServerConnection_ServerNameOK_ServerLoginEmpty_ServerPasswordEmpty_ServerDatabaseNameEmpty()
        {
            TechnicalSettings.DatabaseServerName = _serverName;
            TechnicalSettings.DatabaseName = string.Empty;
            TechnicalSettings.DatabaseLoginName = string.Empty;
            TechnicalSettings.DatabasePassword = string.Empty;
            Assert.AreEqual(false, new DatabaseServices().CheckSQLServerConnection());
        }

        [Test]
        public void CheckSQLServerConnection_ServerNameOK_ServerLoginOK_ServerPasswordEmpty_ServerDatabaseNameEmpty()
        {
            TechnicalSettings.DatabaseServerName = _serverName;
            TechnicalSettings.DatabaseName = string.Empty;
            TechnicalSettings.DatabaseLoginName = _databaseLogin;
            TechnicalSettings.DatabasePassword = string.Empty;
            Assert.AreEqual(false, new DatabaseServices().CheckSQLServerConnection());
        }

        [Test]
        public void CheckSQLServerConnection_ServerNameOK_ServerLoginOK_ServerPasswordOK_ServerDatabaseNameEmpty()
        {
            TechnicalSettings.DatabaseServerName = _serverName;
            TechnicalSettings.DatabaseName = string.Empty;
            TechnicalSettings.DatabaseLoginName = _databaseLogin;
            TechnicalSettings.DatabasePassword = _databasePassword;
            Assert.AreEqual(true, new DatabaseServices().CheckSQLServerConnection());
        }
    }
}
