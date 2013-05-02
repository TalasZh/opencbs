// LICENSE PLACEHOLDER

using System;
using System.Text.RegularExpressions;
using OpenCBS.CoreDomain;
using OpenCBS.Shared.Settings;

namespace OpenCBS.Services.Rules
{
    public class RegExCheckerServices : MarshalByRefObject
    {
        private User _user = new User();
        private readonly ApplicationSettings dataParam;

        public RegExCheckerServices(User pUser)
        {
            _user = pUser;
        }

        public RegExCheckerServices(User pUser, string testDB)
        {
            _user = pUser;
            dataParam = ApplicationSettings.GetInstance(pUser.Md5);
        }

        public bool CheckID(string pIDForTest)
        {
            Match m = Regex.Match(pIDForTest, 
                ApplicationSettings.GetInstance(_user != null ? _user.Md5 : "").IDPattern);
            return m.Success;
        }
    }
}
