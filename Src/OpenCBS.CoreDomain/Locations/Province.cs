// LICENSE PLACEHOLDER

using System;

namespace OpenCBS.CoreDomain
{
	/// <summary>
	/// Summary description for ProvinceArea.
    /// </summary>
    [Serializable]
	public class Province
	{
		private int _id;
		private string _name;
        private bool _deleted = false;

		public Province() {}

		public Province(int id,string name)
		{
			_id = id;
			_name = name;
		}
		
		public Province(string name)
		{
			_name = name;
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
