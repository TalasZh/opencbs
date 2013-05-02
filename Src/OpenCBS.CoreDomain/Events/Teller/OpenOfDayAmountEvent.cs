namespace OpenCBS.CoreDomain.Events.Teller
{
    public class OpenOfDayAmountEvent:TellerEvent
    {
        public override string Code
        {
            get { return "ODAE"; }
            set { _code = value; }
        }
    }
}
