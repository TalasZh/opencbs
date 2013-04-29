using System;

namespace Octopus.CoreDomain.Accounting
{
    [Serializable]
    public class Currency
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsPivot { get; set; }
        public bool IsSwapped { get; set; }
        public string Code { get; set; }
        public override bool Equals(object obj)
        {
            if(obj is Currency)
            {
                Currency compareWith = obj as Currency;
                if(this.Name.Equals(compareWith.Name) && this.Code.Equals(compareWith.Code))
                    return true;
                
            }
            return false;
        }
        public override int GetHashCode()
        {
            return Id;
        }
        public override String ToString()
        {
            return Name;
        }
        public bool UseCents { get; set; }
    }
}
