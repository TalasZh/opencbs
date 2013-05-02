//Octopus MFS is an integrated suite for managing a Micro Finance Institution: clients, contracts, accounting, reporting and risk
//Copyright © 2006,2007 OCTO Technology & OXUS Development Network
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
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.CoreDomain.LoanCycles;
using OpenCBS.CoreDomain.Products;
using OpenCBS.Enums;
using System.Data.SqlClient;

namespace OpenCBS.Manager.Products
{
    /// <summary>
    /// This class provides all the methods required to manages Package datas in database
    /// </summary>
    public class LoanProductManager : Manager
    {
        private readonly InstallmentTypeManager installmentTypeManagement;

        public delegate void ProductLoadedEventHandler(LoanProduct product);

        public event ProductLoadedEventHandler ProductLoaded;

        public LoanProductManager(User pUser): base(pUser)
        {
            installmentTypeManagement = new InstallmentTypeManager(pUser);
        }

        public LoanProductManager(string testDB) : base(testDB)
        {
            installmentTypeManagement = new InstallmentTypeManager(testDB);
        }

        private static void SetProduct(OctopusCommand c, LoanProduct pPackage)
        {
            c.AddParam("@packageId", pPackage.Id);
            c.AddParam("@deleted", pPackage.Delete);
            c.AddParam("@name", pPackage.Name);
            c.AddParam("@code", pPackage.Code);
            c.AddParam("@clientType", pPackage.ClientType);
            c.AddParam("@installmentTypeId", pPackage.InstallmentType.Id);
            c.AddParam("@loanType", (int)pPackage.LoanType);
            c.AddParam("@rounding_type", (int)pPackage.RoundingType);
            c.AddParam("@amount", pPackage.Amount);
            c.AddParam("@amountMin", pPackage.AmountMin);
            c.AddParam("@amountMax", pPackage.AmountMax);

            c.AddParam("@interestRate", pPackage.InterestRate);
            c.AddParam("@interestRateMin", pPackage.InterestRateMin);
            c.AddParam("@interestRateMax", pPackage.InterestRateMax);         
   
            c.AddParam("@gracePeriod", pPackage.GracePeriod);
            c.AddParam("@gracePeriodMin", pPackage.GracePeriodMin);
            c.AddParam("@gracePeriodMax", pPackage.GracePeriodMax);
            c.AddParam("@grace_period_of_latefees", pPackage.GracePeriodOfLateFees);
            
            c.AddParam("@nbOfInstallments", pPackage.NbOfInstallments);
            c.AddParam("@nbOfInstallmentsMin", pPackage.NbOfInstallmentsMin);
            c.AddParam("@nbOfInstallmentsMax", pPackage.NbOfInstallmentsMax);
            
            c.AddParam("@anticipatedTotalRepaymentPenalties", pPackage.AnticipatedTotalRepaymentPenalties);
            c.AddParam("@anticipatedTotalRepaymentPenaltiesMin", pPackage.AnticipatedTotalRepaymentPenaltiesMin);
            c.AddParam("@anticipatedTotalRepaymentPenaltiesMax", pPackage.AnticipatedTotalRepaymentPenaltiesMax);

            c.AddParam("@anticipatedPartialRepaymentPenalties", pPackage.AnticipatedPartialRepaymentPenalties);
            c.AddParam("@anticipatedPartialRepaymentPenaltiesMin", pPackage.AnticipatedPartialRepaymentPenaltiesMin);
            c.AddParam("@anticipatedPartialRepaymentPenaltiesMax", pPackage.AnticipatedPartialRepaymentPenaltiesMax);
            
            c.AddParam("@chargeInterestWithInGracePeriod", pPackage.ChargeInterestWithinGracePeriod);
            
            c.AddParam("@AnticipatedTotalRepaymentPenaltiesBase", (int)pPackage.AnticipatedTotalRepaymentPenaltiesBase);
            c.AddParam("@AnticipatedPartialRepaymentPenaltiesBase", (int)pPackage.AnticipatedPartialRepaymentPenaltiesBase);
            
            c.AddParam("@keepExpectedInstallment", pPackage.KeepExpectedInstallment);
            c.AddParam("@currency_id", pPackage.Currency.Id);

            if (pPackage.FundingLine != null)
                c.AddParam("@fundingLine_id", pPackage.FundingLine.Id);
            else
                c.AddParam("@fundingLine_id", null);

            c.AddParam("@nonRepaymentPenaltiesInitialAmount", pPackage.NonRepaymentPenalties.InitialAmount);
            c.AddParam("@nonRepaymentPenaltiesOlb", pPackage.NonRepaymentPenalties.OLB);
            c.AddParam("@nonRepaymentPenaltiesOverdueInterest", pPackage.NonRepaymentPenalties.OverDueInterest);
            c.AddParam("@nonRepaymentPenaltiesOverduePrincipal", pPackage.NonRepaymentPenalties.OverDuePrincipal);

            c.AddParam("@nonRepaymentPenaltiesInitialAmountMin", pPackage.NonRepaymentPenaltiesMin.InitialAmount);
            c.AddParam("@nonRepaymentPenaltiesOlbMin", pPackage.NonRepaymentPenaltiesMin.OLB);
            c.AddParam("@nonRepaymentPenaltiesOverdueInterestMin", pPackage.NonRepaymentPenaltiesMin.OverDueInterest);
            c.AddParam("@nonRepaymentPenaltiesOverduePrincipalMin", pPackage.NonRepaymentPenaltiesMin.OverDuePrincipal);

            c.AddParam("@nonRepaymentPenaltiesInitialAmountMax", pPackage.NonRepaymentPenaltiesMax.InitialAmount);
            c.AddParam("@nonRepaymentPenaltiesOlbMax", pPackage.NonRepaymentPenaltiesMax.OLB);
            c.AddParam("@nonRepaymentPenaltiesOverdueInterestMax", pPackage.NonRepaymentPenaltiesMax.OverDueInterest);
            c.AddParam("@nonRepaymentPenaltiesOverduePrincipalMax", pPackage.NonRepaymentPenaltiesMax.OverDuePrincipal);
            
            if (pPackage.UseLoanCycle)
                c.AddParam("@cycleId", pPackage.CycleId);
            else
                c.AddParam("@cycleId", null);

            if (pPackage.ExoticProduct == null)
                c.AddParam("@exoticId", null);
            else
                c.AddParam("@exoticId", pPackage.ExoticProduct.Id);
            
            /* Line of credit */
            c.AddParam("@DrawingsNumber", pPackage.DrawingsNumber);

            c.AddParam("@AmountUnderLoc", pPackage.AmountUnderLoc);
            c.AddParam("@AmountUnderLocMin", pPackage.AmountUnderLocMin);
            c.AddParam("@AmountUnderLocMax", pPackage.AmountUnderLocMax);

            c.AddParam("@MaturityLoc", pPackage.MaturityLoc);
            c.AddParam("@MaturityLocMin", pPackage.MaturityLocMin);
            c.AddParam("@MaturityLocMax", pPackage.MaturityLocMax);
            c.AddParam("@activated_loc", pPackage.ActivatedLOC);

            c.AddParam("@allow_flexible_schedule", pPackage.AllowFlexibleSchedule);

            /* Some coolish Guarantors and Collaterals */
            c.AddParam("@use_guarantor_collateral", pPackage.UseGuarantorCollateral);
            c.AddParam("@set_separate_guarantor_collateral", pPackage.SetSeparateGuarantorCollateral);
            c.AddParam("@percentage_total_guarantor_collateral", pPackage.PercentageTotalGuarantorCollateral);
            c.AddParam("@percentage_separate_guarantor", pPackage.PercentageSeparateGuarantour);
            c.AddParam("@percentage_separate_collateral", pPackage.PercentageSeparateCollateral);
            
            // Some cool stuff for compulsory savings
            c.AddParam("@use_compulsory_savings", pPackage.UseCompulsorySavings);
            c.AddParam("@compulsory_amount", pPackage.CompulsoryAmount);
            c.AddParam("@compulsory_amount_min", pPackage.CompulsoryAmountMin);
            c.AddParam("@compulsory_amount_max", pPackage.CompulsoryAmountMax);
            //Insurance values
            c.AddParam("@insurance_min", pPackage.CreditInsuranceMin);
            c.AddParam("@insurance_max", pPackage.CreditInsuranceMax);
            
            c.AddParam("@use_entry_fees_cycles", pPackage.UseEntryFeesCycles);
        }

