INSERT INTO [ProfitAndExpense].[dbo].[TblRevenueList]
           ([RevenueGroupId]
           ,[SourceName]
           ,[AmountGRN]
           ,[CreationDate]
           ,[Memo]
           ,[ExchangeRate]
           ,[CurrencyId]
           ,[IsDeleted])
     VALUES
           (<RevenueGroupId, int,>
           ,<SourceName, nvarchar(50),>
           ,<AmountGRN, money,>
           ,<CreationDate, datetime,>
           ,<Memo, nvarchar(256),>
           ,<ExchangeRate, float,>
           ,<CurrencyId, int,>
           ,<IsDeleted, bit,>)