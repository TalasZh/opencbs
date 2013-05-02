// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;

namespace OpenCBS.CoreDomain
{
    [Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
    public class CustomCollection
    {
        public List<String> Collection { get; set; }
        public CustomCollection()
        {
            Collection = new List<String>();
        }
    }
}
