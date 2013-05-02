// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.CoreDomain.Clients;
using OpenCBS.CoreDomain.Contracts.Loans;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment;
using OpenCBS.CoreDomain.EconomicActivities;
using OpenCBS.CoreDomain.FundingLines;
using OpenCBS.CoreDomain.Products;
using OpenCBS.Enums;
using OpenCBS.Manager.Contracts;
using OpenCBS.Shared;
using OpenCBS.Shared.Settings;

namespace OpenCBS.Test.Manager
{
	/// <summary>
	/// Class containing tests ensuring the validity of ContractManagement methods.
	/// </summary>

	[TestFixture]
	public class TestLoanManager : BaseManagerTest
    {
        private LoanManager _loanManager;
        private Loan _loan;
	    private readonly Installment _installment1 = new Installment
	                                            {
	                                                Number = 1,
	                                                CapitalRepayment = 200,
	                                                InterestsRepayment = 100,
	                                                FeesUnpaid = 0,
	                                                ExpectedDate = DateTime.Today.AddDays(-1),
                                                    StartDate = DateTime.Today.AddDays(-2),
                                                    OLB = 400
	                                            };

	    private readonly Installment _installment2 = new Installment
	                                            {
	                                                Number = 2,
	                                                CapitalRepayment = 200,
	                                                InterestsRepayment = 100,
	                                                FeesUnpaid = 0,
	                                                ExpectedDate = DateTime.Today.AddDays(1),
                                                    StartDate = DateTime.Today.AddDays(-1),
                                                    OLB = 200
	                                            };


        protected override void SetUp()
        {
            base.SetUp();
            _loanManager = (LoanManager) container["LoanManager"];
            _loan = new Loan(new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()))
                        {
                            ClientType = OClientTypes.Person,
                            LoanOfficer = new User {Id = 1},
                            BranchCode = "DU",
                            CreationDate = DateTime.Today.AddDays(-1),
                            StartDate = DateTime.Today,
                            AlignDisbursementDate = DateTime.Today,
                            CloseDate = DateTime.Today.AddDays(1),
                            Product = new LoanProduct {Id = 1, Currency = new Currency {Id = 1}},
                            Amount = 1000m,
                            InterestRate = 3,
                            InstallmentType = new InstallmentType {Id = 1},
                            NbOfInstallments = 2,
                            FundingLine = new FundingLine {Id = 1},
                            InstallmentList = new List<Installment> {_installment1, _installment2},
                            EconomicActivityId = 1,
                            EconomicActivity = new EconomicActivity{Id = 1},
                            GracePeriodOfLateFees = 0
                        };
        }

	    [Test]
        public void AddLoan_Group_MinimumValues()
        {
	        _loan.ClientType = OClientTypes.Group;
	        _loan.LoanShares = new List<LoanShare> {new LoanShare {PersonId = 2, Amount = 1000}};

            _loan.Id = _loanManager.Add(_loan, 1);
            Assert.AreNotEqual(0, _loan.Id);
        }

        [Test]
        public void SelectLoan_Group_MinimumValues()
        {
            _loan.ClientType = OClientTypes.Group;
            _loan.LoanShares = new List<LoanShare> { new LoanShare { PersonId = 2, Amount = 1000 } };

            _loan.Id = _loanManager.Add(_loan, 1);
            Assert.AreNotEqual(0, _loan.Id);

            Loan selectedLoan = _loanManager.SelectLoan(_loan.Id, false, false, false);
            _AssertLoanMinimumValues(_loan, selectedLoan);
        }

	    [Test]
        public void SelectLoan_Person_MinimumValues()
        {
            _loan.Id = _loanManager.Add(_loan, 2);
            Assert.AreNotEqual(0,_loan.Id);

            Loan selectedLoan = _loanManager.SelectLoan(_loan.Id, false, false, false);
            _AssertLoanMinimumValues(_loan, selectedLoan);
        }

        [Test]
        public void AddLoan_Person_MinimumValues()
        {
            _loan.Id = _loanManager.Add(_loan, 1);
            Assert.AreNotEqual(0, _loan.Id);
        }

        /* Old tests for collaterals were disabled due to collaterals' logic was overwritten */

