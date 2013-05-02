using System;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.Shared.Settings;

namespace OpenCBS.CoreDomain.Online
{
    public class Remoting : ICoreDomain
    {
        private static bool _exist = false;
        private static Remoting _theUniqueInstance = null;
        static private ICoreDomainRemoting _remoteOperation;

        // Initialise the remoting connection with http channel with binary formatter
        public bool RemotingConnection()
        {
            if (!_exist)
            {
                //IDictionary props = new Hashtable();
                //props["port"] = 0;
                //props.Add("typeFilterLevel", TypeFilterLevel.Full);
                //BinaryClientFormatterSinkProvider clientProvider = new BinaryClientFormatterSinkProvider();
                //HttpChannel channel = new HttpChannel(props, clientProvider, null);
                //ChannelServices.RegisterChannel(channel, false);

                var server = string.Format("http://{0}:{1}/RemoteCoreDomain", TechnicalSettings.RemotingServer, TechnicalSettings.RemotingServerPort);

                _remoteOperation = (ICoreDomainRemoting)Activator.GetObject(typeof(ICoreDomainRemoting), server);
                _exist = true;
            }
            return true;
        }

        private Remoting()
        {
            RemotingConnection();
        }

        public static Remoting GetInstance()
        {
            if (_theUniqueInstance == null)
                return _theUniqueInstance = new Remoting();
            return _theUniqueInstance;
        }


        public ChartOfAccounts GetChartOfAccounts()
        {
            return _remoteOperation.GetChartOfAccounts(User.CurrentUser);
        }

        public ProvisionTable GetProvisioningTable()
        {
            return _remoteOperation.GetProvisioningTable(User.CurrentUser);
        }
        public LoanScaleTable GetLoanScaleTable()
        {
            return _remoteOperation.GetLoanScaleTable(User.CurrentUser);
        }
    }
}
