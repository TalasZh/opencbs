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

namespace OpenCBS.CoreDomain
{
    public  class EntryFee
    {
        public EntryFee()
        {
            Id = null;
            Name = "";
            Min = null;
            Max = null;
            Value = null;
            IsRate = false;
            Index = -1;
        }
        public int? Id { get; set; }
        public string Name { get; set; }
        public decimal? Min { get; set; }
        public decimal? Max { get; set; }
        public decimal? Value { get; set; }
        public bool IsRate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsAdded { get; set; }
        public int Index { get; set; }
        public int? CycleId { get; set; }
        public int IdForNewItem { get; set; }

        public void Copy(EntryFee entryFee)
        {
            entryFee.Id = Id;
            entryFee.Name = Name;
            entryFee.Min = Min;
            entryFee.Max = Max;
            entryFee.Value = Value;
            entryFee.IsRate = IsRate;
            entryFee.IsAdded = IsAdded;
            entryFee.Index = Index;
            entryFee.CycleId = CycleId;
            entryFee.IdForNewItem = IdForNewItem;
        }
    }
}
