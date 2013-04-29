-- Migrating Economic Activities for Individual loans
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EconomicActivityLoanHistory]') 
AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EconomicActivityLoanHistory](
	[contract_id] [int] NOT NULL,
	[person_id] [int] NOT NULL,
	[group_id] [int] NULL,
	[economic_activity_id] [int] NOT NULL,
	[deleted] [bit] NOT NULL
) ON [PRIMARY]
END
GO

-- Migrating Economic Activities for Individual loans
INSERT dbo.EconomicActivityLoanHistory (contract_id, person_id, group_id, economic_activity_id, deleted)
SELECT Cont.id AS [contract_id], Per.id AS [person_id], NULL AS [group_id], 
Doa.id AS [economic_activity_id], 0 AS [deleted]
FROM dbo.Contracts AS Cont
INNER JOIN dbo.Projects AS Pr ON Cont.project_id = Pr.id
INNER JOIN dbo.Tiers AS Tr ON Pr.tiers_id = Tr.id
INNER JOIN dbo.Persons AS Per ON Per.id = Tr.id
INNER JOIN dbo.DomainOfApplications AS Doa ON Per.activity_id = Doa.id
WHERE client_type_code = 'I'
GO

-- Migrating Economic Activities for Group loans
INSERT dbo.EconomicActivityLoanHistory (contract_id, person_id, group_id, economic_activity_id, deleted)
SELECT Cont.id AS [contract_id], Per.id AS [person_id], PGB.group_id AS [group_id], 
Doa.id AS [economic_activity_id], 0 AS [deleted]
FROM dbo.Contracts AS Cont
INNER JOIN dbo.Projects AS Pr ON Cont.project_id = Pr.id
INNER JOIN dbo.Tiers AS Tr ON Pr.tiers_id = Tr.id
INNER JOIN dbo.PersonGroupBelonging AS PGB ON PGB.group_id = Tr.id
LEFT JOIN dbo.Persons AS Per ON Per.id = PGB.person_id
INNER JOIN dbo.DomainOfApplications AS Doa ON Per.activity_id = Doa.id
WHERE client_type_code = 'G'
GO

-- Updating Corporate clients
UPDATE dbo.Corporates 
SET activity_id = (SELECT TOP 1 id FROM dbo.DomainOfApplications)
WHERE activity_id IS NULL
GO

-- Migrating Economic Activities for Corporate loans
INSERT dbo.EconomicActivityLoanHistory (contract_id, person_id, group_id, economic_activity_id, deleted)
SELECT Cont.id AS [contract_id], Corp.id AS [person_id], NULL AS [group_id], 
Doa.id AS [economic_activity_id], 0 AS [deleted]
FROM dbo.Contracts AS Cont
INNER JOIN dbo.Projects AS Pr ON Cont.project_id = Pr.id
INNER JOIN dbo.Tiers AS Tr ON Pr.tiers_id = Tr.id
INNER JOIN dbo.Corporates AS Corp ON Corp.id = Tr.id
INNER JOIN dbo.DomainOfApplications AS Doa ON Corp.activity_id = Doa.id
GO

-- Adding some actions for allow/prohibit in roles
INSERT INTO dbo.ActionItems ( class_name, method_name )
VALUES ( 'LoanServices', 'Reschedule' )
GO

INSERT INTO dbo.ActionItems ( class_name, method_name )
VALUES ( 'LoanServices', 'ManualInterestCalculation' )
GO

INSERT INTO dbo.ActionItems ( class_name, method_name )
VALUES ( 'LoanServices', 'ManualFeesCalculation' )
GO

INSERT INTO dbo.ActionItems ( class_name, method_name )
VALUES ( 'LoanServices', 'ChangeRepaymentType' )
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Statuses]'))
	BEGIN
		CREATE TABLE [dbo].[Statuses](
			[id] [int] IDENTITY(1,1) NOT NULL,
			[status_name] [nvarchar](50) NULL
		) ON [PRIMARY]
	END
	IF 0=(SELECT COUNT(*) FROM [dbo].[Statuses])
	BEGIN
		INSERT INTO [dbo].[Statuses] ([status_name]) VALUES ('Pending')
		INSERT INTO [dbo].[Statuses] ([status_name]) VALUES ('Validated')
		INSERT INTO [dbo].[Statuses] ([status_name]) VALUES ('Refused')
		INSERT INTO [dbo].[Statuses] ([status_name]) VALUES ('Abandoned')
		INSERT INTO [dbo].[Statuses] ([status_name]) VALUES ('Active')
		INSERT INTO [dbo].[Statuses] ([status_name]) VALUES ('Closed')
		INSERT INTO [dbo].[Statuses] ([status_name]) VALUES ('WrittenOff')
	END
