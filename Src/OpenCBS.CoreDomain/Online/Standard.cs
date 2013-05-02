using OpenCBS.CoreDomain.Accounting;

namespace OpenCBS.CoreDomain.Online
{
    public class Standard : ICoreDomain
    {
        public ChartOfAccounts GetChartOfAccounts()
        {
            return ChartOfAccounts.GetInstance(User.CurrentUser);
        }

        public ProvisionTable GetProvisioningTable()
        {
            return ProvisionTable.GetInstance(User.CurrentUser);
        }

        public LoanScaleTable GetLoanScaleTable()
        {
            return LoanScaleTable.GetInstance(User.CurrentUser);
        }
    }
}
