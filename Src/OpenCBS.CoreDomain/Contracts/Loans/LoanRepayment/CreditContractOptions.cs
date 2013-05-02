// LICENSE PLACEHOLDER

using System;
using OpenCBS.Enums;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment
{
    /// <summary>
    /// Summary description for CreditContractOptions.
    /// </summary>
    [Serializable]
    public class CreditContractOptions
    {
        private readonly OLoanTypes _loanType;
        private bool _keepExpectedInstallments;
        private readonly bool _cancelFees;
        private readonly OCurrency _manualFeesAmount;
        private readonly OCurrency _manualCommissionAmount;
        private readonly bool _cancelInterests;
        private OCurrency _manualInterestsAmount;
        private readonly OAnticipatedRepaymentPenaltiesBases _anticipatedTotalRepaymentPenaltiesBase;
        private readonly bool _isForExoticProduct;
        private bool _payProportional;
        
        public CreditContractOptions(OLoanTypes pLoanType, bool pKeepExpectedInstallments,
            bool pCancelFees, OCurrency pManualFeesAmount, OCurrency pManualCommissionAmount,
            bool pCancelInterests, OCurrency pManualInterestsAmount,
            OAnticipatedRepaymentPenaltiesBases pAnticipatedTotalRepaymentPenaltiesBase)
        {
            _loanType = pLoanType;
            _keepExpectedInstallments = pKeepExpectedInstallments;
            _cancelFees = pCancelFees;
            _manualFeesAmount = pManualFeesAmount;
            _manualCommissionAmount = pManualCommissionAmount;
            _cancelInterests = pCancelInterests;
            _manualInterestsAmount = pManualInterestsAmount;
            _anticipatedTotalRepaymentPenaltiesBase = pAnticipatedTotalRepaymentPenaltiesBase;
        }

        public CreditContractOptions(OLoanTypes pLoanType, bool pKeepExpectedInstallments,
            bool pCancelFees, OCurrency pManualFeesAmount, OCurrency pManualCommissionAmount,
            bool pCancelInterests, OCurrency pManualInterestsAmount,
            OAnticipatedRepaymentPenaltiesBases pAnticipatedTotalRepaymentPenaltiesBase,
            bool pIsForExotic, bool payProportional)
        {
            _loanType = pLoanType;
            _keepExpectedInstallments = pKeepExpectedInstallments;
            _cancelFees = pCancelFees;
            _manualFeesAmount = pManualFeesAmount;
            _manualCommissionAmount = pManualCommissionAmount;
            _cancelInterests = pCancelInterests;
            _manualInterestsAmount = pManualInterestsAmount;
            _anticipatedTotalRepaymentPenaltiesBase = pAnticipatedTotalRepaymentPenaltiesBase;
            _isForExoticProduct = pIsForExotic;
            _payProportional = payProportional;
        }

        public OLoanTypes LoansType
        {
            get { return _loanType; }
        }

        public bool KeepExpectedInstallments
        {
            get { return _keepExpectedInstallments; }
            set { _keepExpectedInstallments = value; }
        }

        public bool PayProportional
        {
            get { return _payProportional; }
            set { _payProportional = value; }
        }

        public bool CancelFees
        {
            get { return _cancelFees; }
        }

        public OAnticipatedRepaymentPenaltiesBases AnticipatedTotalRepaymentPenaltiesBase
        {
            get { return _anticipatedTotalRepaymentPenaltiesBase; }
        }

        public OCurrency ManualFeesAmount
        {
            get { return _manualFeesAmount; }
        }

        public OCurrency ManualCommissionAmount
        {
            get { return _manualCommissionAmount; }
        }

        public bool CancelInterests
        {
            get { return _cancelInterests; }
        }

        public OCurrency ManualInterestsAmount
        {
            get { return _manualInterestsAmount; }
        }

        public void UseManualInterestsAmount(OCurrency pAmount)
        {
            _manualInterestsAmount = _manualInterestsAmount - pAmount;
        }

        public bool IsForExotic
        {
            get { return _isForExoticProduct; }
        }
    }
}
