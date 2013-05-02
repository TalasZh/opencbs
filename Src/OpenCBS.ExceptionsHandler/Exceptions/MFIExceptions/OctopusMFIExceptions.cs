using System;

namespace OpenCBS.ExceptionsHandler
{
    [Serializable]
    public class OctopusMFIExceptions : OctopusException
    {
        private string code;

        public OctopusMFIExceptions(OctopusMFIExceptionEnum exceptionCode)
		{
			this.code = this.FindException(exceptionCode);
            Console.WriteLine("EXCEPTION MFI EXCEPTION: {0}",code);
		}

        public override string ToString()
        {
            return code;
        }

        private string FindException(OctopusMFIExceptionEnum exceptionId)
        {
            string returned = String.Empty;
            switch (exceptionId)
            {
                case OctopusMFIExceptionEnum.NameIsEmpty:
                    returned = "OME1.Text";
                    break;

                case OctopusMFIExceptionEnum.PasswordIsNotFilled:
                    returned = "OME3.Text";
                    break;

                case OctopusMFIExceptionEnum.LoginIsNotFilled:
                    returned = "OME2.Text";
                    break;

                case OctopusMFIExceptionEnum.DifferentPassword:
                    returned = "OME4.Text";
                    break;
            }
            return returned;
        }
    }

    [Serializable]
    public enum OctopusMFIExceptionEnum
    {
        NameIsEmpty,
        PasswordIsNotFilled,
        LoginIsNotFilled,
        DifferentPassword
    }
}
