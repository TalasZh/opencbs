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
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using NUnit.Mocks;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.CoreDomain.Clients;
using OpenCBS.CoreDomain.Contracts.Collaterals;
using OpenCBS.CoreDomain.Contracts.Loans;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment;
using OpenCBS.CoreDomain.EconomicActivities;
using OpenCBS.CoreDomain.Events;
using OpenCBS.CoreDomain.FundingLines;
using OpenCBS.CoreDomain.LoanCycles;
using OpenCBS.CoreDomain.Online;
using OpenCBS.CoreDomain.Products;
using OpenCBS.DatabaseConnection;
using OpenCBS.Enums;
using OpenCBS.Manager;
using OpenCBS.Manager.Clients;
using OpenCBS.Manager.Products;
using OpenCBS.Services;
using OpenCBS.ExceptionsHandler;
using OpenCBS.Shared;
using OpenCBS.Shared.Settings;

namespace OpenCBS.Test.Services
{
	/// <summary>
	/// Description r�sum�e de TestProductServices.
	/// </summary>
	/// 
	[TestFixture]
	public class TestProductServices
	{
		private DynamicMock mockPackageManagement;
		private DynamicMock mockInstallmentTypeManagement;
	    //private DynamicMock mockFundingLineService;
		private InstallmentTypeManager installmentTypeManagement;
		private InstallmentType _monthly;
		private InstallmentType _biWeekly;
        private AddDataForTestingTransaction dataHelper = new AddDataForTestingTransaction();

        

		[SetUp]
		public void SetUp()
		{
			_monthly = new InstallmentType();
			_monthly.Id = 1;
			_monthly.Name = "Monthly";
			_monthly.NbOfMonths = 1;

			_biWeekly = new InstallmentType();
			_biWeekly.Id = 2;
			_biWeekly.Name = "bi-weekly";
			_biWeekly.NbOfDays = 14;

            mockPackageManagement = new DynamicMock(typeof(LoanProductManager));
            mockInstallmentTypeManagement = new DynamicMock(typeof(InstallmentTypeManager));
            //mockFundingLineService = new DynamicMock(typeof(FundingLineManager));
            InitScript();

		}

        private static void InitScript()
        {
            Assembly a = Assembly.GetExecutingAssembly();
            Stream stream = a.GetManifestResourceStream("OpenCBS.Test._Sql.Init.sql");

            if (stream == null) return;

            StreamReader streamReader = new StreamReader(stream);
            string stringSql = streamReader.ReadToEnd();
            ConnectionManager connectionManager = ConnectionManager.GetInstance(DataUtil.TESTDB);
            using (SqlCommand insert = new SqlCommand(stringSql, connectionManager.SqlConnection))
            {
                insert.ExecuteNonQuery();
            }
        }

        private static ProductServices SetMockManager(DynamicMock pProductDynamicMock)
        {
            LoanProductManager productManager = (LoanProductManager)pProductDynamicMock.MockInstance;
            return new ProductServices(productManager);
        }

		[Test]
		public void NbOfPackagesInDatabaseCorrectlyGetWhenNoPackages()
		{
            List<LoanProduct> list = new List<LoanProduct>();
            mockPackageManagement.ExpectAndReturn("SelectAllPackages", list, true, OClientTypes.All);

            ProductServices productServices = SetMockManager(mockPackageManagement);
            Assert.AreEqual(0, productServices.FindAllPackages(true, OClientTypes.All).Count);
        }

		[Test]
		public void NbOfPackagesInDatabaseCorrectlyGetWhenSomePackagesInDatabaseWithDeleted()
		{
            List<LoanProduct> list = new List<LoanProduct>();
            list.Add(new LoanProduct());
            list.Add(new LoanProduct());
            list.Add(new LoanProduct());
            mockPackageManagement.ExpectAndReturn("SelectAllPackages", list, true, OClientTypes.All);

            ProductServices productServices = SetMockManager(mockPackageManagement);
            Assert.AreEqual(3, productServices.FindAllPackages(true, OClientTypes.All).Count);
		}

		[Test]
		[ExpectedException(typeof(OpenCbsPackageDeleteException))]
		public void TestDeletePackageWhenPackageAlreadyDeleted()
		{
            LoanProduct pack1 = new LoanProduct();
			pack1.Id = 1;
			pack1.Name = "pack1";
			pack1.Delete = true;
			mockPackageManagement.Expect("DeleteProduct",pack1.Id);
            
            ProductServices productServices = SetMockManager(mockPackageManagement);
            productServices.DeletePackage(pack1);
		}

		[Test]
		public void TestDeletePackageWhenPackageNotAlreadyDeleted()
		{
            LoanProduct pack1 = new LoanProduct();
			pack1.Id = 1;
			pack1.Name = "pack1";
			pack1.Delete = false;
			mockPackageManagement.Expect("DeleteProduct",pack1.Id);

            ProductServices productServices = SetMockManager(mockPackageManagement);
            Assert.IsTrue(productServices.DeletePackage(pack1));
		}

		[Test]
		public void FindPackageByIdWhenNoResult()
		{
            mockPackageManagement.ExpectAndReturn("SelectPackageById",null,13);

            ProductServices productServices = SetMockManager(mockPackageManagement);
            Assert.IsNull(productServices.FindPackage(13));
		}

		[Test]
		public void FindAllInstallmentTypes()
		{
            List<InstallmentType> list = new List<InstallmentType>();
			list.Add(_monthly);
			list.Add(_biWeekly);
			mockInstallmentTypeManagement.SetReturnValue("SelectAllInstallmentTypes",list);
			installmentTypeManagement = (InstallmentTypeManager)mockInstallmentTypeManagement.MockInstance;
            ProductServices productServices = new ProductServices(installmentTypeManagement);

            Assert.AreEqual(2, productServices.FindAllInstallmentTypes().Count);
		}

		[Test]
		public void FindAllInstallmentsTypesWhenNoResult()
		{
            List<InstallmentType> list = new List<InstallmentType>();

            mockInstallmentTypeManagement.SetReturnValue("SelectAllInstallmentTypes", list);
            installmentTypeManagement = (InstallmentTypeManager)mockInstallmentTypeManagement.MockInstance;
            ProductServices productServices = new ProductServices(installmentTypeManagement);

            Assert.AreEqual(0, productServices.FindAllInstallmentTypes().Count);
		}

		[Test]
		public void FindAllInstallmentExoticProducts()
		{
            ExoticInstallmentsTable agro = new ExoticInstallmentsTable();
			agro.Name = "agro";

			ExoticInstallmentsTable services = new ExoticInstallmentsTable();
			services.Name = "services";

            List<ExoticInstallmentsTable> list = new List<ExoticInstallmentsTable>();
			list.Add(agro);
			list.Add(services);

            mockPackageManagement.SetReturnValue("SelectAllInstallmentsTables", list);
            ProductServices productServices = SetMockManager(mockPackageManagement);

			Assert.AreEqual(2,productServices.FindAllExoticProducts().Count);
		}

		[Test]
		public void TestAddExoticProductWhenExoticProductCorrectlySet()
		{
            ExoticInstallment exo = new ExoticInstallment();
            exo.InterestCoeff = 1;
            exo.PrincipalCoeff = 1;
			ExoticInstallmentsTable services = new ExoticInstallmentsTable();
			services.Add(exo);
			services.Name = "services";

            mockPackageManagement.ExpectAndReturn("AddExoticInstallmentsTable", 1, services);
            ProductServices productServices = SetMockManager(mockPackageManagement);
			Assert.AreEqual(1,productServices.AddExoticProduct(services, OLoanTypes.Flat));
		}

		[Test]
		public void TestFindAllExoticProducts()
		{
            ExoticInstallment exo1 = new ExoticInstallment();
			ExoticInstallment exo2 = new ExoticInstallment();

			ExoticInstallmentsTable services = new ExoticInstallmentsTable();
			services.Add(exo1);
			services.Add(exo2);

			ExoticInstallmentsTable trade = new ExoticInstallmentsTable();
			trade.Add(exo1);
			trade.Add(exo2);

			List<ExoticInstallmentsTable> list = new List<ExoticInstallmentsTable>();
			list.Add(services);
			list.Add(trade);

            mockPackageManagement.SetReturnValue("SelectAllInstallmentsTables", list);
            ProductServices productServices = SetMockManager(mockPackageManagement);
			Assert.AreEqual(2,productServices.FindAllExoticProducts().Count);
		}
				
		[Test]
		[ExpectedException(typeof(OpenCbsPackageSaveException))]
		public void TestAddExoticInstallmentInExoticProductWhenInstallmentIsNull()
		{
			ExoticInstallmentsTable product = new ExoticInstallmentsTable();
			ExoticInstallment exoticInstallment = new ExoticInstallment();
            exoticInstallment.InterestCoeff = null;
			exoticInstallment.PrincipalCoeff = 0;
            LoanProductManager productManager = new LoanProductManager(DataUtil.TESTDB);
            ProductServices productServices = new ProductServices(productManager);
            productServices.AddExoticInstallmentInExoticProduct(product, exoticInstallment, 1, false);

		}

