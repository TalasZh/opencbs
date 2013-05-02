// LICENSE PLACEHOLDER

using OpenCBS.Enums;
using System;

namespace OpenCBS.CoreDomain.Contracts.Loans.CalculateInstallments
{
    /// <summary>
    /// Summary description for CalculateInstallmentsOption.
    /// </summary>
    [Serializable]
    public class CalculateInstallmentsOptions
    {
        private readonly OLoanTypes _loanType;
        private readonly bool _isExotic;
        private readonly Loan _contract;
        private readonly bool _changeDate;
        private readonly DateTime _startDate;

        public CalculateInstallmentsOptions(DateTime pStartDate, OLoanTypes pLoanType, bool pIsExotic, Loan pContract, bool pChangeDate)
        {
            _loanType = pLoanType;
            _isExotic = pIsExotic;
            _contract = pContract;
            _changeDate = pChangeDate;
            _startDate = pStartDate;
        }
		
        public Loan Contract
        {
            get { return _contract; }
        }

        public bool ChangeDate
        {
            get { return _changeDate; }
        }

        public OLoanTypes LoanType
        {
            get { return _loanType; }
        }

        public bool IsExotic
        {
            get { return _isExotic; }
        }

        public DateTime StartDate
        {
            get { return _startDate; }

        }
    }
}
