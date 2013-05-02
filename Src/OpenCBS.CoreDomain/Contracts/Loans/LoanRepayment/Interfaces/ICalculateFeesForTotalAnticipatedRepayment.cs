// LICENSE PLACEHOLDER

using System;
using OpenCBS.Shared;
namespace OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Interfaces
{
	/// <summary>
	/// Summary description for IAnticipatedRepaymentFees.
	/// </summary>
	public interface ICalculateFeesForTotalAnticipatedRepayment
	{
		bool CalculateFees(DateTime date,OCurrency initialAmountPaid, ref OCurrency amoundPaid, ref OCurrency feesEvent);
	}
}
