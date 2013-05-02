// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Interfaces;
using OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayNextInstallments.Declining;
using OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayNextInstallments.Flat;
using OpenCBS.Enums;
using OpenCBS.Shared;
using OpenCBS.Shared.Settings;

namespace OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayNextInstallments
{
    /// <summary>
    /// Summary description for RepayNextInstallmentsStrategy.
    /// </summary>
    [Serializable]
    public class RepayNextInstallmentsStrategy
    {
        private readonly IRepayNextInstallments _repayNextInstallments;
        public List<Installment> PaidInstallments{ get; set;}

        public RepayNextInstallmentsStrategy(Loan contract,CreditContractOptions cCO, User pUser, ApplicationSettings pGeneralSettings)
        {
            if(cCO.LoansType != OLoanTypes.Flat) //declining
            {
                if(cCO.KeepExpectedInstallments)
                    _repayNextInstallments = new DecliningKeepExpectedInstallments(contract, cCO);
                else
                {
                    if (contract.Product.ExoticProduct != null)
                    {
                        if(contract.UseCents)
                            _repayNextInstallments = new DecliningExoticKeepNotExpectedInstallmentsWithCents(contract);
                        else
                            _repayNextInstallments = new DecliningExoticKeepNotExpectedInstallmentsWithNoCents(contract);
                    }
				    //???????????????????????????????????????
                    else
                        _repayNextInstallments = new DecliningKeepNotExpectedInstallments(contract, pUser, pGeneralSettings);
                }
            }
            else //flat
            {
                if (cCO.KeepExpectedInstallments)
                {
                    _repayNextInstallments = new FlateKeepExpectedInstallments(contract, cCO);
                }
                else
                {
                    if (contract.Product.ExoticProduct != null)
                    {
                        if (contract.UseCents)
                        {
                            _repayNextInstallments = new FlatExoticKeepNotExpectedInstallmentsWithCents(contract);
                        }
                        else
                        {
                            _repayNextInstallments = new FlatExoticKeepNotExpectedInstallmentsWithNoCents(contract);
                        }
                    }

                    else
                    {   // ??????????????????????????????????????????????????????????????
                        if (contract.UseCents)
                            _repayNextInstallments = new FlateKeepNotExpectedInstallmentsWithCents(contract, pGeneralSettings);
                        else
                            _repayNextInstallments = new FlateKeepNotExpectedInstallmentsWithNoCents(contract, pGeneralSettings);
                    }
                }
            }
        }

        public void RepayNextInstallments(ref OCurrency amountPaid, ref OCurrency interestEvent, ref OCurrency interestPrepayment, ref OCurrency principalEvent, ref OCurrency feesEvent, ref OCurrency commissionsEvent)
        {
            _repayNextInstallments.RepayNextInstallments(ref amountPaid, ref interestEvent,ref interestPrepayment, ref principalEvent, ref feesEvent, ref commissionsEvent);
            PaidInstallments = _repayNextInstallments.PaidInstallments;
        }
    }
}