        /*
        [Test]
        public void AddLoan_Person_MinimumValues_Collateral()
        {
            _loan.Collaterals = new List<Collateral> {new Collateral {Id = 2, Name = "Test"}};
            _loan.Id = _loanManager.Add(_loan, 1, null);
            Assert.AreNotEqual(0, _loan.Id);
        }*/
        /*
        [Test]
        public void SelectLoan_Person_MinimumValues_Collateral()
        {
            _loan.Collaterals = new List<Collateral> { new Collateral { Id = 1, Name = "Collateral", Amount = 199 } };
            _loan.Id = _loanManager.Add(_loan, 2, null);
            Assert.AreNotEqual(0, _loan.Id);

            Loan selectedLoan = _loanManager.SelectLoan(_loan.Id, false, true, true);
            _AssertLoanMinimumValues(_loan, selectedLoan);

            Assert.AreEqual(1, selectedLoan.Collaterals.Count);
            Assert.AreEqual("Collateral", selectedLoan.Collaterals[0].Name);
            Assert.AreEqual(199, selectedLoan.Collaterals[0].Amount.Value);
        }*/
        /*
        [Test]
        public void AddLoan_Group_MinimumValues_Collateral()
        {
            _loan.ClientType = OClientTypes.Group;
            _loan.LoanShares = new List<LoanShare> { new LoanShare { PersonId = 2, Amount = 1000 } };
            _loan.Collaterals = new List<Collateral> {new Collateral {Id = 2, Name = "Test"}};

            _loan.Id = _loanManager.Add(_loan, 1, null);
            Assert.AreNotEqual(0, _loan.Id);
        }*/
        /*
        [Test]
        public void SelectLoan_Group_MinimumValues_Collateral()
        {
            _loan.ClientType = OClientTypes.Group;
            _loan.LoanShares = new List<LoanShare> { new LoanShare { PersonId = 2, Amount = 1000 } };
            _loan.Collaterals = new List<Collateral> { new Collateral { Id = 1, Name = "Collateral", Amount = 200 } };

            _loan.Id = _loanManager.Add(_loan, 1, null);
            Assert.AreNotEqual(0, _loan.Id);

            Loan selectedLoan = _loanManager.SelectLoan(_loan.Id, false, true, true);
            _AssertLoanMinimumValues(_loan, selectedLoan);

            Assert.AreEqual(1, selectedLoan.Collaterals.Count);
            Assert.AreEqual("Collateral", selectedLoan.Collaterals[0].Name);
            Assert.AreEqual(200, selectedLoan.Collaterals[0].Amount.Value);
        }*/

        [Test]
        public void AddLoan_Person_MinimumValues_Guarantor()
        {
            _loan.Guarantors = new List<Guarantor> {new Guarantor {Tiers = new Person {Id = 1}, Amount = 1000}};
            _loan.Id = _loanManager.Add(_loan, 1);
            Assert.AreNotEqual(0, _loan.Id);
        }

        [Test]
        public void AddLoan_Group_MinimumValues_Guarantor()
        {
            _loan.ClientType = OClientTypes.Group;
            _loan.LoanShares = new List<LoanShare> { new LoanShare { PersonId = 2, Amount = 1000 } };
            _loan.Guarantors = new List<Guarantor> { new Guarantor { Tiers = new Person { Id = 1 }, Amount = 1000 } };

            _loan.Id = _loanManager.Add(_loan, 1);
            Assert.AreNotEqual(0, _loan.Id);
        }

        [Test]
        public void SelectLoan_MinimumValues_Guarantor()
        {
            _loan.ClientType = OClientTypes.Group;
            _loan.LoanShares = new List<LoanShare> { new LoanShare { PersonId = 2, Amount = 1000 } };
            _loan.Guarantors = new List<Guarantor> { new Guarantor { Tiers = new Person { Id = 1 }, Amount = 1000 } };


            _loan.Id = _loanManager.Add(_loan, 1);
            Assert.AreNotEqual(0, _loan.Id);

            Loan selectedLoan = _loanManager.SelectLoan(_loan.Id, false, true, true);
            _AssertLoanMinimumValues(_loan, selectedLoan);

            Assert.AreEqual(1, selectedLoan.Guarantors.Count);
            Assert.AreEqual(1, selectedLoan.Guarantors[0].Tiers.Id);
            Assert.AreEqual(1000, selectedLoan.Guarantors[0].Amount.Value);
        }

