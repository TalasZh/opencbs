// LICENSE PLACEHOLDER

using System;

namespace OpenCBS.CoreDomain
{
    [Serializable]
    public class MenuObject : IEquatable<MenuObject>
    {
        private int _id;
        private string _name;
        private bool _notSavedInDBYet;


        public MenuObject()
        {
            _notSavedInDBYet = false;
        }
        public bool NotSavedInDBYet
        {
            get { return _notSavedInDBYet; }
            set { _notSavedInDBYet = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public override string ToString()
        {
            return Name;
        }
        public bool Equals(MenuObject b)
        {
            return string.Equals(_name, b._name);
        }

        public override bool Equals(object obj)
        {
            MenuObject a = obj as MenuObject;
            return a != null && Equals(a);
        }

        public override int GetHashCode()
        {
            return string.IsNullOrEmpty(_name) != null ? _name.GetHashCode() : base.GetHashCode();
        }
        public static bool operator ==(MenuObject a, Object b)
        {

            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }
            if (b is string)
            {
                string str = b.ToString();
                return str.ToUpper().Equals(a._name.ToUpper());
            }

            MenuObject tryToConvert = (MenuObject)b;
            if (tryToConvert == null) return false;

            return Convert.ToBoolean(string.Compare(tryToConvert._name, a._name, true));
        }
        public static bool operator !=(MenuObject a, System.Object b)
        {
            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(a, b))
            {
                return false;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return true;
            }
            if (b is string)
            {
                return !b.Equals(a._name);
            }

            MenuObject tryToConvert = (MenuObject) b;
            if (tryToConvert == null) return true;

            return !a._name.ToUpper().Equals(tryToConvert._name.ToUpper());
        }
    }
}
