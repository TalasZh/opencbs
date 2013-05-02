using log4net;
using log4net.Config;

[assembly: XmlConfigurator(ConfigFile = "log4net.xml", Watch = true)]
namespace OpenCBS.Shared
{
    public static class Log
    {
        public static ILog RemotingLogger
        {
            get { return LogManager.GetLogger("RemotingLogger"); }
        }

        public static ILog RemotingServiceLogger
        {
            get { return LogManager.GetLogger("RemotingServiceLogger"); }
        }

        public static ILog RemotingServiceUsersLogger
        {
            get { return LogManager.GetLogger("RemotingServiceUsersLogger"); }
        }
    }
}
