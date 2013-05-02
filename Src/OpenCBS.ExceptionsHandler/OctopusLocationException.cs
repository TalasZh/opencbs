namespace OpenCBS.ExceptionsHandler
{
    public class OctopusLocationException : OctopusException
    {
        public OctopusLocationException(string message) : base(message) {            
        }
    }

    public class OctopusEmptyLocationException : OctopusLocationException
    {
        public OctopusEmptyLocationException() : base("OctopusEmptyLocationException.Text") { }
    }

    public class OctopusDistrictUsedException : OctopusLocationException
    {
        public OctopusDistrictUsedException() : base("OctopusDistrictUsedException.Text") { }
    }

    public class OctopusCityUsedException : OctopusLocationException
    {
        public OctopusCityUsedException() : base("OctopusCityUsedException.Text") { }
    }
}
