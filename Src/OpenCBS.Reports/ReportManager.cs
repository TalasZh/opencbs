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
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Text;
using OpenCBS.CoreDomain;
using OpenCBS.Shared.Settings;
using System;

namespace OpenCBS.Reports
{
    public class ReportManager
    {
        private static ReportManager _instance;

        public static ReportManager GetInstance()
        {
            return _instance ?? (_instance = new ReportManager());
        }

        private static SqlConnection GetConnection()
        {
            return CoreDomain.DatabaseConnection.GetConnection();
        }

        private static bool HasDatasource(string name)
        {
            const string sql = @"SELECT COUNT(*) AS has_datasource FROM sys.objects WHERE [name] = @name";
            using (SqlConnection conn = GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@name", name);
                bool retval = false;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader != null && reader.HasRows)
                    {
                        reader.Read();
                        retval = 1 == reader.GetInt32(0) ? true : false;
                    }
                }
                return retval;
            }
        }

        private static string GetObjectCreateScript(string name)
        {
            const string query = "sp_helptext";
            using (SqlConnection conn = GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@objname", name);
                cmd.CommandType = CommandType.StoredProcedure;
                var buffer = new StringBuilder(2048);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (null == reader) return string.Empty;
                    while (reader.Read())
                    {
                        buffer.Append(reader.GetString(0));
                    }
                }
                return buffer.ToString();
            }
        }

        private static string GetHelperType(string name)
        {
            const string query = @"SELECT [type] FROM sys.objects WHERE [name] = @name";
            using (SqlConnection conn = GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@name", name);
                object r = cmd.ExecuteScalar();
                return r != null ? r.ToString().Trim() : string.Empty;
            }
        }

        private static void AddDatasource(string addScript)
        {
            SqlCommand cmd = new SqlCommand(addScript, GetConnection());
            cmd.ExecuteNonQuery();
        }

        private static void DeleteDatasource(string name)
        {
            string query = string.Format("DROP PROCEDURE dbo.{0}", name);
            using (SqlConnection conn = GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.ExecuteNonQuery();
            }
        }

        private static void AddHelper(string script)
        {
            AddDatasource(script);
        }

        private static void DeleteHelper(string name, string type)
        {
            string what;
            switch (type)
            {
                case "IF":
                case "TF":
                case "FN":
                    what = "FUNCTION";
                    break;

                case "P":
                    what = "PROCEDURE";
                    break;

                case "V":
                    what = "VIEW";
                    break;

                default:
                    what = string.Empty;
                    break;
            }
            if (string.IsNullOrEmpty(what)) return;

            string query = string.Format("DROP {0} dbo.{1}", what, name);
            using (SqlConnection conn = GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.ExecuteNonQuery();
            }
        }

        private static List<string> GetDatasourceParams(string name)
        {
            List<string> retval = new List<string>();
            const string sql = @"EXEC sys.sp_procedure_params_rowset @procedure_name";
            using (SqlConnection conn = GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@procedure_name", name);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            string paramName = reader.GetString(3);
                            if (paramName != "@RETURN_VALUE")
                            {
                                retval.Add(paramName);
                            }
                        }
                    }
                }
                return retval;
            }
        }

        public void LoadHelpers(Report report)
        {
            foreach (string h in report.Helpers)
            {
                string ht = GetHelperType(h);
                if (!string.IsNullOrEmpty(ht))
                {
                    DeleteHelper(h, ht);
                }
                StreamReader r = new StreamReader(report.GetDatasourceStream(h));
                AddHelper(r.ReadToEnd());
            }
        }

        public DataTable GetDatasource(string name, Report report, string script)
        {
            try
            {
                bool create = true;
                if (HasDatasource(name))
                {
                    string currentScript = GetObjectCreateScript(name);
                    if (script != currentScript)
                    {
                        DeleteDatasource(name);
                    }
                    else
                    {
                        create = false;
                    }
                }
                if (create) AddDatasource(script);

                List<string> dsParams = GetDatasourceParams(name);

                using (SqlConnection conn = GetConnection())
                using (SqlCommand cmd = new SqlCommand(name, conn))
                {
                    cmd.CommandTimeout = ApplicationSettings.GetInstance(string.Empty).ReportTimeout;
                    cmd.CommandType = CommandType.StoredProcedure;

                    foreach (string paramName in dsParams)
                    {
                        object value;
                        switch (paramName.Trim(new[] {'@'}).ToLower())
                        {
                            case "language":
                                value = UserSettings.Language;
                                break;

                            case "branch_name":
                                value = "";
                                break;

                            case "user_id":
                                value = User.CurrentUser.Id;
                                break;

                            case "user_name":
                                value = User.CurrentUser.FirstName + " " + User.CurrentUser.LastName;
                                break;

                            case "branch_id":
                                value = report.GetParamValueByName(paramName) ?? 1;
                                break;

                            default:
                                value = report.GetParamValueByName(paramName);
                                break;
                        }
                        if (value != null)
                            cmd.Parameters.AddWithValue(paramName, value);
                        else
                            cmd.Parameters.AddWithValue(paramName, SqlInt32.Null);
                    }
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet retval = new DataSet();
                    da.Fill(retval, name);
                    return retval.Tables[0];
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                throw;
            }
        }

        public DataTable GetDatasource(string name, Report report)
        {
            StreamReader reader = new StreamReader(report.GetDatasourceStream(name));
            return GetDatasource(name, report, reader.ReadToEnd());
        }

        public List<KeyValuePair<object, object>> GetQueryResult(string query)
        {
            using (SqlConnection conn = GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                List<KeyValuePair<object, object>> retval = new List<KeyValuePair<object, object>>();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        object key = reader.GetValue(0);
                        object value = reader.GetValue(1);
                        retval.Add(new KeyValuePair<object, object>(key, value));
                    }
                }
                if (retval.Count == 0) retval.Add(new KeyValuePair<object, object>(null, "Empty"));
                return retval;
            }
        }

        public List<string> GetUnstarredBulk()
        {
            return UserSettings.GetUnstarredBulk();
        }

        public void SetStarred(string name, bool starred)
        {
            UserSettings.SetReportStarred(name, starred);
        }

        public bool GetUseCents(int currencyId)
        {
            const string query = @"SELECT use_cents 
            FROM Currencies WHERE id = @id";
            using (SqlConnection conn = GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id", currencyId);
                return Convert.ToBoolean(cmd.ExecuteScalar());
            }
        }
    }
}
