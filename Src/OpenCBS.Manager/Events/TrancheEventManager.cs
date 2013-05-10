// LICENSE PLACEHOLDER

using OpenCBS.CoreDomain.Events;
using System.Data.SqlClient;

namespace OpenCBS.Manager.Events
{
    public class TrancheEventManager : Manager
    {
        public TrancheEventManager(string testDB) : base(testDB)
        {
            
        }

        private static void SetTrancheEvent(OpenCbsCommand cmd, TrancheEvent trancheEvent)
        {
            cmd.AddParam("@Id",  trancheEvent.Id);
            cmd.AddParam("@InterestRate", trancheEvent.InterestRate);
            cmd.AddParam("@Amount", trancheEvent.Amount);
            cmd.AddParam("@Maturity", trancheEvent.Maturity);
            cmd.AddParam("@StartDate", trancheEvent.StartDate);
            cmd.AddParam("@applied_new_interest", trancheEvent.StartDate);
        }

        /// <summary>
        /// Method to add a TrancheEvent into database. We use the NullableTypes to make the correspondance between
        /// nullable int, decimal and double types in database and our own objects
        /// </summary>
        /// <param name="trancheEvent">TrancheEvent Object</param>
        /// <returns>The id of the Tranche Event which has been added</returns>
        public int Add(TrancheEvent trancheEvent)
        {
            const string sqlText = @"
                INSERT INTO [TrancheEvents]
                           ( [id]
                            ,[interest_rate]
                            ,[amount]
                            ,[maturity]
                            ,[start_date]
                            ,[applied_new_interest])
                            VALUES
                            (@Id,
                             @InterestRate,
                             @Amount,
                             @Maturity,
                             @StartDate, 
                             @applied_new_interest) 
                SELECT SCOPE_IDENTITY()";
            using (SqlConnection conn = GetConnection())
            using (var cmd = new OpenCbsCommand(sqlText, conn))
            {
                SetTrancheEvent(cmd, trancheEvent);
                return int.Parse(cmd.ExecuteScalar().ToString());
            }
        }

        public void Update(TrancheEvent trancheEvent)
        {
            string sqlText = @"
                            UPDATE [TrancheEvents] SET 
                            [interest_rate] = @InterestRate,
                            [amount] = @Amount,
                            [maturity] = @Maturity,
                            [start_date] = @StartDate,
                            [applied_new_interest] = @applied_new_interest
                            WHERE id = @Id";
            using (SqlConnection conn = GetConnection())
            using (var cmd = new OpenCbsCommand(sqlText, conn))
            {
                SetTrancheEvent(cmd, trancheEvent);
                cmd.ExecuteNonQuery();
            }

        }

    }
}
