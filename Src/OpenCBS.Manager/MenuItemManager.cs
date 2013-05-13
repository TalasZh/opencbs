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
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                using (OpenCbsReader r = c.ExecuteReader())
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
                using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
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
            using (OpenCbsCommand cmd = new OpenCbsCommand(query, conn))
            {
                cmd.AddParam("menuItem", menuItem);
                OpenCbsReader reader = cmd.ExecuteReader();
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
                using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
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
                using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
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
                using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
                {
                    c.AddParam("@menu_item_id", menu.Id);
                    c.ExecuteNonQuery();
                }
            }

            const string q1 = @"DELETE FROM dbo.MenuItems
                            WHERE id = @id";

            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand c = new OpenCbsCommand(q1, conn))
                {
                    c.AddParam("@id", menu.Id);
                    c.ExecuteNonQuery();
                }
            }
        }
    }
}
