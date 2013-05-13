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

namespace OpenCBS.CoreDomain.EconomicActivities
{
	/// <summary>
    /// Summary description for EconomicActivity.
    /// </summary>
    [Serializable]
	public class EconomicActivity
	{
		private int _id;
		private string _name;
		private EconomicActivity _parent;
		private bool _deleted;
	    private List<EconomicActivity> _childrens;
	    private bool _hasValue;
      
		public EconomicActivity()
		{
			_childrens = new List<EconomicActivity>();
		    _hasValue = true;
		}
        
        public bool HasValue
        {
            get { return _hasValue; }
        }

		public EconomicActivity(int id,string name,EconomicActivity pParent,bool deleted)
		{
			_id = id;
			_name = name;
            _parent = pParent;
			_deleted = deleted;
            _hasValue = true;
		}

        public EconomicActivity(string name, EconomicActivity pParent, bool deleted)
		{
			_name = name;
            _parent = pParent;
			_deleted = deleted;
            _hasValue = true;
		}

	    public List<EconomicActivity> Childrens
	    {
            get { return _childrens; }
            set { _childrens = value; }
	    }

	    public bool HasChildrens
	    {
            get { return _childrens.Count > 0; }
	    }

		public int Id
		{
			get {return _id;}
			set{_id = value;}
		}

        public void RemoveChildren(EconomicActivity pChildren)
        {
            for (int i = 0; i < _childrens.Count; i++)
            {
                EconomicActivity domainOfApplication = _childrens[i];
                if (domainOfApplication._id == pChildren.Id)
                {
                    _childrens.Remove(pChildren);
                    break;
                }
            }
        }

		public string Name
		{
			get{ return _name;}
			set{ _name = value;}
		}

		public EconomicActivity Parent
		{
			get{ return _parent;}
			set{ _parent = value;}
		}

		public bool Deleted
		{
			get{ return _deleted;}
			set{ _deleted = value;}
		}
	}
}
