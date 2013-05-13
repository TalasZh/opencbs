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
using System.Linq;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.Enums;
using OpenCBS.Manager.Currencies;
using OpenCBS.Reports;
using OpenCBS.Shared;

namespace OpenCBS.Manager.Accounting
{
    /// <summary>
    /// MovementSetManagement contains all methods relative to selecting, inserting, updating
    /// and deleting movementSet objects to and from our database.
    /// </summary>
    public class AccountingTransactionManager : Manager
    {
        private readonly AccountManager _accountManagement;
        private CurrencyManager _currencyManager;
        private BranchManager _branchManager;

        public AccountingTransactionManager(User pUser) : base(pUser)
        {
            _accountManagement = new AccountManager(pUser);
            _currencyManager = new CurrencyManager(pUser);
            _branchManager = new BranchManager(pUser);
        }

        public AccountingTransactionManager(string testDb) : base(testDb)
        {
            _accountManagement = new AccountManager(testDb);
            _currencyManager = new CurrencyManager(testDb);
            _branchManager = new BranchManager(testDb);
        }

        private static Booking GetBooking(OpenCbsReader reader)
        {
            return new Booking
                       {
                           Id = reader.GetInt("id"),
                           Amount = reader.GetMoney("amount"),
                           IsExported = reader.GetBool("is_exported"),
                           DebitAccount =
                               new Account
                                   {
                                       Id = reader.GetInt("debit_account_number_id")
                                   },
                           CreditAccount =
                               new Account
                                   {
                                       Id = reader.GetInt("credit_account_number_id")
                                   },
                           EventId = reader.GetInt("event_id"),
                           ContractId = reader.GetInt("contract_id"),

                           Date = reader.GetDateTime("transaction_date"),
                           Currency =
                               new Currency
                                   {Id = reader.GetInt("currency_id")},
                           ExchangeRate = reader.GetDouble("exchange_rate"),
                           Description = reader.GetString("description"),
                           Branch = new Branch {Id = reader.GetInt("branch_id")}
                           
                       };
        }

        public List<Booking> SelectMovementsForReverse(List<ExchangeRate> rates, List<FiscalYear> fiscalYears)
        {
            string sqlText = @"SELECT  LoanAccountingMovements.id ,
                                                debit_account_number_id ,
                                                credit_account_number_id ,
                                                amount ,
                                                ContractEvents.cancel_date AS transaction_date ,
                                                export_date ,
                                                LoanAccountingMovements.is_exported ,
                                                currency_id ,
                                                exchange_rate ,
                                                ' ' AS [description],
                                                event_id ,
                                                Users.id AS user_id ,
                                                Users.deleted AS user_deleted ,
                                                Users.user_name AS user_username ,
                                                Users.user_pass AS user_password ,
                                                Users.role_code AS user_role ,
                                                Users.first_name AS user_firstname ,
                                                Users.last_name AS user_lastname,
                                                ContractEvents.contract_id,
                                                branch_id
                                        FROM    LoanAccountingMovements
                                                INNER JOIN ContractEvents ON dbo.LoanAccountingMovements.event_id = dbo.ContractEvents.id
                                                INNER JOIN Users ON [Users].id = ContractEvents.[user_id]
                                        WHERE ContractEvents.is_deleted = 1 
                                          AND ContractEvents.is_exported = 0";

            List<Booking> bookings = new List<Booking>();
            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand select = new OpenCbsCommand(sqlText, conn))
                {
                    using (OpenCbsReader reader = select.ExecuteReader())
                    {
                        if (!reader.Empty)
                        {
                            while (reader.Read())
                            {
                                Booking booking = GetBooking(reader);
                                booking.User = new User {UserName = reader.GetString("user_username")};
                                booking.User.SetRole(reader.GetString("user_role"));
                                booking.Type = OMovementType.Loan;
                                bookings.Add(booking);
                            }
                        }
                    }
                }
            }

