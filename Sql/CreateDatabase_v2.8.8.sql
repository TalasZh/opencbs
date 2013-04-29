IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Accounts_Accounts]') AND parent_object_id = OBJECT_ID(N'[dbo].[Accounts]'))
ALTER TABLE [dbo].[Accounts] DROP CONSTRAINT [FK_Accounts_Accounts]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Accounts_Currencies]') AND parent_object_id = OBJECT_ID(N'[dbo].[Accounts]'))
ALTER TABLE [dbo].[Accounts] DROP CONSTRAINT [FK_Accounts_Currencies]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AmountCycles_Cycles]') AND parent_object_id = OBJECT_ID(N'[dbo].[AmountCycles]'))
ALTER TABLE [dbo].[AmountCycles] DROP CONSTRAINT [FK_AmountCycles_Cycles]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_City_Districts]') AND parent_object_id = OBJECT_ID(N'[dbo].[City]'))
ALTER TABLE [dbo].[City] DROP CONSTRAINT [FK_City_Districts]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Collaterals_Collaterals]') AND parent_object_id = OBJECT_ID(N'[dbo].[Collaterals]'))
ALTER TABLE [dbo].[Collaterals] DROP CONSTRAINT [FK_Collaterals_Collaterals]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CompulsorySavingsContracts_Contracts]') AND parent_object_id = OBJECT_ID(N'[dbo].[CompulsorySavingsContracts]'))
ALTER TABLE [dbo].[CompulsorySavingsContracts] DROP CONSTRAINT [FK_CompulsorySavingsContracts_Contracts]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CompulsorySavingsContracts_SavingContracts]') AND parent_object_id = OBJECT_ID(N'[dbo].[CompulsorySavingsContracts]'))
ALTER TABLE [dbo].[CompulsorySavingsContracts] DROP CONSTRAINT [FK_CompulsorySavingsContracts_SavingContracts]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CompulsorySavingsProducts_SavingProducts]') AND parent_object_id = OBJECT_ID(N'[dbo].[CompulsorySavingsProducts]'))
ALTER TABLE [dbo].[CompulsorySavingsProducts] DROP CONSTRAINT [FK_CompulsorySavingsProducts_SavingProducts]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ContractAccountingRules_AccountingRules]') AND parent_object_id = OBJECT_ID(N'[dbo].[ContractAccountingRules]'))
ALTER TABLE [dbo].[ContractAccountingRules] DROP CONSTRAINT [FK_ContractAccountingRules_AccountingRules]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ContractAccountingRules_DomainOfApplications]') AND parent_object_id = OBJECT_ID(N'[dbo].[ContractAccountingRules]'))
ALTER TABLE [dbo].[ContractAccountingRules] DROP CONSTRAINT [FK_ContractAccountingRules_DomainOfApplications]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ContractAccountingRules_GuaranteesPackages]') AND parent_object_id = OBJECT_ID(N'[dbo].[ContractAccountingRules]'))
ALTER TABLE [dbo].[ContractAccountingRules] DROP CONSTRAINT [FK_ContractAccountingRules_GuaranteesPackages]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ContractAccountingRules_Packages]') AND parent_object_id = OBJECT_ID(N'[dbo].[ContractAccountingRules]'))
ALTER TABLE [dbo].[ContractAccountingRules] DROP CONSTRAINT [FK_ContractAccountingRules_Packages]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ContractAccountingRules_SavingProducts]') AND parent_object_id = OBJECT_ID(N'[dbo].[ContractAccountingRules]'))
ALTER TABLE [dbo].[ContractAccountingRules] DROP CONSTRAINT [FK_ContractAccountingRules_SavingProducts]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ContractAccountsBalance_Contracts]') AND parent_object_id = OBJECT_ID(N'[dbo].[ContractAccountsBalance]'))
ALTER TABLE [dbo].[ContractAccountsBalance] DROP CONSTRAINT [FK_ContractAccountsBalance_Contracts]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ContractAssignHistory_Contracts]') AND parent_object_id = OBJECT_ID(N'[dbo].[ContractAssignHistory]'))
ALTER TABLE [dbo].[ContractAssignHistory] DROP CONSTRAINT [FK_ContractAssignHistory_Contracts]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ContractAssignHistory_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[ContractAssignHistory]'))
ALTER TABLE [dbo].[ContractAssignHistory] DROP CONSTRAINT [FK_ContractAssignHistory_Users]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ContractAssignHistory_Users1]') AND parent_object_id = OBJECT_ID(N'[dbo].[ContractAssignHistory]'))
ALTER TABLE [dbo].[ContractAssignHistory] DROP CONSTRAINT [FK_ContractAssignHistory_Users1]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ContractEvents_Contracts]') AND parent_object_id = OBJECT_ID(N'[dbo].[ContractEvents]'))
ALTER TABLE [dbo].[ContractEvents] DROP CONSTRAINT [FK_ContractEvents_Contracts]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ContractEvents_LoanInterestAccruingEvents]') AND parent_object_id = OBJECT_ID(N'[dbo].[ContractEvents]'))
ALTER TABLE [dbo].[ContractEvents] DROP CONSTRAINT [FK_ContractEvents_LoanInterestAccruingEvents]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ContractEvents_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[ContractEvents]'))
ALTER TABLE [dbo].[ContractEvents] DROP CONSTRAINT [FK_ContractEvents_Users]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Contracts_Projects]') AND parent_object_id = OBJECT_ID(N'[dbo].[Contracts]'))
ALTER TABLE [dbo].[Contracts] DROP CONSTRAINT [FK_Contracts_Projects]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CorporatePersonBelonging_Corporates]') AND parent_object_id = OBJECT_ID(N'[dbo].[CorporatePersonBelonging]'))
ALTER TABLE [dbo].[CorporatePersonBelonging] DROP CONSTRAINT [FK_CorporatePersonBelonging_Corporates]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CorporatePersonBelonging_Persons]') AND parent_object_id = OBJECT_ID(N'[dbo].[CorporatePersonBelonging]'))
ALTER TABLE [dbo].[CorporatePersonBelonging] DROP CONSTRAINT [FK_CorporatePersonBelonging_Persons]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Corporates_DomainOfApplications]') AND parent_object_id = OBJECT_ID(N'[dbo].[Corporates]'))
ALTER TABLE [dbo].[Corporates] DROP CONSTRAINT [FK_Corporates_DomainOfApplications]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Credit_Contracts]') AND parent_object_id = OBJECT_ID(N'[dbo].[Credit]'))
ALTER TABLE [dbo].[Credit] DROP CONSTRAINT [FK_Credit_Contracts]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Credit_InstallmentTypes]') AND parent_object_id = OBJECT_ID(N'[dbo].[Credit]'))
ALTER TABLE [dbo].[Credit] DROP CONSTRAINT [FK_Credit_InstallmentTypes]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Credit_Packages]') AND parent_object_id = OBJECT_ID(N'[dbo].[Credit]'))
ALTER TABLE [dbo].[Credit] DROP CONSTRAINT [FK_Credit_Packages]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Credit_Temp_FundingLines]') AND parent_object_id = OBJECT_ID(N'[dbo].[Credit]'))
ALTER TABLE [dbo].[Credit] DROP CONSTRAINT [FK_Credit_Temp_FundingLines]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Credit_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[Credit]'))
ALTER TABLE [dbo].[Credit] DROP CONSTRAINT [FK_Credit_Users]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Districts_Provinces]') AND parent_object_id = OBJECT_ID(N'[dbo].[Districts]'))
ALTER TABLE [dbo].[Districts] DROP CONSTRAINT [FK_Districts_Provinces]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DomainOfApplications_DomainOfApplications]') AND parent_object_id = OBJECT_ID(N'[dbo].[DomainOfApplications]'))
ALTER TABLE [dbo].[DomainOfApplications] DROP CONSTRAINT [FK_DomainOfApplications_DomainOfApplications]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ElementaryMvts_Credit_Accounts]') AND parent_object_id = OBJECT_ID(N'[dbo].[ElementaryMvts]'))
ALTER TABLE [dbo].[ElementaryMvts] DROP CONSTRAINT [FK_ElementaryMvts_Credit_Accounts]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ElementaryMvts_Debit_Accounts]') AND parent_object_id = OBJECT_ID(N'[dbo].[ElementaryMvts]'))
ALTER TABLE [dbo].[ElementaryMvts] DROP CONSTRAINT [FK_ElementaryMvts_Debit_Accounts]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ElementaryMvts_Transactions1]') AND parent_object_id = OBJECT_ID(N'[dbo].[ElementaryMvts]'))
ALTER TABLE [dbo].[ElementaryMvts] DROP CONSTRAINT [FK_ElementaryMvts_Transactions1]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Events_MovementSet]') AND parent_object_id = OBJECT_ID(N'[dbo].[Events]'))
ALTER TABLE [dbo].[Events] DROP CONSTRAINT [FK_Events_MovementSet]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Events_StatisticalProvisoningEvents]') AND parent_object_id = OBJECT_ID(N'[dbo].[Events]'))
ALTER TABLE [dbo].[Events] DROP CONSTRAINT [FK_Events_StatisticalProvisoningEvents]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Events_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[Events]'))
ALTER TABLE [dbo].[Events] DROP CONSTRAINT [FK_Events_Users]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ExoticInstallments_Exotics]') AND parent_object_id = OBJECT_ID(N'[dbo].[ExoticInstallments]'))
ALTER TABLE [dbo].[ExoticInstallments] DROP CONSTRAINT [FK_ExoticInstallments_Exotics]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_FollowUp_Projects]') AND parent_object_id = OBJECT_ID(N'[dbo].[FollowUp]'))
ALTER TABLE [dbo].[FollowUp] DROP CONSTRAINT [FK_FollowUp_Projects]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_FundingLineAccountingRules_AccountingRules]') AND parent_object_id = OBJECT_ID(N'[dbo].[FundingLineAccountingRules]'))
ALTER TABLE [dbo].[FundingLineAccountingRules] DROP CONSTRAINT [FK_FundingLineAccountingRules_AccountingRules]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_FundingLineAccountingRules_FundingLine]') AND parent_object_id = OBJECT_ID(N'[dbo].[FundingLineAccountingRules]'))
ALTER TABLE [dbo].[FundingLineAccountingRules] DROP CONSTRAINT [FK_FundingLineAccountingRules_FundingLine]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_FundingLineEvents_FundingLines]') AND parent_object_id = OBJECT_ID(N'[dbo].[FundingLineEvents]'))
ALTER TABLE [dbo].[FundingLineEvents] DROP CONSTRAINT [FK_FundingLineEvents_FundingLines]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_FundingLines_Currencies]') AND parent_object_id = OBJECT_ID(N'[dbo].[FundingLines]'))
ALTER TABLE [dbo].[FundingLines] DROP CONSTRAINT [FK_FundingLines_Currencies]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Groups_Tiers]') AND parent_object_id = OBJECT_ID(N'[dbo].[Groups]'))
ALTER TABLE [dbo].[Groups] DROP CONSTRAINT [FK_Groups_Tiers]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Garantees_GaranteesPackages]') AND parent_object_id = OBJECT_ID(N'[dbo].[Guarantees]'))
ALTER TABLE [dbo].[Guarantees] DROP CONSTRAINT [FK_Garantees_GaranteesPackages]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Guarantees_Contracts]') AND parent_object_id = OBJECT_ID(N'[dbo].[Guarantees]'))
ALTER TABLE [dbo].[Guarantees] DROP CONSTRAINT [FK_Guarantees_Contracts]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Guarantees_FundingLines]') AND parent_object_id = OBJECT_ID(N'[dbo].[Guarantees]'))
ALTER TABLE [dbo].[Guarantees] DROP CONSTRAINT [FK_Guarantees_FundingLines]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Guarantees_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[Guarantees]'))
ALTER TABLE [dbo].[Guarantees] DROP CONSTRAINT [FK_Guarantees_Users]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GaranteesPackages_FundingLines]') AND parent_object_id = OBJECT_ID(N'[dbo].[GuaranteesPackages]'))
ALTER TABLE [dbo].[GuaranteesPackages] DROP CONSTRAINT [FK_GaranteesPackages_FundingLines]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GuaranteesPackages_Currencies]') AND parent_object_id = OBJECT_ID(N'[dbo].[GuaranteesPackages]'))
ALTER TABLE [dbo].[GuaranteesPackages] DROP CONSTRAINT [FK_GuaranteesPackages_Currencies]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_InstallmentHistory_ContractEvents]') AND parent_object_id = OBJECT_ID(N'[dbo].[InstallmentHistory]'))
ALTER TABLE [dbo].[InstallmentHistory] DROP CONSTRAINT [FK_InstallmentHistory_ContractEvents]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Installments_Credit]') AND parent_object_id = OBJECT_ID(N'[dbo].[Installments]'))
ALTER TABLE [dbo].[Installments] DROP CONSTRAINT [FK_Installments_Credit]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_LinkGuarantorCredit_Contracts]') AND parent_object_id = OBJECT_ID(N'[dbo].[LinkGuarantorCredit]'))
ALTER TABLE [dbo].[LinkGuarantorCredit] DROP CONSTRAINT [FK_LinkGuarantorCredit_Contracts]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_LinkGuarantorCredit_Tiers]') AND parent_object_id = OBJECT_ID(N'[dbo].[LinkGuarantorCredit]'))
ALTER TABLE [dbo].[LinkGuarantorCredit] DROP CONSTRAINT [FK_LinkGuarantorCredit_Tiers]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CreditOrigEvents_ContractEvents]') AND parent_object_id = OBJECT_ID(N'[dbo].[LoanDisbursmentEvents]'))
ALTER TABLE [dbo].[LoanDisbursmentEvents] DROP CONSTRAINT [FK_CreditOrigEvents_ContractEvents]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MovementSet_ContractEvents]') AND parent_object_id = OBJECT_ID(N'[dbo].[MovementSet]'))
ALTER TABLE [dbo].[MovementSet] DROP CONSTRAINT [FK_MovementSet_ContractEvents]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MovementSet_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[MovementSet]'))
ALTER TABLE [dbo].[MovementSet] DROP CONSTRAINT [FK_MovementSet_Users]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Packages_Currencies]') AND parent_object_id = OBJECT_ID(N'[dbo].[Packages]'))
ALTER TABLE [dbo].[Packages] DROP CONSTRAINT [FK_Packages_Currencies]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Packages_Cycles]') AND parent_object_id = OBJECT_ID(N'[dbo].[Packages]'))
ALTER TABLE [dbo].[Packages] DROP CONSTRAINT [FK_Packages_Cycles]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Packages_Exotics]') AND parent_object_id = OBJECT_ID(N'[dbo].[Packages]'))
ALTER TABLE [dbo].[Packages] DROP CONSTRAINT [FK_Packages_Exotics]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Packages_InstallmentTypes]') AND parent_object_id = OBJECT_ID(N'[dbo].[Packages]'))
ALTER TABLE [dbo].[Packages] DROP CONSTRAINT [FK_Packages_InstallmentTypes]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PastDueLoanEvents_ContractEvents]') AND parent_object_id = OBJECT_ID(N'[dbo].[PastDueLoanEvents]'))
ALTER TABLE [dbo].[PastDueLoanEvents] DROP CONSTRAINT [FK_PastDueLoanEvents_ContractEvents]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PersonGroupBelonging_Persons1]') AND parent_object_id = OBJECT_ID(N'[dbo].[PersonGroupBelonging]'))
ALTER TABLE [dbo].[PersonGroupBelonging] DROP CONSTRAINT [FK_PersonGroupBelonging_Persons1]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PersonGroupCorrespondance_Groups]') AND parent_object_id = OBJECT_ID(N'[dbo].[PersonGroupBelonging]'))
ALTER TABLE [dbo].[PersonGroupBelonging] DROP CONSTRAINT [FK_PersonGroupCorrespondance_Groups]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Persons_Banks]') AND parent_object_id = OBJECT_ID(N'[dbo].[Persons]'))
ALTER TABLE [dbo].[Persons] DROP CONSTRAINT [FK_Persons_Banks]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Persons_Banks1]') AND parent_object_id = OBJECT_ID(N'[dbo].[Persons]'))
ALTER TABLE [dbo].[Persons] DROP CONSTRAINT [FK_Persons_Banks1]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Persons_DomainOfApplications]') AND parent_object_id = OBJECT_ID(N'[dbo].[Persons]'))
ALTER TABLE [dbo].[Persons] DROP CONSTRAINT [FK_Persons_DomainOfApplications]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Persons_Tiers]') AND parent_object_id = OBJECT_ID(N'[dbo].[Persons]'))
ALTER TABLE [dbo].[Persons] DROP CONSTRAINT [FK_Persons_Tiers]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Projects_Tiers]') AND parent_object_id = OBJECT_ID(N'[dbo].[Projects]'))
ALTER TABLE [dbo].[Projects] DROP CONSTRAINT [FK_Projects_Tiers]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RepaymentEvents_ContractEvents]') AND parent_object_id = OBJECT_ID(N'[dbo].[RepaymentEvents]'))
ALTER TABLE [dbo].[RepaymentEvents] DROP CONSTRAINT [FK_RepaymentEvents_ContractEvents]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ReschedulingOfALoanEvents_ContractEvents]') AND parent_object_id = OBJECT_ID(N'[dbo].[ReschedulingOfALoanEvents]'))
ALTER TABLE [dbo].[ReschedulingOfALoanEvents] DROP CONSTRAINT [FK_ReschedulingOfALoanEvents_ContractEvents]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SavingAccountsBalance_SavingContracts]') AND parent_object_id = OBJECT_ID(N'[dbo].[SavingAccountsBalance]'))
ALTER TABLE [dbo].[SavingAccountsBalance] DROP CONSTRAINT [FK_SavingAccountsBalance_SavingContracts]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SavingBookContract_SavingContracts]') AND parent_object_id = OBJECT_ID(N'[dbo].[SavingBookContracts]'))
ALTER TABLE [dbo].[SavingBookContracts] DROP CONSTRAINT [FK_SavingBookContract_SavingContracts]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SavingBookProducts_SavingProducts]') AND parent_object_id = OBJECT_ID(N'[dbo].[SavingBookProducts]'))
ALTER TABLE [dbo].[SavingBookProducts] DROP CONSTRAINT [FK_SavingBookProducts_SavingProducts]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SavingContracts_Tiers]') AND parent_object_id = OBJECT_ID(N'[dbo].[SavingContracts]'))
ALTER TABLE [dbo].[SavingContracts] DROP CONSTRAINT [FK_SavingContracts_Tiers]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SavingContracts_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[SavingContracts]'))
ALTER TABLE [dbo].[SavingContracts] DROP CONSTRAINT [FK_SavingContracts_Users]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Savings_SavingProducts]') AND parent_object_id = OBJECT_ID(N'[dbo].[SavingContracts]'))
ALTER TABLE [dbo].[SavingContracts] DROP CONSTRAINT [FK_Savings_SavingProducts]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SavingDepositContract_SavingContracts]') AND parent_object_id = OBJECT_ID(N'[dbo].[SavingDepositContracts]'))
ALTER TABLE [dbo].[SavingDepositContracts] DROP CONSTRAINT [FK_SavingDepositContract_SavingContracts]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SavingEvents_SavingContracts]') AND parent_object_id = OBJECT_ID(N'[dbo].[SavingEvents]'))
ALTER TABLE [dbo].[SavingEvents] DROP CONSTRAINT [FK_SavingEvents_SavingContracts]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SavingEvents_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[SavingEvents]'))
ALTER TABLE [dbo].[SavingEvents] DROP CONSTRAINT [FK_SavingEvents_Users]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SavingProducts_Currencies]') AND parent_object_id = OBJECT_ID(N'[dbo].[SavingProducts]'))
ALTER TABLE [dbo].[SavingProducts] DROP CONSTRAINT [FK_SavingProducts_Currencies]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TermDepositProducts_InstallmentTypes]') AND parent_object_id = OBJECT_ID(N'[dbo].[TermDepositProducts]'))
ALTER TABLE [dbo].[TermDepositProducts] DROP CONSTRAINT [FK_TermDepositProducts_InstallmentTypes]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TermDepositProducts_SavingProducts]') AND parent_object_id = OBJECT_ID(N'[dbo].[TermDepositProducts]'))
ALTER TABLE [dbo].[TermDepositProducts] DROP CONSTRAINT [FK_TermDepositProducts_SavingProducts]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tiers_Corporates]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tiers]'))
ALTER TABLE [dbo].[Tiers] DROP CONSTRAINT [FK_Tiers_Corporates]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tiers_Districts]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tiers]'))
ALTER TABLE [dbo].[Tiers] DROP CONSTRAINT [FK_Tiers_Districts]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tiers_Districts1]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tiers]'))
ALTER TABLE [dbo].[Tiers] DROP CONSTRAINT [FK_Tiers_Districts1]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserRole_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole]'))
ALTER TABLE [dbo].[UserRole] DROP CONSTRAINT [FK_UserRole_Roles]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserRole_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole]'))
ALTER TABLE [dbo].[UserRole] DROP CONSTRAINT [FK_UserRole_Users]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Villages_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[Villages]'))
ALTER TABLE [dbo].[Villages] DROP CONSTRAINT [FK_Villages_Users]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WriteOffEvents_ContractEvents]') AND parent_object_id = OBJECT_ID(N'[dbo].[WriteOffEvents]'))
ALTER TABLE [dbo].[WriteOffEvents] DROP CONSTRAINT [FK_WriteOffEvents_ContractEvents]
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[dbo].[CK_Packages]') AND parent_object_id = OBJECT_ID(N'[dbo].[Packages]'))
ALTER TABLE [dbo].[Packages] DROP CONSTRAINT [CK_Packages]
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[dbo].[CK_TiersTypeCode]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tiers]'))
ALTER TABLE [dbo].[Tiers] DROP CONSTRAINT [CK_TiersTypeCode]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FundingLineAccountingRules]') AND type in (N'U'))
DROP TABLE [dbo].[FundingLineAccountingRules]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LoanShareAmounts]') AND type in (N'U'))
DROP TABLE [dbo].[LoanShareAmounts]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ElementaryMvts]') AND type in (N'U'))
DROP TABLE [dbo].[ElementaryMvts]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StandardBookings]') AND type in (N'U'))
DROP TABLE [dbo].[StandardBookings]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Villages]') AND type in (N'U'))
DROP TABLE [dbo].[Villages]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VillagesPersons]') AND type in (N'U'))
DROP TABLE [dbo].[VillagesPersons]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MFI]') AND type in (N'U'))
DROP TABLE [dbo].[MFI]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Events]') AND type in (N'U'))
DROP TABLE [dbo].[Events]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pictures]') AND type in (N'U'))
DROP TABLE [dbo].[Pictures]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContractAssignHistory]') AND type in (N'U'))
DROP TABLE [dbo].[ContractAssignHistory]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SavingBookContracts]') AND type in (N'U'))
DROP TABLE [dbo].[SavingBookContracts]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SavingDepositContracts]') AND type in (N'U'))
DROP TABLE [dbo].[SavingDepositContracts]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomizableFieldsSettings]') AND type in (N'U'))
DROP TABLE [dbo].[CustomizableFieldsSettings]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SavingEvents]') AND type in (N'U'))
DROP TABLE [dbo].[SavingEvents]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SavingAccountsBalance]') AND type in (N'U'))
DROP TABLE [dbo].[SavingAccountsBalance]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PersonCustomizableFields]') AND type in (N'U'))
DROP TABLE [dbo].[PersonCustomizableFields]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LoanScale]') AND type in (N'U'))
DROP TABLE [dbo].[LoanScale]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Questionnaire]') AND type in (N'U'))
DROP TABLE [dbo].[Questionnaire]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LinkGuarantorCredit]') AND type in (N'U'))
DROP TABLE [dbo].[LinkGuarantorCredit]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContractAccountsBalance]') AND type in (N'U'))
DROP TABLE [dbo].[ContractAccountsBalance]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProjectPurposes]') AND type in (N'U'))
DROP TABLE [dbo].[ProjectPurposes]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetUp_ProfessionalSituation]') AND type in (N'U'))
DROP TABLE [dbo].[SetUp_ProfessionalSituation]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetUp_Sponsor2]') AND type in (N'U'))
DROP TABLE [dbo].[SetUp_Sponsor2]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetUp_ActivityState]') AND type in (N'U'))
DROP TABLE [dbo].[SetUp_ActivityState]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RepaymentEvents]') AND type in (N'U'))
DROP TABLE [dbo].[RepaymentEvents]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetUp_ProfessionalExperience]') AND type in (N'U'))
DROP TABLE [dbo].[SetUp_ProfessionalExperience]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HousingSituation]') AND type in (N'U'))
DROP TABLE [dbo].[HousingSituation]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Info]') AND type in (N'U'))
DROP TABLE [dbo].[Info]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetUp_HousingSituation]') AND type in (N'U'))
DROP TABLE [dbo].[SetUp_HousingSituation]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TrancheEvents]') AND type in (N'U'))
DROP TABLE [dbo].[TrancheEvents]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetUp_HousingLocation]') AND type in (N'U'))
DROP TABLE [dbo].[SetUp_HousingLocation]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WriteOffEvents]') AND type in (N'U'))
DROP TABLE [dbo].[WriteOffEvents]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetUp_Sponsor1]') AND type in (N'U'))
DROP TABLE [dbo].[SetUp_Sponsor1]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetUp_SageJournal]') AND type in (N'U'))
DROP TABLE [dbo].[SetUp_SageJournal]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetUp_BankSituation]') AND type in (N'U'))
DROP TABLE [dbo].[SetUp_BankSituation]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetUp_PersonalSituation]') AND type in (N'U'))
DROP TABLE [dbo].[SetUp_PersonalSituation]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetUp_SocialSituation]') AND type in (N'U'))
DROP TABLE [dbo].[SetUp_SocialSituation]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ExoticInstallments]') AND type in (N'U'))
DROP TABLE [dbo].[ExoticInstallments]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GeneralParameters]') AND type in (N'U'))
DROP TABLE [dbo].[GeneralParameters]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetUp_InsertionTypes]') AND type in (N'U'))
DROP TABLE [dbo].[SetUp_InsertionTypes]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TechnicalParameters]') AND type in (N'U'))
DROP TABLE [dbo].[TechnicalParameters]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetUp_SubventionTypes]') AND type in (N'U'))
DROP TABLE [dbo].[SetUp_SubventionTypes]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tconso_SP_Octopus_Consolidation_default]') AND type in (N'U'))
DROP TABLE [dbo].[Tconso_SP_Octopus_Consolidation_default]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetUp_DwellingPlace]') AND type in (N'U'))
DROP TABLE [dbo].[SetUp_DwellingPlace]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetUp_FamilySituation]') AND type in (N'U'))
DROP TABLE [dbo].[SetUp_FamilySituation]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetUp_BusinessPlan]') AND type in (N'U'))
DROP TABLE [dbo].[SetUp_BusinessPlan]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetUp_StudyLevel]') AND type in (N'U'))
DROP TABLE [dbo].[SetUp_StudyLevel]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FundingLineEvents]') AND type in (N'U'))
DROP TABLE [dbo].[FundingLineEvents]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetUp_Registre]') AND type in (N'U'))
DROP TABLE [dbo].[SetUp_Registre]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ExchangeRates]') AND type in (N'U'))
DROP TABLE [dbo].[ExchangeRates]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CorporatePersonBelonging]') AND type in (N'U'))
DROP TABLE [dbo].[CorporatePersonBelonging]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProvisioningRules]') AND type in (N'U'))
DROP TABLE [dbo].[ProvisioningRules]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Installments]') AND type in (N'U'))
DROP TABLE [dbo].[Installments]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Guarantees]') AND type in (N'U'))
DROP TABLE [dbo].[Guarantees]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InstallmentHistory]') AND type in (N'U'))
DROP TABLE [dbo].[InstallmentHistory]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Collaterals]') AND type in (N'U'))
DROP TABLE [dbo].[Collaterals]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PastDueLoanEvents]') AND type in (N'U'))
DROP TABLE [dbo].[PastDueLoanEvents]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LinkCollateralCredit]') AND type in (N'U'))
DROP TABLE [dbo].[LinkCollateralCredit]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PublicHolidays]') AND type in (N'U'))
DROP TABLE [dbo].[PublicHolidays]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CorporateEventsType]') AND type in (N'U'))
DROP TABLE [dbo].[CorporateEventsType]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LoanDisbursmentEvents]') AND type in (N'U'))
DROP TABLE [dbo].[LoanDisbursmentEvents]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserRole]') AND type in (N'U'))
DROP TABLE [dbo].[UserRole]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MenuItems]') AND type in (N'U'))
DROP TABLE [dbo].[MenuItems]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AllowedRoleMenus]') AND type in (N'U'))
DROP TABLE [dbo].[AllowedRoleMenus]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ActionItems]') AND type in (N'U'))
DROP TABLE [dbo].[ActionItems]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AllowedRoleActions]') AND type in (N'U'))
DROP TABLE [dbo].[AllowedRoleActions]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReschedulingOfALoanEvents]') AND type in (N'U'))
DROP TABLE [dbo].[ReschedulingOfALoanEvents]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetUp_FiscalStatus]') AND type in (N'U'))
DROP TABLE [dbo].[SetUp_FiscalStatus]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetUp_LegalStatus]') AND type in (N'U'))
DROP TABLE [dbo].[SetUp_LegalStatus]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SavingBookProducts]') AND type in (N'U'))
DROP TABLE [dbo].[SavingBookProducts]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TermDepositProducts]') AND type in (N'U'))
DROP TABLE [dbo].[TermDepositProducts]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AlertSettings]') AND type in (N'U'))
DROP TABLE [dbo].[AlertSettings]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AmountCycles]') AND type in (N'U'))
DROP TABLE [dbo].[AmountCycles]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PersonGroupBelonging]') AND type in (N'U'))
DROP TABLE [dbo].[PersonGroupBelonging]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CompulsorySavingsProducts]') AND type in (N'U'))
DROP TABLE [dbo].[CompulsorySavingsProducts]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CompulsorySavingsContracts]') AND type in (N'U'))
DROP TABLE [dbo].[CompulsorySavingsContracts]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[City]') AND type in (N'U'))
DROP TABLE [dbo].[City]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Test]') AND type in (N'U'))
DROP TABLE [dbo].[Test]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FollowUp]') AND type in (N'U'))
DROP TABLE [dbo].[FollowUp]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContractAccountingRules]') AND type in (N'U'))
DROP TABLE [dbo].[ContractAccountingRules]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AccountingRules]') AND type in (N'U'))
DROP TABLE [dbo].[AccountingRules]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FundingLines]') AND type in (N'U'))
DROP TABLE [dbo].[FundingLines]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Accounts]') AND type in (N'U'))
DROP TABLE [dbo].[Accounts]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MovementSet]') AND type in (N'U'))
DROP TABLE [dbo].[MovementSet]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContractEvents]') AND type in (N'U'))
DROP TABLE [dbo].[ContractEvents]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
DROP TABLE [dbo].[Users]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LoanInterestAccruingEvents]') AND type in (N'U'))
DROP TABLE [dbo].[LoanInterestAccruingEvents]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Contracts]') AND type in (N'U'))
DROP TABLE [dbo].[Contracts]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StatisticalProvisoningEvents]') AND type in (N'U'))
DROP TABLE [dbo].[StatisticalProvisoningEvents]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tiers]') AND type in (N'U'))
DROP TABLE [dbo].[Tiers]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SavingProducts]') AND type in (N'U'))
DROP TABLE [dbo].[SavingProducts]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SavingContracts]') AND type in (N'U'))
DROP TABLE [dbo].[SavingContracts]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InstallmentTypes]') AND type in (N'U'))
DROP TABLE [dbo].[InstallmentTypes]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Packages]') AND type in (N'U'))
DROP TABLE [dbo].[Packages]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Roles]') AND type in (N'U'))
DROP TABLE [dbo].[Roles]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Exotics]') AND type in (N'U'))
DROP TABLE [dbo].[Exotics]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DomainOfApplications]') AND type in (N'U'))
DROP TABLE [dbo].[DomainOfApplications]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Banks]') AND type in (N'U'))
DROP TABLE [dbo].[Banks]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Persons]') AND type in (N'U'))
DROP TABLE [dbo].[Persons]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Corporates]') AND type in (N'U'))
DROP TABLE [dbo].[Corporates]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Credit]') AND type in (N'U'))
DROP TABLE [dbo].[Credit]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GuaranteesPackages]') AND type in (N'U'))
DROP TABLE [dbo].[GuaranteesPackages]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Currencies]') AND type in (N'U'))
DROP TABLE [dbo].[Currencies]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Cycles]') AND type in (N'U'))
DROP TABLE [dbo].[Cycles]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Groups]') AND type in (N'U'))
DROP TABLE [dbo].[Groups]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Provinces]') AND type in (N'U'))
DROP TABLE [dbo].[Provinces]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Districts]') AND type in (N'U'))
DROP TABLE [dbo].[Districts]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Projects]') AND type in (N'U'))
DROP TABLE [dbo].[Projects]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LoanShareAmounts]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[LoanShareAmounts](
	[person_id] [int] NOT NULL,
	[group_id] [int] NOT NULL,
	[contract_id] [int] NOT NULL,
	[amount] [money] NOT NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StandardBookings]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[StandardBookings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](128) NULL,
	[debit_account_id] [int] NOT NULL,
	[credit_account_id] [int] NOT NULL,
 CONSTRAINT [inx_uniq_StandardBooking] UNIQUE NONCLUSTERED 
