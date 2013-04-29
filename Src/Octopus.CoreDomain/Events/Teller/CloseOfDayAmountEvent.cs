namespace Octopus.CoreDomain.Events.Teller
{
    public class CloseOfDayAmountEvent:TellerEvent
    {
        public override string Code
        {
            get { return "CDAE"; }
            set { _code = value; }
        }
    }
}
