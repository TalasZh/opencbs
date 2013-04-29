using System;
using Octopus.CoreDomain.Events.Loan;
using Octopus.Shared;

namespace Octopus.CoreDomain.Events
{
    /// <summary>
    /// TrancheEvent class
    /// </summary>
    [Serializable]
    public class TrancheEvent : Event
    {
        public override string Code
        {
            get { return "TEET"; }
            set { _code = value; }
        }

        public OCurrency InterestRate { get; set; }
        public OCurrency Amount { get; set; }
        public int? Maturity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CloseDate { get; set; }
        public bool Closed { get; set; }
        public bool ApplyNewInterest { get; set; }
        /// <summary>
        /// Number of tranche event
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// Installment on which the tranche was added
        /// </summary>
        public int StartedFromInstallment { get; set; }

        public override string Description { get; set; }

        public override Event Copy()
        {
            return (TrancheEvent) MemberwiseClone();
        }
    }
}
