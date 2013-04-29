using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Octopus.CoreDomain.Export.FieldType
{
    [Serializable]
    public class StringFieldType : IFieldType
    {
        //public Enum? Enum { get; set; }
        public int StartPosition { get; set; }
        public int? EndPosition { get; set; }
        public Dictionary<string, string> ReplacementList
        {
            get;
            set;
        }

        #region IFieldType Members

        public bool AlignRight { get; set; }

        public StringFieldType()
        {
            ReplacementList = new Dictionary<string, string>();
        }

        public string Format(object o, int? length)
        {
            var s = (string)o;

            if (s != null)
            {
                foreach (var key in ReplacementList.Keys)
                {
                    s = s.Replace(key, ReplacementList[key]);
                }

                if (EndPosition.HasValue)
                    s = s.Substring(StartPosition, EndPosition.Value - StartPosition);
                else
                    s = s.Substring(StartPosition);


                if (length.HasValue)
                    return Format(s, length.Value, AlignRight);
                else
                    return s;
            }
            else
                return string.Empty;
        }

        public object Parse(string s)
        {
            return s;
        }

        #endregion

        public static string Format(string s, int length, bool alignRight)
        {
            if (s.Length > length)
                return s.Substring(0, length);
            else
                if (alignRight)
                    return s.PadLeft(length);
                else
                    return s.PadRight(length);
        }

        #region ICloneable Members

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }
}
