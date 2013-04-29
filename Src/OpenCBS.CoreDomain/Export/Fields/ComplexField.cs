using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Octopus.CoreDomain.Export.Fields
{
    [Serializable]
    public class ComplexField : IField
    {
        public ComplexField()
        {
            Fields = new List<IField>();
        }

        #region IField Members

        public string Name
        {
            get;
            set;
        }

        public string DisplayName
        {
            get;
            set;
        }

        public string Header
        {
            get { return DisplayName; }
            set { DisplayName = value; }
        }

        public int? Length
        {
            get;
            set;
        }

        #endregion

        #region ICloneable Members

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion

        public List<IField> Fields
        {
            get;
            set;
        }
    }
}
