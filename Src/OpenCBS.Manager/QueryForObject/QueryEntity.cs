// LICENSE PLACEHOLDER

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
