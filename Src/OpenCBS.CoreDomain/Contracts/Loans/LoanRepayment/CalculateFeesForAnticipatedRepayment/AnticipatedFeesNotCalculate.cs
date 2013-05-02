// LICENSE PLACEHOLDER

using System;
using OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Interfaces;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.CalculateFeesForAnticipatedRepayment
{
    /// <summary>
    /// Summary description for AnticipatedFeesNotCalculate.
    /// </summary>
    [Serializable]
    public class AnticipatedFeesNotCalculate : ICalculateFeesForAnticipatedRepayment
    {
        public void CalculateFees(DateTime pDate, ref OCurrency pAmountPaid, ref OCurrency pFeesEvent)
        {	
            //Nothing to calculate
        }
    }
}
