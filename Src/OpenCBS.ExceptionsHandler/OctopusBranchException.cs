// LICENSE PLACEHOLDER

namespace OpenCBS.ExceptionsHandler
{
    public class OpenCbsBranchSameNameException : OpenCbsException
    {
        public override string ToString()
        {
            return "BranchSameNameException";
        }
    }

    public class OpenCbsBranchSameCodeException : OpenCbsException
    {
        public override string ToString()
        {
            return "BranchSameCodeException";
        }
    }

    public class OpenCbsBranchNameIsEmptyException : OpenCbsException
    {
        public override string ToString()
        {
            return "BranchNameIsEmpty.Text";
        }
    }

    public class OpenCbsBranchCodeIsEmptyException : OpenCbsException
    {
        public override string ToString()
        {
            return "BranchCodeIsEmpty.Text";
        }
    }

    public class OpenCbsBranchAddressIsEmptyException : OpenCbsException
    {
        public override string ToString()
        {
            return "BranchAddressIsEmpty.Text";
        }
    }
}
