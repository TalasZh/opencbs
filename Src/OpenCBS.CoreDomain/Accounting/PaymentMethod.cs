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
