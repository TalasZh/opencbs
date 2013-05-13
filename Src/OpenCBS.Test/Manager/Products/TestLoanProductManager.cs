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
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.CoreDomain.Clients;
using OpenCBS.CoreDomain.Contracts.Loans;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment;
using OpenCBS.CoreDomain.FundingLines;
using OpenCBS.CoreDomain.Products;
using NUnit.Framework;
using OpenCBS.Enums;
using OpenCBS.Manager.Products;

namespace OpenCBS.Test.Manager.Products
{
    /// <summary>
    /// Description r�sum�e de TestProductManager.
    /// </summary>
    [TestFixture]
    public class TestLoanProductManager : BaseManagerTest
    {
        private LoanProduct _productWithValues;
        private LoanProduct _productWithRangeValues;
        private ExoticInstallmentsTable _exoticInstallmentsTable;
       

        protected override void SetUp()
        {
            base.SetUp();
            _SetUp();
        }

        private void _SetUp()
        {

            _exoticInstallmentsTable = new ExoticInstallmentsTable
            {
                Id = 1,
                Name = "Exotic"
            };
            _exoticInstallmentsTable.Add(new ExoticInstallment(1, 1, 1));

            _productWithValues = new LoanProduct
                                     {
                                         Id = 1,
                                         Name = "Product1",
                                         Code = "Code",
                Delete = false,
                ClientType = 'I',
                Amount = 1000,
                InterestRate = 1,
                InstallmentType = new InstallmentType { Id = 1 },
                GracePeriod = 1,
                NbOfInstallments = 6,
                AnticipatedTotalRepaymentPenalties = 10,
                KeepExpectedInstallment = true,
                ChargeInterestWithinGracePeriod = true,
                AnticipatedTotalRepaymentPenaltiesBase = OAnticipatedRepaymentPenaltiesBases.RemainingInterest,
                NonRepaymentPenalties = new NonRepaymentPenaltiesNullableValues
                {
                    OverDueInterest = 2,
                    InitialAmount = 3,
                    OLB = 4,
                    OverDuePrincipal = 5
                },
            };
            EntryFee fee = new EntryFee();
            fee.Value = 3;
            _productWithValues.EntryFees = new List<EntryFee>();
            _productWithValues.EntryFees.Add(fee);

            _productWithRangeValues = new LoanProduct
            {
                Id = 2,
                Name = "Product2",
                Code = "Code2",
                Delete = false,
                ClientType = 'C',
                FundingLine = new FundingLine { Id = 1 },
                AmountMin = 1000,
                AmountMax = 10111,
                InterestRateMin = 1,
                InterestRateMax = 3,
                InstallmentType = new InstallmentType { Id = 1 },
                GracePeriodMin = 1,
                GracePeriodMax = 5,
                NbOfInstallmentsMin = 3,
                NbOfInstallmentsMax = 10,
                AnticipatedTotalRepaymentPenaltiesMin = 1,
                AnticipatedTotalRepaymentPenaltiesMax = 4,
                KeepExpectedInstallment = true,
                ChargeInterestWithinGracePeriod = true,
                AnticipatedTotalRepaymentPenaltiesBase = OAnticipatedRepaymentPenaltiesBases.RemainingInterest,
                NonRepaymentPenaltiesMin = new NonRepaymentPenaltiesNullableValues
                {
                    OverDueInterest = 1,
                    InitialAmount = 2,
                    OLB = 3,
                    OverDuePrincipal = 4
                },
                NonRepaymentPenaltiesMax = new NonRepaymentPenaltiesNullableValues
                {
                    OverDueInterest = 11,
                    InitialAmount = 12,
                    OLB = 13,
                    OverDuePrincipal = 14
                },
            };
            _productWithRangeValues.EntryFees=new List<EntryFee>();
            EntryFee entryFee2 = new EntryFee();
            entryFee2.Min = 0;
            entryFee2.Max = 10;
            _productWithRangeValues.EntryFees.Add(entryFee2);
        }

