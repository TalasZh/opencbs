SET DATEFORMAT YMD

--------------------DELETE TABLE------------------------------------------------------------
DELETE FROM [InstallmentHistory]
DELETE FROM [Installments] 
DELETE FROM [LoansLinkSavingsBook]
DELETE FROM [SavingEvents]
DELETE FROM [SavingBookContracts]
DELETE FROM [SavingDepositContracts]
DELETE FROM [SavingContracts]
DELETE FROM [ContractAccountingRules]
DELETE FROM [FundingLineAccountingRules]
DELETE FROM [AccountingRules]
DELETE FROM [SavingBookProducts]
DELETE FROM [TermDepositProducts]
DELETE FROM [SavingProducts]
DELETE FROM [WriteOffEvents]
DELETE FROM [ReschedulingOfALoanEvents]
DELETE FROM [LoanDisbursmentEvents]
DELETE FROM [LoanInterestAccruingEvents]
DELETE FROM [RepaymentEvents]
DELETE FROM [ContractEvents]   
DELETE FROM [Credit]  
DELETE FROM [AmountCycles] 
DELETE FROM [Cycles]
DELETE FROM [ExoticInstallments]
DELETE FROM [Exotics]
DELETE FROM [EntryFees]
DELETE FROM [Packages] 
DELETE FROM [LinkGuarantorCredit]  
DELETE FROM [ContractAssignHistory]  
DELETE FROM [Contracts]  
DELETE FROM [Projects] 
DELETE FROM [AllowedRoleActions]
DELETE FROM [AllowedRoleMenus]
DELETE FROM [InstallmentTypes] 
DELETE FROM [UsersBranches]
DELETE FROM [Villages]
DELETE FROM [Tiers] 
DELETE FROM [Branches]
DELETE FROM [UsersSubordinates]
DELETE FROM [UserRole]
DELETE FROM [Roles]   
DELETE FROM [Users] 
DELETE FROM [Tiers]   
DELETE FROM [City]  
DELETE FROM [Districts]  
DELETE FROM [Provinces]  
DELETE FROM [PersonGroupBelonging]  
DELETE FROM [Persons]  
DELETE FROM [Groups]  
DELETE FROM [Corporates]  
DELETE FROM [FundingLineEvents] 
DELETE FROM [FundingLines]   
DELETE FROM [EconomicActivities]  
DELETE FROM [MFI]
DELETE FROM [ProvisioningRules]
DELETE FROM [ExchangeRates]
DELETE FROM [Villages]
DELETE FROM [VillagesPersons]
DELETE FROM [StandardBookings]
DELETE FROM [Currencies]
DELETE FROM [AlertSettings]
DELETE FROM [ChartOFAccounts]
DELETE FROM [AccountsCategory]
DELETE FROM [EventAttributes]
DELETE FROM [EventTypes]
DELETE FROM [PackagesClientTypes]
DELETE FROM [ClientTypes]
DELETE FROM [SavingBookContracts]
DELETE FROM [SavingContracts]
DELETE FROM [SavingBookProducts]
DELETE FROM [TermDepositProducts]
DELETE FROM [SavingProducts]


IF EXISTS(SELECT name FROM [octopus_test]..sysobjects WHERE name = N'ell' AND xtype='U')
DELETE FROM [ell]
-------------------------------------------------------------------EXOTIC------------------------------------------------------------------ 
SET IDENTITY_INSERT Exotics ON
INSERT INTO Exotics([id],[name]) VALUES(1,'Exotic')
INSERT INTO Exotics([id],[name]) VALUES(2,'Exotic2')
SET IDENTITY_INSERT Exotics OFF

-------------------------------------------------------------------EXOTICINSTALLMENT------------------------------------------------------------------ 
INSERT INTO ExoticInstallments([number],[exotic_id],[principal_coeff],[interest_coeff]) VALUES(1,1,1,1)
INSERT INTO ExoticInstallments([number],[exotic_id],[principal_coeff],[interest_coeff]) VALUES(1,2,0.5,1)
INSERT INTO ExoticInstallments([number],[exotic_id],[principal_coeff],[interest_coeff]) VALUES(2,2,0.5,1)

-------------------------------------------------------------------CYCLES------------------------------------------------------------------ 
SET IDENTITY_INSERT Cycles ON
INSERT INTO Cycles([id],[name]) VALUES(1,'Cycle')
INSERT INTO Cycles([id],[name]) VALUES(2,'Cycle2')
SET IDENTITY_INSERT Cycles OFF

-------------------------------------------------------------------[ClientTypes]------------------------------------------------------------------ 
SET IDENTITY_INSERT [ClientTypes] ON
INSERT INTO [dbo].[ClientTypes] ([id], [type_name])  VALUES (1, 'All')
INSERT INTO [dbo].[ClientTypes] ([id], [type_name])  VALUES (2, 'Group')
INSERT INTO [dbo].[ClientTypes] ([id], [type_name])  VALUES (3, 'Individual')
INSERT INTO [dbo].[ClientTypes] ([id], [type_name])  VALUES (4, 'Corporate')
INSERT INTO [dbo].[ClientTypes] ([id], [type_name])  VALUES (5, 'Village')
SET IDENTITY_INSERT [ClientTypes] OFF

-------------------------------------------------------ALERT SETTINGS-----------------------------------------------

INSERT INTO [dbo].[AlertSettings] ([parameter], [value]) VALUES('client_name', '1')
INSERT INTO [dbo].[AlertSettings] ([parameter], [value]) VALUES('amount', '1')
INSERT INTO [dbo].[AlertSettings] ([parameter], [value]) VALUES('effect_date', '1')
INSERT INTO [dbo].[AlertSettings] ([parameter], [value]) VALUES('phone_num', '1')
INSERT INTO [dbo].[AlertSettings] ([parameter], [value]) VALUES('address', '1')
INSERT INTO [dbo].[AlertSettings] ([parameter], [value]) VALUES('loan_officer', '1')
INSERT INTO [dbo].[AlertSettings] ([parameter], [value]) VALUES('contract_id', '1')
INSERT INTO [dbo].[AlertSettings] ([parameter], [value]) VALUES('show_only_late_loans', '1')

-------------------------------------------------------------------AMOUNTCYCLES------------------------------------------------------------------ 
INSERT INTO AmountCycles([number],[cycle_id],[amount_min],[amount_max]) VALUES(1,1,1000,10000)
INSERT INTO AmountCycles([number],[cycle_id],[amount_min],[amount_max]) VALUES(1,2,1111,11111)

