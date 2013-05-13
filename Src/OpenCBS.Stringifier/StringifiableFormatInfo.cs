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
using System.Text.RegularExpressions;

namespace OpenCBS.Stringifier
{
    public class StringifiableFormatInfo : IFormatProvider, ICustomFormatter
    {
        public object GetFormat(Type formatType)
        {
            return (typeof (ICustomFormatter).Equals(formatType)) ? this : null;
        }
    
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (null == arg) throw new ArgumentNullException("arg");

            if (format != null && arg is decimal)
            {
                const string re = @"^(N|D)(%?)(\[[a-z]{2}\])$";
                Match match = Regex.Match(format, re);
                if (!match.Success) throw new Exception("Invalid format");

                string lang = match.Groups[3].ToString();
                lang = lang.ToLower();
                lang = lang.Trim(new[] {'[', ']'});

                IStringifiable iface = GetStringifiable(lang);
                if (null == iface) throw new Exception("Language not implemented");

                decimal amount = Convert.ToDecimal(arg);
                if ("N" == match.Groups[1].ToString())
                {
                    amount = Math.Floor(amount);
                }

                return "%" == match.Groups[2].ToString()
                           ? iface.StringifyWithPercent(amount)
                           : iface.Stringify(amount);
            }

            if (arg is IFormattable)
            {
                return ((IFormattable) arg).ToString(format, formatProvider);
            }

            return arg.ToString();
        }

        private static IStringifiable GetStringifiable(string lang)
        {
            switch (lang)
            {
                case "en":
                    return new English();

                case "ru":
                    return new Russian();

                case "fr":
                    return new French();

                case "tj":
                    return new Tadjik();

                case "es":
                    return new Spanish();

                case "mr":
                    return new Marathi();

                default:
                    return null;
            }
        }
    }
}
