using System.Data.SqlClient;
using System.Windows.Forms;
using Octopus.CoreDomain.Clients;

namespace Octopus.Extensions
{
    public interface IPerson
    {
        TabPage[] GetTabPages(Person person);
        void Save(Person person, SqlTransaction tx);
    }
}