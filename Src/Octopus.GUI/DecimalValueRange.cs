using System;
using Octopus.Shared;

namespace Octopus.GUI
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
