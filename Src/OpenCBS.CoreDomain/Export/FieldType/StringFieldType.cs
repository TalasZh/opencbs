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

namespace OpenCBS.CoreDomain.Export.FieldType
{
    [Serializable]
    public class StringFieldType : IFieldType
    {
        //public Enum? Enum { get; set; }
        public int StartPosition { get; set; }
        public int? EndPosition { get; set; }
        public Dictionary<string, string> ReplacementList
        {
            get;
            set;
        }

        #region IFieldType Members

        public bool AlignRight { get; set; }

        public StringFieldType()
        {
            ReplacementList = new Dictionary<string, string>();
        }

        public string Format(object o, int? length)
        {
            var s = (string)o;

            if (s != null)
            {
                foreach (var key in ReplacementList.Keys)
                {
                    s = s.Replace(key, ReplacementList[key]);
                }

                if (EndPosition.HasValue)
                    s = s.Substring(StartPosition, EndPosition.Value - StartPosition);
                else
                    s = s.Substring(StartPosition);


                if (length.HasValue)
                    return Format(s, length.Value, AlignRight);
                else
                    return s;
            }
            else
                return string.Empty;
        }

        public object Parse(string s)
        {
            return s;
        }

        #endregion

        public static string Format(string s, int length, bool alignRight)
        {
            if (s.Length > length)
                return s.Substring(0, length);
            else
                if (alignRight)
                    return s.PadLeft(length);
                else
                    return s.PadRight(length);
        }

        #region ICloneable Members

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }
}
