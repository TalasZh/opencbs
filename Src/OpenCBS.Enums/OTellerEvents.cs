using System;

namespace OpenCBS.Enums
{
    [Serializable]
    public static class OTellerEvents
    {
        public const string CashIn = "TCIE"; // Teller Cash In Event
        public const string CashOut = "TCOE"; // Teller Cash Out Event
        public const string OpenDay = "ODAE"; // Teller Open
        public const string CloseDay = "CDAE"; // Teller Close
        public const string OpenDayPositiveDifference = "OPDE"; // Open Day Positive Difference
        public const string OpenDayNegativeDifference = "ONDE"; // Open Day Negative Difference
        public const string CloseDayPositiveDifference = "CDPD"; // Close Day Positive Difference
        public const string CloseDayNegativeDifference = "CDND"; // Close Day Negative Difference
    }
}
