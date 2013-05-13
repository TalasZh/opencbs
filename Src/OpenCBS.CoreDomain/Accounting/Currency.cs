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

namespace OpenCBS.CoreDomain.Accounting
{
    [Serializable]
    public class Currency
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsPivot { get; set; }
        public bool IsSwapped { get; set; }
        public string Code { get; set; }
        public override bool Equals(object obj)
        {
            if(obj is Currency)
            {
                Currency compareWith = obj as Currency;
                if(this.Name.Equals(compareWith.Name) && this.Code.Equals(compareWith.Code))
                    return true;
                
            }
            return false;
        }
        public override int GetHashCode()
        {
            return Id;
        }
        public override String ToString()
        {
            return Name;
        }
        public bool UseCents { get; set; }
    }
}
