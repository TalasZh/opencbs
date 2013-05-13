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
