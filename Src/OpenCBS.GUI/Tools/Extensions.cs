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
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using OpenCBS.Enums;
using OpenCBS.MultiLanguageRessources;
using OpenCBS.Shared;

namespace OpenCBS.GUI.Tools
{
    public static class Extensions
    {
        public static void DataBind(this ComboBox pComboBox, Type pEnum, Ressource pRessource, bool pSort)
        {
            pComboBox.DisplayMember = "Display";
            pComboBox.ValueMember = "Value";

            var list = (from item in Enum.GetNames(pEnum)
                        select new
                        {
                            Display = MultiLanguageStrings.GetString(pRessource, item + ".Text") ?? item,
                            Value = item
                        });

            if (pSort) list = list.OrderBy(item => item.Value);

            pComboBox.DataSource = list.ToList();
        }

        public static void LoadGender(this ComboBox comboBox)
        {
            var items = comboBox.Items;
            items.Clear();
            var male = new DictionaryEntry(OGender.Male, OGender.Male.GenderString());
            var female = new DictionaryEntry(OGender.Female, OGender.Female.GenderString());
            items.Add(male);
            items.Add(female);
            comboBox.SelectedItem = male;
        }

        public static string GenderString(this char gender)
        {
            Debug.Assert(OGender.CheckGender(gender), string.Format("Not valid gender: {0}", gender));
            var maleString = MultiLanguageStrings.GetString(Ressource.PersonUserControl, "sexM.Text");
            switch (gender)
            {
                case OGender.Male:
                    return maleString;
                case OGender.Female:
                    return MultiLanguageStrings.GetString(Ressource.PersonUserControl, "sexF.Text");
                default:
                    return maleString;
            }
        }

        public static void SelectGender(this ComboBox comboBox, char sex)
        {
            foreach (DictionaryEntry item in comboBox.Items)
            {
                if ((char)item.Key == sex)
                    comboBox.SelectedItem = item;
            }
        }

        public static char GetGender(this ComboBox comboBox)
        {
            var de = (DictionaryEntry)comboBox.SelectedItem;
            return (char)de.Key;
        }

        public static void SetRangeText(this Label lblRange, OCurrency value)
        {
            lblRange.Text = string.Format(
                "{0}{1}\r\n{2}{3}",
                MultiLanguageStrings.GetString(Ressource.CreditContractForm, "min.Text"),
                value.Value,
                MultiLanguageStrings.GetString(Ressource.CreditContractForm, "max.Text"),
                value.Value
                );
        }

        public static void SetRangeText(this Label lblRange, OCurrency minValue, OCurrency maxValue)
        {
            lblRange.Text = string.Format(
                "{0}{1}\r\n{2}{3}",
                MultiLanguageStrings.GetString(Ressource.CreditContractForm, "min.Text"),
                minValue.Value,
                MultiLanguageStrings.GetString(Ressource.CreditContractForm, "max.Text"),
                maxValue.Value
                );
        }
    }
}
