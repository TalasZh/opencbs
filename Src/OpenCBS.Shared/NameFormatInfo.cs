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

namespace OpenCBS.Shared
{
    /// <remarks>
    /// Implement functionality for custom name formatting
    /// </remarks>
    public class NameFormatInfo : IFormatProvider, ICustomFormatter
    {
        public object GetFormat(Type formatType)
        {
            if (typeof(ICustomFormatter).Equals(formatType)) return this;
            return null;
        }

        /// <summary>
        /// Formats string according to the format specifier
        /// #: returns string as is, e.g. "BObOjAn" -> "BObOjAn"
        /// L: return lowercase string, e.g. "BObOjAn" -> "bobojan"
        /// U: return uppercase string, e.g. "BObOjAn" -> "BOBOJAN"
        /// U#: return string as is with the first letter uppercase, e.g. "boboJAN" -> "BobobJAN"
        /// UL: return lowercase string with first letter uppercase, e.g. "boboJAN" -> "Bobojan"
        /// </summary>
        /// <param name="format">Format specifier</param>
        /// <param name="arg">Name to format</param>
        /// <param name="formatProvider">Format provider</param>
        /// <returns></returns>
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (null == arg) throw new ArgumentNullException("arg");

            if (format != null && arg is string)
            {
                var s = format.Trim().ToUpper();
                var retval = arg as string;
                if ("#" == s)
                {
                    return retval;
                }
                else if ("L" == s)
                {
                    return retval.ToLower();
                }
                else if ("U" == s)
                {
                    return retval.ToUpper();
                }
                else if ("U#" == s)
                {
                    return Char.ToUpper(retval[0]) + retval.Substring(1);
                }
                else if ("UL" == s)
                {
                    return Char.ToUpper(retval[0]) + retval.Substring(1).ToLower();
                }
            }

            if (arg is IFormattable)
            {
                return ((IFormattable) arg).ToString(format, formatProvider);
            }
            else
            {
                return arg.ToString();
            }
        }
    }
}
