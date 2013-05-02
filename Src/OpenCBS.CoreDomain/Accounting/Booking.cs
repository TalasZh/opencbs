// LICENSE PLACEHOLDER

using System;
using OpenCBS.CoreDomain.Accounting.Interfaces;
using OpenCBS.Enums;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Accounting
{
	/// <summary>
	/// Description r�sum�e de Booking.
    /// </summary>
    [Serializable]
	public class Booking : IBooking
	{
	    private OCurrency   _amount;
        public int Id { get; set; }
	    public OAccountingLabels Label { get; set; }
	    public int Number { get; set; }
	    public Account CreditAccount { get; set; }
        public string CreditAccountNumber { get { return CreditAccount.Number; } }
	    public bool IsExported { get; set; }
        public bool ToExport { get; set; }
	    public Account DebitAccount { get; set; }
        public string DebitAccountNumber { get { return DebitAccount.Number; } }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public Currency Currency { get; set; }
        public double ExchangeRate { get; set; }
        public User User { get; set; }
        public int EventId { get; set; }
        public string EventType { get; set; }
        public int RuleId { get; set; }
        public int ClosureId { get; set; }
        public int ContractId { get; set; }
        public string Code { get; set; }
        public Branch Branch { get; set; }
        public FiscalYear FiscalYear { get; set; }
        public OMovementType Type { get; set; }

	    public Booking(){}

	    /// <summary>
	    /// use this constructor only if booking isn't a Repayment
	    /// </summary>
	    /// <param name="pNumber"></param>
	    /// <param name="pCreditaccount"></param>
	    /// <param name="pAmount"></param>
	    /// <param name="pDebitaccount"></param>
	    /// <param name="pDate"></param>
	    /// <param name="branch"> </param>
	    public Booking(int pNumber, Account pCreditaccount, OCurrency pAmount, Account pDebitaccount, DateTime pDate, Branch branch)
        {
            Number = pNumber;
            CreditAccount = pCreditaccount;
            DebitAccount = pDebitaccount;
            _amount = pAmount;
            Label = OAccountingLabels.Nothing;
            Date = pDate;
            Branch = branch;
        }

        public Booking(int pNumber, Account pCreditaccount, OCurrency pAmount, Account pDebitaccount, OAccountingLabels pLabel, DateTime pDate, Branch branch)
		{
            Number = pNumber;
            CreditAccount = pCreditaccount;
            DebitAccount = pDebitaccount;
            _amount = pAmount;
		    Label = pLabel;
            Date = pDate;
            Branch = branch;
		}

	    public OCurrency Amount
		{
			get{return _amount;}
			set{_amount = value;}
        }

		public OCurrency AmountRounding
		{
            get{return Math.Round(_amount.Value,2);}
		}

        public override string ToString()
        {
            return Name + "  " + DebitAccount + " : " + CreditAccount;
        }
	}
}
