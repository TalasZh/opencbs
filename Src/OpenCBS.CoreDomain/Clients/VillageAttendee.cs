// LICENSE PLACEHOLDER

using System;

namespace OpenCBS.CoreDomain.Clients
{
    [Serializable]
    public class VillageAttendee
    {
        public int Id { get; set; }
        public int VillageId { get; set; }
        public int TiersId { get; set; }
        public string PersonName { get; set; }
        public DateTime AttendedDate { get; set; }
        public bool Attended { get; set; }
        public string Comment { get; set; }
        public int LoanId { get; set; }
    }
}
