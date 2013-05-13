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
using OpenCBS.CoreDomain.Events.Teller;
using OpenCBS.Manager.Accounting;
using OpenCBS.Manager.Currencies;
using OpenCBS.Shared;

namespace OpenCBS.Manager
{
    public class TellerManager : Manager
    {
        private readonly AccountManager accountManager;
        private readonly BranchManager branchManager;
        private readonly UserManager userManager;
        private readonly CurrencyManager currencyManager;
        
        public TellerManager(User user) : base(user)
        {
            accountManager = new AccountManager(user);
            branchManager = new BranchManager(user);
            userManager = new UserManager(user);
            currencyManager = new CurrencyManager(user);
        }

        public TellerManager(string pTestDb) : base(pTestDb)
        {
            accountManager = new AccountManager(pTestDb);
            branchManager = new BranchManager(pTestDb);
            userManager = new UserManager(pTestDb);
            currencyManager = new CurrencyManager(pTestDb);
        }

        public Teller SelectTellerOfUser(int userId)
        {
            var teller = new Teller();
            const string q = @"SELECT id
                                    , name
                                    , [desc]
                                    , account_id
                                    , deleted
                                    , branch_id
                                    , user_id
                                    , currency_id
                                    , amount_min
                                    , amount_max
                                    , deposit_amount_min
                                    , deposit_amount_max
                                    , withdrawal_amount_min
                                    , withdrawal_amount_max
                                    FROM dbo.Tellers
                                    WHERE user_id = @user_id AND deleted = 0";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                c.AddParam("@user_id", userId);
                using (OpenCbsReader r = c.ExecuteReader())
                {
                    if (r.Empty) return teller;

                    while (r.Read())
                    {
                        teller.Id = r.GetInt("id");
                        teller.Name = r.GetString("name");
                        teller.Description = r.GetString("desc");
                        teller.Deleted = r.GetBool("deleted");
                        teller.Account = accountManager.Select(r.GetInt("account_id"));
                        teller.Branch = branchManager.Select(r.GetInt("branch_id"));
                        int uId = r.GetInt("user_id");
                        teller.User = uId == 0 ? new User {Id = 0} : userManager.SelectUser(uId, false);
                        teller.Currency = currencyManager.SelectCurrencyById(r.GetInt("currency_id"));
                        teller.MinAmountTeller = r.GetMoney("amount_min");
                        teller.MaxAmountTeller = r.GetMoney("amount_max");
                        teller.MinAmountDeposit = r.GetMoney("deposit_amount_min");
                        teller.MaxAmountDeposit = r.GetMoney("deposit_amount_max");
                        teller.MinAmountWithdrawal = r.GetMoney("withdrawal_amount_min");
                        teller.MaxAmountWithdrawal = r.GetMoney("withdrawal_amount_max");
                    }
                }
            }
            return teller;
        }

        public Teller SelectVault(int branchId)
        {
            var teller = new Teller();
            const string q = @"SELECT id
                                    , name
                                    , [desc]
                                    , account_id
                                    , deleted
                                    , branch_id
                                    , currency_id
                                    FROM dbo.Tellers
                                    WHERE branch_id = @branch_id AND deleted = 0 AND user_id = 0";

            using (var conn = GetConnection())
            using (var c = new OpenCbsCommand(q, conn))
            {
                c.AddParam("@branch_id", branchId);
                using (OpenCbsReader r = c.ExecuteReader())
                {
                    r.Read();
                    if (r.Empty) return null;
                    teller.Id = r.GetInt("id");
                    teller.Name = r.GetString("name");
                    teller.Description = r.GetString("desc");
                    teller.Deleted = r.GetBool("deleted");
                    teller.Account = accountManager.Select(r.GetInt("account_id"));
                    teller.Branch = branchManager.Select(r.GetInt("branch_id"));
                    teller.Currency = currencyManager.SelectCurrencyById(r.GetInt("currency_id"));
                }
            }
            return teller;
        }

        public OCurrency SumPositiveEvent()
        {
            return new OCurrency();
        }

        public OCurrency SumNegativeEvent()
        {
            return new OCurrency();
        }

        public List<Teller> SelectAll()
        {
            var tellers = new List<Teller>();
            const string q = @"SELECT id
                                    , name
                                    , [desc]
                                    , account_id
                                    , deleted
                                    , branch_id
                                    , user_id
                                    , currency_id
                                    , amount_min
                                    , amount_max
                                    , deposit_amount_min
                                    , deposit_amount_max
                                    , withdrawal_amount_min
                                    , withdrawal_amount_max
                                    FROM dbo.Tellers";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            using (OpenCbsReader r = c.ExecuteReader())
            {
                if (r.Empty) return tellers;

                while (r.Read())
                {
                    var teller = new Teller();
                    teller.Id = r.GetInt("id");
                    teller.Name = r.GetString("name");
                    teller.Description = r.GetString("desc");
                    teller.Deleted = r.GetBool("deleted");
                    teller.Account = accountManager.Select(r.GetInt("account_id"));
                    teller.Branch = branchManager.Select(r.GetInt("branch_id"));
                    int uId = r.GetInt("user_id");
                    teller.User = uId == 0 ? new User { Id = 0 } : userManager.SelectUser(uId, false);
                    teller.Currency = currencyManager.SelectCurrencyById(r.GetInt("currency_id"));
                    teller.MinAmountTeller = r.GetMoney("amount_min");
                    teller.MaxAmountTeller = r.GetMoney("amount_max");
                    teller.MinAmountDeposit = r.GetMoney("deposit_amount_min");
                    teller.MaxAmountDeposit = r.GetMoney("deposit_amount_max");
                    teller.MinAmountWithdrawal = r.GetMoney("withdrawal_amount_min");
                    teller.MaxAmountWithdrawal = r.GetMoney("withdrawal_amount_max");
                    tellers.Add(teller);
                }
            }
            return tellers;
        }

