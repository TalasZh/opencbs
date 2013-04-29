using System;
using System.Collections.Generic;
using System.Text;
using Octopus.CoreDomain.Accounting;

namespace Octopus.CoreDomain.Online
{
    public interface  ICoreDomainRemoting
    {
        ChartOfAccounts GetChartOfAccounts(User pUser);
        ProvisionTable GetProvisioningTable(User pUser);
        LoanScaleTable GetLoanScaleTable(User pUser);
    }
}
