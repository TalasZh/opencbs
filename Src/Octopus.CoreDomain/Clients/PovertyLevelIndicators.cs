using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Octopus.CoreDomain.Clients
{
    [Serializable]
    public class PovertyLevelIndicators
    {
        public int ChildrenEducation { get; set; }
        public int EconomicEducation { get; set; }
        public int SocialParticipation { get; set; }
        public int HealthSituation { get; set; }
    }
}
