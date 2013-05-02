using System;
using System.Collections;
using OpenCBS.CoreDomain.Contracts.Loans;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;

namespace OpenCBS.CoreDomain.Contracts
{
    [Serializable]
    public class FlatRoundingSchedule
    {
        public Loan EditSchedule(Loan pLoan, int roundTo)
        {
            Loan temp = pLoan.Copy();
            int sum = 0;
            if (roundTo == 0)
                return pLoan;

            foreach (Installment installment in temp.InstallmentList)
            {
                if (installment.Number != temp.InstallmentList.Count)
                {
                    int dif = Convert.ToInt32(installment.CapitalRepayment.Value + installment.InterestsRepayment.Value) % roundTo;
                    dif = dif > roundTo - dif ? roundTo - dif : -dif;
                    installment.CapitalRepayment += dif;
                    sum += dif;
                }
                else
                {
                    installment.CapitalRepayment -= sum;
                }
            }
            return temp;
        }
    }
}