        [Test]
        public void AddLoan_Group_MinimumValues_Installments()
        {
            _loan.ClientType = OClientTypes.Group;
            _loan.LoanShares = new List<LoanShare> { new LoanShare { PersonId = 2, Amount = 1000 } };

            _loan.Id = _loanManager.Add(_loan, 1);
            Assert.AreNotEqual(0, _loan.Id);
        }

        [Test]
        public void SelectLoan_MinimumValues_Installments()
        {
            _loan.ClientType = OClientTypes.Group;
            _loan.LoanShares = new List<LoanShare> { new LoanShare { PersonId = 2, Amount = 1000 } };
            
            _loan.Id = _loanManager.Add(_loan, 1);
            Assert.AreNotEqual(0, _loan.Id);

            Loan selectedLoan = _loanManager.SelectLoan(_loan.Id, false, false, true);
            _AssertLoanMinimumValues(_loan, selectedLoan);

            Assert.AreEqual(2, selectedLoan.InstallmentList.Count);
            Assert.AreEqual(1, selectedLoan.InstallmentList[0].Number);
            Assert.AreEqual(2, selectedLoan.InstallmentList[1].Number);
        }

        [Test]
        public void SelectLoan_MinimumValues_InstallmentType()
        {
            _loan.ClientType = OClientTypes.Group;
            _loan.LoanShares = new List<LoanShare> { new LoanShare { PersonId = 2, Amount = 1000 } };

            _loan.Id = _loanManager.Add(_loan, 1);
            Assert.AreNotEqual(0, _loan.Id);

            Loan selectedLoan = _loanManager.SelectLoan(_loan.Id, false, false, true);
            _AssertLoanMinimumValues(_loan, selectedLoan);

            Assert.AreEqual(1, selectedLoan.InstallmentType.Id);
            Assert.AreEqual("monthly", selectedLoan.InstallmentType.Name);
        }

        [Test]
        public void SelectLoan_NoData()
        {
            Assert.IsNull(_loanManager.SelectLoan(-18, false, false, true));
        }

	    [Test]
        public void SelectLoan_MinimumValues_GeneralInformation()
        {
            _loan.ClientType = OClientTypes.Group;
            _loan.LoanShares = new List<LoanShare> { new LoanShare { PersonId = 2, Amount = 1000 } };

            _loan.Id = _loanManager.Add(_loan, 1);
            Assert.AreNotEqual(0, _loan.Id);

            Loan selectedLoan = _loanManager.SelectLoan(_loan.Id, true, false, true);
            
            Assert.AreEqual(1, selectedLoan.Product.Id);
            Assert.AreEqual(0, selectedLoan.Events.GetNumberOfEvents);
        }

        [Test]
        public void SelectLoan_MinimumValues_OptionalInformation()
        {
            _loan.ClientType = OClientTypes.Group;
            _loan.LoanShares = new List<LoanShare> { new LoanShare { PersonId = 2, Amount = 1000 } };

            _loan.Id = _loanManager.Add(_loan, 1);
            Assert.AreNotEqual(0, _loan.Id);

            Loan selectedLoan = _loanManager.SelectLoan(_loan.Id, false, true, true);

            Assert.AreEqual(1, selectedLoan.LoanOfficer.Id);
            Assert.AreEqual(1, selectedLoan.FundingLine.Id);
            Assert.AreEqual(1, selectedLoan.LoanShares.Count);
        }

        [Test]
        public void AddLoan_Person_MinimumValues_Installments()
        {
            _loan.Id = _loanManager.Add(_loan, 1);
            Assert.AreNotEqual(0, _loan.Id);
        }

        [Test]
        public void AddLoan_Person_MaximumValuesForContract()
        {
            _loan.CreditCommiteeDate = new DateTime(2009, 1, 1);
            _loan.CreditCommitteeCode = "Code";
            _loan.CreditCommiteeComment = "Comment";

            _loan.Id = _loanManager.Add(_loan, 1);
            Assert.AreNotEqual(0, _loan.Id);
        }

