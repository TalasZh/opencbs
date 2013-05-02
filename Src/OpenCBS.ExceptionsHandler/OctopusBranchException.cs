// LICENSE PLACEHOLDER

namespace OpenCBS.ExceptionsHandler
{
    public class OctopusBranchSameNameException : OctopusException
    {
        public override string ToString()
        {
            return "BranchSameNameException";
        }
    }

    public class OctopusBranchSameCodeException : OctopusException
    {
        public override string ToString()
        {
            return "BranchSameCodeException";
        }
    }

    public class OctopusBranchNameIsEmptyException : OctopusException
    {
        public override string ToString()
        {
            return "BranchNameIsEmpty.Text";
        }
    }

    public class OctopusBranchCodeIsEmptyException : OctopusException
    {
        public override string ToString()
        {
            return "BranchCodeIsEmpty.Text";
        }
    }

    public class OctopusBranchAddressIsEmptyException : OctopusException
    {
        public override string ToString()
        {
            return "BranchAddressIsEmpty.Text";
        }
    }
}