		[Test]
		public void TestAddExoticInstallmentInExoticProductWhenInstallmentIsNotNull()
		{
			ExoticInstallmentsTable product = new ExoticInstallmentsTable();
			ExoticInstallment exoticInstallment = new ExoticInstallment();
			exoticInstallment.InterestCoeff = 0;
			exoticInstallment.PrincipalCoeff = 0;
            int number = product.GetNumberOfInstallments;
            ProductServices productServices = new ProductServices(new User {Id = 1});
            Assert.AreEqual(number + 1,
                productServices.AddExoticInstallmentInExoticProduct(product, exoticInstallment, 1, false).GetNumberOfInstallments);
        }

        #region -= Create Loan Product =-
        private static LoanProduct CreatePackage(string name,
            double? anticipatedTotalRepaymentPenalties,
            double? anticipatedTotalRepaymentPenaltiesMin,
            double? anticipatedTotalRepaymentPenaltiesMax,
            int? gracePeriod,int? gracePeriodMin,int? gracePeriodMax,
			decimal? interestRate,decimal? interestRateMin,decimal? interestRateMax,
			int? nbOfinstallments,int? nbOfinstallmentsMin,int? nbOfinstallmentsMax,
            List<EntryFee> entryFees,
            NonRepaymentPenaltiesNullableValues nonRepaymentPenalties, 
            NonRepaymentPenaltiesNullableValues nonRepaymentPenaltiesMin,
            NonRepaymentPenaltiesNullableValues nonRepaymentPenaltiesMax,
			InstallmentType type,
            OCurrency  amount, OCurrency amountMin, OCurrency  amountMax,
            ExoticInstallmentsTable product, 
            int? cycleId,
            List<LoanAmountCycle> amountCycles, List<RateCycle> rateCycles, List<MaturityCycle> maturityCycles,
            string productCode, List<ProductClientType> productClientTypes,
            OCurrency amountUnderLoc, OCurrency amountUnderLocMin, OCurrency amountUnderLocMax, 
            double? anticipatedPartialRepaymentPenalties,
            double? anticipatedPartialRepaymentPenaltiesMin,
            double? anticipatedPartialRepaymentPenaltiesMax,
            int? compulsoryAmount, int? compulsoryAmountMin, int? compulsoryAmountMax,
            Currency currency
            )
		{
		    var package = new LoanProduct();
			                          
		    package.Name = name;
		    package.Amount = amount;
		    package.AmountMin = amountMin;
		    package.AmountMax = amountMax;
            if (nonRepaymentPenalties!=null)
		    package.NonRepaymentPenalties = new NonRepaymentPenaltiesNullableValues
		                                        {
		                                            InitialAmount = nonRepaymentPenalties.InitialAmount,
		                                            OLB = nonRepaymentPenalties.OLB,
		                                            OverDueInterest = nonRepaymentPenalties.OverDueInterest,
		                                            OverDuePrincipal = nonRepaymentPenalties.OverDuePrincipal
		                                        };
            else if (nonRepaymentPenaltiesMin!=null && nonRepaymentPenaltiesMax!=null)
            {
                package.NonRepaymentPenaltiesMin = new NonRepaymentPenaltiesNullableValues
                {
                    InitialAmount = nonRepaymentPenaltiesMin.InitialAmount,
                    OLB = nonRepaymentPenaltiesMin.OLB,
                    OverDueInterest = nonRepaymentPenaltiesMin.OverDueInterest,
                    OverDuePrincipal = nonRepaymentPenaltiesMin.OverDuePrincipal
                };
                package.NonRepaymentPenaltiesMax = new NonRepaymentPenaltiesNullableValues
                {
                    InitialAmount = nonRepaymentPenaltiesMax.InitialAmount,
                    OLB = nonRepaymentPenaltiesMax.OLB,
                    OverDueInterest = nonRepaymentPenaltiesMax.OverDueInterest,
                    OverDuePrincipal = nonRepaymentPenaltiesMax.OverDuePrincipal
                };
            }

            package.AnticipatedTotalRepaymentPenalties = anticipatedTotalRepaymentPenalties;
            package.AnticipatedTotalRepaymentPenaltiesMin = anticipatedTotalRepaymentPenaltiesMin;
            package.AnticipatedTotalRepaymentPenaltiesMax = anticipatedTotalRepaymentPenaltiesMax;
            package.AnticipatedPartialRepaymentPenalties = anticipatedPartialRepaymentPenalties;
            package.AnticipatedPartialRepaymentPenaltiesMin = anticipatedPartialRepaymentPenaltiesMax;
            package.AnticipatedPartialRepaymentPenaltiesMax = anticipatedPartialRepaymentPenaltiesMin;

		    package.GracePeriod = gracePeriod;
		    package.GracePeriodMin = gracePeriodMin;
		    package.GracePeriodMax = gracePeriodMax;
		    package.InterestRate = interestRate;
		    package.InterestRateMin = interestRateMin;
		    package.InterestRateMax = interestRateMax;
		    package.InstallmentType = type;
		    package.ExoticProduct = product;
		    package.CycleId = cycleId;
		    package.LoanAmountCycleParams = amountCycles;
		    package.MaturityCycleParams = maturityCycles;
		    package.RateCycleParams = rateCycles;
		    package.Code = productCode;
		    package.AmountUnderLoc = amountUnderLoc;
		    package.AmountUnderLocMin = amountUnderLocMin;
		    package.AmountUnderLocMax = amountUnderLocMax;
		    
		    package.Currency = currency;
		    package.CompulsoryAmount = compulsoryAmount;
		    package.CompulsoryAmountMin = compulsoryAmountMin;
		    package.CompulsoryAmountMax = compulsoryAmountMax;
		    package.FundingLine = null;
		    package.NbOfInstallments = nbOfinstallments;
		    package.NbOfInstallmentsMin = nbOfinstallmentsMin;
		    package.NbOfInstallmentsMax = nbOfinstallmentsMax;
		    package.ProductClientTypes = productClientTypes;
		    package.EntryFees = entryFees;

			return package;
        }
        #endregion

        [Test]
		[ExpectedException(typeof(OpenCbsPackageSaveException))]
		public void TestParseFieldsAndCheckErrorsWhenNameIsNull()
		{
            var currency = new Currency();
            currency.Id = 1;
            var entryFees = new List<EntryFee>();

            LoanProduct package = CreatePackage(
                                    "", //package name
                                    null, //anticipatedTotalRepaymentPenalties
                                    3, //anticipatedTotalRepaymentPenaltiesMin
                                    6, //anticipatedTotalRepaymentPenaltiesMax
                                    4, //grace period
                                    null, //gracePeriodMin
                                    null, //gracePeriodMax
                                    9, //interestRate
                                    null, //interesrRateMin
                                    null, //interestRateMax
                                    0, //NmbOfInstallments
                                    null, //NmbOfInstallmentsMin
                                    null, //NmbOfInstallmentsMax
                                    entryFees, //List<EntryFee>
                                    new NonRepaymentPenaltiesNullableValues(1, 1, 1, 1), //NonRepaymentPenalties
                                    new NonRepaymentPenaltiesNullableValues(null, null, null, null), //NonRepaymentPenaltiesMin
                                    new NonRepaymentPenaltiesNullableValues(null, null, null, null), //NonRepaymentPenaltiesMax
                                    _monthly, //InstallmentType
                                    1000, //amount
                                    null, //amountMin
                                    null, //AmountMax
                                    new ExoticInstallmentsTable(), //ExoticInstallmentsTable
                                    null, //cycleId
                                    null, //List<LoanAmountCycle>
                                    null, //List<RateCycle>
                                    null, //List<MaturityCycle>
                                    "CODE1", //ProductCode
                                    null, //List<ProductClientType>
                                    null, //amountUnderLoc
                                    null, //amountUnderLocMin
                                    null, //amountUnderLocMax
                                    10, //anticipatedPartialRepaymentPenalties
                                    null, //anticipatedPartialRepaymentPenaltiesMin
                                    null, //anticipatedPartialRepaymentPenaltiesMax
                                    null, //compulsoryAmount
                                    null, //compulsoryAmountMin
                                    null, //compulsoryAmountMax
                                    currency
                );
            new ProductServices(new User {Id = 1}).ParseFieldsAndCheckErrors(package, false);			
		}