--------------------------------------PROVISIONINGRULES-------------------------------------------------------------- 
INSERT INTO [ExchangeRates]([exchange_date],[exchange_rate], [currency_id]) VALUES('2007-08-08',1,2)
INSERT INTO [ExchangeRates]([exchange_date],[exchange_rate], [currency_id]) VALUES('2008-08-08',2,2)
INSERT INTO [ExchangeRates]([exchange_date],[exchange_rate], [currency_id]) VALUES(GETDATE()+1,3,2)

--------------------------------------PROVISIONINGRULES-------------------------------------------------------------- 
INSERT INTO [ProvisioningRules]([id], [number_of_days_min], [number_of_days_max], [provisioning_value]) VALUES(1, 0, 0, 0.02)
INSERT INTO [ProvisioningRules]([id], [number_of_days_min], [number_of_days_max], [provisioning_value]) VALUES(2, 1, 30, 0.1)
INSERT INTO [ProvisioningRules]([id], [number_of_days_min], [number_of_days_max], [provisioning_value]) VALUES(3, 31, 60, 0.25)
INSERT INTO [ProvisioningRules]([id], [number_of_days_min], [number_of_days_max], [provisioning_value]) VALUES(4, 61, 90, 0.5)
INSERT INTO [ProvisioningRules]([id], [number_of_days_min], [number_of_days_max], [provisioning_value]) VALUES(5, 91, 180, 0.75)
INSERT INTO [ProvisioningRules]([id], [number_of_days_min], [number_of_days_max], [provisioning_value]) VALUES(6, 181, 365, 1)
INSERT INTO [ProvisioningRules]([id], [number_of_days_min], [number_of_days_max], [provisioning_value]) VALUES(7, 366, 99999, 1)

---------------------------------------CURRENCIES--------------------------------------------
SET IDENTITY_INSERT Currencies ON  
INSERT INTO [Currencies] ([id], [name],[is_pivot],[code]) VALUES	(1, 'KGS', 1,'KGS')
INSERT INTO [Currencies] ([id], [name],[is_pivot],[code]) VALUES	(2, 'USD', 0,'USD')

SET IDENTITY_INSERT Currencies OFF
--------------------------------------MFI-------------------------------------------------------------- 
SET IDENTITY_INSERT MFI ON
INSERT INTO [MFI]([id],[name],[login],[password]) VALUES(1,'Finadev','finadev','&1password')
SET IDENTITY_INSERT MFI OFF

--------------------------------------ECONOMICACTIVITIES------------------------------------------------- 
SET IDENTITY_INSERT EconomicActivities ON
INSERT INTO [EconomicActivities]([id],[name],[deleted]) VALUES(1,'Services',0)
INSERT INTO [EconomicActivities] ([id],[name], [parent_id], [deleted])VALUES (2,'Peche',null,0)
SET IDENTITY_INSERT EconomicActivities OFF

------------------------------------- FUNDINGLINES-----------------------------------------------
SET IDENTITY_INSERT FundingLines ON  
INSERT INTO [FundingLines]([id],[name], [deleted],[begin_date],[end_date],[amount],[purpose], [currency_id]) VALUES(1,'DRE',0,'','',10000,'JHT',1)  
INSERT INTO [FundingLines]([id],[name], [deleted],[begin_date],[end_date],[amount],[purpose], [currency_id]) VALUES(2,'GHJ',0,'','',10000,'JHT',1)  
INSERT INTO [FundingLines]([id],[name], [deleted],[begin_date],[end_date],[amount],[purpose], [currency_id]) VALUES(3,'FRR',0,'','',500,'JHT',1)  
SET IDENTITY_INSERT FundingLines OFF  

-------------------------------------- PROVINCES -------------------------------------------------------------------
SET IDENTITY_INSERT Provinces ON  
INSERT INTO Provinces(id,[name],deleted)VALUES(1,'FRANCE',0)  
SET IDENTITY_INSERT Provinces OFF  

------------------------------------- DISTRICTS --------------------------------------------------------------------
SET IDENTITY_INSERT Districts ON  
INSERT INTO Districts(id,[name],province_id,deleted)VALUES(1,'PARIS',1,0) 
INSERT INTO Districts(id,[name],province_id,deleted)VALUES(2,'NANCY',1,0)  
SET IDENTITY_INSERT Districts OFF 

------------------------------------- DISTRICTS --------------------------------------------------------------------
SET IDENTITY_INSERT Branches ON  
INSERT INTO Branches(id,[name], [code])VALUES(1,'Bish', 'CH') 
SET IDENTITY_INSERT Branches OFF

------------------------------------- Questionnaire ------------------------------------------------------------------  
INSERT INTO Questionnaire ([Name],Country, Email, is_sent) VALUES('test','test','test@test.com',1)

-------------------------------------- TIERS -----------------------------------------------------------------------        
SET IDENTITY_INSERT Tiers ON  
INSERT INTO Tiers(id,client_type_code,loan_cycle,active,bad_client,district_id,creation_date, branch_id) 
VALUES(1,'G',1,1,0,1,'2008-08-08', 1)  
INSERT INTO Tiers(id,client_type_code,loan_cycle,active,bad_client,district_id,creation_date, branch_id) 
VALUES(2,'I',0,0,0,1,'2008-08-08', 1)  
INSERT INTO Tiers(id,client_type_code,loan_cycle,active,bad_client,district_id,status,creation_date, branch_id) 
VALUES(3,'G',1,1,0,1,3,'2008-08-08', 1)  
INSERT INTO Tiers(id,client_type_code,loan_cycle,active,bad_client,district_id,status,creation_date, branch_id) 
VALUES(4,'I',1,1,0,1,3,'2008-08-08', 1)  
INSERT INTO Tiers(id,client_type_code,loan_cycle,active,bad_client,district_id,status,creation_date, branch_id) 
VALUES(5,'C',1,1,0,1,1,'2008-08-08', 1)  

