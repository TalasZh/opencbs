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
