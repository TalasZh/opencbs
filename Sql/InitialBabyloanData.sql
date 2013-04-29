
INSERT dbo.TechnicalParameters VALUES (N'version', N'v2.8.5')
GO

INSERT dbo.ProvisioningRules VALUES (1, 0, 0, 0.02)
INSERT dbo.ProvisioningRules VALUES (2, 1, 30, 0.1)
INSERT dbo.ProvisioningRules VALUES (3, 31, 60, 0.25)
INSERT dbo.ProvisioningRules VALUES (4, 61, 90, 0.5)
INSERT dbo.ProvisioningRules VALUES (5, 91, 180, 0.75)
INSERT dbo.ProvisioningRules VALUES (6, 181, 365, 1)
INSERT dbo.ProvisioningRules VALUES (7, 366, 99999, 1)
INSERT dbo.ProvisioningRules VALUES (8, -1, -1, 0.1)
GO

INSERT dbo.GeneralParameters VALUES ('ACCOUNTING_PROCESS', N'1')
INSERT dbo.GeneralParameters VALUES ('ALLOWS_MULTIPLE_LOANS', N'1')
INSERT dbo.GeneralParameters VALUES ('BRANCH_ADDRESS', N'Not Set')
INSERT dbo.GeneralParameters VALUES ('BRANCH_CODE', N'B')
INSERT dbo.GeneralParameters VALUES ('BRANCH_NAME', N'Babyloan')
INSERT dbo.GeneralParameters VALUES ('CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS', N'1')
INSERT dbo.GeneralParameters VALUES ('CASH_RECEIPT_BEFORE_CONFIRMATION', N'0')
INSERT dbo.GeneralParameters VALUES ('CITY_IS_AN_OPEN_VALUE', N'1')
INSERT dbo.GeneralParameters VALUES ('CITY_MANDATORY', N'1')
INSERT dbo.GeneralParameters VALUES ('CONSO_NUMBER', N'1')
INSERT dbo.GeneralParameters VALUES ('COUNTRY', N'')
INSERT dbo.GeneralParameters VALUES ('CURRENCY_CODE', N'820')
INSERT dbo.GeneralParameters VALUES ('DISABLE_FUTURE_REPAYMENTS', N'1')
INSERT dbo.GeneralParameters VALUES ('EXTERNAL_CURRENCY', NULL)
INSERT dbo.GeneralParameters VALUES ('GROUP_MIN_MEMBERS', N'3')
INSERT dbo.GeneralParameters VALUES ('INN', N'Not set')
INSERT dbo.GeneralParameters VALUES ('INTERNAL_CURRENCY', N'EUR')
INSERT dbo.GeneralParameters VALUES ('LATE_DAYS_AFTER_ACCRUAL_CEASES', NULL)
INSERT dbo.GeneralParameters VALUES ('OLB_BEFORE_REPAYMENT', N'1')
INSERT dbo.GeneralParameters VALUES ('PAY_FIRST_INSTALLMENT_REAL_VALUE', N'1')
INSERT dbo.GeneralParameters VALUES ('USE_CENTS', N'1')
INSERT dbo.GeneralParameters VALUES ('WEEK_END_DAY1', N'6')
INSERT dbo.GeneralParameters VALUES ('WEEK_END_DAY2', N'0')
INSERT dbo.GeneralParameters VALUES ('WEEKLY_CONSOLIDATION_DAY', N'1')
INSERT dbo.GeneralParameters VALUES('INTEREST_RATE_DECIMAL_PLACES', '2')
GO

INSERT dbo.CustomizableFieldsSettings VALUES (1, 0, N'', N'', 0)
INSERT dbo.CustomizableFieldsSettings VALUES (2, 0, N'', N'', 0)
INSERT dbo.CustomizableFieldsSettings VALUES (3, 0, N'', N'', 0)
INSERT dbo.CustomizableFieldsSettings VALUES (4, 0, N'', N'', 0)
INSERT dbo.CustomizableFieldsSettings VALUES (5, 0, N'', N'', 0)
INSERT dbo.CustomizableFieldsSettings VALUES (6, 0, N'', N'', 0)
INSERT dbo.CustomizableFieldsSettings VALUES (7, 0, N'', N'', 0)
INSERT dbo.CustomizableFieldsSettings VALUES (8, 0, N'', N'', 0)
INSERT dbo.CustomizableFieldsSettings VALUES (9, 0, N'', N'', 0)
INSERT dbo.CustomizableFieldsSettings VALUES (10, 0, N'', N'', 0)
GO