        private void FireProductLoaded(LoanProduct product)
        {
            if (null == ProductLoaded) return;
            ProductLoaded(product);
        }

        /// <summary>
        /// Method to add a package into database. We use the NullableTypes to make the correspondance between
        /// nullable int, decimal and double types in database and our own objects
        /// </summary>
        /// <param name="pPackage">Package Object</param>
        /// <returns>The id of the package which has been added</returns>
        public int Add(LoanProduct pPackage)
        {
            int identity;
            const string q = @"INSERT INTO [Packages]
               ([deleted]
                ,[name]
                ,[code]
                ,[client_type]
                ,[amount]
                ,[amount_min]
                ,[amount_max]
                ,[interest_rate]
                ,[interest_rate_min]
                ,[interest_rate_max]
                ,[installment_type]
                ,[grace_period]
                ,[grace_period_min]
                ,[grace_period_max]
                ,[number_of_installments]
                ,[number_of_installments_min]
                ,[number_of_installments_max]
                ,[anticipated_total_repayment_penalties]
                ,[anticipated_total_repayment_penalties_min]
                ,[anticipated_total_repayment_penalties_max]
                ,[anticipated_partial_repayment_penalties]
                ,[anticipated_partial_repayment_penalties_min]
                ,[anticipated_partial_repayment_penalties_max]
                ,[exotic_id]
                ,[loan_type]
                ,[keep_expected_installment]
                ,[charge_interest_within_grace_period]
                ,[anticipated_total_repayment_base]
                ,[anticipated_partial_repayment_base]
                ,[cycle_id]
                ,[non_repayment_penalties_based_on_overdue_interest]
                ,[non_repayment_penalties_based_on_initial_amount]
                ,[non_repayment_penalties_based_on_olb]
                ,[non_repayment_penalties_based_on_overdue_principal]
                ,[non_repayment_penalties_based_on_overdue_interest_min]
                ,[non_repayment_penalties_based_on_initial_amount_min]
                ,[non_repayment_penalties_based_on_olb_min]
                ,[non_repayment_penalties_based_on_overdue_principal_min]
                ,[non_repayment_penalties_based_on_overdue_interest_max]
                ,[non_repayment_penalties_based_on_initial_amount_max]
                ,[non_repayment_penalties_based_on_olb_max]
                ,[non_repayment_penalties_based_on_overdue_principal_max]
                ,[fundingLine_id]                
                ,[currency_id]
                ,[rounding_type]
                ,[grace_period_of_latefees]
                ,[number_of_drawings_loc]
                ,[amount_under_loc]
                ,[amount_under_loc_min]
                ,[amount_under_loc_max]
                ,[maturity_loc]
                ,[maturity_loc_min]
                ,[maturity_loc_max]
                ,[activated_loc]
                ,[allow_flexible_schedule]
                ,[use_guarantor_collateral]
	            ,[set_separate_guarantor_collateral]
	            ,[percentage_total_guarantor_collateral]
	            ,[percentage_separate_guarantor]
	            ,[percentage_separate_collateral]
                ,[use_compulsory_savings]
	            ,[compulsory_amount]
	            ,[compulsory_amount_min]
	            ,[compulsory_amount_max]
                ,[insurance_min]
                ,[insurance_max]
                ,[use_entry_fees_cycles])
                VALUES
                (@deleted
                ,@name
                ,@code
                ,@clientType
                ,@amount
                ,@amountMin
                ,@amountMax
                ,@interestRate
                ,@interestRateMin
                ,@interestRateMax
                ,@installmentTypeId
                ,@gracePeriod
                ,@gracePeriodMin
                ,@gracePeriodMax
                ,@nbOfInstallments
                ,@nbOfInstallmentsMin
                ,@nbOfInstallmentsMax
                ,@anticipatedTotalRepaymentPenalties
                ,@anticipatedTotalRepaymentPenaltiesMin
                ,@anticipatedTotalRepaymentPenaltiesMax
                ,@anticipatedPartialRepaymentPenalties
                ,@anticipatedPartialRepaymentPenaltiesMin
                ,@anticipatedPartialRepaymentPenaltiesMax                
                ,@exoticId
                ,@loanType
                ,@keepExpectedInstallment
                ,@chargeInterestWithInGracePeriod
                ,@AnticipatedTotalRepaymentPenaltiesBase
                ,@AnticipatedPartialRepaymentPenaltiesBase
                ,@cycleId
                ,@nonRepaymentPenaltiesOverdueInterest
                ,@nonRepaymentPenaltiesInitialAmount
                ,@nonRepaymentPenaltiesOlb
                ,@nonRepaymentPenaltiesOverduePrincipal
                ,@nonRepaymentPenaltiesOverdueInterestMin
                ,@nonRepaymentPenaltiesInitialAmountMin
                ,@nonRepaymentPenaltiesOlbMin
                ,@nonRepaymentPenaltiesOverduePrincipalMin
                ,@nonRepaymentPenaltiesOverdueInterestMax
                ,@nonRepaymentPenaltiesInitialAmountMax
                ,@nonRepaymentPenaltiesOlbMax
                ,@nonRepaymentPenaltiesOverduePrincipalMax
                ,@fundingLine_id                
                ,@currency_id
                ,@rounding_type
                ,@grace_period_of_latefees
                ,@DrawingsNumber
                ,@AmountUnderLoc
                ,@AmountUnderLocMin
                ,@AmountUnderLocMax
                ,@MaturityLoc
                ,@MaturityLocMin
                ,@MaturityLocMax
                ,@activated_loc
                ,@allow_flexible_schedule
                ,@use_guarantor_collateral
	            ,@set_separate_guarantor_collateral
	            ,@percentage_total_guarantor_collateral
	            ,@percentage_separate_guarantor
	            ,@percentage_separate_collateral
                ,@use_compulsory_savings
	            ,@compulsory_amount
	            ,@compulsory_amount_min
	            ,@compulsory_amount_max
                ,@insurance_min
                ,@insurance_max
                ,@use_entry_fees_cycles)
                SELECT SCOPE_IDENTITY()";

            using (SqlConnection conn = GetConnection())
            using (OctopusCommand insertPackage = new OctopusCommand(q, conn))
            {
                SetProduct(insertPackage, pPackage);
                identity= int.Parse(insertPackage.ExecuteScalar().ToString());
            }

            foreach (ProductClientType clientType in pPackage.ProductClientTypes)
            {
                if (clientType.IsChecked)
                {
                    string sqlTextForClientTypes = string.Format(@"INSERT INTO PackagesClientTypes 
                                                         ([client_type_id], [package_id])
                                                          VALUES({0}, {1})", 
                                                            clientType.TypeId, identity);
                    using (SqlConnection conn = GetConnection())
                    using (OctopusCommand c = new OctopusCommand(sqlTextForClientTypes, conn))
                    {
                        c.ExecuteNonQuery();
                    }
                }
            }
            return identity;
        }

