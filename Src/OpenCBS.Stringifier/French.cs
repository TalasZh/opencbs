// LICENSE PLACEHOLDER

using System.Diagnostics;

namespace OpenCBS.Stringifier
{
    public class French : Stringifiable
    {
        protected override string[] GetOneToNineteenArray()
        {
            return new[]
            {
                "",
                "un", 
                "deux", 
                "trois", 
                "quatre", 
                "cinq", 
                "six", 
                "sept", 
                "huit", 
                "neuf",
                "dix", 
                "onze", 
                "douze", 
                "treize", 
                "quatorze", 
                "quinze",
                "seize", 
                "dix-sept", 
                "dix-huit", 
                "dix-neuf"
            };
        }

        protected override string[] GetFirstOrderArray()
        {
            return new[]
            {
                "vingt",
                "trente",
                "quarante",
                "cinquante",
                "soixante",
                "soixante",
                "quatre-vingt",
                "quatre-vingt"
            };
        }

        protected override string GetZero()
        {
            return "zéro";
        }

        protected override string GetOneToNineteen(int index, int[] arr, object param)
        {
            Debug.Assert(index >=0 & index < arr.Length, "Out of range");

            if (1 == arr.Length & 0 == arr[index]) return GetZero();

            int i = index%3;
            if (1 == i) return base.GetOneToNineteen(index, arr, param);
            
            Debug.Assert(0 == i, "Invalid index");

            int next = 0;
            Debug.Assert(next >= 0 & next <= 9, "Invalid next number");
            if (index + 1 < arr.Length)
            {
                next = arr[index + 1];
            }

            int num = arr[index];
            Debug.Assert(num >= 0 & num <= 9, "Invalid number");
            switch (next)
            {
                case 0:
                case 1:
                    return base.GetOneToNineteen(index, arr, param);

                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                    string prefix = 1 == num ? " et " : "-";
                    prefix = 0 == num ? "" : prefix;
                    return prefix + GetOneToNineteenArray()[num];

                case 7:
                case 9:
                    num += 10;
                    return "-" + GetOneToNineteenArray()[num];

                case 8:
                    if (num > 0)
                    {
                        return "-" + GetOneToNineteenArray()[num];
                    }
                    return string.Empty;

                default:
                    Debug.Fail("Cannot be here");
                    return string.Empty;
            }
        }

        protected override string GetFirstOrder(int index, int[] arr)
        {
            Debug.Assert(1 == index % 3 & index < arr.Length, "Invalid index");
            int num = arr[index]*10 + arr[index - 1];
            if (80 == num) return GetFirstOrderArray()[6] + "s";
            return base.GetFirstOrder(index, arr);
        }

        protected override string GetSecondOrder(int index, int[] arr)
        {
            Debug.Assert(2 == index % 3 & index < arr.Length, "Out of range");
            int num = arr[index];
            Debug.Assert(num >= 1 & num <= 9, "Out of range");
            return GetOneToNineteenArray()[num] + " cent";
        }

        protected override string GetThousand(int index, int[] arr)
        {
            return "mille";
        }

        protected override string BeforeReturn(string value)
        {
            value = base.BeforeReturn(value);
            return value.Replace(" -", "-");
        }
    }
}