INSERT INTO [Tiers]([id],[client_type_code],[scoring],[loan_cycle],[active],[bad_client],[other_org_name],[other_org_amount],
[other_org_debts],[district_id],[city],[address],[secondary_district_id],[secondary_city],[secondary_address],[cash_input_voucher_number],
[cash_output_voucher_number],[creation_date],[home_phone],[personal_phone],[secondary_home_phone],[secondary_personal_phone],
[home_type],[e_mail],[secondary_homeType],[secondary_e_mail],[status],[other_org_comment],[sponsor1],[follow_up_comment], branch_id)
VALUES (6,'I',0.7,1,1,0,'PlanetFinance',1000,0,1,'Paris','Champs Elysees',1,'Nancy','rue des Jardins',1,2,'2008-08-08','000000',
'111111','222222','333333','house','nma@gmail.com','house','nma@octo.com',3,'Nothing','Nicolas MANGIN','Nothing', 1)  

INSERT INTO [Tiers]([id],[client_type_code],[scoring],[loan_cycle],[active],[bad_client],[other_org_name],[other_org_amount],
[other_org_debts],[district_id],[city],[address],[secondary_district_id],[secondary_city],[secondary_address],[cash_input_voucher_number],
[cash_output_voucher_number],[creation_date],[home_phone],[personal_phone],[secondary_home_phone],[secondary_personal_phone],
[home_type],[e_mail],[secondary_homeType],[secondary_e_mail],[status],[other_org_comment],[sponsor1],[follow_up_comment], branch_id)
VALUES (7,'G',0.7,1,1,0,'PlanetFinance',1000,0,1,'Paris','Champs Elysees',1,'Nancy','rue des Jardins',1,2,'2008-08-08','000000',
'111111','222222','333333','house','nma@gmail.com','house','nma@octo.com',3,'Nothing','Nicolas MANGIN','Nothing', 1)  


INSERT INTO [Tiers]([id],[client_type_code],[scoring],[loan_cycle],[active],[bad_client],[other_org_name],[other_org_amount],
[other_org_debts],[district_id],[city],[address],[secondary_district_id],[secondary_city],[secondary_address],[cash_input_voucher_number],
[cash_output_voucher_number],[creation_date],[home_phone],[personal_phone],[secondary_home_phone],[secondary_personal_phone],
[home_type],[e_mail],[secondary_homeType],[secondary_e_mail],[status],[other_org_comment],[sponsor1],[follow_up_comment], branch_id)
VALUES (8,'C',0.7,1,1,0,'PlanetFinance',1000,0,1,'Paris','Champs Elysees',1,'Nancy','rue des Jardins',1,2,'2008-08-08','000000',
'111111','222222','333333','house','nma@gmail.com','house','nma@octo.com',3,'Nothing','Nicolas MANGIN','Nothing',1)

INSERT INTO Tiers(id,client_type_code,loan_cycle,active,bad_client,district_id,status,creation_date, branch_id)
VALUES(9,'C',1,1,0,1,2,'2008-08-08',1) 
INSERT INTO Tiers(id,client_type_code,loan_cycle,active,bad_client,district_id,status,creation_date, branch_id)
VALUES(10,'C',1,1,0,1,3,'2008-08-08', 1)
INSERT INTO Tiers(id,client_type_code,loan_cycle,active,bad_client,district_id,status,creation_date, branch_id) 
VALUES(11,'V',1,1,0,1,1,'2008-08-08', 1)   
SET IDENTITY_INSERT Tiers OFF  

------------------------------------- PERSONS -----------------------------------------------------------------------
INSERT INTO [Persons]([id],[first_name],[sex],[identification_data],[last_name],[birth_date],[household_head],[nb_of_dependents],
[nb_of_children],[children_basic_education],[livestock_number],[livestock_type],[landplot_size],[home_size],[home_time_living_in],
[capital_other_equipments],[activity_id],[experience],[nb_of_people],[image_path],
[father_name],[birth_place],[nationality],[study_level],[SS],[CAF],[housing_situation],[handicapped],
[professional_experience],[professional_situation],[first_contact],[family_situation],[mother_name])
VALUES (6,'Mariam','F','12345','EL FANIDI', '2000-01-01',0,1,3,2,4,'Mouses',3.5,100,3,'Nothing',1,2,4,'Not set','Test',
'Paris','France','College','2341','98765','locataire',1,'no','yes','2008-02-02','test','Tata')

INSERT INTO [Persons]([id],[first_name],[sex],[identification_data],[last_name],[household_head]) VALUES (4,'Nicolas','M','1234567','MANGIN',1)        
INSERT INTO [Persons]([id],[first_name],[last_name],[sex],[household_head],[handicapped],[identification_data],[activity_id])VALUES(2,'Karim','DDD','F',0,0,'7654321',1)
------------------------------------- ROLES -------------------------------------------------------------------------      
SET IDENTITY_INSERT Roles ON  
INSERT INTO [dbo].[Roles] ([id],[code], [deleted]) VALUES (1,'ADMIN',0)
INSERT INTO [dbo].[Roles] ([id],[code], [deleted]) VALUES (2,'CASHI',0)
INSERT INTO [dbo].[Roles] ([id],[code], [deleted]) VALUES (3,'LOF',0)
INSERT INTO [dbo].[Roles] ([id],[code], [deleted]) VALUES (4,'SUPER',0)
INSERT INTO [dbo].[Roles] ([id],[code], [deleted]) VALUES (5,'VISIT',0)
SET IDENTITY_INSERT Roles OFF


------------------------------------- USERS -------------------------------------------------------------------------      
SET IDENTITY_INSERT Users ON  
INSERT INTO Users([id],[deleted],[user_name],[user_pass],[role_code],[first_name],[last_name],[mail], [sex]) VALUES(1,0,'ZOU','ZOU','ADMIN','ZOU','ZOU','gzou@octo.com', 'M')  
INSERT INTO Users([id],[deleted],[user_name],[user_pass],[role_code],[mail], [sex]) VALUES(2,0,'yu','xiaoqian','ADMIN','gzou@octo.com', 'F')  
INSERT INTO Users([id],[deleted],[user_name],[user_pass],[role_code],[mail], [sex]) VALUES(3,0,'azam','toto','LOF','azam@octo.com', 'M')  
INSERT INTO Users([id],[deleted],[user_name],[user_pass],[role_code],[first_name],[last_name],[mail], [sex]) VALUES(4,0,'user','password','SUPER','Nicolas','MANGIN','nma@octo.com', 'F')  
INSERT INTO Users([id],[deleted],[user_name],[user_pass],[role_code],[first_name],[mail], [sex]) VALUES(5,0,'mariam','mariam','ADMIN','Not Set','mariam@octo.com', 'F')  
INSERT INTO Users([id],[deleted],[user_name],[user_pass],[role_code],[first_name],[mail]) VALUES(6,1,'nicolas','nicolas','ADMIN','Not Set','nicolas@octo.com')  
SET IDENTITY_INSERT Users OFF

