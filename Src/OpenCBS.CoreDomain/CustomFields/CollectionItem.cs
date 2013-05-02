using System.ComponentModel;

namespace OpenCBS.CoreDomain.CustomFields
{
    public class CollectionItem
    {
        [Browsable(false)]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
