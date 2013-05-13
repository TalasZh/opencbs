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

using System.Globalization;
using NUnit.Framework;
using OpenCBS.Shared;

namespace OpenCBS.Test.Shared
{
    [TestFixture]
    public class TestOCurrency
    {
        [Test]
        public void OCurrency_Add_TwoPositivesNumbers()
        {
            OCurrency currencyA = 11.11104m;
            OCurrency currencyB = 22.22205m;

            OCurrency currencyC = currencyA + currencyB;
            Assert.AreEqual(33.3331m, currencyC.Value);
        }

        [Test]
        public void OCurrency_Add_TwoNegativesNumbers()
        {
            OCurrency currencyA = -11.11111m;
            OCurrency currencyB = -22.22222m;

            OCurrency currencyC = currencyA + currencyB;
            Assert.AreEqual(-33.3333m, currencyC.Value);
        }

        [Test]
        public void OCurrency_Add_TwoNumbers_OnePositive_OneNegtive()
        {
            OCurrency ocurrrencyA = 11.1111m;
            OCurrency ocurrencyB = -22.22225m;

            OCurrency c = ocurrrencyA + ocurrencyB;
            Assert.AreEqual(-11.1112m, c.Value);
        }
        [Test]
        public void OCurrency_Sub_TwoPositiveNumbers()
        {
            OCurrency ocurrencyA = 11.111115m;
            OCurrency ocurrencyB = 22.22222m;
            OCurrency occurencyC = ocurrencyA - ocurrencyB;

            Assert.AreEqual(-11.1111m, occurencyC.Value);
        }
        [Test]
        public void OCurrency_Sub_Test_TwoNegativeNumbers()
        {
            OCurrency occurencyA = -11.1111m;
            OCurrency occurencyB = -22.22225m;
            OCurrency occurencyC = occurencyA - occurencyB;
            Assert.AreEqual(11.1112m, occurencyC.Value);
        }
        [Test]
        public void OCurrency_SUb_Test_OnePositive_OneNagative()
        {
            OCurrency ocurrencyA = -11.1111m;
            OCurrency ocurrrencyB = 22.22224m;
            OCurrency ocurrencyC = ocurrencyA - ocurrrencyB;
            Assert.AreEqual(-33.3333m, ocurrencyC.Value);
        }
        [Test]
        public void OCurrency_Multi_TwoPositiveNumbers()
        {
            OCurrency ocurrencyA = 11.1111m;
            OCurrency ocurrencyB = 11.2222m;
            OCurrency occurencyC = ocurrencyA * ocurrencyB;

            Assert.AreEqual(124.6910m, occurencyC.Value);
        }
        [Test]
        public void OCurrency_Multi_Test_TwoNegativeNumbers()
        {
            OCurrency occurencyA = -11.11115m;
            OCurrency occurencyB = -22.2222m;
            OCurrency occurencyC = occurencyA * occurencyB;
            Assert.AreEqual(246.9153m, occurencyC.Value);
        }
        [Test]
        public void OCurrency_Multi_Test_OnePositiveOCurrency_OneSamllPositiveDouble()
        {
            OCurrency occurencyA = 11.1111m;
            double b = 3.24;
            OCurrency occurencyC = occurencyA * b;
            Assert.AreEqual(36.0000m, occurencyC.Value);
        }
        [Test]
        public void OCurrency_Multi_Test_OnePositiveOCurrency_OneLargerPositiveDouble()
        {
            OCurrency occurencyA = 11.1111m;
            double b = 3.76;
            OCurrency occurencyC = occurencyA * b;
            Assert.AreEqual(41.7777m, occurencyC.Value);
        }
        [Test]
        public void OCurrency_Multi_Test_OnePositiveOCurrency_OneLargerPositiveDouble2()
        {
            OCurrency occurencyA = 11.1111m;
            double b = 3.765;
            OCurrency occurencyC = occurencyA * b;
            Assert.AreEqual(41.8333m, occurencyC.Value);
        }
        [Test]
        public void OCurrency_Multi_Test_OnePositiveOCurrency_OneLargerNegativeDouble()
        {
            OCurrency occurencyA = 11.1111m;
            double b = -3.24;
            OCurrency occurencyC = occurencyA * b;
            Assert.AreEqual(-36.0000m, occurencyC.Value);
        }
        [Test]
        public void OCurrency_Multi_Test_OnePositiveOCurrency_OneSmallerNegativeDouble()
        {
            OCurrency occurencyA = 11.1111m;
            double b = -3.76;
            OCurrency occurencyC = occurencyA * b;
            Assert.AreEqual(-41.7777m, occurencyC.Value);
        }
        [Test]
        public void OCurrency_Multi_Test_OneNegativeOCurrency_OneSmallPositiveDouble()
        {
            OCurrency occurencyA = -11.1111m;
            double b = 3.24;
            OCurrency occurencyC = occurencyA * b;
            Assert.AreEqual(-36.0000m, occurencyC.Value);
        }
        [Test]
        public void OCurrency_Multi_Test_OneNegativeOCurrency_OneLargerPositiveDouble()
        {
            OCurrency occurencyA = -11.1111m;
            double b = 3.76;
            OCurrency occurencyC = occurencyA * b;
            Assert.AreEqual(-41.7777m, occurencyC.Value);
        }
        [Test]
        public void OCurrency_Multi_Test_OneNegativeOCurrency_OneLargerNegativetiveDouble()
        {
            OCurrency occurencyA = -11.1111m;
            double b = -3.76;
            OCurrency occurencyC = occurencyA * b;
            Assert.AreEqual(41.7777m, occurencyC.Value);
        }
        [Test]
        public void OCurrency_Multi_Test_OneNegativeOCurrency_OneSmallerNegativeDouble()
        {
            OCurrency occurencyA = -11.1111m;
            double b = -3.24;
            OCurrency occurencyC = occurencyA * b;
            Assert.AreEqual(36.0000m, occurencyC.Value);
        }
        [Test]
        public void OCurrency_Multi_Test_OnePositive_OneNagative()
        {
            OCurrency ocurrencyA = -11.1111m;
            OCurrency ocurrrencyB = 22.2222m;
            OCurrency ocurrencyC = ocurrencyA * ocurrrencyB;
            Assert.AreEqual(-246.9131m, ocurrencyC.Value);
        }
        [Test]
        public void OCurrency_Div_TwoPositiveNumbers()
        {
            OCurrency ocurrencyA = 11.1111m;
            OCurrency ocurrencyB = 22.2222m;
            OCurrency occurencyC = ocurrencyA / ocurrencyB;

            Assert.AreEqual(0.5000m, occurencyC.Value);
        }
        [Test]
        public void OCurrency_Div_Test_TwoNegativeNumbers()
        {
            OCurrency occurencyA = -11.1111m;
            OCurrency occurencyB = -22.2222m;
            OCurrency occurencyC = occurencyA / occurencyB;
            Assert.AreEqual(0.5000m, occurencyC.Value);
        }
        [Test]
        public void OCurrency_Div_Test_OnePositive_OneNagative()
        {
            OCurrency ocurrencyA = -11.1111m;
            OCurrency ocurrrencyB = 22.2222m;
            OCurrency ocurrencyC = ocurrencyA / ocurrrencyB;
            Assert.AreEqual(-0.5000m, ocurrencyC.Value);
        }

