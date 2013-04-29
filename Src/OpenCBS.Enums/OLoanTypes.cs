using System;

namespace Octopus.Enums
{
    [Serializable]
    public enum OLoanTypes
    {
        Flat = 1,
        DecliningFixedPrincipal = 2,
        DecliningFixedInstallments = 3,
        DecliningFixedPrincipalWithRealInterest = 4
    }
}
