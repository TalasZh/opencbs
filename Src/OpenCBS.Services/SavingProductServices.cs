// Octopus MFS is an integrated suite for managing a Micro Finance Institution: 
// clients, contracts, accounting, reporting and risk
// Copyright © 2006,2007 OCTO Technology & OXUS Development Network
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License along
// with this program; if not, write to the Free Software Foundation, Inc.,
// 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
// Website: http://www.opencbs.com
// Contact: contact@opencbs.com

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
                    throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.InterBranchFlatTransferFeesIsInvalid);

                if (!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(fee.Min, fee.Max, fee.Value))
                    throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.InterBranchFlatTransferFeesMinMaxIsInvalid);

                if (!fee.Value.HasValue && fee.Min < 0)
                    throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.InterBranchFlatTransferFeesMinIsInvalid);
            }
            else
            {
                if (fee.Value.HasValue && fee.Value < 0)
                    throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.InterBranchRateTransferFeesIsInvalid);

                if (!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(fee.Min, fee.Max, fee.Value))
                    throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.InterBranchRateTransferFeesMinMaxIsInvalid);

                if (!fee.Value.HasValue && fee.Value < 0)
                    throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.InterBranchRateTransferFeesMinIsInvalid);
            }
        }

        private static void ValidateSavingBookProduct(SavingsBookProduct savingsProduct)
        {
            if (!Enum.IsDefined(typeof(OSavingInterestFrequency), savingsProduct.InterestFrequency.ToString()))
                throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.InterestsFrequencyIsInvalid);

            if (!Enum.IsDefined(typeof(OSavingInterestBase), savingsProduct.InterestBase.ToString()))
                throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.InterestsBaseIsInvalid);

            if (savingsProduct.InterestBase == OSavingInterestBase.Monthly
                && (savingsProduct.InterestFrequency != OSavingInterestFrequency.EndOfYear 
                && savingsProduct.InterestFrequency != OSavingInterestFrequency.EndOfMonth))
                throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.InterestBaseIncompatibleFrequency);

            if (savingsProduct.InterestBase == OSavingInterestBase.Weekly
                && (savingsProduct.InterestFrequency == OSavingInterestFrequency.EndOfDay))
                throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.InterestBaseIncompatibleFrequency);

            if (savingsProduct.InterestBase != OSavingInterestBase.Daily
                && (!savingsProduct.CalculAmountBase.HasValue))
                throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.CalculAmountBaseIsNull);

            if (!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(savingsProduct.WithdrawingMin, savingsProduct.WithdrawingMax, null))
                    throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.WithdrawAmountIsInvalid);

            if (!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(savingsProduct.DepositMin, savingsProduct.DepositMax, null))
                    throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.DepositAmountIsInvalid);

            if (savingsProduct.DepositMin <= 0)
                throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.DepositMinAmountIsInvalid);
            if (savingsProduct.ChequeDepositMin<=0)
                throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.ChequeDepositIsInvalid);
            if (savingsProduct.ChequeDepositMin>savingsProduct.ChequeDepositMax)
                throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.ChequeDepositIsInvalid);

            if (savingsProduct.WithdrawingMin <= 0)
                throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.WithdrawMinAmountIsInvalid);

            if (!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(savingsProduct.TransferMin, savingsProduct.TransferMax, null))
                throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.TransferAmountIsInvalid);

            if (savingsProduct.DepositFeesMin < 0)
                throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.DepositFeesMinIsInvalid);
            if (savingsProduct.ChequeDepositFeesMin < 0)
                throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.ChequeDepositFeesIsInvalid);
            if (!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(savingsProduct.DepositFeesMin, savingsProduct.DepositFeesMax, savingsProduct.DepositFees))
                throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.DepositFeesIsInvalid);
            if (!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(savingsProduct.ChequeDepositFeesMin, savingsProduct.ChequeDepositFeesMax, savingsProduct.ChequeDepositFees))
                throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.ChequeDepositFeesIsInvalid);

            if (savingsProduct.ReopenFeesMin < 0)
                throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.ReopenFeesMinIsInvalid);
            if (!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(savingsProduct.ReopenFeesMin, savingsProduct.ReopenFeesMax, savingsProduct.ReopenFees))
                throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.ReopenFeesIsInvalid);

            if (savingsProduct.OverdraftFeesMin < 0)
                throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.OverdraftFeesMinIsInvalid);
            if (!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(savingsProduct.OverdraftFeesMin, savingsProduct.OverdraftFeesMax, savingsProduct.OverdraftFees))
                throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.OverdraftFeesIsInvalid);

            if (savingsProduct.AgioFeesMin < 0)
                throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.AgioFeesMinIsInvalid);
            if (!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(savingsProduct.AgioFeesMin, savingsProduct.AgioFeesMax, savingsProduct.AgioFees))
                throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.AgioFeesIsInvalid);

            if (savingsProduct.CloseFeesMin < 0)
                throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.CloseFeesMinIsInvalid);
            if (!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(savingsProduct.CloseFeesMin, savingsProduct.CloseFeesMax, savingsProduct.CloseFees))
                throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.CloseFeesIsInvalid);

            if (savingsProduct.ManagementFeesMin < 0)
                throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.ManagementFeesMinIsInvalid);
            if (!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(savingsProduct.ManagementFeesMin, savingsProduct.ManagementFeesMax, savingsProduct.ManagementFees))
                throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.ManagementFeesIsInvalid);

            if (savingsProduct.TransferMin <= 0)
                throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.TransferAmountMinIsInvalid);

            if (!Enum.IsDefined(typeof(OSavingsFeesType), savingsProduct.WithdrawFeesType.ToString()))
                throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.WithdrawFeesTypeEmpty);

            if (savingsProduct.WithdrawFeesType == OSavingsFeesType.Flat)
            {
                if (savingsProduct.FlatWithdrawFees.HasValue && savingsProduct.FlatWithdrawFees.Value < 0)
                    throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.FlatWithdrawFeesIsInvalid);

                if (!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(savingsProduct.FlatWithdrawFeesMin, savingsProduct.FlatWithdrawFeesMax, savingsProduct.FlatWithdrawFees))
                    throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.FlatWithdrawFeesMinMaxIsInvalid);

                if (!savingsProduct.FlatWithdrawFees.HasValue && savingsProduct.FlatWithdrawFeesMin.Value < 0)
                    throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.FlatWithdrawFeesMinIsInvalid);
            }
            else
            {
                if (savingsProduct.RateWithdrawFees.HasValue && savingsProduct.RateWithdrawFees.Value < 0)
                    throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.RateWithdrawFeesIsInvalid);

                if (!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(savingsProduct.RateWithdrawFeesMin, savingsProduct.RateWithdrawFeesMax, savingsProduct.RateWithdrawFees))
                    throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.RateWithdrawFeesMinMaxIsInvalid);

                if (!savingsProduct.RateWithdrawFees.HasValue && savingsProduct.RateWithdrawFeesMin.Value < 0)
                    throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.RateWithdrawFeesMinIsInvalid);
            }

            if (!Enum.IsDefined(typeof(OSavingsFeesType), savingsProduct.TransferFeesType.ToString()))
                throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.TransferFeesTypeEmpty);

            if (savingsProduct.TransferFeesType == OSavingsFeesType.Flat)
            {
                if (savingsProduct.FlatTransferFees.HasValue && savingsProduct.FlatTransferFees.Value < 0)
                    throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.FlatTransferFeesIsInvalid);

                if (!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(savingsProduct.FlatTransferFeesMin, savingsProduct.FlatTransferFeesMax, savingsProduct.FlatTransferFees))
                    throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.FlatTransferFeesMinMaxIsInvalid);

                if (!savingsProduct.FlatTransferFees.HasValue && savingsProduct.FlatTransferFeesMin.Value < 0)
                    throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.FlatTransferFeesMinIsInvalid);
            }
            else
            {
                if (savingsProduct.RateTransferFees.HasValue && savingsProduct.RateTransferFees.Value < 0)
                    throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.RateTransferFeesIsInvalid);

                if (!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(savingsProduct.RateTransferFeesMin, savingsProduct.RateTransferFeesMax, savingsProduct.RateTransferFees))
                    throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.RateTransferFeesMinMaxIsInvalid);

                if (!savingsProduct.RateTransferFees.HasValue && savingsProduct.RateTransferFeesMin.Value < 0)
                    throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.RateTransferFeesMinIsInvalid);
            }

            ValidateInterBranchTransferFees(savingsProduct);

            if (!savingsProduct.OverdraftFees.HasValue && !(savingsProduct.OverdraftFeesMin.HasValue && savingsProduct.OverdraftFeesMax.HasValue))
                throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.OverdraftFeesIsInvalid);
            if (!savingsProduct.AgioFees.HasValue && !(savingsProduct.AgioFeesMin.HasValue && savingsProduct.AgioFeesMax.HasValue))
                throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.AgioFeesIsInvalid);
            if (savingsProduct.UseTermDeposit)
            {
                if (savingsProduct.TermDepositPeriodMin>savingsProduct.TermDepositPeriodMax)
                    throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.PostingFrequencyIsInvalid);
                if (savingsProduct.TermDepositPeriodMax<=0 || savingsProduct.TermDepositPeriodMin<=0)
                    throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.PostingFrequencyIsInvalid);
                if (savingsProduct.TermDepositPeriodMin==null || savingsProduct.TermDepositPeriodMax == null)
                    throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.PostingFrequencyIsInvalid);
                if (savingsProduct.Periodicity == null)
                    throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.PeriodicityIsNotSet);
            }

            return;
        }

       

        public bool ValidateProduct(ISavingProduct savingsProduct, int clientTypeCounter)
        {
            if (string.IsNullOrEmpty(savingsProduct.Name))
                throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.NameIsEmpty);

            if (string.IsNullOrEmpty(savingsProduct.Code))
                throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.CodeIsEmpty);

            if (savingsProduct.Id == 0 && _savingProductManager.IsThisProductNameAlreadyExist(savingsProduct.Name))
                throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.DuplicateProductName);

            if (savingsProduct.Id == 0 && _savingProductManager.IsThisProductCodeAlreadyExist(savingsProduct.Code))
                throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.DuplicateProductCode);

            if (!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(savingsProduct.InitialAmountMin, savingsProduct.InitialAmountMax, null))
                throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.InitialAmountIsInvalid);

            if (savingsProduct.InitialAmountMin < 0)
                throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.InitialAmountMinIsInvalid);

            if (!ServicesHelper.CheckIfValueBetweenMinAndMax(savingsProduct.BalanceMin, savingsProduct.BalanceMax, savingsProduct.InitialAmountMin))
                throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.InitialAmountMinNotInBalance);

            if (!ServicesHelper.CheckIfValueBetweenMinAndMax(savingsProduct.BalanceMin, savingsProduct.BalanceMax, savingsProduct.InitialAmountMax))
                throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.InitialAmountMaxNotInBalance);

            if (!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(savingsProduct.BalanceMin, savingsProduct.BalanceMax, null))
                throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.BalanceIsInvalid);

            if (!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(savingsProduct.InterestRateMin, savingsProduct.InterestRateMax, savingsProduct.InterestRate))
                throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.InterestRateMinMaxIsInvalid);

            if (savingsProduct.InterestRate.HasValue && savingsProduct.InterestRate < 0)
                throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.InterestRateIsInvalid);

            if (!savingsProduct.InterestRate.HasValue && savingsProduct.InterestRateMin < 0)
                throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.InterestRateMinIsInvalid);
           
            if (savingsProduct.Currency == null)
                throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.CurrencyIsEmpty);

            if (savingsProduct.Currency.Id == 0)
                throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.CurrencyIsEmpty);

            if (clientTypeCounter < 1)
                throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.ClientTypeIsInvalid);

            if (!ServicesHelper.CheckMinMaxAndValueCorrectlyFilled(savingsProduct.EntryFeesMin, savingsProduct.EntryFeesMax, savingsProduct.EntryFees))
                throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.EntryFeesMinMaxIsInvalid);

            if (savingsProduct.EntryFees.HasValue && savingsProduct.EntryFees.Value < 0)
                throw new OpenCbsSavingProductException(OpenCbsSavingProductExceptionEnum.EntryFeesIsInvalid);

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
