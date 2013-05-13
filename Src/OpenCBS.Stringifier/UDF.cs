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
using Microsoft.SqlServer.Server;

namespace OpenCBS.Stringifier
{
    public class Udf
    {
        [SqlFunction(DataAccess = DataAccessKind.Read, IsDeterministic = true, Name = "AmountToLetters")]
        public static string AmountToLetters(decimal amount, string lang)
        {
            try
            {
                StringifiableFormatInfo provider = new StringifiableFormatInfo();
                string fmt = "{0:D[" + lang + "]}";
                return string.Format(provider, fmt, Convert.ToDecimal(amount));
            }
            catch (Exception e)
            {
                return string.Format("Exception: {0}", e.Message);
            }
        }

        [SqlFunction(DataAccess = DataAccessKind.Read, IsDeterministic = true, Name = "AmountToLettersPercent")]
        public static string AmountToLettersPercent(decimal amount, string lang, bool showPercent)
        {
            try
            {
                StringifiableFormatInfo provider = new StringifiableFormatInfo();
                string fmt = "{0:D" + (showPercent ? "%" : "") + "[" + lang + "]}";
                return string.Format(provider, fmt, amount);
            }
            catch (Exception e)
            {
                return string.Format("Exception: {0}", e.Message);
            }
        }

        [SqlFunction(DataAccess = DataAccessKind.Read, IsDeterministic = true, Name = "Stringify")]
        public static string Stringify(string format, decimal amount)
        {
            try
            {
                StringifiableFormatInfo provider = new StringifiableFormatInfo();
                string fmt = "{0:" + format + "}";
                return string.Format(provider, fmt, amount);
            }
            catch (Exception ex)
            {
                return string.Format("Exception: {0}", ex.Message);
            }
        }
    }
}
