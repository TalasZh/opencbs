// Octopus MFS is an integrated suite for managing a Micro Finance Institution: 
// clients, contracts, accounting, reporting and risk
// Copyright © 2006,2007 OCTO Technology & OXUS Development Network
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License along
// with this program; if not, write to the Free Software Foundation, Inc.,
// 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
// Website: http://www.opencbs.com
// Contact: contact@opencbs.com

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
