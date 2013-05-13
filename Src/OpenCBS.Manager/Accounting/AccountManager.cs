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
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using OpenCBS.CoreDomain;
using OpenCBS.Enums;
using OpenCBS.CoreDomain.Accounting;

namespace OpenCBS.Manager.Accounting
{
	/// <summary>
	/// AccountManagement contains all methods relative to selecting, inserting, updating
	/// and deleting account objects to and from our database.
	/// </summary>
	public class AccountManager : Manager
	{
        public AccountManager(User pUser) : base(pUser){}
		public AccountManager(string testDB) : base(testDB){}

        public Account Select(int pAccountId)
		{
            const string sqlText = @"SELECT 
                                       id,
                                       account_number,
                                       label,
                                       debit_plus, 
                                       type_code,
                                       account_category_id,
                                       type,
                                       parent_account_id, lft, rgt
                                     FROM [ChartOfAccounts] 
                                     WHERE id = @id";
            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand select = new OpenCbsCommand(sqlText, conn))
                {
                    select.AddParam("@id", pAccountId);
                    using (OpenCbsReader reader = select.ExecuteReader())
                    {
                        if (reader == null || reader.Empty) return null;

                        reader.Read();
                        return GetAccount(reader);
                    }
                }
            }
		}

        public Account SelectChartAccount(int pAccountId)
        {
            const string sqlText = @"SELECT id, 
                                       account_number,
                                       label,
                                       debit_plus, 
                                       type_code,                                                    
                                       account_category_id, 
                                       type,  
                                       parent_account_id, lft, rgt
                                     FROM ChartOfAccounts 
                                     WHERE id = @id";
            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand select = new OpenCbsCommand(sqlText, conn))
                {
                    select.AddParam("@id", pAccountId);
                    using (OpenCbsReader reader = select.ExecuteReader())
                    {
                        if (reader == null || reader.Empty) return null;

                        reader.Read();
                        return GetAccount(reader);
                    }
                }
            }
        }

        public List<Account> SelectAccountByCategory(AccountCategory accountCategory)
        {
            const string sqlText = @"SELECT id, 
                                       account_number,
                                       label,
                                       debit_plus, 
                                       type_code,                                                    
                                       account_category_id, 
                                       type,  
                                       parent_account_id, lft, rgt
                                     FROM ChartOfAccounts 
                                     WHERE account_category_id = @account_category_id";
            List<Account> accounts = new List<Account>();
            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand select = new OpenCbsCommand(sqlText, conn))
                {
                    select.AddParam("@account_category_id", accountCategory.Id);
                    using (OpenCbsReader reader = select.ExecuteReader())
                    {
                        if (reader == null || reader.Empty) return null;

                        reader.Read();
                        while (reader.Read())
                        {
                            accounts.Add(GetAccount(reader));
                        }
                        return accounts;
                    }
                }
            }
        }

        public List<Account> SelectAllAccounts()
        {
            List<Account> list = new List<Account>();

            const string sqlText = @"SELECT id, 
                                       account_number,
                                       label,
                                       debit_plus, 
                                       type_code,                                                    
                                       account_category_id, 
                                       type,  
                                       parent_account_id, lft, rgt
                                     FROM ChartOfAccounts
                                     ORDER BY account_number";

            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand select = new OpenCbsCommand(sqlText, conn))
                {
                    using (OpenCbsReader reader = select.ExecuteReader())
                    {
                        if (reader == null || reader.Empty) return list;
                        while (reader.Read())
                        {
                            list.Add(GetAccount(reader));
                        }
                    }
                    return list;
                }
            }
        }

        public List<Account> SelectAllAccountsWithoutTeller(int accountId)
        {
            List<Account> list = new List<Account>();

            const string sqlText = @"SELECT id, 
                                       account_number,
                                       label,
                                       debit_plus, 
                                       type_code,                                                    
                                       account_category_id, 
                                       type,  
                                       parent_account_id, lft, rgt
                                     FROM ChartOfAccounts
                                     WHERE id NOT IN (SELECT account_id FROM dbo.Tellers WHERE deleted = 0) OR id = @id   
                                     ORDER BY account_number";

            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand select = new OpenCbsCommand(sqlText, conn))
                {
                    select.AddParam("@id", accountId);
                    using (OpenCbsReader reader = select.ExecuteReader())
                    {
                        if (reader == null || reader.Empty) return list;
                        while (reader.Read())
                        {
                            list.Add(GetAccount(reader));
                        }
                    }
                    return list;
                }
            }
        }

        public DataSet GetAccountsDataset()
        {
            const string sqlText = @"SELECT 
                                       id,
                                       account_number,                                       
                                       label,
                                       debit_plus,
                                       type_code,                                                    
                                       account_category_id,
                                       type,
                                       ISNULL(parent_account_id, 0) AS parent_account_id,
                                       lft,
                                       rgt                                        
                                     FROM ChartOfAccounts";

            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand select = new SqlCommand(sqlText, conn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(select))
                    {
                        DataSet dataSet = new DataSet();
                        adapter.Fill(dataSet);
                        return dataSet;
                    }
                }
            }
        }

        public List<AccountCategory> SelectAccountCategories()
        {
            List<AccountCategory> list = new List<AccountCategory>();

            const string sqlText = @"SELECT id, name FROM AccountsCategory";
            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand select = new OpenCbsCommand(sqlText, conn))
                {
                    using (OpenCbsReader reader = select.ExecuteReader())
                    {
                        if (reader == null || reader.Empty) return list;
                        while (reader.Read())
                        {
                            list.Add(new AccountCategory
                                         {
                                             Id = reader.GetSmallInt("id"),
                                             Name = reader.GetString("name"),
                                         }
                                );
                        }
                        return list;
                    }
                }
            }
        }

        public AccountCategory SelectAccountCategoriesById(int id)
        {
            const string sqlText = @"SELECT id, name 
                                     FROM AccountsCategory
                                     WHERE id = @id";

            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand select = new OpenCbsCommand(sqlText, conn))
                {
                    select.AddParam("@id", id);
                    using (OpenCbsReader reader = select.ExecuteReader())
                    {
                        if (reader == null || reader.Empty) return null;
                        reader.Read();
                        return new AccountCategory
                                   {
                                       Id = reader.GetSmallInt("id"),
                                       Name = reader.GetString("name"),
                                   };

                    }
                }
            }
        }

        public int InsertAccountCategory(AccountCategory accountCategory, SqlTransaction pSqlTransac)
        {
            const string sqlText = @"INSERT INTO AccountsCategory (name) VALUES (@name) SELECT SCOPE_IDENTITY()";

            using (OpenCbsCommand insert = new OpenCbsCommand(sqlText, pSqlTransac.Connection, pSqlTransac))
            {
                insert.AddParam("@name",  accountCategory.Name);

                return int.Parse(insert.ExecuteScalar().ToString());
            }
        }

        public void DeleteAccountCategory(AccountCategory accountCategory)
        {
            const string sqlText = @"DELETE FROM AccountsCategory WHERE id = @id";
            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand delete = new OpenCbsCommand(sqlText, conn))
                {
                    delete.AddParam("@id", accountCategory.Id);
                    delete.ExecuteNonQuery();
                }
            }
        }

	    private static Account GetAccount(OpenCbsReader pReader)
        {
            return new Account
            {
                Id = pReader.GetInt("id"),
                Number = pReader.GetString("account_number"),
                Label = pReader.GetString("label"),
                DebitPlus = pReader.GetBool("debit_plus"),
                TypeCode = pReader.GetString("type_code"),
                AccountCategory = ((OAccountCategories)pReader.GetSmallInt("account_category_id")),
                Type = pReader.GetBool("type"),
                ParentAccountId = pReader.GetNullInt("parent_account_id"),
                Left = pReader.GetInt("lft"),
                Right = pReader.GetInt("rgt")
            };
        }

		public void Update(Account pAccount)
		{
            const string sqlText = @"UPDATE [ChartOFAccounts] 
                                    SET [account_number] = @number,                                          
                                        [label] = @label, 
                                        [debit_plus] = @debitPlus, 
                                        [type_code] = @typeCode, 
                                        [account_category_id] = @account_category_id, 
                                        [parent_account_id] = @parentAccountId
                                     WHERE id = @id";

            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand update = new OpenCbsCommand(sqlText, conn))
                {
                    update.AddParam("@id",  pAccount.Id);
                    SetAccount(update, pAccount);

                    update.ExecuteNonQuery();
                }
            }
		}

        public void Insert(Account account, SqlTransaction sqlTransac)
        {
            Insert(account, sqlTransac, false);
        }

	    public void Insert(Account account, SqlTransaction sqlTransac, bool setIdentity)
        {
            int rightMostSibling;
	        string sqlText;
            if (null == account.ParentAccountId || 0 == account.ParentAccountId)
            {
                 sqlText = @"SELECT CASE 
                                             WHEN MAX(rgt) IS NULL THEN 1 
                                             ELSE MAX(rgt) + 1 
                                    END
                                    FROM dbo.ChartOfAccounts";
                using (OpenCbsCommand cmd = new OpenCbsCommand(sqlText, sqlTransac.Connection, sqlTransac))
                {
                    rightMostSibling = Convert.ToInt32(cmd.ExecuteScalar());
                }
                
            }
            else
            {
                 sqlText = @"SELECT rgt
                                    FROM dbo.ChartOfAccounts
                                    WHERE id = @parent_account_id";
                using (OpenCbsCommand cmd = new OpenCbsCommand(sqlText, sqlTransac.Connection, sqlTransac))
                {
                    cmd.AddParam("@parent_account_id", account.ParentAccountId);
                    rightMostSibling = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }

            sqlText = @"UPDATE dbo.ChartOfAccounts
                                SET lft = CASE 
                                            WHEN lft > @right_most_sibling THEN lft + 2 
                                            ELSE lft 
                                          END
                                , rgt = CASE 
                                          WHEN rgt >= @right_most_sibling THEN rgt + 2 
                                          ELSE rgt 
                                        END
                                WHERE rgt >= @right_most_sibling";
            using (OpenCbsCommand cmd = new OpenCbsCommand(sqlText, sqlTransac.Connection, sqlTransac))
            {
                cmd.AddParam("@right_most_sibling", rightMostSibling);
                cmd.ExecuteNonQuery();

                account.Left = rightMostSibling;
                account.Right = account.Left + 1;
            }
	        if (setIdentity)
            {
                sqlText = @"
                                SET IDENTITY_INSERT [ChartOFAccounts] ON  
                                INSERT INTO ChartOFAccounts (
                                  id,
                                  account_number, 
                                  label, 
                                  debit_plus, 
                                  type_code, 
                                  account_category_id, 
                                  type, 
                                  parent_account_id, 
                                  lft, 
                                  rgt)
                                VALUES (
                                  @id,
                                  @number, 
                                  @label, 
                                  @debitPlus, 
                                  @typeCode, 
                                  @account_category_id, 
                                  @type, 
                                  @parentAccountId, 
                                  @lft, 
                                  @rgt)
                                SET IDENTITY_INSERT [ChartOFAccounts] OFF
                ";
                using (OpenCbsCommand cmd = new OpenCbsCommand(sqlText, sqlTransac.Connection, sqlTransac))
                {
                    cmd.AddParam("@id", account.Id);
                    SetAccount(cmd, account);
                    cmd.ExecuteNonQuery();
                }
            }
            else
            {
                sqlText =
                    @"INSERT INTO ChartOFAccounts (
                                  account_number, 
                                  label, 
                                  debit_plus, 
                                  type_code, 
                                  account_category_id, 
                                  type, 
                                  parent_account_id, 
                                  lft, 
                                  rgt)
                                VALUES (
                                  @number, 
                                  @label, 
                                  @debitPlus, 
                                  @typeCode, 
                                  @account_category_id, 
                                  @type, 
                                  @parentAccountId, 
                                  @lft, 
                                  @rgt)";
                using (OpenCbsCommand cmd = new OpenCbsCommand(sqlText, sqlTransac.Connection, sqlTransac))
                {
                    SetAccount(cmd, account);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteAccount(Account account)
        {
            using (SqlConnection conn = GetConnection())
            {
                OpenCbsCommand cmd = new OpenCbsCommand
                                         {
                                             Connection = conn,
                                             CommandText =
                                                 @"DELETE FROM dbo.ChartOfAccounts
                                               WHERE lft >= @lft AND rgt <= @rgt"
                                         };
                cmd.AddParam("@lft", account.Left);
                cmd.AddParam("@rgt", account.Right);
                cmd.ExecuteNonQuery();

                cmd.ResetParams();
                cmd.CommandText =
                    @"UPDATE dbo.ChartOfAccounts
                                SET lft = lft - @diff, rgt = rgt - @diff
                                WHERE lft > @lft";
                cmd.AddParam("@diff", account.Right - account.Left + 1);
                cmd.AddParam("@lft", account.Left);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete()
        {
            using (SqlConnection conn = GetConnection())
            using (SqlTransaction transaction = conn.BeginTransaction())
                try
                {
                    Delete(transaction);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
        }

        public void Delete(SqlTransaction transaction)
        {
            DeleteDatasFromTable("ChartOfAccounts", transaction);
        }

        private static void SetAccount(OpenCbsCommand cmd, Account pAccount)
        {
            cmd.AddParam("@number", pAccount.Number);
            cmd.AddParam("@label",  pAccount.Label);
            cmd.AddParam("@debitPlus", pAccount.DebitPlus);
            cmd.AddParam("@type",  pAccount.Type);
            cmd.AddParam("@typeCode",  pAccount.TypeCode);
            cmd.AddParam("@account_category_id",  (int)pAccount.AccountCategory);
            if (pAccount.ParentAccountId == 0 || pAccount.ParentAccountId == null)
                cmd.AddParam("parentAccountId", null);
            else
                cmd.AddParam("@parentAccountId",  pAccount.ParentAccountId);
            cmd.AddParam("@lft", pAccount.Left);
            cmd.AddParam("@rgt", pAccount.Right);
        }

        public bool SelectAccountingRuleByChartOfAccountsId(Account account)
        {
            const string sqlText = @"SELECT TOP 1 id
                                    FROM AccountingRules
                                    WHERE debit_account_number_id = @id OR credit_account_number_id = @id";

            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand select = new OpenCbsCommand(sqlText, conn))
                {
                    select.AddParam("@id", account.Id);

                    int? inUse;
                    using (OpenCbsReader reader = select.ExecuteReader())
                    {
                        if (reader == null || reader.Empty) return false;

                        reader.Read();
                        inUse = reader.GetInt("id");
                    }
                    return (inUse != null);
                }
            }
        }

	    private static readonly string[]
	        RelatedTables = {
	                             "Tellers",
                                 "FundingLineAccountingRules",                                 
                                 "ContractAccountingRules",                                 
	                             "AccountingRules",
	                             "LinkBranchesPaymentMethods",
	                             "LoanAccountingMovements",
	                             "SavingsAccountingMovements",
                                 "ManualAccountingMovements",                                 
	                             "StandardBookings"
	                         };

	    public IEnumerable<string> NotEmptyRelatedObjects()
	    {
	        using (SqlConnection connection = GetConnection())
	            foreach (var tableName in RelatedTables)
                    if (RecordExists(tableName, connection)) yield return tableName;
	    }

	    public void DeleteRelatedRecords(SqlTransaction transaction)
	    {
	        foreach (var tableName in RelatedTables) DeleteDatasFromTable(tableName, transaction);
	    }

	    public bool IdsExist(int[] ids)
	    {
	        string[] idStrings = ids.Select(i => i.ToString(CultureInfo.InvariantCulture)).ToArray();
	        string query = string.Format(
	            "IF EXISTS(SELECT 1 FROM ChartOfAccounts WHERE id in ({0})) SELECT 1 ELSE SELECT 0",
	            string.Join(",", idStrings)
	            );
	        using (SqlConnection connection = GetConnection())
            using (OpenCbsCommand command = new OpenCbsCommand(query, connection))
                return Convert.ToInt32(command.ExecuteScalar()) == 1;
	    }

	    public bool NumbersExist(string[] accountNumbers)
	    {
	        string query = string.Format(
	            "IF EXISTS(SELECT 1 FROM ChartOfAccounts WHERE account_number in ({0})) SELECT 1 ELSE SELECT 0",
	            string.Join(",", accountNumbers)
	            );

            using (SqlConnection connection = GetConnection())
            using (OpenCbsCommand command = new OpenCbsCommand(query, connection))
                return Convert.ToInt32(command.ExecuteScalar()) == 1;
	    }

        public void InsertFiscalYear(SqlTransaction sqlTransaction, FiscalYear fiscalYear)
        {
            const string sqlText =
                   @"INSERT INTO dbo.FiscalYear
                            ( name ,
                              open_date ,
                              close_date
                            )
                    VALUES  ( @name ,
                              @open_date , 
                              @close_date 
                            )";
            using (OpenCbsCommand command = new OpenCbsCommand(sqlText, sqlTransaction.Connection, sqlTransaction))
            {
                command.AddParam("@name", fiscalYear.Name);
                if (fiscalYear.OpenDate != null)
                {
                    command.AddParam("@open_date", ((DateTime) fiscalYear.OpenDate).Date);
                }
                else
                {
                    command.AddParam("@open_date", null);
                }
                
                if (fiscalYear.CloseDate != null)
                {
                    command.AddParam("@close_date", ((DateTime) fiscalYear.CloseDate).Date);
                }
                else
                {
                    command.AddParam("@close_date", null);
                }

                command.ExecuteNonQuery();
            }   
        }

        public void DeleteFiscalYear(SqlTransaction sqlTransaction, FiscalYear fiscalYear)
        {
            const string sqlText =
                   @"DELETE FROM dbo.FiscalYear
                     WHERE id = @id";
            using (OpenCbsCommand command = new OpenCbsCommand(sqlText, sqlTransaction.Connection, sqlTransaction))
            {
                command.AddParam("@id", fiscalYear.Id);
                command.ExecuteNonQuery();
            }
        }

        public void UpdateFiscalYear(SqlTransaction sqlTransaction, FiscalYear fiscalYear)
        {
            const string sqlText =
                   @"UPDATE FiscalYear
                    SET close_date = @close_date,
                    open_date = @open_date,
                    name = @name
                    WHERE id = @id";
            using (OpenCbsCommand command = new OpenCbsCommand(sqlText, sqlTransaction.Connection, sqlTransaction))
            {
                if (fiscalYear.OpenDate != null)
                {
                    command.AddParam("@open_date", ((DateTime)fiscalYear.OpenDate).Date);
                }
                else
                {
                    command.AddParam("@open_date", null);
                }

                if (fiscalYear.CloseDate != null)
                {
                    command.AddParam("@close_date", ((DateTime)fiscalYear.CloseDate).Date);
                }
                else
                {
                    command.AddParam("@close_date", null);
                }
                command.AddParam("@name", fiscalYear.Name);
                command.AddParam("@id", fiscalYear.Id);
                command.ExecuteNonQuery();
            }
        }

        public List<FiscalYear> SelectFiscalYears()
        {
            const string sqlText = @"SELECT 
                                      id, 
                                      name, 
                                      open_date, 
                                      close_date 
                                    FROM dbo.FiscalYear";

            List<FiscalYear> fiscalYears = new List<FiscalYear>();
            
            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand select = new OpenCbsCommand(sqlText, conn))
                {
                    using (OpenCbsReader reader = select.ExecuteReader())
                    {
                        if (reader.Empty) return null;
                        while (reader.Read())
                        {
                            fiscalYears.Add(new FiscalYear
                                                {
                                                    Id = reader.GetInt("id"),
                                                    Name = reader.GetString("name"),
                                                    CloseDate = reader.GetNullDateTime("close_date"),
                                                    OpenDate = reader.GetNullDateTime("open_date")
                                                });
                        }
                    }
                }
            }
            return fiscalYears;
        }
	}
}
