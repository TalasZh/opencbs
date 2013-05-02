//Octopus MFS is an integrated suite for managing a Micro Finance Institution: clients, contracts, accounting, reporting and risk
//Copyright � 2006,2007 OCTO Technology & OXUS Development Network
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
using System.Data.SqlClient;
using System.Threading;
using OpenCBS.CoreDomain.Clients;
using OpenCBS.CoreDomain.Contracts.Loans;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.CoreDomain.LoanCycles;
using OpenCBS.CoreDomain.Products;
using OpenCBS.DatabaseConnection;
using OpenCBS.Enums;
using OpenCBS.Manager;
using OpenCBS.CoreDomain;
using OpenCBS.ExceptionsHandler;
using OpenCBS.Manager.Products;
using OpenCBS.MultiLanguageRessources;
using OpenCBS.Services.Currencies;
using OpenCBS.Shared.Settings;

namespace OpenCBS.Services
{
	/// <summary>
	/// Description r�sum�e de ProductServices.
	/// </summary>
    public class ProductServices : MarshalByRefObject
	{
		private LoanProductManager _productManager;
	    private FundingLineManager _fundingLineManager;
		private InstallmentTypeManager _installmentTypeManager;
        private User _user = new User();
	    private int _clientTypeCheckBoxCounter = 1;

	    public ProductServices(InstallmentTypeManager installmentTypeManager)
		{
			_installmentTypeManager = installmentTypeManager;
		}

        public ProductServices(LoanProductManager productManager)
		{
			_productManager = productManager;
            _productManager.ProductLoaded += ProductLoaded;
		}
        public ProductServices(FundingLineManager fundingLineManager)
        {
            _fundingLineManager = fundingLineManager;
            _productManager.ProductLoaded += ProductLoaded;
        }

        public ProductServices(User user)
        {
            _user = user;
            _productManager = new LoanProductManager(user);
            _installmentTypeManager = new InstallmentTypeManager(user);
            _fundingLineManager = new FundingLineManager(user);
            _productManager.ProductLoaded += ProductLoaded;
        }

		public int AddPackage(LoanProduct package)
		{
            if (package.UseLoanCycle)
                SetDefaultValues(package);
            int packageId = _productManager.Add(package);
            _productManager.InsertEntryFees(package.EntryFees, packageId);
		   
		    return packageId;
		}

	    private static void SetDefaultValues(LoanProduct pPackage)
	    {
	        if (!pPackage.Amount.HasValue && 
                !pPackage.AmountMin.HasValue && 
                !pPackage.AmountMax.HasValue)
	            {
                    pPackage.Amount = 0;
	            }
	            
	        if (!pPackage.NbOfInstallmentsMin.HasValue && 
	            !pPackage.NbOfInstallmentsMax.HasValue &&
	            !pPackage.NbOfInstallments.HasValue
	            )
	            {
                    pPackage.NbOfInstallments = 0;
	            }
	                
	        if (!pPackage.InterestRate.HasValue &&
	            !pPackage.InterestRateMin.HasValue &&
	            !pPackage.InterestRateMax.HasValue)
	        {
	            pPackage.InterestRate = 0;
	        }
	    }

	    public List<CycleObject> GetCycleObjects()
        {
	        var cycleObjects = _productManager.SelectCycleObjects();
            foreach (var cycleObject in cycleObjects)
            {
                cycleObject.Name = MultiLanguageStrings.GetString(Ressource.FrmAddLoanProduct, cycleObject.Name + ".Text");;
            }
            return cycleObjects;
        }

        public List<EntryFee> GetProductEntryFees(LoanProduct product, IClient client)
        {
            if (product.UseEntryFeesCycles)
            {
                int loanCycle = _productManager.SelectSuitableEntryFeeCycle(product.Id, client.LoanCycle+1);
                return _productManager.SelectEntryFeesAccordingCycle(product.Id, loanCycle, false);
            }
            else
                return _productManager.SelectEntryFeesWithoutCycles(product.Id, false);
        }

        public List<MaturityCycle> GetMaturityCycleParams(int productId, int cycleId)
        {
            return _productManager.SelectMaturityCycleParams(cycleId);
        }

        public List<RateCycle> GetInterestRateCycleParams(int cycleId)
        {
            List<RateCycle> rateCycles= _productManager.SelectRateCycleParams(cycleId);
            return rateCycles;
        }

        public List<LoanAmountCycle> GetLoanAmountCycleParameters(int cycleId)
        {
           return _productManager.SelectLoanAmountCycleParams(cycleId);
        }

