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
using NUnit.Framework;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.CoreDomain.Products;
using OpenCBS.Enums;
using OpenCBS.Manager.Products;
using OpenCBS.Manager.Contracts;
using OpenCBS.CoreDomain.Contracts.Savings;
using OpenCBS.Shared.Settings;
using OpenCBS.CoreDomain.Clients;

namespace OpenCBS.Test.Manager.Products
{
    [TestFixture]
    public class TestSavingProductManager : BaseManagerTest
    {
        private SavingProductManager _savingProductManager;
        private SavingManager _savingManager;

        [Test]
        public void TestSavingProductManagerInit()
        {
            _savingProductManager = (SavingProductManager)container["SavingProductManager"];
            Assert.IsNotNull(_savingProductManager);
        }

        [Test]
        public void TestAddSavingBookProduct()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
                                              {
                                                  Name = "Good savings account",
                                                  Code = "P123",
                                                  InitialAmountMin = 100,
                                                  InitialAmountMax = 200,
                                                  DepositMin = 250,
                                                  DepositMax = 400,
                                                  WithdrawingMin = 400,
                                                  WithdrawingMax = 450,
                                                  TransferMin = 100,
                                                  TransferMax = 500,
                                                  InterestRateMin = 0.12,
                                                  InterestRateMax = 0.20,
                                                  BalanceMin = 1000,
                                                  BalanceMax = 2000,
                                                  InterestBase = OSavingInterestBase.Daily,
                                                  InterestFrequency = OSavingInterestFrequency.EndOfYear,
                                                  EntryFeesMin = 10,
                                                  EntryFeesMax = 20,
                                                  WithdrawFeesType = OSavingsFeesType.Flat,
                                                  FlatWithdrawFeesMin = 1,
                                                  FlatWithdrawFeesMax = 5,
                                                  TransferFeesType = OSavingsFeesType.Flat,
                                                  FlatTransferFeesMin = 1,
                                                  FlatTransferFeesMax = 5,
                                                  DepositFeesMin = 1,
                                                  DepositFeesMax = 5,
                                                  CloseFeesMin = 2,
                                                  CloseFeesMax = 7,
                                                  ManagementFeesMin = 4,
                                                  ManagementFeesMax = 16,
                                                  OverdraftFeesMin = 2,
                                                  OverdraftFeesMax = 6,
                                                  AgioFeesMin = 1,
                                                  AgioFeesMax = 4,
                                                  Currency = new Currency {Id = 1}
                                              };

            InstallmentType managementFeeFreq = new InstallmentType{Name = "Weekly", NbOfDays = 7, NbOfMonths = 0};
            savingsProduct.ManagementFeeFreq = managementFeeFreq;

            InstallmentType agioFeeFreq = new InstallmentType { Name = "Daily", NbOfDays = 1, NbOfMonths = 0 };
            savingsProduct.AgioFeesFreq = agioFeeFreq;

            _savingProductManager = (SavingProductManager)container["SavingProductManager"];
            _savingProductManager.Add(savingsProduct);

            Assert.Greater(savingsProduct.Id, 0);
        }

       

        [Test]
        public void TestUpdateSavingBookProduct()
        {
            Assert.Ignore(); // Ru55
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "Good savings account",
                Code = "P123",
                ClientType = OClientTypes.All,
                InitialAmountMin = 100,
                InitialAmountMax = 200,
                DepositMin = 250,
                DepositMax = 400,
                WithdrawingMin = 400,
                WithdrawingMax = 450,
                TransferMin = 100,
                TransferMax = 250,
                InterestRateMin = 0.12,
                InterestRateMax = 0.20,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Daily,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                EntryFeesMin = 10,
                EntryFeesMax = 20,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 5,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 5,
                Currency = new Currency { Id = 1 }
            };

            InstallmentType managementFeeFreq = new InstallmentType { Name = "Weekly", NbOfDays = 7, NbOfMonths = 0, Id = 1 };
            savingsProduct.ManagementFeeFreq = managementFeeFreq;

            InstallmentType agioFeeFreq = new InstallmentType { Name = "Daily", NbOfDays = 1, NbOfMonths = 0, Id = 2 };
            savingsProduct.AgioFeesFreq = agioFeeFreq;

