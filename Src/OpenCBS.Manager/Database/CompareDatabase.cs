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
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Xml;
using OpenCBS.Shared;
using OpenCBS.Shared.Settings;

namespace OpenCBS.Manager.Database
{
    /// <summary>
    /// Summary description for CompareDatabase.
    /// </summary>
    public static class CompareDatabase
    {
        private const string SCHEMA_FILE_NAME = "OCTOPUS_Data_Schema_({0}).xml";
        private const string LOCAL_SCHEMA_FILE_NAME = "OCTOPUS_Local_Data_Schema_({0}).xml";
        /// <summary>
        /// Create at "SetupPath" a OCTOPUS_('version').xml who contains diagram of the current database
        /// </summary>
        public static void SaveDatabaseDiagramsInXml(bool pDestination, string pDatabaseName, SqlConnection pSqlConnection)
        {
            string sql = string.Format(@"USE [{0}] SELECT table_name, column_name, data_type, is_nullable 
			                FROM information_schema.columns WHERE(table_name IN (SELECT table_name FROM Information_Schema.Tables 
		                    WHERE Table_Type = 'Base Table')) ORDER BY table_name", pDatabaseName);
            OpenCbsCommand command = new OpenCbsCommand(sql, pSqlConnection);

            string path = !pDestination 
                              ? Path.Combine(UserSettings.GetUpdatePath,string.Format(SCHEMA_FILE_NAME, TechnicalSettings.SoftwareVersion)) 
                              : Path.Combine(UserSettings.GetUpdatePath,string.Format(LOCAL_SCHEMA_FILE_NAME, TechnicalSettings.SoftwareVersion));

            XmlTextWriter xml = new XmlTextWriter(path, Encoding.Unicode);
            xml.WriteStartDocument();
            xml.WriteStartElement("Database");
            xml.WriteAttributeString("Version", TechnicalSettings.SoftwareVersion);
            xml.WriteAttributeString("SystemDate", DateTime.Today.ToString("dd MM yyyy"));

            bool firstTable = true;
            using(OpenCbsReader reader = command.ExecuteReader())
            {
                string tableName = "";
                while(reader.Read())
                {
                    string tableNameTemp = reader.GetString("table_name");
                    if ((tableNameTemp != "sysdiagrams") && (tableNameTemp != "dtproperties") && (!tableNameTemp.StartsWith("Tconso_SP_Octopus_Consolidation_")))
                    {
                        if(tableName != tableNameTemp && firstTable)
                        {
                            xml.WriteStartElement("table");
                            tableName = tableNameTemp;
                            xml.WriteAttributeString("name",tableNameTemp);
                            firstTable = false;
                        }
                        else if(tableName != tableNameTemp)
                        {
                            xml.WriteEndElement(); //table

                            xml.WriteStartElement("table");
                            tableName = tableNameTemp;
                            xml.WriteAttributeString("name",tableNameTemp);
                        }
                        xml.WriteStartElement("column");
                        xml.WriteAttributeString("name", reader.GetString("column_name"));
                        xml.WriteAttributeString("type", reader.GetString("data_type"));
                        xml.WriteAttributeString("is_nullable", reader.GetString("is_nullable").Trim().ToUpper());
                        xml.WriteEndElement(); //column
                    }
                }
            }
            xml.WriteEndElement(); //database
            xml.Close();
        }

        
		
        /// <summary>
        /// Load the .xml of the right database for current version and check if the current database have the pSame struct.
        /// </summary>
        /// <returns>CompareOperationStatus</returns>
        public static void IsCurrentDatabaseIsRight(SqlConnection pSqlConnection, string pDatabaseName, ref bool pSame, ref string pAdditionalColumns, ref string pMissingColumns)
        {
            pSame = true;

            string path = Path.Combine(UserSettings.GetUpdatePath,string.Format(SCHEMA_FILE_NAME, TechnicalSettings.SoftwareVersion));

            XmlDocument xmlSource = new XmlDocument();
            xmlSource.Load(path);

            SaveDatabaseDiagramsInXml(true, pDatabaseName, pSqlConnection);

            path = Path.Combine(UserSettings.GetUpdatePath,string.Format(LOCAL_SCHEMA_FILE_NAME, TechnicalSettings.SoftwareVersion));

            XmlDocument xmlDestination = new XmlDocument();
            xmlDestination.Load(path);

            _CompareXml(xmlSource, xmlDestination,false,ref pSame,ref pAdditionalColumns,ref pMissingColumns);

            bool sameLess = false;
            string additionalColumnsLess = string.Empty, missingColumnsLess = string.Empty;
            _CompareXml(xmlDestination, xmlSource, true, ref sameLess, ref additionalColumnsLess, ref missingColumnsLess);

            if (!sameLess)
            {
                pSame = false;
                pAdditionalColumns += additionalColumnsLess;
                pMissingColumns += missingColumnsLess;
            }
        }

        private static void _CompareXml(XmlDocument pXmlA, XmlDocument pXmlB, bool pIsLess, ref bool pSame, ref string pAdditionalColumns, ref string pMissingColumns)
        {
            pSame = true;

            XmlNodeList listColumn = pXmlA.GetElementsByTagName("column");
            foreach (XmlNode elem in listColumn)
            {
                string columnName = elem.Attributes["name"].Value;
                string columnType = elem.Attributes["type"].Value;
                string columnIsNullable = elem.Attributes["is_nullable"].Value;
                string tableName = elem.ParentNode.Attributes["name"].Value;

                XmlNode xmlNode = pXmlB.SelectSingleNode(String.Format("Database/table[@name='{0}']/column[@name='{1}'][@type='{2}'][@is_nullable='{3}']", tableName, columnName, columnType, columnIsNullable));

                if (xmlNode == null)
                {
                    pSame = false;
                    if (pIsLess)
                        pAdditionalColumns += string.Format("\tTable '{0}', column '{1}', type '{2}', isnullable '{3}' \n", tableName, columnName, columnType, columnIsNullable);
                    else
                        pMissingColumns += string.Format("\tTable '{0}', column '{1}', type '{2}', isnullable '{3}' \n", tableName, columnName, columnType, columnIsNullable);
                }
            }
        }
    }
}