(
	[Name] ASC,
	[debit_account_id] ASC,
	[credit_account_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Cycles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Cycles](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_Cycles] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Currencies]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Currencies](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[is_pivot] [bit] NOT NULL,
	[code] [nvarchar](20) NOT NULL,
	[is_swapped] [bit] NOT NULL DEFAULT ((0)),
	[use_cents] [bit] NOT NULL DEFAULT ((1)),
 CONSTRAINT [PK_Currencies] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VillagesPersons]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[VillagesPersons](
	[village_id] [int] NOT NULL,
	[person_id] [int] NOT NULL,
	[joined_date] [datetime] NOT NULL,
	[left_date] [datetime] NULL,
 CONSTRAINT [PK_VillagesPersons] PRIMARY KEY CLUSTERED 
(
	[village_id] ASC,
	[person_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MFI]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MFI](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](55) NOT NULL,
	[login] [nvarchar](55) NULL,
	[password] [nvarchar](55) NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pictures]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Pictures](
	[group] [nvarchar](50) NOT NULL,
	[id] [int] NOT NULL,
	[subid] [int] NOT NULL CONSTRAINT [DF_Pictures_subid]  DEFAULT ((0)),
	[picture] [image] NOT NULL,
	[thumbnail] [image] NOT NULL,
	[name] [varchar](50) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Provinces]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Provinces](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[deleted] [bit] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_Provinces] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[deleted] [bit] NOT NULL CONSTRAINT [DF_Users_delete]  DEFAULT ((0)),
	[user_name] [nvarchar](50) NOT NULL,
	[user_pass] [nvarchar](50) NOT NULL,
	[role_code] [varchar](5) NOT NULL,
	[first_name] [nvarchar](200) NULL,
	[last_name] [nvarchar](200) NULL,
	[mail] [nvarchar](100) NOT NULL CONSTRAINT [DF_Users_mail]  DEFAULT (N'Not Set'),
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_Users_username_pwd] UNIQUE NONCLUSTERED 
(
	[user_name] ASC,
	[user_pass] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InstallmentTypes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[InstallmentTypes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[nb_of_days] [int] NOT NULL,
	[nb_of_months] [int] NOT NULL,
 CONSTRAINT [PK_InstallmentTypes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StatisticalProvisoningEvents]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[StatisticalProvisoningEvents](
	[id] [int] NOT NULL,
	[olb] [money] NOT NULL,
 CONSTRAINT [PK_StatisticalProvisoningEvents] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LoanScale]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[LoanScale](
	[id] [int] NOT NULL,
	[ScaleMin] [int] NULL,
	[ScaleMax] [int] NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Questionnaire]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Questionnaire](
	[Name] [nvarchar](100) NOT NULL,
	[Country] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[NumberOfClients] [nvarchar](50) NULL CONSTRAINT [DF_Questionnaire_NumberOfClients]  DEFAULT (''),
	[GrossPortfolio] [nvarchar](50) NULL CONSTRAINT [DF_Questionnaire_GrossPortfolio]  DEFAULT (''),
	[PositionInCompony] [nvarchar](100) NULL,
	[BeContacted] [nvarchar](150) NULL,
	[FirstTime] [bit] NOT NULL,
	[DailyActivity] [nvarchar](150) NOT NULL,
	[MainPriorities] [nvarchar](4000) NULL,
	[MainAdvantages] [nvarchar](4000) NULL,
	[OtherMessages] [nvarchar](4000) NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DomainOfApplications]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[DomainOfApplications](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[parent_id] [int] NULL,
	[deleted] [bit] NOT NULL,
 CONSTRAINT [PK_DomainOfApplications] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PersonCustomizableFields]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PersonCustomizableFields](
	[person_id] [int] NOT NULL,
	[key] [int] NOT NULL,
	[value] [nvarchar](100) NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LoanInterestAccruingEvents]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[LoanInterestAccruingEvents](
	[id] [int] NOT NULL,
	[interest_prepayment] [money] NOT NULL,
	[accrued_interest] [money] NOT NULL,
	[rescheduled] [bit] NOT NULL,
	[installment_number] [int] NOT NULL CONSTRAINT [DF_LoanInterestAccruingEvents_installment_number]  DEFAULT ((1)),
 CONSTRAINT [PK_LoanInterestAccruingEvents] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomizableFieldsSettings]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CustomizableFieldsSettings](
	[number] [int] NOT NULL,
	[use] [bit] NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[type] [nvarchar](50) NOT NULL,
	[mandatory] [bit] NOT NULL,
	[unique] [bit] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_CustomizableFields] PRIMARY KEY CLUSTERED 
