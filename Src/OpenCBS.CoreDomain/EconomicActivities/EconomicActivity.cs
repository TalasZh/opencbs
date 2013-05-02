// LICENSE PLACEHOLDER

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
