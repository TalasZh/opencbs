// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using OpenCBS.CoreDomain;

namespace OpenCBS.Manager
{
    public class BranchManager : Manager
    {
        public BranchManager(User user) : base(user)
        {
        }

        public BranchManager(string pTestDb) : base(pTestDb)
        {

        }

        public List<Branch> SelectAll()
        {
            List<Branch> branches = new List<Branch>();
            const string q =
                @"SELECT 
                    id, 
                    name, 
                    deleted
                    , code, address, description
            FROM dbo.Branches";
            using (SqlConnection conn = GetConnection())
            using (OctopusCommand c = new OctopusCommand(q, conn))
            using (OctopusReader r = c.ExecuteReader())
            {
                if (r.Empty) return branches;

                while (r.Read())
                {
                    Branch b = new Branch
                                   {
                                       Id = r.GetInt("id")
                                       ,
                                       Name = r.GetString("name")
                                       ,
                                       Deleted = r.GetBool("deleted")
                                       ,
                                       Code = r.GetString("code")
                                       ,
                                       Address = r.GetString("address")
                                       ,
                                       Description = r.GetString("description")
                                   };
                    branches.Add(b);
                }
            }
            return branches;
        }

        public List<Branch> SelectAllWithVault()
        {
            List<Branch> branches = new List<Branch>();
            const string q =
                @"SELECT 
                    id, 
                    name, 
                    deleted, 
                    code,
                    address,
                    description
                    FROM dbo.Branches
                    WHERE id IN (SELECT branch_id FROM Tellers WHERE user_id = 0 AND deleted = 0)";
            using (SqlConnection conn = GetConnection())
            using (OctopusCommand c = new OctopusCommand(q, conn))
            using (OctopusReader r = c.ExecuteReader())
            {
                if (r.Empty) return branches;

                while (r.Read())
                {
                    Branch b = new Branch
                    {
                        Id = r.GetInt("id"),
                        Name = r.GetString("name"),
                        Deleted = r.GetBool("deleted"),
                        Code = r.GetString("code"),
                        Address = r.GetString("address"),
                        Description = r.GetString("description")
                    };
                    branches.Add(b);
                }
            }
            return branches;
        }

        public Branch Add(Branch branch, SqlTransaction t)
        {
            const string q = @"INSERT INTO dbo.Branches
                              (name, code, address, description)
                              VALUES (@name, @code, @address, @description)
                              SELECT SCOPE_IDENTITY()";
            using (OctopusCommand c = new OctopusCommand(q, t.Connection, t))
            {
                c.AddParam("@name", branch.Name);
                c.AddParam("@code", branch.Code);
                c.AddParam("@address", branch.Address);
                c.AddParam("@description", branch.Description);
                branch.Id = Convert.ToInt32(c.ExecuteScalar());
                return branch;
            }
        }

