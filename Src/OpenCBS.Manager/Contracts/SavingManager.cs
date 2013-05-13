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

using System.Collections.Generic;
using System.Data.SqlClient;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Clients;
using OpenCBS.CoreDomain.Products;
using OpenCBS.Manager.Events;
using OpenCBS.Manager.Products;
using OpenCBS.CoreDomain.Contracts.Savings;
using OpenCBS.Shared;
using OpenCBS.Shared.Settings;
using OpenCBS.CoreDomain.SearchResult;
using OpenCBS.Manager.QueryForObject;
using OpenCBS.Enums;
using System;
using OpenCBS.CoreDomain.Contracts.Loans;
using OpenCBS.Manager.Clients;

namespace OpenCBS.Manager.Contracts
{
    /// <summary>
    /// CreditContractManagement contains all methods relative to selecting, inserting, updating
    /// and deleting creditContract objects to and from our database.
    /// </summary>
    public class SavingManager : Manager
    {
        public delegate void SavingSelectedEventHandler(SavingsContract saving);

        public event SavingSelectedEventHandler SavingSelected;

        private readonly SavingProductManager _savingProductManager;
        private readonly SavingEventManager _savingEventManager;
        private readonly LoanManager _loanManager;
        private readonly ClientManager _clientManager;
        private readonly User _user;

        public SavingManager(User pUser) : base(pUser)
        {
            _savingProductManager = new SavingProductManager(pUser);
            _savingEventManager = new SavingEventManager(pUser);
            _loanManager = new LoanManager(pUser);
            _clientManager = new ClientManager(pUser, false, false);
            _user = pUser;
        }

        public SavingManager(string pTestDB): base(pTestDB)
        {
            _savingProductManager = new SavingProductManager(pTestDB);
            _savingEventManager = new SavingEventManager(pTestDB);
            _loanManager = new LoanManager(pTestDB);
            _user = new User();
        }