        public void SaveAllCycleParams(List<LoanAmountCycle> loanAmountCycles,
                         List<RateCycle> rateCycles, List<MaturityCycle> maturityCycles)
        {
            CheckCycleParams(loanAmountCycles, rateCycles, maturityCycles);
            _productManager.DeleteCycles(loanAmountCycles[0].CycleObjectId, (int)loanAmountCycles[0].CycleId);
            _productManager.DeleteCycles(rateCycles[0].CycleObjectId, (int)rateCycles[0].CycleId);
            _productManager.DeleteCycles(maturityCycles[0].CycleObjectId, (int)maturityCycles[0].CycleId);

            foreach (RateCycle cycle in rateCycles)
            {
                cycle.Min = cycle.Min/100;
                cycle.Max = cycle.Max/100;
            }

            _productManager.InsertLoanAmountCycleParams(loanAmountCycles);
            _productManager.InsertRateCycleParams(rateCycles);
            _productManager.InsertMaturityCycleParams(maturityCycles);
        }

        public void SetCyclesParamsForContract(LoanProduct product, Loan credit, Client client, bool isContractForCreation)
        {
            if (isContractForCreation)
            {
                if (product.LoanAmountCycleParams == null
                    || product.RateCycleParams == null
                    || product.MaturityCycleParams == null)
                {
                    product.LoanAmountCycleParams =
                        ServicesProvider.GetInstance().GetProductServices().GetLoanAmountCycleParameters((int)product.CycleId);
                    product.RateCycleParams =
                        ServicesProvider.GetInstance().GetProductServices().GetInterestRateCycleParams((int)product.CycleId);
                    product.MaturityCycleParams =
                        ServicesProvider.GetInstance().GetProductServices().GetMaturityCycleParams(product.Id, (int)product.CycleId);
                }

                if (product.LoanAmountCycleParams.Count != 0 &&
                    product.RateCycleParams.Count != 0 &&
                    product.MaturityCycleParams.Count != 0)
                {
                    SetLoanAmountCycle(product, client.LoanCycle);
                    SetRateCycle(product, client.LoanCycle);
                    SetMaturityCycle(product, client.LoanCycle);
                }
            }
            else if (credit != null)
            {
                product.AmountMin = credit.AmountMin;
                product.AmountMax = credit.AmountMax;
                product.InterestRateMin = credit.InterestRateMin;
                product.InterestRateMax = credit.InterestRateMax;
                product.NbOfInstallmentsMin = credit.NmbOfInstallmentsMin;
                product.NbOfInstallmentsMax = credit.NmbOfInstallmentsMax;
            }
        }

        private void SetMaturityCycle(LoanProduct product, int loanCycle)
        {
            List<MaturityCycle> maturityCycles = product.MaturityCycleParams;
            for (int i = 0; i < maturityCycles.Count; i++)
            {
                if (i == maturityCycles.Count - 1)
                {
                    product.NbOfInstallmentsMin = (int)maturityCycles[i].Min.Value;
                    product.NbOfInstallmentsMax = (int)maturityCycles[i].Max.Value;
                    break;
                }
                if (maturityCycles[i].LoanCycle == loanCycle)
                {
                    product.NbOfInstallmentsMin = (int)maturityCycles[i].Min.Value;
                    product.NbOfInstallmentsMax = (int)maturityCycles[i].Max.Value;
                    break;
                }
            }
        }

        private void SetRateCycle(LoanProduct product, int loanCycle)
        {
            List<RateCycle> rateCycles = product.RateCycleParams;
            for (int i = 0; i < product.RateCycleParams.Count; i++)
            {
                if (i == rateCycles.Count - 1)
                {
                    product.InterestRateMin = rateCycles[i].Min.Value;
                    product.InterestRateMax = rateCycles[i].Max.Value;
                    break;
                }
                if (rateCycles[i].LoanCycle == loanCycle)
                {
                    product.InterestRateMin = rateCycles[i].Min.Value;
                    product.InterestRateMax = rateCycles[i].Max.Value;
                    break;
                }
            }
        }

        private void SetLoanAmountCycle(LoanProduct product, int loanCycle)
        {
            List<LoanAmountCycle> loanAmountCycleParams = product.LoanAmountCycleParams;
            for (int i = 0; i < loanAmountCycleParams.Count; i++)
            {
                if (i == loanAmountCycleParams.Count - 1)
                {
                    product.AmountMin = loanAmountCycleParams[i].Min;
                    product.AmountMax = loanAmountCycleParams[i].Max;
                    break;
                }
                if (loanAmountCycleParams[i].LoanCycle == loanCycle)
                {
                    product.AmountMin = loanAmountCycleParams[i].Min;
                    product.AmountMax = loanAmountCycleParams[i].Max;
                    break;
                }
            }
        }

