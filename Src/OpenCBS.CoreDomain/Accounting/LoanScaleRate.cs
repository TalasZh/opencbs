using System;
namespace OpenCBS.CoreDomain.Accounting
{
    [Serializable]
    public class LoanScaleRate
    {
        public int ScaleMin { get; set; }
        public int ScaleMax { get; set; }
        public int Number { get; set; }
        public LoanScaleRate() { }
        public LoanScaleRate(int number, int scalemin, int scalemax)
        {
            Number = number;
            ScaleMin = scalemin;
            ScaleMax = scalemax;

        }
    }

}