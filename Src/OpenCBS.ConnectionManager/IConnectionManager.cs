// LICENSE PLACEHOLDER

using System.Data.SqlClient;

namespace OpenCBS.DatabaseConnection
{
    public interface IConnectionManager
    {
        void CloseConnection();
        void CloseSecondaryConnection();
        bool ConnectionInitSuceeded { get; }
        void KillAllConnections();
        void SetConnection(SqlConnection pConnection);
        SqlConnection GetSqlConnection(string pM5);
        SqlConnection GetSecondarySqlConnection(string pM5);
        SqlTransaction GetSqlTransaction(string pMd5);

        SqlConnection SecondarySqlConnection { get; }
        SqlConnection SqlConnection { get; }
        SqlConnection SqlConnectionForRestore { get; }
        SqlConnection SqlConnectionOnMaster { get; }
        SqlConnection AttachmentsSqlConnection { get; }
    }
}
