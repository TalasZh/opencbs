// LICENSE PLACEHOLDER

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
