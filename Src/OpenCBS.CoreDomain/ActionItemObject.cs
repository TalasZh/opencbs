// LICENSE PLACEHOLDER

using System;

namespace OpenCBS.CoreDomain
{
    [Serializable]
    public class ActionItemObject : IEquatable<ActionItemObject>
    {
        private int _id;
        private string _className;
        private string _methodName;
        private bool _notSavedInDbYet;

        public ActionItemObject()
        {
            _notSavedInDbYet = false;
        }
        public ActionItemObject(string pClassName, string pMethodName)
        {
            _notSavedInDbYet = false;
            _className = pClassName;
            _methodName = pMethodName;
        }
        public ActionItemObject(int pId, string pClassName, string pMethodName, bool pNotSavedYet)
        {
            _id = pId;
            _className = pClassName;
            _methodName = pMethodName;
            _notSavedInDbYet = pNotSavedYet;
        }
        public bool NotSavedInDbYet
        {
            get { return _notSavedInDbYet; }
            set { _notSavedInDbYet = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string ClassName
        {
            get { return _className; }
            set { _className = value; }
        }
        public string MethodName
        {
            get { return _methodName; }
            set { _methodName = value; }
        }
        public override string ToString()
        {
            return _className+_methodName;
        }
        public bool Equals(ActionItemObject b)
        {
            return string.Equals(_className+_methodName, b._className+ b._methodName);
        }

        public override bool Equals(object obj)
        {
            ActionItemObject a = obj as ActionItemObject;
            return a != null && Equals(a);
        }

        public override int GetHashCode()
        {
            return string.IsNullOrEmpty(_className + _methodName) != null ? (_className + _methodName).GetHashCode() : base.GetHashCode();
        }
        public static bool operator ==(ActionItemObject a, Object b)
        {

            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || (b == null))
            {
                return false;
            }
            if (b is string)
            {
                string str = b.ToString();
                return string.Equals(str, a.ClassName + a.MethodName, StringComparison.OrdinalIgnoreCase);
            }

            ActionItemObject tryToConvert = (ActionItemObject)b;
            return (a.ClassName + a.MethodName).Equals(tryToConvert.ClassName + tryToConvert.MethodName);
        }
        public static bool operator !=(ActionItemObject a, Object b)
        {
            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(a, b))
            {
                return false;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || (b == null))
            {
                return true;
            }
            if (b is string)
            {
                return !b.Equals(a.ClassName + a.MethodName);
            }

            ActionItemObject tryToConvert = (ActionItemObject)b;

            return !(a.ClassName + a.MethodName).Equals(tryToConvert.ClassName + tryToConvert.MethodName);
        }
    }
}

