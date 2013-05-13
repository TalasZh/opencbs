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
