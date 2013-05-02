// LICENSE PLACEHOLDER

using System.Collections.Generic;
using System.Data.SqlClient;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Accounting;

namespace OpenCBS.Manager.Accounting
{
    public class ProvisioningRuleManager : Manager
    {
        public ProvisioningRuleManager(string pTestDB) : base(pTestDB){}
        public ProvisioningRuleManager(User pUser) : base(pUser){}


        public void AddProvisioningRate(ProvisioningRate pR, SqlTransaction sqlTransac)
        {
            const string sqlText = @"INSERT INTO ProvisioningRules(
                                        id,
                                        number_of_days_min, 
                                        number_of_days_max, 
                                        provisioning_value)
                                    VALUES(
                                      @number,
                                      @numberOfDaysMin, 
                                      @numberOfDaysMax, 
                                      @provisioningPercentage) 
                                   SELECT SCOPE_IDENTITY()";

            using (OctopusCommand insert = new OctopusCommand(sqlText, sqlTransac.Connection, sqlTransac))
            {
                SetProvisioningRate(insert, pR);
                insert.ExecuteScalar();
            }
        }

        private static void SetProvisioningRate(OctopusCommand octCommand, ProvisioningRate pProvisionningRate)
        {
            octCommand.AddParam("@number", pProvisionningRate.Number);
            octCommand.AddParam("@numberOfDaysMin", pProvisionningRate.NbOfDaysMin);
            octCommand.AddParam("@numberOfDaysMax", pProvisionningRate.NbOfDaysMax);
            octCommand.AddParam("@provisioningPercentage", pProvisionningRate.Rate);
        }

        ///// <summary>
        ///// This method Fill the instance of the ProvisioningTable object accessed by singleton
        ///// </summary>
        public List<ProvisioningRate> SelectAllProvisioningRates()
        {
            List<ProvisioningRate> list = new List<ProvisioningRate>();

            const string sqlText = @"SELECT 
                                       id,
                                       number_of_days_min, 
                                       number_of_days_max, 
                                       provisioning_value 
                                     FROM ProvisioningRules";
            using (SqlConnection conn = GetConnection())
            {
                using (OctopusCommand select = new OctopusCommand(sqlText, conn))
                {
                    using (OctopusReader reader = select.ExecuteReader())
                    {
                        if (reader.Empty) return list;
                        while (reader.Read())
                        {
                            list.Add(GetProvisionningRate(reader));
                        }
                        return list;
                    }
                }
            }
        }

        private static ProvisioningRate GetProvisionningRate(OctopusReader reader)
        {
            return new ProvisioningRate
                       {
                           Number = reader.GetInt("id"),
                           NbOfDaysMin = reader.GetInt("number_of_days_min"),
                           NbOfDaysMax = reader.GetInt("number_of_days_max"),
                           Rate = reader.GetDouble("provisioning_value")
                       };
        }

        public void DeleteAllProvisioningRules(SqlTransaction sqlTransac)
        {
            const string sqlText = "DELETE FROM ProvisioningRules";
            using (OctopusCommand delete = new OctopusCommand(sqlText, sqlTransac.Connection, sqlTransac))
            {
                delete.ExecuteNonQuery();
            }
        }
    }
}
