//Octopus MFS is an integrated suite for managing a Micro Finance Institution: clients, contracts, accounting, reporting and risk
//Copyright © 2006,2007 OCTO Technology & OXUS Development Network
//
//This program is free software; you can redistribute it and/or modify
//it under the terms of the GNU Lesser General Public License as published by
//the Free Software Foundation; either version 2 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU Lesser General Public License for more details.
//
//You should have received a copy of the GNU Lesser General Public License along
//with this program; if not, write to the Free Software Foundation, Inc.,
//51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
//
// Licence : http://www.octopusnetwork.org/OverviewLicence.aspx
//
// Website : http://www.octopusnetwork.org
// Business contact: business(at)octopusnetwork.org
// Technical contact email : tech(at)octopusnetwork.org 

using System;
using System.Collections.Generic;
using System.Linq;
using OpenCBS.CoreDomain;
using OpenCBS.Enums;
using OpenCBS.Extensions;
using OpenCBS.Manager;

namespace OpenCBS.Services
{
    public class MenuItemServices : BaseServices
    {
        private readonly MenuItemManager _menuItemManager;
        
        public MenuItemServices(User user)
        {
            _menuItemManager = new MenuItemManager(user);
        }

        public List<MenuObject> GetMenuList(params OSecurityObjectTypes[] securityObjectTypes)
        {
            List<MenuObject> list = _menuItemManager.GetMenuList(securityObjectTypes);

            foreach (IExtension extension in Extension.Instance.Extensions.Where(extension => extension.QueryInterface(typeof(IMenu)) != null 
                                                                                 && list.Find(item => item.Name == extension.GetMeta("Name")) == null))
            {
                list.Add(_menuItemManager.AddNewMenu(extension.GetMeta("Name")));
            }
            return list;
        }

        public MenuObject AddNewMenu(string name)
        {
            return _menuItemManager.AddNewMenu(name);
        }

        public MenuObject FindMenuItem(string menuItem)
        {
            return _menuItemManager.FindMenuItem(menuItem);
        }

        public void DeleteMenuItem(MenuObject menuItem)
        {
            _menuItemManager.DeleteItem(menuItem);
        }

        public static bool CheckMenuAccess(Guid menuItem)
        {
            MenuItemServices menuItemServices = ServicesProvider.GetInstance().GetMenuItemServices();
            MenuObject menuObject = menuItemServices.FindMenuItem(menuItem.ToString());
            return User.CurrentUser.UserRole.IsMenuAllowed(menuObject);
        }
    }
}
