// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using OpenCBS.CoreDomain;
using OpenCBS.Enums;

namespace OpenCBS.Manager
{
    public class CustomizableFieldsManager : Manager
    {
        public CustomizableFieldsManager(User pUser) : base(pUser) {}

        public CustomizableFieldsManager(string testDB) : base(testDB) {}

        public List<string> SelectAllEntites()
        {
            string sqlText = @"SELECT name 
                               FROM AdvancedFieldsEntities";
                    
            List<string> entities = new List<string>();
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand cmd = new OpenCbsCommand(sqlText, conn))
            {
                using (OpenCbsReader reader = cmd.ExecuteReader())
                {
                    if (reader.Empty) 
                        return new List<string>();
                    while (reader.Read())
                    {
                        entities.Add(reader.GetString("name"));
                    }
                }
            }
            
            return entities;
        }

        public List<CustomizableFieldSearchResult> SelectCreatedEntites()
        {
            string sqlText = @"SELECT 
                                 AdvancedFieldsEntities.id, 
                                 AdvancedFieldsEntities.name, 
                                 COUNT(AdvancedFieldsEntities.id) AS fields_number 
                               FROM AdvancedFieldsEntities
                               INNER JOIN dbo.AdvancedFields ON dbo.AdvancedFieldsEntities.id = dbo.AdvancedFields.entity_id
                               GROUP BY 
                                 AdvancedFieldsEntities.id, 
                                 AdvancedFieldsEntities.name
                               ORDER BY dbo.AdvancedFieldsEntities.id";

            List<CustomizableFieldSearchResult> entities = new List<CustomizableFieldSearchResult>();
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand cmd = new OpenCbsCommand(sqlText, conn))
            {
                using (OpenCbsReader reader = cmd.ExecuteReader())
                {
                    if (reader.Empty) return new List<CustomizableFieldSearchResult>();
                    while (reader.Read())
                    {
                        CustomizableFieldSearchResult result = new CustomizableFieldSearchResult();
                        result.EntityName = reader.GetString("name");
                        result.FieldsNumber = reader.GetInt("fields_number");
                        result.Fields = SelectFieldNamesForEntity(reader.GetInt("id"));
                        entities.Add(result);
                    }
                }
            }

