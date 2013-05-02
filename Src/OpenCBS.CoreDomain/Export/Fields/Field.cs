// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenCBS.CoreDomain.Export.FieldType;

namespace OpenCBS.CoreDomain.Export.Fields
{
    [Serializable]
    public class Field : IField
    {
        #region IField Members

        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Header { get; set; }
        public int? Length { get; set; }

        #endregion

        #region ICloneable Members
        public object Clone()
        {
            object clone = this.MemberwiseClone();
            ((Field)clone).FieldType = this.FieldType.Clone() as IFieldType;
            return clone;
        }
        #endregion

        public int DefaultLength { get; set; }
        public IFieldType FieldType { get; set; }
        public bool IsRequired { get; set; }
        public string Format(object o)
        {
            return FieldType.Format(o, Length);
        }
    }
}
