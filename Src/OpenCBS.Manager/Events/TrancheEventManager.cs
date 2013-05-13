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

using OpenCBS.CoreDomain.Events;
using System.Data.SqlClient;

namespace OpenCBS.Manager.Events
{
    public class TrancheEventManager : Manager
    {
        public TrancheEventManager(string testDB) : base(testDB)
        {
            
        }

        private static void SetTrancheEvent(OpenCbsCommand cmd, TrancheEvent trancheEvent)
        {
            cmd.AddParam("@Id",  trancheEvent.Id);
            cmd.AddParam("@InterestRate", trancheEvent.InterestRate);
            cmd.AddParam("@Amount", trancheEvent.Amount);
            cmd.AddParam("@Maturity", trancheEvent.Maturity);
            cmd.AddParam("@StartDate", trancheEvent.StartDate);
            cmd.AddParam("@applied_new_interest", trancheEvent.StartDate);
        }

        /// <summary>
        /// Method to add a TrancheEvent into database. We use the NullableTypes to make the correspondance between
        /// nullable int, decimal and double types in database and our own objects
        /// </summary>
        /// <param name="trancheEvent">TrancheEvent Object</param>
        /// <returns>The id of the Tranche Event which has been added</returns>
        public int Add(TrancheEvent trancheEvent)
        {
            const string sqlText = @"
                INSERT INTO [TrancheEvents]
                           ( [id]
                            ,[interest_rate]
                            ,[amount]
                            ,[maturity]
                            ,[start_date]
                            ,[applied_new_interest])
                            VALUES
                            (@Id,
                             @InterestRate,
                             @Amount,
                             @Maturity,
                             @StartDate, 
                             @applied_new_interest) 
                SELECT SCOPE_IDENTITY()";
            using (SqlConnection conn = GetConnection())
            using (var cmd = new OpenCbsCommand(sqlText, conn))
            {
                SetTrancheEvent(cmd, trancheEvent);
                return int.Parse(cmd.ExecuteScalar().ToString());
            }
        }

        public void Update(TrancheEvent trancheEvent)
        {
            string sqlText = @"
                            UPDATE [TrancheEvents] SET 
                            [interest_rate] = @InterestRate,
                            [amount] = @Amount,
                            [maturity] = @Maturity,
                            [start_date] = @StartDate,
                            [applied_new_interest] = @applied_new_interest
                            WHERE id = @Id";
            using (SqlConnection conn = GetConnection())
            using (var cmd = new OpenCbsCommand(sqlText, conn))
            {
                SetTrancheEvent(cmd, trancheEvent);
                cmd.ExecuteNonQuery();
            }

        }

    }
}
