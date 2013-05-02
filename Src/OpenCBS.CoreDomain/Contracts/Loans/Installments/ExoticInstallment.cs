// LICENSE PLACEHOLDER

using System;

namespace OpenCBS.CoreDomain.Contracts.Loans.Installments
{
    /// <summary>
    /// Summary description for ExoticInstallment.
    /// </summary>
    [Serializable]
    public class ExoticInstallment
    {
        public ExoticInstallment()
        {}

        public ExoticInstallment(int pNumber, double pPincipalCoeff, double? pInterestCoeff)
        {
            Number = pNumber;
            PrincipalCoeff = pPincipalCoeff;
            InterestCoeff = pInterestCoeff;
        }

        public int Number { get; set; }
        public double PrincipalCoeff { get; set; }

        /// <summary>
        /// the interests coefficient refers to the interests to pay for one period and not the total interests
        /// </summary>
        public double? InterestCoeff { get; set; }
    }
}
