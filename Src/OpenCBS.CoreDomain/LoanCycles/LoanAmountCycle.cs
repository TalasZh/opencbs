// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.LoanCycles
{
   public  class LoanAmountCycle:Cycle
    {
       public LoanAmountCycle() {}

       public LoanAmountCycle(int id, int loanCycle,  OCurrency min, OCurrency max, int cycleObjectId, int cycleId)
       {
           Id = id;
           LoanCycle = loanCycle;
           Min = min;
           Max = max;
           CycleObjectId = cycleObjectId;
           CycleId = cycleId;
       }
    }
}
