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

namespace OpenCBS.Enums
{
    [Serializable]
    public enum OClientTypes
    {
        Person = 1,
        Group = 2,
        All = 3,
        Corporate = 4,
        Village = 5
    }

    public static class OClientTypeExtensions
    {
        public static OClientTypes ConvertToClientType(this char toConvert)
        {
            switch (toConvert)
            {
                case 'C' :
                    return OClientTypes.Corporate;
                case 'G':
                    return OClientTypes.Group;
                case 'I':
                    return OClientTypes.Person;
                case 'V':
                    return OClientTypes.Village;
                default:
                    return OClientTypes.All;
            }
        }

        public static OClientTypes ConvertToClientType(this string toConvert)
        {
            if(toConvert == null) throw new ArgumentNullException("toConvert");
            if(toConvert.Length != 1)  throw new ArgumentException("Lenth of toConvert argument should be 1");

            return toConvert[0].ConvertToClientType();
        }
    }
}