            _savingProductManager = (SavingProductManager)container["SavingProductManager"];
            _savingProductManager.Add(savingsProduct);

            SavingsBookProduct loadedProduct = _savingProductManager.SelectSavingsBookProduct(savingsProduct.Id);

            savingsProduct.Name = "Good updating product";
            savingsProduct.ClientType = OClientTypes.Person;
            savingsProduct.Code = "P125";
            savingsProduct.InitialAmountMin = 200;
            savingsProduct.InitialAmountMax = 400;
            savingsProduct.DepositMin = 500;
            savingsProduct.DepositMax = 800;
            savingsProduct.WithdrawingMin = 800;
            savingsProduct.WithdrawingMax = 900;
            savingsProduct.TransferMin = 300;
            savingsProduct.TransferMax = 800;
            savingsProduct.InterestRateMin = 0.24;
            savingsProduct.InterestRateMax = 0.20;
            savingsProduct.BalanceMin = 2000;
            savingsProduct.BalanceMax = 4000;
            savingsProduct.InterestBase = OSavingInterestBase.Monthly;
            savingsProduct.InterestFrequency = OSavingInterestFrequency.EndOfMonth;
            savingsProduct.EntryFees = 15;
            savingsProduct.EntryFeesMax = null;
            savingsProduct.EntryFeesMin = null;
            savingsProduct.CalculAmountBase = OSavingCalculAmountBase.MinimalAmount;
            savingsProduct.WithdrawFeesType = OSavingsFeesType.Rate;
            savingsProduct.RateWithdrawFeesMin = 0.01;
            savingsProduct.RateWithdrawFeesMax = 0.05;
            savingsProduct.TransferFeesType = OSavingsFeesType.Rate;
            savingsProduct.RateTransferFeesMin = 0.01;
            savingsProduct.RateTransferFeesMax = 0.05;
            savingsProduct.Currency = new Currency { Id = 2 };

            _savingProductManager.Update(savingsProduct);

            loadedProduct = _savingProductManager.SelectSavingsBookProduct(savingsProduct.Id);

