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


namespace OpenCBS.ExceptionsHandler
{

    [Serializable]
    public class OpenCbsProjectSaveException : OpenCbsProjectException
    {
        private string code;

        protected OpenCbsProjectSaveException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            code = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", code);
            base.GetObjectData(info, context);
        }

        public OpenCbsProjectSaveException(OpenCbsProjectSaveExceptionEnum exceptionCode)
        {
            code = FindException(exceptionCode);
        }

        public override string ToString()
        {
            return code;
        }

        private string FindException(OpenCbsProjectSaveExceptionEnum exceptionId)
        {
            string returned = String.Empty;
            switch (exceptionId)
            {
                case OpenCbsProjectSaveExceptionEnum.CodeIsEmpty:
                    returned = "ProjectExceptionCodeIsNull.Text";
                    break;
                case OpenCbsProjectSaveExceptionEnum.NameIsEmpty:
                    returned = "ProjectExceptionNameIsNull.Text";
                    break;
                case OpenCbsProjectSaveExceptionEnum.AimIsEmpty:
                    returned = "ProjectExceptionAimIsNull.Text";
                    break;
                case OpenCbsProjectSaveExceptionEnum.ClientIsEmpty:
                    returned = "ProjectExceptionClientIsNull.Text";
                    break;
                case OpenCbsProjectSaveExceptionEnum.BeginDateEmpty:
                    returned = "ProjectExceptionBeginDateEmpty.Text";
                    break;
                case OpenCbsProjectSaveExceptionEnum.CAIsBad:
                    returned = "ProjectExceptionCAIsBad.Text";
                    break;
                case OpenCbsProjectSaveExceptionEnum.FinancialPlanAmountIsBad:
                    returned = "ProjectExceptionFinancialPlanAmountIsBad.Text";
                    break;
                case OpenCbsProjectSaveExceptionEnum.FinancialTotalPlanAmountIsBad:
                    returned = "ProjectExceptionFinancialTotalPlanAmountIsBad.Text";
                    break;
                case OpenCbsProjectSaveExceptionEnum.CACannotBeNullInFollowUp:
                    returned = "ProjectExceptionCACannotBeNullInFollowUp.Text";
                    break;
            }
            return returned;
        }
    }

    [Serializable]
    public enum OpenCbsProjectSaveExceptionEnum
    {
        CodeIsEmpty,
        NameIsEmpty,
        AimIsEmpty,
        ClientIsEmpty,
        BeginDateEmpty,
        CAIsBad,
        FinancialPlanAmountIsBad,
        FinancialTotalPlanAmountIsBad,
        CACannotBeNullInFollowUp
    }
}

