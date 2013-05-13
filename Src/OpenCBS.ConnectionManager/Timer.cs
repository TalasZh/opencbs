// Octopus MFS is an integrated suite for managing a Micro Finance Institution: 
// clients, contracts, accounting, reporting and risk
// Copyright © 2006,2007 OCTO Technology & OXUS Development Network
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License along
// with this program; if not, write to the Free Software Foundation, Inc.,
// 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
// Website: http://www.opencbs.com
// Contact: contact@opencbs.com

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
