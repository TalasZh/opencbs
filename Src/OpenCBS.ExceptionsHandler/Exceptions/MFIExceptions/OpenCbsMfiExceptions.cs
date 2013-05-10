// LICENSE PLACEHOLDER

using System;

namespace OpenCBS.ExceptionsHandler
{
    [Serializable]
    public class OpenCbsMfiExceptions : OpenCbsException
    {
        private string code;

        public OpenCbsMfiExceptions(OpenCbsMFIExceptionEnum exceptionCode)
		{
			this.code = this.FindException(exceptionCode);
            Console.WriteLine("EXCEPTION MFI EXCEPTION: {0}",code);
		}

        public override string ToString()
        {
            return code;
        }

        private string FindException(OpenCbsMFIExceptionEnum exceptionId)
        {
            string returned = String.Empty;
            switch (exceptionId)
            {
                case OpenCbsMFIExceptionEnum.NameIsEmpty:
                    returned = "OME1.Text";
                    break;

                case OpenCbsMFIExceptionEnum.PasswordIsNotFilled:
                    returned = "OME3.Text";
                    break;

                case OpenCbsMFIExceptionEnum.LoginIsNotFilled:
                    returned = "OME2.Text";
                    break;

                case OpenCbsMFIExceptionEnum.DifferentPassword:
                    returned = "OME4.Text";
                    break;
            }
            return returned;
        }
    }

    [Serializable]
    public enum OpenCbsMFIExceptionEnum
    {
        NameIsEmpty,
        PasswordIsNotFilled,
        LoginIsNotFilled,
        DifferentPassword
    }
}
