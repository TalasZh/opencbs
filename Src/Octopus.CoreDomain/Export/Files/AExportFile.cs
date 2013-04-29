using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Octopus.CoreDomain.Export.Fields;
using Octopus.CoreDomain.Export.FieldType;
using System.IO;

namespace Octopus.CoreDomain.Export.Files
{
    [Serializable]
    public abstract class AExportFile<T> : IFile where T : new()
    {
        #region IFile Members

        public abstract List<IField> DefaultList
        {
            get; 
        }

        public List<IField> SelectedFields
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public bool HasFieldsDelimiter
        {
            get;
            set;
        }

        public char FieldsDelimiter
        {
            get;
            set;
        }

        public bool HasFieldsSpecificLength
        {
            get;
            set;
        }

        public bool HasStringEncloseChar
        {
            get;
            set;
        }

        public char EncloseChar
        {
            get;
            set;
        }

        public string Extension
        {
            get;
            set;
        }

        public bool DisplayHeader
        {
            get;
            set;
        }

        public bool IsExportFile
        {
            get { return true; }
        }

        #endregion

        public List<string> GetFormatedRowData(T pRowData)
        {
            List<string> formatedRowData = new List<string>();

            foreach (IField field in SelectedFields)
            {
                if (field is Field)
                {
                    if (HasStringEncloseChar && (((Field)field).FieldType is StringFieldType))
                        formatedRowData.Add(EncloseChar + _getFormatedField(pRowData, field as Field) + EncloseChar);
                    else
                        formatedRowData.Add(_getFormatedField(pRowData, field as Field));
                }
                else if (field is CustomField)
                {
                    if (HasStringEncloseChar)
                        formatedRowData.Add(EncloseChar + ((CustomField)field).Format() + EncloseChar);
                    else
                        formatedRowData.Add(((CustomField)field).Format());
                }
                else if (field is ComplexField)
                {
                    if (HasStringEncloseChar)
                        formatedRowData.Add(EncloseChar + _getFormatedComplexField(pRowData, field as ComplexField) + EncloseChar);
                    else
                        formatedRowData.Add(_getFormatedComplexField(pRowData, field as ComplexField));
                }
            }

            return formatedRowData;
        }

        protected string _getFormatedComplexField(T pRowData, ComplexField pField)
        {
            string formatedString = "";

            for (int i = 0; i < pField.Fields.Count; i++)
            {
                if (pField.Fields[i] is Field)
                    formatedString += _getFormatedField(pRowData, pField.Fields[i] as Field);
                else if (pField.Fields[i] is CustomField)
                    formatedString += ((CustomField)pField.Fields[i]).Format();
                else if (pField.Fields[i] is ComplexField)
                    formatedString += _getFormatedComplexField(pRowData, pField.Fields[i] as ComplexField);

                if (i < pField.Fields.Count - 1)
                    formatedString += " ";
            }

            return formatedString;
        }

        public void ExportData (string pFilePath, List<T> pData)
        {
            FileStream fs = new FileStream(pFilePath, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);

            string delimiter = HasFieldsDelimiter ? FieldsDelimiter.ToString() : "";

            if (DisplayHeader)
                sw.WriteLine(string.Join(delimiter, GetFormatedRowHeader().ToArray()));

            foreach (var installment in pData)
            {
                sw.WriteLine(string.Join(delimiter, GetFormatedRowData(installment).ToArray()));
            }

            sw.Flush();
            fs.Close();
        }

        public List<string> GetFormatedRowHeader()
        {
            List<string> formatedRowHeader = new List<string>();
            foreach (IField field in SelectedFields)
            {
                var header = field.Header;
                if (field.Length.HasValue)
                    header = StringFieldType.Format(header, field.Length.Value, false);

                if (HasStringEncloseChar)
                    formatedRowHeader.Add(EncloseChar + header + EncloseChar);
                else
                    formatedRowHeader.Add(EncloseChar + header + EncloseChar);
            }

            return formatedRowHeader;
        }

        protected abstract string _getFormatedField(T pRowData, Field pField);
    }
}
