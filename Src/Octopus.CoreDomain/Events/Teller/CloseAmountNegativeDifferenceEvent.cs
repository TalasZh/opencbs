namespace Octopus.CoreDomain.Events.Teller
{
    public class CloseAmountNegativeDifferenceEvent:TellerEvent
    {
        public override string Code
        {
            get {return "CNDE";}
            set {}
        }
    }
}
