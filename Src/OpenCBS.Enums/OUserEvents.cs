// LICENSE PLACEHOLDER

using System;

namespace OpenCBS.Enums
{
    [Serializable]
    public static class OUserEvents
    {
        public static readonly string UserOpenTellerEvent = "UOTE"; // User Open Teller Event
        public static readonly string UserCloseTellerEvent = "UCTE"; // User Close Teller Event
        public static readonly string UserLogInEvent = "ULIE"; // User Log In Event
        public static readonly string UserLogOutEvent = "ULOE"; // User Log Out Event
        public static readonly string UserManualEntryEvent = "UMEE";
        public static readonly string UserStandardBookingEvent = "USBE";

        public static readonly string UserOpenTellerDescription = "Teller opened";
        public static readonly string UserCloseTellerDescription = "Teller closed";
        public static readonly string UserLoginDescription = "User connected";
        public static readonly string UserLogoutDescription = "User disconnected";
        public static readonly string UserManualEntryDescription = "Manual accounting entry event";
        public static readonly string UserStandardBookingDescription = "Manual standard booking event";
    }
}
