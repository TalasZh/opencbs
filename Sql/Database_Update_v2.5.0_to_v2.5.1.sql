CREATE TABLE [dbo].[ReportObject](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[report_name] [nvarchar](150) NOT NULL,
 CONSTRAINT [PK_ReportObject] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ReportDataObjectSource](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[report_object_id] [int] NOT NULL,
	[sub_report_name] [nvarchar](150) NOT NULL,
	[data_object_source] [nvarchar](150) NOT NULL,
	[data_object_type] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_ReportDataObject] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ReportParametrs](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](150) NOT NULL,
	[type] [nvarchar](50) NOT NULL,
	[data_object_id] [int] NOT NULL,
 CONSTRAINT [PK_Parametrs] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ReportLookUpFields](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[parametr_id] [int] NOT NULL,
	[field_to_send] [nvarchar](100) NOT NULL,
	[field_to_show] [nvarchar](100) NOT NULL,
	[data_object] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_LookUpFields] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ReportDataObjectSource]  WITH CHECK ADD  CONSTRAINT [FK_ReportDataObject_ReportObject] FOREIGN KEY([report_object_id])
REFERENCES [dbo].[ReportObject] ([id])
GO

ALTER TABLE [dbo].[ReportDataObjectSource] CHECK CONSTRAINT [FK_ReportDataObject_ReportObject]
GO

ALTER TABLE [dbo].[ReportLookUpFields]  WITH CHECK ADD  CONSTRAINT [FK_ReportLookUpFields_ReportParametrs] FOREIGN KEY([parametr_id])
REFERENCES [dbo].[ReportParametrs] ([id])
GO

ALTER TABLE [dbo].[ReportLookUpFields] CHECK CONSTRAINT [FK_ReportLookUpFields_ReportParametrs]
GO

ALTER TABLE [dbo].[ReportParametrs]  WITH CHECK ADD  CONSTRAINT [FK_ReportParametrs_ReportDataObject] FOREIGN KEY([data_object_id])
REFERENCES [dbo].[ReportDataObjectSource] ([id])
GO

ALTER TABLE [dbo].[ReportParametrs] CHECK CONSTRAINT [FK_ReportParametrs_ReportDataObject]
GO

CREATE TABLE [dbo].[LoanScale](
	[id] [int] NOT NULL,
	[ScaleMin] [int] NULL,
	[ScaleMax] [int] NULL
) ON [PRIMARY]
GO

INSERT INTO [LoanScale]([id],[ScaleMin],[ScaleMax]) VALUES(1,1,100)
INSERT INTO [LoanScale]([id],[ScaleMin],[ScaleMax]) VALUES(2,101,500)
INSERT INTO [LoanScale]([id],[ScaleMin],[ScaleMax]) VALUES(3,501,1000)
INSERT INTO [LoanScale]([id],[ScaleMin],[ScaleMax]) VALUES(4,1001,2000)
INSERT INTO [LoanScale]([id],[ScaleMin],[ScaleMax]) VALUES(5,2001,5000)
INSERT INTO [LoanScale]([id],[ScaleMin],[ScaleMax]) VALUES(6,5001,10000)
INSERT INTO [LoanScale]([id],[ScaleMin],[ScaleMax]) VALUES(7,10001,20000)
INSERT INTO [LoanScale]([id],[ScaleMin],[ScaleMax]) VALUES(8,20001,50000)
INSERT INTO [LoanScale]([id],[ScaleMin],[ScaleMax]) VALUES(9,50001,100000)
INSERT INTO [LoanScale]([id],[ScaleMin],[ScaleMax]) VALUES(10,100001,200001)
INSERT INTO [LoanScale]([id],[ScaleMin],[ScaleMax]) VALUES(11,200001,500000)
INSERT INTO [LoanScale]([id],[ScaleMin],[ScaleMax]) VALUES(12,500001,600000)
INSERT INTO [LoanScale]([id],[ScaleMin],[ScaleMax]) VALUES(13,600001,700000)
INSERT INTO [LoanScale]([id],[ScaleMin],[ScaleMax]) VALUES(14,700001,800000)
INSERT INTO [LoanScale]([id],[ScaleMin],[ScaleMax]) VALUES(15,800001,1000000)
GO

/************** VERSION ***********************/
UPDATE [TechnicalParameters] SET [value]='v2.5.1'
GO

