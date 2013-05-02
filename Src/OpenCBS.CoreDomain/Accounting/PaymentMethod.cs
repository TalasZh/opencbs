// LICENSE PLACEHOLDER

using System;
using OpenCBS.Enums;
using OpenCBS.MultiLanguageRessources;

namespace OpenCBS.CoreDomain.Accounting
{
    public class PaymentMethod
    {
        private string _name;
        public int Id { get; set; }
        public string Name
        {
            get
            {
                if (Id == 0)
                    return MultiLanguageStrings.GetString(Ressource.AccountingRule, "All.Text");
                return _name;
            }
            set { _name = value; }
        }
        public string Description { get; set; }
        public bool IsPending { get; set; }
        public OPaymentMethods Method { get; set; }
        public int LinkId { get; set; }
        public Branch Branch { get; set; }
        public DateTime Date { get; set; }
        public Account Account { get; set; }

        public PaymentMethod()
        {
            DeterminePaymentMethodByName();
        }

        public PaymentMethod(int id, string name, string description, bool isPending)
        {
            Id = id;
            Name = name;
            Description = description;
            IsPending = isPending;
            DeterminePaymentMethodByName();
        }

        public PaymentMethod(int id, int linkId, string name, string description, bool isPending, Branch branch, DateTime date, Account account)
        {
            Id = id;
            LinkId = linkId;
            Name = name;
            Description = description;
            IsPending = isPending;
            Branch = branch;
            Account = account;
            Date = date;
            DeterminePaymentMethodByName();
        }

        private void DeterminePaymentMethodByName()
        {
            if (Name==Enum.GetName(typeof(OPaymentMethods), OPaymentMethods.All)) Method = OPaymentMethods.All;
            else if (Name == Enum.GetName(typeof(OPaymentMethods), OPaymentMethods.Cash)) Method = OPaymentMethods.Cash;
            else if (Name == Enum.GetName(typeof(OPaymentMethods), OPaymentMethods.Voucher)) Method = OPaymentMethods.Voucher;
            else if (Name == Enum.GetName(typeof(OPaymentMethods), OPaymentMethods.Withdrawal)) Method = OPaymentMethods.Withdrawal;
            else if (Name == Enum.GetName(typeof(OPaymentMethods), OPaymentMethods.DirectDebit)) Method = OPaymentMethods.DirectDebit;
            else if (Name == Enum.GetName(typeof(OPaymentMethods), OPaymentMethods.WireTransfer)) Method = OPaymentMethods.WireTransfer;
            else if (Name == Enum.GetName(typeof(OPaymentMethods), OPaymentMethods.DebitCard)) Method = OPaymentMethods.DebitCard;
            else if (Name == Enum.GetName(typeof(OPaymentMethods), OPaymentMethods.Cheque)) Method = OPaymentMethods.Cheque;
            else if (Name == Enum.GetName(typeof(OPaymentMethods), OPaymentMethods.Savings)) Method = OPaymentMethods.Savings;
            else Method = OPaymentMethods.Unknown;
        }
    }
}
