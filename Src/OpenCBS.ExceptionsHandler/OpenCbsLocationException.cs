// LICENSE PLACEHOLDER

namespace OpenCBS.ExceptionsHandler
{
    public class OpenCbsLocationException : OpenCbsException
    {
        public OpenCbsLocationException(string message) : base(message) {            
        }
    }

    public class OpenCbsEmptyLocationException : OpenCbsLocationException
    {
        public OpenCbsEmptyLocationException() : base("OpenCbsEmptyLocationException.Text") { }
    }

    public class OpenCbsDistrictUsedException : OpenCbsLocationException
    {
        public OpenCbsDistrictUsedException() : base("OpenCbsDistrictUsedException.Text") { }
    }

    public class OpenCbsCityUsedException : OpenCbsLocationException
    {
        public OpenCbsCityUsedException() : base("OpenCbsCityUsedException.Text") { }
    }
}
