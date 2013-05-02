using System;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.FeesRepayment
{
    [Serializable]
    public class RepayCommisionStrategy
    {
        private readonly CreditContractOptions _pCCO;
        public RepayCommisionStrategy(CreditContractOptions pCCO)
        {
            _pCCO = pCCO;
        }

        public void RepayCommission(Installment pInstallment, ref OCurrency pAmountPaid, ref OCurrency pCommisionEvent)
        {
            if (!_pCCO.CancelFees)
                AutomaticRepayMethod(pInstallment, ref pAmountPaid, ref pCommisionEvent);
            else
                ManualRepayMethod(pInstallment, ref pAmountPaid, ref pCommisionEvent);
        }

        private static void AutomaticRepayMethod(Installment pInstallment, ref OCurrency pAmountPaid, ref OCurrency pCommisionEvent)
        {
            if (AmountComparer.Compare(pAmountPaid, pInstallment.CommissionsUnpaid) > 0)
            {
                pCommisionEvent += pInstallment.CommissionsUnpaid;
                pAmountPaid -= pInstallment.CommissionsUnpaid;
                pInstallment.PaidCommissions += pInstallment.CommissionsUnpaid;
                pInstallment.CommissionsUnpaid = 0;
            }
            else
            {
                pCommisionEvent += pAmountPaid;
                pInstallment.PaidCommissions += pAmountPaid;
                pInstallment.CommissionsUnpaid -= pAmountPaid;
                pAmountPaid = 0;
            }
        }

        private static void ManualRepayMethod(Installment pInstallment, ref OCurrency pAmountPaid, ref OCurrency pCommisionEvent)
        {
            pInstallment.PaidCommissions = pCommisionEvent;
            pInstallment.CommissionsUnpaid = 0;
            pAmountPaid -= pCommisionEvent;
            pCommisionEvent = 0;
            //Nothing to modify
        }
    }
}
