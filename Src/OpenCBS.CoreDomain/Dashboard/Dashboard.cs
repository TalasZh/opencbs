
using System.Collections.Generic;

namespace OpenCBS.CoreDomain.Dashboard
{
    public class Dashboard
    {
        public List<Action> Actions { get; private set; }
        public List<ActionStat> ActionStats { get; private set; }

        public Dashboard()
        {
            Actions = new List<Action>();
            ActionStats = new List<ActionStat>();
        }

        public decimal Olb { get; set; }
        public decimal Par1To30 { get; set; }
        public decimal Par30 { get; set; }
        public decimal Par
        {
            get { return Par1To30 + Par30; }
        }
    }
}