		[Test]
		[ExpectedException(typeof(OpenCbsPackageSaveException))]
		public void TestParseFieldsAndCheckErrorsWhenInstallmentTypeIsNull()
		{
            var currency = new Currency();
            currency.Id = 1;
            var entryFees = new List<EntryFee>();

            LoanProduct package = CreatePackage(
                                    "pack1", //package name
                                    null, //anticipatedTotalRepaymentPenalties
                                    3, //anticipatedTotalRepaymentPenaltiesMin
                                    6, //anticipatedTotalRepaymentPenaltiesMax
                                    4, //grace period
                                    null, //gracePeriodMin
                                    null, //gracePeriodMax
                                    9, //interestRate
                                    null, //interesrRateMin
                                    null, //interestRateMax
                                    0, //NmbOfInstallments
                                    null, //NmbOfInstallmentsMin
                                    null, //NmbOfInstallmentsMax
                                    entryFees, //List<EntryFee>
                                    new NonRepaymentPenaltiesNullableValues(1, 1, 1, 1), //NonRepaymentPenalties
                                    new NonRepaymentPenaltiesNullableValues(null, null, null, null), //NonRepaymentPenaltiesMin
                                    new NonRepaymentPenaltiesNullableValues(null, null, null, null), //NonRepaymentPenaltiesMax
                                    null, //InstallmentType
                                    1000, //amount
                                    null, //amountMin
                                    null, //AmountMax
                                    new ExoticInstallmentsTable(), //ExoticInstallmentsTable
                                    null, //cycleId
                                    null, //List<LoanAmountCycle>
                                    null, //List<RateCycle>
                                    null, //List<MaturityCycle>
                                    "CODE1", //ProductCode
                                    null, //List<ProductClientType>
                                    null, //amountUnderLoc
                                    null, //amountUnderLocMin
                                    null, //amountUnderLocMax
                                    10, //anticipatedPartialRepaymentPenalties
                                    null, //anticipatedPartialRepaymentPenaltiesMin
                                    null, //anticipatedPartialRepaymentPenaltiesMax
                                    null, //compulsoryAmount
                                    null, //compulsoryAmountMin
                                    null, //compulsoryAmountMax
                                    currency
                );

            new ProductServices(new User {Id = 1}).ParseFieldsAndCheckErrors(package, false);
		}

		[Test]
		[ExpectedException(typeof(OpenCbsPackageSaveException))]
		public void TestParseFieldsAndCheckErrorsWhenNonRepaymentPenaltiesIsNull()
		{
            var currency = new Currency();
            currency.Id = 1;
            var entryFees = new List<EntryFee>();

            LoanProduct package = CreatePackage(
                                    "pack1", //package name
                                    null, //anticipatedTotalRepaymentPenalties
                                    3, //anticipatedTotalRepaymentPenaltiesMin
                                    6, //anticipatedTotalRepaymentPenaltiesMax
                                    4, //grace period
                                    null, //gracePeriodMin
                                    null, //gracePeriodMax
                                    9, //interestRate
                                    null, //interesrRateMin
                                    null, //interestRateMax
                                    0, //NmbOfInstallments
                                    null, //NmbOfInstallmentsMin
                                    null, //NmbOfInstallmentsMax
                                    entryFees, //List<EntryFee>
                                    new NonRepaymentPenaltiesNullableValues(null, null, null, null), //NonRepaymentPenalties
                                    new NonRepaymentPenaltiesNullableValues(null, null, null, null), //NonRepaymentPenaltiesMin
                                    new NonRepaymentPenaltiesNullableValues(null, null, null, null), //NonRepaymentPenaltiesMax
                                    _monthly, //InstallmentType
                                    1000, //amount
                                    null, //amountMin
                                    null, //AmountMax
                                    new ExoticInstallmentsTable(), //ExoticInstallmentsTable
                                    null, //cycleId
                                    null, //List<LoanAmountCycle>
                                    null, //List<RateCycle>
                                    null, //List<MaturityCycle>
                                    "CODE1", //ProductCode
                                    null, //List<ProductClientType>
                                    null, //amountUnderLoc
                                    null, //amountUnderLocMin
                                    null, //amountUnderLocMax
                                    10, //anticipatedPartialRepaymentPenalties
                                    null, //anticipatedPartialRepaymentPenaltiesMin
                                    null, //anticipatedPartialRepaymentPenaltiesMax
                                    null, //compulsoryAmount
                                    null, //compulsoryAmountMin
                                    null, //compulsoryAmountMax
                                   currency
                );

                new ProductServices(new User {Id = 1}).ParseFieldsAndCheckErrors(package,  false);
		}

        [Test]
        [ExpectedException(typeof(OpenCbsPackageSaveException))]
        public void TestParseFieldsAndCheckErrorsWhenAnticipatedPartialRepaymentPenaltiesIsNull()
        {
            var currency = new Currency();
            currency.Id = 1;
            var entryFees = new List<EntryFee>();
            var nrp = new NonRepaymentPenaltiesNullableValues(200, 100, 10, 0.2);

            LoanProduct package = CreatePackage(
                                                "pack1", //package name
                                                20, //anticipatedTotalRepaymentPenalties
                                                null, //anticipatedTotalRepaymentPenaltiesMin
                                                null, //anticipatedTotalRepaymentPenaltiesMax
                                                4, //grace period
                                                null, //gracePeriodMin
                                                null, //gracePeriodMax
                                                9, //interestRate
                                                null, //interesrRateMin
                                                null, //interestRateMax
                                                0, //NmbOfInstallments
                                                null, //NmbOfInstallmentsMin
                                                null, //NmbOfInstallmentsMax
                                                entryFees, //List<EntryFee>
                                                nrp, //NonRepaymentPenalties
                                                new NonRepaymentPenaltiesNullableValues(null, null, null, null), //NonRepaymentPenaltiesMin
                                                new NonRepaymentPenaltiesNullableValues(null, null, null, null), //NonRepaymentPenaltiesMax
                                                _monthly, //InstallmentType
                                                1000, //amount
                                                null, //amountMin
                                                null, //AmountMax
                                                new ExoticInstallmentsTable(), //ExoticInstallmentsTable
                                                null, //cycleId
                                                null, //List<LoanAmountCycle>
                                                null, //List<RateCycle>
                                                null, //List<MaturityCycle>
                                                "CODE1", //ProductCode
                                                null, //List<ProductClientType>
                                                null, //amountUnderLoc
                                                null, //amountUnderLocMin
                                                null, //amountUnderLocMax
                                                null, //anticipatedPartialRRepaymentPenalties
                                                null, //anticipatedPartialRRepaymentPenaltiesMin
                                                null, //anticipatedPartialRRepaymentPenaltiesMax
                                                null, //compulsoryAmount
                                                null, //compulsoryAmountMin
                                                null, //compulsoryAmountMax
                                                currency
                );

            new ProductServices(new User {Id = 1}).ParseFieldsAndCheckErrors(package, false);
        }


	    [Test]
		[ExpectedException(typeof(OpenCbsPackageSaveException))]
		public void TestParseFieldsAndCheckErrorsWhenAnticipatedTotalRepaymentPenaltiesIsNull()
		{
            var currency = new Currency();
            currency.Id = 1;
            var entryFees = new List<EntryFee>();
		    var nrp = new NonRepaymentPenaltiesNullableValues(200, 100, 10, 0.2);

            LoanProduct package = CreatePackage(
                                                "pack1", //package name
                                                null, //anticipatedTotalRepaymentPenalties
                                                null, //anticipatedTotalRepaymentPenaltiesMin
                                                null, //anticipatedTotalRepaymentPenaltiesMax
                                                4, //grace period
                                                null, //gracePeriodMin
                                                null, //gracePeriodMax
                                                9, //interestRate
                                                null, //interesrRateMin
                                                null, //interestRateMax
                                                8, //NmbOfInstallments
                                                null, //NmbOfInstallmentsMin
                                                null, //NmbOfInstallmentsMax
                                                entryFees, //List<EntryFee>
                                                nrp, //NonRepaymentPenalties
                                                new NonRepaymentPenaltiesNullableValues(null, null, null, null), //NonRepaymentPenaltiesMin
                                                new NonRepaymentPenaltiesNullableValues(null, null, null, null), //NonRepaymentPenaltiesMax
                                                _monthly, //InstallmentType
                                                1000, //amount
                                                null, //amountMin
                                                null, //AmountMax
                                                new ExoticInstallmentsTable(), //ExoticInstallmentsTable
                                                null, //cycleId
                                                null, //List<LoanAmountCycle>
                                                null, //List<RateCycle>
                                                null, //List<MaturityCycle>
                                                "CODE1", //ProductCode
                                                null, //List<ProductClientType>
                                                null, //amountUnderLoc
                                                null, //amountUnderLocMin
                                                null, //amountUnderLocMax
                                                20, //anticipatedPartialRRepaymentPenalties
                                                null, //anticipatedPartialRRepaymentPenaltiesMin
                                                null, //anticipatedPartialRRepaymentPenaltiesMax
                                                null, //compulsoryAmount
                                                null, //compulsoryAmountMin
                                                null, //compulsoryAmountMax
                                                currency
                );

            new ProductServices(new User {Id = 1}).ParseFieldsAndCheckErrors(package, false);
		}

