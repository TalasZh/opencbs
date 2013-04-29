using System;
using System.Collections.Generic;
using System.Text;
using Octopus.CoreDomain.Accounting;

namespace Octopus.CoreDomain.Online
{
    public interface ICoreDomain
    {
        ChartOfAccounts GetChartOfAccounts();
        ProvisionTable GetProvisioningTable();
        LoanScaleTable GetLoanScaleTable();
    }
}
