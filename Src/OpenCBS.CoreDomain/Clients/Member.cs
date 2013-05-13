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
using OpenCBS.Shared;


namespace OpenCBS.CoreDomain.Clients
{
    [Serializable]
    public class Member
    {
        private bool _newer;
        public Member()
        {
            Tiers = new Person();
            _newer = true;
        }

        public static bool operator ==(Member a,Member b)
        {
            if((object)b == null) return (object) a == null;
            return a.Equals(b);
        }

        public override bool Equals(object b)
        {
            if (null == b) return false;
            if (GetType() != b.GetType()) return false;
            string id1 = ((Person)Tiers).IdentificationData;
            string id2 = ((Person) ((Member) b).Tiers).IdentificationData;
            return id2 == id1;
        }

        public override int GetHashCode()
        {
            return Tiers.Id;
        }

        public bool Newer
        {
            get { return _newer; }
            set { _newer = value; }
        }

        public static bool operator !=(Member a, Member b)
        {
            return !(a == b);
        }

        public IClient Tiers { get; set; }

        public OCurrency LoanShareAmount { get; set; }

        public bool CurrentlyIn { get; set; }

        public bool IsLeader { get; set; }

        public DateTime JoinedDate { get; set; }

        public DateTime? LeftDate { get; set; }

        public Member Copy()
        {
            Member clonedMember = (Member)MemberwiseClone();
            clonedMember.Tiers = Tiers.Copy();
            return clonedMember;
        }
    }
}
