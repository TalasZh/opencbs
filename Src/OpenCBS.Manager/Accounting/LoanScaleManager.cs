//Octopus MFS is an integrated suite for managing a Micro Finance Institution: clients, contracts, accounting, reporting and risk
//Copyright ?2006,2007 OCTO Technology & OXUS Development Network
//
//This program is free software; you can redistribute it and/or modify
//it under the terms of the GNU Lesser General Public License as published by
//the Free Software Foundation; either version 2 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU Lesser General Public License for more details.
//
//You should have received a copy of the GNU Lesser General Public License along
//with this program; if not, write to the Free Software Foundation, Inc.,
//51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
//
// Licence : http://www.octopusnetwork.org/OverviewLicence.aspx
//
// Website : http://www.octopusnetwork.org
// Business contact: business(at)octopusnetwork.org
// Technical contact email : tech(at)octopusnetwork.org 

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
