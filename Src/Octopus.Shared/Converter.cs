using System;
using System.Globalization;

namespace Octopus.Shared
{
    public class Converter
    {
        public const string CustomFieldDateFormat = "yyyy-MM-dd";


        public static DateTime CustomFieldValueToDate(string value)
        {            
            if (string.IsNullOrEmpty(value)) return DateTime.MinValue;
            DateTime dateTime;
            if (!DateTime.TryParseExact(value, CustomFieldDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out dateTime))
                dateTime = DateTime.Now.Date;
            return dateTime;
        }

        public static string CustomFieldDateToString(DateTime dateValue)
        {
            return dateValue.ToString(CustomFieldDateFormat, CultureInfo.InvariantCulture);
        }

        public static bool CustomFieldDecimalParse(out decimal result, string value)
        {
            NumberFormatInfo formatInfo = GetCustomFieldNumberFormatInfo();
            return decimal.TryParse(value, NumberStyles.AllowDecimalPoint, formatInfo, out result);
        }

        private static NumberFormatInfo GetCustomFieldNumberFormatInfo()
        {
            return new NumberFormatInfo {NumberDecimalSeparator = ","};
        }

        public static decimal CustomFieldValueToDecimal(string value)
        {
            decimal result;
            return !CustomFieldDecimalParse(out result, value) ? decimal.Zero : result;
        }

        public static string CustomFieldDecimalToString(decimal decimalValue)
        {
            NumberFormatInfo formatInfo = GetCustomFieldNumberFormatInfo();
            return decimalValue.ToString("G", formatInfo);
        }
    }
}