GO

-- repayment event sequence restore
declare bizarre_contracts cursor for
select t.contract_id from (

	select i.contract_id, c.creation_date, c.contract_code, c.closed, cr.written_off, i.number, i.expected_date, i.capital_repayment, i.interest_repayment, re.principal, re.interests
	from Installments i
	right join (
		select ce.contract_id, re.installment_number, SUM(re.principal) principal, SUM(re.interests) interests
		from RepaymentEvents re
		left join ContractEvents ce on re.id = ce.id
		where ce.is_deleted = 0
		group by ce.contract_id, re.installment_number
	) as re on i.contract_id = re.contract_id and i.number = re.installment_number
	left join Contracts as c on c.id = i.contract_id
	left join Credit as cr on cr.id = c.id
	where (i.capital_repayment < re.principal or i.interest_repayment < re.interests) and i.capital_repayment > 0
	--and c.start_date > '2010-01-01'
	and c.start_date >= '2009-09-01'
	and c.closed = 0
	and cr.written_off = 0
	and i.contract_id not in (
		-- Exclude ATR contracts
		select contract_id
		from Installments 
		where capital_repayment <= 0 and interest_repayment <= 0
		group by contract_id
	)
	and i.contract_id not in (
		-- Exclude contracts for which sum(expected) < sum(paid)
		select i.contract_id from
		(
			select contract_id, SUM(capital_repayment) as principal, SUM(interest_repayment) as interest
			from Installments
			group by contract_id
		) as i
		left join (
			select ce.contract_id, sum(re.principal) as principal, SUM(re.interests) as interest
			from RepaymentEvents as re
			left join ContractEvents as ce on ce.id = re.id
			where ce.is_deleted = 0
			group by ce.contract_id
		) as re on i.contract_id = re.contract_id
		left join contracts as c on c.id = i.contract_id
		where (i.principal < re.principal or i.interest < re.interest)
		and c.closed = 0	
	)
	and i.contract_id not in (
		select ce.contract_id
		from RepaymentEvents re
		left join ContractEvents ce on ce.id = re.id
		where (re.principal < 0 or re.interests < 0) and ce.is_deleted = 0
	)

) as t
group by t.contract_id, t.contract_code, t.creation_date
order by t.contract_id

declare @contract_id int

open bizarre_contracts
fetch next from bizarre_contracts into @contract_id

while 0 = @@FETCH_STATUS
begin

declare @result_i table (
	id int not null
	, installment_number int not null
	, interests money not null
	, event_date datetime not null
)

declare @result_p table (
	id int not null
	, installment_number int not null
	, principal money not null
	, event_date datetime not null
	, commissions money not null
	, penalties money not null
)

delete from @result_i
delete from @result_p

declare @id int
declare @number int
declare @interests money
declare @principal money
declare @event_date datetime
declare @current_number int
declare @paid_portion money
declare @remainder money
declare @due money
declare @commissions money
declare @penalties money

set @current_number = 1
set @paid_portion = 0

-- PROCESS INTEREST
declare re_cur_i cursor for
select re.id, installment_number, interests, event_date
from RepaymentEvents re
left join ContractEvents ce on ce.id = re.id
where ce.contract_id = @contract_id and ce.is_deleted = 0
order by event_date, installment_number
open re_cur_i

fetch next from re_cur_i
into @id, @number, @interests, @event_date

while 0 = @@FETCH_STATUS
begin
	
	select @due = interest_repayment
	from Installments
	where contract_id = @contract_id and number = @current_number
	
	if 0 = @interests and @due > 0
	begin
		insert into @result_i values (@id, @number, @interests, @event_date)
		goto next_iter_i
	end
	
	set @remainder = @interests

