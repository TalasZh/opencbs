using System.Windows.Forms;
using System.Data.SqlClient;
using OpenCBS.CoreDomain.Clients;

namespace OpenCBS.Extensions
{
    public interface ISolidarityGroup
    {
        TabPage[] GetTabPages(Group group);
        void Save(Group group, SqlTransaction tx);
    }
}