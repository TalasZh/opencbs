//Octopus MFS is an integrated suite for managing a Micro Finance Institution: clients, contracts, accounting, reporting and risk
//Copyright © 2006,2007 OCTO Technology & OXUS Development Network
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

using System.Collections.Generic;
using System.Data.SqlClient;
using Octopus.CoreDomain;
using Octopus.CoreDomain.Accounting;

namespace Octopus.Manager.Accounting
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
