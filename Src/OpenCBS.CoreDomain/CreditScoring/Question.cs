// LICENSE PLACEHOLDER

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
