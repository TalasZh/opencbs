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

using System.Diagnostics;

namespace OpenCBS.Stringifier
{
    public class French : Stringifiable
    {
        protected override string[] GetOneToNineteenArray()
        {
            return new[]
            {
                "",
                "un", 
                "deux", 
                "trois", 
                "quatre", 
                "cinq", 
                "six", 
                "sept", 
                "huit", 
                "neuf",
                "dix", 
                "onze", 
                "douze", 
                "treize", 
                "quatorze", 
                "quinze",
                "seize", 
                "dix-sept", 
                "dix-huit", 
                "dix-neuf"
            };
        }

        protected override string[] GetFirstOrderArray()
        {
            return new[]
            {
                "vingt",
                "trente",
                "quarante",
                "cinquante",
                "soixante",
                "soixante",
                "quatre-vingt",
                "quatre-vingt"
            };
        }

        protected override string GetZero()
        {
            return "zéro";
        }

        protected override string GetOneToNineteen(int index, int[] arr, object param)
        {
            Debug.Assert(index >=0 & index < arr.Length, "Out of range");

            if (1 == arr.Length & 0 == arr[index]) return GetZero();

            int i = index%3;
            if (1 == i) return base.GetOneToNineteen(index, arr, param);
            
            Debug.Assert(0 == i, "Invalid index");

            int next = 0;
            Debug.Assert(next >= 0 & next <= 9, "Invalid next number");
            if (index + 1 < arr.Length)
            {
                next = arr[index + 1];
            }

            int num = arr[index];
            Debug.Assert(num >= 0 & num <= 9, "Invalid number");
            switch (next)
            {
                case 0:
                case 1:
                    return base.GetOneToNineteen(index, arr, param);

                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                    string prefix = 1 == num ? " et " : "-";
                    prefix = 0 == num ? "" : prefix;
                    return prefix + GetOneToNineteenArray()[num];

                case 7:
                case 9:
                    num += 10;
                    return "-" + GetOneToNineteenArray()[num];

                case 8:
                    if (num > 0)
                    {
                        return "-" + GetOneToNineteenArray()[num];
                    }
                    return string.Empty;

                default:
                    Debug.Fail("Cannot be here");
                    return string.Empty;
            }
        }

        protected override string GetFirstOrder(int index, int[] arr)
        {
            Debug.Assert(1 == index % 3 & index < arr.Length, "Invalid index");
            int num = arr[index]*10 + arr[index - 1];
            if (80 == num) return GetFirstOrderArray()[6] + "s";
            return base.GetFirstOrder(index, arr);
        }

        protected override string GetSecondOrder(int index, int[] arr)
        {
            Debug.Assert(2 == index % 3 & index < arr.Length, "Out of range");
            int num = arr[index];
            Debug.Assert(num >= 1 & num <= 9, "Out of range");
            return GetOneToNineteenArray()[num] + " cent";
        }

        protected override string GetThousand(int index, int[] arr)
        {
            return "mille";
        }

        protected override string BeforeReturn(string value)
        {
            value = base.BeforeReturn(value);
            return value.Replace(" -", "-");
        }
    }
}
