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
using System.Text;
using System.Runtime.Serialization;

namespace OpenCBS.ExceptionsHandler.Exceptions.ExportExceptions
{
    [Serializable]
    public class OpenCbsCustomExportException : OpenCbsException
    {
        private readonly string _message;
        private OpenCbsCustomExportExceptionEnum _code;

        public OpenCbsCustomExportExceptionEnum Code
        { get { return _code; } }

        protected OpenCbsCustomExportException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _message = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", _code);
            base.GetObjectData(info, context);
        }

        public OpenCbsCustomExportException()
        {
            _message = string.Empty;
        }

        public OpenCbsCustomExportException(OpenCbsCustomExportExceptionEnum exceptionCode)
        {
            _code = exceptionCode;
            _message = _findException(exceptionCode);
        }

        public OpenCbsCustomExportException(string message)
        {
            _message = message;
        }

        public override string ToString()
        {
            return _message;
        }

        private string _findException(OpenCbsCustomExportExceptionEnum exceptionCode)
        {
            switch (exceptionCode)
            {
                case OpenCbsCustomExportExceptionEnum.FileNameIsEmpty: 
                    return "CustomExportFileNameIsEmpty.Text";
                case OpenCbsCustomExportExceptionEnum.FileExtensionIsIncorrect:
                    return "CustomExportFileExtensionIsIncorrect.Text";
                case OpenCbsCustomExportExceptionEnum.SomeRequiredFieldsAreMissing:
                    return "CustomExportSomeRequiredFieldsAreMissing.Text";
                default: return string.Empty;
            }
        }
    }

    [Serializable]
    public enum OpenCbsCustomExportExceptionEnum
    {
        FileNameIsEmpty,
        FileExtensionIsIncorrect,
        SomeRequiredFieldsAreMissing
    }
}
