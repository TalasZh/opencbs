using System;
using System.Collections.Generic;
using System.Text;
using OpenCBS.CoreDomain.Accounting;

namespace OpenCBS.CoreDomain.Online
{
    public interface  ICoreDomainRemoting
    {
        ChartOfAccounts GetChartOfAccounts(User pUser);
        ProvisionTable GetProvisioningTable(User pUser);
        LoanScaleTable GetLoanScaleTable(User pUser);
    }
}
