USE [ProfitAndExpense]
GO
/****** Object:  Table [dbo].[TblCurrencyList]    Script Date: 01/13/2010 14:58:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblCurrencyList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](10) COLLATE Cyrillic_General_CI_AS NOT NULL,
 CONSTRAINT [PK_TblCurrencyList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