        [Test]
        public void AddProduct_Values_ExoticInstallmentsTable_AmountCyclesStock()
        {
            Assert.Ignore();
            LoanProductManager loanProductManager = (LoanProductManager)container["LoanProductManager"];
            _productWithValues.ExoticProduct = _exoticInstallmentsTable;
//            _productWithValues.AmountCycles = _amountCycleStock;
            _productWithValues.Amount = null;
            _productWithValues.Name = "Test";
            _productWithValues.Currency = new Currency {Id = 1};

            int productId = loanProductManager.Add(_productWithValues);
            Assert.AreNotEqual(0, productId);
        }

        [Test]
        public void AddProduct_Values()
        {
            LoanProductManager loanProductManager = (LoanProductManager)container["LoanProductManager"];
            _productWithValues.Name = "Test";
            _productWithValues.Currency = new Currency { Id = 1 };
            int productId = loanProductManager.Add(_productWithValues);
            Assert.AreNotEqual(0, productId);
        }

        [Test]
        public void AddProduct_RangeValues()
        {
            LoanProductManager loanProductManager = (LoanProductManager)container["LoanProductManager"];
            _productWithRangeValues.Name = "Test";
            _productWithRangeValues.Currency = new Currency { Id = 1 };
            int productId = loanProductManager.Add(_productWithRangeValues);
            Assert.AreNotEqual(0, productId);
        }

        [Test]
        public void SelectProduct_Values()
        {
            LoanProductManager loanProductManager = (LoanProductManager)container["LoanProductManager"];

            LoanProduct loanProduct = loanProductManager.Select(_productWithValues.Id);
            EntryFee entryFee = new EntryFee();
            entryFee.Value = 3;
            loanProduct.EntryFees=new List<EntryFee>();
            loanProduct.EntryFees.Add(entryFee);
            AssertLoanProduct(_productWithValues, loanProduct, false);
        }

        [Test]
        public void SelectProduct_Values_ExoticInstallmentsTable_AmountCyclesStock()
        {
            Assert.Ignore();
            LoanProductManager loanProductManager = (LoanProductManager)container["LoanProductManager"];
            _productWithValues.ExoticProduct = _exoticInstallmentsTable;
//            _productWithValues.AmountCycles = _amountCycleStock;
            _productWithValues.Amount = null;
            _productWithValues.Name = "Test";
            _productWithValues.Currency = new Currency { Id = 1 };
            _productWithValues.Id = loanProductManager.Add(_productWithValues);

            LoanProduct selectedProduct = loanProductManager.Select(_productWithValues.Id);

            selectedProduct.EntryFees=new List<EntryFee>();
            EntryFee entryFee = new EntryFee();
            entryFee.Value = 3;
            selectedProduct.EntryFees.Add(entryFee);

            AssertLoanProduct(_productWithValues, selectedProduct, true);
        }

        [Test]
        public void SelectProduct_RangeValues()
        {
            LoanProductManager loanProductManager = (LoanProductManager)container["LoanProductManager"];

            LoanProduct loanProduct = loanProductManager.Select(_productWithRangeValues.Id);
            EntryFee entryFee = new EntryFee();
            entryFee.Min = 0;
            entryFee.Max = 10;
            loanProduct.EntryFees=new List<EntryFee>();
            loanProduct.EntryFees.Add(entryFee);
            AssertLoanProduct(_productWithRangeValues, loanProduct, false);
        }

