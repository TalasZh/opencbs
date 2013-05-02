// LICENSE PLACEHOLDER

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
