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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenCBS.CoreDomain.Export.FieldType;

namespace OpenCBS.CoreDomain.Export.Fields
{
    [Serializable]
    public class Field : IField
    {
        #region IField Members

        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Header { get; set; }
        public int? Length { get; set; }

        #endregion

        #region ICloneable Members
        public object Clone()
        {
            object clone = this.MemberwiseClone();
            ((Field)clone).FieldType = this.FieldType.Clone() as IFieldType;
            return clone;
        }
        #endregion

        public int DefaultLength { get; set; }
        public IFieldType FieldType { get; set; }
        public bool IsRequired { get; set; }
        public string Format(object o)
        {
            return FieldType.Format(o, Length);
        }
    }
}
