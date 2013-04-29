INSERT INTO [AccountsCategory] ([name])
VALUES ('BalanceSheetAsset')
INSERT INTO [AccountsCategory] ([name])
VALUES ('BalanceSheetLiabilities')
INSERT INTO [AccountsCategory] ([name])
VALUES ('ProfitAndLossIncome')
INSERT INTO [AccountsCategory] ([name])
VALUES ('ProfitAndLossExpense')


--SET IDENTITY_INSERT ChartOfAccounts ON
--INSERT INTO ChartOfAccounts (id, account_number, label, debit_plus, type_code, account_category_id, type) SELECT  1,'10101', 'Cash' ,1  ,'CASH' ,1,1
--INSERT INTO ChartOfAccounts (id, account_number, label, debit_plus, type_code, account_category_id, type) SELECT  2,'2031', 'Cash Credit'  ,1  ,'CASH_CREDIT'  ,1,1
--INSERT INTO ChartOfAccounts (id, account_number, label, debit_plus, type_code, account_category_id, type) SELECT  3,'2032', 'Rescheduled Loans',1  ,'RESCHEDULED_LOANS',1,1
--INSERT INTO ChartOfAccounts (id, account_number, label, debit_plus, type_code, account_category_id, type) SELECT  4,'2037', 'Accrued interests receivable' ,1  ,'ACCRUED_INTERESTS_LOANS'  ,1,1
--INSERT INTO ChartOfAccounts (id, account_number, label, debit_plus, type_code, account_category_id, type) SELECT  5,'2038', 'Accrued interests on rescheduled loans',1  ,'ACCRUED_INTERESTS_RESCHEDULED_LOANS'  ,1,1
--INSERT INTO ChartOfAccounts (id, account_number, label, debit_plus, type_code, account_category_id, type) SELECT  6,'2911', 'Bad Loans',1  ,'BAD_LOANS',1,1
--INSERT INTO ChartOfAccounts (id, account_number, label, debit_plus, type_code, account_category_id, type) SELECT  7,'2921', 'Unrecoverable Bad Loans'  ,1  ,'UNRECO_BAD_LOANS' ,1,1
--INSERT INTO ChartOfAccounts (id, account_number, label, debit_plus, type_code, account_category_id, type) SELECT  8,'2971', 'Interest on Past Due Loans',1  ,'INTERESTS_ON_PAST_DUE_LOANS'  ,1,1
--INSERT INTO ChartOfAccounts (id, account_number, label, debit_plus, type_code, account_category_id, type) SELECT  9,'2972', 'Penalties on Past Due Loans'  ,1  ,'PENALTIES_ON_PAST_DUE_LOANS_ASSET',1,1
--INSERT INTO ChartOfAccounts (id, account_number, label, debit_plus, type_code, account_category_id, type) SELECT  10  ,'2991', 'Loan Loss Reserve',0  ,'LOAN_LOSS_RESERVE',2,1
--INSERT INTO ChartOfAccounts (id, account_number, label, debit_plus, type_code, account_category_id, type) SELECT  11  ,'3882', 'Deferred Income'  ,0  ,'DEFERRED_INCOME'  ,2,1
--INSERT INTO ChartOfAccounts (id, account_number, label, debit_plus, type_code, account_category_id, type) SELECT  12  ,'6712', 'Provision on bad loans',1  ,'PROVISION_ON_BAD_LOANS',3,1
--INSERT INTO ChartOfAccounts (id, account_number, label, debit_plus, type_code, account_category_id, type) SELECT  13  ,'6751', 'Loan Loss',1  ,'LOAN_LOSS',3,1
--INSERT INTO ChartOfAccounts (id, account_number, label, debit_plus, type_code, account_category_id, type) SELECT  14  ,'7021', 'Interests on cash credit' ,0  ,'INTERESTS_ON_CASH_CREDIT' ,4,1
--INSERT INTO ChartOfAccounts (id, account_number, label, debit_plus, type_code, account_category_id, type) SELECT  15  ,'7022', 'Interests on rescheduled loans',0  ,'INTERESTS_ON_RESCHEDULED_LOANS',4,1
--INSERT INTO ChartOfAccounts (id, account_number, label, debit_plus, type_code, account_category_id, type) SELECT  16  ,'7027', 'Penalties on past due loans'  ,0  ,'PENALTIES_ON_PAST_DUE_LOANS_INCOME',4,1
--INSERT INTO ChartOfAccounts (id, account_number, label, debit_plus, type_code, account_category_id, type) SELECT  17  ,'7028','Interests on bad loans',0  ,'INTERESTS_ON_BAD_LOANS',4,1
--INSERT INTO ChartOfAccounts (id, account_number, label, debit_plus, type_code, account_category_id, type) SELECT  18  ,'7029', 'Commissions'  ,0  ,'COMMISSIONS'  ,4,1
--INSERT INTO ChartOfAccounts (id, account_number, label, debit_plus, type_code, account_category_id, type) SELECT  19  ,'7712', 'Provision write off'  ,0  ,'PROVISION_WRITE_OFF'  ,4,1
--INSERT INTO ChartOfAccounts (id, account_number, label, debit_plus, type_code, account_category_id, type) SELECT  20  ,'5211', 'Loan Loss Allowance on Current Loans' ,0  ,'LIABILITIES_LOAN_LOSS_CURRENT',2,1
--INSERT INTO ChartOfAccounts (id, account_number, label, debit_plus, type_code, account_category_id, type) SELECT  21  ,'6731', 'Loan Loss Allowance on Current Loans' ,1  ,'EXPENSES_LOAN_LOSS_CURRENT',3,1
--INSERT INTO ChartOfAccounts (id, account_number, label, debit_plus, type_code, account_category_id, type) SELECT  22  ,'7731', 'Resumption of Loan Loss allowance on current loans',0  ,'INCOME_LOAN_LOSS_CURRENT' ,4,1
--INSERT INTO ChartOfAccounts (id, account_number, label, debit_plus, type_code, account_category_id, type) SELECT  23  ,'1322', 'Accounts and Terms Loans' ,0  ,'ACCOUNTS_AND_TERM_LOANS'  ,2,1
--INSERT INTO ChartOfAccounts (id, account_number, label, debit_plus, type_code, account_category_id, type) SELECT  24  ,'221', 'Savings'  ,0  ,'SAVINGS'  ,2,1
--INSERT INTO ChartOfAccounts (id, account_number, label, debit_plus, type_code, account_category_id, type) SELECT  25  ,'2261', 'Account payable interests on Savings Books',0  ,'ACCOUNT_PAYABLE_INTERESTS_ON_SAVINGS_BOOKS',3,1
--INSERT INTO ChartOfAccounts (id, account_number, label, debit_plus, type_code, account_category_id, type) SELECT  26  ,'60132', 'Interests on deposit account' ,1  ,'INTERESTS_ON_DEPOSIT_ACCOUNT' ,3,1
--INSERT INTO ChartOfAccounts (id, account_number, label, debit_plus, type_code, account_category_id, type) SELECT  27  ,'42802', 'Recovery of charged off assets',0  ,'RECOVERY_OF_CHARGED_OFF_ASSETS',3,1
--INSERT INTO ChartOfAccounts (id, account_number, label, debit_plus, type_code, account_category_id, type) SELECT  28  ,'9330201', 'NON_BALANCE_COMMITTED_FUNDS'  ,0  ,'NON_BALANCE_COMMITTED_FUNDS'  ,3,1
--INSERT INTO ChartOfAccounts (id, account_number, label, debit_plus, type_code, account_category_id, type) SELECT  29  ,'9330202', 'NON_BALANCE_VALIDATED_LOANS'  ,1  ,'NON_BALANCE_VALIDATED_LOANS'  ,1,1
--INSERT INTO ChartOfAccounts (id, account_number, label, debit_plus, type_code, account_category_id, type) SELECT  30  ,'222', 'Term Deposit' ,0  ,'TERM_DEPOSIT' ,2,1
--INSERT INTO ChartOfAccounts (id, account_number, label, debit_plus, type_code, account_category_id, type) SELECT  31  ,'223', 'Compulsory Savings',0  ,'COMPULSORY_SAVINGS',2,1
--INSERT INTO ChartOfAccounts (id, account_number, label, debit_plus, type_code, account_category_id, type) SELECT  32  ,'2262', 'Account payable interests on Term Deposit',0  ,'ACCOUNT_PAYABLE_INTERESTS_ON_TERM_DEPOSIT',3,1
--INSERT INTO ChartOfAccounts (id, account_number, label, debit_plus, type_code, account_category_id, type) SELECT  33  ,'2263', 'Account payable interests on Compulsory Savings'  ,0  ,'ACCOUNT_PAYABLE_INTERESTS_ON_COMPULSORY_SAVINGS'  ,3,1
--INSERT INTO ChartOfAccounts (id, account_number, label, debit_plus, type_code, account_category_id, type) SELECT  34  ,'10915', 'Cash credit foreign currency' ,1  ,'CASH CREDIT FOREIGN CURRENCY' ,1,0
--INSERT INTO ChartOfAccounts (id, account_number, label, debit_plus, type_code, account_category_id, type) SELECT  35  ,'10105', 'Cash foreign currency',1  ,'CASH FOREIGN CURRENCY',1,0
--INSERT INTO ChartOfAccounts (id, account_number, label, debit_plus, type_code, account_category_id, type) SELECT  36  ,'10901', 'Cash credit corporate',1  ,'CASH CREDIT CORPORATE',1,0
--INSERT INTO ChartOfAccounts (id, account_number, label, debit_plus, type_code, account_category_id, type) SELECT  37  ,'10903', 'Cash credit corporate foreign currency',1  ,'CASH CREDIT CORPORATE FOREIGN CURRENCY',1,0
--INSERT INTO ChartOfAccounts (id, account_number, label, debit_plus, type_code, account_category_id, type) SELECT  38  ,'10301', 'Bank account' ,1  ,'BANK ACCOUNT' ,1,0
--INSERT INTO ChartOfAccounts (id, account_number, label, debit_plus, type_code, account_category_id, type) SELECT  39  ,'10305', 'Bank account foreign currency',1  ,'BANK ACCOUNT FOREIGN CURRENCY',1,0
--INSERT INTO ChartOfAccounts (id, account_number, label, debit_plus, type_code, account_category_id, type) SELECT  40  ,'40260', 'Interest income'  ,0  ,'INTEREST INCOME'  ,3,0
--INSERT INTO ChartOfAccounts (id, account_number, label, debit_plus, type_code, account_category_id, type) SELECT  41  ,'40256', 'Interest income from corporates'  ,0  ,'INTEREST INCOME FROM CORPORATES'  ,3,0
--INSERT INTO ChartOfAccounts (id, account_number, label, debit_plus, type_code, account_category_id, type) SELECT  42  ,'40294', 'Penalties and commision income',0  ,'PENALTIES AND COMMISION INCOME',3,0
--INSERT INTO ChartOfAccounts (id, account_number, label, debit_plus, type_code, account_category_id, type) SELECT  43  ,'10919', 'Bad loans corporates' ,1  ,'BAD LOANS CORPORATE'  ,1,0
--INSERT INTO ChartOfAccounts (id, account_number, label, debit_plus, type_code, account_category_id, type) SELECT  44  ,'11101', 'Rescheduled loans corporates' ,1  ,'RESCHEDULED LOANS CORPORATES' ,1,0
--SET IDENTITY_INSERT ChartOfAccounts OFF