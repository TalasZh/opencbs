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

	public class AmountComparer
	{
		private static readonly decimal DELTA = 0.02m;
        private static readonly OCurrency OcurrencyDelta = 0.0045m;

		static public decimal Delta
		{
			get
			{
				return DELTA;
			}
		}
        static public OCurrency OcurrencyDELTA
        {
            get { return OcurrencyDelta; }
        }
		/// <returns>true if a1 > a2</returns>
		static public bool Equals(decimal a1, decimal a2)
		{
			return Math.Abs(a1 - a2) < DELTA ? true : false;
		}

        static public bool Equals(OCurrency a1, OCurrency a2)
        {
            return Math.Abs(a1.Value - a2.Value) < OcurrencyDelta.Value ? true : false;
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="a1"></param>
		/// <param name="a2"></param>
		/// <returns>a negative value if a1 lower than a2, 0 if a1 and a2 are equals, a positive value if a1 greater than a2</returns>
		static public int Compare(decimal a1, decimal a2)
		{
			if ( a1 - a2 > DELTA) return 1;
			else if (a2 - a1 > DELTA) return -1;
			else return 0;
		}

        static public int Compare(decimal a1, decimal a2, int installmentNumber)
        {
            if (a1 - a2 > (DELTA*installmentNumber)) return 1;
            else if (a2 - a1 > (DELTA * installmentNumber)) return -1;
            else return 0;
            
        }
        static public int Compare(OCurrency o1, OCurrency o2)
        {
            if (o1 - o2 > OcurrencyDelta) return 1;
            else if (o2 - o1 > OcurrencyDelta) return -1;
            else return 0;
        }

        static public int Compare(OCurrency o1, OCurrency o2, int installmentNumber)
        {
            if (o1 - o2 > (OcurrencyDelta*(double)installmentNumber) )return 1;
            else if (o2 - o1 > (OcurrencyDelta * (double)installmentNumber)) return -1;
            else return 0;
        }
	}
}
