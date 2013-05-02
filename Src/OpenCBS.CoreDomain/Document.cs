using System;

namespace OpenCBS.CoreDomain
{
    public class Document
    {
        public int Id { get; set; }
        public int ClientId { get; set;}
        public string DocumentName { get; set; }
        public string FileName { get; set; }
        public bool Deleted { get; set; }
        public DateTime Date { get; set; }
        public string User { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; }
    }
}
