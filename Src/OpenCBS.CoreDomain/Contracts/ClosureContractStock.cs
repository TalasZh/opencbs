// LICENSE PLACEHOLDER

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
