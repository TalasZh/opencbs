// LICENSE PLACEHOLDER

using System;
using System.Data.SqlClient;
using System.Text;
using OpenCBS.Manager;
using OpenCBS.CoreDomain;

namespace OpenCBS.Services
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
