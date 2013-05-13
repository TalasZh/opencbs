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
using OpenCBS.DatabaseConnection;
using OpenCBS.Enums;
using OpenCBS.Shared;
using OpenCBS.CoreDomain.Accounting;

namespace OpenCBS.Manager.Accounting
{
	/// <summary>
	/// AccountManagement contains all methods relative to selecting, inserting, updating
	/// and deleting account objects to and from our database.
	/// </summary>
	public class AccountManagement : Manager
	{
	    private User _user = new User();
        public AccountManagement(User pUser) : base(pUser)
        {
            _user = pUser;
        }

		public AccountManagement(string testDB) : base(testDB)
		{
         
		}

		public int AddAccount(Account account)
		{
			string sqlText = "INSERT INTO [Accounts]([account_number], [local_account_number], [label], "+
				"[balance], [debit_plus], [type_code], [description]) "+
				" VALUES (@number,@localNumber,@label,@balance,@debitPlus,@typeCode,@description) SELECT SCOPE_IDENTITY()";					

			SqlCommand insertInAccountTable = new SqlCommand(sqlText,CurrentConnection);
			_SetAccount(insertInAccountTable,account);

            return int.Parse(insertInAccountTable.ExecuteScalar().ToString());
		}

		public Account SelectAccountById(int accountId)
		{
			return SelectAccountById(accountId,null);	
		}

	    private static Account _GetAccount(SqlDataReader pReader)
        {
            Account account = new Account();
            account.Id = DatabaseHelper.GetInt32("id", pReader);
            account.Number = DatabaseHelper.GetString("account_number", pReader);
            account.LocalNumber = DatabaseHelper.GetString("local_account_number", pReader);
            account.Label = DatabaseHelper.GetString("label", pReader);
            account.Balance = DatabaseHelper.GetMoney("balance", pReader);
            account.DebitPlus = DatabaseHelper.GetBoolean("debit_plus", pReader);
            account.TypeCode = DatabaseHelper.GetString("type_code", pReader);
            account.Description = (OAccountDescriptions)DatabaseHelper.GetSmallInt("description", pReader);
            return account;
        }

        public Account SelectAccountById(int accountId,SqlTransaction sqlTransac)
		{
			Account account = null;

			string sqlText = "SELECT [id], [account_number], [local_account_number], [label], [balance], [debit_plus], "+
				"[type_code], [description] FROM [Accounts] "+
				"WHERE id = @id";

            SqlCommand select = new SqlCommand(sqlText, CurrentConnection, sqlTransac);

			DatabaseHelper.InsertInt32Param("@id",select,accountId);

            using (SqlDataReader reader = select.ExecuteReader())
			{
				if (reader.HasRows)
				{
					reader.Read();
				    account = _GetAccount(reader);
				}
			}
			return account;
		}

		public Account SelectAccountByType(string type,SqlTransaction sqlTransac)
		{
			Account account = null;

			string sqlText = "SELECT [id], [account_number], [local_account_number], [label], [balance], [debit_plus], "+
				"[type_code], [description] FROM [Accounts] "+
				"WHERE type_code = @type";

            SqlCommand select = new SqlCommand(sqlText, CurrentConnection, sqlTransac);

			DatabaseHelper.InsertStringVarCharParam("@type",select,type);

            using (SqlDataReader reader = select.ExecuteReader())
			{
				if (reader.HasRows)
				{
					reader.Read();
					account = _GetAccount(reader);
				}
			}
			return account;
		}

	
		public List<Account> SelectAllAccounts()
		{
			string sqlText = "SELECT [id], [account_number], [local_account_number], [label], [balance], [debit_plus],[type_code], [description] "+
				"FROM [Accounts]";

            List<Account> list = new List<Account>();

            SqlCommand CmdSelect = new SqlCommand(sqlText, CurrentConnection);
            using (SqlDataReader reader = CmdSelect.ExecuteReader())
			{
				while(reader.Read())
				{
				    Account account = _GetAccount(reader);
					list.Add(account);
				}
			}
			return list;
		}

		public void UpdateAccount(Account account,SqlTransaction sqlTransac)
		{
			string sqlText = "UPDATE [Accounts] SET [account_number]=@number, [local_account_number]=@localNumber, "+
				"[label]=@label, [balance]=@balance, [debit_plus]=@debitPlus, [type_code]=@typeCode, [description]=@description "+
				"WHERE id = @id";

            SqlCommand update = new SqlCommand(sqlText, CurrentConnection, sqlTransac);
			DatabaseHelper.InsertInt32Param("@id",update,account.Id);
			_SetAccount(update,account);

            update.ExecuteNonQuery();
		}

