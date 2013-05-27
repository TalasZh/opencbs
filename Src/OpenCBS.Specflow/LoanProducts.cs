using System.Collections.Generic;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment;
using OpenCBS.CoreDomain.Products;
using OpenCBS.Enums;

namespace OpenCBS.Specflow
{
    class LoanProducts
    {
        private static LoanProducts _instance;
        private Dictionary<string, LoanProduct> _loanProducts = new Dictionary<string, LoanProduct>();

        public LoanProducts()
        {
            _loanProducts.Add("IL Monthly Repayment - Declining rate", Get_IL_MonthlyRepayment_DecliningRate());
        }

        public LoanProduct this[string key]
        {
            get { return _loanProducts[key]; }
        }

        public static LoanProducts Instance
        {
            get { return _instance ?? (_instance = new LoanProducts()); }
        }

        private LoanProduct Get_IL_MonthlyRepayment_DecliningRate()
        {
            return new LoanProduct
            {
                Name = "IL Monthly Repayment - Declining rate",
                InstallmentType = new InstallmentType { Name = "Monthly", NbOfDays = 0, NbOfMonths = 1 },
                InterestRate = 3,
                GracePeriodMin = 0,
                GracePeriodMax = 0,
                LoanType = OLoanTypes.DecliningFixedInstallments,
                ChargeInterestWithinGracePeriod = true,
                KeepExpectedInstallment = false,
                AnticipatedTotalRepaymentPenaltiesBase = OAnticipatedRepaymentPenaltiesBases.RemainingOLB,
                AnticipatedPartialRepaymentPenaltiesBase = OAnticipatedRepaymentPenaltiesBases.RemainingOLB,
                NonRepaymentPenalties = new NonRepaymentPenaltiesNullableValues(0, 0, 0, 0.01),
                Currency = new Currency { UseCents = false }
            };
        }
    }
}
