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

namespace OpenCBS.CoreDomain.CreditScoring
{
    [RefreshProperties(RefreshProperties.All)]
    [Browsable(true)]
    public class Question : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _answerScore = string.Empty;
        [LocalizedDisplayName("Name")]
        public string Name { get; set; }
        [LocalizedDisplayName("Answers")]
        public BindingList<Answer> Answers { get; set; }
        [LocalizedDisplayName("Count")]
        public int Count { get { return Answers.Count; } }
        private OQuestionType _oQuestionType;
        [LocalizedDisplayName("Type")]
        public OQuestionType Type
        {
            get { return _oQuestionType; }
            set
            {
                _oQuestionType = value;
                OnPropertyChanged(_oQuestionType);
            }
        }
        [Browsable(false)]
        public int Id { get; set; }
        [Browsable(false)]
        public string AnswerValue
        {
            get { return _answerScore; }
            set
            {
                _answerScore = value;
                OnPropertyChanged(this);
            }
        }
        [Browsable(false)]
        public int AnswerScore { get; set; }

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
           if(Answers.Count > 0)
           {
               Type = OQuestionType.Collection;
           }
        }

        [RefreshProperties(RefreshProperties.All)]
        public Question()
        {
            Type = OQuestionType.Text;
            Answers = new BindingList<Answer>();
            Answers.ListChanged += ListChanged;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