		[Test]
		public void TestParseFieldsAndCheckErrorsWhenEntryFeesIsNull()
		{
            var currency = new Currency();
            currency.Id = 1;
            var nrp = new NonRepaymentPenaltiesNullableValues(200, 100, 20, 0.2);
            
            LoanProduct package = CreatePackage(
                                                "pack1", //package name
                                                null, //anticipatedTotalRepaymentPenalties
                                                3, //anticipatedTotalRepaymentPenaltiesMin
                                                6, //anticipatedTotalRepaymentPenaltiesMax
                                                4, //grace period
                                                null, //gracePeriodMin
                                                null, //gracePeriodMax
                                                9, //interestRate
                                                null, //interesrRateMin
                                                null, //interestRateMax
                                                0, //NmbOfInstallments
                                                null, //NmbOfInstallmentsMin
                                                null, //NmbOfInstallmentsMax
                                                null, //List<EntryFee>
                                                nrp, //NonRepaymentPenalties
                                                new NonRepaymentPenaltiesNullableValues(null, null, null, null), //NonRepaymentPenaltiesMin
                                                new NonRepaymentPenaltiesNullableValues(null, null, null, null), //NonRepaymentPenaltiesMax
                                                _monthly, //InstallmentType
                                                1000, //amount
                                                null, //amountMin
                                                null, //AmountMax
                                                new ExoticInstallmentsTable(), //ExoticInstallmentsTable
                                                null, //cycleId
                                                null, //List<LoanAmountCycle>
                                                null, //List<RateCycle>
                                                null, //List<MaturityCycle>
                                                "CODE1", //ProductCode
                                                null, //List<ProductClientType>
                                                null, //amountUnderLoc
                                                null, //amountUnderLocMin
                                                null, //amountUnderLocMax
                                                10, //anticipatedPartialRRepaymentPenalties
                                                null, //anticipatedPartialRRepaymentPenaltiesMin
                                                null, //anticipatedPartialRRepaymentPenaltiesMax
                                                null, //compulsoryAmount
                                                null, //compulsoryAmountMin
                                                null, //compulsoryAmountMax
                                                currency
                );

            new ProductServices(new User {Id = 1}).ParseFieldsAndCheckErrors(package, false);
		}

		[Test]
		[ExpectedException(typeof(OpenCbsPackageSaveException))]
		public void TestParseFieldsAndCheckErrorsWhenGracePeriodIsNull()
		{
            var currency = new Currency();
            currency.Id = 1;
            var nrp = new NonRepaymentPenaltiesNullableValues(200, 100, 20, 0.2);
            var entryFees = new List<EntryFee>();

            LoanProduct package = CreatePackage(
                                                "pack1", //package name
                                                null, //anticipatedTotalRepaymentPenalties
                                                3, //anticipatedTotalRepaymentPenaltiesMin
                                                6, //anticipatedTotalRepaymentPenaltiesMax
                                                null, //grace period
                                                null, //gracePeriodMin
                                                null, //gracePeriodMax
                                                9, //interestRate
                                                null, //interesrRateMin
                                                null, //interestRateMax
                                                0, //NmbOfInstallments
                                                null, //NmbOfInstallmentsMin
                                                null, //NmbOfInstallmentsMax
                                                entryFees, //List<EntryFee>
                                                nrp, //NonRepaymentPenalties
                                                new NonRepaymentPenaltiesNullableValues(null, null, null, null), //NonRepaymentPenaltiesMin
                                                new NonRepaymentPenaltiesNullableValues(null, null, null, null), //NonRepaymentPenaltiesMax
                                                _monthly, //InstallmentType
                                                1000, //amount
                                                null, //amountMin
                                                null, //AmountMax
                                                new ExoticInstallmentsTable(), //ExoticInstallmentsTable
                                                null, //cycleId
                                                null, //List<LoanAmountCycle>
                                                null, //List<RateCycle>
                                                null, //List<MaturityCycle>
                                                "CODE1", //ProductCode
                                                null, //List<ProductClientType>
                                                null, //amountUnderLoc
                                                null, //amountUnderLocMin
                                                null, //amountUnderLocMax
                                                10, //anticipatedPartiaRRepaymentPenalties
                                                null, //anticipatedPartialRepaymentPenaltiesMin
                                                null, //anticipatedPartialRepaymentPenaltiesMax
                                                null, //compulsoryAmount
                                                null, //compulsoryAmountMin
                                                null, //compulsoryAmountMax
                                                currency
                );

            new ProductServices(new User {Id = 1}).ParseFieldsAndCheckErrors(package, false);
		}

		[Test]
		[ExpectedException(typeof(OpenCbsPackageSaveException))]
		public void TestParseFieldsAndCheckErrorsWhenNumberOfInstallmentsIsNull()
		{
            var currency = new Currency();
		    currency.Id = 1;
            var nrp = new NonRepaymentPenaltiesNullableValues(200,100,20,0.2);
		    var entryFees = new List<EntryFee>();

            LoanProduct package = CreatePackage(
                                                "pack1", //package name
                                                null, //anticipatedTotalRepaymentPenalties
                                                3, //anticipatedTotalRepaymentPenaltiesMin
                                                6, //anticipatedTotalRepaymentPenaltiesMax
                                                4, //grace period
                                                null, //gracePeriodMin
                                                null, //gracePeriodMax
                                                9, //interestRate
                                                null, //interesrRateMin
                                                null, //interestRateMax
                                                null, //NmbOfInstallments
                                                null, //NmbOfInstallmentsMin
                                                null, //NmbOfInstallmentsMax
                                                entryFees, //List<EntryFee>
                                                nrp, //NonRepaymentPenalties
                                                new NonRepaymentPenaltiesNullableValues(null, null, null, null), //NonRepaymentPenaltiesMin
                                                new NonRepaymentPenaltiesNullableValues(null, null, null, null), //NonRepaymentPenaltiesMax
                                                _monthly, //InstallmentType
                                                1000, //amount
                                                null, //amountMin
                                                null, //AmountMax
                                                new ExoticInstallmentsTable(), //ExoticInstallmentsTable
                                                null, //cycleId
                                                null, //List<LoanAmountCycle>
                                                null, //List<RateCycle>
                                                null, //List<MaturityCycle>
                                                "CODE1", //ProductCode
                                                null, //List<ProductClientType>
                                                null, //amountUnderLoc
                                                null, //amountUnderLocMin
                                                null, //amountUnderLocMax
                                                1, //anticipatedPartialRepaymentPenalties
                                                null, //anticipatedPartialRepaymentPenaltiesMin
                                                null, //anticipatedPartialRepaymentPenaltiesMax
                                                null, //compulsoryAmount
                                                null, //compulsoryAmountMin
                                                null, //compulsoryAmountMax
                                                currency
                );

            new ProductServices(new User {Id = 1}).ParseFieldsAndCheckErrors(package,  false);
		}

		[Test]
		[ExpectedException(typeof(OpenCbsPackageSaveException))]
		public void TestParseFieldsAndCheckErrorsWhenAmountMustBeNotNull()
		{
            LoanProduct package = CreatePackage(
                                                "pack1", //package name
                                                null, //anticipatedTotalRepaymentPenalties
                                                3, //anticipatedTotalRepaymentPenaltiesMin
                                                6, //anticipatedTotalRepaymentPenaltiesMax
                                                4, //grace period
                                                null, //gracePeriodMin
                                                null, //gracePeriodMax
                                                9, //interestRate
                                                null, //interesrRateMin
                                                null, //interestRateMax
                                                8, //NmbOfInstallments
                                                null, //NmbOfInstallmentsMin
                                                null, //NmbOfInstallmentsMax
                                                null, //List<EntryFee>
                                                new NonRepaymentPenaltiesNullableValues(null, null, 1, 1), //NonRepaymentPenalties
                                                new NonRepaymentPenaltiesNullableValues(null, null, null, null), //NonRepaymentPenaltiesMin
                                                new NonRepaymentPenaltiesNullableValues(null, null, null, null), //NonRepaymentPenaltiesMax
                                                _monthly, //InstallmentType
                                                null, //amount
                                                null, //amountMin
                                                null, //AmountMax
                                                null, //ExoticInstallmentsTable
                                                1, //cycleId
                                                null, //List<LoanAmountCycle>
                                                null, //List<RateCycle>
                                                null, //List<MaturityCycle>
                                                "CODE1", //ProductCode
                                                null, //List<ProductClientType>
                                                null, //amountUnderLoc
                                                null, //amountUnderLocMin
                                                null, //amountUnderLocMax
                                                null, //anticipatedPartialRRepaymentPenalties
                                                null, //anticipatedPartialRRepaymentPenaltiesMin
                                                null, //anticipatedPartialRRepaymentPenaltiesMax
                                                null, //compulsoryAmount
                                                null, //compulsoryAmountMin
                                                null, //compulsoryAmountMax
                                                new Currency()

                );

            new ProductServices(new User {Id = 1}).ParseFieldsAndCheckErrors(package,  false);
		}
		
