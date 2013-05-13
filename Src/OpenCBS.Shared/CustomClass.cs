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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;

namespace OpenCBS.Shared
{
	/// <summary>
	/// CustomClass (Which is binding to property grid)
	/// </summary>
    public class CustomClass: CollectionBase, ICustomTypeDescriptor
	{
		/// <summary>
		/// Add CustomProperty to Collectionbase List
		/// </summary>
		/// <param name="value"></param>
		public void Add(CustomProperty value)
		{
			List.Add(value);
		}

		/// <summary>
		/// Remove item from List
		/// </summary>
		/// <param name="name"></param>
		public void Remove(string name)
		{
			foreach(CustomProperty prop in List)
			{
				if(prop.Name == name)
				{
					List.Remove(prop);
					return;
				}
			}
		}

        public bool Contains(string name)
        {
            foreach (CustomProperty prop in List)
                if (prop.Name == name) 
                    return true;
            return false;
        }

        public object GetPropertyValueByName(string name)
        {
            foreach (CustomProperty prop in List)
                if (prop.Name == name) 
                    return prop.Value;
            return null;
        }

        public void SetPropertyValueByName(string name, object value)
        {
            foreach (CustomProperty prop in List)
                if (prop.Name == name)
                {
                    prop.Value = value;
                    break;
                }
        }

        public object GetPropertyTypeByName(string name)
        {
            foreach (CustomProperty prop in List)
                if (prop.Name == name)
                    return prop.Type;
            return null;
        }

		/// <summary>
		/// Indexer
		/// </summary>
		public CustomProperty this[int index] 
		{
			get  { return (CustomProperty) List[index]; }
			set { List[index] = value; }
		}

        public int GetSize()
        {
            return List.Count;
        }

		#region "TypeDescriptor Implementation"
		/// <summary>
		/// Get Class Name
		/// </summary>
		/// <returns>String</returns>
		public String GetClassName()
		{
			return TypeDescriptor.GetClassName(this,true);
		}

		/// <summary>
		/// GetAttributes
		/// </summary>
		/// <returns>AttributeCollection</returns>
		public AttributeCollection GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this,true);
		}

		/// <summary>
		/// GetComponentName
		/// </summary>
		/// <returns>String</returns>
		public String GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		/// <summary>
		/// GetConverter
		/// </summary>
		/// <returns>TypeConverter</returns>
		public TypeConverter GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		/// <summary>
		/// GetDefaultEvent
		/// </summary>
		/// <returns>EventDescriptor</returns>
		public EventDescriptor GetDefaultEvent() 
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		/// <summary>
		/// GetDefaultProperty
		/// </summary>
		/// <returns>PropertyDescriptor</returns>
		public PropertyDescriptor GetDefaultProperty() 
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		/// <summary>
		/// GetEditor
		/// </summary>
		/// <param name="editorBaseType">editorBaseType</param>
		/// <returns>object</returns>
		public object GetEditor(Type editorBaseType) 
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		public EventDescriptorCollection GetEvents(Attribute[] attributes) 
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		public EventDescriptorCollection GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			PropertyDescriptor[] newProps = new PropertyDescriptor[this.Count];
			for (int i = 0; i < this.Count; i++)
			{
				CustomProperty  prop = (CustomProperty) this[i];
				newProps[i] = new CustomPropertyDescriptor(ref prop, attributes);
			}

			return new PropertyDescriptorCollection(newProps);
		}

		public PropertyDescriptorCollection GetProperties()
		{
			return TypeDescriptor.GetProperties(this, true);
		}

		public object GetPropertyOwner(PropertyDescriptor pd) 
		{
			return this;
		}
		#endregion
	
	}

	/// <summary>
	/// Custom property class 
	/// </summary>
    public class CustomProperty
	{
        private string sName = string.Empty;
	    private string sDesc = string.Empty;
		private bool bReadOnly;
		private bool bVisible = true;
		private object _objValue;

		public CustomProperty(string sName, string sDesc, object value, Type type, bool bReadOnly, bool bVisible)
		{
            this.sName = sName;
		    this.sDesc = sDesc;
			_objValue = value;
			this.type = type;
			this.bReadOnly = bReadOnly;
			this.bVisible = bVisible;
		}

		private Type type;
		public Type Type
		{
			get { return type; }
		}

		public bool ReadOnly
		{
			get
			{
				return bReadOnly;
			}
		}

		public string Name
		{
			get
			{
				return sName;
			}
		}

        public string Description
        {
            get
            {
                return sDesc;
            }
        }

		public bool Visible
		{
			get
			{
				return bVisible;
			}
		}
    
        public object Value
		{
			get
			{
				return _objValue;
			}
			set
			{
				_objValue = value;
			}
		}
	}

    /// <summary>
	/// Custom PropertyDescriptor
	/// </summary>
    public class CustomPropertyDescriptor: PropertyDescriptor
	{
        readonly CustomProperty _customProperty;
		public CustomPropertyDescriptor(ref CustomProperty customProperty, Attribute [] attrs) : base(customProperty.Name, attrs)
		{
			_customProperty = customProperty;
		}

		#region PropertyDescriptor specific
		
		public override bool CanResetValue(object component)
		{
			return false;
		}

		public override Type ComponentType
		{
			get { return null; }
		}

		public override object GetValue(object component)
		{
			return _customProperty.Value;
		}

		public override string Description
		{
			get { return _customProperty.Description; }
		}
		
		public override string Category
		{
			get	{ return string.Empty; }
		}

		public override string DisplayName
		{
			get { return _customProperty.Name; }
		}

		public override bool IsReadOnly
		{
			get { return _customProperty.ReadOnly; }
		}

		public override void ResetValue(object component)
		{
			//Have to implement
		}

		public override bool ShouldSerializeValue(object component)
		{
			return false;
		}

		public override void SetValue(object component, object value)
		{
			_customProperty.Value = value;
		}

		public override Type PropertyType
		{
			get { return _customProperty.Type; }
		}

		#endregion
	}

    public class CollectionList
    {
        private Dictionary<string, List<string>> Collections;
        public CollectionList()
        {
            Collections = new Dictionary<string, List<string>>();
        }

        public void Add(string collectionName, List<string> collection)
        {
            Collections.Add(collectionName, collection);
        }

        public int GetItemIndexByName(string collectionName, string itemValue)
        {
            List<string> collection;
            if (Collections.ContainsKey(collectionName))
            {
                collection = Collections[collectionName];
                for (int i = 0; i < collection.Count; i++)
                    if (collection[i] == itemValue)
                        return i;
            }
            return -1;
        }
    }

    [Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
    public class Collection
    {
        public static List<string> Items;
    }

    [Browsable(true)]
    [TypeConverter(typeof(CollectionConverter))]
    [Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
    public class CollectionType { }
    public class CollectionConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true; // true means show a combobox
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true; // true will limit to list. false will show the list, but allow free-form entry
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(Collection.Items);
        }
    }
}