            return entities;
        }

        private string SelectFieldNamesForEntity(int entityId)
        {
            string sqlText = @"SELECT name 
                               FROM dbo.AdvancedFields 
                               WHERE entity_id = @entity_id ";

            CommaDelimitedStringCollection commaStr = new CommaDelimitedStringCollection();
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand cmd = new OpenCbsCommand(sqlText, conn))
            {
                cmd.AddParam("@entity_id", entityId);

                using (OpenCbsReader reader = cmd.ExecuteReader())
                {
                    if (reader.Empty) 
                        return string.Empty;
                    while (reader.Read())
                    {
                        commaStr.Add(reader.GetString("name"));
                    }
                }
            }

            return commaStr.ToString();
        }

        public void AddCustomizableFields(List<CustomizableField> fields)
        {
            foreach (CustomizableField field in fields)
            {
                AddCustomizableField(field);
            }
        }

        private void AddCustomizableField(CustomizableField field)
        {
            string sqlText = @"INSERT INTO [AdvancedFields] 
                                ([entity_id]
                                , [type_id]
                                , [name]
                                , [desc]
                                , [is_mandatory]
                                , [is_unique]) 
                               VALUES 
                                (@entity_id
                                , @type_id
                                , @f_name
                                , @f_desc
                                , @is_mandatory
                                , @is_unique)
                               SELECT CONVERT(int, SCOPE_IDENTITY())";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand insertCmd = new OpenCbsCommand(sqlText, conn))
            {                
                insertCmd.AddParam("@entity_id", (int)field.Entity);
                insertCmd.AddParam("@type_id", (int)field.Type);
                insertCmd.AddParam("@f_name", field.Name);
                insertCmd.AddParam("@f_desc", field.Description);
                insertCmd.AddParam("@is_mandatory", field.IsMandatory);
                insertCmd.AddParam("@is_unique", field.IsUnique);

                field.Id = int.Parse(insertCmd.ExecuteScalar().ToString());

                if (field.Type == OCustomizableFieldTypes.Collection)
                {
                    foreach (string colItem in field.Collection)
                    {
                        if (colItem.Trim().Length > 0)
                        {
                            AddCollectionItem(field.Id, colItem);
                        }
                    }
                }
            }
        }

        public void AddCollectionItem(int fieldId, string colItem)
        {
            string sqlListText = @"INSERT INTO [AdvancedFieldsCollections] 
                                    ([field_id], [value]) 
                                   VALUES (@field_id, @col_item)";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand insertCmd = new OpenCbsCommand(sqlListText, conn))
            {
                insertCmd.AddParam("@field_id", fieldId);
                insertCmd.AddParam("@col_item", colItem);
                insertCmd.ExecuteNonQuery();
            }
        }

        public void DeleteCollection(int fieldId)
        {
            string sqlListText = @"DELETE FROM [AdvancedFieldsCollections] 
                                   WHERE [field_id] = @field_id";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand insertCmd = new OpenCbsCommand(sqlListText, conn))
            {
                insertCmd.AddParam("@field_id", fieldId);
                insertCmd.ExecuteNonQuery();
            }
        }

        public List<CustomizableField> SelectCustomizableFields(int entityId)
        {
            List<CustomizableField> fields = new List<CustomizableField>();

            const string sqlPropertyText = @"
                                            SELECT 
                                                [id]
                                                ,[entity_id]
                                                ,[type_id]
                                                ,[name]
                                                ,[desc]
                                                ,[is_mandatory]
                                                ,[is_unique]
                                            FROM [AdvancedFields] 
                                            WHERE entity_id = @entity_id";
           
            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand selectCmd = new OpenCbsCommand(sqlPropertyText, conn))
                {
                    selectCmd.AddParam("@entity_id", entityId);
                    using (OpenCbsReader reader = selectCmd.ExecuteReader())
                    {
                        if (reader == null || reader.Empty) return null;

                        while (reader.Read())
                        {
                            CustomizableField field = new CustomizableField
                                                          {
                                                              Id = reader.GetInt("id"),
                                                              Entity =
                                                                  (OCustomizableFieldEntities)
                                                                  Enum.ToObject(
                                                                      typeof (OCustomizableFieldEntities),
                                                                      reader.GetInt("entity_id")),
                                                              Type =
                                                                  (OCustomizableFieldTypes)
                                                                  Enum.ToObject(
                                                                      typeof (OCustomizableFieldTypes),
                                                                      reader.GetInt("type_id")),
                                                              Name = reader.GetString("name"),
                                                              Description = reader.GetString("desc"),
                                                              IsMandatory =
                                                                  reader.GetBool("is_mandatory"),
                                                              IsUnique = reader.GetBool("is_unique")
                                                          };
                            fields.Add(field);
                        }
                    }
                }
            }

            foreach (CustomizableField customizableField in fields.Where(item => item.Type == OCustomizableFieldTypes.Collection))
            {

                List<string> fieldList = new List<string>();
                const string sqlListText = @"SELECT [value] 
                                                        FROM AdvancedFieldsCollections 
                                                        WHERE field_id = @field_id";

                using (SqlConnection conn2 = GetConnection())
                using (OpenCbsCommand selectList = new OpenCbsCommand(sqlListText, conn2))
                {
                    selectList.AddParam("@field_id", customizableField.Id);
                    using (OpenCbsReader listReader = selectList.ExecuteReader())
                    {
                        if (listReader == null || listReader.Empty) return null;

                        while (listReader.Read())
                        {
                            fieldList.Add(listReader.GetString("value"));
                        }

                        customizableField.Collection = fieldList;
                    }
                }
            }

            return fields;
        }

        public bool CustomizableFieldsExistFor(OCustomizableFieldEntities entity)
        {
            string sqlText = @"SELECT COUNT(*) AS [number] 
                               FROM dbo.AdvancedFields 
                               WHERE [entity_id] = @entity_id ";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand selectCmd = new OpenCbsCommand(sqlText, conn))
            {
                selectCmd.AddParam("@entity_id",
                                   (int) Enum.Parse(typeof (OCustomizableFieldEntities), entity.ToString()));

                using (OpenCbsReader reader = selectCmd.ExecuteReader())
                {
                    if (reader == null || reader.Empty) return false;
                    reader.Read();
                    if (reader.GetInt("number") > 0) return true;
                }
            }

            return false;
        }

        public void SaveCustomizableFieldValues(List<CustomizableFieldValue> fieldValues, int linkId, char linkType)
        {
            int groupId;

            string sqlText = @"SELECT id 
                               FROM dbo.AdvancedFieldsLinkEntities 
                               WHERE link_id = @link_id 
                               AND link_type = @link_type ";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand selectCmd = new OpenCbsCommand(sqlText, conn))
            {
                selectCmd.AddParam("@link_id", linkId);
                selectCmd.AddParam("@link_type", linkType);

                using (OpenCbsReader reader = selectCmd.ExecuteReader())
                {
                    if (reader.Empty) groupId = 0;
                    else
                    {
                        reader.Read();
                        groupId = reader.GetInt("id");
                    }
                }
            }

            if (groupId == 0) // add
            {
                sqlText =@"
                        INSERT INTO [AdvancedFieldsLinkEntities] 
                        ([link_id]
                        ,[link_type]) 
                        VALUES (@link_id, @link_type) 
                        SELECT CONVERT(int, SCOPE_IDENTITY())";
                using (SqlConnection conn = GetConnection())
                using (OpenCbsCommand insertCmd = new OpenCbsCommand(sqlText, conn))
                {
                    insertCmd.AddParam("@link_id", linkId);
                    insertCmd.AddParam("@link_type", linkType);
                    groupId = int.Parse(insertCmd.ExecuteScalar().ToString());
                }

                foreach (CustomizableFieldValue fieldValue in fieldValues)
                {
                    SaveCustomizableFieldValue(fieldValue, groupId, true);
                }
            }
            else // update
            {
                foreach (CustomizableFieldValue fieldValue in fieldValues)
                {
                    int fieldId;

                    string sqlTextSel =@"
                                        SELECT id 
                                        FROM dbo.AdvancedFieldsValues 
                                        WHERE entity_field_id = @entity_field_id 
                                        AND field_id = @field_id ";
                    using (SqlConnection conn = GetConnection())
                    using (OpenCbsCommand selCmd = new OpenCbsCommand(sqlTextSel, conn))
                    {
                        selCmd.AddParam("@entity_field_id", groupId);
                        selCmd.AddParam("@field_id", fieldValue.Field.Id);

                        using (OpenCbsReader reader = selCmd.ExecuteReader())
                        {
                            if (reader == null || reader.Empty)
                            {
                                fieldId = 0;
                            }
                            else
                            {
                                reader.Read();
                                fieldId = reader.GetInt("id");
                            }
                        }
                    }
                    SaveCustomizableFieldValue(fieldValue, groupId, (fieldId == 0));
                }
            }
        }

        private void SaveCustomizableFieldValue(CustomizableFieldValue fieldValue, int groupId, bool isInsert)
        {
            string sqlText = @"
                                INSERT INTO [AdvancedFieldsValues] 
                                    ([entity_field_id]
                                    ,[field_id]
                                    ,[value]) 
                                VALUES 
                                    (@entity_field_id
                                    ,@field_id
                                    ,@value)";

            if (!isInsert)
            {
                sqlText = @"UPDATE [AdvancedFieldsValues] 
                            SET [value] = @value
                            WHERE [entity_field_id] = @entity_field_id 
                            AND [field_id] = @field_id";
            }
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand insertValue = new OpenCbsCommand(sqlText, conn))
            {
                insertValue.AddParam("@entity_field_id", groupId);
                insertValue.AddParam("@field_id", fieldValue.Field.Id);
                insertValue.AddParam("@value", fieldValue.Value);
                insertValue.ExecuteNonQuery();
            }
        }

        public List<CustomizableFieldValue> SelectCustomizableFieldValues(int linkId, char linkType)
        {
            int groupId;
            List<CustomizableFieldValue> fieldsValues = new List<CustomizableFieldValue>();

            string sqlText = @"SELECT id 
                               FROM dbo.AdvancedFieldsLinkEntities 
                               WHERE link_id = @link_id 
                               AND link_type = @link_type ";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand selectCmd = new OpenCbsCommand(sqlText, conn))
            {
                selectCmd.AddParam("@link_id", linkId);
                selectCmd.AddParam("@link_type", linkType);

                using (OpenCbsReader reader = selectCmd.ExecuteReader())
                {
                    if (reader == null || reader.Empty) return null;
                    reader.Read();
                    groupId = reader.GetInt("id");
                }
            }

            if (groupId != 0)
            {
                Dictionary<int, string> dict = new Dictionary<int, string>();

                sqlText =@"
                            SELECT 
                                [field_id]
                                ,[value] 
                            FROM dbo.AdvancedFieldsValues
                            WHERE [entity_field_id] = @entity_field_id ";
                using (SqlConnection conn = GetConnection())
                using (OpenCbsCommand selectValues = new OpenCbsCommand(sqlText, conn))
                {
                    selectValues.AddParam("@entity_field_id", groupId);
                    using (OpenCbsReader readerValues = selectValues.ExecuteReader())
                    {
                        if (readerValues == null || readerValues.Empty) return null;

                        while (readerValues.Read())
                        {
                            dict.Add(readerValues.GetInt("field_id"),
                                     readerValues.GetString("value"));
                        }
                    }
                }

                fieldsValues.AddRange(dict.Select(item => new CustomizableFieldValue
                                                  {
                                                      Field = SelectCustomizableFieldById(item.Key), 
                                                      Value = item.Value
                                                  }));
            }
            

            return fieldsValues;
        }

        private CustomizableField SelectCustomizableFieldById(int fieldId)
        {
            string sqlPropertyText = @"SELECT 
                                        [id]
                                        ,[entity_id]
                                        ,[type_id]
                                        ,[name]
                                        ,[desc]
                                        ,[is_mandatory]
                                        ,[is_unique]
                                      FROM [AdvancedFields] 
                                      WHERE id = @id";

            CustomizableField field;
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand selectCmd = new OpenCbsCommand(sqlPropertyText, conn))
            {
                selectCmd.AddParam("@id", fieldId);

                using (OpenCbsReader reader = selectCmd.ExecuteReader())
                {
                    if (reader == null || reader.Empty) return null;
                    reader.Read();

                    field = new CustomizableField
                                {
                                    Id = reader.GetInt("id"),
                                    Entity =
                                        (OCustomizableFieldEntities)
                                        Enum.ToObject(typeof (OCustomizableFieldEntities),
                                                      reader.GetInt("entity_id")),
                                    Type =
                                        (OCustomizableFieldTypes)
                                        Enum.ToObject(typeof (OCustomizableFieldTypes),
                                                      reader.GetInt("type_id")),
                                    Name = reader.GetString("name"),
                                    Description = reader.GetString("desc"),
                                    IsMandatory = reader.GetBool("is_mandatory"),
                                    IsUnique = reader.GetBool("is_unique")
                                };
                }
            }


            if (field.Type == OCustomizableFieldTypes.Collection)
                {
                    List<string> fieldList = new List<string>();
                    const string sqlListText = @"SELECT [value] 
                                                 FROM AdvancedFieldsCollections 
                                                 WHERE field_id = @field_id";
                    using (SqlConnection conn2 = GetConnection())
                    using (OpenCbsCommand selectList = new OpenCbsCommand(sqlListText, conn2))
                    {
                        selectList.AddParam("@field_id", field.Id);
                        using (OpenCbsReader listReader = selectList.ExecuteReader())
                        {
                            if (listReader == null || listReader.Empty) return null;

                            while (listReader.Read())
                            {
                                fieldList.Add(listReader.GetString("value"));
                            }

                            field.Collection = fieldList;
                        }
                    }
                }
            
            return field;
        }

        public bool FieldValueExists(int linkId, char linkType, int fieldId, string value)
        {
            int groupId;

            string sqlTextGroup = @"SELECT id 
                                    FROM dbo.AdvancedFieldsLinkEntities 
                                    WHERE link_id = @link_id 
                                    AND link_type = @link_type ";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand selectGroup = new OpenCbsCommand(sqlTextGroup, conn))
            {
                selectGroup.AddParam("@link_id", linkId);
                selectGroup.AddParam("@link_type", linkType);

                using (OpenCbsReader reader = selectGroup.ExecuteReader())
                {
                    if (reader == null || reader.Empty)
                    {
                        groupId = 0;
                    }
                    else
                    {
                        reader.Read();
                        groupId = reader.GetInt("id");
                    }
                }
            }

            if (groupId == 0) // add
            {
                string sqlText = @"SELECT COUNT(AdvancedFieldsValues.id) AS [number] 
                                   FROM dbo.AdvancedFieldsValues
                                   WHERE field_id = @field_id 
                                   AND [value] = @value ";
                using (SqlConnection conn = GetConnection())
                using (OpenCbsCommand selectCmd = new OpenCbsCommand(sqlText, conn))
                {
                    selectCmd.AddParam("@field_id", fieldId);
                    selectCmd.AddParam("@value", value);

                    using (OpenCbsReader reader = selectCmd.ExecuteReader())
                    {
                        if (reader == null || reader.Empty) return false;
                        reader.Read();

                        if (reader.GetInt("number") > 0) return true;
                    }
                }
            }
            else
            {
                string sqlText = @"SELECT COUNT(AdvancedFieldsValues.id) 
                                   AS [number] 
                                   FROM dbo.AdvancedFieldsValues
                                   WHERE entity_field_id <> @entity_field_id 
                                   AND field_id = @field_id 
                                   AND [value] = @value ";
                using (SqlConnection conn = GetConnection())
                using (OpenCbsCommand selectCmd = new OpenCbsCommand(sqlText, conn))
                {
                    selectCmd.AddParam("@entity_field_id", groupId);
                    selectCmd.AddParam("@field_id", fieldId);
                    selectCmd.AddParam("@value", value);

                    using (OpenCbsReader reader = selectCmd.ExecuteReader())
                    {
                        if (reader == null || reader.Empty) return false;
                        reader.Read();

                        if (reader.GetInt("number") > 0) return true;
                    }
                }
            }
            return false;
        }

        public bool FieldValuesExistForFieldId(int fieldId)
        {
            string sqlText = @"SELECT COUNT(*) 
                               AS [number]     
                               FROM dbo.AdvancedFieldsValues 
                               WHERE field_id = @field_id";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand selectCmd = new OpenCbsCommand(sqlText, conn))
            {
                selectCmd.AddParam("@field_id", fieldId);

                using (OpenCbsReader reader = selectCmd.ExecuteReader())
                {
                    if (reader == null || reader.Empty) return false;
                    reader.Read();
                    if (reader.GetInt("number") > 0) return true;
                }
            }

            return false;
        }

        public void DeleteField(CustomizableField field)
        {
            if (field.Type == OCustomizableFieldTypes.Collection)
            {
                string sqlColText = @"DELETE FROM dbo.AdvancedFieldsCollections 
                                      WHERE field_id = @field_id";
                using (SqlConnection conn = GetConnection())
                using (OpenCbsCommand deleteCmd = new OpenCbsCommand(sqlColText, conn))
                {
                    deleteCmd.AddParam("@field_id", field.Id);
                    deleteCmd.ExecuteNonQuery();
                }
            }
            
            string sqlText = @"DELETE FROM dbo.AdvancedFields 
                               WHERE id = @field_id";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand deleteCmd = new OpenCbsCommand(sqlText, conn))
            {
                deleteCmd.AddParam("@field_id", field.Id);
                deleteCmd.ExecuteNonQuery();
            }
        }
    }
}