        [Test]
        public void UpdateProduct_Values_DontUpdateContracts()
        {
            LoanProductManager loanProductManager = (LoanProductManager)container["LoanProductManager"];

            LoanProduct loanProduct = loanProductManager.Select(_productWithValues.Id);
            loanProduct.GracePeriod = 8;
            loanProduct.AnticipatedTotalRepaymentPenaltiesBase = OAnticipatedRepaymentPenaltiesBases.RemainingInterest;
            loanProduct.AnticipatedTotalRepaymentPenalties = 33;
            loanProduct.ChargeInterestWithinGracePeriod = true;
            loanProduct.NbOfInstallments = 5;
            loanProduct.NonRepaymentPenalties.InitialAmount = 111;
            loanProduct.NonRepaymentPenalties.OLB = 222;
            loanProduct.NonRepaymentPenalties.OverDueInterest = 333;
            loanProduct.NonRepaymentPenalties.OverDuePrincipal = 444;
            loanProduct.EntryFees = new List<EntryFee>();
            EntryFee fee = new EntryFee();
            fee.Value = 2;
            fee.IsAdded = true;
            loanProduct.EntryFees.Add(fee);
            loanProductManager.UpdatePackage(loanProduct,false);
            loanProductManager.InsertEntryFees(loanProduct.EntryFees, loanProduct.Id);
            LoanProduct updatedLoanProduct = loanProductManager.Select(loanProduct.Id);
            updatedLoanProduct.EntryFees = loanProductManager.SelectEntryFeesWithoutCycles(updatedLoanProduct.Id, false);

            AssertLoanProduct(loanProduct, updatedLoanProduct, false);
        }

        [Test]
        public void UpdateProduct_Values_ExoticInstallmentsTable_AmountCyclesStock_DontUpdateContracts()
        {
            Assert.Ignore();
            LoanProductManager loanProductManager = (LoanProductManager)container["LoanProductManager"];

            LoanProduct loanProduct = loanProductManager.Select(_productWithValues.Id);
            loanProduct.GracePeriod = 8;
            loanProduct.AnticipatedTotalRepaymentPenaltiesBase = OAnticipatedRepaymentPenaltiesBases.RemainingInterest;
            loanProduct.AnticipatedTotalRepaymentPenalties = 33;
            loanProduct.ChargeInterestWithinGracePeriod = true;
            loanProduct.NbOfInstallments = 5;
            loanProduct.NonRepaymentPenalties.InitialAmount = 111;
            loanProduct.NonRepaymentPenalties.OLB = 222;
            loanProduct.NonRepaymentPenalties.OverDueInterest = 333;
            loanProduct.NonRepaymentPenalties.OverDuePrincipal = 444;
            loanProduct.ExoticProduct = new ExoticInstallmentsTable { Id = 2, Name = "Exotic2" };
//            loanProduct.AmountCycles = new AmountCycleStock { Id = 2, Name = "Cycle2" };
            loanProduct.EntryFees = new List<EntryFee>();
            EntryFee entryFee = new EntryFee();
            entryFee.Value = 2;
            entryFee.IsAdded = true;
            loanProduct.EntryFees.Add(entryFee);

            loanProductManager.UpdatePackage(loanProduct,false);
            loanProductManager.InsertEntryFees(loanProduct.EntryFees, loanProduct.Id);
            LoanProduct updatedLoanProduct = loanProductManager.Select(loanProduct.Id);

            updatedLoanProduct.EntryFees = loanProductManager.SelectEntryFeesWithoutCycles(updatedLoanProduct.Id, false);
            

            AssertLoanProduct(loanProduct, updatedLoanProduct, true);
        }

