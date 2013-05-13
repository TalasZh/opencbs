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

using System.Collections.Generic;
using System.Data.SqlClient;
using NUnit.Framework;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Clients;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.CoreDomain.Contracts.Savings;
using OpenCBS.CoreDomain.Products;
using OpenCBS.Enums;
using OpenCBS.ExceptionsHandler.Exceptions.SavingExceptions;
using OpenCBS.Manager.Contracts;
using OpenCBS.Services;
using OpenCBS.Shared;
using NUnit.Mocks;
using OpenCBS.Manager.Events;
using OpenCBS.Shared.Settings;
using OpenCBS.CoreDomain.Accounting;
using System;

namespace OpenCBS.Test.Services.Savings
{
	/// <summary>
	/// Description r�sum�e de TestSavingServices.
	/// </summary>
	[TestFixture]
	public class TestSavingServices
	{
        private SavingServices _savingServices;// = new SavingServices(DataUtil.TESTDB);
		private SavingsBookProduct _savingsProduct;

        private DynamicMock _savingManagerMock;
        private DynamicMock _savingEventManagerMock;
        private DynamicMock _loanManagerMock; 

		[TestFixtureSetUp]
		public void TestFixtureSetUp()
		{
			ApplicationSettings.GetInstance("").DeleteAllParameters();
            ChartOfAccounts.SuppressAll();
		}

		[SetUp]
		public void SetUp()
		{
            _savingsProduct = new SavingsBookProduct
            {
                Id = 1,
                Name = "SavingProduct",
                Code = "P123",
                InitialAmountMin = 1000,
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
                EntryFeesMin = 10,
                EntryFeesMax = 20,
                Currency = new Currency { Id = 1 },
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFeesMin = 1,
                FlatWithdrawFeesMax = 5,
                TransferFeesType = OSavingsFeesType.Flat,
                FlatTransferFeesMin = 1,
                FlatTransferFeesMax = 5,
                DepositFeesMin = 1,
                DepositFeesMax = 5,
                ManagementFeeFreq = new InstallmentType {Id = 1, Name = "Monthly", NbOfDays = 0, NbOfMonths = 1},
                AgioFeesFreq = new InstallmentType { Id = 2, Name = "Daily", NbOfDays = 1, NbOfMonths = 0 },
                AgioFeesMin = 4,
                AgioFeesMax = 7

            };
		}

        [Test]
        public void TestSaving_When_InitialAmount_IsNull()
        {
            Assert.Ignore();
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),
                                                                new User())
                                            {
                                                CreationDate = TimeProvider.Today,
                                                InterestRate = 0.13,
                                                Product = _savingsProduct
                                            };

            _savingServices = new SavingServices(null, null, new User { Id = 6 });