(
	[number] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Exotics]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Exotics](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_Exotics] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_Exotics_name] UNIQUE NONCLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LinkCollateralCredit]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[LinkCollateralCredit](
	[contract_id] [int] NOT NULL,
	[collateral_id] [int] NOT NULL,
	[collateral_amount] [money] NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PublicHolidays]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PublicHolidays](
	[date] [datetime] NOT NULL,
	[name] [nvarchar](100) NOT NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CorporateEventsType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CorporateEventsType](
	[id] [int] NOT NULL,
	[code] [nvarchar](50) NULL,
 CONSTRAINT [PK_CorporateEventsType] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Roles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Roles](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[code] [nvarchar](50) NOT NULL,
	[deleted] [bit] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Collaterals]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Collaterals](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[parent_id] [int] NULL,
	[deleted] [bit] NOT NULL,
 CONSTRAINT [PK_Collaterals] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MenuItems]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MenuItems](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[menu_name] [nvarchar](50) NOT NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AllowedRoleMenus]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AllowedRoleMenus](
	[menu_item_id] [int] NOT NULL,
	[role_id] [int] NOT NULL,
	[allowed] [bit] NOT NULL,
 CONSTRAINT [PK_AllowedRoleMenus] PRIMARY KEY CLUSTERED 
(
	[menu_item_id] ASC,
	[role_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Banks]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Banks](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NULL,
	[address] [nvarchar](200) NULL,
	[BIC] [nvarchar](50) NULL,
	[IBAN1] [nvarchar](100) NULL,
	[IBAN2] [nvarchar](100) NULL,
	[customIBAN1] [bit] NULL,
	[customIBAN2] [bit] NULL,
 CONSTRAINT [PK_Banks] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProjectPurposes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProjectPurposes](
	[name] [nvarchar](200) NOT NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetUp_ProfessionalSituation]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SetUp_ProfessionalSituation](
	[value] [nvarchar](50) NOT NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetUp_Sponsor2]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SetUp_Sponsor2](
	[value] [nvarchar](50) NOT NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetUp_ActivityState]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SetUp_ActivityState](
	[value] [nvarchar](50) NOT NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetUp_ProfessionalExperience]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SetUp_ProfessionalExperience](
	[value] [nvarchar](50) NOT NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HousingSituation]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[HousingSituation](
	[name] [nvarchar](50) NOT NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Info]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Info](
	[ceo] [nvarchar](50) NULL,
	[accountant] [nvarchar](50) NULL,
	[mfi] [nvarchar](50) NULL,
	[branch] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[cashier] [nvarchar](50) NULL,
	[branchmanager] [nvarchar](50) NULL,
	[branchadress] [nvarchar](50) NULL,
	[BIK] [nvarchar](50) NULL,
	[INN] [nvarchar](50) NULL,
	[AN] [nvarchar](50) NULL,
	[BranchLicense] [nvarchar](100) NULL,
	[LA] [nvarchar](50) NULL,
	[Superviser] [nvarchar](50) NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetUp_HousingSituation]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SetUp_HousingSituation](
	[value] [nvarchar](50) NOT NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TrancheEvents]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TrancheEvents](
	[id] [int] NOT NULL,
	[interest_rate] [money] NOT NULL,
	[amount] [money] NOT NULL,
	[maturity] [int] NOT NULL,
	[start_date] [datetime] NOT NULL,
	[applied_new_interest] [bit] NOT NULL,
	[started_from_installment] [int] NULL,
 CONSTRAINT [PK_TrancheEvents] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetUp_HousingLocation]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SetUp_HousingLocation](
	[value] [nvarchar](50) NOT NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetUp_Sponsor1]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SetUp_Sponsor1](
	[value] [nvarchar](50) NOT NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetUp_SageJournal]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SetUp_SageJournal](
	[product_code] [nvarchar](50) NOT NULL,
	[journal_code] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_SetUp_SageJournal] PRIMARY KEY CLUSTERED 
