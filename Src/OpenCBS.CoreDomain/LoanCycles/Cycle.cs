// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.LoanCycles
{
    public abstract class Cycle
    {
       public int Id { get; set; }
       public int LoanCycle { get; set; }
       public OCurrency Min { get; set; }
       public OCurrency Max { get; set; }
       public int CycleObjectId { get; set; }
       public int? CycleId { get; set; }

       public Cycle() {}

       public Cycle(int id, int loanCycle,  OCurrency min, OCurrency max, int cycleObjectId, int cycleId)
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
