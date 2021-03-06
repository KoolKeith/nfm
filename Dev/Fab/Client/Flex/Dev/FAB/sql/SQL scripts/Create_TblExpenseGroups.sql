USE [ProfitAndExpense]
GO
/****** Object:  Table [dbo].[TblExpenseGroups]    Script Date: 01/13/2010 14:59:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblExpenseGroups](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) COLLATE Cyrillic_General_CI_AS NOT NULL,
	[CreationDate] [datetime] NOT NULL CONSTRAINT [DF_TblExpenseGroups_CreationDate]  DEFAULT (getdate()),
	[IsDeleted] [bit] NOT NULL CONSTRAINT [DF_TblExpenseGroups_IsDeleted]  DEFAULT ((0)),
 CONSTRAINT [PK_TblExpenseGroups] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
