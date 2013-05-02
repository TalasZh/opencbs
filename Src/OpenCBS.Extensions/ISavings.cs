// LICENSE PLACEHOLDER

using System.Data.SqlClient;
using System.Windows.Forms;
using OpenCBS.CoreDomain.Contracts.Savings;

namespace OpenCBS.Extensions
{
    public interface ISavings
    {
        TabPage[] GetTabPages(ISavingsContract savings);
        void Save(ISavingsContract savings, SqlTransaction tx);
    }
}