(
	[product_code] ASC,
	[journal_code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetUp_BankSituation]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SetUp_BankSituation](
	[value] [nvarchar](50) NOT NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetUp_PersonalSituation]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SetUp_PersonalSituation](
	[value] [nvarchar](50) NOT NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetUp_SocialSituation]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SetUp_SocialSituation](
	[value] [nvarchar](50) NOT NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GeneralParameters]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[GeneralParameters](
	[key] [varchar](50) NOT NULL,
	[value] [nvarchar](200) NULL,
 CONSTRAINT [PK_GeneralParameters] PRIMARY KEY CLUSTERED 
(
	[key] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetUp_InsertionTypes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SetUp_InsertionTypes](
	[value] [nvarchar](50) NOT NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TechnicalParameters]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TechnicalParameters](
	[name] [nvarchar](100) NOT NULL,
	[value] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_TechnicalParameters] PRIMARY KEY CLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetUp_SubventionTypes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SetUp_SubventionTypes](
	[value] [nvarchar](50) NOT NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tconso_SP_Octopus_Consolidation_default]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tconso_SP_Octopus_Consolidation_default](
	[branch_code] [nvarchar](50) NULL,
	[country] [nvarchar](50) NULL,
	[contract_code] [nvarchar](50) NOT NULL,
	[client_name] [nvarchar](100) NOT NULL,
	[OLB] [money] NOT NULL,
	[period_number] [int] NULL,
	[period_type] [char](1) NULL,
	[date] [datetime] NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetUp_DwellingPlace]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SetUp_DwellingPlace](
	[value] [nvarchar](50) NOT NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetUp_FamilySituation]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SetUp_FamilySituation](
	[value] [nvarchar](50) NOT NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetUp_BusinessPlan]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SetUp_BusinessPlan](
	[value] [nvarchar](50) NOT NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetUp_StudyLevel]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SetUp_StudyLevel](
	[value] [nvarchar](50) NOT NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetUp_FiscalStatus]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SetUp_FiscalStatus](
	[value] [nvarchar](50) NOT NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetUp_LegalStatus]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SetUp_LegalStatus](
	[value] [nvarchar](50) NOT NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetUp_Registre]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SetUp_Registre](
	[value] [nvarchar](50) NOT NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ExchangeRates]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ExchangeRates](
	[exchange_date] [datetime] NOT NULL,
	[exchange_rate] [float] NOT NULL,
	[currency_id] [int] NOT NULL,
 CONSTRAINT [PK_Exchange_rate] PRIMARY KEY CLUSTERED 
(
	[exchange_date] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProvisioningRules]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProvisioningRules](
	[id] [int] NOT NULL,
	[number_of_days_min] [int] NOT NULL,
	[number_of_days_max] [int] NOT NULL,
	[provisioning_value] [float] NOT NULL,
 CONSTRAINT [PK_ProvisioningRules] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Test]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Test](
	[char_type] [char](1) NULL,
	[varchar_type] [varchar](50) NULL,
	[nvarchar_type] [nvarchar](50) NULL,
	[integer_type] [int] NULL,
	[double_type] [float] NULL,
	[money_type] [money] NULL,
	[boolean_type] [bit] NULL,
	[datetime_type] [datetime] NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AlertSettings]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AlertSettings](
	[parameter] [nvarchar](20) NOT NULL,
	[value] [nvarchar](5) NOT NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AccountingRules]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AccountingRules](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[generic_account_number] [nvarchar](50) NOT NULL,
	[specific_account_number] [nvarchar](50) NOT NULL,
	[rule_type] [char](1) NOT NULL,
	[deleted] [bit] NOT NULL DEFAULT ((0)),
	[booking_direction] [smallint] NOT NULL,
 CONSTRAINT [PK_AccountingRules] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AmountCycles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AmountCycles](
	[cycle_id] [int] NOT NULL,
	[number] [int] NOT NULL,
	[amount_min] [money] NOT NULL,
	[amount_max] [money] NOT NULL,
 CONSTRAINT [PK_AmountCycle] PRIMARY KEY CLUSTERED 
(
	[cycle_id] ASC,
	[number] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Packages]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Packages](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[deleted] [bit] NOT NULL CONSTRAINT [DF__Tmp_Packa__delet__6F2063EF]  DEFAULT ((0)),
	[code] [nvarchar](50) NOT NULL CONSTRAINT [DF_Packages_code]  DEFAULT (N'NotSet'),
	[name] [nvarchar](100) NOT NULL,
	[client_type] [char](1) NULL CONSTRAINT [DF__Tmp_Packa__clien__70148828]  DEFAULT ('-'),
	[amount] [money] NULL,
	[amount_min] [money] NULL,
	[amount_max] [money] NULL,
	[interest_rate] [float] NULL,
	[interest_rate_min] [float] NULL,
	[interest_rate_max] [float] NULL,
	[installment_type] [int] NOT NULL,
	[grace_period] [int] NULL,
	[grace_period_min] [int] NULL,
	[grace_period_max] [int] NULL,
	[number_of_installments] [int] NULL,
	[number_of_installments_min] [int] NULL,
	[number_of_installments_max] [int] NULL,
	[anticipated_total_repayment_penalties] [float] NULL,
	[anticipated_total_repayment_penalties_min] [float] NULL,
	[anticipated_total_repayment_penalties_max] [float] NULL,
	[exotic_id] [int] NULL,
	[entry_fees] [float] NULL,
	[entry_fees_min] [float] NULL,
	[entry_fees_max] [float] NULL,
	[entry_fees_percentage] [bit] NOT NULL DEFAULT ((1)),
	[loan_type] [smallint] NOT NULL,
	[rounding_type] [smallint] NOT NULL DEFAULT ((1)),
	[keep_expected_installment] [bit] NOT NULL,
	[charge_interest_within_grace_period] [bit] NOT NULL,
	[cycle_id] [int] NULL,
	[non_repayment_penalties_based_on_overdue_interest] [float] NULL,
	[non_repayment_penalties_based_on_initial_amount] [float] NULL,
	[non_repayment_penalties_based_on_olb] [float] NULL,
	[non_repayment_penalties_based_on_overdue_principal] [float] NULL,
	[non_repayment_penalties_based_on_overdue_interest_min] [float] NULL,
	[non_repayment_penalties_based_on_initial_amount_min] [float] NULL,
	[non_repayment_penalties_based_on_olb_min] [float] NULL,
	[non_repayment_penalties_based_on_overdue_principal_min] [float] NULL,
	[non_repayment_penalties_based_on_overdue_interest_max] [float] NULL,
	[non_repayment_penalties_based_on_initial_amount_max] [float] NULL,
	[non_repayment_penalties_based_on_olb_max] [float] NULL,
	[non_repayment_penalties_based_on_overdue_principal_max] [float] NULL,
	[fundingLine_id] [int] NULL,
	[fake] [bit] NOT NULL CONSTRAINT [DF__Tmp_Packag__fake__7108AC61]  DEFAULT ((0)),
	[currency_id] [int] NOT NULL,
	[grace_period_of_latefees] [int] NOT NULL DEFAULT ((0)),
	[number_of_drawings_loc] [int] NULL,
	[amount_under_loc] [money] NULL,
	[amount_under_loc_min] [money] NULL,
	[amount_under_loc_max] [money] NULL,
	[maturity_loc] [int] NULL,
	[maturity_loc_min] [int] NULL,
	[maturity_loc_max] [int] NULL,
	[activated_loc] [bit] NOT NULL DEFAULT ((0)),
	[anticipated_partial_repayment_penalties] [float] NULL,
	[anticipated_partial_repayment_penalties_min] [float] NULL,
	[anticipated_partial_repayment_penalties_max] [float] NULL,
	[anticipated_partial_repayment_base] [smallint] NOT NULL DEFAULT ((0)),
	[anticipated_total_repayment_base] [smallint] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_Packages] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_Packages_name] UNIQUE NONCLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Events]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Events](
	[id] [int] NOT NULL,
	[event_type] [char](4) NOT NULL,
	[event_date] [datetime] NOT NULL,
	[user_id] [int] NOT NULL,
	[description] [nvarchar](512) NULL DEFAULT (''),
 CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ElementaryMvts]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ElementaryMvts](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[number] [int] NOT NULL,
	[debit_account_id] [int] NOT NULL,
	[credit_account_id] [int] NOT NULL,
	[amount] [money] NOT NULL,
	[movement_set_id] [int] NOT NULL,
	[is_exported] [bit] NOT NULL CONSTRAINT [DF_ElementaryMvts_Exported]  DEFAULT ((0)),
	[voucher_number] [int] NULL,
	[label] [smallint] NOT NULL CONSTRAINT [DF_ElementaryMvts_label]  DEFAULT ((4)),
	[export_date] [datetime] NULL,
 CONSTRAINT [PK_ElementaryMvtsPK] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Accounts]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Accounts](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[account_number] [nvarchar](50) NOT NULL,
	[local_account_number] [nvarchar](50) NULL,
	[label] [nvarchar](200) NOT NULL,
	[balance] [money] NOT NULL,
	[debit_plus] [bit] NOT NULL,
	[type_code] [varchar](60) NOT NULL,
	[description] [smallint] NOT NULL,
	[type] [bit] NOT NULL DEFAULT ((0)),
	[currency_id] [int] NOT NULL,
	[parent_account_id] [int] NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UK_Accounts] UNIQUE NONCLUSTERED 
(
	[account_number] ASC,
	[currency_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SavingProducts]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SavingProducts](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[deleted] [bit] NOT NULL DEFAULT ((0)),
	[name] [nvarchar](100) NOT NULL,
	[code] [nvarchar](50) NOT NULL,
	[client_type] [char](1) NULL DEFAULT ('-'),
	[initial_amount_min] [money] NULL,
	[initial_amount_max] [money] NULL,
	[balance_min] [money] NULL,
	[balance_max] [money] NULL,
	[withdraw_min] [money] NULL,
	[withdraw_max] [money] NULL,
	[deposit_min] [money] NULL,
	[deposit_max] [money] NULL,
	[transfer_min] [money] NOT NULL,
	[transfer_max] [money] NOT NULL,
	[interest_rate] [float] NULL,
	[interest_rate_min] [float] NULL,
	[interest_rate_max] [float] NULL,
	[entry_fees_min] [money] NULL,
	[entry_fees_max] [money] NULL,
	[entry_fees] [money] NULL,
	[currency_id] [int] NOT NULL,
	[product_type] [char](1) NOT NULL,
 CONSTRAINT [PK_SavingProduct] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_SavingProduct_name] UNIQUE NONCLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FundingLines]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[FundingLines](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[begin_date] [datetime] NOT NULL,
	[end_date] [datetime] NOT NULL,
	[amount] [decimal](18, 0) NOT NULL,
	[purpose] [nvarchar](50) NOT NULL,
	[deleted] [bit] NOT NULL,
	[currency_id] [int] NOT NULL,
 CONSTRAINT [PK_TEMP_FUNDINGLINES_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GuaranteesPackages]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[GuaranteesPackages](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[deleted] [bit] NOT NULL,
	[name] [nvarchar](100) NULL,
	[client_type] [char](1) NULL,
	[amount] [money] NULL,
	[amount_min] [money] NULL,
	[amount_max] [money] NULL,
	[amount_limit] [money] NULL,
	[amount_limit_min] [money] NULL,
	[amount_limit_max] [money] NULL,
	[rate] [float] NULL,
	[rate_min] [float] NULL,
	[rate_max] [float] NULL,
	[rate_limit] [float] NULL,
	[rate_limit_min] [float] NULL,
	[rate_limit_max] [float] NULL,
	[guaranted_amount] [money] NULL,
	[guaranted_amount_min] [money] NULL,
	[guaranted_amount_max] [money] NULL,
	[guarantee_fees] [float] NULL,
	[guarantee_fees_min] [float] NULL,
	[guarantee_fees_max] [float] NULL,
	[fundingLine_id] [int] NULL,
	[currency_id] [int] NOT NULL,
 CONSTRAINT [PK_GaranteesPackages] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MovementSet]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MovementSet](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[transaction_date] [datetime] NOT NULL,
	[user_id] [int] NOT NULL,
 CONSTRAINT [PK_TransactionsTbl] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WriteOffEvents]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WriteOffEvents](
	[id] [int] NOT NULL,
	[olb] [money] NOT NULL CONSTRAINT [DF_WriteOffEvents_olb]  DEFAULT ((0)),
	[accrued_interests] [money] NOT NULL,
	[accrued_penalties] [money] NOT NULL,
	[past_due_days] [int] NOT NULL CONSTRAINT [DF_WriteOffEvents_past_due_days]  DEFAULT ((365)),
 CONSTRAINT [PK_WriteOffEvents] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RepaymentEvents]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RepaymentEvents](
	[id] [int] NOT NULL,
	[past_due_days] [int] NOT NULL,
	[principal] [money] NOT NULL,
	[interests] [money] NOT NULL,
	[voucher_number] [int] NULL,
	[installment_number] [int] NOT NULL,
	[commissions] [money] NOT NULL DEFAULT ((0)),
	[penalties] [money] NOT NULL DEFAULT ((0))
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReschedulingOfALoanEvents]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ReschedulingOfALoanEvents](
	[id] [int] NOT NULL,
	[amount] [money] NOT NULL CONSTRAINT [DF_ReschedulingOfALoanEvents_amount]  DEFAULT ((0)),
	[nb_of_maturity] [int] NOT NULL CONSTRAINT [DF_ReschedulingOfALoanEvents_maturity]  DEFAULT ((0)),
	[date_offset] [int] NOT NULL CONSTRAINT [DF_ReschedulingOfALoanEvents_date_offset]  DEFAULT ((0)),
	[interest] [money] NOT NULL DEFAULT ((0)),
    [grace_period] [int] NOT NULL DEFAULT((0)),
    [charge_interest_during_shift] [bit] NOT NULL DEFAULT((0)),
    [charge_interest_during_grace_period] [bit] NOT NULL DEFAULT((0))
 CONSTRAINT [PK_ReschedulingOfALoanEvents] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PastDueLoanEvents]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PastDueLoanEvents](
	[id] [int] NOT NULL,
	[olb] [money] NOT NULL CONSTRAINT [DF_PastDueLoanEvents_olb]  DEFAULT ((0)),
	[accrued_interests] [money] NOT NULL CONSTRAINT [DF_PastDueLoanEvents_accrued_interests]  DEFAULT ((0)),
	[accrued_penalties] [money] NOT NULL CONSTRAINT [DF_PastDueLoanEvents_accrued_penalties]  DEFAULT ((0)),
	[provisioning_amount] [money] NOT NULL CONSTRAINT [DF_PastDueLoanEvents_provisioning_amount]  DEFAULT ((0)),
	[past_due_days] [int] NOT NULL CONSTRAINT [DF_PastDueLoanEvents_past_due_days]  DEFAULT ((0)),
 CONSTRAINT [PK_PastDueLoanEvents] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LoanDisbursmentEvents]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[LoanDisbursmentEvents](
	[id] [int] NOT NULL,
	[amount] [money] NOT NULL CONSTRAINT [DF_LoanDisbursmentEvents_amount]  DEFAULT ((0)),
	[fees] [money] NOT NULL CONSTRAINT [DF_LoanDisbursmentEvents_fees]  DEFAULT ((0)),
	[voucher_number] [int] NULL,
	[interest] [money] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_EventTbl] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InstallmentHistory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[InstallmentHistory](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[contract_id] [int] NOT NULL,
	[event_id] [int] NOT NULL,
	[number] [int] NOT NULL,
	[expected_date] [datetime] NOT NULL,
	[capital_repayment] [money] NOT NULL,
	[interest_repayment] [money] NOT NULL,
	[paid_interest] [money] NOT NULL,
	[paid_capital] [money] NOT NULL,
	[paid_fees] [money] NOT NULL,
	[fees_unpaid] [money] NOT NULL,
	[paid_date] [datetime] NULL,
	[delete_date] [datetime] NULL,
	[payment_method] [smallint] NULL,
	[comment] [nvarchar](50) NULL,
	[pending] [bit] NOT NULL DEFAULT ((0))
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Districts]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Districts](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[province_id] [int] NOT NULL,
	[deleted] [bit] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_Districts] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CompulsorySavingsContracts]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CompulsorySavingsContracts](
	[id] [int] NOT NULL,
	[loan_id] [int] NULL,
 CONSTRAINT [PK_CompulsorySavingsContracts] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SavingAccountsBalance]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SavingAccountsBalance](
	[saving_id] [int] NOT NULL,
	[account_number] [nvarchar](50) NOT NULL,
	[balance] [money] NOT NULL,
 CONSTRAINT [PK_SavingAccountsBalance] PRIMARY KEY CLUSTERED 
(
	[saving_id] ASC,
	[account_number] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SavingEvents]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SavingEvents](
	[id] [int] NOT NULL,
	[user_id] [int] NOT NULL,
	[contract_id] [int] NOT NULL,
	[code] [char](4) NOT NULL,
	[amount] [money] NOT NULL,
	[description] [nvarchar](200) NULL,
	[deleted] [bit] NOT NULL,
	[creation_date] [datetime] NOT NULL,
	[cancelable] [bit] NOT NULL,
	[is_fired] [bit] NOT NULL,
	[related_contract_code] [nvarchar](50) NULL,
	[fees] [money] NULL,
 CONSTRAINT [PK_SavingEvents] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SavingBookContracts]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SavingBookContracts](
	[id] [int] NOT NULL,
	[flat_withdraw_fees] [money] NULL,
	[rate_withdraw_fees] [float] NULL,
	[flat_transfer_fees] [money] NULL,
	[rate_transfer_fees] [float] NULL,
 CONSTRAINT [PK_SavingBookContracts] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SavingDepositContracts]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SavingDepositContracts](
	[id] [int] NOT NULL,
	[number_periods] [int] NOT NULL,
	[rollover] [smallint] NOT NULL,
	[transfer_account] [nvarchar](50) NULL,
	[withdrawal_fees] [float] NOT NULL,
	[next_maturity] [datetime] NULL,
 CONSTRAINT [PK_SavingDepositContracts] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SavingContracts]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SavingContracts](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[product_id] [int] NOT NULL,
	[user_id] [int] NOT NULL,
	[code] [nvarchar](50) NOT NULL,
	[status] [smallint] NOT NULL,
	[tiers_id] [int] NOT NULL,
	[creation_date] [datetime] NOT NULL,
	[closed_date] [datetime] NULL,
	[interest_rate] [float] NOT NULL,
 CONSTRAINT [PK_SavingContracts] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContractAssignHistory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ContractAssignHistory](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[DateChanged] [datetime] NOT NULL CONSTRAINT [DF_ContractAssignHistory_DateChanged]  DEFAULT (getdate()),
	[loanofficerFrom_id] [int] NOT NULL,
	[loanofficerTo_id] [int] NOT NULL,
	[contract_id] [int] NOT NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Villages]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Villages](
	[id] [int] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[establishment_date] [datetime] NOT NULL,
	[loan_officer] [int] NOT NULL,
 CONSTRAINT [PK_Villages] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContractEvents]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ContractEvents](
	[id] [int] NOT NULL,
	[event_type] [char](4) NOT NULL,
	[contract_id] [int] NOT NULL,
	[event_date] [datetime] NOT NULL,
	[user_id] [int] NOT NULL,
	[is_deleted] [bit] NOT NULL,
	[entry_date] [datetime] NULL DEFAULT (getdate()),
 CONSTRAINT [PK_ContractEvents] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Credit]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Credit](
	[id] [int] NOT NULL,
	[package_id] [int] NOT NULL,
	[amount] [money] NOT NULL,
	[interest_rate] [float] NOT NULL,
	[installment_type] [int] NOT NULL,
	[nb_of_installment] [int] NOT NULL,
	[anticipated_total_repayment_penalties] [float] NOT NULL,
	[disbursed] [bit] NOT NULL,
	[loanofficer_id] [int] NOT NULL,
	[entry_fees] [float] NOT NULL,
	[entry_fees_percentage] [bit] NOT NULL DEFAULT ((1)),
	[grace_period] [int] NULL,
	[written_off] [bit] NOT NULL,
	[rescheduled] [bit] NOT NULL,
	[bad_loan] [bit] NOT NULL,
	[comments_of_end] [nvarchar](1000) NULL,
	[non_repayment_penalties_based_on_overdue_principal] [float] NOT NULL,
	[non_repayment_penalties_based_on_initial_amount] [float] NOT NULL,
	[non_repayment_penalties_based_on_olb] [float] NOT NULL,
	[non_repayment_penalties_based_on_overdue_interest] [float] NOT NULL,
	[fundingLine_id] [int] NOT NULL,
	[fake] [bit] NOT NULL DEFAULT ((0)),
	[synchronize] [bit] NOT NULL CONSTRAINT [DF_Credit_synchronize]  DEFAULT ((0)),
	[interest] [money] NOT NULL DEFAULT ((0)),
	[savings_is_mandatory] [bit] NOT NULL DEFAULT ((0)),
	[grace_period_of_latefees] [int] NOT NULL DEFAULT ((0)),
	[number_of_drawings_loc] [int] NULL,
	[amount_under_loc] [money] NULL,
	[maturity_loc] [int] NULL,
	[anticipated_partial_repayment_penalties] [float] NULL DEFAULT ((0)),
	[anticipated_partial_repayment_base] [smallint] NOT NULL DEFAULT ((0)),
	[anticipated_total_repayment_base] [smallint] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_Credit] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserRole]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserRole](
	[role_id] [int] NOT NULL,
	[user_id] [int] NOT NULL,
	[date_role_set] [datetime] NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Guarantees]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Guarantees](
	[id] [int] NOT NULL,
	[guarantee_package_id] [int] NOT NULL,
	[amount] [money] NULL,
	[amount_limit] [money] NULL,
	[amount_guaranted] [money] NULL,
	[guarantee_fees] [float] NULL,
	[fundingLine_id] [int] NOT NULL,
	[activated] [bit] NOT NULL CONSTRAINT [DF_Guarantees_activated]  DEFAULT ((0)),
	[loanofficer_id] [int] NOT NULL,
	[banque] [nvarchar](50) NULL,
 CONSTRAINT [PK_Garantees] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Installments]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Installments](
	[expected_date] [datetime] NOT NULL,
	[interest_repayment] [money] NOT NULL,
	[capital_repayment] [money] NOT NULL,
	[contract_id] [int] NOT NULL,
	[number] [int] NOT NULL,
	[paid_interest] [money] NOT NULL,
	[paid_capital] [money] NOT NULL,
	[fees_unpaid] [money] NOT NULL CONSTRAINT [DF_Installments_fees_unpaid]  DEFAULT ((0)),
	[paid_date] [datetime] NULL,
	[paid_fees] [money] NOT NULL DEFAULT ((0)),
	[payment_method] [smallint] NULL,
	[comment] [nvarchar](50) NULL,
	[pending] [bit] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_Installments] PRIMARY KEY CLUSTERED 
