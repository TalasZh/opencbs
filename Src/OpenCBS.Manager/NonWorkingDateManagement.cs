// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Collections;
using OpenCBS.CoreDomain;
using OpenCBS.Shared;
using OpenCBS.Shared.Settings;

namespace OpenCBS.Manager
{
    /// <summary>
    /// Summary description for NonWorkingDateManager.
    /// </summary>
    public class NonWorkingDateManagement : Manager
    {
        private NonWorkingDateSingleton nonWorkingDateHelper;
        private ApplicationSettings dbParam;
        private User _user;

        public NonWorkingDateManagement(User pUser) : base(pUser)
        {
            _user = pUser;
            dbParam = ApplicationSettings.GetInstance(pUser.Md5);
            nonWorkingDateHelper = NonWorkingDateSingleton.GetInstance(_user.Md5);
        }

        public NonWorkingDateManagement(string testDB) : base(testDB)
        {
            dbParam = ApplicationSettings.GetInstance("");
            nonWorkingDateHelper = NonWorkingDateSingleton.GetInstance("");
        }

        private void SelectAllPublicHolidays()
        {
            const string sqlText = @"SELECT name, date 
                                    FROM PublicHolidays 
                                    ORDER BY date";

            using (SqlConnection connection = GetConnection())
            using (OctopusCommand select = new OctopusCommand(sqlText, connection))
            using (OctopusReader reader = select.ExecuteReader())
            {
                while (reader.Read())
                {
                    nonWorkingDateHelper.PublicHolidays.Add(reader.GetDateTime("date"),
                                                            reader.GetString("name"));
                }
            }
            
        }

        public NonWorkingDateSingleton FillNonWorkingDateHelper()
        {
            nonWorkingDateHelper.PublicHolidays = new Dictionary<DateTime, string>();
            SelectAllPublicHolidays();
            nonWorkingDateHelper.WeekEndDay1 = dbParam.WeekEndDay1;
            nonWorkingDateHelper.WeekEndDay2 = dbParam.WeekEndDay2;
            return nonWorkingDateHelper;
        }

        public  void DeleteAllPublicHolidays()
        {
            const string sqlText = "DELETE PublicHolidays";
            using (SqlConnection connection = GetConnection())
            using (OctopusCommand cmd = new OctopusCommand(sqlText, connection))
                cmd.ExecuteNonQuery();
        }


        public void AddPublicHoliday(DictionaryEntry entry)
        {
            const string sqlText = "INSERT INTO [PublicHolidays]([date], [name]) VALUES(@date, @name)";
            using(SqlConnection connection = GetConnection())
            using (OctopusCommand cmd = new OctopusCommand(sqlText, connection))
            {
                cmd.AddParam("@date",Convert.ToDateTime(entry.Key));
                cmd.AddParam("@name", entry.Value.ToString());
                cmd.ExecuteNonQuery();
            }
        }

        public void DeletePublicHoliday(DictionaryEntry entry)
        {
            const string sqlText = "DELETE FROM [PublicHolidays] WHERE [date] = @date";
            using (SqlConnection connection = GetConnection())
            using (OctopusCommand cmd = new OctopusCommand(sqlText, connection))
            {
                cmd.AddParam("@date", Convert.ToDateTime(entry.Key));
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdatePublicHoliday(DictionaryEntry entry)
        {
            const string sqlText = "UPDATE [PublicHolidays] SET [name] = @name WHERE [date]= @date";
            using (SqlConnection connection = GetConnection())
            using (OctopusCommand cmd = new OctopusCommand(sqlText, connection))
            {
                cmd.AddParam("@date", Convert.ToDateTime(entry.Key));
                cmd.AddParam("@name", entry.Value.ToString());
                cmd.ExecuteNonQuery();
            }
        }
    }
}
