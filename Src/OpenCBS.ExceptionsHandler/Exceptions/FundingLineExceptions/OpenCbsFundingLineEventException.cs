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
   public class OpenCbsFundingLineEventException : OpenCbsException
 
   {
      private string _code;
        public OpenCbsFundingLineEventException(OpenCbsFundingLineEventExceptionEnum exception)
        {
            _code = _FindException(exception);
        }

        public override string ToString()
        {
            return _code;
        }

        protected OpenCbsFundingLineEventException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _code = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", _code);
            base.GetObjectData(info, context);
        }

        private string _FindException(OpenCbsFundingLineEventExceptionEnum exception)
        {
            string returned = String.Empty;
            switch (exception)
            {
                case OpenCbsFundingLineEventExceptionEnum.CodeIsEmpty:
                    returned = "OpenCbsFundingLineEventExceptionCodeIsEmpty.Text";
                    break;

                case OpenCbsFundingLineEventExceptionEnum.NameIsEmpty:
                    returned = "OpenCbsFundingLineEventExceptionNameIsEmpty.Text";
                    break;


                case OpenCbsFundingLineEventExceptionEnum.AmountIsEmpty:
                    returned = "OpenCbsFundingLineEventExceptionAmountIsEmpty.Text";
                    break;

                case OpenCbsFundingLineEventExceptionEnum.AmountIsLessZero:
                    returned = "OpenCbsFundingLineEventExceptionAmountIsLessZero.Text";
                    break;
                case OpenCbsFundingLineEventExceptionEnum.DirectionIsEmpty:
                    returned = "OpenCbsFundingLineEventExceptionDirectionIsEmpty.Text";
                    break;
                case OpenCbsFundingLineEventExceptionEnum.AmountIsTaller:
                    returned = "OpenCbsFundingLineEventExceptionAmountIsTaller.Text";
                    break;

                case OpenCbsFundingLineEventExceptionEnum.AmountIsBigger:
                    returned = "OpenCbsFundingLineEventExceptionAmountIsBigger.Text";
                    break;
                case OpenCbsFundingLineEventExceptionEnum.CommitmentFinancialIsNotEnough:
                    returned = "OpenCbsFundingLineEventExceptionCommitmentFinancialIsNotEnough.Text";
                    break;

                case OpenCbsFundingLineEventExceptionEnum.AmountIsNonCompliant:
                    returned = "OpenCbsFundingLineEventExceptionAmountIsNonCompliant.Text";
                    break;
            }
            return returned;
        }


       
    }

    [Serializable]
     public enum OpenCbsFundingLineEventExceptionEnum
        {
            CodeIsEmpty,
            NameIsEmpty,
            AmountIsEmpty,
            AmountIsLessZero,
            DirectionIsEmpty,
            AmountIsTaller,
            CommitmentFinancialIsNotEnough,
            AmountIsNonCompliant,
            AmountIsBigger
        }
   }
