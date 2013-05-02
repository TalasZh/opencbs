// LICENSE PLACEHOLDER

using System.Windows.Forms;
using System.Data.SqlClient;
using OpenCBS.CoreDomain.Clients;

namespace OpenCBS.Extensions
{
    public interface INonSolidarityGroup
    {
        TabPage[] GetTabPages(Village village);
        void Save(Village village, SqlTransaction tx);
    }
}
