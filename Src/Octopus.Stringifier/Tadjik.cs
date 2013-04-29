using System.Diagnostics;

namespace Octopus.Stringifier
{
    public class Tadjik : Stringifiable
    {
        protected override string[] GetOneToNineteenArray()
        {
            return new[]
            {
                "",
                "як",
                "ду",
                "се",
                "чор",
                "панx",
                "шаш",
                "[афт",
                "[ашт",
                "ну[",
                "да[",
                "ёзда[",
                "дувозда[",
                "сензда[",
                "чорда[",
                "понзда[",
                "шонзда[",
                "[абда[",
                "[ажда[",
                "нузда["
            };
        }

        protected override string[] GetFirstOrderArray()
        {
            return new[]
            {
                "бист",
                "си",
                "чил",
                "панxо[",
                "шаст",
                "[афтод",
                "[аштод",
                "навад"
            };
        }

        protected override string[] GetSecondOrderArray()
        {
            return new[]
            {
                "сад",
                "дусад",
                "сесад",
                "чорсад",
                "панxсад",
                "шашсад",
                "[афтсад",
                "[аштсад",
                "ну[сад"
            };
        }

        protected override string GetFirstOrder(int index, int[] arr)
        {
            Debug.Assert(index >= 1 & index < arr.Length, "Out of range");
            string retval = base.GetFirstOrder(index, arr);
            int prev = arr[index - 1];
            if (0 == prev) return retval;
            return 3 == arr[index] ? retval + "ю" : retval + "у";
        }

        protected override string GetSecondOrder(int index, int[] arr)
        {
            Debug.Assert(index >= 2 & index < arr.Length, "Out of range");
            int num = arr[index];
            Debug.Assert(num >= 1 & num <= 9, "Out of range");
            int prev = 10*arr[index - 1] + arr[index - 2];
            string retval = GetSecondOrderArray()[num - 1];
            if (0 == prev) return retval;
            return 1 == num ? "як " + retval + "у" : retval + "у";
        }

        protected override string GetThousand(int index, int[] arr)
        {
            Debug.Assert(3 == index, "Not a thousand");
            int prev = 100*arr[index - 1] + 10*arr[index - 2] + arr[index - 3];
            return 0 == prev ? "[азор" : "[азору";
        }

        protected override string GetZero()
        {
            return "сифр";
        }

        protected override string GetWhole(int whole)
        {
            return "бутуну";
        }

        protected override string GetTenths(int fraction)
        {
            return "аз да[";
        }

        protected override string GetHundredths(int fraction)
        {
            return "аз сад";
        }

        protected override bool IsFractionToLeft()
        {
            return true;
        }

        protected override string GetPercent(decimal amount)
        {
            return "фоиз";
        }
    }
}