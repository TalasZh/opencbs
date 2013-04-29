namespace Octopus.CoreDomain.Events.Teller
{
    public class TellerCashOutEvent : TellerEvent
    {
        public override string Code
        {
            get { return "TCOE"; }
            set { }
        }
    }
}
