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

using System.Collections.Generic;

namespace OpenCBS.CoreDomain.Accounting
{
    public class ClosureOptions
    {
        public bool DoOverdue{ get; set;}
        public bool DoProvision { get; set; }
        public bool DoAccrued { get; set; }
        public bool DoLoanClosure { get; set; }
        public bool DoSavingClosure { get; set; }
        public bool DoTellerManagementClosure { get; set; }
        public bool DoReversalTransactions { get; set; }
        public bool DoSavingEvents { get; set; }
        public bool DoManualEntries { get; set; }
    }

    public class ClosureOption : object
    {
        public string Name { get; set; }
        public int Id { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public class ClosureItems
    {
        public ClosureItems()
        {
            Items = new List<ClosureOption>();
        }

        public List<ClosureOption> Items { get; set; }
    }
}