            sqlText = @"SELECT  SavingsAccountingMovements.id ,
                                                debit_account_number_id ,
                                                credit_account_number_id ,
                                                SavingsAccountingMovements.amount,
                                                transaction_date ,
                                                export_date ,
                                                SavingsAccountingMovements.is_exported ,
                                                currency_id ,
                                                exchange_rate ,
                                                ' ' AS [description] ,
                                                event_id ,
                                                Users.id AS user_id ,
                                                Users.deleted AS user_deleted ,
                                                Users.user_name AS user_username ,
                                                Users.user_pass AS user_password ,
                                                Users.role_code AS user_role ,
                                                Users.first_name AS user_firstname ,
                                                Users.last_name AS user_lastname,
                                                SavingEvents.contract_id,
                                                branch_id
                                        FROM    dbo.SavingsAccountingMovements
                                                INNER JOIN SavingEvents ON dbo.SavingsAccountingMovements.event_id = SavingEvents.id
                                                INNER JOIN Users ON [Users].id = SavingEvents.[user_id]
                                        WHERE SavingEvents.deleted = 1 
                                          AND SavingEvents.is_exported = 0";

            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand select = new OpenCbsCommand(sqlText, conn))
                {
                    using (OpenCbsReader reader = select.ExecuteReader())
                    {
                        if (!reader.Empty)
                        {
                            while (reader.Read())
                            {
                                Booking booking = GetBooking(reader);
                                booking.User = new User {UserName = reader.GetString("user_username")};
                                booking.User.SetRole(reader.GetString("user_role"));
                                booking.Type = OMovementType.Saving;
                                bookings.Add(booking);
                            }
                        }
                    }
                }
            }

            foreach (Booking booking in bookings)
            {
                booking.CreditAccount = _accountManagement.Select(booking.DebitAccount.Id);
                booking.DebitAccount = _accountManagement.Select(booking.CreditAccount.Id); 
                booking.Currency = _currencyManager.SelectCurrencyById(booking.Currency.Id);
                booking.Branch = _branchManager.Select(booking.Branch.Id);
                booking.Name = booking.EventId + " <- [" + booking.Date + "]";

                //setting fiscal year
                booking.FiscalYear =
                    fiscalYears
                        .First(
                            f =>
                            f.OpenDate <= booking.Date.Date &&
                            (f.CloseDate == null || f.CloseDate >= booking.Date.Date)
                        );
                //setting xrate
                ExchangeRate rate = null;
                if (rates != null)
                    rate = rates.FirstOrDefault(r => r.Date.Date == booking.Date.Date);

                booking.ExchangeRate = booking.Currency.IsPivot ? 1 : rate == null ? 0 : rate.Rate;
            }
            return bookings;
        }

        public List<Booking> SelectClosureMovments(int closureId)
        {
            const string sqlText = @"SELECT 
                                      lam.id,
                                      lam.contract_id, 
                                      contract_code AS [description],
                                      debit_account_number_id,
                                      d_acc.account_number AS  debit_account_number,
                                      d_acc.label AS debit_label,
                                      credit_account_number_id,
                                      c_acc.account_number AS credit_account_number,
                                      c_acc.label AS credit_label,
                                      amount,
                                      event_id,
                                      ce.event_type,
                                      transaction_date,
                                      export_date,
                                      lam.is_exported,
                                      currency_id,
                                      curr.code AS currency_code,
                                      exchange_rate,
                                      rule_id,
                                      branch_id,
                                      b.code AS branch_code,
                                       b.name AS branch_name,
                                      'L' AS [type]
                                    FROM dbo.LoanAccountingMovements lam
                                    INNER JOIN dbo.Contracts c ON lam.contract_id = c.id
                                    INNER JOIN dbo.ContractEvents ce ON ce.id = lam.event_id
                                    INNER JOIN dbo.Currencies curr ON lam.currency_id = curr.id
                                    INNER JOIN dbo.Branches b ON lam.branch_id = b.id
                                    INNER JOIN dbo.ChartOfAccounts c_acc ON c_acc.id = lam.credit_account_number_id
                                    INNER JOIN dbo.ChartOfAccounts d_acc ON d_acc.id = lam.debit_account_number_id
                                    WHERE closure_id = @closure_id
                                    UNION
                                    SELECT 
                                      sam.id,
                                      sam.contract_id, 
                                      c.code AS [description],
                                      debit_account_number_id,
                                      d_acc.account_number AS  debit_account_number,
                                      d_acc.label AS debit_label,
                                      credit_account_number_id,
                                      c_acc.account_number AS credit_account_number,
                                      c_acc.label AS credit_label,
                                      sam.amount,
                                      event_id,
                                      se.code AS event_type,
                                      transaction_date,
                                      export_date,
                                      sam.is_exported,
                                      currency_id,
                                      curr.code AS currency_code,
                                      exchange_rate,
                                      rule_id,
                                      branch_id,
                                      b.code AS branch_code,
                                      b.name AS branch_name,
                                      'S' AS [type]
                                    FROM dbo.SavingsAccountingMovements sam
                                    INNER JOIN dbo.SavingContracts c ON sam.contract_id = c.id
                                    INNER JOIN dbo.SavingEvents se ON se.id = sam.event_id
                                    INNER JOIN dbo.Currencies curr ON sam.currency_id = curr.id
                                    INNER JOIN dbo.Branches b ON branch_id = b.id
                                    INNER JOIN dbo.ChartOfAccounts c_acc ON c_acc.id = sam.credit_account_number_id
                                    INNER JOIN dbo.ChartOfAccounts d_acc ON d_acc.id = sam.debit_account_number_id
                                    WHERE closure_id = @closure_id
                                    UNION
                                    SELECT 
                                      mam.id,
                                      0 AS contract_id,
                                      mam.description,
                                      debit_account_number_id,
                                      d_acc.account_number AS  debit_account_number,
                                      d_acc.label AS debit_label,
                                      credit_account_number_id,
                                      c_acc.account_number AS credit_account_number,
                                      c_acc.label AS credit_label,
                                      mam.amount,
                                      event_id,
                                      'Manual' AS event_type,
                                      transaction_date,
                                      export_date,
                                      mam.is_exported,
                                      currency_id,
                                      curr.code AS currency_code,
                                      exchange_rate,
                                      0 AS rule_id,
                                      branch_id AS branch_code,
                                      b.name AS branch_name,
                                      b.code,
                                      'M' AS [type]
                                    FROM dbo.ManualAccountingMovements mam
                                    INNER JOIN dbo.Currencies curr ON mam.currency_id = curr.id
                                    INNER JOIN dbo.Branches b ON branch_id = b.id
                                    INNER JOIN dbo.ChartOfAccounts c_acc ON c_acc.id = mam.credit_account_number_id
                                    INNER JOIN dbo.ChartOfAccounts d_acc ON d_acc.id = mam.debit_account_number_id
                                    WHERE closure_id = @closure_id";

            List<Booking> bookings = new List<Booking>();
            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand select = new OpenCbsCommand(sqlText, conn))
                {
                    select.AddParam("@closure_id", closureId);
                    using (OpenCbsReader reader = select.ExecuteReader())
                    {
                        if (!reader.Empty)
                        {
                            while (reader.Read())
                            {
                                Booking booking = GetBooking(reader);
                                booking.DebitAccount.Number = reader.GetString("debit_account_number");
                                booking.CreditAccount.Number = reader.GetString("credit_account_number");
                                booking.EventType = reader.GetString("event_type");
                                booking.Currency.Code = reader.GetString("currency_code");
                                booking.Branch.Code = reader.GetString("branch_code");
                                booking.Branch.Name = reader.GetString("branch_name");
                                bookings.Add(booking);
                            }
                        }
                    }
                }
            }

            foreach (Booking booking in bookings)
            {
                booking.Name = booking.EventId + " <- [" + booking.Date + "]";
            }

            return bookings;
        }

        public List<Booking> SelectManualMovements(bool all, List<ExchangeRate> rates, List<FiscalYear> fiscalYears)
        {
            const string sqlText = @"SELECT 
                                      ManualAccountingMovements.id,
                                      debit_account_number_id,
                                      credit_account_number_id,
                                      amount,
                                      transaction_date,
                                      export_date,
                                      is_exported,
                                      currency_id,
                                      exchange_rate,
                                      [description],
                                      event_id,
                                      0 AS contract_id,
                                      Users.id AS user_id, 
                                      Users.deleted AS user_deleted, 
                                      Users.user_name AS user_username, 
                                      Users.user_pass AS user_password, 
                                      Users.role_code AS user_role, 
                                      Users.first_name AS user_firstname, 
                                      Users.last_name AS user_lastname,
                                      branch_id
                                    FROM dbo.ManualAccountingMovements
                                    INNER JOIN Users ON [Users].id = ManualAccountingMovements.[user_id]
                                    WHERE closure_id = 0 OR @all = 1";

            List<Booking> bookings = new List<Booking>();
            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand select = new OpenCbsCommand(sqlText, conn))
                {
                    select.AddParam("@all", all);

                    using (OpenCbsReader reader = select.ExecuteReader())
                    {
                        if (reader.Empty) return new List<Booking>();
                        while (reader.Read())
                        {
                            Booking booking = GetBooking(reader);
                            booking.User = new User { UserName = reader.GetString("user_username") };
                            booking.User.SetRole(reader.GetString("user_role"));
                            booking.Type = OMovementType.Manual;
                            booking.EventType = "Manual";
                            booking.Name = reader.GetString("description");

                            //setting fiscal year
                            booking.FiscalYear =
                                fiscalYears
                                    .First(
                                        f =>
                                        f.OpenDate <= booking.Date.Date &&
                                        (f.CloseDate == null || f.CloseDate >= booking.Date.Date)
                                    );
                            //setting xrate
                            ExchangeRate rate = null;
                            if (rates != null)
                                rate = rates.FirstOrDefault(r => r.Date.Date == booking.Date.Date);

                            booking.ExchangeRate = booking.Currency.IsPivot ? 1 : rate == null ? 0 : rate.Rate;
                            bookings.Add(booking);
                        }
                    }

                    foreach (Booking booking in bookings)
                    {
                        booking.CreditAccount = _accountManagement.Select(booking.CreditAccount.Id);
                        booking.DebitAccount = _accountManagement.Select(booking.DebitAccount.Id);
                        booking.Currency = _currencyManager.SelectCurrencyById(booking.Currency.Id);
                        booking.Branch = _branchManager.Select(booking.Branch.Id);

                        //setting fiscal year
                        booking.FiscalYear =
                            fiscalYears
                                .First(
                                    f =>
                                    f.OpenDate <= booking.Date.Date &&
                                    (f.CloseDate == null || f.CloseDate >= booking.Date.Date)
                                );
                        //setting xrate
                        ExchangeRate rate = null;
                        if (rates != null)
                            rate = rates.FirstOrDefault(r => r.Date.Date == booking.Date.Date);

                        booking.ExchangeRate = booking.Currency.IsPivot ? 1 : rate == null ? 0 : rate.Rate;
                    }

                    return bookings;
                }
            }
        }

        public Booking SelectProvisionMovments(int contractId, List<ExchangeRate> rates, List<FiscalYear> fiscalYears)
        {
            const string sqlText = @"SELECT  LoanAccountingMovements.id ,
                                        debit_account_number_id ,
                                        credit_account_number_id ,
                                        amount ,
                                        ContractEvents.cancel_date AS transaction_date ,
                                        export_date ,
                                        LoanAccountingMovements.is_exported ,
                                        currency_id ,
                                        exchange_rate ,
                                        Contracts.contract_code AS [description] ,
                                        event_id ,
                                        Users.id AS user_id ,
                                        Users.deleted AS user_deleted ,
                                        Users.user_name AS user_username ,
                                        Users.user_pass AS user_password ,
                                        Users.role_code AS user_role ,
                                        Users.first_name AS user_firstname ,
                                        Users.last_name AS user_lastname ,
                                        ContractEvents.contract_id ,
                                        branch_id
                                FROM    LoanAccountingMovements
                                        INNER JOIN ContractEvents 
                                          ON dbo.LoanAccountingMovements.event_id = dbo.ContractEvents.id
                                        INNER JOIN Users ON [Users].id = ContractEvents.[user_id]
                                        INNER JOIN dbo.Contracts ON dbo.ContractEvents.contract_id = dbo.Contracts.id
                                WHERE   ContractEvents.is_deleted = 1
                                        AND ContractEvents.is_exported = 0
                                        AND dbo.ContractEvents.id IN (
                                        SELECT  MAX(ContractEvents.id) AS event_id
                                        FROM    ContractEvents
                                                INNER JOIN ProvisionEvents ON ContractEvents.id = ProvisionEvents.id
                                        WHERE   ContractEvents.is_exported = 1
                                                AND event_type = @event_type
                                                AND contract_id = @contract_id)";

            Booking booking = null;
            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand select = new OpenCbsCommand(sqlText, conn))
                {
                    select.AddParam("@event_type", "LLPE");
                    select.AddParam("@contract_id", contractId);
                    using (OpenCbsReader reader = select.ExecuteReader())
                    {
                        if (reader.Empty) return null;
                        while (reader.Read())
                        {
                            booking = GetBooking(reader);
                            booking.User.SetRole(reader.GetString("user_role"));
                            booking.Type = OMovementType.Loan;
                        }
                    }

                    //oposite
                    if (booking != null)
                    {
                        booking.CreditAccount = _accountManagement.Select(booking.DebitAccount.Id);
                        booking.DebitAccount = _accountManagement.Select(booking.CreditAccount.Id);

                        booking.Currency = _currencyManager.SelectCurrencyById(booking.Currency.Id);
                        booking.Branch = _branchManager.Select(booking.Branch.Id);
                        booking.Name = booking.Description + " [" + booking.Date + "]";

                        //setting fiscal year
                        booking.FiscalYear =
                            fiscalYears
                                .First(
                                    f =>
                                    f.OpenDate <= booking.Date.Date &&
                                    (f.CloseDate == null || f.CloseDate >= booking.Date.Date)
                                );
                        //setting xrate
                        ExchangeRate rate = null;
                        if (rates != null)
                            rate = rates.FirstOrDefault(r => r.Date.Date == booking.Date.Date);

                        booking.ExchangeRate = booking.Currency.IsPivot ? 1 : rate == null ? 0 : rate.Rate;
                    }

                    return booking;
                }
            }
        }

        public Booking SelectBookingByEventId(int eventId)
        {
            const string sqlText = @"SELECT 
                                      ManualAccountingMovements.id,
                                      debit_account_number_id,
                                      credit_account_number_id,
                                      amount,
                                      transaction_date,
                                      export_date,
                                      is_exported,
                                      currency_id,
                                      exchange_rate,
                                      [description],
                                      event_id,
                                      0 AS contract_id,
                                      Users.id AS user_id, 
                                      Users.deleted AS user_deleted, 
                                      Users.user_name AS user_username, 
                                      Users.user_pass AS user_password, 
                                      Users.role_code AS user_role, 
                                      Users.first_name AS user_firstname, 
                                      Users.last_name AS user_lastname,
                                      branch_id
                                    FROM dbo.ManualAccountingMovements
                                    INNER JOIN Users ON [Users].id = ManualAccountingMovements.[user_id]
                                    WHERE event_id = @event_id";

            Booking booking = new Booking();
            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand select = new OpenCbsCommand(sqlText, conn))
                {
                    select.AddParam("@event_id", eventId);
                    using (OpenCbsReader reader = select.ExecuteReader())
                    {
                        if (reader.Empty) return null;
                        while (reader.Read())
                        {
                            booking = GetBooking(reader);
                            booking.User = new User
                                               {
                                                   Id = reader.GetInt("user_id"),
                                                   IsDeleted = reader.GetBool("user_deleted"),
                                                   Password = reader.GetString("user_password"),
                                                   UserName = reader.GetString("user_username"),
                                                   FirstName = reader.GetString("user_firstname"),
                                                   LastName = reader.GetString("user_lastname")
                                               };

                            booking.User.SetRole(reader.GetString("user_role"));
                        }
                    }

                    booking.CreditAccount = _accountManagement.Select(booking.CreditAccount.Id);
                    booking.DebitAccount = _accountManagement.Select(booking.DebitAccount.Id);
                    booking.Currency = _currencyManager.SelectCurrencyById(booking.Currency.Id);
                    booking.Branch = _branchManager.Select(booking.Branch.Id);
                    return booking;
                }
            }
        }

        public List<string> SelectExportAccountingProcNames()
        {
            const string sqlText = @"SELECT name
                                     FROM sys.objects
                                     WHERE type = 'P' AND name LIKE '%ExportAccounting_%'";

            List<string> names = new List<string>();
            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand select = new OpenCbsCommand(sqlText, conn))
                {
                    using (OpenCbsReader reader = select.ExecuteReader())
                    {
                        if (reader.Empty) return null;
                        while (reader.Read())
                        {
                            names.Add(reader.GetString("name"));
                        }
                    }

                    return names;
                }
            }
        }

        public Dictionary<string, string> SelectExportAccountingProcParams(string procName)
        {
            const string sqlText = @"SELECT 
                                 PARAMETER_NAME, 
                                 DATA_TYPE 
                               FROM information_schema.parameters
                               WHERE specific_name = @proc_name";

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand select = new OpenCbsCommand(sqlText, conn))
                {
                    select.AddParam("@proc_name", procName);

                    using (OpenCbsReader reader = select.ExecuteReader())
                    {
                        if (reader.Empty) return null;
                        
                        while (reader.Read())
                        {
                            parameters.Add(reader.GetString("PARAMETER_NAME"), reader.GetString("DATA_TYPE"));
                        }
                    }

                    return parameters;
                }
            }
        }

        public DataTable SelectBookingsToExport(string procName, List<ReportParamV2> procParams, out DataTable idTable)
        {
            DataSet dataSet = new DataSet();
            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand cmdSelect = new SqlCommand(procName, conn))
                {
                    cmdSelect.CommandType = CommandType.StoredProcedure;

                    if (procParams != null && procParams.Count > 0)
                    {
                        foreach (ReportParamV2 procParam in procParams)
                        {
                            cmdSelect.Parameters.AddWithValue(procParam.Name, procParam.Value);
                        }
                    }

                    SqlDataAdapter da = new SqlDataAdapter(cmdSelect);
                    da.Fill(dataSet);
                }
            }

            DataTableCollection tables = dataSet.Tables;
            int tableCount = tables.Count;
            switch (tableCount)
            {
                case 0:
                    idTable = null;
                    return null;
                case 1:
                    idTable = null;
                    return tables[0];
                default:
                    idTable = tables[1];
                    return tables[0];
            }
        }

        public void UpdateSelectedBooking(string idSet, bool pIsExported, int transactioType)
        {
            string sqlText;

            if (transactioType == 0)
            {
                sqlText = @"UPDATE 
                              [LoanAccountingMovements] 
                           SET [is_exported] = @isExported, 
                              export_date = @exportDate
                           WHERE id IN (" +
                    idSet + ")";
                using (SqlConnection conn = GetConnection())
                {
                    using (OpenCbsCommand update = new OpenCbsCommand(sqlText, conn))
                    {
                        update.CommandTimeout = 0;

                        update.AddParam("@isExported", pIsExported);
                        update.AddParam("@exportDate", TimeProvider.Now);
                        update.ExecuteNonQuery();
                    }
                }
            }

            if (transactioType == 1)
            {
                sqlText =
                    @"UPDATE 
                          [SavingsAccountingMovements] 
                       SET [is_exported] = @isExported, 
                          export_date = @exportDate
                       WHERE id IN (" +
                    idSet + ")";
                using (SqlConnection conn = GetConnection())
                {
                    using (OpenCbsCommand update = new OpenCbsCommand(sqlText, conn))
                    {
                        update.CommandTimeout = 0;
                        update.AddParam("@isExported", pIsExported);
                        update.AddParam("@exportDate", TimeProvider.Now);
                        update.ExecuteNonQuery();
                    }
                }
            }

            if (transactioType == 2)
            {
                sqlText =
                    @"UPDATE 
                          [ManualAccountingMovements] 
                       SET [is_exported] = @isExported, 
                          export_date = @exportDate
                       WHERE id IN (" +
                    idSet + ")";

                using (SqlConnection conn = GetConnection())
                {
                    using (OpenCbsCommand update = new OpenCbsCommand(sqlText, conn))
                    {
                        update.CommandTimeout = 0;
                        update.AddParam("@isExported", pIsExported);
                        update.AddParam("@exportDate", TimeProvider.Now);
                        update.ExecuteNonQuery();
                    }
                }
            }
        }

        public BookingToViewStock SelectBookings(Account pAccount, DateTime pBeginDate, DateTime pEndDate,
                                                 int currencyId, OBookingTypes pBookingType, int pBranchId)
        {
            const string sqlQuery = "GetAccountBookings";

            bool? isExported = null;
            switch (pBookingType)
            {
                case OBookingTypes.Exported:
                    isExported = true;
                    break;
                case OBookingTypes.NotExported:
                    isExported = false;
                    break;
            }

            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand cmd = new OpenCbsCommand(sqlQuery, conn).AsStoredProcedure())
                {
                    cmd.AddParam("@beginDate",pBeginDate);
                    cmd.AddParam("@endDate", pEndDate);
                    cmd.AddParam("@account_id",  pAccount.Id);
                    cmd.AddParam("@currency_id", currencyId);
                    cmd.AddParam("@is_exported",  isExported);
                    cmd.AddParam("@branch_id",  pBranchId);

                    using (OpenCbsReader reader = cmd.ExecuteReader())
                    {
                        if (reader == null || reader.Empty) return new BookingToViewStock(pAccount);

                        BookingToViewStock stock = new BookingToViewStock(pAccount);
                        while (reader.Read())
                        {
                            stock.Add(GetBooking(pAccount, reader));
                        }
                        return stock;
                    }
                }
            
        }

        public List<Account> GetTrialBalance(DateTime pBeginDate, DateTime pEndDate, int currencyId, int pBranchId)
        {
            const string sqlQuery =
                @"	DECLARE @_from DATETIME
	                                    DECLARE @_to DATETIME
	                                    SET @_from = CAST(ROUND(CAST(@from AS FLOAT), 0) AS DATETIME)
	                                    SET @_to = CAST(ROUND(CAST(@to AS FLOAT), 0) AS DATETIME)
	                                    DECLARE @from_next_day DATETIME
	                                    SET @from_next_day = DATEADD(dd, 1, @_from)

	                                    SELECT tb1.account_id
		                                    , tb1.account_number
		                                    , tb1.account_label
		                                    , ac.category_name
                                            , CONVERT(INT, ac.category_id) AS category_id
		                                    , am.debit
		                                    , am.credit
		                                    , tb1.balance opening_balance
		                                    , tb2.balance closing_balance
		                                    , am.agg_debit
		                                    , am.agg_credit
		                                    , tb1.agg_balance agg_opening_balance
		                                    , tb2.agg_balance agg_closing_balance
		                                    , coa.indentation
	                                    FROM dbo.TrialBalance(@_from, @disbursed_in, @display_in, 0, @branch_id, 7) tb1
	                                    LEFT JOIN dbo.TrialBalance(@_to, @disbursed_in, @display_in, 0, @branch_id, 7) tb2 ON tb2.account_id = tb1.account_id
	                                    LEFT JOIN dbo.AccountMovements(@from_next_day, @_to, @disbursed_in, @display_in, 0, @branch_id, 7) am ON am.account_id = tb1.account_id
	                                    LEFT JOIN 
	                                    (	SELECT id category_id
			                                    , name category_name
			                                    , CASE
				                                    WHEN name LIKE '%asset%' THEN 1
				                                    WHEN name LIKE '%liab%' THEN 2
				                                    WHEN name LIKE '%income%' OR name LIKE '%revenue%' THEN 3
				                                    ELSE 4
			                                    END category_order
		                                    FROM dbo.AccountsCategory
	                                    ) ac ON ac.category_id = tb1.account_category_id
	                                    LEFT JOIN
	                                    (
		                                    SELECT COUNT(coa2.id)-1 indentation, coa1.id, coa1.lft
		                                    FROM dbo.ChartOfAccounts coa1, dbo.ChartOfAccounts coa2
		                                    WHERE coa1.lft BETWEEN coa2.lft AND coa2.rgt
		                                    GROUP BY coa1.id, coa1.label, coa1.lft		
	                                    ) coa ON coa.id = tb1.account_id
	                                    ORDER BY ac.category_order, coa.lft
                                    ";

            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand select = new OpenCbsCommand(sqlQuery, conn))
                {
                    select.AddParam("@from", pBeginDate);
                    select.AddParam("@to", pEndDate);
                    select.AddParam("@disbursed_in", 0);
                    select.AddParam("@display_in", currencyId);
                    select.AddParam("@branch_id", pBranchId);

                    using (OpenCbsReader reader = select.ExecuteReader())
                    {
                        if (reader.Empty)
                            return null;

                        List<Account> list = new List<Account>();
                        while (reader.Read())
                        {
                            OAccountCategories categ = OAccountCategories.Other;
                            switch (reader.GetInt("category_id"))
                            {
                                case 1:
                                    categ = OAccountCategories.BalanceSheetAsset;
                                    break;
                                case 2:
                                    categ = OAccountCategories.BalanceSheetLiabilities;
                                    break;
                                case 3:
                                    categ = OAccountCategories.ProfitAndLossIncome;
                                    break;
                                case 4:
                                    categ = OAccountCategories.ProfitAndLossExpense;
                                    break;
                            }

                            list.Add(new Account
                                         {
                                             Id = reader.GetInt("account_id"),
                                             Number = reader.GetString("account_number"),
                                             Label = reader.GetString("account_label"),
                                             AccountCategory = categ,
                                             OpenBalance = reader.GetMoney("closing_balance")
                                         });
                        }

                        foreach (Account account in list)
                        {
                            account.DebitPlus = _accountManagement.Select(account.Id).DebitPlus;
                        }
                        return list;
                    }
                }
            }
        }

        private static BookingToView GetBooking(Account pAccount, OpenCbsReader reader)
        {
            return new BookingToView
                       {
                           Date = reader.GetDateTime("date"),
                           EventCode = reader.GetString("event_code"),
                           ExchangeRate = reader.GetNullDouble("exchange_rate"),
                           AmountInternal = reader.GetMoney("amount"),
                           ContractCode = reader.GetString("contract_code"),
                           Direction =
                               (reader.GetString("debit_local_account_number") == pAccount.Number
                                    ? OBookingDirections.Debit
                                    : OBookingDirections.Credit),
                           IsExported = reader.GetBool("is_exported")
                       };

        }

        public OCurrency GetAccountBalance(int accountId, int currencyId, int contractId, int? mode,
                                           int toSumParent, int branchId)
        {
            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand cmd = new OpenCbsCommand("GetAccountBalance", conn).AsStoredProcedure())
                {
                    cmd.AddParam("@account_number_id",  accountId);
                    cmd.AddParam("@currency_id",  currencyId);
                    cmd.AddParam("@contract_id",  contractId);
                    cmd.AddParam("@mode",  mode);
                    cmd.AddParam("@to_sum_parent",  toSumParent);
                    cmd.AddParam("@branch_id",  branchId);

                    using (OpenCbsReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Empty) return 0;
                        OCurrency balance = 0;
                        while (reader.Read())
                        {
                            balance = reader.GetMoney("balance");
                        }
                        return balance;
                    }
                }
            }
        }

        public OCurrency GetAccountCategoryBalance(int accountId, int currencyId, int contractId, int? mode)
        {
            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand command = new OpenCbsCommand("GetBalanceByAccountCategory", conn).AsStoredProcedure())
                {
                    command.AddParam("@account_category_id",  accountId);
                    command.AddParam("@currency_id",  currencyId);
                    command.AddParam("@contract_id",  contractId);
                    command.AddParam("@mode", mode);

                    using (OpenCbsReader reader = command.ExecuteReader())
                    {
                        if (reader == null || reader.Empty) return 0;
                        OCurrency balance = 0;
                        while (reader.Read())
                        {
                            balance = reader.GetMoney("balance");
                        }
                        return balance;
                    }
                }
            }
        }


        public void InsertManualMovment(Booking booking, SqlTransaction sqlTransaction)
        {
            const string sqlText =
                @"INSERT INTO dbo.ManualAccountingMovements
                               ( debit_account_number_id,
                                  credit_account_number_id,
                                  amount,
                                  transaction_date,
                                  currency_id,
                                  exchange_rate,
                                  description,
                                  user_id,
                                  is_exported,
                                  event_id,
                                  branch_id,
                                  closure_id)
                               VALUES  
                               ( @debit_account_number_id, 
                                 @credit_account_number_id, 
                                 @amount, 
                                 @transaction_date, 
                                 @currency_id, 
                                 @exchange_rate, 
                                 @description,
                                 @user_id,
                                 0,
                                 @event_id,
                                 @branch_id,
                                 @closure_id)";

            using (OpenCbsCommand command = new OpenCbsCommand(sqlText, sqlTransaction.Connection, sqlTransaction))
            {
                command.AddParam("@debit_account_number_id", booking.DebitAccount.Id);
                command.AddParam("@credit_account_number_id", booking.CreditAccount.Id);
                command.AddParam("@amount", booking.Amount.Value);
                command.AddParam("@transaction_date", booking.Date);
                command.AddParam("@currency_id", booking.Currency.Id);
                command.AddParam("@exchange_rate", booking.ExchangeRate);
                command.AddParam("@description", booking.Description);
                command.AddParam("@user_id", booking.User.Id);
                command.AddParam("@event_id", booking.EventId);
                command.AddParam("@branch_id", booking.Branch.Id);
                command.AddParam("@closure_id", booking.ClosureId);

                command.ExecuteNonQuery();
            }
        }

        public void InsertLoanMovement(Booking booking, SqlTransaction sqlTransaction)
        {
            const string sqlText =
                @"INSERT INTO dbo.LoanAccountingMovements
                               ( debit_account_number_id,
                                  credit_account_number_id,
                                  amount,
                                  transaction_date,
                                  currency_id,
                                  exchange_rate,
                                  is_exported,
                                  event_id,
                                  contract_id,
                                  rule_id,
                                  branch_id,
                                  closure_id,
                                  fiscal_year_id,
                                  booking_type)
                               VALUES  
                               ( @debit_account_number_id, 
                                 @credit_account_number_id, 
                                 @amount, 
                                 @transaction_date, 
                                 @currency_id, 
                                 @exchange_rate,
                                 0,
                                 @event_id,
                                 @contract_id,
                                 @rule_id,
                                 @branch_id,
                                 @closure_id,
                                 @fiscal_year_id,
                                 @booking_type)";
            using (OpenCbsCommand cmd = new OpenCbsCommand(sqlText, sqlTransaction.Connection, sqlTransaction))
            {
                cmd.AddParam("@debit_account_number_id", booking.DebitAccount.Id);
                cmd.AddParam("@credit_account_number_id", booking.CreditAccount.Id);
                cmd.AddParam("@amount", booking.Amount.Value);
                cmd.AddParam("@transaction_date", booking.Date);
                cmd.AddParam("@currency_id", booking.Currency.Id);
                cmd.AddParam("@exchange_rate", booking.ExchangeRate);
                cmd.AddParam("@event_id", booking.EventId);
                cmd.AddParam("@contract_id", booking.ContractId);
                cmd.AddParam("@branch_id", booking.Branch.Id);
                cmd.AddParam("@closure_id", booking.ClosureId);
                cmd.AddParam("@fiscal_year_id", booking.FiscalYear.Id);
                cmd.AddParam("@booking_type", (int)booking.Type);

                if (booking.RuleId != 0)
                {
                    cmd.AddParam("@rule_id", booking.RuleId);
                }
                else
                {
                    cmd.AddParam("@rule_id", null);
                }

                cmd.ExecuteNonQuery();
            }
        }

        public void InsertSavingMovment(Booking booking, SqlTransaction sqlTransaction)
        {
            const string sqlText =
                @"INSERT INTO dbo.SavingsAccountingMovements
                               ( debit_account_number_id,
                                  credit_account_number_id,
                                  amount,
                                  transaction_date,
                                  currency_id,
                                  exchange_rate,
                                  is_exported,
                                  event_id,
                                  contract_id,
                                  rule_id,                                  
                                  branch_id,
                                  closure_id,
                                  fiscal_year_id)
                               VALUES  
                               ( @debit_account_number_id, 
                                 @credit_account_number_id, 
                                 @amount, 
                                 @transaction_date, 
                                 @currency_id, 
                                 @exchange_rate, 
                                 0,
                                 @event_id,
                                 @contract_id,
                                 @rule_id,
                                 @branch_id,
                                 @closure_id,
                                 @fiscal_year_id)";
            using (OpenCbsCommand command = new OpenCbsCommand(sqlText, sqlTransaction.Connection, sqlTransaction))
            {
                command.AddParam("@debit_account_number_id", booking.DebitAccount.Id);
                command.AddParam("@credit_account_number_id", booking.CreditAccount.Id);
                command.AddParam("@amount", booking.Amount.Value);
                command.AddParam("@transaction_date", booking.Date);
                command.AddParam("@currency_id", booking.Currency.Id);
                command.AddParam("@exchange_rate", booking.ExchangeRate);
                command.AddParam("@event_id", booking.EventId);
                command.AddParam("@contract_id", booking.ContractId);
                command.AddParam("@branch_id", booking.Branch.Id);
                command.AddParam("@closure_id", booking.ClosureId);
                command.AddParam("@fiscal_year_id", booking.FiscalYear.Id);

                if (booking.RuleId != 0)
                {
                    command.AddParam("@rule_id", booking.RuleId);
                }
                else
                {
                    command.AddParam("@rule_id", null);
                }

                command.ExecuteNonQuery();
            }
        }

        public int CreateClosure(int countOfTransactions)
        {
            const string sqlText =
                @"INSERT INTO dbo.AccountingClosure
                                                ( user_id ,
                                                  date_of_closure ,
                                                  count_of_transactions
                                                )
                                        VALUES  (@user_id,
                                                 @date,
                                                 @count
                                                )
                                     SELECT SCOPE_IDENTITY()";
            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand command = new OpenCbsCommand(sqlText, conn))
                {
                    command.AddParam("@user_id", User.CurrentUser.Id);
                    command.AddParam("@date", TimeProvider.Now);
                    command.AddParam("@count", countOfTransactions);

                    return int.Parse(command.ExecuteScalar().ToString());
                }
            }
        }

        public void DeleteClosure(int closureId, SqlTransaction sqlTransaction)
        {
            string sql =
                @"UPDATE  ContractEvents
                            SET     is_exported = 0
                            WHERE   id IN ( SELECT  event_id
                                            FROM    LoanAccountingMovements
                                            WHERE   closure_id = @closure_id
                                            GROUP BY event_id )";

            using (OpenCbsCommand command = new OpenCbsCommand(sql, sqlTransaction.Connection, sqlTransaction))
            {
                command.AddParam("@closure_id", closureId);
                command.ExecuteNonQuery();
            }

            sql =
                @"UPDATE  SavingEvents
                            SET     is_exported = 0
                            WHERE   id IN ( SELECT  event_id
                                            FROM    dbo.SavingsAccountingMovements
                                            WHERE   closure_id = @closure_id
                                            GROUP BY event_id )";

            using (OpenCbsCommand command = new OpenCbsCommand(sql, sqlTransaction.Connection, sqlTransaction))
            {
                command.AddParam("@closure_id", closureId);
                command.ExecuteNonQuery();
            }

            sql = @"DELETE FROM LoanAccountingMovements WHERE closure_id = 1";

            using (OpenCbsCommand command = new OpenCbsCommand(sql, sqlTransaction.Connection, sqlTransaction))
            {
                command.AddParam("@closure_id", closureId);
                command.ExecuteNonQuery();
            }

            sql = @"DELETE FROM SavingsAccountingMovements WHERE closure_id = 1";

            using (OpenCbsCommand command = new OpenCbsCommand(sql, sqlTransaction.Connection, sqlTransaction))
            {
                command.AddParam("@closure_id", closureId);
                command.ExecuteNonQuery();
            }

            sql =
                @" UPDATE dbo.AccountingClosure
                     SET date_of_closure = @date_of_closure,
                         user_id = @user_id,
                         is_deleted = 1
                     WHERE id = @closure_id";

            using (OpenCbsCommand command = new OpenCbsCommand(sql, sqlTransaction.Connection, sqlTransaction))
            {
                command.AddParam("@closure_id", closureId);
                command.AddParam("@date_of_closure", TimeProvider.Now);
                command.AddParam("@user_id", User.CurrentUser.Id);

                command.ExecuteNonQuery();
            }
        }

        public bool IsDeletable(int closureId)
        {
            const string sqlText =
                @"SELECT closure_id
                                    FROM dbo.LoanAccountingMovements
                                    WHERE closure_id = @closure_id
                                      AND is_exported = 1
                                    UNION ALL
                                    SELECT closure_id
                                    FROM dbo.LoanAccountingMovements
                                    WHERE closure_id = @closure_id
                                      AND is_exported = 1";

            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand select = new OpenCbsCommand(sqlText, conn))
                {
                    select.AddParam("@closure_id", closureId);
                    using (OpenCbsReader reader = select.ExecuteReader())
                    {
                        return reader.Empty;
                    }
                }
            }
        }

        public void UpdateManualMovment(Booking booking, SqlTransaction sqlTransaction)
        {
            const string sqlText =
                @"UPDATE dbo.ManualAccountingMovements
                    SET closure_id = @closure_id,
                       fiscal_year_id = @fiscal_year_id
                    WHERE id = @id";

            using (OpenCbsCommand command = new OpenCbsCommand(sqlText, sqlTransaction.Connection, sqlTransaction))
            {
                command.AddParam("@id", booking.Id);
                command.AddParam("@closure_id", booking.ClosureId);
                command.AddParam("@fiscal_year_id", booking.FiscalYear.Id);
                command.ExecuteNonQuery();
            }
        }

        public List<AccountingClosure> SelectAccountingClosures()
        {
            const string sqlText = @"SELECT  user_id ,
                                            date_of_closure ,
                                            count_of_transactions ,
                                            AccountingClosure.id,
                                            role_code,
                                            user_name,
                                            last_name,
                                            first_name,
                                            is_deleted
                                    FROM    dbo.AccountingClosure
                                    INNER JOIN dbo.Users ON dbo.AccountingClosure.user_id = dbo.Users.id";

            List<AccountingClosure> closures = new List<AccountingClosure>();
            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand select = new OpenCbsCommand(sqlText, conn))
                {
                    using (OpenCbsReader reader = select.ExecuteReader())
                    {
                        if (reader.Empty) return null;
                        while (reader.Read())
                        {
                            closures.Add(new AccountingClosure
                                             {
                                                 Id = reader.GetInt("id"),
                                                 Date = reader.GetDateTime("date_of_closure"),
                                                 CountOfTransactions = reader.GetInt("count_of_transactions"),
                                                 Deleted = reader.GetBool("is_deleted"),
                                                 User =
                                                     new User
                                                         {
                                                             Id = reader.GetInt("user_id"),
                                                             UserName = reader.GetString("user_name"),
                                                             UserRole = new Role { RoleName = reader.GetString("role_code") },
                                                             FirstName = reader.GetString("first_name"),
                                                             LastName = reader.GetString("last_name")
                                                         }
                                             });
                        }
                    }

                    return closures;
                }
            }
        }
	}
}