        [Test]
        public void UpdateProduct_RangeValues_DontUpdateContracts()
        {
            LoanProductManager loanProductManager = (LoanProductManager)container["LoanProductManager"];

            LoanProduct loanProduct = loanProductManager.Select(_productWithRangeValues.Id);
            loanProduct.GracePeriodMin = 8;
            loanProduct.GracePeriodMax = 12;
            loanProduct.AnticipatedTotalRepaymentPenaltiesBase = OAnticipatedRepaymentPenaltiesBases.RemainingInterest;
            loanProduct.AnticipatedTotalRepaymentPenalties = 33;
            loanProduct.ChargeInterestWithinGracePeriod = true;
            loanProduct.NbOfInstallmentsMin = 5;
            loanProduct.NbOfInstallmentsMax = 6;
            loanProduct.NonRepaymentPenaltiesMin.InitialAmount = 111;
            loanProduct.NonRepaymentPenaltiesMin.OLB = 222;
            loanProduct.NonRepaymentPenaltiesMin.OverDueInterest = 333;
            loanProduct.NonRepaymentPenaltiesMin.OverDuePrincipal = 444;
            loanProduct.NonRepaymentPenaltiesMax.InitialAmount = 1111;
            loanProduct.NonRepaymentPenaltiesMax.OLB = 2222;
            loanProduct.NonRepaymentPenaltiesMax.OverDueInterest = 3333;
            loanProduct.NonRepaymentPenaltiesMax.OverDuePrincipal = 4444;

            EntryFee fee = new EntryFee();
            fee.Min = 2;
            fee.Max = 4;
            fee.IsAdded = true;
            loanProduct.EntryFees = new List<EntryFee>();
            loanProduct.EntryFees.Add(fee);

            loanProductManager.UpdatePackage(loanProduct,false);
            loanProductManager.InsertEntryFees(loanProduct.EntryFees, loanProduct.Id);
            LoanProduct updatedLoanProduct = loanProductManager.Select(loanProduct.Id);
            updatedLoanProduct.EntryFees = loanProductManager.SelectEntryFeesWithoutCycles(updatedLoanProduct.Id, false);

            AssertLoanProduct(loanProduct, updatedLoanProduct, false);
        }

        [Test]
        public void IsTheProductNameAlreadyExist_ReallyExist()
        {
            LoanProductManager loanProductManager = (LoanProductManager)container["LoanProductManager"];
            bool exist = loanProductManager.IsThisProductNameAlreadyExist("Product1");

            Assert.IsTrue(exist);
        }

        [Test]
        public void IsTheProductNameAlreadyExist_DontExist()
        {
            LoanProductManager loanProductManager = (LoanProductManager)container["LoanProductManager"];
            bool exist = loanProductManager.IsThisProductNameAlreadyExist("DDDD");

            Assert.IsFalse(exist);
        }

        [Test]
        public void IsTheExoticProductNameAlreadyExist_ReallyExist()
        {
            LoanProductManager loanProductManager = (LoanProductManager)container["LoanProductManager"];
            bool exist = loanProductManager.IsThisExoticProductNameAlreadyExist("Exotic");

            Assert.IsTrue(exist);
        }

        [Test]
        public void IsTheExoticProductNameAlreadyExist_DontExist()
        {
            LoanProductManager loanProductManager = (LoanProductManager)container["LoanProductManager"];
            bool exist = loanProductManager.IsThisExoticProductNameAlreadyExist("SSSZE");

            Assert.IsFalse(exist);
        }

        [Test]
        public void IsTheAmountCycleStockNameAlreadyExist_ReallyExist()
        {
            LoanProductManager loanProductManager = (LoanProductManager)container["LoanProductManager"];
            bool exist = loanProductManager.IsLoanCycleNameAlreadyExist("Cycle");

            Assert.IsTrue(exist);
        }

        [Test]
        public void IsTheAmountCycleStockNameAlreadyExist_DontExist()
        {
            LoanProductManager loanProductManager = (LoanProductManager)container["LoanProductManager"];
            bool exist = loanProductManager.IsLoanCycleNameAlreadyExist("SDQSD");

            Assert.IsFalse(exist);
        }

        [Test]
        public void DeleteProduct_AlreadyDeleted()
        {
            LoanProductManager loanProductManager = (LoanProductManager)container["LoanProductManager"];
            loanProductManager.DeleteProduct(3);

            LoanProduct deletedProduct = loanProductManager.Select(3);
            Assert.IsTrue(deletedProduct.Delete);
        }

        [Test]
        public void DeleteProduct()
        {
            LoanProductManager loanProductManager = (LoanProductManager)container["LoanProductManager"];
            loanProductManager.DeleteProduct(1);

            LoanProduct deletedProduct = loanProductManager.Select(1);
            Assert.IsTrue(deletedProduct.Delete);
        }

