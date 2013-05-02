// LICENSE PLACEHOLDER

namespace OpenCBS.ExceptionsHandler
{
    public class ReschedulingContractClosedException : ReschedulingException
    {
        private const string _message = "Cannot reschedule closed contract.";

        public override string ToString()
        {
            return _message;
        }
    }
}
