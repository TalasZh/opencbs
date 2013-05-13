// Octopus MFS is an integrated suite for managing a Micro Finance Institution: 
// clients, contracts, accounting, reporting and risk
// Copyright © 2006,2007 OCTO Technology & OXUS Development Network
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License along
// with this program; if not, write to the Free Software Foundation, Inc.,
// 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
// Website: http://www.opencbs.com
// Contact: contact@opencbs.com

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
            using (OpenCbsCommand select = new OpenCbsCommand(sqlText, connection))
            using (OpenCbsReader reader = select.ExecuteReader())
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
                using (OpenCbsCommand cmd = new OpenCbsCommand(sqlText, connection))
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
                using (OpenCbsCommand cmd = new OpenCbsCommand(sqlText, connection))
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
                using (OpenCbsCommand cmd = new OpenCbsCommand(sqlText, connection))
                    cmd.ExecuteNonQuery();
            }
        }
    }
}