insert into UsersBranches(user_id, branch_id) values(1,1)

------------------------------------- USERROLE -------------------------------------------------------------------------      
INSERT INTO [dbo].[UserRole] ([user_id], [role_id]) VALUES (1,1)
INSERT INTO [dbo].[UserRole] ([user_id], [role_id]) VALUES (2,1)
INSERT INTO [dbo].[UserRole] ([user_id], [role_id]) VALUES (3,3)
INSERT INTO [dbo].[UserRole] ([user_id], [role_id]) VALUES (4,4)
INSERT INTO [dbo].[UserRole] ([user_id], [role_id]) VALUES (5,1)
INSERT INTO [dbo].[UserRole] ([user_id], [role_id]) VALUES (6,1)

------------------------------------- CORPORATES --------------------------------------------------------------------    
INSERT INTO [Corporates]([id],[name], [deleted]) VALUES(5,'Natexis',0) 
INSERT INTO [Corporates]([id],[name], [deleted], [sigle], [small_name], [volunteer_count], [agrement_date],
[agrement_solidarity],[insertionType],[employee_count],[siret],[activity_id],[legalForm],[date_create]) 
VALUES(9,'PizzaPino',0,
'TestTest','Te',1,'01/01/2008',1,'insertionType',2,'Teriii',1,'legalForm','2008-08-08')    
INSERT INTO [Corporates]([id],[name], [deleted]) VALUES(10,'SA',0) 

------------------------------------- GROUPS ------------------------------------------------------------------------      
INSERT INTO [Groups](id,[name])VALUES(1,'Group Nicolas')  
INSERT INTO [Groups](id,[name],[establishment_date],[comments])VALUES(3,'Group Nicolas Bis','2008-08-01','test')  
------------------------------------- VILLAGES ------------------------------------------------------------------------
INSERT INTO Villages (id, [name], establishment_date, loan_officer, meeting_day) VALUES (11,'Village','01/01/2008',1,NULL)

------------------------------------- GROUPSBELONGING ----------------------------------------------------   
INSERT INTO [PersonGroupBelonging](person_id,group_id,left_date,is_leader,currently_in,joined_date)VALUES(2,1,null,1,1,'2008-02-02') 
INSERT INTO [PersonGroupBelonging](person_id,group_id,left_date,is_leader,currently_in,joined_date)VALUES(2,3,null,1,1,'2008-02-02')   

------------------------------------- FUNDINGLINEEVENTS ----------------------------------------------------   
SET IDENTITY_INSERT FundingLineEvents ON  
INSERT INTO [FundingLineEvents]([id],[fundingline_id],[code],[direction],[deleted],[amount],[creation_date],[type]) VALUES(1,3,'Test',1,0,100000,'2007-01-01',1)  
INSERT INTO [FundingLineEvents]([id],[fundingline_id],[code],[direction],[deleted],[amount],[creation_date],[type]) VALUES(2,3,'fgg',2,0,100,'2007-02-02',2)  
INSERT INTO [FundingLineEvents]([id],[fundingline_id],[code],[direction],[deleted],[amount],[creation_date],[type]) VALUES(3,3,'jkk',1,0,10,'2007-03-03',3)  
INSERT INTO [FundingLineEvents]([id],[fundingline_id],[code],[direction],[deleted],[amount],[creation_date],[type]) VALUES(4,3,'jkk',2,0,1111,'2007-04-04',4) 
INSERT INTO [FundingLineEvents]([id],[fundingline_id],[code],[direction],[deleted],[amount],[creation_date],[type]) VALUES(5,3,'Test2',2,1,1111,'2007-05-05',5)  
SET IDENTITY_INSERT [FundingLineEvents] OFF  

------------------------------------- INSTALLMENTTYPES ----------------------------------------------------   
SET IDENTITY_INSERT InstallmentTypes ON  
INSERT INTO InstallmentTypes(id,name,nb_of_days,nb_of_months)VALUES(1,'monthly',0,1)
INSERT INTO InstallmentTypes(id,name,nb_of_days,nb_of_months)VALUES(2,'weekly',7,0)
SET IDENTITY_INSERT InstallmentTypes OFF  

------------------------------------- PACKAGES ----------------------------------------------------   
SET IDENTITY_INSERT Packages ON
INSERT INTO [Packages]([id],[deleted],[code],[name],[client_type],[amount],[interest_rate],[installment_type],[grace_period],[number_of_installments],
[anticipated_total_repayment_penalties],[loan_type],[keep_expected_installment],[charge_interest_within_grace_period],[anticipated_total_repayment_base],
[non_repayment_penalties_based_on_overdue_interest],[non_repayment_penalties_based_on_initial_amount],[non_repayment_penalties_based_on_olb]
,[non_repayment_penalties_based_on_overdue_principal],[fundingLine_id],[currency_id])
VALUES (1,0,'Code','Product1','I',1000,1,1,1,6,10,3,1,1,1,2,3,4,5,1, 1) 
INSERT INTO [Packages]([id],[deleted],[code],[name],[client_type],[amount_min],[amount_max],[interest_rate_min],[interest_rate_max],[installment_type],
[grace_period_min],[grace_period_max],[number_of_installments_min],[number_of_installments_max],[anticipated_total_repayment_penalties_min],
[anticipated_total_repayment_penalties_max],[loan_type],[keep_expected_installment],
[charge_interest_within_grace_period],[anticipated_total_repayment_base],[non_repayment_penalties_based_on_overdue_interest_min]
,[non_repayment_penalties_based_on_initial_amount_min],[non_repayment_penalties_based_on_olb_min],[non_repayment_penalties_based_on_overdue_principal_min],
[non_repayment_penalties_based_on_overdue_interest_max],[non_repayment_penalties_based_on_initial_amount_max],[non_repayment_penalties_based_on_olb_max]
,[non_repayment_penalties_based_on_overdue_principal_max],[fundingLine_id],[currency_id])
VALUES (2,0,'Code2','Product2','C',1000,10111,1,3,1,1,5,3,10,1,4,1,1,1,1,1,11,2,12,3,13,4,14,1,1)
INSERT INTO [Packages]([id],[deleted],[code],[name],[client_type],[amount],[interest_rate],[installment_type],[grace_period],[number_of_installments],
[anticipated_total_repayment_penalties],[loan_type],[keep_expected_installment],[charge_interest_within_grace_period],[anticipated_total_repayment_base],
[non_repayment_penalties_based_on_overdue_interest],[non_repayment_penalties_based_on_initial_amount],[non_repayment_penalties_based_on_olb]
,[non_repayment_penalties_based_on_overdue_principal],[fundingLine_id],[currency_id])
VALUES (3,1,'Code','ProductDeleted','I',1000,1,1,1,6,10,1,1,1,1,2,3,4,5,1,1) 
INSERT INTO [Packages]([id],[deleted],[code],[name],[client_type],[amount],[interest_rate],[installment_type],[grace_period],[number_of_installments],
[anticipated_total_repayment_penalties],[loan_type],[keep_expected_installment],[charge_interest_within_grace_period],[anticipated_total_repayment_base],
[non_repayment_penalties_based_on_overdue_interest],[non_repayment_penalties_based_on_initial_amount],[non_repayment_penalties_based_on_olb]
,[non_repayment_penalties_based_on_overdue_principal],[fundingLine_id],[currency_id])
VALUES (4,0,'Code','Product4','-',1000,1,1,1,6,10,1,1,1,1,2,3,4,5,1,1) 
SET IDENTITY_INSERT Packages OFF

