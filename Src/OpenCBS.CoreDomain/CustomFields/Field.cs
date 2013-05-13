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