		[Test]
		[ExpectedException(typeof(OpenCbsPackageSaveException))]
		public void TestParseFieldsAndCheckErrorsWhenExoticProductMustBeNotNull()
		{
            LoanProduct package = CreatePackage(
                                                "pack1", //package name
                                                null, //anticipatedTotalRepaymentPenalties
                                                3, //anticipatedTotalRepaymentPenaltiesMin
                                                6, //anticipatedTotalRepaymentPenaltiesMax
                                                4, //grace period
                                                null, //gracePeriodMin
                                                null, //gracePeriodMax
                                                9, //interestRate
                                                null, //interesrRateMin
                                                null, //interestRateMax
                                                8, //NmbOfInstallments
                                                null, //NmbOfInstallmentsMin
                                                null, //NmbOfInstallmentsMax
                                                null, //List<EntryFee>
                                                new NonRepaymentPenaltiesNullableValues(null, null, 1, 1), //NonRepaymentPenalties
                                                new NonRepaymentPenaltiesNullableValues(null, null, null, null), //NonRepaymentPenaltiesMin
                                                new NonRepaymentPenaltiesNullableValues(null, null, null, null), //NonRepaymentPenaltiesMax
                                                _monthly, //InstallmentType
                                                null, //amount
                                                null, //amountMin
                                                null, //AmountMax
                                                null, //ExoticInstallmentsTable
                                                1, //cycleId
                                                null, //List<LoanAmountCycle>
                                                null, //List<RateCycle>
                                                null, //List<MaturityCycle>
                                                "CODE1", //ProductCode
                                                null, //List<ProductClientType>
                                                null, //amountUnderLoc
                                                null, //amountUnderLocMin
                                                null, //amountUnderLocMax
                                                null, //anticipatedPartialRRepaymentPenalties
                                                null, //anticipatedPartialRRepaymentPenaltiesMin
                                                null, //anticipatedPartialRRepaymentPenaltiesMax
                                                null, //compulsoryAmount
                                                null, //compulsoryAmountMin
                                                null, //compulsoryAmountMax
                                                new Currency()

                );
            new ProductServices(new User {Id = 1}).ParseFieldsAndCheckErrors(package, true);
		}

		[Test]
		[ExpectedException(typeof(OpenCbsPackageSaveException))]
		public void TestParseFieldsAndCheckErrorsWhenPackageNameAlreadyExist()
		{
			mockPackageManagement.ExpectAndReturn("IsThisProductNameAlreadyExist",true,"pack1");
            ProductServices productServices = SetMockManager(mockPackageManagement);

            LoanProduct package = CreatePackage(
                                                "pack1", //package name
                                                null, //anticipatedTotalRepaymentPenalties
                                                3, //anticipatedTotalRepaymentPenaltiesMin
                                                6, //anticipatedTotalRepaymentPenaltiesMax
                                                4, //grace period
                                                null, //gracePeriodMin
                                                null, //gracePeriodMax
                                                9, //interestRate
                                                null, //interesrRateMin
                                                null, //interestRateMax
                                                8, //NmbOfInstallments
                                                null, //NmbOfInstallmentsMin
                                                null, //NmbOfInstallmentsMax
                                                null, //List<EntryFee>
                                                null, //NonRepaymentPenalties
                                                null, //NonRepaymentPenaltiesMin
                                                null, //NonRepaymentPenaltiesMax
                                                _monthly, //InstallmentType
                                                null, //amount
                                                null, //amountMin
                                                null, //AmountMax
                                                null, //ExoticInstallmentsTable
                                                1, //cycleId
                                                null, //List<LoanAmountCycle>
                                                null, //List<RateCycle>
                                                null, //List<MaturityCycle>
                                                "CODE1", //ProductCode
                                                null, //List<ProductClientType>
                                                null, //amountUnderLoc
                                                null, //amountUnderLocMin
                                                null, //amountUnderLocMax
                                                null, //anticipatedPartialRRepaymentPenalties
                                                null, //anticipatedPartialRRepaymentPenaltiesMin
                                                null, //anticipatedPartialRRepaymentPenaltiesMax
                                                null, //compulsoryAmount
                                                null, //compulsoryAmountMin
                                                null, //compulsoryAmountMax
                                                new Currency()

		        );

            productServices.ParseFieldsAndCheckErrors(package, false);
		}

        [Test]
        [ExpectedException(typeof(OpenCbsPackageSaveException))]
        public void TestProductWithoutAmountCycle()
        {
            LoanProductManager productManager = new LoanProductManager(DataUtil.TESTDB);
            ProductServices productServices = new ProductServices(productManager);

            var maturityCycles = GetMaturityCycles();
            var rateCycles = GetRateCycles(); 
            var entryFees = new List<EntryFee>();

            var nrp = new NonRepaymentPenaltiesNullableValues(200, 100, 20, 1);

            var currency = new Currency();
            currency.Id = 1;

            LoanProduct package = CreatePackage(
                                                "NewPackage", //package name
                                                null, //anticipatedTotalRepaymentPenalties
                                                3, //anticipatedTotalRepaymentPenaltiesMin
                                                6, //anticipatedTotalRepaymentPenaltiesMax
                                                4, //grace period
                                                null, //gracePeriodMin
                                                null, //gracePeriodMax
                                                null, //interestRate
                                                null, //interesrRateMin
                                                null, //interestRateMax
                                                null, //NmbOfInstallments
                                                null, //NmbOfInstallmentsMin
                                                null, //NmbOfInstallmentsMax
                                                entryFees, //List<EntryFee>
                                                nrp, //NonRepaymentPenalties
                                                null, //NonRepaymentPenaltiesMin
                                                null, //NonRepaymentPenaltiesMax
                                                _monthly, //InstallmentType
                                                null, //amount
                                                null, //amountMin
                                                null, //AmountMax
                                                null, //ExoticInstallmentsTable
                                                1, //cycleId
                                                null, //List<LoanAmountCycle>
                                                rateCycles, //List<RateCycle>
                                                maturityCycles, //List<MaturityCycle>
                                                "CODE1", //ProductCode
                                                null, //List<ProductClientType>
                                                null, //amountUnderLoc
                                                null, //amountUnderLocMin
                                                null, //amountUnderLocMax
                                                10, //anticipatedPartialRepaymentPenalties
                                                null, //anticipatedPartialRepaymentPenaltiesMin
                                                null, //anticipatedPartialRepaymentPenaltiesMax
                                                null, //compulsoryAmount
                                                null, //compulsoryAmountMin
                                                null, //compulsoryAmountMax
                                                currency
                );

            productServices.ParseFieldsAndCheckErrors(package, false);
        }

	    [Test]
        [ExpectedException(typeof(OpenCbsPackageSaveException))]
        public void TestProductWithoutRateCycle()
        {
            var loanAmountCycles = GetLoanAmountCycles();
	        var maturityCycles = GetMaturityCycles();
            
            var rateCycles = new List<RateCycle>();

            var entryFees = new List<EntryFee>();

            var nrp = new NonRepaymentPenaltiesNullableValues(200,100,20,1);

            var currency = new Currency();
            currency.Id = 1;

            LoanProduct product = CreatePackage(
                                                "NewPackage", //package name
                                                null, //anticipatedTotalRepaymentPenalties
                                                3, //anticipatedTotalRepaymentPenaltiesMin
                                                6, //anticipatedTotalRepaymentPenaltiesMax
                                                4, //grace period
                                                null, //gracePeriodMin
                                                null, //gracePeriodMax
                                                null, //interestRate
                                                null, //interesrRateMin
                                                null, //interestRateMax
                                                null, //NmbOfInstallments
                                                null, //NmbOfInstallmentsMin
                                                null, //NmbOfInstallmentsMax
                                                entryFees, //List<EntryFee>
                                                nrp, //NonRepaymentPenalties
                                                null, //NonRepaymentPenaltiesMin
                                                null, //NonRepaymentPenaltiesMax
                                                _monthly, //InstallmentType
                                                null, //amount
                                                null, //amountMin
                                                null, //AmountMax
                                                null, //ExoticInstallmentsTable
                                                1, //cycleId
                                                loanAmountCycles, //List<LoanAmountCycle>
                                                rateCycles, //List<RateCycle>
                                                maturityCycles, //List<MaturityCycle>
                                                "CODE1", //ProductCode
                                                null, //List<ProductClientType>
                                                null, //amountUnderLoc
                                                null, //amountUnderLocMin
                                                null, //amountUnderLocMax
                                                10, //anticipatedPartialRRepaymentPenalties
                                                null, //anticipatedPartialRRepaymentPenaltiesMin
                                                null, //anticipatedPartialRRepaymentPenaltiesMax
                                                null, //compulsoryAmount
                                                null, //compulsoryAmountMin
                                                null, //compulsoryAmountMax
                                                currency //Currency
                );
            ProductServices productServices = new ProductServices(new User() {Id = 1});
            productServices.ParseFieldsAndCheckErrors(product, false);
        }

