// LICENSE PLACEHOLDER

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
