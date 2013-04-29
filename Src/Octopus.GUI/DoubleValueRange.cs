using System;

namespace Octopus.GUI
{
    public class DoubleValueRange
    {
        private double? _min;
        private double? _max;
        private double? _value;

        public DoubleValueRange(double? pValue)
        {
            _min = null;
            _max = null;
            _value = pValue;
        }

        public DoubleValueRange(double? pMin, double? pMax)
        {
            _min = pMin;
            _max = pMax;
            _value = null;
        }

        public double? Min
        {
            get
            {
                if (_min.HasValue)
                    return Math.Round(_min.Value, 4);
                return null;
            }
        }

        public double? Max
        {
            get
            {
                if (_max.HasValue)
                    return Math.Round(_max.Value, 4);
                return null;
            }
        }

        public double? Value
        {
            get
            {
                if (_value.HasValue)
                    return Math.Round(_value.Value, 4);
                return null;
            }
        }
    }
}
