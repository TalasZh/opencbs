using OpenCBS.CoreDomain;
using OpenCBS.Shared.Settings;

namespace OpenCBS.Services
{
    public class ServicesProvider
    {
        private IServices _iServices;
        private static ServicesProvider _theUniqueInstance;

        private ServicesProvider()
        {
            if (TechnicalSettings.UseOnlineMode)
                _iServices = Remoting.GetInstance();
            else
                _iServices = new Standard();
        }

        public static IServices GetInstance()
        {
            if (_theUniqueInstance == null)
                _theUniqueInstance = new ServicesProvider();

            return _theUniqueInstance._iServices;
        }

        public static ServicesProvider GetServiceProvider()
        {
            if (_theUniqueInstance == null)
                return _theUniqueInstance = new ServicesProvider();
            return _theUniqueInstance;
        }

        static bool _status = false;
        
        public void InitOnlineConnection(string pUserName, string pUserPass, string pDbName, string pComputerName, string pLoginName)
        {
            if (TechnicalSettings.UseOnlineMode && _status == false)
            {
                ((Remoting)_iServices).UserName = pUserName;
                ((Remoting)_iServices).Pass = pUserPass;
                ((Remoting)_iServices).Account = pDbName;

                User.CurrentUser.Md5 = _iServices.GetAuthentification(pUserName, 
                                                                        pUserPass, 
                                                                        pDbName, 
                                                                        pComputerName, 
                                                                        pLoginName);
                _iServices.RunTimeout();

                _status = true;
            }
        }
    }
}
