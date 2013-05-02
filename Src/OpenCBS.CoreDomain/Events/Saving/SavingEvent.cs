// LICENSE PLACEHOLDER

using System;
using OpenCBS.CoreDomain.Contracts.Savings;
using OpenCBS.CoreDomain.Events.Loan;
using OpenCBS.CoreDomain.Products;
using OpenCBS.Shared;
using OpenCBS.Enums;

namespace OpenCBS.CoreDomain.Events.Saving
{
    [Serializable]
	public abstract class SavingEvent : Event, IComparable, ISavingsFees
	{
        protected bool _isDebit;
		public override string Code { get; set; }
		public OCurrency Amount { get; set; }
        public OCurrency Fee { get; set; }
        public Type ProductType { get; set; }
//        public OPaymentMethods? SavingsMethod { get; set; }
        public OSavingsMethods? SavingsMethod { get; set; }
        public bool IsPending { get; set; }
        public int? PendingEventId { get; set; }
        public int? LoanEventId { get; set; }
        public new DateTime? CancelDate { get; set; }
        public virtual string ExtraInfo
        {
            get
            {
                return string.Empty;
            }
        }
        public bool IsDebit
        {
            get
            {
                return _isDebit;
            }
        }
        public ISavingsContract Target { get; set; }

		public override Event Copy()
		{
			return (SavingEvent)MemberwiseClone();
		}

		#region IComparable Members

		public static bool operator >(SavingEvent a, SavingEvent b)
		{
			return a.Date.CompareTo(b.Date) > 0;
		}

		public static bool operator <(SavingEvent a, SavingEvent b)
		{
			return a.Date.CompareTo(b.Date) < 0;
		}

		int IComparable.CompareTo(object obj)
		{
			return Date.CompareTo(((SavingEvent)obj).Date);
        }
        #endregion

        public virtual OCurrency GetAmountForBalance()
        {
            return 0m;
        }

        public virtual OCurrency GetFeeForBalance()
        {
            return 0m;
        }

        public OCurrency GetBalanceChunk(DateTime date)
        {
            if (Deleted || Date.Date > date.Date)
                return 0m;

            OCurrency retval = 0m;
            retval += GetAmountForBalance();
            retval += GetFeeForBalance();
            return retval;
        }
	}

    [Serializable]
    public abstract class SavingPositiveEvent : SavingEvent
    {
        public override sealed OCurrency GetAmountForBalance()
        {
            return Amount;
        }
    }

    [Serializable]
    public abstract class SavingNegativeEvent : SavingEvent
    {
        public override sealed OCurrency GetAmountForBalance()
        {
            if (!Amount.HasValue) return 0m;
            return (decimal) -1*Amount;
        }

        public override sealed OCurrency GetFeeForBalance()
        {
            if (!Fee.HasValue) return 0m;
            return (decimal) -1*Fee;
        }
    }
}