        public void UpdatePackage(LoanProduct pPackage, bool pUpdateContracts)
        {
            string q = @"UPDATE [Packages] 
                SET [deleted] = @deleted
                ,[name] = @name
                ,[code] = @code
                ,[client_type] = @clientType
                ,[amount] = @amount
                ,[amount_min] = @amountMin
                ,[amount_max] = @amountMax
                ,[interest_rate] = @interestRate
                ,[interest_rate_min] = @interestRateMin
                ,[interest_rate_max] = @interestRateMax
                ,[installment_type] = @installmentTypeId
                ,[grace_period] = @gracePeriod
                ,[grace_period_min] = @gracePeriodMin
                ,[grace_period_max] = @gracePeriodMax
                ,[number_of_installments] = @nbOfInstallments
                ,[number_of_installments_min] = @nbOfInstallmentsMin
                ,[number_of_installments_max] = @nbOfInstallmentsMax
                ,[anticipated_total_repayment_penalties] = @anticipatedTotalRepaymentPenalties
                ,[anticipated_total_repayment_penalties_min] = @anticipatedTotalRepaymentPenaltiesMin
                ,[anticipated_total_repayment_penalties_max] = @anticipatedTotalRepaymentPenaltiesMax
                ,[anticipated_partial_repayment_penalties] = @anticipatedPartialRepaymentPenalties
                ,[anticipated_partial_repayment_penalties_min] = @anticipatedPartialRepaymentPenaltiesMin
                ,[anticipated_partial_repayment_penalties_max] = @anticipatedPartialRepaymentPenaltiesMax
                ,[exotic_id] = @exoticId
                ,[loan_type] = @loanType
                ,[keep_expected_installment] = @keepExpectedInstallment
                ,[charge_interest_within_grace_period] = @chargeInterestWithInGracePeriod
                ,[anticipated_total_repayment_base] = @AnticipatedTotalRepaymentPenaltiesBase
                ,[anticipated_partial_repayment_base] = @AnticipatedPartialRepaymentPenaltiesBase  
                ,[cycle_id] = @cycleId
                ,[non_repayment_penalties_based_on_overdue_interest] = @nonRepaymentPenaltiesOverdueInterest
                ,[non_repayment_penalties_based_on_initial_amount] = @nonRepaymentPenaltiesInitialAmount
                ,[non_repayment_penalties_based_on_olb] = @nonRepaymentPenaltiesOlb
                ,[non_repayment_penalties_based_on_overdue_principal] = @nonRepaymentPenaltiesOverduePrincipal
                ,[non_repayment_penalties_based_on_overdue_interest_min] = @nonRepaymentPenaltiesOverdueInterestMin
                ,[non_repayment_penalties_based_on_initial_amount_min] = @nonRepaymentPenaltiesInitialAmountMin
                ,[non_repayment_penalties_based_on_olb_min] = @nonRepaymentPenaltiesOlbMin
                ,[non_repayment_penalties_based_on_overdue_principal_min] = @nonRepaymentPenaltiesOverduePrincipalMin
                ,[non_repayment_penalties_based_on_overdue_interest_max] = @nonRepaymentPenaltiesOverdueInterestMax
                ,[non_repayment_penalties_based_on_initial_amount_max] = @nonRepaymentPenaltiesInitialAmountMax
                ,[non_repayment_penalties_based_on_olb_max] = @nonRepaymentPenaltiesOlbMax
                ,[non_repayment_penalties_based_on_overdue_principal_max] = @nonRepaymentPenaltiesOverduePrincipalMax
                ,[rounding_type] = @rounding_type
                ,[grace_period_of_latefees] = @grace_period_of_latefees
                ,[fundingLine_id] = @fundingLine_id
                ,[number_of_drawings_loc] = @DrawingsNumber
                ,[amount_under_loc] = @AmountUnderLoc
                ,[amount_under_loc_min] = @AmountUnderLocMin
                ,[amount_under_loc_max] = @AmountUnderLocMax
                ,[maturity_loc] = @MaturityLoc
                ,[maturity_loc_min] = @MaturityLocMin
                ,[maturity_loc_max] = @MaturityLocMax
                ,[activated_loc] = @activated_loc
                ,[allow_flexible_schedule] = @allow_flexible_schedule
                ,[use_guarantor_collateral] = @use_guarantor_collateral
                ,[set_separate_guarantor_collateral] = @set_separate_guarantor_collateral
                ,[percentage_total_guarantor_collateral] = @percentage_total_guarantor_collateral
                ,[percentage_separate_guarantor] = @percentage_separate_guarantor
                ,[percentage_separate_collateral] = @percentage_separate_collateral
                ,[use_compulsory_savings] = @use_compulsory_savings
                ,[compulsory_amount] = @compulsory_amount
                ,[compulsory_amount_min] = @compulsory_amount_min
                ,[compulsory_amount_max] = @compulsory_amount_max
                ,[currency_id] = @currency_id 
                ,[insurance_min] = @insurance_min
                ,[insurance_max] = @insurance_max  
                ,[use_entry_fees_cycles] = @use_entry_fees_cycles             
                WHERE id = @packageId";

            using (SqlConnection conn = GetConnection())
            using (OctopusCommand updatePackage = new OctopusCommand(q, conn))
            {
                SetProduct(updatePackage, pPackage);
                updatePackage.ExecuteNonQuery();
            }

            q = string.Format(@"DELETE FROM PackagesClientTypes WHERE package_id={0}", pPackage.Id);
            using (SqlConnection conn = GetConnection())
            using (OctopusCommand c = new OctopusCommand(q, conn))
            c.ExecuteNonQuery();

            
            foreach (ProductClientType clientType in pPackage.ProductClientTypes)
            {
                if (clientType.IsChecked)
                {
                    q = string.Format(@"INSERT INTO PackagesClientTypes ([client_type_id], [package_id])
                                     VALUES({0}, {1})", clientType.TypeId, pPackage.Id);
                    using (SqlConnection conn = GetConnection())
                    using (OctopusCommand c = new OctopusCommand(q, conn))
                    {
                        c.ExecuteNonQuery();
                    }
                }
            }

            if (!pUpdateContracts) return;

            q = @"UPDATE Credit 
                       SET anticipated_total_repayment_penalties = ISNULL(@anticipated_total_repayment_penalties, 0), 
                       anticipated_partial_repayment_penalties = ISNULL(@anticipated_partial_repayment_penalties, 0),   
                       non_repayment_penalties_based_on_overdue_principal = ISNULL(@non_repayment_penalties_based_on_overdue_principal, 0),
                       non_repayment_penalties_based_on_initial_amount = ISNULL(@non_repayment_penalties_based_on_initial_amount, 0),
                       non_repayment_penalties_based_on_olb = ISNULL(@non_repayment_penalties_based_on_olb, 0),
                       non_repayment_penalties_based_on_overdue_interest = ISNULL(@non_repayment_penalties_based_on_overdue_interest, 0),
                       [grace_period_of_latefees] = ISNULL(@grace_period_of_latefees, 0),
                       [number_of_drawings_loc] = ISNULL(@number_of_drawings_loc, 0), 
                       [amount_under_loc] = ISNULL(@amount_under_loc, 0),
                       [maturity_loc] = ISNULL(@maturity_loc, 0),
                       [anticipated_partial_repayment_base] = ISNULL(@AnticipatedPartialRepaymentPenaltiesBase, 0),
                       [anticipated_total_repayment_base] = ISNULL(@AnticipatedTotalRepaymentPenaltiesBase, 0)
                       WHERE package_id = @packageId";

            using (SqlConnection conn = GetConnection())
            using (OctopusCommand updatePackage = new OctopusCommand(q, conn))
            {
                updatePackage.AddParam("@anticipated_total_repayment_penalties", pPackage.AnticipatedTotalRepaymentPenalties??pPackage.AnticipatedTotalRepaymentPenaltiesMin);
                updatePackage.AddParam("@anticipated_partial_repayment_penalties", pPackage.AnticipatedPartialRepaymentPenalties??pPackage.AnticipatedPartialRepaymentPenaltiesMin);
                
                updatePackage.AddParam("@non_repayment_penalties_based_on_overdue_principal", pPackage.NonRepaymentPenalties.OverDuePrincipal??pPackage.NonRepaymentPenaltiesMin.OverDuePrincipal);
                updatePackage.AddParam("@non_repayment_penalties_based_on_initial_amount", pPackage.NonRepaymentPenalties.InitialAmount??pPackage.NonRepaymentPenaltiesMin.InitialAmount);
                updatePackage.AddParam("@non_repayment_penalties_based_on_olb", pPackage.NonRepaymentPenalties.OLB ?? pPackage.NonRepaymentPenaltiesMin.OLB);
                updatePackage.AddParam("@non_repayment_penalties_based_on_overdue_interest", pPackage.NonRepaymentPenalties.OverDueInterest ?? pPackage.NonRepaymentPenaltiesMin.OverDueInterest);
                updatePackage.AddParam("@grace_period_of_latefees",  pPackage.GracePeriodOfLateFees);

                updatePackage.AddParam("@number_of_drawings_loc", pPackage.DrawingsNumber);
                updatePackage.AddParam("@amount_under_loc", pPackage.AmountUnderLoc);
                updatePackage.AddParam("@maturity_loc", pPackage.MaturityLoc);

                updatePackage.AddParam("@AnticipatedTotalRepaymentPenaltiesBase", (int)pPackage.AnticipatedTotalRepaymentPenaltiesBase);
                updatePackage.AddParam("@AnticipatedPartialRepaymentPenaltiesBase", (int)pPackage.AnticipatedPartialRepaymentPenaltiesBase);

                updatePackage.AddParam("@packageId", pPackage.Id);

                updatePackage.ExecuteNonQuery();
            }
        }

        public void DeleteEntryFees(LoanProduct product)
        {
            string q = @"UPDATE EntryFees
                         SET is_deleted = 1
                         WHERE id_product=@id";
            if (product.DeletedEntryFees== null) return;
            using (SqlConnection conn = GetConnection())
            using (OctopusCommand c = new OctopusCommand(q, conn))
            {
                c.AddParam("@id", product.Id);
                c.ExecuteNonQuery();
            }
        }

        public void InsertEntryFees(List<EntryFee> entryFees, int productId)
        {
            if (entryFees == null) return;
            string q = @"INSERT INTO EntryFees 
                                (id_product, 
                                name_of_fee, 
                                min, max, 
                                value, 
                                rate, 
                                fee_index, 
                                cycle_id)
                              VALUES (@id_product, @name_of_fee, @min, @max, @value, @rate, @fee_index, @cycle_id)";
            foreach (var entryFee in entryFees)
            {
                using (SqlConnection conn = GetConnection())
                using (OctopusCommand c = new OctopusCommand(q, conn))
                {
                    c.AddParam("@id_product", productId);
                    c.AddParam("@name_of_fee", entryFee.Name);
                    c.AddParam("@min", entryFee.Min);
                    c.AddParam("@max", entryFee.Max);
                    c.AddParam("@value", entryFee.Value);
                    c.AddParam("rate", entryFee.IsRate);
                    c.AddParam("@fee_index", entryFee.Index);
                    c.AddParam("@cycle_id", entryFee.CycleId);
                    c.ExecuteNonQuery();
                }
            }
        }


        public List<int> SelectEntryFeeCycles(int productId, bool withDeletedCycles)
        {
            var entryFeeCycles = new List<int>();
            string q =
                @"  SELECT DISTINCT cycle_id
                    FROM [dbo].[EntryFees]
                    WHERE [id_product]=@package_id AND cycle_id IS NOT NULL";
                if (!withDeletedCycles)
                q += " AND [is_deleted]<>1";
                 q+= " ORDER BY cycle_id";
            using (SqlConnection conn = GetConnection())
            using (OctopusCommand c = new OctopusCommand(q, conn))
            {
                c.AddParam("@package_id", productId);
                using (OctopusReader r = c.ExecuteReader())
                {
                    if (r!=null && !r.Empty)
                        while (r.Read())
                            entryFeeCycles.Add(r.GetInt("cycle_id"));
                }
            }
            return entryFeeCycles;
        }


        public List<LoanCycle> SelectLoanCycles()
        {
            List<LoanCycle> loanCycles = new List<LoanCycle>();
            string q = @"SELECT [id], [name]  FROM [dbo].[Cycles]";
            using (SqlConnection conn = GetConnection())
            using (OctopusCommand c = new OctopusCommand(q, conn))
            using (OctopusReader r = c.ExecuteReader())
            {
                while (r.Read())
                {
                    LoanCycle loanCycle = new LoanCycle();
                    loanCycle.Id = r.GetInt("id");
                    loanCycle.Name = r.GetString("name");
                    loanCycles.Add(loanCycle);
                }
            }
        
            return loanCycles;
        }

        public List<MaturityCycle> SelectMaturityCycleParams(int cycleId)
        {
            List<MaturityCycle> cycleParameters = new List<MaturityCycle>();
            string q = @"SELECT [id]
                                  ,[loan_cycle]
                                  ,[min]
                                  ,[max]
                                  ,[cycle_object_id]
                                  ,[cycle_id]
                              FROM [dbo].[CycleParameters] 
                              WHERE [cycle_object_id]=3 
                                    AND [cycle_id]=@cycle_id
                              ORDER BY [loan_cycle]";
            using (SqlConnection conn = GetConnection())
            using (OctopusCommand c = new OctopusCommand(q, conn))
            {
                c.AddParam("@cycle_id", cycleId);

                using (OctopusReader r = c.ExecuteReader())
                {
                    while (r.Read())
                    {
                        MaturityCycle parameter = new MaturityCycle();
                        parameter.Id = r.GetInt("id");
                        parameter.LoanCycle = r.GetInt("loan_cycle");
                        parameter.Min = r.GetDecimal("min");
                        parameter.Max = r.GetDecimal("max");
                        parameter.CycleObjectId = r.GetInt("cycle_object_id");
                        parameter.CycleId = r.GetNullInt("cycle_id");
                        cycleParameters.Add(parameter);
                    }
                }
            }
            return cycleParameters; 
        }

        public List<RateCycle> SelectRateCycleParams(int cycleId)
        {
            List<RateCycle> cycleParameters = new List<RateCycle>();
            string q = @"SELECT [id]
                                  ,[loan_cycle]
                                  ,[min]
                                  ,[max]
                                  ,[cycle_object_id]
                                  ,[cycle_id]
                              FROM [dbo].[CycleParameters] 
                              WHERE [cycle_object_id]=2 
                                    AND [cycle_id]=@cycle_id
                              ORDER BY [loan_cycle]";
            using (SqlConnection conn = GetConnection())
            using (OctopusCommand c = new OctopusCommand(q, conn))
            {
                c.AddParam("@cycle_id", cycleId);

                using (OctopusReader r = c.ExecuteReader())
                {
                    while (r.Read())
                    {
                        RateCycle parameter = new RateCycle();
                        parameter.Id = r.GetInt("id");
                        parameter.LoanCycle = r.GetInt("loan_cycle");
                        parameter.Min = r.GetDecimal("min");
                        parameter.Max = r.GetDecimal("max");
                        parameter.CycleObjectId = r.GetInt("cycle_object_id");
                        parameter.CycleId = r.GetNullInt("cycle_id");
                        cycleParameters.Add(parameter);
                    }
                }
            }
            return cycleParameters;
        }

        public List<LoanAmountCycle> SelectLoanAmountCycleParams(int cycleId)
        {
            List<LoanAmountCycle> cycleParameters = new List<LoanAmountCycle>();
            string q = @"SELECT [id]
                                  ,[loan_cycle]
                                  ,[min]
                                  ,[max]
                                  ,[cycle_object_id]
                                  ,[cycle_id]
                              FROM [dbo].[CycleParameters] 
                              WHERE [cycle_object_id]=1 
                                    AND [cycle_id]=@cycle_id
                              ORDER BY [loan_cycle]";
            using (SqlConnection conn = GetConnection())
            using (OctopusCommand c = new OctopusCommand(q, conn))
            {
                c.AddParam("@cycle_id", cycleId);

                using (OctopusReader r = c.ExecuteReader())
                {
                    while (r.Read())
                    {
                        LoanAmountCycle parameter = new LoanAmountCycle();
                        parameter.Id = r.GetInt("id");
                        parameter.LoanCycle = r.GetInt("loan_cycle");
                        parameter.Min = r.GetDecimal("min");
                        parameter.Max = r.GetDecimal("max");
                        parameter.CycleObjectId = r.GetInt("cycle_object_id");
                        parameter.CycleId = r.GetNullInt("cycle_id");
                        cycleParameters.Add(parameter);
                    }
                }
            }
            return cycleParameters;
        }

        public void DeleteCycles(int objectId, int cycleId)
        {
            string q =
                @"DELETE FROM [dbo].[CycleParameters]
                              WHERE [cycle_object_id]=@cycle_object_id 
                              AND [cycle_id]=@cycle_id";
            using (SqlConnection conn = GetConnection())
            using (OctopusCommand c = new OctopusCommand(q, conn))
            {
                c.AddParam("@cycle_object_id", objectId);
                c.AddParam("@cycle_id", cycleId);
                c.ExecuteNonQuery();
            }
        }

        public void InsertLoanAmountCycleParams(List<LoanAmountCycle> loanAmountCycles)
        {
            string q =
                @"INSERT INTO [dbo].[CycleParameters]
                            ([loan_cycle]
                            ,[min]
                            ,[max]
                            ,[cycle_object_id]
                            ,[cycle_id])
                    VALUES
                           (@loan_cycle
                           ,@min
                           ,@max
                           ,@cycle_object_id
                           ,@cycle_id)";
            foreach (var amountCycle in loanAmountCycles)
            {
                using (SqlConnection conn = GetConnection())
                using (OctopusCommand c = new OctopusCommand(q, conn))
                {
                    SetCommand(c, amountCycle);
                    c.ExecuteNonQuery();
                }
            }
        }

        private void SetCommand(OctopusCommand c, Cycle cycle)
        {
            c.AddParam("@loan_cycle", cycle.LoanCycle);
            c.AddParam("@min", cycle.Min.Value);
            c.AddParam("@max", cycle.Max.Value);
            c.AddParam("@cycle_object_id", cycle.CycleObjectId);
            c.AddParam("@cycle_id", cycle.CycleId);
        }

        public void InsertRateCycleParams(List<RateCycle> rateCycles)
        {
            string q =
                @"INSERT INTO [dbo].[CycleParameters]
                            ([loan_cycle]
                            ,[min]
                            ,[max]
                            ,[cycle_object_id]
                            ,[cycle_id])
                    VALUES
                           (@loan_cycle
                           ,@min
                           ,@max
                           ,@cycle_object_id
                           ,@cycle_id)";
            foreach (var rateCycle in rateCycles)
            {
                using (SqlConnection conn = GetConnection())
                using (OctopusCommand c = new OctopusCommand(q, conn))
                {
                    SetCommand(c, rateCycle);
                    c.ExecuteNonQuery();
                }
            }
        }

        public void InsertMaturityCycleParams(List<MaturityCycle> maturityCycles)
        {
            string q =
                @"INSERT INTO [dbo].[CycleParameters]
                            ([loan_cycle]
                            ,[min]
                            ,[max]
                            ,[cycle_object_id]
                            ,[cycle_id])
                    VALUES
                           (@loan_cycle
                           ,@min
                           ,@max
                           ,@cycle_object_id
                           ,@cycle_id)";
            foreach (var maturityCycle in maturityCycles)
            {
                using (SqlConnection conn = GetConnection())
                using (OctopusCommand c = new OctopusCommand(q, conn))
                {
                    SetCommand(c, maturityCycle);
                    c.ExecuteNonQuery();
                }
            }
        }

        public List<CycleObject> SelectCycleObjects()
        {
            string q = @"SELECT [id], [name]
                               FROM [dbo].[CycleObjects]
                               ORDER BY [id]";

            List<CycleObject> cycleObjects = new List<CycleObject>();
            using (SqlConnection conn = GetConnection())
            using (OctopusCommand c = new OctopusCommand(q, conn))
            {
                using (OctopusReader r = c.ExecuteReader())
                {
                    while (r.Read())
                    {
                        CycleObject cycleObject = new CycleObject(r.GetInt("id"), (r.GetString("name")).Replace(" ",""));
                        cycleObjects.Add(cycleObject);
                    }
                }
            }
            return cycleObjects;
        }

        public List<EntryFee> SelectEntryFeesWithCycles(int productId, bool withDeletedFees)
        {
            List<EntryFee> entryFees = new List<EntryFee>();
            string q =
                            @"SELECT [id]
                             ,[name_of_fee]
                             ,[min]
                             ,[max]
                             ,[value]
                             ,[rate]
                             ,[fee_index]
                             ,[cycle_id]
                            FROM [dbo].[EntryFees]
                            WHERE id_product=@id_product AND cycle_id IS NOT NULL";
            if (!withDeletedFees)
                q += " AND [is_deleted]<>1";
            q += @" ORDER BY [fee_index]";
            using (SqlConnection conn = GetConnection())
            using (OctopusCommand c = new OctopusCommand(q, conn))
            {
                c.AddParam("@id_product", productId);
                using (OctopusReader r = c.ExecuteReader())
                {
                    while (r.Read())
                    {
                        entryFees.Add(GetEntryFee(r));
                    }
                }
            }
            return entryFees;
        }

        public int SelectSuitableEntryFeeCycle(int productId, int clientCycle)
        {
            string q =
                @"SELECT ISNULL(MAX(cycle_id), 0) AS cycle
                FROM [dbo].[EntryFees]
                WHERE [id_product]=@product_id AND cycle_id<=@client_cycle AND cycle_id IS NOT NULL";
            using (SqlConnection conn = GetConnection())
            using (OctopusCommand c = new OctopusCommand(q, conn))
            {
                c.AddParam("@product_id", productId);
                c.AddParam("@client_cycle", clientCycle);
                return Convert.ToInt32(c.ExecuteScalar());
            }
        }

        public List<EntryFee> SelectEntryFeesAccordingCycle(int productId, int cycle, bool withDeletedFees)
        {
            List<EntryFee> entryFees = new List<EntryFee>();
            string q =
                            @"SELECT [id]
                             ,[name_of_fee]
                             ,[min]
                             ,[max]
                             ,[value]
                             ,[rate]
                             ,[fee_index]
                             ,[cycle_id]
                            FROM [dbo].[EntryFees]
                            WHERE id_product=@id_product AND cycle_id=@cycle_id";
            if (!withDeletedFees)
                q += " AND [is_deleted]<>1";
            q += @" ORDER BY [fee_index]";
            using (SqlConnection conn = GetConnection())
            using (OctopusCommand c = new OctopusCommand(q, conn))
            {
                c.AddParam("@id_product", productId);
                c.AddParam("@cycle_id", cycle);
                using (OctopusReader r = c.ExecuteReader())
                {
                   if (r!= null && !r.Empty)
                    while (r.Read())
                    {
                        entryFees.Add(GetEntryFee(r));
                    }
                }
            }
            return entryFees;
        }

        public List<EntryFee> SelectEntryFeesWithoutCycles(int productId, bool withDeletedFees)
        {
            List<EntryFee> entryFees=new List<EntryFee>();
            string q =
                            @"SELECT [id]
                             ,[name_of_fee]
                             ,[min]
                             ,[max]
                             ,[value]
                             ,[rate]
                             ,[fee_index]
                             ,[cycle_id]
                            FROM [dbo].[EntryFees]
                            WHERE id_product=@id_product AND cycle_id IS NULL";
            if (!withDeletedFees)
                q += " AND [is_deleted]<>1";
            q += @" ORDER BY [fee_index]";
            using (SqlConnection conn = GetConnection())
            using (OctopusCommand c = new OctopusCommand(q, conn))
            {
                c.AddParam("@id_product", productId);
                using (OctopusReader r = c.ExecuteReader())
                {
                    if (r!=null && !r.Empty)
                    while (r.Read())
                    {
                        entryFees.Add(GetEntryFee(r));
                    }
                }
            }
            return entryFees;
        }

        private static EntryFee GetEntryFee(OctopusReader r)
        {
            EntryFee entryFee = new EntryFee
                               {
                                   Id = r.GetInt("id"),
                                   Name = r.GetString("name_of_fee"),
                                   Min = r.GetNullDecimal("min"),
                                   Max = r.GetNullDecimal("max"),
                                   Value = r.GetNullDecimal("value"),
                                   IsRate = r.GetNullBool("rate") ?? false,
                                   Index = r.GetInt("fee_index"),
                                   CycleId = r.GetNullInt("cycle_id")
                               };
            return entryFee;
        }

        public EntryFee SelectEntryFeeById(int entryFeeId)
        {
            EntryFee entryFee = new EntryFee();
            string q =
                            @"SELECT [id]
                             ,[name_of_fee]
                             ,[min]
                             ,[max]
                             ,[value]
                             ,[rate]
                             ,[fee_index]
                             ,[cycle_id]
                            FROM [dbo].[EntryFees]
                            WHERE [id]=@entry_fee_id";
            using (SqlConnection conn = GetConnection())
            using (OctopusCommand c = new OctopusCommand(q, conn))
            {
                c.AddParam("@entry_fee_id", entryFeeId);
                using (OctopusReader r = c.ExecuteReader())
                {
                    if (r!= null && !r.Empty)
                    while (r.Read())
                    {
                        entryFee = GetEntryFee(r);
                    }
                }
            }
            return entryFee;
        }

        private bool _IsThisNameAreadyExist(string q,string pExpectedName)
        {
            using (SqlConnection conn = GetConnection())
            using (OctopusCommand c = new OctopusCommand(q, conn))
            {
                c.AddParam("@name", pExpectedName);
                using (OctopusReader r = c.ExecuteReader())
                {
                    if (r == null || r.Empty) return false;
                    return true;
                }
            }
        }

        /// <summary>
        /// Look if selected product name already exist in database
        /// </summary>
        /// <param name="pPackageName"></param>
        /// <returns>true or false</returns>
        public bool IsThisProductNameAlreadyExist(string pPackageName)
        {
            const string sqlText = "SELECT name FROM Packages WHERE name = @name";
            return _IsThisNameAreadyExist(sqlText, pPackageName);
        }

        /// <summary>
        /// Look if selected Exotic Product name already exist in database
        /// </summary>
        /// <param name="pExoticName"></param>
        /// <returns></returns>
        public bool IsThisExoticProductNameAlreadyExist(string pExoticName)
        {
            const string sqlText = "SELECT name FROM Exotics WHERE name = @name";
            return _IsThisNameAreadyExist(sqlText, pExoticName);
        }

        /// <summary>
        /// Look if selected Amount cycle stock name already exist in database
        /// </summary>
        /// <param name="pAmountCycleStockName"></param>
        /// <returns></returns>
        public bool IsLoanCycleNameAlreadyExist(string pAmountCycleStockName)
        {
            const string sqlText = "SELECT name FROM Cycles WHERE name = @name";
            return _IsThisNameAreadyExist(sqlText, pAmountCycleStockName);
        }

        private static LoanProduct GetProduct(OctopusReader r)
        {
            LoanProduct package = new LoanProduct();
            package.Id = r.GetInt("id");
            package.Delete = r.GetBool("deleted");
            package.Name = r.GetString("name");
            package.Code = r.GetString("code");
            package.ClientType = r.GetChar("client_type");
            package.LoanType = (OLoanTypes)r.GetSmallInt("loan_type");
            package.RoundingType = (ORoundingType)r.GetSmallInt("rounding_type");
            package.Amount = r.GetMoney("amount");
            package.AmountMin = r.GetMoney("amount_min");
            package.AmountMax = r.GetMoney("amount_max");
            package.InterestRate = r.GetNullDecimal("interest_rate");
            package.InterestRateMin = r.GetNullDecimal("interest_rate_min");
            package.InterestRateMax = r.GetNullDecimal("interest_rate_max");
            package.GracePeriod = r.GetNullInt("grace_period");
            package.GracePeriodMin = r.GetNullInt("grace_period_min");
            package.GracePeriodMax = r.GetNullInt("grace_period_max");
            package.GracePeriodOfLateFees = r.GetNullInt("grace_period_of_latefees");
            package.NbOfInstallments = r.GetNullInt("number_of_installments");
            package.NbOfInstallmentsMin = r.GetNullInt("number_of_installments_min");
            package.NbOfInstallmentsMax = r.GetNullInt("number_of_installments_max");

            package.AnticipatedTotalRepaymentPenalties = r.GetNullDouble("anticipated_total_repayment_penalties");
            package.AnticipatedTotalRepaymentPenaltiesMin = r.GetNullDouble("anticipated_total_repayment_penalties_min");
            package.AnticipatedTotalRepaymentPenaltiesMax = r.GetNullDouble("anticipated_total_repayment_penalties_max");

            package.AnticipatedPartialRepaymentPenalties = r.GetNullDouble("anticipated_partial_repayment_penalties");
            package.AnticipatedPartialRepaymentPenaltiesMin = r.GetNullDouble("anticipated_partial_repayment_penalties_min");
            package.AnticipatedPartialRepaymentPenaltiesMax = r.GetNullDouble("anticipated_partial_repayment_penalties_max");
            
            package.ChargeInterestWithinGracePeriod = r.GetBool("charge_interest_within_grace_period");
            package.KeepExpectedInstallment = r.GetBool("keep_expected_installment");
            
            package.AnticipatedTotalRepaymentPenaltiesBase = (OAnticipatedRepaymentPenaltiesBases)r.GetSmallInt("anticipated_total_repayment_base");
            package.AnticipatedPartialRepaymentPenaltiesBase = (OAnticipatedRepaymentPenaltiesBases)r.GetSmallInt("anticipated_partial_repayment_base");

            package.NonRepaymentPenalties.InitialAmount = r.GetNullDouble("non_repayment_penalties_based_on_initial_amount");
            package.NonRepaymentPenalties.OLB = r.GetNullDouble("non_repayment_penalties_based_on_olb");
            package.NonRepaymentPenalties.OverDueInterest = r.GetNullDouble("non_repayment_penalties_based_on_overdue_interest");
            package.NonRepaymentPenalties.OverDuePrincipal = r.GetNullDouble("non_repayment_penalties_based_on_overdue_principal");

            package.NonRepaymentPenaltiesMin.InitialAmount = r.GetNullDouble("non_repayment_penalties_based_on_initial_amount_min");
            package.NonRepaymentPenaltiesMin.OLB = r.GetNullDouble("non_repayment_penalties_based_on_olb_min");
            package.NonRepaymentPenaltiesMin.OverDuePrincipal = r.GetNullDouble("non_repayment_penalties_based_on_overdue_principal_min");
            package.NonRepaymentPenaltiesMin.OverDueInterest = r.GetNullDouble("non_repayment_penalties_based_on_overdue_interest_min");

            package.NonRepaymentPenaltiesMax.InitialAmount = r.GetNullDouble("non_repayment_penalties_based_on_initial_amount_max");
            package.NonRepaymentPenaltiesMax.OLB = r.GetNullDouble("non_repayment_penalties_based_on_olb_max");
            package.NonRepaymentPenaltiesMax.OverDueInterest = r.GetNullDouble("non_repayment_penalties_based_on_overdue_interest_max");
            package.NonRepaymentPenaltiesMax.OverDuePrincipal = r.GetNullDouble("non_repayment_penalties_based_on_overdue_principal_max");
            package.AllowFlexibleSchedule = r.GetBool("allow_flexible_schedule");
            
            package.UseGuarantorCollateral = r.GetBool("use_guarantor_collateral");
            package.SetSeparateGuarantorCollateral = r.GetBool("set_separate_guarantor_collateral");

            package.PercentageTotalGuarantorCollateral = r.GetInt("percentage_total_guarantor_collateral");
            package.PercentageSeparateGuarantour = r.GetInt("percentage_separate_guarantor");
            package.PercentageSeparateCollateral = r.GetInt("percentage_separate_collateral");

            package.UseCompulsorySavings = r.GetBool("use_compulsory_savings");
            package.CompulsoryAmount = r.GetNullInt("compulsory_amount");
            package.CompulsoryAmountMin = r.GetNullInt("compulsory_amount_min");
            package.CompulsoryAmountMax = r.GetNullInt("compulsory_amount_max");
            package.UseEntryFeesCycles = r.GetBool("use_entry_fees_cycles");

            //if (DatabaseHelper.GetNullAuthorizedInt32("fundingLine_id", pReader).HasValue)
            //{
            //    package.FundingLine = new FundingLine { Id = r.GetNullInt("fundingLine_id").Value };
            //    package.FundingLine.Name = r.GetString("funding_line_name");
            //    package.FundingLine.Currency = new Currency { Id = r.GetInt("funding_line_currency_id") };
            //}
            if (r.GetNullInt("currency_id").HasValue)
            {
                package.Currency = new Currency
                                       {
                                           Id = r.GetInt("currency_id"),
                                           Code = r.GetString("currency_code"),
                                           Name = r.GetString("currency_name"),
                                           IsPivot = r.GetBool("currency_is_pivot"),
                                           IsSwapped = r.GetBool("currency_is_swapped"),
                                           UseCents = r.GetBool("currency_use_cents")
                                       };
            }

            /* Line of credit */
            package.DrawingsNumber = r.GetNullInt("number_of_drawings_loc");

            package.AmountUnderLoc = r.GetMoney("amount_under_loc");
            package.AmountUnderLocMin = r.GetMoney("amount_under_loc_min");
            package.AmountUnderLocMax = r.GetMoney("amount_under_loc_max");

            package.MaturityLoc = r.GetNullInt("maturity_loc");
            package.MaturityLocMin = r.GetNullInt("maturity_loc_min");
            package.MaturityLocMax = r.GetNullInt("maturity_loc_max");
            package.ActivatedLOC = r.GetBool("activated_loc");
            package.CycleId = r.GetNullInt("cycle_id");
            package.CreditInsuranceMin = r.GetDecimal("insurance_min");
            package.CreditInsuranceMax = r.GetDecimal("insurance_max");
            return package;
        }

        /// <summary>
        /// This method allows us to select a package from database.  We use the NullableTypes to make the correspondance between
        /// nullable int, decimal and double types in database and our own objects
        /// </summary>
        /// <param name="pProductId">id's of package searched</param>
        /// <returns>A package Object if id matches with datas in database, null if not</returns>

        public LoanProduct Select(int pProductId)
        {
            LoanProduct package;
            int? installmentTypeId;
            int? exoticProductId;
            
            const string q = @"SELECT Packages.*, 
                                       FundingLines.name AS funding_line_name,
                                       FundingLines.currency_id as funding_line_currency_id,
                                       Currencies.name as currency_name, 
                                       Currencies.code as currency_code,
                                       Currencies.is_pivot as currency_is_pivot, 
                                       Currencies.is_swapped as currency_is_swapped,
                                       Currencies.use_cents as currency_use_cents
                                     FROM Packages 
                                     LEFT JOIN FundingLines ON Packages.fundingLine_id = FundingLines.id
				                     LEFT JOIN Currencies on Packages.currency_id = Currencies.id
                                     WHERE Packages.id  = @id";

            using (SqlConnection conn = GetConnection())
            using (OctopusCommand c = new OctopusCommand(q, conn))
            {
                c.AddParam("@id", pProductId);

                using (OctopusReader r = c.ExecuteReader())
                {
                    if (r == null || r.Empty) return null;
                    r.Read();

                    package = GetProduct(r);
                    installmentTypeId = r.GetNullInt("installment_type");
                    exoticProductId = r.GetNullInt("exotic_id");
                }
            }
            if(installmentTypeId.HasValue) package.InstallmentType = installmentTypeManagement.SelectInstallmentType(installmentTypeId.Value);
            if(exoticProductId.HasValue) package.ExoticProduct = SelectExoticProductById(exoticProductId.Value);
            FireProductLoaded(package);

            return package;
        }

        public LoanProduct SelectByContractId(int contractId)
        {
            LoanProduct package;
            int? installmentTypeId;
            int? exoticProductId;
            int? amountCycleStockId;

            const string q = @"SELECT 
                                       Packages.*, 
                                       FundingLines.name AS funding_line_name,
                                       FundingLines.currency_id as funding_line_currency_id,
                                       Currencies.name as currency_name, 
                                       Currencies.code as currency_code,
                                       Currencies.is_pivot as currency_is_pivot, 
                                       Currencies.is_swapped as currency_is_swapped,
                                       Currencies.use_cents as currency_use_cents
                                     FROM Packages 
                                     INNER JOIN Credit ON Packages.id = Credit.package_id
                                     LEFT JOIN FundingLines ON Packages.fundingLine_id = FundingLines.id
				                     LEFT JOIN Currencies on Packages.currency_id = Currencies.id
                                     WHERE Credit.id  = @id";

            using (SqlConnection conn = GetConnection())
            using (OctopusCommand c = new OctopusCommand(q, conn))
            {
                c.AddParam("@id", contractId);

                using (OctopusReader r = c.ExecuteReader())
                {
                    if (r == null || r.Empty) return null;
                    r.Read();

                    package = GetProduct(r);
                    amountCycleStockId = r.GetNullInt("cycle_id");
                    installmentTypeId = r.GetNullInt("installment_type");
                    exoticProductId = r.GetNullInt("exotic_id");
                }
            }
            if (installmentTypeId.HasValue) package.InstallmentType = installmentTypeManagement.SelectInstallmentType(installmentTypeId.Value);
            if (exoticProductId.HasValue) package.ExoticProduct = SelectExoticProductById(exoticProductId.Value);
            if (amountCycleStockId.HasValue) package.CycleId = amountCycleStockId.Value;

            FireProductLoaded(package);

            return package;
        }

        public LoanProduct SelectByName(string name)
        {
            LoanProduct package;
            int? installmentTypeId;
            int? exoticProductId;
            int? amountCycleStockId;

            const string q = @"SELECT Packages.*, FundingLines.name AS funding_line_name,FundingLines.currency_id as funding_line_currency_id,
                Currencies.name as currency_name, Currencies.code as currency_code,
                Currencies.is_pivot as currency_is_pivot, Currencies.is_swapped as currency_is_swapped,
                Currencies.use_cents as currency_use_cents
                FROM Packages 
                LEFT JOIN FundingLines ON Packages.fundingLine_id = FundingLines.id
				LEFT JOIN Currencies on Packages.currency_id = Currencies.id
                WHERE Packages.name  = @name OR Packages.code = @name";

            using (SqlConnection conn = GetConnection())
            using (OctopusCommand c = new OctopusCommand(q, conn))
            {
                c.AddParam("@name", name);

                using (OctopusReader r = c.ExecuteReader())
                {
                    if (r == null || r.Empty) return null;
                    r.Read();

                    package = GetProduct(r);
                    amountCycleStockId = r.GetNullInt("cycle_id");
                    installmentTypeId = r.GetNullInt("installment_type");
                    exoticProductId = r.GetNullInt("exotic_id");
                }
            }
            if(installmentTypeId.HasValue) package.InstallmentType = installmentTypeManagement.SelectInstallmentType(installmentTypeId.Value);
            if(exoticProductId.HasValue) package.ExoticProduct = SelectExoticProductById(exoticProductId.Value);
//            if(amountCycleStockId.HasValue) package.AmountCycles = _SelectAmountCycleStockById(amountCycleStockId.Value);

            FireProductLoaded(package);

            return package;
        }

        public List<LoanProduct> SelectAllPackages(bool pShowAlsoDeleted, OClientTypes pClientType)
        {
            List<LoanProduct> packagesList = new List<LoanProduct>();
            string q = @"SELECT DISTINCT 
                                [dbo].[Packages].[id] 
                              FROM [dbo].[Packages] 
                              INNER JOIN [dbo].[PackagesClientTypes] ON [dbo].[Packages].[id]=[dbo].[PackagesClientTypes].[package_id] 
                              INNER JOIN [dbo].[ClientTypes] ON [dbo].[ClientTypes].[id]=[dbo].[PackagesClientTypes].[client_type_id] 
                            WHERE 1 = 1";
            if (!pShowAlsoDeleted)
                q += " AND [dbo].[Packages].[deleted] = 0";

            switch (pClientType)
            {
                case OClientTypes.Person:
                    q += " AND [dbo].[ClientTypes].[type_name] ='Individual' ";
                    break;
                case OClientTypes.Group:
                    q += "  AND [dbo].[ClientTypes].[type_name]='Group' ";
                    break;
                case OClientTypes.Corporate:
                    q += "  AND [dbo].[ClientTypes].[type_name]='Corporate' ";
                    break;
                case OClientTypes.Village:
                    q += " AND [dbo].[ClientTypes].[type_name]='Village' ";
                    break;
            }

            using (SqlConnection conn = GetConnection())
            using (OctopusCommand c = new OctopusCommand(q, conn))
            using (OctopusReader r = c.ExecuteReader())
            {
                if (r == null || r.Empty) return new List<LoanProduct>();
                while (r.Read())
                {
                    LoanProduct pack = new LoanProduct {Id = r.GetInt("id")};
                    packagesList.Add(pack);
                }
            }
            

            for (int i = 0; i < packagesList.Count; i++)
            {
                LoanProduct product = Select(packagesList[i].Id);
                packagesList[i] = product;
                FireProductLoaded(product);
            }
            return packagesList;
        }

        /// <summary>
        /// Delete a product
        /// </summary>
        /// <param name="pProductId"></param>
        public void DeleteProduct(int pProductId)
        {
            const string q = @"UPDATE Packages SET deleted = 1 WHERE id = @id";

            using (SqlConnection conn = GetConnection())
            using (OctopusCommand c = new OctopusCommand(q, conn))
            {
                c.AddParam("@id", pProductId);
                c.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Add an exotic product in databse
        /// </summary>
        /// <param name="pExoticInstallmentsTable"></param>
        /// <returns>database id</returns>
        public int AddExoticInstallmentsTable(ExoticInstallmentsTable pExoticInstallmentsTable)
        {
            const string q = @"INSERT INTO Exotics (name) VALUES (@name) SELECT SCOPE_IDENTITY()";

            using (SqlConnection conn = GetConnection())
            using (OctopusCommand c = new OctopusCommand(q, conn))
            {
                c.AddParam("@name", pExoticInstallmentsTable.Name);
                return Convert.ToInt32(c.ExecuteScalar());
            }
        }

        /// <summary>
        /// Add a loan cycle into the databse
        /// </summary>
        /// <param name="loanCycle">An instance of the class LoanCycle</param>
        /// <param name="transac">SQL transaction</param>
        /// <returns></returns>
        public int InsertLoanCycle(LoanCycle loanCycle, SqlTransaction t)
        {
            const string q = "INSERT INTO Cycles (name) VALUES (@name) SELECT SCOPE_IDENTITY()";

            using (OctopusCommand c = new OctopusCommand(q, t.Connection, t))
            {
                c.AddParam("@name", loanCycle.Name);
                return Convert.ToInt32(c.ExecuteScalar());
            }
        }

        private static ExoticInstallmentsTable _GetExoticProduct(OctopusReader r)
        {
            return new ExoticInstallmentsTable
                       {
                           Id = r.GetInt("id"),
                           Name = r.GetString("name")
                       };
        }

        private ExoticInstallmentsTable SelectExoticProductById(int exoId)
        {
            ExoticInstallmentsTable exoticProduct;

            const string q = "SELECT [id], name FROM Exotics WHERE id = @id";

            using (SqlConnection conn = GetConnection())
            using (OctopusCommand c = new OctopusCommand(q, conn))
            {
                c.AddParam("@id", exoId);
                using (OctopusReader r = c.ExecuteReader())
                {
                    if (r == null || r.Empty) return null;

                    r.Read();
                    exoticProduct = _GetExoticProduct(r);
                }
            }

            exoticProduct.Add(_SelectExoticInstallmentsByProductId(exoticProduct.Id));
            return exoticProduct;
        }

        /// <summary>
        /// Select all exotic product
        /// </summary>
        /// <returns></returns>
        public List<ExoticInstallmentsTable> SelectAllInstallmentsTables()
        {
            List<ExoticInstallmentsTable> list = new List<ExoticInstallmentsTable>();
            const string q = "SELECT [id],name FROM Exotics";

            using (SqlConnection conn = GetConnection())
            using (OctopusCommand c = new OctopusCommand(q, conn))
            {
                using (OctopusReader r = c.ExecuteReader())
                {
                    if(r == null || r.Empty) return new List<ExoticInstallmentsTable>();
                    while (r.Read())
                    {
                        list.Add(_GetExoticProduct(r));
                    }
                }
            }

            foreach (ExoticInstallmentsTable e in list)
            {
                e.Add(_SelectExoticInstallmentsByProductId(e.Id));
            }
            return list;
        }

        /// <summary>
        /// Add an exotic installment in database
        /// </summary>
        /// <param name="exoticInstallment"></param>
        /// <param name="product"></param>
        public void AddExoticInstallment(ExoticInstallment exoticInstallment, ExoticInstallmentsTable product)
        {
            const string q = @"INSERT INTO ExoticInstallments (number,principal_coeff,interest_coeff,exotic_id)
                         VALUES (@number,@principalCoeff,@interestCoeff,@exoticId)";

            using (SqlConnection conn = GetConnection())
            using (OctopusCommand c = new OctopusCommand(q, conn))
            {
                c.AddParam("@number", exoticInstallment.Number);
                c.AddParam("@principalCoeff", exoticInstallment.PrincipalCoeff);
                c.AddParam("@interestCoeff", exoticInstallment.InterestCoeff);
                c.AddParam("@exoticId", product.Id);
                c.ExecuteNonQuery();
            }
        }

        private List<ExoticInstallment> _SelectExoticInstallmentsByProductId(int productId)
        {
            List<ExoticInstallment> list = new List<ExoticInstallment>();
            const string q = @"SELECT number,principal_coeff,interest_coeff 
                                   FROM ExoticInstallments 
                                   WHERE exotic_id =@exoticId";

            using (SqlConnection conn = GetConnection())
            using (OctopusCommand c = new OctopusCommand(q, conn))
            {
                c.AddParam("@exoticId", productId);

                using (OctopusReader r = c.ExecuteReader())
                {
                    if(r == null || r.Empty) return new List<ExoticInstallment>();
                    while (r.Read())
                    {
                        ExoticInstallment exoticInstallment = _GetExoticInstallment(r);
                        list.Add(exoticInstallment);
                    }
                }
                return list;
            }
        }

        private static ExoticInstallment _GetExoticInstallment(OctopusReader r)
        {
            return new ExoticInstallment
                       {
                           Number = r.GetInt("number"),
                           PrincipalCoeff = r.GetDouble("principal_coeff"),
                           InterestCoeff =r.GetNullDouble("interest_coeff")
                       };
        }

        public List<ProductClientType> SelectClientTypes()
        {
            List<ProductClientType> clientTypes = new List<ProductClientType>();
            string q = @"SELECT [id], [type_name] 
                             FROM  [dbo].[ClientTypes]";
            using (SqlConnection conn = GetConnection())
            using (OctopusCommand c = new OctopusCommand(q, conn))
            {
                using (OctopusReader r = c.ExecuteReader())
                {
                    while (r.Read())
                    {
                        clientTypes.Add(new ProductClientType(r.GetInt("id"), r.GetString("type_name")));
                    }
                }
            }
            return clientTypes;
        }

        public void GetAssignedTypes(List<ProductClientType> productClientTypes, int productId)
        {
            foreach (ProductClientType clientType in productClientTypes)
            {
                string q = string.Format(
                    @"SELECT client_type_id
                            ,package_id
                     FROM [dbo].[PackagesClientTypes]
                     WHERE client_type_id={0} AND package_id={1}",
                    clientType.TypeId, productId);
                using (SqlConnection conn = GetConnection())
                using (OctopusCommand c = new OctopusCommand(q, conn))
                using (OctopusReader r = c.ExecuteReader())
                if (!r.Empty)
                {
                    clientType.IsChecked = true;
                }
            }
        }

        public int SelectIdByCode(string code)
        {
            const string q = @"select id
            from dbo.Packages
            where code = @code";
            using (SqlConnection conn = GetConnection())
            using (OctopusCommand c = new OctopusCommand(q, conn))
            {
                c.AddParam("@code", code);
                object v = c.ExecuteScalar();
                return null == v ? 0 : Convert.ToInt32(v);
            }
        }

        public int SelectFundingLineId(int id)
        {
            const string q = @"SELECT ISNULL(fundingLine_id, 0)
            FROM dbo.Packages
            WHERE id = @id";
            using (SqlConnection conn = GetConnection())
            using (OctopusCommand c = new OctopusCommand(q, conn))
            {
                c.AddParam("@id", id);
                return Convert.ToInt32(c.ExecuteScalar());
            }
        }
    }
}