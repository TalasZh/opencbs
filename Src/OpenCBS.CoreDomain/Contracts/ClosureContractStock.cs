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
using System.Collections;
using OpenCBS.CoreDomain.Contracts.Loans;
using OpenCBS.CoreDomain.Events;
using OpenCBS.CoreDomain.Events.Loan;

namespace OpenCBS.CoreDomain.Contracts
{
    /// <summary>
    /// Summary description for ClosureContractStock.
    /// </summary>
    [Serializable]
    public class ClosureContractStock
    {
        private ArrayList contracts;
        private StatisticalProvisionningEvent _GlobalOLBStatisticalEvent;

        public ClosureContractStock()
        {
            contracts = new ArrayList();
        }

        public ArrayList Contracts
        {
            get { return contracts; }
            set { contracts = value; }
        }

        public StatisticalProvisionningEvent GlobalOLBStatisticalEvent
        {
            get { return _GlobalOLBStatisticalEvent; }
            set { _GlobalOLBStatisticalEvent = value; }
        }

        public void AddContract(Loan contract)
        {
            contracts.Add(contract);
        }

        public void DeleteContract(int contractId)
        {
            Loan contractToDelete = null;
            foreach (Loan contract in contracts)
            {
                if (contract.Id == contractId)
                {
                    contractToDelete = contract;
                    break;
                }
            }
            contracts.Remove(contractToDelete);
        }

        public int NberOfEventsNotFired
        {
            get
            {
                int result = 0;
                foreach(Loan contract in contracts)
                {
                    contract.Events.SortEventsByDate();
                    foreach (Event e in contract.Events)
                    {
                        if (!e.IsFired)
                        {
                            if (e is WriteOffEvent)
                                result++;
                        }
                    }
                }
                return result;
            }
        }

        public ArrayList WriteOffContracts
        {
            get
            {
                ArrayList writeOffContracts = new ArrayList();
                foreach (Loan contract in contracts)
                {
                    //contract.Events.SortEventsByDate();
                    //if (contract.Events.GetEvent(contract.Events.GetNumberOfEvents - 1) is WriteOffEvent)
                    if(contract.WrittenOff)
                        writeOffContracts.Add(contract);
                }
                return writeOffContracts;
            }
        }

        public ArrayList GoodContracts
        {
            get
            {
                ArrayList goodContracts = new ArrayList();
                foreach (Loan contract in contracts)
                {
                    contract.Events.SortEventsByDate();
                    if (contract.Events.GetEvent(contract.Events.GetNumberOfEvents - 1) is AccruedInterestEvent)
                        goodContracts.Add(contract);
                }
                return goodContracts;
            }
        }

        public bool IsWriteOffContract(Loan contract)
        {
            if (contract.Events.GetEvent(0) is WriteOffEvent)
                return true;

            return false;
        }
    }
}
