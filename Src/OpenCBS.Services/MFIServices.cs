// LICENSE PLACEHOLDER

using System;
using OpenCBS.CoreDomain;
using OpenCBS.Manager;
using OpenCBS.ExceptionsHandler;

namespace OpenCBS.Services
{
    public class MFIServices : MarshalByRefObject
    {
        private readonly MFIManager _MFIManager;

        public MFIServices(User pUser)
        {
            _MFIManager = new MFIManager(pUser);
        }

        public MFIServices(User pUser, string testDB)
        {
            _MFIManager = new MFIManager(testDB, pUser);
        }

        public MFIServices(MFIManager pMFIManager)
        {
            _MFIManager = pMFIManager;
        }

        public MFI FindMFI()
        {
            return _MFIManager.SelectMFI();
        }

        public bool UpdateMFI(MFI pMFI)
        {
            if (pMFI.Name == String.Empty)
                throw new OpenCbsMfiExceptions(OpenCbsMFIExceptionEnum.NameIsEmpty);
            
            if (pMFI.Login == String.Empty)
                throw new OpenCbsMfiExceptions(OpenCbsMFIExceptionEnum.LoginIsNotFilled);

            if (pMFI.Password == String.Empty)
                throw new OpenCbsMfiExceptions(OpenCbsMFIExceptionEnum.PasswordIsNotFilled);

           return _MFIManager.UpdateMFI(pMFI);            
        }

        public bool CreateMFI(MFI pMFI)
        {
            if (pMFI.Name == String.Empty)
                throw new OpenCbsMfiExceptions(OpenCbsMFIExceptionEnum.NameIsEmpty);

            if (pMFI.Login == String.Empty)
                throw new OpenCbsMfiExceptions(OpenCbsMFIExceptionEnum.LoginIsNotFilled);

            if (pMFI.Password == String.Empty)
                throw new OpenCbsMfiExceptions(OpenCbsMFIExceptionEnum.PasswordIsNotFilled);

            return _MFIManager.CreateMFI(pMFI);           
        }

        public bool CheckIfSamePassword(string pMdp1, string pMdp2)
        {
            if (pMdp1 != pMdp2)
                throw new OpenCbsMfiExceptions(OpenCbsMFIExceptionEnum.DifferentPassword);

            return true;
        }
    }
}
