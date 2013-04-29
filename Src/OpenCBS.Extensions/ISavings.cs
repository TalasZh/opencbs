using System.Data.SqlClient;
using System.Windows.Forms;
using Octopus.CoreDomain.Contracts.Savings;

namespace Octopus.Extensions
{
    public interface ISavings
    {
        TabPage[] GetTabPages(ISavingsContract savings);
        void Save(ISavingsContract savings, SqlTransaction tx);
    }
}
