using System;
using System.Data;
using OpenCBS.Shared.Settings;
using OpenCBS.Shared;
using System.IO;

namespace OpenCBS.DatabaseConnection
{
    public class Timer
    {
        private System.Timers.Timer _timer = new System.Timers.Timer();
        private readonly int _timeout = TechnicalSettings.DatabaseTimeout;
        private int _debugLevel = TechnicalSettings.DebugLevel;

        public void create_timer()
        {
            Log.RemotingServiceLogger.Info("Call of create_timer");
            _timer.Interval = 6000;

            // TODO uncomment : this was just for the test
            _timer.Elapsed += new System.Timers.ElapsedEventHandler(check_token_timeout);
            _timer.Start();
        }

        public void start_timer()
        {
            // TODO uncomment : this was just for the test
            _timer.Start();
        }
        public void stop_timer()
        {
            _timer.Stop();
        }

        private void check_token_timeout(object sender, System.Timers.ElapsedEventArgs e)
        {
            System.Collections.IDictionary account_table = Remoting.GetAccountTable();

            foreach (System.Collections.DictionaryEntry cur_connec in account_table)
            {
                if (((UserRemotingContext)cur_connec.Value).Token.Timeout == _timeout )
                {
                    if (((UserRemotingContext)cur_connec.Value).Connection.State != ConnectionState.Closed)
                    {
                        Log.RemotingServiceLogger.Info("Supress the token unique string : " + ((UserRemotingContext)cur_connec.Value).Token.get_unique_string());
                        ((UserRemotingContext) cur_connec.Value).Connection.Close();
                    }
                    return;
                }
                ((UserRemotingContext)cur_connec.Value).Token.incr_timeout();
            }
        }
    }
}
