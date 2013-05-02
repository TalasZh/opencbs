using System;
using System.Collections.Generic;
using System.Text;
using OpenCBS.CoreDomain.Accounting;

namespace OpenCBS.CoreDomain.Online
{
    public interface ICoreDomain
    {
        ChartOfAccounts GetChartOfAccounts();
        ProvisionTable GetProvisioningTable();
        LoanScaleTable GetLoanScaleTable();
    }
}