        public void Update(Branch branch, SqlTransaction t)
        {
            const string q = @"UPDATE dbo.Branches
                                SET name = @name
                                , code = @code
                                , description = @description
                                , address = @address
                                WHERE id = @id";
            using (OctopusCommand c = new OctopusCommand(q, t.Connection, t))
            {   
                c.AddParam("@id", branch.Id);
                c.AddParam("@name", branch.Name);
                c.AddParam("@code", branch.Code);
                c.AddParam("@description", branch.Description);
                c.AddParam("@address", branch.Address);
                c.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            const string q = @"UPDATE dbo.Branches
            SET deleted = 1 WHERE id = @id";
            using (SqlConnection conn = GetConnection())
            using (OctopusCommand c = new OctopusCommand(q, conn))
            {
                c.AddParam("@id", id);
                c.ExecuteNonQuery();
            }
        }

        public bool NameExists(int id, string name)
        {
            const string q =
                @"select count(*)
            from dbo.Branches
            where name = @name and id <> @id";
            using (SqlConnection conn = GetConnection())
            using (OctopusCommand c = new OctopusCommand(q, conn))
            {
                c.AddParam("@id", id);
                c.AddParam("@name", name);
                return Convert.ToBoolean(c.ExecuteScalar());
            }
        }

        public bool CodeExists(int id, string code)
        {
            const string q =
                @"select count(*)
            from dbo.Branches
            where code = @code and id <> @id";
            using (SqlConnection conn = GetConnection())
            using (OctopusCommand c = new OctopusCommand(q, conn))
            {
                c.AddParam("@id", id);
                c.AddParam("@code", code);
                return Convert.ToBoolean(c.ExecuteScalar());
            }
        }

        public string GetBranchCodeByClientId(int clientId)
        {
            const string q = @"SELECT Branches.code FROM Tiers
                         INNER JOIN Branches ON Branches.id = Tiers.branch_id
                         WHERE Tiers.id = @id";
            using (SqlConnection conn = GetConnection())
            using (OctopusCommand c = new OctopusCommand(q, conn))
            {
                c.AddParam("@id", clientId);
                string code = string.Empty;
                using (OctopusReader r = c.ExecuteReader())
                {
                    if (r.Empty) return null;
                    if (r.Read())
                        code = r.GetString("code");
                }
                return code;
            }            
        }

        public string GetBranchCodeByClientId(int clientId, SqlTransaction sqlTransaction)
        {
            const string sqlText = @"SELECT Branches.code   
                                    FROM Tiers
                                    INNER JOIN Branches ON Branches.id = Tiers.branch_id
                                    WHERE Tiers.id = @id";
            OctopusCommand c = new OctopusCommand(sqlText, sqlTransaction.Connection, sqlTransaction);
            {
                c.AddParam("@id", clientId);
                string code = string.Empty;
                using (OctopusReader r = c.ExecuteReader())
                {
                    if (r.Empty) return null;
                    if (r.Read())
                        code = r.GetString("code");
                }
                return code;
            }
        }

        public Branch Select(int branchId)
        {
            using (SqlConnection conn = GetConnection())
            using (SqlTransaction t = conn.BeginTransaction())
            {
                try
                {
                    Branch b = Select(branchId, t);
                    t.Commit();
                    return b;
                }
                catch (Exception)
                {
                    t.Rollback();
                    throw;
                }
            }
        }

        private Branch Select(int branchId, SqlTransaction pSqlTransac)
        {
            const string sqlText = @"SELECT 
                                       id,
                                       name,
                                       deleted,
                                       code, 
                                       address,
                                       description
                                     FROM [Branches] 
                                     WHERE id = @id";

            using (OctopusCommand c = new OctopusCommand(sqlText, pSqlTransac.Connection, pSqlTransac))
            {
                c.AddParam("@id", branchId);
                using (OctopusReader r = c.ExecuteReader())
                {
                    if (r.Empty) return null;

                    r.Read();
                    Branch branch = new Branch
                                        {
                                            Id = r.GetInt("id"),
                                            Name = r.GetString("name"),
                                            Deleted = r.GetBool("deleted"),
                                            Code = r.GetString("code"),
                                            Address = r.GetString("address"),
                                            Description = r.GetString("description")
                                        };
                    return branch;
                }
            }
        }

        public Branch SelectBranchByName(string name)
        {
            string query = @"SELECT id
                , name
                , deleted
                , code
                , address
                , description
            FROM dbo.Branches
            WHERE name LIKE '%{0}%'";
            query = string.Format(query, name);
            using (SqlConnection conn = GetConnection())
            using (OctopusCommand cmd = new OctopusCommand(query, conn))
            using (OctopusReader r = cmd.ExecuteReader())
            {
                if (r.Empty) return null;
                if (!r.Read()) return null;
                return new Branch
                {
                    Id = r.GetInt("id"),
                    Code = r.GetString("code"),
                    Name = r.GetString("name"),
                    Deleted = r.GetBool("deleted"),
                    Description = r.GetString("description")
                };
            }
        }
    }
}
 