        public int Add(ISavingsContract savings, Client client)
        {
            using (SqlConnection connection = GetConnection())
            using (SqlTransaction transaction = connection.BeginTransaction())
                try
                {
                    int result = Add(savings, client, transaction);
                    transaction.Commit();
                    return result;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
        }

        public int Add(ISavingsContract savings, Client client, SqlTransaction sqlTransac)
        {
            const string sqlText = @"INSERT INTO [SavingContracts] 
                ( 
                    [product_id],
                    [user_id],
                    [code],
                    [status],
                    [tiers_id],
                    [creation_date], 
                    [interest_rate], 
                    [closed_date], 
                    [savings_officer_id],
                    [initial_amount],
                    [entry_fees],
                    [nsg_id]
                ) 
                VALUES 
                (
                    @product_id,
                    @user_id, 
                    @code, 
                    @status, 
                    @tiers_id, 
                    @creation_date, 
                    @interest_rate, 
                    @closedDate, 
                    @savings_officer_id,
                    @initial_amount,
                    @entry_fees,
                    @nsg_id
                )
				SELECT CONVERT(int, SCOPE_IDENTITY())";

            using (OpenCbsCommand cmd = new OpenCbsCommand(sqlText, sqlTransac.Connection, sqlTransac))
            {
                cmd.AddParam("@product_id",  savings.Product.Id);
                cmd.AddParam("@user_id", savings.User.Id);
                cmd.AddParam("@code", savings.Code);
                cmd.AddParam("@status",  (int)savings.Status);
                cmd.AddParam("@tiers_id", client.Id);
                cmd.AddParam("@creation_date", savings.CreationDate);
                cmd.AddParam("@interest_rate", savings.InterestRate);
                cmd.AddParam("@closedDate",  savings.ClosedDate);
                cmd.AddParam("@savings_officer_id", savings.SavingsOfficer.Id);
                cmd.AddParam("@initial_amount",  savings.InitialAmount);
                cmd.AddParam("@entry_fees", savings.EntryFees);
                cmd.AddParam("@nsg_id",  savings.NsgID);

                savings.Id = int.Parse(cmd.ExecuteScalar().ToString());
            }
            
            AddSavingsBookContract((SavingBookContract)savings, sqlTransac);
            
            return savings.Id;
        }

        public void UpdateSavingContractCode(int savingsId, string code, SqlTransaction pSqlTransac)
        {
            const string sqlText = @"UPDATE SavingContracts SET code = @code WHERE id = @id";
            using (OpenCbsCommand cmd = new OpenCbsCommand(sqlText, pSqlTransac.Connection, pSqlTransac))
            {
                cmd.AddParam("@code", code);
                cmd.AddParam("@id", savingsId);
                cmd.ExecuteNonQuery();    
            }
        }

        public int GetLastSavingsId ()
        {
            const string sqlText = @"SELECT TOP 1 id FROM dbo.SavingContracts ORDER BY id DESC";
            using (SqlConnection conn = GetConnection())
            using (SqlCommand select = new SqlCommand(sqlText, conn))
            {
                object val = select.ExecuteScalar();
                return null == val ? 0 : Convert.ToInt32(val);
            }
            
        }

        public void AddSavingsBookContract(SavingBookContract pSaving, SqlTransaction pSqlTransac)
        {
            const string sqlText = @"INSERT INTO [SavingBookContracts] 
                                    (
                                        [id],
                                        [flat_withdraw_fees],
                                        [rate_withdraw_fees], 
                                        [flat_transfer_fees], 
                                        [rate_transfer_fees], 
                                        [flat_deposit_fees], 
                                        [flat_close_fees],
                                        [flat_management_fees], 
                                        [flat_overdraft_fees], 
                                        [in_overdraft], 
                                        [rate_agio_fees],
                                        [cheque_deposit_fees],
                                        [flat_reopen_fees],
                                        [flat_ibt_fee],
                                        [rate_ibt_fee],
                                        [use_term_deposit],
                                        [term_deposit_period],
                                        [term_deposit_period_min],
                                        [term_deposit_period_max],
                                        [transfer_account],
                                        [rollover],
                                        [next_maturity]
                                    )
                                        VALUES 
                                    (
                                        @id,
                                        @flatWithdrawFees,
                                        @rateWithdrawFees,
                                        @flatTransferFees,
                                        @rateTransferFees,
                                        @flatDepositFees, 
                                        @flatCloseFees,
                                        @flatManagementFees,
                                        @flatOverdraftFees,
                                        @inOverdraft,
                                        @rateAgioFees,
                                        @chequeDepositFees,
                                        @flatReopenFees,
                                        @flat_ibt_fee,
                                        @rate_ibt_fee,
                                        @use_term_deposit,
                                        @term_deposit_period,
                                        @term_deposit_period_min,
                                        @term_deposit_period_max,
                                        @transfer_account,
                                        @rollover,
                                        @next_maturity
                                    )";

            using (OpenCbsCommand cmd = new OpenCbsCommand(sqlText, pSqlTransac.Connection, pSqlTransac))
            {
                cmd.AddParam("@id",  pSaving.Id);
                cmd.AddParam("@flatWithdrawFees", pSaving.Product.WithdrawFeesType == OSavingsFeesType.Flat ? pSaving.FlatWithdrawFees : null);
                cmd.AddParam("@rateWithdrawFees", pSaving.Product.WithdrawFeesType == OSavingsFeesType.Rate ? pSaving.RateWithdrawFees : null);
                cmd.AddParam("@flatTransferFees", pSaving.Product.TransferFeesType == OSavingsFeesType.Flat ? pSaving.FlatTransferFees : null);
                cmd.AddParam("@rateTransferFees", pSaving.Product.TransferFeesType == OSavingsFeesType.Rate ? pSaving.RateTransferFees : null);
                OCurrency flat = null;
                double? rate = null;
                if (pSaving.Product.InterBranchTransferFee.IsFlat)
                {
                    flat = pSaving.FlatInterBranchTransferFee;
                }
                else
                {
                    rate = pSaving.RateInterBranchTransferFee;
                }
                cmd.AddParam("@flat_ibt_fee",  flat);
                cmd.AddParam("@rate_ibt_fee", rate);
                cmd.AddParam("@flatDepositFees", pSaving.DepositFees);
                cmd.AddParam("@flatCloseFees", pSaving.CloseFees);
                cmd.AddParam("@flatManagementFees", pSaving.ManagementFees);
                cmd.AddParam("@flatOverdraftFees", pSaving.OverdraftFees);
                cmd.AddParam("@inOverdraft", pSaving.InOverdraft);
                cmd.AddParam("@rateAgioFees", pSaving.AgioFees);
                cmd.AddParam("@chequeDepositFees", pSaving.ChequeDepositFees);
                cmd.AddParam("@flatReopenFees", pSaving.ReopenFees);
                cmd.AddParam("@use_term_deposit",  pSaving.UseTermDeposit);
                cmd.AddParam("@term_deposit_period", pSaving.NumberOfPeriods);
                
//                if (pSaving.UseTermDeposit)
//                {
                    cmd.AddParam("@term_deposit_period_min", pSaving.TermDepositPeriodMin);
                    cmd.AddParam("@term_deposit_period_max", pSaving.TermDepositPeriodMax);
                    cmd.AddParam("@transfer_account",
                        pSaving.TransferAccount != null ? pSaving.TransferAccount.Code : null);
                    cmd.AddParam("@rollover", (int)pSaving.Rollover);
//                }
//                else
//                {
//                    cmd.AddParam("@term_deposit_period_min", null);
//                    cmd.AddParam("@term_deposit_period_max", null);
//                    cmd.AddParam("@transfer_account", null);
//                    cmd.AddParam("@rollover", null);
//                }
                
                cmd.AddParam("@next_maturity", pSaving.NextMaturity);
                
                cmd.ExecuteNonQuery();
            }
        }

        public List<ISavingsContract> SelectAll(string pSavingCode)
        {
            List<ISavingsContract> listSaving = new List<ISavingsContract>();

            const string sqlText = @"SELECT SavingContracts.*,
                                       u2.first_name AS so_first_name, 
                                       u2.last_name AS so_last_name,            
	                                   SavingProducts.product_type,
                                       Users.id AS user_id, 
                                       Users.deleted, 
                                       Users.user_name, 
                                       Users.user_pass, 
                                       Users.role_code, 
                                       Users.first_name, 
                                       Users.last_name 
	                            FROM SavingContracts 
                                INNER JOIN SavingProducts ON SavingContracts.product_id = SavingProducts.id
                                INNER JOIN Users ON SavingContracts.user_id = Users.id
                                INNER JOIN Users AS u2 ON u2.id = SavingContracts.savings_officer_id
                                WHERE SavingContracts.Code LIKE @code";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand select = new OpenCbsCommand(sqlText, conn))
            {
                select.AddParam("@code", string.Format("%{0}%", pSavingCode));
                using (OpenCbsReader reader = select.ExecuteReader())
                {
                    if (reader.Empty) return new List<ISavingsContract>();

                    while (reader.Read())
                    {
                        ISavingsContract saving = GetSavingFromReader(reader);
                        listSaving.Add(saving);
                    }
                }
            }

            foreach (ISavingsContract saving in listSaving)
            {
               SelectSavingsBookContract((SavingBookContract)saving);
               saving.Product = _savingProductManager.SelectSavingProduct(saving.Product.Id);
               saving.Events.AddRange(_savingEventManager.SelectEvents(saving.Id, saving.Product));
            }

            return listSaving;
        }

        public SavingBookContract Select(int pSavingId)
        {
            SavingBookContract saving;

            const string sqlText = @"SELECT SavingContracts.*,
                                       u2.first_name AS so_first_name, 
                                       u2.last_name AS so_last_name,
                                       SavingProducts.product_type,
	                                   Users.id AS user_id, 
                                       Users.deleted, 
                                       Users.user_name, 
                                       Users.user_pass, 
                                       Users.role_code, 
                                       Users.first_name, 
                                       Users.last_name 
	                                FROM SavingContracts
                                    INNER JOIN SavingProducts ON SavingContracts.product_id = SavingProducts.id 
                                    INNER JOIN Users ON SavingContracts.user_id = Users.id
                                    INNER JOIN Users AS u2 ON u2.id = SavingContracts.savings_officer_id
	                                WHERE SavingContracts.id = @id";
            using (SqlConnection conn = GetConnection())
            using(var cmd = new OpenCbsCommand(sqlText, conn))
            {
                cmd.AddParam("@id",  pSavingId);
                using (OpenCbsReader reader = cmd.ExecuteReader())
                {
                    if (reader.Empty) return null;
                    reader.Read();
                    saving = GetSavingFromReader(reader);
                }
            }

            SelectSavingsBookContract(saving);
            saving.Product = _savingProductManager.SelectSavingProduct(saving.Product.Id);
            saving.Events.AddRange(_savingEventManager.SelectEvents(saving.Id, saving.Product));
            return saving;
        }

        public ISavingsContract Select(string pSavingCode)
        {
            ISavingsContract saving;

            const string sqlText = @"SELECT SavingContracts.*,
                                       u2.first_name AS so_first_name, 
                                       u2.last_name AS so_last_name,
                                       SavingProducts.product_type,
	                                   Users.id AS user_id, 
                                       Users.deleted, 
                                       Users.user_name, 
                                       Users.user_pass, 
                                       Users.role_code, 
                                       Users.first_name, 
                                       Users.last_name 
	                                 FROM SavingContracts
                                     INNER JOIN SavingProducts ON SavingContracts.product_id = SavingProducts.id 
                                     INNER JOIN Users ON SavingContracts.user_id = Users.id
                                     INNER JOIN Users AS u2 ON u2.id = SavingContracts.savings_officer_id
	                                 WHERE SavingContracts.Code = @code";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand select = new OpenCbsCommand(sqlText, conn))
            {
               select.AddParam("@code", pSavingCode);
                using (OpenCbsReader reader = select.ExecuteReader())
                {
                    if (reader.Empty) return null;

                    reader.Read();
                    saving = GetSavingFromReader(reader);
                }
            }

            SelectSavingsBookContract((SavingBookContract)saving);
           
            saving.Product = _savingProductManager.SelectSavingProduct(saving.Product.Id);
            saving.Events.AddRange(_savingEventManager.SelectEvents(saving.Id, saving.Product));
            return saving;
        }

        public SavingBookContract SelectSavingsByLoanId(int pLoanId, bool loadLoans)
        {
            SavingBookContract saving;

            const string sqlText = @"SELECT 
                                       SavingContracts.*,
                                       u2.first_name AS so_first_name, 
                                       u2.last_name AS so_last_name,
                                       SavingProducts.product_type,
                                       Users.id AS [user_id], 
                                       Users.deleted, 
                                       Users.user_name, 
                                       Users.user_pass, 
                                       Users.role_code, 
                                       Users.first_name, 
                                       Users.last_name 
                                     FROM SavingContracts
                                     INNER JOIN SavingProducts ON SavingContracts.product_id = SavingProducts.id 
                                     INNER JOIN LoansLinkSavingsBook ON LoansLinkSavingsBook.savings_id = SavingContracts.id
                                     INNER JOIN Users ON SavingContracts.[user_id] = Users.id
                                     INNER JOIN Users AS u2 ON u2.id = SavingContracts.savings_officer_id
                                     WHERE LoansLinkSavingsBook.loan_id = @loanId";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand select = new OpenCbsCommand(sqlText, conn))
            {
                select.AddParam("@loanId",  pLoanId);
                using (OpenCbsReader reader = select.ExecuteReader())
                {
                    if (reader.Empty) return null;

                    reader.Read();
                    saving = (SavingBookContract)GetSavingFromReader(reader);
                }
            }

            if (saving != null)
            {
                saving.Product = (SavingsBookProduct)_savingProductManager.SelectSavingProduct(saving.Product.Id);
                saving.Events.AddRange(_savingEventManager.SelectEvents(saving.Id, saving.Product));
                if (loadLoans) 
                    saving.Loans = SelectLoansBySavingsId(saving.Id);
            }

            return saving;
        }

        public List<Loan> SelectLoansBySavingsId(int savingsId)
        {
            List<Loan> listLoans = new List<Loan>();

            const string sqlText = @"SELECT loan_id
                                     FROM LoansLinkSavingsBook 
                                     WHERE savings_id = @savingsId";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand select = new OpenCbsCommand(sqlText, conn))
            {
                select.AddParam("@savingsId",  savingsId);

                using (OpenCbsReader reader = select.ExecuteReader())
                {
                    if (!reader.Empty)
                    {
                        while (reader.Read())
                        {
                            listLoans.Add(_loanManager.SelectLoan(reader.GetInt("loan_id"), true, false, false));
                        }
                    }
                }
            }

            return listLoans;
        }

        public void SelectSavingsBookContract(SavingBookContract saving)
        {
            const string sqlText = @"SELECT
                                            sbc.flat_withdraw_fees, 
                                            sbc.rate_withdraw_fees, 
                                            sbc.flat_transfer_fees, 
                                            sbc.rate_transfer_fees, 
                                            sbc.flat_deposit_fees, 
                                            sbc.flat_close_fees,
                                            sbc.flat_management_fees, 
                                            sbc.flat_overdraft_fees, 
                                            sbc.in_overdraft,
                                            sbc.rate_agio_fees,
                                            sbc.cheque_deposit_fees,
                                            sbc.flat_reopen_fees,
                                            sbc.flat_ibt_fee,
                                            sbc.rate_ibt_fee,
                                            sbc.use_term_deposit,
                                            sbc.term_deposit_period,
                                            sbc.term_deposit_period_min,
                                            sbc.term_deposit_period_max,
                                            sbc.transfer_account,
                                            sbc.rollover,
                                            t.branch_id,
                                            sbc.next_maturity
                                    FROM dbo.SavingBookContracts sbc
                                    INNER JOIN dbo.SavingContracts sc on sc.id = sbc.id
                                    INNER JOIN dbo.Tiers t on t.id = sc.tiers_id
                                    WHERE sbc.id = @id";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand select = new OpenCbsCommand(sqlText, conn))
            {
                select.AddParam("@id", saving.Id);
                using (OpenCbsReader reader = select.ExecuteReader())
                {
                    if (!reader.Read()) return;
                    GetSavingBookFromReader(saving, reader);
                }
                //It needs to launch a new reader, that's why next code is here
                saving.TransferAccount = Select(saving.TransferAccount.Code);
            }
            OnSavingSelected(saving);
        }

        private void GetSavingBookFromReader(SavingBookContract saving, OpenCbsReader reader)
        {
            saving.FlatWithdrawFees = reader.GetNullDecimal("flat_withdraw_fees");
            saving.RateWithdrawFees = reader.GetNullDouble("rate_withdraw_fees");
            saving.FlatTransferFees = reader.GetNullDecimal("flat_transfer_fees");
            saving.RateTransferFees = reader.GetNullDouble("rate_transfer_fees");
            saving.DepositFees = reader.GetNullDecimal("flat_deposit_fees");
            saving.ChequeDepositFees = reader.GetNullDecimal("cheque_deposit_fees");
            saving.CloseFees = reader.GetNullDecimal("flat_close_fees");
            saving.ManagementFees = reader.GetNullDecimal("flat_management_fees");
            saving.OverdraftFees = reader.GetNullDecimal("flat_overdraft_fees");
            saving.InOverdraft = reader.GetBool("in_overdraft");
            saving.AgioFees = reader.GetNullDouble("rate_agio_fees");
            saving.ReopenFees = reader.GetNullDecimal("flat_reopen_fees");
            saving.FlatInterBranchTransferFee = reader.GetNullDecimal("flat_ibt_fee");
            saving.RateInterBranchTransferFee =reader.GetNullDouble("rate_ibt_fee");
            saving.UseTermDeposit = reader.GetBool("use_term_deposit");
            saving.NumberOfPeriods = reader.GetInt("term_deposit_period");
            saving.TermDepositPeriodMin = reader.GetNullInt("term_deposit_period_min");
            saving.TermDepositPeriodMax = reader.GetNullInt("term_deposit_period_max");
            saving.TransferAccount = new SavingBookContract(ApplicationSettings.GetInstance(_user.Md5), _user) 
            { Code = reader.GetString("transfer_account") };
            saving.NextMaturity = reader.GetNullDateTime("next_maturity");
            if (saving.UseTermDeposit)
                saving.Rollover = (OSavingsRollover) reader.GetInt("rollover");
            // This is the bozo's way of fetching branch information.
            // Ideally it should be available through the saving's client object.
            // But that object is initialized in many places so that modifying
            // it does not look feasible.
            // Instead, we constract a new Branch object *partially* and then
            // through the OnSavingSelected below call back into the Service layer
            // to finalize the construction.
            saving.Branch = new Branch {Id = reader.GetInt("branch_id")};
        }

        public List<ISavingsContract> SelectAll()
        {
            List<ISavingsContract> listSaving = new List<ISavingsContract>();

            const string sqlText = @"SELECT SavingContracts.*,
                                            u2.first_name AS so_first_name, 
                                            u2.last_name AS so_last_name,
                                            SavingProducts.product_type,
                                            Users.id AS user_id, 
                                            Users.deleted, 
                                            Users.user_name, 
                                            Users.user_pass, 
                                            Users.role_code, 
                                            Users.first_name, 
                                            Users.last_name 
	                                FROM SavingContracts
                                    INNER JOIN SavingProducts ON SavingContracts.product_id = SavingProducts.id 
                                    INNER JOIN Users AS u2 ON u2.id = SavingContracts.savings_officer_id
                                    INNER JOIN Users ON SavingContracts.user_id = Users.id";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand select = new OpenCbsCommand(sqlText, conn))
            {
                using (OpenCbsReader reader = select.ExecuteReader())
                {
                    if (!reader.Empty)
                    {
                        while (reader.Read())
                        {
                            listSaving.Add(GetSavingFromReader(reader));
                        }
                    }
                }
            }

            foreach (ISavingsContract saving in listSaving)
            {
                SelectSavingsBookContract((SavingBookContract)saving);
                saving.Product = _savingProductManager.SelectSavingProduct(saving.Product.Id);
                saving.Events.AddRange(_savingEventManager.SelectEvents(saving.Id, saving.Product));
                if (_clientManager != null)
                    saving.Client = _clientManager.SelectClientBySavingsId(saving.Id);
            }

            return listSaving;
        }

        public List<ISavingsContract> SelectAllActive()
        {
            List<ISavingsContract> listSaving = new List<ISavingsContract>();

            string sqlText = @"SELECT SavingContracts.*,
                                            u2.first_name AS so_first_name, 
                                            u2.last_name AS so_last_name,
                                            SavingProducts.product_type,
                                            Users.id AS user_id, 
                                            Users.deleted, 
                                            Users.user_name, 
                                            Users.user_pass, 
                                            Users.role_code, 
                                            Users.first_name, 
                                            Users.last_name 
	                                FROM SavingContracts
                                    INNER JOIN SavingProducts ON SavingContracts.product_id = SavingProducts.id 
                                    INNER JOIN Users AS u2 ON u2.id = SavingContracts.savings_officer_id
                                    INNER JOIN Users ON SavingContracts.user_id = Users.id
                                    WHERE SavingContracts.status != " + ((int) OSavingsStatus.Closed);
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand select = new OpenCbsCommand(sqlText, conn))
            {
                using (OpenCbsReader reader = select.ExecuteReader())
                {
                    if (!reader.Empty)
                    {
                        while (reader.Read())
                        {
                            listSaving.Add(GetSavingFromReader(reader));
                        }
                    }
                }
            }

            foreach (ISavingsContract saving in listSaving)
            {
                SelectSavingsBookContract((SavingBookContract)saving);
                saving.Product = _savingProductManager.SelectSavingProduct(saving.Product.Id);
                saving.Events.AddRange(_savingEventManager.SelectEvents(saving.Id, saving.Product));
                if (_clientManager != null)
                    saving.Client = _clientManager.SelectClientBySavingsId(saving.Id);
            }

            return listSaving;
        }

