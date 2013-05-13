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
using System.Collections.Generic;
using System.Diagnostics;
using OpenCBS.Enums;

namespace OpenCBS.CoreDomain
{
    [Serializable]
    public class User:IComparable
    {
        private readonly List<User> _subordinates = new List<User>();
        private readonly List<Branch> _branches = new List<Branch>();
        private readonly List<Teller> _tellers = new List<Teller>(); 

        public User()
        {
            _role = Roles.LOF.ToString();
            _userRole = new Role { RoleName = Roles.LOF.ToString() };
            Sex = OGender.Male;
            Md5 = string.Empty;
        }

        private static User _currentUser = new User();

        static public User CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value;}
        }

        private string _role;
        private Role _userRole;

        public Role UserRole
        {
            get { return _userRole; }
            set { _userRole = value; }
        }
        public enum Roles
        {
            VISIT = 0,
            CASHI,
            LOF,
            ADMIN,
            SUPER
        }

        /// <summary>
        /// Returns Fullname of the user
        /// </summary>
        public override string ToString()
        {
            return Name;
        }

        public int CompareTo(object obj)
        {
            User tempUser = (User) obj;
            return string.CompareOrdinal(Name, tempUser.Name);
        }

        public int Id { get; set; }

        public string Md5 { get; set; }

        public string Mail { get; set;}

        public string UserName { get; set;}
        
        public bool IsDeleted { get; set; }
        
        /// <summary>
        ///This property returns Fullname of the user
        /// </summary>
        public string Name
        {
            get { return string.Format("{0} {1}", FirstName, LastName); }
        }

        public string FirstName { get; set;}

        public string LastName { get; set;}

        public string Password { get; set; }

        public string Phone { get; set; }

        public Roles? Role
        {
            get
            {
                if (_role == null) return null;
                try
                {
                    return (Roles)Enum.Parse(typeof(Roles), _role);
                }
                catch (Exception)
                {
                    return Roles.VISIT;
                }
                
            }
            set { _role = value.ToString(); }
        }

        public void SetRole(string pRole)
        {
               _role = pRole; 
        }

        public bool HasAdminRole
        {
            get
            {
                return (_role == Roles.ADMIN.ToString()) || (_role == Roles.SUPER.ToString());
            }
        }

        public bool HasContract { get; set; }

        public void ClearSubordinates()
        {
            _subordinates.Clear();
        }

        public void AddSubordinate(User user)
        {
            if (null == user) return;
            if (HasAsSubordinate(user)) return;
            _subordinates.Add(user);
        }

        public void AddSubordinates(IEnumerable<User> users)
        {
            foreach (User user in users)
            {
                AddSubordinate(user);
            }
        }

        public bool HasAsSubordinate(User user)
        {
            return _subordinates.IndexOf(user) > -1;
        }

        public IEnumerable<User> Subordinates
        {
            get
            {
                foreach (User user in _subordinates)
                {
                    yield return user;
                }
            }
        }

        public char Sex { get; set; }

        public int SubordinateCount
        {
            get
            {
                return _subordinates.Count;
            }
        }

        public void ClearBranches()
        {
            _branches.Clear();
        }

        public bool HasBranch(Branch branch)
        {
            return _branches.IndexOf(branch) > -1;
        }

        public void AddBranch(Branch branch)
        {
            Debug.Assert(branch != null, "Branch is null");
            if (HasBranch(branch)) return;
            _branches.Add(branch);
        }

        public void AddBranches(IEnumerable<Branch> branches)
        {
            foreach (Branch branch in branches)
            {
                _branches.Add(branch);
            }
        }

        public IEnumerable<Branch> Branches
        {
            get
            {
                foreach (Branch branch in _branches) yield return branch;
            }
        }

        public int BranchCount
        {
            get { return _branches.Count; }
        }

        public void ClearTellers()
        {
            _tellers.Clear();
        }

        public bool HasTeller(Teller teller)
        {
            return _tellers.IndexOf(teller) > -1;
        }

        public void AddTeller(Teller teller)
        {
            Debug.Assert(teller != null, "Teller is null");
            if(HasTeller(teller)) return;
            _tellers.Add(teller);
        }

        public void AddTellers(IEnumerable<Teller> tellers)
        {
            foreach (Teller teller in tellers)
            {
                _tellers.Add(teller);
            }
        }

        public IEnumerable<Teller> Tellers
        {
            get
            {
                foreach (Teller teller in _tellers) yield return teller;
            }
        }

        public int TellerCount
        {
            get { return _tellers.Count; }
        }
   }
}
