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
using OpenCBS.Manager;
using OpenCBS.CoreDomain;
using OpenCBS.ExceptionsHandler;

namespace OpenCBS.Services
{
    /// <summary>
    /// Role Services.
    /// </summary>
    [Security()]
    public class RoleServices : MarshalByRefObject
    {
        private readonly RoleManager _roleManager;
        private readonly User _user;

        private Dictionary<int, int> _userToRole;


        public RoleServices(User pUser)
        {
            _roleManager = new RoleManager(pUser);
            _user = pUser;
        }
        public RoleServices(string pTestDb)
        {
            _roleManager = new RoleManager(pTestDb);
        }
        public RoleServices(RoleManager pRoleManager)
        {
            _roleManager = pRoleManager;
        }

        /// <summary>
        /// Delete the selected role
        /// </summary>
        /// <param name="pRoleName"></param>
        /// <returns>true/false or throw an exception</returns>
        public bool DeleteRole(string pRoleName)
        {
            //IF role is null 
            if (string.IsNullOrEmpty(pRoleName))
                throw new OpenCbsRoleDeleteException(OpenCbsRoleDeleteExceptionsEnum.RoleIsNull);
            
            if(_roleManager.SelectUserForThisRole(pRoleName) > 0)
                throw new OpenCbsRoleDeleteException(OpenCbsRoleDeleteExceptionsEnum.RoleHasUsers);
            
            _roleManager.DeleteRole(pRoleName);
            return true;
        }

        /// <summary>
        /// Contains all role's errors
        /// </summary>
        [Serializable]
        public struct RoleErrors
        {
            public bool FindError;
            
            public bool RoleNameError;
            
            //public string ResultMessage;

            public string ErrorCode;
        }

        /// <summary>
        /// Save selected role
        /// </summary>
        /// <param name="pRole"></param>
        /// <returns>A struct contains, if necessary, errors occurs</returns>
        public RoleErrors SaveRole(Role pRole)
        {
            RoleErrors roleErrors = new RoleErrors();

            if (string.IsNullOrEmpty(pRole.RoleName))
            {
                roleErrors.FindError = true;
                roleErrors.RoleNameError = true;
                //roleErrors.ResultMessage += "\n - " + MultiLanguageStrings.GetString(Ressource.StringRes, "Role_RoleName_Empty.Text");
                roleErrors.ErrorCode = "RoleNameEmpty";
            }

            if (roleErrors.FindError) 
                return roleErrors;

            if (pRole.Id == 0)
            {
                if (_roleManager.SelectRole(pRole.RoleName,true) != null)
                {
                    roleErrors.FindError = true;
                    //roleErrors.ResultMessage += "\n - " + MultiLanguageStrings.GetString(Ressource.StringRes, "Role_Save_AlreadyExist.Text");
                    roleErrors.ErrorCode = "RoleExists";
                }
                else
                {
                    _roleManager.AddRole(pRole);
                    //roleErrors.ResultMessage = MultiLanguageStrings.GetString(Ressource.StringRes, "Role_Save_OK.Text");
                    roleErrors.ErrorCode = "RoleCreated";
                }
            }
            else
            {
                //user UpdateRoles for this block
            }

            return roleErrors;
        }

        /// <summary>
        /// Update selected role
        /// </summary>
        /// <param name="pRole"></param>
        /// <returns>A struct contains, if necessary, errors occurs</returns>

        public RoleErrors UpdateRole(Role pRole)
        {

            RoleErrors roleErrors = new RoleErrors();

            _roleManager.UpdateRole(pRole);
            roleErrors.ErrorCode = "RoleUpdated";

            return roleErrors;
        }

        /// <summary>
        /// Find a role by its database id
        /// </summary>
        /// <param name="pRoleId"></param>
        /// <returns>selected role or null otherwise</returns>
        public Role FindRole(int pRoleId)
        {
            return _roleManager.SelectRole(pRoleId, false);
        }

        /// <summary>
        /// Find a role by it's credential (login / password)
        /// </summary>
        /// <param name="pRoleName"></param>
        /// <returns>selected role or null otherwise</returns>
        public Role FindRole(string pRoleName)
        {
            return _roleManager.SelectRole(pRoleName,true);
        }

        public List<Role> FindAllRoles(bool pSelectDeletedRoles)
        {
            return _roleManager.SelectAllRoles(pSelectDeletedRoles);
        }
        public void UpdateMenuList(List<MenuObject> pMenuTitles)
        {
            _roleManager.UpdateMenuList(pMenuTitles);
        }
        public Dictionary<MenuObject, bool> GetAllowedMenuList(int pRoleId)
        {
            return _roleManager.GetAllowedMenuList(pRoleId);
        }
        public Dictionary<ActionItemObject, bool> GetAllowedActionList(int pRoleId)
        {
            return _roleManager.GetAllowedActionList(pRoleId);
        }
        public bool IsThisActionAllowedForThisRole(int pRoleId, ActionItemObject pAction)
        {
            return _roleManager.IsThisActionAllowedForThisRole(pRoleId, pAction);
        }

        private void LoadUserToRole()
        {
            if (_userToRole != null) return;

            _userToRole = _roleManager.SelectUserToRole();
        }

        public Role Find(User user)
        {
            // TODO: Refactor
            // Implement some less db intense mechanism for
            // fetching and assigning allowed menu items and actions.
            LoadUserToRole();
            int roleId = _userToRole[user.Id];
            Role role = _roleManager.SelectRole(roleId, true);
            role.SetMenuItems(_roleManager.GetAllowedMenuList(roleId));
            role.SetActionItems(_roleManager.GetAllowedActionList(roleId));
            return role;
        }
    }
}
