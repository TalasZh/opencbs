// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenCBS.CoreDomain.Export.FieldType
{
    [Serializable]
    public class IntegerFieldType : IFieldType
    {
        public bool DisplayZeroBefore { get; set; }

        #region IFieldType Members

        public bool AlignRight { get; set; }

        public string Format(object o, int? length)
        {
            var i = (int)o;
            var s = length.HasValue && DisplayZeroBefore ? i.ToString(new string('0', length.Value)) : i.ToString();
            if (length.HasValue)
                return StringFieldType.Format(s, length.Value, AlignRight);
            else
                return s;
        }

        public object Parse(string s)
        {
            return Convert.ToInt32(s);
        }

        #endregion

        #region ICloneable Members

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }
}
