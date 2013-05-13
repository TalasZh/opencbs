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
    public class English : Stringifiable
    {
        protected override string[]  GetOneToNineteenArray()
        {
            return new[]
            {
                "", 
                "one",
                "two",
                "three",
                "four",
                "five",
                "six",
                "seven",
                "eight",
                "nine",
                "ten",
                "eleven",
                "twelve",
                "thirteen",
                "fourteen",
                "fifteen",
                "sixteen",
                "seventeen",
                "eighteen",
                "nineteen",
            };
        }

        protected override string[] GetFirstOrderArray()
        {
            return new[]
            {
                "twenty",
                "thirty",
                "fourty",
                "fifty",
                "sixty",
                "seventy",
                "eighty",
                "ninety"
            };
        }

        protected override string GetZero()
        {
            return "zero";
        }

        protected override string GetSecondOrder(int index, int[] arr)
        {
            Debug.Assert(2 == index % 3 & index < arr.Length, "Out of range");
            int num = arr[index];
            Debug.Assert(num >= 1 & num <= 9, "Out of range");
            return GetOneToNineteenArray()[num] + " hundred";
        }

        protected override string GetThousand(int index, int[] arr)
        {
            Debug.Assert(3 == index, "Not a thousand");
            return "thousand";
        }

        protected override string GetWhole(int whole)
        {
            return "and";
        }

        protected override string GetTenths(int fraction)
        {
            return "tenths";
        }

        protected override string GetHundredths(int fraction)
        {
            return "hundredths";
        }

        protected override string GetPercent(decimal amount)
        {
            return "percent";
        }
    }
}
