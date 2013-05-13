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

using NUnit.Framework;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.CoreDomain.Products;
using OpenCBS.ExceptionsHandler;
using OpenCBS.Manager.Products;
using OpenCBS.Services;
using NUnit.Mocks;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.Enums;
using OpenCBS.CoreDomain;

namespace OpenCBS.Test.Services.Savings
{
    /// <summary>
    /// Description r�sum�e de TestSavingProductServices.
    /// </summary>

    [TestFixture]
    public class TestSavingProductServices
    {
        private SavingProductServices _savingProductServices;

        private DynamicMock _savingProductManagerMock;

        [SetUp]
        public void SetUp()
        {
            _savingProductManagerMock = new DynamicMock(typeof(SavingProductManager));
            _savingProductServices = new SavingProductServices((SavingProductManager)_savingProductManagerMock.MockInstance);
        }

        [Test]
        public void TestSavingBookProduct_When_Name_IsNull()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                InitialAmountMin = 1000,
                InitialAmountMax = 1500,
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
                InterestFrequency = OSavingInterestFrequency.EndOfMonth,
                EntryFees = 0,
                ClientType = OClientTypes.All,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 1,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
                Currency = new Currency { Id = 1 },
            };
            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Name is Null).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.NameIsEmpty, exception.Code);
            }
        }

        

        [Test]
        public void TestSavingBookProduct_When_Name_AlreadyExist()
        {
            _savingProductManagerMock.ExpectAndReturn("IsThisProductNameAlreadyExist", true, "Product");

            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "Product",
                Code = "Product",
                InitialAmountMin = 1000,
                InitialAmountMax = 1500,
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
                InterestFrequency = OSavingInterestFrequency.EndOfMonth,
                EntryFees = 0,
                ClientType = OClientTypes.All,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 1,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
                Currency = new Currency { Id = 1 }
            };
            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Name is Already Exist).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.DuplicateProductName, exception.Code);
            }
        }

        

        [Test]
        public void TestSavingBookProduct_When_Code_IsNull()
        {
            _savingProductManagerMock.ExpectAndReturn("IsThisProductCodeAlreadyExist", true, "Product");

            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "Product",
                InitialAmountMin = 1000,
                InitialAmountMax = 1500,
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
                InterestFrequency = OSavingInterestFrequency.EndOfMonth,
                EntryFees = 0,
                ClientType = OClientTypes.All,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 1,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
                Currency = new Currency { Id = 1 }
            };
            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Code is Null).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.CodeIsEmpty, exception.Code);
            }
        }

       
        [Test]
        public void TestSavingBookProduct_When_Code_AlreadyExist()
        {
            _savingProductManagerMock.ExpectAndReturn("IsThisProductCodeAlreadyExist", true, "Product");

            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "Product",
                Code = "Product",
                InitialAmountMin = 1000,
                InitialAmountMax = 1500,
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
                InterestFrequency = OSavingInterestFrequency.EndOfMonth,
                EntryFees = 0,
                ClientType = OClientTypes.All,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 1,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
                Currency = new Currency { Id = 1 }
            };
            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Code is Already Exist).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.DuplicateProductCode, exception.Code);
            }
        }

       

        [Test]
        public void TestSavingBookProduct_When_InialAmountMin_IsNull()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMax = 1500,
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
                InterestFrequency = OSavingInterestFrequency.EndOfMonth,
                EntryFees = 0,
                ClientType = OClientTypes.All,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 1,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
                Currency = new Currency { Id = 1 }
            };
            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Initial Amount Min is Null).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.InitialAmountIsInvalid, exception.Code);
            }
        }

        

        [Test]
        public void TestSavingBookProduct_When_InialAmountMin_IsNegative()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = -100,
                InitialAmountMax = 2000,
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
                InterestFrequency = OSavingInterestFrequency.EndOfMonth,
                EntryFees = 0,
                ClientType = OClientTypes.All,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 1,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
                Currency = new Currency { Id = 1 }
            };
            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Initial Amount Min < 0).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.InitialAmountMinIsInvalid, exception.Code);
            }
        }

      
        [Test]
        public void TestSavingBookProduct_When_InialAmountMax_IsNull()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
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
                InterestFrequency = OSavingInterestFrequency.EndOfMonth,
                EntryFees = 0,
                ClientType = OClientTypes.All,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 1,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
                Currency = new Currency { Id = 1 }
            };
            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Initial Amount Max is Null).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.InitialAmountIsInvalid, exception.Code);
            }
        }

        

        [Test]
        public void TestSavingBookProduct_When_InialAmountMin_IsGreaterThan_InitialAmountMax()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1500,
                InitialAmountMax = 1000,
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
                InterestFrequency = OSavingInterestFrequency.EndOfMonth,
                EntryFees = 0,
                ClientType = OClientTypes.All,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 1,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
                Currency = new Currency { Id = 1 }
            };
            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Initial Amount Min is Greater than Initial Amount Max).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.InitialAmountIsInvalid, exception.Code);
            }
        }

        
        [Test]
        public void TestSavingBookProductIsValid_InitialAmountTooSmallComparedToBalance()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
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
                EntryFees = 0,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 1,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
                ClientType = OClientTypes.All,
                Currency = new Currency { Id = 1 }
            };
            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (initial amount < balance.min).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.InitialAmountMinNotInBalance, exception.Code);
            }
        }

        
        [Test]
        public void TestSavingBookProductIsValid_InitialAmountTooBigComparedToBalance()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 250,
                InitialAmountMax = 350,
                DepositMin = 250,
                DepositMax = 400,
                WithdrawingMin = 400,
                WithdrawingMax = 450,
                TransferMin = 100,
                TransferMax = 300,
                InterestRateMin = 0.12,
                InterestRateMax = 0.20,
                BalanceMin = 250,
                BalanceMax = 300,
                EntryFees = 0,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 1,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
                InterestBase = OSavingInterestBase.Daily,
                InterestFrequency = OSavingInterestFrequency.EndOfMonth,
                ClientType = OClientTypes.All,
                Currency = new Currency { Id = 1 }
            };
            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (initial amount > balance.max).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.InitialAmountMaxNotInBalance, exception.Code);
            }
        }

       

        [Test]
        public void TestSavingBookProduct_When_DepositMin_IsNull()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
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
                InterestFrequency = OSavingInterestFrequency.EndOfMonth,
                ClientType = OClientTypes.All,
                EntryFees = 0,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 1,
                Currency = new Currency { Id = 1 }
            };
            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Deposit Min is Null).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.DepositAmountIsInvalid, exception.Code);
            }
        }

       

        [Test]
        public void TestSavingBookProduct_When_DepositMin_IsNegative()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = -100,
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
                InterestFrequency = OSavingInterestFrequency.EndOfMonth,
                ClientType = OClientTypes.All,
                EntryFees = 0,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 1,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
                Currency = new Currency { Id = 1 }
            };
            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Deposit Min < 0).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.DepositMinAmountIsInvalid, exception.Code);
            }
        }

      
        [Test]
        public void TestSavingBookProduct_When_DepositMax_IsNull()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                WithdrawingMin = 400,
                WithdrawingMax = 450,
                TransferMin = 100,
                TransferMax = 300,
                InterestRateMin = 0.12,
                InterestRateMax = 0.20,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Daily,
                InterestFrequency = OSavingInterestFrequency.EndOfMonth,
                ClientType = OClientTypes.All,
                EntryFees = 0,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 1,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
                Currency = new Currency { Id = 1 }
            };
            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Deposit Max is Null).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.DepositAmountIsInvalid, exception.Code);
            }
        }

       

        [Test]
        public void TestSavingBookProduct_When_DepositMin_IsGreaterThan_DepositMax()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 50,
                WithdrawingMin = 400,
                WithdrawingMax = 450,
                TransferMin = 100,
                TransferMax = 300,
                InterestRateMin = 0.12,
                InterestRateMax = 0.20,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Daily,
                InterestFrequency = OSavingInterestFrequency.EndOfMonth,
                ClientType = OClientTypes.All,
                EntryFees = 0,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 1,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
                Currency = new Currency { Id = 1 }
            };
            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Deposit Min > Deposit Max).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.DepositAmountIsInvalid, exception.Code);
            }
        }

      

        [Test]
        public void TestSavingBookProduct_When_DepositMin_And_DepositMax_Are_Zero()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 0,
                DepositMax = 0,
                WithdrawingMin = 400,
                WithdrawingMax = 450,
                TransferMin = 100,
                TransferMax = 300,
                InterestRateMin = 0.12,
                InterestRateMax = 0.20,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Daily,
                InterestFrequency = OSavingInterestFrequency.EndOfMonth,
                ClientType = OClientTypes.All,
                EntryFees = 0,
                Currency = new Currency { Id = 1 },
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 5,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
            };
            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Deposit Min And Deposit Max = 0).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.DepositAmountIsInvalid, exception.Code);
            }
        }

      

        [Test]
        public void TestSavingBookProduct_When_WithdrawMin_IsNull()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 400,
                WithdrawingMax = 450,
                TransferMin = 100,
                TransferMax = 300,
                InterestRateMin = 0.12,
                InterestRateMax = 0.20,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Daily,
                InterestFrequency = OSavingInterestFrequency.EndOfMonth,
                ClientType = OClientTypes.All,
                EntryFees = 0,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 1,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
                Currency = new Currency { Id = 1 },
            };
            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Withdraw Min is Null).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.WithdrawAmountIsInvalid, exception.Code);
            }
        }

       

        [Test]
        public void TestSavingBookProduct_When_WithdrawMin_IsNegative()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 400,
                WithdrawingMin = -100,
                WithdrawingMax = 450,
                TransferMin = 100,
                TransferMax = 300,
                InterestRateMin = 0.12,
                InterestRateMax = 0.20,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Daily,
                InterestFrequency = OSavingInterestFrequency.EndOfMonth,
                ClientType = OClientTypes.All,
                EntryFees = 0,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 1,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
                Currency = new Currency { Id = 1 },
            };
            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Withdraw Min < 0).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.WithdrawMinAmountIsInvalid, exception.Code);
            }
        }


        [Test]
        public void TestSavingBookProduct_When_WithdrawMax_IsNull()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                WithdrawingMin = 400,
                TransferMin = 100,
                TransferMax = 300,
                InterestRateMin = 0.12,
                InterestRateMax = 0.20,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Daily,
                InterestFrequency = OSavingInterestFrequency.EndOfMonth,
                ClientType = OClientTypes.All,
                EntryFees = 0,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 1,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
                Currency = new Currency { Id = 1 },
            };
            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Withdraw Max is Null).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.WithdrawAmountIsInvalid, exception.Code);
            }
        }

       

        [Test]
        public void TestSavingBookProduct_When_WithdrawtMin_IsGreaterThan_WithdrawMax()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                WithdrawingMin = 400,
                WithdrawingMax = 250,
                TransferMin = 100,
                TransferMax = 300,
                InterestRateMin = 0.12,
                InterestRateMax = 0.20,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Daily,
                InterestFrequency = OSavingInterestFrequency.EndOfMonth,
                ClientType = OClientTypes.All,
                EntryFees = 0,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 1,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
                Currency = new Currency { Id = 1 }
            };
            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Withdraw Min > Withdraw Max).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.WithdrawAmountIsInvalid, exception.Code);
            }
        }

        [Test]
        public void TestSavingBookProduct_When_WithdrawMin_And_WithdrawMan_Are_Zero()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                WithdrawingMin = 0,
                WithdrawingMax = 0,
                TransferMin = 100,
                TransferMax = 300,
                InterestRateMin = 0.12,
                InterestRateMax = 0.20,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Daily,
                InterestFrequency = OSavingInterestFrequency.EndOfMonth,
                ClientType = OClientTypes.All,
                EntryFees = 0,
                Currency = new Currency { Id = 1 },
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 5,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
            };
            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Withdraw Min And Withdraw Max = 0).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.WithdrawAmountIsInvalid, exception.Code);
            }
        }

       

        [Test]
        public void TestSavingBookProduct_When_InterestRateMin_And_InterestRate_AreNull()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                WithdrawingMin = 100,
                WithdrawingMax = 200,
                TransferMin = 100,
                TransferMax = 300,
                InterestRateMax = 0.20,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Daily,
                InterestFrequency = OSavingInterestFrequency.EndOfMonth,
                ClientType = OClientTypes.All,
                EntryFees = 0,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 1,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
                Currency = new Currency { Id = 1 },
            };
            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Interest Rate Min and Interest Rate are Null).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.InterestRateMinMaxIsInvalid, exception.Code);
            }
        }

        

        [Test]
        public void TestSavingBookProduct_When_InterestRateMax_And_InterestRate_AreNull()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                WithdrawingMin = 100,
                WithdrawingMax = 200,
                TransferMin = 100,
                TransferMax = 300,
                InterestRateMin = 0.20,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Daily,
                InterestFrequency = OSavingInterestFrequency.EndOfMonth,
                ClientType = OClientTypes.All,
                EntryFees = 0,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 1,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
                Currency = new Currency { Id = 1 },
            };
            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Interest Rate Max and Interest Rate are Null).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.InterestRateMinMaxIsInvalid, exception.Code);
            }
        }

        

        [Test]
        public void TestSavingBookProduct_When_InterestRateMin_IsNegative()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                WithdrawingMin = 100,
                WithdrawingMax = 200,
                TransferMin = 100,
                TransferMax = 300,
                InterestRateMin = -0.1,
                InterestRateMax = 0.15,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Daily,
                InterestFrequency = OSavingInterestFrequency.EndOfMonth,
                ClientType = OClientTypes.All,
                EntryFees = 0,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 1,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
                Currency = new Currency { Id = 1 },
            };
            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Interest Rate Min < 0).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.InterestRateMinIsInvalid, exception.Code);
            }
        }

       

        [Test]
        public void TestSavingBookProduct_When_InterestRateMin_IsGreaterThan_InterestRateMax()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                WithdrawingMin = 100,
                WithdrawingMax = 200,
                TransferMin = 100,
                TransferMax = 300,
                InterestRateMin = 0.20,
                InterestRateMax = 0.15,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Daily,
                InterestFrequency = OSavingInterestFrequency.EndOfMonth,
                ClientType = OClientTypes.All,
                EntryFees = 0,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 1,
                Currency = new Currency { Id = 1 },
            };
            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Interest Rate Max and Interest Rate are Null).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.InterestRateMinMaxIsInvalid, exception.Code);
            }
        }

       

        [Test]
        public void TestSavingBookProduct_When_InterestRate_IsNegative()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                WithdrawingMin = 100,
                WithdrawingMax = 200,
                TransferMin = 100,
                TransferMax = 300,
                InterestRate = -0.1,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Daily,
                InterestFrequency = OSavingInterestFrequency.EndOfMonth,
                ClientType = OClientTypes.All,
                EntryFees = 0,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 1,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
                Currency = new Currency { Id = 1 },
            };
            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Interest Rate <0).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.InterestRateIsInvalid, exception.Code);
            }
        }

        

        [Test]
        public void TestSavingBookProduct_When_InterestBase_IsNull()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                WithdrawingMin = 100,
                WithdrawingMax = 200,
                TransferMin = 100,
                TransferMax = 300,
                InterestRate = 0.2,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestFrequency = OSavingInterestFrequency.EndOfMonth,
                ClientType = OClientTypes.All,
                EntryFees = 0,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 1,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
                Currency = new Currency { Id = 1 },
            };
            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Interest Base is Null).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.InterestsBaseIsInvalid, exception.Code);
            }
        }

        [Test]
        public void TestSavingBookProduct_When_InterestFrequency_IsNull()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                WithdrawingMin = 100,
                WithdrawingMax = 200,
                TransferMin = 100,
                TransferMax = 300,
                InterestRate = 0.2,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Daily,
                ClientType = OClientTypes.All,
                EntryFees = 0,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 1,
                Currency = new Currency { Id = 1 },
            };
            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Interest Frequency is Null).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.InterestsFrequencyIsInvalid, exception.Code);
            }
        }

        [Test]
        public void TestSavingBookProduct_When_InterestBaseIs_Weekly_And_InterestFrequencyIs_EndOfDay()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                WithdrawingMin = 100,
                WithdrawingMax = 200,
                TransferMin = 100,
                TransferMax = 300,
                InterestRate = 0.2,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Weekly,
                InterestFrequency = OSavingInterestFrequency.EndOfDay,
                ClientType = OClientTypes.All,
                EntryFees = 0,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 1,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
                Currency = new Currency { Id = 1 },
            };
            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Interest Frequency is incorrect).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.InterestBaseIncompatibleFrequency, exception.Code);
            }
        }

        [Test]
        public void TestSavingBookProduct_When_InterestBaseIs_Monthly_And_InterestFrequencyIs_EndOfDay()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                WithdrawingMin = 100,
                WithdrawingMax = 200,
                TransferMin = 100,
                TransferMax = 300,
                InterestRate = 0.2,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfDay,
                ClientType = OClientTypes.All,
                EntryFees = 0,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 1,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
                Currency = new Currency { Id = 1 },
            };
            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Interest Frequency is incorrect).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.InterestBaseIncompatibleFrequency, exception.Code);
            }
        }

        [Test]
        public void TestSavingBookProduct_When_InterestBaseIs_Monthly_And_InterestFrequencyIs_EndOfWeek()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                WithdrawingMin = 100,
                WithdrawingMax = 200,
                TransferMin = 100,
                TransferMax = 300,
                InterestRate = 0.2,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfWeek,
                ClientType = OClientTypes.All,
                EntryFees = 0,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 1,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
                Currency = new Currency { Id = 1 },
            };
            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Interest Frequency is incorrect).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.InterestBaseIncompatibleFrequency, exception.Code);
            }
        }

        [Test]
        public void TestSavingBookProduct_When_InterestBaseIs_Weekly_And_InterestFrequencyIs_EndOfWeek()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                ChequeDepositMin = 100,
                ChequeDepositMax = 200,
                ChequeDepositFeesMin = 1,
                ChequeDepositFeesMax = 5,
                WithdrawingMin = 100,
                WithdrawingMax = 200,
                TransferMin = 100,
                TransferMax = 300,
                InterestRate = 0.2,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Weekly,
                InterestFrequency = OSavingInterestFrequency.EndOfWeek,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount,
                ClientType = OClientTypes.All,
                EntryFees = 0,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 1,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
                DepositFeesMin = 1,
                DepositFeesMax = 5,
                CloseFeesMin = 1,
                CloseFeesMax = 5,
                ManagementFeesMin = 1,
                ManagementFeesMax = 5,
                OverdraftFeesMin = 1,
                OverdraftFeesMax = 5,
                AgioFeesMin = 1,
                AgioFeesMax = 5,
                ReopenFeesMin = 1,
                ReopenFeesMax = 5,
                Currency = new Currency { Id = 1 },
            };
            savingsProduct.InterBranchTransferFee.Value = 10;

            _savingProductManagerMock.ExpectAndReturn("Add", 1, savingsProduct);

            Assert.AreEqual(_savingProductServices.SaveProduct(savingsProduct), 1);
        }

        [Test]
        public void TestSavingBookProduct_When_InterestBaseIs_Weekly_And_InterestFrequencyIs_EndOfMonth()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                ChequeDepositMin = 100,
                ChequeDepositMax = 200,
                ChequeDepositFeesMin = 1,
                ChequeDepositFeesMax = 5,
                WithdrawingMin = 100,
                WithdrawingMax = 200,
                TransferMin = 100,
                TransferMax = 300,
                InterestRate = 0.2,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Weekly,
                InterestFrequency = OSavingInterestFrequency.EndOfMonth,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount,
                ClientType = OClientTypes.All,
                EntryFees = 0,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 1,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
                DepositFeesMin = 1,
                DepositFeesMax = 5,
                CloseFeesMin = 1,
                CloseFeesMax = 5,
                ManagementFeesMin = 1,
                ManagementFeesMax = 5,
                OverdraftFeesMin = 1,
                OverdraftFeesMax = 5,
                AgioFeesMin = 1,
                AgioFeesMax = 5,
                ReopenFeesMin = 1,
                ReopenFeesMax = 5,
                Currency = new Currency { Id = 1 },
            };
            savingsProduct.InterBranchTransferFee.Value = 10;

            _savingProductManagerMock.ExpectAndReturn("Add", 1, savingsProduct);

            Assert.AreEqual(_savingProductServices.SaveProduct(savingsProduct), 1);
        }

        [Test]
        public void TestSavingBookProduct_When_InterestBaseIs_Weekly_And_InterestFrequencyIs_EndOfYear()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                ChequeDepositMin = 100,
                ChequeDepositMax = 200,
                ChequeDepositFeesMin = 1,
                ChequeDepositFeesMax = 5,
                WithdrawingMin = 100,
                WithdrawingMax = 200,
                TransferMin = 100,
                TransferMax = 300,
                InterestRate = 0.2,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Weekly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount,
                ClientType = OClientTypes.All,
                EntryFees = 0,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 1,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
                DepositFeesMin = 1,
                DepositFeesMax = 5,
                CloseFeesMin = 1,
                CloseFeesMax = 5,
                ManagementFeesMin = 1,
                ManagementFeesMax = 5,
                OverdraftFeesMin = 1,
                OverdraftFeesMax = 5,
                AgioFeesMin = 1,
                AgioFeesMax = 5,
                ReopenFeesMin = 1,
                ReopenFeesMax = 5,
                Currency = new Currency { Id = 1 },
            };
            savingsProduct.InterBranchTransferFee.Value = 10;

            _savingProductManagerMock.ExpectAndReturn("Add", 1, savingsProduct);

            Assert.AreEqual(_savingProductServices.SaveProduct(savingsProduct), 1);
        }

        [Test]
        public void TestSavingBookProduct_When_InterestBaseIs_Monthly_And_InterestFrequencyIs_EndOfMonth()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                ChequeDepositMin = 100,
                ChequeDepositMax = 200,
                ChequeDepositFeesMin = 1,
                ChequeDepositFeesMax = 5,
                WithdrawingMin = 100,
                WithdrawingMax = 200,
                TransferMin = 100,
                TransferMax = 300,
                InterestRate = 0.2,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfMonth,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount,
                ClientType = OClientTypes.All,
                EntryFees = 0,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 1,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
                DepositFeesMin = 1,
                DepositFeesMax = 5,
                CloseFeesMin = 1,
                CloseFeesMax = 5,
                ManagementFeesMin = 1,
                ManagementFeesMax = 5,
                OverdraftFeesMin = 1,
                OverdraftFeesMax = 5,
                AgioFeesMin = 1,
                AgioFeesMax = 5,
                ReopenFeesMin = 1,
                ReopenFeesMax = 5,
                Currency = new Currency { Id = 1 },
            };
            savingsProduct.InterBranchTransferFee.Value = 10;

            _savingProductManagerMock.ExpectAndReturn("Add", 1, savingsProduct);

            Assert.AreEqual(_savingProductServices.SaveProduct(savingsProduct), 1);
        }

        [Test]
        public void TestSavingBookProduct_When_InterestBaseIs_Monthly_And_InterestFrequencyIs_EndOfYear()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                ChequeDepositMin = 100,
                ChequeDepositMax = 200,
                ChequeDepositFeesMin = 1,
                ChequeDepositFeesMax = 5,
                WithdrawingMin = 100,
                WithdrawingMax = 200,
                TransferMin = 100,
                TransferMax = 300,
                InterestRate = 0.2,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount,
                ClientType = OClientTypes.All,
                EntryFees = 0,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 1,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
                DepositFeesMin = 1,
                DepositFeesMax = 5,
                CloseFeesMin = 1,
                CloseFeesMax = 5,
                ManagementFeesMin = 1,
                ManagementFeesMax = 5,
                OverdraftFeesMin = 1,
                OverdraftFeesMax = 5,
                AgioFeesMin = 1,
                AgioFeesMax = 5,
                ReopenFeesMin = 1,
                ReopenFeesMax = 5,
                Currency = new Currency { Id = 1 },
            };
            savingsProduct.InterBranchTransferFee.Value = 10;

            _savingProductManagerMock.ExpectAndReturn("Add", 1, savingsProduct);

            Assert.AreEqual(_savingProductServices.SaveProduct(savingsProduct), 1);
        }

        [Test]
        public void TestSavingBookProduct_When_ClientType_IsNull()
        {
            Assert.Ignore();
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                DepositFees = 10,
                WithdrawingMin = 100,
                WithdrawingMax = 200,
                TransferMin = 100,
                TransferMax = 300,
                InterestRate = 0.2,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Daily,
                InterestFrequency = OSavingInterestFrequency.EndOfMonth,
                EntryFees = 0,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 1,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
                Currency = new Currency { Id = 1 },
                ManagementFeeFreq = new InstallmentType {Id = 1, Name = "Monthly", NbOfDays = 0, NbOfMonths = 1},
                CloseFees = 10,
                ManagementFees = 10
            };
            try
            {
                _savingProductServices.SaveProduct(savingsProduct, 0);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Client Type is Null).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.ClientTypeIsInvalid, exception.Code);
            }
        }

       

        [Test]
        public void TestSavingBookProduct_When_EntryFees_IsNull_And_EntryFeesMin_IsNull()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                WithdrawingMin = 100,
                WithdrawingMax = 200,
                TransferMin = 100,
                TransferMax = 300,
                InterestRate = 0.2,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount,
                ClientType = OClientTypes.All,
                EntryFeesMax = 20,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 1,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
                Currency = new Currency { Id = 1 },
            };

            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Entry Fees and Entry Fees Min are Null).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.EntryFeesMinMaxIsInvalid, exception.Code);
            }
        }

        

        [Test]
        public void TestSavingBookProduct_When_EntryFees_IsNull_And_EntryFeesMax_IsNull()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                WithdrawingMin = 100,
                WithdrawingMax = 200,
                TransferMin = 100,
                TransferMax = 300,
                InterestRate = 0.2,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount,
                ClientType = OClientTypes.All,
                EntryFeesMin = 20,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 1,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
                Currency = new Currency { Id = 1 },
            };

            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Entry Fees and Entry Fees Max are Null).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.EntryFeesMinMaxIsInvalid, exception.Code);
            }
        }

       

        [Test]
        public void TestSavingBookProduct_When_EntryFeesMin_IsGreater_Than_EntryFeesMax()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                WithdrawingMin = 100,
                WithdrawingMax = 200,
                TransferMin = 100,
                TransferMax = 300,
                InterestRate = 0.2,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount,
                ClientType = OClientTypes.All,
                EntryFeesMax = 20,
                EntryFeesMin = 25,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 1,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
                Currency = new Currency { Id = 1 },
            };

            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Entry Fees Min is Greater than Entry Fees Max).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.EntryFeesMinMaxIsInvalid, exception.Code);
            }
        }

    
        [Test]
        public void TestSavingBookProduct_When_EntryFees_IsNegative()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                WithdrawingMin = 100,
                WithdrawingMax = 200,
                TransferMin = 100,
                TransferMax = 300,
                InterestRate = 0.2,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount,
                ClientType = OClientTypes.All,
                EntryFees = -1,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 1,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
                Currency = new Currency { Id = 1 },
            };

            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Entry Fees is Negative).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.EntryFeesIsInvalid, exception.Code);
            }
        }

      

        [Test]
        public void TestSavingBookProduct_When_EntryFees_IsCorrect()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                ChequeDepositMin = 100,
                ChequeDepositMax = 200,
                ChequeDepositFeesMin = 1,
                ChequeDepositFeesMax = 5,
                WithdrawingMin = 100,
                WithdrawingMax = 200,
                TransferMin = 100,
                TransferMax = 300,
                InterestRate = 0.2,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount,
                ClientType = OClientTypes.All,
                EntryFees = 5,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 1,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
                DepositFeesMin = 1,
                DepositFeesMax = 5,
                CloseFeesMin = 1,
                CloseFeesMax = 5,
                ManagementFeesMin = 1,
                ManagementFeesMax = 5,
                OverdraftFeesMin = 1,
                OverdraftFeesMax = 5,
                AgioFeesMin = 1,
                AgioFeesMax = 5,
                ReopenFeesMin = 1,
                ReopenFeesMax = 5,
                Currency = new Currency { Id = 1 },
            };
            savingsProduct.InterBranchTransferFee.Value = 10;

            _savingProductManagerMock.ExpectAndReturn("Add", 1, savingsProduct);

            Assert.AreEqual(_savingProductServices.SaveProduct(savingsProduct), 1);
        }

        

        [Test]
        public void TestSavingBookProduct_When_EntryFeesMin_And_EntryFeesMax_AreCorrect()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                ChequeDepositMin = 100,
                ChequeDepositMax = 200,
                ChequeDepositFeesMin = 1,
                ChequeDepositFeesMax = 5,
                WithdrawingMin = 100,
                WithdrawingMax = 200,
                TransferMin = 100,
                TransferMax = 300,
                InterestRate = 0.2,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount,
                ClientType = OClientTypes.All,
                EntryFeesMin = 5,
                EntryFeesMax = 15,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 1,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
                DepositFeesMin = 1,
                DepositFeesMax = 5,
                CloseFeesMin = 1,
                CloseFeesMax = 5,
                ManagementFeesMin = 1,
                ManagementFeesMax = 5,
                OverdraftFeesMin = 1,
                OverdraftFeesMax = 5,
                AgioFeesMin = 1,
                AgioFeesMax = 5,
                ReopenFeesMin = 1,
                ReopenFeesMax = 5,
                Currency = new Currency { Id = 1 },
            };
            savingsProduct.InterBranchTransferFee.Value = 10;

            _savingProductManagerMock.ExpectAndReturn("Add", 1, savingsProduct);

            Assert.AreEqual(_savingProductServices.SaveProduct(savingsProduct), 1);
        }

        [Test]
        public void TestSavingBookProduct_When_WithdrawFeesType_Is_Empty()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                ChequeDepositMin = 100,
                ChequeDepositMax = 200,
                ChequeDepositFeesMin = 1,
                ChequeDepositFeesMax = 5,
                WithdrawingMin = 100,
                WithdrawingMax = 200,
                TransferMin = 100,
                TransferMax = 300,
                InterestRate = 0.2,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount,
                ClientType = OClientTypes.All,
                EntryFees = 10,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
                Currency = new Currency { Id = 1 },
                DepositFeesMin = 1,
                DepositFeesMax = 5,
                CloseFeesMin = 1,
                CloseFeesMax = 5,
                ManagementFeesMin = 1,
                ManagementFeesMax = 5,
                OverdraftFeesMin = 1,
                OverdraftFeesMax = 5,
                AgioFeesMin = 1,
                AgioFeesMax = 5,
                ReopenFeesMin = 1,
                ReopenFeesMax = 5
            };

            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Withdraw Fees Type is Empty).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.WithdrawFeesTypeEmpty, exception.Code);
            }
        }

        [Test]
        public void TestSavingBookProduct_When_WithdrawFeesType_Is_Flat_And_FlatWithdrawFees_Are_Null()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                ChequeDepositMin = 100,
                ChequeDepositMax = 200,
                ChequeDepositFeesMin = 1,
                ChequeDepositFeesMax = 5,
                WithdrawingMin = 100,
                WithdrawingMax = 200,
                TransferMin = 100,
                TransferMax = 300,
                InterestRate = 0.2,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount,
                ClientType = OClientTypes.All,
                EntryFees = 10,
                Currency = new Currency { Id = 1 },
                WithdrawFeesType = OSavingsFeesType.Flat,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
                DepositFeesMin = 1,
                DepositFeesMax = 5,
                CloseFeesMin = 1,
                CloseFeesMax = 5,
                ManagementFeesMin = 1,
                ManagementFeesMax = 5,
                OverdraftFeesMin = 1,
                OverdraftFeesMax = 5,
                AgioFeesMin = 1,
                AgioFeesMax = 5,
                ReopenFeesMin = 1,
                ReopenFeesMax = 5
            };

            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Flat Withdraw Fees are Empty).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.FlatWithdrawFeesMinMaxIsInvalid, exception.Code);
            }
        }

        [Test]
        public void TestSavingBookProduct_When_WithdrawFeesType_Is_Rate_And_RateWithdrawFees_Are_Null()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                ChequeDepositMin = 100,
                ChequeDepositMax = 200,
                ChequeDepositFeesMin = 1,
                ChequeDepositFeesMax = 5,
                WithdrawingMin = 100,
                WithdrawingMax = 200,
                TransferMin = 100,
                TransferMax = 300,
                InterestRate = 0.2,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount,
                ClientType = OClientTypes.All,
                EntryFees = 10,
                Currency = new Currency { Id = 1 },
                WithdrawFeesType = OSavingsFeesType.Rate,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
                DepositFeesMin = 1,
                DepositFeesMax = 5,
                CloseFeesMin = 1,
                CloseFeesMax = 5,
                ManagementFeesMin = 1,
                ManagementFeesMax = 5,
                OverdraftFeesMin = 1,
                OverdraftFeesMax = 5,
                AgioFeesMin = 1,
                AgioFeesMax = 5,
                ReopenFeesMin = 1,
                ReopenFeesMax = 5
            };

            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Rate Withdraw Fees are Empty).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.RateWithdrawFeesMinMaxIsInvalid, exception.Code);
            }
        }

        [Test]
        public void TestSavingBookProduct_When_WithdrawFeesType_Is_Flat_And_FlatWithdrawFees_Is_Negative()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                ChequeDepositMin = 100,
                ChequeDepositMax = 200,
                ChequeDepositFeesMin = 1,
                ChequeDepositFeesMax = 5,
                WithdrawingMin = 100,
                WithdrawingMax = 200,
                TransferMin = 100,
                TransferMax = 300,
                InterestRate = 0.2,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount,
                ClientType = OClientTypes.All,
                EntryFees = 10,
                Currency = new Currency { Id = 1 },
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = -1,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
                DepositFeesMin = 1,
                DepositFeesMax = 5,
                CloseFeesMin = 1,
                CloseFeesMax = 5,
                ManagementFeesMin = 1,
                ManagementFeesMax = 5,
                OverdraftFeesMin = 1,
                OverdraftFeesMax = 5,
                AgioFeesMin = 1,
                AgioFeesMax = 5,
                ReopenFeesMin = 1,
                ReopenFeesMax = 5
            };

            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Flat Withdraw Fees is Negative).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.FlatWithdrawFeesIsInvalid, exception.Code);
            }
        }

        [Test]
        public void TestSavingBookProduct_When_WithdrawFeesType_Is_Flat_And_FlatWithdrawFeesMin_Is_Negative()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                ChequeDepositMin = 100,
                ChequeDepositMax = 200,
                ChequeDepositFeesMin = 1,
                ChequeDepositFeesMax = 5,
                WithdrawingMin = 100,
                WithdrawingMax = 200,
                TransferMin = 100,
                TransferMax = 300,
                InterestRate = 0.2,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount,
                ClientType = OClientTypes.All,
                EntryFees = 10,
                Currency = new Currency { Id = 1 },
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFeesMin = -1,
                FlatWithdrawFeesMax = 2,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
                DepositFeesMin = 1,
                DepositFeesMax = 5,
                CloseFeesMin = 1,
                CloseFeesMax = 5,
                ManagementFeesMin = 1,
                ManagementFeesMax = 5,
                OverdraftFeesMin = 1,
                OverdraftFeesMax = 5,
                AgioFeesMin = 1,
                AgioFeesMax = 5,
                ReopenFeesMin = 1,
                ReopenFeesMax = 5
            };

            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Flat Withdraw Fees Min is Negative).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.FlatWithdrawFeesMinIsInvalid, exception.Code);
            }
        }

        [Test]
        public void TestSavingBookProduct_When_WithdrawFeesType_Is_Flat_And_FlatWithdrawFeesMin_IsHigher_Than_FlatWithdrawFeesMax()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                ChequeDepositMin = 100,
                ChequeDepositMax = 200,
                ChequeDepositFeesMin = 1,
                ChequeDepositFeesMax = 5,
                WithdrawingMin = 100,
                WithdrawingMax = 200,
                TransferMin = 100,
                TransferMax = 300,
                InterestRate = 0.2,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount,
                ClientType = OClientTypes.All,
                EntryFees = 10,
                Currency = new Currency { Id = 1 },
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFeesMin = 5,
                FlatWithdrawFeesMax = 2,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
                DepositFeesMin = 1,
                DepositFeesMax = 5,
                CloseFeesMin = 1,
                CloseFeesMax = 5,
                ManagementFeesMin = 1,
                ManagementFeesMax = 5,
                OverdraftFeesMin = 1,
                OverdraftFeesMax = 5,
                AgioFeesMin = 1,
                AgioFeesMax = 5,
                ReopenFeesMin = 1,
                ReopenFeesMax = 5
            };

            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Flat Withdraw Fees Min is Higher than Flat Withdraw Fees Max).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.FlatWithdrawFeesMinMaxIsInvalid, exception.Code);
            }
        }

        [Test]
        public void TestSavingBookProduct_When_WithdrawFeesType_Is_Flat_And_FlatWithdrawFees_Is_Correct()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                ChequeDepositMin = 100,
                ChequeDepositMax = 200,
                ChequeDepositFeesMin = 1,
                ChequeDepositFeesMax = 5,
                WithdrawingMin = 100,
                WithdrawingMax = 200,
                TransferMin = 100,
                TransferMax = 300,
                InterestRate = 0.2,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount,
                ClientType = OClientTypes.All,
                EntryFees = 10,
                Currency = new Currency { Id = 1 },
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 5,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
                DepositFeesMin = 1,
                DepositFeesMax = 5,
                CloseFeesMin = 1,
                CloseFeesMax = 5,
                ManagementFeesMin = 1,
                ManagementFeesMax = 5,
                OverdraftFeesMin = 1,
                OverdraftFeesMax = 5,
                AgioFeesMin = 1,
                AgioFeesMax = 5,
                ReopenFeesMin = 1,
                ReopenFeesMax = 5
            };
            savingsProduct.InterBranchTransferFee.Value = 10;

            _savingProductManagerMock.ExpectAndReturn("Add", 1, savingsProduct);

            Assert.AreEqual(_savingProductServices.SaveProduct(savingsProduct), 1);
        }

        [Test]
        public void TestSavingBookProduct_When_WithdrawFeesType_Is_Flat_And_FlatWithdrawFeesMinMax_Are_Correct()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                ChequeDepositMin = 100,
                ChequeDepositMax = 200,
                ChequeDepositFeesMin = 1,
                ChequeDepositFeesMax = 5,
                WithdrawingMin = 100,
                WithdrawingMax = 200,
                TransferMin = 100,
                TransferMax = 300,
                InterestRate = 0.2,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount,
                ClientType = OClientTypes.All,
                EntryFees = 10,
                Currency = new Currency { Id = 1 },
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFeesMin = 1,
                FlatWithdrawFeesMax = 5,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
                DepositFeesMin = 1,
                DepositFeesMax = 5,
                CloseFeesMin = 1,
                CloseFeesMax = 5,
                ManagementFeesMin = 1,
                ManagementFeesMax = 5,
                OverdraftFeesMin = 1,
                OverdraftFeesMax = 5,
                AgioFeesMin = 1,
                AgioFeesMax = 5,
                ReopenFeesMin = 1,
                ReopenFeesMax = 5
            };
            savingsProduct.InterBranchTransferFee.Value = 10;

            _savingProductManagerMock.ExpectAndReturn("Add", 1, savingsProduct);

            Assert.AreEqual(_savingProductServices.SaveProduct(savingsProduct), 1);
        }

        [Test]
        public void TestSavingBookProduct_When_WithdrawFeesType_Is_Rate_And_RateWithdrawFees_Is_Negative()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                ChequeDepositMin = 100,
                ChequeDepositMax = 200,
                ChequeDepositFeesMin = 1,
                ChequeDepositFeesMax = 5,
                WithdrawingMin = 100,
                WithdrawingMax = 200,
                TransferMin = 100,
                TransferMax = 300,
                InterestRate = 0.2,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount,
                ClientType = OClientTypes.All,
                EntryFees = 10,
                Currency = new Currency { Id = 1 },
                WithdrawFeesType = OSavingsFeesType.Rate,
                RateWithdrawFees = -0.1,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
                DepositFeesMin = 1,
                DepositFeesMax = 5,
                CloseFeesMin = 1,
                CloseFeesMax = 5,
                ManagementFeesMin = 1,
                ManagementFeesMax = 5,
                OverdraftFeesMin = 1,
                OverdraftFeesMax = 5,
                AgioFeesMin = 1,
                AgioFeesMax = 5,
                ReopenFeesMin = 1,
                ReopenFeesMax = 5
            };

            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Rate Withdraw Fees is Negative).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.RateWithdrawFeesIsInvalid, exception.Code);
            }
        }

        [Test]
        public void TestSavingBookProduct_When_WithdrawFeesType_Is_Rate_And_RateWithdrawFeesMin_Is_Negative()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                ChequeDepositMin = 100,
                ChequeDepositMax = 200,
                ChequeDepositFeesMin = 1,
                ChequeDepositFeesMax = 5,
                WithdrawingMin = 100,
                WithdrawingMax = 200,
                TransferMin = 100,
                TransferMax = 300,
                InterestRate = 0.2,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount,
                ClientType = OClientTypes.All,
                EntryFees = 10,
                Currency = new Currency { Id = 1 },
                WithdrawFeesType = OSavingsFeesType.Rate,
                RateWithdrawFeesMin = -0.1,
                RateWithdrawFeesMax = 0.2,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
                DepositFeesMin = 1,
                DepositFeesMax = 5,
                CloseFeesMin = 1,
                CloseFeesMax = 5,
                ManagementFeesMin = 1,
                ManagementFeesMax = 5,
                OverdraftFeesMin = 1,
                OverdraftFeesMax = 5,
                AgioFeesMin = 1,
                AgioFeesMax = 5,
                ReopenFeesMin = 1,
                ReopenFeesMax = 5
            };

            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Rate Withdraw Fees Min is Negative).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.RateWithdrawFeesMinIsInvalid, exception.Code);
            }
        }

        [Test]
        public void TestSavingBookProduct_When_WithdrawFeesType_Is_Rate_And_RateWithdrawFeesMin_IsHigher_Than_RateWithdrawFeesMax()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                ChequeDepositMin = 100,
                ChequeDepositMax = 200,
                ChequeDepositFeesMin = 1,
                ChequeDepositFeesMax = 5,
                WithdrawingMin = 100,
                WithdrawingMax = 200,
                TransferMin = 100,
                TransferMax = 300,
                InterestRate = 0.2,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount,
                ClientType = OClientTypes.All,
                EntryFees = 10,
                Currency = new Currency { Id = 1 },
                WithdrawFeesType = OSavingsFeesType.Rate,
                RateWithdrawFeesMin = 0.5,
                RateWithdrawFeesMax = 0.2,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
                DepositFeesMin = 1,
                DepositFeesMax = 5,
                CloseFeesMin = 1,
                CloseFeesMax = 5,
                ManagementFeesMin = 1,
                ManagementFeesMax = 5,
                OverdraftFeesMin = 1,
                OverdraftFeesMax = 5,
                AgioFeesMin = 1,
                AgioFeesMax = 5,
                ReopenFeesMin = 1,
                ReopenFeesMax = 5
            };

            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Rate Withdraw Fees Min is Higher than Rate Withdraw Fees Max).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.RateWithdrawFeesMinMaxIsInvalid, exception.Code);
            }
        }

        [Test]
        public void TestSavingBookProduct_When_WithdrawFeesType_Is_Rate_And_RateWithdrawFees_Is_Correct()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                ChequeDepositMin = 100,
                ChequeDepositMax = 200,
                ChequeDepositFeesMin = 1,
                ChequeDepositFeesMax = 5,
                WithdrawingMin = 100,
                WithdrawingMax = 200,
                TransferMin = 100,
                TransferMax = 300,
                InterestRate = 0.2,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount,
                ClientType = OClientTypes.All,
                EntryFees = 10,
                Currency = new Currency { Id = 1 },
                WithdrawFeesType = OSavingsFeesType.Rate,
                RateWithdrawFees = 0.5,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
                DepositFeesMin = 1,
                DepositFeesMax = 5,
                CloseFeesMin = 1,
                CloseFeesMax = 5,
                ManagementFeesMin = 1,
                ManagementFeesMax = 5,
                OverdraftFeesMin = 1,
                OverdraftFeesMax = 5,
                AgioFeesMin = 1,
                AgioFeesMax = 5,
                ReopenFeesMin = 1,
                ReopenFeesMax = 5
            };
            savingsProduct.InterBranchTransferFee.Value = 10;

            _savingProductManagerMock.ExpectAndReturn("Add", 1, savingsProduct);

            Assert.AreEqual(_savingProductServices.SaveProduct(savingsProduct), 1);
        }

        [Test]
        public void TestSavingBookProduct_When_WithdrawFeesType_Is_Rate_And_RateWithdrawFeesMinMax_Are_Correct()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                ChequeDepositMin = 100,
                ChequeDepositMax = 200,
                ChequeDepositFeesMin = 1,
                ChequeDepositFeesMax = 5,
                WithdrawingMin = 100,
                WithdrawingMax = 200,
                TransferMin = 100,
                TransferMax = 300,
                InterestRate = 0.2,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount,
                ClientType = OClientTypes.All,
                EntryFees = 10,
                Currency = new Currency { Id = 1 },
                WithdrawFeesType = OSavingsFeesType.Rate,
                RateWithdrawFeesMin = 0.1,
                RateWithdrawFeesMax = 0.5,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
                DepositFeesMin = 1,
                DepositFeesMax = 5,
                CloseFeesMin = 1,
                CloseFeesMax = 5,
                ManagementFeesMin = 1,
                ManagementFeesMax = 5,
                OverdraftFeesMin = 1,
                OverdraftFeesMax = 5,
                AgioFeesMin = 1,
                AgioFeesMax = 5,
                ReopenFeesMin = 1,
                ReopenFeesMax = 5
            };
            savingsProduct.InterBranchTransferFee.Value = 10;

            _savingProductManagerMock.ExpectAndReturn("Add", 1, savingsProduct);

            Assert.AreEqual(_savingProductServices.SaveProduct(savingsProduct), 1);
        }

        [Test]
        public void TestSavingBookProduct_When_TransferFeesType_Is_Empty()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                ChequeDepositMin = 100,
                ChequeDepositMax = 200,
                ChequeDepositFeesMin = 1,
                ChequeDepositFeesMax = 5,
                WithdrawingMin = 100,
                WithdrawingMax = 200,
                TransferMin = 100,
                TransferMax = 300,
                InterestRate = 0.2,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount,
                ClientType = OClientTypes.All,
                EntryFees = 10,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 1,
                DepositFeesMin = 1,
                DepositFeesMax = 5,
                CloseFeesMin = 1,
                CloseFeesMax = 5,
                ManagementFeesMin = 1,
                ManagementFeesMax = 5,
                OverdraftFeesMin = 1,
                OverdraftFeesMax = 5,
                AgioFeesMin = 1,
                AgioFeesMax = 5,
                ReopenFeesMin = 1,
                ReopenFeesMax = 5,
                Currency = new Currency { Id = 1 },
            };

            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Withdraw Fees Type is Empty).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.TransferFeesTypeEmpty, exception.Code);
            }
        }

        [Test]
        public void TestSavingBookProduct_When_TransferFeesType_Is_Flat_And_FlatTransferFees_Are_Null()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                ChequeDepositMin = 100,
                ChequeDepositMax = 200,
                ChequeDepositFeesMin = 1,
                ChequeDepositFeesMax = 5,
                WithdrawingMin = 100,
                WithdrawingMax = 200,
                TransferMin = 100,
                TransferMax = 300,
                InterestRate = 0.2,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount,
                ClientType = OClientTypes.All,
                EntryFees = 10,
                Currency = new Currency { Id = 1 },
                TransferFeesType = OSavingsFeesType.Flat,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 1,
                DepositFeesMin = 1,
                DepositFeesMax = 5,
                CloseFeesMin = 1,
                CloseFeesMax = 5,
                ManagementFeesMin = 1,
                ManagementFeesMax = 5,
                OverdraftFeesMin = 1,
                OverdraftFeesMax = 5,
                AgioFeesMin = 1,
                AgioFeesMax = 5,
                ReopenFeesMin = 1,
                ReopenFeesMax = 5
            };

            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Flat Withdraw Fees are Empty).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.FlatTransferFeesMinMaxIsInvalid, exception.Code);
            }
        }

        [Test]
        public void TestSavingBookProduct_When_TransferFeesType_Is_Rate_And_RateTransferFees_Are_Null()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                ChequeDepositMin = 100,
                ChequeDepositMax = 200,
                ChequeDepositFeesMin = 1,
                ChequeDepositFeesMax = 5,
                WithdrawingMin = 100,
                WithdrawingMax = 200,
                TransferMin = 100,
                TransferMax = 300,
                InterestRate = 0.2,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount,
                ClientType = OClientTypes.All,
                EntryFees = 10,
                Currency = new Currency { Id = 1 },
                TransferFeesType = OSavingsFeesType.Rate,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 1,
                DepositFeesMin = 1,
                DepositFeesMax = 5,
                CloseFeesMin = 1,
                CloseFeesMax = 5,
                ManagementFeesMin = 1,
                ManagementFeesMax = 5,
                OverdraftFeesMin = 1,
                OverdraftFeesMax = 5,
                AgioFeesMin = 1,
                AgioFeesMax = 5,
                ReopenFeesMin = 1,
                ReopenFeesMax = 5
            };

            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Rate Withdraw Fees are Empty).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.RateTransferFeesMinMaxIsInvalid, exception.Code);
            }
        }

        [Test]
        public void TestSavingBookProduct_When_TransferFeesType_Is_Flat_And_FlatTransferFees_Is_Negative()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                ChequeDepositMin = 100,
                ChequeDepositMax = 200,
                ChequeDepositFeesMin = 1,
                ChequeDepositFeesMax = 5,
                WithdrawingMin = 100,
                WithdrawingMax = 200,
                TransferMin = 100,
                TransferMax = 300,
                InterestRate = 0.2,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount,
                ClientType = OClientTypes.All,
                EntryFees = 10,
                Currency = new Currency { Id = 1 },
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = -1,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 1,
                DepositFeesMin = 1,
                DepositFeesMax = 5,
                CloseFeesMin = 1,
                CloseFeesMax = 5,
                ManagementFeesMin = 1,
                ManagementFeesMax = 5,
                OverdraftFeesMin = 1,
                OverdraftFeesMax = 5,
                AgioFeesMin = 1,
                AgioFeesMax = 5,
                ReopenFeesMin = 1,
                ReopenFeesMax = 5
            };

            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Flat Withdraw Fees is Negative).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.FlatTransferFeesIsInvalid, exception.Code);
            }
        }

        [Test]
        public void TestSavingBookProduct_When_TransferFeesType_Is_Flat_And_FlatTransferFeesMin_Is_Negative()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                ChequeDepositMin = 100,
                ChequeDepositMax = 200,
                ChequeDepositFeesMin = 1,
                ChequeDepositFeesMax = 5,
                WithdrawingMin = 100,
                WithdrawingMax = 200,
                TransferMin = 100,
                TransferMax = 300,
                InterestRate = 0.2,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount,
                ClientType = OClientTypes.All,
                EntryFees = 10,
                Currency = new Currency { Id = 1 },
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFeesMin = -1,
                FlatTransferFeesMax = 2,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 1,
                DepositFeesMin = 1,
                DepositFeesMax = 5,
                CloseFeesMin = 1,
                CloseFeesMax = 5,
                ManagementFeesMin = 1,
                ManagementFeesMax = 5,
                OverdraftFeesMin = 1,
                OverdraftFeesMax = 5,
                AgioFeesMin = 1,
                AgioFeesMax = 5,
                ReopenFeesMin = 1,
                ReopenFeesMax = 5
            };

            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Flat Withdraw Fees Min is Negative).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.FlatTransferFeesMinIsInvalid, exception.Code);
            }
        }

        [Test]
        public void TestSavingBookProduct_When_TransferFeesType_Is_Flat_And_FlatTransferFeesMin_IsHigher_Than_FlatTransferFeesMax()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                ChequeDepositMin = 100,
                ChequeDepositMax = 200,
                ChequeDepositFeesMin = 1,
                ChequeDepositFeesMax = 5,
                WithdrawingMin = 100,
                WithdrawingMax = 200,
                TransferMin = 100,
                TransferMax = 300,
                InterestRate = 0.2,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount,
                ClientType = OClientTypes.All,
                EntryFees = 10,
                Currency = new Currency { Id = 1 },
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFeesMin = 5,
                FlatTransferFeesMax = 2,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 1,
                DepositFeesMin = 1,
                DepositFeesMax = 5,
                CloseFeesMin = 1,
                CloseFeesMax = 5,
                ManagementFeesMin = 1,
                ManagementFeesMax = 5,
                OverdraftFeesMin = 1,
                OverdraftFeesMax = 5,
                AgioFeesMin = 1,
                AgioFeesMax = 5,
                ReopenFeesMin = 1,
                ReopenFeesMax = 5
            };

            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Flat Withdraw Fees Min is Higher than Flat Withdraw Fees Max).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.FlatTransferFeesMinMaxIsInvalid, exception.Code);
            }
        }

        [Test]
        public void TestSavingBookProduct_When_TransferFeesType_Is_Flat_And_FlatTransferFees_Is_Correct()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                ChequeDepositMin = 100,
                ChequeDepositMax = 200,
                ChequeDepositFeesMin = 1,
                ChequeDepositFeesMax = 5,
                WithdrawingMin = 100,
                WithdrawingMax = 200,
                TransferMin = 100,
                TransferMax = 300,
                InterestRate = 0.2,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount,
                ClientType = OClientTypes.All,
                EntryFees = 10,
                Currency = new Currency { Id = 1 },
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 5,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 1,
                DepositFeesMin = 1,
                DepositFeesMax = 5,
                CloseFeesMin = 1,
                CloseFeesMax = 5,
                ManagementFeesMin = 1,
                ManagementFeesMax = 5,
                OverdraftFeesMin = 1,
                OverdraftFeesMax = 5,
                AgioFeesMin = 1,
                AgioFeesMax = 5,
                ReopenFeesMin = 1,
                ReopenFeesMax = 5
            };
            savingsProduct.InterBranchTransferFee.Value = 10;

            _savingProductManagerMock.ExpectAndReturn("Add", 1, savingsProduct);

            Assert.AreEqual(_savingProductServices.SaveProduct(savingsProduct), 1);
        }

        [Test]
        public void TestSavingBookProduct_When_TransferFeesType_Is_Flat_And_FlatTransferFeesMinMax_Are_Correct()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                ChequeDepositMin = 100,
                ChequeDepositMax = 200,
                ChequeDepositFeesMin = 1,
                ChequeDepositFeesMax = 5,
                WithdrawingMin = 100,
                WithdrawingMax = 200,
                TransferMin = 100,
                TransferMax = 300,
                InterestRate = 0.2,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount,
                ClientType = OClientTypes.All,
                EntryFees = 10,
                Currency = new Currency { Id = 1 },
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFeesMin = 1,
                FlatTransferFeesMax = 5,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 1,
                DepositFeesMin = 1,
                DepositFeesMax = 5,
                CloseFeesMin = 1,
                CloseFeesMax = 5,
                ManagementFeesMin = 1,
                ManagementFeesMax = 5,
                OverdraftFeesMin = 1,
                OverdraftFeesMax = 5,
                AgioFeesMin = 1,
                AgioFeesMax = 5,
                ReopenFeesMin = 1,
                ReopenFeesMax = 5
            };
            savingsProduct.InterBranchTransferFee.Value = 10;

            _savingProductManagerMock.ExpectAndReturn("Add", 1, savingsProduct);

            Assert.AreEqual(_savingProductServices.SaveProduct(savingsProduct), 1);
        }

        [Test]
        public void TestSavingBookProduct_When_TransferFeesType_Is_Rate_And_RateTransferFees_Is_Negative()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                ChequeDepositMin = 100,
                ChequeDepositMax = 200,
                ChequeDepositFeesMin = 1,
                ChequeDepositFeesMax = 5,
                WithdrawingMin = 100,
                WithdrawingMax = 200,
                TransferMin = 100,
                TransferMax = 300,
                InterestRate = 0.2,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount,
                ClientType = OClientTypes.All,
                EntryFees = 10,
                Currency = new Currency { Id = 1 },
                TransferFeesType = OSavingsFeesType.Rate,
                RateTransferFees = -0.1,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 1,
                DepositFeesMin = 1,
                DepositFeesMax = 5,
                CloseFeesMin = 1,
                CloseFeesMax = 5,
                ManagementFeesMin = 1,
                ManagementFeesMax = 5,
                OverdraftFeesMin = 1,
                OverdraftFeesMax = 5,
                AgioFeesMin = 1,
                AgioFeesMax = 5,
                ReopenFeesMin = 1,
                ReopenFeesMax = 5
            };

            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Rate Withdraw Fees is Negative).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.RateTransferFeesIsInvalid, exception.Code);
            }
        }

        [Test]
        public void TestSavingBookProduct_When_TransferFeesType_Is_Rate_And_RateTransferFeesMin_Is_Negative()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                ChequeDepositMin = 100,
                ChequeDepositMax = 200,
                ChequeDepositFeesMin = 1,
                ChequeDepositFeesMax = 5,
                WithdrawingMin = 100,
                WithdrawingMax = 200,
                TransferMin = 100,
                TransferMax = 300,
                InterestRate = 0.2,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount,
                ClientType = OClientTypes.All,
                EntryFees = 10,
                Currency = new Currency { Id = 1 },
                TransferFeesType = OSavingsFeesType.Rate,
                RateTransferFeesMin = -0.1,
                RateTransferFeesMax = 0.2,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 1,
                DepositFeesMin = 1,
                DepositFeesMax = 5,
                CloseFeesMin = 1,
                CloseFeesMax = 5,
                ManagementFeesMin = 1,
                ManagementFeesMax = 5,
                OverdraftFeesMin = 1,
                OverdraftFeesMax = 5,
                AgioFeesMin = 1,
                AgioFeesMax = 5,
                ReopenFeesMin = 1,
                ReopenFeesMax = 5
            };

            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Rate Withdraw Fees Min is Negative).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.RateTransferFeesMinIsInvalid, exception.Code);
            }
        }

        [Test]
        public void TestSavingBookProduct_When_TransferFeesType_Is_Rate_And_RateTransferFeesMin_IsHigher_Than_RateTransferFeesMax()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                ChequeDepositMin = 100,
                ChequeDepositMax = 200,
                ChequeDepositFeesMin = 1,
                ChequeDepositFeesMax = 5,
                WithdrawingMin = 100,
                WithdrawingMax = 200,
                TransferMin = 100,
                TransferMax = 300,
                InterestRate = 0.2,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount,
                ClientType = OClientTypes.All,
                EntryFees = 10,
                Currency = new Currency { Id = 1 },
                TransferFeesType = OSavingsFeesType.Rate,
                RateTransferFeesMin = 0.5,
                RateTransferFeesMax = 0.2,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 1,
                DepositFeesMin = 1,
                DepositFeesMax = 5,
                CloseFeesMin = 1,
                CloseFeesMax = 5,
                ManagementFeesMin = 1,
                ManagementFeesMax = 5,
                OverdraftFeesMin = 1,
                OverdraftFeesMax = 5,
                AgioFeesMin = 1,
                AgioFeesMax = 5,
                ReopenFeesMin = 1,
                ReopenFeesMax = 5
            };

            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Rate Withdraw Fees Min is Higher than Rate Withdraw Fees Max).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.RateTransferFeesMinMaxIsInvalid, exception.Code);
            }
        }

        [Test]
        public void TestSavingBookProduct_When_TransferFeesType_Is_Rate_And_RateTransferFees_Is_Correct()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                ChequeDepositMin = 100,
                ChequeDepositMax = 200,
                ChequeDepositFeesMin = 1,
                ChequeDepositFeesMax = 5,
                WithdrawingMin = 100,
                WithdrawingMax = 200,
                TransferMin = 100,
                TransferMax = 300,
                InterestRate = 0.2,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount,
                ClientType = OClientTypes.All,
                EntryFees = 10,
                Currency = new Currency { Id = 1 },
                TransferFeesType = OSavingsFeesType.Rate,
                RateTransferFees = 0.5,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 1,
                DepositFeesMin = 1,
                DepositFeesMax = 5,
                CloseFeesMin = 1,
                CloseFeesMax = 5,
                ManagementFeesMin = 1,
                ManagementFeesMax = 5,
                OverdraftFeesMin = 1,
                OverdraftFeesMax = 5,
                AgioFeesMin = 1,
                AgioFeesMax = 5,
                ReopenFeesMin = 1,
                ReopenFeesMax = 5
            };
            savingsProduct.InterBranchTransferFee.Value = 10;

            _savingProductManagerMock.ExpectAndReturn("Add", 1, savingsProduct);

            Assert.AreEqual(_savingProductServices.SaveProduct(savingsProduct), 1);
        }

        [Test]
        public void TestSavingBookProduct_When_TransferFeesType_Is_Rate_And_RateTransferFeesMinMax_Are_Correct()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                ChequeDepositMin = 100,
                ChequeDepositMax = 200,
                ChequeDepositFeesMin = 1,
                ChequeDepositFeesMax = 5,
                WithdrawingMin = 100,
                WithdrawingMax = 200,
                TransferMin = 100,
                TransferMax = 300,
                InterestRate = 0.2,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount,
                ClientType = OClientTypes.All,
                EntryFees = 10,
                Currency = new Currency { Id = 1 },
                TransferFeesType = OSavingsFeesType.Rate,
                RateTransferFeesMin = 0.1,
                RateTransferFeesMax = 0.5,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 1,
                DepositFeesMin = 1,
                DepositFeesMax = 5,
                CloseFeesMin = 1,
                CloseFeesMax = 5,
                ManagementFeesMin = 1,
                ManagementFeesMax = 5,
                OverdraftFeesMin = 1,
                OverdraftFeesMax = 5,
                AgioFeesMin = 1,
                AgioFeesMax = 5,
                ReopenFeesMin = 1,
                ReopenFeesMax = 5
            };
            savingsProduct.InterBranchTransferFee.Value = 10;

            _savingProductManagerMock.ExpectAndReturn("Add", 1, savingsProduct);

            Assert.AreEqual(_savingProductServices.SaveProduct(savingsProduct), 1);
        }

        [Test]
        public void TestSavingBookProduct_When_TransferMin_IsNull()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 400,
                WithdrawingMin = 100,
                WithdrawingMax = 450,
                TransferMax = 300,
                InterestRateMin = 0.12,
                InterestRateMax = 0.20,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Daily,
                InterestFrequency = OSavingInterestFrequency.EndOfMonth,
                ClientType = OClientTypes.All,
                EntryFees = 0,
                Currency = new Currency { Id = 1 },
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 5,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
            };
            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Transfer Min is Null).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.TransferAmountIsInvalid, exception.Code);
            }
        }

        
        [Test]
        public void TestSavingBookProduct_When_TransferMin_IsNegative()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 400,
                ChequeDepositMin = 100,
                ChequeDepositMax = 200,
                ChequeDepositFeesMin = 1,
                ChequeDepositFeesMax = 5,
                WithdrawingMin = 100,
                WithdrawingMax = 450,
                TransferMin = -100,
                TransferMax = 300,
                InterestRateMin = 0.12,
                InterestRateMax = 0.20,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Daily,
                InterestFrequency = OSavingInterestFrequency.EndOfMonth,
                ClientType = OClientTypes.All,
                EntryFees = 0,
                Currency = new Currency { Id = 1 },
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 5,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
                DepositFeesMin = 1,
                DepositFeesMax = 5,
                CloseFeesMin = 1,
                CloseFeesMax = 5,
                ManagementFeesMin = 1,
                ManagementFeesMax = 5,
                OverdraftFeesMin = 1,
                OverdraftFeesMax = 5,
                AgioFeesMin = 1,
                AgioFeesMax = 5,
                ReopenFeesMin = 1,
                ReopenFeesMax = 5
            };
            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Transfer Min < 0).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.TransferAmountMinIsInvalid, exception.Code);
            }
        }

        

        [Test]
        public void TestSavingBookProduct_When_TransferMax_IsNull()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                WithdrawingMin = 400,
                WithdrawingMax = 500,
                TransferMin = 100,
                InterestRateMin = 0.12,
                InterestRateMax = 0.20,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Daily,
                InterestFrequency = OSavingInterestFrequency.EndOfMonth,
                ClientType = OClientTypes.All,
                EntryFees = 0,
                Currency = new Currency { Id = 1 },
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 5,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
            };
            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Transfer Max is Null).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.TransferAmountIsInvalid, exception.Code);
            }
        }

       

        [Test]
        public void TestSavingBookProduct_When_TransferMin_IsGreaterThan_TransferMax()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                WithdrawingMin = 100,
                WithdrawingMax = 250,
                TransferMin = 200,
                TransferMax = 150,
                InterestRateMin = 0.12,
                InterestRateMax = 0.20,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Daily,
                InterestFrequency = OSavingInterestFrequency.EndOfMonth,
                ClientType = OClientTypes.All,
                EntryFees = 0,
                Currency = new Currency { Id = 1 },
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 5,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
            };
            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Transfer Min > Transfer Max).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.TransferAmountIsInvalid, exception.Code);
            }
        }


        [Test]
        public void TestSavingBookProduct_When_TransferMin_And_TransferMax_Are_Zero()
        {
            SavingsBookProduct savingsProduct = new SavingsBookProduct
            {
                Name = "NewSavingProduct",
                Code = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                WithdrawingMin = 100,
                WithdrawingMax = 200,
                TransferMin = 0,
                TransferMax = 0,
                InterestRateMin = 0.12,
                InterestRateMax = 0.20,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Daily,
                InterestFrequency = OSavingInterestFrequency.EndOfMonth,
                ClientType = OClientTypes.All,
                EntryFees = 0,
                Currency = new Currency { Id = 1 },
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 5,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFees = 1,
            };
            try
            {
                _savingProductServices.SaveProduct(savingsProduct);
                Assert.Fail("Saving Contract shouldn't pass validation test while trying to save (Transfer Min And Transfer Max = 0).");
            }
            catch (OpenCbsSavingProductException exception)
            {
                Assert.AreEqual(OpenCbsSavingProductExceptionEnum.TransferAmountIsInvalid, exception.Code);
            }
        }
    }
}