        /// <summary>
        /// The method sets to the village member cycle parameters such as 
        /// loan amount, rate and number of installments 
        /// according to the loan cycle
        /// </summary>
        /// <param name="member">Member of the NonSolidaryGroup</param>
        /// <param name="productId">Product id</param>
        /// <param name="loanCycle">LoanCycle of the member</param>
        public void SetVillageMemberCycleParams(VillageMember member, int productId, int loanCycle)
        {
            member.Product = ServicesProvider.GetInstance().GetProductServices().FindPackage(productId);
            if (member.Product.LoanAmountCycleParams.Count != 0 &&
                member.Product.RateCycleParams.Count != 0 &&
                member.Product.MaturityCycleParams.Count != 0)
            {
                SetLoanAmountCycle(member, loanCycle);
                SetRateCycle(member, loanCycle);
                SetMaturityCycle(member, loanCycle);
            }
        }

        /// <summary>
        /// The method sets number of installments limitation 
        /// for the village member according to the loan cycle
        /// </summary>
        /// <param name="member">Member of the NonSolidaryGroup</param>
        /// <param name="loanCycle">LoanCycle of the member</param>
        private void SetMaturityCycle(VillageMember member, int loanCycle)
        {
            List<MaturityCycle> maturityCycles = member.Product.MaturityCycleParams;
            member.Product.NbOfInstallments = null;
            for (int i = 0; i < maturityCycles.Count; i++)
            {
                if (i == maturityCycles.Count - 1)
                {
                    member.Product.NbOfInstallmentsMin = (int)maturityCycles[i].Min.Value;
                    member.Product.NbOfInstallmentsMax = (int)maturityCycles[i].Max.Value;
                    break;
                }
                if (maturityCycles[i].LoanCycle == loanCycle)
                {
                    member.Product.NbOfInstallmentsMin = (int)maturityCycles[i].Min.Value;
                    member.Product.NbOfInstallmentsMax = (int)maturityCycles[i].Max.Value;
                    break;
                }
            }
        }

        /// <summary>
        /// The method sets interest rate limitation 
        /// for the village member according to the loan cycle
        /// </summary>
        /// <param name="member">Member of the NonSolidaryGroup</param>
        /// <param name="loanCycle">LoanCycle of the member</param>
        private void SetRateCycle(VillageMember member, int loanCycle)
        {
            List<RateCycle> rateCycles = member.Product.RateCycleParams;
            member.Product.InterestRate = null;
            for (int i = 0; i < rateCycles.Count; i++)
            {
                if (i == rateCycles.Count - 1)
                {
                    member.Product.InterestRateMin = rateCycles[i].Min.Value;
                    member.Product.InterestRateMax = rateCycles[i].Max.Value;
                    break;
                }
                if (rateCycles[i].LoanCycle == loanCycle)
                {
                    member.Product.InterestRateMin = rateCycles[i].Min.Value;
                    member.Product.InterestRateMax = rateCycles[i].Max.Value;
                    break;
                }
            }
        }

        /// <summary>
        /// The method sets loan amount limitation 
        /// for the village member according to the loan cycle
        /// </summary>
        /// <param name="member">VillageMember</param>
        /// <param name="loanCycle">Value of the loan cycle</param>
        private void SetLoanAmountCycle(VillageMember member, int loanCycle)
        {
            List<LoanAmountCycle> loanAmountCycleParams = member.Product.LoanAmountCycleParams;
            member.Product.Amount = null;
            for (int i = 0; i < loanAmountCycleParams.Count; i++)
            {
                if (i == loanAmountCycleParams.Count - 1)
                {
                    member.Product.AmountMin = loanAmountCycleParams[i].Min;
                    member.Product.AmountMax = loanAmountCycleParams[i].Max;
                    break;
                }
                if (loanAmountCycleParams[i].LoanCycle == loanCycle)
                {
                    member.Product.AmountMin = loanAmountCycleParams[i].Min;
                    member.Product.AmountMax = loanAmountCycleParams[i].Max;
                    break;
                }
            }
        }

        public void CheckIfEntryFeeCycleExists(LoanProduct product, int newCycle)
        {
            foreach (int cycle in product.EntryFeeCycles)
            {
                if (cycle==newCycle)
                    throw new OctopusPackageSaveException(OctopusPackageSaveExceptionEnum.CycleAlreadyExists);
            }
        }