SET IDENTITY_INSERT dbo.Users ON
INSERT dbo.Users (id, deleted, user_name, user_pass, role_code, first_name, last_name, mail) VALUES (1, 0, N'admin', N'admin', 'SUPER', N'admin', N'admin', N'Not Set')
INSERT dbo.Users (id, deleted, user_name, user_pass, role_code, first_name, last_name, mail) VALUES (2, 0, N'babyloan', N'babyloan_user', 'BABYL', N'babyloan', N'babyloan', N'Not Set')
GO
SET IDENTITY_INSERT dbo.Users OFF

GO
SET IDENTITY_INSERT dbo.Cycles ON
INSERT dbo.Cycles (id, name) VALUES (1, N'Default')
GO
SET IDENTITY_INSERT dbo.Cycles OFF


GO

SET IDENTITY_INSERT dbo.DomainOfApplications ON
ALTER TABLE dbo.DomainOfApplications DROP CONSTRAINT FK_DomainOfApplications_DomainOfApplications

INSERT dbo.DomainOfApplications (id, name, parent_id, deleted) VALUES (1, N'Agriculture', NULL, 0)
INSERT dbo.DomainOfApplications (id, name, parent_id, deleted) VALUES (2, N'Retail', NULL, 0)
INSERT dbo.DomainOfApplications (id, name, parent_id, deleted) VALUES (3, N'Arts and Crafts', NULL, 0)
INSERT dbo.DomainOfApplications (id, name, parent_id, deleted) VALUES (4, N'Manufacturing', NULL, 0)
INSERT dbo.DomainOfApplications (id, name, parent_id, deleted) VALUES (5, N'Food', NULL, 0)
INSERT dbo.DomainOfApplications (id, name, parent_id, deleted) VALUES (6, N'Services', NULL, 0)
INSERT dbo.DomainOfApplications (id, name, parent_id, deleted) VALUES (7, N'Health', NULL, 0)
GO
ALTER TABLE dbo.DomainOfApplications ADD CONSTRAINT [FK_DomainOfApplications_DomainOfApplications] FOREIGN KEY (   [parent_id] ) REFERENCES [dbo].[DomainOfApplications] (    [id] ) 

SET IDENTITY_INSERT dbo.DomainOfApplications OFF
GO

SET IDENTITY_INSERT dbo.Provinces ON
INSERT dbo.Provinces (id, name, deleted) VALUES (1, N'Paris', 0)
SET IDENTITY_INSERT dbo.Provinces OFF
GO

SET IDENTITY_INSERT dbo.InstallmentTypes ON
INSERT dbo.InstallmentTypes (id, name, nb_of_days, nb_of_months) VALUES (1, N'Monthly', 0, 1)
SET IDENTITY_INSERT dbo.InstallmentTypes OFF
GO

SET IDENTITY_INSERT dbo.FundingLines ON
INSERT dbo.FundingLines (id, name, begin_date, end_date, amount, purpose, residual_amount, deleted) VALUES (1, N'Babyloan', '2008-01-01 00:00:00.000', '2020-01-01 00:00:00.000', 10000000, N'Babyloan', 10000000.0000, 0)
SET IDENTITY_INSERT dbo.FundingLines OFF
GO

