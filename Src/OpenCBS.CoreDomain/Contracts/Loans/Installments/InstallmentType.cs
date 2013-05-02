// LICENSE PLACEHOLDER

using System;

namespace OpenCBS.CoreDomain.Contracts.Loans.Installments
{
    [Serializable]
	public class InstallmentType
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public int NbOfDays { get; set; }
        public int NbOfMonths { get; set; }

		public InstallmentType(){}

		public InstallmentType(int id, string name, int nbOfDays, int nbOfMonths)
		{
            Id = id;
            Name = name;
            NbOfDays = nbOfDays;
            NbOfMonths = nbOfMonths;
		}

		public InstallmentType(string name, int nbOfDays, int nbOfMonths)
		{
            Name = name;
            NbOfDays = nbOfDays;
            NbOfMonths = nbOfMonths;
		}

		public override string ToString()
		{
			return Name;
		}

        public override int GetHashCode()
        {
            return Id;
        }

        public override bool Equals(object obj)
        {
            InstallmentType it = obj as InstallmentType;
            if (null == it) return false;

            return it.Id == Id;
        }
	}
}
