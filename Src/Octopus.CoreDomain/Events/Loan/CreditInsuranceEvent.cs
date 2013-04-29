using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Octopus.CoreDomain.Events.Loan;
using Octopus.Shared;

namespace Octopus.CoreDomain.Events
{
    public class CreditInsuranceEvent:Event
    {
        public CreditInsuranceEvent()
        {
            Id = -1;
            Commission = 0;
            Date = TimeProvider.Today;
        }
        public OCurrency Commission { get; set; }
        public OCurrency Principal { get; set; }
        public override string Code
        {
            get {return _code;}
            set {_code = value;}
        }
    }
}
