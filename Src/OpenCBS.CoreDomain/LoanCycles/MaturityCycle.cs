// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenCBS.CoreDomain.LoanCycles
{
    public class MaturityCycle: Cycle
    {
       public MaturityCycle() {}
       public MaturityCycle(int id, int loanCycle, decimal min, decimal max, int cycleObjectId, int cycleId)
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
