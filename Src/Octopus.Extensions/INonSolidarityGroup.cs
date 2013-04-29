using System.Windows.Forms;
using System.Data.SqlClient;
using Octopus.CoreDomain.Clients;

namespace Octopus.Extensions
{
    public interface INonSolidarityGroup
    {
        TabPage[] GetTabPages(Village village);
        void Save(Village village, SqlTransaction tx);
    }
}