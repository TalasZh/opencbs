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

namespace OpenCBS.Manager.QueryForObject
{
    [Serializable]
    public class QueryEntity
    {
        private string _sqlEntite;
        private IterateSQL _iterateSQL;
        private string _where;
        private string _parameter;
        private Dictionary<string, object> _dynamicParameterList = new Dictionary<string, object>();

      
        public QueryEntity(string parameter,string sqlEntite,string where)
        {
            _sqlEntite = sqlEntite;
            _where = where;
            _parameter = parameter;
            _iterateSQL = new IterateSQL();
            _iterateSQL.AddParameter(_sqlEntite, _where, _parameter);
        }

        public Dictionary<string, object> DynamiqParameters()
        {
            return _iterateSQL.FinalParameterList; 
        }

        public string ConstructSQLEntityByCriteres(int start, int end)
        {
            return _iterateSQL.TransformAndReturnSql(start,end);
        }

        public string ConstructSQLEntityByCriteresProxy(int start, int end)
        {
            return _iterateSQL.TransformAndReturnSQLProxy(start, end);
        }

        public string ConstructSQLEntityNumber()
        {
            return _iterateSQL.TransformAndReturnSqlCount();
        }

        public string ConstructSQLEntityNumberProxy()
        {
            return _iterateSQL.TransformAndReturnSQLCountProxy();
        }
    }
}