            try
            {
                _savingServices.SaveContract(saving, new Person { Id = 6 });
                saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);
                Assert.Fail("Saving Contract shouldn't pass validation test before save (Initial Amount is Null).");
            }
            catch (OpenCbsSavingException exception)
            {
                Assert.AreEqual(exception.Code, OpenCbsSavingExceptionEnum.EntryFeesIsInvalid);
            }
        }       

        [Test]
        public void TestSaving_When_InterestRate_IsNull()
        {
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(), TimeProvider.Today, null)
            {
                Product = _savingsProduct
            };

            _savingServices = new SavingServices(null, null, new User { Id = 6 });

            try
            {
                _savingServices.SaveContract(saving, new Person { Id = 6 });
                Assert.Fail("Saving Contract shouldn't pass validation test before save (Interest Rate is Null).");
            }
            catch (OpenCbsSavingException exception)
            {
                Assert.AreEqual(exception.Code, OpenCbsSavingExceptionEnum.InterestRateIsInvalid);
            }
        }

        [Test]
        public void TestSaving_When_Product_IsNull()
        {
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(), TimeProvider.Today, null)
            {
                InterestRate = 0.15
            };

            _savingServices = new SavingServices(null, null, new User { Id = 6 });

            try
            {
                _savingServices.SaveContract(saving, new Person { Id = 6 });
                Assert.Fail("Saving Contract shouldn't pass validation test before save (Product is Null).");
            }
            catch (OpenCbsSavingException exception)
            {
                Assert.AreEqual(exception.Code, OpenCbsSavingExceptionEnum.ProductIsInvalid);
            }
        }

		[Test]
		public void TestSavingIsValid_InitialAmountTooSmall()
		{
            Assert.Ignore();
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(), TimeProvider.Today, null) 
            { InterestRate = 0.13, Product = _savingsProduct };

            _savingServices = new SavingServices(null, null, new User { Id = 6 });

			try
			{
				Client client = new Person { Id = 1 };
				_savingServices.SaveContract(saving, client);
                saving.FirstDeposit(1, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);
				Assert.Fail("Saving Contract shouldn't pass validation test before save (initial amount < product.min).");
			}
			catch (OpenCbsSavingException exception)
			{
				Assert.AreEqual((int)OpenCbsSavingExceptionEnum.EntryFeesIsInvalid, (int)exception.Code);
			}
		}

        [Test]
        public void Test_SimulateCloseAccount()
        {
            Assert.Ignore();
            _savingServices = new SavingServices(null, null, new User { Id = 6 });

            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""), 
                new User { Id = 6 }, new DateTime(2009, 01, 01), _savingsProduct, null) { InterestRate = 0.15, ManagementFees = 0, AgioFees = 0 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            Assert.AreEqual(_savingServices.SimulateCloseAccount(saving, new DateTime(2009, 02, 15), new User(), false, Teller.CurrentTeller).GetBalance(), 5650);
        }

		[Test]
		public void TestSavingIsValid_InitialAmountCorrect()
		{
//            Assert.Ignore();
            SavingBookContract saving = new SavingBookContract(
                                                                ApplicationSettings.GetInstance(""), 
                                                                 
                                                                new User { Id = 6 }, 
                                                                TimeProvider.Today, 
                                                                _savingsProduct, 
                                                                null) 
                                                                {
                                                                    InterestRate = 0.13, 
                                                                    FlatWithdrawFees = 3, 
                                                                    FlatTransferFees = 3, 
                                                                    DepositFees = 5, 
                                                                    CloseFees = 6, 
                                                                    ManagementFees = 7, 
                                                                    AgioFees = 6 
                                                                };
            Client client = new Person { Id = 6, LastName = "El Fanidi", Branch = new Branch()};

            _savingManagerMock = new DynamicMock(typeof(SavingManager));
            _savingEventManagerMock = new DynamicMock(typeof(SavingEventManager));
            
            DynamicMock connectionMock = new DynamicMock(typeof(SqlConnection));
            DynamicMock transactionMock = new DynamicMock(typeof(SqlTransaction));
            connectionMock.SetReturnValue("BeginTransaction", transactionMock.MockInstance);

            _savingManagerMock.SetReturnValue("GetConnection", connectionMock.MockInstance);

            _savingManagerMock.ExpectAndReturn("GetNumberOfSavings", 1, 6);
            _savingManagerMock.ExpectAndReturn("Add", 1, saving, client, transactionMock.MockInstance);

            _savingServices = new SavingServices(
                                                    (SavingManager)_savingManagerMock.MockInstance, 
                                                    (SavingEventManager)_savingEventManagerMock.MockInstance,
                                                     new User { Id = 6 }
                                                 );

            try
            {
                Assert.GreaterOrEqual(_savingServices.SaveContract(saving, client), 0);

                saving = new SavingBookContract(
                                                    ApplicationSettings.GetInstance(""), 
                                                     
                                                    new User { Id = 6 }, 
                                                    TimeProvider.Today, 
                                                    _savingsProduct, 
                                                    null) 
                                                    {
                                                        InterestRate = 0.13, 
                                                        FlatWithdrawFees = 3, 
                                                        FlatTransferFees = 3, 
                                                        DepositFees = 5, 
                                                        CloseFees = 6, 
                                                        ManagementFees = 7, 
                                                        AgioFees = 6 
                                                    };

                _savingManagerMock.ExpectAndReturn("GetNumberOfSavings", 2, 6);
                _savingManagerMock.ExpectAndReturn("Add", 2, saving, client, transactionMock.MockInstance);

                Assert.GreaterOrEqual(_savingServices.SaveContract(saving, client), 0);
            }
            catch (OpenCbsSavingException exception)
            {
                Assert.Fail(exception.Code.ToString());
            }
		}

		[Test]
		public void TestSavingIsValid_InitialAmountTooBig()
		{
            Assert.Ignore();
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  
                new User(), TimeProvider.Today, null) { InterestRate = 0.13, Product = _savingsProduct };

            _savingServices = new SavingServices(null, null, new User { Id = 6 });

			try
			{
				Client client = new Person { Id = 1 };
				_savingServices.SaveContract(saving, client);
                saving.FirstDeposit(100000000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);
				Assert.Fail("Saving Contract shouldn't pass validation test before save (initial amount > product.max).");
			}
			catch (OpenCbsSavingException exception)
			{
				Assert.AreEqual((int)OpenCbsSavingExceptionEnum.EntryFeesIsInvalid, (int)exception.Code);
			}
		}

        [Test]
        public void TestSavingIsValid_TransferAmountIsInvalid()
        {
            Assert.Ignore();
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(), TimeProvider.Today, null) { Id = 1, InterestRate = 0.13, Product = _savingsProduct };
            SavingBookContract savingTarget = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(), TimeProvider.Today, null) { Id = 2, InterestRate = 0.13, Product = _savingsProduct };

            _savingManagerMock = new DynamicMock(typeof(SavingManager));
            _savingServices = new SavingServices((SavingManager)_savingManagerMock.MockInstance, null, new User { Id = 6 });

            _savingManagerMock.Expect("UpdateAccountsBalance", saving, null);
            _savingManagerMock.Expect("UpdateAccountsBalance", savingTarget, null);

            try
            {
                _savingServices.Transfer(saving, savingTarget, TimeProvider.Today, 99, 0, "transfer", new User(), false);
                Assert.Fail("Saving Contract shouldn't pass validation test before transfer (transfer amount < transfer.min).");
            }
            catch (OpenCbsSavingException exception)
            {
                Assert.AreEqual((int)OpenCbsSavingExceptionEnum.TransferAmountIsInvalid, (int)exception.Code);
            }

            _savingManagerMock.Expect("UpdateAccountsBalance", saving, null);
            _savingManagerMock.Expect("UpdateAccountsBalance", savingTarget, null);

            try
            {
                _savingServices.Transfer(saving, savingTarget, TimeProvider.Today, 301, 0, "transfer", new User(), false);
                Assert.Fail("Saving Contract shouldn't pass validation test before transfer (transfer amount > transfer.max).");
            }
            catch (OpenCbsSavingException exception)
            {
                Assert.AreEqual((int)OpenCbsSavingExceptionEnum.TransferAmountIsInvalid, (int)exception.Code);
            }

            _savingManagerMock.Expect("UpdateAccountsBalance", saving, null);
            _savingManagerMock.Expect("UpdateAccountsBalance", savingTarget, null);

            try
            {
                _savingServices.Transfer(saving, savingTarget, TimeProvider.Today, 200, 0, "transfer", new User(), false);
                Assert.Fail("Saving Contract shouldn't pass validation test before transfer (balance < balance.min).");
            }
            catch (OpenCbsSavingException exception)
            {
                Assert.AreEqual((int)OpenCbsSavingExceptionEnum.BalanceIsInvalid, (int)exception.Code);
            }
        }

        [Test]
        public void TestSavingIsValid_TransferIsInvalid()
        {
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(), TimeProvider.Today, null) { InterestRate = 0.13, Product = _savingsProduct };
            SavingBookContract savingTarget = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(), TimeProvider.Today, null) { InterestRate = 0.13, Product = _savingsProduct, Status = OSavingsStatus.Closed };

            _savingServices = new SavingServices(null, null, new User { Id = 6 });

            try
            {
                _savingServices.Transfer(saving, saving, TimeProvider.Today, 99, 0, "transfer", new User(), false);
                Assert.Fail("Saving Contract shouldn't pass validation test before transfer (Saving source = Saving Target).");
            }
            catch (OpenCbsSavingException exception)
            {
                Assert.AreEqual((int)OpenCbsSavingExceptionEnum.SavingsContractForTransferIdenticals, (int)exception.Code);
            }

            try
            {
                _savingServices.Transfer(saving, savingTarget, TimeProvider.Today, 99, 0, "transfer", new User(), false);
                Assert.Fail("Saving Contract shouldn't pass validation test before transfer (Saving Target is Closed).");
            }
            catch (OpenCbsSavingException exception)
            {
                Assert.AreEqual((int)OpenCbsSavingExceptionEnum.CreditTransferAccountInvalid, (int)exception.Code);
            }
        }

        [Test]
        public void TestSaving_FlatWithdrawFees()
        {
            Assert.Ignore();
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User { Id = 6 }, 
                TimeProvider.Today, _savingsProduct, null) { InterestRate = 0.15, FlatTransferFees = 3, DepositFees = 5, CloseFees = 6,
            ManagementFees = 7};
            Client client = new Person { Id = 6, LastName = "El Fanidi" };

            _savingManagerMock = new DynamicMock(typeof(SavingManager));
            _savingEventManagerMock = new DynamicMock(typeof(SavingEventManager));

            _savingManagerMock.ExpectAndReturn("GetNumberOfSavings", 1, 6);
            _savingManagerMock.ExpectAndReturn("Add", 1, saving, client, null);

            _savingServices = new SavingServices((SavingManager)_savingManagerMock.MockInstance, (SavingEventManager)_savingEventManagerMock.MockInstance, new User { Id = 6 });

            try
            {
                _savingServices.SaveContract(saving, client);
            }
            catch (OpenCbsSavingException exception)
            {
                Assert.AreEqual((int)OpenCbsSavingExceptionEnum.WithdrawFeesIsInvalid, (int)exception.Code);
            }

            saving.FlatWithdrawFees = 0;

            try
            {
                _savingServices.SaveContract(saving, client);
            }
            catch (OpenCbsSavingException exception)
            {
                Assert.AreEqual((int)OpenCbsSavingExceptionEnum.WithdrawFeesIsInvalid, (int)exception.Code);
            }

            saving.FlatWithdrawFees = 6;

            try
            {
                _savingServices.SaveContract(saving, client);
            }
            catch (OpenCbsSavingException exception)
            {
                Assert.AreEqual((int)OpenCbsSavingExceptionEnum.WithdrawFeesIsInvalid, (int)exception.Code);
            }

            saving.FlatWithdrawFees = 3;
            Assert.AreEqual(1, _savingServices.SaveContract(saving, client));

            saving.Product.FlatWithdrawFees = 2;
            saving.Product.FlatWithdrawFeesMin = null;
            saving.Product.FlatWithdrawFeesMax = null;

            _savingManagerMock.ExpectAndReturn("GetNumberOfSavings", 2, 6);
            _savingManagerMock.ExpectAndReturn("Add", 2, saving, client, null);

            saving.FlatWithdrawFees = null;

            try
            {
                _savingServices.SaveContract(saving, client);
            }
            catch (OpenCbsSavingException exception)
            {
                Assert.AreEqual((int)OpenCbsSavingExceptionEnum.WithdrawFeesIsInvalid, (int)exception.Code);
            }

            saving.FlatWithdrawFees = 1;

            try
            {
                _savingServices.SaveContract(saving, client);
            }
            catch (OpenCbsSavingException exception)
            {
                Assert.AreEqual((int)OpenCbsSavingExceptionEnum.WithdrawFeesIsInvalid, (int)exception.Code);
            }

            saving.FlatWithdrawFees = 3;

            try
            {
                _savingServices.SaveContract(saving, client);
            }
            catch (OpenCbsSavingException exception)
            {
                Assert.AreEqual((int)OpenCbsSavingExceptionEnum.WithdrawFeesIsInvalid, (int)exception.Code);
            }

            saving.FlatWithdrawFees = 2;
            Assert.AreEqual(2, _savingServices.SaveContract(saving, client));
        }

        [Test]
        public void TestSaving_RateWithdrawFees()
        {
            Assert.Ignore();
            _savingsProduct.WithdrawFeesType = OSavingsFeesType.Rate;
            _savingsProduct.RateWithdrawFeesMin = 0.01;
            _savingsProduct.RateWithdrawFeesMax = 0.05;

            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User { Id = 6 }, 
                TimeProvider.Today, _savingsProduct, null) { InterestRate = 0.15, FlatTransferFees = 3, DepositFees = 5, CloseFees = 6,
                 ManagementFees = 7, AgioFees = 8 };
            Client client = new Person { Id = 6, LastName = "El Fanidi" };

            _savingManagerMock = new DynamicMock(typeof(SavingManager));
            _savingEventManagerMock = new DynamicMock(typeof(SavingEventManager));

            _savingManagerMock.ExpectAndReturn("GetNumberOfSavings", 1, 6);
            _savingManagerMock.ExpectAndReturn("Add", 1, saving, client, null);

            _savingServices = new SavingServices((SavingManager)_savingManagerMock.MockInstance, (SavingEventManager)_savingEventManagerMock.MockInstance, new User { Id = 6 });

            try
            {
                _savingServices.SaveContract(saving, client);
            }
            catch (OpenCbsSavingException exception)
            {
                Assert.AreEqual((int)OpenCbsSavingExceptionEnum.WithdrawFeesIsInvalid, (int)exception.Code);
            }

            saving.RateWithdrawFees = 0;

            try
            {
                _savingServices.SaveContract(saving, client);
            }
            catch (OpenCbsSavingException exception)
            {
                Assert.AreEqual((int)OpenCbsSavingExceptionEnum.WithdrawFeesIsInvalid, (int)exception.Code);
            }

            saving.RateWithdrawFees = 0.06;

            try
            {
                _savingServices.SaveContract(saving, client);
            }
            catch (OpenCbsSavingException exception)
            {
                Assert.AreEqual((int)OpenCbsSavingExceptionEnum.WithdrawFeesIsInvalid, (int)exception.Code);
            }

            saving.RateWithdrawFees = 0.03;
            Assert.AreEqual(1, _savingServices.SaveContract(saving, client));

            saving.Product.RateWithdrawFees = 0.02;
            saving.Product.RateWithdrawFeesMin = null;
            saving.Product.RateWithdrawFeesMax = null;

            _savingManagerMock.ExpectAndReturn("GetNumberOfSavings", 2, 6);
            _savingManagerMock.ExpectAndReturn("Add", 2, saving, client, null);

            saving.RateWithdrawFees = null;

            try
            {
                _savingServices.SaveContract(saving, client);
            }
            catch (OpenCbsSavingException exception)
            {
                Assert.AreEqual((int)OpenCbsSavingExceptionEnum.WithdrawFeesIsInvalid, (int)exception.Code);
            }

            saving.RateWithdrawFees = 0.01;

            try
            {
                _savingServices.SaveContract(saving, client);
            }
            catch (OpenCbsSavingException exception)
            {
                Assert.AreEqual((int)OpenCbsSavingExceptionEnum.WithdrawFeesIsInvalid, (int)exception.Code);
            }

            saving.RateWithdrawFees = 0.03;

            try
            {
                _savingServices.SaveContract(saving, client);
            }
            catch (OpenCbsSavingException exception)
            {
                Assert.AreEqual((int)OpenCbsSavingExceptionEnum.WithdrawFeesIsInvalid, (int)exception.Code);
            }

            saving.RateWithdrawFees = 0.02;
            Assert.AreEqual(2, _savingServices.SaveContract(saving, client));
        }

        [Test]
        public void TestSaving_FlatTransferFees()
        {
            Assert.Ignore();
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  
                new User { Id = 6 }, TimeProvider.Today, _savingsProduct, null) { FlatWithdrawFees = 3, //FlatTransferFees = 4, 
                InterestRate = 0.15, DepositFees = 5, CloseFees = 6, ManagementFees = 7, AgioFees = 9 };
            Client client = new Person { Id = 6, LastName = "El Fanidi" };

            _savingManagerMock = new DynamicMock(typeof(SavingManager));
            _savingEventManagerMock = new DynamicMock(typeof(SavingEventManager));

            _savingManagerMock.ExpectAndReturn("GetNumberOfSavings", 1, 6);
            _savingManagerMock.ExpectAndReturn("Add", 1, saving, client, null);

            _savingServices = new SavingServices((SavingManager)_savingManagerMock.MockInstance, (SavingEventManager)_savingEventManagerMock.MockInstance, new User { Id = 6 });

            try
            {
                _savingServices.SaveContract(saving, client);
            }
            catch (OpenCbsSavingException exception)
            {
                Assert.AreEqual((int)OpenCbsSavingExceptionEnum.TransferFeesIsInvalid, (int)exception.Code);
            }

            saving.FlatTransferFees = 0;

            try
            {
                _savingServices.SaveContract(saving, client);
            }
            catch (OpenCbsSavingException exception)
            {
                Assert.AreEqual((int)OpenCbsSavingExceptionEnum.TransferFeesIsInvalid, (int)exception.Code);
            }

            saving.FlatTransferFees = 6;

            try
            {
                _savingServices.SaveContract(saving, client);
            }
            catch (OpenCbsSavingException exception)
            {
                Assert.AreEqual((int)OpenCbsSavingExceptionEnum.TransferFeesIsInvalid, (int)exception.Code);
            }

            saving.FlatTransferFees = 3;
            Assert.AreEqual(1, _savingServices.SaveContract(saving, client));

            saving.Product.FlatTransferFees = 2;
            saving.Product.FlatTransferFeesMin = null;
            saving.Product.FlatTransferFeesMax = null;

            _savingManagerMock.ExpectAndReturn("GetNumberOfSavings", 2, 6);
            _savingManagerMock.ExpectAndReturn("Add", 2, saving, client, null);

            saving.FlatTransferFees = null;

            try
            {
                _savingServices.SaveContract(saving, client);
            }
            catch (OpenCbsSavingException exception)
            {
                Assert.AreEqual((int)OpenCbsSavingExceptionEnum.TransferFeesIsInvalid, (int)exception.Code);
            }

            saving.FlatTransferFees = 1;

            try
            {
                _savingServices.SaveContract(saving, client);
            }
            catch (OpenCbsSavingException exception)
            {
                Assert.AreEqual((int)OpenCbsSavingExceptionEnum.TransferFeesIsInvalid, (int)exception.Code);
            }

            saving.FlatTransferFees = 3;

            try
            {
                _savingServices.SaveContract(saving, client);
            }
            catch (OpenCbsSavingException exception)
            {
                Assert.AreEqual((int)OpenCbsSavingExceptionEnum.TransferFeesIsInvalid, (int)exception.Code);
            }

            saving.FlatTransferFees = 2;
            Assert.AreEqual(2, _savingServices.SaveContract(saving, client));
        }

        [Test]
        public void TestSaving_RateTransferFees()
        {
            Assert.Ignore();
            _savingsProduct.TransferFeesType = OSavingsFeesType.Rate;
            _savingsProduct.RateTransferFeesMin = 0.01;
            _savingsProduct.RateTransferFeesMax = 0.05;

            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User { Id = 6 }, 
                TimeProvider.Today, _savingsProduct, null) { FlatWithdrawFees = 3, InterestRate = 0.15, DepositFees = 5, 
                CloseFees = 6, ManagementFees = 7, AgioFees = 10 };
            Client client = new Person { Id = 6, LastName = "El Fanidi" };

            _savingManagerMock = new DynamicMock(typeof(SavingManager));
            _savingEventManagerMock = new DynamicMock(typeof(SavingEventManager));

            _savingManagerMock.ExpectAndReturn("GetNumberOfSavings", 1, 6);
            _savingManagerMock.ExpectAndReturn("Add", 1, saving, client, null);

            _savingServices = new SavingServices((SavingManager)_savingManagerMock.MockInstance, (SavingEventManager)_savingEventManagerMock.MockInstance, new User { Id = 6 });

            try
            {
                _savingServices.SaveContract(saving, client);
            }
            catch (OpenCbsSavingException exception)
            {
                Assert.AreEqual((int)OpenCbsSavingExceptionEnum.TransferFeesIsInvalid, (int)exception.Code);
            }

            saving.RateTransferFees = 0;

            try
            {
                _savingServices.SaveContract(saving, client);
            }
            catch (OpenCbsSavingException exception)
            {
                Assert.AreEqual((int)OpenCbsSavingExceptionEnum.TransferFeesIsInvalid, (int)exception.Code);
            }

            saving.RateTransferFees = 0.06;

            try
            {
                _savingServices.SaveContract(saving, client);
            }
            catch (OpenCbsSavingException exception)
            {
                Assert.AreEqual((int)OpenCbsSavingExceptionEnum.TransferFeesIsInvalid, (int)exception.Code);
            }

            saving.RateTransferFees = 0.03;
            Assert.AreEqual(1, _savingServices.SaveContract(saving, client));

            saving.Product.RateTransferFees = 0.02;
            saving.Product.RateTransferFeesMin = null;
            saving.Product.RateTransferFeesMax = null;

            _savingManagerMock.ExpectAndReturn("GetNumberOfSavings", 2, 6);
            _savingManagerMock.ExpectAndReturn("Add", 2, saving, client, null);

            saving.RateTransferFees = null;

            try
            {
                _savingServices.SaveContract(saving, client);
            }
            catch (OpenCbsSavingException exception)
            {
                Assert.AreEqual((int)OpenCbsSavingExceptionEnum.TransferFeesIsInvalid, (int)exception.Code);
            }

            saving.RateTransferFees = 0.01;

            try
            {
                _savingServices.SaveContract(saving, client);
            }
            catch (OpenCbsSavingException exception)
            {
                Assert.AreEqual((int)OpenCbsSavingExceptionEnum.TransferFeesIsInvalid, (int)exception.Code);
            }

            saving.RateTransferFees = 0.03;

            try
            {
                _savingServices.SaveContract(saving, client);
            }
            catch (OpenCbsSavingException exception)
            {
                Assert.AreEqual((int)OpenCbsSavingExceptionEnum.TransferFeesIsInvalid, (int)exception.Code);
            }

            saving.RateTransferFees = 0.02;
            Assert.AreEqual(2, _savingServices.SaveContract(saving, client));
        }

		[Test]
		public void TestSavingIsValid_InterestRate()
		{
            Assert.Ignore();
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User { Id = 6 }, 
                TimeProvider.Today, _savingsProduct, null) { FlatWithdrawFees = 3, FlatTransferFees = 3, DepositFees = 5, CloseFees = 6, ManagementFees = 7 };
            Client client = new Person { Id = 6, LastName = "El Fanidi" };

            _savingManagerMock = new DynamicMock(typeof(SavingManager));
            _savingEventManagerMock = new DynamicMock(typeof(SavingEventManager));

            _savingManagerMock.ExpectAndReturn("GetNumberOfSavings", 1, 6);
            _savingManagerMock.ExpectAndReturn("Add", 1, saving, client, null);

            _savingServices = new SavingServices((SavingManager)_savingManagerMock.MockInstance, (SavingEventManager)_savingEventManagerMock.MockInstance,
                 new User { Id = 6 });

            //Test InterestRate is valid (between min/max)
            saving.InterestRate = 0.11;
            try
            {
                _savingServices.SaveContract(saving, client);
            }
            catch (OpenCbsSavingException exception)
            {
                Assert.AreEqual((int)OpenCbsSavingExceptionEnum.AgioFeesIsInvalid, (int)exception.Code);
            }

            try
            {
                _savingManagerMock.ExpectAndReturn("GetNumberOfSavings", 2, 6);
                _savingManagerMock.ExpectAndReturn("Add", 2, saving, client, null);
                saving.InterestRate = 0.12;
                _savingServices.SaveContract(saving, client);

                _savingManagerMock.ExpectAndReturn("GetNumberOfSavings", 3, 6);
                _savingManagerMock.ExpectAndReturn("Add", 3, saving, client, null);
                saving.InterestRate = 0.13;
                _savingServices.SaveContract(saving, client);

                _savingManagerMock.ExpectAndReturn("GetNumberOfSavings", 4, 6);
                _savingManagerMock.ExpectAndReturn("Add", 4, saving, client, null);
                saving.InterestRate = 0.16;
                _savingServices.SaveContract(saving, client);

                _savingManagerMock.ExpectAndReturn("GetNumberOfSavings", 5, 6);
                _savingManagerMock.ExpectAndReturn("Add", 5, saving, client, null);
                saving.InterestRate = 0.19;
                _savingServices.SaveContract(saving, client);

                _savingManagerMock.ExpectAndReturn("GetNumberOfSavings", 6, 6);
                _savingManagerMock.ExpectAndReturn("Add", 6, saving, client, null);
                saving.InterestRate = 0.20;
                _savingServices.SaveContract(saving, client);

                _savingManagerMock.ExpectAndReturn("GetNumberOfSavings", 7, 6);
                _savingManagerMock.ExpectAndReturn("Add", 7, saving, client, null);
                saving.InterestRate = 0.21;
            }
            catch (OpenCbsSavingException exception)
            {
                Assert.Fail(exception.Code.ToString());
            }
            try
            {

                _savingManagerMock.ExpectAndReturn("GetNumberOfSavings", 8, 6);
                _savingManagerMock.ExpectAndReturn("Add", 8, saving, client, null);
                _savingServices.SaveContract(saving, client);
            }
            catch (OpenCbsSavingException exception)
            {
                Assert.AreEqual((int)OpenCbsSavingExceptionEnum.InterestRateIsInvalid, (int)exception.Code);
            }

            //Test InterestRate is valid (equals to required value)
            //Create a product with min/max InterestRate
            _savingsProduct.InterestRate = 0.15;
            _savingsProduct.InterestRateMin = null;
            _savingsProduct.InterestRateMax = null;

            saving.InterestRate = 0.14;
            try
            {
                _savingManagerMock.ExpectAndReturn("GetNumberOfSavings", 9, 6);
                _savingManagerMock.ExpectAndReturn("Add", 9, saving, client, null);
                _savingServices.SaveContract(saving, client);
            }
            catch (OpenCbsSavingException exception)
            {
                Assert.AreEqual((int)OpenCbsSavingExceptionEnum.InterestRateIsInvalid, (int)exception.Code);
            }


            _savingManagerMock.ExpectAndReturn("GetNumberOfSavings", 10, 6);
            _savingManagerMock.ExpectAndReturn("Add", 10, saving, client, null);
            saving.InterestRate = 0.15;
            _savingServices.SaveContract(saving, client);

            saving.InterestRate = 0.16;
            try
            {
                _savingManagerMock.ExpectAndReturn("GetNumberOfSavings", 11, 6);
                _savingManagerMock.ExpectAndReturn("Add", 11, saving, client, null);
                _savingServices.SaveContract(saving, client);
            }
            catch (OpenCbsSavingException exception)
            {
                Assert.AreEqual((int)OpenCbsSavingExceptionEnum.InterestRateIsInvalid, (int)exception.Code);
            }

		}

		[Test]
		public void TestSavingIsValid_Balance()
		{
            Assert.Ignore();

            //Client client = new Person { Id = 6, LastName = "El Fanidi" };
            //User user = new User { Id = 6 };
            //_savingsProduct.InitialAmountMin = 1000;
            //_savingsProduct.InitialAmountMax = 2000;

            //Saving savings = new Saving(ApplicationSettings.GetInstance(""), new User { Id = 6 }, 1000, TimeProvider.Today) { InterestRate = 0.13, Product = _savingsProduct };


            //_savingManagerMock = new DynamicMock(typeof(SavingManager));
            //_savingEventManagerMock = new DynamicMock(typeof(SavingEventManager));

            //_savingManagerMock.ExpectAndReturn("GetNumberOfSavings", 1, 6);
            //_savingManagerMock.ExpectAndReturn("Add", 1, savings, client, null);
            //_savingManagerMock.Expect("UpdateAccountsBalance", savings, null);

            //_savingServices = new SavingServices((SavingManager)_savingManagerMock.MockInstance, (SavingEventManager)_savingEventManagerMock.MockInstance,
            //     new User { Id = 6 });

            //try
            //{
            //    _savingServices.SaveContract(savings, client);

            //    _savingManagerMock.Expect("UpdateAccountsBalance", savings, null);
            //    _savingServices.Deposit(savings, TimeProvider.Today, 250, "", user);

            //    _savingManagerMock.Expect("UpdateAccountsBalance", savings, null);
            //    _savingServices.Deposit(savings, TimeProvider.Today, 250, "", user);

            //    _savingManagerMock.Expect("UpdateAccountsBalance", savings, null);
            //    _savingServices.Deposit(savings, TimeProvider.Today, 250, "", user);
            //}
            //catch (OpenCbsSavingException exception)
            //{
            //    Assert.Fail(exception.Code.ToString());
            //}
            //try
            //{
            //    _savingManagerMock.Expect("UpdateAccountsBalance", savings, null);
            //    _savingServices.Deposit(savings, TimeProvider.Today, 300, "", user);
            //    Assert.Fail("Saving Contract shouldn't pass validation test before save (balance > product.max).");
            //}
            //catch (OpenCbsSavingException exception)
            //{
            //    Assert.AreEqual((int)OpenCbsSavingExceptionEnum.BalanceIsInvalid, (int)exception.Code);
            //}

            //_savingManagerMock.Expect("UpdateAccountsBalance", savings, null);
            //_savingServices.Deposit(savings, TimeProvider.Today, 250, "", user);
		}

		public void HandleOctopusSavingException_BalanceIsInvalid(OpenCbsSavingException ex)
		{
			Assert.AreEqual(OpenCbsSavingExceptionEnum.BalanceIsInvalid, ex.Code);
		}

		public void HandleOctopusSavingException_DepositAmountIsInvalid(OpenCbsSavingException ex)
		{
			Assert.AreEqual(OpenCbsSavingExceptionEnum.DepositAmountIsInvalid, ex.Code);
		}

		public void HandleOctopusSavingException_InitialAmountIsInvalid(OpenCbsSavingException ex)
		{
			Assert.AreNotEqual(OpenCbsSavingExceptionEnum.InitialAmountIsInvalid, ex.Code);
		}

		public void HandleOctopusSavingException_InterestRateIsInvalid(OpenCbsSavingException ex)
		{
			Assert.AreEqual(OpenCbsSavingExceptionEnum.InterestRateIsInvalid, ex.Code);
		}

		public void HandleOctopusSavingException_SavingContractIsNull(OpenCbsSavingException ex)
		{
			Assert.AreEqual(OpenCbsSavingExceptionEnum.SavingContractIsNull, ex.Code);
		}

		public void HandleOctopusSavingException_WithdrawAmountIsInvalid(OpenCbsSavingException ex)
		{
			Assert.AreEqual(OpenCbsSavingExceptionEnum.WithdrawAmountIsInvalid, ex.Code);
		}

        [Test]
        public void GetContractCodesForClient()
        {
            const int tiersId = 6;
            _savingManagerMock = new DynamicMock(typeof(SavingManager));
            _savingManagerMock.ExpectAndReturn("SelectClientSavingBookCodes",
                                               new[]
                                                   {
                                                       new KeyValuePair<int, string>(1, "S/BC/2007/SAVIN-1/ELFA-6")
                                                   },
                                               tiersId, 1
                );

            var savingService = new SavingServices(
                (SavingManager)_savingManagerMock.MockInstance,
                null,
                new User()
                );            
            var savings = savingService.SelectClientSavingBookCodes(tiersId, 1);
            Assert.AreEqual(1, savings.Length);
            var saving = savings[0];
            Assert.AreEqual(1, saving.Key);
            Assert.AreEqual("S/BC/2007/SAVIN-1/ELFA-6", saving.Value);
        }
	}
}
