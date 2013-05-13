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

using System;
using System.Globalization;
using OpenCBS.ExceptionsHandler;
using OpenCBS.Shared;

namespace OpenCBS.Services
{
    [Security()]

	/// <summary>
	/// Summary description for ServicesHelper.
	/// </summary>
    public class ServicesHelper
    {

        /// <summary>
        /// Convert a string to decimal
        /// </summary>
        /// <param name="text">string to convert</param>
        /// <returns>Value</returns>
        /// <returns>0 if exception has occured</returns>
        public static decimal ConvertStringToDecimal(string text, bool useCents)
        {
            if (!string.IsNullOrEmpty(text))
            {
                try
                {
                    return Math.Round(Convert.ToDecimal(text), useCents ? 2 : 0, MidpointRounding.AwayFromZero);
                }
                catch
                {
                    return 0;
                }
            }
            return 0;
        }

        public static decimal ConvertStringToDecimal(string text, decimal defaultValue, bool useCents)
        {
            if (!string.IsNullOrEmpty(text))
            {
                try
                {
                    //return Convert.ToDecimal(text);
                    return Math.Round(Convert.ToDecimal(text), useCents ? 2 : 0, MidpointRounding.AwayFromZero);
                }
                catch
                {
                    throw new OpenCbsRepayException(OpenCbsRepayExceptionsEnum.AmountIsNull);
                }
            }
            return defaultValue;
        }
        //add a function to convert string to OCurrency

        public static OCurrency ConvertStringToDecimal(string text, OCurrency defaultValue, bool useCents)
        {
            if (!string.IsNullOrEmpty(text))
            {
                try
                {
                    return new OCurrency(Math.Round(Convert.ToDecimal(text), useCents ? 2 : 0, MidpointRounding.AwayFromZero));
                }
                catch
                {
                    throw new OpenCbsRepayException(OpenCbsRepayExceptionsEnum.AmountIsNull);
                }
            }
            return defaultValue;
        }