        public int GetNumberSavingContract(string pQuery, bool all, bool activeContractsOnly)
        {
            string sql =@"SELECT TOP 100 percent 
                            SavingContracts.id, 
                            SavingContracts.code AS contract_code, 
                            SavingContracts.creation_date as start_date, 
                            Persons.identification_data as identification_data, 
                            Tiers.client_type_code, 
                            ISNULL(Users.first_name + SPACE(1) + Users.last_name, Users.user_name) AS user_name, 
                            ISNULL(Persons.first_name + SPACE(1) + Persons.last_name, ISNULL(Groups.name, Corporates.name)) AS client_name,
                            ISNULL(Users.first_name + SPACE(1) + Users.last_name, Users.user_name) AS loanofficer_name
                            FROM SavingContracts 
                            INNER JOIN Users ON SavingContracts.user_id = Users.id
                            INNER JOIN Tiers ON SavingContracts.tiers_id = Tiers.id
                            LEFT OUTER JOIN Persons ON Tiers.id = Persons.id 
                            LEFT OUTER JOIN Groups ON Tiers.id = Groups.id 
                            LEFT OUTER JOIN Corporates ON Tiers.id = Corporates.id";
            sql += " WHERE 1=1 ";
            if (activeContractsOnly)
                sql += " AND SavingContracts.[status]=1 ";
            if (!all)
            {
                sql += @" AND Tiers.branch_id in
                (
                    select
                    branch_id
                    from
                    dbo.UsersBranches
                    WHERE user_id = @user_id
                )";
            }
            sql += @") maTable";

            const string closeWhere = @" WHERE (contract_code LIKE @contractCode 
                                           OR client_name LIKE @clientName 
                                           OR user_name LIKE @userName 
                                           OR identification_data LIKE @numberPassport 
                                           OR loanofficer_name LIKE @loanofficerName )) maTable";

