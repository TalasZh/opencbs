// LICENSE PLACEHOLDER

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
