USE [ProfitAndExpense]
GO
/****** Object:  Table [dbo].[TblRevenueList]    Script Date: 01/13/2010 15:03:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblRevenueList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RevenueGroupId] [int] NOT NULL,
	[SourceName] [nvarchar](50) COLLATE Cyrillic_General_CI_AS NOT NULL,
	[AmountGRN] [money] NOT NULL,
	[CreationDate] [datetime] NOT NULL CONSTRAINT [DF_TblRevenueList_CreationDate]  DEFAULT (getdate()),
	[Memo] [nvarchar](256) COLLATE Cyrillic_General_CI_AS NOT NULL,
	[ExchangeRate] [float] NOT NULL CONSTRAINT [DF_TblRevenueList_ExchangeRate]  DEFAULT ((0)),
	[CurrencyId] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL CONSTRAINT [DF_TblRevenueList_IsDeleted]  DEFAULT ((0)),
 CONSTRAINT [PK_TblRevenueList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[TblRevenueList]  WITH CHECK ADD  CONSTRAINT [FK_TblRevenueList_TblCurrencyList1] FOREIGN KEY([CurrencyId])
REFERENCES [dbo].[TblCurrencyList] ([Id])
GO
ALTER TABLE [dbo].[TblRevenueList] CHECK CONSTRAINT [FK_TblRevenueList_TblCurrencyList1]
GO
ALTER TABLE [dbo].[TblRevenueList]  WITH CHECK ADD  CONSTRAINT [FK_TblRevenueList_TblRevenueGroups] FOREIGN KEY([RevenueGroupId])
REFERENCES [dbo].[TblRevenueGroups] ([Id])
GO
ALTER TABLE [dbo].[TblRevenueList] CHECK CONSTRAINT [FK_TblRevenueList_TblRevenueGroups]