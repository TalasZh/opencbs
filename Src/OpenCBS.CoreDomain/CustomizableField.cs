// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using OpenCBS.Enums;

namespace OpenCBS.CoreDomain
{
    [Serializable]
    public class CustomizableField
    {
        public int Id { get; set; }
        public OCustomizableFieldEntities Entity { get; set; }
        public OCustomizableFieldTypes Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public List<string> Collection { get; set; }
        public bool IsMandatory { get; set; }
        public bool IsUnique { get; set; }
    }

    [Serializable]
    public class CustomizableFieldValue
    {
        public int Id { get; set; }
        public CustomizableField Field { get; set; }
        public string Value { get; set; }
    }

    
    public class CustomizableFieldSearchResult
    {
        public string EntityName;
        public int FieldsNumber;
        public string Fields;
    }
}
