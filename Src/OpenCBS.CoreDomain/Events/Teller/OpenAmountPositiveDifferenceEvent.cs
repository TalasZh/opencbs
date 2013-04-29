namespace Octopus.CoreDomain.Events.Teller
{
    public class OpenAmountPositiveDifferenceEvent : TellerEvent
    {
        public override string Code
        {
            get { return "OPDE"; }
            set { }
        }
    }
}
