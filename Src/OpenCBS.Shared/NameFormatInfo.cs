// LICENSE PLACEHOLDER

using System;

namespace OpenCBS.Shared
{
    /// <remarks>
    /// Implement functionality for custom name formatting
    /// </remarks>
    public class NameFormatInfo : IFormatProvider, ICustomFormatter
    {
        public object GetFormat(Type formatType)
        {
            if (typeof(ICustomFormatter).Equals(formatType)) return this;
            return null;
        }

        /// <summary>
        /// Formats string according to the format specifier
        /// #: returns string as is, e.g. "BObOjAn" -> "BObOjAn"
        /// L: return lowercase string, e.g. "BObOjAn" -> "bobojan"
        /// U: return uppercase string, e.g. "BObOjAn" -> "BOBOJAN"
        /// U#: return string as is with the first letter uppercase, e.g. "boboJAN" -> "BobobJAN"
        /// UL: return lowercase string with first letter uppercase, e.g. "boboJAN" -> "Bobojan"
        /// </summary>
        /// <param name="format">Format specifier</param>
        /// <param name="arg">Name to format</param>
        /// <param name="formatProvider">Format provider</param>
        /// <returns></returns>
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (null == arg) throw new ArgumentNullException("arg");

            if (format != null && arg is string)
            {
                var s = format.Trim().ToUpper();
                var retval = arg as string;
                if ("#" == s)
                {
                    return retval;
                }
                else if ("L" == s)
                {
                    return retval.ToLower();
                }
                else if ("U" == s)
                {
                    return retval.ToUpper();
                }
                else if ("U#" == s)
                {
                    return Char.ToUpper(retval[0]) + retval.Substring(1);
                }
                else if ("UL" == s)
                {
                    return Char.ToUpper(retval[0]) + retval.Substring(1).ToLower();
                }
            }

            if (arg is IFormattable)
            {
                return ((IFormattable) arg).ToString(format, formatProvider);
            }
            else
            {
                return arg.ToString();
            }
        }
    }
}
