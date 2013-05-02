// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenCBS.CoreDomain
{
    public class CycleObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        #region Constructors
        public CycleObject() { }
        
        public CycleObject(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public CycleObject(string name)
        {
            Name = name;
        }

        public CycleObject(int id)
        {
            Id = id;
        }
        #endregion
    }
}
