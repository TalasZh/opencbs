//Octopus MFS is an integrated suite for managing a Micro Finance Institution: clients, contracts, accounting, reporting and risk
//Copyright ?2006,2007 OCTO Technology & OXUS Development Network
//
//This program is free software; you can redistribute it and/or modify
//it under the terms of the GNU Lesser General Public License as published by
//the Free Software Foundation; either version 2 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU Lesser General Public License for more details.
//
//You should have received a copy of the GNU Lesser General Public License along
//with this program; if not, write to the Free Software Foundation, Inc.,
//51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
//
// Licence : http://www.octopusnetwork.org/OverviewLicence.aspx
//
// Website : http://www.octopusnetwork.org
// Business contact: business(at)octopusnetwork.org
// Technical contact email : tech(at)octopusnetwork.org 

using System;
using System.Collections.Generic;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment;
using OpenCBS.CoreDomain.FundingLines;
using OpenCBS.CoreDomain.LoanCycles;
using OpenCBS.Enums;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Products
{
	/// <summary>
	/// Description r�sum�e de Package.
    /// </summary>
    [Serializable]
	public class LoanProduct : IProduct
	{
	    private OLoanTypes _loanType;
	    private readonly OProductTypes _productType;
		private OCurrency _amount;
		private OCurrency _amountMin;
		private OCurrency _amountMax;
	    private NonRepaymentPenaltiesNullableValues _nonRepaymentPenalties = new NonRepaymentPenaltiesNullableValues();
        private NonRepaymentPenaltiesNullableValues _nonRepaymentPenaltiesMin = new NonRepaymentPenaltiesNullableValues();
        private NonRepaymentPenaltiesNullableValues _nonRepaymentPenaltiesMax = new NonRepaymentPenaltiesNullableValues();


        private ORoundingType roundingType;
	    private bool _hasValue;

        private List<ProductClientType> productClientTypes = new List<ProductClientType>();

	    public List<ProductClientType> ProductClientTypes
	    {
	        get { return productClientTypes; }
	        set { productClientTypes = value; }
	    }

       public LoanProduct()
	    {
           _productType = OProductTypes.Loan;
	       roundingType = ORoundingType.Approximate;
	       GracePeriodOfLateFees = 0;
           _hasValue = true;
           EntryFeeCycles = new List<int>();
	    }

        public int Id { get; set; }
        public int? GracePeriod { get; set; }
        public int? GracePeriodOfLateFees { get; set; }
        public int? GracePeriodMin { get; set; }
        public int? GracePeriodMax { get; set; }
	    private decimal? _interestRate;
	    public decimal? InterestRate
	    {
            get { return _interestRate; }
            set { _interestRate = value; }
	    }
        public decimal? InterestRateMin { get; set; }
        public decimal? InterestRateMax { get; set; }
        public int? NbOfInstallments { get; set; }
        public int? NbOfInstallmentsMin { get; set; }
        public int? NbOfInstallmentsMax { get; set; }
        public bool UseEntryFeesCycles { get; set; }
        public List<int> EntryFeeCycles { get; set; }

	    public int? CycleId { get; set; }

        public OProductTypes ProductType
	    {
	        get { return _productType;}
	    }

        public FundingLine FundingLine { get; set; }
        public bool AllowFlexibleSchedule { get; set; }
        public Currency Currency { get; set; }
        public ExoticInstallmentsTable ExoticProduct { get; set; }
        public InstallmentType InstallmentType { get; set; }

        public List<LoanAmountCycle> LoanAmountCycleParams { get; set; }
        public List<RateCycle> RateCycleParams { get; set; }
        public List<MaturityCycle> MaturityCycleParams { get; set; }

        public double? AnticipatedTotalRepaymentPenalties { get; set; }
        public double? AnticipatedTotalRepaymentPenaltiesMin { get; set; }
        public double? AnticipatedTotalRepaymentPenaltiesMax { get; set; }

        public double? AnticipatedPartialRepaymentPenalties { get; set; }
        public double? AnticipatedPartialRepaymentPenaltiesMin { get; set; }
        public double? AnticipatedPartialRepaymentPenaltiesMax { get; set; }

        public OPackageMode PackageMode { get; set; }
        public bool Delete { get; set; }

        public OAnticipatedRepaymentPenaltiesBases AnticipatedTotalRepaymentPenaltiesBase { get; set; }
        public OAnticipatedRepaymentPenaltiesBases AnticipatedPartialRepaymentPenaltiesBase { get; set; }

        public bool ChargeInterestWithinGracePeriod { get; set; }
        public string Name { get; set; }
	    public string Code { get; set; }
	    
	    public char ClientType { get; set; }

        public bool KeepExpectedInstallment { get; set; }
        public bool ActivatedLOC { get; set; }

        // Guarantors and collaterals
        public bool UseGuarantorCollateral { get; set; }
        public bool SetSeparateGuarantorCollateral { get; set; }
        public int PercentageTotalGuarantorCollateral { get; set; }
        public int PercentageSeparateGuarantour { get; set; }
        public int PercentageSeparateCollateral { get; set; }

        public int? DrawingsNumber { get; set; }

        public OCurrency AmountUnderLoc { get; set; }
        public OCurrency AmountUnderLocMin { get; set; }
        public OCurrency AmountUnderLocMax { get; set; }

        public int? MaturityLoc { get; set; }
        public int? MaturityLocMin { get; set; }
        public int? MaturityLocMax { get; set; }

        // Compulsory
        public bool UseCompulsorySavings { get; set; }
        public int? CompulsoryAmount { get; set; }
        public int? CompulsoryAmountMin { get; set; }
        public int? CompulsoryAmountMax { get; set; }

	    public NonRepaymentPenaltiesNullableValues NonRepaymentPenalties
        {
            get { return _nonRepaymentPenalties; }
            set { _nonRepaymentPenalties = value; }
        }

        public NonRepaymentPenaltiesNullableValues NonRepaymentPenaltiesMin
        {
            get { return _nonRepaymentPenaltiesMin; }
            set { _nonRepaymentPenaltiesMin = value; }
        }

        public NonRepaymentPenaltiesNullableValues NonRepaymentPenaltiesMax
        {
            get { return _nonRepaymentPenaltiesMax; }
            set { _nonRepaymentPenaltiesMax = value; }
        }

        public bool HasValue
        {
            get { return _hasValue; }
        }

        public ORoundingType RoundingType
		{
            get { return roundingType; }
            set { roundingType = value; }
		}

        public OLoanTypes LoanType
        {
            get { return _loanType; }
            set { _loanType = value; }
        }

	    public OCurrency   Amount
		{
			get { return _amount; }
			set { _amount = value; }
		}

		public OCurrency  AmountMin
		{
			get { return _amountMin; }
			set { _amountMin = value; }
		}

		public OCurrency   AmountMax
		{
			get { return _amountMax; }
			set { _amountMax = value; }
		}

	    public bool UseLoanCycle
	    {
	        get
	        {
                if (CycleId != null) return true;
	            return false;
	        }
	    }

		public bool IsExotic
		{
			get { return ExoticProduct != null; }
		}

		public bool IsDeclining
		{
			get { return _loanType != OLoanTypes.Flat; }
		}

	    public bool UseCents
	    {
	        get
	        {
	            return null == Currency || Currency.UseCents;
	        }
	    }

        public List<EntryFee> EntryFees { get; set; }
        public List<EntryFee> DeletedEntryFees { get; set; }
        public List<EntryFee> AddedEntryFees { get; set; }

        public decimal CreditInsuranceMin { get; set; }
        public decimal CreditInsuranceMax { get; set; }
	}
}