        [Test]
        public void Ocurrency_CompareBigger_Test_TwoPositiveNumbers()
        {
            OCurrency ocurrencyA = 31.1111m;
            OCurrency ocurrencyB = 22.2222m;

            bool resultat = ocurrencyA > ocurrencyB;
            Assert.AreEqual(true, resultat);
        }
        [Test]
        public void Ocurrency_CompareBigger_Test_TwoNegativetiveNumbers()
        {
            OCurrency ocurrencyA = -11.1111m;
            OCurrency ocurrencyB = -22.2222m;

            bool resultat = ocurrencyA > ocurrencyB;
            Assert.AreEqual(true, resultat);
        }
        [Test]
        public void Ocurrency_CompareBigger_Test_OnePositive_OneNegativeNumbers()
        {
            OCurrency ocurrencyA = 11.1111m;
            OCurrency ocurrencyB = -22.2222m;

            bool resultat = ocurrencyA > ocurrencyB;
            Assert.AreEqual(true, resultat);
        }
        [Test]
        public void Ocurrency_CompareSmaller_Test_TwoPositiveNumbers()
        {
            OCurrency ocurrencyA = 11.1111m;
            OCurrency ocurrencyB = 22.2222m;

            bool resultat = ocurrencyA < ocurrencyB;
            Assert.AreEqual(true, resultat);
        }
        [Test]
        public void Ocurrency_CompareSmallerr_Test_TwoNegativetiveNumbers()
        {
            OCurrency ocurrencyA = -22.2222m;
            OCurrency ocurrencyB = -11.1111m;

            bool resultat = ocurrencyA < ocurrencyB;
            Assert.AreEqual(true, resultat);
        }
        [Test]
        public void Ocurrency_CompareSmaller_Test_OnePositive_OneNegativeNumbers()
        {
            OCurrency ocurrencyA = -11.1111m;
            OCurrency ocurrencyB = 22.2222m;

            bool resultat = ocurrencyA < ocurrencyB;
            Assert.AreEqual(true, resultat);
        }

