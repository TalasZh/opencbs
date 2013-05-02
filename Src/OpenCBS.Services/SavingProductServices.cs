// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using System.Threading;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.CoreDomain.Products;
using OpenCBS.ExceptionsHandler;
using OpenCBS.Manager;
using OpenCBS.Manager.Products;
using OpenCBS.Enums;

namespace OpenCBS.Services
{
	public class SavingProductServices : MarshalByRefObject
	{
		private readonly SavingProductManager _savingProductManager;
	    private InstallmentTypeManager _installmentTypeManager;
        private User _user;
	    private int _clientTypeCounter = 1;

		public SavingProductServices(SavingProductManager savingProductManager)
		{
			_savingProductManager = savingProductManager;
		}

		public SavingProductServices(User user)
		{
            _user = user;
			_savingProductManager = new SavingProductManager(user);
            _installmentTypeManager = new InstallmentTypeManager(user);
		}

		public SavingProductServices(string testDB)
		{
			_savingProductManager = new SavingProductManager(testDB);
		}

        public List<ISavingProduct> FindAllSavingsProducts(bool showAlsoDeleted, OClientTypes clientType)
        {
            return _savingProductManager.SelectProducts(showAlsoDeleted, clientType);
        }

        public ISavingProduct FindSavingProductById(int id)
        {
            return _savingProductManager.SelectSavingProduct(id);
        }

        public SavingsBookProduct FindSavingBookProductById(int id)
        {
            return _savingProductManager.SelectSavingsBookProduct(id);
        }

       

        public bool IsThisProductAlreadyUsed(int id)
        {
            return _savingProductManager.IsThisProductAlreadyUsed(id);
        }

        public List<ProductClientType> GetAllClientTypes()
        {
            return _savingProductManager.GetAllClientTypes();
        }

        public void GetProductAssignedClientTypes(List<ProductClientType> savingClietTypes, int productId)
        {
            _savingProductManager.GetProductAssignedClientTypes(savingClietTypes, productId);
        }

        public void DeleteSavingProduct(int id)
        {
            _savingProductManager.DeleteSavingProduct(id);
        }

        private static void ValidateInterBranchTransferFees(SavingsBookProduct savingsProduct)
        {
            Fee fee = savingsProduct.InterBranchTransferFee;
            if (fee.IsFlat)
            {
                if (fee.Value.HasValue && fee.Value < 0)
                    throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.InterBranchFlatTransferFeesIsInvalid);

                if (!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(fee.Min, fee.Max, fee.Value))
                    throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.InterBranchFlatTransferFeesMinMaxIsInvalid);

