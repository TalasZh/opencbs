// LICENSE PLACEHOLDER

using System;
using System.Data.SqlClient;
using System.Collections;
using OpenCBS.CoreDomain;
using OpenCBS.Shared;
using OpenCBS.Shared.Settings;

namespace OpenCBS.Manager.Database
{
	/// <summary>
	/// General settings database storage manager.
	/// </summary>
	public class ApplicationSettingsManager : Manager
	{
        private User _user = new User();

        public ApplicationSettingsManager(User pUser) : base(pUser)
        {
            _user = pUser;
        }

        public ApplicationSettingsManager(string testDB) : base(testDB) { }


        /// <summary>
        /// Fills General Settings with values from database
        /// </summary>
        /// 

        public void FillGeneralSettings()
        {
            ApplicationSettings.GetInstance(_user.Md5).DeleteAllParameters();

            string sqlText = "SELECT  UPPER([key]) as [key], [value] FROM GeneralParameters";

            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(sqlText, conn))
            using (OpenCbsReader r = c.ExecuteReader())
            {
                while (r.Read())
                    ApplicationSettings.GetInstance(_user.Md5).AddParameter(r.GetString("key"),
                                                                 r.GetString("value"));
            }
        }

        public object SelectParameterValue(string key)
        {
            string sqlText = "SELECT [value] FROM GeneralParameters WHERE [key] = @name";

            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(sqlText, conn))
            {
                c.AddParam("@name", key);
                using (OpenCbsReader r = c.ExecuteReader())
                {
                    if (!r.Empty)
                    {
                        r.Read();
                        return r.GetString("value");
                    }
                }
            }
            return null;
        }

        public Guid? GetGuid()
        {
            string query = "SELECT [value] FROM TechnicalParameters WHERE [name] = 'GUID'";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(query, conn))
            using (OpenCbsReader r = c.ExecuteReader())
            {
                if (!r.Empty)
                {
                    r.Read();
                    string temp = r.GetString("value");
                    return new Guid(temp);
                }
            }
            return null;
        }

        public void SetGuid(Guid guid)
        {
            string query = "INSERT INTO [TechnicalParameters] ([name], [value]) VALUES ('GUID', @value)";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(query, conn))
            {
                c.AddParam("@value", guid.ToString());
                c.ExecuteNonQuery();
            }
        }
        public void SetBuildNumber(string buildNum)
        {
            string query = "UPDATE [TechnicalParameters] SET [value]=@value WHERE [name]='BuildNumber'";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(query, conn))
            {
                c.AddParam("@value", buildNum);
                c.ExecuteNonQuery();
            }
        }
     
        public void AddParameter(DictionaryEntry entry)
        {
            ApplicationSettings.GetInstance(_user.Md5).AddParameter(entry.Key.ToString(), entry.Value);

            string sqlText = "INSERT INTO [GeneralParameters]([key], [value])" +
                " VALUES(@name,@value)";

            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(sqlText, conn))
            {
                c.AddParam("@name", entry.Key.ToString());

                if (entry.Value != null)
                    c.AddParam("@value", entry.Value.ToString());
                else
                    c.AddParam("@value", null);

                c.ExecuteNonQuery();
            }
        }

        public int UpdateSelectedParameter(string pName)
        {
            ApplicationSettings.GetInstance(_user.Md5).UpdateParameter(pName, null);
            string sql = "UPDATE GeneralParameters SET [value] = @value WHERE upper([key]) = @key";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(sql, conn))
            {
                c.AddParam("@value", null);
                c.AddParam("@key", pName);

                return c.ExecuteNonQuery();
            }
        }

	    public int UpdateSelectedParameter(string pName, string pNewValue)
        {
            ApplicationSettings.GetInstance(_user.Md5).UpdateParameter(pName, pNewValue);

            string sql = "UPDATE GeneralParameters SET [value] = @value WHERE upper([key]) = @key";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(sql, conn))
            {
                c.AddParam("@value", pNewValue);
                c.AddParam("@key", pName);

                return c.ExecuteNonQuery();
            }
        }

        public int UpdateSelectedParameter(string pName, int pNewValue)
        {
            ApplicationSettings.GetInstance(_user.Md5).UpdateParameter(pName, pNewValue);

            string sql = "UPDATE GeneralParameters SET [value] = @value WHERE upper([key]) = @key";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(sql, conn))
            {
                c.AddParam("@value", pNewValue.ToString());
                c.AddParam("@key", pName);

                return c.ExecuteNonQuery();
            }
        }

        public int UpdateSelectedParameter(string pName, bool pNewValue)
        {
            ApplicationSettings.GetInstance(_user.Md5).UpdateParameter(pName, pNewValue);
            string sql = "UPDATE GeneralParameters SET [value] = @value WHERE upper([key]) = @key";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(sql, conn))
            {
                if (pNewValue)
                    c.AddParam("@value", "1");
                else
                    c.AddParam("@value", "0");

                c.AddParam("@key", pName);
                return c.ExecuteNonQuery();
            }
        }

	    public void DeleteSelectedParameter(string key)
        {
            string sqlText = "DELETE FROM GeneralParameters WHERE  upper([key]) = @name";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(sqlText, conn))
            {
                c.AddParam("@name", key);
                c.ExecuteNonQuery();
            }
        }
	}
}
