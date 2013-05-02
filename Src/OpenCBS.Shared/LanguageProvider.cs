// LICENSE PLACEHOLDER

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
