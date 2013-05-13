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
using System.Text;

using OpenCBS.Shared;

namespace OpenCBS.Manager.QueryForObject
{
    [Serializable]
    public class IterateSQL
    {
        private int _start;
        private int _end;
        private int number;
        private string TOP_100_PERCENT = "100 percent ";
        private string SELECT_FROM = " SELECT * FROM (";
        private string SELECT_TOP_I = "SELECT TOP $I * FROM(";
        private string SELECT_TOP_F = "SELECT TOP $F * FROM(";
        private string JOINTURE = @" WHERE maTable.id NOT IN (SELECT maTable.id FROM(";
        private string CLOSE = ") maTable)";
        private string SELECT_COUNT = "SELECT COUNT(*) FROM ( XYZ ) maTable";
        private string _sql;
        private string _where;
        private string _parameter = string.Empty;
        private List<string> _tempParameterList = new List<string>();
        private Dictionary<string, object> _finalParameterList = new Dictionary<string, object>();

        public IterateSQL()
        {

        }

        public IterateSQL(string sql, string where, string parameter)
        {
            _sql = sql;
            _where = where;
            _parameter = parameter;
        }

        public Dictionary<string, object> FinalParameterList
        {
            get { return _finalParameterList; }
        }

        public void AddParameter(string sql, string where, string parameter)
        {
            _sql = sql;
            _where = where;
            _parameter = parameter;
            AddDynamicParameters(where);
        }

        public string TransformAndReturnSql(int start, int end)
        {
            _start = start;
            _end = end;
            if (_parameter == null)
                _parameter = string.Empty;
            string[] split = Utils.Splitter(_parameter);
            number = split.Length;
            StringBuilder sb = new StringBuilder(" XYZ ");
            StringBuilder st = new StringBuilder();

            for (int i = 0; i < number; i++)
            {
                if (split[i] != string.Empty)
                    sb.Insert(0, SELECT_FROM);
            }

            for (int i = 0; i < number; i++)
            {
                if (split[i] != string.Empty)
                {
                    sb.Insert(sb.Length, _where);
                    sb.Replace("@param", split[i]);
                }
            }
            sb.Replace("XYZ", _sql);
            st.Append(sb.ToString());
            sb.Insert(0, SELECT_TOP_I);
            sb.Append(JOINTURE);
            sb.Append(SELECT_TOP_F);
            sb.Append(st.ToString());
            sb.Append(CLOSE);
            sb.Replace("$I", _start.ToString());
            sb.Replace("$F", _end.ToString());

            return sb.ToString();
        }

        public string TransformAndReturnSQLProxy(int start, int end)
        {
            _start = start;
            _end = end;
            if (_parameter == null)
                _parameter = string.Empty;
            string[] split = Utils.Splitter(_parameter);
            number = split.Length;
            StringBuilder sb = new StringBuilder(" XYZ ");
            StringBuilder st = new StringBuilder();

            for (int i = 0; i < number; i++)
            {
                if (split[i] != string.Empty)
                    sb.Insert(0, SELECT_FROM);
            }

            for (int i = 0; i < number; i++)
            {
                if (split[i] != string.Empty)
                {
                    string strWhere = string.Empty;
                    StringBuilder sWhere;
                    sWhere = new StringBuilder(ReplaceInCloseWhereParameters(_where, i, split[i]));
                    strWhere = sWhere.ToString();
                    sb.Insert(sb.Length, strWhere);
                }
            }
            sb.Replace("XYZ", _sql);
            st.Append(sb.ToString());
            sb.Insert(0, SELECT_TOP_I);
            sb.Append(JOINTURE);
            sb.Append(SELECT_TOP_F);
            sb.Append(st.ToString());
            sb.Append(CLOSE);
            sb.Replace("$I", _start.ToString());
            sb.Replace("$F", _end.ToString());

            return sb.ToString();
        }

        public string TransformAndReturnSQLCountProxy()
        {
            _end = 0;
            if (_parameter == null)
                _parameter = string.Empty;
            string[] split = Utils.Splitter(_parameter);
            number = split.Length;
            StringBuilder sb = new StringBuilder(" XYZ ");
            StringBuilder st = new StringBuilder();
            StringBuilder sw = new StringBuilder(SELECT_COUNT);


            for (int i = 0; i < number; i++)
            {
                if (split[i] != string.Empty)
                    sb.Insert(0, SELECT_FROM);
            }

            for (int i = 0; i < number; i++)
            {
                if (split[i] != string.Empty)
                {
                    string strWhere = string.Empty;
                    StringBuilder sWhere;
                    sWhere = new StringBuilder(ReplaceInCloseWhereParameters(_where, i, split[i]));
                    strWhere = sWhere.ToString();
                    sb.Insert(sb.Length, strWhere);
                }

            }
            sb.Replace("XYZ", _sql);
            st.Append(sb.ToString());
            sb.Insert(0, SELECT_TOP_I);
            sb.Append(JOINTURE);
            sb.Append(SELECT_TOP_F);
            sb.Append(st.ToString());
            sb.Append(CLOSE);
            sb.Replace("$I", TOP_100_PERCENT);
            sb.Replace("$F", _end.ToString());
            sw.Replace("XYZ", sb.ToString());

            return sw.ToString();
        }
        public string TransformAndReturnSqlCount()
        {
            _end = 0;
            if (_parameter == null)
                _parameter = string.Empty;
            string[] split = Utils.Splitter(_parameter);
            number = split.Length;
            StringBuilder sb = new StringBuilder(" XYZ ");
            StringBuilder st = new StringBuilder();
            StringBuilder sw = new StringBuilder(SELECT_COUNT);

            for (int i = 0; i < number; i++)
            {
                if (split[i] != string.Empty)
                    sb.Insert(0, SELECT_FROM);
            }

            for (int i = 0; i < number; i++)
            {
                if (split[i] != string.Empty)
                {
                    sb.Insert(sb.Length, _where);
                    sb.Replace("@param", split[i]);
                }
            }

            sb.Replace("XYZ", _sql);
            st.Append(sb.ToString());
            sb.Insert(0, SELECT_TOP_I);
            sb.Append(JOINTURE);
            sb.Append(SELECT_TOP_F);
            sb.Append(st.ToString());
            sb.Append(CLOSE);
            sb.Replace("$I", TOP_100_PERCENT);
            sb.Replace("$F", _end.ToString());
            sw.Replace("XYZ", sb.ToString());
            return sw.ToString();
        }

        private void AddDynamicParameters(string where)
        {
            string[] split = Utils.Splitter(where);
            foreach (string item in split)
            {
              if(item.Contains("@"))
                  _tempParameterList.Add(item);
            }
        }

        private string ReplaceInCloseWhereParameters(string where, int line, string strValue)
        {
            StringBuilder sw = new StringBuilder(where);
            foreach (string param in _tempParameterList)
            {
                sw.Replace(param, string.Concat(param, line));
                _finalParameterList.Add(string.Concat(param, line), strValue);

            }
            return sw.ToString();
        }
    }
}

