// LICENSE PLACEHOLDER

using System.ComponentModel;
using OpenCBS.CoreDomain.CreditScoring;
using OpenCBS.Enums;

namespace OpenCBS.CoreDomain.CustomFields
{
    public class Field
    {
        public string Name { get; set; }
        public OCustomizableFieldTypes Type {get; set; }
        public BindingList<CollectionItem> Collection { get; set; }
        public int Id {get; set;}
        public OCustomizableFieldEntities Entity { get; set; }
        public bool IsMandatory { get; set; }
        public bool IsUnique  { get; set; }
        public string Description { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        // Create the OnPropertyChanged method to raise the event
        protected void OnPropertyChanged(object sender)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(sender.ToString()));
            }
        }

        void ListChanged(object sender, ListChangedEventArgs e)
        {
            if (Collection.Count > 0)
            {
                Type = OCustomizableFieldTypes.Collection;
            }
        }

        [RefreshProperties(RefreshProperties.All)]
        public Field()
        {
            Type = OCustomizableFieldTypes.String;
            Collection = new BindingList<CollectionItem>();
            Collection.ListChanged += ListChanged;
        }
    }
}
