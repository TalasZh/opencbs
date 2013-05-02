
using OpenCBS.CoreDomain;
using System.Data.SqlClient;
using OpenCBS.Shared;

namespace OpenCBS.Manager
{
    public class MFIManager : Manager
    {
        private User _user;
        public MFIManager(User pUser)
            : base(pUser)
        {
            _user = pUser;
        }

        public MFIManager(string testDB) : base(testDB) { }

        public MFIManager(string testDB, User pUser)
            : base(testDB)
        {
            _user = pUser;
        }

        public MFI SelectMFI()
        {
            MFI mfi = new MFI();

            string sqlText = "SELECT * FROM [MFI]";
            using (SqlConnection connection = GetConnection())
            using (OctopusCommand select = new OctopusCommand(sqlText, connection))
            using (OctopusReader reader = select.ExecuteReader())
            {
                if (!reader.Empty)
                {
                    reader.Read();
                    mfi = new MFI();
                    mfi.Name = reader.GetString("name");
                    mfi.Login = reader.GetString("login");
                    mfi.Password = reader.GetString("password");
                }
            }
            return mfi;
        }

        public bool UpdateMFI(MFI pMFI)
        {
            if (SelectMFI().Login != null)
            {
                string sqlText = @"UPDATE [MFI] SET [name]=@name, [login]=@login, [password]=@password";

                using (SqlConnection connection = GetConnection())
                using (OctopusCommand cmd = new OctopusCommand(sqlText, connection))
                {
                    cmd.AddParam("@name", pMFI.Name);
                    cmd.AddParam("@login", pMFI.Login);
                    cmd.AddParam("@password", pMFI.Password);
                    cmd.ExecuteNonQuery();
                }
                return true;
            }

            return false;
        }

        public bool CreateMFI(MFI pMFI)
        {
            if (SelectMFI().Login == null)
            {
                string sqlText = "INSERT INTO [MFI] ([name], [login], [password]) VALUES(@name,@login,@password)";

                using (SqlConnection connection = GetConnection())
                using (OctopusCommand cmd = new OctopusCommand(sqlText, connection))
                {
                    cmd.AddParam("@name", pMFI.Name);
                    cmd.AddParam("@login", pMFI.Login);
                    cmd.AddParam("@password",  pMFI.Password);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            return false;
        }

        public void DeleteMFI()
        {
            if (SelectMFI()!=null)
            {
                string sqlText = "DELETE FROM [MFI]";
                using (SqlConnection connection = GetConnection())
                using (OctopusCommand cmd = new OctopusCommand(sqlText, connection))
                    cmd.ExecuteNonQuery();
            }
        }
    }
}
