// LICENSE PLACEHOLDER

using System;
using OpenCBS.CoreDomain.Events.Loan;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Events
{
    [Serializable]
    public class StatisticalProvisionningEvent : Event
    {
        internal OCurrency   _OLBAmount;
        internal OCurrency   _provisionning;

        public override string Code
        {
            get { return "GOSE"; }
            set { _code = value; }
        }

        public OCurrency   OLBAmount
        {
            get { return _OLBAmount; }
            set { _OLBAmount = value; }
        }

        public OCurrency   Provisionning
        {
            get { return _provisionning; }
            set { _provisionning = value; }
        }

        public override string Description { get; set; }

        public override Event Copy()
        {
            return (StatisticalProvisionningEvent)MemberwiseClone();
        }
    }
}
