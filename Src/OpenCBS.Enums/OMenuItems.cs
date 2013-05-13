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

namespace OpenCBS.Enums
{
    [Serializable]
    public enum OMenuItems
    {
        mnuClients = 1,
        mnuContracts = 2,
        mnuAccounting = 3,
        mnuConfiguration = 4,
        mnuReporting = 5,
        mnuAuditTrail = 6,
        mnuDatamanagement = 7,
        mnuWindow = 8,
        mnuHelp = 9,
        mnuPersonForm = 10,
        mnuContractForm = 11,
        mnuGroupForm = 12,
        mnuVillageForm = 13,
        mnuLoanRepayment = 14,
        mnuSavingContractForm = 15,
        tabLoanDetails = 16

    }

    public static class OMenuItemsExtensions
    {
        public static bool IsControl(this OMenuItems menuItem)
        {
            switch (menuItem)
            {
                case OMenuItems.mnuPersonForm:
                case OMenuItems.mnuContractForm:
                case OMenuItems.mnuGroupForm:
                case OMenuItems.mnuVillageForm:
                case OMenuItems.mnuLoanRepayment:
                case OMenuItems.mnuSavingContractForm:
                case OMenuItems.tabLoanDetails:
                    return true;
                default:
                    return false;
            }
        }

        public static string GetName(this OMenuItems menuItem)
        {
            return Enum.GetName(typeof (OMenuItems), menuItem);
        }
    }
}
