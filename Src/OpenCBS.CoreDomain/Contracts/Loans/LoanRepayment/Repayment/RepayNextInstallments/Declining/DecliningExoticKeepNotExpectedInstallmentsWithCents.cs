// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Interfaces;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayNextInstallments.Declining
{
    /// <summary>
    /// Summary description for DecliningExoticKeepNotExpectedInstallmentsWithCents.
    /// </summary>
    [Serializable]
    public class DecliningExoticKeepNotExpectedInstallmentsWithCents : IRepayNextInstallments
    {
        private readonly Loan _contract;

        public List<Installment> _paidInstallments;

        public List<Installment> PaidInstallments
        {
            get { return _paidInstallments; }
            set { _paidInstallments = value; }
        }

        public DecliningExoticKeepNotExpectedInstallmentsWithCents(Loan contract)
        {
            _paidInstallments = new List<Installment>();
            _contract = contract;
        }

        /// <summary>
        /// This method recalculates installments for a declining rate exotic loan in case of over payment.
        /// </summary>
        public void RepayNextInstallments(ref OCurrency amountPaid, ref OCurrency interestEvent, ref OCurrency interestPrepayment, ref OCurrency principalEvent, ref OCurrency feesEvent, ref OCurrency commissionsEvent)
        {
            OCurrency totalAmount = 0;
            bool firstInstallmentToRepay = false;
            double baseTocalculatePrincipalCoeff = 0;

            if(amountPaid > 0)
            {
                for (int i = 0; i < _contract.NbOfInstallments; i++)
                {
                    Installment installment = _contract.GetInstallment(i);
                    if (installment.IsRepaid &&  installment.Number > _contract.GracePeriod)
                        baseTocalculatePrincipalCoeff += _contract.Product.ExoticProduct.GetExoticInstallment(i - _contract.GracePeriod.Value).PrincipalCoeff;
                }

                baseTocalculatePrincipalCoeff = 1 - baseTocalculatePrincipalCoeff;

                for(int i = 0;i < _contract.NbOfInstallments;i++)
                {
                    Installment installment = _contract.GetInstallment(i);
                    if(!installment.IsRepaid && installment.Number <= _contract.GracePeriod)  //during Grace Period
                    {
                        installment.OLB -= amountPaid;
                        installment.CapitalRepayment = 0;
                        installment.InterestsRepayment = Math.Round(installment.OLB.Value * Convert.ToDecimal(_contract.InterestRate),2);
                    }
                    else if(!installment.IsRepaid && !firstInstallmentToRepay)
                    {
                        ExoticInstallment exoticInstallment = _contract.Product.ExoticProduct.GetExoticInstallment(i - _contract.GracePeriod.Value);
                        firstInstallmentToRepay = true;
                        installment.OLB -= amountPaid;
                        totalAmount = installment.OLB;

                        installment.CapitalRepayment = Math.Round(totalAmount.Value * Convert.ToDecimal(exoticInstallment.PrincipalCoeff / baseTocalculatePrincipalCoeff),2);
                        installment.InterestsRepayment = Math.Round(installment.OLB.Value * Convert.ToDecimal(_contract.InterestRate),2);
                    }
                    else if(!installment.IsRepaid)
                    {
                        ExoticInstallment exoticInstallment = _contract.Product.ExoticProduct.GetExoticInstallment(i - _contract.GracePeriod.Value);
                        Installment _installment = _contract.GetInstallment(i - 1);
                        installment.OLB  = _installment.OLB - _installment.CapitalRepayment;

                        installment.CapitalRepayment = Math.Round(totalAmount.Value * Convert.ToDecimal(exoticInstallment.PrincipalCoeff / baseTocalculatePrincipalCoeff),2);
                        installment.InterestsRepayment = Math.Round(installment.OLB.Value * Convert.ToDecimal(_contract.InterestRate),2); 
                    }

                    _paidInstallments.Add(installment);
                }

                principalEvent += amountPaid;
                amountPaid = 0;
            }
        }
    }
}
