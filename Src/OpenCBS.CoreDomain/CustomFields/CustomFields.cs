// LICENSE PLACEHOLDER

using System.ComponentModel;
using OpenCBS.CoreDomain.CreditScoring;
using OpenCBS.Enums;
using OpenCBS.MultiLanguageRessources;

namespace OpenCBS.CoreDomain.CustomFields
{
    public class CustomFields : INotifyPropertyChanged
    {
        [RefreshProperties(RefreshProperties.Repaint)]
        [LocalizedDisplayName("Fields")]
        public BindingList<Field> Fields { get; set; }

        [RefreshProperties(RefreshProperties.Repaint)]
        [LocalizedDisplayName("Count")]
        public int Count { get { return Fields.Count; } }

        private bool _isChanged;
        [Browsable(false)]
        public bool IsChanged
        {
            get { return _isChanged; }
            set
            {
                _isChanged = value;
                OnPropertyChanged(Fields);
            }
        }

        void ListChanged(object sender, ListChangedEventArgs e)
        {
            foreach (Field question in Fields)
            {
                if (question.Type != OCustomizableFieldTypes.Collection)
                {
                    if (question.Collection != null)
                        question.Collection.Clear();
                }
            }
        }

        public override string ToString()
        {
            return MultiLanguageStrings.GetString(Ressource.CreditScoringForm, "Questions.Text");
        }

        [RefreshProperties(RefreshProperties.All)]
        public CustomFields()
        {
            Fields = new BindingList<Field>
            {
                AllowNew = true,
                AllowRemove = true,
                RaiseListChangedEvents = true,
                AllowEdit = true
            };
            Fields.ListChanged += ListChanged;
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