SET IDENTITY_INSERT dbo.Accounts ON
INSERT dbo.Accounts (id, account_number, local_account_number, label, balance, debit_plus, type_code, description) VALUES (1, N'1011', N'10101', N'Cash', 0.0000, -1, 'CASH', 1)
INSERT dbo.Accounts (id, account_number, local_account_number, label, balance, debit_plus, type_code, description) VALUES (2, N'20311', N'10913', N'Cash Credit individual loan', 0.0000, -1, 'CASH_CREDIT_INDIVIDUAL_LOAN', 1)
INSERT dbo.Accounts (id, account_number, local_account_number, label, balance, debit_plus, type_code, description) VALUES (3, N'20312', N'10913', N'Cash Credit group loan', 0.0000, -1, 'CASH_CREDIT_GROUP_LOAN', 1)
INSERT dbo.Accounts (id, account_number, local_account_number, label, balance, debit_plus, type_code, description) VALUES (4, N'2032', N'11113', N'Rescheduled Loans', 0.0000, -1, 'RESCHEDULED_LOANS', 1)
INSERT dbo.Accounts (id, account_number, local_account_number, label, balance, debit_plus, type_code, description) VALUES (5, N'2037', N'11113', N'Accrued interests receivable', 0.0000, -1, 'ACCRUED_INTERESTS_LOANS', 1)
INSERT dbo.Accounts (id, account_number, local_account_number, label, balance, debit_plus, type_code, description) VALUES (6, N'2038', N'11113', N'Accrued interests on rescheduled loans', 0.0000, -1, 'ACCRUED_INTERESTS_RESCHEDULED_LOANS', 1)
INSERT dbo.Accounts (id, account_number, local_account_number, label, balance, debit_plus, type_code, description) VALUES (7, N'2911', N'10923', N'Bad Loans', 0.0000, -1, 'BAD_LOANS', 1)
INSERT dbo.Accounts (id, account_number, local_account_number, label, balance, debit_plus, type_code, description) VALUES (8, N'2921', N'10923', N'Unrecoverable Bad Loans', 0.0000, -1, 'UNRECO_BAD_LOANS', 1)
INSERT dbo.Accounts (id, account_number, local_account_number, label, balance, debit_plus, type_code, description) VALUES (9, N'2971', N'40260', N'Interest and Penalties on Past Due Loans', 0.0000, -1, 'IPOPDL', 1)
INSERT dbo.Accounts (id, account_number, local_account_number, label, balance, debit_plus, type_code, description) VALUES (10, N'2991', N'10989', N'Loan Loss Reserve', 0.0000, 0, 'LOAN_LOSS_RESERVE', 2)
INSERT dbo.Accounts (id, account_number, local_account_number, label, balance, debit_plus, type_code, description) VALUES (11, N'3882', N'42820', N'Deferred Income', 0.0000, 0, 'DEFERRED_INCOME', 2)
INSERT dbo.Accounts (id, account_number, local_account_number, label, balance, debit_plus, type_code, description) VALUES (12, N'6712', N'57101', N'Provision on bad loans', 0.0000, -1, 'PROVISION_ON_BAD_LOANS', 3)
INSERT dbo.Accounts (id, account_number, local_account_number, label, balance, debit_plus, type_code, description) VALUES (13, N'6751', N'57107', N'Loan Loss', 0.0000, -1, 'LOAN_LOSS', 3)
INSERT dbo.Accounts (id, account_number, local_account_number, label, balance, debit_plus, type_code, description) VALUES (14, N'70211', N'40260', N'Interests on cash credit individual loan', 0.0000, 0, 'INTERESTS_ON_CASH_CREDIT_INDIVIDUAL_LOAN', 4)
INSERT dbo.Accounts (id, account_number, local_account_number, label, balance, debit_plus, type_code, description) VALUES (15, N'70212', N'40260', N'Interests on cash credit group loan', 0.0000, 0, 'INTERESTS_ON_CASH_CREDIT_GROUP_LOAN', 4)
INSERT dbo.Accounts (id, account_number, local_account_number, label, balance, debit_plus, type_code, description) VALUES (16, N'7022', N'40222', N'Interests on rescheduled loans', 0.0000, 0, 'INTERESTS_ON_RESCHEDULED_LOANS', 4)
INSERT dbo.Accounts (id, account_number, local_account_number, label, balance, debit_plus, type_code, description) VALUES (17, N'70271', N'70270', N'Penalties on past due loans individual loan', 0.0000, 0, 'PENALTIES_ON_PAST_DUE_LOANS_INDIVIDUAL_LOAN', 4)
INSERT dbo.Accounts (id, account_number, local_account_number, label, balance, debit_plus, type_code, description) VALUES (18, N'70272', N'70270', N'Penalties on past due loans group loan', 0.0000, 0, 'PENALTIES_ON_PAST_DUE_LOANS_GROUP_LOAN', 4)
INSERT dbo.Accounts (id, account_number, local_account_number, label, balance, debit_plus, type_code, description) VALUES (19, N'7028', N'40260', N'Interests on bad loans', 0.0000, 0, 'INTERESTS_ON_BAD_LOANS', 4)
INSERT dbo.Accounts (id, account_number, local_account_number, label, balance, debit_plus, type_code, description) VALUES (20, N'7029', N'40402', N'Commissions', 0.0000, 0, 'COMMISSIONS', 4)
INSERT dbo.Accounts (id, account_number, local_account_number, label, balance, debit_plus, type_code, description) VALUES (21, N'7712', N'42802', N'Provision write off', 0.0000, 0, 'PROVISION_WRITE_OFF', 4)
INSERT dbo.Accounts (id, account_number, local_account_number, label, balance, debit_plus, type_code, description) VALUES (22, N'5211', N'42802', N'Loan Loss Allowance on Current Loans', 0.0000, 0, 'LIABILITIES_LOAN_LOSS_CURRENT', 2)
INSERT dbo.Accounts (id, account_number, local_account_number, label, balance, debit_plus, type_code, description) VALUES (23, N'6731', N'42802', N'Loan Loss Allowance on Current Loans', 0.0000, -1, 'EXPENSES_LOAN_LOSS_CURRENT', 3)
INSERT dbo.Accounts (id, account_number, local_account_number, label, balance, debit_plus, type_code, description) VALUES (24, N'7731', N'42802', N'Resumption of Loan Loss allowance on current loans', 0.0000, 0, 'INCOME_LOAN_LOSS_CURRENT', 4)
SET IDENTITY_INSERT dbo.Accounts OFF
GO