        public List<Teller> SelectAllOfUser(int userId)
        {
            var tellers = new List<Teller>();
            const string q = @"SELECT id
                                    , name
                                    , [desc]
                                    , account_id
                                    , deleted
                                    , branch_id
                                    , user_id
                                    , currency_id
                                    , amount_min
                                    , amount_max
                                    , deposit_amount_min
                                    , deposit_amount_max
                                    , withdrawal_amount_min
                                    , withdrawal_amount_max
                                    FROM dbo.Tellers
                                    WHERE user_id = @user_id AND deleted = 0";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                c.AddParam("@user_id", userId);
                using (OpenCbsReader r = c.ExecuteReader())
                {
                    if (r.Empty) return tellers;

                    while (r.Read())
                    {
                        var teller = new Teller();
                        teller.Id = r.GetInt("id");
                        teller.Name = r.GetString("name");
                        teller.Description = r.GetString("desc");
                        teller.Deleted = r.GetBool("deleted");
                        teller.Account = accountManager.Select(r.GetInt("account_id"));
                        teller.Branch = branchManager.Select(r.GetInt("branch_id"));
                        int uId = r.GetInt("user_id");
                        teller.User = uId == 0 ? new User { Id = 0 } : userManager.SelectUser(uId, false);
                        teller.Currency = currencyManager.SelectCurrencyById(r.GetInt("currency_id"));
                        teller.MinAmountTeller = r.GetMoney("amount_min");
                        teller.MaxAmountTeller = r.GetMoney("amount_max");
                        teller.MinAmountDeposit = r.GetMoney("deposit_amount_min");
                        teller.MaxAmountDeposit = r.GetMoney("deposit_amount_max");
                        teller.MinAmountWithdrawal = r.GetMoney("withdrawal_amount_min");
                        teller.MaxAmountWithdrawal = r.GetMoney("withdrawal_amount_max");
                        tellers.Add(teller);
                    }
                }
            }
            return tellers;
        }

        public Teller Select(int tellerId)
        {
            var teller = new Teller();
            const string q = @"SELECT id
                                    , name
                                    , [desc]
                                    , account_id
                                    , deleted
                                    , branch_id
                                    , user_id
                                    , currency_id
                                    , amount_min
                                    , amount_max
                                    , deposit_amount_min
                                    , deposit_amount_max
                                    , withdrawal_amount_min
                                    , withdrawal_amount_max
                                    FROM dbo.Tellers
                                    WHERE id = @id";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                c.AddParam("@id", tellerId);
                using (OpenCbsReader r = c.ExecuteReader())
                {
                    if (r.Empty) return null;

                    teller.Id = r.GetInt("id");
                    teller.Name = r.GetString("name");
                    teller.Description = r.GetString("desc");
                    teller.Deleted = r.GetBool("deleted");
                    teller.Account = accountManager.Select(r.GetInt("account_id"));
                    teller.Branch = branchManager.Select(r.GetInt("branch_id"));
                    int uId = r.GetInt("user_id");
                    teller.User = uId == 0 ? new User { Id = 0 } : userManager.SelectUser(uId, false);
                    teller.Currency = currencyManager.SelectCurrencyById(r.GetInt("currency_id"));
                    teller.MinAmountTeller = r.GetMoney("amount_min");
                    teller.MaxAmountTeller = r.GetMoney("amount_max");
                    teller.MinAmountDeposit = r.GetMoney("deposit_amount_min");
                    teller.MaxAmountDeposit = r.GetMoney("deposit_amount_max");
                    teller.MinAmountWithdrawal = r.GetMoney("withdrawal_amount_min");
                    teller.MaxAmountWithdrawal = r.GetMoney("withdrawal_amount_max");
                }
            }
            return teller;
        }

        public Teller Add(Teller teller, SqlTransaction t)
        {
            const string sqlText =
                @"INSERT INTO dbo.Tellers (name, [desc], account_id, branch_id, user_id, currency_id,
                                            amount_min, amount_max, deposit_amount_min, deposit_amount_max, withdrawal_amount_min, withdrawal_amount_max) 
                                   VALUES (@name, @desc, @account_id, @branch_id, @user_id, @currency_id,
                                            @amount_min, @amount_max, @deposit_amount_min, @deposit_amount_max, @withdrawal_amount_min, @withdrawal_amount_max) 
                                            SELECT SCOPE_IDENTITY()";
            using (var c = new OpenCbsCommand(sqlText, t.Connection, t))
            {

                c.AddParam("@name", teller.Name);
                c.AddParam("@desc", teller.Description);

                if (teller.Branch != null)
                    c.AddParam("@branch_id", teller.Branch.Id);
                else
                    c.AddParam("@branch_id", null);

                if (teller.Account != null)
                    c.AddParam("@account_id", teller.Account.Id);
                else
                    c.AddParam("@account_id", null);

                if (teller.User != null)
                    c.AddParam("@user_id", teller.User.Id);
                else
                    c.AddParam("@user_id", null);

                if (teller.Currency != null)
                    c.AddParam("@currency_id", teller.Currency.Id);
                else
                    c.AddParam("@currency_id", null);

                c.AddParam("@amount_min", teller.MinAmountTeller);
                c.AddParam("@amount_max", teller.MaxAmountTeller);
                c.AddParam("@deposit_amount_min", teller.MinAmountDeposit);
                c.AddParam("@deposit_amount_max", teller.MaxAmountDeposit);
                c.AddParam("@withdrawal_amount_min", teller.MinAmountWithdrawal);
                c.AddParam("@withdrawal_amount_max", teller.MaxAmountWithdrawal);

                teller.Id = Convert.ToInt32(c.ExecuteScalar());
            }
            return teller;
        }

        public void Update(Teller teller, SqlTransaction t)
        {
            const string sqlText = @"UPDATE dbo.Tellers 
                                              SET name = @name, 
                                                 [desc] = @desc, 
                                                 account_id = @account_id,
                                                 branch_id = @branch_id,
                                                 user_id = @user_id,
                                                 currency_id = @currency_id,
                                                 amount_min = @amount_min,
                                                 amount_max = @amount_max,
                                                 deposit_amount_min = @deposit_amount_min,
                                                 deposit_amount_max = @deposit_amount_max,
                                                 withdrawal_amount_min = @withdrawal_amount_min,
                                                 withdrawal_amount_max = @withdrawal_amount_max
                                              WHERE id = @id";
            using (var c = new OpenCbsCommand(sqlText, t.Connection, t))
            {
                c.AddParam("@id", teller.Id);
                c.AddParam("@name", teller.Name);
                c.AddParam("@desc", teller.Description);

                if (teller.Branch != null)
                    c.AddParam("@branch_id", teller.Branch.Id);
                else
                    c.AddParam("@branch_id", null);

                if (teller.Account != null)
                    c.AddParam("@account_id", teller.Account.Id);
                else
                    c.AddParam("@account_id", null);

                if (teller.User != null)
                    c.AddParam("@user_id", teller.User.Id);
                else
                    c.AddParam("@user_id", null);

                if (teller.Currency != null)
                    c.AddParam("@currency_id", teller.Currency.Id);
                else
                    c.AddParam("@currency_id", null);

                c.AddParam("@amount_min", teller.MinAmountTeller);
                c.AddParam("@amount_max", teller.MaxAmountTeller);
                c.AddParam("@deposit_amount_min", teller.MinAmountDeposit);
                c.AddParam("@deposit_amount_max", teller.MaxAmountDeposit);
                c.AddParam("@withdrawal_amount_min", teller.MinAmountWithdrawal);
                c.AddParam("@withdrawal_amount_max", teller.MaxAmountWithdrawal);

                c.ExecuteNonQuery();
            }
        }

        public void Delete(int? id)
        {
            const string q = @"UPDATE dbo.Tellers SET deleted = 1                                                           
                                        WHERE id = @id";
            using (SqlConnection conn = GetConnection())
            using (var c = new OpenCbsCommand(q, conn))
            {
                c.AddParam("@id", id);
                c.ExecuteNonQuery();
            }
        }

        public void DeleteAll(SqlTransaction transaction)
        {
            const string q = @"DELETE FROM dbo.Tellers";
            using (var c = new OpenCbsCommand(q, transaction.Connection, transaction))
            {
                c.ExecuteNonQuery();
            }
        }

        public OCurrency GetTellerBalance(Teller teller)
        {
            using (SqlConnection conn = GetConnection())
            {
                using (var c = new OpenCbsCommand("GetTellerBalance", conn).AsStoredProcedure())
                {
                    c.AddParam("@teller_id", teller.Id);
                    return decimal.Parse(c.ExecuteScalar().ToString());
                }
            }
        }

        public OCurrency GetLatestTellerOpenBalance(Teller teller)
        {
            const string sql =
                                @"SELECT TOP 1 [amount]
                                FROM [TellerEvents]
                                WHERE  [teller_id] = @teller_id
                                   AND [event_code] = 'ODAE'
                                ORDER BY id DESC";
            using (SqlConnection connection = GetConnection())
            {
                using (OpenCbsCommand cmd = new OpenCbsCommand(sql, connection))
                {
                    cmd.AddParam("@teller_id", teller.Id);
                    object balance = cmd.ExecuteScalar();
                    if (balance == null)
                        return 0;
                    return decimal.Parse(balance.ToString());
                }
            }
        }
    }
}
