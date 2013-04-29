namespace Octopus.CoreDomain.Events.Teller
{
    public class CloseAmountPositiveDifferenceEvent:TellerEvent
    {
        public override string Code
        {
            get { return "CPDE"; }
            set {}
        }
    }
}
