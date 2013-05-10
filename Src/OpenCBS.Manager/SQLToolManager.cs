// LICENSE PLACEHOLDER

using OpenCBS.CoreDomain;
using System.Data.SqlClient;

namespace OpenCBS.Manager
{
    public class SQLToolManager : Manager
    {
        public SQLToolManager(User pUser): base(pUser){}
        public SQLToolManager(string pDbConnectionString): base(pDbConnectionString){}

        public void RunSQL(string sqlText, SqlTransaction tran)
        {
            using (OpenCbsCommand sqlCommand = new OpenCbsCommand(sqlText, tran.Connection, tran))
            {
                sqlCommand.ExecuteNonQuery();
            }
        }
    }
}
