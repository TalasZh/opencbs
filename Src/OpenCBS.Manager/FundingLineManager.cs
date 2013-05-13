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
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.CoreDomain.FundingLines;
using OpenCBS.Enums;
using OpenCBS.Manager.Events;
using OpenCBS.Shared;
using FundingLine = OpenCBS.CoreDomain.FundingLines.FundingLine;

namespace OpenCBS.Manager
{
    public class FundingLineManager : Manager, IFundingLineLazyLoader
    {
        #region Constructeur

        private FundingLineEventManager _eventFundingLineManager;

        public FundingLineManager(User pUser) : base(pUser)
        {
            _eventFundingLineManager = new FundingLineEventManager(pUser);
        }

        public FundingLineManager(string testDB) : base(testDB)
        {
            _eventFundingLineManager = new FundingLineEventManager(testDB);
        }

        #endregion

        #region IFundingLineLazyLoader

        public OCurrency GetAmount(FundingLine fl)
        {
            return GetAmountImpl(fl, OFundingLineEventTypes.Entry, true);
        }

        public OCurrency GetRealAmount(FundingLine fl)
        {
            return GetAmountImpl(fl, OFundingLineEventTypes.Commitment, false);
        }

        public OCurrency GetAnticipatedAmount(FundingLine fl)
        {
            return GetAmountImpl(fl, OFundingLineEventTypes.Disbursment, false);
        }

        public List<FundingLineEvent> GetEvents(FundingLine fl)
        {
            List<FundingLineEvent> list = new List<FundingLineEvent>();
            const string q = @"SELECT id,
                                    code,
                                    amount,
                                    direction, 
                                    deleted,
                                    creation_date,
                                    type 
                               FROM dbo.FundingLineEvents
                               WITH (READUNCOMMITTED)
                               WHERE fundingline_id = @id
                               ORDER BY creation_date DESC, id DESC";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                c.AddParam("@id", fl.Id);
                using (OpenCbsReader r = c.ExecuteReader())
                {
                    if (null == r || r.Empty) return list;
                    while (r.Read())
                    {
                        FundingLineEvent e = new FundingLineEvent();
                        e.Id = r.GetInt("id");
                        e.Code = r.GetString("code");
                        e.Amount = r.GetMoney("amount");
                        e.Movement = (OBookingDirections) r.GetSmallInt("direction");
                        e.IsDeleted = r.GetBool("deleted");
                        e.CreationDate = r.GetDateTime("creation_date");
                        e.Type = (OFundingLineEventTypes) r.GetSmallInt("type");
                        e.FundingLine = fl;
                        list.Add(e);
                    }
                }
            }
            return list;
        }