	    [Test]
        [ExpectedException(typeof(OpenCbsPackageSaveException))]
        public void TestProductWithoutMaturityCycle()
        {
            var loanAmountCycles = new List<LoanAmountCycle>();
            var loanAmountCycle = new LoanAmountCycle(0, 0, 1,2,1,1);
            loanAmountCycles.Add(loanAmountCycle);
            var rateCycles = new List<RateCycle>();
            var rateCycle = new RateCycle(0,0,1,2,1,1);
            var maturityCycles = new List<MaturityCycle>();
            rateCycles.Add(rateCycle);
            Currency currency = new Currency();
            currency.Id = 1;
            var entryFees = new List<EntryFee>();

            var nrp = new NonRepaymentPenaltiesNullableValues(100, 200, 10, 0.2);
            
            LoanProduct product = CreatePackage(
                                                "NewPackage", //package name
                                                null, //anticipatedTotalRepaymentPenalties
                                                3, //anticipatedTotalRepaymentPenaltiesMin
                                                6, //anticipatedTotalRepaymentPenaltiesMax
                                                4, //grace period
                                                null, //gracePeriodMin
                                                null, //gracePeriodMax
                                                null, //interestRate
                                                null, //interesrRateMin
                                                null, //interestRateMax
                                                null, //NmbOfInstallments
                                                null, //NmbOfInstallmentsMin
                                                null, //NmbOfInstallmentsMax
                                                entryFees, //List<EntryFee>
                                                nrp, //NonRepaymentPenalties
                                                null, //NonRepaymentPenaltiesMin
                                                null, //NonRepaymentPenaltiesMax
                                                _monthly, //InstallmentType
                                                null, //amount
                                                null, //amountMin
                                                null, //AmountMax
                                                null, //ExoticInstallmentsTable
                                                1, //cycleId
                                                loanAmountCycles, //List<LoanAmountCycle>
                                                rateCycles, //List<RateCycle>
                                                maturityCycles, //List<MaturityCycle>
                                                "CODE1", //ProductCode
                                                null, //List<ProductClientType>
                                                null, //amountUnderLoc
                                                null, //amountUnderLocMin
                                                null, //amountUnderLocMax
                                                5, //anticipatedPartialRepaymentPenalties
                                                null, //anticipatedPartialRepaymentPenaltiesMin
                                                null, //anticipatedPartialRepaymentPenaltiesMax
                                                null, //compulsoryAmount
                                                null, //compulsoryAmountMin
                                                null, //compulsoryAmountMax
                                                currency
                );
            ProductServices productServices = new ProductServices(new User() { Id = 1 });
            productServices.ParseFieldsAndCheckErrors(product, false);
        }

        [Test]
        public void TestCreationOfProductWithoutLoanCycles()
        {
            Currency currency = new Currency();
            currency.Id = 1;
            var nrp = new NonRepaymentPenaltiesNullableValues(100, 200, 10, 0.2);
            var entryFees = new List<EntryFee>();
            LoanProduct product = CreatePackage(
                                                "NewPackage", //package name
                                                null, //anticipatedTotalRepaymentPenalties
                                                3, //anticipatedTotalRepaymentPenaltiesMin
                                                6, //anticipatedTotalRepaymentPenaltiesMax
                                                4, //grace period
                                                null, //gracePeriodMin
                                                null, //gracePeriodMax
                                                0.2m, //interestRate
                                                null, //interesrRateMin
                                                null, //interestRateMax
                                                0, //NmbOfInstallments
                                                null, //NmbOfInstallmentsMin
                                                null, //NmbOfInstallmentsMax
                                                entryFees, //List<EntryFee>
                                                nrp, //NonRepaymentPenalties
                                                null, //NonRepaymentPenaltiesMin
                                                null, //NonRepaymentPenaltiesMax
                                                _monthly, //InstallmentType
                                                2000, //amount
                                                null, //amountMin
                                                null, //AmountMax
                                                null, //ExoticInstallmentsTable
                                                null, //cycleId
                                                null, //List<LoanAmountCycle>
                                                null, //List<RateCycle>
                                                null, //List<MaturityCycle>
                                                "CODE1", //ProductCode
                                                null, //List<ProductClientType>
                                                null, //amountUnderLoc
                                                null, //amountUnderLocMin
                                                null, //amountUnderLocMax
                                                1, //anticipatedPartialRRepaymentPenalties
                                                null, //anticipatedPartialRRepaymentPenaltiesMin
                                                null, //anticipatedPartialRRepaymentPenaltiesMax
                                                null, //compulsoryAmount
                                                null, //compulsoryAmountMin
                                                null, //compulsoryAmountMax
                                                currency
                );
            LoanProductManager productManager = new LoanProductManager(DataUtil.TESTDB);
            ProductServices productServices = new ProductServices(productManager);
            productServices.ParseFieldsAndCheckErrors(product, false);
        }

        [Test]
        public void TestCreationOfProductWithLoanCycles()
        {
            var loanAmountCycle = GetLoanAmountCycles();
            var maturityCycles = GetMaturityCycles();
            var rateCycles = GetRateCycles();
            var entryFees = new List<EntryFee>();

            Currency currency = new Currency();
            currency.Id = 1;
            var nrp = new NonRepaymentPenaltiesNullableValues(100, 200, 10, 0.2);
            LoanProduct product = CreatePackage(
                                                "NewPackage", //package name
                                                null, //anticipatedTotalRepaymentPenalties
                                                3, //anticipatedTotalRepaymentPenaltiesMin
                                                6, //anticipatedTotalRepaymentPenaltiesMax
                                                4, //grace period
                                                null, //gracePeriodMin
                                                null, //gracePeriodMax
                                                0.2m, //interestRate
                                                null, //interesrRateMin
                                                null, //interestRateMax
                                                0, //NmbOfInstallments
                                                null, //NmbOfInstallmentsMin
                                                null, //NmbOfInstallmentsMax
                                                entryFees, //List<EntryFee>
                                                nrp, //NonRepaymentPenalties
                                                null, //NonRepaymentPenaltiesMin
                                                null, //NonRepaymentPenaltiesMax
                                                _monthly, //InstallmentType
                                                2000, //amount
                                                null, //amountMin
                                                null, //AmountMax
                                                null, //ExoticInstallmentsTable
                                                1, //cycleId
                                                loanAmountCycle, //List<LoanAmountCycle>
                                                rateCycles, //List<RateCycle>
                                                maturityCycles, //List<MaturityCycle>
                                                "CODE1", //ProductCode
                                                null, //List<ProductClientType>
                                                null, //amountUnderLoc
                                                null, //amountUnderLocMin
                                                null, //amountUnderLocMax
                                                1, //anticipatedPartialRRepaymentPenalties
                                                null, //anticipatedPartialRRepaymentPenaltiesMin
                                                null, //anticipatedPartialRRepaymentPenaltiesMax
                                                null, //compulsoryAmount
                                                null, //compulsoryAmountMin
                                                null, //compulsoryAmountMax
                                                currency
                );
            LoanProductManager productManager = new LoanProductManager(DataUtil.TESTDB);
            ProductServices productServices = new ProductServices(productManager);
            productServices.ParseFieldsAndCheckErrors(product, false);
        }

        [Test]
        [ExpectedException(typeof(OpenCbsPackageSaveException))]
        public void TestSavingOfLoanCycleWithWrongParams()
        {
            var loanAmountCycles = GetLoanAmountCycles();
            var maturityCycles = GetMaturityCycles();
            var rateCycles = GetRateCycles();

            loanAmountCycles.Add(new LoanAmountCycle(1,2,20,10,2,1));
            maturityCycles.Add(new MaturityCycle(1,2,20,10,2,1));
            rateCycles.Add(new RateCycle(1,2, 20,10,2,1));
            ProductServices productServices = new ProductServices(new User {Id = 1});
            productServices.SaveAllCycleParams(loanAmountCycles,rateCycles, maturityCycles);
        }

        private List<MaturityCycle> GetMaturityCycles()
        {
            var maturityCycle = new MaturityCycle();
            maturityCycle.CycleId = 2;
            maturityCycle.LoanCycle = 0;
            maturityCycle.CycleObjectId = 3;
            maturityCycle.Min = 10;
            maturityCycle.Max = 20;
            var maturityCycles = new List<MaturityCycle>();
            maturityCycles.Add(maturityCycle);

            maturityCycle = new MaturityCycle();
            maturityCycle.CycleId = 2;
            maturityCycle.LoanCycle = 1;
            maturityCycle.CycleObjectId = 3;
            maturityCycle.Min = 20;
            maturityCycle.Max = 30;
            maturityCycles.Add(maturityCycle);

            maturityCycle = new MaturityCycle();
            maturityCycle.CycleId = 2;
            maturityCycle.LoanCycle = 2;
            maturityCycle.CycleObjectId = 3;
            maturityCycle.Min = 30;
            maturityCycle.Max = 40;
            maturityCycles.Add(maturityCycle);
            
            return maturityCycles;
        }

        private List<RateCycle> GetRateCycles()
        {
            List<RateCycle> rateCycles = new List<RateCycle>();
            
            var rateCycle = new RateCycle();
            rateCycle.CycleId = 2;
            rateCycle.CycleObjectId = 2;
            rateCycle.LoanCycle = 0;
            rateCycle.Min = 0.2m;
            rateCycle.Max = 0.3m;
            rateCycles.Add(rateCycle);

            rateCycle = new RateCycle();
            rateCycle.CycleId = 2;
            rateCycle.CycleObjectId = 2;
            rateCycle.LoanCycle = 1;
            rateCycle.Min = 0.15m;
            rateCycle.Max = 0.2m;
            rateCycles.Add(rateCycle);

            rateCycle = new RateCycle();
            rateCycle.CycleId = 2;
            rateCycle.CycleObjectId = 2;
            rateCycle.LoanCycle = 2;
            rateCycle.Min = 0.1m;
            rateCycle.Max = 0.15m;
            
            rateCycles.Add(rateCycle);

            return rateCycles;
        }

