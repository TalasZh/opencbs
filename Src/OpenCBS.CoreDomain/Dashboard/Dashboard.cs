
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
        public decimal Par31To60 { get; set; }
        public decimal Par61To90 { get; set; }
        public decimal Par91To180 { get; set; }
        public decimal Par181To365 { get; set; }
        public decimal Par365 { get; set; }
        public decimal Par
        {
            get { return Par1To30 + Par31To60 + Par61To90 + Par91To180 + Par181To365 + Par365; }
        }
    }
}
