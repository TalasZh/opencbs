using System;

namespace Octopus.Shared
{
    public class Utils
    {
        public static string[] Splitter(string chaine)
        {   
            string delimeter = ",;=% ";
            char[] separateur = delimeter.ToCharArray();
            return  chaine.Split(separateur);
        }
    }

    public class DoubleValueRange
    {
        private double? _min;
        private double? _max;
        private double? _value;
        private int _precision = 4;

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

        public int precision
        {
            get { return _precision; }
            set { _precision = value; }
        }

        public double? Min
        {
            get
            {
                if (_min.HasValue)
                    return Math.Round(_min.Value, _precision);
                return null;
            }
        }

        public double? Max
        {
            get
            {
                if (_max.HasValue)
                    return Math.Round(_max.Value, _precision);
                return null;
            }
        }

        public double? Value
        {
            get
            {
                if (_value.HasValue)
                    return Math.Round(_value.Value, _precision);
                return null;
            }
        }
    }


}


        



    
