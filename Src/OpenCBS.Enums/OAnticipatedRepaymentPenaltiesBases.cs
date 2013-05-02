using System;

namespace OpenCBS.Enums
{
    [Serializable]
    public enum OAnticipatedRepaymentPenaltiesBases
    {
        RemainingInterest = 1,
        RemainingOLB = 2,
        PrepaidPrincipal = 3
    }
}
