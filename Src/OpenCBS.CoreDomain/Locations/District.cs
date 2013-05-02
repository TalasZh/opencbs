// LICENSE PLACEHOLDER

using System;

namespace OpenCBS.CoreDomain
{
	/// <summary>
	/// Summary description for DistrictArea.
    /// </summary>
    [Serializable]
	public class District
	{
		private int _id;
		private string _name;
		private Province _province;
        private bool _deleted = false;

        public District()
		{

		}

		public District(int id,string name)
		{
			_id = id;
			_name = name;
		}

		public District(int id,string name,Province province)
		{
			_id = id;
			_name = name;
			_province = province;
		}

		public District(string name,Province province)
		{
			_name = name;
			_province = province;
		}

		public int Id
		{
			get
			{
				return _id;	
			}
			set
			{
				_id = value;
			}
		}
		
		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
			}
		}

		public Province Province
		{
			get
			{
				return _province;
			}
			set
			{
				_province = value;
			}
		}

        public bool Deleted
        {
            get
            {
                return _deleted;
            }
            set
            {
                _deleted = value;
            }
        }
		
		public override string ToString()
		{
			return _name;
		}
	}
}
