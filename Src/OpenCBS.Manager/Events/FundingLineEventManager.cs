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

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.FundingLines;
using OpenCBS.Enums;
using OpenCBS.Shared;

namespace OpenCBS.Manager.Events
{
    internal class FundingLineEventManager : Manager
    {
        private User _user;

        public FundingLineEventManager(User pUser) : base(pUser)
        {
            _user = pUser;
        }

        public FundingLineEventManager(string pTestDb) : base(pTestDb)
        {
        }


        /// <summary>
        /// Select all events for selected Funding Line
        /// </summary>
        /// <param name="fundingLine">funding line </param>
        /// <returns>list of Funding Line events</returns>
        public List<FundingLineEvent> SelectFundingLineEvents(FundingLine fundingLine)
        {
            List<FundingLineEvent> list = new List<FundingLineEvent>();

            const string sqlText =
                @"SELECT 
                        [id],
                        [code],
                        [amount],
                        [direction],
                        [fundingline_id],
                        [deleted],
                        [creation_date],
                        [type] 
                  FROM [FundingLineEvents] 
                  WHERE fundingline_id = @fundingline_id 
                  ORDER BY creation_date DESC, id DESC";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand cmd = new OpenCbsCommand(sqlText, conn))
            {
                cmd.AddParam("@fundingline_id", fundingLine.Id);

                using (OpenCbsReader reader = cmd.ExecuteReader())
                {
                    if (reader == null || reader.Empty) return list;
                    {
                        while (reader.Read())
                        {
                            FundingLineEvent fundingLineEvent = new FundingLineEvent
                                                                    {
                                                                        Id = reader.GetInt("id"),
                                                                        Code = reader.GetString("code"),
                                                                        Amount = reader.GetMoney("amount"),
                                                                        Movement =
                                                                            ((OBookingDirections)
                                                                             reader.GetSmallInt("direction")),
                                                                        IsDeleted =
                                                                            reader.GetBool("deleted"),
                                                                        CreationDate =
                                                                            reader.GetDateTime("creation_date"),
                                                                        Type =
                                                                            ((OFundingLineEventTypes)
                                                                             reader.GetSmallInt("type")),
                                                                        FundingLine = fundingLine
                                                                    };
                            list.Add(fundingLineEvent);
                        }
                    }
                }
                return list;
            }
        }

        public int SelectFundingLineEventId(FundingLineEvent pFundingLineEvent, SqlTransaction sqlTransac,
                                            bool includeDeleted)
        {
            int id = -1;

            string sqlText =
                        @"SELECT [id] 
                          FROM [FundingLineEvents] 
                          WHERE [code] = @code 
                                 AND [amount] = @amount
                                 AND [direction] = @direction
                                 AND [type] = @type
                                 AND [fundingline_id] = @fundinglineid";
            if (!includeDeleted) sqlText += " and deleted = @deleted";

            OpenCbsCommand cmd = new OpenCbsCommand(sqlText, sqlTransac.Connection, sqlTransac);
            cmd.AddParam("@code", pFundingLineEvent.Code);
            cmd.AddParam("@amount", pFundingLineEvent.Amount);
            cmd.AddParam("@direction", (int)pFundingLineEvent.Movement);
            cmd.AddParam("@type",  (int)pFundingLineEvent.Type);
            cmd.AddParam("@fundinglineid", pFundingLineEvent.FundingLine.Id);
            if (!includeDeleted) cmd.AddParam("@deleted", pFundingLineEvent.IsDeleted);

            using (OpenCbsReader reader = cmd.ExecuteReader())
            {
                if (reader != null)
                {
                    if (!reader.Empty)
                    {
                        reader.Read();
                        id = reader.GetInt("id");
                    }
                }
            }
            return id;
        }


        public void DeleteFundingLineEvent(FundingLineEvent pFundingLineEvent, SqlTransaction sqlTransac)
        {
            const string sqlText = @"
                                UPDATE [FundingLineEvents] 
                                SET [deleted]=1 
                                WHERE [id] = @id";
            using (OpenCbsCommand c = new OpenCbsCommand(sqlText, sqlTransac.Connection, sqlTransac))
            {
                c.AddParam("@id", pFundingLineEvent.Id);
                c.AddParam("@deleted", true);
                c.ExecuteScalar();
            }
        }

        public int AddFundingLineEvent(FundingLineEvent pFundingLineEvent, SqlTransaction pTransac)
        {
            const string sqlText =
                @"INSERT INTO [FundingLineEvents] 
                                ([code],
                                [amount],
                                [direction],
                                [fundingline_id],
                                [deleted],
                                [creation_date],
                                [type],
                                user_id,
                                contract_event_id) 
                VALUES 
                           (@code,
                            @amount,
                            @direction,
                            @fundingLineId, 
                            @deleted, 
                            @creationDate,
                            @type,
                            @user_id, 
                            @contract_event_id
                            ) 
                SELECT SCOPE_IDENTITY()";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand cmd = pTransac == null
                                    ? new OpenCbsCommand(sqlText, conn)
                                    : new OpenCbsCommand(sqlText, pTransac.Connection, pTransac))
            {
                cmd.AddParam("@code", pFundingLineEvent.Code);
                cmd.AddParam("@amount", pFundingLineEvent.Amount);
                cmd.AddParam("@direction", (int)pFundingLineEvent.Movement);
                cmd.AddParam("@fundingLineId", pFundingLineEvent.FundingLine.Id);
                // pFundingLineId);
                cmd.AddParam("@deleted", false);
                cmd.AddParam("@creationDate", pFundingLineEvent.CreationDate);
                cmd.AddParam("@type", (int)pFundingLineEvent.Type);
                cmd.AddParam("@user_id", _user==null? (object)null:_user.Id);
                cmd.AddParam("contract_event_id",pFundingLineEvent.AttachTo==null ?
                                        (object)null: pFundingLineEvent.AttachTo.Id);
                pFundingLineEvent.Id = Convert.ToInt32(cmd.ExecuteScalar());

                return pFundingLineEvent.Id;
            } 
        }

        /* update funding line event method updates only IsDeleted property of the Funding Line event*/

        public void UpdateFundingLineEvent(FundingLineEvent pFundingLineEvent, SqlTransaction sqlTransac)
        {
            const string sqlText = @"
                                    UPDATE [FundingLineEvents] 
                                    SET [deleted] = @deleted 
                                    WHERE [id] = @id";

            using (OpenCbsCommand c = new OpenCbsCommand(sqlText, sqlTransac.Connection, sqlTransac))
            {
                c.AddParam("@id", pFundingLineEvent.Id);
                c.AddParam("@deleted", pFundingLineEvent.IsDeleted);
                c.ExecuteNonQuery();
            }
        }
    }
}
