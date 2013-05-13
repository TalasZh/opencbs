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
using OpenCBS.CoreDomain.Accounting;

namespace OpenCBS.Manager.Accounting
{
    public class StandardBookingManager : Manager
    {
        private readonly ChartOfAccounts _chartOfAccounts;

        private readonly User _user = new User();

        public StandardBookingManager(User pUser): base(pUser)
        {
            _user = pUser;
            AccountManager _accountManagement =  new AccountManager(pUser);
            _chartOfAccounts = new ChartOfAccounts();
            _chartOfAccounts.Accounts = _accountManagement.SelectAllAccounts();
        }

        public StandardBookingManager(string testDB) : base(testDB) { }

        public void CreateStandardBooking(Booking booking)
        {
            const string sqlText = @"INSERT INTO StandardBookings([Name], debit_account_id, credit_account_id) 
                                     VALUES (@name, @debit_account_id, @credit_account_id)";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand insertIntoTable = new OpenCbsCommand(sqlText, conn))
                {
                    insertIntoTable.AddParam("@name", booking.Name);
                    insertIntoTable.AddParam("@debit_account_id", booking.DebitAccount.Id);
                    insertIntoTable.AddParam("@credit_account_id", booking.CreditAccount.Id);
                    insertIntoTable.ExecuteNonQuery();
                }
            
        }

        public void UpdateStandardBooking(Booking booking)
        {
            const string sqlText = @"UPDATE StandardBookings
                                       SET [Name] = @name, 
                                           debit_account_id = @debit_account_id, 
                                           credit_account_id = @credit_account_id
                                    WHERE Id = @Id";
            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand insertIntoTable = new OpenCbsCommand(sqlText, conn))
                {
                    insertIntoTable.AddParam("@name", booking.Name);
                    insertIntoTable.AddParam("@debit_account_id", booking.DebitAccount.Id);
                    insertIntoTable.AddParam("@credit_account_id", booking.CreditAccount.Id);
                    insertIntoTable.AddParam("@Id", booking.Id);
                    insertIntoTable.ExecuteNonQuery();
                }
            }
        }

        public Booking SelectStandardBookingById(int id)
        {
            const string sqlText = @"SELECT Id, [Name], debit_account_id, credit_account_id 
                                    FROM StandardBookings
                                    WHERE Id = @Id";
            Booking standardBooking = new Booking();
            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand select = new OpenCbsCommand(sqlText, conn))
                {
                    select.AddParam("@Id", id);

                    using (OpenCbsReader reader = select.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            standardBooking.Id = reader.GetInt("Id");
                            standardBooking.Name = reader.GetString("Name");

                            Account account =
                                _chartOfAccounts.GetAccountById(reader.GetInt("debit_account_id"));
                            standardBooking.DebitAccount = account;

                            account =
                                _chartOfAccounts.GetAccountById(reader.GetInt("credit_account_id"));
                            standardBooking.CreditAccount = account;
                        }
                    }
                }
            }
            return standardBooking;
        }

        public void DeleteStandardBooking(int id)
        {
            const string sqlText = @"DELETE FROM StandardBookings WHERE Id = @Id";
            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand insertIntoTable = new OpenCbsCommand(sqlText, conn))
                {
                    insertIntoTable.AddParam("@Id", id);
                    insertIntoTable.ExecuteNonQuery();
                }
            }
        }

        public List<Booking> SelectAllStandardBookings()
        {
            const string sqlText = @"SELECT Id, [Name], debit_account_id, credit_account_id FROM StandardBookings";
            List<Booking> list = new List<Booking>();
            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand select = new OpenCbsCommand(sqlText, conn))
                {
                    using (OpenCbsReader reader = select.ExecuteReader())
                    {
                        if (reader.Empty) return list;
                        while (reader.Read())
                        {
                            Booking standardBooking = new Booking();
                            standardBooking.Id = reader.GetInt("Id");
                            standardBooking.Name = reader.GetString("Name");

                            Account account =
                                _chartOfAccounts.GetAccountById(reader.GetInt("debit_account_id"));
                            standardBooking.DebitAccount = account;

                            account =
                                _chartOfAccounts.GetAccountById(reader.GetInt("credit_account_id"));
                            standardBooking.CreditAccount = account;

                            list.Add(standardBooking);
                        }
                    }
                }
                return list;
            }
        }
    }
}
