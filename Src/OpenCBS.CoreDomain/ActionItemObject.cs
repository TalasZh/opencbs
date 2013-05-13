// Octopus MFS is an integrated suite for managing a Micro Finance Institution: 
// clients, contracts, accounting, reporting and risk
// Copyright © 2006,2007 OCTO Technology & OXUS Development Network
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License along
// with this program; if not, write to the Free Software Foundation, Inc.,
// 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
// Website: http://www.opencbs.com
// Contact: contact@opencbs.com

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

