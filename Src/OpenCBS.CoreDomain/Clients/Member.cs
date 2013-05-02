// LICENSE PLACEHOLDER

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