	    public void CheckCycleParams(List<LoanAmountCycle> loanAmountCycles, List<RateCycle> rateCycles, List<MaturityCycle> maturityCycles)
	    {
	        if (loanAmountCycles==null || rateCycles == null || maturityCycles==null)
                throw new OctopusPackageSaveException(OctopusPackageSaveExceptionEnum.CycleParametersAreNotFilled);

            if (loanAmountCycles.Count==0 || rateCycles.Count==0 || maturityCycles.Count==0)
                throw new OctopusPackageSaveException(OctopusPackageSaveExceptionEnum.CycleParametersAreNotFilled);

	        foreach (var rateCycle in rateCycles)
	        {
	            bool result = ServicesHelper.CheckIfValueBetweenMinAndMaxOrValuesAreEqual(rateCycle.Min.Value,
	                                                                                      rateCycle.Max.Value);
                if (!result)
                    throw  new OctopusPackageSaveException(OctopusPackageSaveExceptionEnum.CycleParametersHaveBeenFilledIncorrectly);
	        }

	        foreach (var amountCycle in loanAmountCycles)
	        {
	            bool result = ServicesHelper.CheckIfValueBetweenMinAndMaxOrValuesAreEqual(amountCycle.Min.Value, amountCycle.Max.Value);
	            if (!result)
	                throw new OctopusPackageSaveException((OctopusPackageSaveExceptionEnum.CycleParametersHaveBeenFilledIncorrectly));
                if (amountCycle.Min == 0 || amountCycle.Max==0)
                    throw new  OctopusPackageSaveException((OctopusPackageSaveExceptionEnum.AmountCanNotBeZero));
	        }

            foreach (var maturityCycle in maturityCycles)
	        {
	            bool result=ServicesHelper.CheckIfValueBetweenMinAndMaxOrValuesAreEqual(maturityCycle.Min.Value, maturityCycle.Max.Value);
                if (!result)
                    throw new OctopusPackageSaveException(OctopusPackageSaveExceptionEnum.CycleParametersHaveBeenFilledIncorrectly);
	            if (maturityCycle.Min==0 || maturityCycle.Max==0)
	            {
	                throw new OctopusPackageSaveException(OctopusPackageSaveExceptionEnum.MaturityCanNotBeZero);
	            }
	        }

	    }

	    public EntryFee GetEntryFeeById (int entryFeeId)
        {
          return  _productManager.SelectEntryFeeById(entryFeeId);
        }

        public void UpdatePackage(LoanProduct pPackage, bool updateContracts)
        {
            _productManager.UpdatePackage(pPackage, updateContracts);
            _productManager.DeleteEntryFees(pPackage);
            _productManager.InsertEntryFees(pPackage.EntryFees, pPackage.Id);
        }

		public int AddExoticProduct(ExoticInstallmentsTable pExoticProduct, OLoanTypes loanType)
		{
            if (_productManager.IsThisExoticProductNameAlreadyExist(pExoticProduct.Name))
				throw new OctopusPackageSaveException(OctopusPackageSaveExceptionEnum.ExoticProductNameAlreadyExist);
            if(!pExoticProduct.CheckIfSumIsOk(loanType))
                throw new OctopusPackageSaveException(OctopusPackageSaveExceptionEnum.ExoticProductSumInIncorrect);
			
            pExoticProduct.Id = _productManager.AddExoticInstallmentsTable(pExoticProduct);

            foreach (ExoticInstallment installment in pExoticProduct)
            {
                _productManager.AddExoticInstallment(installment, pExoticProduct);
            }

		    return pExoticProduct.Id;
		}

        public int AddInstallmentType(InstallmentType pInstallmentType)
        {
            List<InstallmentType> list = _installmentTypeManager.SelectAllInstallmentTypes();
            foreach (InstallmentType type in list)
            {
                if(type.Name.ToLower() == pInstallmentType.Name.ToLower())
                    throw new OctopusPackageSaveException(OctopusPackageSaveExceptionEnum.InstallmentTypeNameAlreadyExist);
                if(type.NbOfDays == pInstallmentType.NbOfDays && type.NbOfMonths == pInstallmentType.NbOfMonths)
                    throw new OctopusPackageSaveException(OctopusPackageSaveExceptionEnum.InstallmentTypeValuesAlreadyExist);
            }
            return _installmentTypeManager.AddInstallmentType(pInstallmentType);
        }

        public void EditInstallmentType(InstallmentType pInstallmentType)
        {
            _installmentTypeManager.EditInstallmentType(pInstallmentType);
        }

        public void DeleteInstallmentType(InstallmentType pInstallmentType)
        {
            if (_installmentTypeManager.NumberOfLinksToInstallmentType(pInstallmentType) > 0)
                throw new OctopusPackageSaveException(OctopusPackageSaveExceptionEnum.InstallmentTypeValuesAreUsed);
            
            _installmentTypeManager.DeleteInstallmentType(pInstallmentType);
        }

