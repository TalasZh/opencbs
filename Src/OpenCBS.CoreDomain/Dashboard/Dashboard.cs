
using System.Collections.Generic;
using System.Linq;

namespace OpenCBS.CoreDomain.Dashboard
{
    public class Dashboard
    {
        public List<Action> Actions { get; private set; }
        public List<ActionStat> ActionStats { get; private set; }
        public List<PortfolioLine> PortfolioLines { get; private set; }

        public Dashboard()
        {
            Actions = new List<Action>();
            ActionStats = new List<ActionStat>();
            PortfolioLines = new List<PortfolioLine>();
        }

        public decimal Olb
        {
            get { return PortfolioLines.Find(line => line.Name == "Total").Amount; }
        }

        public decimal Par
        {
            get { return PortfolioLines.FindAll(line => line.Name.StartsWith("PAR")).Sum(item => item.Amount); }
        }
    }
}
