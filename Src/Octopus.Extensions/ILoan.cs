using System.Data.SqlClient;
using System.Windows.Forms;
using Octopus.CoreDomain.Contracts.Loans;

namespace Octopus.Extensions
{
    public interface ILoan
    {
        TabPage[] GetTabPages(Loan loan);
        TabPage[] GetRepaymentTabPages(Loan loan);
        void Save(Loan loan, SqlTransaction tx);
    }
}