	    public int InsertLoanCycle(LoanCycle loanCycle)
        {
            if (_productManager.IsLoanCycleNameAlreadyExist(loanCycle.Name))
                    throw new OctopusPackageSaveException(OctopusPackageSaveExceptionEnum.LoanCycleNameAlreadyExists);
            using (SqlConnection conn = _productManager.GetConnection())
            using (SqlTransaction t = conn.BeginTransaction())
            {
                try
                {
                    loanCycle.Id = _productManager.InsertLoanCycle(loanCycle, t);
                    t.Commit();
                    return loanCycle.Id;
                }
                catch (Exception ex)
                {
                    t.Rollback();
                    throw ex;
                }
            }
        }


		public LoanProduct FindPackage(int idPackage)
		{
		    try
		    {
                LoanProduct package = _productManager.Select(idPackage);
                if (package == null) return null;
                if (package.FundingLine != null)
                    package.FundingLine = _fundingLineManager.SelectFundingLineById(package.FundingLine.Id, false);
                if (package.CycleId != null)
                {
                    package.LoanAmountCycleParams = _productManager.SelectLoanAmountCycleParams((int)package.CycleId);
                    package.RateCycleParams = _productManager.SelectRateCycleParams((int)package.CycleId);
                    package.MaturityCycleParams = _productManager.SelectMaturityCycleParams((int)package.CycleId);
                }
                GetEntryFees(package);
                return package;
		    }
		    catch (Exception exception)
		    {
		        throw exception;
		    }
		}

	    public void GetEntryFees(LoanProduct package)
	    {
	        if (!package.UseEntryFeesCycles)
	            package.EntryFees = _productManager.SelectEntryFeesWithoutCycles(package.Id, false);
	        else
	        {
	            package.EntryFeeCycles = _productManager.SelectEntryFeeCycles(package.Id, false);
	            package.EntryFees = _productManager.SelectEntryFeesWithCycles(package.Id, false);
	        }
	    }

	    public LoanProduct FindProductByName(string name)
        {
            LoanProduct product = _productManager.SelectByName(name);
            if (product == null) return null;
            if(product.FundingLine != null)
		        product.FundingLine = _fundingLineManager.SelectFundingLineById(product.FundingLine.Id, false);
            if (product.UseEntryFeesCycles)
            {
                product.EntryFeeCycles = _productManager.SelectEntryFeeCycles(product.Id, false);
                product.EntryFees = _productManager.SelectEntryFeesWithCycles(product.Id, false);
            }
            else
            {
                product.EntryFees = _productManager.SelectEntryFeesWithoutCycles(product.Id, false);
            }
		    return product;
        }

		public bool DeletePackage(LoanProduct package)
		{
			if(package == null)
				throw new OctopusPackageDeleteException(OctopusPackageDeleteExceptionEnum.PackageIsNull);

			if(package.Id == 0)
				throw new OctopusPackageDeleteException(OctopusPackageDeleteExceptionEnum.PackageIsNull);

			if(package.Delete)
				throw new OctopusPackageDeleteException(OctopusPackageDeleteExceptionEnum.AlreadyDeleted);

			_productManager.DeleteProduct(package.Id);
			
            return true;
        }

        public List<LoanProduct> FindAllPackages(bool selectDeleted,OClientTypes pClientType)
        {
            List<LoanProduct> retval = _productManager.SelectAllPackages(selectDeleted,pClientType);
            foreach (LoanProduct product in retval)
            {
                if (null == product.FundingLine) continue;
                product.FundingLine.Currency = new CurrencyServices(_user).GetCurrency(product.FundingLine.Currency.Id);

                if (product.UseEntryFeesCycles)
                {
                    product.EntryFeeCycles = _productManager.SelectEntryFeeCycles(product.Id, false);
                    product.EntryFees = _productManager.SelectEntryFeesWithCycles(product.Id, false);
                }
                else
                {
                    product.EntryFees = _productManager.SelectEntryFeesWithoutCycles(product.Id, false);
                }
            }
            return retval;
        }	

		public List<InstallmentType> FindAllInstallmentTypes()
		{
			return _installmentTypeManager.SelectAllInstallmentTypes();
		}

        public InstallmentType FindInstallmentType(int pId)
        {
            return _installmentTypeManager.SelectInstallmentType(pId);
        }

        public InstallmentType FindInstallmentTypeByName(string name)
        {
            return _installmentTypeManager.SelectInstallmentTypeByName(name);
        }

	    public List<ExoticInstallmentsTable> FindAllExoticProducts()
		{
			return _productManager.SelectAllInstallmentsTables(); 
		}

