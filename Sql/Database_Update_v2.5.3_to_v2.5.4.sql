-- Add Repayement Cash Receipt as a custom report
DELETE FROM [ReportParametrs] WHERE [data_object_id] IN (SELECT id FROM [ReportDataObjectSource] WHERE [sub_report_name] = 'RepaymentCashReceipt.rpt')
DELETE FROM [ReportDataObjectSource] WHERE [sub_report_name] = 'RepaymentCashReceipt.rpt'
DELETE FROM [ReportObject] WHERE [report_name] = 'RepaymentCashReceipt.rpt'
DECLARE @ObjectID INT
DECLARE @SourceID INT
INSERT INTO ReportObject (report_name) VALUES ('RepaymentCashReceipt.rpt') 
SELECT @ObjectID = @@IDENTITY
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'RepaymentCashReceipt.rpt', 'GetRepaymentCashReceiptInfo', 'P') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type) VALUES (@SourceID, '@contract_id', 'TextType') 
GO

INSERT INTO [GeneralParameters]([key], [value]) VALUES('DONOT_SKIP_WEEKENDS_IN_INSTALLMENTS_DATE',0)
GO

INSERT INTO [GeneralParameters]([key], [value]) VALUES('INCREMENTAL_DURING_DAYOFFS',1)
GO

ALTER TABLE dbo.LoanInterestAccruingEvents ADD
	installment_number int NOT NULL CONSTRAINT DF_LoanInterestAccruingEvents_installment_number DEFAULT 1
GO

UPDATE [TechnicalParameters] SET [value]='v2.5.4'
GO

