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

using System.Collections.Generic;
using OpenCBS.CoreDomain;
using System.Data.SqlClient;

namespace OpenCBS.Manager
{
    /// <summary>
    /// Description rйsumйe de RoleManager.
    /// </summary>
    public class RoleManager : Manager
    {
        private MenuItemManager menuItemManager;
        public RoleManager(string testDb) : base(testDb)
        {
           
        }

        public RoleManager(User user)
            : base(user)
        {
            menuItemManager = new MenuItemManager(user);
        }

        /// <summary>
        /// Add Role in database
        /// </summary>
        /// <param name="pRole"></param>
        /// <returns>Role id</returns>
        public int AddRole(Role pRole)
        {
            const string q = @"INSERT INTO [Roles]
                                     ([deleted], [code], [description], [role_of_loan], [role_of_saving], [role_of_teller]) 
                                     VALUES(@deleted, @code, @description, @role_of_loan, @role_of_saving, @role_of_teller) 
                                     SELECT SCOPE_IDENTITY()";

            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
                {
                    c.AddParam("@deleted", false);
                    c.AddParam("@code", pRole.RoleName);
                    c.AddParam("@description", pRole.Description);
                    c.AddParam("@role_of_loan", pRole.IsRoleForLoan);
                    c.AddParam("@role_of_saving", pRole.IsRoleForSaving);
                    c.AddParam("@role_of_teller", pRole.IsRoleForTeller);

                    pRole.Id = int.Parse(c.ExecuteScalar().ToString());
                    SaveRoleMenu(pRole);
                    SaveAllowedActionsForRole(pRole);
                }
            }
            return pRole.Id;
        }