------------------------------------- PackagesClientTypes ----------------------------------------------------   
INSERT PackagesClientTypes (client_type_id, package_id) SELECT 3, 1
INSERT PackagesClientTypes (client_type_id, package_id) SELECT 4, 2
INSERT PackagesClientTypes (client_type_id, package_id) SELECT 3, 3
INSERT PackagesClientTypes (client_type_id, package_id) SELECT 1, 4

------------------------------------- PROJECTS ----------------------------------------------------          
SET IDENTITY_INSERT Projects ON  
INSERT INTO [Projects] ([id],[tiers_id],[status],[name],[code],[aim],[begin_date],[abilities],[experience],[market],[concurrence],[purpose])
VALUES(1,1,1,'DEDE','code','aim','2007-01-01','aibilities','experience','market','concurrence','purpose')  
INSERT INTO [Projects] ([id],[tiers_id],[status],[name],[code],[aim],[begin_date],[abilities],[experience],[market],[concurrence],[purpose])
VALUES(2,2,1,'DEUX','code','aim','2007-01-01','aibilities','experience','market','concurrence','purpose') 
INSERT INTO [Projects] ([id],[tiers_id],[status],[name],[code],[aim],[begin_date],[abilities],[experience],[market],[concurrence],[purpose])
VALUES(3,10,1,'Postpone','code','aim','2012-01-01','aibilities','experience','market','concurrence','purpose') 
SET IDENTITY_INSERT Projects OFF  

------------------------------------- CONTRACTS ----------------------------------------------------   
SET IDENTITY_INSERT Contracts ON  
INSERT INTO [Contracts] ([id],[contract_code],[branch_code],[creation_date],[start_date],[close_date],[closed],[project_id],[activity_id])
VALUES(1,'441','branch1','2007-01-01','2008-01-02','2009-01-01',0,1,1)  
INSERT INTO [Contracts] ([id],[contract_code],[branch_code],[creation_date],[start_date],[close_date],[closed],[project_id],[activity_id])
VALUES(2,'442','branch2','2007-01-01','2008-01-02','2009-01-01',0,1,1)  
INSERT INTO [Contracts] ([id],[contract_code],[branch_code],[creation_date],[start_date],[close_date],[closed],[project_id],[activity_id])
VALUES(3,'443','branch3','2007-01-01','2008-01-02','2009-01-01',0,1,1)  
INSERT INTO [Contracts] ([id],[contract_code],[branch_code],[creation_date],[start_date],[close_date],[closed],[project_id],[activity_id])
VALUES(4,'444','branch4','2007-01-01','2008-01-02','2009-01-01',0,1,1)  
INSERT INTO [Contracts] ([id],[contract_code],[branch_code],[creation_date],[start_date],[close_date],[closed],[project_id],[status], [nsg_id],[activity_id])
VALUES(5,'445','branch5','2007-01-01','2012-01-02','2018-01-01',0,3,8,11,1)
SET IDENTITY_INSERT Contracts OFF  

------------------------------------- CREDIT ----------------------------------------------------       
INSERT INTO Credit(id,package_id,amount,interest_rate,installment_type,nb_of_installment,anticipated_total_repayment_penalties,
disbursed,loanofficer_id,written_off,rescheduled,bad_loan,non_repayment_penalties_based_on_overdue_principal,
non_repayment_penalties_based_on_initial_amount,non_repayment_penalties_based_on_olb,non_repayment_penalties_based_on_overdue_interest,
fundingLine_id) VALUES(1,1,1000,10,1,3,100,0,1,10,0,0,0,40,40,40,1)
INSERT INTO Credit(id,package_id,amount,interest_rate,installment_type,nb_of_installment,anticipated_total_repayment_penalties,
disbursed,loanofficer_id,written_off,rescheduled,bad_loan,non_repayment_penalties_based_on_overdue_principal,
non_repayment_penalties_based_on_initial_amount,non_repayment_penalties_based_on_olb,non_repayment_penalties_based_on_overdue_interest,
fundingLine_id) VALUES(5,1,1000,10,1,3,100,0,1,10,0,0,0,40,40,40,1)

SET IDENTITY_INSERT [ContractEvents] ON  
INSERT INTO [ContractEvents]([id],[event_type],[contract_id],[event_date],[user_id],[is_deleted]) VALUES(1, 'LODE',1,'2009-1-1',1,0)
INSERT INTO [ContractEvents]([id],[event_type],[contract_id],[event_date],[user_id],[is_deleted]) VALUES(2, 'RBLE',1,'2009-2-1',1,0)
INSERT INTO [ContractEvents]([id],[event_type],[contract_id],[event_date],[user_id],[is_deleted]) VALUES(3, 'PDLE',1,'2009-3-1',1,0)
INSERT INTO [ContractEvents]([id],[event_type],[contract_id],[event_date],[user_id],[is_deleted]) VALUES(4, 'RGLE',1,'2009-4-1',1,0)
INSERT INTO [ContractEvents]([id],[event_type],[contract_id],[event_date],[user_id],[is_deleted]) VALUES(5, 'WROE',1,'2009-5-1',1,0)
INSERT INTO [ContractEvents]([id],[event_type],[contract_id],[event_date],[user_id],[is_deleted]) VALUES(6, 'ROLE',1,'2009-6-1',1,0)
INSERT INTO [ContractEvents]([id],[event_type],[contract_id],[event_date],[user_id],[is_deleted]) VALUES(7, 'LIAE',1,'2009-7-1',1,0)
INSERT INTO [ContractEvents]([id],[event_type],[contract_id],[event_date],[user_id],[is_deleted]) VALUES(8, 'RRLE',1,'2009-8-1',1,0)
INSERT INTO [ContractEvents]([id],[event_type],[contract_id],[event_date],[user_id],[is_deleted]) VALUES(9, 'LOVE',5,'2007-1-1',1,0)
SET IDENTITY_INSERT [ContractEvents] OFF
  
