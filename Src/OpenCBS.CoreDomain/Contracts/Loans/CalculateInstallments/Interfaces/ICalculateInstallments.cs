// LICENSE PLACEHOLDER

using System.Collections.Generic;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;

namespace OpenCBS.CoreDomain.Contracts.Loans.CalculateInstallments.Interfaces
{
    /// <summary>
    /// Summary description for ICalculateInstallments.
    /// </summary>
    public interface ICalculateInstallments 
    {
        List<Installment> CalculateInstallments(bool changeDate);
    }
}
