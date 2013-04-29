using System;
using System.Collections.Generic;

namespace Octopus.CoreDomain.Online
{
    [Serializable]
    public class Token
    {
        public void incr_timeout()
        { ++_timeout; }

        public void dcr_timeout()
        {
            _timeout = 0;
        }

        public Token(string login, string pass, string account)
        {
            _login = login;
            _pass = pass;
            _account = account;
        }

        #region accessors
        public string Login
        {
            get { return _login; }
            set { _login = value; }
        }

        public string Pass
        {
            get { return _pass; }
            set { _pass = value; }
        }

        public string Account
        {
            get { return _account; }
            set { _account = value; }
        }

        public int Timeout
        {
            get { return _timeout; }
            set { _timeout = value; }
        }
        #endregion


        public string get_unique_string()
        {
            return _pass + "-" + _login + "-" + _account;
        }

        string _login = "";
        string _pass = "";
        string _account = "";
        int _timeout = 0;
    }
}