------------------------------------- LOANDISBURSMENTEVENTS -------------------------------------------------------
INSERT INTO [LoanDisbursmentEvents] ([id],[amount],[fees]) VALUES(1,1000,10)


------------------------------------- REPAYMENTEVENTS -------------------------------------------------------
INSERT INTO [RepaymentEvents] ([id],[past_due_days],[principal],[interests],[penalties],[installment_number]) VALUES (2,10,1000,24,2,1)
INSERT INTO [RepaymentEvents] ([id],[past_due_days],[principal],[interests],[penalties],[installment_number]) VALUES (4,10,1000,24,2,1)
INSERT INTO [RepaymentEvents] ([id],[past_due_days],[principal],[interests],[penalties],[installment_number]) VALUES (8,105,544,5,7,8)


------------------------------------- WRITEOFFEVENTS -------------------------------------------------------
INSERT INTO [WriteOffEvents] ([id], [olb], [accrued_interests], [accrued_penalties], [past_due_days], [overdue_principal]) VALUES (5, 1000, 20, 24, 1, 0)


------------------------------------- RESCHEDULINGLOANEVENT -------------------------------------------------------
INSERT INTO [ReschedulingOfALoanEvents] ([id],[amount],[nb_of_maturity],[date_offset]) VALUES (6,1000,2,0)


------------------------------------- LOANINTERESTACCRUINGEVENT -------------------------------------------------------
INSERT INTO [LoanInterestAccruingEvents] ([id],[interest_prepayment],[accrued_interest],[rescheduled],[installment_number]) VALUES (7,342,22,1,3)


------------------------------------- INSTALLMENTS ----------------------------------------------------       
INSERT INTO [Installments]([expected_date],[interest_repayment] ,[capital_repayment],[contract_id],[number],[paid_interest],[paid_capital],
[fees_unpaid])VALUES ('2007-01-01',2,20,1,1,2,10,0)  
INSERT INTO [Installments]([expected_date],[interest_repayment] ,[capital_repayment],[contract_id],[number],[paid_interest],[paid_capital],
[fees_unpaid])VALUES ('2007-02-01',2,20,1,2,2,10,0)  
INSERT INTO [Installments]([expected_date],[interest_repayment] ,[capital_repayment],[contract_id],[number],[paid_interest],[paid_capital],
[fees_unpaid])VALUES ('2007-03-01',2,20,1,3,2,10,0)
INSERT INTO [Installments]([expected_date],[interest_repayment] ,[capital_repayment],[contract_id],[number],[paid_interest],[paid_capital],
[fees_unpaid])VALUES ('2007-01-01',2,20,5,1,2,10,0)  
INSERT INTO [Installments]([expected_date],[interest_repayment] ,[capital_repayment],[contract_id],[number],[paid_interest],[paid_capital],
[fees_unpaid])VALUES ('2007-02-01',2,20,5,2,2,10,0)  
INSERT INTO [Installments]([expected_date],[interest_repayment] ,[capital_repayment],[contract_id],[number],[paid_interest],[paid_capital],
[fees_unpaid])VALUES ('2007-03-01',2,20,5,3,2,10,0)

------------------------------------- SAVING PRODUCTS ----------------------------------------------------       
SET IDENTITY_INSERT [SavingProducts] ON
INSERT INTO [SavingProducts] ([id],[deleted],[name],[code],[client_type],[initial_amount_min],[initial_amount_max],[balance_min],[balance_max],
[withdraw_min],[withdraw_max],[deposit_min],[deposit_max],[transfer_min],[transfer_max],[interest_rate],[interest_rate_min],[interest_rate_max], 
[currency_id], [product_type]) 
VALUES (1,0,'SavingProduct1','P1','-',100,500,0, 1000, 100, 150, 200, 250, 100, 300, NULL, 0.2, 0.3,1, 'B')
INSERT INTO [SavingBookProducts] ([id],[interest_base], [interest_frequency], 
[withdraw_fees_type],[flat_withdraw_fees_min],[flat_withdraw_fees_max],
[transfer_fees_type],[flat_transfer_fees_min],[flat_transfer_fees_max],
management_fees, management_fees_freq)
VALUES (1,10, 30,1,1,5, 1, 1, 5, 10.00, 1)
INSERT INTO [SavingProducts] ([id],[deleted],[name],[code],[client_type],[initial_amount_min],[initial_amount_max],[balance_min],[balance_max],
[withdraw_min],[withdraw_max],[deposit_min],[deposit_max],[transfer_min],[transfer_max],[interest_rate],[interest_rate_min],[interest_rate_max], 
[currency_id], [product_type]) 
VALUES (2,0,'TermProduct1','T1','-',100,50,0, 1000, 0, 0, 0, 0, 0,0,0.1, NULL, NULL,1, 'T')
INSERT INTO [TermDepositProducts] ([id],[installment_types_id],[number_period],[interest_frequency],[withdrawal_fees_type],[withdrawal_fees])
VALUES (2,1,3,0.1,0,0.04)
INSERT INTO [SavingProducts] ([id],[deleted],[name],[code],[client_type],[initial_amount_min],[initial_amount_max],[balance_min],[balance_max],
[withdraw_min],[withdraw_max],[deposit_min],[deposit_max],[transfer_min],[transfer_max],[interest_rate],[interest_rate_min],[interest_rate_max], 
[currency_id], [product_type]) 
VALUES (3,0,'CompulsoryProduct1','C1','-',100,500,0, 1000, 0, 0, 0, 0, 0,0,0.1, NULL, NULL,1, 'C')
SET IDENTITY_INSERT [SavingProducts] OFF