        private static void _SetAccount(SqlCommand pSqlCommand, Account pAccount)
        {
            DatabaseHelper.InsertStringNVarCharParam("@number", pSqlCommand, pAccount.Number);
            DatabaseHelper.InsertStringNVarCharParam("@localNumber", pSqlCommand, pAccount.LocalNumber);
            DatabaseHelper.InsertStringNVarCharParam("@label", pSqlCommand, pAccount.Label);
            DatabaseHelper.InsertMoneyParam("@balance", pSqlCommand, pAccount.Balance);
            DatabaseHelper.InsertBooleanParam("@debitPlus", pSqlCommand, pAccount.DebitPlus);
            DatabaseHelper.InsertStringVarCharParam("@typeCode", pSqlCommand, pAccount.TypeCode);
            DatabaseHelper.InsertSmalIntParam("@description", pSqlCommand, (int)pAccount.Description);
        }


		public void InsertProvisioningValue(ProvisioningRate pR,SqlTransaction sqlTransac)
		{
			string sqlText = "INSERT INTO ProvisioningRules(id,number_of_days_min, number_of_days_max, provisioning_value) "+
				"VALUES(@number,@numberOfDaysMin, @numberOfDaysMax, @provisioningPercentage)";

            SqlCommand insert = new SqlCommand(sqlText, CurrentConnection, sqlTransac);

			DatabaseHelper.InsertInt32Param("@number",insert,pR.Number);
			DatabaseHelper.InsertInt32Param("@numberOfDaysMin",insert,pR.NbOfDaysMin);
			DatabaseHelper.InsertInt32Param("@numberOfDaysMax",insert,pR.NbOfDaysMax);
			DatabaseHelper.InsertDoubleParam("@provisioningPercentage",insert,pR.Rate);

            insert.ExecuteNonQuery();
		}
        
		/// <summary>
		/// This method Fill the instance of the ProvisioningTable object accessed by singleton
		/// </summary>
		public void SelectAllProvisioningRates()
		{
		    ProvisioningTable provisioningTable = ProvisioningTable.GetInstance(_user);

			string sqlText = "SELECT id,number_of_days_min, number_of_days_max, provisioning_value FROM ProvisioningRules";

            SqlCommand select = new SqlCommand(sqlText, CurrentConnection);

            using (SqlDataReader reader = select.ExecuteReader())
			{
				while(reader.Read())
				{
					ProvisioningRate pR = new ProvisioningRate();
					pR.Number = DatabaseHelper.GetInt32("id",reader);
					pR.NbOfDaysMin = DatabaseHelper.GetInt32("number_of_days_min",reader);
					pR.NbOfDaysMax = DatabaseHelper.GetInt32("number_of_days_max",reader);
					pR.Rate = DatabaseHelper.GetDouble("provisioning_value",reader);

					provisioningTable.AddProvisioningRate(pR);
				}
			}
		}
        public void DeleteAllProvisioningRules(SqlTransaction sqlTransac)
        {
            string sqlText = "DELETE FROM ProvisioningRules";
            SqlCommand delete = new SqlCommand(sqlText, CurrentConnection, sqlTransac);

            delete.ExecuteNonQuery();
        }
        public void InsertLoanScaleValue(LoanScaleRate lR, SqlTransaction sqlTransac)
        {
            string sqlText = "INSERT INTO LoanScale(id,ScaleMin, ScaleMax) " +
                "VALUES(@number,@Min, @Max)";

            SqlCommand insert = new SqlCommand(sqlText, CurrentConnection, sqlTransac);

            DatabaseHelper.InsertInt32Param("@number", insert, lR.Number);
            DatabaseHelper.InsertInt32Param("@Min", insert, lR.ScaleMin);
            DatabaseHelper.InsertInt32Param("@Max", insert, lR.ScaleMax);

            insert.ExecuteNonQuery();
        }
        public void SelectLoanScale()
        {
            LoanScaleTable loanscaleTable = LoanScaleTable.GetInstance(_user);
            string sqlText = "Select id, ScaleMin, ScaleMax FROM LoanScale";
            SqlCommand select = new SqlCommand(sqlText, CurrentConnection);
            using (SqlDataReader reader = select.ExecuteReader())
            {
                while (reader.Read())
                {
                    LoanScaleRate lR = new LoanScaleRate();
                    lR.Number = DatabaseHelper.GetInt32("id", reader);
                    lR.ScaleMin = DatabaseHelper.GetInt32("ScaleMin", reader);
                    lR.ScaleMax = DatabaseHelper.GetInt32("ScaleMax", reader);
                    loanscaleTable.AddLoanScaleRate(lR);
                }
            }
        }

		

        public void DeleteAllLoanScale(SqlTransaction sqltran)
        {
            string sqltext = "Delete FROM LoanScale";
            SqlCommand delete = new SqlCommand(sqltext, CurrentConnection, sqltran);
            delete.ExecuteNonQuery();
        }

        public void DeleteAccountById(int id)
        {
            SqlCommand delete = new SqlCommand("DELETE Accounts WHERE Accounts.id = @id", CurrentConnection);
            DatabaseHelper.InsertInt32Param("@id", delete, id);
            delete.ExecuteNonQuery();
        }

	}
}