            QueryEntity q = new QueryEntity(pQuery, sql, closeWhere);
            string sqlText = q.ConstructSQLEntityNumberProxy();
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand select = new OpenCbsCommand(sqlText, conn))
            {
                foreach (var item in q.DynamiqParameters())
                {
                    select.AddParam(item.Key, string.Format("%{0}%", item.Value));
                }
                select.AddParam("@user_id", User.CurrentUser.Id);

                using (OpenCbsReader reader = select.ExecuteReader())
                {
                    if (reader.Empty) return 0;

                    reader.Read();
                    return reader.GetInt(0);
                }
            }
        }

        public List<SavingSearchResult> SearchSavingContractByCritere(int pPageNumber, string pQuery, bool all, bool activeContractOnly)
        {
            List<SavingSearchResult> list = new List<SavingSearchResult>();

            string sql = @"SELECT  
                                SavingContracts.id, 
                                SavingContracts.code AS contract_code, 
                                SavingContracts.status AS contract_status,
                                SavingContracts.creation_date as start_date, 
                                SavingContracts.closed_date as end_date, 
                                SavingProducts.product_type as product_type,
                                Persons.identification_data as identification_data, 
                                Tiers.client_type_code, 
                                Tiers.id AS client_id,
                                ISNULL(Users.first_name + SPACE(1) + Users.last_name, Users.user_name) AS user_name, 
                                ISNULL(Persons.first_name + SPACE(1) + Persons.last_name, ISNULL(Groups.name, Corporates.name)) AS client_name,
                                ISNULL(Users.first_name + SPACE(1) + Users.last_name, Users.user_name) AS loanofficer_name,
                                SavingContracts.user_id AS loan_officer_id,
                                SavingProducts.currency_id
                                FROM SavingContracts 
                                INNER JOIN SavingProducts ON SavingContracts.product_id = SavingProducts.id 
                                INNER JOIN Users ON SavingContracts.user_id = Users.id
                                INNER JOIN Tiers ON SavingContracts.tiers_id = Tiers.id
                                LEFT OUTER JOIN Persons ON Tiers.id = Persons.id 
                                LEFT OUTER JOIN Groups ON Tiers.id = Groups.id 
                                LEFT OUTER JOIN Corporates ON Tiers.id = Corporates.id";
            sql += " WHERE 1=1 ";
            if (activeContractOnly)
                sql += " AND SavingContracts.status=1 ";
            if (!all)
            {
                sql += @" AND Tiers.branch_id in (
                select branch_id
                from dbo.UsersBranches  
                WHERE user_id = @user_id
                )";
            }
            sql += ") maTable";

            const string closeWhere = @" WHERE ( contract_code LIKE @contractCode 
                                     OR client_name LIKE @clientName 
                                     OR user_name LIKE @UserName 
                                     OR identification_data LIKE @numberPassport 
                                     OR loanofficer_name LIKE @loanofficerName )) maTable";

            QueryEntity q = new QueryEntity(pQuery, sql, closeWhere);
            string pSqlText = q.ConstructSQLEntityByCriteresProxy(20, (pPageNumber - 1) * 20);

            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand select = new OpenCbsCommand(pSqlText, conn))
            {

                foreach (var item in q.DynamiqParameters())
                {
                    select.AddParam(item.Key, string.Format("%{0}%", item.Value));
                }
                select.AddParam("@user_id", User.CurrentUser.Id);

                using (OpenCbsReader reader = select.ExecuteReader())
                {
                    if (!reader.Empty)
                    {
                        while (reader.Read())
                        {
                            SavingSearchResult result =
                                new SavingSearchResult
                                    {
                                        Id = reader.GetInt("id"),
                                        ClientId = reader.GetInt("client_id"),
                                        ContractCode = reader.GetString("contract_code"),
                                        Status = (OSavingsStatus)reader.GetSmallInt("contract_status"),
                                        ClientTypeCode = reader.GetString("client_type_code")
                                    };
                            switch (result.ClientTypeCode)
                            {
                                case "I": result.ClientType = OClientTypes.Person; break;
                                case "G": result.ClientType = OClientTypes.Group; break;
                                case "V": result.ClientType = OClientTypes.Village; break;
                                case "C": result.ClientType = OClientTypes.Corporate; break;
                                default: result.ClientType = OClientTypes.Person; break;
                            }
                            result.ClientName = reader.GetString("client_name");
                            result.ContractStartDate = reader.GetDateTime("start_date");
                            result.ContractEndDate = reader.GetNullDateTime("end_date");
                            result.ContractType = reader.GetString("product_type");
                            result.LoanOfficer = new User { Id = reader.GetInt("loan_officer_id") };
                            result.CurrencyId = reader.GetInt("currency_id");
                            list.Add(result);
                        }
                    }
                }

                return list;
            }
        }

        public KeyValuePair<int, string>[] SelectClientSavingBookCodes(int tiersId, int? currencyId)
        {
            const string sqlText = @"SELECT sc.id, sc.code
                                    FROM   SavingContracts sc
                                    INNER JOIN SavingProducts sp
                                        ON sc.product_id = sp.id AND (@currency_id IS NULL OR sp.currency_id = @currency_id) 
                                    WHERE  tiers_id = @tiersId AND closed_date IS NULL";
            using (SqlConnection conn = GetConnection())
            using (var command = new OpenCbsCommand(sqlText, conn))
            {
                command.AddParam("tiersId", tiersId);
                command.AddParam("currency_id", currencyId);
                var result = new List<KeyValuePair<int, string>>();
                using (var reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        var savingId = reader.GetInt("id");
                        var savingCode = reader.GetString("code");
                        result.Add(
                            new KeyValuePair<int, string>(savingId, savingCode)
                            );
                    }
                    return result.ToArray();
                }
            }
        }

        public List<ISavingsContract> SelectSavings(int pClientId)
        {
            const string sqlText = @"SELECT    SavingContracts.[id]
                                              ,SavingContracts.[product_id]
                                              ,SavingContracts.[user_id]
                                              ,SavingContracts.[code]
                                              ,SavingContracts.[tiers_id]
                                              ,SavingContracts.[creation_date]
                                              ,SavingContracts.[interest_rate]
                                              ,SavingContracts.[status]
                                              ,SavingContracts.[closed_date]
                                              ,SavingContracts.[savings_officer_id]
                                              ,SavingContracts.[initial_amount]
                                              ,SavingContracts.[entry_fees]
                                              ,SavingContracts.[nsg_id]
                                              ,u2.first_name AS so_first_name
                                              ,u2.last_name AS so_last_name
                                              ,SavingProducts.product_type
	                                          ,Users.id AS user_id 
                                              ,Users.deleted
                                              ,Users.user_name 
                                              ,Users.user_pass 
                                              ,Users.role_code 
                                              ,Users.first_name 
                                              ,Users.last_name 
	                                FROM SavingContracts
                                    INNER JOIN SavingProducts ON SavingContracts.product_id = SavingProducts.id 
                                    INNER JOIN Users ON SavingContracts.user_id = Users.id
                                    INNER JOIN Users AS u2 ON u2.id = SavingContracts.savings_officer_id
	                                WHERE SavingContracts.tiers_id = @tiers_id";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand select = new OpenCbsCommand(sqlText, conn))
            {
                List<ISavingsContract> savings = new List<ISavingsContract>();
                select.AddParam("@tiers_id", pClientId);

                using (OpenCbsReader reader = select.ExecuteReader())
                {
                    if (reader.Empty) return new List<ISavingsContract>();

                    while (reader.Read())
                    {
                        savings.Add(GetSavingFromReader(reader));
                    }
                }
                foreach (ISavingsContract saving in savings)
                {
                    SelectSavingsBookContract((SavingBookContract)saving);
                    saving.Product = _savingProductManager.SelectSavingProduct(saving.Product.Id);
                    saving.Events.AddRange(_savingEventManager.SelectEvents(saving.Id, saving.Product));
                    ((SavingBookContract)saving).Loans = SelectLoansBySavingsId(saving.Id);
                }
                return savings;
            }
        }

        public int GetNumberOfSavings(int clientId)
        {
            const string sqlText = @"SELECT COUNT(id) FROM SavingContracts WHERE SavingContracts.tiers_id = @tiers_id";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand select = new OpenCbsCommand(sqlText, conn))
            {
                select.AddParam("@tiers_id", clientId);
                return (int)select.ExecuteScalar();
            }
        }

        public void UpdateNextMaturityForSavingBook(int savingId, DateTime? nextMaturity)
        {
            const string sqlText =
                            @"UPDATE SavingBookContracts
                             SET next_maturity = @nextMaturity 
                             WHERE id = @id";

            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand cmd = new OpenCbsCommand(sqlText, conn))
            {
                cmd.AddParam("@nextMaturity", nextMaturity);
                cmd.AddParam("@id", savingId);
                cmd.ExecuteNonQuery();
            }
       }

        public void UpdateNextMaturity(int savingId, DateTime? nextMaturity)
        {
            const string sqlText = @"UPDATE SavingDepositContracts 
                                     SET next_maturity = @nextMaturity 
                                     WHERE id = @id";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand update = new OpenCbsCommand(sqlText, conn))
            {
                update.AddParam("@nextMaturity", nextMaturity);
                update.AddParam("@id", savingId);
                update.ExecuteNonQuery();
            } 
        }

        public void UpdateStatus(int savingId, OSavingsStatus status, DateTime? closedDate)
        {
            const string sqlText = @"UPDATE SavingContracts SET status = @status, closed_date = @closedDate WHERE id = @id";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand update = new OpenCbsCommand(sqlText, conn))
            {
                update.AddParam("@status",  (int)status);
                update.AddParam("@closedDate", closedDate);
                update.AddParam("@id", savingId);
                update.ExecuteNonQuery();
            }
        }

        public void UpdateOverdraftStatus(int savingId, bool inOverdraft)
        {
            const string sqlText = @"UPDATE SavingBookContracts SET in_overdraft = @inOverdraft WHERE id = @id";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand update = new OpenCbsCommand(sqlText, conn))
            {
                update.AddParam("@id", savingId);
                update.AddParam("@inOverdraft", inOverdraft);

                update.ExecuteNonQuery();
            }
        }

        public void UpdateInitialData(int pSavingId, OCurrency initialAmount, OCurrency entryFees)
        {
            const string sqlText = @"UPDATE SavingContracts SET initial_amount = @initial_amount, entry_fees = @entry_fees WHERE id = @id";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand update = new OpenCbsCommand(sqlText, conn))
            {
                update.AddParam("@id", pSavingId);
                update.AddParam("@initial_amount", initialAmount);
               update.AddParam("@entry_fees", entryFees);

                update.ExecuteNonQuery();
            }
        }

        private SavingBookContract GetSavingFromReader(OpenCbsReader pReader)
        {
            var savingContract = new SavingBookContract(
                ApplicationSettings.GetInstance(_user.Md5),
                _user);
            savingContract.Product = new SavingsBookProduct
            {
                Id = pReader.GetInt("product_id")
            };

            savingContract.Id = pReader.GetInt("id");
            savingContract.Code = pReader.GetString("code");
            savingContract.Status = (OSavingsStatus)pReader.GetSmallInt("status");
            savingContract.CreationDate = pReader.GetDateTime("creation_date");
            savingContract.ClosedDate = pReader.GetNullDateTime("closed_date");
            savingContract.InterestRate = pReader.GetDouble("interest_rate");
            savingContract.SavingsOfficer = new User
            {
                Id = pReader.GetInt("savings_officer_id")
                , FirstName = pReader.GetString("so_first_name")
                , LastName = pReader.GetString("so_last_name")
            };
            savingContract.InitialAmount = pReader.GetMoney("initial_amount");
            savingContract.EntryFees = pReader.GetMoney("entry_fees");
            savingContract.NsgID = pReader.GetNullInt("nsg_id");

            return savingContract;
        }

        public void OnSavingSelected(SavingsContract saving)
        {
            if (null == SavingSelected) return;
            SavingSelected(saving);
        }
    }
}
