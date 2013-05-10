// LICENSE PLACEHOLDER

using System.Data.SqlClient;
using OpenCBS.Shared.Settings;

namespace OpenCBS.CoreDomain
{
    public class DatabaseConnection
    {
        public static bool IsProductionDatabase = true;

        public static SqlConnection GetConnection()
        {
            SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder();
            csb.UserID = TechnicalSettings.DatabaseLoginName;
            csb.Password = TechnicalSettings.DatabasePassword;
            csb.DataSource = TechnicalSettings.DatabaseServerName;
            csb.PersistSecurityInfo = false;
            csb.InitialCatalog = IsProductionDatabase ? TechnicalSettings.DatabaseName : "opencbs_test";
            csb.ConnectTimeout = TechnicalSettings.DatabaseTimeout;

            SqlConnection conn = new SqlConnection(csb.ConnectionString);
            conn.Open();
            return conn;
        }
    }
}
