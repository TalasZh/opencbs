// LICENSE PLACEHOLDER

using System.Data.SqlClient;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Accounting;

namespace OpenCBS.Manager.Accounting
{
    public class LoanScaleManager : Manager
    {
        private readonly User _user = new User();
        public LoanScaleManager(string pTestDB) : base(pTestDB){}

        public LoanScaleManager(User pUser) : base(pUser)
        {
            _user = pUser;
        }

        public void InsertLoanScale(LoanScaleRate pLoanScaleRate, SqlTransaction pSqlTransaction)
        {
            const string sqlText = @"INSERT INTO LoanScale(id,ScaleMin, ScaleMax) VALUES(@number,@Min, @Max)";

            using (OctopusCommand insert = new OctopusCommand(sqlText, pSqlTransaction.Connection, pSqlTransaction))
            {
                SetLoanScale(insert, pLoanScaleRate);
                insert.ExecuteNonQuery();
            }
        }

        private static void SetLoanScale(OctopusCommand octCmd, LoanScaleRate pLoanScaleRate)
        {
            octCmd.AddParam("@number", pLoanScaleRate.Number);
            octCmd.AddParam("@Min", pLoanScaleRate.ScaleMin);
            octCmd.AddParam("@Max", pLoanScaleRate.ScaleMax);
        }

        public void SelectLoanScales()
        {
            LoanScaleTable loanscaleTable = LoanScaleTable.GetInstance(_user);
            const string sqlText = @"SELECT id, ScaleMin, ScaleMax 
                                     FROM LoanScale";
            
            using (SqlConnection conn = GetConnection())
            {
                using (OctopusCommand select = new OctopusCommand(sqlText, conn))
                {
                    using (OctopusReader reader = select.ExecuteReader())
                    {
                        if (reader.Empty) return;
                        while (reader.Read())
                        {
                            loanscaleTable.AddLoanScaleRate(GetLoanScale(reader));
                        }
                    }
                }
            }
        }

        private static LoanScaleRate GetLoanScale(OctopusReader pReader)
        {
            return new LoanScaleRate
                       {
                           Number = pReader.GetInt("id"),
                           ScaleMin = pReader.GetInt("ScaleMin"),
                           ScaleMax = pReader.GetInt("ScaleMax")
                       };
        }

        public void Delete(SqlTransaction pSqlTransaction)
        {
            const string sqltext = "DELETE FROM LoanScale";
            using (OctopusCommand delete = new OctopusCommand(sqltext, pSqlTransaction.Connection, pSqlTransaction))
            {
                delete.ExecuteNonQuery();
            }
        }
    }
}
