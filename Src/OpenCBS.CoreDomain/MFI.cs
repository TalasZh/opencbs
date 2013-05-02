using System;

namespace OpenCBS.CoreDomain
{
    [Serializable]
    public class MFI
    {
        private string _name;
        private string _login;
        private string _password;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Login
        {
            get { return _login; }
            set { _login = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
    }
}