        private List<LoanAmountCycle> GetLoanAmountCycles()
        {
            var amountCycle = new LoanAmountCycle();
            amountCycle.CycleId = 2;
            amountCycle.LoanCycle = 0;
            amountCycle.Min = 1000;
            amountCycle.Max = 2000;
            amountCycle.CycleObjectId = 1;
            var loanAmountCycles = new List<LoanAmountCycle>();
            loanAmountCycles.Add(amountCycle);
            
            amountCycle = new LoanAmountCycle();
            amountCycle.CycleId = 2;
            amountCycle.LoanCycle = 1;
            amountCycle.Min = 2000;
            amountCycle.Max = 3000;
            amountCycle.CycleObjectId = 1;
            loanAmountCycles.Add(amountCycle);

            amountCycle = new LoanAmountCycle();
            amountCycle.CycleId = 2;
            amountCycle.LoanCycle = 2;
            amountCycle.Min = 3000;
            amountCycle.Max = 4000;
            amountCycle.CycleObjectId = 1;
            loanAmountCycles.Add(amountCycle);
            
            return loanAmountCycles;
        }

        [Test]
        public void TestCreationOfIndividualLoanWithLoanCycle()
        {
            Person person = new Person();
            person.Id = 23456;
            
            var loanAmountCycle = GetLoanAmountCycles();
            var rateCycles = GetRateCycles();
            var maturityCycles = GetMaturityCycles();
            LoanProductManager productManager = new LoanProductManager(DataUtil.TESTDB);
            ProductServices productServices = new ProductServices(productManager);
            productServices.SaveAllCycleParams(loanAmountCycle, rateCycles, maturityCycles);
            var entryFees = new List<EntryFee>();
            Currency currency = new Currency();
            currency.Id = 1;
            var nrp = new NonRepaymentPenaltiesNullableValues(100, 200, 10, 0.2);
            LoanProduct product = CreatePackage(
                                                "NewPackage", //package name
                                                null, //anticipatedTotalRepaymentPenalties
                                                3, //anticipatedTotalRepaymentPenaltiesMin
                                                6, //anticipatedTotalRepaymentPenaltiesMax
                                                4, //grace period
                                                null, //gracePeriodMin
                                                null, //gracePeriodMax
                                                0.2m, //interestRate
                                                null, //interesrRateMin
                                                null, //interestRateMax
                                                0, //NmbOfInstallments
                                                null, //NmbOfInstallmentsMin
                                                null, //NmbOfInstallmentsMax
                                                entryFees, //List<EntryFee>
                                                nrp, //NonRepaymentPenalties
                                                null, //NonRepaymentPenaltiesMin
                                                null, //NonRepaymentPenaltiesMax
                                                _monthly, //InstallmentType
                                                2000, //amount
                                                null, //amountMin
                                                null, //AmountMax
                                                null, //ExoticInstallmentsTable
                                                2, //cycleId
                                                loanAmountCycle, //List<LoanAmountCycle>
                                                rateCycles, //List<RateCycle>
                                                maturityCycles, //List<MaturityCycle>
                                                "CODE1", //ProductCode
                                                null, //List<ProductClientType>
                                                null, //amountUnderLoc
                                                null, //amountUnderLocMin
                                                null, //amountUnderLocMax
                                                1, //anticipatedPartialRRepaymentPenalties
                                                null, //anticipatedPartialRRepaymentPenaltiesMin
                                                null, //anticipatedPartialRRepaymentPenaltiesMax
                                                null, //compulsoryAmount
                                                null, //compulsoryAmountMin
                                                null, //compulsoryAmountMax
                                                currency
                );

            Loan credit = new Loan();
            credit.Product = product;
            person.LoanCycle = 0;
            productServices.SetCyclesParamsForContract(product,credit,person,true);
            //Cheking loan amount values
            Assert.AreEqual(product.AmountMin, loanAmountCycle[0].Min.Value);
            Assert.AreEqual(product.AmountMax, loanAmountCycle[0].Max.Value);
            //Checking rate values
            Assert.AreEqual(product.InterestRateMin, rateCycles[0].Min.Value);
            Assert.AreEqual(product.InterestRateMax, rateCycles[0].Max.Value);
            //Checking maturity values
            Assert.AreEqual(product.NbOfInstallmentsMin, maturityCycles[0].Min.Value);
            Assert.AreEqual(product.NbOfInstallmentsMax, maturityCycles[0].Max.Value);

            person.LoanCycle = 2;

            productServices.SetCyclesParamsForContract(product, credit, person, true);
            //Cheking loan amount values
            Assert.AreEqual(product.AmountMin, loanAmountCycle[2].Min.Value);
            Assert.AreEqual(product.AmountMax, loanAmountCycle[2].Max.Value);
            //Checking rate values
            Assert.AreEqual(product.InterestRateMin, rateCycles[2].Min.Value);
            Assert.AreEqual(product.InterestRateMax, rateCycles[2].Max.Value);
            //Checking maturity values
            Assert.AreEqual(product.NbOfInstallmentsMin, maturityCycles[2].Min.Value);
            Assert.AreEqual(product.NbOfInstallmentsMax, maturityCycles[2].Max.Value);

            person.LoanCycle = 1;

            productServices.SetCyclesParamsForContract(product, credit, person, true);
            //Cheking loan amount values
            Assert.AreEqual(product.AmountMin, loanAmountCycle[1].Min.Value);
            Assert.AreEqual(product.AmountMax, loanAmountCycle[1].Max.Value);
            //Checking rate values
            Assert.AreEqual(product.InterestRateMin, rateCycles[1].Min.Value);
            Assert.AreEqual(product.InterestRateMax, rateCycles[1].Max.Value);
            //Checking maturity values
            Assert.AreEqual(product.NbOfInstallmentsMin, maturityCycles[1].Min.Value);
            Assert.AreEqual(product.NbOfInstallmentsMax, maturityCycles[1].Max.Value);
        }