INSERT dbo.Corporates VALUES (1, N'Babyloan', 10000000.0000, 0, N'Babyloan', N'BBL', NULL, '2006-01-01 00:00:00.000', 0, NULL, NULL, N'', 1, NULL, '2006-01-01 00:00:00.000', NULL)
GO

SET IDENTITY_INSERT dbo.Packages ON
INSERT dbo.Packages (id, deleted, name, client_type, amount, amount_min, amount_max, interest_rate, interest_rate_min, interest_rate_max, installment_type, grace_period, grace_period_min, grace_period_max, number_of_installments, number_of_installments_min, number_of_installments_max, anticipated_total_repayment_penalties, anticipated_total_repayment_penalties_min, anticipated_total_repayment_penalties_max, exotic_id, entry_fees, entry_fees_min, entry_fees_max, loan_type, keep_expected_installment, charge_interest_within_grace_period, anticipated_repayment_base, cycle_id, non_repayment_penalties_based_on_overdue_interest, non_repayment_penalties_based_on_initial_amount, non_repayment_penalties_based_on_olb, non_repayment_penalties_based_on_overdue_principal, non_repayment_penalties_based_on_overdue_interest_min, non_repayment_penalties_based_on_initial_amount_min, non_repayment_penalties_based_on_olb_min, non_repayment_penalties_based_on_overdue_principal_min, non_repayment_penalties_based_on_overdue_interest_max, non_repayment_penalties_based_on_initial_amount_max, non_repayment_penalties_based_on_olb_max, non_repayment_penalties_based_on_overdue_principal_max, fundingLine_id, corporate_id, fake) VALUES (1, 0, N'Babyloan', '-', NULL, 100.0000, 5000.0000, 0, NULL, NULL, 1, NULL, 0, 4, NULL, 4, 24, 0, NULL, NULL, NULL, 0, NULL, NULL, 2, -1, -1, 2, NULL, 0, 0, 0, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, 0)
SET IDENTITY_INSERT dbo.Packages OFF
GO

SET IDENTITY_INSERT dbo.CorporateEvents ON
INSERT dbo.CorporateEvents (id, code, amount, mouvement, corporate_id, deleted, creation_date, type) VALUES (1, N'Dotation babyloan', 10000000.0000, 1, 1, 0, '2008-10-06 00:00:00.000', 0)
GO
SET IDENTITY_INSERT dbo.CorporateEvents OFF
GO

SET IDENTITY_INSERT dbo.Districts ON
INSERT dbo.Districts (id, name, province_id, deleted) VALUES (1, N'Paris', 1, 0)
SET IDENTITY_INSERT dbo.Districts OFF
GO

SET IDENTITY_INSERT dbo.Tiers ON
INSERT dbo.Tiers (id, client_type_code, scoring, loan_cycle, active, bad_client, other_org_name, other_org_amount, other_org_debts, other_org_comment, district_id, city, address, secondary_district_id, secondary_city, secondary_address, cash_input_voucher_number, cash_output_voucher_number, creation_date, home_phone, personal_phone, secondary_home_phone, secondary_personal_phone, home_type, e_mail, secondary_home_type, secondary_e_mail, status, sponsor, follow_up_comment) VALUES (1, 'C', NULL, 1, 0, 0, NULL, NULL, NULL, NULL, 1, N'Paris', N'Bd Haussman', NULL, NULL, NULL, NULL, NULL, '2008-10-06 00:00:00.000', '', '', NULL, NULL, N'', N'', NULL, NULL, 0, NULL, N'')
SET IDENTITY_INSERT dbo.Tiers OFF
GO

INSERT dbo.CorporateFundingLineBelonging VALUES (1, 1, -1)
GO

INSERT dbo.AmountCycles VALUES (1, 1, 100.0000, 1000.0000)
INSERT dbo.AmountCycles VALUES (1, 2, 100.0000, 10000.0000)
INSERT dbo.AmountCycles VALUES (1, 3, 100.0000, 100000.0000)
INSERT dbo.AmountCycles VALUES (1, 4, 100.0000, 1000000.0000)
INSERT dbo.AmountCycles VALUES (1, 5, 100.0000, 10000000.0000)
GO