        [Test]
        public void AddLoan_Person_MaximumValues()
        {
            _loan.AnticipatedTotalRepaymentPenalties = 2;
            _loan.Disbursed = true;
//            _loan.EntryFees = 1;
            _loan.WrittenOff = false;
            _loan.Rescheduled = false;
            _loan.BadLoan = true;
            _loan.NonRepaymentPenalties = new NonRepaymentPenalties(0.01, 0.02, 0.03, 0.04);
//            _loan.CommentsOfEnd = "Test";
            _loan.Synchronize = false;

            _loan.Id = _loanManager.Add(_loan, 1);
            Assert.AreNotEqual(0, _loan.Id);
        }

        [Test]
        public void GetNbOfLoansForClosure_ZeroResult()
        {
            Assert.AreEqual(0, _loanManager.GetNbOfLoansForClosure());
        }

        [Test]
        public void GetLoansForClosure_Degradation_ZeroResult()
        {
            Assert.AreEqual(0, _loanManager.SelectLoansForClosure(OClosureTypes.Degradation).Count);
        }

        [Test]
        public void GetLoansForClosure_Accrual_ZeroResult()
        {
            Assert.AreEqual(0, _loanManager.SelectLoansForClosure(OClosureTypes.Accrual).Count);
        }

	    [Test]
        public void GetNbOfLoansForClosure()
        {
	        _loan.Disbursed = true;
            _loan.Id = _loanManager.Add(_loan, 1);

            Assert.AreEqual(1, _loanManager.GetNbOfLoansForClosure());
        }

        [Test]
        public void GetLoansForClosure_Degradation()
        {
            _loan.Disbursed = true;
            _loan.Id = _loanManager.Add(_loan, 1);

            List<Loan> loans = _loanManager.SelectLoansForClosure(OClosureTypes.Degradation);
            Assert.AreEqual(1, loans.Count);
            Assert.AreEqual(_loan.Id, loans[0].Id);
        }

        [Test]
        public void GetLoansForClosure_Accrual()
        {
            _loan.Disbursed = true;
            _loan.Id = _loanManager.Add(_loan, 1);

            List<Loan> loans = _loanManager.SelectLoansForClosure(OClosureTypes.Degradation);
            Assert.AreEqual(1, loans.Count);
            Assert.AreEqual(_loan.Id, loans[0].Id);
        }

        [Test]
        public void UpdateLoanLoanOfficer()
        {
            _loan.Id = _loanManager.Add(_loan, 1);
            _loanManager.UpdateLoanLoanOfficer(_loan.Id, 2, 1);

            Loan updatedLoan = _loanManager.SelectLoan(_loan.Id, false, true, true);
            Assert.AreEqual(2, updatedLoan.LoanOfficer.Id);
        }

	    [Test]
        public void SelectLoans_NoResult()
        {
            Assert.AreEqual(0, _loanManager.SelectLoansByLoanOfficer(2).GetNumberOfAlerts);
        }

        [Test]
        public void CalculateCashToDisburseByDay_OneResult()
        {
            List<KeyValuePair<DateTime, decimal>> list = _loanManager.CalculateCashToDisburseByDay(new DateTime(2008, 1, 1), new DateTime(2008, 1, 5)); 

            Assert.AreEqual(1,list.Count);
            Assert.AreEqual(new DateTime(2008, 1, 2), list[0].Key);
            Assert.AreEqual(1000, list[0].Value);
        }

        [Test]
        public void CalculateCashToDisburseByDay_NoResult()
        {
            List<KeyValuePair<DateTime, decimal>> list = _loanManager.CalculateCashToDisburseByDay(new DateTime(2004, 1, 1), new DateTime(2004, 1, 5));

            Assert.AreEqual(0, list.Count);
        }

        [Test]
        public void CalculateCashToRepayByDay_NoResult()
        {
            List<KeyValuePair<DateTime, decimal>> list = _loanManager.CalculateCashToRepayByDay(new DateTime(2007, 1, 1), new DateTime(2007, 1, 5));
            Assert.AreEqual(0, list.Count);
        }

