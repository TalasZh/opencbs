using System;
using OpenCBS.CoreDomain.Events;
using OpenCBS.Enums;
using OpenCBS.CoreDomain.Products;
using OpenCBS.CoreDomain.EconomicActivities;

namespace OpenCBS.CoreDomain.Accounting
{
    [Serializable]
    public class ContractAccountingRule : IAccountingRule
    {
        #region IAccountingRule Members
        public int Id { get; set; }
        public Account DebitAccount { get; set; }
        public Account CreditAccount { get; set; }
        public bool Deleted { get; set; }
        public OBookingDirections BookingDirection { get; set; }
        public EventType EventType { get; set; }
        public EventAttribute EventAttribute { get; set; }
        public int Order { get; set; }
        public string Description { get; set; }
        #endregion

        public ContractAccountingRule()
        {
            PaymentMethod = new PaymentMethod();
        }
        public OProductTypes ProductType { get; set; }
        public LoanProduct LoanProduct { get; set; }
        public Currency Currency { get; set; }
        public ISavingProduct SavingProduct { get; set; }
        public OClientTypes ClientType { get; set; }
        public EconomicActivity EconomicActivity { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public ContractAccountingRule Copy()
        {
            return (ContractAccountingRule) MemberwiseClone();
        }
    }
}
