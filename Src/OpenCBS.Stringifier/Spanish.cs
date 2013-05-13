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