        [Test]
        public void GetGlobalOLBForProvisionning_NoResult()
        {
            Assert.AreEqual(0,_loanManager.GetGlobalOLBForProvisionning());
        }

        [Test]
        public void GetGlobalOLBForProvisionning_Result()
        {
            _loan.Disbursed = true;
            _loan.Id = _loanManager.Add(_loan, 1);

            Assert.AreEqual(400, _loanManager.GetGlobalOLBForProvisionning());
        }

        [Test]
        public void SelectLoansByProject_NoResult()
        {
            Assert.AreEqual(0, _loanManager.SelectLoansByProject(-1).Count);
        }

        [Test]
        public void SelectLoansByProject_Result()
        {
            Assert.AreEqual(1, _loanManager.SelectLoansByProject(1).Count);
        }

        [Test]
        public void UpdateLoanStatus()
        {
            _loan.ContractStatus = OContractStatus.Active;
            _loan.CreditCommiteeDate = new DateTime(2009, 1, 1);
            _loan.CreditCommiteeComment = "NotSet";

            _loan.Id = _loanManager.Add(_loan, 1);

            _loan.ContractStatus = OContractStatus.Validated;
            _loan.CreditCommiteeDate = new DateTime(2009,2,2);
            _loan.CreditCommiteeComment = "NotSet2";

            _loanManager.UpdateLoanStatus(_loan);

            Loan updatedLoan = _loanManager.SelectLoan(_loan.Id, true, true, true);
            Assert.AreEqual(OContractStatus.Validated, updatedLoan.ContractStatus);
            Assert.AreEqual(new DateTime(2009, 2, 2), updatedLoan.CreditCommiteeDate);
            Assert.AreEqual("NotSet2", updatedLoan.CreditCommiteeComment);
        }

	    [Test]
        public void CalculateCashToRepayByDay_Result()
        {
            _loan.Disbursed = true;
            _loan.Id = _loanManager.Add(_loan, 1);
            List<KeyValuePair<DateTime, decimal>> list = _loanManager.CalculateCashToRepayByDay(DateTime.Today.AddDays(-1), DateTime.Today.AddDays(1));

            Assert.AreEqual(2, list.Count);
        }

        [Test]
        public void UpdateLoanToWriteOff()
        {
            _loan.WrittenOff = false;
            _loan.Id = _loanManager.Add(_loan, 1);
            _loanManager.UpdateLoanToWriteOff(_loan.Id);

            Loan updatedLoan = _loanManager.SelectLoan(_loan.Id, false, false, false);
            Assert.IsTrue(updatedLoan.WrittenOff);
        }

        [Test]
        public void UpdateLoanToRescheduled()
        {
            _loan.Rescheduled = false;
            _loan.Id = _loanManager.Add(_loan, 1);
            _loanManager.UpdateLoanToRescheduled(Convert.ToDecimal(_loan.InterestRate), 0,_loan);

            Loan updatedLoan = _loanManager.SelectLoan(_loan.Id, false, false, false);
            Assert.IsTrue(updatedLoan.Rescheduled);
        }

        [Test]
        public void UpdateLoanToBadLoan()
        {
            _loan.BadLoan = false;
            _loan.Id = _loanManager.Add(_loan, 1);
            _loanManager.UpdateLoanToBadLoan(_loan.Id);

            Loan updatedLoan = _loanManager.SelectLoan(_loan.Id, false, false, false);
            Assert.IsTrue(updatedLoan.BadLoan);
        }

        [Test]
        public void UpdateLoan_AddAGuarantor_NoCollateral()
        {
            _loan.Guarantors = new List<Guarantor> { new Guarantor { Tiers = new Person { Id = 1 }, Amount = 1000 } };
            _loan.Id = _loanManager.Add(_loan, 1);

            _loan.Guarantors.Add(new Guarantor { Tiers = new Person { Id = 2 }, Amount = 600 });
            _loanManager.UpdateLoan(_loan);

            Loan updatedLoan = _loanManager.SelectLoan(_loan.Id, true, true, true);
            Assert.AreEqual(2,updatedLoan.Guarantors.Count);
        }

