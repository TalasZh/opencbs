// LICENSE PLACEHOLDER

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
            using (OctopusCommand insertIntoTable = new OctopusCommand(sqlText, conn))
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
                using (OctopusCommand insertIntoTable = new OctopusCommand(sqlText, conn))
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
                using (OctopusCommand select = new OctopusCommand(sqlText, conn))
                {
                    select.AddParam("@Id", id);

                    using (OctopusReader reader = select.ExecuteReader())
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
                using (OctopusCommand insertIntoTable = new OctopusCommand(sqlText, conn))
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
                using (OctopusCommand select = new OctopusCommand(sqlText, conn))
                {
                    using (OctopusReader reader = select.ExecuteReader())
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
