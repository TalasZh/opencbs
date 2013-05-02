// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.CalculateFeesForAnticipatedRepayment;
using OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.MaxRepayment;
using OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment;
using OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayNextInstallments;
using OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayRealInterestInstallments;
using OpenCBS.Enums;
using OpenCBS.Shared;
using OpenCBS.Shared.Settings;

namespace OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment
{
    /// <summary>
    /// Summary description for ICreditContractRepayment.
    /// </summary>
    [Serializable]
    public class CreditContractRepayment
    {
        private readonly Repayment.RepayLateInstallments.CalculateInstallments _calculateInstallments;
        private readonly CalculateRealInterestInstallments _calculateRealInterestInstallments;
        private readonly CalculateAnticipatedFeesStrategy _feesForAnticipatedRepayment;
        private readonly RepayNextInstallmentsStrategy _repayNextInstallments;
        private readonly CalculateMaximumAmountToRepayStrategy _amountToRepayTotalyLoan;
        private readonly CalculateMaximumAmountToRegradingLoanStrategy _amountToRegradingLoan;
        private readonly CalculateAmountToRepaySpecifiedInstallmentStrategy _amountToRepayInstallment;

        private readonly RepaymentMethod _repaymentMethod;

        private readonly DateTime _date;
        private readonly int _installmentNumber;
        private OCurrency _maximumAmountAuthorizeToRepay;
        private OCurrency _maximumAmountToRegradingLoan;
        private OCurrency _maximumAmountForEscapedMember = 0;
        private readonly ApplicationSettings _generalSettings;
        private readonly NonWorkingDateSingleton _nWds;

        public List<Installment> PaidIstallments;
        public CreditContractOptions LoanOptions;
        private readonly Loan _loan;

        public CreditContractRepayment(Loan contract, CreditContractOptions creditOptions, DateTime pDate,int pInstallmentNumber, User user, ApplicationSettings pGeneralSettings,NonWorkingDateSingleton pNonWorkingDate)
        {
            _generalSettings = pGeneralSettings;
            _nWds = pNonWorkingDate;

            _date = pDate.Date;
            _installmentNumber = pInstallmentNumber;

            PaidIstallments = new List<Installment>();

            _amountToRepayTotalyLoan = new CalculateMaximumAmountToRepayStrategy(creditOptions, contract.Copy(), user, _generalSettings,_nWds);
            _amountToRegradingLoan = new CalculateMaximumAmountToRegradingLoanStrategy(creditOptions, contract.Copy(), user, _generalSettings, _nWds);
            _amountToRepayInstallment = new CalculateAmountToRepaySpecifiedInstallmentStrategy(creditOptions, contract.Copy(), user, _generalSettings, _nWds);

            _calculateInstallments = new Repayment.RepayLateInstallments.CalculateInstallments(creditOptions, contract, user, _generalSettings, _nWds);
            _calculateRealInterestInstallments = new CalculateRealInterestInstallments(creditOptions, _amountToRepayTotalyLoan, contract, _generalSettings, _nWds);
            
            _feesForAnticipatedRepayment = new CalculateAnticipatedFeesStrategy(creditOptions, contract, _generalSettings);
            _repayNextInstallments = new RepayNextInstallmentsStrategy(contract, creditOptions, user, _generalSettings);
            _repaymentMethod = new RepaymentMethod(contract,creditOptions);

            _loan = contract;

            CalculateMaximumAmountAuthorizedToRepay();
            CalculateAmountToRegradingLoan();
            CalculateMaximumAmountForEscapedMember();
            LoanOptions = creditOptions;
        }

        public OCurrency   AmountToRepayInstallment
        {
            get { return _amountToRepayInstallment.CalculateAmountToRepaySpecifiedInstallment(_date, _installmentNumber); }
        }

        public OCurrency   MaximumAmountAuthorizeToRepay
        {
            get { return _maximumAmountAuthorizeToRepay; }
        }

        public OCurrency   MaximumAmountToRegradingLoan
        {
            get { return _maximumAmountToRegradingLoan; }
        }

        public OCurrency MaximumAmountForEscapedMember
        {
            get { return _maximumAmountForEscapedMember; }
        }

        private void CalculateAmountToRegradingLoan()
        {
            _maximumAmountToRegradingLoan = _amountToRegradingLoan.CalculateMaximumAmountToRegradingLoan(_date);
        }

        private void CalculateMaximumAmountAuthorizedToRepay()
        {
            _maximumAmountAuthorizeToRepay = _amountToRepayTotalyLoan.CalculateMaximumAmountAuthorizedToRepay(_date);
        }

