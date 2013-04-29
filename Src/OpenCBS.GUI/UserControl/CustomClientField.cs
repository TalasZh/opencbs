//Octopus MFS is an integrated suite for managing a Micro Finance Institution: clients, contracts, accounting, reporting and risk
//Copyright ?2006,2012 OCTO Technology & OXUS Development Network
//
//This program is free software; you can redistribute it and/or modify
//it under the terms of the GNU Lesser General Public License as published by
//the Free Software Foundation; either version 2 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU Lesser General Public License for more details.
//
//You should have received a copy of the GNU Lesser General Public License along
//with this program; if not, write to the Free Software Foundation, Inc.,
//51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
//
// Licence : http://www.octopusnetwork.org/OverviewLicence.aspx
//
// Website : http://www.octopusnetwork.org
// Business contact: business(at)octopusnetwork.org
// Technical contact email : tech(at)octopusnetwork.org 
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using Octopus.CoreDomain.Clients;
using Octopus.MultiLanguageRessources;
using Octopus.Services;

namespace Octopus.GUI.UserControl
{
    [Editor(typeof(CustomizableFieldClientEditor), typeof(UITypeEditor))]
    [TypeConverter(typeof(CustomClientFieldConverter))]
    public class CustomClientField
    {
        private readonly int _clientId;
        private readonly string _fullName;

        public CustomClientField(int clientId, string fullName)
        {
            _clientId = clientId;
            _fullName = fullName;
        }

        public CustomClientField(Person person)
        {
            _clientId = person.Id;
            _fullName = person.FullName;
        }

        public string FullName
        {
            get { return _fullName; }
        }

        public int ClientId
        {
            get { return _clientId; }
        }

        public override string ToString()
        {
            return this == Empty ? string.Empty : ClientId.ToString(CultureInfo.InvariantCulture);
        }

        public static readonly CustomClientField Empty = new CustomClientField(int.MinValue, MultiLanguageStrings.GetString("Common", "CFClientNotSelected"));
    }

    public class CustomClientFieldConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value == null) return CustomClientField.Empty;

            if (value is string)
            {
                int personId;
                if(!int.TryParse(value.ToString(), out personId)) return CustomClientField.Empty;

                var service = ServicesProvider.GetInstance().GetClientServices();
                var person = service.FindPersonById(personId);
                return new CustomClientField(person);
            }            

            return base.ConvertFrom(context, culture, value);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (value == null) return CustomClientField.Empty.FullName;

            if (destinationType == typeof(string))
                return ((CustomClientField) value).FullName;

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}