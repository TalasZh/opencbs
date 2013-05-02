using System.Data.SqlClient;
using System.Windows.Forms;
using OpenCBS.CoreDomain.Clients;

namespace OpenCBS.Extensions
{
    public interface ICorporate
    {
        TabPage[] GetTabPages(Corporate corporate);
        void Save(Corporate corporate, SqlTransaction tx);
    }
}