		public ExoticInstallmentsTable AddExoticInstallmentInExoticProduct(ExoticInstallmentsTable product,ExoticInstallment exoticInstallment, int installmentCount, bool declining) //fait
		{
			if (!declining)
			{
				if(exoticInstallment.PrincipalCoeff == 0 && !exoticInstallment.InterestCoeff.HasValue)
					throw new OctopusPackageSaveException(OctopusPackageSaveExceptionEnum.ExoticInstallmentIsNull);
			}

			exoticInstallment.Number = installmentCount;
			product.Add(exoticInstallment);
			return product;
		}

		public bool CheckInstallmentValues(string principalCoeff,string interestCoeff,string correctionCoeff)
		{
			if (ServicesHelper.CheckIfDouble(principalCoeff) && ServicesHelper.CheckIfDouble(interestCoeff) &&
				ServicesHelper.CheckIfDouble(correctionCoeff))
			{
				return true;
			}
			return false;
		}

		private void CheckValueForPackageName(string pName)
		{
            if (string.IsNullOrEmpty(pName))
				throw new OctopusPackageSaveException(OctopusPackageSaveExceptionEnum.NameIsNull);
            
            if (_productManager.IsThisProductNameAlreadyExist(pName))
				throw new OctopusPackageSaveException(OctopusPackageSaveExceptionEnum.NameAlreadyExist);
		}

		private static void CheckValueForPackageInstallmentType(InstallmentType pInstallmentType)
		{
            if (pInstallmentType == null)
				throw new OctopusPackageSaveException(OctopusPackageSaveExceptionEnum.InstallmentTypeIsNull);
            
            if (pInstallmentType.Id == 0)
				throw new OctopusPackageSaveException(OctopusPackageSaveExceptionEnum.InstallmentTypeIsBad);
		}

        private static void CheckValueForPackageInterestRate(LoanProduct pPackage)
		{
            if (pPackage.UseLoanCycle==false)
            if (!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(pPackage.InterestRateMin, pPackage.InterestRateMax, pPackage.InterestRate))
				throw new OctopusPackageSaveException(OctopusPackageSaveExceptionEnum.InterestRateGroupBadlyInformed);
		}

        private static void CheckValueForPackageGracePeriod(LoanProduct pPackage)
		{
            if (!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(pPackage.GracePeriodMin, pPackage.GracePeriodMax, pPackage.GracePeriod))
				throw new OctopusPackageSaveException(OctopusPackageSaveExceptionEnum.GracePeriodGroupBadlyInformed);
		}

        private static void CheckValueForPackageNonRepaymentPenalties(LoanProduct pPackage)
		{
            if (!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(pPackage.NonRepaymentPenaltiesMin.InitialAmount, pPackage.NonRepaymentPenaltiesMax.InitialAmount, pPackage.NonRepaymentPenalties.InitialAmount))
                throw new OctopusPackageSaveException(OctopusPackageSaveExceptionEnum.NonRepaymentPenaltiesBadlyInformed);

            if (!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(pPackage.NonRepaymentPenaltiesMin.OLB, pPackage.NonRepaymentPenaltiesMax.OLB, pPackage.NonRepaymentPenalties.OLB))
                throw new OctopusPackageSaveException(OctopusPackageSaveExceptionEnum.NonRepaymentPenaltiesBadlyInformed);

            if (!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(pPackage.NonRepaymentPenaltiesMin.OverDueInterest, pPackage.NonRepaymentPenaltiesMax.OverDueInterest, pPackage.NonRepaymentPenalties.OverDueInterest))
                throw new OctopusPackageSaveException(OctopusPackageSaveExceptionEnum.NonRepaymentPenaltiesBadlyInformed);

            if (!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(pPackage.NonRepaymentPenaltiesMin.OverDuePrincipal, pPackage.NonRepaymentPenaltiesMax.OverDuePrincipal, pPackage.NonRepaymentPenalties.OverDuePrincipal))
                throw new OctopusPackageSaveException(OctopusPackageSaveExceptionEnum.NonRepaymentPenaltiesBadlyInformed);
		}

        private static void CheckValueForPackageAnticipatedTotalRepaymentPenalties(LoanProduct pPackage)
		{
            if (!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(pPackage.AnticipatedTotalRepaymentPenaltiesMin,pPackage.AnticipatedTotalRepaymentPenaltiesMax, pPackage.AnticipatedTotalRepaymentPenalties))
				throw new OctopusPackageSaveException(OctopusPackageSaveExceptionEnum.AnticipatedRepaymentPenaltiesBadlyInformed);
		}

        private static void CheckValuesForPackageAnticipatedPartialRepaymentPenalties(LoanProduct product)
        {
            if (!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(product.AnticipatedPartialRepaymentPenaltiesMin, product.AnticipatedPartialRepaymentPenaltiesMax, product.AnticipatedPartialRepaymentPenalties))
                throw new OctopusPackageSaveException(OctopusPackageSaveExceptionEnum.AnticipatedRepaymentPenaltiesBadlyInformed);
        }

