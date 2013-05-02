// LICENSE PLACEHOLDER

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
