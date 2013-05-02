using System;

namespace OpenCBS.CoreDomain.Clients
{
    [Serializable]
    public class Bank
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string BIC { get; set; }
        public string IBAN1 { get; set; }
        public string IBAN2 { get; set; }
        public bool UseCustomIBAN1 { get; set; }
        public bool UseCustomIBAN2 { get; set; }
    }
}
