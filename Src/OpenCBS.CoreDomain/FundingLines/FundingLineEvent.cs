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
using OpenCBS.CoreDomain.Events;
using OpenCBS.CoreDomain.Events.Loan;
using OpenCBS.Enums;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.FundingLines
{
    [Serializable]
    public class FundingLineEvent : Event
    {
        public new int Id { get; set; }

        private OCurrency _amount = decimal.Zero;
        private new string _code = string.Empty;
        private bool _isDeleted = false;
        private OBookingDirections _movement = OBookingDirections.Credit;
        private OFundingLineEventTypes _type = OFundingLineEventTypes.Entry;
        public DateTime EndDate { get; set; }
        public DateTime CreationDate { get; set; }
        public override string Description { get; set; }
        private FundingLine _fundingLine;

        public OFundingLineEventTypes Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public bool IsDeleted
        {
            get { return _isDeleted; }
            set { _isDeleted = value; }
        }

        public override string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        public OBookingDirections Movement
        {
            get { return _movement; }
            set { _movement = value; }
        }

        public OCurrency Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public FundingLine FundingLine
        {
            get { return _fundingLine; }
            set { _fundingLine = value; }
        }

        public override Event Copy()
        {
            return (FundingLineEvent) MemberwiseClone();
        }

        public Event AttachTo { get; set; }
    }
}
