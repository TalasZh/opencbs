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
using System.Diagnostics;

namespace OpenCBS.Stringifier
{
    public class Russian : Stringifiable
    {
        protected override string[] GetOneToNineteenArray()
        {
            return new[]
            {
                "",
                "один",
                "два",
                "три",
                "четыре",
                "пять",
                "шесть",
                "семь",
                "восемь",
                "девять",
                "десять",
                "одиннадцать",
                "двенадцать",
                "тринадцать",
                "четырнадцать",
                "пятнадцать",
                "шестнадцать",
                "семнадцать",
                "восемнадцать",
                "девятнадцать",
            };
        }

        protected override string GetZero()
        {
            return "ноль";
        }
        protected override string GetOneToNineteen(int index, int[] arr, object param)
        {
            Debug.Assert(index >= 0 & index <= arr.Length, "Out of range");
            if (1 == arr.Length & 0 == arr[index]) return GetZero();
            if (3 == index || (param != null & 0 == index%3))
            {
                if (1 == arr[index]) return "одна";
                if (2 == arr[index]) return "две";
            }
            return base.GetOneToNineteen(index, arr, param);
        }

        protected override string[] GetFirstOrderArray()
        {
            return new[]
            {
                "двадцать",
                "тридцать",
                "сорок",
                "пятьдесят",
                "шестьдесят",
                "семьдесят",
                "восемьдесят",
                "девяносто"
            };
        }

        protected override string[] GetSecondOrderArray()
        {
            return new[]
            {
                "сто",
                "двести",
                "триста",
                "четыреста",
                "пятьсот",
                "шестьсот",
                "семьсот",
                "восемьсот",
                "девятьсот"
            };
        }

        protected override string GetThousand(int index, int[] arr)
        {
            Debug.Assert(3 == index, "Not a thousand");
            
            int next = arr[index];
            if (index + 1 < arr.Length)
            {
                next += 10*arr[index + 1];
            }
            if (index + 2 < arr.Length)
            {
                next += 100*arr[index + 2];
            }

            int num = arr[index];
            int i = num % 10;
            if (next % 100 >= 11 & next % 100 <= 20)
            {
                return "тысяч";
            }
            switch (i)
            {
                case 0:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                    return "тысяч";

                case 1:
                    return "тысяча";

                case 2:
                case 3:
                case 4:
                    return "тысячи";

                default:
                    Debug.Fail("Cannot be here");
                    break;
            }
            return "тысяча";
        }

        protected override string GetMillion(int index, int[] arr)
        {
            Debug.Assert(6 == index, "Not a million");
            int next = arr[index];
            if (index + 1 < arr.Length)
            {
                next += 10 * arr[index + 1];
            }
            if (index + 2 < arr.Length)
            {
                next += 100 * arr[index + 2];
            }

            int num = arr[index];
            int i = num % 10;
            if (next % 100 >= 11 & next % 100 <= 20)
            {
                return "миллионов";
            }
            switch (i)
            {
                case 0:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                    return "миллионов";

                case 1:
                    return "миллион";

                case 2:
                case 3:
                case 4:
                    return "миллиона";

                default:
                    Debug.Fail("Cannot be here");
                    break;
            }
            return "миллион";
        }

        protected override string GetWhole(int whole)
        {
            return 1 == whole%10 & whole%100 != 11 ? "целая" : "целых";
        }

        protected override string GetTenths(int fraction)
        {
            return 1 == fraction%10 ? "десятая" : "десятых";
        }

        protected override string GetHundredths(int fraction)
        {
            return 1 == fraction%10 & fraction%100 != 11 ? "сотая" : "сотых";
        }

        protected override string GetPercent(decimal amount)
        {
            if (amount != Math.Floor(amount)) return "процента";
            int mod10 = Convert.ToInt32(amount%10);
            int mod100 = Convert.ToInt32(amount%100);
            if (1 == mod10 && mod100 != 11) return "процент";
            if (2 == mod10 || 3 == mod10 || 4 == mod10) return "процента";
            return "процентов";
        }
    }
}
