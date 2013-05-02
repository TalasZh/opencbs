using System;
using System.Collections.Generic;
using OpenCBS.CoreDomain.EconomicActivities;
using OpenCBS.Enums;

namespace OpenCBS.CoreDomain.Clients
{
    [Serializable]
    public class Corporate : Client
    {
        public Corporate() : base(new List<Project>(), OClientTypes.Corporate)
        {
            Contacts = new List<Contact>();
        }

        public List<Contact> Contacts { get; set; }
        public EconomicActivity Activity { get; set; }
        public string LegalForm { get; set; }
        public string InsertionType { get; set; }
        public string Sigle { get; set; }
        public string SmallName { get; set; }
        public int? VolunteerCount { get; set; }
        public DateTime? AgrementDate { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool? AgrementSolidarity { get; set; }
        public int? EmployeeCount { get; set; }
        public string Siret { get; set; }
        public bool IsDeleted { get; set; }
        public string Comments { get; set; }
        public string FiscalStatus { get; set; }
        public string Registre { get; set; }

        public string ImagePath { get; set; }
        public bool IsImageAdded { get; set; }
        public bool IsImageUpdated { get; set; }
        public bool IsImageDeleted { get; set; }

        public string Image2Path { get; set; }
        public bool IsImage2Added { get; set; }
        public bool IsImage2Updated { get; set; }
        public bool IsImage2Deleted { get; set; }

        public User FavouriteLoanOfficer { get; set; }
        public int? FavouriteLoanOfficerId { get; set; }        

        public override string ToString()
        {
            return Name;
        }
    }
}
