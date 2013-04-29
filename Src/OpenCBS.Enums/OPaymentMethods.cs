using System;

namespace Octopus.Enums
{
    [Serializable]
    public enum OPaymentMethods
    {
        All = 0,
        Cash = 1,
        Savings = 2,
        Voucher = 3,
        Withdrawal = 4,
        DirectDebit = 5,
        WireTransfer = 6,
        DebitCard = 7,
        Cheque = 8,
        Unknown = 9
    }
}
