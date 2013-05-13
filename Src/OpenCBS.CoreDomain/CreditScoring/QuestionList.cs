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
using OpenCBS.Enums;
using OpenCBS.MultiLanguageRessources;

namespace OpenCBS.CoreDomain.CreditScoring
{
    class LocalizedDisplayNameAttribute : DisplayNameAttribute
    {
        private readonly string _resourceName;
        public LocalizedDisplayNameAttribute(string resourceName)
        {
            _resourceName = resourceName;
        }

        public override string DisplayName
        {
            get
            {
                return MultiLanguageStrings.GetString(Ressource.CreditScoringForm, _resourceName + @".Text");
            }
        }
    }

    public class LocalizableDescriptionAttribute : DescriptionAttribute
    {
        public LocalizableDescriptionAttribute(string resourceKey)
            : base(MultiLanguageStrings.GetString(Ressource.CreditScoringForm, resourceKey + @".Text"))
        { }
    }
    
    public class QuestionList : INotifyPropertyChanged
    {
        [RefreshProperties(RefreshProperties.Repaint)]
        [LocalizedDisplayName("Questions")]
        public BindingList<Question> Questions { get; set; }

        [RefreshProperties(RefreshProperties.Repaint)]
        [LocalizedDisplayName("Count")]
        public int Count { get { return Questions.Count; } }

        private bool _isChanged;
        [Browsable(false)]
        public bool IsChanged
        {
            get { return _isChanged; }
            set
            {
                _isChanged = value;
                OnPropertyChanged(Questions);
            }
        }

        void ListChanged(object sender, ListChangedEventArgs e)
        {
            foreach (Question question in Questions)
            {
                if (question.Type != OQuestionType.Collection)
                {
                    if (question.Answers != null)
                        question.Answers.Clear();
                }
            }
        }

        public override string ToString()
        {
            return MultiLanguageStrings.GetString(Ressource.CreditScoringForm, "Questions.Text");
        }

        [RefreshProperties(RefreshProperties.All)]
        public QuestionList()
        {
            Questions = new BindingList<Question>
            {
                AllowNew = true,
                AllowRemove = true,
                RaiseListChangedEvents = true,
                AllowEdit = true
            };
            Questions.ListChanged += ListChanged;
            _isChanged = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(object sender)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(sender.ToString()));
            }
        }
    }
}