        private void CalculateMaximumAmountForEscapedMember()
        {
            if(_loan.EscapedMember != null)
            _maximumAmountForEscapedMember = _amountToRepayTotalyLoan.CalculateMaximumAmountForEscapedMember(_date);
        }

        public void Repay(OCurrency amountPaid, 
                         ref OCurrency penaltiesEvent, 
                         ref OCurrency commissionsEvent, 
                         ref OCurrency interestsEvent,
                         ref OCurrency iterestPrepayment, 
                         ref OCurrency principalEvent, 
                         ref OCurrency manualInterestAmount, 
                         ref OPaymentType paymentType)
        {
            _repaymentMethod.Repay(ref amountPaid, ref penaltiesEvent, ref commissionsEvent, ref interestsEvent);
            //repayLateInstallment + eventualy a good installment => Penalties
            _calculateInstallments.CalculateNewInstallmentsWithLateFees(_date);

            if (paymentType == OPaymentType.PartialPayment 
                || paymentType == OPaymentType.TotalPayment
                || paymentType == OPaymentType.ProportionalPayment)
                _calculateInstallments.CalculateAnticipateRepayment(_date, paymentType, amountPaid);

            switch (paymentType)
            {
                case OPaymentType.StandardPayment:
                    {
                        if (LoanOptions.LoansType == OLoanTypes.DecliningFixedPrincipalWithRealInterest)
                        {
                            _calculateRealInterestInstallments.RepayInstallments(_date,
                                                                                ref amountPaid,
                                                                                ref interestsEvent,
                                                                                ref principalEvent,
                                                                                ref penaltiesEvent,
                                                                                ref commissionsEvent,
                                                                                ref paymentType);
                            PaidIstallments.AddRange(AddInstallments(_calculateRealInterestInstallments.PaidIstallments, PaidIstallments));
                        }
                        else
                        {
                            _calculateInstallments.RepayInstallments(_date,
                                                                 ref amountPaid,
                                                                 ref interestsEvent,
                                                                 ref principalEvent,
                                                                 ref penaltiesEvent,
                                                                 ref commissionsEvent);
                        }

                        break;
                    }

                case OPaymentType.TotalPayment:
                    {
                        _calculateInstallments.RepayTotalAnticipateInstallments(_date, ref amountPaid,
                                                                                ref interestsEvent, ref principalEvent,
                                                                                ref penaltiesEvent,
                                                                                ref commissionsEvent);
                        break;
                    }

                case OPaymentType.PartialPayment:
                    {
                        _calculateInstallments.RepayPartialAnticipateInstallments(_date, ref amountPaid,
                                                                                  ref interestsEvent,
                                                                                  ref principalEvent,
                                                                                  ref penaltiesEvent,
                                                                                  ref commissionsEvent);
                        break;
                    }
                case OPaymentType.ProportionalPayment:
                    {
                        _calculateInstallments.RepayProportinalyInstallments(_date, ref amountPaid,
                                                                                  ref interestsEvent,
                                                                                  ref principalEvent,
                                                                                  ref penaltiesEvent,
                                                                                  ref commissionsEvent);
                        break;
                    }
            }

            PaidIstallments.AddRange(AddInstallments(_calculateInstallments.PaidIstallments, PaidIstallments));
            PaidIstallments.AddRange(AddInstallments(_repaymentMethod.PaidIstallments, PaidIstallments));

            //old legacy one day we will remove it
            if (paymentType == OPaymentType.StandardPayment)
                _feesForAnticipatedRepayment.calculateFees(_date, ref amountPaid, ref commissionsEvent);

            //repaynextinstallment
            if (amountPaid > 0)
            {
                _repayNextInstallments.RepayNextInstallments(ref amountPaid, ref interestsEvent, ref iterestPrepayment,
                                                             ref principalEvent, ref penaltiesEvent,
                                                             ref commissionsEvent);

                PaidIstallments.AddRange(AddInstallments(_repayNextInstallments.PaidInstallments, PaidIstallments));
            }
        }

        List<Installment> AddInstallments(List<Installment> paidInstallments, List<Installment> listedInstallment)
        {
            List<Installment> list = new List<Installment>();

            foreach (Installment paidIstallment in paidInstallments)
            {
                bool toAdd = true;

                foreach (Installment inst in listedInstallment)
                {
                    if (paidIstallment == inst)
                        toAdd = false;
                }

                if (toAdd)
                    list.Add(paidIstallment);
            }

            return list;
        }
    }
}