        private static void CheckValueForLineOfCredit(LoanProduct pPackage)
        {
            if ((pPackage.AmountUnderLoc == null && pPackage.AmountUnderLocMin == null && pPackage.AmountUnderLocMax == null) || (pPackage.MaturityLoc == null && pPackage.MaturityLocMin == null && pPackage.MaturityLocMax == null))
                throw  new OctopusPackageSaveException(OctopusPackageSaveExceptionEnum.LOCFieldsAreNotFilled);
            if (!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(pPackage.AmountUnderLocMin, pPackage.AmountUnderLocMax, pPackage.AmountUnderLoc))
                throw new OctopusPackageSaveException(OctopusPackageSaveExceptionEnum.LOCAmountHaveBeenFilledIncorrectly);
            if (!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(pPackage.MaturityLocMin, pPackage.MaturityLocMax, pPackage.MaturityLoc))
                throw new OctopusPackageSaveException(OctopusPackageSaveExceptionEnum.LOCMaturityHaveBeenFilledIncorrectly);
        }

        private static void CheckValueForPackageEntryFees(LoanProduct pPackage)
		{
            if (pPackage.EntryFees == null) 
                return;
            
            foreach (EntryFee entryFee in pPackage.EntryFees)
            {
                if (!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(entryFee.Min, entryFee.Max, entryFee.Value))
                    throw new OctopusPackageSaveException(OctopusPackageSaveExceptionEnum.EntryFeesBadlyInformed);
            }
		}

        private static void CheckCumpolsorySavingSettings(LoanProduct pPackage)
        {
            if (!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(pPackage.CompulsoryAmountMin, pPackage.CompulsoryAmountMax, pPackage.CompulsoryAmount))
                throw new OctopusPackageSaveException(OctopusPackageSaveExceptionEnum.CompulsorySavingSettingsEmpty);
        }

        private void CheckValueForPackageNumberOfInstallments(LoanProduct pPackage)
        {
            if (pPackage.UseLoanCycle) return;
            if (!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(pPackage.NbOfInstallmentsMin, pPackage.NbOfInstallmentsMax, pPackage.NbOfInstallments))
                throw new OctopusPackageSaveException(OctopusPackageSaveExceptionEnum.NumberOfInstallmentBadlyInformed);

            if (pPackage.NbOfInstallmentsMin != null)
                if (pPackage.NbOfInstallmentsMin > ApplicationSettings.GetInstance(_user != null ? _user.Md5 : "").MaxNumberInstallment)
                {
                    throw new OctopusPackageSaveException(OctopusPackageSaveExceptionEnum.NumberOfInstallmentBadlyInformed);
                }

            if (pPackage.NbOfInstallmentsMax != null)
                if (pPackage.NbOfInstallmentsMax > ApplicationSettings.GetInstance(_user != null ? _user.Md5 : "").MaxNumberInstallment)
                {
                    throw new OctopusPackageSaveException(OctopusPackageSaveExceptionEnum.NumberOfInstallmentBadlyInformed);
                }
            int maxInstallments = ApplicationSettings.GetInstance(_user != null ? _user.Md5 : "").MaxNumberInstallment;
            if (pPackage.NbOfInstallments != null)
                if (pPackage.NbOfInstallments > ApplicationSettings.GetInstance(_user != null ? _user.Md5 : "").MaxNumberInstallment)
                {
                    throw new OctopusPackageSaveException(OctopusPackageSaveExceptionEnum.NumberOfInstallmentBadlyInformed);
                }
        }

        private static void CheckValueForPackageAmount(LoanProduct pPackage)
		{
            if (pPackage.UseLoanCycle)return;
            if(!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(pPackage.AmountMin,pPackage.AmountMax, pPackage.Amount))
				throw new OctopusPackageSaveException(OctopusPackageSaveExceptionEnum.AmountBadlyInformed);
		}

        private static void CheckValueForCreditInsuranceValues(LoanProduct pPackage)
        {
            if (pPackage.CreditInsuranceMin>pPackage.CreditInsuranceMax)
            {
                throw  new OctopusPackageSaveException(OctopusPackageSaveExceptionEnum.InsuranceBadlyFilled);
            }
        }

        private static void CheckValueForPackageExoticProduct(ExoticInstallmentsTable exoticProduct)
		{
			if(exoticProduct == null)
				throw new OctopusPackageSaveException(OctopusPackageSaveExceptionEnum.ExoticProductIsNull);
			if(exoticProduct.Id == 0)
				throw new OctopusPackageSaveException(OctopusPackageSaveExceptionEnum.ExoticProductIsBad);
		}

