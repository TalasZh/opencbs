using System.Diagnostics;

namespace OpenCBS.Stringifier
{
    public class Spanish : Stringifiable
    {
        protected override string[] GetOneToNineteenArray()
        {
            return new[]
                       {
                           "",
                           "un(o",
                           "dos",
                           "tres",
                           "cuatro",
                           "cinco",
                           "seis",
                           "siete",
                           "ocho",
                           "nueve",
                           "dieze",
                           "once",
                           "doce",
                           "trece",
                           "catorce",
                           "quince",
                           "dieciseis",
                           "diecisiete",
                           "dieciocho",
                           "diecinueve"
                       };


        }
        protected override string[] GetFirstOrderArray()
        {
            return new[]
            {
                "viente",
                "treinta y",
                "cuarenta y",
                "cincuaenta y",
                "sesenta y",
                "setenta y",
                "ochenta y",
                "noventa y"
            };
        }

        protected override string[] GetSecondOrderArray()
        {
            return new[]
            {
                " ciento",
                " doscientos",
                " trescientos",
                " cuatrocientos",
                " quinientos",
                " seiscientos",
                " setecientos",
                " ochocientos",
                " novecientos"
            };
        }

        protected override string GetOneToNineteen(int index, int[] arr, object param)
        {
            Debug.Assert(index >= 0 & index <= arr.Length, "Out of range");
            if (1 == arr.Length & 0 == arr[index]) return GetZero();
            if (3 == index || (param != null & 0 == index % 3))
            {
                if (1 == arr[index]) return "un";
            }
            return base.GetOneToNineteen(index, arr, param);
        }

        protected override string GetZero()
        {
            return "cero";
        }

        protected override string GetThousand(int index, int[] arr)
        {
            Debug.Assert(3 == index, "Not a thousand");
            return " mil";
        }
    }
}

