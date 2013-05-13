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
using OpenCBS.Shared;

namespace OpenCBS.GUI
{
    public class DecimalValueRange
    {
        private OCurrency   _min;
        private OCurrency  _max;
        private OCurrency  _value;

        public DecimalValueRange(OCurrency  pValue)
        {
            _min = null;
            _max = null;
            _value = pValue;
        }

        public DecimalValueRange(OCurrency  pMin, OCurrency  pMax)
        {
            _min = pMin;
            _max = pMax;
            _value = null;
        }

        public OCurrency Min
        {
            get
            {
                if (_min.HasValue)
                    return Math.Round(_min.Value, 2, MidpointRounding.AwayFromZero);// (decimal)Math.Round(_min.Value, 2, MidpointRounding.AwayFromZero);
                return null;
            }
        }

        public OCurrency  Max
        {
            get
            {
                if (_max.HasValue)
                    return Math.Round(_max.Value, 2,MidpointRounding.AwayFromZero);
                return null;
            }
        }

        public OCurrency  Value
        {
            get
            {
                if (_value.HasValue)
                    return Math.Round(_value.Value, 2,MidpointRounding.AwayFromZero);
                return null;
            }
        }
    }
}
