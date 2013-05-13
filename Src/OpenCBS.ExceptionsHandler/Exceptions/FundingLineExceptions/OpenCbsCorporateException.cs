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
    public class OpenCbsCorporateException : OpenCbsException
    {
        private string _code;
        public OpenCbsCorporateException(OpenCbsCorporateExceptionEnum exception)
        {
            _code = _FindException(exception);
        }

        public override string ToString()
        {
            return _code;
        }

        protected OpenCbsCorporateException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _code = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", _code);
            base.GetObjectData(info, context);
        }

        private string _FindException(OpenCbsCorporateExceptionEnum exception)
        {
            string returned = String.Empty;
            switch (exception)
            {
                case OpenCbsCorporateExceptionEnum.CodeIsEmpty:
                    returned = "OpenCbsCorporateExceptionCodeIsEmpty.Text";
                    break;

                case OpenCbsCorporateExceptionEnum.NameIsEmpty:
                    returned = "OpenCbsCorporateExceptionNameIsEmpty.Text";
                    break;
                case OpenCbsCorporateExceptionEnum.CityIsEmpty:
                    returned = "OpenCbsCorporateExceptionCityIsEmpty.Text";
                    break;
                case OpenCbsCorporateExceptionEnum.ProvinceIsEmpty:
                    returned = "OpenCbsCorporateExceptionProvinceIsEmpty.Text";
                    break;
                case OpenCbsCorporateExceptionEnum.DistrictIsEmpty:
                    returned = "OpenCbsCorporateExceptionDistrictIsEmpty.Text";
                    break;
                case OpenCbsCorporateExceptionEnum.EmployeeIsFalseFormat:
                    returned = "OpenCbsCorporateExceptionEmployeeIsFalseFormat.Text";
                    break;
                case OpenCbsCorporateExceptionEnum.VolunteerIsFalseFormat:
                    returned = "OpenCbsCorporateExceptionVolunteerIsFalseFormat.Text";
                    break;

                case OpenCbsCorporateExceptionEnum.BranchIsEmpty:
                    returned = "BranchIsEmpty";
                    break;
            }
            return returned;
        }


       
    }

    [Serializable]
    public enum OpenCbsCorporateExceptionEnum
        {
            CodeIsEmpty,
            NameIsEmpty,
            CityIsEmpty,
            DistrictIsEmpty,
            ProvinceIsEmpty,
            VolunteerIsFalseFormat,
            EmployeeIsFalseFormat,
            BranchIsEmpty
        }

}