        [Test]
        public void SelectAllProducts_OnlyForPerson_DontShowDeleted()
        {
            LoanProductManager loanProductManager = (LoanProductManager)container["LoanProductManager"];
            List<LoanProduct> list = loanProductManager.SelectAllPackages(false, OClientTypes.Person);
            EntryFee entryFee = new EntryFee();
            entryFee.Value = 3;
            list[0].EntryFees = new List<EntryFee>();
            list[0].EntryFees.Add(entryFee);
            Assert.AreEqual(1,list.Count);
            AssertLoanProduct(_productWithValues, list[0], false);
            _productWithValues.ClientType = '-';
            _productWithValues.Id = 4;
            _productWithValues.Name = "Product4";
        }

        [Test]
        public void SelectAllProducts_OnlyForPerson_ShowDeleted()
        {
            LoanProductManager loanProductManager = (LoanProductManager)container["LoanProductManager"];
            List<LoanProduct> list = loanProductManager.SelectAllPackages(true, OClientTypes.Person);

            Assert.AreEqual(2, list.Count);
        }

        [Test]
        public void SelectAllProducts_OnlyForGroup_DontShowDeleted()
        {
            LoanProductManager loanProductManager = (LoanProductManager)container["LoanProductManager"];
            List<LoanProduct> list = loanProductManager.SelectAllPackages(false, OClientTypes.Group);

            Assert.AreEqual(0, list.Count);
        }

        [Test]
        public void SelectAllProducts_OnlyForCorporate_DontShowDeleted()
        {
            LoanProductManager loanProductManager = (LoanProductManager)container["LoanProductManager"];
            List<LoanProduct> list = loanProductManager.SelectAllPackages(false, OClientTypes.Corporate);

            Assert.AreEqual(1, list.Count);
        }

        [Test]
        public void SelectAllProducts_All_DontShowDeleted()
        {
            LoanProductManager loanProductManager = (LoanProductManager)container["LoanProductManager"];
            List<LoanProduct> list = loanProductManager.SelectAllPackages(false, OClientTypes.All);

            Assert.AreEqual(3, list.Count);
        }

        [Test]
        public void SelectAllProducts_All_ShowDeleted()
        {
            LoanProductManager loanProductManager = (LoanProductManager)container["LoanProductManager"];
            List<LoanProduct> list = loanProductManager.SelectAllPackages(true, OClientTypes.All);

            Assert.AreEqual(4, list.Count);
        }
        
        [Test]
        public void AddExoticProduct()
        {
            LoanProductManager loanProductManager = (LoanProductManager)container["LoanProductManager"];
            _exoticInstallmentsTable.Name = "Test";
            int id = loanProductManager.AddExoticInstallmentsTable(_exoticInstallmentsTable);

            Assert.AreNotEqual(0, id);
        }

        [Test]
        public void AddExoticInstallment()
        {
            LoanProductManager loanProductManager = (LoanProductManager)container["LoanProductManager"];
            loanProductManager.AddExoticInstallment(new ExoticInstallment{Number = 2,InterestCoeff = null,PrincipalCoeff = 2}, _exoticInstallmentsTable );
        }

        [Test]
        public void AddAmountCycleStock()
        {
            Assert.Ignore();
//            LoanProductManager loanProductManager = (LoanProductManager)container["LoanProductManager"];
//            int id = loanProductManager.InsertLoanCycle(_amountCycleStock, null);
//
//            Assert.AreNotEqual(0, id);
        }

        [Test]
        public void AddAmountCycle()
        {
            Assert.Ignore();
//            LoanProductManager loanProductManager = (LoanProductManager)container["LoanProductManager"];
//            loanProductManager.AddAmountCycle(new AmountCycle {Number = 2, Max = 1000, Min = 100}, _amountCycleStock, null);
        }