        [Test]
        public void UpdateLoan_ReplaceAGuarantor_NoCollateral()
        {
            _loan.Guarantors = new List<Guarantor> { new Guarantor { Tiers = new Person { Id = 1 }, Amount = 1000 } };
            _loan.Id = _loanManager.Add(_loan, 1);

            _loan.Guarantors = new List<Guarantor> { new Guarantor { Tiers = new Person { Id = 2 }, Amount = 1000 } };
            _loanManager.UpdateLoan(_loan);

            Loan updatedLoan = _loanManager.SelectLoan(_loan.Id, true, true, true);
            Assert.AreEqual(1, updatedLoan.Guarantors.Count);
            Assert.AreEqual(2, updatedLoan.Guarantors[0].Tiers.Id);
        }

	    [Test]
        public void UpdateLoan_NoGuarantor_NoCollateral()
        {
            Assert.Ignore();
            _loan.Id = _loanManager.Add(_loan, 2);
            
            _loan.FundingLine = new FundingLine{Id = 2};
            _loan.LoanOfficer = new User{Id = 2};
            _loan.Disbursed = false;
            _loan.Synchronize = true;
            _loan.NbOfInstallments = 2;
            _loan.Amount = 450;
            _loan.InterestRate = 5;
            _loan.GracePeriod = 9;
//            _loan.EntryFees = 4;
            _loan.AnticipatedTotalRepaymentPenalties = 7;
            _loan.NonRepaymentPenalties = new NonRepaymentPenalties(2, 4, 5, 9);
            _loan.StartDate = TimeProvider.Today;
            _loan.CloseDate = TimeProvider.Today.AddMonths(1);
            _loan.Closed = true;
            _loan.ContractStatus = OContractStatus.Refused;

            _loanManager.UpdateLoan(_loan);

            Loan updatedLoan = _loanManager.SelectLoan(_loan.Id, true, true, true);
            _AssertLoanMinimumValues(_loan, updatedLoan);
        }

        //[Test]
        //public void Test_SelectDisbursedLoan()
        //{
        //    List<AccountTiers> accountsTiers = _loanManager.SelectDisbursedContracts(new DateTime(2010, 01, 01), new DateTime(2010, 02, 01));
        //    Assert.AreEqual(0, accountsTiers.Count);
        //    //Assert.AreEqual(5, accountsTiers[0].ContractId);
        //    //Assert.AreEqual("EDE34/LALAU/1/1/2", accountsTiers[0].ContractCode);
        //    //Assert.AreEqual(new DateTime(2010, 01, 01), accountsTiers[0].DisbursmentDate);
        //    //Assert.AreEqual(1, accountsTiers[0].ClientId);
        //    //Assert.AreEqual(OClientTypes.Group, accountsTiers[0].ClientType);
        //    //Assert.AreEqual("Group Nicolas", accountsTiers[0].ClientName);
        //    //Assert.AreEqual(1, accountsTiers[0].LoanOfficerId);
        //    //Assert.AreEqual("ZOU ZOU", accountsTiers[0].LoanOfficerName);
        //    //Assert.AreEqual(2, accountsTiers[0].CollectiveAccount.Id);
        //    //Assert.AreEqual("10913", accountsTiers[0].CollectiveAccount.LocalNumber);
        //    //Assert.AreEqual("Cash Credit", accountsTiers[0].CollectiveAccount.Label);
        //}

	    private static void _AssertAlert(Alert pActualAlert, char pType, int pLoanId, string pLoanCode, string pClientName, 
            DateTime pEffectDate, OCurrency pAmount, DateTime pStartDate, DateTime pCloseDate, DateTime pCreationDate, string pInstallmentType,
            decimal pInterestRate, OCurrency pOLB, string pDistrict)
        {
            Assert.AreEqual(pType, pActualAlert.Type);
            Assert.AreEqual(pLoanId, pActualAlert.LoanId);
            Assert.AreEqual(pLoanCode, pActualAlert.LoanCode);
            Assert.AreEqual(pClientName, pActualAlert.ClientName);
            Assert.AreEqual(pEffectDate, pActualAlert.EffectDate);
            Assert.AreEqual(pAmount.Value, pActualAlert.Amount.Value);
            Assert.AreEqual(pStartDate, pActualAlert.StartDate);
            Assert.AreEqual(pCloseDate, pActualAlert.CloseDate);
            Assert.AreEqual(pCreationDate, pActualAlert.CreationDate);
            Assert.AreEqual(pInstallmentType, pActualAlert.InstallmentTypes);
            Assert.AreEqual(pInterestRate, pActualAlert.InterestRate);
            Assert.AreEqual(pOLB.Value, pActualAlert.OLB.Value);
            Assert.AreEqual(pDistrict, pActualAlert.DistrictName);
        }

