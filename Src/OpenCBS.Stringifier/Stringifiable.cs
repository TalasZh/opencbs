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
using System.Diagnostics;
using System.Text;

namespace OpenCBS.Stringifier
{
    public interface IStringifiable
    {
        string Stringify(decimal amount);
        string StringifyWithPercent(decimal amount);
    }

    public abstract class Stringifiable : IStringifiable
    {
        protected virtual string[] GetOneToNineteenArray()
        {
            return new string[20];
        }

        protected virtual string[] GetFirstOrderArray()
        {
            return new string[8];
        }

        protected virtual string[] GetSecondOrderArray()
        {
            return new string[9];
        }

        protected virtual string GetZero()
        {
            return string.Empty;
        }

        protected virtual string GetOneToNineteen(int index, int[] arr, object param)
        {
            Debug.Assert(index >= 0 & index < arr.Length, "Out of range");
            if (1 == arr.Length & 0 == arr[index]) return GetZero();
            int i = index % 3;
            int num = 1 == i ? 10 + arr[index - 1] : arr[index];
            return GetOneToNineteenArray()[num];
        }

        protected virtual string GetFirstOrder(int index, int[] arr)
        {
            Debug.Assert(index >= 1 & index < arr.Length, "Out of range");
            int num = arr[index];
            return GetFirstOrderArray()[num - 2];
        }

        protected virtual string GetSecondOrder(int index, int[] arr)
        {
            Debug.Assert(index >= 2 & index < arr.Length, "Out of range");
            int num = arr[index];
            return GetSecondOrderArray()[num - 1];
        }

        protected abstract string GetThousand(int index, int[] arr);
        protected virtual string GetMillion(int index, int[] arr)
        {
            return string.Empty;
        }

        protected virtual string BeforeReturn(string value)
        {
            return value.Replace("  ", " ");
        }

        public string Stringify(int amount)
        {
            return Stringify(amount, null);
        }

        protected bool HasThousands(string amount)
        {
            if (amount.Length <= 3) return false;
            var arr = amount.ToCharArray();
            Array.Reverse(arr);
            amount = new string(arr);
            var upper = Math.Min(5, amount.Length - 1);
            return Convert.ToInt32(amount.Substring(3, upper - 2)) > 0;
        }

        protected string Stringify(int amount, object param)
        {
            string input = amount.ToString();
            int[] arr = new int[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                arr[i] = Int32.Parse(input[i].ToString());
            }
            Array.Reverse(arr);

            List<string> retval = new List<string>();
            for (int i = 0; i < arr.Length; i++)
            {
                int i3 = i % 3;

                if (3 == i && HasThousands(input))
                {
                    retval.Add(GetThousand(i, arr));
                }
                if (6 == i)
                {
                    retval.Add(GetMillion(i, arr));
                }

                switch (i3)
                {
                    case 0:
                        bool gotoNext = false;
                        if (i + 1 < arr.Length)
                        {
                            gotoNext = 1 == arr[i + 1];
                        }
                        if (gotoNext) continue;
                        retval.Add(GetOneToNineteen(i, arr, param));
                        break;

                    case 1:
                        if (0 == arr[i]) continue;

                        if (1 == arr[i])
                        {
                            retval.Add(GetOneToNineteen(i, arr, param));
                        }
                        else
                        {
                            retval.Add(GetFirstOrder(i, arr));
                        }
                        break;

                    case 2:
                        if (0 == arr[i]) continue;

                        retval.Add(GetSecondOrder(i, arr));
                        break;

                    default:
                        Debug.Fail("Cannot be here");
                        break;
                }
            }

            retval.Reverse();
            return BeforeReturn(string.Join(" ", retval.ToArray()));
        }

        protected virtual string GetWhole(int whole)
        {
            return string.Empty;
        }

        protected virtual string GetTenths(int fraction)
        {
            return string.Empty;
        }

        protected virtual string GetHundredths(int fraction)
        {
            return string.Empty;
        }

        protected virtual string GetPercent(decimal amount)
        {
            return string.Empty;
        }

        protected virtual bool IsFractionToLeft()
        {
            return false;
        }

        public string StringifyWithPercent(decimal amount)
        {
            return Stringify(amount) + " " + GetPercent(amount);
        }

        public string Stringify(decimal amount)
        {
            amount = Math.Round(amount, 2);
            int whole = Convert.ToInt32(Math.Floor(amount));
            int fraction = Convert.ToInt32((amount - whole)*100);
            fraction = 0 == fraction%10 ? fraction/10 : fraction;

            if (0 == fraction) return Stringify(whole);

            // Find out the number of fractional places
            string s = amount.ToString("N2");
            s = s.Split(new[] {'.', ','})[1];
            s = s.TrimEnd(new[] {'0'});

            StringBuilder sb = new StringBuilder(128);
            sb.Append(Stringify(whole, true));
            sb.Append(" ");
            sb.Append(GetWhole(whole));
            sb.Append(" ");
            if (!IsFractionToLeft())
            {
                sb.Append(Stringify(fraction, true));
                sb.Append(" ");
            }

            switch (s.Length)
            {
                case 1:
                    sb.Append(GetTenths(fraction));
                    break;

                case 2:
                    sb.Append(GetHundredths(fraction));
                    break;

                default:
                    Debug.Fail("Cannot be here");
                    break;
            }

            if (IsFractionToLeft())
            {
                sb.Append(" ");
                sb.Append(Stringify(fraction, true));
            }

            return sb.ToString();
        }
    }
}
