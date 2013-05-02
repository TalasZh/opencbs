// LICENSE PLACEHOLDER

using OpenCBS.Shared.Settings;

namespace OpenCBS.CoreDomain.Online
{
    public class CoreDomainProvider
    {
        private readonly ICoreDomain _iCoreDomain = null;
        private static CoreDomainProvider _theUniqueInstance;

        private CoreDomainProvider()
        {
            if (TechnicalSettings.UseOnlineMode)
                _iCoreDomain = Remoting.GetInstance();
            else
                _iCoreDomain = new Standard();
        }

        public static ICoreDomain GetInstance()
        {
            if (_theUniqueInstance == null)
                _theUniqueInstance = new CoreDomainProvider();

            return _theUniqueInstance._iCoreDomain;
        }
    }
}
