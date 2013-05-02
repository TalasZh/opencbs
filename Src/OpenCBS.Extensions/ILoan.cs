// LICENSE PLACEHOLDER

using System.Data.SqlClient;
using System.Windows.Forms;
using OpenCBS.CoreDomain.Contracts.Loans;

namespace OpenCBS.Extensions
{
    public interface ILoan
    {
        TabPage[] GetTabPages(Loan loan);
        TabPage[] GetRepaymentTabPages(Loan loan);
        void Save(Loan loan, SqlTransaction tx);
    }
}
