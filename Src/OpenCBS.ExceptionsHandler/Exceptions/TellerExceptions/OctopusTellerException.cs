//Octopus MFS is an integrated suite for managing a Micro Finance Institution: clients, contracts, accounting, reporting and risk
//Copyright © 2006,2007 OCTO Technology & OXUS Development Network
//
//This program is free software; you can redistribute it and/or modify
//it under the terms of the GNU Lesser General Public License as published by
//the Free Software Foundation; either version 2 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU Lesser General Public License for more details.
//
//You should have received a copy of the GNU Lesser General Public License along
//with this program; if not, write to the Free Software Foundation, Inc.,
//51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
//
// Licence : http://www.octopusnetwork.org/OverviewLicence.aspx
//
// Website : http://www.octopusnetwork.org
// Business contact: business(at)octopusnetwork.org
// Technical contact email : tech(at)octopusnetwork.org 

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler
{
    /// <summary>
    /// Summary description for OctopusSavingException.
    /// </summary>
    [Serializable]
    public class OctopusTellerException : OctopusException
    {
        private readonly string _message;
        private OctopusTellerExceptionEnum _code;
        public OctopusTellerExceptionEnum Code { get { return _code; } }

        protected OctopusTellerException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _code = (OctopusTellerExceptionEnum)info.GetInt32("Code");
        }

        protected OctopusTellerException(SerializationInfo info, StreamingContext context, List<string> options)
            : base(info, context)
        {
            _code = (OctopusTellerExceptionEnum)info.GetInt32("Code");
            AdditionalOptions = options;
        }


        public OctopusTellerException()
        {
            _message = string.Empty;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", _code);
            base.GetObjectData(info, context);
        }

        public OctopusTellerException(OctopusTellerExceptionEnum exceptionCode)
        {
            _code = exceptionCode;
            _message = FindException(exceptionCode);
        }

        public OctopusTellerException(string message)
        {
            _message = message;
        }

        public override string ToString()
        {
            return _message;
        }

        private static string FindException(OctopusTellerExceptionEnum exceptionId)
        {
            string returned = String.Empty;
            switch (exceptionId)
            {
                case OctopusTellerExceptionEnum.NameIsEmpty:
                    returned = "TellerNameIsEmpty.Text";
                    break;
                
                case OctopusTellerExceptionEnum.AccountIsEmpty:
                    returned = "TellerAccountIsEmpty.Text";
                    break;

                case OctopusTellerExceptionEnum.BranchIsEmpty:
                    returned = "TellerBranchIsEmpty.Text";
                    break;

                case OctopusTellerExceptionEnum.CurrencyIsEmpty:
                    returned = "TellerCurrencyIsEmpty.Text";
                    break;

                case OctopusTellerExceptionEnum.UserIsEmpty:
                    returned = "TellerUserIsEmpty.Text";
                    break;

                case OctopusTellerExceptionEnum.NameIsExists:
                    returned = "TellerNameIsExists.Text";
                    break;

                case OctopusTellerExceptionEnum.MinMaxAmountIsInvalid:
                    returned = "TellerAmountMinMaxIsInvalid.Text";
                    break;

                case OctopusTellerExceptionEnum.VaultExists:
                    returned = "TellerVaultExists.Text";
                    break;

            }
            return returned;
        }
    }

    [Serializable]
    public enum OctopusTellerExceptionEnum
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
