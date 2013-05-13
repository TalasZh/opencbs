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
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler
{
    /// <summary>
    /// Summary description for OpenCbsSavingException.
    /// </summary>
    [Serializable]
    public class OpenCbsTellerException : OpenCbsException
    {
        private readonly string _message;
        private OpenCbsTellerExceptionEnum _code;
        public OpenCbsTellerExceptionEnum Code { get { return _code; } }

        protected OpenCbsTellerException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _code = (OpenCbsTellerExceptionEnum)info.GetInt32("Code");
        }

        protected OpenCbsTellerException(SerializationInfo info, StreamingContext context, List<string> options)
            : base(info, context)
        {
            _code = (OpenCbsTellerExceptionEnum)info.GetInt32("Code");
            AdditionalOptions = options;
        }


        public OpenCbsTellerException()
        {
            _message = string.Empty;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", _code);
            base.GetObjectData(info, context);
        }

        public OpenCbsTellerException(OpenCbsTellerExceptionEnum exceptionCode)
        {
            _code = exceptionCode;
            _message = FindException(exceptionCode);
        }

        public OpenCbsTellerException(string message)
        {
            _message = message;
        }

        public override string ToString()
        {
            return _message;
        }

        private static string FindException(OpenCbsTellerExceptionEnum exceptionId)
        {
            string returned = String.Empty;
            switch (exceptionId)
            {
                case OpenCbsTellerExceptionEnum.NameIsEmpty:
                    returned = "TellerNameIsEmpty.Text";
                    break;
                
                case OpenCbsTellerExceptionEnum.AccountIsEmpty:
                    returned = "TellerAccountIsEmpty.Text";
                    break;

                case OpenCbsTellerExceptionEnum.BranchIsEmpty:
                    returned = "TellerBranchIsEmpty.Text";
                    break;

                case OpenCbsTellerExceptionEnum.CurrencyIsEmpty:
                    returned = "TellerCurrencyIsEmpty.Text";
                    break;

                case OpenCbsTellerExceptionEnum.UserIsEmpty:
                    returned = "TellerUserIsEmpty.Text";
                    break;

                case OpenCbsTellerExceptionEnum.NameIsExists:
                    returned = "TellerNameIsExists.Text";
                    break;

                case OpenCbsTellerExceptionEnum.MinMaxAmountIsInvalid:
                    returned = "TellerAmountMinMaxIsInvalid.Text";
                    break;

                case OpenCbsTellerExceptionEnum.VaultExists:
                    returned = "TellerVaultExists.Text";
                    break;

            }
            return returned;
        }
    }

    [Serializable]
    public enum OpenCbsTellerExceptionEnum
    {
        NameIsEmpty
        , AccountIsEmpty
        , UserIsEmpty
        , BranchIsEmpty
        , CurrencyIsEmpty
        , NameIsExists
        , MinMaxAmountIsInvalid
        , VaultExists
    }
}
