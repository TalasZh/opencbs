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
            using (OpenCbsCommand select = new OpenCbsCommand(sqlText, connection))
            using (OpenCbsReader reader = select.ExecuteReader())
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
            using (OpenCbsCommand cmd = new OpenCbsCommand(sqlText, connection))
                cmd.ExecuteNonQuery();
        }


        public void AddPublicHoliday(DictionaryEntry entry)
        {
            const string sqlText = "INSERT INTO [PublicHolidays]([date], [name]) VALUES(@date, @name)";
            using(SqlConnection connection = GetConnection())
            using (OpenCbsCommand cmd = new OpenCbsCommand(sqlText, connection))
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
            using (OpenCbsCommand cmd = new OpenCbsCommand(sqlText, connection))
            {
                cmd.AddParam("@date", Convert.ToDateTime(entry.Key));
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdatePublicHoliday(DictionaryEntry entry)
        {
            const string sqlText = "UPDATE [PublicHolidays] SET [name] = @name WHERE [date]= @date";
            using (SqlConnection connection = GetConnection())
            using (OpenCbsCommand cmd = new OpenCbsCommand(sqlText, connection))
            {
                cmd.AddParam("@date", Convert.ToDateTime(entry.Key));
                cmd.AddParam("@name", entry.Value.ToString());
                cmd.ExecuteNonQuery();
            }
        }
    }
}