        /// <summary>
        /// Convert a string to NullableDecimal
        /// </summary>
        /// <param name="text">string to convert</param>
        /// <returns>Value</returns>
        /// <returns>NullableDecimal.Null if exception has occured</returns>
        public static decimal? ConvertStringToNullableDecimal(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                try
                {
                    return Convert.ToDecimal(text);
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }
        //to add a function to transfert string to nullable ocurrency
        public static OCurrency? ConvertStringToNullableOCurrency(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                try
                {
                    return new OCurrency(Convert.ToDecimal(text));
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }
        public static decimal? ConvertStringToNullableDecimal(string text, decimal defaultValue)
        {
            if (!string.IsNullOrEmpty(text))
            {
                try
                {
                    return Convert.ToDecimal(text);
                }
                catch
                {
                    return defaultValue;
                }
            }
            return null;
        }
        // a function to convert string to OCurrency
        public static OCurrency? ConvertStringToNullableDecimal(string text, OCurrency defaultValue)
        {
            if (!string.IsNullOrEmpty(text))
            {
                try
                {
                    return  new OCurrency( Convert.ToDecimal(text));
                }
                catch
                {
                    return defaultValue;
                }
            }
            return null;
        }

        /// <summary>
        /// Convert a NullableInt32 to string
        /// </summary>
        /// <param name="number">NullableInt32 to convert</param>
        /// <returns>Value</returns>
        /// <returns>String.Empty if exception has occured</returns>
        public static string ConvertNullableInt32ToString(int? number)
        {
            if (number.HasValue)
            {
                try
                {
                    return number.Value.ToString();
                }
                catch
                {
                    return String.Empty;
                }
            }
            return String.Empty;
        }

        public static bool CheckIfValueBetweenMinAndMax(OCurrency  min, OCurrency max, OCurrency number)
        {
            bool result = false;
            if (number <= max && number >= min) result = true;
            return result;
        }

        public static bool CheckIfValueBetweenMinAndMax(double? min, double? max, double number)
        {
            bool result = false;
            if (number <= max && number >= min) result = true;
            return result;
        }

        public static bool CheckIfValueBetweenMinAndMax(double? min, double? max, double? number)
        {
            bool result = false;
            if (number <= max && number >= min) result = true;
            return result;
        }

        /// <summary>
        /// Convert a NullableDateTime to string
        /// </summary>
        /// <param name="date">NullableDateTime to convert</param>
        /// <returns>Value</returns>
        /// <returns>String.Empty if exception has occured</returns>
        public static string ConvertNullableDateTimeToString(DateTime? date)
        {
            if (date.HasValue)
            {
                try
                {
                    return date.Value.ToShortDateString();
                }
                catch
                {
                    return String.Empty;
                }
            }
            return String.Empty;
        }

        public static string ConvertNullableDoubleToString(double? number, bool isPercent)
        {
            if (number.HasValue)
            {
                try
                {
                    double value = isPercent ? (number.Value*100) : number.Value;
                    return Convert.ToString(Math.Round(value, 2, MidpointRounding.AwayFromZero));
                }
                catch
                {
                    return String.Empty;
                }
            }
            return String.Empty;
        }

        public static string ConvertNullableDecimalToString(decimal? number, bool isPercent)
        {
            if (number.HasValue)
            {
                try
                {
                    decimal value = isPercent ? (number.Value * 100) : number.Value;
                    return Convert.ToString(Math.Round(value, 2, MidpointRounding.AwayFromZero));
                }
                catch
                {
                    return String.Empty;
                }
            }
            return String.Empty;
        }

        public static string ConvertNullableDecimalToString(decimal? number)
        {
            if (number.HasValue)
            {
                try
                {
                    return (Math.Round(number.Value, 2,MidpointRounding.AwayFromZero)).ToString();
                }
                catch
                {
                    return String.Empty;
                }
            }
            return String.Empty;
        }
        //convert OCurrency to string
        public static string ConvertNullableDecimalToString(OCurrency  number)
        {
            if (number.HasValue)
            {
                try
                {
                    return number.GetFormatedValue(true);
                }
                catch
                {
                    return String.Empty;
                }
            }
            return String.Empty;
        }
        /// <summary>
        /// Convert a string to Double
        /// </summary>
        /// <param name="text">string to convert</param>
        /// <param name="isPercent"></param>
        /// <returns>Value</returns>
        /// <returns>0 if exception has occured</returns>
        public static double ConvertStringToDouble(string text, bool isPercent)
        {
            if (!string.IsNullOrEmpty(text))
            {
                try
                {
                    if (isPercent)
                        return Convert.ToDouble(text) / 100;
                    else
                        return Convert.ToDouble(text);
                }
                catch
                {
                    return 0;
                }
            }
            return 0;
        }

        public static int ConvertStringToInt32(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                try
                {
                    return Convert.ToInt32(text);
                }
                catch (Exception)
                {
                    return 0;
                }
            }
            return 0;
        }

        /// <summary>
        /// Convert a NullableInt32 to decimal
        /// </summary>
        /// <param name="number">decimal to convert</param>
        /// <returns>Value</returns>
        /// <returns>0 if exception has occured</returns>
        public static decimal ConvertNullableInt32ToDecimal(int? number)
        {
            try
            {
                return Convert.ToDecimal(number);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Convert a decimal to NullableInt32
        /// </summary>
        /// <param name="number">decimal to convert</param>
        /// <returns>Value</returns>
        /// <returns>NullableInt32.Null if exception has occured</returns>
        public static int? ConvertDecimalToNullableInt32(decimal number)
        {
            try
            {
                return Convert.ToInt32(number);
            }
            catch
            {
                return null;
            }
        }
        // add a function to convert OCurrency to nullable int32
        public static int? ConvertDecimalToNullableInt32(OCurrency number)
        {
            try
            {
                return Convert.ToInt32(number.Value);
            }
            catch
            {
                return null;
            }
        }

        public static string ConvertDecimalToString(decimal number)
        {
            try
            {
                return Convert.ToString(Math.Round(number, 2,MidpointRounding.AwayFromZero));
            }
            catch
            {
                return String.Empty;
            }
        }
        //to add a function to convert OCurrency to string
        public static string ConvertDecimalToString(OCurrency number)
        {
            try
            {
                return number.GetFormatedValue(true);
            }
            catch
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// Convert a string to NullableDouble
        /// </summary>
        /// <param name="text">string to convert</param>
        /// <returns>Value</returns>
        /// <returns>NullableDouble.Null if exception has occured</returns>
        /// <param name="isPercent"></param>
        public static double? ConvertStringToNullableDouble(string text, bool isPercent)
        {
            if (!string.IsNullOrEmpty(text))
            {
                try
                {
                    double converted = Convert.ToDouble(text);
                    if (isPercent)
                        return converted / 100;
                    return converted;
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }

        public static decimal? ConvertStringToNullableDecimal(string text, bool isPercent)
        {
            if (!string.IsNullOrEmpty(text))
            {
                try
                {
                    decimal converted = Convert.ToDecimal(text);
                    if (isPercent)
                        return converted / 100m;
                    return converted;
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }

        public static double? ConvertStringToNullableDouble(string text, bool isPercent, double defaultValue)
        {
            if (!string.IsNullOrEmpty(text))
            {
                try
                {
                    if (isPercent)
                        return Convert.ToDouble(text) / 100;
                    return Convert.ToDouble(text);
                }
                catch
                {
                    return defaultValue;
                }
            }
            return null;
        }

        public static decimal? ConvertStringToNullableDecimal(string text, bool isPercent, decimal defaultValue)
        {
            if (!string.IsNullOrEmpty(text))
            {
                try
                {
                    if (isPercent)
                        return Convert.ToDecimal(text) / 100;
                    return Convert.ToDecimal(text);
                }
                catch
                {
                    return defaultValue;
                }
            }
            return null;
        }

        /// <summary>
        /// Convert a string to NullableInt32
        /// </summary>
        /// <param name="text">string to convert</param>
        /// <returns>Value</returns>
        /// <returns>NullableInt32.Null if exception has occured</returns>
        public static int? ConvertStringToNullableInt32(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                try
                {
                    return Convert.ToInt32(text);
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }

        public static int? ConvertStringToNullableInt32(string text, int defaultValue)
        {
            if (!string.IsNullOrEmpty(text))
            {
                try
                {
                    return Convert.ToInt32(text);
                }
                catch
                {
                    return defaultValue;
                }
            }
            return null;
        }

        /// <summary>
        /// Check if number is double and positif
        /// </summary>
        /// <param name="text">string to convert</param>
        /// <returns>true/false</returns>
        public static bool CheckIfDouble(string number)
        {
            try
            {
                if (number == null) return false;

                double d = Convert.ToDouble(number);
                if (d >= 0) return true; else return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Check if number is integer and positif
        /// </summary>
        /// <param name="text">string to convert</param>
        /// <returns>true/false</returns>
        public static bool CheckIfInteger(string number)
        {
            try
            {
                if (number == null) return false;

                int i = Convert.ToInt32(number);
                if (i >= 0) return true; else return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// this methode check text in textBox
        /// </summary>
        /// <param name="text">text to check</param>
        /// <returns>text if not empty</returns>
        /// <returns>null if empty</returns>
        public static string CheckTextBoxText(string text)
        {
            return text != "" ? text : null;
        }

        ///<summary>
        ///this methode check if first param lower than second param
        ///</summary>
        ///<param name="a">first param</param>
        ///<param name="b">second param</param>
        ///<returns>true if first param lower than second</returns>
		///<returns>false if second param lower than first</returns>
        public static bool CheckIfMinLowerThanMax(int a, int b)
        {
            return a < b;
        }

        ///<summary>
        ///this methode check if first param lower than second param
        ///</summary>
        ///<param name="a">first param</param>
        ///<param name="b">second param</param>
        ///<returns>true if first param lower than second</returns>
		///<returns>false if second param lower than first</returns>
        public static bool CheckIfMinLowerThanMax(double a, double b)
        {
            return a < b;
        }

        ///<summary>
        ///this methode check if first param lower than second param
        ///</summary>
        ///<param name="a">first param</param>
        ///<param name="b">second param</param>
        ///<returns>true if first param lower than second</returns>
		///<returns>false if second param lower than first</returns>
        public static bool CheckIfMinLowerThanMax(double? a, double? b)
        {
            return a < b;
        }

        public static bool CheckIfMinLowerThanMax(DateTime a, DateTime b)
        {
            return a <= b;
        }

        /// <summary>
        /// Check If Min Max And Value correctly Filled
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="fixedValue"></param>
        /// <returns>true/false</returns>
        public static bool CheckMinMaxAndValueCorrectlyFilled(double? min, double? max, double? fixedValue)
        {
            bool returned = false;

            if (min.HasValue && max.HasValue && !fixedValue.HasValue)
                returned = CheckIfMinLowerThanMax(min, max);

            else if (!min.HasValue && !max.HasValue && fixedValue.HasValue)
                returned = true;

            return returned;
        }

        ///<summary>
        ///this methode check if first param lower than second param
        ///</summary>
        ///<param name="a">first param</param>
        ///<param name="b">second param</param>
        ///<returns>true if first param lower than second</returns>
		///<returns>false if second param lower than first</returns>
        public static bool CheckIfMinLowerThanMax( OCurrency? a, OCurrency? b)
        {
            bool result = false;
            if (a < b) 
                result=true;

            return result;
        }

        /// <summary>
        /// Check If Min Max And Value correctly Filled
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="fixedValue"></param>
        /// <returns>true/false</returns>
        public static bool CheckMinMaxAndValueCorrectlyFilled(OCurrency min, OCurrency max, OCurrency fixedValue)
        {
            bool returned = false;

            if (min.HasValue && max.HasValue&& !fixedValue.HasValue)
                returned = CheckIfMinLowerThanMax(min, max);

            else if (!min.HasValue && !max.HasValue && fixedValue.HasValue)
                returned = true;

            return returned;
        }

        /// <summary>
        /// This method checks are Min, Max and Fixed values correctly filled
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="fixedValue"></param>
        /// <returns></returns>
        public static bool CheckMinMaxAndValueCorrectlyFilled(decimal? min, decimal? max, decimal? fixedValue)
        {
            bool result = false;
            if (min != null && max != null && fixedValue == null)
            {
                result = CheckIfValueBetweenMinAndMax(min, max);
            }
            else if (min == null && max == null && fixedValue != null)
                result = true;
            return result;
        }
        /// <summary>
        /// This method checks if the 1st param lower than the 2nd one
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static bool CheckIfValueBetweenMinAndMax(decimal? min, decimal? max)
        {
            if (min < max) return true;
            return false;
        }

        /// <summary>
        /// This mothod checks if the 1-st param is lower than 2-nd one or if they are equal
        /// </summary>
        /// <param name="min">decimal min</param>
        /// <param name="max">decimal max</param>
        /// <returns></returns>
        public static bool CheckIfValueBetweenMinAndMaxOrValuesAreEqual (decimal min, decimal max)
        {
            if (min < max) return true;
            if (min == max) return true;
            return false;
        }

        ///<summary>
        ///this methode check if first param lower than second param
        ///</summary>
        ///<param name="a">first param</param>
        ///<param name="b">second param</param>
        ///<returns>true if first param lower than second</returns>
		///<returns>false if second param lower than first</returns>
        public static bool CheckIfMinLowerThanMax(int? a, int? b)
        {
            if (a < b) return true; return false;
        }

        /// <summary>
        /// Check If Min Max And Value correctly Filled
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="fixedValue"></param>
        /// <returns>true/false</returns>
        public static bool CheckMinMaxAndValueCorrectlyFilled(int? min, int? max, int? fixedValue)
        {
            bool returned = false;

            if (min.HasValue && max.HasValue && !fixedValue.HasValue)
                returned = CheckIfMinLowerThanMax(min, max);

            else if (!min.HasValue && !max.HasValue && fixedValue.HasValue)
                returned = true;

            return returned;
        }
    }
}
