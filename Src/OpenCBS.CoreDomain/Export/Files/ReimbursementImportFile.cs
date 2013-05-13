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
using System.Linq;
using System.Text;
using System.IO;
using OpenCBS.Shared;
using OpenCBS.CoreDomain.Export.Fields;
using OpenCBS.CoreDomain.Export.FieldType;

namespace OpenCBS.CoreDomain.Export.Files
{
    [Serializable]
    public class ReimbursementImportFile : AImportFile<Installment>
    {
        public ReimbursementImportFile()
        {
            HasFieldsSpecificLength = true;
            Extension = ".txt";
            SelectedFields = new List<IField>();
        }

        public override List<IField> DefaultList
        {
            get
            {
                return new List<IField>
                {   
                    new Field { Name = "ContractCode", FieldType = new StringFieldType(), DefaultLength = 20, IsRequired = true },
                    new Field { Name = "InstallmentNumber", FieldType = new IntegerFieldType(), DefaultLength = 2, IsRequired = true},
                    new Field { Name = "RepaymentStatus", FieldType = new IntegerFieldType(), DefaultLength = 1, IsRequired = true},
                    new Field { Name = "RepaymentAmount", FieldType = new DecimalFieldType(), DefaultLength = 10, IsRequired = true},
                    new Field { Name = "RepaymentDate", FieldType = new DateFieldType(), DefaultLength = 6, IsRequired = true}
                };
            }
        }

        protected override void _setValueFromField(Field pField, Installment pInstallment, string pValue)
        {
            switch (pField.Name)
            {
                case "ContractCode": pInstallment.ContractCode = (string)pField.FieldType.Parse(pValue); break;
                case "InstallmentNumber": pInstallment.InstallmentNumber = (int)pField.FieldType.Parse(pValue); break;
                case "RepaymentAmount": pInstallment.InstallmentAmount = (decimal)pField.FieldType.Parse(pValue); break;
                case "RepaymentDate": pInstallment.InstallmentDate = (DateTime)pField.FieldType.Parse(pValue); break;
                case "RepaymentStatus": pInstallment.RepaymentStatus = (int)pField.FieldType.Parse(pValue); break;
            }
        }
    }
}