        [Test]
        public void SelectAllAmountCycleStock()
        {
            Assert.Ignore();
//            LoanProductManager loanProductManager = (LoanProductManager)container["LoanProductManager"];
//            List<AmountCycleStock> list = loanProductManager.SelectAllAmountCyclesStock();
//
//            Assert.AreEqual(2,list.Count);
//            _AssertAmountCycleStock(list[0], "Cycle", 1);
//            _AssertAmountCycleStock(list[1], "Cycle2", 1);
        }

//        private static  void _AssertAmountCycleStock(AmountCycleStock pAmountCycleStock,string pName, int pNumberOfLoanCycle)
//        {
//            Assert.AreEqual(pName, pAmountCycleStock.Name);
//            Assert.AreEqual(pNumberOfLoanCycle, pAmountCycleStock.GetNumberOfLoanCycles);
//        }

        [Test]
        public void SelectAllExoticInstallmentTable()
        {
            LoanProductManager loanProductManager = (LoanProductManager)container["LoanProductManager"];
            List<ExoticInstallmentsTable> list = loanProductManager.SelectAllInstallmentsTables();

            Assert.AreEqual(2, list.Count);
            _AssertExoticInstallmentTable(list[0], "Exotic", 1);
            _AssertExoticInstallmentTable(list[1], "Exotic2",2);
        }

        private static void _AssertExoticInstallmentTable(ExoticInstallmentsTable pExoticInstallmentsTable, string pName, int pNumberOfExoticInstallment)
        {
            Assert.AreEqual(pName, pExoticInstallmentsTable.Name);
            Assert.AreEqual(pNumberOfExoticInstallment, pExoticInstallmentsTable.GetNumberOfInstallments);
        }

        private static void AssertLoanProduct(LoanProduct pExpectedProduct, LoanProduct pActualProduct, bool pUseAmountCycle)
        {
            Assert.AreEqual(pExpectedProduct.Id, pActualProduct.Id);
            Assert.AreEqual(pExpectedProduct.Name, pActualProduct.Name);
            Assert.AreEqual(pExpectedProduct.Code, pActualProduct.Code);
            Assert.AreEqual(pExpectedProduct.Delete, pActualProduct.Delete);
            Assert.AreEqual(pExpectedProduct.ClientType, pActualProduct.ClientType);
            Assert.AreEqual(pExpectedProduct.ProductType, pActualProduct.ProductType);
            Assert.AreEqual(pExpectedProduct.InstallmentType.Id, pActualProduct.InstallmentType.Id);

            _AssertLoanProductGracePeriodValues(pExpectedProduct, pActualProduct);

            _AssertLoanProductNbOfInstallmentsValues(pExpectedProduct, pActualProduct);

            _AssertLoanProductAnticipatedTotalRepaymentPenaltiesValues(pExpectedProduct, pActualProduct);

            Assert.AreEqual(pExpectedProduct.ChargeInterestWithinGracePeriod, pActualProduct.ChargeInterestWithinGracePeriod);
            Assert.AreEqual(pExpectedProduct.AnticipatedTotalRepaymentPenaltiesBase, pActualProduct.AnticipatedTotalRepaymentPenaltiesBase);
            Assert.AreEqual(pExpectedProduct.KeepExpectedInstallment, pActualProduct.KeepExpectedInstallment);


            if (pExpectedProduct.ExoticProduct != null)
            {
                Assert.AreEqual(pExpectedProduct.ExoticProduct.Name, pActualProduct.ExoticProduct.Name);
            }
            else
                Assert.AreEqual(null, pActualProduct.ExoticProduct);


            _AssertLoanProductEntryFeesValues(pExpectedProduct, pActualProduct);

            _AssertLoanProductAmountValues(pExpectedProduct, pActualProduct, pUseAmountCycle);

            _AssertLoanProductInterestRateValues(pExpectedProduct, pActualProduct);
        }

