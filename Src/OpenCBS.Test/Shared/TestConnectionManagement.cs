// LICENSE PLACEHOLDER

using NUnit.Framework;
using OpenCBS.DatabaseConnection;
using OpenCBS.Shared;
using System.Data;

namespace OpenCBS.Test.Shared
{
	/// <summary>
	/// Summary description for TestConnectionManagement.
	/// </summary>
	[TestFixture]
	public class TestConnectionManagement
	{
		private ConnectionManager connectionManager;

		[Test]
		public void TestIfConnectionCorrectlyGetWhenConnectToOctopusTest()
		{
			connectionManager = ConnectionManager.GetInstance(DataUtil.TESTDB);
			Assert.AreEqual(ConnectionState.Open,connectionManager.SqlConnection.State);
			connectionManager.CloseConnection();
		}

		[Test]
			public void TestIfConenctionCorrectlyGetWhenConnectWithoutRegistry()
		{
			connectionManager = ConnectionManager.GetInstance("octopus_user", "octopus", "octopus", "octopus_test","30");
			Assert.AreEqual(ConnectionState.Open,connectionManager.SqlConnection.State);
			connectionManager.CloseConnection();
		}
	}
}
