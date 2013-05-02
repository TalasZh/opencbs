using System;

namespace OpenCBS.Enums
{
    [Serializable]
    public enum OLateRepaymentTypes
    {
        Principal = 1,
        Interest = 2,
        Olb = 3,
        Amount = 4,
        Fixed = 5
    }
}