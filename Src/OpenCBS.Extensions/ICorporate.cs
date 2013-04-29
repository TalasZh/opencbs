using System.Data.SqlClient;
using System.Windows.Forms;
using Octopus.CoreDomain.Clients;

namespace Octopus.Extensions
{
    public interface ICorporate
    {
        TabPage[] GetTabPages(Corporate corporate);
        void Save(Corporate corporate, SqlTransaction tx);
    }
}