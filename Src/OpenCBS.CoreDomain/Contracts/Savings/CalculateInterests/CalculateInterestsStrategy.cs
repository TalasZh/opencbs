// LICENSE PLACEHOLDER

using OpenCBS.CoreDomain.Contracts.Savings.CalculateInterests.Interfaces;
using OpenCBS.Enums;
using OpenCBS.CoreDomain.Events.Saving;
using System.Collections.Generic;

namespace OpenCBS.CoreDomain.Contracts.Savings.CalculateInterests
{
    public class CalculateInterestsStrategy : ICalculateInterests
    {
        private readonly ICalculateInterests _ici;

        public CalculateInterestsStrategy(ISavingsContract pSaving, User pUser, int pWeekEndDay2)
        {
            if (((SavingBookContract)pSaving).Product.InterestBase == OSavingInterestBase.Daily)
                _ici = new Accrual.Daily(pSaving, pUser);
            else if (((SavingBookContract)pSaving).Product.InterestBase == OSavingInterestBase.Monthly)
            {
                if (((SavingBookContract)pSaving).Product.CalculAmountBase == OSavingCalculAmountBase.MinimalAmount)
                    _ici = new Accrual.MinimalAmount.Monthly((SavingBookContract)pSaving, pUser);
            }
            else if (((SavingBookContract)pSaving).Product.InterestBase == OSavingInterestBase.Weekly)
            {
                if (((SavingBookContract)pSaving).Product.CalculAmountBase == OSavingCalculAmountBase.MinimalAmount)
                    _ici = new Accrual.MinimalAmount.Weekly((SavingBookContract)pSaving, pUser, pWeekEndDay2);
            }
        }

        public List<SavingInterestsAccrualEvent> CalculateInterest(System.DateTime closureDate)
        {
            return _ici.CalculateInterest(closureDate);
        }
    }
}