        public void ParseFieldsAndCheckErrors(LoanProduct pPackage, bool pUseExoticProduct, int clientTypeCheckBoxCounter)
        {
            this._clientTypeCheckBoxCounter = clientTypeCheckBoxCounter;
            ParseFieldsAndCheckErrors(pPackage, pUseExoticProduct);
        }

		public void ParseFieldsAndCheckErrors(LoanProduct pPackage, bool pUseExoticProduct)
		{
            if (pPackage.PackageMode == OPackageMode.Insert)
                CheckValueForPackageName(pPackage.Name);

            if(string.IsNullOrEmpty(pPackage.Code))
                throw new OctopusPackageSaveException(OctopusPackageSaveExceptionEnum.CodeIsEmpty);

            if (pPackage.Currency==null)
                throw new OctopusPackageSaveException(OctopusPackageSaveExceptionEnum.CurrencyIsEmpty);
            if (pPackage.Currency.Id == 0)
                throw new OctopusPackageSaveException(OctopusPackageSaveExceptionEnum.CurrencyIsEmpty);
            if(pPackage.FundingLine!=null)
                if(!pPackage.Currency.Equals(pPackage.FundingLine.Currency))
                    throw new OctopusPackageSaveException(OctopusPackageSaveExceptionEnum.CurrencyMisMatch);
            if (_clientTypeCheckBoxCounter<=0)
                throw new OctopusPackageSaveException(OctopusPackageSaveExceptionEnum.ClientTypeIsEmpty);
		    CheckValueForPackageAmount(pPackage);
            CheckValueForPackageInstallmentType(pPackage.InstallmentType);
            CheckValueForPackageInterestRate(pPackage);
            CheckValueForPackageNonRepaymentPenalties(pPackage);
            CheckValueForPackageAnticipatedTotalRepaymentPenalties(pPackage);
		    CheckValuesForPackageAnticipatedPartialRepaymentPenalties(pPackage);
            CheckValueForPackageEntryFees(pPackage);
            CheckValueForPackageGracePeriod(pPackage);
            CheckValueForPackageNumberOfInstallments(pPackage);
            CheckValueForFundingLine(pPackage);
		    CheckValueForCreditInsuranceValues(pPackage);
            if (pPackage.GracePeriodOfLateFees == null)
                throw new OctopusPackageSaveException(OctopusPackageSaveExceptionEnum.GracePeriodOfLateFeesIsNotFilled);

            if (!pPackage.ActivatedLOC)
            {
                pPackage.AmountUnderLocMin = null;
                pPackage.AmountUnderLocMax = null;
                pPackage.AmountUnderLoc = null;
                pPackage.MaturityLocMin = null;
                pPackage.MaturityLocMax = null;
                pPackage.MaturityLoc = null;
                pPackage.DrawingsNumber = null;
            }
            else
            {
                CheckValueForLineOfCredit(pPackage);    
            }

            if (pPackage.UseCompulsorySavings)
                CheckCumpolsorySavingSettings(pPackage);
			if(pUseExoticProduct)
                CheckValueForPackageExoticProduct(pPackage.ExoticProduct);
            if (pPackage.UseLoanCycle)
                CheckCycleParams(pPackage.LoanAmountCycleParams, pPackage.RateCycleParams, pPackage.MaturityCycleParams);
		}

        private static void CheckValueForFundingLine(LoanProduct pPackage)
	    {
            if (pPackage.FundingLine == null) return;
	        if (pPackage.FundingLine.Id != 0) return;

	        pPackage.FundingLine = null;
	    }

        public List<LoanCycle> GetLoanCycles()
        {
            return _productManager.SelectLoanCycles();
        }

        public List<ProductClientType> SelectClientTypes()
        {
            return _productManager.SelectClientTypes();
        }

        public void GetAssignedTypes(List<ProductClientType> productClientTypes, int productId)
        {
            _productManager.GetAssignedTypes(productClientTypes, productId);
        }

        public LoanProduct FindProductByCode(string code)
        {
            LoanProduct product = _productManager.SelectByName(code);
            if (product.FundingLine != null)
            {
                product.FundingLine = _fundingLineManager.SelectFundingLineById(product.FundingLine.Id, false);
            }
            product.EntryFees = _productManager.SelectEntryFeesWithoutCycles(product.Id, false);
            return product;
        }

        public void ProductLoaded(LoanProduct product)
        {
            if (null == product) return;
            int id = _productManager.SelectFundingLineId(product.Id);
            if (0 == id) return;
            product.FundingLine = _fundingLineManager.SelectFundingLineById(id, false);
        }
	}
}
