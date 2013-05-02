// LICENSE PLACEHOLDER

using System;

namespace OpenCBS.CoreDomain
{
    /// <summary>
    /// City.
    /// </summary>
    [Serializable]
    public class City
    {
        private string _name;

        public int DistrictId { get; set; }
        public bool Deleted { get; set; }
        public int Id { get; set; }

        public City()
        {

        }

        //public City(string pName, int pDistrictId)
        //{
        //    _name = pName;
        //    DistrictId = pDistrictId;
        //}

        //public City(int pId, string pName, int pDistrictId)
        //{
        //    Id = pId;
        //    _name = pName;
        //    DistrictId = pDistrictId;
        //}

        public string Name
        {
            get {return _name;}
            set {_name = value;}
        }

        public override string ToString()
        {
            return _name;
        }
    }
}
