// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using OpenCBS.CoreDomain;
using OpenCBS.Enums;

namespace OpenCBS.Manager
{
    public class MenuItemManager : Manager
    {
        public MenuItemManager(string pDatabaseConnectionString) : base(pDatabaseConnectionString)
        {
        }

        public MenuItemManager(User pUser) : base(pUser)
        {
        }

        public List<MenuObject> GetMenuList(OSecurityObjectTypes[] securityObjectTypes)
        {
            string q = @"SELECT [id], [component_name] FROM [MenuItems]";
            if(securityObjectTypes.Any())
            {
                string[] types = securityObjectTypes.Select(t => Convert.ToString((int) t)).ToArray();
                string condition = string.Format(" WHERE [type] in ({0})", string.Join(",", types));
                q += condition;
            }

            List<MenuObject> menus = new List<MenuObject>();

            using (SqlConnection conn = GetConnection())
            using (OctopusCommand c = new OctopusCommand(q, conn))
            {
                using (OctopusReader r = c.ExecuteReader())
                {
                    if (r != null && !r.Empty)
                    {
                        while (r.Read())
                        {
                            menus.Add(new MenuObject
                            {
                                Id = r.GetInt("id"),
                                Name = r.GetString("component_name").Trim(),
                                NotSavedInDBYet = false
                            });
                        }
                    }
                }
            }
            return menus;
        }

        public MenuObject AddNewMenu(string name)
        {
            MenuObject newMenu = new MenuObject();

            const string q = @"INSERT INTO [MenuItems]([component_name]) 
                               VALUES (@menu) 
                               SELECT SCOPE_IDENTITY()";

            using (SqlConnection conn = GetConnection())
            {
                using (OctopusCommand c = new OctopusCommand(q, conn))
                {
                    c.AddParam("@menu", name.Trim());
                    newMenu.Id = int.Parse(c.ExecuteScalar().ToString());
                    newMenu.NotSavedInDBYet = false;
                    newMenu.Name = name;
                }
            }
            return newMenu;
        }

        public MenuObject FindMenuItem(string menuItem)
        {
            const string query = @"SELECT id, component_name 
                                   FROM MenuItems 
                                   WHERE component_name = @menuItem";
            using (SqlConnection conn = GetConnection())
            using (OctopusCommand cmd = new OctopusCommand(query, conn))
            {
                cmd.AddParam("menuItem", menuItem);
                OctopusReader reader = cmd.ExecuteReader();
                if (reader == null || !reader.Read()) return null;
                return new MenuObject
                           {
                               Id = reader.GetInt("id"),
                               Name = reader.GetString("component_name"),
                               NotSavedInDBYet = false
                           };
            }
        }

        public void AddAllowedItem(int pRoleId, int pActionId, bool pAllowed)
        {
            const string q = @"INSERT INTO AllowedRoleActions (action_item_id, role_id, allowed)
                                        VALUES (@actionId, @roleId, @allowed)";

            using (SqlConnection conn = GetConnection())
            {
                using (OctopusCommand c = new OctopusCommand(q, conn))
                {
                    c.AddParam("@allowed", pAllowed);
                    c.AddParam("@actionId", pActionId);
                    c.AddParam("@roleId", pRoleId);
                    c.ExecuteNonQuery();
                }
            }

        }

        public void UpdateAllowedItem(int pRoleId, int pActionId, bool pAllowed)
        {
            const string q = @"UPDATE [AllowedRoleActions]
                                            SET allowed = @allowed
                                            WHERE [action_item_id] = @actionId AND
                                            role_id = @roleId";

            using (SqlConnection conn = GetConnection())
            {
                using (OctopusCommand c = new OctopusCommand(q, conn))
                {
                    c.AddParam("@allowed", pAllowed);
                    c.AddParam("@actionId", pActionId);
                    c.AddParam("@roleId", pRoleId);
                    c.ExecuteNonQuery();
                }
            }
        }

        public void DeleteItem(MenuObject menu)
        {
            const string q = @"DELETE FROM dbo.AllowedRoleMenus
                            WHERE menu_item_id = @menu_item_id";

            using (SqlConnection conn = GetConnection())
            {
                using (OctopusCommand c = new OctopusCommand(q, conn))
                {
                    c.AddParam("@menu_item_id", menu.Id);
                    c.ExecuteNonQuery();
                }
            }

            const string q1 = @"DELETE FROM dbo.MenuItems
                            WHERE id = @id";

            using (SqlConnection conn = GetConnection())
            {
                using (OctopusCommand c = new OctopusCommand(q1, conn))
                {
                    c.AddParam("@id", menu.Id);
                    c.ExecuteNonQuery();
                }
            }
        }
    }
}
