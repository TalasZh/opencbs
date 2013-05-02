// LICENSE PLACEHOLDER

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
