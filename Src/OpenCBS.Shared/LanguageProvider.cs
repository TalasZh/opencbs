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

namespace OpenCBS.Shared
{
    [Serializable]
    public static class LanguageProvider
    {
        private static readonly List<Language> _languages;

        public static readonly string ENGLISH_CODE = "en-US";
        public static readonly string FRENCH_CODE = "fr";
        public static readonly string RUSSIAN_CODE = "ru-RU";
        public static readonly string ARABIAN_CODE = "ar-MA";
        public static readonly string ENGLISH_CUSTOM_CODE = "custom";

        public class Language
        {
            public string Code;
            public string Label;

            public Language(string pCode, string pLabel)
            {
                Code = pCode;
                Label = pLabel;
            }
        }

        static LanguageProvider()
        {
            _languages = new List<Language>();
            _languages.Add(new Language(ENGLISH_CODE, "English"));
            _languages.Add(new Language(FRENCH_CODE, "French"));
            _languages.Add(new Language(RUSSIAN_CODE, "Russian"));
            _languages.Add(new Language(ARABIAN_CODE, "Arabian"));
        }

        /// <summary>
        /// Returns list of supported language labels
        /// </summary>
        static public List<string> LanguageLabels
        {
            get
            {
                List<string> labels = new List<string>();
                foreach (Language l in _languages)
                {
                    labels.Add(l.Label);
                }
                return labels;
            }
        }

        /// <summary>
        /// Returns language code for given language label.
        /// </summary>
        /// <param name="pLabel">Language label</param>
        /// <returns>Language code</returns>
        static public string GetLanguageCode(string pLabel)
        {
            foreach (Language l in _languages)
            {
                if (l.Label == pLabel)
                {
                    return l.Code;
                }
            }
            return null;
        }
    }
}
