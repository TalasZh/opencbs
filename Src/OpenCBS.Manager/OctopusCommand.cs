using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using OpenCBS.Shared;

namespace OpenCBS.Manager
{
    public class OctopusReader : IDisposable
    {
        private readonly SqlDataReader _reader;
        private bool _disposed;

        public OctopusReader(SqlDataReader reader)
        {
            _reader = reader;
        }

        public bool Empty
        {
            get
            {
                return null == _reader || !_reader.HasRows;
            }
        }

        public bool Read()
        {
            Debug.Assert(_reader != null, "Reader is null");
            return _reader.Read();
        }

        public int GetInt(int num)
        {
            return _reader.GetInt32(num);
        }

        public byte GetByte(int num)
        {
            return _reader.GetByte(num);
        }

        public byte GetByte(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Column name should no be empty");
            int ordinal = _reader.GetOrdinal(name);
            return _reader.GetByte(ordinal);
        }

        private bool IsNull(string name)
        {
            return DBNull.Value == _reader.GetValue(_reader.GetOrdinal(name));
        }

        public string GetString(int num)
        {
            return _reader.GetString(num);
        }

        public byte[] GetBytes(int num)
        {
            return (byte[])_reader[0];
        }

        public byte[] GetBytes(string name)
        {
            var i = _reader.GetOrdinal(name);
            return (byte[])_reader[i];
        }

        public long GetBytes(int i, long startIndex, byte[] buffer, int p, int bufferSize)
        {
            return _reader.GetBytes(i, startIndex, buffer, p, bufferSize);
        }

        public string GetString(string name)
        {
            return IsNull(name) 
                ? null 
                : _reader.GetString(_reader.GetOrdinal(name));
        }

        public char GetChar(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Column name should no be empty");
            return _reader.GetString(_reader.GetOrdinal(name))[0];
        }

        public int GetInt(string name)
        {
            return _reader.GetInt32(_reader.GetOrdinal(name));
        }

        public int? GetNullInt(string name)
        {
            if (IsNull(name))
                return null;
            return GetInt(name);
        }

        public int? GetNullSmallInt(string name)
        {
            if (IsNull(name))
                return null;
            return GetSmallInt(name);
        }

        public int GetSmallInt(string name)
        {
            return _reader.GetInt16(_reader.GetOrdinal(name));
        }

        public bool GetBool(string name)
        {
            return _reader.GetBoolean(_reader.GetOrdinal(name));
        }

        public bool? GetNullBool(string name)
        {
            if (IsNull(name))
                return null;
            return GetBool(name);
        }

        public double GetDouble(string name)
        {
            return _reader.GetDouble(_reader.GetOrdinal(name));
        }

        public double? GetNullDouble(string name)
        {
            if (IsNull(name))
                return null;
            return GetDouble(name);
        }

        public DateTime GetDateTime(string name)
        {
            return _reader.GetDateTime(_reader.GetOrdinal(name));
        }

        public DateTime? GetNullDateTime(string name)
        {
            if (IsNull(name))
                return null;
            return GetDateTime(name);
        }

        public OCurrency GetMoney(string name)
        {
            if (IsNull(name))
                return null;
            return GetDecimal(name);
        }

        public decimal GetDecimal(string name)
        {
            return _reader.GetDecimal(_reader.GetOrdinal(name));
        }

        public string GetName(int number)
        {
            return _reader.GetName(number);
        }

        public object GetValue(int number)
        {
            return _reader.GetValue(number);
        }

        public decimal? GetNullDecimal(string name)
        {
            if (IsNull(name))
                return null;
            return GetDecimal(name);
        }

        public bool HasColumn(string pColName)
        {
            for (var i = 0; i < _reader.FieldCount; i++)
                if (_reader.GetName(i).Equals(pColName, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            return false;
        }

        public int FieldCount
        {
            get { return _reader.FieldCount; }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                _reader.Dispose();
            }
            _disposed = true;
        }

        public void Close()
        {
            _reader.Close();
        }
    }

    public class OctopusCommand : IDisposable
    {
        private readonly SqlCommand _cmd;

        public OctopusCommand()
        {
            _cmd = new SqlCommand();
        }

        public OctopusCommand(string query, SqlConnection conn)
        {
            _cmd = new SqlCommand(query, conn) {CommandTimeout = conn.ConnectionTimeout};
        }

        public OctopusCommand(string query, SqlConnection conn, SqlTransaction transaction)
        {
            _cmd = new SqlCommand(query, conn, transaction) {CommandTimeout = conn.ConnectionTimeout};
        }

        public void AddParam(string name, object value)
        {
            Debug.Assert(_cmd != null, "Command is null");

            if (value is OCurrency)
            {
                OCurrency val = (OCurrency) value;
                if (val.HasValue)
                {
                    _cmd.Parameters.Add(name, SqlDbType.Money);
                    _cmd.Parameters[name].Value = val.Value;
                    return;
                }
                value = null;
            }

            if(value == null)
            {
                _cmd.Parameters.Add(name, SqlDbType.NVarChar);
                _cmd.Parameters[name].Value = DBNull.Value;
            } else
                _cmd.Parameters.AddWithValue(name, value);
        }

        public void ResetParams()
        {
            _cmd.Parameters.Clear();
        }

        public void ExecuteAsStoredProcedure()
        {
            _cmd.CommandType = CommandType.StoredProcedure;
        }

        public OctopusReader ExecuteReader()
        {
            return new OctopusReader(_cmd.ExecuteReader());
        }

        public OctopusReader ExecuteReader(CommandType commandType)
        {
            _cmd.CommandType = commandType;
            return new OctopusReader(_cmd.ExecuteReader());
        }

        public OctopusReader ExecuteReader(CommandBehavior commandBehavior)
        {
            return new OctopusReader(_cmd.ExecuteReader(commandBehavior));
        }

        public int ExecuteNonQuery()
        {
            return _cmd.ExecuteNonQuery();
        }

        public object ExecuteScalar()
        {
            return _cmd.ExecuteScalar();
        }

        public int FieldCount
        {
            get { return FieldCount; }
        }

        public string CommandText
        {
            get
            {
                return _cmd.CommandText;
            }

            set
            {
                _cmd.CommandText = value;
            }
        }

        public int CommandTimeout
        {
            get
            {
                return _cmd.CommandTimeout;
            }
            set
            {
                _cmd.CommandTimeout = value;
            }
        }

        public SqlConnection Connection
        {
            get
            {
                return _cmd.Connection;
            }

            set
            {
                _cmd.Connection = value;
            }
        }

        public SqlTransaction Transaction
        {
            get
            {
                return _cmd.Transaction;
            }
            set
            {
                _cmd.Transaction = value;
            }
        }

        public void Dispose()
        {
            _cmd.Dispose();
        }

        public OctopusCommand AsStoredProcedure()
        {
            _cmd.CommandType = CommandType.StoredProcedure;
            return this;
        }
    }
}