// LICENSE PLACEHOLDER

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
            using (OctopusCommand cmd = new OctopusCommand(sqlText, conn))
            {
                cmd.AddParam("@fundingline_id", fundingLine.Id);

                using (OctopusReader reader = cmd.ExecuteReader())
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

            OctopusCommand cmd = new OctopusCommand(sqlText, sqlTransac.Connection, sqlTransac);
            cmd.AddParam("@code", pFundingLineEvent.Code);
            cmd.AddParam("@amount", pFundingLineEvent.Amount);
            cmd.AddParam("@direction", (int)pFundingLineEvent.Movement);
            cmd.AddParam("@type",  (int)pFundingLineEvent.Type);
            cmd.AddParam("@fundinglineid", pFundingLineEvent.FundingLine.Id);
            if (!includeDeleted) cmd.AddParam("@deleted", pFundingLineEvent.IsDeleted);

            using (OctopusReader reader = cmd.ExecuteReader())
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
            using (OctopusCommand c = new OctopusCommand(sqlText, sqlTransac.Connection, sqlTransac))
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
            using (OctopusCommand cmd = pTransac == null
                                    ? new OctopusCommand(sqlText, conn)
                                    : new OctopusCommand(sqlText, pTransac.Connection, pTransac))
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

            using (OctopusCommand c = new OctopusCommand(sqlText, sqlTransac.Connection, sqlTransac))
            {
                c.AddParam("@id", pFundingLineEvent.Id);
                c.AddParam("@deleted", pFundingLineEvent.IsDeleted);
                c.ExecuteNonQuery();
            }
        }
    }
}
