// LICENSE PLACEHOLDER

using OpenCBS.CoreDomain.Accounting;
using OpenCBS.Shared;


namespace OpenCBS.CoreDomain
{
    public class Teller
    {
        private int? _id;
        public int? Id
        {
            get { return _id == 0 ? null : _id; }
            set { _id = value; }
        }
        public string Name;
        public string Description;
        public Account Account;
        public bool Deleted;
        public Branch Branch;
        public OCurrency MinAmountTeller;
        public OCurrency MaxAmountTeller;
        public OCurrency MinAmountWithdrawal;
        public OCurrency MaxAmountWithdrawal;
        public OCurrency MinAmountDeposit;
        public OCurrency MaxAmountDeposit;
        public Currency Currency;
        public User User;

        public Teller Vault;

        private static Teller _currentTeller = new Teller();

        static public Teller CurrentTeller
        {
            get { return _currentTeller; }
            set { _currentTeller = value; }
        }

        public OCurrency CashInTill { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