            Assert.AreEqual(savingsProduct.Id, loadedProduct.Id);
            Assert.AreEqual(savingsProduct.ClientType, loadedProduct.ClientType);
            Assert.AreEqual(savingsProduct.Name, loadedProduct.Name);
            Assert.AreEqual(savingsProduct.Code, loadedProduct.Code);
            Assert.AreEqual(savingsProduct.BalanceMax.Value, loadedProduct.BalanceMax.Value);
            Assert.AreEqual(savingsProduct.BalanceMin.Value, loadedProduct.BalanceMin.Value);
            Assert.AreEqual(savingsProduct.InitialAmountMax.Value, loadedProduct.InitialAmountMax.Value);
            Assert.AreEqual(savingsProduct.InitialAmountMin.Value, loadedProduct.InitialAmountMin.Value);
            Assert.AreEqual(savingsProduct.DepositMax.Value, loadedProduct.DepositMax.Value);
            Assert.AreEqual(savingsProduct.DepositMin.Value, loadedProduct.DepositMin.Value);
            Assert.AreEqual(savingsProduct.WithdrawingMax.Value, loadedProduct.WithdrawingMax.Value);
            Assert.AreEqual(savingsProduct.WithdrawingMin.Value, loadedProduct.WithdrawingMin.Value);
            Assert.AreEqual(savingsProduct.TransferMin.Value, loadedProduct.TransferMin.Value);
            Assert.AreEqual(savingsProduct.TransferMax.Value, loadedProduct.TransferMax.Value);
            Assert.AreEqual(savingsProduct.InterestRate, loadedProduct.InterestRate);
            Assert.AreEqual(savingsProduct.InterestRateMax, loadedProduct.InterestRateMax);
            Assert.AreEqual(savingsProduct.InterestRateMin, loadedProduct.InterestRateMin);
            Assert.AreEqual(savingsProduct.InterestBase, loadedProduct.InterestBase);
            Assert.AreEqual(savingsProduct.InterestFrequency, loadedProduct.InterestFrequency);
            Assert.AreEqual(savingsProduct.CalculAmountBase, loadedProduct.CalculAmountBase);
            Assert.AreEqual(savingsProduct.EntryFees, loadedProduct.EntryFees);
            Assert.AreEqual(savingsProduct.EntryFeesMax.HasValue, loadedProduct.EntryFeesMax.HasValue);
            Assert.AreEqual(savingsProduct.EntryFeesMin.HasValue, loadedProduct.EntryFeesMin.HasValue);
            Assert.AreEqual(savingsProduct.WithdrawFeesType, loadedProduct.WithdrawFeesType);
            Assert.AreEqual(false, loadedProduct.FlatWithdrawFees.HasValue);
            Assert.AreEqual(false, loadedProduct.FlatWithdrawFeesMax.HasValue);
            Assert.AreEqual(false, loadedProduct.FlatWithdrawFeesMin.HasValue);
            Assert.AreEqual(savingsProduct.RateWithdrawFees, loadedProduct.RateWithdrawFees);
            Assert.AreEqual(savingsProduct.RateWithdrawFeesMax, loadedProduct.RateWithdrawFeesMax);
            Assert.AreEqual(savingsProduct.RateWithdrawFeesMin, loadedProduct.RateWithdrawFeesMin);
            Assert.AreEqual(savingsProduct.TransferFeesType, loadedProduct.TransferFeesType);
            Assert.AreEqual(false, loadedProduct.FlatTransferFees.HasValue);
            Assert.AreEqual(false, loadedProduct.FlatTransferFeesMax.HasValue);
            Assert.AreEqual(false, loadedProduct.FlatTransferFeesMin.HasValue);
            Assert.AreEqual(savingsProduct.RateTransferFees, loadedProduct.RateTransferFees);
            Assert.AreEqual(savingsProduct.RateTransferFeesMax, loadedProduct.RateTransferFeesMax);
            Assert.AreEqual(savingsProduct.RateTransferFeesMin, loadedProduct.RateTransferFeesMin);
            Assert.AreEqual(savingsProduct.Currency.Id, loadedProduct.Currency.Id);
        }

        [Test]
        public void TestUpdateUsedSavingBookProduct()
        {
            Assert.Ignore(); // Ru55
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "Good savings account",
                Code = "P123",
                ClientType = OClientTypes.All,
                InitialAmountMin = 100,
                InitialAmountMax = 200,
                DepositMin = 250,
                DepositMax = 400,
                WithdrawingMin = 400,
                WithdrawingMax = 450,
                TransferMin = 100,
                TransferMax = 300,
                InterestRateMin = 0.12,
                InterestRateMax = 0.20,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Daily,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                EntryFeesMin = 10,
                EntryFeesMax = 20,
                WithdrawFeesType = OSavingsFeesType.Rate,
                RateWithdrawFeesMin = 0.01,
                RateWithdrawFeesMax = 0.05,
                TransferFeesType = OSavingsFeesType.Rate,
                RateTransferFeesMin = 0.01,
                RateTransferFeesMax = 0.05,
                Currency = new Currency { Id = 1 }
            };

            InstallmentType managementFeeFreq = new InstallmentType { Name = "Weekly", NbOfDays = 7, NbOfMonths = 0, Id = 1};
            savingsProduct.ManagementFeeFreq = managementFeeFreq;

            InstallmentType agioFeeFreq = new InstallmentType { Name = "Daily", NbOfDays = 1, NbOfMonths = 0, Id = 2 };
            savingsProduct.AgioFeesFreq = agioFeeFreq;

            _savingProductManager = (SavingProductManager)container["SavingProductManager"];
            _savingProductManager.Add(savingsProduct);

            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),
                                                               new User() {Id = 1}, new DateTime(2009, 01, 01),
                                                               savingsProduct, null)
                                            {
                                                Code = "S/CR/2009/P123/BAR-1",
                                                Status = OSavingsStatus.Active,
                                                InterestRate = 0.01
                                            };

            _savingManager = (SavingManager)container["SavingManager"];
            _savingManager.Add(saving, new Person { Id = 6 });

            SavingsBookProduct loadedProduct = _savingProductManager.SelectSavingsBookProduct(savingsProduct.Id);

            savingsProduct.Name = "Good updating product";
            savingsProduct.ClientType = OClientTypes.Person;
            savingsProduct.Code = "P125";
            savingsProduct.InitialAmountMin = 200;
            savingsProduct.InitialAmountMax = 400;
            savingsProduct.DepositMin = 500;
            savingsProduct.DepositMax = 800;
            savingsProduct.WithdrawingMin = 800;
            savingsProduct.WithdrawingMax = 900;
            savingsProduct.TransferMin = 500;
            savingsProduct.TransferMax = 1000;
            savingsProduct.InterestRateMin = 0.24;
            savingsProduct.InterestRateMax = 0.20;
            savingsProduct.BalanceMin = 2000;
            savingsProduct.BalanceMax = 4000;
            savingsProduct.InterestBase = OSavingInterestBase.Monthly;
            savingsProduct.InterestFrequency = OSavingInterestFrequency.EndOfMonth;
            savingsProduct.EntryFees = 15;
            savingsProduct.EntryFeesMax = null;
            savingsProduct.EntryFeesMin = null;
            savingsProduct.WithdrawFeesType = OSavingsFeesType.Rate;
            savingsProduct.RateWithdrawFees = 0.03;
            savingsProduct.RateWithdrawFeesMin = null;
            savingsProduct.RateWithdrawFeesMax = null;
            savingsProduct.FlatWithdrawFeesMin = null;
            savingsProduct.FlatWithdrawFeesMax = null;
            savingsProduct.TransferFeesType = OSavingsFeesType.Rate;
            savingsProduct.RateTransferFees = 0.03;
            savingsProduct.RateTransferFeesMin = null;
            savingsProduct.RateTransferFeesMax = null;
            savingsProduct.FlatTransferFeesMin = null;
            savingsProduct.FlatTransferFeesMax = null;
            savingsProduct.Currency = new Currency { Id = 2 };

            _savingProductManager.Update(savingsProduct);

            loadedProduct = _savingProductManager.SelectSavingsBookProduct(savingsProduct.Id);

            Assert.AreEqual(savingsProduct.Id, loadedProduct.Id);
            Assert.AreEqual(OClientTypes.All, loadedProduct.ClientType);
            Assert.AreEqual("Good savings account", loadedProduct.Name);
            Assert.AreEqual("P123", loadedProduct.Code);
            Assert.AreEqual(savingsProduct.BalanceMax.Value, loadedProduct.BalanceMax.Value);
            Assert.AreEqual(savingsProduct.BalanceMin.Value, loadedProduct.BalanceMin.Value);
            Assert.AreEqual(savingsProduct.InitialAmountMax.Value, loadedProduct.InitialAmountMax.Value);
            Assert.AreEqual(savingsProduct.InitialAmountMin.Value, loadedProduct.InitialAmountMin.Value);
            Assert.AreEqual(savingsProduct.DepositMax.Value, loadedProduct.DepositMax.Value);
            Assert.AreEqual(savingsProduct.DepositMin.Value, loadedProduct.DepositMin.Value);
            Assert.AreEqual(savingsProduct.WithdrawingMax.Value, loadedProduct.WithdrawingMax.Value);
            Assert.AreEqual(savingsProduct.WithdrawingMin.Value, loadedProduct.WithdrawingMin.Value);
            Assert.AreEqual(savingsProduct.TransferMin.Value, loadedProduct.TransferMin.Value);
            Assert.AreEqual(savingsProduct.TransferMax.Value, loadedProduct.TransferMax.Value);
            Assert.AreEqual(savingsProduct.InterestRate, loadedProduct.InterestRate);
            Assert.AreEqual(savingsProduct.InterestRateMax, loadedProduct.InterestRateMax);
            Assert.AreEqual(savingsProduct.InterestRateMin, loadedProduct.InterestRateMin);
            Assert.AreEqual(OSavingInterestBase.Daily, loadedProduct.InterestBase);
            Assert.AreEqual(OSavingInterestFrequency.EndOfYear, loadedProduct.InterestFrequency);
            Assert.AreEqual(savingsProduct.EntryFees, loadedProduct.EntryFees);
            Assert.AreEqual(savingsProduct.EntryFeesMax.HasValue, loadedProduct.EntryFeesMax.HasValue);
            Assert.AreEqual(savingsProduct.EntryFeesMin.HasValue, loadedProduct.EntryFeesMin.HasValue);
            Assert.AreEqual(savingsProduct.WithdrawFeesType, loadedProduct.WithdrawFeesType);
            Assert.AreEqual(savingsProduct.FlatWithdrawFees.HasValue, loadedProduct.FlatWithdrawFees.HasValue);
            Assert.AreEqual(savingsProduct.FlatWithdrawFeesMax.HasValue, loadedProduct.FlatWithdrawFeesMax.HasValue);
            Assert.AreEqual(savingsProduct.FlatWithdrawFeesMin.HasValue, loadedProduct.FlatWithdrawFeesMin.HasValue);
            Assert.AreEqual(savingsProduct.RateWithdrawFees, loadedProduct.RateWithdrawFees);
            Assert.AreEqual(savingsProduct.RateWithdrawFeesMax.HasValue, loadedProduct.RateWithdrawFeesMax.HasValue);
            Assert.AreEqual(savingsProduct.RateWithdrawFeesMin.HasValue, loadedProduct.RateWithdrawFeesMin.HasValue);
            Assert.AreEqual(savingsProduct.TransferFeesType, loadedProduct.TransferFeesType);
            Assert.AreEqual(savingsProduct.FlatTransferFees.HasValue, loadedProduct.FlatTransferFees.HasValue);
            Assert.AreEqual(savingsProduct.FlatTransferFeesMax.HasValue, loadedProduct.FlatTransferFeesMax.HasValue);
            Assert.AreEqual(savingsProduct.FlatTransferFeesMin.HasValue, loadedProduct.FlatTransferFeesMin.HasValue);
            Assert.AreEqual(savingsProduct.RateTransferFees, loadedProduct.RateTransferFees);
            Assert.AreEqual(savingsProduct.RateTransferFeesMax.HasValue, loadedProduct.RateTransferFeesMax.HasValue);
            Assert.AreEqual(savingsProduct.RateTransferFeesMin.HasValue, loadedProduct.RateTransferFeesMin.HasValue);
            Assert.AreEqual(1, loadedProduct.Currency.Id);
        }

        [Test]
        public void TestGetSavingBookProduct()
        {
            Assert.Ignore(); // Ru55
            SavingsBookProduct savingsProduct = new SavingsBookProduct
                                              {
                                                  Name="Good savings account",
                                                  Code = "P123",
                                                  ClientType = OClientTypes.All,
                                                  InitialAmountMin = 100,
                                                  InitialAmountMax = 200,
                                                  DepositMin = 250,
                                                  DepositMax = 400,
                                                  WithdrawingMin = 400,
                                                  WithdrawingMax = 450,
                                                  TransferMin = 100,
                                                  TransferMax = 500,
                                                  InterestRateMin = 0.12,
                                                  InterestRateMax = 0.20,
                                                  BalanceMin = 1000,
                                                  BalanceMax = 2000,
                                                  InterestBase = OSavingInterestBase.Monthly,
                                                  InterestFrequency = OSavingInterestFrequency.EndOfYear,
                                                  CalculAmountBase = OSavingCalculAmountBase.MinimalAmount,
                                                  EntryFeesMax = 20,
                                                  EntryFeesMin = 10,
                                                  WithdrawFeesType = OSavingsFeesType.Flat,
                                                  FlatWithdrawFeesMin = 1,
                                                  FlatWithdrawFeesMax = 5,
                                                  TransferFeesType = OSavingsFeesType.Flat,
                                                  FlatTransferFeesMin = 1,
                                                  FlatTransferFeesMax = 5,
                                                  OverdraftFeesMin = 2,
                                                  OverdraftFeesMax = 6,
                                                  AgioFeesMin = 1,
                                                  AgioFeesMax = 4,
                                                  Currency = new Currency { Id = 1 }
                                              };

            InstallmentType managementFeeFreq = new InstallmentType { Name = "Weekly", NbOfDays = 7, NbOfMonths = 0, Id = 1 };
            savingsProduct.ManagementFeeFreq = managementFeeFreq;

            InstallmentType agioFeeFreq = new InstallmentType { Name = "Daily", NbOfDays = 1, NbOfMonths = 0, Id = 2 };
            savingsProduct.AgioFeesFreq = agioFeeFreq;

            _savingProductManager = (SavingProductManager)container["SavingProductManager"];
            _savingProductManager.Add(savingsProduct);

            SavingsBookProduct loadedProduct = (SavingsBookProduct)_savingProductManager.SelectSavingProduct(savingsProduct.Id);

            Assert.AreEqual(savingsProduct.Id, loadedProduct.Id);
            Assert.AreEqual(savingsProduct.ClientType, loadedProduct.ClientType);
            Assert.AreEqual(savingsProduct.Name, loadedProduct.Name);
            Assert.AreEqual(savingsProduct.BalanceMax.Value, loadedProduct.BalanceMax.Value);
            Assert.AreEqual(savingsProduct.BalanceMin.Value, loadedProduct.BalanceMin.Value);
            Assert.AreEqual(savingsProduct.InitialAmountMax.Value, loadedProduct.InitialAmountMax.Value);
            Assert.AreEqual(savingsProduct.InitialAmountMin.Value, loadedProduct.InitialAmountMin.Value);
            Assert.AreEqual(savingsProduct.DepositMax.Value, loadedProduct.DepositMax.Value);
            Assert.AreEqual(savingsProduct.DepositMin.Value, loadedProduct.DepositMin.Value);
            Assert.AreEqual(savingsProduct.WithdrawingMax.Value, loadedProduct.WithdrawingMax.Value);
            Assert.AreEqual(savingsProduct.WithdrawingMin.Value, loadedProduct.WithdrawingMin.Value);
            Assert.AreEqual(savingsProduct.TransferMin.Value, loadedProduct.TransferMin.Value);
            Assert.AreEqual(savingsProduct.TransferMax.Value, loadedProduct.TransferMax.Value);
            Assert.AreEqual(savingsProduct.InterestRate, loadedProduct.InterestRate);
            Assert.AreEqual(savingsProduct.InterestRateMax, loadedProduct.InterestRateMax);
            Assert.AreEqual(savingsProduct.InterestRateMin, loadedProduct.InterestRateMin);
            Assert.AreEqual(savingsProduct.InterestBase, loadedProduct.InterestBase);
            Assert.AreEqual(savingsProduct.InterestFrequency, loadedProduct.InterestFrequency);
            Assert.AreEqual(savingsProduct.CalculAmountBase, loadedProduct.CalculAmountBase);
            Assert.AreEqual(savingsProduct.EntryFees.HasValue, loadedProduct.EntryFees.HasValue);
            Assert.AreEqual(savingsProduct.EntryFeesMax, loadedProduct.EntryFeesMax);
            Assert.AreEqual(savingsProduct.EntryFeesMin, loadedProduct.EntryFeesMin);
            Assert.AreEqual(savingsProduct.WithdrawFeesType, loadedProduct.WithdrawFeesType);
            Assert.AreEqual(savingsProduct.FlatWithdrawFees.HasValue, loadedProduct.FlatWithdrawFees.HasValue);
            Assert.AreEqual(savingsProduct.FlatWithdrawFeesMax, loadedProduct.FlatWithdrawFeesMax);
            Assert.AreEqual(savingsProduct.FlatWithdrawFeesMin, loadedProduct.FlatWithdrawFeesMin);
            Assert.AreEqual(savingsProduct.RateWithdrawFees, loadedProduct.RateWithdrawFees);
            Assert.AreEqual(savingsProduct.RateWithdrawFeesMax, loadedProduct.RateWithdrawFeesMax);
            Assert.AreEqual(savingsProduct.RateWithdrawFeesMin, loadedProduct.RateWithdrawFeesMin);
            Assert.AreEqual(savingsProduct.TransferFeesType, loadedProduct.TransferFeesType);
            Assert.AreEqual(savingsProduct.FlatTransferFees.HasValue, loadedProduct.FlatTransferFees.HasValue);
            Assert.AreEqual(savingsProduct.FlatTransferFeesMax, loadedProduct.FlatTransferFeesMax);
            Assert.AreEqual(savingsProduct.FlatTransferFeesMin, loadedProduct.FlatTransferFeesMin);
            Assert.AreEqual(savingsProduct.RateTransferFees, loadedProduct.RateTransferFees);
            Assert.AreEqual(savingsProduct.RateTransferFeesMax, loadedProduct.RateTransferFeesMax);
            Assert.AreEqual(savingsProduct.RateTransferFeesMin, loadedProduct.RateTransferFeesMin);
        }

        [Test]
        public void TestIsThisProductNameAlreadyExist()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
                                              {
                                                  Name = "Good savings account",
                                                  Code = "P123",
                                                  ClientType = OClientTypes.All,
                                                  InitialAmountMin = 100,
                                                  InitialAmountMax = 200,
                                                  DepositMin = 250,
                                                  DepositMax = 400,
                                                  WithdrawingMin = 400,
                                                  WithdrawingMax = 450,
                                                  TransferMin = 100,
                                                  TransferMax = 500,
                                                  InterestRateMin = 0.12,
                                                  InterestRateMax = 0.20,
                                                  BalanceMin = 1000,
                                                  BalanceMax = 2000,
                                                  Currency = new Currency { Id = 1 },
                                                  InterestBase = OSavingInterestBase.Daily,
                                                  InterestFrequency = OSavingInterestFrequency.EndOfWeek
                                              };
            InstallmentType managementFeeFreq = new InstallmentType { Name = "Weekly", NbOfDays = 7, NbOfMonths = 0, Id = 1 };
            savingsProduct.ManagementFeeFreq = managementFeeFreq;

            InstallmentType agioFeeFreq = new InstallmentType { Name = "Daily", NbOfDays = 1, NbOfMonths = 0 };
            savingsProduct.AgioFeesFreq = agioFeeFreq;

            _savingProductManager = (SavingProductManager)container["SavingProductManager"];

            Assert.IsTrue(_savingProductManager.IsThisProductNameAlreadyExist("SavingProduct1"));
            Assert.IsFalse(_savingProductManager.IsThisProductNameAlreadyExist(savingsProduct.Name));
	
            _savingProductManager.Add(savingsProduct);

            Assert.IsTrue(_savingProductManager.IsThisProductNameAlreadyExist(savingsProduct.Name));

        }

        [Test]
        public void TestIsThisProductCodeAlreadyExist()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "Good savings account",
                Code = "P123",
                ClientType = OpenCBS.Enums.OClientTypes.All,
                InitialAmountMin = 100,
                InitialAmountMax = 200,
                DepositMin = 250,
                DepositMax = 400,
                WithdrawingMin = 400,
                WithdrawingMax = 450,
                TransferMin = 100,
                TransferMax = 500,
                InterestRateMin = 0.12,
                InterestRateMax = 0.20,
                BalanceMin = 1000,
                BalanceMax = 2000,
                Currency = new Currency { Id = 1 },
                InterestBase = OSavingInterestBase.Daily,
                InterestFrequency = OSavingInterestFrequency.EndOfWeek
            };
            InstallmentType managementFeeFreq = new InstallmentType { Name = "Weekly", NbOfDays = 7, NbOfMonths = 0, Id = 1 };
            savingsProduct.ManagementFeeFreq = managementFeeFreq;

            InstallmentType agioFeeFreq = new InstallmentType { Name = "Daily", NbOfDays = 1, NbOfMonths = 0 };
            savingsProduct.AgioFeesFreq = agioFeeFreq;

            _savingProductManager = (SavingProductManager)container["SavingProductManager"];

            Assert.IsTrue(_savingProductManager.IsThisProductCodeAlreadyExist("P1"));
            Assert.IsFalse(_savingProductManager.IsThisProductCodeAlreadyExist(savingsProduct.Code));

            _savingProductManager.Add(savingsProduct);

            Assert.IsTrue(_savingProductManager.IsThisProductCodeAlreadyExist(savingsProduct.Code));

        }
    }
}
