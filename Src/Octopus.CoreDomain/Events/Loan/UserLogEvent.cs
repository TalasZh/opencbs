using System;
using Octopus.CoreDomain.Events.Loan;

namespace Octopus.CoreDomain.Events
{
    [Serializable]
    public class UserLogEvent: Event
    {
        public UserLogEvent ()
        {}

        public UserLogEvent(string code)
        {
            this.code = code;
            description = code == "ULIE" ? "User connected" : "User disconnected";
        }

        private string description;
        private string code;
        public override string Code
        {
            get { return code; }
            set { code = value; }
        }

        public override string Description
        {
            get { return description; }
            set { description=value; }
        }
    }
}
