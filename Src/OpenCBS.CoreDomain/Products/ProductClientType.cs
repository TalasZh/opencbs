// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenCBS.CoreDomain.Products
{
    public class ProductClientType
    {
        private int typeId;
        private string typeName;
        private bool isChecked;

        public ProductClientType(int typeId, string typeName, bool isChecked)
        {
            this.typeId = typeId;
            this.typeName = typeName;
            this.isChecked = isChecked;
        }

        public ProductClientType(int typeId, string typeName)
        {
            this.typeId = typeId;
            this.typeName = typeName;
        }

        public int TypeId
        {
            get { return typeId; }
            set { typeId = value; }
        }

        public string TypeName
        {
            get { return typeName; }
            set { typeName = value; }
        }

        public bool IsChecked
        {
            get { return isChecked; }
            set { isChecked = value; }
        }
    }
}
