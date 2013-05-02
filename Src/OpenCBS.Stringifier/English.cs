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