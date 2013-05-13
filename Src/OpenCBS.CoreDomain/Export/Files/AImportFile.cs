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
using OpenCBS.CoreDomain.Export.Fields;
using OpenCBS.CoreDomain.Export.FieldType;
using System.IO;

namespace OpenCBS.CoreDomain.Export.Files
{
    [Serializable]
    public abstract class AImportFile<T> : IFile where T : new()
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
            get { return false; }
        }

        #endregion

        public List<T> GetDataFromFile(string pFilePath)
        {
            List<T> data = new List<T>();

            FileStream fs = new FileStream(pFilePath, FileMode.Open);
            StreamReader sr = new StreamReader(fs);

            if (DisplayHeader)
                sr.ReadLine();

            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine();
                data.Add(_getDataFromLine(line));
            }

            sr.Close();
            fs.Close();

            return data;
        }

        public static List<string> SplitLine(char pFieldsDelimiter, char pEncloseChar, string pLine)
        {
            List<string> fields = new List<string>();

            bool isInString = false;
            int lastSplit = 0;

            for (int i = 0; i < pLine.Length; i++)
            {
                if (pLine[i] == pEncloseChar)
                    isInString = !isInString;

                if (pLine[i] == pFieldsDelimiter && !isInString)
                {
                    fields.Add(pLine.Substring(lastSplit, i - lastSplit));
                    lastSplit = i + 1;
                }
            }

            fields.Add(pLine.Substring(lastSplit, pLine.Length - lastSplit));

            return fields;
        }

        public List<string> SplitLine(string pLine)
        {
            List<string> lineData;

            if (HasFieldsDelimiter)
                if (HasStringEncloseChar)
                    lineData = AImportFile<T>.SplitLine(FieldsDelimiter, EncloseChar, pLine);
                else
                    lineData = pLine.Split(FieldsDelimiter).ToList();
            else
            {
                lineData = new List<string>();
                foreach (Field field in SelectedFields)
                    lineData.Add(pLine.Substring(0, field.Length.Value));
            }

            return lineData;
        }

        protected abstract void _setValueFromField(Field pField, T pT, string pValue);

        protected T _getDataFromLine(string pLine) 
        {
            List<string> lineData = SplitLine(pLine);
            T data = new T();
            for (int i = 0; i < SelectedFields.Count; i++)
            {
                var value = lineData[i];
                if (HasFieldsDelimiter)
                    value = value.Replace(EncloseChar.ToString(), "");

                _setValueFromField(SelectedFields[i] as Field, data, value);
            }

            return data;
        }
    }
}