------------------------------------- SAVING CONTRACTS ----------------------------------------------------         
SET IDENTITY_INSERT [SavingContracts] ON
INSERT INTO [SavingContracts] ([id], [product_id],[user_id],[code],[status],[tiers_id],[creation_date],[interest_rate]) 
VALUES	(1, 1,6,'S/BC/2007/SAVIN-1/ELFA-6',1,6,'2007-01-01', 0.25)
INSERT INTO [SavingBookContracts] ([id], [flat_withdraw_fees], [flat_transfer_fees])
VALUES (1, 3, 3)
INSERT INTO [SavingContracts] ([id], [product_id],[user_id],[code],[status],[tiers_id],[creation_date],[interest_rate],[closed_date]) 
VALUES	(2, 1,6,'S/BC/2008/SAVIN-1/ELFA-7',2,6,'2008-01-02', 0.3, '2008-10-01')
INSERT INTO [SavingBookContracts] ([id], [flat_withdraw_fees], [flat_transfer_fees])
VALUES (2, 3, 3)
SET IDENTITY_INSERT [SavingContracts] OFF

------------------------------------- SAVING EVENTS ----------------------------------------------------  
SET IDENTITY_INSERT [SavingEvents] ON
INSERT INTO [SavingEvents] ([id],[user_id],[contract_id],[code],[amount],[description],[deleted],[creation_date],[cancelable],[is_fired])
VALUES  (200,6,1,'SVIE',100,'',0,'2007-01-01',0,0)
SET IDENTITY_INSERT [SavingEvents] OFF

SET IDENTITY_INSERT [AccountsCategory] ON	
INSERT INTO [AccountsCategory] ([id], [name]) VALUES (1, 'BalanceSheetAsset')
INSERT INTO [AccountsCategory] ([id], [name]) VALUES (2, 'BalanceSheetLiabilities')
INSERT INTO [AccountsCategory] ([id], [name]) VALUES (3, 'ProfitAndLossIncome')
INSERT INTO [AccountsCategory] ([id], [name]) VALUES (4, 'ProfitAndLossExpense')
SET IDENTITY_INSERT [AccountsCategory] OFF