        [Test]
        public void Ocurrency_CompareNotEqual_Test_TwoPositiveNumbers()
        {
            OCurrency ocurrencyA = 11.1111m;
            OCurrency ocurrencyB = 10.1111m;

            bool resultat = ocurrencyA != ocurrencyB;
            Assert.AreEqual(true, resultat);
        }
        [Test]
        public void Ocurrency_CompareNotEqual_Test_TwoNegativetiveNumbers()
        {
            OCurrency ocurrencyA = -11.1111m;
            OCurrency ocurrencyB = -22.2222m;

            bool resultat = ocurrencyA != ocurrencyB;
            Assert.AreEqual(true, resultat);
        }
        [Test]
        public void Ocurrency_CompareNotEqual_Test_OnePositive_OneNegativeNumbers()
        {
            OCurrency ocurrencyA = 12.1111m;
            OCurrency ocurrencyB = -9.9999m;

            bool resultat = ocurrencyA != ocurrencyB;
            Assert.AreEqual(true, resultat);
        }
        [Test]
        public void Ocurrency_CompareEqual_Test_TwoPositiveNumbers()
        {
            OCurrency ocurrencyA = 11.1111m;
            OCurrency ocurrencyB = 11.1111m;

            bool resultat = ocurrencyA == ocurrencyB;
            Assert.AreEqual(true, resultat);
        }
        [Test]
        public void Ocurrency_CompareEqual_Test_TwoNegativetiveNumbers()
        {
            OCurrency ocurrencyA = -11.1111m;
            OCurrency ocurrencyB = -11.1111m;

            bool resultat = ocurrencyA == ocurrencyB;
            Assert.AreEqual(true, resultat);
        }
        [Test]
        public void Ocurrency_CompareEquals_Test_OnePositive_OneNegativeNumbers()
        {
            OCurrency ocurrencyA = 11.1111m;
            OCurrency ocurrencyB = -11.1111m;

            bool resultat = ocurrencyA == ocurrencyB;
            Assert.AreEqual(false, resultat);
        }
        [Test]
        public void Ocurrency_CompareLargerEqual_Test_TwoPositiveNumbers()
        {
            OCurrency ocurrencyA = 11.1111m;
            OCurrency ocurrencyB = 9.9999m;
            bool res = ocurrencyA >= ocurrencyB;
            Assert.AreEqual(true, res);
        }
        [Test]
        public void Ocurrency_CompareLargerEqual_Test_TwoNegativeNumbers()
        {
            OCurrency ocurrencyA = -111.1111m;
            OCurrency ocurrencyB = -112.1111m;
            bool res = ocurrencyA >= ocurrencyB;
            Assert.AreEqual(true, res);
        }
        [Test]
        public void Ocurrency_CompareLargerEqual_Test_OnePositive_OneNegativeNumber()
        {
            OCurrency ocurrencyA = 11.1234m;
            OCurrency ocurrencyB = -11.1234m;
            bool res = ocurrencyA >= ocurrencyB;
            Assert.AreEqual(true, res);
        }
        [Test]
        public void Ocurrency_CompareSmallerEqual_Test_TwoPositiveNumbers()
        {
            OCurrency ocurrencyA = 10.1111m;
            OCurrency ocurrencyB = 11.1111m;
            bool res = ocurrencyA <= ocurrencyB;
            Assert.AreEqual(true, res);
        }
        [Test]
        public void Ocurrency_CompareSmallerEqual_Test_TwoNegativeNumbers()
        {
            OCurrency ocurrencyA = -10.1111m;
            OCurrency ocurrencyB = -9.9999m;
            bool res = ocurrencyA <= ocurrencyB;
            Assert.AreEqual(true, res);
        }
        [Test]
        public void Ocurrency_CompareSmallerEqual_Test_OnePositive_OneNegativeNumber()
        {
            OCurrency ocurrencyA = -10.1111m;
            OCurrency ocurrencyB = 9.9999m;
            bool res = ocurrencyA <= ocurrencyB;
            Assert.AreEqual(true, res);
        }

        [Test]
        public void OCurrency_Test_ToString_PositiveNumbers()
        {
            OCurrency ocurrencyA = 4.4444m;
            Assert.AreEqual(4.4444.ToString(), ocurrencyA.ToString());
        }

        [Test]
        public void OCurrency_Test_ToString_NegativeNumbers()
        {
            OCurrency ocurrencyA = -4.4444m;
            Assert.AreEqual((-4.4444).ToString(), ocurrencyA.ToString());
        }

        [Test]
        public void OCurrency_Equals_Ocurrency_String()
        {
            OCurrency x = 1000;
            decimal y = 1000.00m;
            Assert.AreEqual(x,y);
        }

        [Test]
        public void OCurrency_Test_Ocurrency_Object_Deux_NonNull_Egality()
        {
            OCurrency ocurrencyA = 100;
            object ocurrencyB = 100;
            Assert.AreEqual(ocurrencyA.Value, ocurrencyB);
        }

        [Test]
        public void OCurrency_Test_Ocurrency_Object_Deux_Null_Egality()
        {
            OCurrency ocurrencyA = null;
            Assert.IsTrue(!ocurrencyA.HasValue);
        }
    }
}