        private OCurrency GetAmountImpl(FundingLine fl, OFundingLineEventTypes type, bool equals)
        {
            string q = @"SELECT ISNULL(SUM(
                CASE 
                    WHEN 1 = direction THEN CAST(amount AS DECIMAL(20,4))
                    ELSE -1*CAST(amount  AS DECIMAL(20,4))
                END), 0) amount
            FROM dbo.FundingLineEvents
            WITH (READUNCOMMITTED)
            WHERE fundingline_id = @id
            AND deleted = 0
            AND CAST(FLOOR(CAST(creation_date AS FLOAT)) AS DATETIME) <= @date
            AND type ";
            q += equals ? "= @type" : "<> @type";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                c.AddParam("@id", fl.Id);
                c.AddParam("@date", TimeProvider.Now.Date);
                c.AddParam("@type", (int) type);
                object retval = c.ExecuteScalar();
                return null == retval ? 0m : Convert.ToDecimal(retval);
            }
        }

        private void SetLazyLoader(FundingLine fl)
        {
            if (null == fl) return;
            fl.SetLazyLoader(this);
        }

        #endregion IFundingLineLazyLoader

        public FundingLine SelectFundingLineById(int id, SqlTransaction sqlTrans)
        {
            FundingLine fundingLine = null;

            string q =
                @"SELECT FundingLines.[id],
                         FundingLines.[name], 
                         FundingLines.[deleted],
                         FundingLines.[amount],
                         FundingLines.[begin_date],
                         FundingLines.[end_date],
                         FundingLines.[purpose], 
                         FundingLines.[currency_id],
                         Currencies.[name] as currency_name, 
                         Currencies.[code] as currency_code,
                         Currencies.[is_pivot] as currency_is_pivot,
                         Currencies.[is_swapped] as currency_is_swapped
                        FROM [FundingLines] 
                        LEFT JOIN Currencies ON FundingLines.currency_id = Currencies.id
                        WHERE FundingLines.[id]=@id AND [deleted]=0";
            using (OpenCbsCommand c = new OpenCbsCommand(q, sqlTrans.Connection, sqlTrans))
            {
                c.AddParam("@id", id);

                using (OpenCbsReader r = c.ExecuteReader())
                {
                    if (!r.Empty)
                    {
                        r.Read();
                        fundingLine = new FundingLine();
                        fundingLine.Id = r.GetInt("id");
                        fundingLine.Name = r.GetString("name");
                        fundingLine.Deleted = r.GetBool("deleted");
                        fundingLine.StartDate = r.GetDateTime("begin_date");
                        fundingLine.Purpose = r.GetString("purpose");
                        fundingLine.EndDate = r.GetDateTime("end_date");
                        fundingLine.Amount = r.GetMoney("amount");
                        fundingLine.Currency = new Currency
                                                   {
                                                       Id = r.GetInt("currency_id"),
                                                       Name = r.GetString("currency_name"),
                                                       Code = r.GetString("currency_code"),
                                                       IsPivot = r.GetBool("currency_is_pivot"),
                                                       IsSwapped = r.GetBool("currency_is_swapped")
                                                   };
                    }
                }
                SetLazyLoader(fundingLine);
            }
            return fundingLine;
        }

        public FundingLine SelectFundingLineByName(string name)
        {
            FundingLine fundingLine = null;

            string q =
                @"SELECT FundingLines.[id],
                         FundingLines.[name], 
                         FundingLines.[deleted],
                         FundingLines.[amount],
                         FundingLines.[begin_date],
                         FundingLines.[end_date],
                         FundingLines.[purpose], 
                         FundingLines.[currency_id],
                         Currencies.[name] as currency_name, 
                         Currencies.[code] as currency_code, 
                         Currencies.[is_pivot] as currency_is_pivot,
                         Currencies.[is_swapped] as currency_is_swapped
                        FROM [FundingLines] 
                        LEFT JOIN Currencies ON FundingLines.currency_id = Currencies.id
                        WHERE FundingLines.[name]=@name AND [deleted]=0";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                c.AddParam("@name", name);

                using (OpenCbsReader r = c.ExecuteReader())
                {
                    if (r != null)
                    {
                        if (!r.Empty)
                        {
                            r.Read();
                            fundingLine = new FundingLine();
                            fundingLine.Id = r.GetInt("id");
                            fundingLine.Name = r.GetString("name");
                            fundingLine.Deleted = r.GetBool("deleted");
                            fundingLine.StartDate = r.GetDateTime("begin_date");
                            fundingLine.Purpose = r.GetString("purpose");
                            fundingLine.EndDate = r.GetDateTime("end_date");
                            fundingLine.Amount = r.GetMoney("amount");
                            fundingLine.Currency = new Currency
                                                       {
                                                           Id = r.GetInt("currency_id"),
                                                           Name = r.GetString("currency_name"),
                                                           Code = r.GetString("currency_code"),
                                                           IsPivot = r.GetBool("currency_is_pivot"),
                                                           IsSwapped = r.GetBool("currency_is_swapped")
                                                       };
                        }
                    }
                }
            }
            SetLazyLoader(fundingLine);
            
            return fundingLine;
        }

        public FundingLine SelectFundingLineById(int id, bool pAddOptionalEventInformations)
        {
            FundingLine fundingLine = null;

            string q =
                @"SELECT 
                                FundingLines.[id],
                                FundingLines.[name], 
                                FundingLines.[deleted],
                                FundingLines.[amount],FundingLines.[begin_date],FundingLines.[end_date],
                                FundingLines.[purpose], FundingLines.[currency_id],
                                Currencies.[name] as currency_name, 
                                Currencies.[code] as currency_code, 
                                Currencies.[is_pivot] as currency_is_pivot,
                                Currencies.[is_swapped] as currency_is_swapped
                              FROM [FundingLines] 
                              LEFT JOIN Currencies ON FundingLines.currency_id = Currencies.id
                              WHERE FundingLines.[id]=@id AND [deleted]=0";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                c.AddParam("@id", id);

                using (OpenCbsReader r = c.ExecuteReader())
                {
                    if (r != null)
                    {
                        if (!r.Empty)
                        {
                            r.Read();
                            fundingLine = new FundingLine();
                            fundingLine.Id = r.GetInt("id");
                            fundingLine.Name = r.GetString("name");
                            fundingLine.Deleted = r.GetBool("deleted");
                            fundingLine.StartDate = r.GetDateTime("begin_date");
                            fundingLine.Purpose = r.GetString("purpose");
                            fundingLine.EndDate = r.GetDateTime("end_date");
                            fundingLine.Amount = r.GetMoney("amount");
                            fundingLine.Currency = new Currency
                                                       {
                                                           Id = r.GetInt("currency_id"),
                                                           Name = r.GetString("currency_name"),
                                                           Code = r.GetString("currency_code"),
                                                           IsPivot = r.GetBool("currency_is_pivot"),
                                                           IsSwapped = r.GetBool("currency_is_swapped")
                                                       };
                        }
                    }
                }
            }
            SetLazyLoader(fundingLine);

            return fundingLine;
        }

        public FundingLine SelectFundingLineByNameAndPurpose(FundingLine lookupFundingLine, SqlTransaction sqlTransac,
                                                             bool includeAll)
        {
            FundingLine newFL = new FundingLine();

            string q =
                @"SELECT FundingLines.[id],
                         [deleted],
                         [currency_id],
                         Currencies.[name] as currency_name,
                         Currencies.[code] as currency_code, 
                         Currencies.[is_pivot] as currency_pivot,
                         Currencies.[is_swapped] as currency_is_swapped
                FROM [FundingLines] 
                LEFT JOIN Currencies ON FundingLines.currency_id = Currencies.id
                WHERE [purpose] = @purpose AND
                FundingLines.[name] = @name";
            if (!includeAll) q += "and [deleted]=@deleted";
            using (OpenCbsCommand c = new OpenCbsCommand(q, sqlTransac.Connection, sqlTransac))
            {
                c.AddParam("@purpose", lookupFundingLine.Purpose);
                c.AddParam("@name", lookupFundingLine.Name);

                if (!includeAll) c.AddParam("@deleted", lookupFundingLine.Deleted);

                using (OpenCbsReader r = c.ExecuteReader())
                {
                    if (r != null)
                    {
                        if (!r.Empty)
                        {
                            r.Read();

                            newFL.Id = r.GetInt("id");
                            newFL.Deleted = r.GetBool("deleted");
                            newFL.Currency = new Currency
                                                 {
                                                     Id = r.GetInt("currency_id"),
                                                     Name = r.GetString("currency_name"),
                                                     Code = r.GetString("currency_code"),
                                                     IsPivot = r.GetBool("currency_is_pivot"),
                                                     IsSwapped = r.GetBool("currency_is_swapped")
                                                 };
                        }
                    }
                }
            }
            SetLazyLoader(newFL);

            return newFL;
        }

        public int SelectFundingLineEventId(FundingLineEvent lookupFundingLineEvent)
        {
            using (SqlConnection conn = GetConnection())
            using (SqlTransaction t = conn.BeginTransaction())
            {
                try
                {
                    int retval = SelectFundingLineEventId(lookupFundingLineEvent, t);
                    t.Commit();
                    return retval;
                }
                catch (Exception)
                {
                    t.Rollback();
                    throw;
                }
            }
        }

        public int SelectFundingLineEventId(FundingLineEvent lookupFundingLineEvent, SqlTransaction sqlTransac)
        {
            return _eventFundingLineManager.SelectFundingLineEventId(lookupFundingLineEvent, sqlTransac, false);
        }

        public List<FundingLineEvent> SelectFundingLineEvents(FundingLine fl)
        {
            return _eventFundingLineManager.SelectFundingLineEvents(fl);
        }

        public List<FundingLine> SelectFundingLines()
        {
            List<FundingLine> list = new List<FundingLine>();
            string q =
                @"SELECT FundingLines.[id],
                         FundingLines.[name],
                         [begin_date],
                         [end_date],
                         [amount],
                         [purpose], 
                         [deleted], 
                         [currency_id],
                         Currencies.[name] as currency_name, 
                         Currencies.[code] as currency_code,
                         Currencies.[is_pivot] as currency_is_pivot,
                         Currencies.[is_swapped] as currency_is_swapped      
                FROM [FundingLines] 
                LEFT JOIN Currencies ON FundingLines.currency_id = Currencies.id
                WHERE [deleted] = 0";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            using (OpenCbsReader r = c.ExecuteReader())
            {
                while (r.Read())
                {
                    FundingLine fundingLine = new FundingLine();
                    fundingLine.Id = r.GetInt("id");
                    fundingLine.Name = r.GetString("name");
                    fundingLine.StartDate = r.GetDateTime("begin_date");
                    fundingLine.EndDate = r.GetDateTime("end_date");
                    fundingLine.Amount = r.GetMoney("amount");
                    fundingLine.Amount = r.GetMoney("amount");
                    fundingLine.Purpose = r.GetString("purpose");
                    fundingLine.Deleted = r.GetBool("deleted");
                    fundingLine.Currency = new Currency
                                               {
                                                   Id = r.GetInt("currency_id"),
                                                   Name = r.GetString("currency_name"),
                                                   Code = r.GetString("currency_code"),
                                                   IsPivot = r.GetBool("currency_is_pivot"),
                                                   IsSwapped = r.GetBool("currency_is_swapped")
                                               };
                    SetLazyLoader(fundingLine);
                    list.Add(fundingLine);
                }
            }

            return list;
        }

        public int AddFundingLine(FundingLine pFund)
        {
            using (SqlConnection conn = GetConnection())
            using (SqlTransaction t = conn.BeginTransaction())
            {
                try
                {
                    int id = AddFundingLine(pFund, t);
                    t.Commit();
                    return id;
                }
                catch (Exception)
                {
                    t.Rollback();
                    throw;
                }
            }
        }

        public int AddFundingLine(FundingLine pFund, SqlTransaction sqlTransac)
        {
            string q =
                @"INSERT INTO [FundingLines]([name],[begin_date],[end_date],[amount],[purpose], [deleted], [currency_id]) 
                    VALUES(@name,@beginDate,@endDate,@amount,@purpose,@deleted, @currency_id ) SELECT SCOPE_IDENTITY()";

            using (OpenCbsCommand c = new OpenCbsCommand(q, sqlTransac.Connection, sqlTransac))
            {
                c.AddParam("@name", pFund.Name);
                c.AddParam("@beginDate", pFund.StartDate);
                c.AddParam("@endDate", pFund.EndDate);
                c.AddParam("@amount", pFund.Amount);
                c.AddParam("@purpose", pFund.Purpose);
                c.AddParam("@deleted", pFund.Deleted);
                c.AddParam("@currency_id", pFund.Currency.Id);
                pFund.Id = int.Parse(c.ExecuteScalar().ToString());


                foreach (FundingLineEvent eventFL in pFund.Events)
                {
                    if (eventFL.Id == 0)
                        _eventFundingLineManager.AddFundingLineEvent(eventFL, sqlTransac); //new event
                    else
                        _eventFundingLineManager.UpdateFundingLineEvent(eventFL, sqlTransac); // delete event
                }
            }
            SetLazyLoader(pFund);
            return pFund.Id;
        }

        public void UpdateFundingLine(FundingLine fund)
        {
            using (SqlConnection conn = GetConnection())
            using (SqlTransaction t = conn.BeginTransaction())
            {
                try
                {
                    UpdateFundingLine(fund, t);
                    t.Commit();
                }
                catch (Exception)
                {
                    t.Rollback();
                    throw;
                }
            }
        }

        public void UpdateFundingLine(FundingLine fund, SqlTransaction pSqlTransac)
        {
            string q =
                @"UPDATE [FundingLines] 
                        SET [name] = @name, 
                        [begin_date] = @beginDate,
                        [end_date]=@endDate,
                        [amount] = @amount,
                        [purpose] = @purpose, 
                        [deleted]=@deleted, 
                        [currency_id] = @currency_id
                        WHERE [id] = @id";

            using (OpenCbsCommand c = new OpenCbsCommand(q, pSqlTransac.Connection, pSqlTransac))
            {
                c.AddParam("@id", fund.Id);
                c.AddParam("@name", fund.Name);
                c.AddParam("@beginDate", fund.StartDate);
                c.AddParam("@purpose", fund.Purpose);
                c.AddParam("@endDate", fund.EndDate);
                c.AddParam("@amount", fund.Amount);
                c.AddParam("@deleted", fund.Deleted);
                c.AddParam("@currency_id", fund.Currency.Id);
                c.ExecuteNonQuery();


                foreach (FundingLineEvent eventFL in fund.Events)
                {
                    if (eventFL.Id == 0)
                        _eventFundingLineManager.AddFundingLineEvent(eventFL, pSqlTransac); //new event
                    else
                        _eventFundingLineManager.UpdateFundingLineEvent(eventFL, pSqlTransac); // delete event
                }
            }
        }

        public int AddFundingLineEvent(FundingLineEvent newFundingLineEvent, SqlTransaction sqlTrans)
        {
            return _eventFundingLineManager.AddFundingLineEvent(newFundingLineEvent, sqlTrans); //new event
        }

        public void UpdateFundingLineEvent(FundingLineEvent newFundingLineEvent, SqlTransaction sqlTrans)
        {
            _eventFundingLineManager.UpdateFundingLineEvent(newFundingLineEvent, sqlTrans); //new event
        }

        public void DeleteFundingLineEvent(FundingLineEvent newFundingLineEvent)
        {
            using (SqlConnection conn = GetConnection())
            using (SqlTransaction t = conn.BeginTransaction())
            {
                try
                {
                    DeleteFundingLineEvent(newFundingLineEvent, t);
                    t.Commit();
                }
                catch (Exception)
                {
                    t.Rollback();
                    throw;
                }
            }
        }

        public void DeleteFundingLineEvent(FundingLineEvent newFundingLineEvent, SqlTransaction sqlTrans)
        {
            _eventFundingLineManager.DeleteFundingLineEvent(newFundingLineEvent, sqlTrans);
        }

        public void DeleteFundingLine(FundingLine fund)
        {
            string q = "UPDATE [FundingLines] SET [deleted] = @deleted WHERE [id] = @id";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                c.AddParam("@id", fund.Id);
                c.AddParam("@deleted", true);
                c.ExecuteScalar();
            }
        }
    }
}