SET IDENTITY_INSERT [ChartOfAccounts] ON
INSERT INTO [ChartOfAccounts]([id],[account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) VALUES(1, '1011','Cash',1,'CASH',1, 1)
INSERT INTO [ChartOfAccounts]([id],[account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) VALUES(2, '2031','Cash Credit',1,'CASH_CREDIT',1, 1)
INSERT INTO [ChartOfAccounts]([id],[account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) VALUES(3, '2032','Rescheduled Loans',1,'RESCHEDULED_LOANS',1, 1)
INSERT INTO [ChartOfAccounts]([id],[account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) VALUES(4, '2037','Accrued interests receivable',1,'ACCRUED_INTERESTS_LOANS',1, 1)
INSERT INTO [ChartOfAccounts]([id],[account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) VALUES(5, '2038','Accrued interests on rescheduled loans',1,'ACCRUED_INTERESTS_RESCHEDULED_LOANS',1, 1)
INSERT INTO [ChartOfAccounts]([id],[account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) VALUES(6, '2911','Bad Loans',1,'BAD_LOANS',1, 1)
INSERT INTO [ChartOfAccounts]([id],[account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) VALUES(7, '2921','Unrecoverable Bad Loans',1,'UNRECO_BAD_LOANS',1, 1)
INSERT INTO [ChartOfAccounts]([id],[account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) VALUES(8, '2971','Interest on Past Due Loans',1,'INTERESTS_ON_PAST_DUE_LOANS',1, 1)
INSERT INTO [ChartOfAccounts]([id],[account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) VALUES(9, '2972','Penalties on Past Due Loans',1,'PENALTIES_ON_PAST_DUE_LOANS_ASSET',1, 1)
INSERT INTO [ChartOfAccounts]([id],[account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) VALUES(10, '2991','Loan Loss Reserve',0,'LOAN_LOSS_RESERVE',2, 1)
INSERT INTO [ChartOfAccounts]([id],[account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) VALUES(11, '3882','Deferred Income',0,'DEFERRED_INCOME',2, 1)
INSERT INTO [ChartOfAccounts]([id],[account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) VALUES(12, '6712','Provision on bad loans',1,'PROVISION_ON_BAD_LOANS',3, 1)
INSERT INTO [ChartOfAccounts]([id],[account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) VALUES(13, '6751','Loan Loss',1,'LOAN_LOSS',3, 1)
INSERT INTO [ChartOfAccounts]([id],[account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) VALUES(14, '7021','Interests on cash credit',0,'INTERESTS_ON_CASH_CREDIT',4, 1)
INSERT INTO [ChartOfAccounts]([id],[account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) VALUES(15, '7022','Interests on rescheduled loans',0,'INTERESTS_ON_RESCHEDULED_LOANS',4, 1)
INSERT INTO [ChartOfAccounts]([id],[account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) VALUES(16, '7027','Penalties on past due loans',0,'PENALTIES_ON_PAST_DUE_LOANS_INCOME',4, 1)
INSERT INTO [ChartOfAccounts]([id],[account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) VALUES(17, '7028','Interests on bad loans',0,'INTERESTS_ON_BAD_LOANS',4, 1)
INSERT INTO [ChartOfAccounts]([id],[account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) VALUES(18, '7029','Commissions',0,'COMMISSIONS',4, 1)
INSERT INTO [ChartOfAccounts]([id],[account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) VALUES(19, '7712','Provision write off',0,'PROVISION_WRITE_OFF',4, 1)
INSERT INTO [ChartOfAccounts]([id],[account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) VALUES(20, '5211','Loan Loss Allowance on Current Loans',0,'LIABILITIES_LOAN_LOSS_CURRENT',2, 1)
INSERT INTO [ChartOfAccounts]([id],[account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) VALUES(21, '6731','Loan Loss Allowance on Current Loans',1,'EXPENSES_LOAN_LOSS_CURRENT',3, 1)
INSERT INTO [ChartOfAccounts]([id],[account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) VALUES(22, '7731','Resumption of Loan Loss allowance on current loans',0,'INCOME_LOAN_LOSS_CURRENT',4, 1)
INSERT INTO [ChartOfAccounts]([id],[account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) VALUES(23, '1322','Accounts and Terms Loans',0,'ACCOUNTS_AND_TERM_LOANS', 2, 1)
INSERT INTO [ChartOfAccounts]([id],[account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) VALUES(24, '221','Savings',0,'SAVINGS', 2, 1)
INSERT INTO [ChartOfAccounts]([id],[account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) VALUES(25, '2261','Account payable interests on Savings Books',0,'ACCOUNT_PAYABLE_INTERESTS_ON_SAVINGS_BOOKS',3, 1)
INSERT INTO [ChartOfAccounts]([id],[account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) VALUES(26, '60132','Interests on deposit account',1,'INTERESTS_ON_DEPOSIT_ACCOUNT',3, 1)
INSERT INTO [ChartOfAccounts]([id],[account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) VALUES(27, '42802','Recovery of charged off assets',0,'RECOVERY_OF_CHARGED_OFF_ASSETS', 3, 1)
INSERT INTO [ChartOfAccounts]([id],[account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) VALUES(28, '9330201','NON_BALANCE_COMMITTED_FUNDS', 0,'NON_BALANCE_COMMITTED_FUNDS', 3, 1)
INSERT INTO [ChartOfAccounts]([id],[account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) VALUES(29, '9330202','NON_BALANCE_VALIDATED_LOANS', 1,'NON_BALANCE_VALIDATED_LOANS', 1, 1)
INSERT INTO [ChartOfAccounts]([id],[account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) VALUES(30, '222','Term Deposit',0,'TERM_DEPOSIT', 2, 1)
INSERT INTO [ChartOfAccounts]([id],[account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) VALUES(31, '223','Compulsory Savings',0,'COMPULSORY_SAVINGS', 2, 1)
INSERT INTO [ChartOfAccounts]([id],[account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) VALUES(32, '2262','Account payable interests on Term Deposit',0,'ACCOUNT_PAYABLE_INTERESTS_ON_TERM_DEPOSIT',3, 1)
INSERT INTO [ChartOfAccounts]([id],[account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) VALUES(33, '2263','Account payable interests on Compulsory Savings',0,'ACCOUNT_PAYABLE_INTERESTS_ON_COMPULSORY_SAVINGS',3, 1)
SET IDENTITY_INSERT [ChartOfAccounts] OFF

INSERT INTO EventTypes (event_type, description) VALUES('LOVE', 'Loan Validation Event')
INSERT INTO EventTypes (event_type, description) VALUES('LODE', 'Loan Disbursement Event')
INSERT INTO EventTypes (event_type, description) VALUES('RGLE', 'Repayment of Good Loan Event')
INSERT INTO EventTypes (event_type, description) VALUES('RBLE', 'Repayment of Bad Loan Event')
INSERT INTO EventTypes (event_type, description) VALUES('PDLE', 'Past Due Loan Event')
INSERT INTO EventTypes (event_type, description) VALUES('WROE', 'Write-Off Even')
INSERT INTO EventTypes (event_type, description) VALUES('ROWE', 'Repayment Over Write-Off')
INSERT INTO EventTypes (event_type, description) VALUES('ROLE', 'Reschedule Of Loan Event')
INSERT INTO EventTypes (event_type, description) VALUES('RRLE', 'Repayment for Rescheduled Loan Event')
INSERT INTO EventTypes (event_type, description) VALUES('TEET', 'Tranche event')
INSERT INTO EventTypes (event_type, description) VALUES('APR', 'Anticipated Partial Repayment')
INSERT INTO EventTypes (event_type, description) VALUES('ATR', 'Anticipated Total Repayment')
INSERT INTO EventTypes (event_type, description) VALUES('APTR', 'Anticipated Partial Total Repayment')

INSERT INTO EventAttributes (event_type, name) VALUES('RRLE', 'principal')
INSERT INTO EventAttributes (event_type, name) VALUES('RRLE', 'penalties')
INSERT INTO EventAttributes (event_type, name) VALUES('RRLE', 'commissions')
INSERT INTO EventAttributes (event_type, name) VALUES('RRLE', 'interests')

INSERT INTO EventAttributes (event_type, name) VALUES('RGLE', 'principal')
INSERT INTO EventAttributes (event_type, name) VALUES('RGLE', 'penalties')
INSERT INTO EventAttributes (event_type, name) VALUES('RGLE', 'commissions')
INSERT INTO EventAttributes (event_type, name) VALUES('RGLE', 'interests')

INSERT INTO EventAttributes (event_type, name) VALUES('RBLE', 'principal')
INSERT INTO EventAttributes (event_type, name) VALUES('RBLE', 'penalties')
INSERT INTO EventAttributes (event_type, name) VALUES('RBLE', 'commissions')
INSERT INTO EventAttributes (event_type, name) VALUES('RBLE', 'interests')

INSERT INTO EventAttributes (event_type, name) VALUES('RRLE', 'principal')
INSERT INTO EventAttributes (event_type, name) VALUES('RRLE', 'penalties')
INSERT INTO EventAttributes (event_type, name) VALUES('RRLE', 'commissions')
INSERT INTO EventAttributes (event_type, name) VALUES('RRLE', 'interests')

INSERT INTO EventAttributes (event_type, name) VALUES('ATR', 'principal')
INSERT INTO EventAttributes (event_type, name) VALUES('ATR', 'penalties')
INSERT INTO EventAttributes (event_type, name) VALUES('ATR', 'commissions')
INSERT INTO EventAttributes (event_type, name) VALUES('ATR', 'interests')

INSERT INTO EventAttributes (event_type, name) VALUES('APR', 'principal')
INSERT INTO EventAttributes (event_type, name) VALUES('APR', 'penalties')
INSERT INTO EventAttributes (event_type, name) VALUES('APR', 'commissions')
INSERT INTO EventAttributes (event_type, name) VALUES('APR', 'interests')

INSERT INTO EventAttributes (event_type, name) VALUES('APTR', 'principal')
INSERT INTO EventAttributes (event_type, name) VALUES('APTR', 'penalties')
INSERT INTO EventAttributes (event_type, name) VALUES('APTR', 'commissions')
INSERT INTO EventAttributes (event_type, name) VALUES('APTR', 'interests')

INSERT INTO EventAttributes (event_type, name) VALUES('LODE', 'amount')
INSERT INTO EventAttributes (event_type, name) VALUES('LODE', 'fees')
INSERT INTO EventAttributes (event_type, name) VALUES('LODE', 'interest')
