
using System.Collections.Generic;

namespace OpenCBS.CoreDomain.Dashboard
{
    public class Dashboard
    {
        public List<Action> Actions { get; private set; }
        public List<Portfolio> Portfolios { get; private set; }

        public Dashboard()
        {
            Actions = new List<Action>();
            Portfolios = new List<Portfolio>();
        }
    }
}
