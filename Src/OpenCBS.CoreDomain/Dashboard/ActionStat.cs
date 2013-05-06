
using System;

namespace OpenCBS.CoreDomain.Dashboard
{
    public class ActionStat
    {
        public DateTime Date { get; set; }
        public int NumberDisbursed { get; set; }
        public decimal AmountDisbursed { get; set; }
        public int NumberRepaid { get; set; }
        public decimal AmountRepaid { get; set; }
        public decimal OlbGrowth
        {
            get { return AmountDisbursed - AmountRepaid; }
        }
    }
}
