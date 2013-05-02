// LICENSE PLACEHOLDER

using System;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Contracts.Loans.Installments
{
    /// <summary>
    /// Summary description for IInstallment.
    /// </summary>
    public interface IInstallment
    {
        DateTime StartDate { get; set; }
        OCurrency OLB { get; set; }
        DateTime ExpectedDate{get;set;}
        OCurrency InterestsRepayment{get;set;}
        OCurrency CapitalRepayment{get;set;}
        OCurrency PaidInterests{get;set;}
        OCurrency PaidCapital{get;set;}
        OCurrency PaidFees { get; set; }
        DateTime? PaidDate{get;set;}
        int Number{get;set;}
        bool IsRepaid{get;}
        OCurrency Amount{get;}
        string Comment { get; set; }
        bool IsPending { get; set; }
    }
}