                if (!fee.Value.HasValue && fee.Min < 0)
                    throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.InterBranchFlatTransferFeesMinIsInvalid);
            }
            else
            {
                if (fee.Value.HasValue && fee.Value < 0)
                    throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.InterBranchRateTransferFeesIsInvalid);

                if (!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(fee.Min, fee.Max, fee.Value))
                    throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.InterBranchRateTransferFeesMinMaxIsInvalid);

                if (!fee.Value.HasValue && fee.Value < 0)
                    throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.InterBranchRateTransferFeesMinIsInvalid);
            }
        }

        private static void ValidateSavingBookProduct(SavingsBookProduct savingsProduct)
        {
            if (!Enum.IsDefined(typeof(OSavingInterestFrequency), savingsProduct.InterestFrequency.ToString()))
                throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.InterestsFrequencyIsInvalid);

            if (!Enum.IsDefined(typeof(OSavingInterestBase), savingsProduct.InterestBase.ToString()))
                throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.InterestsBaseIsInvalid);

            if (savingsProduct.InterestBase == OSavingInterestBase.Monthly
                && (savingsProduct.InterestFrequency != OSavingInterestFrequency.EndOfYear 
                && savingsProduct.InterestFrequency != OSavingInterestFrequency.EndOfMonth))
                throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.InterestBaseIncompatibleFrequency);

            if (savingsProduct.InterestBase == OSavingInterestBase.Weekly
                && (savingsProduct.InterestFrequency == OSavingInterestFrequency.EndOfDay))
                throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.InterestBaseIncompatibleFrequency);

            if (savingsProduct.InterestBase != OSavingInterestBase.Daily
                && (!savingsProduct.CalculAmountBase.HasValue))
                throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.CalculAmountBaseIsNull);

            if (!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(savingsProduct.WithdrawingMin, savingsProduct.WithdrawingMax, null))
                    throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.WithdrawAmountIsInvalid);

            if (!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(savingsProduct.DepositMin, savingsProduct.DepositMax, null))
                    throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.DepositAmountIsInvalid);

            if (savingsProduct.DepositMin <= 0)
                throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.DepositMinAmountIsInvalid);
            if (savingsProduct.ChequeDepositMin<=0)
                throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.ChequeDepositIsInvalid);
            if (savingsProduct.ChequeDepositMin>savingsProduct.ChequeDepositMax)
                throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.ChequeDepositIsInvalid);

            if (savingsProduct.WithdrawingMin <= 0)
                throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.WithdrawMinAmountIsInvalid);

            if (!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(savingsProduct.TransferMin, savingsProduct.TransferMax, null))
                throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.TransferAmountIsInvalid);

            if (savingsProduct.DepositFeesMin < 0)
                throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.DepositFeesMinIsInvalid);
            if (savingsProduct.ChequeDepositFeesMin < 0)
                throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.ChequeDepositFeesIsInvalid);
            if (!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(savingsProduct.DepositFeesMin, savingsProduct.DepositFeesMax, savingsProduct.DepositFees))
                throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.DepositFeesIsInvalid);
            if (!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(savingsProduct.ChequeDepositFeesMin, savingsProduct.ChequeDepositFeesMax, savingsProduct.ChequeDepositFees))
                throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.ChequeDepositFeesIsInvalid);

            if (savingsProduct.ReopenFeesMin < 0)
                throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.ReopenFeesMinIsInvalid);
            if (!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(savingsProduct.ReopenFeesMin, savingsProduct.ReopenFeesMax, savingsProduct.ReopenFees))
                throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.ReopenFeesIsInvalid);

            if (savingsProduct.OverdraftFeesMin < 0)
                throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.OverdraftFeesMinIsInvalid);
            if (!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(savingsProduct.OverdraftFeesMin, savingsProduct.OverdraftFeesMax, savingsProduct.OverdraftFees))
                throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.OverdraftFeesIsInvalid);

            if (savingsProduct.AgioFeesMin < 0)
                throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.AgioFeesMinIsInvalid);
            if (!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(savingsProduct.AgioFeesMin, savingsProduct.AgioFeesMax, savingsProduct.AgioFees))
                throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.AgioFeesIsInvalid);

            if (savingsProduct.CloseFeesMin < 0)
                throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.CloseFeesMinIsInvalid);
            if (!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(savingsProduct.CloseFeesMin, savingsProduct.CloseFeesMax, savingsProduct.CloseFees))
                throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.CloseFeesIsInvalid);

            if (savingsProduct.ManagementFeesMin < 0)
                throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.ManagementFeesMinIsInvalid);
            if (!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(savingsProduct.ManagementFeesMin, savingsProduct.ManagementFeesMax, savingsProduct.ManagementFees))
                throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.ManagementFeesIsInvalid);

            if (savingsProduct.TransferMin <= 0)
                throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.TransferAmountMinIsInvalid);

            if (!Enum.IsDefined(typeof(OSavingsFeesType), savingsProduct.WithdrawFeesType.ToString()))
                throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.WithdrawFeesTypeEmpty);

            if (savingsProduct.WithdrawFeesType == OSavingsFeesType.Flat)
            {
                if (savingsProduct.FlatWithdrawFees.HasValue && savingsProduct.FlatWithdrawFees.Value < 0)
                    throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.FlatWithdrawFeesIsInvalid);

                if (!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(savingsProduct.FlatWithdrawFeesMin, savingsProduct.FlatWithdrawFeesMax, savingsProduct.FlatWithdrawFees))
                    throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.FlatWithdrawFeesMinMaxIsInvalid);

                if (!savingsProduct.FlatWithdrawFees.HasValue && savingsProduct.FlatWithdrawFeesMin.Value < 0)
                    throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.FlatWithdrawFeesMinIsInvalid);
            }
            else
            {
                if (savingsProduct.RateWithdrawFees.HasValue && savingsProduct.RateWithdrawFees.Value < 0)
                    throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.RateWithdrawFeesIsInvalid);

                if (!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(savingsProduct.RateWithdrawFeesMin, savingsProduct.RateWithdrawFeesMax, savingsProduct.RateWithdrawFees))
                    throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.RateWithdrawFeesMinMaxIsInvalid);

                if (!savingsProduct.RateWithdrawFees.HasValue && savingsProduct.RateWithdrawFeesMin.Value < 0)
                    throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.RateWithdrawFeesMinIsInvalid);
            }

            if (!Enum.IsDefined(typeof(OSavingsFeesType), savingsProduct.TransferFeesType.ToString()))
                throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.TransferFeesTypeEmpty);

            if (savingsProduct.TransferFeesType == OSavingsFeesType.Flat)
            {
                if (savingsProduct.FlatTransferFees.HasValue && savingsProduct.FlatTransferFees.Value < 0)
                    throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.FlatTransferFeesIsInvalid);

                if (!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(savingsProduct.FlatTransferFeesMin, savingsProduct.FlatTransferFeesMax, savingsProduct.FlatTransferFees))
                    throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.FlatTransferFeesMinMaxIsInvalid);

                if (!savingsProduct.FlatTransferFees.HasValue && savingsProduct.FlatTransferFeesMin.Value < 0)
                    throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.FlatTransferFeesMinIsInvalid);
            }
            else
            {
                if (savingsProduct.RateTransferFees.HasValue && savingsProduct.RateTransferFees.Value < 0)
                    throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.RateTransferFeesIsInvalid);

                if (!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(savingsProduct.RateTransferFeesMin, savingsProduct.RateTransferFeesMax, savingsProduct.RateTransferFees))
                    throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.RateTransferFeesMinMaxIsInvalid);

                if (!savingsProduct.RateTransferFees.HasValue && savingsProduct.RateTransferFeesMin.Value < 0)
                    throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.RateTransferFeesMinIsInvalid);
            }

            ValidateInterBranchTransferFees(savingsProduct);

            if (!savingsProduct.OverdraftFees.HasValue && !(savingsProduct.OverdraftFeesMin.HasValue && savingsProduct.OverdraftFeesMax.HasValue))
                throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.OverdraftFeesIsInvalid);
            if (!savingsProduct.AgioFees.HasValue && !(savingsProduct.AgioFeesMin.HasValue && savingsProduct.AgioFeesMax.HasValue))
                throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.AgioFeesIsInvalid);
            if (savingsProduct.UseTermDeposit)
            {
                if (savingsProduct.TermDepositPeriodMin>savingsProduct.TermDepositPeriodMax)
                    throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.PostingFrequencyIsInvalid);
                if (savingsProduct.TermDepositPeriodMax<=0 || savingsProduct.TermDepositPeriodMin<=0)
                    throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.PostingFrequencyIsInvalid);
                if (savingsProduct.TermDepositPeriodMin==null || savingsProduct.TermDepositPeriodMax == null)
                    throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.PostingFrequencyIsInvalid);
                if (savingsProduct.Periodicity == null)
                    throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.PeriodicityIsNotSet);
            }

            return;
        }

       

        public bool ValidateProduct(ISavingProduct savingsProduct, int clientTypeCounter)
        {
            if (string.IsNullOrEmpty(savingsProduct.Name))
                throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.NameIsEmpty);

            if (string.IsNullOrEmpty(savingsProduct.Code))
                throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.CodeIsEmpty);

            if (savingsProduct.Id == 0 && _savingProductManager.IsThisProductNameAlreadyExist(savingsProduct.Name))
                throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.DuplicateProductName);

            if (savingsProduct.Id == 0 && _savingProductManager.IsThisProductCodeAlreadyExist(savingsProduct.Code))
                throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.DuplicateProductCode);

            if (!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(savingsProduct.InitialAmountMin, savingsProduct.InitialAmountMax, null))
                throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.InitialAmountIsInvalid);

            if (savingsProduct.InitialAmountMin < 0)
                throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.InitialAmountMinIsInvalid);

            if (!ServicesHelper.CheckIfValueBetweenMinAndMax(savingsProduct.BalanceMin, savingsProduct.BalanceMax, savingsProduct.InitialAmountMin))
                throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.InitialAmountMinNotInBalance);

            if (!ServicesHelper.CheckIfValueBetweenMinAndMax(savingsProduct.BalanceMin, savingsProduct.BalanceMax, savingsProduct.InitialAmountMax))
                throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.InitialAmountMaxNotInBalance);

            if (!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(savingsProduct.BalanceMin, savingsProduct.BalanceMax, null))
                throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.BalanceIsInvalid);

            if (!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(savingsProduct.InterestRateMin, savingsProduct.InterestRateMax, savingsProduct.InterestRate))
                throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.InterestRateMinMaxIsInvalid);

            if (savingsProduct.InterestRate.HasValue && savingsProduct.InterestRate < 0)
                throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.InterestRateIsInvalid);

            if (!savingsProduct.InterestRate.HasValue && savingsProduct.InterestRateMin < 0)
                throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.InterestRateMinIsInvalid);
           
            if (savingsProduct.Currency == null)
                throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.CurrencyIsEmpty);

            if (savingsProduct.Currency.Id == 0)
                throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.CurrencyIsEmpty);

            if (clientTypeCounter < 1)
                throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.ClientTypeIsInvalid);

            if (!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(savingsProduct.EntryFeesMin, savingsProduct.EntryFeesMax, savingsProduct.EntryFees))
                throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.EntryFeesMinMaxIsInvalid);

            if (savingsProduct.EntryFees.HasValue && savingsProduct.EntryFees.Value < 0)
                throw new OctopusSavingProductException(OctopusSavingProductExceptionEnum.EntryFeesIsInvalid);

            if (savingsProduct is SavingsBookProduct)
                ValidateSavingBookProduct((SavingsBookProduct)savingsProduct);
           return true;
        }

        public int SaveProduct(ISavingProduct savingProduct,  int clientTypeCounter)
        {
            _clientTypeCounter = clientTypeCounter;
            return SaveProduct(savingProduct);
        }

        public void SaveProduct(ISavingProduct savingProduct, int clientTypeCounter, bool applyToExistingProduct)
        {
            _clientTypeCounter = clientTypeCounter;
            ValidateProduct(savingProduct, clientTypeCounter);
            _savingProductManager.Update(savingProduct);
            if (applyToExistingProduct)
            {
                _savingProductManager.UpdateExistingSavingBooksContracts(savingProduct);
            }
        }

        public int SaveProduct(ISavingProduct savingProduct)
        {
            ValidateProduct(savingProduct, _clientTypeCounter);

            if (savingProduct.Id == 0)
                return _savingProductManager.Add(savingProduct);

            _savingProductManager.Update(savingProduct);
            return savingProduct.Id;
        }

	    public List<InstallmentType> GetAllInstallmentTypes()
	    {
	        return _installmentTypeManager.SelectAllInstallmentTypes();
	    }
	}
}
