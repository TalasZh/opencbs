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
using OpenCBS.CoreDomain.Events.Loan;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Events
{
    /// <summary>
    /// TrancheEvent class
    /// </summary>
    [Serializable]
    public class TrancheEvent : Event
    {
        public override string Code
        {
            get { return "TEET"; }
            set { _code = value; }
        }

        public OCurrency InterestRate { get; set; }
        public OCurrency Amount { get; set; }
        public int? Maturity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CloseDate { get; set; }
        public bool Closed { get; set; }
        public bool ApplyNewInterest { get; set; }
        /// <summary>
        /// Number of tranche event
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// Installment on which the tranche was added
        /// </summary>
        public int StartedFromInstallment { get; set; }

        public override string Description { get; set; }

        public override Event Copy()
        {
            return (TrancheEvent) MemberwiseClone();
        }
    }
}
