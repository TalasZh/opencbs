using System;
using Microsoft.SqlServer.Server;

namespace Octopus.Stringifier
{
    public class Udf
    {
        [SqlFunction(DataAccess = DataAccessKind.Read, IsDeterministic = true, Name = "AmountToLetters")]
        public static string AmountToLetters(decimal amount, string lang)
        {
            try
            {
                StringifiableFormatInfo provider = new StringifiableFormatInfo();
                string fmt = "{0:D[" + lang + "]}";
                return string.Format(provider, fmt, Convert.ToDecimal(amount));
            }
            catch (Exception e)
            {
                return string.Format("Exception: {0}", e.Message);
            }
        }

        [SqlFunction(DataAccess = DataAccessKind.Read, IsDeterministic = true, Name = "AmountToLettersPercent")]
        public static string AmountToLettersPercent(decimal amount, string lang, bool showPercent)
        {
            try
            {
                StringifiableFormatInfo provider = new StringifiableFormatInfo();
                string fmt = "{0:D" + (showPercent ? "%" : "") + "[" + lang + "]}";
                return string.Format(provider, fmt, amount);
            }
            catch (Exception e)
            {
                return string.Format("Exception: {0}", e.Message);
            }
        }

        [SqlFunction(DataAccess = DataAccessKind.Read, IsDeterministic = true, Name = "Stringify")]
        public static string Stringify(string format, decimal amount)
        {
            try
            {
                StringifiableFormatInfo provider = new StringifiableFormatInfo();
                string fmt = "{0:" + format + "}";
                return string.Format(provider, fmt, amount);
            }
            catch (Exception ex)
            {
                return string.Format("Exception: {0}", ex.Message);
            }
        }
    }
}