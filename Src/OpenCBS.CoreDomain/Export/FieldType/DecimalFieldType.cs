// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenCBS.Shared;
using System.Globalization;

namespace OpenCBS.CoreDomain.Export.FieldType
{
    [Serializable]
    public class DecimalFieldType : IFieldType
    {
        public int DecimalNumber { get; set; }
        public string DecimalSeparator { get; set; }
        public string GroupSeparator { get; set; }

        public DecimalFieldType()
        {
            DecimalNumber = 2;
            DecimalSeparator = ",";
            GroupSeparator = "";
        }

        #region IFieldType Members

        public bool AlignRight { get; set; }

        public string Format(object o, int? length)
        {
            var d = (decimal)o;
            var numberFormat = new CultureInfo(CultureInfo.InvariantCulture.Name).NumberFormat;
            numberFormat.NumberGroupSeparator = GroupSeparator;
            numberFormat.NumberDecimalSeparator = DecimalSeparator;

            var format = "##,#." + new string('0', DecimalNumber);
            var s = d.ToString(format, _getFormatProvider());
            if (length.HasValue)
                return StringFieldType.Format(s, length.Value, AlignRight);
            else
                return s;
        }

        public object Parse(string s)
        {
            return Convert.ToDecimal(s, _getFormatProvider());
        }

        #endregion

        #region ICloneable Members

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion

        private IFormatProvider _getFormatProvider()
        {
            var numberFormat = new CultureInfo(CultureInfo.InvariantCulture.Name).NumberFormat;
            numberFormat.NumberGroupSeparator = GroupSeparator;
            numberFormat.NumberDecimalSeparator = DecimalSeparator;

            return numberFormat;
        }
    }
}