(
	[contract_id] ASC,
	[number] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TermDepositProducts]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TermDepositProducts](
	[id] [int] NOT NULL,
	[installment_types_id] [int] NOT NULL,
	[number_period] [int] NULL,
	[number_period_min] [int] NULL,
	[number_period_max] [int] NULL,
	[interest_frequency] [smallint] NOT NULL,
	[withdrawal_fees_type] [smallint] NOT NULL,
	[withdrawal_fees] [float] NULL,
	[withdrawal_fees_min] [float] NULL,
	[withdrawal_fees_max] [float] NULL,
 CONSTRAINT [PK_TermDepositProducts] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Corporates]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Corporates](
	[id] [int] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[deleted] [bit] NOT NULL,
	[sigle] [nvarchar](50) NULL,
	[small_name] [nvarchar](50) NULL,
	[volunteer_count] [int] NULL,
	[agrement_date] [datetime] NULL,
	[agrement_solidarity] [bit] NULL,
	[employee_count] [int] NULL,
	[siret] [nvarchar](50) NULL,
	[activity_id] [int] NULL,
	[date_create] [datetime] NULL,
	[fiscal_status] [nvarchar](50) NULL,
	[registre] [nvarchar](50) NULL,
	[legalForm] [nvarchar](50) NULL,
	[insertionType] [nvarchar](50) NULL,
 CONSTRAINT [PK_BODYCORPORATE] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Persons]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Persons](
	[id] [int] NOT NULL,
	[first_name] [nvarchar](100) NOT NULL,
	[sex] [char](1) NOT NULL,
	[identification_data] [nvarchar](200) NOT NULL,
	[last_name] [nvarchar](100) NOT NULL,
	[birth_date] [datetime] NULL,
	[household_head] [bit] NOT NULL,
	[nb_of_dependents] [int] NULL,
	[nb_of_children] [int] NULL,
	[children_basic_education] [int] NULL,
	[livestock_number] [int] NULL,
	[livestock_type] [nvarchar](100) NULL,
	[landplot_size] [float] NULL,
	[home_size] [float] NULL,
	[home_time_living_in] [int] NULL,
	[capital_other_equipments] [nvarchar](500) NULL,
	[activity_id] [int] NULL,
	[experience] [int] NULL,
	[nb_of_people] [int] NULL,
	[monthly_income] [money] NULL,
	[monthly_expenditure] [money] NULL,
	[comments] [nvarchar](500) NULL,
	[image_path] [nvarchar](500) NULL,
	[father_name] [nvarchar](200) NULL,
	[birth_place] [nvarchar](50) NULL,
	[nationality] [nvarchar](50) NULL,
	[study_level] [nvarchar](50) NULL,
	[SS] [nvarchar](50) NULL,
	[CAF] [nvarchar](50) NULL,
	[housing_situation] [nvarchar](50) NULL,
	[bank_situation] [nvarchar](50) NULL,
	[handicapped] [bit] NOT NULL CONSTRAINT [DF_Credit_handicapped]  DEFAULT ((0)),
	[professional_experience] [nvarchar](50) NULL,
	[professional_situation] [nvarchar](50) NULL,
	[first_contact] [datetime] NULL,
	[first_appointment] [datetime] NULL,
	[family_situation] [nvarchar](50) NULL,
	[mother_name] [nvarchar](200) NULL,
	[povertylevel_childreneducation] [int] NOT NULL CONSTRAINT [DF_Persons_povertylevel_childreneducation]  DEFAULT ((0)),
	[povertylevel_economiceducation] [int] NOT NULL CONSTRAINT [DF_Persons_povertylevel_economiceducation]  DEFAULT ((0)),
	[povertylevel_socialparticipation] [int] NOT NULL CONSTRAINT [DF_Persons_povertylevel_socialparticipation]  DEFAULT ((0)),
	[povertylevel_healthsituation] [int] NOT NULL CONSTRAINT [DF_Persons_povertylevel_healthsituation]  DEFAULT ((0)),
	[unemployment_months] [int] NULL,
	[personalBank_id] [int] NULL,
	[businessBank_id] [int] NULL,
 CONSTRAINT [PK_Persons] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_Persons_personal_address_id] UNIQUE NONCLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContractAccountingRules]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ContractAccountingRules](
	[id] [int] NOT NULL,
	[product_type] [smallint] NOT NULL,
	[loan_product_id] [int] NULL,
	[guarantee_product_id] [int] NULL,
	[savings_product_id] [int] NULL,
	[client_type] [char](1) NOT NULL,
	[activity_id] [int] NULL,
 CONSTRAINT [PK_ContractAccountingRules] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ExoticInstallments]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ExoticInstallments](
	[number] [int] NOT NULL,
	[principal_coeff] [float] NOT NULL,
	[interest_coeff] [float] NULL,
	[exotic_id] [int] NOT NULL,
 CONSTRAINT [PK_ExoticInstallments] PRIMARY KEY CLUSTERED 
(
	[number] ASC,
	[exotic_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CorporatePersonBelonging]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CorporatePersonBelonging](
	[corporate_id] [int] NOT NULL,
	[person_id] [int] NOT NULL,
	[role_id] [int] NULL,
 CONSTRAINT [PK_CorporatePersonBelonging] PRIMARY KEY CLUSTERED 
(
	[corporate_id] ASC,
	[person_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tiers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tiers](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[client_type_code] [char](1) NOT NULL,
	[scoring] [float] NULL,
	[loan_cycle] [int] NOT NULL CONSTRAINT [DF_Tiers_loan_cycle]  DEFAULT ((1)),
	[active] [bit] NOT NULL,
	[bad_client] [bit] NOT NULL,
	[other_org_name] [nvarchar](100) NULL,
	[other_org_amount] [money] NULL,
	[other_org_debts] [money] NULL,
	[district_id] [int] NOT NULL,
	[city] [nvarchar](50) NULL,
	[address] [nvarchar](500) NULL,
	[secondary_district_id] [int] NULL,
	[secondary_city] [nvarchar](50) NULL,
	[secondary_address] [nvarchar](500) NULL,
	[cash_input_voucher_number] [int] NULL,
	[cash_output_voucher_number] [int] NULL,
	[creation_date] [datetime] NULL,
	[home_phone] [varchar](50) NULL,
	[personal_phone] [varchar](50) NULL,
	[secondary_home_phone] [varchar](50) NULL,
	[secondary_personal_phone] [varchar](50) NULL,
	[e_mail] [nvarchar](50) NULL,
	[secondary_e_mail] [nvarchar](50) NULL,
	[status] [smallint] NOT NULL CONSTRAINT [DF_Tiers_status]  DEFAULT ((1)),
	[other_org_comment] [nvarchar](500) NULL,
	[sponsor1] [nvarchar](50) NULL,
	[sponsor1_comment] [nvarchar](100) NULL,
	[sponsor2] [nvarchar](50) NULL,
	[sponsor2_comment] [nvarchar](100) NULL,
	[follow_up_comment] [nvarchar](500) NULL,
	[home_type] [nvarchar](50) NULL,
	[secondary_homeType] [nvarchar](50) NULL,
	[zipCode] [nvarchar](50) NULL,
	[secondary_zipCode] [nvarchar](50) NULL,
 CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PersonGroupBelonging]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PersonGroupBelonging](
	[person_id] [int] NOT NULL,
	[group_id] [int] NOT NULL,
	[is_leader] [bit] NOT NULL,
	[currently_in] [bit] NOT NULL,
	[joined_date] [datetime] NOT NULL,
	[left_date] [datetime] NULL,
 CONSTRAINT [PK_PersonGroupBelonging] PRIMARY KEY CLUSTERED 
(
	[person_id] ASC,
	[group_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FollowUp]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[FollowUp](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[project_id] [int] NOT NULL,
	[year] [int] NOT NULL,
	[CA] [money] NOT NULL,
	[Jobs1] [int] NOT NULL,
	[Jobs2] [int] NOT NULL,
	[PersonalSituation] [nvarchar](50) NOT NULL,
	[activity] [nvarchar](50) NOT NULL,
	[comment] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_FollowUp] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Contracts]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Contracts](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[contract_code] [nvarchar](50) NOT NULL,
	[branch_code] [varchar](50) NOT NULL,
	[creation_date] [datetime] NOT NULL,
	[start_date] [datetime] NOT NULL,
	[align_disbursed_date] [datetime] NULL,
	[close_date] [datetime] NOT NULL,
	[rural] [bit] NOT NULL,
	[closed] [bit] NOT NULL,
	[project_id] [int] NOT NULL CONSTRAINT [DF_Contracts_project_id]  DEFAULT ((0)),
	[status] [smallint] NOT NULL CONSTRAINT [DF_Contracts_status]  DEFAULT ((1)),
	[credit_commitee_date] [datetime] NULL,
	[credit_commitee_comment] [nvarchar](4000) NULL,
	[credit_commitee_code] [nvarchar](100) NULL,
 CONSTRAINT [PK_Contracts] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SavingBookProducts]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SavingBookProducts](
	[id] [int] NOT NULL,
	[interest_base] [smallint] NOT NULL,
	[interest_frequency] [smallint] NOT NULL,
	[calcul_amount_base] [smallint] NULL,
	[withdraw_fees_type] [smallint] NOT NULL,
	[flat_withdraw_fees_min] [money] NULL,
	[flat_withdraw_fees_max] [money] NULL,
	[flat_withdraw_fees] [money] NULL,
	[rate_withdraw_fees_min] [float] NULL,
	[rate_withdraw_fees_max] [float] NULL,
	[rate_withdraw_fees] [float] NULL,
	[transfer_fees_type] [smallint] NOT NULL,
	[flat_transfer_fees_min] [money] NULL,
	[flat_transfer_fees_max] [money] NULL,
	[flat_transfer_fees] [money] NULL,
	[rate_transfer_fees_min] [float] NULL,
	[rate_transfer_fees_max] [float] NULL,
	[rate_transfer_fees] [float] NULL,
 CONSTRAINT [PK_SavingBookProducts] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CompulsorySavingsProducts]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CompulsorySavingsProducts](
	[id] [int] NOT NULL,
	[loan_amount_min] [float] NULL,
	[loan_amount_max] [float] NULL,
	[loan_amount] [float] NULL,
 CONSTRAINT [PK_CompulsorySavingsProducts] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FundingLineAccountingRules]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[FundingLineAccountingRules](
	[id] [int] NOT NULL,
	[funding_line_id] [int] NULL,
 CONSTRAINT [PK_FundingLineAccountingRules] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FundingLineEvents]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[FundingLineEvents](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[code] [nvarchar](200) NOT NULL,
	[amount] [money] NOT NULL,
	[direction] [smallint] NOT NULL,
	[fundingline_id] [int] NOT NULL,
	[deleted] [bit] NOT NULL,
	[creation_date] [datetime] NOT NULL,
	[type] [smallint] NOT NULL,
 CONSTRAINT [PK_EVENTFUNDINGLINE] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[City]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[City](
	[name] [nvarchar](100) NOT NULL,
	[district_id] [int] NOT NULL,
	[deleted] [bit] NOT NULL DEFAULT ((0)),
	[id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Projects]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Projects](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[tiers_id] [int] NOT NULL,
	[status] [smallint] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[code] [nvarchar](50) NOT NULL,
	[aim] [nvarchar](200) NOT NULL,
	[begin_date] [datetime] NOT NULL,
	[abilities] [nvarchar](50) NULL,
	[experience] [nvarchar](50) NULL,
	[market] [nvarchar](50) NULL,
	[concurrence] [nvarchar](50) NULL,
	[purpose] [nvarchar](50) NULL,
	[corporate_name] [nvarchar](50) NULL,
	[corporate_juridicStatus] [nvarchar](50) NULL,
	[corporate_FiscalStatus] [nvarchar](50) NULL,
	[corporate_registre] [nvarchar](50) NULL,
	[corporate_siret] [nvarchar](50) NULL,
	[corporate_CA] [money] NULL,
	[corporate_nbOfJobs] [int] NULL,
	[corporate_financialPlanType] [nvarchar](50) NULL,
	[corporateFinancialPlanAmount] [money] NULL,
	[corporate_financialPlanTotalAmount] [money] NULL,
	[address] [nvarchar](20) NULL,
	[city] [nvarchar](50) NULL,
	[zipCode] [nvarchar](50) NULL,
	[district_id] [int] NULL,
	[home_phone] [nvarchar](50) NULL,
	[personalPhone] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[hometype] [nvarchar](50) NULL,
 CONSTRAINT [PK_Projects2] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Groups]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Groups](
	[id] [int] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[establishment_date] [datetime] NULL,
	[comments] [nvarchar](500) NULL,
 CONSTRAINT [PK_ClientGroups] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LinkGuarantorCredit]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[LinkGuarantorCredit](
	[tiers_id] [int] NOT NULL,
	[contract_id] [int] NOT NULL,
	[guarantee_amount] [money] NOT NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContractAccountsBalance]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ContractAccountsBalance](
	[contract_id] [int] NOT NULL,
	[account_number] [nvarchar](50) NOT NULL,
	[balance] [money] NOT NULL,
 CONSTRAINT [PK_ContractAccountsBalance] PRIMARY KEY CLUSTERED 
(
	[contract_id] ASC,
	[account_number] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ActionItems]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ActionItems](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[class_name] [nvarchar](50) NOT NULL,
	[method_name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ActionItems] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AllowedRoleActions]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AllowedRoleActions](
	[action_item_id] [int] NOT NULL,
	[role_id] [int] NOT NULL,
	[allowed] [bit] NOT NULL,
 CONSTRAINT [PK_AllowedRoleActions] PRIMARY KEY CLUSTERED 
(
	[action_item_id] ASC,
	[role_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[dbo].[CK_Packages]') AND parent_object_id = OBJECT_ID(N'[dbo].[Packages]'))
ALTER TABLE [dbo].[Packages]  WITH NOCHECK ADD  CONSTRAINT [CK_Packages] CHECK  (([client_type]='I' OR [client_type]='G' OR [client_type]='-' OR [client_type]='C' OR [client_type]='V'))
GO
ALTER TABLE [dbo].[Packages] CHECK CONSTRAINT [CK_Packages]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[dbo].[CK_TiersTypeCode]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tiers]'))
ALTER TABLE [dbo].[Tiers]  WITH NOCHECK ADD  CONSTRAINT [CK_TiersTypeCode] CHECK  (([client_type_code]='G' OR [client_type_code]='I' OR [client_type_code]='C' OR [client_type_code]='V'))
GO
ALTER TABLE [dbo].[Tiers] CHECK CONSTRAINT [CK_TiersTypeCode]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Accounts_Accounts]') AND parent_object_id = OBJECT_ID(N'[dbo].[Accounts]'))
ALTER TABLE [dbo].[Accounts]  WITH NOCHECK ADD  CONSTRAINT [FK_Accounts_Accounts] FOREIGN KEY([parent_account_id])
REFERENCES [dbo].[Accounts] ([id])
GO
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [FK_Accounts_Accounts]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Accounts_Currencies]') AND parent_object_id = OBJECT_ID(N'[dbo].[Accounts]'))
ALTER TABLE [dbo].[Accounts]  WITH NOCHECK ADD  CONSTRAINT [FK_Accounts_Currencies] FOREIGN KEY([currency_id])
REFERENCES [dbo].[Currencies] ([id])
GO
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [FK_Accounts_Currencies]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AmountCycles_Cycles]') AND parent_object_id = OBJECT_ID(N'[dbo].[AmountCycles]'))
ALTER TABLE [dbo].[AmountCycles]  WITH CHECK ADD  CONSTRAINT [FK_AmountCycles_Cycles] FOREIGN KEY([cycle_id])
REFERENCES [dbo].[Cycles] ([id])
GO
ALTER TABLE [dbo].[AmountCycles] CHECK CONSTRAINT [FK_AmountCycles_Cycles]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_City_Districts]') AND parent_object_id = OBJECT_ID(N'[dbo].[City]'))
ALTER TABLE [dbo].[City]  WITH CHECK ADD  CONSTRAINT [FK_City_Districts] FOREIGN KEY([district_id])
REFERENCES [dbo].[Districts] ([id])
GO
ALTER TABLE [dbo].[City] CHECK CONSTRAINT [FK_City_Districts]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Collaterals_Collaterals]') AND parent_object_id = OBJECT_ID(N'[dbo].[Collaterals]'))
ALTER TABLE [dbo].[Collaterals]  WITH NOCHECK ADD  CONSTRAINT [FK_Collaterals_Collaterals] FOREIGN KEY([parent_id])
REFERENCES [dbo].[Collaterals] ([id])
GO
ALTER TABLE [dbo].[Collaterals] CHECK CONSTRAINT [FK_Collaterals_Collaterals]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CompulsorySavingsContracts_Contracts]') AND parent_object_id = OBJECT_ID(N'[dbo].[CompulsorySavingsContracts]'))
ALTER TABLE [dbo].[CompulsorySavingsContracts]  WITH CHECK ADD  CONSTRAINT [FK_CompulsorySavingsContracts_Contracts] FOREIGN KEY([loan_id])
REFERENCES [dbo].[Contracts] ([id])
GO
ALTER TABLE [dbo].[CompulsorySavingsContracts] CHECK CONSTRAINT [FK_CompulsorySavingsContracts_Contracts]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CompulsorySavingsContracts_SavingContracts]') AND parent_object_id = OBJECT_ID(N'[dbo].[CompulsorySavingsContracts]'))
ALTER TABLE [dbo].[CompulsorySavingsContracts]  WITH CHECK ADD  CONSTRAINT [FK_CompulsorySavingsContracts_SavingContracts] FOREIGN KEY([id])
REFERENCES [dbo].[SavingContracts] ([id])
GO
ALTER TABLE [dbo].[CompulsorySavingsContracts] CHECK CONSTRAINT [FK_CompulsorySavingsContracts_SavingContracts]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CompulsorySavingsProducts_SavingProducts]') AND parent_object_id = OBJECT_ID(N'[dbo].[CompulsorySavingsProducts]'))
ALTER TABLE [dbo].[CompulsorySavingsProducts]  WITH CHECK ADD  CONSTRAINT [FK_CompulsorySavingsProducts_SavingProducts] FOREIGN KEY([id])
REFERENCES [dbo].[SavingProducts] ([id])
GO
ALTER TABLE [dbo].[CompulsorySavingsProducts] CHECK CONSTRAINT [FK_CompulsorySavingsProducts_SavingProducts]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ContractAccountingRules_AccountingRules]') AND parent_object_id = OBJECT_ID(N'[dbo].[ContractAccountingRules]'))
ALTER TABLE [dbo].[ContractAccountingRules]  WITH CHECK ADD  CONSTRAINT [FK_ContractAccountingRules_AccountingRules] FOREIGN KEY([id])
REFERENCES [dbo].[AccountingRules] ([id])
GO
ALTER TABLE [dbo].[ContractAccountingRules] CHECK CONSTRAINT [FK_ContractAccountingRules_AccountingRules]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ContractAccountingRules_DomainOfApplications]') AND parent_object_id = OBJECT_ID(N'[dbo].[ContractAccountingRules]'))
ALTER TABLE [dbo].[ContractAccountingRules]  WITH CHECK ADD  CONSTRAINT [FK_ContractAccountingRules_DomainOfApplications] FOREIGN KEY([activity_id])
REFERENCES [dbo].[DomainOfApplications] ([id])
GO
ALTER TABLE [dbo].[ContractAccountingRules] CHECK CONSTRAINT [FK_ContractAccountingRules_DomainOfApplications]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ContractAccountingRules_GuaranteesPackages]') AND parent_object_id = OBJECT_ID(N'[dbo].[ContractAccountingRules]'))
ALTER TABLE [dbo].[ContractAccountingRules]  WITH CHECK ADD  CONSTRAINT [FK_ContractAccountingRules_GuaranteesPackages] FOREIGN KEY([guarantee_product_id])
REFERENCES [dbo].[GuaranteesPackages] ([id])
GO
ALTER TABLE [dbo].[ContractAccountingRules] CHECK CONSTRAINT [FK_ContractAccountingRules_GuaranteesPackages]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ContractAccountingRules_Packages]') AND parent_object_id = OBJECT_ID(N'[dbo].[ContractAccountingRules]'))
ALTER TABLE [dbo].[ContractAccountingRules]  WITH CHECK ADD  CONSTRAINT [FK_ContractAccountingRules_Packages] FOREIGN KEY([loan_product_id])
REFERENCES [dbo].[Packages] ([id])
GO
ALTER TABLE [dbo].[ContractAccountingRules] CHECK CONSTRAINT [FK_ContractAccountingRules_Packages]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ContractAccountingRules_SavingProducts]') AND parent_object_id = OBJECT_ID(N'[dbo].[ContractAccountingRules]'))
ALTER TABLE [dbo].[ContractAccountingRules]  WITH CHECK ADD  CONSTRAINT [FK_ContractAccountingRules_SavingProducts] FOREIGN KEY([savings_product_id])
REFERENCES [dbo].[SavingProducts] ([id])
GO
ALTER TABLE [dbo].[ContractAccountingRules] CHECK CONSTRAINT [FK_ContractAccountingRules_SavingProducts]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ContractAccountsBalance_Contracts]') AND parent_object_id = OBJECT_ID(N'[dbo].[ContractAccountsBalance]'))
ALTER TABLE [dbo].[ContractAccountsBalance]  WITH NOCHECK ADD  CONSTRAINT [FK_ContractAccountsBalance_Contracts] FOREIGN KEY([contract_id])
REFERENCES [dbo].[Contracts] ([id])
GO
ALTER TABLE [dbo].[ContractAccountsBalance] CHECK CONSTRAINT [FK_ContractAccountsBalance_Contracts]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ContractAssignHistory_Contracts]') AND parent_object_id = OBJECT_ID(N'[dbo].[ContractAssignHistory]'))
ALTER TABLE [dbo].[ContractAssignHistory]  WITH CHECK ADD  CONSTRAINT [FK_ContractAssignHistory_Contracts] FOREIGN KEY([contract_id])
REFERENCES [dbo].[Contracts] ([id])
GO
ALTER TABLE [dbo].[ContractAssignHistory] CHECK CONSTRAINT [FK_ContractAssignHistory_Contracts]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ContractAssignHistory_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[ContractAssignHistory]'))
ALTER TABLE [dbo].[ContractAssignHistory]  WITH CHECK ADD  CONSTRAINT [FK_ContractAssignHistory_Users] FOREIGN KEY([loanofficerFrom_id])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[ContractAssignHistory] CHECK CONSTRAINT [FK_ContractAssignHistory_Users]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ContractAssignHistory_Users1]') AND parent_object_id = OBJECT_ID(N'[dbo].[ContractAssignHistory]'))
ALTER TABLE [dbo].[ContractAssignHistory]  WITH CHECK ADD  CONSTRAINT [FK_ContractAssignHistory_Users1] FOREIGN KEY([loanofficerTo_id])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[ContractAssignHistory] CHECK CONSTRAINT [FK_ContractAssignHistory_Users1]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ContractEvents_Contracts]') AND parent_object_id = OBJECT_ID(N'[dbo].[ContractEvents]'))
ALTER TABLE [dbo].[ContractEvents]  WITH NOCHECK ADD  CONSTRAINT [FK_ContractEvents_Contracts] FOREIGN KEY([contract_id])
REFERENCES [dbo].[Contracts] ([id])
GO
ALTER TABLE [dbo].[ContractEvents] CHECK CONSTRAINT [FK_ContractEvents_Contracts]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ContractEvents_LoanInterestAccruingEvents]') AND parent_object_id = OBJECT_ID(N'[dbo].[ContractEvents]'))
ALTER TABLE [dbo].[ContractEvents]  WITH NOCHECK ADD  CONSTRAINT [FK_ContractEvents_LoanInterestAccruingEvents] FOREIGN KEY([id])
REFERENCES [dbo].[LoanInterestAccruingEvents] ([id])
NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[ContractEvents] NOCHECK CONSTRAINT [FK_ContractEvents_LoanInterestAccruingEvents]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ContractEvents_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[ContractEvents]'))
ALTER TABLE [dbo].[ContractEvents]  WITH NOCHECK ADD  CONSTRAINT [FK_ContractEvents_Users] FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[ContractEvents] CHECK CONSTRAINT [FK_ContractEvents_Users]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Contracts_Projects]') AND parent_object_id = OBJECT_ID(N'[dbo].[Contracts]'))
ALTER TABLE [dbo].[Contracts]  WITH CHECK ADD  CONSTRAINT [FK_Contracts_Projects] FOREIGN KEY([project_id])
REFERENCES [dbo].[Projects] ([id])
GO
ALTER TABLE [dbo].[Contracts] CHECK CONSTRAINT [FK_Contracts_Projects]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CorporatePersonBelonging_Corporates]') AND parent_object_id = OBJECT_ID(N'[dbo].[CorporatePersonBelonging]'))
ALTER TABLE [dbo].[CorporatePersonBelonging]  WITH CHECK ADD  CONSTRAINT [FK_CorporatePersonBelonging_Corporates] FOREIGN KEY([corporate_id])
REFERENCES [dbo].[Corporates] ([id])
GO
ALTER TABLE [dbo].[CorporatePersonBelonging] CHECK CONSTRAINT [FK_CorporatePersonBelonging_Corporates]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CorporatePersonBelonging_Persons]') AND parent_object_id = OBJECT_ID(N'[dbo].[CorporatePersonBelonging]'))
ALTER TABLE [dbo].[CorporatePersonBelonging]  WITH CHECK ADD  CONSTRAINT [FK_CorporatePersonBelonging_Persons] FOREIGN KEY([person_id])
REFERENCES [dbo].[Persons] ([id])
GO
ALTER TABLE [dbo].[CorporatePersonBelonging] CHECK CONSTRAINT [FK_CorporatePersonBelonging_Persons]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Corporates_DomainOfApplications]') AND parent_object_id = OBJECT_ID(N'[dbo].[Corporates]'))
ALTER TABLE [dbo].[Corporates]  WITH CHECK ADD  CONSTRAINT [FK_Corporates_DomainOfApplications] FOREIGN KEY([activity_id])
REFERENCES [dbo].[DomainOfApplications] ([id])
GO
ALTER TABLE [dbo].[Corporates] CHECK CONSTRAINT [FK_Corporates_DomainOfApplications]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Credit_Contracts]') AND parent_object_id = OBJECT_ID(N'[dbo].[Credit]'))
ALTER TABLE [dbo].[Credit]  WITH NOCHECK ADD  CONSTRAINT [FK_Credit_Contracts] FOREIGN KEY([id])
REFERENCES [dbo].[Contracts] ([id])
GO
ALTER TABLE [dbo].[Credit] CHECK CONSTRAINT [FK_Credit_Contracts]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Credit_InstallmentTypes]') AND parent_object_id = OBJECT_ID(N'[dbo].[Credit]'))
ALTER TABLE [dbo].[Credit]  WITH NOCHECK ADD  CONSTRAINT [FK_Credit_InstallmentTypes] FOREIGN KEY([installment_type])
REFERENCES [dbo].[InstallmentTypes] ([id])
GO
ALTER TABLE [dbo].[Credit] CHECK CONSTRAINT [FK_Credit_InstallmentTypes]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Credit_Packages]') AND parent_object_id = OBJECT_ID(N'[dbo].[Credit]'))
ALTER TABLE [dbo].[Credit]  WITH NOCHECK ADD  CONSTRAINT [FK_Credit_Packages] FOREIGN KEY([package_id])
REFERENCES [dbo].[Packages] ([id])
GO
ALTER TABLE [dbo].[Credit] CHECK CONSTRAINT [FK_Credit_Packages]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Credit_Temp_FundingLines]') AND parent_object_id = OBJECT_ID(N'[dbo].[Credit]'))
ALTER TABLE [dbo].[Credit]  WITH NOCHECK ADD  CONSTRAINT [FK_Credit_Temp_FundingLines] FOREIGN KEY([fundingLine_id])
REFERENCES [dbo].[FundingLines] ([id])
NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[Credit] CHECK CONSTRAINT [FK_Credit_Temp_FundingLines]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Credit_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[Credit]'))
ALTER TABLE [dbo].[Credit]  WITH NOCHECK ADD  CONSTRAINT [FK_Credit_Users] FOREIGN KEY([loanofficer_id])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[Credit] NOCHECK CONSTRAINT [FK_Credit_Users]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Districts_Provinces]') AND parent_object_id = OBJECT_ID(N'[dbo].[Districts]'))
ALTER TABLE [dbo].[Districts]  WITH NOCHECK ADD  CONSTRAINT [FK_Districts_Provinces] FOREIGN KEY([province_id])
REFERENCES [dbo].[Provinces] ([id])
GO
ALTER TABLE [dbo].[Districts] CHECK CONSTRAINT [FK_Districts_Provinces]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DomainOfApplications_DomainOfApplications]') AND parent_object_id = OBJECT_ID(N'[dbo].[DomainOfApplications]'))
ALTER TABLE [dbo].[DomainOfApplications]  WITH NOCHECK ADD  CONSTRAINT [FK_DomainOfApplications_DomainOfApplications] FOREIGN KEY([parent_id])
REFERENCES [dbo].[DomainOfApplications] ([id])
GO
ALTER TABLE [dbo].[DomainOfApplications] CHECK CONSTRAINT [FK_DomainOfApplications_DomainOfApplications]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ElementaryMvts_Credit_Accounts]') AND parent_object_id = OBJECT_ID(N'[dbo].[ElementaryMvts]'))
ALTER TABLE [dbo].[ElementaryMvts]  WITH NOCHECK ADD  CONSTRAINT [FK_ElementaryMvts_Credit_Accounts] FOREIGN KEY([credit_account_id])
REFERENCES [dbo].[Accounts] ([id])
GO
ALTER TABLE [dbo].[ElementaryMvts] CHECK CONSTRAINT [FK_ElementaryMvts_Credit_Accounts]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ElementaryMvts_Debit_Accounts]') AND parent_object_id = OBJECT_ID(N'[dbo].[ElementaryMvts]'))
ALTER TABLE [dbo].[ElementaryMvts]  WITH NOCHECK ADD  CONSTRAINT [FK_ElementaryMvts_Debit_Accounts] FOREIGN KEY([debit_account_id])
REFERENCES [dbo].[Accounts] ([id])
GO
ALTER TABLE [dbo].[ElementaryMvts] CHECK CONSTRAINT [FK_ElementaryMvts_Debit_Accounts]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ElementaryMvts_Transactions1]') AND parent_object_id = OBJECT_ID(N'[dbo].[ElementaryMvts]'))
ALTER TABLE [dbo].[ElementaryMvts]  WITH CHECK ADD  CONSTRAINT [FK_ElementaryMvts_Transactions1] FOREIGN KEY([movement_set_id])
REFERENCES [dbo].[MovementSet] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ElementaryMvts] CHECK CONSTRAINT [FK_ElementaryMvts_Transactions1]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Events_MovementSet]') AND parent_object_id = OBJECT_ID(N'[dbo].[Events]'))
ALTER TABLE [dbo].[Events]  WITH CHECK ADD  CONSTRAINT [FK_Events_MovementSet] FOREIGN KEY([id])
REFERENCES [dbo].[MovementSet] ([id])
GO
ALTER TABLE [dbo].[Events] CHECK CONSTRAINT [FK_Events_MovementSet]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Events_StatisticalProvisoningEvents]') AND parent_object_id = OBJECT_ID(N'[dbo].[Events]'))
ALTER TABLE [dbo].[Events]  WITH NOCHECK ADD  CONSTRAINT [FK_Events_StatisticalProvisoningEvents] FOREIGN KEY([id])
REFERENCES [dbo].[StatisticalProvisoningEvents] ([id])
GO
ALTER TABLE [dbo].[Events] NOCHECK CONSTRAINT [FK_Events_StatisticalProvisoningEvents]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Events_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[Events]'))
ALTER TABLE [dbo].[Events]  WITH CHECK ADD  CONSTRAINT [FK_Events_Users] FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[Events] CHECK CONSTRAINT [FK_Events_Users]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ExoticInstallments_Exotics]') AND parent_object_id = OBJECT_ID(N'[dbo].[ExoticInstallments]'))
ALTER TABLE [dbo].[ExoticInstallments]  WITH NOCHECK ADD  CONSTRAINT [FK_ExoticInstallments_Exotics] FOREIGN KEY([exotic_id])
REFERENCES [dbo].[Exotics] ([id])
GO
ALTER TABLE [dbo].[ExoticInstallments] CHECK CONSTRAINT [FK_ExoticInstallments_Exotics]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_FollowUp_Projects]') AND parent_object_id = OBJECT_ID(N'[dbo].[FollowUp]'))
ALTER TABLE [dbo].[FollowUp]  WITH CHECK ADD  CONSTRAINT [FK_FollowUp_Projects] FOREIGN KEY([project_id])
REFERENCES [dbo].[Projects] ([id])
GO
ALTER TABLE [dbo].[FollowUp] CHECK CONSTRAINT [FK_FollowUp_Projects]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_FundingLineAccountingRules_AccountingRules]') AND parent_object_id = OBJECT_ID(N'[dbo].[FundingLineAccountingRules]'))
ALTER TABLE [dbo].[FundingLineAccountingRules]  WITH CHECK ADD  CONSTRAINT [FK_FundingLineAccountingRules_AccountingRules] FOREIGN KEY([id])
REFERENCES [dbo].[AccountingRules] ([id])
GO
ALTER TABLE [dbo].[FundingLineAccountingRules] CHECK CONSTRAINT [FK_FundingLineAccountingRules_AccountingRules]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_FundingLineAccountingRules_FundingLine]') AND parent_object_id = OBJECT_ID(N'[dbo].[FundingLineAccountingRules]'))
ALTER TABLE [dbo].[FundingLineAccountingRules]  WITH CHECK ADD  CONSTRAINT [FK_FundingLineAccountingRules_FundingLine] FOREIGN KEY([funding_line_id])
REFERENCES [dbo].[FundingLines] ([id])
GO
ALTER TABLE [dbo].[FundingLineAccountingRules] CHECK CONSTRAINT [FK_FundingLineAccountingRules_FundingLine]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_FundingLineEvents_FundingLines]') AND parent_object_id = OBJECT_ID(N'[dbo].[FundingLineEvents]'))
ALTER TABLE [dbo].[FundingLineEvents]  WITH NOCHECK ADD  CONSTRAINT [FK_FundingLineEvents_FundingLines] FOREIGN KEY([fundingline_id])
REFERENCES [dbo].[FundingLines] ([id])
NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[FundingLineEvents] CHECK CONSTRAINT [FK_FundingLineEvents_FundingLines]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_FundingLines_Currencies]') AND parent_object_id = OBJECT_ID(N'[dbo].[FundingLines]'))
ALTER TABLE [dbo].[FundingLines]  WITH NOCHECK ADD  CONSTRAINT [FK_FundingLines_Currencies] FOREIGN KEY([currency_id])
REFERENCES [dbo].[Currencies] ([id])
GO
ALTER TABLE [dbo].[FundingLines] CHECK CONSTRAINT [FK_FundingLines_Currencies]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Groups_Tiers]') AND parent_object_id = OBJECT_ID(N'[dbo].[Groups]'))
ALTER TABLE [dbo].[Groups]  WITH NOCHECK ADD  CONSTRAINT [FK_Groups_Tiers] FOREIGN KEY([id])
REFERENCES [dbo].[Tiers] ([id])
GO
ALTER TABLE [dbo].[Groups] NOCHECK CONSTRAINT [FK_Groups_Tiers]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Garantees_GaranteesPackages]') AND parent_object_id = OBJECT_ID(N'[dbo].[Guarantees]'))
ALTER TABLE [dbo].[Guarantees]  WITH CHECK ADD  CONSTRAINT [FK_Garantees_GaranteesPackages] FOREIGN KEY([guarantee_package_id])
REFERENCES [dbo].[GuaranteesPackages] ([id])
GO
ALTER TABLE [dbo].[Guarantees] CHECK CONSTRAINT [FK_Garantees_GaranteesPackages]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Guarantees_Contracts]') AND parent_object_id = OBJECT_ID(N'[dbo].[Guarantees]'))
ALTER TABLE [dbo].[Guarantees]  WITH CHECK ADD  CONSTRAINT [FK_Guarantees_Contracts] FOREIGN KEY([id])
REFERENCES [dbo].[Contracts] ([id])
GO
ALTER TABLE [dbo].[Guarantees] CHECK CONSTRAINT [FK_Guarantees_Contracts]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Guarantees_FundingLines]') AND parent_object_id = OBJECT_ID(N'[dbo].[Guarantees]'))
ALTER TABLE [dbo].[Guarantees]  WITH CHECK ADD  CONSTRAINT [FK_Guarantees_FundingLines] FOREIGN KEY([fundingLine_id])
REFERENCES [dbo].[FundingLines] ([id])
GO
ALTER TABLE [dbo].[Guarantees] CHECK CONSTRAINT [FK_Guarantees_FundingLines]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Guarantees_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[Guarantees]'))
ALTER TABLE [dbo].[Guarantees]  WITH CHECK ADD  CONSTRAINT [FK_Guarantees_Users] FOREIGN KEY([loanofficer_id])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[Guarantees] CHECK CONSTRAINT [FK_Guarantees_Users]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GaranteesPackages_FundingLines]') AND parent_object_id = OBJECT_ID(N'[dbo].[GuaranteesPackages]'))
ALTER TABLE [dbo].[GuaranteesPackages]  WITH CHECK ADD  CONSTRAINT [FK_GaranteesPackages_FundingLines] FOREIGN KEY([fundingLine_id])
REFERENCES [dbo].[FundingLines] ([id])
GO
ALTER TABLE [dbo].[GuaranteesPackages] CHECK CONSTRAINT [FK_GaranteesPackages_FundingLines]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GuaranteesPackages_Currencies]') AND parent_object_id = OBJECT_ID(N'[dbo].[GuaranteesPackages]'))
ALTER TABLE [dbo].[GuaranteesPackages]  WITH NOCHECK ADD  CONSTRAINT [FK_GuaranteesPackages_Currencies] FOREIGN KEY([currency_id])
REFERENCES [dbo].[Currencies] ([id])
GO
ALTER TABLE [dbo].[GuaranteesPackages] CHECK CONSTRAINT [FK_GuaranteesPackages_Currencies]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_InstallmentHistory_ContractEvents]') AND parent_object_id = OBJECT_ID(N'[dbo].[InstallmentHistory]'))
ALTER TABLE [dbo].[InstallmentHistory]  WITH CHECK ADD  CONSTRAINT [FK_InstallmentHistory_ContractEvents] FOREIGN KEY([event_id])
REFERENCES [dbo].[ContractEvents] ([id])
GO
ALTER TABLE [dbo].[InstallmentHistory] CHECK CONSTRAINT [FK_InstallmentHistory_ContractEvents]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Installments_Credit]') AND parent_object_id = OBJECT_ID(N'[dbo].[Installments]'))
ALTER TABLE [dbo].[Installments]  WITH NOCHECK ADD  CONSTRAINT [FK_Installments_Credit] FOREIGN KEY([contract_id])
REFERENCES [dbo].[Credit] ([id])
GO
ALTER TABLE [dbo].[Installments] NOCHECK CONSTRAINT [FK_Installments_Credit]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_LinkGuarantorCredit_Contracts]') AND parent_object_id = OBJECT_ID(N'[dbo].[LinkGuarantorCredit]'))
ALTER TABLE [dbo].[LinkGuarantorCredit]  WITH NOCHECK ADD  CONSTRAINT [FK_LinkGuarantorCredit_Contracts] FOREIGN KEY([contract_id])
REFERENCES [dbo].[Contracts] ([id])
GO
ALTER TABLE [dbo].[LinkGuarantorCredit] CHECK CONSTRAINT [FK_LinkGuarantorCredit_Contracts]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_LinkGuarantorCredit_Tiers]') AND parent_object_id = OBJECT_ID(N'[dbo].[LinkGuarantorCredit]'))
ALTER TABLE [dbo].[LinkGuarantorCredit]  WITH NOCHECK ADD  CONSTRAINT [FK_LinkGuarantorCredit_Tiers] FOREIGN KEY([tiers_id])
REFERENCES [dbo].[Tiers] ([id])
GO
ALTER TABLE [dbo].[LinkGuarantorCredit] CHECK CONSTRAINT [FK_LinkGuarantorCredit_Tiers]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CreditOrigEvents_ContractEvents]') AND parent_object_id = OBJECT_ID(N'[dbo].[LoanDisbursmentEvents]'))
ALTER TABLE [dbo].[LoanDisbursmentEvents]  WITH CHECK ADD  CONSTRAINT [FK_CreditOrigEvents_ContractEvents] FOREIGN KEY([id])
REFERENCES [dbo].[ContractEvents] ([id])
GO
ALTER TABLE [dbo].[LoanDisbursmentEvents] CHECK CONSTRAINT [FK_CreditOrigEvents_ContractEvents]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MovementSet_ContractEvents]') AND parent_object_id = OBJECT_ID(N'[dbo].[MovementSet]'))
ALTER TABLE [dbo].[MovementSet]  WITH NOCHECK ADD  CONSTRAINT [FK_MovementSet_ContractEvents] FOREIGN KEY([id])
REFERENCES [dbo].[ContractEvents] ([id])
GO
ALTER TABLE [dbo].[MovementSet] NOCHECK CONSTRAINT [FK_MovementSet_ContractEvents]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MovementSet_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[MovementSet]'))
ALTER TABLE [dbo].[MovementSet]  WITH NOCHECK ADD  CONSTRAINT [FK_MovementSet_Users] FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[MovementSet] CHECK CONSTRAINT [FK_MovementSet_Users]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Packages_Currencies]') AND parent_object_id = OBJECT_ID(N'[dbo].[Packages]'))
ALTER TABLE [dbo].[Packages]  WITH NOCHECK ADD  CONSTRAINT [FK_Packages_Currencies] FOREIGN KEY([currency_id])
REFERENCES [dbo].[Currencies] ([id])
GO
ALTER TABLE [dbo].[Packages] CHECK CONSTRAINT [FK_Packages_Currencies]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Packages_Cycles]') AND parent_object_id = OBJECT_ID(N'[dbo].[Packages]'))
ALTER TABLE [dbo].[Packages]  WITH NOCHECK ADD  CONSTRAINT [FK_Packages_Cycles] FOREIGN KEY([cycle_id])
REFERENCES [dbo].[Cycles] ([id])
GO
ALTER TABLE [dbo].[Packages] NOCHECK CONSTRAINT [FK_Packages_Cycles]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Packages_Exotics]') AND parent_object_id = OBJECT_ID(N'[dbo].[Packages]'))
ALTER TABLE [dbo].[Packages]  WITH NOCHECK ADD  CONSTRAINT [FK_Packages_Exotics] FOREIGN KEY([exotic_id])
REFERENCES [dbo].[Exotics] ([id])
GO
ALTER TABLE [dbo].[Packages] NOCHECK CONSTRAINT [FK_Packages_Exotics]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Packages_InstallmentTypes]') AND parent_object_id = OBJECT_ID(N'[dbo].[Packages]'))
ALTER TABLE [dbo].[Packages]  WITH NOCHECK ADD  CONSTRAINT [FK_Packages_InstallmentTypes] FOREIGN KEY([installment_type])
REFERENCES [dbo].[InstallmentTypes] ([id])
GO
ALTER TABLE [dbo].[Packages] NOCHECK CONSTRAINT [FK_Packages_InstallmentTypes]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PastDueLoanEvents_ContractEvents]') AND parent_object_id = OBJECT_ID(N'[dbo].[PastDueLoanEvents]'))
ALTER TABLE [dbo].[PastDueLoanEvents]  WITH NOCHECK ADD  CONSTRAINT [FK_PastDueLoanEvents_ContractEvents] FOREIGN KEY([id])
REFERENCES [dbo].[ContractEvents] ([id])
GO
ALTER TABLE [dbo].[PastDueLoanEvents] NOCHECK CONSTRAINT [FK_PastDueLoanEvents_ContractEvents]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PersonGroupBelonging_Persons1]') AND parent_object_id = OBJECT_ID(N'[dbo].[PersonGroupBelonging]'))
ALTER TABLE [dbo].[PersonGroupBelonging]  WITH NOCHECK ADD  CONSTRAINT [FK_PersonGroupBelonging_Persons1] FOREIGN KEY([person_id])
REFERENCES [dbo].[Persons] ([id])
GO
ALTER TABLE [dbo].[PersonGroupBelonging] CHECK CONSTRAINT [FK_PersonGroupBelonging_Persons1]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PersonGroupCorrespondance_Groups]') AND parent_object_id = OBJECT_ID(N'[dbo].[PersonGroupBelonging]'))
ALTER TABLE [dbo].[PersonGroupBelonging]  WITH NOCHECK ADD  CONSTRAINT [FK_PersonGroupCorrespondance_Groups] FOREIGN KEY([group_id])
REFERENCES [dbo].[Groups] ([id])
GO
ALTER TABLE [dbo].[PersonGroupBelonging] CHECK CONSTRAINT [FK_PersonGroupCorrespondance_Groups]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Persons_Banks]') AND parent_object_id = OBJECT_ID(N'[dbo].[Persons]'))
ALTER TABLE [dbo].[Persons]  WITH CHECK ADD  CONSTRAINT [FK_Persons_Banks] FOREIGN KEY([personalBank_id])
REFERENCES [dbo].[Banks] ([id])
GO
ALTER TABLE [dbo].[Persons] CHECK CONSTRAINT [FK_Persons_Banks]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Persons_Banks1]') AND parent_object_id = OBJECT_ID(N'[dbo].[Persons]'))
ALTER TABLE [dbo].[Persons]  WITH CHECK ADD  CONSTRAINT [FK_Persons_Banks1] FOREIGN KEY([businessBank_id])
REFERENCES [dbo].[Banks] ([id])
GO
ALTER TABLE [dbo].[Persons] CHECK CONSTRAINT [FK_Persons_Banks1]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Persons_DomainOfApplications]') AND parent_object_id = OBJECT_ID(N'[dbo].[Persons]'))
ALTER TABLE [dbo].[Persons]  WITH NOCHECK ADD  CONSTRAINT [FK_Persons_DomainOfApplications] FOREIGN KEY([activity_id])
REFERENCES [dbo].[DomainOfApplications] ([id])
GO
ALTER TABLE [dbo].[Persons] CHECK CONSTRAINT [FK_Persons_DomainOfApplications]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Persons_Tiers]') AND parent_object_id = OBJECT_ID(N'[dbo].[Persons]'))
ALTER TABLE [dbo].[Persons]  WITH NOCHECK ADD  CONSTRAINT [FK_Persons_Tiers] FOREIGN KEY([id])
REFERENCES [dbo].[Tiers] ([id])
GO
ALTER TABLE [dbo].[Persons] NOCHECK CONSTRAINT [FK_Persons_Tiers]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Projects_Tiers]') AND parent_object_id = OBJECT_ID(N'[dbo].[Projects]'))
ALTER TABLE [dbo].[Projects]  WITH CHECK ADD  CONSTRAINT [FK_Projects_Tiers] FOREIGN KEY([tiers_id])
REFERENCES [dbo].[Tiers] ([id])
GO
ALTER TABLE [dbo].[Projects] CHECK CONSTRAINT [FK_Projects_Tiers]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RepaymentEvents_ContractEvents]') AND parent_object_id = OBJECT_ID(N'[dbo].[RepaymentEvents]'))
ALTER TABLE [dbo].[RepaymentEvents]  WITH NOCHECK ADD  CONSTRAINT [FK_RepaymentEvents_ContractEvents] FOREIGN KEY([id])
REFERENCES [dbo].[ContractEvents] ([id])
GO
ALTER TABLE [dbo].[RepaymentEvents] NOCHECK CONSTRAINT [FK_RepaymentEvents_ContractEvents]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ReschedulingOfALoanEvents_ContractEvents]') AND parent_object_id = OBJECT_ID(N'[dbo].[ReschedulingOfALoanEvents]'))
ALTER TABLE [dbo].[ReschedulingOfALoanEvents]  WITH NOCHECK ADD  CONSTRAINT [FK_ReschedulingOfALoanEvents_ContractEvents] FOREIGN KEY([id])
REFERENCES [dbo].[ContractEvents] ([id])
GO
ALTER TABLE [dbo].[ReschedulingOfALoanEvents] NOCHECK CONSTRAINT [FK_ReschedulingOfALoanEvents_ContractEvents]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SavingAccountsBalance_SavingContracts]') AND parent_object_id = OBJECT_ID(N'[dbo].[SavingAccountsBalance]'))
ALTER TABLE [dbo].[SavingAccountsBalance]  WITH NOCHECK ADD  CONSTRAINT [FK_SavingAccountsBalance_SavingContracts] FOREIGN KEY([saving_id])
REFERENCES [dbo].[SavingContracts] ([id])
GO
ALTER TABLE [dbo].[SavingAccountsBalance] CHECK CONSTRAINT [FK_SavingAccountsBalance_SavingContracts]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SavingBookContract_SavingContracts]') AND parent_object_id = OBJECT_ID(N'[dbo].[SavingBookContracts]'))
ALTER TABLE [dbo].[SavingBookContracts]  WITH CHECK ADD  CONSTRAINT [FK_SavingBookContract_SavingContracts] FOREIGN KEY([id])
REFERENCES [dbo].[SavingContracts] ([id])
GO
ALTER TABLE [dbo].[SavingBookContracts] CHECK CONSTRAINT [FK_SavingBookContract_SavingContracts]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SavingBookProducts_SavingProducts]') AND parent_object_id = OBJECT_ID(N'[dbo].[SavingBookProducts]'))
ALTER TABLE [dbo].[SavingBookProducts]  WITH CHECK ADD  CONSTRAINT [FK_SavingBookProducts_SavingProducts] FOREIGN KEY([id])
REFERENCES [dbo].[SavingProducts] ([id])
GO
ALTER TABLE [dbo].[SavingBookProducts] CHECK CONSTRAINT [FK_SavingBookProducts_SavingProducts]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SavingContracts_Tiers]') AND parent_object_id = OBJECT_ID(N'[dbo].[SavingContracts]'))
ALTER TABLE [dbo].[SavingContracts]  WITH CHECK ADD  CONSTRAINT [FK_SavingContracts_Tiers] FOREIGN KEY([tiers_id])
REFERENCES [dbo].[Tiers] ([id])
GO
ALTER TABLE [dbo].[SavingContracts] CHECK CONSTRAINT [FK_SavingContracts_Tiers]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SavingContracts_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[SavingContracts]'))
ALTER TABLE [dbo].[SavingContracts]  WITH CHECK ADD  CONSTRAINT [FK_SavingContracts_Users] FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[SavingContracts] CHECK CONSTRAINT [FK_SavingContracts_Users]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Savings_SavingProducts]') AND parent_object_id = OBJECT_ID(N'[dbo].[SavingContracts]'))
ALTER TABLE [dbo].[SavingContracts]  WITH CHECK ADD  CONSTRAINT [FK_Savings_SavingProducts] FOREIGN KEY([product_id])
REFERENCES [dbo].[SavingProducts] ([id])
GO
ALTER TABLE [dbo].[SavingContracts] CHECK CONSTRAINT [FK_Savings_SavingProducts]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SavingDepositContract_SavingContracts]') AND parent_object_id = OBJECT_ID(N'[dbo].[SavingDepositContracts]'))
ALTER TABLE [dbo].[SavingDepositContracts]  WITH CHECK ADD  CONSTRAINT [FK_SavingDepositContract_SavingContracts] FOREIGN KEY([id])
REFERENCES [dbo].[SavingContracts] ([id])
GO
ALTER TABLE [dbo].[SavingDepositContracts] CHECK CONSTRAINT [FK_SavingDepositContract_SavingContracts]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SavingEvents_SavingContracts]') AND parent_object_id = OBJECT_ID(N'[dbo].[SavingEvents]'))
ALTER TABLE [dbo].[SavingEvents]  WITH CHECK ADD  CONSTRAINT [FK_SavingEvents_SavingContracts] FOREIGN KEY([contract_id])
REFERENCES [dbo].[SavingContracts] ([id])
GO
ALTER TABLE [dbo].[SavingEvents] CHECK CONSTRAINT [FK_SavingEvents_SavingContracts]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SavingEvents_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[SavingEvents]'))
ALTER TABLE [dbo].[SavingEvents]  WITH CHECK ADD  CONSTRAINT [FK_SavingEvents_Users] FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[SavingEvents] CHECK CONSTRAINT [FK_SavingEvents_Users]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SavingProducts_Currencies]') AND parent_object_id = OBJECT_ID(N'[dbo].[SavingProducts]'))
ALTER TABLE [dbo].[SavingProducts]  WITH NOCHECK ADD  CONSTRAINT [FK_SavingProducts_Currencies] FOREIGN KEY([currency_id])
REFERENCES [dbo].[Currencies] ([id])
GO
ALTER TABLE [dbo].[SavingProducts] CHECK CONSTRAINT [FK_SavingProducts_Currencies]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TermDepositProducts_InstallmentTypes]') AND parent_object_id = OBJECT_ID(N'[dbo].[TermDepositProducts]'))
ALTER TABLE [dbo].[TermDepositProducts]  WITH CHECK ADD  CONSTRAINT [FK_TermDepositProducts_InstallmentTypes] FOREIGN KEY([installment_types_id])
REFERENCES [dbo].[InstallmentTypes] ([id])
GO
ALTER TABLE [dbo].[TermDepositProducts] CHECK CONSTRAINT [FK_TermDepositProducts_InstallmentTypes]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TermDepositProducts_SavingProducts]') AND parent_object_id = OBJECT_ID(N'[dbo].[TermDepositProducts]'))
ALTER TABLE [dbo].[TermDepositProducts]  WITH CHECK ADD  CONSTRAINT [FK_TermDepositProducts_SavingProducts] FOREIGN KEY([id])
REFERENCES [dbo].[SavingProducts] ([id])
GO
ALTER TABLE [dbo].[TermDepositProducts] CHECK CONSTRAINT [FK_TermDepositProducts_SavingProducts]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tiers_Corporates]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tiers]'))
ALTER TABLE [dbo].[Tiers]  WITH NOCHECK ADD  CONSTRAINT [FK_Tiers_Corporates] FOREIGN KEY([id])
REFERENCES [dbo].[Corporates] ([id])
GO
ALTER TABLE [dbo].[Tiers] NOCHECK CONSTRAINT [FK_Tiers_Corporates]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tiers_Districts]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tiers]'))
ALTER TABLE [dbo].[Tiers]  WITH NOCHECK ADD  CONSTRAINT [FK_Tiers_Districts] FOREIGN KEY([district_id])
REFERENCES [dbo].[Districts] ([id])
GO
ALTER TABLE [dbo].[Tiers] CHECK CONSTRAINT [FK_Tiers_Districts]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tiers_Districts1]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tiers]'))
ALTER TABLE [dbo].[Tiers]  WITH NOCHECK ADD  CONSTRAINT [FK_Tiers_Districts1] FOREIGN KEY([secondary_district_id])
REFERENCES [dbo].[Districts] ([id])
GO
ALTER TABLE [dbo].[Tiers] CHECK CONSTRAINT [FK_Tiers_Districts1]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserRole_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole]'))
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_Roles] FOREIGN KEY([role_id])
REFERENCES [dbo].[Roles] ([id])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserRole_Roles]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserRole_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole]'))
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_Users] FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserRole_Users]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Villages_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[Villages]'))
ALTER TABLE [dbo].[Villages]  WITH CHECK ADD  CONSTRAINT [FK_Villages_Users] FOREIGN KEY([loan_officer])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[Villages] CHECK CONSTRAINT [FK_Villages_Users]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WriteOffEvents_ContractEvents]') AND parent_object_id = OBJECT_ID(N'[dbo].[WriteOffEvents]'))
ALTER TABLE [dbo].[WriteOffEvents]  WITH NOCHECK ADD  CONSTRAINT [FK_WriteOffEvents_ContractEvents] FOREIGN KEY([id])
REFERENCES [dbo].[ContractEvents] ([id])
GO
ALTER TABLE [dbo].[WriteOffEvents] NOCHECK CONSTRAINT [FK_WriteOffEvents_ContractEvents]
GO
ALTER TABLE [dbo].[AllowedRoleActions]  WITH CHECK ADD  CONSTRAINT [FK_AllowedRoleActions_AllowedRoleActions] FOREIGN KEY([action_item_id], [role_id])
REFERENCES [dbo].[AllowedRoleActions] ([action_item_id], [role_id])
GO
ALTER TABLE [dbo].[AllowedRoleActions] CHECK CONSTRAINT [FK_AllowedRoleActions_AllowedRoleActions]
GO
