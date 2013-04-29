using System.Windows.Forms;
using System.Data.SqlClient;
using Octopus.CoreDomain.Clients;

namespace Octopus.Extensions
{
    public interface ISolidarityGroup
    {
        TabPage[] GetTabPages(Group group);
        void Save(Group group, SqlTransaction tx);
    }
}