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
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.CoreDomain.EconomicActivities;
using OpenCBS.CoreDomain.Products;
using OpenCBS.Enums;

namespace OpenCBS.CoreDomain.Events.Loan
{
    [Serializable]
	public abstract class Event
	{
		protected int _id;
		protected string _code;
		protected DateTime _date;
		protected User _user;
		protected bool _cancelable;
		protected bool _deleted;
	    protected bool _isFired;
        protected OClientTypes _clientType;
        OPaymentType _repaymentType = OPaymentType.StandardPayment;
        public DateTime ExportedDate { get; set; }
        public DateTime EntryDate { get; set; }
        public int InstallmentNumber { get; set; }
        public LoanProduct LoanProduct { get; set; }
        public ISavingProduct SavingProduct { get; set; }
        public OClientTypes ClientType
        {
            get { return _clientType; }
            set { _clientType = value; }
        }
        public EconomicActivity EconomicActivity { get; set; }
        public int ContracId { get; set; }
        public Currency Currency { get; set; }
        public Branch Branch { get; set; }

        public string Comment { get; set; }
        public int? TellerId { get; set; }
        public int? ParentId { get; set; }

        public Event()
        {
            EntryDate = DateTime.Today;
        }

		public int Id
		{
			get{return _id;}
			set{_id = value;}
		}

        public bool IsFired
        {
            get { return _isFired; }
            set { _isFired = value; }
        }
		
		public bool Deleted
		{
            get { return _deleted; }
            set { _deleted = value; }
		}
		
		public bool Cancelable
		{
            get { return _cancelable; }
            set { _cancelable = value; }
		}

        public OPaymentType RepaymentType
        {
            get { return _repaymentType; }
            set { _repaymentType = value; }
        }

		public virtual string Code{get;set;}
        public virtual string Description { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

		public DateTime Date
		{
            get { return _date; }
            set { _date = value; }
		}

		public User User
		{
			get { return _user; }
            set { _user = value; }
		}

		public virtual Event Copy()
		{
		    return (Event) MemberwiseClone();
		}

        public DateTime? CancelDate { get; set; }
	}
}