        private static void _AssertLoanProductInterestRateValues(IProduct pExpectedProduct, IProduct pActualProduct)
        {
            if (!pExpectedProduct.InterestRate.HasValue)
            {
                Assert.AreEqual(pExpectedProduct.InterestRateMin, pActualProduct.InterestRateMin);
                Assert.AreEqual(pExpectedProduct.InterestRateMax, pActualProduct.InterestRateMax);
            }
            else
                Assert.AreEqual(pExpectedProduct.InterestRate, pActualProduct.InterestRate);
        }

        private static void _AssertLoanProductAmountValues(IProduct pExpectedProduct, IProduct pActualProduct, bool pUseAmountCycle)
        {
            if (pUseAmountCycle) return;
            if (!pExpectedProduct.Amount.HasValue)
            {
                Assert.AreEqual(pExpectedProduct.AmountMin.Value, pActualProduct.AmountMin.Value);
                Assert.AreEqual(pExpectedProduct.AmountMax.Value, pActualProduct.AmountMax.Value);
            }
            else
                Assert.AreEqual(pExpectedProduct.Amount.Value, pActualProduct.Amount.Value);
        }

        private static void _AssertLoanProductEntryFeesValues(LoanProduct pExpectedProduct, LoanProduct pActualProduct)
        {
            if (!pExpectedProduct.EntryFees[0].Value.HasValue)
            {
                Assert.AreEqual(pExpectedProduct.EntryFees[0].Min, pActualProduct.EntryFees[0].Min);
                Assert.AreEqual(pExpectedProduct.EntryFees[0].Max, pActualProduct.EntryFees[0].Max);
            }
            else
                Assert.AreEqual(pExpectedProduct.EntryFees[0].Value, pActualProduct.EntryFees[0].Value);
        }

        private static void _AssertLoanProductAnticipatedTotalRepaymentPenaltiesValues(LoanProduct pExpectedProduct, LoanProduct pActualProduct)
        {
            if (!pExpectedProduct.AnticipatedTotalRepaymentPenalties.HasValue)
            {
                Assert.AreEqual(pExpectedProduct.AnticipatedTotalRepaymentPenaltiesMin.Value, pActualProduct.AnticipatedTotalRepaymentPenaltiesMin.Value);
                Assert.AreEqual(pExpectedProduct.AnticipatedTotalRepaymentPenaltiesMax.Value, pActualProduct.AnticipatedTotalRepaymentPenaltiesMax.Value);
            }
            else
                Assert.AreEqual(pExpectedProduct.AnticipatedTotalRepaymentPenalties.Value, pActualProduct.AnticipatedTotalRepaymentPenalties.Value);
        }

        private static void _AssertLoanProductNbOfInstallmentsValues(LoanProduct pExpectedProduct, LoanProduct pActualProduct)
        {
            if (!pExpectedProduct.NbOfInstallments.HasValue)
            {
                Assert.AreEqual(pExpectedProduct.NbOfInstallmentsMin.Value, pActualProduct.NbOfInstallmentsMin.Value);
                Assert.AreEqual(pExpectedProduct.NbOfInstallmentsMax.Value, pActualProduct.NbOfInstallmentsMax.Value);
            }
            else
                Assert.AreEqual(pExpectedProduct.NbOfInstallments.Value, pActualProduct.NbOfInstallments.Value);
        }

        private static void _AssertLoanProductGracePeriodValues(LoanProduct pExpectedProduct, LoanProduct pActualProduct)
        {
            if (!pExpectedProduct.GracePeriod.HasValue)
            {
                Assert.AreEqual(pExpectedProduct.GracePeriodMin.Value, pActualProduct.GracePeriodMin.Value);
                Assert.AreEqual(pExpectedProduct.GracePeriodMax.Value, pActualProduct.GracePeriodMax.Value);
            }
            else
                Assert.AreEqual(pExpectedProduct.GracePeriod.Value, pActualProduct.GracePeriod.Value);
        }
    }
}