        [Test]
        public void TestCreatingContractForNonSolidaryGroupWithLoanCycles()
        {
            LoanProductManager productManager = new LoanProductManager(DataUtil.TESTDB);
            ProductServices productServices = new ProductServices(productManager);
            
            ClientManager clientManager = new ClientManager(DataUtil.TESTDB);
            ClientServices clientServices = new ClientServices(clientManager);
            
            Village village = new Village();
            Person person = clientServices.FindPersonById(2);
            person.LoanCycle = 0;

            VillageMember villageMember = new VillageMember();
            villageMember.Tiers = person;
            village.Members.Add(villageMember);

            person = clientServices.FindPersonById(4);
            person.LoanCycle = 1;
            
            villageMember = new VillageMember();
            villageMember.Tiers = person;
            village.Members.Add(villageMember);

            person = clientServices.FindPersonById(6);
            person.LoanCycle = 2;
            villageMember = new VillageMember();
            villageMember.Tiers = person;
            village.Members.Add(villageMember);

            var loanAmountCycle = GetLoanAmountCycles();
            var maturityCycles = GetMaturityCycles();
            var rateCycles = GetRateCycles();
            var entryFees = new List<EntryFee>();

            Currency currency = new Currency();
            currency.Id = 1;
            var nrp = new NonRepaymentPenaltiesNullableValues(100, 200, 10, 0.2);
            LoanProduct product = CreatePackage(
                                                "NewPackage68", //package name
                                                null, //anticipatedTotalRepaymentPenalties
                                                3, //anticipatedTotalRepaymentPenaltiesMin
                                                6, //anticipatedTotalRepaymentPenaltiesMax
                                                4, //grace period
                                                null, //gracePeriodMin
                                                null, //gracePeriodMax
                                                0.2m, //interestRate
                                                null, //interesrRateMin
                                                null, //interestRateMax
                                                10, //NmbOfInstallments
                                                null, //NmbOfInstallmentsMin
                                                null, //NmbOfInstallmentsMax
                                                entryFees, //List<EntryFee>
                                                nrp, //NonRepaymentPenalties
                                                null, //NonRepaymentPenaltiesMin
                                                null, //NonRepaymentPenaltiesMax
                                                _monthly, //InstallmentType
                                                2000, //amount
                                                null, //amountMin
                                                null, //AmountMax
                                                null, //ExoticInstallmentsTable
                                                2, //cycleId
                                                loanAmountCycle, //List<LoanAmountCycle>
                                                rateCycles, //List<RateCycle>
                                                maturityCycles, //List<MaturityCycle>
                                                "CODE1", //ProductCode
                                                null, //List<ProductClientType>
                                                null, //amountUnderLoc
                                                null, //amountUnderLocMin
                                                null, //amountUnderLocMax
                                                1, //anticipatedPartialRRepaymentPenalties
                                                null, //anticipatedPartialRRepaymentPenaltiesMin
                                                null, //anticipatedPartialRRepaymentPenaltiesMax
                                                null, //compulsoryAmount
                                                null, //compulsoryAmountMin
                                                null, //compulsoryAmountMax
                                                currency
                );
            product.ClientType = '-';
            product.ProductClientTypes = new List<ProductClientType>();
            product.AddedEntryFees = entryFees;
            product.Id=productServices.AddPackage(product);
            productServices.SaveAllCycleParams(loanAmountCycle, rateCycles, maturityCycles);

            foreach (VillageMember member in village.Members)
            {
                productServices.SetVillageMemberCycleParams(member, product.Id, ((Person)member.Tiers).LoanCycle);
            }

            for (int i = 0; i < village.Members.Count; i++)
            {
                int j = i;
                //to avoid index out of range exception (it occurs in product.LoanAmountCycleParams)
                if (i == product.LoanAmountCycleParams.Count) j = product.LoanAmountCycleParams.Count - 1;
                Assert.AreEqual(village.Members[i].Product.AmountMin, product.LoanAmountCycleParams[j].Min);
                Assert.AreEqual(village.Members[i].Product.AmountMax, product.LoanAmountCycleParams[j].Max);
                Assert.AreEqual(village.Members[i].Product.InterestRateMin, product.RateCycleParams[j].Min.Value);
                Assert.AreEqual(village.Members[i].Product.InterestRateMax, product.RateCycleParams[j].Max.Value);
                Assert.AreEqual(village.Members[i].Product.NbOfInstallmentsMin, (int) product.MaturityCycleParams[j].Min.Value);
                Assert.AreEqual(village.Members[i].Product.NbOfInstallmentsMax, (int) product.MaturityCycleParams[j].Max.Value);
            }

            foreach (VillageMember member in village.Members)
            {
                Loan loan = new Loan();
                loan.LoanPurpose = "Unit tests";
                loan.Product = member.Product;
                
                loan.Amount = member.Product.AmountMin;
                loan.AmountMin = member.Product.AmountMin;
                loan.AmountMax = member.Product.AmountMax;
                loan.InterestRate = member.Product.InterestRateMin.Value;
                loan.InterestRateMin = member.Product.InterestRateMin.Value;
                loan.InterestRateMax = member.Product.InterestRateMax.Value;
                loan.NbOfInstallments = member.Product.NbOfInstallmentsMin.Value;
                loan.NmbOfInstallmentsMin = member.Product.NbOfInstallmentsMin.Value;
                loan.NmbOfInstallmentsMax = member.Product.NbOfInstallmentsMax;
                loan.StartDate = TimeProvider.Now;
                loan.FirstInstallmentDate = loan.FirstInstallmentDate = DateTime.Now + new TimeSpan(30, 0, 0, 0);
                loan.CloseDate = TimeProvider.Now + new TimeSpan(365, 0, 0, 0);
                loan.EconomicActivityId = 1;
                
                loan.NonRepaymentPenalties = new NonRepaymentPenalties
                {
                    InitialAmount = member.Product.NonRepaymentPenalties.InitialAmount ?? 0,
                    OLB = member.Product.NonRepaymentPenalties.OLB ?? 0,
                    OverDuePrincipal = member.Product.NonRepaymentPenalties.OverDuePrincipal ?? 0,
                    OverDueInterest = member.Product.NonRepaymentPenalties.OverDueInterest ?? 0
                };
                loan.LoanEntryFeesList = new List<LoanEntryFee>();
                loan.InstallmentType = member.Product.InstallmentType;
                loan.AnticipatedTotalRepaymentPenalties = 0;
                loan.FundingLine = new FundingLine("New_Founding_line", false);
                loan.FundingLine.Currency = loan.Product.Currency;
                loan.FundingLine.Id = 1;
                loan.LoanOfficer = User.CurrentUser;
                loan.LoanOfficer.LastName = "Unit";
                loan.LoanOfficer.FirstName = "Test";
                loan.Synchronize = false;
                loan.ContractStatus = OContractStatus.Validated;
                loan.CreditCommitteeCode = "OK";
                loan.GracePeriod = 0;
                loan.GracePeriodOfLateFees = member.Product.GracePeriodOfLateFees;
                loan.AnticipatedPartialRepaymentPenalties = 2;
                loan.AnticipatedTotalRepaymentPenalties = 3;
                loan.LoanCycle = ((Person)member.Tiers).LoanCycle;
                loan.InstallmentList = new List<Installment>();
                loan.InstallmentList.Add(new Installment());
                loan.InstallmentList[0].ExpectedDate = TimeProvider.Now + new TimeSpan(60, 0, 0, 0);
                loan.InstallmentList[0].InterestsRepayment = 10;
                loan.InstallmentList[0].Number = 1;
                loan.InstallmentList[0].CapitalRepayment = 10;
                loan.InstallmentList[0].PaidInterests = 10;
                loan.InstallmentList[0].PaidCapital = 10;
                loan.InstallmentList[0].PaidDate = TimeProvider.Now;
                loan.InstallmentList[0].FeesUnpaid = 10;
                loan.InstallmentList[0].PaidFees = 10;
                loan.InstallmentList[0].Comment = "Unit test";
                loan.InstallmentList[0].IsPending = false;
                loan.InstallmentList[0].StartDate = TimeProvider.Now;
                loan.InstallmentList[0].OLB = 20;

                loan.InstallmentList.Add(new Installment());
                loan.InstallmentList[1].ExpectedDate = TimeProvider.Now + new TimeSpan(120, 0, 0, 0);
                loan.InstallmentList[1].InterestsRepayment = 10;
                loan.InstallmentList[1].Number = 2;
                loan.InstallmentList[1].CapitalRepayment = 10;
                loan.InstallmentList[1].PaidInterests = 10;
                loan.InstallmentList[1].PaidCapital = 10;
                loan.InstallmentList[1].PaidDate = TimeProvider.Now + new TimeSpan(120, 0, 0, 0); 
                loan.InstallmentList[1].FeesUnpaid = 10;
                loan.InstallmentList[1].PaidFees = 10;
                loan.InstallmentList[1].Comment = "Unit test2";
                loan.InstallmentList[1].IsPending = false;
                loan.InstallmentList[1].StartDate = TimeProvider.Now.AddDays(60);
                loan.InstallmentList[1].OLB = 10;
                loan.Events = new EventStock();
                loan.AlignDisbursementDate = TimeProvider.Now;
                loan.CreditCommiteeDate = TimeProvider.Now;
                loan.Guarantors = new List<Guarantor>();
                loan.Collaterals = new List<ContractCollateral>();

                Project project;
                IClient client = member.Tiers;
                if (0 == client.Projects.Count)
                {
                    project = new Project("Village");
                    project.Name = "Village";
                    project.Code = "Village";
                    project.Aim = "Village";
                    project.BeginDate = TimeProvider.Now;
                    project.Id = ServicesProvider.GetInstance().GetProjectServices().SaveProject(project, client);
                    member.Tiers.AddProject(project);
                }
                project = client.Projects[0];
                client.District = new District(1, "Unit Test");
                client.Name = "Unit Test";

                ApplicationSettings appSettings = ApplicationSettings.GetInstance(User.CurrentUser.Md5);
                appSettings.UpdateParameter("ALLOWS_MULTIPLE_LOANS", 1);
                appSettings.UpdateParameter("CONTRACT_CODE_TEMPLATE", "Contract");
                
                ServicesProvider.GetInstance().GetContractServices().SaveLoan(ref loan, project.Id, ref client);
            }

            foreach (VillageMember member in village.Members)
            {
                member.ActiveLoans = ServicesProvider.GetInstance().GetContractServices().FindActiveContracts(member.Tiers.Id);
            }

            for (int i = 0; i < village.Members.Count; i++)
            {
                int j = i;
                //to avoid index out of range exception (it occurs in product.LoanAmountCycleParams)
                if (i == product.LoanAmountCycleParams.Count) j = product.LoanAmountCycleParams.Count - 1;
                for (int k = 0; k < village.Members[i].ActiveLoans.Count; k++)
                {
                    Assert.AreEqual(village.Members[i].ActiveLoans[k].AmountMin, loanAmountCycle[j].Min);
                    Assert.AreEqual(village.Members[i].ActiveLoans[k].AmountMax, loanAmountCycle[j].Max);
                    Assert.AreEqual(village.Members[i].ActiveLoans[k].Amount, loanAmountCycle[j].Min);
                    Assert.AreEqual(village.Members[i].ActiveLoans[k].InterestRateMin, (double?) rateCycles[j].Min.Value);
                    Assert.AreEqual(village.Members[i].ActiveLoans[k].InterestRateMax, (double?) rateCycles[j].Max.Value);
                    Assert.AreEqual(village.Members[i].ActiveLoans[k].InterestRate, (double?) rateCycles[j].Min.Value);
                    Assert.AreEqual(village.Members[i].ActiveLoans[k].NmbOfInstallmentsMin,
                                    (int) maturityCycles[j].Min.Value);
                    Assert.AreEqual(village.Members[i].ActiveLoans[k].NmbOfInstallmentsMax,
                                    (int) maturityCycles[j].Max.Value);
                    Assert.AreEqual(village.Members[i].ActiveLoans[k].NbOfInstallments, (int) maturityCycles[j].Min.Value);
                }
            }
        }
	}
}
