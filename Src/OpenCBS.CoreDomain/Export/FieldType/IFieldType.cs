// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenCBS.CoreDomain.Export.FieldType
{
    public interface IFieldType : ICloneable
    {
        bool AlignRight { get; set; }
        string Format(object o, int? length);
        object Parse(string s);
    }
}
