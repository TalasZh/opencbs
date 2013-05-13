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
using OpenCBS.CoreDomain.Clients;
using OpenCBS.CoreDomain.Contracts.Loans;

namespace OpenCBS.CoreDomain.EconomicActivities
{
	/// <summary>
    /// Summary description for EconomicActivityLoanHistory.
    /// </summary>
    [Serializable]
	public class EconomicActivityLoanHistory
	{
	    private IClient _client;
        private Group _group;
        private Loan _contract;
        private EconomicActivity _economicActivity;
		private bool _deleted;

        public IClient Person
        {
            get { return _client; }
            set { _client = value; }
        }

	    public Group Group
	    {
            get { return _group; }
            set { _group = value; }
	    }

        public Loan Contract
        {
            get { return _contract; }
            set { _contract = value; }
        }

        public EconomicActivity EconomicActivity
        {
            get { return _economicActivity; }
            set { _economicActivity = value; }
        }

        public bool Deleted
        {
            get { return _deleted; }
            set { _deleted = value; }
        }

	}
}