	    private static void _AssertLoanMinimumValues(Loan pExpectedLoan, Loan pActualLoan)
        {
           Assert.Ignore();
            Assert.AreEqual(2, pActualLoan.InstallmentList.Count);
            Assert.AreEqual(1, pActualLoan.InstallmentType.Id);
            Assert.AreEqual("monthly", pActualLoan.InstallmentType.Name);
            Assert.AreEqual(pExpectedLoan.Id, pActualLoan.Id);
            Assert.AreEqual(pExpectedLoan.ClientType, pActualLoan.ClientType);
            Assert.AreEqual(pExpectedLoan.ContractStatus, pActualLoan.ContractStatus);
            Assert.AreEqual(pExpectedLoan.CreditCommiteeDate, pActualLoan.CreditCommiteeDate);
            Assert.AreEqual(pExpectedLoan.CreditCommiteeComment, pActualLoan.CreditCommiteeComment);
            Assert.AreEqual(pExpectedLoan.CreditCommitteeCode, pActualLoan.CreditCommitteeCode);
            Assert.AreEqual(pExpectedLoan.Amount, pActualLoan.Amount);
            Assert.AreEqual(pExpectedLoan.InterestRate, pActualLoan.InterestRate);
            Assert.AreEqual(pExpectedLoan.NbOfInstallments, pActualLoan.NbOfInstallments);
            Assert.AreEqual(pExpectedLoan.NonRepaymentPenalties.InitialAmount, pActualLoan.NonRepaymentPenalties.InitialAmount);
            Assert.AreEqual(pExpectedLoan.NonRepaymentPenalties.OLB, pActualLoan.NonRepaymentPenalties.OLB);
            Assert.AreEqual(pExpectedLoan.NonRepaymentPenalties.OverDueInterest, pActualLoan.NonRepaymentPenalties.OverDueInterest);
            Assert.AreEqual(pExpectedLoan.NonRepaymentPenalties.OverDuePrincipal, pActualLoan.NonRepaymentPenalties.OverDuePrincipal);
            Assert.AreEqual(pExpectedLoan.AnticipatedTotalRepaymentPenalties, pActualLoan.AnticipatedTotalRepaymentPenalties);
            Assert.AreEqual(pExpectedLoan.Disbursed, pActualLoan.Disbursed);
//            Assert.AreEqual(pExpectedLoan.EntryFees, pActualLoan.EntryFees);
            Assert.AreEqual(pExpectedLoan.GracePeriod, pActualLoan.GracePeriod);
            Assert.AreEqual(pExpectedLoan.WrittenOff, pActualLoan.WrittenOff);
            Assert.AreEqual(pExpectedLoan.Rescheduled, pActualLoan.Rescheduled);
            Assert.AreEqual(pExpectedLoan.Code, pActualLoan.Code);
            Assert.AreEqual(pExpectedLoan.BranchCode, pActualLoan.BranchCode);
            Assert.AreEqual(pExpectedLoan.CreationDate, pActualLoan.CreationDate);
            Assert.AreEqual(pExpectedLoan.StartDate, pActualLoan.StartDate);
            Assert.AreEqual(pExpectedLoan.CloseDate, pActualLoan.CloseDate);
            Assert.AreEqual(pExpectedLoan.BadLoan, pActualLoan.BadLoan);
            Assert.AreEqual(pExpectedLoan.Synchronize, pActualLoan.Synchronize);
        }

        [Test]
        public void PostponedLoanIsActiveForGroup()
        {
            List<Loan> activeLoans = _loanManager.SelectActiveLoans(10);
            Assert.IsTrue(activeLoans.Any(l => l.Code == "445"));
        }
  	}
}
