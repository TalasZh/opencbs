// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using OpenCBS.CoreDomain.Online;
using OpenCBS.Shared;

namespace OpenCBS.DatabaseConnection
{
    public class UserRemotingContext
    {
        private Token _token = null;
        private SqlConnection _connection = null;
        private SqlConnection _secondaryConnection = null;
      
        public Token Token
        {
            get { return _token; }
            set { _token = value; }
        }

        public SqlConnection Connection
        {
            get { return _connection; }
            set { _connection = value; }
        }

        public SqlConnection SecondaryConnection
        {
            get { return _secondaryConnection; }
            set { _secondaryConnection = value; }
        }

    }
}