once_again_i:	
	select @due = interest_repayment
	from Installments
	where contract_id = @contract_id and number = @current_number
	set @due = @due - @paid_portion

	if @remainder > @due
	begin
		-- Paid more
		insert into @result_i values (@id, @current_number, @due, @event_date)
		set @remainder = @remainder - @due
		set @current_number = @current_number + 1
		set @paid_portion = 0
		goto once_again_i
	end
	else if @remainder < @due
	begin
		-- Paid less
		set @paid_portion = @paid_portion + @remainder
		insert into @result_i values(@id, @current_number, @remainder, @event_date)
	end
	else
	begin
		-- Paid exactly the amount
		insert into @result_i values(@id, @current_number, @remainder, @event_date)
		set @paid_portion = 0
		set @current_number = @current_number + 1
	end

next_iter_i:	
	fetch next from re_cur_i
	into @id, @number, @interests, @event_date
end

close re_cur_i
deallocate re_cur_i

-- PROCESS PRINCIPAL
set @current_number = 1
set @paid_portion = 0

declare @fields_set bit

declare re_cur_p cursor for
select re.id, installment_number, principal, event_date, commissions, penalties
from RepaymentEvents re
left join ContractEvents ce on ce.id = re.id
where ce.contract_id = @contract_id and ce.is_deleted = 0
order by event_date, installment_number
open re_cur_p

fetch next from re_cur_p
into @id, @number, @principal, @event_date, @commissions, @penalties

while 0 = @@FETCH_STATUS
begin
	set @fields_set = 0
	
	select @due = capital_repayment
	from Installments
	where contract_id = @contract_id and number = @current_number

	if 0 = @principal and @due > 0
	begin
		insert into @result_p values (@id, @number, @principal, @event_date, @commissions, @penalties)
		goto next_iter_p
	end
	
	set @remainder = @principal

once_again_p:
	select @due = capital_repayment
	from Installments
	where contract_id = @contract_id and number = @current_number
	set @due = @due - @paid_portion

	set @commissions = case when 1 = @fields_set then 0 else @commissions end
	set @penalties = case when 1 = @fields_set then 0 else @penalties end

	if @remainder > @due
	begin
		-- Paid more		
		insert into @result_p values (@id, @current_number, @due, @event_date, @commissions, @penalties)
		set @remainder = @remainder - @due
		set @current_number = @current_number + 1
		set @paid_portion = 0
		set @fields_set = 1
		goto once_again_p
	end
	else if @remainder < @due
	begin
		-- Paid less
		set @paid_portion = @paid_portion + @remainder
		insert into @result_p values(@id, @current_number, @remainder, @event_date, @commissions, @penalties)
	end
	else
	begin
		-- Paid exactly the amount
		insert into @result_p values(@id, @current_number, @remainder, @event_date, @commissions, @penalties)
		set @paid_portion = 0
		set @current_number = @current_number + 1
	end

next_iter_p:
	fetch next from re_cur_p
	into @id, @number, @principal, @event_date, @commissions, @penalties
end

close re_cur_p
deallocate re_cur_p

-- DELETE EXISTING EVENTS
delete from RepaymentEvents where id in (
	select re.id
	from RepaymentEvents re
	left join ContractEvents ce on ce.id = re.id
	where ce.contract_id = @contract_id and ce.is_deleted = 0
	group by re.id
)

-- MERGE AND SAVE RESULTS
insert into RepaymentEvents (id, installment_number, principal, interests, commissions, penalties, past_due_days)
select id, installment_number, SUM(principal) as principal, SUM(interests) as interests
, SUM(commissions) as commissions, SUM(penalties) as penalties, 0 as past_due_days
from (
	select id, installment_number, 0 as principal, interests, event_date, 0 as commissions, 0 as penalties
	from @result_i
	where interests > 0
	union all
	select id, installment_number, principal, 0 as interests, event_date, commissions, penalties	
	from @result_p
	where principal > 0 or commissions > 0 or penalties > 0
) as t
group by id, installment_number, event_date

fetch next from bizarre_contracts into @contract_id
end

close bizarre_contracts
deallocate bizarre_contracts
GO


CREATE TABLE dbo.Reports
(
	name VARCHAR(255),
	starred BIT DEFAULT 0
)
GO

UPDATE [TechnicalParameters] SET [value] = 'v2.8.11' WHERE [name] = 'VERSION'
GO
