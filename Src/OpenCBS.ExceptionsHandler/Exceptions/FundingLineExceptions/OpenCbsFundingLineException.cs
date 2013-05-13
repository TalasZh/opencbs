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
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler.Exceptions.FundingLineExceptions
{

    [Serializable]
    public class OpenCbsFundingLineException : OpenCbsException
    {
         private string _code;
         public OpenCbsFundingLineException(OpenCbsFundingLineExceptionEnum exception)
        {
            _code = _FindException(exception);
        }

        public override string ToString()
        {
            return _code;
        }

        protected OpenCbsFundingLineException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _code = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", _code);
            base.GetObjectData(info, context);
        }

        private string _FindException(OpenCbsFundingLineExceptionEnum exception)
        {
            string returned = String.Empty;
            switch (exception)
            {
                case OpenCbsFundingLineExceptionEnum.CodeIsEmpty:
                    returned = "OpenCbsFundingLineExceptionCodeIsEmpty.Text";
                    break;
                case OpenCbsFundingLineExceptionEnum.NameIsEmpty:
                    returned = "OpenCbsFundingLineExceptionNameIsEmpty.Text";
                    break;
                case OpenCbsFundingLineExceptionEnum.BeginDateGreaterEndDate:
                    returned = "OpenCbsFundingLineExceptionBeginDateGreaterEndDate.Text";
                    break;
                case OpenCbsFundingLineExceptionEnum.BadFundingLineID:
                    returned = "OpenCbsFundingLineExceptionBadFundingLineId.Text";
                    break;
                case OpenCbsFundingLineExceptionEnum.FundingLineNameExists:
                    returned = "OpenCbsFundingLineExceptionFundingLineNameExists.Text";
                    break;
                case OpenCbsFundingLineExceptionEnum.CurrencyIsEmpty:
                    returned = "CurrencyIsEmpty.Text";
                    break;
            }
            return returned;
        }
    }


    [Serializable]
    public enum OpenCbsFundingLineExceptionEnum
    {
        CodeIsEmpty,
        NameIsEmpty,
        BeginDateGreaterEndDate,
        BadFundingLineID,
        FundingLineNameExists,
        CurrencyIsEmpty
    }


}