        /// <summary>
        /// Update selected Role in database
        /// </summary>
        /// <param name="pRole"></param>
        public void UpdateRole(Role pRole)
        {
            const string q = @"UPDATE [Roles] 
                                     SET [code]=@code, 
                                         [deleted]=@deleted, 
                                         [description]=@description,
                                         [role_of_loan] = @role_of_loan, 
                                         [role_of_saving] = @role_of_saving, 
                                         [role_of_teller] = @role_of_teller 
                                     WHERE [id] = @RoleId";

            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
                {
                    c.AddParam("@RoleId", pRole.Id);
                    c.AddParam("@deleted", pRole.IsDeleted);
                    c.AddParam("@code", pRole.RoleName);
                    c.AddParam("@description", pRole.Description);
                    c.AddParam("@role_of_loan", pRole.IsRoleForLoan);
                    c.AddParam("@role_of_saving", pRole.IsRoleForSaving);
                    c.AddParam("@role_of_teller", pRole.IsRoleForTeller);

                    c.ExecuteNonQuery();
                    SaveRoleMenu(pRole);
                    SaveAllowedActionsForRole(pRole);
                }
            }
        }

        /// <summary>
        /// DeleteAccount selected Role.
        /// A Role was never erased, just update a boolean
        /// </summary>
        /// <param name="pRoleName"></param>
        public void DeleteRole(string pRoleName)
        {
            const string q = "UPDATE [Roles] SET deleted = 1 WHERE [code] = @code";

            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
                {
                    c.AddParam("@code", pRoleName);
                    c.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Select a Role by its database id with an Sqltransaction contexte
        /// </summary>
        /// <param name="pRoleId"></param>
        /// <param name="pIncludeDeletedRole"></param>
        /// <param name="pSqlTransac"></param>
        /// <returns>selected Role or null otherwise</returns>
        public Role SelectRole(int pRoleId, bool pIncludeDeletedRole, SqlTransaction pSqlTransac)
        {
            string q = @"SELECT [Roles].[id], [code], [deleted], [description], [role_of_loan], [role_of_saving], [role_of_teller]  
                               FROM [Roles] WHERE [id] = @id ";

            if (!pIncludeDeletedRole)
                q += " AND [deleted] = 0";

            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                c.AddParam("@id", pRoleId);
                using (OpenCbsReader r = c.ExecuteReader())
                {
                    if (r != null)
                    {
                        if (!r.Empty)
                        {
                            r.Read();
                            Role role = GetRole(r) ;
                            return role;

                        }
                    }
                }
                return null;
            }
        }
        public int SelectUserForThisRole(string pRoleName)
        {
            string q = @"SELECT TOP 1 [user_id] 
                               FROM UserRole 
                               INNER JOIN Roles ON UserRole.role_id = Roles.id 
                               INNER JOIN Users ON Users.id = UserRole.[user_id]
                               WHERE Roles.code = @roleCode AND Users.deleted = 0";
            int foundId = 0;
            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
                {
                    c.AddParam("@roleCode", pRoleName);
                    using (OpenCbsReader r = c.ExecuteReader())
                    {
                        if (r != null)
                        {
                            if (!r.Empty)
                            {
                                r.Read();
                                foundId = r.GetInt("user_id");
                            }
                        }
                    }
                    return foundId;
                }
            }
        }
        /// <summary>
        /// Select a Role by its database id
        /// </summary>
        /// <param name="pRoleId"></param>
        /// <param name="pIncludeDeletedRole"></param>
        /// <returns>selected Role or null otherwise</returns>
        public Role SelectRole(int pRoleId, bool pIncludeDeletedRole)
        {
            return SelectRole(pRoleId, pIncludeDeletedRole, null);
        }

        public List<Role> SelectAllRoles(bool pSelectDeletedRoles)
        {
            string q = @"SELECT [Roles].[id], [code], [deleted], [description], [role_of_loan], [role_of_saving], [role_of_teller]
                               FROM [Roles] WHERE 1 = 1 ";

            if (!pSelectDeletedRoles)
                q += " AND [deleted] = 0";

            List<Role> Roles = new List<Role>();

            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
                {
                    using (OpenCbsReader r = c.ExecuteReader())
                    {
                        if (r != null)
                        {
                            if (!r.Empty)
                                while (r.Read())
                                    Roles.Add(GetRole(r));
                        }
                    }
                }
            }
            foreach (Role r in Roles)
            {                              
                r.SetMenuItems(GetAllowedMenuList(r.Id));
                r.SetActionItems(GetAllowedActionList(r.Id));
            }
            return Roles;
        }
        /// <summary>
        /// Select a role by its name
        /// </summary>
        /// <param name="pRoleName"></param>
        /// <param name="pIncludeDeleted"></param>
        /// <returns>selected role or null otherwise</returns>
        public Role SelectRole(string pRoleName,bool pIncludeDeleted)
        {
            string q = @"SELECT [id], [code], [deleted], [description], [role_of_loan], [role_of_saving], [role_of_teller]
                                    FROM [Roles] WHERE [code] = @name ";

            q += pIncludeDeleted ? "" : "AND [deleted] = 0";

            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
                {
                    c.AddParam("@name", pRoleName);
                    Role role;
                    using (OpenCbsReader r = c.ExecuteReader())
                    {
                        if (r == null || r.Empty) return null;

                        r.Read();
                        role = GetRole(r);
                    }

                    role.SetMenuItems(GetAllowedMenuList(role.Id));
                    role.SetActionItems(GetAllowedActionList(role.Id));
                    return role;
                }
            }
        }

        public void UpdateMenuList(List<MenuObject> pMenuTitles)
        {
            foreach (MenuObject mi in pMenuTitles)
            {
                try
                {
                    const string q = @"INSERT INTO [MenuItems]([menu_name]) VALUES (@menu)";

                    using (SqlConnection conn = GetConnection())
                    {
                        using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
                        {
                            c.AddParam("@menu", mi.Name);
                            c.ExecuteScalar();
                        }
                    }
                }
                catch (SqlException e)
                {
                    if (e.Message.IndexOf("UNIQUE") != -1)
                    {
                        // ignore
                        //reader.Close();
                    }
                    else
                        throw;
                }
            }
        }

        public Dictionary<MenuObject, bool> GetAllowedMenuList(int pRoleId)
        {
            string q = @"SELECT MenuItems.[id], MenuItems.[component_name], AllowedRoleMenus.allowed 
                              FROM [MenuItems] 
                              INNER JOIN AllowedRoleMenus ON MenuItems.id = AllowedRoleMenus.menu_item_id
                              WHERE AllowedRoleMenus.role_id = @roleId";

            Dictionary<MenuObject, bool> Menus = new Dictionary<MenuObject, bool>();

            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
                {
                    c.AddParam("@roleId", pRoleId);

                    using (OpenCbsReader r = c.ExecuteReader())
                    {
                        if (r != null)
                        {
                            if (!r.Empty)
                                while (r.Read())
                                {
                                    Menus.Add(new MenuObject
                                                  {
                                                      Id = r.GetInt("id"),
                                                      Name = r.GetString("component_name").Trim(),
                                                      NotSavedInDBYet = false
                                                  }, r.GetBool("allowed"));
                                }
                        }
                    }
                }
            }
            return Menus;
        }
        public Dictionary<ActionItemObject, bool> GetAllowedActionList(int pRoleId)
        {
            //select maintained allowed flags for actions or true if nothing was specified
            string q = @"SELECT 
                                 ActionItems.[id], 
                                 ActionItems.[class_name], 
                                 ActionItems.[method_name], 
                                 ISNULL((SELECT allowed 
                                         FROM AllowedRoleActions 
                                         WHERE AllowedRoleActions.action_item_id = ActionItems.id 
                                           AND AllowedRoleActions.role_id = @roleId), 1) as allowed
                                FROM ActionItems";

            Dictionary<ActionItemObject, bool> Actions = new Dictionary<ActionItemObject, bool>();

            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
                {
                    c.AddParam("@roleId", pRoleId);

                    using (OpenCbsReader r = c.ExecuteReader())
                    {
                        if (r != null)
                        {
                            if (!r.Empty)
                                while (r.Read())
                                {
                                    Actions.Add(new ActionItemObject
                                                    {
                                                        Id = r.GetInt("id"),
                                                        ClassName =
                                                            r.GetString("class_name").Trim(),
                                                        MethodName =
                                                            r.GetString("method_name").Trim()
                                                    }, r.GetBool("allowed"));
                                }
                        }
                    }
                }
            }
            return Actions;
        }
        public bool IsThisActionAllowedForThisRole(int pRoleId, ActionItemObject pAction)
        {
            string q = @"SELECT allowed FROM AllowedRoleActions
                            WHERE action_id = @actionId AND role_id = @roleId";

            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
                {
                    c.AddParam("@roleId", pRoleId);
                    c.AddParam("@actionId", pAction.Id);
                    using (OpenCbsReader r = c.ExecuteReader())
                    {
                        if (r != null)
                        {
                            if (!r.Empty)
                            {
                                return r.GetBool("allowed");
                            }
                        }
                    }
                }
            }
            return true;
        }
        private void  SaveRoleMenu(Role pRole)
        {
            foreach (KeyValuePair<MenuObject, bool> kvp in pRole.GetMenuItems())
            {
                string q = @"SELECT allowed FROM AllowedRoleMenus 
                             WHERE menu_item_id = @menuId 
                               AND role_id = @roleId";

                using (SqlConnection conn = GetConnection())
                {
                    using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
                    {
                        c.AddParam("@roleId", pRole.Id);
                        c.AddParam("@menuId", kvp.Key.Id);

                        using (OpenCbsReader r = c.ExecuteReader())
                        {
                            if (r != null)
                            {
                                if (!r.Empty)
                                {
                                    r.Read();
                                    bool oldValue = r.GetBool("allowed");
                                    
                                    if (oldValue != kvp.Value)
                                    {
                                        UpdateAllowedMenuItem(pRole.Id, kvp.Key.Id, kvp.Value);
                                    }
                                }
                                else
                                {
                                    AddAllowedMenuItem(pRole.Id, kvp.Key.Id, kvp.Value);
                                }
                            }
                        }
                    }
                }

            }
        }

        private void SaveAllowedActionsForRole(Role pRole)
        {
            foreach (KeyValuePair<ActionItemObject, bool> kvp in pRole.GetActionItems())
            {
                string q = @"SELECT allowed 
                            FROM AllowedRoleActions 
                             WHERE action_item_id = @actionId 
                               AND role_id = @roleId";


                using (SqlConnection conn = GetConnection())
                {
                    using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
                    {
                        c.AddParam("@roleId", pRole.Id);
                        c.AddParam("@actionId", kvp.Key.Id);
                        using (OpenCbsReader r = c.ExecuteReader())
                        {
                            if (r != null)
                            {
                                if (!r.Empty)
                                {
                                    r.Read();
                                    bool oldValue = r.GetBool("allowed");
                                    if (oldValue != kvp.Value)
                                    {
                                        menuItemManager.UpdateAllowedItem(pRole.Id, kvp.Key.Id, kvp.Value);
                                    }
                                }
                                else
                                {
                                    menuItemManager.AddAllowedItem(pRole.Id, kvp.Key.Id, kvp.Value);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void UpdateAllowedMenuItem(int pRoleId, int pMenuId, bool pAllowed)
        {
            const string q = @"UPDATE [AllowedRoleMenus]
                                            SET allowed = @allowed
                                            WHERE [menu_item_id] = @menuId AND
                                            role_id = @roleId";

            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
                {
                    c.AddParam("@allowed", pAllowed);
                    c.AddParam("@menuId", pMenuId);
                    c.AddParam("@roleId", pRoleId);
                    c.ExecuteNonQuery();
                }
            }
        }

        private void AddAllowedMenuItem(int pRoleId, int pMenuId, bool pAllowed)
        {
            const string q = @"INSERT INTO AllowedRoleMenus (menu_item_id, role_id, allowed)
                                        VALUES (@menuId, @roleId, @allowed)";

            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
                {
                    c.AddParam("@allowed", pAllowed);
                    c.AddParam("@menuId", pMenuId);
                    c.AddParam("@roleId", pRoleId);
                    c.ExecuteNonQuery();
                }
            }
        }

        private static Role GetRole(OpenCbsReader r)
        {
            return new Role
                {           
                    Id = r.GetInt("id"),
                    RoleName = r.GetString("code"),
                    IsDeleted = r.GetBool("deleted"),
                    Description = r.GetString("description"),
                    IsRoleForLoan = r.GetBool("role_of_loan"),
                    IsRoleForSaving = r.GetBool("role_of_saving"),
                    IsRoleForTeller = r.GetBool("role_of_teller")
                };;
        }

        public Dictionary<int, int> SelectUserToRole()
        {
            const string q = @"SELECT u.id AS user_id, r.id AS role_id
                               FROM dbo.Users AS u
                               LEFT JOIN dbo.Roles AS r ON r.code = u.role_code";

            Dictionary<int, int> userToRole = new Dictionary<int, int>();
            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
                {
                    using (OpenCbsReader r = c.ExecuteReader())
                    {
                        if (r.Empty) return userToRole;

                        while (r.Read())
                        {
                            int userId = r.GetInt("user_id");
                            int roleId = r.GetInt("role_id");
                            userToRole.Add(userId, roleId);
                        }
                    }
                }
            }
            return userToRole;
        }
    }
}
