using System;
using System.Text.RegularExpressions;

namespace Octopus.Stringifier
{
    public class StringifiableFormatInfo : IFormatProvider, ICustomFormatter
    {
        public object GetFormat(Type formatType)
        {
            return (typeof (ICustomFormatter).Equals(formatType)) ? this : null;
        }
    
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (null == arg) throw new ArgumentNullException("arg");

            if (format != null && arg is decimal)
            {
                const string re = @"^(N|D)(%?)(\[[a-z]{2}\])$";
                Match match = Regex.Match(format, re);
                if (!match.Success) throw new Exception("Invalid format");

                string lang = match.Groups[3].ToString();
                lang = lang.ToLower();
                lang = lang.Trim(new[] {'[', ']'});

                IStringifiable iface = GetStringifiable(lang);
                if (null == iface) throw new Exception("Language not implemented");

                decimal amount = Convert.ToDecimal(arg);
                if ("N" == match.Groups[1].ToString())
                {
                    amount = Math.Floor(amount);
                }

                return "%" == match.Groups[2].ToString()
                           ? iface.StringifyWithPercent(amount)
                           : iface.Stringify(amount);
            }

            if (arg is IFormattable)
            {
                return ((IFormattable) arg).ToString(format, formatProvider);
            }

            return arg.ToString();
        }

        private static IStringifiable GetStringifiable(string lang)
        {
            switch (lang)
            {
                case "en":
                    return new English();

                case "ru":
                    return new Russian();

                case "fr":
                    return new French();

                case "tj":
                    return new Tadjik();

                case "es":
                    return new Spanish();

                case "mr":
                    return new Marathi();

                default:
                    return null;
            }
        }
    }
}
