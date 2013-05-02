// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Interfaces;
using OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.FeesRepayment;
using OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.InterestRepayment;
using OpenCBS.Shared;


namespace OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayNextInstallments.Declining
{
    /// <summary>
    /// Summary description for DecliningKeepExpectedInstallments.
    /// </summary>
    [Serializable]
    public class DecliningKeepExpectedInstallments : IRepayNextInstallments
    {
        private readonly Loan _contract;
        private readonly RepayFeesStrategy _methodToRepayFees;
        private readonly RepayCommisionStrategy _methodToRepayCommission;
        private readonly RepayInterestStrategy _methodToRepayInterest;

        public List<Installment> _paidInstallments;

        public List<Installment> PaidInstallments
        {
            get { return _paidInstallments; }
            set { _paidInstallments = value; }
        }

        public DecliningKeepExpectedInstallments(Loan pContract, CreditContractOptions pCco)
        {
            _paidInstallments = new List<Installment>();
            _contract = pContract;
            _methodToRepayInterest = new RepayInterestStrategy(pCco);
            _methodToRepayFees = new RepayFeesStrategy(pCco);
            _methodToRepayCommission = new RepayCommisionStrategy(pCco);
        }

        public void RepayNextInstallments(ref OCurrency amountPaid, ref OCurrency interestEvent, ref OCurrency interestPrepayment, ref OCurrency principalEvent, ref OCurrency feesEvent, ref OCurrency commissionsEvent)
        {
            if (amountPaid == 0) return;
            for(int i = 0 ; i < _contract.NbOfInstallments ; i++)
            {
                Installment installment = _contract.GetInstallment(i);
                if (!installment.IsRepaid && amountPaid > 0)
                {
                    OCurrency initiAlamount = amountPaid;
                    //commission
                    _methodToRepayCommission.RepayCommission(installment, ref amountPaid, ref commissionsEvent);
                    if (amountPaid == 0) break;

                    //penalty
                    _methodToRepayFees.RepayFees(installment, ref amountPaid, ref feesEvent);
                    if (amountPaid == 0) break;

                    //Interests
                    if (amountPaid == 0) return;
                    _methodToRepayInterest.RepayInterest(installment, ref amountPaid, ref interestEvent,ref interestPrepayment);

                    // Principal
                    if (amountPaid == 0)
                    {
                        _paidInstallments.Add(installment);
                        return;
                    }

                    if (AmountComparer.Compare(amountPaid, installment.CapitalRepayment - installment.PaidCapital) > 0)
                    {
                        OCurrency  principalHasToPay = installment.CapitalRepayment - installment.PaidCapital;
                        installment.PaidCapital = installment.CapitalRepayment;
                        amountPaid -= principalHasToPay;
                        principalEvent += principalHasToPay;
                    }
                    else
                    {
                        installment.PaidCapital += amountPaid;
                        installment.PaidCapital = Math.Round(installment.PaidCapital.Value, 2);
                        principalEvent += amountPaid;
                        amountPaid = 0;
                    }

                    if (initiAlamount != amountPaid)
                        _paidInstallments.Add(installment);
                }
            }
        }
    }
}
