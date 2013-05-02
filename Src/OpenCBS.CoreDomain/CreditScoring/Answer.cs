// LICENSE PLACEHOLDER

using System.ComponentModel;

namespace OpenCBS.CoreDomain.CreditScoring
{
    public class Answer
    {
        [LocalizedDisplayName("Name")]
        public string Name { get; set; }
        [LocalizedDisplayName("Score")]
        public int Score { get; set; }
        [Browsable(false)]
        public int Id { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
