using System.Data.SqlClient;
using System.Windows.Forms;
using OpenCBS.CoreDomain.Clients;

namespace OpenCBS.Extensions
{
    public interface IPerson
    {
        TabPage[] GetTabPages(Person person);
        void Save(Person person, SqlTransaction tx);
    }
}